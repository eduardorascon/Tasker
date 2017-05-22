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
    public class ExecutionMainControlBarViewModel: ViewModelBase
    {
        #region private Member
		private readonly IDataService _dataService;
		#endregion

		#region Constructor

		/// <summary>
		/// Constructor de la clase
		/// </summary>
        public ExecutionMainControlBarViewModel(IDataService dataService)
		{
		_dataService = dataService;
		InicializarComandos();
		}

		#endregion

		#region Command

		public RelayCommand NewCommand      { get; set; }
		public RelayCommand EditCommand     { get; set; }
		public RelayCommand CancelCommand   { get; set; }
        public RelayCommand PlayCommand     { get; set; }


		#endregion

		#region Private Methods
		
        /// <summary>
        /// Inicializa los controles y registra los mensajes.
        /// </summary>
		private void InicializarComandos()
		{
			NewCommand = new RelayCommand(NuevoTestCase);
			EditCommand = new RelayCommand(Edit);
			CancelCommand = new RelayCommand(Cancelar);
            PlayCommand = new RelayCommand(Play);


            RestartButtomEnableProperty("DEFAULT");

			//Register Message
            Messenger.Default.Register<string>(this, "SET_ENABLEBUTTOMPROPERTY_EXECUTIONMAINCONTROLBARVIEWMODEL", RestartButtomEnableProperty);

		}

        /// <summary>
        /// Oculta el panel y restaable los controles.
        /// </summary>
		private void Cancelar()
		{
            Messenger.Default.Send<bool>(true,"CLEAN_SELECTEDITEM_EXECUTIONLISTVIEWMODEL");
            Messenger.Default.Send<bool>(false, "SHOW_SHOWEXECUTIONVIEW_EXECUTIONMAINVIEWMODEL");
            RestartButtomEnableProperty("DEFAULT");
		}

        /// <summary>
        /// Agrega un nuevo item.
        /// </summary>
		private void NuevoTestCase()
		{
            Messenger.Default.Send<bool>(true, "CLEAN_CLEANCONTROL_ADDEXECUTIONVIEWMODEL");
            Messenger.Default.Send<bool>(true, "SHOW_SHOWEXECUTIONVIEW_EXECUTIONMAINVIEWMODEL");
            Messenger.Default.Send<bool>(true, "CLEAN_SELECTEDTESTCASEPROPERTY_ADDEXECUTIONLISTVIEWMODEL");
            Messenger.Default.Send<bool>(true, "SHOW_BLACKCANVASVISIBILITYPROP_EXECUTIONMAINVIEWMODEL");
		}

        /// <summary>
        /// Modifica el item selecionado.
        /// </summary>
		private void Edit()
        {
            //pendiente debo de setear una propiedad whatdoYou.... 
            Messenger.Default.Send<bool>(true, "SHOW_SHOWEXECUTIONVIEW_EXECUTIONMAINVIEWMODEL");
            Messenger.Default.Send<string>("EDIT", "SET_WHATDOYOUWHANTTODO_ADDSTEPSMAINVM");
            Messenger.Default.Send<bool>(true, "SHOW_BLACKCANVASVISIBILITYPROP_EXECUTIONMAINVIEWMODEL");

		}

        /// <summary>
        /// Muestra el panel para correr la execution
        /// </summary>
        private void Play()
        {
            Messenger.Default.Send<bool>(true, "SHOW_SHOWRUNEXECUTIONVIEW_EXECUTIONMAINVIEWMODEL");
            Messenger.Default.Send<bool>(true, "SHOW_BLACKCANVASVISIBILITYPROP_EXECUTIONMAINVIEWMODEL");
        }
        
        /// <summary>
        /// Cambio los estados enable de los botones.
        /// </summary>
        /// <param name="option"></param>
		private void RestartButtomEnableProperty(string option)
		{
			switch (option)
			{
				 case "DEFAULT":
						EnableEdit      = false;
			            EnableNew       = true;
                        EnablePlay      = false;
                        EnableCancel    = true;
					 break;

				 case "SELECTEDITEM":
						EnableEdit = true;
						EnableNew = false;
						EnableCancel = true;
                        EnablePlay = true;
					 break;

				 case "UNSELECTED":
					 EnableEdit = false;
					 EnableNew = false;
					 break;

                case "UNPLAY":
                     EnablePlay = false;
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


        public bool EnablePlay
        {
            get { return _enablePlay; }
            set
            {
                if (_enablePlay != value)
                {
                    _enablePlay = value;
                    RaisePropertyChanged("EnablePlay");
                }
            }
        }
        private bool _enablePlay;


		#endregion

    }
}
