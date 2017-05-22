using System;
using System.Collections.Generic;
using System.Globalization;
using Tasker.Model;
using Tasker.Model.QA;

namespace Tasker.Design
{
    public class DesignDataService : IDataService
    {
        #region General Tasker Methods

        public void GetData(Action<DataItem, Exception> callback)
        {
            // Use this to create design time data

            var item = new DataItem("Welcome to MVVM Light [design]");
            callback(item, null);
        }


        public void GetTask(Action<IList<TaskItem>, Exception> callback)
        {
            List<TaskItem> TaskItems = new List<TaskItem>();
            for (int i = 0; i < 15; i++)
            {
                TaskItems.Add(CreateTask(i));
            }
            callback(TaskItems, null);
        }

        public static TaskItem CreateTask(int index)
        {
            TaskItem oTaskItem = new TaskItem(1, "Proyecto", "Nombre de la tarea Tarea en la que se trabaja" + index, DateTime.Now, 15, "OPEN", "0:15", "02-22-2012", "CT-01");
            return oTaskItem;
        }


        public bool SaveTask(TaskItem taskItem)
        {
            throw new NotImplementedException();
        }

        public bool UndoTask(TaskItem modifiedTaskItem, TaskItem originalTaskItem)
        {
            throw new NotImplementedException();
        }


        public void GetCategory(Action<IList<CategoryItem>, Exception> callback)
        {
            var taskItems = new List<CategoryItem>();
            for (var i = 0; i < 5; i++)
            {
                taskItems.Add(new CategoryItem() { CategoryId = i.ToString(CultureInfo.InvariantCulture), Description = "Category " + i, Color = "#FFFFA500" });
            }
            callback(taskItems, null);
        }


        public void SaveCategory(CategoryItem categoryItem, Action<ResultDTO, Exception> callback)
        {
            throw new NotImplementedException();
        }

        public void GetPendingTask(Action<IList<PendingTaskItem>, Exception> callback)
        {
            List<PendingTaskItem> TaskItems = new List<PendingTaskItem>();
            for (int i = 0; i < 15; i++)
            {
                TaskItems.Add(CreatePendingTask(i));
            }
            callback(TaskItems, null);
        }

        public static PendingTaskItem CreatePendingTask(int i)
        {
            PendingTaskItem oTaskItem = new PendingTaskItem(1, "Urgente", "Nombre de la Tarea Pendiente en la que se trabajara " + i, DateTime.Now, DateTime.Now.AddDays(5), "NEW", 4, 9, 60, 20);
            return oTaskItem;

        }

        public bool SavePendingTask(PendingTaskItem taskItem)
        {
            throw new NotImplementedException();
        }

        public bool UndoPendingTask(PendingTaskItem modifiedTaskItem, PendingTaskItem originalTaskItem)
        {
            throw new NotImplementedException();
        }

        public void GetPendingTaskCategory(Action<IList<CategoryItem>, Exception> callback)
        {
            throw new NotImplementedException();
        }


        public void GetStatisticData(int datos, Action<IList<ChartItem>, Exception> callback)
        {
            throw new NotImplementedException();
        }


        public void CloseTasks()
        {
            throw new NotImplementedException();
        }


        public bool CheckTodayPendingTask(string pendingTaskId)
        {
            throw new NotImplementedException();
        }


        public void GetReleaseItems(Action<IList<ReleaseItem>, Exception> callback)
        {
            throw new NotImplementedException();
        }


        public void GetIssueItems(Action<IList<IssueItem>, Exception> callback)
        {
            throw new NotImplementedException();
        }


        public List<IssueItem> GetIssues(SprintItem oSprintItem)
        {
            throw new NotImplementedException();
        }


        public void GetSprints(Action<IList<SprintItem>, Exception> callback)
        {
            throw new NotImplementedException();
        }


        public bool CheckTodayIssueTask(string issueTaskId)
        {
            throw new NotImplementedException();
        }


        public void GetTotalTimeByDayStatistic(int datos, string user, Action<IList<ChartItem>, Exception> callback)
        {
            throw new NotImplementedException();
        }

        public List<IndividualStatistics> GetMyFollowingsStatistic()
        {
            throw new NotImplementedException();
        }


        public void GetStatisticCategoryRange(string category, DateTime startDate, DateTime endDate, Action<IList<ChartItem>, Exception> callback)
        {
            throw new NotImplementedException();
        }

        public void GetStatisticDataRange(int datos, DateTime startDate, DateTime endDate, Action<IList<ChartItem>, Exception> callback)
        {
            throw new NotImplementedException();
        }


        public void GetStatisticData(int datos, Action<IList<ChartCategoryItem>, Exception> callback)
        {
            throw new NotImplementedException();
        }

        public void GetStatisticCategoryRange(string category, DateTime startDate, DateTime endDate, Action<IList<ChartCategoryItem>, Exception> callback)
        {
            throw new NotImplementedException();
        }

        public void GetStatisticDataRange(int datos, DateTime startDate, DateTime endDate, Action<IList<ChartCategoryItem>, Exception> callback)
        {
            throw new NotImplementedException();
        }

        public void GetTotalTimeByDayStatistic(int datos, string user, Action<IList<ChartCategoryItem>, Exception> callback)
        {
            throw new NotImplementedException();
        }


        public void GetGlobalCategory(Action<IList<GlobalCategoryItem>, Exception> callback)
        {
            throw new NotImplementedException();
        }


        public void RecordTimeTask(TaskItem taskItem, int seconds, Action<ResultDTO, Exception> callback)
        {
            throw new NotImplementedException();
        }



        #endregion


        #region QA

        #region TestPlan
        
        public void GetAplicationQA(Action<IList<Model.QA.AplicationItem>, Exception> callback)
        {
            throw new NotImplementedException();
        }


        public bool SaveTestPlan(TestPlanItem testPlan)
        {
            throw new NotImplementedException();
        }

        public void GetTestPlanList(DateTime startDate, DateTime endDate, string testPlanName, Action<IList<TestPlanItem>, Exception> callback)
        {
            throw new NotImplementedException();
        }

        public void GetTestPlanList(Action<IList<TestPlanItem>, Exception> callback)
        {
            var taskItems = new List<TestPlanItem>();
            for (var i = 0; i < 5; i++)
            {
                taskItems.Add(CreateTestPlanItem(i));
            }
            callback(taskItems, null);
        }

        public bool EditTestPlan(TestPlanItem testPlan)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region TestCase

        public static TestPlanItem CreateTestPlanItem(int index)
        {
            var oTaskItem = new TestPlanItem()
            {
                TestPlanId = index,
                Description = "Test Plan Description Design Data " + index,
                Date = DateTime.Now,
                AplicationId = 1,
                ObjectItem = "View.XAML",
            };
            return oTaskItem;
        }

        public void GetTestCases(Action<IList<TestCaseItem>, Exception> callback)
        {
            var TestCasesItems = new List<TestCaseItem>();
            for (int i = 0; i < 15; i++)
            {
                TestCasesItems.Add(CreateTestCase(i));
            }
            callback(TestCasesItems, null);
        }

        TestCaseItem CreateTestCase(int index)
        {
            var oTestCaseItem = new TestCaseItem(index, "Test Case" + index, DateTime.Now, 15, "Run Test", "Debe ingresar solo multiplos de 2", "Debe inicia desde 8", 100);
            return oTestCaseItem;
        }

        public void SaveTestCase(TestCaseItem testCaseItem, Action<ResultDTO, Exception> callback)
        {
            throw new NotImplementedException();
        }

        public static StepItem CreateStepItemItem(int index)
        {
            var oStepItem = new StepItem()
            {
                Description = "Step Item Description" + index,
                RowNumber = index,
                TestCaseId = 10,
                StepId = index,
                Input = "Step Item input" + index,
                ExpectedResult = "Step Item Result" + index,
            };
            return oStepItem;
        }

        public void SaveStep(StepItem testCaseItem, Action<ResultDTO, Exception> callback)
        {
            throw new NotImplementedException();
        }

        public void GetStepList(int testPlanIdTemp, Action<IList<StepItem>, Exception> callback)
        {
            var stepItems = new List<StepItem>();
            for (var i = 0; i < 5; i++)
            {
                stepItems.Add(CreateStepItemItem(i));
            }
            callback(stepItems, null);
        }

        public void GetStepList(int testCaseIdTemp, int _execIdTemp, Action<IList<StepItem>, Exception> callback)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Execution

        public void GetAllTestPlanListbyActualYear(Action<IList<TestPlanItem>, Exception> callback)
        {
            throw new NotImplementedException();
        }
        public void GetTestCaseList(int testPlanIdTemp, Action<IList<TestCaseItem>, Exception> callback)
        {
            throw new NotImplementedException();
        }
        public void SaveExecutionHeader(ExecutionItem testCaseItem, Action<ResultDTO, Exception> callback)
        {
            throw new NotImplementedException();
        }
        public void GetLastExecutionCreated(Action<ExecutionItem, Exception> callback)
        {
            throw new NotImplementedException();
        }
        public void GetExecution(Action<IList<ExecutionItem>, Exception> callback)
        {
            throw new NotImplementedException();
        }

        public void SaveExecutionDetail(ExecutionDetailItem testCaseItem, Action<ResultDTO, Exception> callback)
        {
            throw new NotImplementedException();
        }

        public void UpdateExecutionDetailStatus(int executionId, int testCaseId, Action<ResultDTO, Exception> callback)
        {
            throw new NotImplementedException();
        }

        public void GetExecutionDetailList(int tempExecId,Action<IList<ExecutionDetailItem>, Exception> callback)
        {
            throw new NotImplementedException();
        }

        public void GetExecutionDetailListByExecId(int TempExecId, Action<IList<ExecutionDetailItem>, Exception> callback)
        {
            throw new NotImplementedException();
        }

        public void SaveRunExecutionHeader(RunExecutionItem _tempReceivedItem, Action<ResultDTO, Exception> callback)
        {
            throw new NotImplementedException();
        }

        public void SaveRunExecutionDetail(RunExecutionDetailItem _tempReceivedItem, int _recieveExecId, Action<ResultDTO, Exception> callback)
        {
            throw new NotImplementedException();
        }

        public void GetRunExecutionDetailSaved(int _testCaseTemp, int _stepIdTemp, int _execIdTemp, Action<int, Exception> callback)
        {
            throw new NotImplementedException();
        }

        public void SaveFaultExecutionItem(FaultExecutionItem receiveItem, Action<ResultDTO, Exception> callback)
        {
            throw new NotImplementedException();
        }

        public void GetFaultExecutionCreated(int _tempRunExecutionDetailId, Action<FaultExecutionItem, Exception> callback)
        {
            throw new NotImplementedException();
        }

        public void DeleteStep(StepItem StepItem, Action<ResultDTO, Exception> callback)
        {
            throw new NotImplementedException();
        }

        public void SaveRunExecutionHeader2(RunExecutionItem _tempReceivedItem, Action<ResultDTO, Exception> callback)
        {
            throw new NotImplementedException();
        }

        public void GetRunExecutionByRunExecId(int TempRunExecId, Action<RunExecutionItem, Exception> callback)
        {
            throw new NotImplementedException();
        }


        public void GetRunExecutionFromExecutionId(int _tempReceivedItem, Action<int, Exception> callback)
        {
            throw new NotImplementedException();
        }


        public void CloseRunExecutionHeader(RunExecutionItem _tempReceivedItem, Action<ResultDTO, Exception> callback)
        {
            throw new NotImplementedException();
        }

        #endregion


        #endregion


        public void DeletesRunExecutionDetailStep(int _tempRunExecDetailId, Action<ResultDTO, Exception> callback)
        {
            throw new NotImplementedException();
        }


        public void GetSprints(string projectId, Action<IList<SprintItem>, Exception> callback)
        {
            var sprintItems = new List<SprintItem>();
            for (int i = 0; i < 15; i++)
            {
                sprintItems.Add(CreateSprint(i));
            }
            callback(sprintItems, null);
        }


        public static SprintItem CreateSprint(int index)
        {
            var oSprintItem = new SprintItem(index,index,"Project-"+index,"Sprint "+index,"JIRAProject","JIRAInstance","JQLFilter",true,"","TotalAccess");
            return oSprintItem;
        }


        public void SaveSprint(SprintItem sprintItem, Action<ResultDTO, Exception> callback)
        {
            throw new NotImplementedException();
        }



        public void GetActiveSprint(Action<IList<SprintItem>, Exception> callback)
        {
            throw new NotImplementedException();
        }
    }
}