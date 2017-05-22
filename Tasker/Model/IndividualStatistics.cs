using System.Collections.Generic;
using GalaSoft.MvvmLight;

namespace Tasker.Model
{
    public class IndividualStatistics: ObservableObject
    {
        public IndividualStatistics(string userName, List<ChartCategoryItem> totalDayTime)
        {
            UserName = userName;
            TotalDayTime = totalDayTime;
        }

        public string UserName
        {
            get;
            private set;
        }

        public List<ChartCategoryItem> TotalDayTime
              {
            get;
            private set;
        }
    }
}
