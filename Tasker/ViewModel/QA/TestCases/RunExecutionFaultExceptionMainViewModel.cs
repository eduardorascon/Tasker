using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Tasker.Helpers;
using Tasker.Model;
using Tasker.Model.QA;

namespace Tasker.ViewModel.QA.TestCases
{
   public class RunExecutionFaultExceptionMainViewModel:ViewModelBase
    {

       #region Private Member
       private readonly IDataService _dataService;
       #endregion

       #region Constructor

       public RunExecutionFaultExceptionMainViewModel(IDataService dataService)
       {
           _dataService = dataService;
           InitializeComponents();
       }

       #endregion

       #region Comandos

       public RelayCommand AttachImageCommand { get; set; }
       public RelayCommand CopyFromClipBoardCommand { get; set; }

       #endregion

       #region Properties

        public int ReceivedRunExecutionDetailId
        {
            get { return _receivedRunExecutionDetailId; }
            set
            {
                if (_receivedRunExecutionDetailId != value)
                {
                    _receivedRunExecutionDetailId = value;
                    RaisePropertyChanged("ReceivedRunExecutionDetailId");
                }
            }
        }
        private int _receivedRunExecutionDetailId;

        byte[] bytes = new byte[0];

        public string ImagePath
        {
            get { return _imagePath; }
            set
            {
                if (_imagePath != value)
                {
                    _imagePath = value;
                    RaisePropertyChanged("ImagePath");
                }
            }
        }
        private string _imagePath;

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

        /// <summary>
        /// The <see cref="FaultExecution" /> property's name.
        /// </summary>
        public const string FaultExecutionPropertyName = "FaultExecution";

        private FaultExecutionItem _faultExecution = new FaultExecutionItem();

        /// <summary>
        /// Sets and gets the FaultExecution property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public FaultExecutionItem FaultExecution
        {
            get
            {
                return _faultExecution;
            }

            set
            {
                if (_faultExecution == value)
                {
                    return;
                }

                RaisePropertyChanging(FaultExecutionPropertyName);
                _faultExecution = value;
                RaisePropertyChanged(FaultExecutionPropertyName);
            }
        }

        /// <summary>
        /// Recive el item que ha sido rechazado si el usuario cancela se borra no se guarda
        /// </summary>
        public RunExecutionDetailItem ReceivedRunExecutionDetailObject
        {
            get { return _receivedRunExecutionDetailObject; }
            set
            {
                if (_receivedRunExecutionDetailObject != value)
                {
                    _receivedRunExecutionDetailObject = value;
                    RaisePropertyChanged("ReceivedRunExecutionDetailObject");
                }
            }
        }
        private RunExecutionDetailItem _receivedRunExecutionDetailObject = new RunExecutionDetailItem();


        public int ReceivedRunExecFromRunExecPanel
        {
            get { return _receivedRunExecFromRunExecPanel; }
            set
            {
                if (_receivedRunExecFromRunExecPanel != value)
                {
                    _receivedRunExecFromRunExecPanel = value;
                    RaisePropertyChanged("ReceivedRunExecFromRunExecPanel");
                }
            }
        }
        private int _receivedRunExecFromRunExecPanel = 0;
        
        

        #endregion

       #region Private Methods

        /// <summary>
        /// Inicializa las variables y Registra los mensajes.
        /// </summary>
        private void InitializeComponents()
        {
            //command
            AttachImageCommand = new RelayCommand(SearchPicture);
            CopyFromClipBoardCommand = new RelayCommand(CopyImageFromClipboard);

            //Listen to Messages
            Messenger.Default.Register<PaqueteMSJ>(this, "SET_FAULTEXECUTIONPROPERTY_IN_RUNEXECUTIONFAULTEXCEPTIONMAINVM_FROMCONSTRUCTOR", SetFaultExecutionPropertyFromContructor);
            Messenger.Default.Register<int>(this, "SET_RECEIVEDRUNEXECUTIONDETAILIDPROP_RUNEXECUTIONFAULTEXCEPTIONMAINVM", SetReceivedRunExecutionDetailIdProp);
            Messenger.Default.Register<RunExecutionDetailItem>(this, "SET_RUNEXECDETAILITEM_RUNEXECUTIONFAULTEXCEPTIONMAINVM", SetReceiveRunExecDetail);
            Messenger.Default.Register<bool>(this, "ACTION_MAINBUTTONBAR_RUNEXECUTIONFAULTEXCEPTIONMAINVM", BarraPrincipalBotones);
            Messenger.Default.Register<bool>(this, "CLEANCONTROL_RUNEXECUTIONFAULTEXCEPTIONMAINVM", Clean_Control);
            Messenger.Default.Register<int>(this, "EXIST_FAULTEXECUTION_RUNEXECUTIONFAULTEXCEPTIONMAINVM", ExistFaultExecution);
            Messenger.Default.Register<int>(this, "SET_EXECUTIONID_RUNEXECUTIONFAULTEXCEPTIONMAINVM", SetExecutionIdFromRunExecform);
            
        }
        
        /// <summary>
        /// Recibe un entero y lo iguala a ReceivedRunExecFromRunExecPanel
        /// </summary>
        /// <param name="objId">ExecutionId</param>
        private void SetExecutionIdFromRunExecform(int objId)
        {
            if (objId != 0)
            {
                ReceivedRunExecFromRunExecPanel = objId;
            }
                
        }

        /// <summary>
        /// Método que verifica si existe la Falla
        /// </summary>
        /// <param name="temp_RunExecDetailId"></param>
        private void ExistFaultExecution(int temp_RunExecDetailId)
        {
            _dataService.GetFaultExecutionCreated(temp_RunExecDetailId, AfterGetRunExecDetailId);
        }

        /// <summary>
        /// Recibe un objeto y lo iguala a RunExecutionDetail
        /// </summary>
        /// <param name="oReceived"></param>
        private void SetReceiveRunExecDetail(RunExecutionDetailItem oReceived)
        {
            if (oReceived != null)
            {
                ReceivedRunExecutionDetailObject = oReceived;
            }
                
        }

        private void AfterGetRunExecDetailId(FaultExecutionItem faultExecTemp, Exception exception)
        {
            if (faultExecTemp.FaultExecId != 0)
            {
                FaultExecution = faultExecTemp;
                ShowPictureFromDB(FaultExecution.Image);
                bytes = FaultExecution.Image;
            }
            
        }
        
        /// <summary>
        /// Ejecuta la opración en base a lo selecionado en la barra de botonoes.
        /// </summary>
        /// <param name="option"></param>
        private void BarraPrincipalBotones(bool option)
        {
            if (option == true)
            {
                saveFaultExecution();
            }
        }

        /// <summary>
        /// Método que guarda la FaultExecution
        /// </summary>
        private void saveFaultExecution()
        {

            if (ValidateFaultExecutionItem(FaultExecution) == true)
            {
                SaveRunExecutionDetail(ReceivedRunExecutionDetailObject);//no lo voy a salvar lo voy a enviar a fault execution
                if (FaultExecution.RunExecDetailId == 0)
                GetRunExecutionDetailIdAndSendToFaultResult(ReceivedRunExecutionDetailObject); // llevar este metodo al otro lado , el método recibe 3 para metros step ,testcaseid ,execid
                /////guardar antes el runexecdetail

                FaultExecution.Image = bytes;
                FaultExecution.Status = "Habilitado";
                _dataService.SaveFaultExecutionItem(FaultExecution, (resulDTO, exception) =>
                {
                    if (!resulDTO.HasError)
                    {
                        Messenger.Default.Send<bool>(false, "SHOW_FAULTEXECUTIONPANEL_RUNEXECUTIONMAINVIEWMODEL");
                        Messenger.Default.Send<bool>(true, "COLLAPSE_RUNEXECUTIONPANEL_RUNEXECUTIONCONTROLBARVIEWMODEL");
                        Clean_Control(true);
                    }

                    TaskerHelper.SetStatusBarMessage(
                        resulDTO.Message);
                });
            }
        }

        /// <summary>
        /// Guarda en la DB la RunExecutionDetail.
        /// </summary>
        /// <param name="_tempReceivedItem"></param>
        private void SaveRunExecutionDetail(RunExecutionDetailItem _tempReceivedItem)
        {
            {
                _dataService.SaveRunExecutionDetail(_tempReceivedItem, ReceivedRunExecFromRunExecPanel, (resulDTO, exception) =>
                {
                    if (!resulDTO.HasError)
                    {
                    }
                    //TaskerHelper.SetStatusBarMessage(
                    //     resulDTO.Message);
                });
            }
        }

        /// <summary>
        /// Obtiene la RunExecution y lo envia al ViewModel RunExecutionMain.
        /// </summary>
        /// <param name="receiveObject"></param>
        private void GetRunExecutionDetailIdAndSendToFaultResult(RunExecutionDetailItem receiveObject)
        {
            _dataService.GetRunExecutionDetailSaved(receiveObject.TestCaseId, receiveObject.StepId, ReceivedRunExecFromRunExecPanel, AfterGetRunExecDetailId);
        }
  
        private void AfterGetRunExecDetailId(int tempId, Exception exception)
        {
            if (tempId != 0)
            {
                SetReceivedRunExecutionDetailIdProp(tempId);
                Messenger.Default.Send<int>(tempId, "SET_RUNEXECUTION_RUNEXECUTIONMAINVIEWMODEL");
            }
        }

        /// <summary>
        /// Set la varaible RunExecutionDetail.
        /// </summary>
        /// <param name="receiveItem"></param>
        private void SetReceivedRunExecutionDetailIdProp(int receiveItem)
        {
            FaultExecution.RunExecDetailId = receiveItem;
            ExistFaultExecution(receiveItem);//problema
        }

        /// <summary>
        /// Valida la variable para que pueda ser guardada.
        /// </summary>
        /// <param name="tempItem"></param>
        /// <returns></returns>
        private bool ValidateFaultExecutionItem(FaultExecutionItem tempItem)
        {
            int contador = 0;

            if (string.IsNullOrEmpty(tempItem.Comment) || string.IsNullOrWhiteSpace(tempItem.Comment))
            {
                TaskerHelper.SetStatusBarMessage("You must fill the Comment in order to continue.");
                contador++;
            }

            if (tempItem.Image.Length == 0)
            {
                TaskerHelper.SetStatusBarMessage("You must select an Image in order to continue.");
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
        /// Set las cambios en el viewModel
        /// </summary>
        /// <param name="optionTemp"></param>
        private void SetFaultExecutionPropertyFromContructor(PaqueteMSJ optionTemp)
        {
            switch (optionTemp.NombrePropiedad)
            {
                case "Comment":
                    FaultExecution.Comment = optionTemp.Informacion;
                    break;

                case "RunExecDetailId":
                    FaultExecution.RunExecDetailId = Convert.ToInt32(optionTemp.Informacion);
                    break;
            }
        }

        /// <summary>
        /// Restablece las variables a Vacio.
        /// </summary>
        /// <param name="doYouWantToClean"></param>
        private void Clean_Control(bool doYouWantToClean)
        {
            if (doYouWantToClean == true)
            {
                FaultExecution = new FaultExecutionItem();
                ImagePath = string.Empty;
                ImageSourceProp = null;
            }
        }

        /// <summary>
        /// Busca la imagen y la almacena en un arreglo de bits.
        /// </summary>
        private void SearchPicture()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = false;
            dlg.DefaultExt = ".JPG";
            dlg.Filter = "Archivo de imagen(*.PNG;*.JPG;*.BMP)|*.PNG;*.JPG;*.BMP";

            bool? retval = dlg.ShowDialog();

            if (retval != null && retval == true)
            {
                FileStream fs = new FileStream(dlg.FileName, FileMode.Open,
                                    FileAccess.Read);

                bytes = new byte[fs.Length];
                fs.Read(bytes, 0, System.Convert.ToInt32(fs.Length));


                fs.Close();

                //probar enviar esta variable al principal para guardar
                ImagePath = dlg.FileName.ToString();
                FaultExecution.Image = bytes;
                ShowPictureFromDB(bytes);
                //Messenger.Default.Send<byte[]>(bytes, "SET_BYTESPROPERTYASIMAGE_ADDSTEPSMAINVM");
            }
        }

        /// <summary>
        /// Lee la variable de tipo Arreglo de bits y la transforma a imagen para su representación.
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
        /// Almacena la imagen en un arreglo para su posterior manipulación.
        /// </summary>
        private void CopyImageFromClipboard()
        {
            ImageSourceProp = Clipboard.GetImage();
            BitmapSource bit = System.Windows.Clipboard.GetImage();
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();

            if (bit != null)
            {
                byte[] bit2 = new byte[0];
                using (MemoryStream stream = new MemoryStream())
                {
                    encoder.Frames.Add(BitmapFrame.Create(bit));
                    encoder.Save(stream);
                    bit2 = stream.ToArray();
                    stream.Close();
                    bytes = new byte[0];
                    bytes = bit2;
                    FaultExecution.Image = bytes;
                    //Messenger.Default.Send<byte[]>(bit2, "SET_BYTESPROPERTYASIMAGE_ADDSTEPSMAINVM");
                }
            }
        }

        #endregion
    }
}
