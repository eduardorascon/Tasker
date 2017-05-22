using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Tasker.Classes;
using Tasker.Helpers;
using Tasker.Model;

namespace Tasker.ViewModel
{
    /// <summary>
    /// A simple view model for configuring theme, font and accent colors.
    /// </summary>
    public class CategoryViewModel
        : ViewModelBase
    {

        private readonly IDataService _dataService;
        private CategoryItem originalSelectedCategory = new CategoryItem();
        public CategoryViewModel(IDataService dataService)
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
                AutomapperTypeAdapter.SetEntityODataObjectValue(_selectedCategory, originalSelectedCategory, false);
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

        public RelayCommand RemoveCategoryCommand
        {
            get;
            private set;
        }

        public RelayCommand ActivateCategoryCommand
        {
            get;
            private set;
        }
        #endregion

        #region Methods
        void DelegarComandos()
        {
           
            _dataService.GetGlobalCategory(AfterGetGlobalCategories);
            // Listen the Messages
            Messenger.Default.Register<CategoryItem>(this, UpdateSelectedCategory);
            Messenger.Default.Register<string>(this, "CATEGORY", ProcessMesseger);
        }

        private void UpdateSelectedCategory(CategoryItem selecCategory)
        {
            SelectedCategory = selecCategory;
        }

        private void ProcessMesseger(string function)
        {
            if (SelectedCategory != null)
            {
                switch (function)
                {

                    case "Activar":
                    case "Salvar" :

                        if (function == "Activar")
                        {
                            SelectedCategory.IsActive = !SelectedCategory.IsActive;
                        }

                         _dataService.SaveCategory(SelectedCategory, (resulDTO, exception) =>
                                                                     {
                                                                         if (!resulDTO.HasError)
                                                                         {

                                                                             Messenger.Default.Send("Collapse",
                                                                               "CATEGORY_EDIT_MODE");                                                                             
                                                                                
                                                                                 Messenger.Default.Send("Update", "UPDATE_CATEGORIES");

                                                                         }
                                                                            TaskerHelper.SetStatusBarMessage(
                                                                                 resulDTO.Message);
                                                                     });

                        if (function == "Activar")
                        {
                            TaskerHelper.SetStatusBarMessage(
                                SelectedCategory.IsActive ? "Category Active" : "Category Inactive");
                        }

                        break;
                    case "Cancelar":
                        AutomapperTypeAdapter.SetEntityODataObjectValue(originalSelectedCategory, SelectedCategory,false);
                       if (SelectedCategory.IsNew)
                       {
                           Messenger.Default.Send("RemoveNewCategory", "CATEGORY");
                       }
                       Messenger.Default.Send("Collapse", "CATEGORY_EDIT_MODE");
                        break;
                }
                
            }
        }

        private void AfterGetGlobalCategories(IList<GlobalCategoryItem> globalCategories, Exception exception)
        {
            GlobalCategories = new ObservableCollection<GlobalCategoryItem>(globalCategories);
        }

      
        #endregion





    }
}
