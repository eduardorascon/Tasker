using GalaSoft.MvvmLight;

namespace Tasker.Model
{
    public class DataItem: ObservableObject
    {
        public DataItem(string title)
        {
            Title = title;
        }

        public string Title
        {
            get;
            private set;
        }
    }
}
