using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Tasker.Classes;
using Tasker.Model;

namespace Tasker.ViewModel
{
    /// <summary>
    /// A simple view model for configuring theme, font and accent colors.
    /// </summary>
    public class CategoriesViewModel
        : ViewModelBase
    {

        private readonly IDataService _dataService;
        public CategoriesViewModel(IDataService dataService)
        {
            _dataService = dataService;
           DelegarComandos();
        }

        # region Properties

        #region Categories
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

                RaisePropertyChanging(CategoriesPropertyName);
                _categories = value;
                RaisePropertyChanged(CategoriesPropertyName);
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
                Messenger.Default.Send<CategoryItem>(_selectedCategory);
            }
        }
        #endregion

        #endregion

        #region Methods
        void DelegarComandos()
        {
            // Inicializar Datos.
            _dataService.GetCategory(AfterGetCategories); 
            // Listen the Messenger
            Messenger.Default.Register<string>(this, "CATEGORY", ProcessMesseger);
        }

        private void ProcessMesseger(string function)
        {
  
                switch (function)
                {
                    case "Nuevo":
                        // Adding the new category to the list.
                        // and set the flag IsNew
                        var oNewCategory = new CategoryItem {IsNew = true};
                        Categories.Add(oNewCategory);
                        SelectedCategory = oNewCategory;
                        break;
                    case "RemoveNewCategory":
                      // Remove the new category cause never should be stored in the database
                        Categories.Remove(SelectedCategory);
                        break;
                }

        }

        private void AfterGetCategories(IList<CategoryItem> categories, Exception exception)
        {
            Categories = new ObservableCollection<CategoryItem>(categories);
        }
        #endregion





    }
}
