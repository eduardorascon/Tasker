using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using GalaSoft.MvvmLight;
using Tasker.Model;

namespace Tasker.ViewModel.Statistics
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class StatisticsViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        /// <summary>
        /// Initializes a new instance of the StatisticViewModel class.
        /// </summary>
        public StatisticsViewModel(IDataService dataService)
        {
            _dataService = dataService;
            DelegarComandos();
        }

        private void DelegarComandos()
        {
            RefreshCharts();
        }

        private void RefreshCharts()
        {
            CategoriesYTD.ChartType = "Bar";
           
            _dataService.GetStatisticData(100, delegate(IList<ChartCategoryItem> list, Exception exception)
                                             {
                                                 CategoriesYTD.Categories = list.OrderBy(c => c.DataPointValue).ToList();
                                             } );
        }

        #region Properties

        #region CategoriesYTD
        /// <summary>
        /// The <see cref="CategoriesYTD" /> property's name.
        /// </summary>
        public const string CategoriesYTDPropertyName = "CategoriesYTD";

        private ChartItem _categoriesYTD= new ChartItem();

        /// <summary>
        /// Sets and gets the CategoriesYTD property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ChartItem CategoriesYTD
        {
            get
            {
                return _categoriesYTD;
            }

            set
            {
                if (_categoriesYTD == value)
                {
                    return;
                }

                RaisePropertyChanging(CategoriesYTDPropertyName);
                _categoriesYTD = value;
                RaisePropertyChanged(CategoriesYTDPropertyName);
            }
        }
        #endregion

        #endregion
    }
}