using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Model;
using Tasker.Model.QA;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using Tasker.Helpers;

namespace Tasker.ViewModel.QA
{
	public class TestPlanMainViewModel : ViewModelBase
	{
		#region Constructor
			private readonly IDataService _dataService;
			public TestPlanMainViewModel(IDataService dataService)
		{
			_dataService = dataService;
            ReceivedTestPlan = new TestPlanItem();
            ShowNewTestPlanPanel = false;
            MostrarCanvas = Visibility.Collapsed;
            WhatDoYouWantToDo = string.Empty;

			//Register Messages
            Messenger.Default.Register<bool>(this, "MAINBUTTONBAR_TESTPLANMAINVM", BarraPrincipalBotones);
            Messenger.Default.Register<bool>(this, "MAINBUTTONBAR_NEWTESTPLANSHOW_TESTMAINVM", ShowHeaderNewTestPlan);
            Messenger.Default.Register<PaqueteMSJ>(this, "SET_TESTPLANPROPERTY_IN-MAINVMTESTPLAN_FROMCONSTRUCTOR", SetTestPlanPropertyFromContructor);
            Messenger.Default.Register<string>(this, "SET_WHATDOYOUWHANTTODO_INTESTPLANMAINVM", Set_WhatDoYouWantToDo);
            Messenger.Default.Register<bool>(this, "CLEAN_RECEIVEDTESTPLAN_TESTPLANMAINVM", Clean_ReceivedTestPlan);
            Messenger.Default.Register<TestPlanItem>(this, "SET_SELECTEDTESTPLAN_TESTPLANMAINVM", SetTestPlanPropertyFromTestPlanListBoxVM);

		}
		#endregion

		#region Propiedades

        public string WhatDoYouWantToDo { get; set; }
        
		public TestPlanItem ReceivedTestPlan { get; set; }

        /// <summary>
        /// Propiedad que almacena el estado del canvas que bloquea la edición.
        /// </summary>
        public Visibility MostrarCanvas
        {
            get { return _mostrarCanvas; }
            set
            {
                if (_mostrarCanvas != value)
                {
                    _mostrarCanvas = value;
                    RaisePropertyChanged("MostrarCanvas");
                }
            }
        }
        private Visibility _mostrarCanvas;

        /// <summary>
        /// propiedad que almacena el estado de mostrar del panel búsqueda.
        /// </summary>
        public bool ShowNewTestPlanPanel
        {
            get { return _showNewTestPlanPanel; }
            set
            {
                if (_showNewTestPlanPanel != value)
                {
                    _showNewTestPlanPanel = value;
                    RaisePropertyChanged("ShowNewTestPlanPanel");
                }
            }
        }
        private bool _showNewTestPlanPanel;



		#endregion
	   
		#region Métodos Privados

		/// <summary>
		/// Método que ejecuta los botones de la barra principal.
		/// </summary>
		/// <param name="?"></param>
        private void BarraPrincipalBotones(bool doYouWantToSave)
        {
            if (doYouWantToSave==true)
                SaveTestPlan();
        }

        /// <summary>
        /// Método que Ejecuta el insert o update dependiendo de la variable WhatDoYouWantToDo
        /// </summary>
        private void SaveTestPlan()
        {
            switch (WhatDoYouWantToDo)
            {
                case "NEW":
                    InsertTestPlanInDB();
                    break;

                case "EDIT":
                    EditTestPlanInDB();
                    break;

            }

        }

        /// <summary>
        /// Método encargado de insertar en item a la base de datos
        /// </summary>
        private void InsertTestPlanInDB()
        {
            if (ValidarTestPlan(ReceivedTestPlan) == true)
            {
                _dataService.SaveTestPlan(ReceivedTestPlan);
                //mensaje notificando que se agregó correctamente el testplan.
                Messenger.Default.Send<bool>(true, "CLEANCONTROLS_HEADERTESTPLANVM");
                Messenger.Default.Send<bool>(true, "FILL_TESTPLANITEMlIST_TESTPLANLISTBOXVM");
                WhatDoYouWantToDo = string.Empty;
                ShowHeaderNewTestPlan(false);
                Messenger.Default.Send<string>("SAVE", "SET_ENABLEBUTTOMPROPERTY_TESTPLANMAINCONTROLBARVM");
                TaskerHelper.SetStatusBarMessage("You have succesful record a new Test Plan.");
            }
        }

        /// <summary>
        /// Método encargado de actualizar el item selecionado.
        /// </summary>
        private void EditTestPlanInDB()
        {
            //edita en la base de datos el testplan seleccionado.

            if (ValidarTestPlan(ReceivedTestPlan) == true)
            {
                _dataService.EditTestPlan(ReceivedTestPlan);
                //mensaje notificando que se agregó correctamente el testplan.
                Messenger.Default.Send<bool>(true, "CLEANCONTROLS_HEADERTESTPLANVM");
                Messenger.Default.Send<bool>(true, "FILL_TESTPLANITEMlIST_TESTPLANLISTBOXVM");
                ShowHeaderNewTestPlan(false);
                WhatDoYouWantToDo = string.Empty;
                Messenger.Default.Send<string>("SAVE", "SET_ENABLEBUTTOMPROPERTY_TESTPLANMAINCONTROLBARVM");
                TaskerHelper.SetStatusBarMessage("You have succesful modify the selected item.");
            }


        }


        /// <summary>
        /// Método encargado de validar que los parametros sean correctos para poder almacenar el la DB
        /// </summary>
        /// <param name="tempTestPlan"></param>
        /// <returns></returns>
        private bool ValidarTestPlan(TestPlanItem tempTestPlan)
        {
            int contador = 0;

            if (string.IsNullOrEmpty(tempTestPlan.Description) || string.IsNullOrWhiteSpace(tempTestPlan.Description))
            {
                TaskerHelper.SetStatusBarMessage("You must fill description in order to continue.");
                contador++;
            }

            if (tempTestPlan.AplicationId == 9999999)
            {
                TaskerHelper.SetStatusBarMessage("You must Select an aplication in order to continue.");
                contador++;
            }

            if (string.IsNullOrEmpty(tempTestPlan.ObjectItem) || string.IsNullOrWhiteSpace(tempTestPlan.ObjectItem))
            {
                TaskerHelper.SetStatusBarMessage("You must fill object in order to continue.");
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
        /// Oculta o muestra el panel de testPlan
        /// </summary>
        /// <param name="opcionMostrar"></param>
        private void ShowHeaderNewTestPlan(bool opcionMostrar)
        {
            if (opcionMostrar == true)
            {
                ShowNewTestPlanPanel = true;
                MostrarCanvas = Visibility.Visible;
            }
            else
            {
                ShowNewTestPlanPanel = false;
                MostrarCanvas = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Set la variable ReceivedTestPlan
        /// </summary>
        /// <param name="testPlanRecibido"></param>
        private void LlenarAPartirTestPlanItem(TestPlanItem testPlanRecibido)
        {
            //enviar la infomración al VMEncabezado, y al VMDetalle
            ReceivedTestPlan = testPlanRecibido;
        }

        /// <summary>
        /// Set la variable en base a los datos modificados en el VIEW
        /// </summary>
        /// <param name="optionTemp"></param>
        private void SetTestPlanPropertyFromContructor(PaqueteMSJ optionTemp)
        {
            switch (optionTemp.NombrePropiedad)
            {
                case "Description":
                    ReceivedTestPlan.Description = optionTemp.Informacion;
                    break;

                case "Aplication":
                    ReceivedTestPlan.AplicationId =Convert.ToInt32(optionTemp.Informacion);
                    break;

                case "ObjectItem":
                    ReceivedTestPlan.ObjectItem = optionTemp.Informacion;
                    break;
            }
        }

        /// <summary>
        /// Set el objeto recibido a la variable ReceivedTestPlan
        /// </summary>
        /// <param name="newTestPlan"></param>
        private void SetTestPlanPropertyFromTestPlanListBoxVM(TestPlanItem newTestPlan)
        {
            if (newTestPlan != null)
                ReceivedTestPlan = newTestPlan;
        }

        /// <summary>
        /// La variable que maneja que accion se va a ejectuar
        /// </summary>
        /// <param name="TempWhatDoYouWantToDo"></param>
        private void Set_WhatDoYouWantToDo(string TempWhatDoYouWantToDo)
        {
            WhatDoYouWantToDo = TempWhatDoYouWantToDo;
        }

        /// <summary>
        /// Crea una nueva instancia de ReceivedTestPlan
        /// </summary>
        /// <param name="doYouWantToCleanTheProp"></param>
        private void Clean_ReceivedTestPlan(bool doYouWantToCleanTheProp)
        {
            if (doYouWantToCleanTheProp == true)
                ReceivedTestPlan = new TestPlanItem();
        }
            
		#endregion
	}
}
