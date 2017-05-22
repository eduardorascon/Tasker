using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tasker.Classes;
using Tasker.Helpers;
using Tasker.Model;
using Tasker.Model.QA;

namespace Tasker.ViewModel.QA.TestCases
{
	public class TestCaseMainViewModel : ViewModelBase
	{
		
		#region Miembros privados

		private readonly IDataService _dataService;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor de la clase
		/// </summary>
		public TestCaseMainViewModel(IDataService dataService)
		{
			_dataService = dataService;
			InicializarComandos();
		}

		#endregion

		#region Comandos
		// Puedes usar snippet: comandovm
		#endregion

		#region Propiedades

		 public TestCaseItem ReceivedTestCase { get; set; }

		 public int SelectedTestPlanId { get; set; }

		 public string WhatDoYouWantToDo { get; set; }


		 public Visibility VisibilityPropStepList
		 {
			 get { return _visibilityPropStepList; }
			 set
			 {
				 if (_visibilityPropStepList != value)
				 {
					 _visibilityPropStepList = value;
					 RaisePropertyChanged("VisibilityPropStepList");
				 }
			 }
		 }
		 private Visibility _visibilityPropStepList;


		 public Visibility ShowListTestCase
		 {
			 get { return _showListTestCase; }
			 set
			 {
				 if (_showListTestCase != value)
				 {
					 _showListTestCase = value;
					 RaisePropertyChanged("ShowListTestCase");
				 }
			 }
		 }
		 private Visibility _showListTestCase;
		 

		 public Visibility ShowStepList
		 {
			 get { return _showStepList; }
			 set
			 {
				 if (_showStepList != value)
				 {
					 _showStepList = value;
					 RaisePropertyChanged("ShowStepList");
				 }
			 }
		 }
		 private Visibility _showStepList;


		 public Visibility ShowAddNewStep
		 {
			 get { return _showAddNewStep; }
			 set
			 {
				 if (_showAddNewStep != value)
				 {
					 _showAddNewStep = value;
					 RaisePropertyChanged("ShowAddNewStep");
				 }
			 }
		 }
		 private Visibility _showAddNewStep;


		 public bool ShowNewTestCaseHeader
		 {
			 get { return _showNewTestCaseHeader; }
			 set
			 {
				 if (_showNewTestCaseHeader != value)
				 {
					 _showNewTestCaseHeader = value;
					 RaisePropertyChanged("ShowNewTestCaseHeader");
				 }
			 }
		 }
		 private bool _showNewTestCaseHeader;


		 public bool ShowAddNewStepHeader
		 {
			 get { return _showAddNewStepHeader; }
			 set
			 {
				 if (_showAddNewStepHeader != value)
				 {
					 _showAddNewStepHeader = value;
					 RaisePropertyChanged("ShowAddNewStepHeader");
				 }
			 }
		 }
		 private bool _showAddNewStepHeader;
		 

		 public Visibility ShowHeaderTestCase
		 {
			 get { return _showHeaderTestCase; }
			 set
			 {
				 if (_showHeaderTestCase != value)
				 {
					 _showHeaderTestCase = value;
					 RaisePropertyChanged("ShowHeaderTestCase");
				 }
			 }
		 }
		 private Visibility _showHeaderTestCase;


		 public Visibility ShowListTestCaseDetail
		 {
			 get { return _showListTestCaseDetail; }
			 set
			 {
				 if (_showListTestCaseDetail != value)
				 {
					 _showListTestCaseDetail = value;
					 RaisePropertyChanged("ShowListTestCaseDetail");
				 }
			 }
		 }
		 private Visibility _showListTestCaseDetail;


		 public Visibility ShowBlackBlockScreenControlBarStepList
		 {
			 get { return _showBlackBlockScreenControlBarStepList; }
			 set
			 {
				 if (_showBlackBlockScreenControlBarStepList != value)
				 {
					 _showBlackBlockScreenControlBarStepList = value;
					 RaisePropertyChanged("ShowBlackBlockScreenControlBarStepList");
				 }
			 }
		 }
		 private Visibility _showBlackBlockScreenControlBarStepList;


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
		 private Visibility _showBlackBlockScreenControlBar;
		 

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
		 private Visibility _ShowBlackBlockScreen;

		 /// <summary>
		 /// propiedad que almacena el estado de mostrar del header del testcasemainVM.
		 /// </summary>
		 public bool ShowNewTestCasePanel
		 {
			 get { return _showNewTestCasePanel; }
			 set
			 {
				 if (_showNewTestCasePanel != value)
				 {
					 _showNewTestCasePanel = value;
					 RaisePropertyChanged("ShowNewTestCasePanel");
				 }
			 }
		 }
		 private bool _showNewTestCasePanel;


		#endregion

		#region Métodos Privados

		/// <summary>
		 /// Inicializa los comandos
		 /// </summary>
		private void InicializarComandos()
		 {
			 ReceivedTestCase = new TestCaseItem();
			 SetVisibilityPropForMainVM("Default");
			 VisibilityPropStepList = Visibility.Collapsed;//pendiente

			 //Inicializar Valores
			 SelectedTestPlanId = new int();
			 SelectedTestPlanId = 0;

			 //Register Messages
			 Messenger.Default.Register<bool>(this, "CLEAN_RECEIVEDTESTCASE_TESTCASEMAINVM", Clean_ReceivedTestCase);
			 Messenger.Default.Register<bool>(this, "MAINBUTTONBAR_NEWTESTCASESHOW_TESTCASEMAINVM", ShowHeaderNewTestCase);
			 Messenger.Default.Register<string>(this, "SET_WHATDOYOUWHANTTODO_TESTCASEMAINVM", Set_WhatDoYouWantToDo);
			 Messenger.Default.Register<PaqueteMSJ>(this, "SET_TESTCASEPROPERTY_IN-MAINVMTESTCASE_FROMCONSTRUCTOR", SetTestCasePropertyFromContructor);
			 Messenger.Default.Register<bool>(this, "MAINBUTTONBAR_TESTCASEMAINVM", BarraPrincipalBotones);
			 Messenger.Default.Register<TestCaseItem>(this, "SET_SELECTEDTESTCASE_TESTCASEMAINVM", SetTestCasePropertyFromTestCasesVM);
			 Messenger.Default.Register<int>(this, "SET_SELECTEDTESTPLANID_TESTCASEMAINVM", SetSelectedTestPlanIdProperty);
			 Messenger.Default.Register<bool>(this, "SET_VISIBILITYPROPSTEPLIST_TESTCASEMAINVM", SetVisibilityPropForShowStepList);
			 Messenger.Default.Register<bool>(this, "SET_VISIBILITYPROPADDSTEPLIST_TESTCASEMAINVM", SetVisibilityPropForAddStepList);
			 Messenger.Default.Register<bool>(this, "SET_VISIBILITYPROPADDNEWTESTCASEHEADER_TESTCASEMAINVM", SetVisibilityPropForAddNewTestCaseHeader);
			 Messenger.Default.Register<string>(this, "SET_VISIBILITYPROPFORSETDEAFULTORINADDMODE_TESTCASEMAINVM", SetVisibilityPropForMainVM);
             Messenger.Default.Register<Visibility>(this,"SET_VISIBILITYPROPSTEPLIST_TESTCASEMAINVM", SetVisibilityPropShowStepList);
             Messenger.Default.Register<int>(this, "SET_SELECTEDTESTPLANIDFROMTESTPLANMV_TESTCASEMAINVM", GetSelectedTestPlanIdFromTestPlanVM);
             Messenger.Default.Send<bool>(true, "GET_SELECTEDTESTPLANIDFROMTESTPLANMV_TESTCASEMAINVM");
		 }

        /// <summary>
        /// Recibe de TestPlanVM el Id de TestPlan
        /// </summary>
        /// <param name="receivedTestPlanId"></param>
        private void GetSelectedTestPlanIdFromTestPlanVM(int receivedTestPlanId)
         {
            if(receivedTestPlanId!=0)
             SelectedTestPlanId = receivedTestPlanId;
         }

        /// <summary>
        /// oculta la lista de step
        /// </summary>
        /// <param name="setProp"></param>
        private void SetVisibilityPropShowStepList(Visibility setProp)
         {
             ShowStepList = setProp;
         }

        /// <summary>
        /// Oculta o muestra el panel para agrear un nuevo TestCase
        /// </summary>
        /// <param name="showAddHeaderTestCase"></param>
		private void SetVisibilityPropForAddNewTestCaseHeader(bool showAddHeaderTestCase)
		 {
			 if (showAddHeaderTestCase == true)
			 {
				 SetVisibilityPropForMainVM("Default");
				 ShowHeaderTestCase = Visibility.Visible;
				 ShowBlackBlockScreen = Visibility.Visible;
				 ShowBlackBlockScreenControlBarStepList = Visibility.Visible;
				 ShowNewTestCaseHeader = true;
			 }
			 else
			 {
				 SetVisibilityPropForMainVM("Default");
				 ShowBlackBlockScreenControlBarStepList = Visibility.Collapsed;
			 }
				 
		 }

        /// <summary>
        /// Muestra u oculta StepList
        /// </summary>
        /// <param name="showAddStep"></param>
		private void SetVisibilityPropForAddStepList(bool showAddStep)
		 {
			 if (showAddStep == true)
			 {
				 SetVisibilityPropForMainVM("AddNewStep");
				 ShowAddNewStep = Visibility.Visible;
			 }
			 else
			 {
				 SetVisibilityPropForMainVM("Default");
				 ShowStepList = Visibility.Visible;
			 }
				 
		 }

        /// <summary>
        /// Oculta o muestra la lista de Steps
        /// </summary>
        /// <param name="showStepList"></param>
		private void SetVisibilityPropForShowStepList(bool showStepList)
		 {
			 if (showStepList == true)
			 {
				 SetVisibilityPropForMainVM("Default");
				 ShowStepList = Visibility.Visible;
			 }
		 }

        /// <summary>
        /// Maneja los estados para mostrar u ocultar el panel de New Step
        /// </summary>
        /// <param name="opcion"></param>
		private void SetVisibilityPropForMainVM(string opcion)
		 {
			switch (opcion)
				{
				case "Default":
								 ShowBlackBlockScreen = Visibility.Collapsed;
								 ShowBlackBlockScreenControlBar = Visibility.Collapsed;
								 ShowHeaderTestCase = Visibility.Collapsed;
								 ShowStepList = Visibility.Collapsed;
								 ShowListTestCase = Visibility.Visible;
								 ShowAddNewStep = Visibility.Collapsed;
								 ShowListTestCaseDetail = Visibility.Visible;
								 ShowBlackBlockScreenControlBarStepList = Visibility.Collapsed;
								 ShowNewTestCaseHeader = false;
								 ShowAddNewStepHeader = false;
					break;
				case "AddNewStep":
								 ShowBlackBlockScreen = Visibility.Collapsed;
								 ShowBlackBlockScreenControlBar = Visibility.Visible;
								 ShowHeaderTestCase = Visibility.Collapsed;
								 ShowStepList = Visibility.Collapsed;
								 ShowListTestCase = Visibility.Collapsed;
								 ShowAddNewStep = Visibility.Visible;
								 ShowListTestCaseDetail = Visibility.Collapsed;
								 ShowNewTestCaseHeader = false;
								 ShowAddNewStepHeader = true;
					break;
					
				}

		 }

        /// <summary>
        /// Set la variable TestPlanId
        /// </summary>
        /// <param name="selectedtTestPlanIdTemp"></param>
		private void SetSelectedTestPlanIdProperty(int selectedtTestPlanIdTemp)
		 {
				 SelectedTestPlanId = selectedtTestPlanIdTemp;
				 ReceivedTestCase.TestPlanId = SelectedTestPlanId;             
		 }

        /// <summary>
        /// Set la variable ReceivedTestCase del VM TestCase
        /// </summary>
        /// <param name="newTestCase"></param>
		private void SetTestCasePropertyFromTestCasesVM(TestCaseItem newTestCase)
		 {
			 if (newTestCase != null)
				 ReceivedTestCase = newTestCase;
		 }

        /// <summary>
        /// Establece en una variable de tipo string la accion a ejecutar
        /// </summary>
        /// <param name="TempWhatDoYouWantToDo"></param>
		private void Set_WhatDoYouWantToDo(string TempWhatDoYouWantToDo)
		 {
			 WhatDoYouWantToDo = TempWhatDoYouWantToDo;
		 }

        /// <summary>
        /// restablece la variable ReceivedTestCase a nueva instancia
        /// </summary>
        /// <param name="doYouWantToCleanTheProp"></param>
		private void Clean_ReceivedTestCase(bool doYouWantToCleanTheProp)
		 {
			 if (doYouWantToCleanTheProp == true && SelectedTestPlanId!=0)
			 {
				 ReceivedTestCase = new TestCaseItem();
				 ReceivedTestCase.TestPlanId = SelectedTestPlanId;
				 SetVisibilityPropForMainVM("Default");
			 }  
		 }

        /// <summary>
        /// Muestra el Header de TestCase
        /// </summary>
        /// <param name="opcionMostrar"></param>
		private void ShowHeaderNewTestCase(bool opcionMostrar)
		 {
			 if (opcionMostrar == true)
			 {
				 ShowNewTestCasePanel = true;
				 ShowBlackBlockScreen = Visibility.Visible;
			 }
			 else
			 {
				 ShowNewTestCasePanel = false;
				 ShowBlackBlockScreen = Visibility.Collapsed;
			 }
		 }

        /// <summary>
        /// Set la variable desde las modificaciones de VM
        /// </summary>
        /// <param name="optionTemp"></param>
		private void SetTestCasePropertyFromContructor(PaqueteMSJ optionTemp)
		 {
			 switch (optionTemp.NombrePropiedad)
			 {
				 case "Description":
					 ReceivedTestCase.Description = optionTemp.Informacion;
					 break;

				 case "Duration":
					 ReceivedTestCase.Duration = Convert.ToInt32(optionTemp.Informacion);
					 break;

				 case "Objetive":
					 ReceivedTestCase.Objetive = optionTemp.Informacion;
					 break;

				 case "TestData":
					 ReceivedTestCase.TestData = optionTemp.Informacion;
					 break;

				 case "PreCondition":
					 ReceivedTestCase.PreCondition = optionTemp.Informacion;
					 break;

				 case "TestPlanId":
					 ReceivedTestCase.TestPlanId = Convert.ToInt32(optionTemp.Informacion);
					 break;
			 }
		 }

		/// <summary>
		 /// Método que ejecuta los botones de la barra principal.
		 /// </summary>
		 /// <param name="?"></param>
		private void BarraPrincipalBotones(bool doYouWantToSave)
		 {
			 if (doYouWantToSave == true)
				 SaveTestCase();
		 }

        /// <summary>
        /// Método que actualiza o inserta según el contenido de la variable WhatDoYouWantToDo
        /// </summary>
		private void SaveTestCase()
		 {
			 if (SelectedTestPlanId == 0)
			 {
				 TaskerHelper.SetStatusBarMessage("You must Select a Test Plan in order to continue.");
			 }
			 else
			 {
				 switch (WhatDoYouWantToDo)
				 {
					 case "NEW":
						 InsertTestCaseInDB();
						 break;

					 case "EDIT":
						 EditTestCaseInDB();
						 break;
				 }
			 }
		 }

        /// <summary>
        /// Método encargado de Insertar el TestCase
        /// </summary>
		private void InsertTestCaseInDB()
		 {
			 if (ValidarTestCase(ReceivedTestCase) == true)
			 {
				 ReceivedTestCase.IsNew = true;
				 ReceivedTestCase.TestPlanId = SelectedTestPlanId;
				 _dataService.SaveTestCase(ReceivedTestCase, (resulDTO, exception) =>
				 {
					 if (!resulDTO.HasError)
					 {
						 //Messenger.Default.Send("Collapse",
						 //  "TESTCASE_EDIT_MODE");

						 //Notificar que fue guardao exitosamente pendiente.
						Messenger.Default.Send<bool>(true, "CLEANCONTROLS_HEADERTESTCASEVM");
						Messenger.Default.Send<bool>(true, "FILL_TESTCASEITEMLIST_TESTCASESVM");
						WhatDoYouWantToDo = string.Empty;
						ShowHeaderNewTestCase(false);
						Messenger.Default.Send<string>("SAVE", "SET_ENABLEBUTTOMPROPERTY_TESTCASECONTROLBARVM");
                        Messenger.Default.Send<bool>(false, "SET_VISIBILITYPROPADDNEWTESTCASEHEADER_TESTCASEMAINVM");
						//Messenger.Default.Send<int>(0, "SET_SELECTEDTESTPLANID_TESTCASECONTROLBARVM");
					 }
					 resulDTO.Message = "The New Item has been save.";
					 TaskerHelper.SetStatusBarMessage(
						  resulDTO.Message);
				 });
			 }
		 }

        /// <summary>
        /// Modifica el TestCase
        /// </summary>
		private void EditTestCaseInDB()
		 {

			 if (ValidarTestCase(ReceivedTestCase) == true)
			 {
				 ReceivedTestCase.IsNew = false;
				 _dataService.SaveTestCase(ReceivedTestCase, (resulDTO, exception) =>
				 {
					 if (!resulDTO.HasError)
					 {
						 //Messenger.Default.Send("Collapse",
						 //  "TESTCASE_EDIT_MODE");

						 //Notificar que fue guardao exitosamente pendiente.
						 Messenger.Default.Send<bool>(true, "CLEANCONTROLS_HEADERTESTCASEVM");
						 Messenger.Default.Send<bool>(true, "FILL_TESTCASEITEMLIST_TESTCASESVM");
						 WhatDoYouWantToDo = string.Empty;
						 ShowHeaderNewTestCase(false);
						 Messenger.Default.Send<string>("SAVE", "SET_ENABLEBUTTOMPROPERTY_TESTCASECONTROLBARVM");
                         Messenger.Default.Send<bool>(false, "SET_VISIBILITYPROPADDNEWTESTCASEHEADER_TESTCASEMAINVM");
					 }
					 resulDTO.Message = "The item has been successfully modified.";
					 TaskerHelper.SetStatusBarMessage(
						  resulDTO.Message);
				 });
			 }
		 }

        /// <summary>
        /// Método que valida el objeto para que pueda ser guardado correctamente
        /// </summary>
        /// <param name="tempTestCase"></param>
        /// <returns></returns>
		private bool ValidarTestCase(TestCaseItem tempTestCase)
		 {
			 int contador = 0;

			 if (string.IsNullOrEmpty(tempTestCase.Description) || string.IsNullOrWhiteSpace(tempTestCase.Description))
			 {
				 TaskerHelper.SetStatusBarMessage("You must fill the description in order to continue.");
				 contador++;
			 }

			 if (tempTestCase.Duration == 0)
			 {
				 TaskerHelper.SetStatusBarMessage("You must place a numeric data in duration textbox order to continue.");
				 contador++;
			 }

			 if (string.IsNullOrEmpty(tempTestCase.Objetive) || string.IsNullOrWhiteSpace(tempTestCase.Objetive))
			 {
				 TaskerHelper.SetStatusBarMessage("You must fill the objective in order to continue.");
				 contador++;
			 }

			 if (tempTestCase.TestPlanId == 0)
			 {
				 TaskerHelper.SetStatusBarMessage("You must Select a Test Plan in order to continue.");
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

		#endregion

	}
}
