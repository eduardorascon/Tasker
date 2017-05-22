using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Messaging;
using Tasker.Classes;
using Tasker.Model;

namespace Tasker.Controls
{
    /// <summary>
    /// Description for StatusBarView.
    /// </summary>
    public partial class GanttBar : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the StatusBarView class.
        /// </summary>
        public GanttBar()
        {
            InitializeComponent();
            DataContext = this;
        }

        #region Dependency Properties


        //Declaring the dependency property for StartTime
        public static readonly DependencyProperty StartTimeProperty =
            DependencyProperty.Register("StartTime", typeof (int), typeof (GanttBar),
                new PropertyMetadata(50, StartTimeChanged));

        private static void StartTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //((GanttBar)d).UpdateTronClock();
        }

        public int StartTime
        {
            get { return (int) GetValue(StartTimeProperty); }
            set { SetValue(StartTimeProperty, value); }
        }

        //Declaring the dependency property for DurationTime
        public static readonly DependencyProperty DurationTimeProperty =
            DependencyProperty.Register("DurationTime", typeof (int), typeof (GanttBar),
                new PropertyMetadata(45, DurationTimeChanged));

        private static void DurationTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //((GanttBar)d).UpdateTronClock();
        }

        public int DurationTime
        {
            get { return (int) GetValue(DurationTimeProperty); }
            set { SetValue(DurationTimeProperty, value); }
        }

        #endregion




        private void UserControlLoaded(object sender, RoutedEventArgs e)
        {

        }
    }
}