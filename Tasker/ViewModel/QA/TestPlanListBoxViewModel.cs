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

namespace Tasker.ViewModel.QA
{
	public class TestPlanListBoxViewModel : ViewModelBase
	{

		#region Constructor

		public TestPlanListBoxViewModel(IDataService dataService)
		{
			_dataService = dataService;
			DelegarComandos();
		}

		#endregion

		#region Miembros privados

		private readonly IDataService _dataService;
		private TestPlanItem originalSelectedQA = new TestPlanItem();

		void DelegarComandos()
		{
            TestPlanList = new ObservableCollection<TestPlanItem>();
			_dataService.GetTestPlanList(AfterGetTestCaseList);
            Messenger.Default.Register<bool>(this, "CLEANCONTROLS_HEADERTESTPLANVM", CleanControls);
            Messenger.Default.Register<bool>(this, "FILL_TESTPLANITEMlIST_TESTPLANLISTBOXVM", FillTestPlanList);
            Messenger.Default.Register<bool>(this, "GET_SELECTEDTESTPLANIDFROMTESTPLANMV_TESTCASEMAINVM", SetSelectedTestPlanIdFromTestPlanVM);
		}

		#endregion

		#region Propiedades

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
					Messenger.Default.Send<TestPlanItem>(SelectedTestPlan, "SELECTEDTESTPLAN_TESTPLANENCABEZADOVM");
                    Messenger.Default.Send<TestPlanItem>(SelectedTestPlan, "SET_SELECTEDTESTPLAN_TESTPLANMAINVM");
                    Messenger.Default.Send<string>("SELECTEDITEM", "SET_ENABLEBUTTOMPROPERTY_TESTPLANMAINCONTROLBARVM");
                    Messenger.Default.Send<int>(SelectedTestPlan.TestPlanId, "SET_SELECTEDTESTPLANID_TESTCASECONTROLBARVM");
                    Messenger.Default.Send<int>(SelectedTestPlan.TestPlanId, "SET_SELECTEDTESTPLANID_TESTCASEMAINVM");
                    Messenger.Default.Send<int>(SelectedTestPlan.TestPlanId, "SET_SELECTEDTESTPLANID_TESTCASEVM");

                    Messenger.Default.Send<bool>(true, "CLEANCONTROLS_HEADERTESTCASEVM");
                    Messenger.Default.Send<string>("CANCEL", "SET_ENABLEBUTTOMPROPERTY_TESTCASECONTROLBARVM");
                    Messenger.Default.Send<bool>(true, "CLEAN_SELECTEDTESTCASEPROPERTY_TESTCASESVM");
                    Messenger.Default.Send<bool>(false, "MAINBUTTONBAR_NEWTESTCASESHOW_TESTCASEMAINVM");
				}
			}
		}
		private TestPlanItem _selectedTestPlan;

		public int TestPlanId { get; set; }

		#endregion

		#region Métodos Privados

        /// <summary>
        /// Envía el testplanId al view principal de TestCase
        /// </summary>
        /// <param name="option"></param>
        private void SetSelectedTestPlanIdFromTestPlanVM(bool option)
        {
            if(SelectedTestPlan!=null)
            Messenger.Default.Send<int>(SelectedTestPlan.TestPlanId, "SET_SELECTEDTESTPLANIDFROMTESTPLANMV_TESTCASEMAINVM");
        }

        /// <summary>
        /// Llena la lista de TestPlan
        /// </summary>
        /// <param name="doYouWantToFill"></param>
        private void FillTestPlanList(bool doYouWantToFill)
        {
            if (doYouWantToFill==true)
                _dataService.GetTestPlanList(AfterGetTestCaseList);
            Messenger.Default.Send<bool>(true, "RELOAD_TESTPLANLIST_ADDEXECUTIONTESTPLANLISTVIEWMODEL");
        }

		private void AfterGetTestCaseList(IList<TestPlanItem> testCaseListTemp, Exception exception)
		{
			TestPlanList = new ObservableCollection<TestPlanItem>(testCaseListTemp);
//            tempTestPlanItem = new ObservableCollection<TestPlanItem>(TestPlanList);
		}

        /// <summary>
        /// Inicializa las variables a vacio.
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
