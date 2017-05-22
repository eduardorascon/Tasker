using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
using Tasker.Model;
using Tasker.Model.QA;

namespace Tasker.ViewModel.QA.TestCases
{
   public class RunExecutionStepListViewModel:ViewModelBase
    {

       #region Private Member

       private readonly IDataService _dataService;

       #endregion

       #region Constructor

       public RunExecutionStepListViewModel(IDataService dataService)
       {
           _dataService = dataService;
           InitializeComponent();
           History1Image = new Path();
           History1Image.Data = Geometry.Parse("M-150.204,626.126C-152.317,626.126 -154.429,626.126 -156.541,626.126 -167.642,633.42 -180.629,646.047 -189.668,657.238 -190.916,658.782 -192.945,662.362 -193.701,662.422 -194.041,662.448 -198.024,659.719 -198.614,659.297 -202.818,656.279 -205.779,653.709 -209.257,650.899 -211.248,652.172 -212.879,653.805 -214.153,655.797 -206.627,665.074 -200.283,675.534 -193.124,685.18 -181.491,665.11 -168.473,644.683 -152.796,629.006 -151.735,627.946 -149.817,626.933 -150.204,626.126z");
           
       }

       #endregion
       
       #region Properties

       public StepItem LastValidStepItem { get; set; }
       
       /// <summary>
       /// The <see cref="ReceivedExecId" /> property's name.
       /// </summary>
       public const string ReceivedExecIdPropertyName = "ReceivedExecId";

       private int _receivedExecId = 0;

       /// <summary>
       /// Sets and gets the ReceivedExecId property.
       /// Changes to that property's value raise the PropertyChanged event. 
       /// </summary>
       public int ReceivedExecId
       {
           get
           {
               return _receivedExecId;
           }

           set
           {
               if (_receivedExecId == value)
               {
                   return;
               }

               RaisePropertyChanging(ReceivedExecIdPropertyName);
               _receivedExecId = value;
               RaisePropertyChanged(ReceivedExecIdPropertyName);
           }
       }

       /// <summary>
       /// Almacena el TestCase del Item Seleccionado en la lista de TestCase
       /// </summary>
       public int ReceivedTestCaseId
       {
           get { return _receivedTestCaseId; }
           set
           {
               if (_receivedTestCaseId != value)
               {
                   _receivedTestCaseId = value;
                   RaisePropertyChanged("ReceivedTestCaseId");
               }
           }
       }
       private int _receivedTestCaseId;
       
       /// <summary>
       /// The <see cref="StepList" /> property's name.
       /// </summary>
       public const string StepListPropertyName = "StepList";

       private ObservableCollection<StepItem> _stepList = new ObservableCollection<StepItem>();

       /// <summary>
       /// Sets and gets the StepList property.
       /// Changes to that property's value raise the PropertyChanged event. 
       /// </summary>
       public ObservableCollection<StepItem> StepList
       {
           get
           {
               return _stepList;
           }

           set
           {
               if (_stepList == value)
               {
                   return;
               }

               RaisePropertyChanging(StepListPropertyName);
               _stepList = value;
               RaisePropertyChanged(StepListPropertyName);
               //pendiente borrar solo para probar el tamaño
           }
       }


       /// <summary>
       /// The <see cref="SelectedStepItem" /> property's name.
       /// </summary>
       public const string SelectedStepItemPropertyName = "SelectedStepItem";

       private StepItem _selectedStepItem = new StepItem();

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
               if (SelectedStepItem != null)
               {
                   //Send To: RUNEXECUTIONMAINVIEWMODEL
                   Messenger.Default.Send<StepItem>(SelectedStepItem, "SET_RECEIVEDTESTPLANIDPROPERTY_RUNEXECUTIONMAINVIEWMODEL");
                   //Send To: RUNEXECUTIONCONTROLBARVIEWMODEL
                   Messenger.Default.Send<bool>(true, "ACTIVE_NAVIGATIONCONTROL_RUNEXECUTIONCONTROLBARVIEWMODEL");
                   LastValidStepItem = new StepItem();
                   LastValidStepItem = SelectedStepItem;
               }
               else
               {
                   //Send To: RUNEXECUTIONMAINVIEWMODEL
                   Messenger.Default.Send<bool>(true, "SET_RECEIVEDTESTPLANIDPROPERTY_RUNEXECUTIONMAINVIEWMODEL");
                   Messenger.Default.Send<bool>(true, "CLEANALL_PROPERTY_RUNEXECUTIONMAINVIEWMODEL");
                   //Send To: RUNEXECUTIONCONTROLBARVIEWMODEL
                   Messenger.Default.Send<bool>(false, "ACTIVE_NAVIGATIONCONTROL_RUNEXECUTIONCONTROLBARVIEWMODEL");
               }
           }
       }


       public Path History1Image
       {
           get { return _history1Image; }
           set
           {
               if (_history1Image != value)
               {
                   _history1Image = value;
                   RaisePropertyChanged("History1Image");
               }
           }
       }
       private Path _history1Image;
       
        
        #endregion

       #region Methods

        /// <summary>
        /// Inicializar controles y Registrar mensajes.
        /// </summary>
        void InitializeComponent()
        {
            
            // Listen the Messenger
            Messenger.Default.Register<bool>(this, "CLEAN_SELECTEDTESTCASEPROPERTY_RUNEXECUTIONSTEPLISTVIEWMODEL", CleanControls);
            Messenger.Default.Register<int>(this, "SET_RECEIVEDTESTPLANIDPROPERTY_RUNEXECUTIONSTEPLISTVIEWMODEL", SetReceivedTestCaseIdProperty);
            Messenger.Default.Register<int>(this, "SET_RECEIVEDEXECIDPROPERTY_RUNEXECUTIONSTEPLISTVIEWMODEL", SetReceivedExecIdProperty);
            Messenger.Default.Register<string>(this, "NAVI_NAVIGATEINSTEPLIST_RUNEXECUTIONSTEPLISTVIEWMODEL", NavigateInStepList);
            Messenger.Default.Register<bool>(this, "RELOAD_STEPLIST_RUNEXECUTIONSTEPLISTVIEWMODEL", ReloadStepList);

        }

        /// <summary>
        /// Recargar La lista cuando ocurren cambios.
        /// </summary>
        /// <param name="option"></param>
        private void ReloadStepList(bool option)
        {
            if (option == true)
            {
               FillStepList(ReceivedTestCaseId);
            }
        }

        /// <summary>
        /// Set la variable ExecId.
        /// </summary>
        /// <param name="receiveItem"></param>
        private void SetReceivedExecIdProperty(int receiveItem)
        {
            ReceivedExecId = receiveItem;
        }

        /// <summary>
        /// Llena la lista de step.
        /// </summary>
        /// <param name="doYouWantToFill"></param>
        private void FillStepList(bool doYouWantToFill)
        {
            if (doYouWantToFill == true)
                if (ReceivedTestCaseId != 0)
                    _dataService.GetStepList(ReceivedTestCaseId, ReceivedExecId, AfterGetTestCases);
        }

        /// <summary>
        /// Llena la lista en base a los parámetros recibidos.
        /// </summary>
        /// <param name="tempId"></param>
        private void FillStepList(int tempId)
        {
            if (tempId != 0)
                _dataService.GetStepList(ReceivedTestCaseId, ReceivedExecId, AfterGetTestCases);
        }


        private void AfterGetTestCases(IList<StepItem> StepListTemp, Exception exception)
        {
            StepList = new ObservableCollection<StepItem>(StepListTemp);
            foreach (var item in StepList)
            {
                if (LastValidStepItem!=null)
                if (item.StepId == LastValidStepItem.StepId)
                {
                    SelectedStepItem = item;
                }
            }
        }

        /// <summary>
        /// Restable las varaibles a vacio.
        /// </summary>
        /// <param name="restart"></param>
        private void CleanControls(bool restart)
        {
            if (restart == true)
            {
                SelectedStepItem = null;
                ReceivedTestCaseId = 0;
                StepList = new ObservableCollection<StepItem>();
            }
        }

        /// <summary>
        /// Set la variable ReceivedTestCaseId
        /// </summary>
        /// <param name="receivedTempProperty"></param>
        private void SetReceivedTestCaseIdProperty(int receivedTempProperty)
        {
            if (receivedTempProperty != 0)
            {
                ReceivedTestCaseId = receivedTempProperty;
                FillStepList(true);
            }
        }

        /// <summary>
        /// Recorre la lista y mueve el objeto selecionado hacia atras o adelante según el parametro que recibe.
        /// </summary>
        /// <param name="opcionTemp"></param>
        private void NavigateInStepList(string opcionTemp)
        {
            int NumberOfStep = 0;
            int StepPosition=0;
            if (StepList != null)
                NumberOfStep = StepList.Count;

            if (opcionTemp == "Front")
            {
                StepPosition= Convert.ToInt32(SelectedStepItem.RowNumber);
                if (StepPosition < NumberOfStep)
                {
                    foreach (var item in StepList)
                    {
                        if ((item.RowNumber - 1) == StepPosition)
                        {
                            SelectedStepItem = item;
                        }
                    }
                }
            }

            if (opcionTemp == "Back")
            {
                StepPosition = Convert.ToInt32(SelectedStepItem.RowNumber);
                if (StepPosition <= NumberOfStep)
                {
                    foreach (var item in StepList)
                    {
                        if ((item.RowNumber + 1) == StepPosition)
                        {
                            SelectedStepItem = item;
                        }
                    }
                }
            }
            
        }

        #endregion

    }
}
