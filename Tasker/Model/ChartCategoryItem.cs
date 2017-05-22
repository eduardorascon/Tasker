using System.Windows.Media;
using GalaSoft.MvvmLight;

namespace Tasker.Model
{
    public class ChartCategoryItem : ObservableObject
    {

        public ChartCategoryItem(string category, decimal totaltime, bool exploded)
        {
            Category = category;
            DataPointValue = totaltime;
            Exploded = exploded;
        }

        public ChartCategoryItem()
        {

        }

        /// <summary>
        /// The <see cref="Category" /> property's name.
        /// </summary>
        public const string CategoryPropertyName = "Category";

        private string _category = string.Empty;

        /// <summary>
        /// Sets and gets the Category property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Category
        {
            get
            {
                return _category;
            }

            set
            {
                if (_category == value)
                {
                    return;
                }

                _category = value;
                RaisePropertyChanged(CategoryPropertyName);
            }
        }

       

        /// <summary>
        /// The <see cref="DataPointValue" /> property's name.
        /// </summary>
        public const string DataPointValuePropertyName = "DataPointValue";

        private decimal _dataPointValue = 0;

        /// <summary>
        /// Sets and gets the CurrentTime property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public decimal DataPointValue
        {
            get
            {
                return _dataPointValue;
            }

            set
            {
                if (_dataPointValue == value)
                {
                    return;
                }

                _dataPointValue = value;
                RaisePropertyChanged(DataPointValuePropertyName);
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

        /// <summary>
        /// The <see cref="Exploded" /> property's name.
        /// </summary>
        public const string ExplodedPropertyName = "Exploded";

        private bool _exploded = false;

        /// <summary>
        /// Sets and gets the Category property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool Exploded
        {
            get
            {
                return _exploded;
            }

            set
            {
                if (_exploded == value)
                {
                    return;
                }

                _exploded = value;
                RaisePropertyChanged(ExplodedPropertyName);
            }
        }

      
    }
}
