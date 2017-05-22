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
   public class RunExecutionControlBarViewModel: ViewModelBase
	{

		#region private Member
		private readonly IDataService _dataService;
		#endregion

		#region Constructor
		/// <summary>
		/// Constructor de la clase
		/// </summary>
		public RunExecutionControlBarViewModel(IDataService dataService)
		{
			_dataService = dataService;
			InitializeComponent();
		}
		#endregion

		#region Comandos

		public RelayCommand RejectCommand   { get; set; }
		public RelayCommand ApproveCommand  { get; set; }
		public RelayCommand NextCommand     { get; set; }
		public RelayCommand BackCommand     { get; set; }
		public RelayCommand PreviousScreenCommand       { get; set; }
        public RelayCommand CloseRunExecutionCommand    { get; set; }
		
		#endregion

		#region Metodos privados

        /// <summary>
        /// Registra los mensajes y inciializa los controles.
        /// </summary>
		private void InitializeComponent()
		{
			RejectCommand               = new RelayCommand(Reject);
			ApproveCommand              = new RelayCommand(Approve);
			NextCommand                 = new RelayCommand(Next);
			BackCommand                 = new RelayCommand(Back);
			PreviousScreenCommand       = new RelayCommand(CollapseRunExecutionPanel);
            CloseRunExecutionCommand    = new RelayCommand(CloseRunExecution);
		//Listen to Messages
			Messenger.Default.Register<bool>(this, "ACTIVE_NAVIGATIONCONTROL_RUNEXECUTIONCONTROLBARVIEWMODEL", ActiveNavigationControl);
            Messenger.Default.Register<bool>(this, "ACTIVE_CLOSERUNEXECUTION_RUNEXECUTIONCONTROLBARVIEWMODEL", ActiveCloseRunExecution);
            Messenger.Default.Register<bool>(this, "COLLAPSE_RUNEXECUTIONPANEL_RUNEXECUTIONCONTROLBARVIEWMODEL", CollapseRunExecutionPanel);
		}

        /// <summary>
        /// Rechaza el item selecionado
        /// </summary>
		private void Reject()
		{
            //Send To: RUNEXECUTIONMAINVIEWMODEL
            Messenger.Default.Send<bool>(false, "SAVE_DOYOUAPPROVETHESTEP_RUNEXECUTIONMAINVIEWMODEL");
            //Send To: RUNEXECUTIONSTEPLISTVIEWMODEL
            Messenger.Default.Send<bool>(true, "RELOAD_STEPLIST_RUNEXECUTIONSTEPLISTVIEWMODEL");
		}

        /// <summary>
        /// Aprueba el item selecionado
        /// </summary>
		private void Approve()
		{
            //Send To: RUNEXECUTIONMAINVIEWMODEL
            Messenger.Default.Send<bool>(true, "SAVE_DOYOUAPPROVETHESTEP_RUNEXECUTIONMAINVIEWMODEL");
            //Send To: RUNEXECUTIONSTEPLISTVIEWMODEL
            Messenger.Default.Send<bool>(true, "RELOAD_STEPLIST_RUNEXECUTIONSTEPLISTVIEWMODEL");
		}

        /// <summary>
        /// Seleciona el item siguiente de la lista.
        /// </summary>
		private void Next()
		{
			//Send To: RUNEXECUTIONSTEPLISTVIEWMODEL
			Messenger.Default.Send<string>("Front", "NAVI_NAVIGATEINSTEPLIST_RUNEXECUTIONSTEPLISTVIEWMODEL");
		}

        /// <summary>
        /// Seleciona el item anterior de la lista.
        /// </summary>
		private void Back()
		{
			//Send To: RUNEXECUTIONSTEPLISTVIEWMODEL
			Messenger.Default.Send<string>("Back", "NAVI_NAVIGATEINSTEPLIST_RUNEXECUTIONSTEPLISTVIEWMODEL");
		}

        /// <summary>
        /// oculta el panel de run execution.
        /// </summary>
		private void CollapseRunExecutionPanel()
		{
			Messenger.Default.Send<bool>(false, "SHOW_SHOWRUNEXECUTIONVIEW_EXECUTIONMAINVIEWMODEL");
			Messenger.Default.Send<bool>(false, "SHOW_BLACKCANVASVISIBILITYPROP_EXECUTIONMAINVIEWMODEL");
			Messenger.Default.Send<bool>(true, "CLEAN_PROPERTIES_RUNEXECUTIONTESTCASELISTVIEWMODEL");
            Messenger.Default.Send<bool>(true, "REFRESH_EXECUTIONLIST_EXECUTIONLISTVIEWMODEL");
		}

        /// <summary>
        /// oculta el panel de run execution.
        /// </summary>
        /// <param name="option"></param>
        private void CollapseRunExecutionPanel(bool option)
        {
            if (option == true)
            {
                Messenger.Default.Send<bool>(false, "SHOW_SHOWRUNEXECUTIONVIEW_EXECUTIONMAINVIEWMODEL");
                Messenger.Default.Send<bool>(false, "SHOW_BLACKCANVASVISIBILITYPROP_EXECUTIONMAINVIEWMODEL");
                Messenger.Default.Send<bool>(true, "CLEAN_PROPERTIES_RUNEXECUTIONTESTCASELISTVIEWMODEL");
                Messenger.Default.Send<bool>(true, "REFRESH_EXECUTIONLIST_EXECUTIONLISTVIEWMODEL");
            }
        }

        /// <summary>
        /// Habilita o desabilita los botones
        /// </summary>
        /// <param name="option"></param>
        private void ActiveCloseRunExecution(bool option)
        {
            if (option == true)
                EnableCloseRunExecution = true;
            else
                EnableCloseRunExecution = false;
        }

        /// <summary>
        /// Habilita o desabilita los botones
        /// </summary>
        /// <param name="turnOn"></param>
	    private void ActiveNavigationControl(bool turnOn)
		{
			if (turnOn == true)
			{
				EnableNext = true;
				EnableBack = true;
				EnableApprove = true;
				EnableReject = true;
			}
			else
			{
				EnableNext = false;
				EnableBack = false;
                EnableApprove = false;
				EnableReject = false;
			}
		}

        /// <summary>
        /// Cierra la run execution para crear un nuevo item si se vuelve a selecionar.
        /// </summary>
        private void CloseRunExecution()
        {
            Messenger.Default.Send<bool>(true, "CLOSE_RUNEXECUTIONHEADER_RUNEXECUTIONMAINVIEWMODEL");
            CollapseRunExecutionPanel();
        }

		#endregion

		#region Property


		public bool EnableNext
		{
			get { return _enableNext; }
			set
			{
				if (_enableNext != value)
				{
					_enableNext = value;
					RaisePropertyChanged("EnableNext");
				}
			}
		}
		private bool _enableNext;



		public bool EnableBack
		{
			get { return _enableBack; }
			set
			{
				if (_enableBack != value)
				{
					_enableBack = value;
					RaisePropertyChanged("EnableBack");
				}
			}
		}
		private bool _enableBack;


		public bool EnableReject
		{
			get { return _enableReject; }
			set
			{
				if (_enableReject != value)
				{
					_enableReject = value;
					RaisePropertyChanged("EnableReject");
				}
			}
		}
		private bool _enableReject;


		public bool EnableApprove
		{
			get { return _enableApprove; }
			set
			{
				if (_enableApprove != value)
				{
					_enableApprove = value;
					RaisePropertyChanged("EnableApprove");
				}
			}
		}
		private bool _enableApprove;


        public bool EnableCloseRunExecution
        {
            get { return _enableCloseRunExecution; }
            set
            {
                if (_enableCloseRunExecution != value)
                {
                    _enableCloseRunExecution = value;
                    RaisePropertyChanged("EnableCloseRunExecution");
                }
            }
        }
        private bool _enableCloseRunExecution = false;
        


		#endregion

	}
}
