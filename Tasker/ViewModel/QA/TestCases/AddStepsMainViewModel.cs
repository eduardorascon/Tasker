using GalaSoft.MvvmLight;
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
	public class AddStepsMainViewModel : ViewModelBase
	{

		#region private Member
		private readonly IDataService _dataService;
		#endregion

		#region Constructor
		/// <summary>
		/// Constructor de la clase
		/// </summary>
		public AddStepsMainViewModel(IDataService dataService)
		{
			_dataService = dataService;
			InitializeCommands();
		}

		#endregion

		#region Private Methods

        /// <summary>
        /// Registra los mensajes e inicializa los controles.
        /// </summary>
		private void InitializeCommands()
		{
			ReceivedStepITem = new StepItem();
			SelectedTestCaseId = new int();
			SelectedTestCaseId = 0;

			//Register Messages
			Messenger.Default.Register<bool>(this, "CLEAN_RECEIVEDSTEPITEM_ADDSTEPSMAINVM", Clean_ReceivedStepITem);
			Messenger.Default.Register<string>(this, "SET_WHATDOYOUWHANTTODO_ADDSTEPSMAINVM", Set_WhatDoYouWantToDo);
			Messenger.Default.Register<PaqueteMSJ>(this, "SET_RECEIVEDSTEPITEM_IN_ADDSTEPSMAINVM_FROMCONSTRUCTOR", SetTestCasePropertyFromContructor);
			Messenger.Default.Register<bool>(this, "MAINBUTTONBAR_ADDSTEPSMAINVM", BarraPrincipalBotones);
			Messenger.Default.Register<StepItem>(this, "SET_RECEIVEDSTEPITEMFROMLIST_ADDSTEPSMAINVM", SetReceivedStepItemFromStepListViewModelVM);
			Messenger.Default.Register<int>(this, "SET_SELECTEDTESTCASEID_ADDSTEPSMAINVM", SetSelectedTestCaseId);
			Messenger.Default.Register<byte[]>(this, "SET_BYTESPROPERTYASIMAGE_ADDSTEPSMAINVM", SetDataPropImage);
            Messenger.Default.Register<bool>(this, "CLEAN_IMAGEPROP_ADDSTEPSMAINVM", CleanImageProp);
		}
		
        /// <summary>
        /// Set un objeto recibido de tipo arreglo de bit.
        /// </summary>
        /// <param name="imageTemp"></param>
		private void SetDataPropImage(byte[] imageTemp)
		{
			bytes = imageTemp;
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
        /// Método que inserta o actualiza según la operacion selecionada.
        /// </summary>
		private void SaveSteps()
		{
			if (SelectedTestCaseId == 0)
			{
				TaskerHelper.SetStatusBarMessage("You must Select a Test Plan in order to continue.");
			}
			else
			{
				switch (WhatDoYouWantToDo)
				{
					case "NEW":
						InsertStepInDB();
						break;

					case "EDIT":
						EditStepInDB();
						break;

                    case "DELETE":
                        DeleteStepInDB();
                        break;
				}
			}
		}

        /// <summary>
        /// Inserta un nuevo item en la base de datos.
        /// </summary>
		private void InsertStepInDB()
		{
			if (ValidateStepsItem(ReceivedStepITem) == true)
			{
				ReceivedStepITem.IsNew = true;
				ReceivedStepITem.TestCaseId = SelectedTestCaseId;
				ReceivedStepITem.Image = bytes;
				_dataService.SaveStep(ReceivedStepITem, (resulDTO, exception) =>
				{
					if (!resulDTO.HasError)
					{
						//Messenger.Default.Send("Collapse",
						//  "TESTCASE_EDIT_MODE");

						//Notificar que fue guardao exitosamente pendiente.
						Messenger.Default.Send<bool>(true, "CLEANCONTROL_ADDSTEPSVIEWMODEL");
						Messenger.Default.Send<bool>(true, "FILL_STEPITEMLIST_STEPLISTVIEWMODEL");
						//Messenger.Default.Send<string>("SAVE", "SET_ENABLEBUTTOMPROPERTY_TESTCASECONTROLBARVM");
						//Messenger.Default.Send<int>(0, "SET_SELECTEDTESTPLANID_TESTCASECONTROLBARVM");
                        Messenger.Default.Send<bool>(false, "SET_ENABLEDELETEBUTTONPROPERTY_ADDSTEPSCONTROLBARVIEWMODEL");
                        CleanImageProp(true);
						WhatDoYouWantToDo = string.Empty;
					}
					resulDTO.Message = "The New Item has been save.";
					TaskerHelper.SetStatusBarMessage(
						 resulDTO.Message);
				});
			}
		}

        /// <summary>
        /// Actualiza un item selecionado.
        /// </summary>
		private void EditStepInDB()
		{

			if (ValidateStepsItem(ReceivedStepITem) == true)
			{
				ReceivedStepITem.IsNew = false;
				if (bytes != null)
				{
					if (bytes.Length != 0)
						ReceivedStepITem.Image = bytes;
				}


				_dataService.SaveStep(ReceivedStepITem, (resulDTO, exception) =>
				{
					if (!resulDTO.HasError)
					{
						//Messenger.Default.Send("Collapse",
						//"TESTCASE_EDIT_MODE");

						//Notificar que fue guardao exitosamente pendiente.
						Messenger.Default.Send<bool>(true, "CLEANCONTROL_ADDSTEPSVIEWMODEL");
						Messenger.Default.Send<bool>(true, "FILL_STEPITEMLIST_STEPLISTVIEWMODEL");
						//Messenger.Default.Send<string>("SAVE", "SET_ENABLEBUTTOMPROPERTY_TESTCASECONTROLBARVM");
						//Messenger.Default.Send<int>(0, "SET_SELECTEDTESTPLANID_TESTCASECONTROLBARVM");
                        Messenger.Default.Send<bool>(false, "SET_ENABLEDELETEBUTTONPROPERTY_ADDSTEPSCONTROLBARVIEWMODEL");
                        CleanImageProp(true);
						WhatDoYouWantToDo = string.Empty;
					}
					resulDTO.Message = "The item has been successfully modified.";
					TaskerHelper.SetStatusBarMessage(
						 resulDTO.Message);
				});
			}
		}

        /// <summary>
        /// Elimina un setp en la base de datos.
        /// </summary>
        private void DeleteStepInDB()
        {
            _dataService.DeleteStep(ReceivedStepITem, (resulDTO, exception) =>
            {
                if (!resulDTO.HasError)
                {
                    //Messenger.Default.Send("Collapse",
                    //"TESTCASE_EDIT_MODE");

                    //Notificar que fue guardao exitosamente pendiente.
                    Messenger.Default.Send<bool>(true, "CLEANCONTROL_ADDSTEPSVIEWMODEL");
                    Messenger.Default.Send<bool>(true, "FILL_STEPITEMLIST_STEPLISTVIEWMODEL");
                    //Messenger.Default.Send<string>("SAVE", "SET_ENABLEBUTTOMPROPERTY_TESTCASECONTROLBARVM");
                    //Messenger.Default.Send<int>(0, "SET_SELECTEDTESTPLANID_TESTCASECONTROLBARVM");
                    Messenger.Default.Send<bool>(false, "SET_ENABLEDELETEBUTTONPROPERTY_ADDSTEPSCONTROLBARVIEWMODEL");
                    WhatDoYouWantToDo = string.Empty;
                }
                
                TaskerHelper.SetStatusBarMessage(
                        resulDTO.Message);
            });
            
        }

        /// <summary>
        /// Valida el objeto para que pueda guardarse sin mostrar error.
        /// </summary>
        /// <param name="tempStep"></param>
        /// <returns></returns>
		private bool ValidateStepsItem(StepItem tempStep)
		{
			int contador = 0;

			if (string.IsNullOrEmpty(tempStep.Description) || string.IsNullOrWhiteSpace(tempStep.Description))
			{
				TaskerHelper.SetStatusBarMessage("You must fill the description in order to continue.");
				contador++;
			}

			if (string.IsNullOrEmpty(tempStep.Input) || string.IsNullOrWhiteSpace(tempStep.Input))
			{
				TaskerHelper.SetStatusBarMessage("You must fill the input in order to continue.");
				contador++;
			}

			if (string.IsNullOrEmpty(tempStep.ExpectedResult) || string.IsNullOrWhiteSpace(tempStep.ExpectedResult))
			{
				TaskerHelper.SetStatusBarMessage("You must fill the result in order to continue.");
				contador++;
			}

            //if (string.IsNullOrEmpty(Convert.ToString(tempStep.NumberOfStep)) || string.IsNullOrWhiteSpace(Convert.ToString(tempStep.ExpectedResult)))
            //{
            //    TaskerHelper.SetStatusBarMessage("You must fill the result in order to continue.");
            //    contador++;
            //}

            //string tempatringNumber=Convert.ToString(tempStep.NumberOfStep);
            //int defaultNumber = 0;
            //bool canConvert = int.TryParse(tempatringNumber, out defaultNumber);
            //if (canConvert == true)
            //    Console.WriteLine("number1 now = {0}", number1);
            //else
            //    Console.WriteLine("numString is not a valid long");


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
        /// Set los cambios que se efectuan desde el view.
        /// </summary>
        /// <param name="optionTemp"></param>
		private void SetTestCasePropertyFromContructor(PaqueteMSJ optionTemp)
		{
			switch (optionTemp.NombrePropiedad)
			{
				case "Description":
					ReceivedStepITem.Description = optionTemp.Informacion;
					break;

				case "Input":
					ReceivedStepITem.Input = optionTemp.Informacion;
					break;

				case "ExpectedResult":
					ReceivedStepITem.ExpectedResult = optionTemp.Informacion;
					break;

                case "NumberOfStep":
                    ReceivedStepITem.NumberOfStep = Convert.ToInt32(optionTemp.Informacion);
                    break;

				case "Image":
					//ReceivedStepITem.Image = optionTemp.Informacion;
					//Pendiente trabajar const La imagen
					break;
			}
		}

        /// <summary>
        /// Limpia el Step recibido.
        /// </summary>
        /// <param name="doYouWantToClean"></param>
		private void Clean_ReceivedStepITem(bool doYouWantToClean)
		{
			if (doYouWantToClean == true)
			{
				ReceivedStepITem = new StepItem();
			}
		}

        /// <summary>
        /// Guarda en una variable de tipo string la accion que desea ejecutar el usuario.
        /// </summary>
        /// <param name="whatDoYouWantToDo"></param>
		private void Set_WhatDoYouWantToDo(string whatDoYouWantToDo)
		{
			WhatDoYouWantToDo = whatDoYouWantToDo;
		}

        /// <summary>
        /// Recibe el objeto selecionado en la lista de Step.
        /// </summary>
        /// <param name="tempReceivedStepItem"></param>
		private void SetReceivedStepItemFromStepListViewModelVM(StepItem tempReceivedStepItem)
		{
			ReceivedStepITem = tempReceivedStepItem;
		}

        /// <summary>
        /// Set el Id de TestCase.
        /// </summary>
        /// <param name="tempSelectedTestCaseId"></param>
		private void SetSelectedTestCaseId(int tempSelectedTestCaseId)
		{
			SelectedTestCaseId = tempSelectedTestCaseId;
		}

        private void CleanImageProp(bool doYouWantToClean)
        {
            if (doYouWantToClean == true)
            bytes = new byte[0];
        }

		#endregion

		#region Properties

		byte[] bytes = null;

		public string WhatDoYouWantToDo { get; set; }

		public int SelectedTestCaseId
		{
			get { return _selectedTestCaseId; }
			set
			{
				if (_selectedTestCaseId != value)
				{
					_selectedTestCaseId = value;
					RaisePropertyChanged("SelectedTestCaseId");
				}
			}
		}
		private int _selectedTestCaseId;
		

		/// <summary>
		/// The <see cref="ReceivedStepITem" /> property's name.
		/// </summary>
		public const string ReceivedStepITemPropertyName = "ReceivedStepITem";

		private StepItem _receivedStepItem = null;

		/// <summary>
		/// Sets and gets the ReceivedStepITem property.
		/// Changes to that property's value raise the PropertyChanged event. 
		/// </summary>
		public StepItem ReceivedStepITem
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

				RaisePropertyChanging(ReceivedStepITemPropertyName);
				_receivedStepItem = value;
				RaisePropertyChanged(ReceivedStepITemPropertyName);
			}
		}

		#endregion

	}
}
