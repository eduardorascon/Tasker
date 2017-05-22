using System;
using System.Collections.ObjectModel;
using System.Linq;

using System.Windows.Controls;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Messaging;
using Tasker.Classes;
using Tasker.Model;


namespace Tasker.View
{
    /// <summary>
    /// Description for StatusBarView.
    /// </summary>
    public partial class StatusBarView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the StatusBarView class.
        /// </summary>
        public StatusBarView()
        {
            InitializeComponent();
            Messenger.Default.Register<ObservableSortedList<TaskItem>>(this, "SPEND_TIME", ProcessMesseger);
        }
        
        private void ProcessMesseger(ObservableSortedList<TaskItem> tasksList)
        {
            ProgressStatus.Children.Clear();
            double ancho = ProgressStatus.Width;
            // ancho  = 10:00:00 hrs

            foreach (TaskItem taskItem in tasksList.Where(t => t.StringDate == "TODAY").OrderBy(o => o.CreatedDate))
            {
                        // Que porcentaje del ancho equivale el tiempo de la tarea si el ancho es igual a 10:00 hrs.
                        decimal porcentajeAncho = (Decimal)taskItem.CurrentTime / (Decimal)((10 * 60) * 60);
                        double anchoTareaCalculado = ((double)porcentajeAncho*ancho);
                        Rectangle rectangle = new Rectangle();
                        rectangle.ToolTip = taskItem.Category;
                        rectangle.Width = anchoTareaCalculado;
                        rectangle.Height = 40;
                        rectangle.Fill = taskItem.CategoryColorBrush;
                        ProgressStatus.Children.Add(rectangle);
                    }
                }
            }
}