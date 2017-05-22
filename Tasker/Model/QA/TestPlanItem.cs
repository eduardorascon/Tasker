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
	public class TestPlanItem : ModelBaseEx
	{

		#region Fields
		Functions oFx = new Functions();
		#endregion

		#region Constructor

		 public TestPlanItem ()
			{
			}

		 public TestPlanItem(int testPlanId, string description, DateTime date, int aplicationId, string objectItem)
		{
			TestPlanId      = testPlanId;
			Description     = description;
			Date            = date;
			AplicationId    = aplicationId;
			ObjectItem      = objectItem;
		}

         public TestPlanItem(int testPlanId, string description, DateTime date, int aplicationId, string nameOfAplicationTemp,  string objectItem)
         {
             TestPlanId = testPlanId;
             Description = description;
             Date = date;
             AplicationId = aplicationId;
             NameOfAplicationTemp = nameOfAplicationTemp;
             ObjectItem = objectItem;
         }

         ~TestPlanItem()
         {
         }
		#endregion

		#region Propiedades  

         #region TestPlanId
         /// <summary>
         /// The <see cref="TestPlanId" /> property's name.
         /// </summary>
         public const string TestPlanIdPropertyName = "TestPlanId";

         private int _testPlanId = 999999;

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
                 if (TestPlanId != 999999)
                 { TestPlanIdString = TestPlanId.ToString(); }
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
                     Messenger.Default.Send<PaqueteMSJ>(paqueteTemp, "SET_TESTPLANPROPERTY_IN-MAINVMTESTPLAN_FROMCONSTRUCTOR");
                 }
             }
         }
         #endregion

         #region Date
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
         #endregion

         #region AplicationId
         /// <summary>
         /// The <see cref="AplicationId" /> property's name.
         /// </summary>
         public const string AplicationIdPropertyName = "AplicationId";

         private int _aplicationId = 9999999;

         /// <summary>
         /// Sets and gets the AplicationId property.
         /// Changes to that property's value raise the PropertyChanged event. 
         /// </summary>
         public int AplicationId
         {
             get
             {
                 return _aplicationId;
             }

             set
             {
                 if (_aplicationId == value)
                 {
                     return;
                 }

                 RaisePropertyChanging(AplicationIdPropertyName);
                 _aplicationId = value;
                 RaisePropertyChanged(AplicationIdPropertyName);
                 if (AplicationId != 9999999)
                 {
                     PaqueteMSJ paqueteTemp = new PaqueteMSJ();
                     paqueteTemp.Informacion = AplicationId.ToString();
                     paqueteTemp.NombrePropiedad = "AplicationId";
                     Messenger.Default.Send<PaqueteMSJ>(paqueteTemp, "SET_TESTPLANPROPERTY_IN-MAINVMTESTPLAN_FROMCONSTRUCTOR");
                 }

             }
         }
         #endregion

         #region NameOfAplicationTemp
         /// <summary>
         /// The <see cref="NameOfAplicationTemp" /> property's name.
         /// </summary>
         public const string NameOfAplicationTempPropertyName = "NameOfAplicationTemp";

         private string _nameOfAplicationTemp = string.Empty;

         /// <summary>
         /// Sets and gets the NameOfAplicationTemp property.
         /// Changes to that property's value raise the PropertyChanged event. 
         /// </summary>
         public string NameOfAplicationTemp
         {
             get
             {
                 return _nameOfAplicationTemp;
             }

             set
             {
                 if (_nameOfAplicationTemp == value)
                 {
                     return;
                 }

                 RaisePropertyChanging(NameOfAplicationTempPropertyName);
                 _nameOfAplicationTemp = value;
                 RaisePropertyChanged(NameOfAplicationTempPropertyName);
             }
         }
         #endregion

         #region Object
         /// <summary>
         /// The <see cref="Object" /> property's name.
         /// </summary>
         public const string ObjectItemPropertyName = "Object";

         private string _object = string.Empty;

         /// <summary>
         /// Sets and gets the Object property.
         /// Changes to that property's value raise the PropertyChanged event. 
         /// </summary>
         public string ObjectItem
         {
             get
             {
                 return _object;
             }

             set
             {
                 if (_object == value)
                 {
                     return;
                 }

                 RaisePropertyChanging(ObjectItemPropertyName);
                 _object = value;
                 RaisePropertyChanged(ObjectItemPropertyName);
                 if (ObjectItem != null)
                 {
                     PaqueteMSJ paqueteTemp = new PaqueteMSJ();
                     paqueteTemp.Informacion = ObjectItem;
                     paqueteTemp.NombrePropiedad = "ObjectItem";
                     Messenger.Default.Send<PaqueteMSJ>(paqueteTemp, "SET_TESTPLANPROPERTY_IN-MAINVMTESTPLAN_FROMCONSTRUCTOR");
                 }
             }
         }
         #endregion

         #region TestPlanIdString
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
