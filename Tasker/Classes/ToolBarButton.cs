using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tasker.Classes
{
   
    public class ToolBarButton : Button
    {
        public ToolBarButton()
        {
            DefaultStyleKey = typeof(ToolBarButton);
        }

        public static readonly DependencyProperty MetroImageSourceProperty =
            DependencyProperty.Register("MetroImageSource", typeof(Visual), typeof(ToolBarButton), new PropertyMetadata(default(Visual)));

        public Visual MetroImageSource
        {
            get { return (Visual)GetValue(MetroImageSourceProperty); }
            set { SetValue(MetroImageSourceProperty, value); }
        }
    }
}