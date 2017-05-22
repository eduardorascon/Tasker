using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Helpers;

namespace Tasker.Model.QA
{
   public class RunExecutionDetailItem: ModelBaseEx
    {

       #region Fields
       Functions oFx = new Functions();
       #endregion

       #region Constructor
       public RunExecutionDetailItem()
       {
       }

       public RunExecutionDetailItem(int runExecDetail, int runExecId, int testCaseId, int stepId, string approved)
       {
           RunExecDetailId = runExecDetail;
           RunExecId = runExecId;
           TestCaseId = testCaseId;
           StepId = stepId;
           Approved = approved;
       }

       public RunExecutionDetailItem(int runExecId, int testCaseId)
       {
           RunExecId = runExecId;
           TestCaseId = testCaseId;
       }

       public RunExecutionDetailItem(int testCaseId)
       {
           TestCaseId = testCaseId;
       }

        #endregion

       #region Properties


       /// <summary>
       /// The <see cref="RunExecDetailId" /> property's name.
       /// </summary>
       public const string RunExecDetailIdPropertyName = "RunExecDetailId";

       private int _runExecDetailId = 0;

       /// <summary>
       /// Sets and gets the RunExecDetailId property.
       /// Changes to that property's value raise the PropertyChanged event. 
       /// </summary>
       public int RunExecDetailId
       {
           get
           {
               return _runExecDetailId;
           }

           set
           {
               if (_runExecDetailId == value)
               {
                   return;
               }

               RaisePropertyChanging(RunExecDetailIdPropertyName);
               _runExecDetailId = value;
               RaisePropertyChanged(RunExecDetailIdPropertyName);
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

        #endregion

    }
}
