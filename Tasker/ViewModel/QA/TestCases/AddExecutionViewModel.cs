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
using Tasker.Model.QA;

namespace Tasker.ViewModel.QA.TestCases
{
    public class AddExecutionViewModel: ViewModelBase
    {

        #region private Member
		private readonly IDataService _dataService;
		#endregion

		#region Constructor
		/// <summary>
		/// Constructor de la clase
		/// </summary>
        public AddExecutionViewModel(IDataService dataService)
		{
			_dataService = dataService;
			InitializeCommands();
		}

		#endregion

        #region  Methods

        /// <summary>
        /// Set una string para saber que accion quiere tomar el usuario
        /// </summary>
        /// <param name="opcion"></param>
        private void Set_WhatDoYouWantToDo(string opcion)
        {
            WhatDoYouWantToDo = opcion;
        }

        /// <summary>
        /// Inicializa los controles a vacio.
        /// </summary>
        /// <param name="doYouWantToClean"></param>
        private void CleanControls(bool doYouWantToClean)
        {
            if (doYouWantToClean == true)
            {
                ExecutionHeaderItem = new ExecutionItem();
                ReceivedExecutionId = 0;
                ReceivedTestCase = new TestCaseItem();
                EnableAddTestCase = false;
                EnableRemoveTestCase = false;
            }
        }

        /// <summary>
        /// Recibe un OBjeto de tipo TestCae.
        /// </summary>
        /// <param name="tempTestCase"></param>
        private void SetReceivedTestCase(TestCaseItem tempTestCase)
        {
            if (tempTestCase != null)
            {
                ReceivedTestCase = new TestCaseItem();
                ReceivedTestCase = tempTestCase;
            }
            
        }

        /// <summary>
        /// Recibe el Id de la Execution
        /// </summary>
        /// <param name="TempIdExec"></param>
        private void SetReceivedExecutionId(int TempIdExec)
        {
            ReceivedExecutionId = TempIdExec;
        }

        /// <summary>
        /// Inicializa algunas variables, y registra los mensajes.
        /// </summary>
        private void InitializeCommands()
        {
            CleanControls(true);
            AddTestCaseCommand = new RelayCommand(AddTestCase);
            RemoveTestCaseCommand = new RelayCommand(RemoveTestCase);

            //Register Messages
            Messenger.Default.Register<bool>(this, "CLEAN_CLEANCONTROL_ADDEXECUTIONVIEWMODEL", CleanControls);
            Messenger.Default.Register<string>(this, "SET_WHATDOYOUWHANTTODO_ADDSTEPSMAINVM", Set_WhatDoYouWantToDo);
            Messenger.Default.Register<PaqueteMSJ>(this, "SET_RECEIVEDEXECUTIONITEM_IN_ADDEXECUTIONVIEWMODEL", SetExecutionPropertyFromContructor);
            //Messenger.Default.Register<StepItem>(this, "SET_RECEIVEDSTEPITEMFROMLIST_ADDEXECUTIONVIEWMODEL", SetReceivedStepItemFromStepListViewModelVM);
            Messenger.Default.Register<int>(this, "SET_SELECTEDTESTCASEID_ADDEXECUTIONVIEWMODEL", SetReceivedExecutionId);
            Messenger.Default.Register<TestCaseItem>(this, "SET_RECEIVEDTESTCASE_ADDEXECUTIONVIEWMODEL", SetReceivedTestCase);
            Messenger.Default.Register<bool>(this, "SAVE_ADDEXECUTIONHEADER_ADDEXECUTIONVIEWMODEL", BarraPrincipalBotones);
            Messenger.Default.Register<ExecutionItem>(this, "SET_RECEIVEDEXECUTIONHEADER_ADDEXECUTIONVIEWMODEL", SetReceivedExecutionHeader);
            Messenger.Default.Register<bool>(this,"SET_ENABLEADDTESTCASEBUTTOM_ADDEXECUTIONVIEWMODEL", SetEnableAddTestCaseButtom);
            Messenger.Default.Register<bool>(this, "SET_ENABLEREMOVETESTCASEBUTTOM_ADDEXECUTIONVIEWMODEL", SetEnableRemoveTestCaseButtom);
            Messenger.Default.Register<ExecutionDetailItem>(this, "SET_SETRECEIVEDEXECUTIONDETAILITEM_ADDEXECUTIONVIEWMODEL", SetReceivedExecutionDetailItem);
        }

        /// <summary>
        /// Set Un objeto de tipo Execution Detail.
        /// </summary>
        /// <param name="receivedItem"></param>
        private void SetReceivedExecutionDetailItem(ExecutionDetailItem receivedItem)
        {
            if (receivedItem != null)
                ReceivedExecutionDetailItemForErase = receivedItem;
        }

        /// <summary>
        /// agrega un nuevo Test Case
        /// </summary>
        private void AddTestCase()
        {
            if(ExecutionHeaderItem!=null)
                if (string.IsNullOrWhiteSpace(ExecutionHeaderItem.Description)|| string.IsNullOrEmpty(ExecutionHeaderItem.Description))
                    TaskerHelper.SetStatusBarMessage("You Must Fill the description in order to continue");
                else
                {
                    if (ExecutionHeaderItem.ExecId == 0)
                    {
                        InsertExecutionHeaderInDB();
                    }
                    EnableAddTestCase = false;
                    bool tempObjectPassTheTest = ValidateExecutionDetail();
                    if (tempObjectPassTheTest == true)
                    {
                        ExecutionDetailItem tempItem = new ExecutionDetailItem();
                        tempItem = SetExecDetailItem();
                        _dataService.SaveExecutionDetail(tempItem, (resulDTO, exception) =>
                        {
                            if (!resulDTO.HasError)
                            {
                                Messenger.Default.Send<bool>(true, "FILL_TESTCASEITEMLIST_ADDEXECUTIONLISTVIEWMODEL");
                                WhatDoYouWantToDo = string.Empty;
                            }
                            TaskerHelper.SetStatusBarMessage(
                                 resulDTO.Message);
                        });
                    }
                    else
                    {
                        TaskerHelper.SetStatusBarMessage("The item Wasn't Save.");
                    }
                }
        }

        /// <summary>
        /// Remueve un objeto de Tipo Test Case
        /// </summary>
        private void RemoveTestCase()
        {
            EnableRemoveTestCase = false;
            //selected test case = null
            if (ReceivedExecutionDetailItemForErase != null)
                if (ReceivedExecutionDetailItemForErase.ExecId != 0)
                { 

                }

            if (ReceivedExecutionDetailItemForErase != null)
                if (ReceivedExecutionDetailItemForErase.ExecDetailId != 0)
                    {
                        ReceivedExecutionDetailItemForErase.IsErase = true;
                        _dataService.SaveExecutionDetail(ReceivedExecutionDetailItemForErase, (resulDTO, exception) =>
                        {
                            if (!resulDTO.HasError)
                            {
                                Messenger.Default.Send<bool>(true, "FILL_TESTCASEITEMLIST_ADDEXECUTIONLISTVIEWMODEL");
                                WhatDoYouWantToDo = string.Empty;
                            }
                            TaskerHelper.SetStatusBarMessage(
                                 resulDTO.Message);
                        });
                    }
        }

        /// <summary>
        /// Método que se encarga de validar los objetos
        /// </summary>
        /// <returns></returns>
        private bool ValidateExecutionDetail()
        {
            int contador = 0;
            if (ExecutionHeaderItem == null)
                if (ExecutionHeaderItem.ExecId==0)
                    contador++;
      

            if (contador == 0)
                return true;
            else
                return false;

        }

        /// <summary>
        /// Retorna un objeto de tipo ExecDetail, que fue salvado.
        /// </summary>
        /// <returns></returns>
        private ExecutionDetailItem SetExecDetailItem()
        {
            ExecutionDetailItem tempItem = new ExecutionDetailItem();
            tempItem.ExecId = ExecutionHeaderItem.ExecId;
            tempItem.IsErase = false;
            tempItem.TestCaseId = ReceivedTestCase.TestCaseId;
            if (ExecutionHeaderItem.ExecId == 0)
            {
                _dataService.GetLastExecutionCreated(AfterGetLatestExecutionItem);
                tempItem.ExecId = ReceivedExecutionId;
            }
            else
                tempItem.ExecId = ExecutionHeaderItem.ExecId;


            return tempItem;
        }

        private void AfterGetLatestExecutionItem(ExecutionItem tempExecutionItem, Exception exception)
        {
            if (tempExecutionItem != null)
            {
                ExecutionHeaderItem = tempExecutionItem;
                ReceivedExecutionId = tempExecutionItem.ExecId;
            }
                
        }

        /// <summary>
        /// Maneja los estado enable de los botones
        /// </summary>
        /// <param name="doYouWantoToEnable"></param>
        private void SetEnableAddTestCaseButtom(bool doYouWantoToEnable)
        {
            EnableAddTestCase = doYouWantoToEnable;
        }

        /// <summary>
        /// Maneja los estado enable de los botones
        /// </summary>
        /// <param name="doYouWantToEnable"></param>
        private void SetEnableRemoveTestCaseButtom(bool doYouWantToEnable)
        {
            EnableRemoveTestCase = doYouWantToEnable;
        }

        /// <summary>
        /// Recibe el obejto de tipo execution.
        /// </summary>
        /// <param name="tempReceivedExecution"></param>
        private void SetReceivedExecutionHeader(ExecutionItem tempReceivedExecution)
        {
            if (tempReceivedExecution!=null)
            ExecutionHeaderItem = tempReceivedExecution;
        }

        /// <summary>
        /// Método que ejecuta los botones de la barra principal.
        /// </summary>
        /// <param name="?"></param>
        private void BarraPrincipalBotones(bool doYouWantToSave)
        {
            if (doYouWantToSave == true)
                SaveSteps();
        }

        /// <summary>
        /// Salva los steps, de la execution.
        /// </summary>
        private void SaveSteps()
        {
                switch (WhatDoYouWantToDo)
                {
                    case "NEW":
                        InsertExecutionHeaderInDB();
                        break;

                    case "EDIT":
                        EditStepInDB();
                        break;
                }
            
        }

        /// <summary>
        /// Agrega un nuevo item a la DB de tipo Execution Header.
        /// </summary>
        private void InsertExecutionHeaderInDB()
        {
            if (ValidateStepsItem(ExecutionHeaderItem) == true)
            {
                ExecutionHeaderItem.IsNew = true;

                _dataService.SaveExecutionHeader(ExecutionHeaderItem, (resulDTO, exception) =>
                {
                    if (!resulDTO.HasError)
                    {
                        //Notificar que fue guardao exitosamente pendiente.
                        Messenger.Default.Send<bool>(true, "REFRESH_EXECUTIONLIST_EXECUTIONLISTVIEWMODEL");
                       // Messenger.Default.Send<bool>(false, "SHOW_SHOWEXECUTIONVIEW_EXECUTIONMAINVIEWMODEL"); oculta la ventana
                        Messenger.Default.Send<bool>(true, "FILL_TESTCASEITEMLIST_ADDEXECUTIONLISTVIEWMODEL");
                        _dataService.GetLastExecutionCreated(AfterGetLatestExecutionItem);
                        WhatDoYouWantToDo = string.Empty;
                    }
                    resulDTO.Message = "The New Item has been save.";
                    TaskerHelper.SetStatusBarMessage(
                         resulDTO.Message);
                });
            }
        }

        /// <summary>
        /// Modifica un item de la DB.
        /// </summary>
        private void EditStepInDB()
        {

            if (ValidateStepsItem(ExecutionHeaderItem) == true)
            {
                ExecutionHeaderItem.IsNew = false;

                _dataService.SaveExecutionHeader(ExecutionHeaderItem, (resulDTO, exception) =>
                {
                    if (!resulDTO.HasError)
                    {
                        //Messenger.Default.Send("Collapse",
                        //"TESTCASE_EDIT_MODE");

                        //Notificar que fue guardao exitosamente pendiente.
                        Messenger.Default.Send<bool>(true, "REFRESH_EXECUTIONLIST_EXECUTIONLISTVIEWMODEL");
                        Messenger.Default.Send<bool>(false, "SHOW_SHOWEXECUTIONVIEW_EXECUTIONMAINVIEWMODEL");
                        //Messenger.Default.Send<string>("SAVE", "SET_ENABLEBUTTOMPROPERTY_TESTCASECONTROLBARVM");
                        //Messenger.Default.Send<int>(0, "SET_SELECTEDTESTPLANID_TESTCASECONTROLBARVM");
                        WhatDoYouWantToDo = string.Empty;
                    }
                    resulDTO.Message = "The item has been successfully modified.";
                    TaskerHelper.SetStatusBarMessage(
                         resulDTO.Message);
                });
            }
        }

        /// <summary>
        /// Valida el objeto
        /// </summary>
        /// <param name="tempStep"></param>
        /// <returns></returns>
        private bool ValidateStepsItem(ExecutionItem tempStep)
        {
            int contador = 0;

            if (string.IsNullOrEmpty(tempStep.Description) || string.IsNullOrWhiteSpace(tempStep.Description))
            {
                TaskerHelper.SetStatusBarMessage("You must fill the description in order to continue.");
                contador++;
            }

            if (contador == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Set las modificaciones del View.
        /// </summary>
        /// <param name="optionTemp"></param>
        private void SetExecutionPropertyFromContructor(PaqueteMSJ optionTemp)
        {
            switch (optionTemp.NombrePropiedad)
            {
                case "Description":
                    ExecutionHeaderItem.Description = optionTemp.Informacion;
                    break;

                case "Status":
                    ExecutionHeaderItem.Status = optionTemp.Informacion;
                    break;
            }
        }

        #endregion

        #region Properties

        public string WhatDoYouWantToDo { get; set; }

        public TestCaseItem ReceivedTestCase { get; set; }

        public int ReceivedExecutionId { get; set; }

        public ExecutionDetailItem ReceivedExecutionDetailItemForErase { get; set; }

        public bool EnableAddTestCase
        {
            get { return _enableAddTestCase; }
            set
            {
                if (_enableAddTestCase != value)
                {
                    _enableAddTestCase = value;
                    RaisePropertyChanged("EnableAddTestCase");
                }
            }
        }
        private bool _enableAddTestCase;


        public bool EnableRemoveTestCase
        {
            get { return _enableRemoveTestCase; }
            set
            {
                if (_enableRemoveTestCase != value)
                {
                    _enableRemoveTestCase = value;
                    RaisePropertyChanged("EnableRemoveTestCase");
                }
            }
        }
        private bool _enableRemoveTestCase;


        /// <summary>
        /// The <see cref="ExecutionHeaderItem" /> property's name.
        /// </summary>
        public const string ExecutionHeaderItemPropertyName = "ExecutionHeaderItem";

        private ExecutionItem _executionItem = new ExecutionItem();

        /// <summary>
        /// Sets and gets the ExecutionHeaderItem property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ExecutionItem ExecutionHeaderItem
        {
            get
            {
                return _executionItem;
            }

            set
            {
                if (_executionItem == value)
                {
                    return;
                }

                RaisePropertyChanging(ExecutionHeaderItemPropertyName);
                _executionItem = value;
                RaisePropertyChanged(ExecutionHeaderItemPropertyName);
                if (ExecutionHeaderItem != null)
                {
                    if (ExecutionHeaderItem.ExecId != 0)
                        Messenger.Default.Send<int>(ExecutionHeaderItem.ExecId, "SET_RECEIVEDEXECUTIONIDPROPERTY_ADDEXECUTIONLISTVIEWMODEL");
                    Messenger.Default.Send<bool>(true, "FILL_TESTCASEITEMLIST_ADDEXECUTIONLISTVIEWMODEL");
                }
            }
        }



        #endregion

        #region Command

        public RelayCommand AddTestCaseCommand { get; set; }
        
		public RelayCommand RemoveTestCaseCommand { get; set; }
		
        #endregion

    }
}
