/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:Tasker"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using SLTaskList.ViewModel;
using Tasker.Model;
using Tasker.ViewModel.QA;
using Tasker.ViewModel.QA.TestCases;
using Tasker.ViewModel.Sprint;
using Tasker.ViewModel.Statistics;

namespace Tasker.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator2
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator2()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<IDataService, Design.DesignDataService>();
            }
            else
            {
                SimpleIoc.Default.Register<IDataService, DataService>();
            }

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<TaskListViewModel>();
            SimpleIoc.Default.Register<TaskViewModel>();
            SimpleIoc.Default.Register<StatusBarViewModel>();
            SimpleIoc.Default.Register<DeskTopViewModel>();
            SimpleIoc.Default.Register<TabsViewModel>();
            SimpleIoc.Default.Register<PendingTaskViewModel>();
            SimpleIoc.Default.Register<PendingTasksViewModel>();
            SimpleIoc.Default.Register<TaskRunViewModel>();
            SimpleIoc.Default.Register<ReleaseListViewModel>();
            SimpleIoc.Default.Register<IssueListViewModel>();
            SimpleIoc.Default.Register<TabTasksViewModel>();
            SimpleIoc.Default.Register<ProjectDailySummaryViewModel>();
            SimpleIoc.Default.Register<CategoriesViewModel>();
            SimpleIoc.Default.Register<CategoryViewModel>();
            SimpleIoc.Default.Register<CategoriesWorkSpaceViewModel>();
            SimpleIoc.Default.Register<StatisticsViewModel>();
            SimpleIoc.Default.Register<IssuesWorkSpaceViewModel>();
            SimpleIoc.Default.Register<TasksWorkSpaceViewModel>();
            SimpleIoc.Default.Register<SprintsViewModel>();
            SimpleIoc.Default.Register<SprintViewModel>();
            SimpleIoc.Default.Register<SprintsWorkSpaceViewModel>();



            #region TestPlan SimpleIoC
            SimpleIoc.Default.Register<TestPlanEncabezadoViewModel>();
            SimpleIoc.Default.Register<TestPlanListBoxViewModel>();
            SimpleIoc.Default.Register<TestPlanMainViewModel>();
            SimpleIoc.Default.Register<TestPlanMainViewControlBarViewModel>();
            SimpleIoc.Default.Register<TestPlanBusquedaViewModel>();
            #endregion

            #region TestCase SimpleIoC
            SimpleIoc.Default.Register<TestCaseViewModel>();
            SimpleIoc.Default.Register<TestCasesViewModel>();
            SimpleIoc.Default.Register<TestCaseControlBarViewModel>();
            SimpleIoc.Default.Register<TestCaseMainViewModel>();
            SimpleIoc.Default.Register<TestCaseStepsListControlBarViewModel>();
            SimpleIoc.Default.Register<StepListViewModel>();
            SimpleIoc.Default.Register<AddStepsControlBarViewModel>();
            SimpleIoc.Default.Register<AddStepsMainViewModel>();
            SimpleIoc.Default.Register<AddStepsViewModel>();

            #endregion

            #region Execution SimpleIoC

            SimpleIoc.Default.Register<AddExecutionTestPlanListViewModel>();
            SimpleIoc.Default.Register<AddExecutionViewModel>();
            SimpleIoc.Default.Register<ExecutionListViewModel>();
            SimpleIoc.Default.Register<ExecutionMainControlBarViewModel>();
            SimpleIoc.Default.Register<ExecutionMainViewModel>();
            SimpleIoc.Default.Register<AddExecutionTestCaseListViewModel>();
            SimpleIoc.Default.Register<AddExecutionListViewModel>();
            SimpleIoc.Default.Register<AddExecutionControlBarViewModel>();
            SimpleIoc.Default.Register<RunExecutionControlBarViewModel>();
            SimpleIoc.Default.Register<RunExecutionMainViewModel>();
            SimpleIoc.Default.Register<RunExecutionStepListViewModel>();
            SimpleIoc.Default.Register<RunExecutionTestCaseListViewModel>();
            SimpleIoc.Default.Register<ExecutionMainViewTestCaseListViewModel>();
            SimpleIoc.Default.Register<RunExecutionFaultExceptionControlBarViewModel>();
            SimpleIoc.Default.Register<RunExecutionFaultExceptionMainViewModel>();

            #endregion
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]

        #region VM Generales

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public DeskTopViewModel DeskTop
        {
            get
            {
                return ServiceLocator.Current.GetInstance<DeskTopViewModel>();
            }
        }

        public TaskListViewModel TaskList
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TaskListViewModel>();
            }
        }

        public TaskViewModel Task
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TaskViewModel>();
            }
        }

        public StatusBarViewModel StatusBar
        {
            get
            {
                return ServiceLocator.Current.GetInstance<StatusBarViewModel>();
            }
        }

        public TabsViewModel Tabs
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TabsViewModel>();
            }
        }

        public PendingTasksViewModel PendingTasks
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PendingTasksViewModel>();
            }
        }

        public PendingTaskViewModel PendingTask
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PendingTaskViewModel>();
            }
        }

        public TaskRunViewModel TaskRunViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TaskRunViewModel>();
            }
        }

        public ReleaseListViewModel ReleaseListViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ReleaseListViewModel>();
            }
        }

        public IssueListViewModel IssueListViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IssueListViewModel>();
            }
        }

        public TabTasksViewModel TabTasksViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TabTasksViewModel>();
            }
        }

        public ProjectDailySummaryViewModel ProjectDailySummaryViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ProjectDailySummaryViewModel>();
            }
        }

        public CategoriesViewModel CategoriesViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CategoriesViewModel>();
            }
        }

        public CategoryViewModel CategoryViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CategoryViewModel>();
            }
        }

        public CategoriesWorkSpaceViewModel CategoriesWorkSpaceViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CategoriesWorkSpaceViewModel>();
            }
        }

        public StatisticsViewModel StatisticsViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<StatisticsViewModel>();
            }
        }

        public IssuesWorkSpaceViewModel IssuesWorkSpaceViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IssuesWorkSpaceViewModel>();
            }
        }

        public TasksWorkSpaceViewModel TasksWorkSpaceViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TasksWorkSpaceViewModel>();
            }
        }

        public SprintsViewModel SprintsViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SprintsViewModel>();
            }
        }

        public SprintViewModel SprintViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SprintViewModel>();
            }
        }

        public SprintsWorkSpaceViewModel SprintsWorkSpaceViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SprintsWorkSpaceViewModel>();
            }
        }

        #endregion

        #region QA

            #region TestPlan

            public TestPlanMainViewControlBarViewModel TestPlanMainViewControlBarViewModel
            {
                get
                {
                    return ServiceLocator.Current.GetInstance<TestPlanMainViewControlBarViewModel>();
                }
            }

            public TestPlanMainViewModel TestPlanMainViewModel
            {
                get
                {
                    return ServiceLocator.Current.GetInstance<TestPlanMainViewModel>();
                }
            }

            public TestPlanEncabezadoViewModel TestPlanEncabezadoViewModel
            {
                get
                {
                    return ServiceLocator.Current.GetInstance<TestPlanEncabezadoViewModel>();
                }
            }

            public TestPlanListBoxViewModel TestPlanListBoxViewModel
            {
                get
                {
                    return ServiceLocator.Current.GetInstance<TestPlanListBoxViewModel>();
                }
            }

            public TestPlanBusquedaViewModel TestPlanBusquedaViewModel
            {
                get
                {
                    return ServiceLocator.Current.GetInstance<TestPlanBusquedaViewModel>();
                }
            }

            #endregion

            #region TestCase

            public TestCaseViewModel TestCaseViewModel
            {
                get
                {
                    return ServiceLocator.Current.GetInstance<TestCaseViewModel>();
                }
            }

            public TestCasesViewModel TestCasesViewModel
            {
                get
                {
                    return ServiceLocator.Current.GetInstance<TestCasesViewModel>();
                }
            }

            public TestCaseControlBarViewModel TestCaseControlBarViewModel
            {
                get
                {
                    return ServiceLocator.Current.GetInstance<TestCaseControlBarViewModel>();
                }
            }


            public TestCaseMainViewModel TestCaseMainViewModel
            {
                get
                {
                    return ServiceLocator.Current.GetInstance<TestCaseMainViewModel>();
                }
            }

            public TestCaseStepsListControlBarViewModel TestCaseStepsListControlBarViewModel
            {
                get
                {
                    return ServiceLocator.Current.GetInstance<TestCaseStepsListControlBarViewModel>();
                }
            }

            #endregion

            #region Steps

            public StepListViewModel StepListViewModel
            {
                get
                {
                    return ServiceLocator.Current.GetInstance<StepListViewModel>();
                }
            }

            public AddStepsControlBarViewModel AddStepsControlBarViewModel
            {
                get
                {
                    return ServiceLocator.Current.GetInstance<AddStepsControlBarViewModel>();
                }
            }

            public AddStepsMainViewModel AddStepsMainViewModel
            {
                get
                {
                    return ServiceLocator.Current.GetInstance<AddStepsMainViewModel>();
                }
            }

            public AddStepsViewModel AddStepsViewModel
            {
                get
                {
                    return ServiceLocator.Current.GetInstance<AddStepsViewModel>();
                }
            }

            #endregion

            #region Execution

            public AddExecutionTestPlanListViewModel AddExecutionTestPlanListViewModel
            {
                get
                {
                    return ServiceLocator.Current.GetInstance<AddExecutionTestPlanListViewModel>();
                }
            }

            public AddExecutionViewModel AddExecutionViewModel
            {
                get
                {
                    return ServiceLocator.Current.GetInstance<AddExecutionViewModel>();
                }
            }

            public ExecutionListViewModel ExecutionListViewModel
            {
                get
                {
                    return ServiceLocator.Current.GetInstance<ExecutionListViewModel>();
                }
            }

            public ExecutionMainControlBarViewModel ExecutionMainControlBarViewModel
            {
                get
                {
                    return ServiceLocator.Current.GetInstance<ExecutionMainControlBarViewModel>();
                }
            }

            public ExecutionMainViewModel ExecutionMainViewModel
            {
                get
                {
                    return ServiceLocator.Current.GetInstance<ExecutionMainViewModel>();
                }
            }

            public AddExecutionTestCaseListViewModel AddExecutionTestCaseListViewModel
            {
                get
                {
                    return ServiceLocator.Current.GetInstance<AddExecutionTestCaseListViewModel>();
                }
            }

            public AddExecutionListViewModel AddExecutionListViewModel
            {
                get
                {
                    return ServiceLocator.Current.GetInstance<AddExecutionListViewModel>();
                }
            }

            public AddExecutionControlBarViewModel AddExecutionControlBarViewModel
            {
                get
                {
                    return ServiceLocator.Current.GetInstance<AddExecutionControlBarViewModel>();
                }
            }

            public RunExecutionControlBarViewModel RunExecutionControlBarViewModel
            {
                get
                {
                    return ServiceLocator.Current.GetInstance<RunExecutionControlBarViewModel>();
                }
            }

            public RunExecutionMainViewModel RunExecutionMainViewModel
            {
                get
                {
                    return ServiceLocator.Current.GetInstance<RunExecutionMainViewModel>();
                }
            }

            public RunExecutionStepListViewModel RunExecutionStepListViewModel
            {
                get
                {
                    return ServiceLocator.Current.GetInstance<RunExecutionStepListViewModel>();
                }
            }

            public RunExecutionTestCaseListViewModel RunExecutionTestCaseListViewModel
            {
                get
                {
                    return ServiceLocator.Current.GetInstance<RunExecutionTestCaseListViewModel>();
                }
            }

            public ExecutionMainViewTestCaseListViewModel ExecutionMainViewTestCaseListViewModel
            {
                get
                {
                    return ServiceLocator.Current.GetInstance<ExecutionMainViewTestCaseListViewModel>();
                }
            }

            public RunExecutionFaultExceptionMainViewModel RunExecutionFaultExceptionMainViewModel
            {
                get
                {
                    return ServiceLocator.Current.GetInstance<RunExecutionFaultExceptionMainViewModel>();
                }
            }

            public RunExecutionFaultExceptionControlBarViewModel RunExecutionFaultExceptionControlBarViewModel
            {
                get
                {
                    return ServiceLocator.Current.GetInstance<RunExecutionFaultExceptionControlBarViewModel>();
                }
            }

            #endregion

        #endregion



            public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}