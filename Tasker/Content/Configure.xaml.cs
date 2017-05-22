using System.Windows;
using DataServices.JIRA;
using GalaSoft.MvvmLight.Messaging;

namespace Tasker.Content
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class Configure
    {
        public Configure()
        {
            InitializeComponent();
            DataContext = new ConfigureViewModel();
        }

      
    }
}
