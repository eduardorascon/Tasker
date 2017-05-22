using System;
using FirstFloor.ModernUI.Presentation;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Tasker.Controls.FluidStatusBar;
using Tasker.Helpers;

namespace Tasker.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class TabsViewModel : ViewModelBase
    {
        private Uri OriginalTab = new Uri("/View/TabTasks.xaml", UriKind.Relative);
        /// <summary>
        /// Initializes a new instance of the TabsViewModel class.
        /// </summary>
        public TabsViewModel()
        {
            // Cargando las collecciones o los links
            LinkList.Add(new Link { DisplayName = "Tasks", Source = new Uri("/View/TabTasks.xaml", UriKind.Relative) });
          //  LinkList.Add(new Link { DisplayName = "Statistics", Source = new Uri("/View/Statistics/StatisticsView.xaml", UriKind.Relative) });

            //Tab para QA
          //  LinkList.Add(new Link { DisplayName = "QA", Source = new Uri("/View/QA/TabsQA.xaml", UriKind.Relative) });
           
            LinkList.Add(new Link { DisplayName = "Settings", Source = new Uri("/View/Settings.xaml", UriKind.Relative) });
           //Cargando e indicando que el Tab de Task es el seleccionado.
            AppVariables.SetValue("TASK_TYPE", true);
            //Registrandose para escuchar los mensajes de la Barra de Status.
            Messenger.Default.Register<String>(this,"STATUS_BAR_MESSAGE",CreateStatusMessage);


        }

        private void CreateStatusMessage(String message)
        {
           // creating the message object
            var msg = new StatusMessage {Message = message, IsAnimated = true};
            StatusBarMessage = msg;
        }

        /// <summary>
        /// The <see cref="LinkList" /> property's name.
        /// </summary>
        public const string LinkListPropertyName = "LinkList";

        private LinkCollection _linkListt = new LinkCollection();

        /// <summary>
        /// Sets and gets the TasksList property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public LinkCollection LinkList
        {
            get
            {
                return _linkListt;
            }

            set
            {
                if (_linkListt == value)
                {
                    return;
                }

                _linkListt = value;
                RaisePropertyChanged(LinkListPropertyName);
            }
        }


        /// <summary>
        /// The <see cref="SelectedTab" /> property's name.
        /// </summary>
        public const string SelectedTabPropertyName = "SelectedTab";

        private Uri _selectedTab = new Uri("/View/TabTasks.xaml", UriKind.Relative);

        /// <summary>
        /// Sets and gets the TasksList property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Uri SelectedTab
        {
            get
            {
                return _selectedTab;
            }

            set
            {
                if (_selectedTab == value)
                {
                    return;
                }

                _selectedTab = value;
                RaisePropertyChanged(SelectedTabPropertyName);
                //Verificando que sea el TASK de las tareas el activo
                AppVariables.SetValue("TASK_TYPE", _selectedTab == OriginalTab);
            }
        }

        /// <summary>
        /// The <see cref="StatusBarMessage" /> property's name.
        /// </summary>
        public const string StatusBarMessagePropertyName = "StatusBarMessage";

        private StatusMessage _statusBarMessage = null;

        /// <summary>
        /// Sets and gets the StatusBarMessage property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public StatusMessage StatusBarMessage
        {
            get
            {
                return _statusBarMessage;
            }

            set
            {
                if (_statusBarMessage == value)
                {
                    return;
                }

                RaisePropertyChanging(StatusBarMessagePropertyName);
                _statusBarMessage = value;
                RaisePropertyChanged(StatusBarMessagePropertyName);
            }
        }

    }
}