using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tasker.Model;
using Tasker.Model.QA;

namespace Tasker.ViewModel.QA.TestCases
{
    public class ExecutionMainViewModel: ViewModelBase
    {

        	
		#region Miembros privados

		private readonly IDataService _dataService;
		

		#endregion

        #region Constructor

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public ExecutionMainViewModel(IDataService dataService)
        {
            _dataService = dataService;
            InitializationCommand();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Inicializa los comandos
        /// </summary>
        private void InitializationCommand()
        {
            //initialization Properties
            ReceivedExecutionItem = new ExecutionItem();
            ShowRunExecutionView(false);
            //Register Messages
            Messenger.Default.Register<bool>(this, "SHOW_SHOWEXECUTIONVIEW_EXECUTIONMAINVIEWMODEL", ShowAddExecutionView);
            Messenger.Default.Register<bool>(this, "SHOW_SHOWRUNEXECUTIONVIEW_EXECUTIONMAINVIEWMODEL", ShowRunExecutionView);
            Messenger.Default.Register<ExecutionItem>(this, "SET_RECEIVEDEXECUTIONITEM_EXECUTIONMAINVIEWMODEL", SetReceivedExecutionItem);
            Messenger.Default.Register<bool>(this, "CLEAN_CLEANCONTROL_EXECUTIONMAINVIEWMODEL", CleanControl);
            Messenger.Default.Register<bool>(this, "SHOW_BLACKCANVASVISIBILITYPROP_EXECUTIONMAINVIEWMODEL",ShowBlackCanvasAndProtectButtom);
            //Messenger.Default.Register<bool>(this, "CLEAN_RECEIVEDTESTCASE_TESTCASEMAINVM", Clean_ReceivedTestCase);
            //Messenger.Default.Register<bool>(this, "MAINBUTTONBAR_NEWTESTCASESHOW_TESTCASEMAINVM", ShowHeaderNewTestCase);
            //Messenger.Default.Register<string>(this, "SET_WHATDOYOUWHANTTODO_EXECUTIONMAINVIEWMODEL", Set_WhatDoYouWantToDo);
            //Messenger.Default.Register<PaqueteMSJ>(this, "SET_TESTCASEPROPERTY_IN-MAINVMTESTCASE_FROMCONSTRUCTOR", SetTestCasePropertyFromContructor);
            //Messenger.Default.Register<bool>(this, "MAINBUTTONBAR_TESTCASEMAINVM", BarraPrincipalBotones);
            //Messenger.Default.Register<TestCaseItem>(this, "SET_SELECTEDTESTCASE_TESTCASEMAINVM", SetTestCasePropertyFromTestCasesVM);
            //Messenger.Default.Register<int>(this, "SET_SELECTEDTESTPLANID_TESTCASEMAINVM", SetSelectedTestPlanIdProperty);
            ////Messenger.Default.Register<Visibility>(this, "SET_VISIBILITYPROPSTEPLIST_TESTCASEMAINVM", SetVisibilityPropStepList);
            //Messenger.Default.Register<bool>(this, "SET_VISIBILITYPROPSTEPLIST_TESTCASEMAINVM", SetVisibilityPropForShowStepList);
            //Messenger.Default.Register<bool>(this, "SET_VISIBILITYPROPADDSTEPLIST_TESTCASEMAINVM", SetVisibilityPropForAddStepList);
            //Messenger.Default.Register<bool>(this, "SET_VISIBILITYPROPADDNEWTESTCASEHEADER_TESTCASEMAINVM", SetVisibilityPropForAddNewTestCaseHeader);
            //Messenger.Default.Register<string>(this, "SET_VISIBILITYPROPFORSETDEAFULTORINADDMODE_TESTCASEMAINVM", SetVisibilityPropForMainVM);
            //Messenger.Default.Register<Visibility>(this, "SET_VISIBILITYPROPSTEPLIST_TESTCASEMAINVM", SetVisibilityPropShowStepList);
            //Messenger.Default.Register<int>(this, "SET_SELECTEDTESTPLANIDFROMTESTPLANMV_TESTCASEMAINVM", GetSelectedTestPlanIdFromTestPlanVM);
            //Messenger.Default.Send<bool>(true, "GET_SELECTEDTESTPLANIDFROMTESTPLANMV_TESTCASEMAINVM");
        }

        /// <summary>
        /// restablece la propiedad recievedExecutionitem
        /// </summary>
        /// <param name="doYouWantToClean"></param>
        private void CleanControl(bool doYouWantToClean)
        {
            if (doYouWantToClean == true)
                ReceivedExecutionItem = new ExecutionItem();
        }

        /// <summary>
        /// Iguala la propiedad recievedExecution con la obtenida por el message.
        /// </summary>
        /// <param name="tempExecutionItem"></param>
        private void SetReceivedExecutionItem(ExecutionItem tempExecutionItem)
        {
            if (tempExecutionItem != null)
            {
                ReceivedExecutionItem = new ExecutionItem();
                ReceivedExecutionItem = tempExecutionItem;
            }
                
        }

        /// <summary>
        /// Muestra u oculta el view de addExecution
        /// </summary>
        /// <param name="doYouWantToShow"></param>
        private void ShowAddExecutionView(bool doYouWantToShow)
        {
            if (doYouWantToShow == true)
            {
                IsExpandedAddExecution=true;
                AddExecutionVisibility=Visibility.Visible;
            }
            else
            {
                IsExpandedAddExecution = false;
                AddExecutionVisibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Método que oculta o muestra el control de RunExecution
        /// </summary>
        /// <param name="doYouWantToShow"></param>
        private void ShowRunExecutionView(bool doYouWantToShow)
        {
            if (doYouWantToShow == true)
            {
                RunExecutionVisibility = Visibility.Visible;
                IsExpandedRunExecution = true;
            }
            else
            {
                IsExpandedRunExecution = false;
                RunExecutionVisibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Muestra el canvas que bloquea los controles.
        /// </summary>
        /// <param name="doYouWantToShow"></param>
        private void ShowBlackCanvasAndProtectButtom(bool doYouWantToShow)
        {
            if (doYouWantToShow == true)
            {
                ShowBlackBlockScreen = Visibility.Visible;
                ShowBlackBlockScreenControlBar = Visibility.Visible;
            }
            else 
            {
                ShowBlackBlockScreen = Visibility.Collapsed;
                ShowBlackBlockScreenControlBar = Visibility.Collapsed;
            }


        
        }

        #endregion

        #region Properties

        public ExecutionItem ReceivedExecutionItem { get; set; }

        public string WhatDoYouWantToDo { get; set; }

        #region Lock and Hide Control

        public Visibility ShowBlackBlockScreenControlBar
        {
            get { return _showBlackBlockScreenControlBar; }
            set
            {
                if (_showBlackBlockScreenControlBar != value)
                {
                    _showBlackBlockScreenControlBar = value;
                    RaisePropertyChanged("ShowBlackBlockScreenControlBar");
                }
            }
        }
        private Visibility _showBlackBlockScreenControlBar=Visibility.Collapsed;

        /// <summary>
        /// Oculta el control de AddExecutionView
        /// </summary>
        public Visibility AddExecutionVisibility
        {
            get { return _addExecutionVisibility; }
            set
            {
                if (_addExecutionVisibility != value)
                {
                    _addExecutionVisibility = value;
                    RaisePropertyChanged("AddExecutionVisibility");
                }
            }
        }
        private Visibility _addExecutionVisibility=Visibility.Collapsed;

        /// <summary>
        /// Oculta la lista de Execution
        /// </summary>
        public Visibility VisibilityListExecution
        {
            get { return _visibilityListExecution; }
            set
            {
                if (_visibilityListExecution != value)
                {
                    _visibilityListExecution = value;
                    RaisePropertyChanged("VisibilityListExecution");
                }
            }
        }
        private Visibility _visibilityListExecution;
        
        /// <summary>
        /// Propiedad que almacena el estado del canvas que bloquea la edición.
        /// </summary>
        public Visibility ShowBlackBlockScreen
        {
            get { return _ShowBlackBlockScreen; }
            set
            {
                if (_ShowBlackBlockScreen != value)
                {
                    _ShowBlackBlockScreen = value;
                    RaisePropertyChanged("ShowBlackBlockScreen");
                }
            }
        }
        private Visibility _ShowBlackBlockScreen=Visibility.Collapsed;

        /// <summary>
        /// Collpase effect of ExecutionView
        /// </summary>
        public bool IsExpandedAddExecution
        {
            get { return _isExpandedAddExecution; }
            set
            {
                if (_isExpandedAddExecution != value)
                {
                    _isExpandedAddExecution = value;
                    RaisePropertyChanged("IsExpandedAddExecution");
                }
            }
        }
        private bool _isExpandedAddExecution;

        /// <summary>
        /// Hide RunExecutionControl
        /// </summary>
        public Visibility RunExecutionVisibility
        {
            get { return _runExecutionVisibility; }
            set
            {
                if (_runExecutionVisibility != value)
                {
                    _runExecutionVisibility = value;
                    RaisePropertyChanged("RunExecutionVisibility");
                }
            }
        }
        private Visibility _runExecutionVisibility;

        /// <summary>
        /// Hace el efecto de ocultamiento en el control
        /// </summary>
        public bool IsExpandedRunExecution
        {
            get { return _isExpandedRunExecution; }
            set
            {
                if (_isExpandedRunExecution != value)
                {
                    _isExpandedRunExecution = value;
                    RaisePropertyChanged("IsExpandedRunExecution");
                }
            }
        }
        private bool _isExpandedRunExecution;
        
        #endregion

        #endregion

    }
}
