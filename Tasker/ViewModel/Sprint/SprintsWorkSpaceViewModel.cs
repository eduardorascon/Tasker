using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Tasker.Model;

namespace Tasker.ViewModel
{
    /// <summary>
    /// A simple view model for configuring theme, font and accent colors.
    /// </summary>
    public class SprintsWorkSpaceViewModel
        : ViewModelBase
    {

        private readonly IDataService _dataService;
        public SprintsWorkSpaceViewModel(IDataService dataService)
        {
            _dataService = dataService;
           DelegarComandos();
        }

        # region Properties
       

        #region SelectedSprint
        /// <summary>
        /// The <see cref="SelectedSprint" /> property's name.
        /// </summary>
        public const string SelectedSprintPropertyName = "SelectedSprint";

        private CategoryItem _selectedSprint;

        /// <summary>
        /// Sets and gets the SelectedCategory property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public CategoryItem SelectedCategory
        {
            get
            {
                return _selectedSprint;
            }

            set
            {
                if (_selectedSprint == value)
                {
                    return;
                }

                RaisePropertyChanging(SelectedSprintPropertyName);
                _selectedSprint = value;
                RaisePropertyChanged(SelectedSprintPropertyName);
            }
        }
        #endregion

        #region IsEditMode
        /// <summary>
        /// The <see cref="IsEditMode" /> property's name.
        /// </summary>
        public const string IsEditModePropertyName = "IsEditMode";

        private bool _isEditMode = false;

        /// <summary>
        /// Sets and gets the ShowDetail property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsEditMode
        {
            get
            {
                return _isEditMode;
            }

            set
            {
                if (_isEditMode == value)
                {
                    return;
                }

                RaisePropertyChanging(IsEditModePropertyName);
                _isEditMode = value;
                RaisePropertyChanged(IsEditModePropertyName);
            }
        }
        #endregion

        #endregion

        # region Commands
        public RelayCommand AddSprintCommand
        {
            get;
            private set;
        }

        public RelayCommand SaveSprintCommand
        {
            get;
            private set;
        }

        public RelayCommand ActivateSprintCommand
        {
            get;
            private set;
        }

        public RelayCommand EditSprintCommand
        {
            get;
            private set;
        }

        public RelayCommand CancelEditSprintCommand
        {
            get;
            private set;
        }
        #endregion

        #region Methods
        void DelegarComandos()
        {
            AddSprintCommand = new RelayCommand(AddSprint);
            CancelEditSprintCommand = new RelayCommand(CancelEditSprint);
            SaveSprintCommand = new RelayCommand(SaveSprint);
            EditSprintCommand = new RelayCommand(EditSprint);
            ActivateSprintCommand = new RelayCommand(ActivateSprint);
         
            Messenger.Default.Register<string>(this, "SPRINT_EDIT_MODE", (string msg) =>
                                                                           {
                                                                               IsEditMode = (msg != "Collapse");
                                                                           });
        }

        private void EditSprint()
        {
            IsEditMode = true;
        }

        private void CancelEditSprint()
        {
            Messenger.Default.Send("Cancelar", "SPRINT");
            IsEditMode = false;
        }

        private void ActivateSprint()
        {
            Messenger.Default.Send("Activar", "SPRINT");
        }

        private void SaveSprint()
        {
            Messenger.Default.Send("Salvar", "SPRINT");
        }

        private void AddSprint()
        {
            Messenger.Default.Send("Nuevo", "SPRINT");
           IsEditMode = true;
        }
        #endregion

    }
}
