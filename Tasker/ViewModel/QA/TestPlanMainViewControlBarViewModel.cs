using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Model;

namespace Tasker.ViewModel.QA
{
    public class TestPlanMainViewControlBarViewModel : ViewModelBase
    {
        
        #region Miembros privados
		private readonly IDataService _dataService;
//        Tuple<string, string> varAlgo = new Tuple<string, string>("Salvar","");
        #endregion

        #region Constructor
		/// <summary>
        /// Constructor de la clase
        /// </summary>
        public TestPlanMainViewControlBarViewModel(IDataService dataService)
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
        /// Inicializa los comandos
        /// </summary>
		 private void InicializarComandos()
        {
            CancelCommand   = new RelayCommand(Cancelar);
            SaveCommand     = new RelayCommand(Guardar);
            NewCommand      = new RelayCommand(NuevoTestPlan);
            EditCommand     = new RelayCommand(Edit);

             //Enable Buttom
            EnableEdit = false;
            EnableSave=false;
            EnableCancel=false;
            EnableNew=true;

            //messenger
            //Messenger.Default.Register<Tuple<string, bool>>(this,"SET_ENABLEBUTTOMPROPERTY_TESTPLANMAINCONTROLBARVM",SetEnableButtomProperty);
            Messenger.Default.Register<string>(this, "SET_ENABLEBUTTOMPROPERTY_TESTPLANMAINCONTROLBARVM", RestarButtomEnableProperty);

		}

        private void Cancelar()
         {
            Messenger.Default.Send<bool>(true, "CLEANCONTROLS_HEADERTESTCASEVM");
            Messenger.Default.Send<bool>(true, "CLEANCONTROLS_HEADERTESTPLANVM");
            Messenger.Default.Send<bool>(false, "MAINBUTTONBAR_NEWTESTPLANSHOW_TESTMAINVM");
            Messenger.Default.Send<bool>(true, "FILL_TESTPLANITEMlIST_TESTPLANLISTBOXVM");
            Messenger.Default.Send<int>(0, "SET_SELECTEDTESTPLANID_TESTCASECONTROLBARVM");
            Messenger.Default.Send<int>(0, "SET_SELECTEDTESTPLANID_TESTCASEMAINVM");
            Messenger.Default.Send<bool>(false, "MAINBUTTONBAR_NEWTESTCASESHOW_TESTCASEMAINVM");
            Messenger.Default.Send<string>("CANCEL", "SET_ENABLEBUTTOMPROPERTY_TESTCASECONTROLBARVM");
            RestarButtomEnableProperty("CANCEL");
         }

        private void Guardar()
        {
            Messenger.Default.Send<bool>(true, "MAINBUTTONBAR_TESTPLANMAINVM");
        }

        private void NuevoTestPlan()
        {
            //limpiar el selectede item de TestPlanMainVM
            Messenger.Default.Send<bool>(true, "CREATENEWTESTPLAN_TESTPLANENCABEZADOVM");
            Messenger.Default.Send<bool>(true, "CLEAN_RECEIVEDTESTPLAN_TESTPLANMAINVM");
            Messenger.Default.Send<bool>(true, "MAINBUTTONBAR_NEWTESTPLANSHOW_TESTMAINVM");
            Messenger.Default.Send<string>("NEW", "SET_WHATDOYOUWHANTTODO_INTESTPLANMAINVM");
            RestarButtomEnableProperty("NEW");
        }

        private void Edit()
        {
            Messenger.Default.Send<bool>(true, "MAINBUTTONBAR_NEWTESTPLANSHOW_TESTMAINVM");
            Messenger.Default.Send<string>("EDIT", "SET_WHATDOYOUWHANTTODO_INTESTPLANMAINVM");
            RestarButtomEnableProperty("EDIT");
        }

        /// <summary>
        /// Método que maneja los estados de los botones
        /// </summary>
        /// <param name="option"></param>
        private void RestarButtomEnableProperty(string option)
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
