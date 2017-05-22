using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace Tasker.Helpers
{
    public class RejectToVisibility : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility estado = new Visibility();
            var _receiveObj = (string)value;
            if (_receiveObj == "Rejected")
                estado = Visibility.Visible;
            else
                estado = Visibility.Collapsed;

            return estado;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ApproveToVisibility : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility estado = new Visibility();
           var _receiveObj = (string)value;
           if (_receiveObj == "Approved")
              estado = Visibility.Visible;
            else
              estado= Visibility.Collapsed;

            return estado;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class BooleanToVisibilityConverter : IValueConverter
    {

        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            if (targetType == typeof(Visibility))
            {
                var visible = System.Convert.ToBoolean(value, culture);
                if (InvertVisibility)
                    visible = !visible;
                return visible ? Visibility.Visible : Visibility.Collapsed;
            }
            throw new InvalidOperationException("Converter can only convert to value of type Visibility.");
        }

        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException("Converter cannot convert back.");
        }

        public Boolean InvertVisibility { get; set; }
    }


    public class IntToStringTimeConverter : IValueConverter
    {

        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            if (targetType == typeof(string))
            {
                var num = (int)value;

                int minutes = (int)(num / 60); //take integral part
                int seconds = (num - (minutes * 60)); //add if you want seconds

                int hours = (int)(minutes / 60); //take integral part
                minutes = (int)(minutes - (hours * 60)); //multiply fractional part with 60

                int H = hours;
                int M = minutes;
                int S = seconds; //add if you want seconds
                return H.ToString("00") + ":" + M.ToString("00") + ":" + S.ToString("00"); //add if you want seconds
            }
            throw new InvalidOperationException("Converter can only convert to value of type Visibility.");
        }

        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException("Converter cannot convert back.");
        }

        public Boolean InvertVisibility { get; set; }
    }

    /// <summary>
    /// Convierte un valor booleano a su valor invesero
    /// </summary>
    [ValueConversion(typeof(bool), typeof(bool))]
    public class InverseBooleanConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(bool))
                throw new InvalidOperationException("The target must be a boolean");

            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
                                                                        

}
