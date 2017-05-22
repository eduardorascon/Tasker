using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Helpers;

namespace Tasker.Model.QA
{
   public class ExecutionDetailItem: ModelBaseEx
    {
        #region Fields
        Functions _oFx = new Functions();
        #endregion

        #region Contructor

        public ExecutionDetailItem()
        {

        }

        public ExecutionDetailItem(int execDetailId, int execId, int testCaseId, bool status)
        {
            ExecDetailId = execDetailId;
            ExecId = execId;
            TestCaseId = testCaseId;
            Status = status;
        }

        public ExecutionDetailItem(int execDetailId, int execId, int testCaseId, bool status, int duration, string objective)
        {
            ExecDetailId = execDetailId;
            ExecId = execId;
            TestCaseId = testCaseId;
            Status = status;
            Duration = duration;
            Objective = objective;
        }

        public ExecutionDetailItem(int execId, int testCaseId)
        {
            ExecId = execId;
            TestCaseId = testCaseId;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The <see cref="Objective" /> property's name.
        /// </summary>
        public const string ObjectivePropertyName = "Objective";

        private string _objective = string.Empty;

        /// <summary>
        /// Sets and gets the Objective property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Objective
        {
            get
            {
                return _objective;
            }

            set
            {
                if (_objective == value)
                {
                    return;
                }

                RaisePropertyChanging(ObjectivePropertyName);
                _objective = value;
                RaisePropertyChanged(ObjectivePropertyName);
            }
        }
        
        /// <summary>
        /// The <see cref="Duration" /> property's name.
        /// </summary>
        public const string DurationPropertyName = "Duration";

        private int _duration = 0;

        /// <summary>
        /// Sets and gets the Duration property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int Duration
        {
            get
            {
                return _duration;
            }

            set
            {
                if (_duration == value)
                {
                    return;
                }

                RaisePropertyChanging(DurationPropertyName);
                _duration = value;
                RaisePropertyChanged(DurationPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="IsErase" /> property's name.
        /// </summary>
        public const string IsErasePropertyName = "IsErase";

        private bool _isErase = false;

        /// <summary>
        /// Sets and gets the IsErase property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsErase
        {
            get
            {
                return _isErase;
            }

            set
            {
                if (_isErase == value)
                {
                    return;
                }

                RaisePropertyChanging(IsErasePropertyName);
                _isErase = value;
                RaisePropertyChanged(IsErasePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Description" /> property's name.
        /// </summary>
        public const string DescriptionPropertyName = "Description";

        private string _description = string.Empty;

        /// <summary>
        /// Sets and gets the Description property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Description
        {
            get
            {
                return _description;
            }

            set
            {
                if (_description == value)
                {
                    return;
                }

                RaisePropertyChanging(DescriptionPropertyName);
                _description = value;
                RaisePropertyChanged(DescriptionPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="ExecDetailId" /> property's name.
        /// </summary>
        public const string ExecDetailIdPropertyName = "ExecDetailId";

        private int _execDetailId = 0;

        /// <summary>
        /// Sets and gets the ExecDetailId property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int ExecDetailId
        {
            get
            {
                return _execDetailId;
            }

            set
            {
                if (_execDetailId == value)
                {
                    return;
                }

                RaisePropertyChanging(ExecDetailIdPropertyName);
                _execDetailId = value;
                RaisePropertyChanged(ExecDetailIdPropertyName);
            }
        }


        /// <summary>
        /// The <see cref="ExecId" /> property's name.
        /// </summary>
        public const string ExecIdPropertyName = "ExecId";

        private int _execId = 0;

        /// <summary>
        /// Sets and gets the ExecId property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int ExecId
        {
            get
            {
                return _execId;
            }

            set
            {
                if (_execId == value)
                {
                    return;
                }

                RaisePropertyChanging(ExecIdPropertyName);
                _execId = value;
                RaisePropertyChanged(ExecIdPropertyName);
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
                TestCaseIdString = value.ToString(CultureInfo.InvariantCulture);

            }
        }


        /// <summary>
        /// The <see cref="Status" /> property's name.
        /// </summary>
        public const string StatusPropertyName = "Status";

        private bool _status = true;

        /// <summary>
        /// Sets and gets the Status property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool Status
        {
            get
            {
                return _status;
            }

            set
            {
                if (_status == value)
                {
                    return;
                }

                RaisePropertyChanging(StatusPropertyName);
                _status = value;
                RaisePropertyChanged(StatusPropertyName);
            }
        }


        
        /// <summary>
        /// The <see cref="TestCaseIdString" /> property's name.
        /// </summary>
        public const string TestCaseIdStringPropertyName = "TestCaseIdString";

        private string _testCaseIdString = string.Empty;

        /// <summary>
        /// Sets and gets the TestCaseIdString property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string TestCaseIdString
        {
            get
            {
                return _testCaseIdString;
            }

            set
            {
                if (_testCaseIdString == value)
                {
                    return;
                }

                RaisePropertyChanging(TestCaseIdStringPropertyName);
                _testCaseIdString = value;
                RaisePropertyChanged(TestCaseIdStringPropertyName);
            }
        }

        #endregion

    }
}
