using System.Windows.Media;
using GalaSoft.MvvmLight;

namespace Tasker.Model
{
    public class CategoryItem : GlobalCategoryItem
    {
        public CategoryItem(string categoryId, string description, string color, string globalCategoryId, bool isActive)
        {
            CategoryId = categoryId;
            GlobalCategoryId = globalCategoryId;
            Description = description;
            Color = color;
            BrushConverter conv = new BrushConverter();
            CategoryColorBrush = conv.ConvertFromString(Color) as SolidColorBrush;
            IsActive = isActive;
        }

        public CategoryItem()
        {
            
        }

        /// <summary>
        /// The <see cref="CategoryId" /> property's name.
        /// </summary>
        public const string CategoryIdPropertyName = "CategoryId";

        private string _CategoryId = string.Empty;

        /// <summary>
        /// Sets and gets the CategoryId property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string CategoryId
        {
            get
            {
                return _CategoryId;
            }

            set
            {
                if (_CategoryId == value)
                {
                    return;
                }

                _CategoryId = value;
                RaisePropertyChanged(CategoryIdPropertyName);
            }
        }
        
        #region IsActive
        /// <summary>
        /// The <see cref="IsActive" /> property's name.
        /// </summary>
        public const string IsActivePropertyName = "IsActive";

        private bool _isActive = true;

        /// <summary>
        /// Sets and gets the IsActive property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsActive
        {
            get
            {
                return _isActive;
            }

            set
            {
                if (_isActive == value)
                {
                    return;
                }

                RaisePropertyChanging(IsActivePropertyName);
                _isActive = value;
                RaisePropertyChanged(IsActivePropertyName);
            }
        }
        #endregion
    }
}
