using System.Windows;
using System.Windows.Media;

namespace Tasker.Controls
{
    /// <summary>
    /// Interaction logic for MainWindow.xamlMyControls
    /// </summary>
    public partial class TronClock
    {
        #region Dependency Properties


        //Declaring the dependency property for TotalClockTime
        public static readonly DependencyProperty TotalClockTimeProperty =
            DependencyProperty.Register("TotalClockTime", typeof (int), typeof (TronClock), new PropertyMetadata(0,TotalClockTimeChanged));

        private static void TotalClockTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((TronClock)d).UpdateTronClock();
        }

        public int TotalClockTime
        {
            get { return (int) GetValue(TotalClockTimeProperty); }
            set { SetValue(TotalClockTimeProperty, value); }
        }

        //Declaring the dependency property for TotalClockTime
        public static readonly DependencyProperty HoursProperty =
            DependencyProperty.Register("Hours", typeof(int), typeof(TronClock), new PropertyMetadata(0));

        public int Hours
        {
            get { return (int)GetValue(HoursProperty); }
            set { SetValue(HoursProperty, value); }
        }

        //Declaring the dependency property for Minutes
        public static readonly DependencyProperty MinutesProperty =
            DependencyProperty.Register("Minutes", typeof(int), typeof(TronClock), new PropertyMetadata(0));

        public int Minutes
        {
            get { return (int)GetValue(MinutesProperty); }
            set { SetValue(MinutesProperty, value); }
        }

        //Declaring the dependency property for Minutes
        public static readonly DependencyProperty SecondsProperty =
            DependencyProperty.Register("Seconds", typeof(int), typeof(TronClock), new PropertyMetadata(0));

        public int Seconds
        {
            get { return (int)GetValue(SecondsProperty); }
            set { SetValue(SecondsProperty, value); }
        }
        
        public static readonly DependencyProperty ColorClockProperty =
           DependencyProperty.Register("ColorClock", typeof(SolidColorBrush), typeof(TronClock), new PropertyMetadata(new SolidColorBrush(Colors.DimGray)));

        public SolidColorBrush ColorClock
        {
            get { return (SolidColorBrush)GetValue(ColorClockProperty); }
            set { SetValue(ColorClockProperty, value); }
        }
        #endregion

     

        public TronClock()
        {
            InitializeComponent();
            DataContext = this;
        }

      

      
        private void UserControlLoaded(object sender, RoutedEventArgs e)
        {
           
            UpdateTronClock();
        }

   
        private void UpdateTronClock()
        {

            int nTotalSegundos = (TotalClockTime);
            int nMinutos = 0;
            int nHoras = 0;

            // Calcular los minutos
            if (nTotalSegundos > 59)
                nMinutos = (nTotalSegundos / 60); // Horas

            // Segundos remanentes
            int nSegundos = nTotalSegundos - (nMinutos * 60);

            // Calcular las horas
            if (nMinutos > 59)
                nHoras = (nMinutos / 60); // Horas

            // Minutos remanentes
            nMinutos = nMinutos - (nHoras * 60);

            Seconds = (nSegundos*6);
            Minutes = (nMinutos*6);
            Hours = (nHoras * 30) + (int)(nMinutos * 0.5);
        }

    }
}
