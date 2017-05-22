using GalaSoft.MvvmLight;

namespace Tasker.Model
{
    public class ResultDTO: ObservableObject
    {
        public ResultDTO(bool hasError, string title, string operation, string message, int errorNo=200)
        {
            HasError = hasError; 
            Title = title;
            Operation = operation;
            Message = message;
            ErroNo = errorNo;
        }

        public ResultDTO()
        {
        
        }

        public bool HasError
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public string Operation
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }
        // 100 : Database Error.
        // 200 : OK

        public int ErroNo
        {
            get;
            set;
        }


    }
}
