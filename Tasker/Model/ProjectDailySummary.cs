using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows.Media;
using GalaSoft.MvvmLight;
using Tasker.Helpers;

namespace Tasker.Model
{
    public class ProjectDailySummary : ObservableObject
    {
      Functions  ofx = new Functions();

      public ProjectDailySummary()
        {

        }

      #region ClosedIssueSelected
      /// <summary>
      /// The <see cref="ClosedIssueSelected" /> property's name.
      /// </summary>
      public const string ClosedIssueSelectedPropertyName = "ClosedIssueSelected";

      private IssueItem _closedIssueSelected;

      /// <summary>
      /// Sets and gets the ClosedItemSelected property.
      /// Changes to that property's value raise the PropertyChanged event. 
      /// </summary>
      public IssueItem ClosedItemSelected
      {
          get
          {
              return _closedIssueSelected;
          }

          set
          {
              if (_closedIssueSelected == value)
              {
                  return;
              }

              RaisePropertyChanging(ClosedIssueSelectedPropertyName);
              _closedIssueSelected = value;
              RaisePropertyChanged(ClosedIssueSelectedPropertyName);
          }
      }
      #endregion

      #region ClosedIssues
      /// <summary>
      /// The <see cref="ClosedIssues" /> property's name.
      /// </summary>
      public const string ClosedIssuesPropertyName = "ClosedIssues";

      private ObservableCollection<IssueItem> _closedIssues = new ObservableCollection<IssueItem>();

      /// <summary>
      /// Sets and gets the ClosedIssues property.
      /// Changes to that property's value raise the PropertyChanged event. 
      /// </summary>
      public ObservableCollection<IssueItem> ClosedIssues
      {
          get
          {
              return _closedIssues;
          }

          set
          {
              if (_closedIssues == value)
              {
                  return;
              }

              RaisePropertyChanging(ClosedIssuesPropertyName);
              _closedIssues = value;
              RaisePropertyChanged(ClosedIssuesPropertyName);
          }
      }
      #endregion
      
      #region ProjectName
      /// <summary>
      /// The <see cref="ProjectName" /> property's name.
      /// </summary>
      public const string ProjectNamePropertyName = "ProjectName";

      private string _projectName = string.Empty;

      /// <summary>
      /// Sets and gets the ProjectName property.
      /// Changes to that property's value raise the PropertyChanged event. 
      /// </summary>
      public string ProjectName
      {
          get
          {
              return _projectName;
          }

          set
          {
              if (_projectName == value)
              {
                  return;
              }

              RaisePropertyChanging(ProjectNamePropertyName);
              _projectName = value;
              RaisePropertyChanged(ProjectNamePropertyName);
          }
      }
      #endregion

      #region TotalClosedIssues
      /// <summary>
      /// The <see cref="TotalClosedIssues" /> property's name.
      /// </summary>
      public const string TotalClosedIssuesPropertyName = "TotalClosedIssues";

      private int _totalClosedIssues;

      /// <summary>
      /// Sets and gets the TotalClosedIssues property.
      /// Changes to that property's value raise the PropertyChanged event. 
      /// </summary>
      public int TotalClosedIssues
      {
          get
          {
              return _totalClosedIssues;
          }

          set
          {
              if (_totalClosedIssues == value)
              {
                  return;
              }

              RaisePropertyChanging(TotalClosedIssuesPropertyName);
              _totalClosedIssues = value;
              RaisePropertyChanged(TotalClosedIssuesPropertyName);
          }
      }
      #endregion

      
    }
}
