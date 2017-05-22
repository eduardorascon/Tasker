using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Tasker.Classes;
using Tasker.Model;
using Tasker.Model.QA;
using System.Windows;

namespace Tasker.ViewModel.QA.TestCases
{
    public class TestCasesViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        public TestCasesViewModel(IDataService dataService)
        {
            _dataService = dataService;
           DelegarComandos();
        }

        #region Properties
        
        /// <summary>
        /// The <see cref="TestCases" /> property's name.
        /// </summary>
        public const string TestCasesPropertyName = "TestCases";

        private ObservableCollection<TestCaseItem> _testCases = new ObservableCollection<TestCaseItem>();

        /// <summary>
        /// Sets and gets the TestCases property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<TestCaseItem> TestCases
        {
            get
            {
                return _testCases;
            }

            set
            {
                if (_testCases == value)
                {
                    return;
                }

                RaisePropertyChanging(TestCasesPropertyName);
                _testCases = value;
                RaisePropertyChanged(TestCasesPropertyName);
            }
        }

        
        /// <summary>
        /// The <see cref="SelectedTestCaseItem" /> property's name.
        /// </summary>
        public const string SelectedTestCaseItemPropertyName = "SelectedTestCaseItem";

        private TestCaseItem _selectedTestCaseItem;

        /// <summary>
        /// Sets and gets the SelectedTestCaseItem property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public TestCaseItem SelectedTestCaseItem
        {
            get
            {
                return _selectedTestCaseItem;
            }

            set
            {
                if (_selectedTestCaseItem == value)
                {
                    return;
                }

                RaisePropertyChanging(SelectedTestCaseItemPropertyName);
                _selectedTestCaseItem = value;
                RaisePropertyChanged(SelectedTestCaseItemPropertyName);
                if (SelectedTestCaseItem != null)
                {
                    Messenger.Default.Send<TestCaseItem>(SelectedTestCaseItem, "SELECTEDTESTCASE_TESTCASEVM");
                    Messenger.Default.Send<TestCaseItem>(SelectedTestCaseItem, "SET_SELECTEDTESTCASE_TESTCASEMAINVM");
                    Messenger.Default.Send<string>("SELECTEDITEM", "SET_ENABLEBUTTOMPROPERTY_TESTCASECONTROLBARVM");
                    Messenger.Default.Send<int>(SelectedTestCaseItem.TestPlanId, "SET_SELECTEDTESTPLANID_TESTCASECONTROLBARVM");
                    Messenger.Default.Send<int>(SelectedTestCaseItem.TestPlanId, "SET_SELECTEDTESTPLANID_TESTCASEMAINVM");

                    //Messages send to StepsListVM
                    Messenger.Default.Send<int>(SelectedTestCaseItem.TestCaseId, "SET_SELECTEDTESTCASEID_STEPLISTVIEWMODEL");
                    Messenger.Default.Send<bool>(true, "FILL_STEPITEMLIST_STEPLISTVIEWMODEL");
                    Messenger.Default.Send<string>("DEFAULT", "SET_ENABLEBUTTOMPROPERTY_TESTCASESTEPSLISTCONTROLBARVM");

                    //Messages send to AddStepMainVM
                    Messenger.Default.Send<int>(SelectedTestCaseItem.TestCaseId, "SET_SELECTEDTESTCASEID_ADDSTEPSMAINVM");

                    //messages send to addStepsControlBarVM
                    Messenger.Default.Send<int>(SelectedTestCaseItem.TestCaseId, "SET_SELECTEDTESTCASEID_STEPLISTCONTROLBARVM");

                    //Send to: ADDSTEPSMAINVM
                    Messenger.Default.Send<bool>(true, "CLEANCONTROL_ADDSTEPSVIEWMODEL");
                    
                }
                else
                {
                    Messenger.Default.Send<Visibility>(Visibility.Collapsed, "SET_VISIBILITYPROPSTEPLIST_TESTCASEMAINVM");
                    Messenger.Default.Send<string>("UNSELECTED", "SET_ENABLEBUTTOMPROPERTY_TESTCASESTEPSLISTCONTROLBARVM");
                    Messenger.Default.Send<bool>(true, "CLEANLIST_STEPLISTVIEWMODEL");
                    //messages send to addStepsControlBarVM
                    Messenger.Default.Send<int>(0, "SET_SELECTEDTESTCASEID_STEPLISTCONTROLBARVM");
                    
                }
            }
        }

        
        #endregion

        #region Methods

        /// <summary>
        /// Inicializa las varaibles y Registra los mensajes.
        /// </summary>
        void DelegarComandos()
        {
            // Inicializar Datos.
            _dataService.GetTestCases(AfterGetTestCases);
            // Listen the Messenger
            //Messenger.Default.Register<string>(this, "TESTCASE", ProcessMesseger);
            //Messenger.Default.Register<bool>(this, "Case", FillTestCaseList);
            Messenger.Default.Register<bool>(this, "FILL_TESTCASEITEMLIST_TESTCASESVM", FillTestCaseList);
            Messenger.Default.Register<bool>(this, "CLEAN_SELECTEDTESTCASEPROPERTY_TESTCASESVM", CleanControls);

        }

        /// <summary>
        /// Procesa la instrucción que desea realizar
        /// </summary>
        /// <param name="function"></param>
        private void ProcessMesseger(string function)
        {
                switch (function)
                {
                    case "Nuevo":
                        // Adding the new category to the list.
                        // and set the flag IsNew
                        var oNewTestCase = new TestCaseItem(){IsNew = true};
                        TestCases.Add(oNewTestCase);
                        SelectedTestCaseItem = oNewTestCase;
                        break;
                    case "RemoveNewTestCase":
                      // Remove the new category cause never should be stored in the database
                        TestCases.Remove(SelectedTestCaseItem);
                        break;
                }

        }

        /// <summary>
        /// Método que llena la lista de testCase
        /// </summary>
        /// <param name="doYouWantToFill"></param>
        private void FillTestCaseList(bool doYouWantToFill)
        {
            if (doYouWantToFill == true)
                _dataService.GetTestCases(AfterGetTestCases);
        }

        private void AfterGetTestCases(IList<TestCaseItem> testCases, Exception exception)
        {
            TestCases = new ObservableCollection<TestCaseItem>(testCases);
        }

        /// <summary>
        /// Inicializa los controles a vacio.
        /// </summary>
        /// <param name="borrar"></param>
        private void CleanControls(bool borrar)
        {
            if (borrar == true)
            {
                SelectedTestCaseItem = null;
            }
        }

        #endregion

    }
}
