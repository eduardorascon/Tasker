using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Tasker.Helpers;
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
    public class DeskTopViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;

        /// <summary>
        /// Initializes a new instance of the DeskTopViewModel class.
        /// </summary>
        public DeskTopViewModel(IDataService dataService)
        {
            _dataService = dataService;
            DelegarComandos();
            IsTask = true;
        }

        #region Comandos
        void DelegarComandos()
        {
            EditCommand = new RelayCommand(() => SendEditMessage("F2"));
            CancelCommand = new RelayCommand(() => SendEditMessage("Escape"));
            SaveCommand = new RelayCommand(() => SendEditMessage("Enter"));
            NewCommand = new RelayCommand(() => SendEditMessage("New"));
            RefreshCommand = new RelayCommand(() => Messenger.Default.Send("Refresh", "PRESS_KEY_ISSUE"));

        }

        private void SendEditMessage(string funcion)
        {
            Messenger.Default.Send(funcion,  AppVariables.GetValue<bool>("TASK_TYPE") ? "PRESS_KEY_TASK" : "PRESS_KEY_PENDING_TASK");
        }

        #endregion

        public bool IsTask
        {
            get;
            set;
        }

        public RelayCommand EditCommand
        {
            get;
            private set;
        }

        public RelayCommand CancelCommand
        {
            get;
            private set;
        }

        public RelayCommand SaveCommand
        {
            get;
            private set;
        }

        public RelayCommand NewCommand
        {
            get;
            private set;
        }

        public RelayCommand RefreshCommand
        {
            get;
            private set;
        }
    }
}