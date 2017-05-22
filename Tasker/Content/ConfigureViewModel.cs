using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using RelayCommand = GalaSoft.MvvmLight.Command.RelayCommand;

namespace Tasker.Content
{
    /// <summary>
    /// A simple view model for configuring theme, font and accent colors.
    /// </summary>
    public class ConfigureViewModel
        : ViewModelBase
    {
        public ConfigureViewModel()
        {
           IsPinWindow = Properties.Settings.Default.uPinWindow;
           DelegarComandos();
        }

        # region Properties
        /// <summary>
        /// The <see cref="IsPinWindow" /> property's name.
        /// </summary>
        public const string IsPinWindowPropertyName = "IsPinWindow";

        private bool _isPinWindow;

        /// <summary>
        /// Sets and gets the IsPinWindow property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsPinWindow
        {
            get
            {
                return _isPinWindow;
            }

            set
            {
                if (_isPinWindow == value)
                {
                    return;
                }

                RaisePropertyChanging(IsPinWindowPropertyName);
                _isPinWindow = value;
                RaisePropertyChanged(IsPinWindowPropertyName);
                Properties.Settings.Default.uPinWindow = value;
                Messenger.Default.Send(value,"PINWINDOW");
            }
        }

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
            AddCategoryCommand = new RelayCommand(AddCategory);
            RemoveCategoryCommand = new RelayCommand(RemoveCategory);
            ActivateCategoryCommand = new RelayCommand(ActivateCategory);
        }

        private void ActivateCategory()
        {
            throw new NotImplementedException();
        }

        private void RemoveCategory()
        {
            throw new NotImplementedException();
        }

        private void AddCategory()
        {
            throw new NotImplementedException();
        }
        #endregion




    }
}
