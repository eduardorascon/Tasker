using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Tasker.Classes;
using Tasker.Helpers;
using Tasker.Model;

namespace SLTaskList.ViewModel
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
   
    public class StatusBarViewModel : ViewModelBase
    {
        Functions oFx = new Functions();
        private readonly IDataService _dataService;
        /// <summary>
        /// Initializes a new instance of the StatusBarViewModel class.
        /// </summary>
        public StatusBarViewModel(IDataService dataService)
        {
            _dataService = dataService;
            Messenger.Default.Register<ObservableSortedList<TaskItem>>(this, "SPEND_TIME", ProcessMesseger);
            Messenger.Default.Send<string>("Enter");


        }

        private void ProcessMesseger(ObservableSortedList<TaskItem> tasksList)
        {
    
            int totalCurrentTime = tasksList.Where(taskItem => taskItem.StringDate == "TODAY").Aggregate(0, (current, taskItem) => current + taskItem.CurrentTime);
            string spendHours = oFx.MakeTimeString(totalCurrentTime);
            SpendTime = spendHours;
        }

        /// <summary>
        /// The <see cref="SpendTime" /> property's name.
        /// </summary>
        public const string SpendTimePropertyName = "SpendTime";

        private string _spendTime = "00:00:00";

        /// <summary>
        /// Sets and gets the SpendTime property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string SpendTime
        {
            get
            {
                return _spendTime;
            }

            set
            {
                if (_spendTime == value)
                {
                    return;
                }

                _spendTime = value;
                RaisePropertyChanged(SpendTimePropertyName);
            }
        }
    }
}