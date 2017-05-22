using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tasker.Model;
using Tasker.Model.QA;

namespace Tasker.ViewModel.QA.TestCases
{
    public class AddExecutionTestCaseListViewModel: ViewModelBase
    {
        private readonly IDataService _dataService;
        public AddExecutionTestCaseListViewModel(IDataService dataService)
        {
            _dataService = dataService;
            DelegarComandos();
        }

        #region Properties

        /// <summary>
        /// The <see cref="TestCases" /> property's name.
        /// </summary>
        public const string TestCasesPropertyName = "TestCases";

        private ObservableCollection<TestCaseItem> _testCases = new ObservableCollection<TestCaseItem>();

        /// <summary>
        /// Sets and gets the TestCases property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<TestCaseItem> TestCases
        {
            get
            {
                return _testCases;
            }

            set
            {
                if (_testCases == value)
                {
                    return;
                }

                RaisePropertyChanging(TestCasesPropertyName);
                _testCases = value;
                RaisePropertyChanged(TestCasesPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SelectedTestCaseItem" /> property's name.
        /// </summary>
        public const string SelectedTestCaseItemPropertyName = "SelectedTestCaseItem";

        private TestCaseItem _selectedTestCaseItem;

        /// <summary>
        /// Sets and gets the SelectedTestCaseItem property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public TestCaseItem SelectedTestCaseItem
        {
            get
            {
                return _selectedTestCaseItem;
            }

            set
            {
                if (_selectedTestCaseItem == value)
                {
                    return;
                }

                RaisePropertyChanging(SelectedTestCaseItemPropertyName);
                _selectedTestCaseItem = value;
                RaisePropertyChanged(SelectedTestCaseItemPropertyName);
                if (SelectedTestCaseItem != null)
                {
                    Messenger.Default.Send<TestCaseItem>(SelectedTestCaseItem, "SET_RECEIVEDTESTCASE_ADDEXECUTIONVIEWMODEL");
                    Messenger.Default.Send<bool>(true, "SET_ENABLEADDTESTCASEBUTTOM_ADDEXECUTIONVIEWMODEL");
                    //Messenger.Default.Send<TestCaseItem>(SelectedTestCaseItem, "SET_SELECTEDTESTCASE_TESTCASEMAINVM");
                    //Messenger.Default.Send<string>("SELECTEDITEM", "SET_ENABLEBUTTOMPROPERTY_TESTCASECONTROLBARVM");
                    //Messenger.Default.Send<int>(SelectedTestCaseItem.TestPlanId, "SET_SELECTEDTESTPLANID_TESTCASECONTROLBARVM");
                    //Messenger.Default.Send<int>(SelectedTestCaseItem.TestPlanId, "SET_SELECTEDTESTPLANID_TESTCASEMAINVM");

                    ////Messages send to StepsListVM
                    //Messenger.Default.Send<int>(SelectedTestCaseItem.TestCaseId, "SET_SELECTEDTESTCASEID_STEPLISTVIEWMODEL");
                    //Messenger.Default.Send<bool>(true, "FILL_STEPITEMLIST_STEPLISTVIEWMODEL");
                    //Messenger.Default.Send<string>("DEFAULT", "SET_ENABLEBUTTOMPROPERTY_TESTCASESTEPSLISTCONTROLBARVM");

                    ////Messages send to AddStepMainVM
                    //Messenger.Default.Send<int>(SelectedTestCaseItem.TestCaseId, "SET_SELECTEDTESTCASEID_ADDSTEPSMAINVM");

                    ////messages send to addStepsControlBarVM
                    //Messenger.Default.Send<int>(SelectedTestCaseItem.TestCaseId, "SET_SELECTEDTESTCASEID_STEPLISTCONTROLBARVM");

                }
                else
                {
                    //Messenger.Default.Send<Visibility>(Visibility.Collapsed, "SET_VISIBILITYPROPSTEPLIST_TESTCASEMAINVM");
                    //Messenger.Default.Send<string>("UNSELECTED", "SET_ENABLEBUTTOMPROPERTY_TESTCASESTEPSLISTCONTROLBARVM");
                    ////messages send to addStepsControlBarVM
                    //Messenger.Default.Send<int>(0, "SET_SELECTEDTESTCASEID_STEPLISTCONTROLBARVM");
                }
            }
        }

        /// <summary>
        /// Propiedad que recibe el test plan id del elemento selecionado.
        /// </summary>
        public int ReceivedTestPlanId { get; set; }
        
        #endregion

        #region Methods

        /// <summary>
        /// Inicializa los comandos y registra mensajes.
        /// </summary>
        void DelegarComandos()
        {
            //Inicializar Datos.
            ReceivedTestPlanId = 0;
            // Listen the Messenger
            Messenger.Default.Register<bool>(this, "FILL_TESTCASEITEMLIST_ADDEXECUTIONTESTCASELISTVIEWMODEL", FillTestCaseList);
            Messenger.Default.Register<bool>(this, "CLEAN_SELECTEDTESTCASEPROPERTY_ADDEXECUTIONTESTCASELISTVIEWMODEL", CleanControls);
            Messenger.Default.Register<int>(this, "SET_RECEIVEDTESTPLANIDPROPERTY_ADDEXECUTIONTESTCASELISTVIEWMODEL", SetReceivedTestPlanIdProperty);
            Messenger.Default.Send<bool>(true, "GET_TESTPLANIDPROPERTY_ADDEXECUTIONTESTPLANLISTVIEWMODEL");
           
        }

        /// <summary>
        /// Llena las lista de TestCase
        /// </summary>
        /// <param name="doYouWantToFill"></param>
        private void FillTestCaseList(bool doYouWantToFill)
        {
            if (doYouWantToFill == true)
                if (ReceivedTestPlanId != 0)
                    _dataService.GetTestCaseList(ReceivedTestPlanId, AfterGetTestCases);
        }

        /// <summary>
        /// Set la variable de tipo lista.
        /// </summary>
        /// <param name="testCases"></param>
        /// <param name="exception"></param>
        private void AfterGetTestCases(IList<TestCaseItem> testCases, Exception exception)
        {
            TestCases = new ObservableCollection<TestCaseItem>(testCases);
        }

        /// <summary>
        /// Inicializa los contorles a vacio.
        /// </summary>
        /// <param name="restart"></param>
        private void CleanControls(bool restart)
        {
            if (restart == true)
            {
                SelectedTestCaseItem = null;
                ReceivedTestPlanId = 0;
                TestCases = new ObservableCollection<TestCaseItem>();
            }
        }

        /// <summary>
        /// Set el testPlanId
        /// </summary>
        /// <param name="receivedTempProperty"></param>
        private void SetReceivedTestPlanIdProperty(int receivedTempProperty)
        {
            if (receivedTempProperty!=0)
            ReceivedTestPlanId = receivedTempProperty;
        }

        #endregion
    }
}
