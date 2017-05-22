using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight;
using Tasker.Helpers;

namespace Tasker.Model
{
    public class ChartItem : ObservableObject 
    {
        readonly Functions  _ofx = new Functions();

        #region AverageString
        /// <summary>
        /// The <see cref="AverageString" /> property's name.
        /// </summary>
        public const string AverageStringPropertyName = "AverageString";

        private string _averageString = string.Empty;

        /// <summary>
        /// Sets and gets the AverageString property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string AverageString
        {
            get
            {
                return _averageString;
            }

            set
            {
                if (_averageString != null && _averageString == value)
                {
                    return;
                }
            
                _averageString = value;
            }
        }
        #endregion

        #region Average
        /// <summary>
        /// The <see cref="Average" /> property's name.
        /// </summary>
        public const string AveragePropertyName = "Average";

        private decimal _average;

        /// <summary>
        /// Sets and gets the Average property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public decimal Average
        {
            get
            {
                return _average;
            }

            set
            {
                if (_average == value)
                {
                    return;
                }
                _average = value;
            }
        }
        #endregion
        
        #region Frecuency
        /// <summary>
        /// The <see cref="Frecuency" /> property's name.
        /// </summary>
        public const string FrecuencyPropertyName = "Frecuency";

        private int _frecuency;

        /// <summary>
        /// Sets and gets the Frecuency property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int Frecuency
        {
            get
            {
                return _frecuency;
            }

            set
            {
                if (_frecuency == value)
                {
                    return;
                }

                _frecuency = value;
            }
        }
        #endregion

        #region ChartType
        /// <summary>
        /// The <see cref="ChartType" /> property's name.
        /// </summary>
        public const string ChartTypePropertyName = "ChartType";

        private string _chartType = "Pie";

        /// <summary>
        /// Sets and gets the ChartType property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string ChartType
        {
            get
            {
                return _chartType;
            }

            set
            {
                if (_chartType != null && _chartType == value)
                {
                    return;
                }
                _chartType = value;
            }
        }
        #endregion

        #region Categories
        /// <summary>
        /// The <see cref="Categories" /> property's name.
        /// </summary>
        public const string CategoriesPropertyName = "Categories";

        private List<ChartCategoryItem> _categories;
        /// <summary>
        /// Sets and gets the Category property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<ChartCategoryItem> Categories
        {
            get { return _categories; }

            set
            {
                if (_categories != null && _categories== value)
                {
                    return;
                }
                RaisePropertyChanging(CategoriesPropertyName);
                _categories = value;
                RaisePropertyChanged(CategoriesPropertyName);
                
                CalculateStatistic();
            }
        }
        #endregion

        void CalculateStatistic()
        {
            var categories = Categories ;
            if (categories != null)
            {
                Frecuency = categories.Count();

                if (Frecuency > 0)
                {
                    Average = categories.Average(c => c.DataPointValue);
                   AverageString =  _ofx.MakeTimeString((double)Average);
                }
                else
                {
                    AverageString = "00:00";
                }
            }
            else
            {
               AverageString = "00:00";
                Frecuency = 0;

            }
           
        }
    }
}
