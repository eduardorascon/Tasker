using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Model;
using Tasker.Model.QA;

namespace Tasker.ViewModel.QA
{
	public class TestPlanEncabezadoViewModel : ViewModelBase
	{
			
		#region Miembros privados

		private readonly IDataService _dataService;
		private AplicationItem originalSelectedQA = new AplicationItem();
  
		#endregion

		#region Constructor

		public TestPlanEncabezadoViewModel(IDataService dataService)
		{
			_dataService = dataService;
			DelegarComandos();

			////Register Message
			Messenger.Default.Register<bool>(this, "CLEANCONTROLS_HEADERTESTPLANVM", CleanControls);
			//Messenger.Default.Register<TestPlanItem>(this, "TESTPLAN_RETURNEDOFSEARCH", LlenarAPartirTestPlanItem);
			Messenger.Default.Register<TestPlanItem>(this,"SELECTEDTESTPLAN_TESTPLANENCABEZADOVM", SetPropertySelectedTestPlanItem);
			Messenger.Default.Register<bool>(this, "CREATENEWTESTPLAN_TESTPLANENCABEZADOVM", NewPropertySelectedTestPlanItem);
            
		}

		private void InicializarComandos()
		{
            TestPlanItem = new TestPlanItem();
            SelectedAplication = new AplicationItem();
		}

		
		#endregion

		#region Comandos
		
		#endregion

		#region Metodos privados

		private void AfterGetQA(IList<AplicationItem> qa, Exception exception)
		{
			AplicationList = new List<AplicationItem>(qa);
		}

        /// <summary>
        /// Inicializa las variables y Registra los mensajes
        /// </summary>
		void DelegarComandos()
		 {
			 _dataService.GetAplicationQA(AfterGetQA);
		 }

        /// <summary>
        /// Inicializa las variables a vacio.
        /// </summary>
        /// <param name="borrar"></param>
		private void CleanControls(bool borrar)
		{
			if (borrar == true)
			{
				TestPlanItem = new TestPlanItem();
                SelectedAplication = null;
			}
		}

        /// <summary>
        /// Set TestPlanId por el objeto recibido
        /// </summary>
        /// <param name="tempTestPlanItem"></param>
		private void SetPropertySelectedTestPlanItem(TestPlanItem tempTestPlanItem)
		{
			if (tempTestPlanItem != null)
			{
				TestPlanItem = tempTestPlanItem;
                if (TestPlanItem.AplicationId != 9999999)
                {
                    if (AplicationList != null)
                    {
                        foreach (var item in AplicationList)
                        {
                            if (item.AplicationId == TestPlanItem.AplicationId)
                                SelectedAplication = item;
                        }
                    }
                }
			}
			else
			{
				//pendiente msj no se pudo mostra la información.
			}
			
		}

        /// <summary>
        /// Crea una nueva instancia de TestPlanId
        /// </summary>
        /// <param name="createNewTestPlanObject"></param>
		private void NewPropertySelectedTestPlanItem(bool createNewTestPlanObject)
		{
			if (createNewTestPlanObject==true)
			TestPlanItem = new TestPlanItem();
		}

		#endregion

		#region Propiedades

		/// <summary>
		/// Propiedad que almacena los campos del encabezado TestPlan.
		/// </summary>
		public TestPlanItem TestPlanItem
		{
			get 
			{
				return _testPlanItem; 
			}
			set
			{
				if (_testPlanItem != value)
				{
					_testPlanItem = value;
					RaisePropertyChanged("TestPlanItem");
				}
			}
		}
		private TestPlanItem _testPlanItem;

		/// <summary>
		/// Lista que contiene todo los item de aplicacion.
		/// </summary>
		 public List<AplicationItem> AplicationList
		 {
			 get { return _aplicationList; }
			 set
			 {
				 if (_aplicationList != value)
				 {
					 _aplicationList = value;
					 RaisePropertyChanged("AplicationList");
				 }
			 }
		 }
		 private List<AplicationItem> _aplicationList;

		/// <summary>
		/// Propiedad que almacena la información de el item seleccionado en el comboBox de aplicaciones.
		/// </summary>
		 public AplicationItem SelectedAplication
		 {
			 get 
			 { 
				 return _selectedAplication; 
			 }
			 set
			 {
				 if (_selectedAplication != value)
				 {
					 _selectedAplication = value;
					 RaisePropertyChanged("SelectedAplication");
				 }
				 if (SelectedAplication != null)
				 {
					 PaqueteMSJ paqueteTemp = new PaqueteMSJ();
					 paqueteTemp.Informacion = SelectedAplication.AplicationId.ToString();
                     paqueteTemp.NombrePropiedad = "Aplication";
					 Messenger.Default.Send<PaqueteMSJ>(paqueteTemp, "SET_TESTPLANPROPERTY_IN-MAINVMTESTPLAN_FROMCONSTRUCTOR");
				 }
			 }
		 }
		 private AplicationItem _selectedAplication;

		#endregion
	

	}
}
