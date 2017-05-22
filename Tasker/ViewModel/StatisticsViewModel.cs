using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Documents;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
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
    public class StatisticsViewModel : ViewModelBase
    {
        /// <summary>
        /// The <see cref="ChartsData" /> property's name.
        /// </summary>
        public const string ChartsDataPropertyName = "ChartsData";

        private readonly IDataService _dataService;
        
        /// <summary>
        /// Initializes a new instance of the TaskModelView class.
        /// </summary>
        public StatisticsViewModel(IDataService dataService)
        {
            _dataService = dataService;
            if (IsInDesignMode)
            {
                // SelectedTask = Design.DesignDataService.CreateTask(0);
            }


            _dataService.GetCategory(
              (categories, error) =>
              {
                  if (error != null)
                  {
                      // Report error here
                      return;
                  }

               //   Categories = new ObservableCollection<CategoryItem>(categories);
              });


          //  Messenger.Default.Register<string>(this,"UPDATE_STATISTICS",UpdateStatistics);



        }

       
    }
}