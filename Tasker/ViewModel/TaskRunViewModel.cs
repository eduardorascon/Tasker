using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Tasker.Design;
using Tasker.Helpers;
using Tasker.Model;


namespace Tasker.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm/getstarted
    /// </para>
    /// </summary>
    public class TaskRunViewModel : ViewModelBaseEx
    {
        private readonly IDataService _dataService;
        readonly DispatcherTimer _timer = new DispatcherTimer();
        int _secondsToAdd = 0;
        private bool IsErrorMonitor = false;
       
        /// <summary>
        /// Initializes a new instance of the TaskModelView class.
        /// </summary>
        public TaskRunViewModel(IDataService dataService)
        {

            Contract.Requires(dataService == null);

            _dataService = dataService;

            if (IsInDesignMode)
            {
                SelectedTask = DesignDataService.CreateTask(0);
            }


            _dataService.GetCategory(
                (categories, error) =>
                {
                    if (error != null)
                    {
                        // Report error here
                        return;
                    }

                    Categories = new ObservableCollection<CategoryItem>(categories);
                });


            //! registrando para escuchar el mensaje
            Messenger.Default.Register<TaskItem>(this,"TASKRUNNING" ,UpdateSelectedTask);

            _timer.Tick += async delegate
            {

                if (!SelectedTask.IsRunning) return;

                SelectedTask.CurrentTime = SelectedTask.CurrentTime + 1;
                _secondsToAdd = _secondsToAdd + 1;

                // Checking is the current Task is Running
                if (_secondsToAdd % 10 != 0) return;

                // Update the time
                // Executing the Query Asyncronouly
                await Task.Factory.StartNew(() => _dataService.RecordTimeTask(SelectedTask, 0, (resul, exc) =>
                {
                    if (resul.HasError)
                    {
                        TaskerHelper.SetStatusBarMessage("You are working OFF-LINE");
                        IsErrorMonitor = true;
                    }
                    else
                    {
                        
                        if (IsErrorMonitor)
                        {
                            TaskerHelper.SetStatusBarMessage("");
                            IsErrorMonitor = false;
                        }
                        
                    }
                })
                    );

            };
           
        }

        #region Metodos

       
     void UpdateSelectedTask(TaskItem oTaskItem)
        {
            SelectedTask = oTaskItem;
        }

        void StartRecordingTime()
        {

            _secondsToAdd = 0;
            // Contando el reloj
            _timer.Stop();
            _timer.Interval = TimeSpan.FromSeconds(1.0);
            _timer.Start();

        }
    
        #endregion 

        #region Propiedades

        /// <summary>
        /// The <see cref="SelectedTask" /> property's name.
        /// </summary>
        public const string SelectedTaskPropertyName = "SelectedTask";

        private TaskItem _selectedTask;

        /// <summary>
        /// Sets and gets the SelectedTask property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public TaskItem SelectedTask
        {
            get
            {
                return _selectedTask;
            }

            set
            {
                if (_selectedTask == value)
                {
                    return;
                }

                _selectedTask = value;
                RaisePropertyChanged(SelectedTaskPropertyName);
                //Identificando si la tarea es la que esta corriendo.
                StartRecordingTime();
            }
        }

        /// <summary>
        /// The <see cref="Categories" /> property's name.
        /// </summary>
        public const string CategoriesPropertyName = "Categories";

        private ObservableCollection<CategoryItem> _categories;

        /// <summary>
        /// Sets and gets the Categories property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<CategoryItem> Categories
        {
            get
            {
                return _categories;
            }

            set
            {
                if (_categories == value)
                {
                    return;
                }

                _categories = value;
                RaisePropertyChanged(CategoriesPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SelectedCategory" /> property's name.
        /// </summary>
        public const string SelectedCategoryPropertyName = "SelectedCategory";

        private CategoryItem _selectedCategory = new CategoryItem();

        /// <summary>
        /// Sets and gets the SelectedCategory property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public CategoryItem SelectedCategory
        {
            get
            {
                return _selectedCategory;
            }

            set
            {
                if (_selectedCategory == value)
                {
                    return;
                }

                _selectedCategory = value;
             
                if (SelectedTask != null)
                {
                    var conv = new BrushConverter();
                    if (_selectedCategory != null)
                        SelectedTask.CategoryColorBrush = conv.ConvertFromString(_selectedCategory.Color) as SolidColorBrush;
                }
                RaisePropertyChanged(SelectedCategoryPropertyName);
            }
        }
        #endregion

        #region Commandos
        public ICommand ValidateCommand { get; private set; }
        public ICommand NewCommand { get; private set; }
        #endregion

    }
}