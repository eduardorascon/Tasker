using System;
using System.Windows.Media;
using GalaSoft.MvvmLight;
using Tasker.Helpers;

namespace Tasker.Model
{
    public class PendingTaskItem:ObservableObject
    {
        #region Fields

        readonly Functions _oFx = new Functions();
        #endregion
        public PendingTaskItem(int pendingTaskId,string category, string title, DateTime createdDate, DateTime dueDate, string status, int ocurrence, int risk, int estimatedTime, int currentTime)
        {
            PendingTaskId = pendingTaskId;
            Title = title;
            Category = category;
            CreatedDate = createdDate;
            Status = status;
            DueDate = dueDate;
            Ocurrence = ocurrence;
            Risk = risk;
            EstimatedTime = estimatedTime;
            CurrentTime = currentTime;
        }

        public PendingTaskItem()
        {
            
        }

        /// <summary>
        /// The <see cref="PendingTaskId" /> property's name.
        /// </summary>
        public const string PendingTaskIdPropertyName = "PendingTaskId";

        private int _pendingTaskId;

        /// <summary>
        /// Sets and gets the TaskId property.
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
                RaisePropertyChanged(CreatedDatePropertyName);
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
        /// The <see cref="IsNew" /> property's name.
        /// </summary>
        public const string IsNewPropertyName = "IsNew";

        private bool _isNew;

        /// <summary>
        /// Sets and gets the IsNew property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsNew
        {
            get
            {
                return _isNew;
            }

            set
            {
                if (_isNew == value)
                {
                    return;
                }

                _isNew = value;
                RaisePropertyChanged(IsNewPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="DueDate" /> property's name.
        /// </summary>
        public const string DueDatePropertyName = "DueDate";

        private DateTime? _dueDate;

        /// <summary>
        /// Sets and gets the DueDate property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public DateTime? DueDate
        {
            get
            {
                return _dueDate;
            }

            set
            {
                if (_dueDate == value)
                {
                    return;
                }

                _dueDate = value;
                RaisePropertyChanged(DueDatePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="DueDate" /> property's name.
        /// </summary>
        public const string OcurrencePropertyName = "Ocurrence";

        private int _ocurrence;

        /// <summary>
        /// Sets and gets the DueDate property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int Ocurrence
        {
            get
            {
                return _ocurrence;
            }

            set
            {
                if (_ocurrence == value)
                {
                    return;
                }

                _ocurrence = value;
                Problem100 = (_ocurrence*_risk);
                RaisePropertyChanged(OcurrencePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Risk" /> property's name.
        /// </summary>
        public const string RiskPropertyName = "Risk";

        private int _risk;

        /// <summary>
        /// Sets and gets the DueDate property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int Risk
        {
            get
            {
                return _risk;
            }

            set
            {
                if (_risk == value)
                {
                    return;
                }

                _risk = value;
                Problem100 = (_ocurrence * _risk);
                RaisePropertyChanged(RiskPropertyName);
            }
        }


        /// <summary>
        /// The <see cref="Risk" /> property's name.
        /// </summary>
        public const string EstimatedTimePropertyName = "EstimatedTime";
        private int _estimatedTime;

        /// <summary>
        /// Sets and gets the DueDate property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int EstimatedTime
        {
            get
            {
                return _estimatedTime;
            }

            set
            {
                if (_estimatedTime == value)
                {
                    return;
                }

                _estimatedTime = value;

                RaisePropertyChanged(EstimatedTimePropertyName);
                if (EstimatedTime > 0)
                    Completed = ((double)(_currentTime) / (double)EstimatedTime) * 100;
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
                RaisePropertyChanged(CurrentTimePropertyName);
                if (EstimatedTime > 0)
                    Completed = ((double)(_currentTime) / (double)EstimatedTime)*100;
            }
        }

        /// <summary>
        /// The <see cref="Problem100" /> property's name.
        /// </summary>
        public const string Problem100PropertyName = "Problem100";

        private int _problem100;

        /// <summary>
        /// Sets and gets the DueDate property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int Problem100
        {
            get
            {
                return _problem100;
            }

            set
            {
                if (_problem100 == value)
                {
                    return;
                }

                _problem100 = value;
                RaisePropertyChanged(Problem100PropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Completed" /> property's name.
        /// </summary>
        public const string CompletedropertyName = "Completed";

        private double _completed;

        /// <summary>
        /// Sets and gets the DueDate property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public double Completed
        {
            get
            {
                return  Math.Round(_completed,2);
            }

            set
            {
                if (_completed == value)
                {
                    return;
                }

                _completed = value;
                RaisePropertyChanged(CompletedropertyName);
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

    }
}
