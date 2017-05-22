using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Windows.Media;
using DataServices.JIRA;
using Tasker.Helpers;
using Tasker.Model.QA;

namespace Tasker.Model
{
    public class DataService : IDataService
    {
        private const string SQLConnection = @"Server=172.19.10.40;Database=TaskList;User Id=glenn; Password=glenn2017!;";
        private readonly string _user = "grodriguez";

        // private const string SQLConnection = @"Server=ck01backup02;Database=TaskList;Trusted_Connection=True;";
        // private readonly string _user = AppVariables.GetValue<String>("WinUser");

        private readonly Functions _oFx = new Functions();

        private string _userArea = string.Empty;

        #region General Tasker Service

        public void GetData(Action<DataItem, Exception> callback)
        {
            // Use this to connect to the actual data service

            var item = new DataItem("Welcome to MVVM Light");
            callback(item, null);
        }

        public void GetTask(Action<IList<TaskItem>, Exception> callback)
        {
            if (_userArea == string.Empty)
            {
                UpdateUserArea();
            }

            var oTasks = new ObservableCollection<TaskItem>();
            using (var conn = new SqlConnection(SQLConnection))
            {
                const string sqlString1 =
                    "SELECT  TOP (30) T.TaskId, T.UserName, T.CategoryId, T.Task, T.CreatedDate, T.CurrentTime, T.StatusId, ISNULL(C.Color,'Black') as CategoryColor, T.SprintItem ";
                var sqlString2 =
                    "FROM  Tasks T LEFT JOIN CategoriesByUser C ON T.CategoryId = C.CategoryId and T.UserName = C.UserName where T.UserName = '" +
                    _user + "' order by  T.CreatedDate Desc";

                conn.Open();

                using (var cmd = new SqlCommand(string.Format("{0}{1}", sqlString1, sqlString2), conn)
                {
                    CommandType =
                        CommandType.Text
                })
                {
                    var reader = cmd.ExecuteReader();

                    var conv = new BrushConverter();
                    while (reader.Read())
                    {
                        var oTaskItem = new TaskItem((int)reader["TaskId"], (string)reader["CategoryId"],
                            (string)reader["Task"], (DateTime)reader["CreatedDate"],
                            (int)reader["CurrentTime"], (string)reader["StatusId"],
                            _oFx.MakeTimeString(((int)reader["CurrentTime"])),
                            _oFx.MakeDateString((DateTime)reader["CreatedDate"]), (string)reader["SprintItem"])
                        {
                            CategoryColorBrush =
                                conv.ConvertFromString((string)reader["CategoryColor"]) as SolidColorBrush
                        };

                        oTasks.Add(oTaskItem);
                    }
                }
            }

            callback?.Invoke(oTasks, null);
        }

        public bool SaveTask(TaskItem taskItem)
        {
            var conn = new SqlConnection(SQLConnection);

            string category = taskItem.Category;
            string task = taskItem.Title;
            string status = taskItem.Status;
            int pendingTaskId = taskItem.PendingTaskId;
            // DateTime createdDate = taskItem.CreatedDate;
            decimal currentTime = (taskItem.CurrentTime);
            int taskId = taskItem.TaskId;


            string sqLstringNew =
                string.Format(
                    @"INSERT INTO Tasks (UserName, CategoryId, Task, CreatedDate, CurrentTime, StatusId, PendingTaskId, SprintItem) VALUES ('{0}','{1}','{2}',getdate(),{3},'OPEN',{4},'{5}')",
                    _user, category, task, currentTime, pendingTaskId, taskItem.SprintItem.Trim());


            string sqLstringUpdate =
                string.Format(
                    @"UPDATE Tasks Set UserName = '{0}', CategoryId = '{1}', Task = '{2}', CurrentTime = {3}, StatusId = '{4}' Where Tasks.TaskId = {5}",
                    _user, category, task, currentTime, status, taskId.ToString(CultureInfo.InvariantCulture));

            var cmd = new SqlCommand((taskItem.IsNew ? sqLstringNew : sqLstringUpdate), conn) { CommandType = CommandType.Text };

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.RecordsAffected > 0)
            {
                taskItem.IsNew = false;
                conn.Close();
                return true;
            }
            conn.Close();
            return false;
        }

        public bool UndoTask(TaskItem modifiedTaskItem, TaskItem originalTaskItem)
        {
            if (originalTaskItem != null || modifiedTaskItem != null)
            {
                if (originalTaskItem != null)
                {
                    modifiedTaskItem.Title = originalTaskItem.Title;
                    modifiedTaskItem.Category = originalTaskItem.Category;
                    modifiedTaskItem.CurrentTime = originalTaskItem.CurrentTime;
                    modifiedTaskItem.Status = originalTaskItem.Status;
                    modifiedTaskItem.CategoryColorBrush = originalTaskItem.CategoryColorBrush;
                    modifiedTaskItem.CreatedDate = originalTaskItem.CreatedDate;
                }
            }
            return true;
        }

        public void GetCategory(Action<IList<CategoryItem>, Exception> callback)
        {
            var conn = new SqlConnection(SQLConnection);

            var cmd =
                new SqlCommand(
                    "Select CategoryByUserId, UserName, CategoryId, Description, Color, ISNULL(GlobalCategoryId,'') as GlobalCategoryId, IsActive  from CategoriesByUser where UserName = '" +
                    _user + "'", conn) { CommandType = CommandType.Text };

            var oCategoryItems = new ObservableCollection<CategoryItem>();
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {
                var oCategoryItem = new CategoryItem((string)reader["CategoryId"], (string)reader["Description"],
                    (string)reader["Color"], (string)reader["GlobalCategoryId"] ?? "", (bool)reader["IsActive"]);
                oCategoryItems.Add(oCategoryItem);
            }

            conn.Close();
            callback(oCategoryItems, null);
        }

        public void GetGlobalCategory(Action<IList<GlobalCategoryItem>, Exception> callback)
        {
            var conn = new SqlConnection(SQLConnection);

            var cmd = new SqlCommand("Select * from GlobalCategories Where Area = 'All' or Area = '" + _userArea + "'",
                conn) { CommandType = CommandType.Text };

            var oCategoryItems = new ObservableCollection<GlobalCategoryItem>();
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {
                var oCategoryItem = new GlobalCategoryItem((string)reader["GlobalCategoryId"],
                    (string)reader["Description"], (string)reader["Color"]);
                oCategoryItems.Add(oCategoryItem);
            }

            conn.Close();
            callback(oCategoryItems, null);
        }

        public void SaveCategory(CategoryItem categoryItem, Action<ResultDTO, Exception> callback)
        {
            var oResultDTO = new ResultDTO
            {
                HasError = false,
                Message = "Category Saved successfully",
                Operation = "Save",
                Title = "Save Category"
            };

            // Checking for empty fields

            if (String.IsNullOrEmpty(categoryItem.GlobalCategoryId) ||
                string.IsNullOrWhiteSpace(categoryItem.CategoryId))
            {
                oResultDTO.HasError = true;
                oResultDTO.Message = "Warning: Categories can't be empty";
                oResultDTO.Title = "Warning";
            }
            else
            {
                var conn = new SqlConnection(SQLConnection);
                string category = categoryItem.CategoryId;
                string description = categoryItem.Description;
                string color = categoryItem.Color;
                string globalCategory = categoryItem.GlobalCategoryId;


                string sqLstringNew =
                    string.Format(
                        @"INSERT INTO CategoriesByUser (UserName, CategoryId, Description, Color, GlobalCategoryId, IsActive) VALUES ('{0}','{1}','{2}','{3}','{4}',{5})",
                        _user, category, description, color, globalCategory, categoryItem.IsActive ? 1 : 0);


                string sqLstringUpdate =
                    string.Format(
                        @"UPDATE CategoriesByUser Set description = '{2}', color = '{3}', GlobalCategoryId = '{4}', IsActive = {5} Where UserName = '{0}' and CategoryId = '{1}'",
                        _user, category, description, color, globalCategory, categoryItem.IsActive ? 1 : 0);

                var cmd = new SqlCommand((categoryItem.IsNew ? sqLstringNew : sqLstringUpdate), conn)
                {
                    CommandType =
                        CommandType
                        .Text
                };

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow);

                if (reader.RecordsAffected > 0)
                {
                    categoryItem.IsNew = false;
                    conn.Close();
                }
                else
                {
                    oResultDTO.HasError = true;
                    oResultDTO.Message = "Error! Database";
                }

                conn.Close();
            }
            callback(oResultDTO, null);
        }

        public void GetPendingTask(Action<IList<PendingTaskItem>, Exception> callback)
        {
            var conn = new SqlConnection(SQLConnection);
            var conn2 = new SqlConnection(SQLConnection);

            const string sqlString1 =
                "SELECT  T.PendingTaskId, T.UserName, T.CategoryId, T.PendingTask, T.CreatedDate, T.DueDate, T.StatusId, T.Ocurrence, T.Risk, ISNULL(C.Color,'Black') as CategoryColor , T.EstimatedTime, T.CurrentTime ";
            string sqlString2 =
                String.Format(
                    "FROM  PendingTasks T LEFT JOIN CategoriesByUser C ON T.CategoryId = C.CategoryId and T.UserName = C.UserName where T.UserName = '{0}' and T.StatusId = 'OPEN' order by  (T.Ocurrence * T.Risk) Desc",
                    _user);


            var cmd = new SqlCommand(string.Format("{0}{1}", sqlString1, sqlString2), conn) { CommandType = CommandType.Text };

            var oTasks = new ObservableCollection<PendingTaskItem>();
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            var conv = new BrushConverter();

            while (reader.Read())
            {
                //Calculate the currentTime
                conn2.Open();
                int currentTime = 0;
                string sqlString3 =
                    String.Format(
                        "SELECT [t1].[value] AS [TotalTiempo]FROM (SELECT SUM([t0].[CurrentTime]) AS [value], [t0].[PendingTaskId] FROM [Tasks] AS [t0] WHERE [t0].[PendingTaskId] = {0} GROUP BY [t0].[PendingTaskId] ) AS [t1] ORDER BY [t1].[PendingTaskId]",
                        (int)reader["PendingTaskId"]);
                var cmd2 = new SqlCommand(sqlString3, conn2) { CommandType = CommandType.Text };

                SqlDataReader reader2 = cmd2.ExecuteReader();
                while (reader2.Read())
                {
                    currentTime = (int)reader2["TotalTiempo"];
                }
                conn2.Close();


                var oTaskItem = new PendingTaskItem((int)reader["PendingTaskId"], (string)reader["CategoryId"],
                    (string)reader["PendingTask"], (DateTime)reader["CreatedDate"],
                    (DateTime)reader["DueDate"], (string)reader["StatusId"],
                    (int)reader["Ocurrence"], (int)reader["Risk"], (int)reader["EstimatedTime"], currentTime)

                {
                    CategoryColorBrush =
                        conv.ConvertFromString((string)reader["CategoryColor"]) as SolidColorBrush
                };

                oTasks.Add(oTaskItem);
            }

            conn.Close();
            callback(oTasks, null);
        }

        public bool SavePendingTask(PendingTaskItem taskItem)
        {
            var conn = new SqlConnection(SQLConnection);
            string category = taskItem.Category;
            string task = taskItem.Title;
            string status = taskItem.Status;
            // DateTime createdDate = taskItem.CreatedDate;
            DateTime? dueDate = taskItem.DueDate;
            int ocurrence = taskItem.Ocurrence;
            int risk = taskItem.Risk;
            // int problem100 = taskItem.Problem100;
            int pendingTaskId = taskItem.PendingTaskId;
            int estimatedTime = taskItem.EstimatedTime;

            string sqLstringNew =
                string.Format(
                    @"INSERT INTO PendingTasks (UserName, CategoryId, PendingTask, CreatedDate, DueDate, Ocurrence,Risk,StatusId,EstimatedTime) VALUES ('{0}','{1}','{2}',getdate(),'{3}',{4},{5},'OPEN',{6})",
                    _user, category, task, dueDate, ocurrence, risk, estimatedTime);

            string sqLstringUpdate =
                string.Format(
                    @"UPDATE PendingTasks Set UserName = '{0}', CategoryId = '{1}', PendingTask = '{2}', DueDate = '{3}',
                                       Ocurrence = {4},Risk = {5}, StatusId = '{6}', EstimatedTime = {7} Where PendingTasks.PendingTaskId = {8}",
                    _user, category, task, dueDate, ocurrence, risk, status, estimatedTime,
                    pendingTaskId.ToString(CultureInfo.InvariantCulture));
            var cmd = new SqlCommand((taskItem.IsNew ? sqLstringNew : sqLstringUpdate), conn)
            {
                CommandType =
                    CommandType.Text
            };

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.RecordsAffected > 0)
            {
                taskItem.IsNew = false;
                conn.Close();
                return true;
            }
            conn.Close();
            return false;
        }

        public bool UndoPendingTask(PendingTaskItem modifiedTaskItem, PendingTaskItem originalTaskItem)
        {
            if (originalTaskItem != null)
            {
                if (modifiedTaskItem != null)
                {
                    modifiedTaskItem.Title = originalTaskItem.Title;
                    modifiedTaskItem.Category = originalTaskItem.Category;
                    modifiedTaskItem.DueDate = originalTaskItem.DueDate;
                    modifiedTaskItem.Ocurrence = originalTaskItem.Ocurrence;
                    modifiedTaskItem.Risk = originalTaskItem.Risk;
                    modifiedTaskItem.Problem100 = originalTaskItem.Problem100;
                    modifiedTaskItem.Status = originalTaskItem.Status;
                    modifiedTaskItem.CategoryColorBrush = originalTaskItem.CategoryColorBrush;
                    modifiedTaskItem.CreatedDate = originalTaskItem.CreatedDate;
                }
            }
            return true;
        }

        public void GetPendingTaskCategory(Action<IList<CategoryItem>, Exception> callback)
        {
            throw new NotImplementedException();
        }

        public void GetStatisticData(int datos, Action<IList<ChartCategoryItem>, Exception> callback)
        {
            var conn = new SqlConnection(SQLConnection);
            var cmd =
                new SqlCommand(
                    "SELECT T.CategoryId, sum((T.CurrentTime/60.00/60.00)) as TotalTime,ISNULL(C.Color,'Black') as CategoryColor from [Tasks] T " +
                    " LEFT JOIN CategoriesByUser C ON T.CategoryId = C.CategoryId and T.UserName = C.UserName where T.UserName = '" +
                    _user + "' Group BY T.CategoryId,C.Color",
                    conn) { CommandType = CommandType.Text };

            var oCategoryItems = new List<ChartCategoryItem>();
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            var conv = new BrushConverter();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var oPieItem = new ChartCategoryItem((string)reader["CategoryId"],
                        (decimal)reader["TotalTime"], false)
                    {
                        CategoryColorBrush =
                            conv.ConvertFromString((string)reader["CategoryColor"]) as
                            SolidColorBrush
                    };
                    oCategoryItems.Add(oPieItem);
                }
            }
            conn.Close();
            callback(oCategoryItems, null);
        }

        public void CloseTasks()
        {
            var conn = new SqlConnection(SQLConnection);
            string sqLstringUpdate =
                string.Format(
                    @"UPDATE Tasks Set  StatusId = '{0}' Where Tasks.UserName = '{1}'  and (YEAR([CreatedDate]) <> YEAR(GETDATE()) or   MONTH([CreatedDate]) <> MONTH(GETDATE()) OR Day([CreatedDate]) <> Day(GETDATE()))",
                    "CLOSED", _user);

            var cmd = new SqlCommand(sqLstringUpdate, conn) { CommandType = CommandType.Text };

            conn.Open();
            cmd.ExecuteReader(CommandBehavior.SingleRow);

            conn.Close();
        }

        public bool CheckTodayPendingTask(string pendingTaskId)
        {
            int taskId = 0;

            using (var conn = new SqlConnection(SQLConnection))
            {
                const string sqlString1 =
                    "SELECT  T.TaskId ";
                string sqlString2 =
                    "FROM  Tasks T where (MONTH(T.CreatedDate) = MONTH(GETDATE()) AND DAY(T.CreatedDate) = DAY(GETDATE()) AND YEAR(T.CreatedDate) = YEAR(GETDATE())) and T.PendingTaskId = " +
                    pendingTaskId + " and T.UserName = '" +
                    _user + "'";

                conn.Open();

                var cmd = new SqlCommand(string.Format("{0}{1}", sqlString1, sqlString2), conn)
                {
                    CommandType =
                        CommandType.Text
                };

                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    taskId = (int)reader["TaskId"];
                }
            }
            return taskId > 0;
        }

        public void GetReleaseItems(Action<IList<ReleaseItem>, Exception> callback)
        {
            AppVariables.GetValue<String>("WinUser");
            var oReleaseItems = new ObservableCollection<ReleaseItem>();
            using (var conn = new SqlConnection(SQLConnection))
            {
                const string sqlString1 =
                    "SELECT TOP (50) ReleaseId, ReleaseItemId, ExternalReference, Title, Description, CreatedDate, StatusId, UserName, Tags FROM ReleaseItems where StatusId ='OPEN'";

                conn.Open();

                var cmd = new SqlCommand(sqlString1, conn) { CommandType = CommandType.Text };


                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var oReleaseItem = new ReleaseItem((int)reader["ReleaseId"], (int)reader["ReleaseItemId"],
                        (string)reader["ExternalReference"], (string)reader["Title"],
                        (string)reader["Description"], (DateTime)reader["CreatedDate"],
                        (string)reader["StatusId"], (string)reader["UserName"],
                        (string)reader["Tags"], _oFx.MakeDateString((DateTime)reader["CreatedDate"]));


                    oReleaseItems.Add(oReleaseItem);
                }
            }

            callback(oReleaseItems, null);
        }

        public void GetIssueItems(Action<IList<IssueItem>, Exception> callback)
        {
            var js = new JiraService();
            var issuesItem = new List<IssueItem>();
            IssuesDTO issueGroup = js.GetIssues("Dummy", "x", "");

            if (issueGroup != null)
            {
                issuesItem.AddRange(issueGroup.Issues.Select(issue => new IssueItem
                {
                    Title = issue.Fields.Summary,
                    Description = issue.Fields.Description,
                    IssueId = issue.Key,
                    Status = issue.Fields.Status.Name,
                    UserName = issue.Fields.Assignee.DisplayName,
                    IssueType = issue.Fields.IssueType.Name
                }));
            }
            callback(issuesItem, null);
        }

        public List<IssueItem> GetIssues(SprintItem oSprintItem)
        {
            string apiQueryFilter;

            if (oSprintItem.OverWriteJQLFilter)
            {
                apiQueryFilter = oSprintItem.JqlFilter.Trim();
            }
            else
            {
                apiQueryFilter =
                    string.Format(
                        "rest/api/2/search?jql=PROJECT='{0}'+AND+sprint+in+openSprints()+AND+sprint+not+in+futureSprints()+AND+ISSUETYPE!='Sub-Task'+AND+issuetype!='Technical Task'+ORDER+BY+Rank+ASC",
                        oSprintItem.JIRAProject.Trim());
            }


            var js = new JiraService();
            var issuesItem = new List<IssueItem>();
            IssuesDTO issueGroup = js.GetIssues(apiQueryFilter, oSprintItem.JiraInstance, oSprintItem.Authorization);


            if (issueGroup != null && issueGroup.Issues != null)
            {
                foreach (IssueDTO issue in issueGroup.Issues)
                {
                    var issueItem = new IssueItem();

                    issueItem.Title = issue.Fields.Summary;
                    issueItem.Description = issue.Fields.Description ?? string.Empty;
                    issueItem.IssueId = issue.Key;
                    issueItem.Status = issue.Fields.Status.Name;
                    if (issue.Fields.Assignee != null)
                        issueItem.UserName = issue.Fields.Assignee.DisplayName ?? string.Empty;

                    issueItem.IssueType = issue.Fields.IssueType.Name;
                    issueItem.Link =
                        string.Format(@"{0}/secure/RapidBoard.jspa?rapidView={1}&view=detail&selectedIssue={2}",
                            oSprintItem.JiraInstance.Trim(), oSprintItem.JiraRapidView.Trim(),
                            issue.Key.Trim());


                    issuesItem.Add(issueItem);

                    List<IssueSubTaskDTO> subTasks = issue.Fields.SubTasks;
                    foreach (IssueSubTaskDTO issueSubTaskDTO in subTasks)
                    {
                        var subTaskItem = new IssueItem();

                        subTaskItem.Title = issueSubTaskDTO.Fields.Summary;
                        subTaskItem.IssueId = issueSubTaskDTO.Key;
                        subTaskItem.Status = issueSubTaskDTO.Fields.Status.Name;
                        if (issue.Fields.Assignee != null)
                            subTaskItem.UserName = issue.Fields.Assignee.DisplayName ?? string.Empty;
                        subTaskItem.IssueType = "Sub-task";
                        subTaskItem.Link =
                            string.Format(@"{0}/secure/RapidBoard.jspa?rapidView={1}&view=detail&selectedIssue={2}",
                                oSprintItem.JiraInstance.Trim(), oSprintItem.JiraRapidView.Trim(),
                                issueSubTaskDTO.Key.Trim());


                        issuesItem.Add(subTaskItem);
                    }
                }
            }
            return issuesItem;
        }

        public void GetActiveSprint(Action<IList<SprintItem>, Exception> callback)
        {
            var oSprintItems = new ObservableCollection<SprintItem>();
            using (var conn = new SqlConnection(SQLConnection))
            {
                string sqlString1 =
                    "SELECT tm.IsScrumMaster, tm.OnlyMyItems, t.Team, t.TeamId, p.DocumentationURL,p.ProjectId,p.JIRAProject,js.SprintId, js.SprintName, p.StatusId, p.Name, js.JIRAInstance,js.JQLFilter,js.OverWriteJQLFilter, js.JiraRapidView, js.AuthorizationValue FROM TeamMembers tm left JOIN JIRASprints js ON tm.TeamId = js.TeamId AND js.IsActive = 1";
                string sqlString2 =
                    " LEFT join Projects p ON js.ProjectId = p.ProjectId LEFT JOIN Teams t ON tm.TeamId = t.TeamId WHERE tm.UserName = '" +
                    _user + "'";

                conn.Open();

                var cmd = new SqlCommand(sqlString1 + sqlString2, conn) { CommandType = CommandType.Text };

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var oSprintItem = new SprintItem((int)reader["SprintId"], (int)reader["TeamId"], (string)reader["ProjectId"], (string)reader["SprintName"],
                       (string)reader["JIRAProject"], (string)reader["JIRAInstance"], (string)reader["JQLFilter"],
                        (bool)reader["OverWriteJQLFilter"], (string)reader["JiraRapidView"],
                        (string)reader["AuthorizationValue"]);

                    oSprintItems.Add(oSprintItem);
                }
            }

            callback(oSprintItems, null);
        }

        public bool CheckTodayIssueTask(string issueTaskId)
        {
            int taskId = 0;

            using (var conn = new SqlConnection(SQLConnection))
            {
                const string sqlString1 =
                    "SELECT  T.TaskId ";
                string sqlString2 =
                    "FROM  Tasks T where (MONTH(T.CreatedDate) = MONTH(GETDATE()) AND DAY(T.CreatedDate) = DAY(GETDATE()) AND YEAR(T.CreatedDate) = YEAR(GETDATE())) and T.SprintItem = '" +
                    issueTaskId.Trim() + "' and T.UserName = '" +
                    _user + "'";

                conn.Open();

                var cmd = new SqlCommand(string.Format("{0}{1}", sqlString1, sqlString2), conn)
                {
                    CommandType =
                        CommandType.Text
                };

                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    taskId = (int)reader["TaskId"];
                }
            }
            return taskId > 0;
        }

        public void GetTotalTimeByDayStatistic(int datos, string user,
            Action<IList<ChartCategoryItem>, Exception> callback)
        {
            callback(GetTimeByDate(user), null);
        }

        public List<IndividualStatistics> GetMyFollowingsStatistic()
        {
            var individualStatisticsList = new List<IndividualStatistics>();

            var individualStatistic = new IndividualStatistics("rdiscua", GetTimeByDate("rdiscua"));

            individualStatisticsList.Add(individualStatistic);

            return individualStatisticsList;
        }

        public void GetStatisticCategoryRange(string category, DateTime startDate, DateTime endDate,
            Action<IList<ChartCategoryItem>, Exception> callback)
        {
            var conn = new SqlConnection(SQLConnection);
            var cmd =
                new SqlCommand(
                    "SELECT CONVERT(VARCHAR(10),T.CreatedDate,10) as Dia, sum((T.CurrentTime/60.00/60.00)) as TotalTime from [Tasks] T " +
                    " LEFT JOIN CategoriesByUser C ON T.CategoryId = C.CategoryId and T.UserName = C.UserName where  T.CategoryId = '" +
                    category + "' and T.UserName = '" +
                    _user + "' and CreatedDate BETWEEN '" + startDate.ToString("yyyy-M-dd") + "' and '" +
                    endDate.ToString("yyyy-M-dd") + "' Group BY CONVERT(VARCHAR(10),T.CreatedDate,10)",
                    conn) { CommandType = CommandType.Text };

            var oCategoryItems = new List<ChartCategoryItem>();

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            var conv = new BrushConverter();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var oPieItem = new ChartCategoryItem((string)reader["Dia"],
                        (decimal)reader["TotalTime"], false)
                    {
                        CategoryColorBrush =
                            conv.ConvertFromString("#FF00DD00") as
                            SolidColorBrush
                    };
                    oCategoryItems.Add(oPieItem);
                }
            }

            //for (int i = 0; i < 30; i++)
            //{
            //    var oPieItem = new PieItem();
            //   var dateRun = startDate.AddDays(i).ToString("MM-dd-yy");
            //    oPieItem = oCategoryItemsDummy.SingleOrDefault(c => c.Category == dateRun) ?? new PieItem
            //                                                                                  {
            //                                                                                      Category = dateRun,
            //                                                                                      TotalTime = 0,
            //                                                                                      Exploded = false,
            //                                                                                      CategoryColorBrush = conv.ConvertFromString("#FF00DD00") as
            //                                                                                          SolidColorBrush
            //                                                                                  };
            //    oCategoryItems.Add(oPieItem);
            //}

            conn.Close();
            callback(oCategoryItems, null);
        }

        public void GetStatisticDataRange(int datos, DateTime startDate, DateTime endDate,
            Action<IList<ChartCategoryItem>, Exception> callback)
        {
            var conn = new SqlConnection(SQLConnection);
            var cmd =
                new SqlCommand(
                    "SELECT T.CategoryId, sum((T.CurrentTime/60.00/60.00)) as TotalTime,ISNULL(C.Color,'Black') as CategoryColor from [Tasks] T " +
                    " LEFT JOIN CategoriesByUser C ON T.CategoryId = C.CategoryId and T.UserName = C.UserName where T.UserName = '" +
                    _user + "' and CreatedDate BETWEEN '" + startDate.ToString("yyyy-M-dd") + "' and '" +
                    endDate.ToString("yyyy-M-dd") + "' Group BY T.CategoryId,C.Color",
                    conn) { CommandType = CommandType.Text };

            var oCategoryItems = new List<ChartCategoryItem>();
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            var conv = new BrushConverter();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var oPieItem = new ChartCategoryItem((string)reader["CategoryId"],
                        (decimal)reader["TotalTime"], false)
                    {
                        CategoryColorBrush =
                            conv.ConvertFromString((string)reader["CategoryColor"]) as
                            SolidColorBrush
                    };
                    oCategoryItems.Add(oPieItem);
                }
            }
            conn.Close();
            callback(oCategoryItems, null);
        }

        public void RecordTimeTask(TaskItem taskItem, int seconds, Action<ResultDTO, Exception> callback)
        {
            var oResultDTO = new ResultDTO
            {
                HasError = false,
                Message = "Task time successfully recorded",
                Operation = "Save",
                Title = "Save Task"
            };

            try
            {
                var conn = new SqlConnection(SQLConnection);
                string sqLstringUpdate =
                    string.Format(
                        @"UPDATE Tasks Set CurrentTime = {0}, LastUpdateDate = GetDate() Where Tasks.TaskId = {1}   
                    Update Users set TaskId = {1}, Online = 1  Where UserName = '{2}'",
                        taskItem.CurrentTime + seconds, taskItem.TaskId.ToString(CultureInfo.InvariantCulture), _user);

                var cmd = new SqlCommand(sqLstringUpdate, conn) { CommandType = CommandType.Text };

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.RecordsAffected > 0)
                {
                    conn.Close();
                }
                else
                {
                    oResultDTO.HasError = true;
                    oResultDTO.Message = "Error! Database";
                }

                conn.Close();
            }
            catch (Exception)
            {
                oResultDTO.HasError = true;
                oResultDTO.Message = "Database Connection Lost";
            }

            callback(oResultDTO, null);
        }

        private void UpdateUserArea()
        {
            using (var conn = new SqlConnection(SQLConnection))
            {
                string sqlString2 =
                    "SELECT UserName,Online,TaskId,Area FROM Users where UserName = '" + _user + "' ";

                conn.Open();

                var cmd = new SqlCommand(sqlString2, conn)
                {
                    CommandType =
                        CommandType.Text
                };


                SqlDataReader reader = cmd.ExecuteReader();
                string sAreaItem = string.Empty;

                while (reader.Read())
                {
                    sAreaItem = (string)reader["Area"];
                }
                AppVariables.SetValue("AreaUser", sAreaItem);
                _userArea = sAreaItem;
            }
        }

        private List<ChartCategoryItem> GetTimeByDate(string user)
        {
            var conn = new SqlConnection(SQLConnection);
            var cmd =
                new SqlCommand(
                    "SELECT TOP 50 [UserName],CONVERT(VARCHAR(10),CreatedDate,11) as Dia, sum((CurrentTime/60.00)/60.00) as Horas FROM dbo.Tasks Where UserName = '" +
                    user +
                    "'  group BY UserName, CONVERT(VARCHAR(10),CreatedDate,11) order by CONVERT(VARCHAR(10),CreatedDate,11) ",
                    conn) { CommandType = CommandType.Text };

            var oCategoryItems = new List<ChartCategoryItem>();
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            var conv = new BrushConverter();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var oPieItem = new ChartCategoryItem((string)reader["Dia"],
                        (decimal)reader["Horas"], false)
                    {
                        CategoryColorBrush =
                            conv.ConvertFromString("#FF00DD00") as
                            SolidColorBrush
                    };
                    oCategoryItems.Add(oPieItem);
                }
            }
            conn.Close();

            return oCategoryItems;
        }

        #endregion

        #region QA DataService

        #region TestPlan

        /// <summary>
        /// Obtiene una lista de todos las Entorno de Desarrollo (CTS.NET, Genesis, etc)
        /// </summary>
        /// <param name="callback"></param>
        public void GetAplicationQA(Action<IList<AplicationItem>, Exception> callback)
        {
            var conn = new SqlConnection(SQLConnection);

            var cmd =
                new SqlCommand(
                    "SELECT AplicationId,Description FROM AplicationQA", conn) { CommandType = CommandType.Text };

            var oCategoryItems = new ObservableCollection<AplicationItem>();
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var oCategoryItem = new AplicationItem((int)reader["AplicationId"], (string)reader["Description"]);
                oCategoryItems.Add(oCategoryItem);
            }

            conn.Close();
            callback(oCategoryItems, null);
        }

        /// <summary>
        /// Obtiene una lista de TestPlan Filtrado por Fecha y Nombre
        /// </summary>
        /// <param name="startDate">Fecha Inicial</param>
        /// <param name="endDate"> Fecha Final</param>
        /// <param name="testPlanName"> Nombre del Test Plan</param>
        /// <param name="callback"></param>
        public void GetTestPlanList(DateTime startDate, DateTime endDate, string testPlanName,
            Action<IList<TestPlanItem>, Exception> callback)
        {
            var conn = new SqlConnection(SQLConnection);
            string sqlQuery = string.Format(
                "SELECT TestPlanId, Description, Date, AplicationId, Object  FROM TestPlanQA WHERE (Date>='{0}' AND Date<='{1}') AND Description LIKE '%{2}%'",
                startDate.ToShortDateString(), endDate.ToShortDateString(), testPlanName);
            var cmd =
                new SqlCommand(sqlQuery
                    ,
                    conn) { CommandType = CommandType.Text };
            var oCategoryItems = new List<TestPlanItem>();
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {
                var testCaseItem = new TestPlanItem
                {
                    TestPlanId = (int)reader["TestPlanId"],
                    Description = (string)reader["Description"],
                    Date = (DateTime)reader["Date"],
                    AplicationId = (int)reader["AplicationId"],
                    ObjectItem = (string)reader["Object"]
                };
                oCategoryItems.Add(testCaseItem);
            }
            conn.Close();
            callback(oCategoryItems, null);
        }

        /// <summary>
        /// Retorna una lista de test plan de los utlimos 20 creados
        /// </summary>
        /// <param name="callback"></param>
        public void GetTestPlanList(Action<IList<TestPlanItem>, Exception> callback)
        {
            var conn = new SqlConnection(SQLConnection);
           // string sqlQuery = "SELECT TOP 20 TestPlanId, Description, Date, AplicationId, Object  FROM TestPlanQA ORDER BY dbo.TestPlanQA.TestPlanId DESC";
            string sqlQuery = "SELECT TOP 20 TestPlanId, Description, Date, AplicationId, (SELECT Description FROM AplicationQA WHERE AplicationId = q.AplicationId) AS NameOfAplicationTemp, Object  FROM TestPlanQA q ORDER BY TestPlanId DESC";
            var cmd =
                new SqlCommand(sqlQuery
                    ,
                    conn) { CommandType = CommandType.Text };
            var oCategoryItems = new List<TestPlanItem>();
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {
                var testCaseItem = new TestPlanItem
                {
                    TestPlanId = (int)reader["TestPlanId"],
                    Description = (string)reader["Description"],
                    Date = (DateTime)reader["Date"],
                    AplicationId = (int)reader["AplicationId"],
                    NameOfAplicationTemp = (string)reader["NameOfAplicationTemp"],
                    ObjectItem = (string)reader["Object"]
                };
                oCategoryItems.Add(testCaseItem);
            }
            conn.Close();
            callback(oCategoryItems, null);
        }

        /// <summary>
        /// Inserta un nuevo test plan a la DB
        /// </summary>
        /// <param name="testPlan"></param>
        /// <returns></returns>
        public bool SaveTestPlan(TestPlanItem testPlan)
        {
            var conn = new SqlConnection(SQLConnection);

            string sqLstringNew =
            string.Format(
            "INSERT INTO TestPlanQA (Description, AplicationId, Object) VALUES ('{0}', {1}, '{2}')", testPlan.Description, testPlan.AplicationId, testPlan.ObjectItem);

            var cmd = new SqlCommand((sqLstringNew), conn) { CommandType = CommandType.Text };

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.RecordsAffected > 0)
            {
                conn.Close();
                return true;
            }
            conn.Close();
            return false;
        }

        /// <summary>
        /// Modifica un test plan existente.
        /// </summary>
        /// <param name="testPlan"></param>
        /// <returns></returns>
        public bool EditTestPlan(TestPlanItem testPlan)
        {
            var conn = new SqlConnection(SQLConnection);

            string sqLstringNew =
            string.Format(
            "UPDATE TestPlanQA SET Description = '{0}' ,AplicationId = {1} ,Object = '{2}' WHERE TestPlanId={3}", testPlan.Description, testPlan.AplicationId, testPlan.ObjectItem, testPlan.TestPlanId);

            var cmd = new SqlCommand((sqLstringNew), conn) { CommandType = CommandType.Text };

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.RecordsAffected > 0)
            {
                conn.Close();
                return true;
            }
            conn.Close();
            return false;
        }

        #endregion

        #region TestCase

        /// <summary>
        /// Obtiene una lista de los ultimos 20 Test Case 
        /// </summary>
        /// <param name="callback"></param>
        public void GetTestCases(Action<IList<TestCaseItem>, Exception> callback)
        {
            // creating the TestCases collection 
            var oTestCases = new List<TestCaseItem>();
            var oResultDTO = new ResultDTO
            {
                HasError = false,
                Message = "Category Saved successfully",
                Operation = "Save",
                Title = "Save Category"
            };

            using (var conn = new SqlConnection(SQLConnection))
            {
                const string sqlString1 =
                    "SELECT TOP (20) TestCaseId, Description, CreatedDate, Duration, Objective, TestData, Precondition, MainScreenImage, TestPlanId FROM TestCaseHeaderQA ORDER BY TestCaseId DESC";

                conn.Open();

                var cmd = new SqlCommand(sqlString1, conn) { CommandType = CommandType.Text };


                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var oTestCaseItem = new TestCaseItem((int)reader["TestCaseId"], (string)reader["Description"],
                        (DateTime)reader["CreatedDate"], (int)reader["Duration"],
                        (string)reader["Objective"], (string)reader["TestData"], (string)reader["Precondition"],
                        (int)reader["TestPlanId"]);

                    oTestCases.Add(oTestCaseItem);
                }
            }
            callback(oTestCases, null);
        }

        /// <summary>
        /// Inserta o Modifica un test case dependiendo del estado IsNew
        /// </summary>
        /// <param name="testCaseItem"></param>
        /// <param name="callback"></param>
        public void SaveTestCase(TestCaseItem testCaseItem, Action<ResultDTO, Exception> callback)
        {
            var oResultDTO = new ResultDTO
            {
                HasError = false,
                Message = "Category Saved successfully",
                Operation = "Save",
                Title = "Save Category"
            };

            // Checking for empty fields

            if (String.IsNullOrEmpty(testCaseItem.Description) ||
                string.IsNullOrWhiteSpace(testCaseItem.TestData))
            {
                oResultDTO.HasError = true;
                oResultDTO.Message = "Warning: can't be empty";
                oResultDTO.Title = "Warning";
            }
            else
            {
                var conn = new SqlConnection(SQLConnection);

                string sqLstringNew =
                    string.Format(
                        @"INSERT INTO TestCaseHeaderQA ( Description, Duration, Objective, TestData, Precondition, TestPlanId) VALUES ('{0}',{1},'{2}','{3}','{4}',{5})",
                        testCaseItem.Description, testCaseItem.Duration, testCaseItem.Objetive, testCaseItem.TestData, testCaseItem.PreCondition, testCaseItem.TestPlanId);

                string sqLstringUpdate =
                    string.Format(
                        @"UPDATE TestCaseHeaderQA Set Description = '{1}', Duration = {2}, Objective = '{3}', TestData = '{4}' , Precondition = '{5}', TestPlanId = {6}   Where TestCaseId = {0} ",
                         testCaseItem.TestCaseId, testCaseItem.Description, testCaseItem.Duration, testCaseItem.Objetive, testCaseItem.TestData, testCaseItem.PreCondition, testCaseItem.TestPlanId);

                var cmd = new SqlCommand((testCaseItem.IsNew ? sqLstringNew : sqLstringUpdate), conn)
                {
                    CommandType =
                        CommandType
                        .Text
                };

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow);

                if (reader.RecordsAffected > 0)
                {
                    testCaseItem.IsNew = false;
                    conn.Close();
                }
                else
                {
                    oResultDTO.HasError = true;
                    oResultDTO.Message = "Error! Database";
                }
                conn.Close();
            }
            callback(oResultDTO, null);
        }

        /// <summary>
        /// Obtiene una lista de pasos asignados a un test case específico
        /// </summary>
        /// <param name="testCaseIdTemp"></param>
        /// <param name="callback"></param>
        public void GetStepList(int testCaseIdTemp, Action<IList<StepItem>, Exception> callback)
        {
            var conn = new SqlConnection(SQLConnection);

            var cmd =
                new SqlCommand(
                    string.Format(
                        "SELECT row_number() OVER (ORDER BY StepId ASC) as RowNumber, NumberOfStep, TestCaseId, StepId, Description, Input, ExpectedResult, Image, IsErasable FROM TestCaseDetailQA WHERE TestCaseId = {0} ORDER BY StepId ASC",
                        testCaseIdTemp),
                    conn) { CommandType = CommandType.Text };
            var oCategoryItems = new List<StepItem>();
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {
                var StepItem = new StepItem(
                    (Int64)reader["RowNumber"],
                    (int)reader["NumberOfStep"],
                    (int)reader["TestCaseId"],
                    (int)reader["StepId"],
                    (string)reader["Description"],
                    (string)reader["Input"],
                    (string)reader["ExpectedResult"],
                    (byte[])reader["Image"],
                    (bool)reader["IsErasable"]
                    );

                var conn2 = new SqlConnection(SQLConnection);
                using (SqlCommand cm = conn2.CreateCommand())
                {
                    cm.CommandText = @"
                                    SELECT Image
                                    FROM   TestCaseDetailQA
                                    WHERE  StepId = @Id";
                    cm.Parameters.AddWithValue("@Id", StepItem.StepId);
                    conn2.Open();
                    StepItem.Image = cm.ExecuteScalar() as byte[];
                }


                oCategoryItems.Add(StepItem);
            }
            conn.Close();
            callback(oCategoryItems, null);
        }

        /// <summary>
        /// Obtiene una lista de pasos asignados a un test case específico
        /// </summary>
        /// <param name="testCaseIdTemp"></param>
        /// <param name="callback"></param>
        public void GetStepList(int testCaseIdTemp, int _execIdTemp, Action<IList<StepItem>, Exception> callback)
        {
            var conn = new SqlConnection(SQLConnection);
            int _runExecIdTemp = GetRunExecutionFromExecId(_execIdTemp);
            var cmd =
                new SqlCommand(
                    string.Format(
                        "SELECT row_number() OVER (ORDER BY StepId ASC) as RowNumber, NumberOfStep, TestCaseId, isnull((SELECT Approved FROM RunExecutionDetailQA WHERE StepId = q.StepId  AND TestCaseID = {0} AND RunExecId = {1}), 'New') as ApproveReject, StepId, Description, Input, ExpectedResult, Image, IsErasable FROM TestCaseDetailQA q WHERE TestCaseId = {0} ORDER BY StepId ASC",
                        testCaseIdTemp, _runExecIdTemp),
                    conn) { CommandType = CommandType.Text };
            var oCategoryItems = new List<StepItem>();
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {
                var StepItem = new StepItem(
                    (Int64)reader["RowNumber"],
                    (int)reader["NumberOfStep"],
                    (int)reader["TestCaseId"],
                    (int)reader["StepId"],
                    (string)reader["Description"],
                    (string)reader["Input"],
                    (string)reader["ExpectedResult"],
                    (byte[])reader["Image"],
                    (string)reader["ApproveReject"],
                    (bool)reader["IsErasable"],
                    string.Empty,
                    string.Empty,
                    string.Empty
                    );

                var conn2 = new SqlConnection(SQLConnection);
                using (SqlCommand cm = conn2.CreateCommand())
                {
                    cm.CommandText = @"
                                    SELECT Image
                                    FROM   TestCaseDetailQA
                                    WHERE  StepId = @Id";
                    cm.Parameters.AddWithValue("@Id", StepItem.StepId);
                    conn2.Open();
                    StepItem.Image = cm.ExecuteScalar() as byte[];
                }

                //ejecutar un query que me retorne una lista del histórico de ejecuciones , Pendiente
                ObservableCollection<HistoricRunExecutionItem> newList = GetHistoricRunExecutionList(_runExecIdTemp, StepItem.TestCaseId, StepItem.StepId);
                if(newList.Count!=0)
                    foreach (var item in newList)
                    {
                        if (item.RowNumber == 1)
                            StepItem.History1 = item.Approved;
                        if (item.RowNumber == 2)
                            StepItem.History2 = item.Approved;
                        if (item.RowNumber == 3)
                            StepItem.History3 = item.Approved;
                    }

                

                oCategoryItems.Add(StepItem);
            }
            conn.Close();
            callback(oCategoryItems, null);
        }

        /// <summary>
        /// Guarda o modifíca un step en base al estado de IsNew
        /// </summary>
        /// <param name="StepItem"></param>
        /// <param name="callback"></param>
        public void SaveStep(StepItem StepItem, Action<ResultDTO, Exception> callback)
        {
            var oResultDTO = new ResultDTO
            {
                HasError = false,
                Message = "Category Saved successfully",
                Operation = "Save",
                Title = "Save Category"
            };

            // Checking for empty fields

            if (string.IsNullOrEmpty(StepItem.Description) ||
                string.IsNullOrWhiteSpace(StepItem.ExpectedResult) || string.IsNullOrEmpty(StepItem.Input))
            {
                oResultDTO.HasError = true;
                oResultDTO.Message = "Warning: can't be empty";
                oResultDTO.Title = "Warning";
            }
            else
            {
                var conn = new SqlConnection(SQLConnection);
                conn.Open();
                SqlCommand scInsert = new SqlCommand(
                    "INSERT INTO TestCaseDetailQA(TestCaseId, Description, Input, ExpectedResult, Image, NumberOfStep) VALUES(@TestCaseId, @Description, @Input, @Expected, @Image, @NumberOfStep)", conn);

                scInsert.Parameters.AddWithValue("@TestCaseId", StepItem.TestCaseId);
                scInsert.Parameters.AddWithValue("@Description", StepItem.Description);
                scInsert.Parameters.AddWithValue("@Input", StepItem.Input);
                scInsert.Parameters.AddWithValue("@Expected", StepItem.ExpectedResult);
                
                if (StepItem.Image == null)
                    StepItem.Image = new byte[0];
                scInsert.Parameters.AddWithValue("@Image", StepItem.Image);
                //scInsert.ExecuteNonQuery();

                SqlCommand scUpdate = new SqlCommand(
                    "UPDATE TestCaseDetailQA SET Description = @Description, Input = @Input, ExpectedResult = @Expected, Image = @Image, NumberOfStep = @NumberOfStep WHERE StepId = @StepId ", conn);

                scUpdate.Parameters.AddWithValue("@StepId", StepItem.StepId);
                scUpdate.Parameters.AddWithValue("@Description", StepItem.Description);
                scUpdate.Parameters.AddWithValue("@Input", StepItem.Input);
                scUpdate.Parameters.AddWithValue("@Expected", StepItem.ExpectedResult);
                scUpdate.Parameters.AddWithValue("@NumberOfStep", StepItem.NumberOfStep);
                if (StepItem.Image == null)
                    StepItem.Image = new byte[0];
                scUpdate.Parameters.AddWithValue("@Image", StepItem.Image);

                if (StepItem.NumberOfStep == 0)
                    StepItem.NumberOfStep = CountItemOfTheStepsList(StepItem.TestCaseId);
                //else esto va a ir en el eliminar Pendiente
                //    RearrangeItemOfTheStepsList(StepItem);

                scInsert.Parameters.AddWithValue("@NumberOfStep", StepItem.NumberOfStep);
                

                    if (StepItem.IsNew == true)
                    {
                        scInsert.ExecuteNonQuery();
                    }
                    else
                    {
                        scUpdate.ExecuteNonQuery();
                    }

                conn.Close();
                callback(oResultDTO, null);
            }
        }

        /// <summary>
        /// Elimina un step en base si IsErasable = true
        /// </summary>
        /// <param name="StepItem"></param>
        /// <param name="callback"></param>
        public void DeleteStep(StepItem StepItemReceiv, Action<ResultDTO, Exception> callback)
        {
            var oResultDTO = new ResultDTO
            {
                HasError = false,
                Message = "The Item Was Deleted",
                Operation = "Save",
                Title = "Save Category"
            };

            // Checking for empty fields

            if (StepItemReceiv.StepId == 0 || StepItemReceiv.IsErasable == false)
            {
                oResultDTO.HasError = true;
                oResultDTO.Message = "Warning: can't be Deleted";
                oResultDTO.Title = "Warning";
            }
            else
            {
                var conn = new SqlConnection(SQLConnection);
                conn.Open();

                SqlCommand scDelete = new SqlCommand(
                    "DELETE FROM TestCaseDetailQA WHERE StepId = @StepId", conn);
                scDelete.Parameters.AddWithValue("@StepId", StepItemReceiv.StepId);
                //if (StepItem.NumberOfStep == 0)
                //    StepItem.NumberOfStep = CountItemOfTheStepsList(StepItem.TestCaseId);

                //reordenar
                StepItem OStepItemSend = StepItemReceiv;
                RearrangeItemOfTheStepsList(OStepItemSend);
                //StepItem.StepId = StepItem.StepId - 1;
                

                scDelete.ExecuteNonQuery();
                

                conn.Close();
               // callback(oResultDTO, null);
            }
            callback(oResultDTO, null);
        }

        #endregion

        #region Execution

        /// <summary>
        /// Obtiene una lista de todos los TestPlan en el año actual.
        /// </summary>
        /// <param name="callback"></param>
        public void GetAllTestPlanListbyActualYear(Action<IList<TestPlanItem>, Exception> callback)
        {
            var conn = new SqlConnection(SQLConnection);
            var now = DateTime.Now;
            DateTime startOfYear = new DateTime(now.Year,1,1);
            DateTime EndtOfYear = new DateTime(now.Year, 12, 31);


            string sqlQuery = string.Format("SELECT TestPlanId, Description, Date, Object  FROM TestPlanQA WHERE (Date>='{0}' AND Date<='{1}') ORDER BY dbo.TestPlanQA.TestPlanId DESC", startOfYear, EndtOfYear);
            var cmd =
                new SqlCommand(sqlQuery,conn) { CommandType = CommandType.Text };
            var oCategoryItems = new List<TestPlanItem>();
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {
                var testCaseItem = new TestPlanItem
                {
                    TestPlanId = (int)reader["TestPlanId"],
                    Description = (string)reader["Description"],
                    Date = (DateTime)reader["Date"],
                    ObjectItem = (string)reader["Object"]
                };
                oCategoryItems.Add(testCaseItem);
            }
            conn.Close();
            callback(oCategoryItems, null);
        }

        /// <summary>
        /// Obtiene una lista de los ultimos Execution Creados
        /// </summary>
        /// <param name="callback"></param>
        public void GetExecution(Action<IList<ExecutionItem>, Exception> callback)
        {
            var conn = new SqlConnection(SQLConnection);
            //string sqlQuery = string.Format("SELECT TOP 50 ExecId, Description, CreatedDate, Status, EnableModify FROM ExecutionQA ORDER BY ExecId DESC");
            //string sqlQuery = string.Format("SELECT TOP 50 ExecId, Description, CreatedDate, Status, EnableModify, isnull((SELECT Porcent FROM RunExecutionQA WHERE ExecId = q.ExecId),0)as Porcent FROM ExecutionQA q ORDER BY ExecId DESC"); 
            string sqlQuery = string.Format("SELECT TOP 50 ExecId, Description, CreatedDate, Status, EnableModify, isnull((SELECT Porcent FROM RunExecutionQA WHERE ExecId = q.ExecId AND Finish = 0 AND UserName = '{0}'),-1)as Porcent FROM ExecutionQA q ORDER BY ExecId DESC", _user); 
            var cmd =
                new SqlCommand(sqlQuery, conn) { CommandType = CommandType.Text };
            var oExecutionItems = new List<ExecutionItem>();
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var executionItemTemp = new ExecutionItem
                {
                    ExecId = (int)reader["ExecId"],
                    Description = (string)reader["Description"],
                    CreatedDate = (DateTime)reader["CreatedDate"],
                    Status = (string)reader["Status"],
                    EnableModify = (bool)reader["EnableModify"],
                    Porcent = (double)reader["Porcent"]
                };
                oExecutionItems.Add(executionItemTemp);
            }
            conn.Close();
            callback(oExecutionItems, null);
        }

        /// <summary>
        /// Obtiene una lista de testCase filtrado por testplanId
        /// </summary>
        /// <param name="testPlanIdTemp"></param>
        /// <param name="callback"></param>
        public void GetTestCaseList(int testPlanIdTemp, Action<IList<TestCaseItem>, Exception> callback)
        {
            var conn = new SqlConnection(SQLConnection);

            var cmd =
                new SqlCommand(
                    string.Format(
                        "SELECT TestCaseId, Description, CreatedDate, Duration, Objective, TestPlanId FROM TestCaseHeaderQA WHERE TestPlanId = {0}",
                        testPlanIdTemp),
                    conn) { CommandType = CommandType.Text };
            var oCategoryItems = new List<TestCaseItem>();
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {
                var testCaseItem = new TestCaseItem(
                        (int)reader["TestCaseId"],
                        (string)reader["Description"],
                        (DateTime)reader["CreatedDate"],
                        (int)reader["Duration"],
                        (string)reader["Objective"],
                        (int)reader["TestPlanId"]);

                oCategoryItems.Add(testCaseItem);
            }
            conn.Close();
            callback(oCategoryItems, null);
        }

        /// <summary>
        /// Guarda o modifíca una Ejecución
        /// </summary>
        /// <param name="StepItem"></param>
        /// <param name="callback"></param>
        public void SaveExecutionHeader(ExecutionItem executionItem, Action<ResultDTO, Exception> callback)
        {
            var oResultDTO = new ResultDTO
            {
                HasError = false,
                Message = "Category Saved successfully",
                Operation = "Save",
                Title = "Save Category"
            };

            // Checking for empty fields

            if (string.IsNullOrEmpty(executionItem.Description) || string.IsNullOrWhiteSpace(executionItem.Description))
            {
                oResultDTO.HasError = true;
                oResultDTO.Message = "Warning: You must selected an item";
                oResultDTO.Title = "Warning";
            }
            else
            {
                var conn = new SqlConnection(SQLConnection);
                conn.Open();
                SqlCommand scInsert = new SqlCommand(
                    "INSERT INTO ExecutionQA ([Description], [UserName]) VALUES (@description, @UserName)", conn);
                scInsert.Parameters.AddWithValue("@description", executionItem.Description);
                scInsert.Parameters.AddWithValue("@UserName", _user);

                SqlCommand scUpdate = new SqlCommand(
                    "UPDATE ExecutionQA SET Description = @description, Status = @status, EnableModify = @enableModify WHERE ExecId= @execId", conn);

                scUpdate.Parameters.AddWithValue("@execId", executionItem.ExecId);
                scUpdate.Parameters.AddWithValue("@description", executionItem.Description);
                scUpdate.Parameters.AddWithValue("@status", executionItem.Status);
                scUpdate.Parameters.AddWithValue("@enableModify", executionItem.EnableModify);

                //scUpdate.ExecuteNonQuery();

                if (executionItem.IsNew == true)
                {
                    scInsert.ExecuteNonQuery();
                }
                else
                {
                    scUpdate.ExecuteNonQuery();
                }

                conn.Close();
                callback(oResultDTO, null);
            }
        }

        /// <summary>
        /// obtiene el último registro agregado a la tabla de Execution
        /// </summary>
        /// <param name="callback"></param>
        public void GetLastExecutionCreated(Action<ExecutionItem, Exception> callback)
        {
            var conn = new SqlConnection(SQLConnection);
            string sqlQuery = string.Format("SELECT TOP 1 ExecId, Description FROM ExecutionQA ORDER BY ExecId DESC");
            var cmd =
                new SqlCommand(sqlQuery, conn) { CommandType = CommandType.Text };
            var oExecItem = new ExecutionItem();
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var executionItemTemp = new ExecutionItem
                {
                    ExecId = (int)reader["ExecId"],
                    Description = (string)reader["Description"]
                };
                oExecItem = executionItemTemp;
            }
            conn.Close();
            callback(oExecItem, null);
        }

        /// <summary>
        /// Obtiene una lista de los testCase perteneciente a una execution
        /// </summary>
        /// <param name="callback"></param>
        public void GetExecutionDetailList(int tempExecId, Action<IList<ExecutionDetailItem>, Exception> callback)
        {
            var conn = new SqlConnection(SQLConnection);
            string sqlQuery = string.Format("SELECT TOP 50 ExecDetailId, ExecId, (SELECT Description FROM dbo.TestCaseHeaderQA WHERE TestCaseId = q.TestCaseId) as Description, TestCaseId, Status FROM ExecutionDetailQA q WHERE ExecId = {0} ORDER BY ExecId DESC", tempExecId);
            var cmd =
                new SqlCommand(sqlQuery, conn) { CommandType = CommandType.Text };
            var oExecutionItems = new List<ExecutionDetailItem>();
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var executionItemTemp = new ExecutionDetailItem
                {
                    ExecDetailId = (int)reader["ExecDetailId"],
                    ExecId = (int)reader["ExecId"],
                    Description = (string)reader["Description"],
                    TestCaseId = (int)reader["TestCaseId"],
                    Status = (bool)reader["Status"]
                };
                oExecutionItems.Add(executionItemTemp);
            }
            conn.Close();
            callback(oExecutionItems, null);
        }

        /// <summary>
        /// Inserta o elimina un item de la tabla de ExecutionDetail
        /// </summary>
        /// <param name="executionItem"></param>
        /// <param name="callback"></param> 
        public void UpdateExecutionDetailStatus(int executionId, int testCaseId, Action<ResultDTO, Exception> callback)
        {
            var oResultDTO = new ResultDTO
            {
                HasError = false,
                Message = "Category Saved successfully",
                Operation = "Save",
                Title = "Save Category"
            };

            // Checking for empty fields

            if (executionId == 0|| testCaseId==0)
            {
                oResultDTO.HasError = true;
                oResultDTO.Message = "Warning: You must selected an item";
                oResultDTO.Title = "Warning";
            }
            else
            {
                var conn = new SqlConnection(SQLConnection);
                conn.Open();
                SqlCommand scUpdate = new SqlCommand(
                    "UPDATE ExecutionDetailQA  SET Status = @Status WHERE TestCaseId = @TestCaseId AND ExecId = @ExecId", conn);
                scUpdate.Parameters.AddWithValue("@ExecId", executionId);
                scUpdate.Parameters.AddWithValue("@TestCaseId", testCaseId);
                scUpdate.Parameters.AddWithValue("@Status", true);

                scUpdate.ExecuteNonQuery();
                oResultDTO.Message = "The Item has been Updated.";

                conn.Close();
                callback(oResultDTO, null);
            }
        }

        /// <summary>
        /// Inserta o elimina un item de la tabla de ExecutionDetail
        /// </summary>
        /// <param name="executionItem"></param>
        /// <param name="callback"></param>
        public void SaveExecutionDetail(ExecutionDetailItem executionItem, Action<ResultDTO, Exception> callback)
        {
            var oResultDTO = new ResultDTO
            {
                HasError = false,
                Message = "Category Saved successfully",
                Operation = "Save",
                Title = "Save Category"
            };

            // Checking for empty fields

            if (executionItem.TestCaseId == 0)
            {
                oResultDTO.HasError = true;
                oResultDTO.Message = "Warning: You must selected an item";
                oResultDTO.Title = "Warning";
            }
            else
            {
                var conn = new SqlConnection(SQLConnection);
                conn.Open();
                SqlCommand scInsert = new SqlCommand(
                    "INSERT INTO ExecutionDetailQA (ExecId, TestCaseId, Status) VALUES (@ExecId, @TestCaseId, @Status)", conn);
                scInsert.Parameters.AddWithValue("@ExecId", executionItem.ExecId);
                scInsert.Parameters.AddWithValue("@TestCaseId", executionItem.TestCaseId);
                scInsert.Parameters.AddWithValue("@Status", false);

                SqlCommand scDelete = new SqlCommand(
                    "DELETE FROM ExecutionDetailQA WHERE ExecDetailId= @ExecDetailId", conn);

                scDelete.Parameters.AddWithValue("@ExecDetailId", executionItem.ExecDetailId);


                if (executionItem.IsErase != true)
                {
                    bool _exist = ExistExecutionDetail(executionItem.TestCaseId, executionItem.ExecId);
                    if (_exist == true)
                    {
                        oResultDTO.Message = "The item already exist.";
                    }
                    else
                    {
                        scInsert.ExecuteNonQuery();
                        oResultDTO.Message = "The Item has been saved.";
                    }

                }
                else
                {
                    if (executionItem.Status == true)
                        oResultDTO.Message = "The test case can not be deleted Because is in use.";
                    else
                    {
                        scDelete.ExecuteNonQuery();
                        oResultDTO.Message = "The Test was erased.";
                    }
                }

                conn.Close();
                callback(oResultDTO, null);
            }
        }

        /// <summary>
        /// Obtiene una lista de testCase filtrado por execId
        /// </summary>
        /// <param name="TempExecId"></param>
        /// <param name="callback"></param>
        public void GetExecutionDetailListByExecId(int TempExecId, Action<IList<ExecutionDetailItem>, Exception> callback)
        {
            var conn = new SqlConnection(SQLConnection);
            string sqlQuery = string.Format("SELECT ExecDetailId, ExecId, (SELECT Description FROM dbo.TestCaseHeaderQA WHERE TestCaseId = q.TestCaseId) as Description, (SELECT Duration FROM dbo.TestCaseHeaderQA WHERE TestCaseId = q.TestCaseId)as Duration, (SELECT Objective FROM dbo.TestCaseHeaderQA WHERE TestCaseId = q.TestCaseId) as Objective, TestCaseId, Status FROM ExecutionDetailQA q WHERE ExecId = {0} ORDER BY ExecId DESC", TempExecId);
            var cmd =
                new SqlCommand(sqlQuery, conn) { CommandType = CommandType.Text };
            var oExecutionItems = new List<ExecutionDetailItem>();
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var executionItemTemp = new ExecutionDetailItem
                {
                    ExecDetailId = (int)reader["ExecDetailId"],
                    ExecId = (int)reader["ExecId"],
                    Description = (string)reader["Description"],
                    Duration = (int)reader["Duration"],
                    Objective = (string)reader["Objective"],
                    TestCaseId = (int)reader["TestCaseId"],
                    Status = (bool)reader["Status"]
                };
                oExecutionItems.Add(executionItemTemp);
            }
            conn.Close();
            callback(oExecutionItems, null);
        }

        /// <summary>
        /// Método que guarda o Actualiza en la base de datos sin validar si existe
        /// </summary>
        /// <param name="_tempReceivedItem"></param>
        /// <param name="callback"></param>
        public void SaveRunExecutionHeader2(RunExecutionItem _tempReceivedItem, Action<ResultDTO, Exception> callback)
        {
            var oResultDTO = new ResultDTO
            {
                HasError = false,
                Message = "Category Saved successfully",
                Operation = "Save",
                Title = "Save Category"
            };

            // Checking for empty fields

            if (_tempReceivedItem.ExecId == 0)
            {
                oResultDTO.HasError = true;
                oResultDTO.Message = "Warning: Failed to save the item.";
                oResultDTO.Title = "Warning";
            }
            else
            {
                var conn = new SqlConnection(SQLConnection);
                conn.Open();
                SqlCommand scInsert = new SqlCommand(
                    "INSERT INTO RunExecutionQA ([ExecId], [Porcent], [UserName], [Finish]) VALUES (@ExecId, @Porcent, @UserName, @Finish)", conn);
                scInsert.Parameters.AddWithValue("@ExecId", _tempReceivedItem.ExecId);
                scInsert.Parameters.AddWithValue("@Porcent", 0);
                scInsert.Parameters.AddWithValue("@UserName", _user);
                scInsert.Parameters.AddWithValue("@Finish", false);

                scInsert.ExecuteNonQuery();
                oResultDTO.Message = string.Empty;

                conn.Close();
                callback(oResultDTO, null);
            }
        }

        public void CloseRunExecutionHeader(RunExecutionItem _tempReceivedItem, Action<ResultDTO, Exception> callback)
        {
            var oResultDTO = new ResultDTO
            {
                HasError = false,
                Message = "The Run Execution was successfully closed",
                Operation = "Save",
                Title = "Save Category"
            };

            // Checking for empty fields

            if (_tempReceivedItem.RunExecId == 0)
            {
                oResultDTO.HasError = true;
                oResultDTO.Message = "Warning: Failed to save the item.";
                oResultDTO.Title = "Warning";
            }
            else
            {
                var conn = new SqlConnection(SQLConnection);
                conn.Open();
                SqlCommand scUpdate = new SqlCommand(
                    "UPDATE RunExecutionQA SET [Finish] = 1, [Porcent] = 100 WHERE RunExecId = @RunExecId", conn);
                scUpdate.Parameters.AddWithValue("@RunExecId", _tempReceivedItem.RunExecId);

                scUpdate.ExecuteNonQuery();
                oResultDTO.Message = string.Empty;

                conn.Close();
                callback(oResultDTO, null);
            }
        }

        /// <summary>
        /// Método que guarda la RunExecution validando si existe para no provocar un fault en la base de datos
        /// </summary>
        /// <param name="_tempReceivedItem"></param>
        /// <param name="callback"></param>
        public void SaveRunExecutionHeader(RunExecutionItem _tempReceivedItem, Action<ResultDTO, Exception> callback)
        {
            var oResultDTO = new ResultDTO
            {
                HasError = false,
                Message = "Category Saved successfully",
                Operation = "Save",
                Title = "Save Category"
            };

            // Checking for empty fields

            if (_tempReceivedItem.ExecId == 0)
            {
                oResultDTO.HasError = true;
                oResultDTO.Message = "Warning: Failed to save the item.";
                oResultDTO.Title = "Warning";
            }
            else
            {
                var conn = new SqlConnection(SQLConnection);
                conn.Open();
                SqlCommand scInsert = new SqlCommand(
                    "INSERT INTO RunExecutionQA ([ExecId], [Porcent], [UserName], [Finish]) VALUES (@ExecId, @Porcent, @UserName, @Finish)", conn);
                scInsert.Parameters.AddWithValue("@ExecId", _tempReceivedItem.ExecId);
                scInsert.Parameters.AddWithValue("@Porcent", 0);
                scInsert.Parameters.AddWithValue("@UserName", _user);
                scInsert.Parameters.AddWithValue("@Finish", false);

                bool ExistInDb = ExistRunExecution(_tempReceivedItem.ExecId);

                if (ExistInDb != true)
                {
                    scInsert.ExecuteNonQuery();
                    oResultDTO.Message = "The item was saved successfully.";
                }

                conn.Close();
                callback(oResultDTO, null);
            }
        }

        /// <summary>
        /// Recibe el RunExecId y Devuelve el item correspondiente al RunExecId
        /// </summary>
        /// <param name="TempRunExecId"></param>
        /// <param name="callback"></param>
        public void GetRunExecutionByRunExecId(int TempRunExecId, Action<RunExecutionItem, Exception> callback)
        {
            var conn = new SqlConnection(SQLConnection);
            string sqlQuery = string.Format("SELECT [RunExecId], [ExecId], [Porcent], [LastEditDate], [UserName], [Finish] FROM RunExecutionQA WHERE RunExecId = {0}", TempRunExecId);
            var cmd =
                new SqlCommand(sqlQuery, conn) { CommandType = CommandType.Text };
            var oExecutionItems = new RunExecutionItem();
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var executionItemTemp = new RunExecutionItem
                {
                    RunExecId = (int)reader["RunExecId"],
                    ExecId = (int)reader["ExecId"],
                    Porcent = (double)reader["Porcent"],
                    LastEditDate = (DateTime)reader["LastEditDate"],
                    UserName = (string)reader["UserName"],
                    Finish = (bool)reader["Finish"]
                };
                oExecutionItems=executionItemTemp;
            }
            conn.Close();
            callback(oExecutionItems, null);
        }

        /// <summary>
        /// Método que guarda o Actualiza en la base de datos 
        /// </summary>
        /// <param name="_tempReceivedItem"></param>
        /// <param name="callback"></param>
        public void SaveRunExecutionDetail(RunExecutionDetailItem _tempReceivedItem, int _recieveExecId, Action<ResultDTO, Exception> callback)
        {
            var oResultDTO = new ResultDTO
            {
                HasError = false,
                Message = "Category Saved successfully",
                Operation = "Save",
                Title = "Save Category"
            };

            // Checking for empty fields
            _tempReceivedItem.RunExecId = GetRunExecutionFromExecId(_recieveExecId);

            if ( (_tempReceivedItem.RunExecId==0) || (_tempReceivedItem.TestCaseId==0) || (_tempReceivedItem.StepId==0))
            {
                oResultDTO.HasError = true;
                oResultDTO.Message = "Warning: Failed to save the item.";
                oResultDTO.Title = "Warning";
            }
            else
            {
                var conn = new SqlConnection(SQLConnection);
                conn.Open();
                

                if (_tempReceivedItem.RunExecId != 0)
                {
                    SqlCommand scInsert = new SqlCommand(
                        "INSERT INTO RunExecutionDetailQA ([RunExecId], [TestCaseId], [StepId], [Approved]) VALUES (@RunExecId, @TestCaseId, @StepId, @Approved)", conn);
                    scInsert.Parameters.AddWithValue("@RunExecId", _tempReceivedItem.RunExecId);
                    scInsert.Parameters.AddWithValue("@TestCaseId", _tempReceivedItem.TestCaseId);
                    scInsert.Parameters.AddWithValue("@StepId", _tempReceivedItem.StepId);
                    scInsert.Parameters.AddWithValue("@Approved", _tempReceivedItem.Approved);

                    SqlCommand scUpdate = new SqlCommand(
                        "UPDATE RunExecutionDetailQA SET Approved = @Approved WHERE (RunExecId = @RunExecId AND TestCaseId = @TestCaseId AND StepId = @StepId)", conn);

                    scUpdate.Parameters.AddWithValue("@Approved", _tempReceivedItem.Approved);
                    scUpdate.Parameters.AddWithValue("@RunExecId", _tempReceivedItem.RunExecId);
                    scUpdate.Parameters.AddWithValue("@TestCaseId", _tempReceivedItem.TestCaseId);
                    scUpdate.Parameters.AddWithValue("@StepId", _tempReceivedItem.StepId);

                    bool ExistInDb = ExistRunExecutionDetail(_tempReceivedItem.RunExecId, _tempReceivedItem.TestCaseId, _tempReceivedItem.StepId);

                    if (ExistInDb == true)
                    {
                        scUpdate.ExecuteNonQuery();
                        UpdateISErasableInStepItem(_tempReceivedItem.StepId);
                        oResultDTO.Message = "The item was updated successfully.";
                        UpdateExecutionDetailIfRunExecutionDetailId(_recieveExecId, _tempReceivedItem.TestCaseId);
                        if (_tempReceivedItem.Approved == "Approved")
                            UpdatFaultExecutionStatus(_tempReceivedItem.RunExecDetailId);
                    }
                    else
                    {
                        scInsert.ExecuteNonQuery();
                        UpdateISErasableInStepItem(_tempReceivedItem.StepId);
                        oResultDTO.Message = "The item was saved successfully.";
                        UpdateExecutionDetailIfRunExecutionDetailId(_recieveExecId, _tempReceivedItem.TestCaseId);
                        if (_tempReceivedItem.Approved == "Approved")
                            UpdatFaultExecutionStatus(_tempReceivedItem.RunExecDetailId);
                    }
                    //revisar pendiente si guarda la fecha correctamente
                    SqlCommand scUpdateHeader = new SqlCommand(
                       "UPDATE RunExecutionQA SET Porcent = @Porcent, LastEditDate = GETDATE() WHERE ExecId = @ExecId AND Finish = 0 AND UserName = @UserName", conn);

                    scUpdateHeader.Parameters.AddWithValue("@Porcent", PorcentRunExecutionDetailApproved(_recieveExecId, _tempReceivedItem.RunExecId));
                    scUpdateHeader.Parameters.AddWithValue("@ExecId", _recieveExecId);
                    scUpdateHeader.Parameters.AddWithValue("@UserName", _user);
                    
                   // scUpdateHeader.Parameters.AddWithValue("@LastEditDate", DateTime.Now);
                    scUpdateHeader.ExecuteNonQuery();
                }

                else
                {
                    oResultDTO.Message = "The item was not saved.";
                }

                conn.Close();
                callback(oResultDTO, null);
            }
        }

        /// <summary>
        /// obtiene el último registro agregado a la tabla de Execution
        /// </summary>
        /// <param name="callback"></param>
        public void GetRunExecutionDetailSaved(int _testCaseTemp, int _stepIdTemp, int _execIdTemp, Action<int, Exception> callback)
        {
            var conn = new SqlConnection(SQLConnection);
            int newRunExecId = GetRunExecutionFromExecId(_execIdTemp);
            string sqlQuery = string.Format("SELECT RunExecDetailId FROM RunExecutionDetailQA WHERE TestCaseId = {0} AND StepId = {1} AND RunExecId = {2}", _testCaseTemp, _stepIdTemp, newRunExecId);
            var cmd =
                new SqlCommand(sqlQuery, conn) { CommandType = CommandType.Text };
            //var oExecItem = new RunExecutionDetailItem();
            int _runExecDetailIdSender = new int();
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                _runExecDetailIdSender = (int)reader["RunExecDetailId"];
            }
            conn.Close();
            callback(_runExecDetailIdSender, null);
        }

        /// <summary>
        /// Guarda o modifíca un Fault Execution en DB
        /// </summary>
        /// <param name="StepItem"></param>
        /// <param name="callback"></param>
        public void SaveFaultExecutionItem(FaultExecutionItem receiveItem, Action<ResultDTO, Exception> callback)
        {
            var oResultDTO = new ResultDTO
            {
                HasError = false,
                Message = "The Item has been Saved successfully",
                Operation = "Save",
                Title = "Save Category"
            };

                var conn = new SqlConnection(SQLConnection);
                conn.Open();
                SqlCommand scInsert = new SqlCommand(
                    "INSERT INTO FaultExecutionQA ( RunExecDetailId, Comment, Image, Status, UserName) VALUES(@RunExecDetailId, @Comment, @Image, @Status, @UserName)", conn);

                scInsert.Parameters.AddWithValue("@RunExecDetailId", receiveItem.RunExecDetailId);
                scInsert.Parameters.AddWithValue("@Comment", receiveItem.Comment);
                scInsert.Parameters.AddWithValue("@Status", "Habilitado");
                scInsert.Parameters.AddWithValue("@UserName", _user);

                if (receiveItem.Image == null)
                    receiveItem.Image = new byte[0];
                scInsert.Parameters.AddWithValue("@Image", receiveItem.Image);

                SqlCommand scUpdate = new SqlCommand(
                    "UPDATE FaultExecutionQA SET Comment = @Comment, Status = @Status, Image = @Image WHERE RunExecDetailId = @RunExecDetailId AND UserName = @UserName ", conn);

                scUpdate.Parameters.AddWithValue("@Comment", receiveItem.Comment);
                scUpdate.Parameters.AddWithValue("@Status", receiveItem.Status);
                scUpdate.Parameters.AddWithValue("@RunExecDetailId", receiveItem.RunExecDetailId);
                scUpdate.Parameters.AddWithValue("@UserName", _user);
                if (receiveItem.Image == null)
                    receiveItem.Image = new byte[0];
                scUpdate.Parameters.AddWithValue("@Image", receiveItem.Image);

                if (ExistExecutionDetail(receiveItem.RunExecDetailId) == true)
                {
                    scUpdate.ExecuteNonQuery();
                }
                else
                {
                    scInsert.ExecuteNonQuery();
                }
                conn.Close();
                
            
            callback(oResultDTO, null);
        }
     
        /// <summary>
        /// Obtiene un registro en base al RunExecutionDetailId
        /// </summary>
        /// <param name="_tempRunExecutionDetailId">RunExecutionDetailId</param>
        /// <param name="callback"></param>
        public void GetFaultExecutionCreated(int _tempRunExecutionDetailId, Action<FaultExecutionItem, Exception> callback)
        {
            var conn = new SqlConnection(SQLConnection);
            var cmd =
                new SqlCommand(
                    string.Format(
                        "SELECT FaultExecId, RunExecDetailId, Comment, Image, Status FROM FaultExecutionQA WHERE RunExecDetailId = {0} AND UserName = '{1}'",
                        _tempRunExecutionDetailId, _user),
                    conn) { CommandType = CommandType.Text };
            
           FaultExecutionItem oFaultItem = new FaultExecutionItem();
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                oFaultItem = new FaultExecutionItem
                {
                    FaultExecId = (int)reader["FaultExecId"],
                    RunExecDetailId = (int)reader["RunExecDetailId"],
                    Comment = (string)reader["Comment"],
                    Image = (byte[])reader["Image"],
                    Status = (string)reader["Status"]
                };
            }
            conn.Close();
            callback(oFaultItem, null);
        }

        /// <summary>
        /// Recibe el ExecID y devuelve el RunExecId
        /// </summary>
        /// <param name="_tempReceivedItem">ExecId</param>
        /// <param name="callback"></param>
        public void GetRunExecutionFromExecutionId(int _tempReceivedItem, Action<int, Exception> callback)
        {
            var conn = new SqlConnection(SQLConnection);
            int _runExecId = 0;
            var cmd =
                new SqlCommand(
                    string.Format(
                        "SELECT RunExecId FROM RunExecutionQA WHERE ExecId = {0} AND UserName = '{1}'",
                        _tempReceivedItem, _user),
                    conn) { CommandType = CommandType.Text };
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                _runExecId = (int)reader["RunExecId"];
            }
            conn.Close();
            callback(_runExecId, null);
        }

        public void DeletesRunExecutionDetailStep(int _tempRunExecDetailId, Action<ResultDTO, Exception> callback)
        {

            var oResultDTO = new ResultDTO
            {
                HasError = false,
                Message = "The Item has been Saved successfully",
                Operation = "Save",
                Title = "Save Category"
            };

            if (_tempRunExecDetailId != 0)
            {
                var conn = new SqlConnection(SQLConnection);
                conn.Open();
                SqlCommand scDelete = new SqlCommand(
                    "DELETE FROM RunExecutionDetailQA WHERE RunExecDetailId = @_tempRunExecDetailId", conn);
                scDelete.Parameters.AddWithValue("@_tempRunExecDetailId", _tempRunExecDetailId);


                if (ExistRunExecutionDetailInFaultExecution(_tempRunExecDetailId) == 0)
                {
                    scDelete.ExecuteNonQuery();
                }
                else
                    oResultDTO.HasError = true;
            }

            callback(oResultDTO, null);

        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Se actualiza el estado cuando se corre un Aprueba un step
        /// </summary>
        /// <param name="_tempExecId"></param>
        /// <param name="_tempTestCase"></param>
        public void UpdatFaultExecutionStatus(int _tempRunExecId)
        {
            // Checking for empty fields

            if (_tempRunExecId == 0)
            {

            }
            else
            {
                var conn = new SqlConnection(SQLConnection);
                conn.Open();
                SqlCommand scUpdate = new SqlCommand(
                     "UPDATE FaultExecutionQA SET Status = @Status WHERE RunExecDetailId = @RunExecDetailId AND UserName = @UserName", conn);

                scUpdate.Parameters.AddWithValue("@Status","Deshabilitado" );
                scUpdate.Parameters.AddWithValue("@RunExecDetailId", _tempRunExecId);
                scUpdate.Parameters.AddWithValue("@UserName", _user);

                if (ExistExecutionDetail(_tempRunExecId) == true)
                {
                    scUpdate.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        /// <summary>
        /// Se actualiza el estado cuando se corre un ejecucion
        /// </summary>
        /// <param name="_tempExecId"></param>
        /// <param name="_tempTestCase"></param>
        public void UpdateExecutionDetailIfRunExecutionDetailId(int _tempExecId, int _tempTestCase)
        {
            // Checking for empty fields

            if (_tempExecId == 0 || _tempTestCase==0)
            {

            }
            else
            {
                var conn = new SqlConnection(SQLConnection);
                conn.Open();
                SqlCommand scUpdate = new SqlCommand(
                    "UPDATE ExecutionDetailQA SET Status = @Status WHERE ExecId = @ExecId AND TestCaseId = @TestCaseId", conn);

                scUpdate.Parameters.AddWithValue("@Status", true);
                scUpdate.Parameters.AddWithValue("@ExecId", _tempExecId);
                scUpdate.Parameters.AddWithValue("@TestCaseId", _tempTestCase);

                scUpdate.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Función privada que devuelve un true or false si ya existe el RunExecDetailId en la tabla de FaultExecutionQA
        /// </summary>
        /// <param name="receiveTempItem">RunExecDetailId</param>
        /// <returns></returns>
        private bool ExistExecutionDetail(int receiveTempItem)
        {
            var conn = new SqlConnection(SQLConnection);
            var cmd =
                new SqlCommand(
                    string.Format(
                        "SELECT FaultExecId FROM FaultExecutionQA WHERE RunExecDetailId = {0} AND UserName = '{1}'",
                        receiveTempItem, _user),
                    conn) { CommandType = CommandType.Text };
            var oFaultId = new int();
            oFaultId = 0;
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                oFaultId = (int)reader["FaultExecId"];
            }
            conn.Close();
            if (oFaultId == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Función privada que devuelve un true or false si ya existe el ExecutionDetail en la tabla
        /// </summary>
        /// <param name="tempTestCase"></param>
        /// <param name="tempExecId"></param>
        /// <returns></returns>
        private bool ExistExecutionDetail(int tempTestCase, int tempExecId)//recibe los parametros y retorn un bool
        {
            var conn = new SqlConnection(SQLConnection);

            var cmd =
                new SqlCommand(
                    string.Format(
                        "SELECT ExecId, TestCaseId FROM ExecutionDetailQA WHERE ExecId={0} AND TestCaseId={1}",
                        tempExecId, tempTestCase),
                    conn) { CommandType = CommandType.Text };
            var oCategoryItems = new List<ExecutionDetailItem>();
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {
                var testCaseItem = new ExecutionDetailItem(
                        (int)reader["ExecId"],
                        (int)reader["TestCaseId"]);

                oCategoryItems.Add(testCaseItem);
            }
            conn.Close();
            if (oCategoryItems.Count > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Función privada que devuelve un true or false si ya existe el RunExecutionDetail en la tabla
        /// </summary>
        /// <param name="tempTestCase"></param>
        /// <param name="tempExecId"></param>
        /// <returns></returns>
        private bool ExistRunExecutionDetail(int tempRunExecId, int tempTestCaseId,int tempStepId)
        {
            var conn = new SqlConnection(SQLConnection);

            var cmd =
                new SqlCommand(
                    string.Format(
                        "SELECT RunExecId, TestCaseId FROM RunExecutionDetailQA WHERE RunExecId = {0} AND TestCaseId = {1} AND StepId = {2}",
                        tempRunExecId, tempTestCaseId, tempStepId),
                    conn) { CommandType = CommandType.Text };
            var oRunExecDetailItems = new List<RunExecutionDetailItem>();
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {
                var tempItem = new RunExecutionDetailItem(
                        (int)reader["TestCaseId"]);

                oRunExecDetailItems.Add(tempItem);
            }
            conn.Close();
            if (oRunExecDetailItems.Count > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Recibe tres parametros y devuelve el runExecDetailId
        /// </summary>
        /// <param name="tempRunExecId">RunExecId</param>
        /// <param name="tempTestCaseId">TestCaseId</param>
        /// <param name="tempStepId">StepId</param>
        /// <returns></returns>
        private int ExistRunExecutionDetailInFaultExecution(int tempRunExecDetailId)
        {
            var conn = new SqlConnection(SQLConnection);

            var cmd =
                new SqlCommand(
                    string.Format(
                        "SELECT RunExecDetailId FROM FaultExecutionQA WHERE RunExecDetailId = {0}",
                        tempRunExecDetailId, _user),
                    conn) { CommandType = CommandType.Text };
            int oRunExecDetailItems=0;
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {
                oRunExecDetailItems = (int)reader["RunExecDetailId"];

            }
            conn.Close();

            return oRunExecDetailItems;
        }

        /// <summary>
        /// Función privada que devuelve un true or false si ya existe el RunExecution en la tabla
        /// </summary>
        /// <param name="tempTestCase"></param>
        /// <param name="tempExecId"></param>
        /// <returns></returns>
        private bool ExistRunExecution(int tempExecId)
        {
            var conn = new SqlConnection(SQLConnection);

            var cmd =
                new SqlCommand(
                    string.Format(
                        "SELECT RunExecId, Porcent FROM RunExecutionQA WHERE ExecId = {0} AND Finish = 0 AND UserName = '{1}'",
                        tempExecId, _user),
                    conn) { CommandType = CommandType.Text };
            var oRunExecutionItems = new List<RunExecutionItem>();
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {
                var tempItem = new RunExecutionItem(
                        (int)reader["RunExecId"],
                        (double)reader["Porcent"]);

                oRunExecutionItems.Add(tempItem);
            }
            conn.Close();
            if (oRunExecutionItems.Count > 0)
                return true;
            else
                return false;
        }

        private double PorcentRunExecutionDetailApproved(int tempExecId, int _tempRunExecId)
        {
            var conn = new SqlConnection(SQLConnection);
            int totalStep = 0;

            var cmd =
                new SqlCommand(
                    string.Format(
                        "SELECT TestCaseId FROM ExecutionDetailQA WHERE ExecId = {0}",
                        tempExecId),
                    conn) { CommandType = CommandType.Text };
            var oRunExecDetailItems = new List<RunExecutionDetailItem>();
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var tempItem = new RunExecutionDetailItem(
                        (int)reader["TestCaseId"]);

                oRunExecDetailItems.Add(tempItem);
            }
            conn.Close();

            foreach (var item in oRunExecDetailItems)
            {
                totalStep += CountTestCaseSteps(item.TestCaseId);
            }

            if (totalStep != 0)
            {
                int approvedItem = CountRunExecutionDetailSteps(_tempRunExecId);
                double resultado = (Convert.ToDouble(approvedItem)/ Convert.ToDouble(totalStep)) * 100;
                resultado.ToString("F");
                return resultado;
            }
            else
                return 0;
            
        }

        private int CountTestCaseSteps(int tempTestCase)
        {
            var conn = new SqlConnection(SQLConnection);
            int totalStep = 0;

            var cmd =
                new SqlCommand(
                    string.Format(
                        "SELECT count(TestCaseId) As Cantidad FROM TestCaseDetailQA WHERE TestCaseId = {0}",
                        tempTestCase),
                    conn) { CommandType = CommandType.Text };
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            
            while (reader.Read())
            {
                totalStep = (int)reader["Cantidad"];
            }
            conn.Close();
            return totalStep;
        }

        private int CountRunExecutionDetailSteps(int tempExecId)
        {
            var conn = new SqlConnection(SQLConnection);
            int totalStep = 0;

            var cmd =
                new SqlCommand(
                    string.Format(
                        "SELECT count(StepId) As Cantidad FROM RunExecutionDetailQA WHERE RunExecId = {0} AND Approved = 'Approved' ",
                        tempExecId),
                    conn) { CommandType = CommandType.Text };
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {
                totalStep = (int)reader["Cantidad"];
            }
            conn.Close();
            return totalStep;
        }

        private int GetRunExecutionFromExecId(int _tempReceivedItem)
        {
            var conn = new SqlConnection(SQLConnection);
            int _runExecId = 0;
            var cmd =
                new SqlCommand(
                    string.Format(
                        "SELECT RunExecId FROM RunExecutionQA WHERE ExecId = {0} AND Finish = 0 AND UserName = '{1}'",
                        _tempReceivedItem, _user),
                    conn) { CommandType = CommandType.Text };
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {
                _runExecId = (int)reader["RunExecId"];
            }
            conn.Close();
            return _runExecId;
        }

        private ObservableCollection<HistoricRunExecutionItem> GetHistoricRunExecutionList(int _tempRunExecId, int _tempTestCaseId, int _tempStepId)
        {
            var conn = new SqlConnection(SQLConnection);
            var cmd =
                new SqlCommand(
                    string.Format(
                        "SELECT TOP 3 row_number() OVER (ORDER BY Date DESC) as RowNumber, Approved, Date FROM HistoryOfRunExecutions WHERE TestCaseId ={0} AND RunExecId = {1} AND StepId = {2} ORDER BY Date DESC",
                        _tempTestCaseId, _tempRunExecId, _tempStepId),
                    conn) { CommandType = CommandType.Text };
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            ObservableCollection<HistoricRunExecutionItem> oHistoricList = new ObservableCollection<HistoricRunExecutionItem>();
            while (reader.Read())
            {
                var oHistoricItem = new HistoricRunExecutionItem(
                    (string)reader["Approved"],
                    (DateTime)reader["Date"],
                    (Int64)reader["RowNumber"]);

                oHistoricList.Add(oHistoricItem);
            }
            conn.Close();
            return oHistoricList;
        }

        private int CountItemOfTheStepsList(int tempTestCaseId)
        {
            var conn = new SqlConnection(SQLConnection);
            int totalStep = 0;

            var cmd =
                new SqlCommand(
                    string.Format(
                        "SELECT count(TestCaseId) As Cantidad FROM TestCaseDetailQA WHERE TestCaseId = {0}",
                        tempTestCaseId),
                    conn) { CommandType = CommandType.Text };
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {
                totalStep = (int)reader["Cantidad"];
            }
            conn.Close();
            totalStep=totalStep + 1;
            return totalStep;
        }

        private void RearrangeItemOfTheStepsList(StepItem tempStepItem)
        {
            var conn = new SqlConnection(SQLConnection);

            var cmd =
                new SqlCommand(
                    string.Format(
                        "SELECT TestCaseId, StepId, Description, Input, ExpectedResult, NumberOfStep FROM TestCaseDetailQA WHERE TestCaseId = {0}",
                        tempStepItem.TestCaseId),
                    conn) { CommandType = CommandType.Text };
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            var oCategoryItems = new List<StepItem>();

            int stepErase = 0;
            stepErase = tempStepItem.NumberOfStep;

            while (reader.Read())
            {
                StepItem oStepItem_Temp = new StepItem(
                    (int)reader["TestCaseId"],
                    (int)reader["StepId"],
                    (int)reader["NumberOfStep"]
                    );

                oCategoryItems.Add(oStepItem_Temp);
            }
            conn.Close();

            foreach (var item in oCategoryItems)//me está modificando los parámetros de 
            {
                if (item.NumberOfStep > stepErase)//que pasa cuando es 1??
                {
                    item.NumberOfStep = item.NumberOfStep - 1;
                        UpdateNumberOStepInStepList(item);
                }       
                if(item.NumberOfStep==stepErase)
                    UpdateNumberOStepInStepList(item);
            }
        }

        /// <summary>
        /// Se actualiza el NumberOfStep
        /// </summary>
        /// <param name="_tempExecId"></param>
        /// <param name="_tempTestCase"></param>
        public void UpdateNumberOStepInStepList(StepItem oStepItemTemp)
        {
            // Checking for empty fields

            if (oStepItemTemp.StepId==0)
            {

            }
            else
            {
                var conn = new SqlConnection(SQLConnection);
                conn.Open();
                SqlCommand scUpdate = new SqlCommand(
                   "UPDATE TestCaseDetailQA SET NumberOfStep = @NumberOfStep WHERE StepId = @StepId ", conn);

                scUpdate.Parameters.AddWithValue("@StepId", oStepItemTemp.StepId);
                scUpdate.Parameters.AddWithValue("@NumberOfStep", oStepItemTemp.NumberOfStep);

                scUpdate.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Se actualiza el NumberOfStep
        /// </summary>
        /// <param name="_tempExecId"></param>
        /// <param name="_tempTestCase"></param>
        public void UpdateISErasableInStepItem(int oStepIdTemp)
        {
            // Checking for empty fields

            if (oStepIdTemp == 0)
            {

            }
            else
            {
                var conn = new SqlConnection(SQLConnection);
                conn.Open();
                SqlCommand scUpdate = new SqlCommand(
                   "UPDATE TestCaseDetailQA SET IsErasable = @IsErasable WHERE StepId = @StepId ", conn);

                scUpdate.Parameters.AddWithValue("@StepId", oStepIdTemp);
                scUpdate.Parameters.AddWithValue("@IsErasable", false);

                scUpdate.ExecuteNonQuery();
            }
        }



        #endregion

        #endregion

        public void GetSprints(string projectId, Action<IList<SprintItem>, Exception> callback)
        {
            var oSprints = new ObservableCollection<SprintItem>();
            using (var conn = new SqlConnection(SQLConnection))
            {
                string sqlString = string.Format("Select js.SprintId, js.TeamId,js.ProjectId,js.SprintName,p.Name,js.CreatedDate,js.JIRAInstance,js.IsActive,"+
                                                    "js.JQLFilter,js.OverWriteJQLFilter,js.JiraRapidView, js.AuthorizationValue "+
                                                    "from JIRASprints js "+
                                                    "Left join Projects p on js.ProjectId = p.ProjectId "+
                                                    "inner join TeamMembers tm on js.TeamId = tm.TeamId and tm.UserName = '{0}' and tm.IsScrumMaster = 1 "+
                                                    "Order by js.CreatedDate Desc", _user);
                conn.Open();

                var cmd = new SqlCommand(sqlString, conn)
                {
                    CommandType = CommandType.Text
                };

                SqlDataReader reader = cmd.ExecuteReader();

                var conv = new BrushConverter();
                while (reader.Read())
                {
                    var oSprint = new SprintItem((int) reader["SprintId"], (int) reader["TeamId"],
                        (string) reader["ProjectId"], (String) reader["SprintName"],
                       (String)reader["Name"], (string)reader["JIRAInstance"], (string)reader["JQLFilter"],
                        (bool) reader["OverWriteJQLFilter"], (string) reader["JiraRapidView"],
                        (string) reader["AuthorizationValue"]);
                  
                    oSprints.Add(oSprint);
                }
            }

            callback(oSprints, null);
        }

        public void SaveSprint(SprintItem sprintItem, Action<ResultDTO, Exception> callback)
        {

            var oResultDTO = new ResultDTO
            {
                HasError = false,
                Message = "JIRA Sprint successfully saved",
                Operation = "Save",
                Title = "Save Sprint"
            };

            try
            {
            var conn = new SqlConnection(SQLConnection);

            string sqLstringNew =
                string.Format(
                    @"INSERT INTO JIRASprints (TeamId,ProjectId, SprintName,CreatedDate,JIRAInstance,IsActive,JQLFilter,OverWriteJQLFilter,JiraRapidView,AuthorizationValue) VALUES ({0},'{1}','{2}',getdate(),{3},1,'{4}','{5}','{6}','{7}')",
                    sprintItem.TeamId, sprintItem.ProjectId, sprintItem.SprintName, sprintItem.JiraInstance, sprintItem.JqlFilter, sprintItem.OverWriteJQLFilter, sprintItem.JiraRapidView, sprintItem.Authorization);


            string sqLstringUpdate =
                string.Format(
                    @"UPDATE JIRASprints Set SprintName = '{0}',JQLFilter={1}, OverWriteJQLFilter = '{2}'",
                    sprintItem.SprintName,sprintItem.JqlFilter ,sprintItem.OverWriteJQLFilter);

            var cmd = new SqlCommand((sprintItem.IsNew ? sqLstringNew : sqLstringUpdate), conn) { CommandType = CommandType.Text };

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.RecordsAffected > 0)
            {
                sprintItem.IsNew = false;
                conn.Close();
            }
            conn.Close();

             }
            catch (Exception)
            {
                oResultDTO.HasError = true;
                oResultDTO.Message = "Database Connection Lost";
            }

            callback(oResultDTO, null);
        }
    }
}