using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Tasker.Classes;
using Tasker.Helpers;
using Tasker.Model;
using Tasker.Model.QA;

namespace Tasker.ViewModel.QA.TestCases
{
    public class TestCaseViewModel : ViewModelBase
    {
        #region Private Member

        private readonly IDataService _dataService;
        private TestCaseItem _originalReceivedTestCaseItem = new TestCaseItem();

        #endregion

        #region Constructor

        public TestCaseViewModel(IDataService dataService)
        {
            _dataService = dataService;
            DelegarComandos();
        }

        #endregion

        #region Properties

        /// <summary>
        /// The <see cref="ReceivedTestCaseItem" /> property's name.
        /// </summary>
        public const string ReceivedTestCaseItemPropertyName = "ReceivedTestCaseItem";

        private TestCaseItem _receivedTestCaseItem ;

        /// <summary>
        /// Sets and gets the SelectedTestCaseItem property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public TestCaseItem ReceivedTestCaseItem
        {
            get
            {
                return _receivedTestCaseItem;
            }

            set
            {
                if (_receivedTestCaseItem == value)
                {
                    return;
                }
                RaisePropertyChanging(ReceivedTestCaseItemPropertyName);
                _receivedTestCaseItem = value;
                RaisePropertyChanged(ReceivedTestCaseItemPropertyName);
                if (ReceivedTestCaseItem != null)
                {
                    if (ReceivedTestCaseItem.TestCaseId!=0)
                      SelectedTestPlanId=  ReceivedTestCaseItem.TestPlanId;
                }
            }
        }


        public int SelectedTestPlanId
        {
            get { return _selectedTestPlanId; }
            set
            {
                if (_selectedTestPlanId != value)
                {
                    _selectedTestPlanId = value;
                    RaisePropertyChanged("SelectedTestPlanId");
                }
            }
        }
        private int _selectedTestPlanId=0;


        #endregion

        #region Commands
        //public RelayCommand AddTestCaseCommand
        //{
        //    get;
        //    private set;
        //}

        //public RelayCommand RemoveTestCaseCommand
        //{
        //    get;
        //    private set;
        //}

        //public RelayCommand ActivateTestCaseCommand
        //{
        //    get;
        //    private set;
        //}
        #endregion

        #region Private Methods
        
        /// <summary>
        /// Inicializa las Varaibles y Registra los mensajes.
        /// </summary>
        void DelegarComandos()
        {
            SelectedTestPlanId = new int();
            SelectedTestPlanId = 0;
            ReceivedTestCaseItem = new TestCaseItem();
            
            // Listen the Messages
            Messenger.Default.Register<bool>(this, "CREATENEWTESTCASE_TESTCASEHEADERVM", NewPropertyReceivedTestCaseItem);
            Messenger.Default.Register<TestCaseItem>(this, "SELECTEDTESTCASE_TESTCASEVM", SetPropertySelectedTestCaseItem);
            Messenger.Default.Register<int>(this, "SET_SELECTEDTESTPLANID_TESTCASEVM", SetPropertySelectedTestPlanId);
            Messenger.Default.Register<int>(this, "SET_SELECTEDTESTPLANIDFROMTESTPLANMV_TESTCASEMAINVM", GetSelectedTestPlanIdFromTestPlanVM);
            Messenger.Default.Send<bool>(true, "GET_SELECTEDTESTPLANIDFROMTESTPLANMV_TESTCASEMAINVM");
           
        }

        /// <summary>
        /// Set TestPlanId, recibe el id de selected item de la lista de step Plan
        /// </summary>
        /// <param name="receivedTestPlanId"></param>
        private void GetSelectedTestPlanIdFromTestPlanVM(int receivedTestPlanId)
        {
            if (receivedTestPlanId != 0)
                SelectedTestPlanId = receivedTestPlanId;
        }

        /// <summary>
        /// Método que recibe un int y Setea la variable SelectedTestPlanId
        /// </summary>
        /// <param name="tempTestPlanId"></param>
        private void SetPropertySelectedTestPlanId(int tempTestPlanId)
        {
            SelectedTestPlanId = tempTestPlanId;
        }

        /// <summary>
        /// Iguala la Varaiable ReceivedTestCaseItem al objeto recibido 
        /// </summary>
        /// <param name="tempTestCaseItem"></param>
        private void SetPropertySelectedTestCaseItem(TestCaseItem tempTestCaseItem)
        {
            if (tempTestCaseItem != null)
            {
                ReceivedTestCaseItem = tempTestCaseItem;
            }
            else
            {
                //pendiente msj no se pudo mostra la información.
            }
        }

        /// <summary>
        /// Crear una nueva instancia de la variable ReceivedTestCaseItem.
        /// </summary>
        /// <param name="createNewTestPlanObject"></param>
        private void NewPropertyReceivedTestCaseItem(bool createNewTestPlanObject)
        {
            if (createNewTestPlanObject == true)
            {
                ReceivedTestCaseItem = new TestCaseItem();
            }
        }

        /// <summary>
        /// Inicializa las variables a vacias.
        /// </summary>
        /// <param name="borrar"></param>
        private void CleanControls(bool borrar)
        {
            if (borrar == true)
            {
                ReceivedTestCaseItem = new TestCaseItem();
            }
        }

        #endregion
    }
}
