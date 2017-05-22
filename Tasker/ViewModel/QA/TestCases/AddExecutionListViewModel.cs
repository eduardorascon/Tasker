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

namespace Tasker.ViewModel.QA.TestCases
{
   public class AddExecutionListViewModel:ViewModelBase
   {

        #region Constructor

       public AddExecutionListViewModel(IDataService dataService)
       {
           _dataService = dataService;
           IniciliziationCommand();
       }

       #endregion

        #region Private Member

       private readonly IDataService _dataService;

       #endregion
       
        #region Properties


       /// <summary>
       /// The <see cref="ExecutionDetailItem" /> property's name.
       /// </summary>
       public const string ExecutionDetailItemPropertyName = "ExecutionDetailItem";

       private ExecutionDetailItem _executionDetailItem = new ExecutionDetailItem();

       /// <summary>
       /// Sets and gets the ExecutionDetailItem property.
       /// Changes to that property's value raise the PropertyChanged event. 
       /// </summary>
       public ExecutionDetailItem ExecutionDetailItem
       {
           get
           {
               return _executionDetailItem;
           }

           set
           {
               if (_executionDetailItem == value)
               {
                   return;
               }

               RaisePropertyChanging(ExecutionDetailItemPropertyName);
               _executionDetailItem = value;
               RaisePropertyChanged(ExecutionDetailItemPropertyName);
               if (ExecutionDetailItem != null)
               {
                   if(ExecutionDetailItem.ExecId!=0)
                   {
                       Messenger.Default.Send<bool>(true, "SET_ENABLEREMOVETESTCASEBUTTOM_ADDEXECUTIONVIEWMODEL");
                       Messenger.Default.Send<ExecutionDetailItem>(ExecutionDetailItem, "SET_SETRECEIVEDEXECUTIONDETAILITEM_ADDEXECUTIONVIEWMODEL");
                   }
                   else
                   Messenger.Default.Send<bool>(false,"SET_ENABLEREMOVETESTCASEBUTTOM_ADDEXECUTIONVIEWMODEL");
               }
           }
       }


       /// <summary>
       /// The <see cref="ExecutionDetailListItem" /> property's name.
       /// </summary>
       public const string ExecutionDetailListItemPropertyName = "ExecutionDetailListItem";

       private ObservableCollection<ExecutionDetailItem> _executionDeatilListItem = new ObservableCollection<ExecutionDetailItem>();

       /// <summary>
       /// Sets and gets the ExecutionDetailListItem property.
       /// Changes to that property's value raise the PropertyChanged event. 
       /// </summary>
       public ObservableCollection<ExecutionDetailItem> ExecutionDetailListItem
       {
           get
           {
               return _executionDeatilListItem;
           }

           set
           {
               if (_executionDeatilListItem == value)
               {
                   return;
               }

               RaisePropertyChanging(ExecutionDetailListItemPropertyName);
               _executionDeatilListItem = value;
               RaisePropertyChanged(ExecutionDetailListItemPropertyName);
           }
       }

        /// <summary>
        /// Propiedad que recibe el valor de la propiedad padre para poder guardar o modificar.
        /// </summary>
        public int ReceivedExecutionId { get; set; }


        public TestCaseItem ReceivedTestCaseItem { get; set; }

        
        #endregion

        #region Methods

        /// <summary>
        /// Inicializa los comandos y registra los mensajes.
        /// </summary>
        private void IniciliziationCommand()
        {
            //Inicializar Datos.
            ReceivedExecutionId = 0;
            ReceivedTestCaseItem = new TestCaseItem();
            // Listen the Messenger
            Messenger.Default.Register<bool>(this, "FILL_TESTCASEITEMLIST_ADDEXECUTIONLISTVIEWMODEL", FillTestCaseList);
            Messenger.Default.Register<bool>(this, "CLEAN_SELECTEDTESTCASEPROPERTY_ADDEXECUTIONLISTVIEWMODEL", CleanControls);
            Messenger.Default.Register<int>(this, "SET_RECEIVEDEXECUTIONIDPROPERTY_ADDEXECUTIONLISTVIEWMODEL", SetReceivedExecutionIdProperty);
            
           
        }
        
        /// <summary>
        /// Llena la variable de tipo lista.
        /// </summary>
        /// <param name="doYouWantToFill"></param>
        private void FillTestCaseList(bool doYouWantToFill)
        {
            if (doYouWantToFill == true)
                if (ReceivedExecutionId != 0)
                {
                    ExecutionDetailItem tempExecutionList = new ExecutionDetailItem();
                    _dataService.GetExecutionDetailList(ReceivedExecutionId, AfterGetTestCases);
                }

         
        }


        private void AfterGetTestCases(IList<ExecutionDetailItem> testCases, Exception exception)
        {
            ExecutionDetailListItem = new ObservableCollection<ExecutionDetailItem>(testCases);
        }

        /// <summary>
        /// Restablece los controles a vacio.
        /// </summary>
        /// <param name="restart"></param>
        private void CleanControls(bool restart)
        {
            if (restart == true)
            {
                ExecutionDetailItem = new ExecutionDetailItem();
                ReceivedExecutionId = 0;
                ExecutionDetailListItem = new ObservableCollection<ExecutionDetailItem>();
                ReceivedTestCaseItem = new TestCaseItem();
            }
        }

        /// <summary>
        /// Recibe el Id de la execution
        /// </summary>
        /// <param name="receivedTempProperty"></param>
        private void SetReceivedExecutionIdProperty(int receivedTempProperty)
        {
            if (receivedTempProperty != 0)
                ReceivedExecutionId = receivedTempProperty;
        }

        #endregion

    }
}
