using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Model;

namespace Tasker.ViewModel.QA.TestCases
{
   public class AddExecutionControlBarViewModel: ViewModelBase
	{

		#region private Member
		private readonly IDataService _dataService;
		#endregion

		#region Constructor
		/// <summary>
		/// Constructor de la clase
		/// </summary>
		public AddExecutionControlBarViewModel(IDataService dataService)
		{
			_dataService = dataService;
			InitializeCommands();
		}
		#endregion

		#region Comandos

		public RelayCommand CancelCommand   { get; set; }
		public RelayCommand SaveCommand     { get; set; }
		public RelayCommand CleanCommand    { get; set; }
		
		#endregion

		#region Metodos privados

        /// <summary>
        /// Constructor de la Clase
        /// </summary>
		private void InitializeCommands()
		{
			CancelCommand = new RelayCommand(Cancel);
			SaveCommand = new RelayCommand(Save);
			CleanCommand = new RelayCommand(Clean);

			//Default menu buttom
			EnableSave = true;
			EnableCancel = true;
			EnableClean = true;
		}
        
		private void Cancel()
		{
            Messenger.Default.Send<bool>(true, "CLEAN_CLEANCONTROL_ADDEXECUTIONVIEWMODEL");
            Messenger.Default.Send<bool>(false, "SHOW_SHOWEXECUTIONVIEW_EXECUTIONMAINVIEWMODEL");
            Messenger.Default.Send<bool>(false, "SHOW_BLACKCANVASVISIBILITYPROP_EXECUTIONMAINVIEWMODEL");
            Messenger.Default.Send<bool>(true, "CLEAN_SELECTEDTESTCASEPROPERTY_ADDEXECUTIONLISTVIEWMODEL");
            Messenger.Default.Send<bool>(true, "CLEAN_SELECTEDITEM_EXECUTIONLISTVIEWMODEL");
            Messenger.Default.Send<string>("DEFAULT", "SET_ENABLEBUTTOMPROPERTY_EXECUTIONMAINCONTROLBARVIEWMODEL");
            Messenger.Default.Send<bool>(true, "CLEAN_SELECTEDTESTCASEPROPERTY_ADDEXECUTIONTESTCASELISTVIEWMODEL");
            Messenger.Default.Send<bool>(true, "CLEAN_CLEANSELECTEDITEM_ADDEXECUTIONTESTPLANLISTVIEWMODEL");
		}

		private void Save()
		{
			//pendiente Mandar salvar al view principal de steps
            Messenger.Default.Send<bool>(true, "SAVE_ADDEXECUTIONHEADER_ADDEXECUTIONVIEWMODEL");
            Messenger.Default.Send<string>("DEFAULT", "SET_ENABLEBUTTOMPROPERTY_EXECUTIONMAINCONTROLBARVIEWMODEL");
            Messenger.Default.Send<bool>(true, "CLEAN_SELECTEDTESTCASEPROPERTY_ADDEXECUTIONLISTVIEWMODEL");
            Messenger.Default.Send<bool>(true, "CLEAN_SELECTEDITEM_EXECUTIONLISTVIEWMODEL");
            Messenger.Default.Send<bool>(true, "CLEAN_SELECTEDTESTCASEPROPERTY_ADDEXECUTIONTESTCASELISTVIEWMODEL");
            Messenger.Default.Send<bool>(true, "CLEAN_CLEANSELECTEDITEM_ADDEXECUTIONTESTPLANLISTVIEWMODEL");
            Messenger.Default.Send<bool>(true, "CLEAN_CLEANCONTROL_ADDEXECUTIONVIEWMODEL");
            Messenger.Default.Send<bool>(false, "SHOW_SHOWEXECUTIONVIEW_EXECUTIONMAINVIEWMODEL");
            Messenger.Default.Send<bool>(false, "SHOW_BLACKCANVASVISIBILITYPROP_EXECUTIONMAINVIEWMODEL");
		}

        /// <summary>
        /// Inicializa los controles a vacio.
        /// </summary>
		private void Clean()
		{
				//Messenger.Default.Send<bool>(true, "CLEANCONTROL_ADDSTEPSVIEWMODEL");
		}

		#endregion

		#region Property

		public bool EnableClean
		{
			get { return _enableClean; }
			set
			{
				if (_enableClean != value)
				{
					_enableClean = value;
					RaisePropertyChanged("EnableClean");
				}
			}
		}
		private bool _enableClean;


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
