using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Helpers;

namespace Tasker.Model.QA
{
  public class RunExecutionItem : ModelBaseEx
    {

      #region Fields
      Functions oFx = new Functions();
      #endregion

      #region Constructor

      public RunExecutionItem()
      {

      }

      public RunExecutionItem(int runExecId, int execId, double porcent)
      {
          RunExecId = runExecId;
          ExecId = execId;
          Porcent = porcent;
      }

      public RunExecutionItem(int runExecId, double porcent)
      {
          RunExecId = runExecId;
          Porcent = porcent;
      }

      public RunExecutionItem(int runExecId, int execId, double porcent, DateTime date, string userName, bool finish)
      {
          RunExecId = runExecId;
          ExecId = execId;
          Porcent = porcent;
          LastEditDate = date;
          UserName = userName;
          Finish = finish;
      }

      #endregion

      #region Properties


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
      /// The <see cref="Porcent" /> property's name.
      /// </summary>
      public const string PorcentPropertyName = "Porcent";

      private double _porcent = 0;

      /// <summary>
      /// Sets and gets the Porcent property.
      /// Changes to that property's value raise the PropertyChanged event. 
      /// </summary>
      public double Porcent
      {
          get
          {
              return _porcent;
          }

          set
          {
              if (_porcent == value)
              {
                  return;
              }

              RaisePropertyChanging(PorcentPropertyName);
              _porcent = value;
              RaisePropertyChanged(PorcentPropertyName);
          }
      }

      
      /// <summary>
      /// The <see cref="LastEditDate" /> property's name.
      /// </summary>
      public const string LastEditDatePropertyName = "LastEditDate";

      private DateTime _lastEditDate = DateTime.Now;

      /// <summary>
      /// Sets and gets the LastEditDate property.
      /// Changes to that property's value raise the PropertyChanged event. 
      /// </summary>
      public DateTime LastEditDate
      {
          get
          {
              return _lastEditDate;
          }

          set
          {
              if (_lastEditDate == value)
              {
                  return;
              }

              RaisePropertyChanging(LastEditDatePropertyName);
              _lastEditDate = value;
              RaisePropertyChanged(LastEditDatePropertyName);
          }
      }


      
      /// <summary>
      /// The <see cref="UserName" /> property's name.
      /// </summary>
      public const string UserNamePropertyName = "UserName";

      private string _userName = string.Empty;

      /// <summary>
      /// Sets and gets the UserName property.
      /// Changes to that property's value raise the PropertyChanged event. 
      /// </summary>
      public string UserName
      {
          get
          {
              return _userName;
          }

          set
          {
              if (_userName == value)
              {
                  return;
              }

              RaisePropertyChanging(UserNamePropertyName);
              _userName = value;
              RaisePropertyChanged(UserNamePropertyName);
          }
      }


      
      /// <summary>
      /// The <see cref="Finish" /> property's name.
      /// </summary>
      public const string FinishPropertyName = "Finish";

      private bool _finish = false;

      /// <summary>
      /// Sets and gets the Finish property.
      /// Changes to that property's value raise the PropertyChanged event. 
      /// </summary>
      public bool Finish
      {
          get
          {
              return _finish;
          }

          set
          {
              if (_finish == value)
              {
                  return;
              }

              RaisePropertyChanging(FinishPropertyName);
              _finish = value;
              RaisePropertyChanged(FinishPropertyName);
          }
      }

        #endregion

    }
}
