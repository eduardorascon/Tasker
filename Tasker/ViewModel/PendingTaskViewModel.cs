using System.Collections.ObjectModel;
using System.Windows.Media;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Command;
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
    public class PendingTaskViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        /// <summary>
        /// Initializes a new instance of the TaskModelView class.
        /// </summary>
        public PendingTaskViewModel(IDataService dataService)
        {
            _dataService = dataService;
            if (IsInDesignMode)
            {
                SelectedTask = Design.DesignDataService.CreatePendingTask(0);
            }

            StatusTaskCol = new ObservableCollection<string>();
            StatusTaskCol.Add("OPEN");
            StatusTaskCol.Add("COMPLETED");

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

            DelegarComandos();
            //! registrando para escuchar el mensaje
            Messenger.Default.Register<PendingTaskItem>(this, UpdateSelectedTask);
            Messenger.Default.Register<string>(this, "PENDING_TASK", ProcessMesseger); 
        }

     
        private void ProcessMesseger(string messege)
        {
            if (SelectedTask != null)
            {
                switch (messege)
                {
                    case "Salvar":
                        Messenger.Default.Send<string>("Collapse", "ALL_TASK");
                        _dataService.SavePendingTask(SelectedTask);
                        break;
                    case "Cancelar":
                        _dataService.UndoPendingTask(SelectedTask, _originalSelectedTask);
                        Messenger.Default.Send<string>("Collapse", "ALL_TASK");
                        if (SelectedTask.IsNew)
                        {
                            Messenger.Default.Send<string>("RemoveTask", "PENDING_TASK");
                        }
                        break;
                }
            }
        }

        private PendingTaskItem _originalSelectedTask = null;

        /// <summary>
        /// The <see cref="SelectedTask" /> property's name.
        /// </summary>
        public const string SelectedTaskPropertyName = "SelectedTask";

        private PendingTaskItem _SelectedTask = null;

        /// <summary>
        /// Sets and gets the SelectedTask property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public PendingTaskItem SelectedTask
        {
            get
            {
                return _SelectedTask;
            }

            set
            {
                if (_SelectedTask == value)
                {
                    return;
                }

                _SelectedTask = value;
                CopyTask();
                RaisePropertyChanged(SelectedTaskPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="StatusTaskCol" /> property's name.
        /// </summary>
        public const string StatusTaskColPropertyName = "StatusTaskCol";

        private ObservableCollection<string> _statusTaskCol = null;

        /// <summary>
        /// Sets and gets the ObservableCollection<string> property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<string> StatusTaskCol
        {
            get
            {
                return _statusTaskCol;
            }

            set
            {
                if (_statusTaskCol == value)
                {
                    return;
                }

                _statusTaskCol = value;
                RaisePropertyChanged(StatusTaskColPropertyName);
            }
        }

        private void CopyTask()
        {
            _originalSelectedTask = null;
            _originalSelectedTask = new PendingTaskItem();
            _originalSelectedTask.Title = _SelectedTask.Title;
            _originalSelectedTask.Category = _SelectedTask.Category;
            _originalSelectedTask.DueDate = _SelectedTask.DueDate;
            _originalSelectedTask.Ocurrence = _SelectedTask.Ocurrence;
            _originalSelectedTask.Risk = _SelectedTask.Risk;
            _originalSelectedTask.Problem100 = _SelectedTask.Problem100;
            _originalSelectedTask.Status = _SelectedTask.Status;
            _originalSelectedTask.CategoryColorBrush = _SelectedTask.CategoryColorBrush;
            _originalSelectedTask.CreatedDate = _SelectedTask.CreatedDate;
        }

        /// <summary>
        /// The <see cref="Categories" /> property's name.
        /// </summary>
        public const string CategoriesPropertyName = "Categories";

        private ObservableCollection<CategoryItem> _categories = null;

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
                    BrushConverter conv = new BrushConverter();
                    if (_selectedCategory != null)
                        SelectedTask.CategoryColorBrush = conv.ConvertFromString(_selectedCategory.Color) as SolidColorBrush;
                }
                RaisePropertyChanged(SelectedCategoryPropertyName);
            }
        }


        #region IsFocused
        /// <summary>
        /// The <see cref="IsFocused" /> property's name.
        /// </summary>
        public const string IsFocusedPropertyName = "IsFocused";

        private bool _isFocused;

        /// <summary>
        /// Sets and gets the IsFocused property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsFocused
        {
            get
            {
                return _isFocused;
            }

            set
            {
                _isFocused = value;
                RaisePropertyChanged(IsFocusedPropertyName);
            }
        }
        #endregion

        void UpdateSelectedTask(PendingTaskItem oPendingTaskItem)
        {
            IsFocused = false;
            SelectedTask = oPendingTaskItem;
            IsFocused = true;
        }


        #region Metodos
        private void NewTask()
        {
            TaskItem NewTask = new TaskItem();
            NewTask.IsNew = true;
            NewTask.Title = SelectedTask.Title;
            NewTask.Category = SelectedTask.Category;
            NewTask.PendingTaskId = SelectedTask.PendingTaskId;
            _dataService.SaveTask(NewTask);
            // refrescar la lista
            Messenger.Default.Send<string>("Refresh","PRESS_KEY_TASK");
        }
        #endregion

        #region Comandos
        public RelayCommand NewTaskCommand
        {
            get;
            private set;
        }
        #endregion

        void DelegarComandos()
        {
            NewTaskCommand = new RelayCommand(NewTask);
        }
    }
}