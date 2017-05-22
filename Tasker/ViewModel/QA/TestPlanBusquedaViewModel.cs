using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Model;
using Tasker.Model.QA;

namespace Tasker.ViewModel.QA
{
    public class TestPlanBusquedaViewModel : ViewModelBase
    {

    //    #region Constructor

    //    public TestPlanBusquedaViewModel (IDataService dataService)
    //{
    //    _dataService = dataService;
    //    DelegarComandos();
    //    InicializarComandos();


    //    DateFrom = DateTime.Today;
    //    DateTo = DateTime.Today;
    //}

    //    #endregion

    //    #region Miembros Privados

    //    private readonly IDataService _dataService;
    //    //private TestCaseListItem originalSelectedQA = new TestCaseListItem();

       
    //    void DelegarComandos()
    //    {
    //       //Messenger.Default.Register<bool>(this, "FILL_TESTPLANLIST", LlenarListaTestPlan);
    //       // Messenger.Default.Register<bool>(this, "LIMPIAR_CONTROLES", LimpiarControles);
    //    }

    //    #endregion

    //    #region Propiedades

    //    #region Encabezado

    //    /// <summary>
    //    /// Propiedad que almacena el valor de el inicio de la fecha, como parámetro de búsqueda.
    //    /// </summary>
    //    public DateTime DateFrom
    //    {
    //        get { return _dateFrom; }
    //        set
    //        {
    //            if (_dateFrom != value)
    //            {
    //                _dateFrom = value;
    //                RaisePropertyChanged("DateFrom");
    //            }
    //        }
    //    }
    //    private DateTime _dateFrom;

    //    /// <summary>
    //    /// Propiedad que almacena el valor de el finál de la fecha, como parámetro de búsqueda.
    //    /// </summary>
    //    public DateTime DateTo
    //    {
    //        get { return _Dateto; }
    //        set
    //        {
    //            if (_Dateto != value)
    //            {
    //                _Dateto = value;
    //                RaisePropertyChanged("DateTo");
    //            }
    //        }
    //    }
    //    private DateTime _Dateto;

    //    /// <summary>
    //    /// Propiedad que almacena el valor de busqueda del nombre.
    //    /// </summary>
    //    public string Descripcion
    //    {
    //        get { return _descripcion; }
    //        set
    //        {
    //            if (_descripcion != value)
    //            {
    //                _descripcion = value;
    //                RaisePropertyChanged("Descripcion");
    //            }
    //        }
    //    }
    //    private string _descripcion;
        
    //    #endregion

    //    #region Detalle

    //    /// <summary>
    //    /// Almacena una lista de los TestPlan
    //    /// </summary>
    //    public List<TestPlanItem> TestPlanList
    //    {
    //        get { return _testPlanList; }
    //        set
    //        {
    //            if (_testPlanList != value)
    //            {
    //                _testPlanList = value;
    //                RaisePropertyChanged("TestPlanList");
    //            }
    //        }
    //    }
    //    private List<TestPlanItem> _testPlanList;

    //    /// <summary>
    //    /// Alamcena la información de item seleccionado en la lista.
    //    /// </summary>
    //    public TestPlanItem SelectedTestPlan
    //    {
    //        get { return _selectedTestPlan; }
    //        set
    //        {
    //            if (_selectedTestPlan != value)
    //            {
    //                _selectedTestPlan = value;
    //                RaisePropertyChanged("SelectedTestPlan");
    //            }
    //        }
    //    }
    //    private TestPlanItem _selectedTestPlan;

    //    #endregion


    //    #endregion

    //    #region Métodos Privados

    //    private void AfterGetTestPlanList(IList<TestPlanItem> testPlanListTemp, Exception exception)
    //    {
    //        TestPlanList = new List<TestPlanItem>(testPlanListTemp);
    //    }

    //    private void LlenarListaTestPlan(bool llenar)
    //    {
    //        if (llenar == true)
    //            _dataService.GetTestPlanList(DateFrom, DateTo, Descripcion, AfterGetTestPlanList);
    //    }

    //    /// <summary>
    //    /// Inicializa los comandos
    //    /// </summary>
    //    private void InicializarComandos()
    //    {
    //        CancelCommand = new RelayCommand(Cancelar);
    //        ConfirmCommand = new RelayCommand(Confirmar);
    //        SearchCommand = new RelayCommand(Search);
    //    }

    //    private void Confirmar()
    //    {
    //        //pendiente validar que lececcionado no sea nulo, mostrar mensaje si es nulo.

    //        if (SelectedTestPlan != null)
    //        {
    //            Messenger.Default.Send<TestPlanItem>(SelectedTestPlan, "TESTPLAN_RETURNEDOFSEARCH");
    //            Messenger.Default.Send<bool>(false, "MAINBUTTONBAR_SHOWSEARCHPANEL");
    //        }
    //        else
    //        {
    //            //notificar que no ha seleccionado ningun testplan.
    //        }

    //    }

    //    private void Cancelar()
    //    {
    //        Messenger.Default.Send<bool>(false, "MAINBUTTONBAR_SHOWSEARCHPANEL");
    //    }

    //    private void Search()
    //    {
    //        _dataService.GetTestPlanList(DateFrom, DateTo, Descripcion, AfterGetTestPlanList);
    //    }


    //    #endregion

    //    #region Comandos

    //    // Puedes usar snippet: comandovm

    //    public RelayCommand CancelCommand { get; set; }
    //    public RelayCommand ConfirmCommand { get; set; }
    //    public RelayCommand SearchCommand { get; set; }


        //#endregion
    }
}
