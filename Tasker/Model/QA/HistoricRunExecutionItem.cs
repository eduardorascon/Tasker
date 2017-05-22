using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Helpers;

namespace Tasker.Model.QA
{
   public class HistoricRunExecutionItem:ModelBaseEx
    {

        #region Fields
        Functions _oFx = new Functions();
        #endregion

        #region Contructor

        public HistoricRunExecutionItem()
        {

        }

        public HistoricRunExecutionItem(string approved, DateTime date, int runExecId, int testCaseId, int stepId)
        {
            Approved = approved;
            Date = date;
            RunExecId = runExecId;
            TestCaseId = testCaseId;
            StepId = stepId;
        }

        public HistoricRunExecutionItem(string approved, DateTime date,Int64 rowNumber)
        {
            Approved = approved;
            Date = date;
            RowNumber = rowNumber;
        }

        ~HistoricRunExecutionItem()
        {

        }
      
        #endregion

        #region Properties
        
        /// <summary>
        /// The <see cref="RowNumber" /> property's name.
        /// </summary>
        public const string RowNumberPropertyName = "RowNumber";

        private Int64 _rowNumber = 0;

        /// <summary>
        /// Sets and gets the RowNumber property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Int64 RowNumber
        {
            get
            {
                return _rowNumber;
            }

            set
            {
                if (_rowNumber == value)
                {
                    return;
                }

                RaisePropertyChanging(RowNumberPropertyName);
                _rowNumber = value;
                RaisePropertyChanged(RowNumberPropertyName);
            }
        }


        /// <summary>
        /// The <see cref="Approved" /> property's name.
        /// </summary>
        public const string ApprovedPropertyName = "Approved";

        private string _approved = string.Empty;

        /// <summary>
        /// Sets and gets the Approved property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Approved
        {
            get
            {
                return _approved;
            }

            set
            {
                if (_approved == value)
                {
                    return;
                }

                RaisePropertyChanging(ApprovedPropertyName);
                _approved = value;
                RaisePropertyChanged(ApprovedPropertyName);
            }
        }

        
        /// <summary>
        /// The <see cref="Date" /> property's name.
        /// </summary>
        public const string DatePropertyName = "Date";

        private DateTime _date = DateTime.Now;

        /// <summary>
        /// Sets and gets the Date property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public DateTime Date
        {
            get
            {
                return _date;
            }

            set
            {
                if (_date == value)
                {
                    return;
                }

                RaisePropertyChanging(DatePropertyName);
                _date = value;
                RaisePropertyChanged(DatePropertyName);
            }
        }

        
        /// <summary>
        /// The <see cref="RunExecId" /> property's name.
        /// </summary>
        public const string RunExecIdPropertyName = "RunExecId";

        private int _runExecId = 0;

        /// <summary>
        /// Sets and gets the RunExecId property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int RunExecId
        {
            get
            {
                return _runExecId;
            }

            set
            {
                if (_runExecId == value)
                {
                    return;
                }

                RaisePropertyChanging(RunExecIdPropertyName);
                _runExecId = value;
                RaisePropertyChanged(RunExecIdPropertyName);
            }
        }


        /// <summary>
        /// The <see cref="TestCaseId" /> property's name.
        /// </summary>
        public const string TestCaseIdPropertyName = "TestCaseId";

        private int _testCaseId = 0;

        /// <summary>
        /// Sets and gets the TestCaseId property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int TestCaseId
        {
            get
            {
                return _testCaseId;
            }

            set
            {
                if (_testCaseId == value)
                {
                    return;
                }

                RaisePropertyChanging(TestCaseIdPropertyName);
                _testCaseId = value;
                RaisePropertyChanged(TestCaseIdPropertyName);
            }
        }

        
        /// <summary>
        /// The <see cref="StepId" /> property's name.
        /// </summary>
        public const string StepIdPropertyName = "StepId";

        private int _stepId = 0;

        /// <summary>
        /// Sets and gets the StepId property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int StepId
        {
            get
            {
                return _stepId;
            }

            set
            {
                if (_stepId == value)
                {
                    return;
                }

                RaisePropertyChanging(StepIdPropertyName);
                _stepId = value;
                RaisePropertyChanged(StepIdPropertyName);
            }
        }
     
        #endregion

    }
}
