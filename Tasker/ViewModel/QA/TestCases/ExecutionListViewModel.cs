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
    public class ExecutionListViewModel: ViewModelBase
    {

		#region Private Members

		private readonly IDataService _dataService;

		#endregion

        #region Constructor

        public ExecutionListViewModel(IDataService dataService)
        {
            _dataService = dataService;
            InicializationCommand();
        }

        #endregion

		#region Properties

		/// <summary>
		/// Almacena una lista de los Execution
		/// </summary>
        public ObservableCollection<ExecutionItem> ExecutionList
        {
            get { return _executionList; }
            set
            {
                if (_executionList != value)
                {
                    _executionList = value;
                    RaisePropertyChanged("ExecutionList");
                }
            }
        }
        private ObservableCollection<ExecutionItem> _executionList = new ObservableCollection<ExecutionItem>();

        /// <summary>
        /// Almacena el item selecionado de la lista de execution
        /// </summary>
        public ExecutionItem SelectedExecutionItem
        {
            get { return _selectedExecutionItem; }
            set
            {
                if (_selectedExecutionItem != value)
                {
                    _selectedExecutionItem = value;
                    RaisePropertyChanged("SelectedExecutionItem");
                }

                if (SelectedExecutionItem != null)
                {
                    if (SelectedExecutionItem.ExecId != 0)
                    {
                        Messenger.Default.Send<ExecutionItem>(SelectedExecutionItem, "SET_RECEIVEDEXECUTIONITEM_EXECUTIONMAINVIEWMODEL");
                        Messenger.Default.Send<ExecutionItem>(SelectedExecutionItem, "SET_RECEIVEDEXECUTIONHEADER_ADDEXECUTIONVIEWMODEL");
                        Messenger.Default.Send<string>("SELECTEDITEM", "SET_ENABLEBUTTOMPROPERTY_EXECUTIONMAINCONTROLBARVIEWMODEL");
                        // Send to: ExecutionMainViewTestCaseListViewModel
                        Messenger.Default.Send<int>(SelectedExecutionItem.ExecId, "SET_RECEIVEDEXECUTIONIDPROPERTY_EXECUTIONMAINVIEWTESTCASELISTVIEWMODEL");
                        //Send To: RUNEXECUTIONTESTCASELISTVIEWMODEL
                        Messenger.Default.Send<int>(SelectedExecutionItem.ExecId, "SET_RECEIVEDEXECUTIONIDPROPERTY_RUNEXECUTIONTESTCASELISTVIEWMODEL");
                        //Send To: RUNEXECUTIONMAINVIEWMODEL
                        Messenger.Default.Send<ExecutionItem>(SelectedExecutionItem, "SET_RECEIVEDEXECIDPROPERTY_RUNEXECUTIONMAINVIEWMODEL");
                        //Send To: RUNEXECUTIONSTEPLISTVIEWMODEL
                        Messenger.Default.Send<int>(SelectedExecutionItem.ExecId, "SET_RECEIVEDEXECIDPROPERTY_RUNEXECUTIONSTEPLISTVIEWMODEL");

                        //recargar lista
                        //Send To: RUNEXECUTIONSTEPLISTVIEWMODEL
                        Messenger.Default.Send<int>(SelectedExecutionItem.ExecId, "SET_RECEIVEDEXECIDPROPERTY_RUNEXECUTIONSTEPLISTVIEWMODEL");

                        if (SelectedExecutionItem.Porcent==-1)
                        {
                            RunExecutionItem newExecHeader = new RunExecutionItem();
                            newExecHeader.ExecId = SelectedExecutionItem.ExecId;
                            SaveRunExecution(newExecHeader);
                            Messenger.Default.Send<string>("UNPLAY", "SET_ENABLEBUTTOMPROPERTY_EXECUTIONMAINCONTROLBARVIEWMODEL");
                            Messenger.Default.Send<bool>(true, "CLEANCONTROL_RUNEXECUTIONFAULTEXCEPTIONMAINVM");
                        }
                    }
                }
                else
                {
                    Messenger.Default.Send<string>("DEAFULT", "SET_ENABLEBUTTOMPROPERTY_EXECUTIONMAINCONTROLBARVIEWMODEL");
                    Messenger.Default.Send<bool>(true, "CLEAN_SELECTEDTESTCASEPROPERTY_EXECUTIONMAINVIEWTESTCASELISTVIEWMODEL");
                }
                                
            }
        }
        private ExecutionItem _selectedExecutionItem = new ExecutionItem();

		#endregion

		#region Private Methods

        void InicializationCommand()
        {
            ExecutionList = new ObservableCollection<ExecutionItem>();
            _dataService.GetExecution(AfterGetExecutionList);

            //Listen Messages
            Messenger.Default.Register<bool>(this, "REFRESH_EXECUTIONLIST_EXECUTIONLISTVIEWMODEL", FillExecutionList);
            Messenger.Default.Register<bool>(this, "CLEAN_SELECTEDITEM_EXECUTIONLISTVIEWMODEL", CleanControls);
        }

        /// <summary>
        /// Llama al método de obtener lista de execution de la base de datos.
        /// </summary>
        /// <param name="doYouWantToFill"></param>
        private void FillExecutionList(bool doYouWantToFill)
        {
            if (doYouWantToFill==true)
                _dataService.GetExecution(AfterGetExecutionList);
        }

        /// <summary>
        /// Despues de obetener la nueva lista de execution se la asigan a la variable de tipo lista.
        /// </summary>
        /// <param name="executionListTemp"></param>
        /// <param name="exception"></param>
        private void AfterGetExecutionList(IList<ExecutionItem> executionListTemp, Exception exception)
		{
            ExecutionList = new ObservableCollection<ExecutionItem>(executionListTemp);
		}

        /// <summary>
        /// Restablece a nulo el item selecionado
        /// </summary>
        /// <param name="borrar"></param>
		private void CleanControls(bool borrar)
		{ 
			if (borrar==true)
			{
				SelectedExecutionItem=null;
			}
		}

        /// <summary>
        /// Guarda encabezado de RunExecution
        /// </summary>
        /// <param name="_tempRecievedItem"></param>
        private void SaveRunExecution(RunExecutionItem _tempRecievedItem)
        {
            _dataService.SaveRunExecutionHeader2(_tempRecievedItem, (resulDTO, exception) =>
            {
                if (!resulDTO.HasError)
                {
                    FillExecutionList(true);
                }
            });
        }

		#endregion

    }
}
