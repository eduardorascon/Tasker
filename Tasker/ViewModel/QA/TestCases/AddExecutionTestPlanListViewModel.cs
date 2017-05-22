using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Model;
using Tasker.Model.QA;

namespace Tasker.ViewModel.QA.TestCases
{
   public class AddExecutionTestPlanListViewModel: ViewModelBase
    {

       	#region Constructor

       public AddExecutionTestPlanListViewModel(IDataService dataService)
		{
			_dataService = dataService;
			InicializateCommand();
		}

		#endregion

		#region Private Members

		private readonly IDataService _dataService;
		private TestPlanItem originalSelectedQA = new TestPlanItem();

		#endregion

		#region Property

		/// <summary>
		/// Almacena una lista de los testCase contenidos por el testPlan
		/// </summary>
		public ObservableCollection<TestPlanItem> TestPlanList
		{
			get { return _testPlanList; }
			set
			{
				if (_testPlanList != value)
				{
					_testPlanList = value;
					RaisePropertyChanged("TestPlanList");
				}
			}
		}
		private ObservableCollection<TestPlanItem> _testPlanList;

        //public ObservableCollection<TestPlanItem> tempTestPlanItem { get; set; }

		/// <summary>
		/// Alamcena la información de item seleccionado en la lista.
		/// </summary>
		public TestPlanItem SelectedTestPlan
		{
			get { return _selectedTestPlan; }
			set
			{
				if (_selectedTestPlan != value)
				{
					_selectedTestPlan = value;
					RaisePropertyChanged("SelectedTestPlan");
				}
				if (SelectedTestPlan != null)
				{
                    if (SelectedTestPlan.TestPlanId != 0)
                    {
                        Messenger.Default.Send<bool>(true, "CLEAN_SELECTEDTESTCASEPROPERTY_ADDEXECUTIONTESTCASELISTVIEWMODEL");
                        Messenger.Default.Send<int>(SelectedTestPlan.TestPlanId, "SET_RECEIVEDTESTPLANIDPROPERTY_ADDEXECUTIONTESTCASELISTVIEWMODEL");
                        Messenger.Default.Send<bool>(true, "FILL_TESTCASEITEMLIST_ADDEXECUTIONTESTCASELISTVIEWMODEL");
                    }
				}
                else
                    Messenger.Default.Send<bool>(true, "FILL_TESTCASEITEMLIST_ADDEXECUTIONTESTCASELISTVIEWMODEL");
			}
		}
		private TestPlanItem _selectedTestPlan;

		public int TestPlanId { get; set; }

		#endregion

		#region Private Methods
        
        /// <summary>
        /// Inicializa los comandos y registra los mensajes.
        /// </summary>
        void InicializateCommand()
        {
            TestPlanList = new ObservableCollection<TestPlanItem>();
            _dataService.GetAllTestPlanListbyActualYear(AfterGetTestCaseList);

            //Listen Messages
            Messenger.Default.Register<bool>(this, "GET_TESTPLANIDPROPERTY_ADDEXECUTIONTESTPLANLISTVIEWMODEL", SendTestPlanIdProperty);
            Messenger.Default.Register<bool>(this, "CLEAN_CLEANSELECTEDITEM_ADDEXECUTIONTESTPLANLISTVIEWMODEL", CleanControls);
            Messenger.Default.Register<bool>(this, "RELOAD_TESTPLANLIST_ADDEXECUTIONTESTPLANLISTVIEWMODEL", FillTestPlanList);
            
        }

        /// <summary>
        /// Envia el Id del Testplan selecionado.
        /// </summary>
        /// <param name="doYouWantToSend"></param>
        private void SendTestPlanIdProperty(bool doYouWantToSend)
        {
            if (doYouWantToSend == true)
                if(SelectedTestPlan!=null)
                    if(SelectedTestPlan.TestPlanId!=0)
                        Messenger.Default.Send<int>(SelectedTestPlan.TestPlanId, "SET_RECEIVEDTESTPLANIDPROPERTY_ADDEXECUTIONTESTCASELISTVIEWMODEL");
        }

        /// <summary>
        /// Llena la lista de TestPlan
        /// </summary>
        /// <param name="doYouWantToFill"></param>
        private void FillTestPlanList(bool doYouWantToFill)
        {
            if (doYouWantToFill==true)
                _dataService.GetTestPlanList(AfterGetTestCaseList);
        }

        /// <summary>
        /// Set la variable de tipo lista de test plan.
        /// </summary>
        /// <param name="testCaseListTemp"></param>
        /// <param name="exception"></param>
		private void AfterGetTestCaseList(IList<TestPlanItem> testCaseListTemp, Exception exception)
		{
			TestPlanList = new ObservableCollection<TestPlanItem>(testCaseListTemp);
		}

        /// <summary>
        /// Inicializa los controles a vacio.
        /// </summary>
        /// <param name="borrar"></param>
		private void CleanControls(bool borrar)
		{ 
			if (borrar==true)
			{
				SelectedTestPlan=null;
			}
		}

		#endregion

    }
}
