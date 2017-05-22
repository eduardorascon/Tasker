using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Helpers;
using Tasker.Model;

namespace Tasker.ViewModel.QA.TestCases
{
	public class TestCaseStepsListControlBarViewModel : ViewModelBase
	{

		#region private Member
		private readonly IDataService _dataService;
		#endregion

		#region Constructor

		/// <summary>
		/// Constructor de la clase
		/// </summary>
		public TestCaseStepsListControlBarViewModel (IDataService dataService)
		{
		_dataService = dataService;
		InicializarComandos();
		}

		#endregion

		#region Command

		public RelayCommand NewCommand      { get; set; }
		public RelayCommand EditCommand     { get; set; }
		public RelayCommand CancelCommand   { get; set; }

		#endregion

		#region Private Methods
		
        /// <summary>
        /// Inicializar las variables y Registrar mensajes.
        /// </summary>
		private void InicializarComandos()
		{
			NewCommand = new RelayCommand(NuevoTestCase);
			EditCommand = new RelayCommand(Edit);
			CancelCommand = new RelayCommand(Cancelar);

			//Inicializar Valores
            SelectedTestCaseId = new int();
            SelectedTestCaseId = 0;

			//Default menu buttom
			EnableEdit = false;
			EnableNew=false;

			//Register Message
			Messenger.Default.Register<string>(this, "SET_ENABLEBUTTOMPROPERTY_TESTCASESTEPSLISTCONTROLBARVM", RestartButtomEnableProperty);
			Messenger.Default.Register<int>(this, "SET_SELECTEDTESTCASEID_STEPLISTCONTROLBARVM", SetSelectedTestPlanIdProperty);
		}

        /// <summary>
        /// Set la variable SelectedTestCaseId
        /// </summary>
        /// <param name="selectedTestCaseIdTemp"></param>
		private void SetSelectedTestPlanIdProperty(int selectedTestCaseIdTemp )
		 {
             SelectedTestCaseId = selectedTestCaseIdTemp;
		 }

        /// <summary>
        /// Limpia los controles y oculta el panel.
        /// </summary>
		private void Cancelar()
		{
			Messenger.Default.Send<bool>(true, "CLEANCONTROLS_STEPLISTVIEWMODEL");
            Messenger.Default.Send<bool>(false, "SET_VISIBILITYPROPADDSTEPLIST_TESTCASEMAINVM");
            Messenger.Default.Send<bool>(true, "CLEANCONTROL_ADDSTEPSVIEWMODEL");
			RestartButtomEnableProperty("DEFAULT");
		}

        /// <summary>
        /// Agrega un nuevo testCase
        /// </summary>
		private void NuevoTestCase()
		{
            if (SelectedTestCaseId != 0)
            {
                Messenger.Default.Send<string>("NEW", "SET_WHATDOYOUWHANTTODO_ADDSTEPSMAINVM");
                Messenger.Default.Send<bool>(true, "SET_VISIBILITYPROPADDSTEPLIST_TESTCASEMAINVM");
                Messenger.Default.Send<bool>(true, "CLEAN_RECEIVEDSTEPITEM_ADDSTEPSMAINVM");
            }
            else
            TaskerHelper.SetStatusBarMessage("You must Select a Test Case in order to continue.");
		}

        /// <summary>
        /// Modifica el item selecionado. 
        /// </summary>
		private void Edit()
        {
            if (SelectedTestCaseId != 0)
            {
                Messenger.Default.Send<string>("EDIT", "SET_WHATDOYOUWHANTTODO_ADDSTEPSMAINVM");
                Messenger.Default.Send<bool>(true, "SET_VISIBILITYPROPADDSTEPLIST_TESTCASEMAINVM");
            }
            else
            TaskerHelper.SetStatusBarMessage("You must Select a Test Case in order to continue.");
		}

        /// <summary>
        /// Habilita y deshabilita los botones.
        /// </summary>
        /// <param name="option"></param>
		private void RestartButtomEnableProperty(string option)
		{
			switch (option)
			{
				 case "DEFAULT":
						EnableEdit = false;
						EnableNew = true;
						EnableCancel = false;
					 break;

				 case "SELECTEDITEM":
						EnableEdit = true;
						EnableNew = false;
						EnableCancel = true;
					 break;

				 case "UNSELECTED":
					 EnableEdit = false;
					 EnableNew = false;
					 break;
			}
		}
		
		#endregion

		#region Property

		public int SelectedTestCaseId { get; set; }

		public bool EnableNew
		{
			get { return _enableNew; }
			set
			{
				if (_enableNew != value)
				{
					_enableNew = value;
					RaisePropertyChanged("EnableNew");
				}
			}
		}
		private bool _enableNew;


		public bool EnableEdit
		{
			get { return _enableEdit; }
			set
			{
				if (_enableEdit != value)
				{
					_enableEdit = value;
					RaisePropertyChanged("EnableEdit");
				}
			}
		}
		private bool _enableEdit;


		public bool EnableCancel
		{
			get { return _enableCancel; }
			set
			{
				if (_enableCancel != value)
				{
					_enableCancel = value;
					RaisePropertyChanged("EnableCancel");
				}
			}
		}
		private bool _enableCancel;


		public bool EnableSave
		{
			get { return _enableSave; }
			set
			{
				if (_enableSave != value)
				{
					_enableSave = value;
					RaisePropertyChanged("EnableSave");
				}
			}
		}
		private bool _enableSave;

		#endregion


	}
}
