using System.Globalization;
using System.Windows.Controls;
using GalaSoft.MvvmLight;
using System;
using Tasker.Helpers;
using Tasker.ViewModel.QA;
using GalaSoft.MvvmLight.Messaging;

namespace Tasker.Model.QA
{
   public class TestCaseItem : ModelBaseEx
    {
        #region Fields
        Functions _oFx = new Functions();
        #endregion

       public TestCaseItem()
       {
           
       }

       public TestCaseItem(int testCaseId, string description, DateTime createdDate,
                           int duration, string objetive, string testData, string preCondition, int testPlanId)
       {
           TestCaseId = testCaseId;
           Description = description;
           CreatedDate = createdDate;
           Duration = duration;
           Objetive = objetive;
           TestData = testData;
           PreCondition = preCondition;
           TestPlanId = testPlanId;
           TestPlanIdString = testPlanId.ToString();
       }

        public TestCaseItem(int testCaseId, string description, DateTime createdDate,
                           int duration, string objetive, int testPlanId)
       {
           TestCaseId = testCaseId;
           Description = description;
           CreatedDate = createdDate;
           Duration = duration;
           Objetive = objetive;
           TestPlanId = testPlanId;
       }



        #region Properties

        #region TestCaseId
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
        #endregion
       
        #region Description
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
                    Messenger.Default.Send<PaqueteMSJ>(paqueteTemp, "SET_TESTCASEPROPERTY_IN-MAINVMTESTCASE_FROMCONSTRUCTOR");
                }
            }
        }
        #endregion
       
        #region CreatedDate
        /// <summary>
        /// The <see cref="CreatedDate" /> property's name.
        /// </summary>
        public const string CreatedDatePropertyName = "CreatedDate";

        private DateTime _createdDate;

        /// <summary>
        /// Sets and gets the CreatedDate property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public DateTime CreatedDate
        {
            get
            {
                return _createdDate;
            }

            set
            {
                if (_createdDate == value)
                {
                    return;
                }

                RaisePropertyChanging(CreatedDatePropertyName);
                _createdDate = value;
                RaisePropertyChanged(CreatedDatePropertyName);
            }
        }
        #endregion

        #region Duration
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
                if (Duration != null)
                {
                    PaqueteMSJ paqueteTemp = new PaqueteMSJ();
                    paqueteTemp.Informacion = Duration.ToString();
                    paqueteTemp.NombrePropiedad = "Duration";
                    Messenger.Default.Send<PaqueteMSJ>(paqueteTemp, "SET_TESTCASEPROPERTY_IN-MAINVMTESTCASE_FROMCONSTRUCTOR");
                }
            }
        }
        #endregion

        #region Objetive
        /// <summary>
        /// The <see cref="Objetive" /> property's name.
        /// </summary>
        public const string ObjetivePropertyName = "Objetive";

        private string _objetive = string.Empty;

        /// <summary>
        /// Sets and gets the Objetive property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Objetive
        {
            get
            {
                return _objetive;
            }

            set
            {
                if (_objetive == value)
                {
                    return;
                }

                RaisePropertyChanging(ObjetivePropertyName);
                _objetive = value;
                RaisePropertyChanged(ObjetivePropertyName);
                if (Objetive != null)
                {
                    PaqueteMSJ paqueteTemp = new PaqueteMSJ();
                    paqueteTemp.Informacion = Objetive;
                    paqueteTemp.NombrePropiedad = "Objetive";
                    Messenger.Default.Send<PaqueteMSJ>(paqueteTemp, "SET_TESTCASEPROPERTY_IN-MAINVMTESTCASE_FROMCONSTRUCTOR");
                }
            }
        }
        #endregion
       
        #region TestData
        /// <summary>
        /// The <see cref="TestData" /> property's name.
        /// </summary>
        public const string TestDataPropertyName = "TestData";

        private string _testData = string.Empty;

        /// <summary>
        /// Sets and gets the TestData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string TestData
        {
            get
            {
                return _testData;
            }

            set
            {
                if (_testData == value)
                {
                    return;
                }

                RaisePropertyChanging(TestDataPropertyName);
                _testData = value;
                RaisePropertyChanged(TestDataPropertyName);
                if (TestData != null)
                {
                    PaqueteMSJ paqueteTemp = new PaqueteMSJ();
                    paqueteTemp.Informacion = TestData;
                    paqueteTemp.NombrePropiedad = "TestData";
                    Messenger.Default.Send<PaqueteMSJ>(paqueteTemp, "SET_TESTCASEPROPERTY_IN-MAINVMTESTCASE_FROMCONSTRUCTOR");
                }
            }
        }
        #endregion
       
        #region PreCondition
        /// <summary>
        /// The <see cref="PreCondition" /> property's name.
        /// </summary>
        public const string PreConditionPropertyName = "PreCondition";

        private string _preCondition = string.Empty;

        /// <summary>
        /// Sets and gets the PreCondition property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string PreCondition
        {
            get
            {
                return _preCondition;
            }

            set
            {
                if (_preCondition == value)
                {
                    return;
                }

                RaisePropertyChanging(PreConditionPropertyName);
                _preCondition = value;
                RaisePropertyChanged(PreConditionPropertyName);
                if (PreCondition != null)
                {
                    PaqueteMSJ paqueteTemp = new PaqueteMSJ();
                    paqueteTemp.Informacion = PreCondition;
                    paqueteTemp.NombrePropiedad = "PreCondition";
                    Messenger.Default.Send<PaqueteMSJ>(paqueteTemp, "SET_TESTCASEPROPERTY_IN-MAINVMTESTCASE_FROMCONSTRUCTOR");
                }
            }
        }
        #endregion
       
        #region MainScreenImage
        /// <summary>
        /// The <see cref="MainScreenImage" /> property's name.
        /// </summary>
        public const string MainScreenImagePropertyName = "MainScreenImage";

        private Image  _mainScreenImage ;

        /// <summary>
        /// Sets and gets the MainScreenImage property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Image MainScreenImage
        {
            get
            {
                return _mainScreenImage;
            }

            set
            {
                if (_mainScreenImage == value)
                {
                    return;
                }

                RaisePropertyChanging(MainScreenImagePropertyName);
                _mainScreenImage = value;
                RaisePropertyChanged(MainScreenImagePropertyName);
            }
        }
        #endregion
  
        #region TestPlanId
        /// <summary>
        /// The <see cref="TestPlanId" /> property's name.
        /// </summary>
        public const string TestPlanIdPropertyName = "TestPlanId";

        private int _testPlanId = 0;

        /// <summary>
        /// Sets and gets the TestPlanId property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int TestPlanId
        {
            get
            {
                return _testPlanId;
            }

            set
            {
                if (_testPlanId == value)
                {
                    return;
                }

                RaisePropertyChanging(TestPlanIdPropertyName);
                _testPlanId = value;
                RaisePropertyChanged(TestPlanIdPropertyName);
                TestPlanIdString = value.ToString(CultureInfo.InvariantCulture);
                if (TestPlanId != null)
                {
                    PaqueteMSJ paqueteTemp = new PaqueteMSJ();
                    paqueteTemp.Informacion = TestPlanId.ToString();
                    paqueteTemp.NombrePropiedad = "TestPlanId";
                    TestPlanIdString = TestPlanId.ToString();
                    Messenger.Default.Send<PaqueteMSJ>(paqueteTemp, "SET_TESTCASEPROPERTY_IN-MAINVMTESTCASE_FROMCONSTRUCTOR");
                }
            }
        }
        #endregion
       
        #region TestCaseIdString just for Filter compatibility
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

        #region TestPlanIdString just for Filter compatibility
        /// <summary>
        /// The <see cref="TestPlanIdString" /> property's name.
        /// </summary>
        public const string TestPlanIdStringPropertyName = "TestPlanIdString";

        private string _testPlanIdString = string.Empty;

        /// <summary>
        /// Sets and gets the TestPlanIdString property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string TestPlanIdString
        {
            get
            {
                return _testPlanIdString;
            }

            set
            {
                if (_testPlanIdString == value)
                {
                    return;
                }

                RaisePropertyChanging(TestPlanIdStringPropertyName);
                _testPlanIdString = value;
                RaisePropertyChanged(TestPlanIdStringPropertyName);
            }
        }
        #endregion

   

        #endregion



    }
}
