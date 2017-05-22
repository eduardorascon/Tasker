using System.Collections.Generic;
using System.ComponentModel;

namespace Tasker.Classes
{
    public class BaseDTO : INotifyPropertyChanged
    {
        public string MensajeError { get; set; }
        public bool EsNuevoRegistro { get; set; }

        readonly Dictionary<string,string> _validationErrors = new Dictionary<string, string>(); 

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
