using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Tasker.Model;

namespace Tasker.ViewModel.Sprint
{
    /// <summary>
    /// A simple view model for configuring theme, font and accent colors.
    /// </summary>
    public class SprintsViewModel
        : ViewModelBase
    {

        private readonly IDataService _dataService;
        public SprintsViewModel(IDataService dataService)
        {
            _dataService = dataService;
           DelegarComandos();
        }

        # region Properties

        #region Sprints
        /// <summary>
        /// The <see cref="Sprints" /> property's name.
        /// </summary>
        public const string SprintsPropertyName = "Sprints";

        private ObservableCollection<SprintItem> _sprints;

        /// <summary>
        /// Sets and gets the Categories property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<SprintItem> Sprints
        {
            get
            {
                return _sprints;
            }

            set
            {
                if (_sprints == value)
                {
                    return;
                }

                RaisePropertyChanging(SprintsPropertyName);
                _sprints = value;
                RaisePropertyChanged(SprintsPropertyName);
            }
        }
        #endregion        

        #region SelectedSprint
        /// <summary>
        /// The <see cref="SelectedSprint" /> property's name.
        /// </summary>
        public const string SelectedSprintPropertyName = "SelectedSprint";

        private SprintItem _selectedSprint;

        /// <summary>
        /// Sets and gets the SelectedCategory property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public SprintItem SelectedSprint
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
                Messenger.Default.Send<SprintItem>(_selectedSprint);
            }
        }
        #endregion

        #endregion

        #region Methods
        void DelegarComandos()
        {
            GetSprints();
            // Listen the Messenger
            Messenger.Default.Register<string>(this, "SPRINT", ProcessMesseger);
        }

        private void ProcessMesseger(string function)
        {
  
                switch (function)
                {
                    case "Nuevo":
                        // Adding the new category to the list.
                        // and set the flag IsNew
                        var oNewCategory = new SprintItem() {IsNew = true};
                        Sprints.Add(oNewCategory);
                        SelectedSprint = oNewCategory;
                        break;
                    case "RemoveNewSprint":
                      // Remove the new category cause never should be stored in the database
                        Sprints.Remove(SelectedSprint);
                        break;
                }

        }

        void GetSprints()
        {
            _dataService.GetSprints("HK-P-001", AfterGetSprints);
        }

        private void AfterGetSprints(IList<SprintItem> sprints, Exception exception)
        {
            Sprints = new ObservableCollection<SprintItem>(sprints);
        }
        #endregion

    }
}
