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
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Tasker.Model;
using Tasker.Model.QA;

namespace Tasker.ViewModel.QA.TestCases
{
   public class AddStepsViewModel : ViewModelBase
	{

		#region private Member
		private readonly IDataService _dataService;
		#endregion

		#region Constructor

		/// <summary>
		/// Constructor de la clase
		/// </summary>
		public AddStepsViewModel(IDataService dataService)
		{
		_dataService = dataService;
        InitializeCommands();
		}

		#endregion

        #region Comandos

        public RelayCommand AttachImageCommand          { get; set; }
        public RelayCommand CopyFromClipBoardCommand    { get; set; }

        #endregion

        #region Private Methods
        
        /// <summary>
        /// Inicializa los controles y registra los mensajes.
        /// </summary>
        private void InitializeCommands()
		{
			//Inicializar Valores
			SelectedStepItem = new StepItem();
			ReceivedTestCaseId = new int();
			ReceivedTestCaseId = 0;
            

            //command
            AttachImageCommand = new RelayCommand(SearchPicture);
            CopyFromClipBoardCommand = new RelayCommand(CopyImageFromClipboard);

			//Register Message
			Messenger.Default.Register<bool>(this, "CLEANCONTROL_ADDSTEPSVIEWMODEL", CleanControl);
			Messenger.Default.Register<StepItem>(this, "SELECTEDTESTCASE_TESTCASESVM", SetPropertySelectedStepItem);
		}

        /// <summary>
        /// Restablece los controles a vacio.
        /// </summary>
        /// <param name="doYouWantToCleanControl"></param>
		private void CleanControl(bool doYouWantToCleanControl)
		{
			if (doYouWantToCleanControl == true)
			{
				SelectedStepItem = new StepItem();
                ImageSourceProp = null;
                ImagePath = string.Empty;
			}
		}

        /// <summary>
        /// Set un objeto recibido de tipo StepItem
        /// </summary>
        /// <param name="tempStepItem"></param>
		private void SetPropertySelectedStepItem(StepItem tempStepItem)
		{
			if (tempStepItem != null)
			{
				SelectedStepItem = tempStepItem;
                ShowPictureFromDB(SelectedStepItem.Image);
                Messenger.Default.Send<bool>(true, "SET_ENABLEDELETEBUTTONPROPERTY_ADDSTEPSCONTROLBARVIEWMODEL");
			}
            else
                Messenger.Default.Send<bool>(false, "SET_ENABLEDELETEBUTTONPROPERTY_ADDSTEPSCONTROLBARVIEWMODEL");
		}

        /// <summary>
        /// Set un objeto de tipo int TestCaseId
        /// </summary>
        /// <param name="tempTestCaseId"></param>
		private void SetPropertySelectedTestCaseId(int tempTestCaseId)
		{
			ReceivedTestCaseId = tempTestCaseId;
		}

        /// <summary>
        /// Busca la imagen y la guarda el resutado en una variable.
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
                ShowPictureFromDB(bytes);
                Messenger.Default.Send<byte[]>(bytes, "SET_BYTESPROPERTYASIMAGE_ADDSTEPSMAINVM");
            }
        }

        /// <summary>
        /// Alamcena la infomración del clipboard en una variable para su uso posterior.
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
                   Messenger.Default.Send<byte[]>(bit2, "SET_BYTESPROPERTYASIMAGE_ADDSTEPSMAINVM");
               }
           }
       }

        /// <summary>
        /// Convierte la imagen recibida en un tipo que pueda ser visulizado.
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

		#endregion

		#region Properties

        byte[] bytes = null;

		/// <summary>
		/// The <see cref="SelectedStepItem" /> property's name.
		/// </summary>
		public const string SelectedStepItemPropertyName = "SelectedStepItem";

		private StepItem _selectedStepItem = null;

		/// <summary>
		/// Sets and gets the SelectedStepItem property.
		/// Changes to that property's value raise the PropertyChanged event. 
		/// </summary>
		public StepItem SelectedStepItem
		{
			get
			{
				return _selectedStepItem;
			}

			set
			{
				if (_selectedStepItem == value)
				{
					return;
				}

				RaisePropertyChanging(SelectedStepItemPropertyName);
				_selectedStepItem = value;
				RaisePropertyChanged(SelectedStepItemPropertyName);
			}
		}

		/// <summary>
		/// The <see cref="ReceivedTestCaseId" /> property's name.
		/// </summary>
		public const string ReceivedTestCaseIdPropertyName = "ReceivedTestCaseId";

		private int _receivedTestCaseId = 0;

		/// <summary>
		/// Sets and gets the ReceivedTestCaseId property.
		/// Changes to that property's value raise the PropertyChanged event. 
		/// </summary>
		public int ReceivedTestCaseId
		{
			get
			{
				return _receivedTestCaseId;
			}

			set
			{
				if (_receivedTestCaseId == value)
				{
					return;
				}

				RaisePropertyChanging(ReceivedTestCaseIdPropertyName);
				_receivedTestCaseId = value;
				RaisePropertyChanged(ReceivedTestCaseIdPropertyName);
			}
		}


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


		#endregion
	}
}
