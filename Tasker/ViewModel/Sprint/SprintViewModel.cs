using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Tasker.Classes;
using Tasker.Model;

namespace Tasker.ViewModel
{
    /// <summary>
    /// A simple view model for configuring theme, font and accent colors.
    /// </summary>
    public class SprintViewModel
        : ViewModelBase
    {

        private readonly IDataService _dataService;
        private SprintItem originalSelectedSprint= new SprintItem();
        public SprintViewModel(IDataService dataService)
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
              //  AutomapperTypeAdapter.SetEntityODataObjectValue(_selectedSprint, originalSelectedSprint, false);
            }
        }
        #endregion

        #endregion

     #region Methods
        void DelegarComandos()
        {    
            // Listen the Messages
            Messenger.Default.Register<string>(this, "SPRINT", ProcessMesseger);
            Messenger.Default.Register<SprintItem>(this,UpdateSelectedSprint);
        }

        private void UpdateSelectedSprint(SprintItem sprintItem)
        {
            SelectedSprint = sprintItem;
        }

        private void ProcessMesseger(string function)
        {
            if (SelectedSprint != null)
            {
                switch (function)
                {

                    case "Activar":
                    case "Salvar" :

                        //if (function == "Activar")
                        //{
                        //    SelectedSprint.IsActive = !SelectedSprint.IsActive;
                        //}

                        // _dataService.SaveCategory(SelectedSprint, (resulDTO, exception) =>
                        //                                             {
                        //                                                 if (!resulDTO.HasError)
                        //                                                 {

                        //                                                     Messenger.Default.Send("Collapse",
                        //                                                       "CATEGORY_EDIT_MODE");                                                                             
                                                                                
                        //                                                         Messenger.Default.Send("Update", "UPDATE_CATEGORIES");

                        //                                                 }
                        //                                                    TaskerHelper.SetStatusBarMessage(
                        //                                                         resulDTO.Message);
                        //                                             });

                        //if (function == "Activar")
                        //{
                        //    TaskerHelper.SetStatusBarMessage(
                        //        SelectedCategory.IsActive ? "Category Active" : "Category Inactive");
                        //}

                        break;
                    case "Cancelar":
                       // AutomapperTypeAdapter.SetEntityODataObjectValue(originalSelectedCategory, SelectedCategory,false);
                       //if (SelectedCategory.IsNew)
                       //{
                       //    Messenger.Default.Send("RemoveNewCategory", "CATEGORY");
                       //}
                       //Messenger.Default.Send("Collapse", "CATEGORY_EDIT_MODE");
                       break;
                }
                
            }
        }

     
      
        #endregion





    }
}
