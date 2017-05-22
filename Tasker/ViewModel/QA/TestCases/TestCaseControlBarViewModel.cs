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
    public class TestCaseControlBarViewModel : ViewModelBase
    {
        
        #region private Member
		private readonly IDataService _dataService;
//        Tuple<string, string> varAlgo = new Tuple<string, string>("Salvar","");
        #endregion

        #region Constructor
		/// <summary>
        /// Constructor de la clase
        /// </summary>
        public TestCaseControlBarViewModel(IDataService dataService)
        {
            _dataService = dataService;
            InicializarComandos();

            
        }
        #endregion

        #region Comandos

        public RelayCommand CancelCommand   { get; set; }
        public RelayCommand SaveCommand     { get; set; }
        public RelayCommand NewCommand      { get; set; }
        public RelayCommand EditCommand     { get; set; }

        #endregion

        #region Metodos privados
		
        /// <summary>
        /// Inicializa las variables y Registra Mensajes.
        /// </summary>
		private void InicializarComandos()
        {
            CancelCommand   = new RelayCommand(Cancelar);
            SaveCommand     = new RelayCommand(Guardar);
            NewCommand      = new RelayCommand(NuevoTestCase);
            EditCommand     = new RelayCommand(Edit);

             //Inicializar Valores
            SelectedTestPlanId = new int();
            SelectedTestPlanId = 0;

             //Default menu buttom
            EnableEdit = false;
            EnableSave=false;
            EnableCancel=false;
            EnableNew=true;

            //Register Message
            Messenger.Default.Register<string>(this, "SET_ENABLEBUTTOMPROPERTY_TESTCASECONTROLBARVM", RestartButtomEnableProperty);
            Messenger.Default.Register<int>(this, "SET_SELECTEDTESTPLANID_TESTCASECONTROLBARVM", SetSelectedTestPlanIdProperty);
            Messenger.Default.Register<int>(this, "SET_SELECTEDTESTPLANIDFROMTESTPLANMV_TESTCASEMAINVM", GetSelectedTestPlanIdFromTestPlanVM);
            Messenger.Default.Send<bool>(true, "GET_SELECTEDTESTPLANIDFROMTESTPLANMV_TESTCASEMAINVM");
		}

        /// <summary>
        /// Recibe el TestPlanId del TestPlanVM
        /// </summary>
        /// <param name="receivedTestPlanId"></param>
        private void GetSelectedTestPlanIdFromTestPlanVM(int receivedTestPlanId)
         {
            if(receivedTestPlanId!=0)
             SelectedTestPlanId = receivedTestPlanId;
         }

        /// <summary>
        /// Método que recibe un int y lo iguala a SelectedTestPlanId.
        /// </summary>
        /// <param name="selectedTestPlanIdTemp"></param>
        private void SetSelectedTestPlanIdProperty(int selectedTestPlanIdTemp )
         {
             SelectedTestPlanId = selectedTestPlanIdTemp;
         }

        private void Cancelar()
         {
            Messenger.Default.Send<bool>(true, "CLEANCONTROLS_HEADERTESTPLANVM");
            Messenger.Default.Send<bool>(true, "CLEANCONTROLS_HEADERTESTCASEVM");
            Messenger.Default.Send<bool>(false, "MAINBUTTONBAR_NEWTESTCASESHOW_TESTCASEMAINVM");
            Messenger.Default.Send<bool>(true, "FILL_TESTCASEITEMLIST_TESTCASESVM");
            Messenger.Default.Send<int>(0, "SET_SELECTEDTESTPLANID_TESTCASECONTROLBARVM");
            Messenger.Default.Send<int>(0, "SET_SELECTEDTESTPLANID_TESTCASEMAINVM");
            Messenger.Default.Send<string>("CANCEL", "SET_ENABLEBUTTOMPROPERTY_TESTPLANMAINCONTROLBARVM");
            Messenger.Default.Send<bool>(false, "SET_VISIBILITYPROPADDNEWTESTCASEHEADER_TESTCASEMAINVM");
            RestartButtomEnableProperty("CANCEL");
         }

        private void Guardar()
        {
            Messenger.Default.Send<bool>(true, "MAINBUTTONBAR_TESTCASEMAINVM");
        }

        private void NuevoTestCase()
        {
            if (SelectedTestPlanId != 0)
            {
                Messenger.Default.Send<bool>(true, "CREATENEWTESTCASE_TESTCASEHEADERVM");
                Messenger.Default.Send<bool>(true, "CLEAN_RECEIVEDTESTCASE_TESTCASEMAINVM");
                Messenger.Default.Send<bool>(true, "MAINBUTTONBAR_NEWTESTCASESHOW_TESTCASEMAINVM");
                Messenger.Default.Send<string>("NEW", "SET_WHATDOYOUWHANTTODO_TESTCASEMAINVM");
                Messenger.Default.Send<bool>(true, "SET_VISIBILITYPROPADDNEWTESTCASEHEADER_TESTCASEMAINVM");
                RestartButtomEnableProperty("NEW");
            }
            else
            {
                TaskerHelper.SetStatusBarMessage("You must Select a Test Plan in order to continue.");
            }
        }

        private void Edit()
        {
            Messenger.Default.Send<bool>(true, "MAINBUTTONBAR_NEWTESTCASESHOW_TESTCASEMAINVM");
            Messenger.Default.Send<string>("EDIT", "SET_WHATDOYOUWHANTTODO_TESTCASEMAINVM");
            Messenger.Default.Send<bool>(true, "SET_TESTPLANIDPROPERTY_TESTCASESVM");
            Messenger.Default.Send<bool>(true, "SET_VISIBILITYPROPADDNEWTESTCASEHEADER_TESTCASEMAINVM");
            RestartButtomEnableProperty("EDIT");
        }

        /// <summary>
        /// Habilita o deshabilita los botones. </summary>
        /// <param name="option"></param>
        private void RestartButtomEnableProperty(string option)
        {
            switch (option)
            {
                 case "NEW":
                        EnableEdit = false;
                        EnableSave = true;
                        EnableCancel = true;
                        EnableNew = false;
                     break;

                 case "SELECTEDITEM":
                        EnableEdit = true;
                        EnableSave = false;
                        EnableCancel = true;
                        EnableNew = false;
                     break;

                 case "CANCEL":
                        EnableEdit = false;
                        EnableSave = false;
                        EnableCancel = false;
                        EnableNew = true;
                     break;

                 case "SAVE":
                        EnableEdit = false;
                        EnableSave = false;
                        EnableCancel = false;
                        EnableNew = true;
                     break;

                 case "EDIT":
                     EnableEdit = false;
                     EnableSave = true;
                     EnableCancel = true;
                     EnableNew = false;
                     break;
            }
        }
		
        #endregion

        #region Property

        public int SelectedTestPlanId 
        { 
            get; 
            set; 
        }

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
