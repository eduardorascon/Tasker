using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Tasker.Helpers;
using Tasker.Model;
using Tasker.Model.QA;
using System.Windows;

namespace Tasker.ViewModel.QA.TestCases
{
   public class RunExecutionMainViewModel:ViewModelBase
    {

       #region Private Member

       private readonly IDataService _dataService;
       public ICommand ShowTestCaseListCommand { get; private set; }
       #endregion

       #region Constructor

       public RunExecutionMainViewModel(IDataService dataService)
       {
           _dataService = dataService;
           InitializeComponent();

           ShowTestCaseListCommand = new RelayCommand(ShowList);
       }

       private void ShowList()
       {
           ShowTestCaseList = !ShowTestCaseList;
       }

       #endregion
       
       #region Properties

       public int RecievedRunExecDetailIdTemp { get; set; }

       /// <summary>
       /// Propiedad que almacena el estado del BlackCanvas
       /// </summary>
       public Visibility ShowBlackBlockScreen
       {
           get { return _showBlackBlockScreen; }
           set
           {
               if (_showBlackBlockScreen != value)
               {
                   _showBlackBlockScreen = value;
                   RaisePropertyChanged("ShowBlackBlockScreen");
               }
           }
       }
       private Visibility _showBlackBlockScreen = Visibility.Collapsed;

       /// <summary>
       /// Propiedad que almacena El efectode expansion del Fault Execution
       /// </summary>
       public bool ExpandFaultExecutionPanel
       {
           get { return _expandFaultExecutionPanel; }
           set
           {
               if (_expandFaultExecutionPanel != value)
               {
                   _expandFaultExecutionPanel = value;
                   RaisePropertyChanged("ExpandFaultExecutionPanel");
               }
           }
       }
       private bool _expandFaultExecutionPanel = false;
       
       /// <summary>
       /// The <see cref="RunExecution" /> property's name.
       /// </summary>
       public const string RunExecutionPropertyName = "RunExecution";

       private RunExecutionItem _runExecution = new RunExecutionItem();

       /// <summary>
       /// Sets and gets the RunExecution property.
       /// Changes to that property's value raise the PropertyChanged event. 
       /// </summary>
       public RunExecutionItem RunExecution
       {
           get
           {
               return _runExecution;
           }

           set
           {
               if (_runExecution == value)
               {
                   return;
               }

               RaisePropertyChanging(RunExecutionPropertyName);
               _runExecution = value;
               RaisePropertyChanged(RunExecutionPropertyName);
           }
       }

       /// <summary>
       /// The <see cref="RunExecutionDetail" /> property's name.
       /// </summary>
       public const string RunExecutionDetailPropertyName = "RunExecutionDetail";

       private RunExecutionDetailItem _runExecutionDetail = new RunExecutionDetailItem();

       /// <summary>
       /// Sets and gets the RunExecutionDetail property.
       /// Changes to that property's value raise the PropertyChanged event. 
       /// </summary>
       public RunExecutionDetailItem RunExecutionDetail
       {
           get
           {
               return _runExecutionDetail;
           }

           set
           {
               if (_runExecutionDetail == value)
               {
                   return;
               }

               RaisePropertyChanging(RunExecutionDetailPropertyName);
               _runExecutionDetail = value;
               RaisePropertyChanged(RunExecutionDetailPropertyName);
           }
       }

       public int ReceivedExecId
       {
           get { return _receivedExecId; }
           set
           {
               if (_receivedExecId != value)
               {
                   _receivedExecId = value;
                   RaisePropertyChanged("ReceivedExecId");
               }
           }
       }
       private int _receivedExecId=0;

       public int RetrieveRunExecId { get; set; }


       
	    public RunExecutionItem RunExecItemDisplay
	    {
		    get { return _runExecItemDisplay;}
		    set 
		    { 
			    if(_runExecItemDisplay != value) 
			    {
				    _runExecItemDisplay = value;
				    RaisePropertyChanged("RunExecItemDisplay");
                    if (RunExecItemDisplay != null)
                        if (RunExecItemDisplay.Porcent == 100)
                            Messenger.Default.Send<bool>(true, "ACTIVE_CLOSERUNEXECUTION_RUNEXECUTIONCONTROLBARVIEWMODEL");
                        else
                            Messenger.Default.Send<bool>(false, "ACTIVE_CLOSERUNEXECUTION_RUNEXECUTIONCONTROLBARVIEWMODEL");
                    else
                        Messenger.Default.Send<bool>(false, "ACTIVE_CLOSERUNEXECUTION_RUNEXECUTIONCONTROLBARVIEWMODEL");
                    

			    }
		    }
	    }
	    private RunExecutionItem _runExecItemDisplay;



       
       #region ShowTestCaseList

       /// <summary>
       /// The <see cref="ShowTestCaseList" /> property's name.
       /// </summary>
       public const string ShowTestCaseListPropertyName = "ShowTestCaseList";

       private bool _showTestCaseList = true;

       /// <summary>
       /// Sets and gets the ShowTestCaseList property.
       /// Changes to that property's value raise the PropertyChanged event. 
       /// </summary>
       public bool ShowTestCaseList
       {
           get
           {
               return _showTestCaseList;
           }

           set
           {
               if (_showTestCaseList == value)
               {
                   return;
               }

               RaisePropertyChanging(ShowTestCaseListPropertyName);
               _showTestCaseList = value;
               RaisePropertyChanged(ShowTestCaseListPropertyName);
           }
       }

       #endregion


       /// <summary>
       /// The <see cref="ReceivedTestCase" /> property's name.
       /// </summary>
       public const string ReceivedTestCasePropertyName = "ReceivedTestCase";

       private TestCaseItem _receivedTestCase = new TestCaseItem();

       /// <summary>
       /// Sets and gets the ReceivedTestCase property.
       /// Changes to that property's value raise the PropertyChanged event. 
       /// </summary>
       public TestCaseItem ReceivedTestCase
       {
           get
           {
               return _receivedTestCase;
           }

           set
           {
               if (_receivedTestCase == value)
               {
                   return;
               }

               RaisePropertyChanging(ReceivedTestCasePropertyName);
               _receivedTestCase = value;
               RaisePropertyChanged(ReceivedTestCasePropertyName);
           }
       }

       
       /// <summary>
       /// The <see cref="ReceivedStepItem" /> property's name.
       /// </summary>
       public const string ReceivedStepItemPropertyName = "ReceivedStepItem";

       private StepItem _receivedStepItem = new StepItem();

       /// <summary>
       /// Sets and gets the ReceivedStepItem property.
       /// Changes to that property's value raise the PropertyChanged event. 
       /// </summary>
       public StepItem ReceivedStepItem
       {
           get
           {
               return _receivedStepItem;
           }

           set
           {
               if (_receivedStepItem == value)
               {
                   return;
               }

               RaisePropertyChanging(ReceivedStepItemPropertyName);
               _receivedStepItem = value;
               RaisePropertyChanged(ReceivedStepItemPropertyName);
           }
       }


       /// <summary>
       /// Almacena el item seleccionado de la lista de Execution Detail
       /// </summary>
       public ExecutionDetailItem ReceivedExecutionDetail
       {
           get { return _receivedExecutionDetail; }
           set
           {
               if (_receivedExecutionDetail != value)
               {
                   _receivedExecutionDetail = value;
                   RaisePropertyChanged("ReceivedExecutionDetail");
               }
           }
       }
       private ExecutionDetailItem _receivedExecutionDetail;

       /// <summary>
       /// Almacena la propiedad que permite respresentar la imagen con el control
       /// </summary>
       public ImageSource ImageSourceProp
       {
           get { return _imageSourceProp; }
           set
           {
               if (_imageSourceProp != value)
               {
                   _imageSourceProp = value;
                   RaisePropertyChanged("ImageSourceProp");
               }
           }
       }
       private ImageSource _imageSourceProp;

       public int TestCaseId
       {
           get { return _testCaseId; }
           set
           {
               if (_testCaseId != value)
               {
                   _testCaseId = value;
                   RaisePropertyChanged("TestCaseId");
               }
           }
       }
       private int _testCaseId = 0;

        
       #endregion

       #region Private Methods

        /// <summary>
        /// Inicializa las variables y registra los mensajes.
        /// </summary>
        void InitializeComponent()
        {
            ShowFaultPanel(false);
            RecievedRunExecDetailIdTemp = 0;
            // Listen the Messenger
            Messenger.Default.Register<bool>(this, "CLEANALL_PROPERTY_RUNEXECUTIONMAINVIEWMODEL", CleanAllControls);
            Messenger.Default.Register<StepItem>(this, "SET_RECEIVEDTESTPLANIDPROPERTY_RUNEXECUTIONMAINVIEWMODEL", SetReceivedStepItemProperty);
            Messenger.Default.Register<ExecutionItem>(this, "SET_RECEIVEDEXECIDPROPERTY_RUNEXECUTIONMAINVIEWMODEL", SetReceivedExecIdProperty);
            Messenger.Default.Register<ExecutionDetailItem>(this, "SET_SETRECEIVEDEXECUTIONDETAILPROPERTY_RUNEXECUTIONMAINVIEWMODEL", SetReceivedExecutionDetailProperty);
            //Messenger.Default.Register<PaqueteMSJ>(this, "SET_RECEIVEDSTEPITEM_IN_ADDSTEPSMAINVM_RUNEXECUTIONMAINVIEWMODEL", SetRunExecutionProperties);
            Messenger.Default.Register<PaqueteMSJ>(this, "SET_RECEIVEDSTEPITEM_IN_ADDSTEPSMAINVM_RUNEXECUTIONMAINVIEWMODEL", SetRunExecutionDetailProperties);
            Messenger.Default.Register<bool>(this, "SAVE_DOYOUAPPROVETHESTEP_RUNEXECUTIONMAINVIEWMODEL", DoYouApproveTheStep);
            Messenger.Default.Register<bool>(this, "SHOW_FAULTEXECUTIONPANEL_RUNEXECUTIONMAINVIEWMODEL", ShowFaultPanel);//pendiente para utilizar y ocutlar el panel de fault
            Messenger.Default.Register<int>(this, "SET_TESTCASEID_RUNEXECUTIONFAULTEXCEPTIONMAINVM", SetTestCaseId);
            Messenger.Default.Register<bool>(this, "CLOSE_RUNEXECUTIONHEADER_RUNEXECUTIONMAINVIEWMODEL", CloseRunExecution);
            Messenger.Default.Register<int>(this, "SET_RUNEXECUTION_RUNEXECUTIONMAINVIEWMODEL", GetRunExecution);
        }

        /// <summary>
        /// Set la variable TestCaseId
        /// </summary>
        /// <param name="_receivedItem"></param>
        private void SetTestCaseId(int _receivedItem)
        {
            TestCaseId = _receivedItem;
        }

        /// <summary>
        /// Muestra u oculta el panel de Rejected
        /// </summary>
        /// <param name="tempOption"></param>
        private void ShowFaultPanel(bool tempOption)
        {
            if (tempOption == true)
            {
                ShowBlackBlockScreen = Visibility.Visible;
                ExpandFaultExecutionPanel = true;
            }
            else
            {
                ShowBlackBlockScreen = Visibility.Collapsed;
                ExpandFaultExecutionPanel = false;
            }
        }

        /// <summary>
        /// Acepta el paso que está selecionado
        /// </summary>
        /// <param name="_approveTheStep"></param>
        private void DoYouApproveTheStep(bool _approveTheStep)
        {
            RunExecutionItem newExecHeader = new RunExecutionItem();
            RunExecutionDetailItem newExecDetail = new RunExecutionDetailItem();
            newExecHeader = GetRunExecHeader();
            newExecDetail = GetRunExecDetail();
            if (ReceivedExecId != 0)
            {
                if (_approveTheStep == true)
                {
                    SaveRunExecution(newExecHeader);
                    newExecDetail.Approved = "Approved";
                    GetRunExecutionDetailIdAndSendToFaultResult();
                    newExecDetail.RunExecDetailId = RecievedRunExecDetailIdTemp;
                    SaveRunExecutionDetail(newExecDetail);
                    //refrescar RunExecItem Pendiente
                    GetRunExecution(newExecDetail.RunExecId);
                }
                else
                {
                    SaveRunExecution(newExecHeader);
                    newExecDetail.Approved = "Rejected";
                    GetRunExecutionDetailIdAndSendToFaultResult();
                    Messenger.Default.Send<RunExecutionDetailItem>(newExecDetail, "SET_RUNEXECDETAILITEM_RUNEXECUTIONFAULTEXCEPTIONMAINVM");//Envia el objeto y no lo salva hasta que guarda el fault
                  
                    ShowFaultPanel(true);
                }
            }
            else
                TaskerHelper.SetStatusBarMessage(
                      "Could not save");
        }

        /// <summary>
        /// Obtiene el item rechazado y lo envia al view de faultExecution.
        /// </summary>
        private void GetRunExecutionDetailIdAndSendToFaultResult()
       {
           _dataService.GetRunExecutionDetailSaved(TestCaseId, ReceivedStepItem.StepId, ReceivedExecId, AfterGetRunExecDetailId);
       }
       
        private void AfterGetRunExecDetailId(int tempId, Exception exception)
       {
           RecievedRunExecDetailIdTemp = tempId;

           Messenger.Default.Send<int>(RecievedRunExecDetailIdTemp, "SET_RECEIVEDRUNEXECUTIONDETAILIDPROP_RUNEXECUTIONFAULTEXCEPTIONMAINVM");
       }

        /// <summary>
        /// Obtiene la Run Execution
        /// </summary>
        /// <param name="tempRunExec"></param>
        private void GetRunExecution(int tempRunExec)
       {
           _dataService.GetRunExecutionByRunExecId(tempRunExec, AfterGetRunExec);
       }

        private void AfterGetRunExec(RunExecutionItem tempId, Exception exception)
       {
           RunExecItemDisplay = tempId;
       }

        /// <summary>
        /// Set la varaible ReceivedExecID
        /// </summary>
        /// <param name="_tempRecievedExecId"></param>
        private void SetReceivedExecIdProperty(ExecutionItem _tempRecievedExecId)
        {
            ReceivedExecId = _tempRecievedExecId.ExecId;
            if (ReceivedExecId != 0)
            {
                GetRunExecutionIdFromExecId(_tempRecievedExecId.ExecId);
                Messenger.Default.Send<int>(ReceivedExecId, "SET_EXECUTIONID_RUNEXECUTIONFAULTEXCEPTIONMAINVM");
            }
        }

        /// <summary>
        /// Obtiene la RunExecution Id a partir de la  ExecutionId
        /// </summary>
        /// <param name="tempRunExec"></param>
        private void GetRunExecutionIdFromExecId(int tempRunExec)
        {
            _dataService.GetRunExecutionFromExecutionId(tempRunExec, AfterGetRunExecId);
        }

        private void AfterGetRunExecId(int tempId, Exception exception)
        {
            RetrieveRunExecId = tempId;
            if (RetrieveRunExecId != 0)
                GetRunExecution(RetrieveRunExecId);
        }

        /// <summary>
        /// Obtiene el Header de la Run Execution
        /// </summary>
        /// <returns></returns>
        private RunExecutionItem GetRunExecHeader()
        {
            RunExecutionItem _newItem = new RunExecutionItem();
            _newItem.ExecId = ReceivedExecId;
           
            return _newItem;
        }

        /// <summary>
        /// Obtiene el detalle de la RunExecution
        /// </summary>
        /// <returns></returns>
        private RunExecutionDetailItem GetRunExecDetail()
        {
            RunExecutionDetailItem _newItem = new RunExecutionDetailItem();
            _newItem.StepId= ReceivedStepItem.StepId;
            _newItem.TestCaseId = TestCaseId;
            return _newItem;
        }

        /// <summary>
        /// Set la variable ReceviedExecutionDetail Prop
        /// </summary>
        /// <param name="TempObject"></param>
        private void SetReceivedExecutionDetailProperty(ExecutionDetailItem TempObject)
        {
            if (TempObject != null)
            {
                ReceivedExecutionDetail = TempObject;
            }
        }

        /// <summary>
        /// Salva la RunExecutionDetail.
        /// </summary>
        /// <param name="_tempReceivedItem"></param>
        private void SaveRunExecutionDetail(RunExecutionDetailItem _tempReceivedItem)
        {
            {
                _dataService.SaveRunExecutionDetail(_tempReceivedItem, ReceivedExecId, (resulDTO, exception) =>
                {
                    if (!resulDTO.HasError)
                    {
                    }
                    TaskerHelper.SetStatusBarMessage(
                         resulDTO.Message);
                });
            }
        }

        /// <summary>
        /// Guarda la Run Execution
        /// </summary>
        /// <param name="_tempRecievedItem"></param>
        private void SaveRunExecution(RunExecutionItem _tempRecievedItem)
        {
            _dataService.SaveRunExecutionHeader(_tempRecievedItem, (resulDTO, exception) =>
                {
                    if (!resulDTO.HasError)
                    {
                        //Notificar que fue guardao exitosamente pendiente.
                        //Messenger.Default.Send<bool>(true, "CLEANCONTROL_ADDSTEPSVIEWMODEL");
                        //Messenger.Default.Send<bool>(true, "FILL_STEPITEMLIST_STEPLISTVIEWMODEL");
                    }
                    TaskerHelper.SetStatusBarMessage(
                         resulDTO.Message);
                });
        }

        /// <summary>
        /// Actauliza el Estado de la ejecución.
        /// </summary>
        /// <param name="_execTempItem"></param>
        /// <param name="_testCaseItem"></param>
        private void UpdateStatusExecution(int _execTempItem, int _testCaseItem)
        {
            _dataService.UpdateExecutionDetailStatus(_execTempItem, _testCaseItem, (resulDTO, exception) =>
            {
                if (!resulDTO.HasError)
                {
                }
            });
        }

        /// <summary>
        ///  Restablece los controles.
        /// </summary>
        /// <param name="restart"></param>
        private void CleanAllControls(bool restart)
        {
            if (restart == true)
            {
                ImageSourceProp = null;
                ReceivedExecutionDetail = new ExecutionDetailItem();
                ReceivedStepItem = new StepItem();
                RunExecutionDetail = new RunExecutionDetailItem();
                RunExecution = new RunExecutionItem();
            }
        }

        /// <summary>
        /// set la variable ReceivedStep
        /// </summary>
        /// <param name="receivedTempProperty"></param>
        private void SetReceivedStepItemProperty(StepItem receivedTempProperty)
        {
            if (receivedTempProperty !=null)
                if (receivedTempProperty.StepId != 0)
                {
                    
                    ReceivedStepItem = receivedTempProperty;
                    ShowPictureFromDB(ReceivedStepItem.Image);
                }
        }

        /// <summary>
       /// Convert a Byte[] into imageSource
       /// </summary>
       /// <param name="data"></param>
        private void ShowPictureFromDB(byte[] data)
        {
            if (data.Length != 0)
            {
                BitmapImage biImg = new BitmapImage();
                MemoryStream ms = new MemoryStream(data);
                biImg.BeginInit();
                biImg.StreamSource = ms;
                biImg.EndInit();

                ImageSource imgSrc = biImg as ImageSource;

                ImageSourceProp = imgSrc;
            }
            else
            {
                ImageSourceProp = null;
            }
        }

        /// <summary>
        /// Set la variable en base a las modificaciones en el View
        /// </summary>
        /// <param name="optionTemp"></param>
        private void SetRunExecutionProperties(PaqueteMSJ optionTemp)
        {
            switch (optionTemp.NombrePropiedad)
            {
                case "RunExecId":
                    RunExecution.RunExecId  = Convert.ToInt32(optionTemp.Informacion);
                    break;

                case "ExecId":
                    RunExecution.ExecId     =  Convert.ToInt32(optionTemp.Informacion);
                    break;

                case "Porcent":
                    RunExecution.Porcent    =  Convert.ToInt32(optionTemp.Informacion);
                    break;
            }
        }

        /// <summary>
        /// Set la variable en base a las modificaciones en el View
        /// </summary>
        /// <param name="optionTemp"></param>
        private void SetRunExecutionDetailProperties(PaqueteMSJ optionTemp)
        {
            switch (optionTemp.NombrePropiedad)
            {
                case "RunExecDetailId":
                    RunExecutionDetail.RunExecDetailId  = Convert.ToInt32(optionTemp.Informacion);
                    break;

                case "RunExecId":
                    RunExecutionDetail.RunExecId        = Convert.ToInt32(optionTemp.Informacion);
                    break;

                case "TestCaseId":
                    RunExecutionDetail.TestCaseId       = Convert.ToInt32(optionTemp.Informacion);
                    break;

                case "StepId":
                    RunExecutionDetail.StepId           = Convert.ToInt32(optionTemp.Informacion);
                    break;

                case "Approved":
                    RunExecutionDetail.Approved         = optionTemp.Informacion;
                    break;
            }
        }

        /// <summary>
        /// Cierra la RunExecution cuando está está al 100 y el usuario lo aprueba.
        /// </summary>
        /// <param name="option"></param>
        private void CloseRunExecution(bool option)
        {
            if (option == true)
            {
                if (RunExecItemDisplay != null)
                    if (RunExecItemDisplay.RunExecId != 0)
                        CloseRunExecutionInDB(RunExecItemDisplay);
                    else
                        TaskerHelper.SetStatusBarMessage(
                            "A problema has ocurred the item could not be closed");
            }
        }

        /// <summary>
        /// Método que cierra la Execution.
        /// </summary>
        /// <param name="_tempReceivedItem"></param>
        private void CloseRunExecutionInDB(RunExecutionItem _tempReceivedItem)
        {
            {
                _dataService.CloseRunExecutionHeader(_tempReceivedItem, (resulDTO, exception) =>
                {
                    if (!resulDTO.HasError)
                    {
                    }
                    TaskerHelper.SetStatusBarMessage(
                        resulDTO.Message);
                });
            }
        }

        #endregion

    }
}
