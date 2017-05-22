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
   public class RunExecutionFaultExceptionControlBarViewModel: ViewModelBase
	{
	   
		#region private Member
		private readonly IDataService _dataService;
		#endregion

		#region Constructor
		/// <summary>
		/// Constructor de la clase
		/// </summary>
		public RunExecutionFaultExceptionControlBarViewModel(IDataService dataService)
		{
			_dataService = dataService;
			InitializeCommands();
		}
		#endregion

		#region Comandos

		public RelayCommand CancelCommand   { get; set; }
		public RelayCommand SaveCommand     { get; set; }
		//public RelayCommand CleanCommand    { get; set; }
        public RelayCommand ReturnCommand   { get; set; }

		#endregion

		#region Metodos privados

        /// <summary>
        /// Constructor
        /// </summary>
		private void InitializeCommands()
		{
			CancelCommand = new RelayCommand(Cancel);
			SaveCommand = new RelayCommand(Save);
			//CleanCommand = new RelayCommand(Clean);
            ReturnCommand = new RelayCommand(PreviousScreen);

			//Default menu buttom
			EnableSave = true;
			EnableCancel = true;
			EnableClean = true;
		}

        /// <summary>
        /// Collapsa el panel para regresar a la pantalla anterior.
        /// </summary>
        private void PreviousScreen()
        {
            Messenger.Default.Send<bool>(false,"ACTION_MAINBUTTONBAR_RUNEXECUTIONFAULTEXCEPTIONMAINVM");
            Messenger.Default.Send<bool>(false, "SHOW_FAULTEXECUTIONPANEL_RUNEXECUTIONMAINVIEWMODEL");
            Messenger.Default.Send<bool>(true, "CLEANCONTROL_RUNEXECUTIONFAULTEXCEPTIONMAINVM");
        }

        /// <summary>
        /// Inicializa los controles a vacio.
        /// </summary>
		private void Cancel()
		{
            Messenger.Default.Send<bool>(true, "CLEANCONTROL_RUNEXECUTIONFAULTEXCEPTIONMAINVM");
		}

        /// <summary>
        /// Metodo que envia un mensae para guardar al viewmodel principal.
        /// </summary>
		private void Save()
		{
            Messenger.Default.Send<bool>(true, "ACTION_MAINBUTTONBAR_RUNEXECUTIONFAULTEXCEPTIONMAINVM");
		}

        //private void Clean()
        //{
        //    Messenger.Default.Send<bool>(true, "CLEANCONTROL_RUNEXECUTIONFAULTEXCEPTIONMAINVM");
        //}

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
