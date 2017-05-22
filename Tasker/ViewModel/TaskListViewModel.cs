using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Threading;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Media;
using Tasker.Classes;
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
    public class TaskListViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        #region Fields

        readonly DispatcherTimer _oDistpachert = new DispatcherTimer();
        #endregion

        /// <summary>
        /// The <see cref="TasksList" /> property's name.
        /// </summary>
        public const string TasksListPropertyName = "TasksList";

        private ObservableSortedList<TaskItem> _tasksList;

        /// <summary>
        /// Sets and gets the TasksList property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableSortedList<TaskItem> TasksList
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
        /// The <see cref="RunningTask" /> property's name.
        /// </summary>
        public const string RunningTaskPropertyName = "RunningTask";

        private TaskItem _runningTask = new TaskItem {Status = "DUMMY"};

        /// <summary>
        /// Sets and gets the RunningTask property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public TaskItem RunningTask
        {
            get
            {
                return _runningTask;
            }

            set
            {
                if (_runningTask == value)
                {
                    return;
                }

                _runningTask = value;
                RaisePropertyChanged(RunningTaskPropertyName);
            }
        }

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
                if (Equals(_backGroundColorBrush, value))
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
        public TaskListViewModel(IDataService dataService)
        {
            DelegarComandos();
            _dataService = dataService;
           //Cerrando las tareas del dia anterior
            _dataService.CloseTasks();
            //! registrando para escuchar el mensaje
           Messenger.Default.Register<string>(this, "PRESS_KEY_TASK", ProcessMessenger);
            Messenger.Default.Register <SolidColorBrush>(this, ChangeBackGroundColor);
            GetTasks();
            RunningTaskMonitor();
      
        }

        private void RunningTaskMonitor()
        {
           
            _oDistpachert.Interval = new TimeSpan(0, 1, 0);
            _oDistpachert.Tick += ODistpachertTick;
            _oDistpachert.Start();

        }

        void ODistpachertTick(object sender, EventArgs e)
        {
            if (RunningTask.Status == "DUMMY")
            {
                //Show the notification
                Messenger.Default.Send(true,"OVERLAY");
               // MessageBox.Show(Notificacion);
            }
        }

        private void ChangeBackGroundColor(SolidColorBrush messege)
        {
            BackGroundColorBrush = messege;
        }

        void GetTasks()
        {
          

            _dataService.GetTask(
               (tasks, error) =>
               {
                   if (error != null)
                   {
                       // Report error here
                       return;
                   }

                  TasksList = new ObservableSortedList<TaskItem>(tasks, new TaskItemComp());

               });


            UpdateSpendTime();

        }

        private void UpdateSpendTime()
        {
           
            Messenger.Default.Send(TasksList, "SPEND_TIME");
        }

        private void ProcessMessenger(string messege)
        {

            switch (messege)
            {
                case "F2":
                        EditTask();
                        break;
                case "Enter":
                case "Refresh":
                case "Close":
                    var isNew = SelectedTask.IsNew;
                   if(messege == "Enter")
                    Messenger.Default.Send("Salvar","TASK");

                   //Cerrando las tareas del dia anterior / si existen al momento de refrescar.
                    if (messege == "Close")
                    {
                        TaskerHelper.SetStatusBarMessage("Closing Yesterday Open Task");
                        _dataService.CloseTasks();
                        GetTasks();
                        isNew = false;
                    }

                    ShowEditTask = false;
                    if (isNew || messege == "Refresh")
                    {
                        // 
                        var oTasksList = new ObservableCollection<TaskItem>();
                        // Recuperar las tareas 
                        _dataService.GetTask(
                             (tasks, error) =>
                             {
                                 if (error != null)
                                 {
                                     // Report error here
                                     return;
                                 }
                                 oTasksList = new ObservableCollection<TaskItem>(tasks);
                             });
                        bool wasfound;
                        // Verificar si las tareas existen en la colleccion actual
                        foreach (var taskItem in oTasksList)
                        {
                            wasfound = false;
                            if (TasksList != null)
                            {
                                if (TasksList.Any(item => taskItem.TaskId == item.TaskId))
                                {
                                    wasfound = true;
                                }

                                if (!wasfound)
                                    TasksList.Add(taskItem);
                            }
                        }

                    }
                  
                    Messenger.Default.Send("", "UPDATE_STATISTICS");
                    UpdateSpendTime();
                    TaskerHelper.SetStatusBarMessage("");
                        break;
              
                case "Escape":
                        Messenger.Default.Send("Cancelar", "TASK");
                        ShowEditTask = false; 
                        if (SelectedTask.IsNew)
                            RemoveNewTask();
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
            var newTask = new TaskItem {IsNew = true, Status = "OPEN"};
            // Select the new task
            SelectedTask = newTask;
            // Call the EditTask
            EditTask();

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

        #region Comandos
        void DelegarComandos()
        {
            StartTaskCommand = new RelayCommand(StopAndStartTask);
            EditTaskCommand = new RelayCommand(EditTask);
        }

        private void EditTask()
        {
            if ((SelectedTask != null) && (SelectedTask.Status != "CLOSED" || SelectedTask.IsNew) && (!SelectedTask.IsRunning))
            {
                //Messenger.Default.Send<TaskView>(new TaskView());
                Messenger.Default.Send(SelectedTask);
                ShowEditTask = true;
            }
        }

        private void StopAndStartTask()
        {
           
            //Start or stops a tasks only if is not closed
            if (SelectedTask.Status != "CLOSED")
            {

                if (RunningTask.TaskId != SelectedTask.TaskId)
                {
                    StopTasks();
                    //Start running the task
                    SelectedTask.IsRunning = true;
                    SelectedTask.StartRunningDate = DateTime.Now;
                    RunningTask = null;
                    RunningTask = SelectedTask;
                    //Remove the overlay icon
                    Messenger.Default.Send(false, "OVERLAY");
                    //Send the taks running to the TaskRunViewModel
                    Messenger.Default.Send(RunningTask, "TASKRUNNING");
                    TaskerHelper.SetStatusBarMessage("A Task is Running");
                }
                else
                {
                    if (SelectedTask.IsRunning)
                    {
                        SelectedTask.EndRunningDate = DateTime.Now;
                        // Stop the current task
                        SelectedTask.IsRunning = false;
                        // Calculate the Task Time
                        UpdateSpendTime();
                        // Clear the Task Running
                        RunningTask = new TaskItem {Status = "DUMMY"};
                        TaskerHelper.SetStatusBarMessage("There is not Task Running");
                    }

                }
            }
            Messenger.Default.Send("", "UPDATE_STATISTICS");
        }

        private void StopTasks()
        {

            RunningTask.IsRunning = false;
            RunningTask.EndRunningDate = DateTime.Now;
            if (RunningTask.StartRunningDate != null)
            {// Calculate the Task Time
                UpdateSpendTime();
            }

        // Reset the DateTime Values
                RunningTask.StartRunningDate = null;
                RunningTask.EndRunningDate = null;
           
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