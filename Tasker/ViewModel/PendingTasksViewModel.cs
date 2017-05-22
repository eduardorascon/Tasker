using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Threading;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Media;
using Tasker.Helpers;
using Tasker.Model;

namespace Tasker.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm/getstarted
    /// </para>
    /// </summary>
    public class PendingTasksViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        #region Fields

        readonly DispatcherTimer _oDistpachert = new DispatcherTimer();
        #endregion

        /// <summary>
        /// The <see cref="TasksList" /> property's name.
        /// </summary>
        public const string TasksListPropertyName = "TasksList";

        private ObservableCollection<PendingTaskItem> _tasksList;

        /// <summary>
        /// Sets and gets the TasksList property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<PendingTaskItem> TasksList
        {
            get
            {
                return _tasksList;
            }

            set
            {
                if (_tasksList == value)
                {
                    return;
                }

                _tasksList = value;
                RaisePropertyChanged(TasksListPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SelectedTask" /> property's name.
        /// </summary>
        public const string SelectedTaskPropertyName = "SelectedTask";

        private PendingTaskItem _selectedTask;

        /// <summary>
        /// Sets and gets the SelectedTask property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public PendingTaskItem SelectedTask
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
            }
        }

        /// <summary>
        /// The <see cref="ShowEditTask" /> property's name.
        /// </summary>
        public const string ShowEditTaskPropertyName = "ShowEditTask";

        private bool _showEditTask;

        /// <summary>
        /// Sets and gets the SelectedTask property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool ShowEditTask
        {
            get
            {
                return _showEditTask;
            }

            set
            {
                if (_showEditTask == value)
                {
                    return;
                }

                _showEditTask = value;
                IsEditTaskHidden = !_showEditTask;
                RaisePropertyChanged(ShowEditTaskPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="IsEditTaskHidden" /> property's name.
        /// </summary>
        public const string IsEditTaskHiddenPropertyName = "IsEditTaskHidden";

        private bool _isEditTaskHidden = true;

        /// <summary>
        /// Sets and gets the SelectedTask property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsEditTaskHidden
        {
            get
            {
                return _isEditTaskHidden;
            }

            set
            {
                if (_isEditTaskHidden == value)
                {
                    return;
                }

                _isEditTaskHidden = value;
                RaisePropertyChanged(IsEditTaskHiddenPropertyName);
            }
        }


        /// <summary>
        /// The <see cref="BackGroundColorBrush" /> property's name.
        /// </summary>
        public const string BackGroundColorBrushPropertyName = "BackGroundColorBrush";

        private SolidColorBrush _backGroundColorBrush = new SolidColorBrush(Colors.LightGray);

        /// <summary>
        /// Sets and gets the CategoryColorBrush property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public SolidColorBrush BackGroundColorBrush
        {
            get
            {
                return _backGroundColorBrush;
            }

            set
            {
                if (!(_backGroundColorBrush != value))
                {
                    return;
                }

                _backGroundColorBrush = value;
                RaisePropertyChanged(BackGroundColorBrushPropertyName);
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public PendingTasksViewModel(IDataService dataService)
        {
            _showEditTask = false;
          
            _dataService = dataService;
           // StartTaskCommand = startTaskCommand;


            //! registrando para escuchar el mensaje
            Messenger.Default.Register<string>(this,"PRESS_KEY_PENDING_TASK",ProcessMessenger);
            Messenger.Default.Register <SolidColorBrush>(this, ChangeBackGroundColor);
            GetPendingTasks();
                  DelegarComandos();

        }

    
        private void ChangeBackGroundColor(SolidColorBrush messege)
        {
            BackGroundColorBrush = messege;
        }

        void GetPendingTasks()
        {
          

            _dataService.GetPendingTask(
               (tasks, error) =>
               {
                   if (error != null)
                   {
                       // Report error here
                       return;
                   }

                   TasksList = new ObservableCollection<PendingTaskItem>(tasks);
               });
        }

        private void ProcessMessenger(string messege)
        {

            switch (messege)
            {
                case "F2":
                        EditPendingTask();
                        break;
                case "Enter":
                        Messenger.Default.Send("Salvar","PENDING_TASK");
                        ShowEditTask = false;
                        GetPendingTasks();
                        break;
                case "Escape":
                        Messenger.Default.Send("Cancelar", "PENDING_TASK");
                        ShowEditTask = false;
                        GetPendingTasks();
                        break;
                case "New":
                    NewTask();
                        break;
                case "RemoveTask":
                        RemoveNewTask();
                        break;
            }
            
            
            
            
        }

        private void RemoveNewTask()
        {
            TasksList.Remove(SelectedTask);
            ShowEditTask = false;
        }

        private void NewTask()
        {
            var newTask = new PendingTaskItem();
            newTask.IsNew = true;
            TasksList.Add(newTask);
            // Select the new task
            SelectedTask = newTask;
            // Call the EditTask
            EditPendingTask();

        }


        /// <summary>
        /// Create a New Sub Task (Daily Task)
        /// </summary>
        private void NewSubTask()
        {
            // Verificando que esa tarea no este creada para el dia.
            if (_dataService.CheckTodayPendingTask(SelectedTask.PendingTaskId.ToString(CultureInfo.InvariantCulture)))
            {
                TaskerHelper.SetStatusBarMessage("A daily task for this Item is already created");
            }
            else
            {
                var newTask = new TaskItem();
                newTask.IsNew = true;
                newTask.Title = SelectedTask.Title;
                newTask.Category = SelectedTask.Category;
                newTask.PendingTaskId = SelectedTask.PendingTaskId;
                _dataService.SaveTask(newTask);
                // refrescar la lista
                Messenger.Default.Send<string>("Refresh", "PRESS_KEY_TASK");
                TaskerHelper.SetStatusBarMessage("A daily task has been created");
            }
           
        }


         public RelayCommand StartTaskCommand
        {
            get;
            private set;
        }

         public RelayCommand EditTaskCommand
         {
             get;
             private set;
         }

         public RelayCommand NewSubTaskCommand
         {
             get;
             private set;
         }

        #region Comandos
        void DelegarComandos()
        {
            EditTaskCommand = new RelayCommand(EditPendingTask);
            NewSubTaskCommand = new RelayCommand(NewSubTask);

        }

        private void EditPendingTask()
        {
            if (SelectedTask != null)
            {
                //Messenger.Default.Send<PendingTaskView>(new PendingTaskView());
                Messenger.Default.Send(SelectedTask);
                ShowEditTask = true;
            }
        }

      

  
        #endregion
        public override void Cleanup()
        {
            // Clean up if needed
            _oDistpachert.Stop();
            base.Cleanup();
        }
    }
}