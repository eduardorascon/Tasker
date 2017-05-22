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
	public class StepListViewModel : ViewModelBase
	{
		#region Private Member
		
		private readonly IDataService _dataService;
		private StepItem _originalReceivedStepItem = new StepItem();

		#endregion

		#region Constructor
		
		public StepListViewModel (IDataService dataService)
		{
			_dataService = dataService;
			DelegarComandos();
		}

		#endregion

		#region Private Methods

        /// <summary>
        /// Inicializar Varaibles y registrar comandos.
        /// </summary>
		void DelegarComandos()
		{
			//Initializer Properties
			

			// Listen the Messages
			Messenger.Default.Register<bool>(this, "CLEANCONTROLS_STEPLISTVIEWMODEL", CleanControls);
			Messenger.Default.Register<bool>(this, "CLEANLIST_STEPLISTVIEWMODEL", CleanList);
			Messenger.Default.Register<bool>(this, "FILL_STEPITEMLIST_STEPLISTVIEWMODEL", FillStepList);
			Messenger.Default.Register<StepItem>(this, "SELECTEDSTEPITEM_STEPLISTVIEWMODEL", SetPropertySelectedStepItem);
			Messenger.Default.Register<int>(this, "SET_SELECTEDTESTCASEID_STEPLISTVIEWMODEL", SetPropertySelectedTestCaseId);
		}

        /// <summary>
        /// Set la variable SelectedStep
        /// </summary>
        /// <param name="selectStepItemTemp"></param>
		private void SetPropertySelectedStepItem(StepItem selectStepItemTemp)
		{
			if (selectStepItemTemp == null)
				SelectedStepItem = selectStepItemTemp;
		}

        /// <summary>
        /// Set la variable TestCaseId
        /// </summary>
        /// <param name="selectedTestCaseIdTemp"></param>
		private void SetPropertySelectedTestCaseId(int selectedTestCaseIdTemp)
		{
			TestCaseId = selectedTestCaseIdTemp;
		}

        /// <summary>
        /// LLena la lista de stepItem
        /// </summary>
        /// <param name="doYouWantToFill"></param>
		private void FillStepList(bool doYouWantToFill)
		{
			if (doYouWantToFill == true)
				_dataService.GetStepList(TestCaseId,AfterGetStepList);
		}

		private void AfterGetStepList(IList<StepItem> stepListTemp, Exception exception)
		{
			StepList = new ObservableCollection<StepItem>(stepListTemp);
			if (StepList.Count == 0)
			{
				Messenger.Default.Send<string>("DEFAULT", "SET_ENABLEBUTTOMPROPERTY_TESTCASESTEPSLISTCONTROLBARVM");
				Messenger.Default.Send<Visibility>(Visibility.Collapsed, "SET_VISIBILITYPROPSTEPLIST_TESTCASEMAINVM");
			}
			else
			{
				Messenger.Default.Send<bool>(true, "SET_VISIBILITYPROPSTEPLIST_TESTCASEMAINVM");
				Messenger.Default.Send<Visibility>(Visibility.Visible, "SET_VISIBILITYPROPSTEPLIST_TESTCASEMAINVM");
			}
		}

        /// <summary>
        /// Inicializa las variables a Vacías.
        /// </summary>
        /// <param name="borrar"></param>
		private void CleanControls(bool borrar)
		{
			if (borrar == true)
			{
				SelectedStepItem = null;
			}
		}

        /// <summary>
        /// Crea un nuevo objeto StepList
        /// </summary>
        /// <param name="borrar"></param>
		private void CleanList(bool borrar)
		{
			if (borrar == true)
			{
				StepList = new ObservableCollection<StepItem>();
			}
		}

		#endregion

		#region Properties

		public int TestCaseId { get; set; }

		/// <summary>
		/// The <see cref="StepList" /> property's name.
		/// </summary>
		public const string StepListPropertyName = "StepList";

		private ObservableCollection<StepItem> _stepList = null;

		/// <summary>
		/// Sets and gets the StepList property.
		/// Changes to that property's value raise the PropertyChanged event. 
		/// </summary>
		public ObservableCollection<StepItem> StepList
		{
			get
			{
				return _stepList;
			}

			set
			{
				if (_stepList == value)
				{
					return;
				}

				RaisePropertyChanging(StepListPropertyName);
				_stepList = value;
				RaisePropertyChanged(StepListPropertyName);
			}
		}

		
		/// <summary>
		/// The <see cref="SelectedStepItem" /> property's name.
		/// </summary>
		public const string SelectedStepItemPropertyName = "SelectedStepItem";

		private StepItem _selectedStepItem = null;

		/// <summary>
		/// Sets and gets the SelectedStepItem property.
		/// Changes to that property's value raise the PropertyChanged event. 
		/// </summary>
		public StepItem SelectedStepItem
		{
			get
			{
				return _selectedStepItem;
			}

			set
			{
				if (_selectedStepItem == value)
				{
					return;
				}

				RaisePropertyChanging(SelectedStepItemPropertyName);
				_selectedStepItem = value;
				RaisePropertyChanged(SelectedStepItemPropertyName);
				if (SelectedStepItem != null)
				{
					Messenger.Default.Send<string>("SELECTEDITEM", "SET_ENABLEBUTTOMPROPERTY_TESTCASESTEPSLISTCONTROLBARVM");

					//Messages send to AddStepMainVM
					Messenger.Default.Send<StepItem>(SelectedStepItem, "SET_RECEIVEDSTEPITEMFROMLIST_ADDSTEPSMAINVM");
					Messenger.Default.Send<int>(SelectedStepItem.TestCaseId, "SET_SELECTEDTESTCASEID_ADDSTEPSMAINVM");

					//Messages send to AddStepVM
					Messenger.Default.Send<StepItem>(SelectedStepItem, "SELECTEDTESTCASE_TESTCASESVM");

					//messages send to addStepsControlBarVM
					Messenger.Default.Send<int>(SelectedStepItem.TestCaseId, "SET_SELECTEDTESTCASEID_STEPLISTCONTROLBARVM");
				}
			}
		}

		#endregion

	}
}
