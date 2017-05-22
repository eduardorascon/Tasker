using System;
using System.ComponentModel;
using System.Windows.Media;
using GalaSoft.MvvmLight;
using Tasker.Helpers;

namespace Tasker.Model
{
    public class TaskItem : ModelBaseEx
    {
        #region Fields
        Functions oFx = new Functions();
        #endregion
        public TaskItem(int taskId,string category, string title, DateTime createdDate, int currenttime, string status, string stringTime, string stringDate,string sprintItem)
        {
            TaskId = taskId;
            Title = title;
            Category = category;
            CreatedDate = createdDate;
            // Se actualiza al momento de actualizar las hrs, mnts, segnds
            //  CurrentTime = currenttime;
            Status = status;
            StringTime = stringTime;
            StringDate = stringDate;
            SprintItem = sprintItem;


            UpdateTime(currenttime);
        }

        private void UpdateTime(int currenttime)
        {
            int minutes = (int) (currenttime/60); //take integral part
            int seconds = (currenttime - (minutes*60)); //add if you want seconds
            int hours = (int) (minutes/60); //take integral part
            minutes = (int) (minutes - (hours*60)); //multiply fractional part with 60

            StringHour = hours;
            StringMinute = minutes;
            StringSecond = seconds;
        }

        public TaskItem()
        {
            
        }

        /// <summary>
        /// The <see cref="TaskId" /> property's name.
        /// </summary>
        public const string TaskIdPropertyName = "TaskId";

        private int _taskId = 9999999;

        /// <summary>
        /// Sets and gets the TaskId property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int TaskId
        {
            get
            {
                return _taskId;
            }

            set
            {
                if (_taskId == value)
                {
                    return;
                }

                _taskId = value;
                RaisePropertyChanged(TaskIdPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Title" /> property's name.
        /// </summary>
        public const string TitlePropertyName = "Title";

        private string _title = string.Empty;

        /// <summary>
        /// Sets and gets the Title property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Title
        {
            get
            {
                return _title;
            }

            set
            {
                if (_title == value)
                {
                    return;
                }

                _title = value;
                RaisePropertyChanged(TitlePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Category" /> property's name.
        /// </summary>
        public const string CategoryPropertyName = "Category";

        private string _category = string.Empty;

        /// <summary>
        /// Sets and gets the Category property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Category
        {
            get
            {
                return _category;
            }

            set
            {
                if (_category == value)
                {
                    return;
                }

                _category = value;
                RaisePropertyChanged(CategoryPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="CreatedDate" /> property's name.
        /// </summary>
        public const string CreatedDatePropertyName = "CreatedDate";

        private DateTime _createdDate = DateTime.Now;

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

                _createdDate = value;
                StringDate = oFx.MakeDateString(_createdDate);
                RaisePropertyChanged(CreatedDatePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="CurrentTime" /> property's name.
        /// </summary>
        public const string CurrentTimePropertyName = "CurrentTime";

        private int _currentTime = 0;

        /// <summary>
        /// Sets and gets the CurrentTime property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int CurrentTime
        {
            get
            {
                return _currentTime;
            }

            set
            {
                if (_currentTime == value)
                {
                    return;
                }

                _currentTime = value;
                StringTime = oFx.MakeTimeString(_currentTime);
                UpdateTime(_currentTime);
                RaisePropertyChanged(CurrentTimePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Status" /> property's name.
        /// </summary>
        public const string StatusPropertyName = "Status";

        private string _status = string.Empty;

        /// <summary>
        /// Sets and gets the Status property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Status
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

                _status = value;
                RaisePropertyChanged(StatusPropertyName);
            }
        }



        /// <summary>
        /// The <see cref="StringTime" /> property's name.
        /// </summary>
        public const string StringTimePropertyName = "StringTime";

        private string _stringTime = string.Empty;

        /// <summary>
        /// Sets and gets the StringTime property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string StringTime
        {
            get
            {
                return _stringTime;
            }

            set
            {
                if (_stringTime == value)
                {
                    return;
                }

                _stringTime = value;
                RaisePropertyChanged(StringTimePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="StringDate" /> property's name.
        /// </summary>
        public const string StringDatePropertyName = "StringDate";

        private string _stringDate = string.Empty;

        /// <summary>
        /// Sets and gets the StringDate property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string StringDate
        {
            get
            {
                return _stringDate;
            }

            set
            {
                if (_stringDate == value)
                {
                    return;
                }

                _stringDate = value;
                RaisePropertyChanged(StringDatePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="IsRunning" /> property's name.
        /// </summary>
        public const string IsRunningPropertyName = "IsRunning";

        private bool _isRunning = false;

        /// <summary>
        /// Sets and gets the IsRunning property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsRunning
        {
            get
            {
                return _isRunning;
            }

            set
            {
                if (_isRunning == value)
                {
                    return;
                }

                _isRunning = value;
                RaisePropertyChanged(IsRunningPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="StartRunningDate" /> property's name.
        /// </summary>
        public const string StartRunningDatePropertyName = "StartRunningDate";

        private DateTime? _startRunningDate;

        /// <summary>
        /// Sets and gets the StartRunningDate property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public DateTime? StartRunningDate
        {
            get
            {
                return _startRunningDate;
            }

            set
            {
                if (_startRunningDate == value)
                {
                    return;
                }

                _startRunningDate = value;
                RaisePropertyChanged(StartRunningDatePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="EndRunningDate" /> property's name.
        /// </summary>
        public const string EndRunningDatePropertyName = "EndRunningDate";

        private DateTime? _endRunningDate;

        /// <summary>
        /// Sets and gets the EndRunningDate property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public DateTime? EndRunningDate
        {
            get
            {
                return _endRunningDate;
            }

            set
            {
                if (_endRunningDate == value)
                {
                    return;
                }

                _endRunningDate = value;
                RaisePropertyChanged(EndRunningDatePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="CategoryColorBrush" /> property's name.
        /// </summary>
        public const string CategoryColorBrushPropertyName = "CategoryColorBrush";

        private SolidColorBrush _categoryColorBrush = new SolidColorBrush();

        /// <summary>
        /// Sets and gets the CategoryColorBrush property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public SolidColorBrush CategoryColorBrush
        {
            get
            {
                return _categoryColorBrush;
            }

            set
            {
                if (_categoryColorBrush == value)
                {
                    return;
                }

                _categoryColorBrush = value;
                RaisePropertyChanged(CategoryColorBrushPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="PendingTaskId" /> property's name.
        /// </summary>
        public const string PendingTaskIdPropertyName = "PendingTaskId";

        private int _pendingTaskId;

        /// <summary>
        /// Sets and gets the PendingTaskId property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int PendingTaskId
        {
            get
            {
                return _pendingTaskId;
            }

            set
            {
                if (_pendingTaskId == value)
                {
                    return;
                }

                _pendingTaskId = value;
                RaisePropertyChanged(PendingTaskIdPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="StringHour" /> property's name.
        /// </summary>
        public const string StringHourPropertyName = "StringHour";

        private int _stringHour = 0;

        /// <summary>
        /// Sets and gets the TaskId property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int StringHour
        {
            get
            {
                return _stringHour;
            }

            set
            {
                if (_stringHour == value)
                {
                    return;
                }

                _stringHour = value;
                ConvertToSeconds();
                RaisePropertyChanged(StringHourPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="StringMinute" /> property's name.
        /// </summary>
        public const string StringMinutePropertyName = "StringMinute";

        private int _stringMinute = 0;

        /// <summary>
        /// Sets and gets the TaskId property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int StringMinute
        {
            get
            {
                return _stringMinute;
            }

            set
            {
                if (_stringMinute == value)
                {
                    return;
                }

                _stringMinute = value;
                ConvertToSeconds();
                RaisePropertyChanged(StringMinutePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="StringSecond" /> property's name.
        /// </summary>
        public const string StringSecondPropertyName = "StringSecond";

        private int _stringSecond = 0;

        /// <summary>
        /// Sets and gets the TaskId property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int StringSecond
        {
            get
            {
                return _stringSecond;
            }

            set
            {
                if (_stringSecond == value)
                {
                    return;
                }

                _stringSecond = value;
                ConvertToSeconds();
                RaisePropertyChanged(StringSecondPropertyName);
            }
        }
        
        #region SprintItem
        /// <summary>
        /// The <see cref="SprintItem" /> property's name.
        /// </summary>
        public const string SprintItemPropertyName = "SprintItem";

        private string _sprintItem = string.Empty;

        /// <summary>
        /// Sets and gets the SprintItem property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string SprintItem
        {
            get
            {
                return _sprintItem;
            }

            set
            {
                if (_sprintItem == value)
                {
                    return;
                }

                RaisePropertyChanging(SprintItemPropertyName);
                _sprintItem = value;
                RaisePropertyChanged(SprintItemPropertyName);
            }
        }
        #endregion

        private void ConvertToSeconds()
        {
            int nSeconds = ((StringHour*60)*60) + (StringMinute*60) + StringSecond;
            CurrentTime = nSeconds;
        }

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
                this.Errors.Remove(columnName);

                //Property name to validate
                if (columnName == GetPropertyName<string>(() => Title))
                {
                    //Validate the property value
                    if (string.IsNullOrEmpty(Title))
                    {
                        Error = "Title cannot be left blank";
                    }
                }


                //Property name to validate
                if (columnName == GetPropertyName<string>(() => Category))
                {
                    //Validate the property value
                    if (string.IsNullOrEmpty(Title))
                    {
                        Error = "Category cannot be left blank";
                    }
                }


                //This stays as it is 
                if (!string.IsNullOrEmpty(Error))
                {
                    this.Errors.Add(columnName, Error);
                }
                return Error;
            }
        }
        #endregion
      
    }
}
