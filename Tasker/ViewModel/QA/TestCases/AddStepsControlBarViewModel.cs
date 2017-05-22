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
	public class AddStepsControlBarViewModel : ViewModelBase
	{

		#region private Member
		private readonly IDataService _dataService;
		#endregion

		#region Constructor
		/// <summary>
		/// Constructor de la clase
		/// </summary>
		public AddStepsControlBarViewModel(IDataService dataService)
		{
			_dataService = dataService;
			InitializeCommands();
		}
		#endregion

		#region Comandos

		public RelayCommand CancelCommand   { get; set; }
		public RelayCommand SaveCommand     { get; set; }
		public RelayCommand CleanCommand    { get; set; }
        public RelayCommand DeleteCommand   { get; set; }
		
		#endregion

		#region Metodos privados

        /// <summary>
        /// Inicializa los controles y registra los mensajes.
        /// </summary>
		private void InitializeCommands()
		{
			CancelCommand = new RelayCommand(Cancel);
			SaveCommand = new RelayCommand(Save);
			CleanCommand = new RelayCommand(Clean);
            DeleteCommand = new RelayCommand(Delete);

			//Default menu buttom
			EnableSave = true;
			EnableCancel = true;
			EnableClean = true;

            Messenger.Default.Register<bool>(this, "SET_ENABLEDELETEBUTTONPROPERTY_ADDSTEPSCONTROLBARVIEWMODEL", EnableDeleteButtonProperty);
		}

        /// <summary>
        /// Elimina un item
        /// </summary>
        private void Delete()
        {
            Messenger.Default.Send<string>("DELETE", "SET_WHATDOYOUWHANTTODO_ADDSTEPSMAINVM");
            Messenger.Default.Send<bool>(true, "MAINBUTTONBAR_ADDSTEPSMAINVM");
        }
         /// <summary>
         /// Oculta el panel y limpa los controles
         /// </summary>
		private void Cancel()
		{
			Messenger.Default.Send<bool>(true, "CLEANCONTROL_ADDSTEPSVIEWMODEL");
            Messenger.Default.Send<bool>(true, "CLEANSELECTEDSTEPITEM_STEPLISTVIEWMODEL");
            Messenger.Default.Send<bool>(false, "SET_VISIBILITYPROPADDSTEPLIST_TESTCASEMAINVM");
            Messenger.Default.Send<bool>(true, "CLEANCONTROLS_STEPLISTVIEWMODEL");
            Messenger.Default.Send<string>("DEFAULT", "SET_ENABLEBUTTOMPROPERTY_TESTCASESTEPSLISTCONTROLBARVM");
            Messenger.Default.Send<bool>(true, "CLEAN_IMAGEPROP_ADDSTEPSMAINVM");
            EnableDeleteButtonProperty(false);
			//pendiente collapsar la ventana
		}

        /// <summary>
        /// Salva los cambios
        /// </summary>
		private void Save()
		{
			//pendiente Mandar salvar al view principal de steps
            Messenger.Default.Send<bool>(true, "MAINBUTTONBAR_ADDSTEPSMAINVM");
            //EnableDeleteButtonProperty(false);
		}

        /// <summary>
        /// Restable los controles a vacio.
        /// </summary>
		private void Clean()
		{
				Messenger.Default.Send<bool>(true, "CLEANCONTROL_ADDSTEPSVIEWMODEL");
		}

        /// <summary>
        /// Habilita u oculta los botones
        /// </summary>
        /// <param name="option"></param>
        private void EnableDeleteButtonProperty(bool option)
        {
            if (option == true)
                EnableDelete = true;
            else
                EnableDelete = false;
        }

		#endregion

		#region Property


        public bool EnableDelete
        {
            get { return _enableDelete; }
            set
            {
                if (_enableDelete != value)
                {
                    _enableDelete = value;
                    RaisePropertyChanged("EnableDelete");
                }
            }
        }
        private bool _enableDelete;


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
