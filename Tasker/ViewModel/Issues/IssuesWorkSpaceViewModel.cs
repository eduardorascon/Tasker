using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Tasker.Model;

namespace Tasker.ViewModel
{
    /// <summary>
    /// A simple view model for configuring theme, font and accent colors.
    /// </summary>
    public class IssuesWorkSpaceViewModel
        : ViewModelBase
    {

        private readonly IDataService _dataService;
        public IssuesWorkSpaceViewModel(IDataService dataService)
        {
            _dataService = dataService;
           DelegarComandos();
        }

        # region Properties
       
        #region GlobalCategories
        /// <summary>
        /// The <see cref="GlobalCategories" /> property's name.
        /// </summary>
        public const string GlobalCategoriesPropertyName = "GlobalCategories";

        private ObservableCollection<GlobalCategoryItem> _globalCategory = new ObservableCollection<GlobalCategoryItem>();

        /// <summary>
        /// Sets and gets the GlobalCategories property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<GlobalCategoryItem> GlobalCategories
        {
            get
            {
                return _globalCategory;
            }

            set
            {
                if (_globalCategory == value)
                {
                    return;
                }

                RaisePropertyChanging(GlobalCategoriesPropertyName);
                _globalCategory = value;
                RaisePropertyChanged(GlobalCategoriesPropertyName);
            }
        }
        #endregion

        #region SelectedCategory
        /// <summary>
        /// The <see cref="SelectedCategory" /> property's name.
        /// </summary>
        public const string SelectedCategoryPropertyName = "SelectedCategory";

        private CategoryItem _selectedCategory;

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

                RaisePropertyChanging(SelectedCategoryPropertyName);
                _selectedCategory = value;
                RaisePropertyChanged(SelectedCategoryPropertyName);
            }
        }
        #endregion

        #region IsEditMode
        /// <summary>
        /// The <see cref="IsEditMode" /> property's name.
        /// </summary>
        public const string IsEditModePropertyName = "IsEditMode";

        private bool _isEditMode = false;

        /// <summary>
        /// Sets and gets the ShowDetail property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsEditMode
        {
            get
            {
                return _isEditMode;
            }

            set
            {
                if (_isEditMode == value)
                {
                    return;
                }

                RaisePropertyChanging(IsEditModePropertyName);
                _isEditMode = value;
                RaisePropertyChanged(IsEditModePropertyName);
            }
        }
        #endregion

        #endregion

        # region Commands
        public RelayCommand AddCategoryCommand
        {
            get;
            private set;
        }

        public RelayCommand SaveCategoryCommand
        {
            get;
            private set;
        }

        public RelayCommand ActivateCategoryCommand
        {
            get;
            private set;
        }

        public RelayCommand EditCategoryCommand
        {
            get;
            private set;
        }

        public RelayCommand CancelEditCategoryCommand
        {
            get;
            private set;
        }

        public RelayCommand RefreshIssuesCommand
        {
            get;
            private set;
        }
        #endregion

        #region Methods
        void DelegarComandos()
        {
            AddCategoryCommand = new RelayCommand(AddCategory);
            CancelEditCategoryCommand = new RelayCommand(CancelEditCategory);
            SaveCategoryCommand = new RelayCommand(SaveCategory);
            EditCategoryCommand = new RelayCommand(EditCategory);
            ActivateCategoryCommand = new RelayCommand(ActivateCategory);
            _dataService.GetGlobalCategory(AfterGetGlobalCategories);
            RefreshIssuesCommand = new RelayCommand(() => Messenger.Default.Send("Refresh", "PRESS_KEY_ISSUE"));

        }

        private void EditCategory()
        {
            IsEditMode = true;
        }

        private void CancelEditCategory()
        {
            Messenger.Default.Send("Cancelar", "CATEGORY");
            IsEditMode = false;
        }

        private void AfterGetGlobalCategories(IList<GlobalCategoryItem> globalCategories, Exception exception)
        {
            GlobalCategories = new ObservableCollection<GlobalCategoryItem>(globalCategories);
        }

        private void ActivateCategory()
        {
            Messenger.Default.Send("Activar", "CATEGORY");
        }

        private void SaveCategory()
        {
            Messenger.Default.Send("Salvar","CATEGORY");

        }

        private void AddCategory()
        {
           Messenger.Default.Send("Nuevo", "CATEGORY");
           IsEditMode = true;
        }
        #endregion

    }
}
