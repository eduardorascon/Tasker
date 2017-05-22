using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Model;
using Tasker.Model.QA;

namespace Tasker.ViewModel.QA.TestCases
{
   public class RunExecutionTestCaseListViewModel: ViewModelBase
   {

       #region Private Member

       private readonly IDataService _dataService;
       public ICommand NewCommand { get; private set; }
       #endregion

       #region Constructor

       public RunExecutionTestCaseListViewModel(IDataService dataService)
       {
           _dataService = dataService;
           InitializeComponent();
       }

       #endregion
       
       #region Properties

       
       /// <summary>
       /// The <see cref="ExecutionDetailList" /> property's name.
       /// </summary>
       public const string ExecutionDetailListPropertyName = "ExecutionDetailList";

       private ObservableCollection<ExecutionDetailItem> _executionDetailList = new ObservableCollection<ExecutionDetailItem>();

       /// <summary>
       /// Sets and gets the ExecutionDetailList property.
       /// Changes to that property's value raise the PropertyChanged event. 
       /// </summary>
       public ObservableCollection<ExecutionDetailItem> ExecutionDetailList
       {
           get
           {
               return _executionDetailList;
           }

           set
           {
               if (_executionDetailList == value)
               {
                   return;
               }

               RaisePropertyChanging(ExecutionDetailListPropertyName);
               _executionDetailList = value;
               RaisePropertyChanged(ExecutionDetailListPropertyName);
           }
       }

       /// <summary>
       /// The <see cref="SelectedExecutionDetailItem" /> property's name.
       /// </summary>
       public const string SelectedExecutionDetailItemPropertyName = "SelectedExecutionDetailItem";

       private ExecutionDetailItem _selectedExecutionDetailItem = new ExecutionDetailItem();

       /// <summary>
       /// Sets and gets the SelectedExecutionDetailItem property.
       /// Changes to that property's value raise the PropertyChanged event. 
       /// </summary>
       public ExecutionDetailItem SelectedExecutionDetailItem
       {
           get
           {
               return _selectedExecutionDetailItem;
           }

           set
           {
               if (_selectedExecutionDetailItem == value)
               {
                   return;
               }

               RaisePropertyChanging(SelectedExecutionDetailItemPropertyName);
               _selectedExecutionDetailItem = value;
               RaisePropertyChanged(SelectedExecutionDetailItemPropertyName);
               if (SelectedExecutionDetailItem != null)
               {
                   //Send To: RUNEXECUTIONMAINVIEWMODEL
                   Messenger.Default.Send<int>(SelectedExecutionDetailItem.TestCaseId, "SET_RECEIVEDTESTPLANIDPROPERTY_RUNEXECUTIONSTEPLISTVIEWMODEL");
                   //Send To: RUNEXECUTIONMAINVIEWMODEL
                   Messenger.Default.Send<ExecutionDetailItem>(SelectedExecutionDetailItem, "SET_SETRECEIVEDEXECUTIONDETAILPROPERTY_RUNEXECUTIONMAINVIEWMODEL");
                   //Send To: RUNEXECUTIONFAULTEXCEPTIONMAINVM
                   Messenger.Default.Send<int>(SelectedExecutionDetailItem.TestCaseId,"SET_TESTCASEID_RUNEXECUTIONFAULTEXCEPTIONMAINVM");
               }
               else
               {
                   //Send To: 
                   Messenger.Default.Send<bool>(true, "CLEAN_SELECTEDTESTCASEPROPERTY_RUNEXECUTIONSTEPLISTVIEWMODEL");
               }
           }
       }

        /// <summary>
        /// Propiedad que recibe el ExecutionId del elemento selecionado.
        /// </summary>
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

        
        #endregion

       #region Methods

        /// <summary>
        /// Inicializa las variables y registra los mensajes.
        /// </summary>
        void InitializeComponent()
        {
            //Inicializar Datos.
            ReceivedExecId = 0;
            // Listen the Messenger
            Messenger.Default.Register<bool>(this, "CLEAN_PROPERTIES_RUNEXECUTIONTESTCASELISTVIEWMODEL", CleanControls);
            Messenger.Default.Register<int>(this, "SET_RECEIVEDEXECUTIONIDPROPERTY_RUNEXECUTIONTESTCASELISTVIEWMODEL", SetReceivedExecIdProperty);
           
        }

        /// <summary>
        /// Llena la lista de tipo ExecutionDetailItem
        /// </summary>
        /// <param name="doYouWantToFill"></param>
        private void FillTestCaseList(bool doYouWantToFill)
        {
            if (doYouWantToFill == true)
                if (ReceivedExecId != 0)
                    _dataService.GetExecutionDetailListByExecId(ReceivedExecId, AfterGetTestCases);
        }

        private void AfterGetTestCases(IList<ExecutionDetailItem> execDetailTempList, Exception exception)
        {
            ExecutionDetailList = new ObservableCollection<ExecutionDetailItem>(execDetailTempList);
        }

        /// <summary>
        /// Restable los controles a vacio.
        /// </summary>
        /// <param name="restart"></param>
        private void CleanControls(bool restart)
        {
            if (restart == true)
            {
                SelectedExecutionDetailItem = null;
                ReceivedExecId = 0;
            }
        }

        /// <summary>
        /// Set el ExecId en base al parámetro recibido.
        /// </summary>
        /// <param name="receivedTempProperty"></param>
        private void SetReceivedExecIdProperty(int receivedTempProperty)
        {
            if (receivedTempProperty != 0)
            {
                ReceivedExecId = receivedTempProperty;
                FillTestCaseList(true);
            }
            
        }

        #endregion

    }
}
