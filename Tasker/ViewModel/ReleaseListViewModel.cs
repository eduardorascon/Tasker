using System.Collections.ObjectModel;
using System.Windows.Threading;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
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
    public class ReleaseListViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        #region Fields

        readonly DispatcherTimer _oDistpachert = new DispatcherTimer();
        #endregion
        /// <summary>
        /// The <see cref="ReleaseItemList" /> property's name.
        /// </summary>
        public const string ReleaseItemListPropertyName = "ReleaseItemList";

        private ObservableCollection<ReleaseItem> _releaseItemList;

        /// <summary>
        /// Sets and gets the TasksList property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<ReleaseItem> ReleaseItemList
        {
            get
            {
                return _releaseItemList;
            }

            set
            {
                if (_releaseItemList == value)
                {
                    return;
                }

                _releaseItemList = value;
                RaisePropertyChanged(ReleaseItemListPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SelectedReleaseItem" /> property's name.
        /// </summary>
        public const string SelectedReleaseItemPropertyName = "SelectedReleaseItem";

        private ReleaseItem _selectedReleaseItem;

        /// <summary>
        /// Sets and gets the SelectedTask property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ReleaseItem SelectedReleaseItem
        {
            get
            {
                return _selectedReleaseItem;
            }

            set
            {
                if (_selectedReleaseItem == value)
                {
                    return;
                }

                _selectedReleaseItem = value;
                RaisePropertyChanged(SelectedReleaseItemPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="ShowEditTask" /> property's name.
        /// </summary>
        public const string ShowEditReleaseItemPropertyName = "ShowEditReleaseItem";

        private bool _showEditReleaseItem;

        /// <summary>
        /// Sets and gets the SelectedTask property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool ShowEditTask
        {
            get
            {
                return _showEditReleaseItem;
            }

            set
            {
                if (_showEditReleaseItem == value)
                {
                    return;
                }

                _showEditReleaseItem = value;
              //  IsEditTaskHidden = !_showEditTask;
                RaisePropertyChanged(ShowEditReleaseItemPropertyName);
            }
        }
      

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public ReleaseListViewModel(IDataService dataService)
        {
            DelegarComandos();
            _dataService = dataService;
           //Cerrando las tareas del dia anterior
            _dataService.CloseTasks();
            //! registrando para escuchar el mensaje
            Messenger.Default.Register<string>(this, "PRESS_KEY_RELEASE", ProcessMessenger);
            GetReleaseItems();
      
        }



        void GetReleaseItems()
        {
          

            _dataService.GetReleaseItems(
               (releaseItems, error) =>
               {
                   if (error != null)
                   {
                       // Report error here
                   }

                   ReleaseItemList = new ObservableCollection<ReleaseItem>(releaseItems);

               });

            foreach (ReleaseItem releaseItem in ReleaseItemList)
            {
             
            }

           
        }

        private void ProcessMessenger(string messege)
        {

            switch (messege)
            {
                case "F2":
                        EditTask();
                        break;
                case "Enter":
                      
                        Messenger.Default.Send("Salvar","TASK");
                        ShowEditTask = false;
                        GetReleaseItems();
                        Messenger.Default.Send("", "UPDATE_STATISTICS");
                        break;
                case "Refresh":
                        ShowEditTask = false;
                        GetReleaseItems();
                        break;
                case "Escape":
                        Messenger.Default.Send("Cancelar", "TASK");
                        ShowEditTask = false;
                        GetReleaseItems();
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
            ReleaseItemList.Remove(SelectedReleaseItem);
            ShowEditTask = false;
        }

        private void NewTask()
        {
            var newReleaseItem = new ReleaseItem {IsNew = true};
            ReleaseItemList.Add(newReleaseItem);
            // Select the new task
            SelectedReleaseItem = newReleaseItem;
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
            if ((SelectedReleaseItem != null) && SelectedReleaseItem.Status != "CLOSED" )
            {
                Messenger.Default.Send(SelectedReleaseItem);
                ShowEditTask = true;
            }
        }

        private void StopAndStartTask()
        {
          
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