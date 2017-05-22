using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Tasker.Design;
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
    public class TaskViewModel : ViewModelBaseEx
    {
        private readonly IDataService _dataService;
       
        /// <summary>
        /// Initializes a new instance of the TaskModelView class.
        /// </summary>
        public TaskViewModel(IDataService dataService)
        {

            Contract.Requires(dataService == null);

            _dataService = dataService;
            if (IsInDesignMode)
            {
                SelectedTask = DesignDataService.CreateTask(0);
            }

            StatusTaskCol = new ObservableCollection<string>();
            StatusTaskCol.Add("OPEN");
            StatusTaskCol.Add("CLOSED");

            UpdatesCategories();
            
            //! registrando para escuchar el mensaje
            Messenger.Default.Register<TaskItem>(this, UpdateSelectedTask);
            Messenger.Default.Register<string>(this,"TASK", ProcessMesseger);
            // Escuchando el mensaje para actualizar las categorias de las tareas
            Messenger.Default.Register<string>(this,"UPDATE_CATEGORIES",
            (string str) => UpdatesCategories());

            ValidateCommand = new RelayCommand(Validate);
            NewCommand = new RelayCommand(New);
  
        }

        private void Validate()
        {
            throw new System.NotImplementedException();
        }

        #region Metodos

        private void New()
        {
            SelectedTask = new TaskItem();
          
        }

     


        private void ProcessMesseger(string messege)
        {
            if (SelectedTask != null)
            {
                switch (messege)
                {
                        //TODO: Agregar Mensajes retroalimentacion
                    case "Salvar":
                        Messenger.Default.Send("Collapse", "ALL_TASK");
                        _dataService.SaveTask(SelectedTask);
                        
                        break;
                    case "Cancelar":

                        _dataService.UndoTask(SelectedTask, _originalSelectedTask);
                        Messenger.Default.Send("Collapse", "ALL_TASK");
                        if (SelectedTask.IsNew)
                        {
                            Messenger.Default.Send("RemoveTask", "TASK");
                        }
                        break;
                }
            }
        }


        void UpdatesCategories()
        {
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

        }

        void UpdateSelectedTask(TaskItem oTaskItem)
        {
            IsFocused = false;
            SelectedTask = oTaskItem;
            IsFocused = true;


        }

    
        #endregion 

        #region Propiedades
       

        private TaskItem _originalSelectedTask;

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
                CopyTask();
                RaisePropertyChanged(SelectedTaskPropertyName);
            }
        }

        private void CopyTask()
        {
            _originalSelectedTask = null;
            _originalSelectedTask = new TaskItem
                {
                    Title = _selectedTask.Title,
                    Category = _selectedTask.Category,
                    CurrentTime = _selectedTask.CurrentTime,
                    Status = _selectedTask.Status,
                    CategoryColorBrush = _selectedTask.CategoryColorBrush,
                    CreatedDate = _selectedTask.CreatedDate
                };
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
                _categories = new ObservableCollection<CategoryItem>(_categories.Where(c => c.IsActive).ToList());
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

       
        #region ValidationErrorsString
        /// <summary>
        /// The <see cref="ValidationErrorsString" /> property's name.
        /// </summary>
        public const string ValidationErrorsStringPropertyName = "ValidationErrorsString";

        private string _validationErrorsString = string.Empty;

        /// <summary>
        /// Sets and gets the ValidationErrorsString property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string ValidationErrorsString
        {
            get
            {
                return _validationErrorsString;
            }

            set
            {
                if (_validationErrorsString == value)
                {
                    return;
                }

                RaisePropertyChanging(ValidationErrorsStringPropertyName);
                _validationErrorsString = value;
                RaisePropertyChanged(ValidationErrorsStringPropertyName);
            }
        }
        #endregion

        #region IsFocused
        /// <summary>
        /// The <see cref="IsFocused" /> property's name.
        /// </summary>
        public const string IsFocusedPropertyName = "IsFocused";

        private bool _isFocused ;

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

        #endregion

        #region Commandos
        public ICommand ValidateCommand { get; private set; }
        public ICommand NewCommand { get; private set; }
        #endregion

    }
}