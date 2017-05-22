using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Helpers;
using Tasker.ViewModel.QA;

namespace Tasker.Model.QA
{
   public class StepItem : ModelBaseEx
    {
        #region Fields
        Functions _oFx = new Functions();
        #endregion

        #region Construct

        public StepItem()
        {
        }

        public StepItem(Int64 rowNumber, int numberOfStep, int testCaseId, int stepId, string description, string input, string expectedResult, byte[] image, bool isErasableTemp)
        {
            RowNumber = rowNumber;
            TestCaseId = testCaseId;
            StepId = stepId;
            Description = description;
            Input = input;
            ExpectedResult = expectedResult;
            image = Image;
            NumberOfStep = numberOfStep;
            IsErasable = isErasableTemp;
        }

        public StepItem(Int64 rowNumber, int numberOfStep, int testCaseId, int stepId, string description, string input, string expectedResult, byte[] image, string _tempApprove, bool isErasableTemp,string history1, string history2, string history3)
        {
            RowNumber = rowNumber;
            TestCaseId = testCaseId;
            StepId = stepId;
            Description = description;
            Input = input;
            ExpectedResult = expectedResult;
            ApprovedReject = _tempApprove;
            image = Image;
            NumberOfStep = numberOfStep;
            IsErasable = isErasableTemp;
            History1 = history1;
            History2 = history2;
            History3 = history3;
        }

        public StepItem(Int64 rowNumber, int numberOfStep, int testCaseId, int stepId, string description, string input, string expectedResult, bool isErasableTemp)
        {
            RowNumber = rowNumber;
            TestCaseId = testCaseId;
            StepId = stepId;
            Description = description;
            Input = input;
            ExpectedResult = expectedResult;
            NumberOfStep = numberOfStep;
            IsErasable = isErasableTemp;
        }

        public StepItem(int testCaseId, int stepId, int numberOfStep )
        {
            TestCaseId = testCaseId;
            StepId = stepId;
            NumberOfStep = numberOfStep;
        }

        ~StepItem()
        {

        }


        #endregion

        #region Property

        
        /// <summary>
        /// The <see cref="IsErasable" /> property's name.
        /// </summary>
        public const string IsErasablePropertyName = "IsErasable";

        private bool _isErasable = true;

        /// <summary>
        /// Sets and gets the IsErasable property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsErasable
        {
            get
            {
                return _isErasable;
            }

            set
            {
                if (_isErasable == value)
                {
                    return;
                }

                RaisePropertyChanging(IsErasablePropertyName);
                _isErasable = value;
                RaisePropertyChanged(IsErasablePropertyName);
            }
        }
        
        /// <summary>
        /// The <see cref="NumberOfStep" /> property's name.
        /// </summary>
        public const string NumberOfStepPropertyName = "NumberOfStep";

        private int _numberOfStep = 0;

        /// <summary>
        /// Sets and gets the NumberOfStep property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int NumberOfStep
        {
            get
            {
                return _numberOfStep;
            }

            set
            {
                if (_numberOfStep == value)
                {
                    return;
                }

                RaisePropertyChanging(NumberOfStepPropertyName);
                _numberOfStep = value;
                RaisePropertyChanged(NumberOfStepPropertyName);
                if (NumberOfStep != 0)
                {
                    PaqueteMSJ paqueteTemp = new PaqueteMSJ();
                    paqueteTemp.Informacion = NumberOfStep.ToString();
                    paqueteTemp.NombrePropiedad = "NumberOfStep";
                    Messenger.Default.Send<PaqueteMSJ>(paqueteTemp, "SET_RECEIVEDSTEPITEM_IN_ADDSTEPSMAINVM_FROMCONSTRUCTOR");
                }
            }
        }

        /// <summary>
        /// The <see cref="IsNew" /> property's name.
        /// </summary>
        public const string IsNewPropertyName = "IsNew";

        private bool _isNew = false;

        /// <summary>
        /// Sets and gets the IsNew property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>


        /// <summary>
        /// The <see cref="Image" /> property's name.
        /// </summary>
        public const string ImagePropertyName = "Image";

        private byte[] _image = new byte[0];

        /// <summary>
        /// Sets and gets the Image property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public byte[] Image
        {
            get
            {
                return _image;
            }

            set
            {
                if (_image == value)
                {
                    return;
                }

                RaisePropertyChanging(ImagePropertyName);
                _image = value;
                RaisePropertyChanged(ImagePropertyName);
            }
        } 

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
                if (Description != null)
                {
                    PaqueteMSJ paqueteTemp = new PaqueteMSJ();
                    paqueteTemp.Informacion = Description;
                    paqueteTemp.NombrePropiedad = "Description";
                    Messenger.Default.Send<PaqueteMSJ>(paqueteTemp, "SET_RECEIVEDSTEPITEM_IN_ADDSTEPSMAINVM_FROMCONSTRUCTOR");
                }
            }
        }


        /// <summary>
        /// The <see cref="Input" /> property's name.
        /// </summary>
        public const string InputPropertyName = "Input";

        private string _input = string.Empty;

        /// <summary>
        /// Sets and gets the Input property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Input
        {
            get
            {
                return _input;
            }

            set
            {
                if (_input == value)
                {
                    return;
                }

                RaisePropertyChanging(InputPropertyName);
                _input = value;
                RaisePropertyChanged(InputPropertyName);
                if (Input != null)
                {
                    PaqueteMSJ paqueteTemp = new PaqueteMSJ();
                    paqueteTemp.Informacion = Input;
                    paqueteTemp.NombrePropiedad = "Input";
                    Messenger.Default.Send<PaqueteMSJ>(paqueteTemp, "SET_RECEIVEDSTEPITEM_IN_ADDSTEPSMAINVM_FROMCONSTRUCTOR");
                }
            }
        }


        /// <summary>
        /// The <see cref="ExpectedResult" /> property's name.
        /// </summary>
        public const string ExpectedResultPropertyName = "ExpectedResult";

        private string _expectedResult = string.Empty;

        /// <summary>
        /// Sets and gets the ExpectedResult property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string ExpectedResult
        {
            get
            {
                return _expectedResult;
            }

            set
            {
                if (_expectedResult == value)
                {
                    return;
                }

                RaisePropertyChanging(ExpectedResultPropertyName);
                _expectedResult = value;
                RaisePropertyChanged(ExpectedResultPropertyName);
                if (ExpectedResult != null)
                {
                    PaqueteMSJ paqueteTemp = new PaqueteMSJ();
                    paqueteTemp.Informacion = ExpectedResult;
                    paqueteTemp.NombrePropiedad = "ExpectedResult";
                    Messenger.Default.Send<PaqueteMSJ>(paqueteTemp, "SET_RECEIVEDSTEPITEM_IN_ADDSTEPSMAINVM_FROMCONSTRUCTOR");
                }
            }
        }


        
        /// <summary>
        /// The <see cref="ApprovedReject" /> property's name.
        /// </summary>
        public const string ApprovedRejectPropertyName = "ApprovedReject";

        private string _approveReject = string.Empty;

        /// <summary>
        /// Sets and gets the ApprovedReject property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string ApprovedReject
        {
            get
            {
                return _approveReject;
            }

            set
            {
                if (_approveReject == value)
                {
                    return;
                }

                RaisePropertyChanging(ApprovedRejectPropertyName);
                _approveReject = value;
                RaisePropertyChanged(ApprovedRejectPropertyName);
            }
        }

        
        /// <summary>
        /// The <see cref="History1" /> property's name.
        /// </summary>
        public const string History1PropertyName = "History1";

        private string _history1 = string.Empty;

        /// <summary>
        /// Sets and gets the History1 property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string History1
        {
            get
            {
                return _history1;
            }

            set
            {
                if (_history1 == value)
                {
                    return;
                }

                RaisePropertyChanging(History1PropertyName);
                _history1 = value;
                RaisePropertyChanged(History1PropertyName);
            }
        }

        
        /// <summary>
        /// The <see cref="History2" /> property's name.
        /// </summary>
        public const string History2PropertyName = "History2";

        private string _history2 = string.Empty;

        /// <summary>
        /// Sets and gets the History2 property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string History2
        {
            get
            {
                return _history2;
            }

            set
            {
                if (_history2 == value)
                {
                    return;
                }

                RaisePropertyChanging(History2PropertyName);
                _history2 = value;
                RaisePropertyChanged(History2PropertyName);
            }
        }



        /// <summary>
        /// The <see cref="History3" /> property's name.
        /// </summary>
        public const string History3PropertyName = "History3";

        private string _history3 = string.Empty;

        /// <summary>
        /// Sets and gets the History3 property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string History3
        {
            get
            {
                return _history3;
            }

            set
            {
                if (_history3 == value)
                {
                    return;
                }

                RaisePropertyChanging(History3PropertyName);
                _history3 = value;
                RaisePropertyChanged(History3PropertyName);
            }
        }

        #endregion

    }
}
