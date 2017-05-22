using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tasker.Helpers
{
    public class Functions
    {
        public string MakeTimeString(double num)
        {
            double hours = Math.Floor(num); //take integral part
            double minutes = (num - hours) * 60.0; //multiply fractional part with 60
            //double seconds = (minutes - Math.Floor(minutes)) * 60.0; //add if you want seconds
            int H = (int)Math.Floor(hours);
            int M = (int)Math.Floor(minutes);
            //int S = (int)Math.Floor(seconds); //add if you want seconds
            return H.ToString("00") + ":" + M.ToString("00"); //+":" + S.ToString("00"); //add if you want seconds
        }


        public string MakeTimeString(int num)
        {
            int minutes = (int)(num / 60); //take integral part
            int seconds = (num - (minutes * 60)); //add if you want seconds

            int hours = (int)(minutes / 60); //take integral part
            minutes = (int)(minutes - (hours * 60)); //multiply fractional part with 60
            

            int H = hours;
            int M = minutes;
            int S = seconds; //add if you want seconds
            return H.ToString("00") + ":" + M.ToString("00") +":" + S.ToString("00"); //add if you want seconds
        }

        public string MakeDateString(DateTime date)
        {
            string _date = string.Empty;
            if (date.Date == DateTime.Now.Date)
            {
                _date = "TODAY";
            }
            else
            {
                _date = string.Format("{0}-{1}-{2}", date.Month.ToString(), date.Day.ToString(), date.Year.ToString());
            }
            return _date;
        }
    }
}
