using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tasker.Model.QA;

namespace Tasker.Model
{
    public interface IDataService
    {
        #region Tasker General Methods

        void GetData(Action<DataItem, Exception> callback);
        void GetTask(Action<IList<TaskItem>, Exception> callback);
        bool SaveTask(TaskItem taskItem);
        void RecordTimeTask(TaskItem taskItem, int seconds, Action<ResultDTO, Exception> callback);

        void CloseTasks();
        bool UndoTask(TaskItem modifiedTaskItem, TaskItem originalTaskItem);

        void GetCategory(Action<IList<CategoryItem>, Exception> callback);
        void GetGlobalCategory(Action<IList<GlobalCategoryItem>, Exception> callback);
        void SaveCategory(CategoryItem categoryItem, Action<ResultDTO, Exception> callback);

        void GetPendingTask(Action<IList<PendingTaskItem>, Exception> callback);
        bool SavePendingTask(PendingTaskItem taskItem);
        bool UndoPendingTask(PendingTaskItem modifiedTaskItem, PendingTaskItem originalTaskItem);
        void GetPendingTaskCategory(Action<IList<CategoryItem>, Exception> callback);
        bool CheckTodayPendingTask(string pendingTaskId);
        bool CheckTodayIssueTask(string issueTaskId);


        void GetSprints(string projectId,Action<IList<SprintItem>, Exception> callback);
        void SaveSprint(SprintItem sprintItem, Action<ResultDTO, Exception> callback);

        void GetReleaseItems(Action<IList<ReleaseItem>, Exception> callback);

        void GetIssueItems(Action<IList<IssueItem>, Exception> callback);

        List<IssueItem> GetIssues(SprintItem oSprintItem);

        // Statistic section
        // YTD Statistic
        void GetStatisticData(int datos, Action<IList<ChartCategoryItem>, Exception> callback);
        // Get the Category Range
        void GetStatisticCategoryRange(string category, DateTime startDate, DateTime endDate, Action<IList<ChartCategoryItem>, Exception> callback);
        // Get the Static Data
        void GetStatisticDataRange(int datos, DateTime startDate, DateTime endDate, Action<IList<ChartCategoryItem>, Exception> callback);

        void GetTotalTimeByDayStatistic(int datos, string user, Action<IList<ChartCategoryItem>, Exception> callback);
        List<IndividualStatistics> GetMyFollowingsStatistic();


        void GetActiveSprint(Action<IList<SprintItem>, Exception> callback);

        #endregion
     
        #region QA

        #region TestPlan

        void GetAplicationQA(Action<IList<AplicationItem>, Exception> callback);
        bool SaveTestPlan(TestPlanItem testPlan);
        void GetTestPlanList(DateTime startDate, DateTime endDate, string testPlanName, Action<IList<TestPlanItem>, Exception> callback);
        void GetTestPlanList(Action<IList<TestPlanItem>, Exception> callback);
        bool EditTestPlan(TestPlanItem testPlan);

        #endregion

        #region TestCases

        void GetTestCases(Action<IList<TestCaseItem>, Exception> callback);
        void SaveTestCase(TestCaseItem testCaseItem, Action<ResultDTO, Exception> callback);
        void GetStepList(int testCaseIdTemp, Action<IList<StepItem>, Exception> callback); 
        void GetStepList(int testCaseIdTemp, int _execIdTemp, Action<IList<StepItem>, Exception> callback);
        void SaveStep(StepItem testCaseItem, Action<ResultDTO, Exception> callback);
        void DeleteStep(StepItem StepItem, Action<ResultDTO, Exception> callback);

        #endregion

        #region Execution

        void GetAllTestPlanListbyActualYear(Action<IList<TestPlanItem>, Exception> callback);
        void GetTestCaseList(int testPlanIdTemp, Action<IList<TestCaseItem>, Exception> callback);
        void SaveExecutionHeader(ExecutionItem testCaseItem, Action<ResultDTO, Exception> callback);
        void SaveExecutionDetail(ExecutionDetailItem testCaseItem, Action<ResultDTO, Exception> callback);
        void GetLastExecutionCreated(Action<ExecutionItem, Exception> callback);
        void GetExecution(Action<IList<ExecutionItem>, Exception> callback);
        void GetExecutionDetailList(int tempExecId, Action<IList<ExecutionDetailItem>, Exception> callback);
        void UpdateExecutionDetailStatus(int executionId, int testCaseId, Action<ResultDTO, Exception> callback);
        void GetRunExecutionByRunExecId(int TempRunExecId, Action<RunExecutionItem, Exception> callback);
        void GetExecutionDetailListByExecId(int TempExecId, Action<IList<ExecutionDetailItem>, Exception> callback);
        void SaveRunExecutionHeader(RunExecutionItem _tempReceivedItem, Action<ResultDTO, Exception> callback);
        void SaveRunExecutionHeader2(RunExecutionItem _tempReceivedItem, Action<ResultDTO, Exception> callback);
        void CloseRunExecutionHeader(RunExecutionItem _tempReceivedItem, Action<ResultDTO, Exception> callback);
        void SaveRunExecutionDetail(RunExecutionDetailItem _tempReceivedItem, int _recieveExecId, Action<ResultDTO, Exception> callback);
        void GetRunExecutionDetailSaved(int _testCaseTemp, int _stepIdTemp, int _execIdTemp, Action<int, Exception> callback);
        void SaveFaultExecutionItem(FaultExecutionItem receiveItem, Action<ResultDTO, Exception> callback);
        void GetFaultExecutionCreated(int _tempRunExecutionDetailId, Action<FaultExecutionItem, Exception> callback);
        void GetRunExecutionFromExecutionId(int _tempReceivedItem, Action<int, Exception> callback);
        void DeletesRunExecutionDetailStep(int _tempRunExecDetailId, Action<ResultDTO, Exception> callback);
        
        #endregion

        #endregion

    }
}
