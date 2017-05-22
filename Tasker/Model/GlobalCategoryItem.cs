using System.Windows.Media;
using GalaSoft.MvvmLight;

namespace Tasker.Model
{
    public class GlobalCategoryItem: ObservableObject
    {
        public GlobalCategoryItem(string categoryId, string description, string color)
        {
            GlobalCategoryId = categoryId;
            Description = description;
            Color = color;
            BrushConverter conv = new BrushConverter();
            CategoryColorBrush = conv.ConvertFromString(Color) as SolidColorBrush; ;
        }

        public  GlobalCategoryItem()
         {
             
         }
        /// <summary>
        /// The <see cref="CategoryId" /> property's name.
        /// </summary>
        public const string GlobalCategoryIdPropertyName = "GlobalCategoryId";

        private string  _globalCategoryId = string.Empty;

        /// <summary>
        /// Sets and gets the CategoryId property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string  GlobalCategoryId
        {
            get
            {
                return _globalCategoryId;
            }

            set
            {
                if (_globalCategoryId == value)
                {
                    return;
                }

                _globalCategoryId = value;
                RaisePropertyChanged(GlobalCategoryIdPropertyName);
            }
        }


        /// <summary>
        /// The <see cref="Description" /> property's name.
        /// </summary>
        public const string DescriptionPropertyName = "Description";

        private string _descripcion = string.Empty;

        /// <summary>
        /// Sets and gets the Description property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Description
        {
            get
            {
                return _descripcion;
            }

            set
            {
                if (_descripcion == value)
                {
                    return;
                }

                _descripcion = value;
                RaisePropertyChanged(DescriptionPropertyName);
            }
        }


        /// <summary>
        /// The <see cref="Color" /> property's name.
        /// </summary>
        public const string ColorPropertyName = "Color";

        private string _color = "Blue";

        /// <summary>
        /// Sets and gets the Color property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Color
        {
            get
            {
                return _color;
            }

            set
            {
                if (_color == value)
                {
                    return;
                }

                _color = value;
                RaisePropertyChanged(ColorPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="IsNew" /> property's name.
        /// </summary>
        public const string IsNewPropertyName = "IsNew";

        private bool _isNew = false;

        /// <summary>
        /// Sets and gets the IsNew property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsNew
        {
            get
            {
                return _isNew;
            }

            set
            {
                if (_isNew == value)
                {
                    return;
                }

                _isNew = value;
                RaisePropertyChanged(IsNewPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="CategoryColorBrush" /> property's name.
        /// </summary>
        public const string CategoryColorBrushPropertyName = "CategoryColorBrush";

        private SolidColorBrush _categoryColorBrush = new SolidColorBrush();

        /// <summary>
        /// Sets and gets the CategoryColorBrush property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public SolidColorBrush CategoryColorBrush
        {
            get
            {
                return _categoryColorBrush;
            }

            set
            {
                if (_categoryColorBrush == value)
                {
                    return;
                }

                _categoryColorBrush = value;
                RaisePropertyChanged(CategoryColorBrushPropertyName);
            }
        }
    }
}
