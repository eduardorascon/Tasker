using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Threading;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
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
    public class IssueListViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        #region Fields

        readonly DispatcherTimer _oDistpachert = new DispatcherTimer();
        #endregion
        /// <summary>
        /// The <see cref="IssueItemList" /> property's name.
        /// </summary>
        public const string IssueItemListPropertyName = "IssueItemList";

        private ObservableCollection<IssueItem> _issueItemList;

        /// <summary>
        /// Sets and gets the TasksList property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<IssueItem> IssueItemList
        {
            get
            {
                return _issueItemList;
            }

            set
            {
                if (_issueItemList == value)
                {
                    return;
                }

                _issueItemList = value;
                RaisePropertyChanged(IssueItemListPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SelectedIssueItem" /> property's name.
        /// </summary>
        public const string SelectedIssueItemPropertyName = "SelectedIssueItem";

        private IssueItem _selectedIssueItem;

        /// <summary>
        /// Sets and gets the SelectedTask property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public IssueItem SelectedIssueItem
        {
            get
            {
                return _selectedIssueItem;
            }

            set
            {
                if (_selectedIssueItem == value)
                {
                    return;
                }

                _selectedIssueItem = value;
                RaisePropertyChanged(SelectedIssueItemPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="ShowEditTask" /> property's name.
        /// </summary>
        public const string ShowEditIssueItemPropertyName = "ShowEditIssueItem";

        private bool _showEditIssueItem;

        /// <summary>
        /// Sets and gets the SelectedTask property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool ShowEditTask
        {
            get
            {
                return _showEditIssueItem;
            }

            set
            {
                if (_showEditIssueItem == value)
                {
                    return;
                }

                _showEditIssueItem = value;
              //  IsEditTaskHidden = !_showEditTask;
                RaisePropertyChanged(ShowEditIssueItemPropertyName);
            }
        }
      


        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public IssueListViewModel(IDataService dataService)
        {
            DelegarComandos();
            _dataService = dataService;
            //! registrando para escuchar el mensaje
            Messenger.Default.Register<string>(this, "PRESS_KEY_ISSUE", ProcessMessenger);
          //  GetIssueItems();
      
        }
        
         void GetIssueItems()
        {
            TaskerHelper.SetStatusBarMessage("Getting Active Sprints in TASKER...");
            // Getting the Active Sprints
            _dataService.GetActiveSprint(ProcessSprints);  
        }

         async void ProcessSprints(IList<SprintItem> sprints, System.Exception error)
         {
             // Get the first item
             SprintItem oSprintItem = sprints[0];

             TaskerHelper.SetStatusBarMessage("Getting JIRA items...");

            // Executing the Query Asyncronouly
            List<IssueItem> oLista = await Task<List<IssueItem>>.Factory
            .StartNew(() => _dataService.GetIssues(oSprintItem));

            IssueItemList = new ObservableCollection<IssueItem>(oLista);

            TaskerHelper.SetStatusBarMessage("JIRA items updated");
        }

        private void ProcessMessenger(string messege)
        {

            switch (messege)
            {
                case "F2":
                      
                        break;
                case "Enter":
                      
                        Messenger.Default.Send("Salvar","TASK");
                        ShowEditTask = false;
                        GetIssueItems();
                        Messenger.Default.Send("", "UPDATE_STATISTICS");
                        break;
                case "Refresh":
                        ShowEditTask = false;
                        GetIssueItems();
                        break;
                case "Escape":
                        Messenger.Default.Send("Cancelar", "TASK");
                        ShowEditTask = false;
                        GetIssueItems();
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
            IssueItemList.Remove(SelectedIssueItem);
            ShowEditTask = false;
        }

        private void NewTask()
        {
            var newIssueItem = new IssueItem {IsNew = true};
            IssueItemList.Add(newIssueItem);
            // Select the new task
            SelectedIssueItem = newIssueItem;
            // Call the EditTask
          

        }

        public RelayCommand NewSubTaskCommand
        {
            get;
            private set;
        }
     
        #region Comandos
        void DelegarComandos()
        {
            NewSubTaskCommand = new RelayCommand(NewSubTask);
          
        }

        /// <summary>
        /// Create a New Sub Task (Daily Task)
        /// </summary>
        private void NewSubTask()
        {
            // Verificando que el tipo de sub tarea sea diferente de epic y Story
            if (SelectedIssueItem.IssueType != "Epic" && SelectedIssueItem.IssueType != "Story")
            {
                // Verificando que esa tarea no este creada para el dia.
                if (_dataService.CheckTodayIssueTask(SelectedIssueItem.IssueId.ToString(CultureInfo.InvariantCulture)))
                {
                    TaskerHelper.SetStatusBarMessage("A daily task for this Sprint Item is already created");
                }
                else
                {
                    var newTask = new TaskItem();
                    newTask.IsNew = true;
                    newTask.Title = SelectedIssueItem.Title;
                    newTask.Category = SelectedIssueItem.IssueType;
                    newTask.SprintItem = SelectedIssueItem.IssueId;
                    _dataService.SaveTask(newTask);
                    // refrescar la lista
                    Messenger.Default.Send<string>("Refresh", "PRESS_KEY_TASK");
                    TaskerHelper.SetStatusBarMessage("A daily task has been created");
                }
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