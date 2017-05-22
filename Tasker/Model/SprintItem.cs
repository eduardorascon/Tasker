using System;
using GalaSoft.MvvmLight;
using Tasker.Helpers;

namespace Tasker.Model
{
    public class SprintItem : ModelBaseEx
    {
        #region Fields

        readonly Functions _oFx = new Functions();
        #endregion
        public SprintItem(
            int sprintId,
                        int teamId,  
                        string projectId, 
                        string sprintName,
            string jiraProject,
                        string jiraInstance,
            string jqlFilter,
            bool overWriteJQLFilter,
            string jiraRapidView,
            string authorization)
        {
            SprintId = sprintId;
            TeamId = teamId;
            ProjectId = projectId;
            SprintName = sprintName;
            JIRAProject = jiraProject;
            JiraInstance = jiraInstance;
            JqlFilter = jqlFilter;
            OverWriteJQLFilter = overWriteJQLFilter;
            JiraRapidView = jiraRapidView;
            Authorization = authorization;
        }

        public SprintItem()
        {
            
        }


        #region SprintId
        /// <summary>
        /// The <see cref="SprintId" /> property's name.
        /// </summary>
        public const string SprintIdPropertyName = "SprintId";

        private int _sprintId;

        /// <summary>
        /// Sets and gets the SprintId property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int SprintId
        {
            get
            {
                return _sprintId;
            }

            set
            {
                if (_sprintId == value)
                {
                    return;
                }

                RaisePropertyChanging(SprintIdPropertyName);
                _sprintId = value;
                RaisePropertyChanged(SprintIdPropertyName);
            }
        }
        #endregion
      
        #region TeamId
        /// <summary>
        /// The <see cref="TeamId" /> property's name.
        /// </summary>
        public const string TeamIdPropertyName = "TeamId";

        private int _TeamId ;

        /// <summary>
        /// Sets and gets the Team property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int TeamId
        {
            get
            {
                return _TeamId;
            }

            set
            {
                if (_TeamId == value)
                {
                    return;
                }

                RaisePropertyChanging(TeamIdPropertyName);
                _TeamId = value;
                RaisePropertyChanged(TeamIdPropertyName);
            }
        }
        #endregion

        #region ProjectId
        /// <summary>
        /// The <see cref="ProjectId" /> property's name.
        /// </summary>
        public const string ProjectIdPropertyName = "ProjectId";

        private string _projectId = string.Empty;

        /// <summary>
        /// Sets and gets the ProjectId property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string ProjectId
        {
            get
            {
                return _projectId;
            }

            set
            {
                if (_projectId == value)
                {
                    return;
                }

                RaisePropertyChanging(ProjectIdPropertyName);
                _projectId = value;
                RaisePropertyChanged(ProjectIdPropertyName);
            }
        }
        #endregion
    
        #region SprintName
        /// <summary>
        /// The <see cref="SprintName" /> property's name.
        /// </summary>
        public const string SprintNamePropertyName = "SprintName";

        private string _sprintName = string.Empty;

        /// <summary>
        /// Sets and gets the SprintName property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string SprintName
        {
            get
            {
                return _sprintName;
            }

            set
            {
                if (_sprintName == value)
                {
                    return;
                }

                RaisePropertyChanging(SprintNamePropertyName);
                _sprintName = value;
                RaisePropertyChanged(SprintNamePropertyName);
            }
        }
        #endregion

        
        #region JIRAProject
        /// <summary>
        /// The <see cref="JIRAProject" /> property's name.
        /// </summary>
        public const string JIRAProjectPropertyName = "JIRAProject";

        private string _jiraProject = string.Empty;

        /// <summary>
        /// Sets and gets the JIRAProject property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string JIRAProject
        {
            get
            {
                return _jiraProject;
            }

            set
            {
                if (_jiraProject == value)
                {
                    return;
                }

                RaisePropertyChanging(JIRAProjectPropertyName);
                _jiraProject = value;
                RaisePropertyChanged(JIRAProjectPropertyName);
            }
        }
        #endregion
     
        #region JiraInstance
        /// <summary>
        /// The <see cref="JiraInstance" /> property's name.
        /// </summary>
        public const string JiraInstancePropertyName = "JiraInstance";

        private string _jiraInstance = string.Empty;

        /// <summary>
        /// Sets and gets the JiraInstance property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string JiraInstance
        {
            get
            {
                return _jiraInstance;
            }

            set
            {
                if (_jiraInstance == value)
                {
                    return;
                }

                RaisePropertyChanging(JiraInstancePropertyName);
                _jiraInstance = value;
                RaisePropertyChanged(JiraInstancePropertyName);
            }
        }
        #endregion
        
        #region JqlFilter
        /// <summary>
        /// The <see cref="JqlFilter" /> property's name.
        /// </summary>
        public const string JqlFilterPropertyName = "JqlFilter";

        private string _jqlFilter = string.Empty;

        /// <summary>
        /// Sets and gets the JqlFilter property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string JqlFilter
        {
            get
            {
                return _jqlFilter;
            }

            set
            {
                if (_jqlFilter == value)
                {
                    return;
                }

                RaisePropertyChanging(JqlFilterPropertyName);
                _jqlFilter = value;
                RaisePropertyChanged(JqlFilterPropertyName);
            }
        }
        #endregion
        
        #region OverWriteJQLFilter
        /// <summary>
        /// The <see cref="OverWriteJQLFilter" /> property's name.
        /// </summary>
        public const string OverWriteJQLFilterPropertyName = "OverWriteJQLFilter";

        private bool _overWriteJQLFilter = false;

        /// <summary>
        /// Sets and gets the OverWriteJQLFilter property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool OverWriteJQLFilter
        {
            get
            {
                return _overWriteJQLFilter;
            }

            set
            {
                if (_overWriteJQLFilter == value)
                {
                    return;
                }

                RaisePropertyChanging(OverWriteJQLFilterPropertyName);
                _overWriteJQLFilter = value;
                RaisePropertyChanged(OverWriteJQLFilterPropertyName);
            }
        }
        #endregion
        
        #region Authorization
        /// <summary>
        /// The <see cref="Authorization" /> property's name.
        /// </summary>
        public const string AuthorizationPropertyName = "Authorization";

        private string _authorization = string.Empty;

        /// <summary>
        /// Sets and gets the Authorization property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Authorization
        {
            get
            {
                return _authorization;
            }

            set
            {
                if (_authorization == value)
                {
                    return;
                }

                RaisePropertyChanging(AuthorizationPropertyName);
                _authorization = value;
                RaisePropertyChanged(AuthorizationPropertyName);
            }
        }
        #endregion
        
        #region JiraRapidView
        /// <summary>
        /// The <see cref="JiraRapidView" /> property's name.
        /// </summary>
        public const string JiraRapidViewPropertyName = "JiraRapidView";

        private string _jiraRapidView = String.Empty;

        /// <summary>
        /// Sets and gets the JiraRapidView property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string JiraRapidView
        {
            get
            {
                return _jiraRapidView;
            }

            set
            {
                if (_jiraRapidView == value)
                {
                    return;
                }

                RaisePropertyChanging(JiraRapidViewPropertyName);
                _jiraRapidView = value;
                RaisePropertyChanged(JiraRapidViewPropertyName);
            }
        }
        #endregion

        #region IDataErrorInfo
        /// <summary>
        /// Implementation of IDataErrorInfo
        /// </summary>
        /// <param name="columnName">The name of the property that is being validated</param>
        /// <returns>The last validation error</returns>
        public override string this[string columnName]
        {
            get
            {
                //Set the error message on Error property. 
                //This property is from IDataErrorInfo and will contain the last Error in any validation.
                Error = string.Empty;
                Errors.Remove(columnName);

                //Property name to validate
                if (columnName == GetPropertyName(() => SprintName))
                {
                    //Validate the property value
                    if (string.IsNullOrEmpty(SprintName))
                    {
                        Error = "Sprint Name cannot be left blank";
                    }
                }


             
                return Error;
            }
        }
        #endregion
      
    }
}
