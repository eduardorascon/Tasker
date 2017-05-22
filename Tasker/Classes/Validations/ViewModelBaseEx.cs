using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Linq.Expressions;

// ReSharper disable CheckNamespace
namespace GalaSoft.MvvmLight
// ReSharper restore CheckNamespace
{
    public class ViewModelBaseEx : ViewModelBase, IDataErrorInfo
    {
        public void RaisePropertyChanged()
        {
            var frames = new System.Diagnostics.StackTrace();
            for (var i = 0; i < frames.FrameCount; i++)
            {
                var frame = frames.GetFrame(i).GetMethod() as MethodInfo;
                if (frame != null)
                    if (frame.IsSpecialName && frame.Name.StartsWith("set_"))
                    {
                        RaisePropertyChanged(frame.Name.Substring(4));
                        return;
                    }
            }
            throw new InvalidOperationException("NotifyPropertyChanged() can only by invoked within a property setter.");
        }



        /// <summary>
        /// The below Code is from http://www.bricewilson.net/blog/2010/12/15/input-validation-and-ui-exceptions-with-mvvm-light/
        /// </summary>        
        #region " UIValidationError Methods "
        private Dictionary<string, UIValidationError> _objUIValidationErrorDictionary = new Dictionary<string, UIValidationError>();

        public int UIValidationErrorCount
        {
            get { return _objUIValidationErrorDictionary.Count; }
        }

        public string UIValidationErrorUserMessages
        {
            get
            {
                StringBuilder sb = new StringBuilder(1024);

                foreach (KeyValuePair<string, UIValidationError> kvp in _objUIValidationErrorDictionary)
                {
                    sb.AppendLine(String.Format("{0} has an invalid entry.", kvp.Value.PropertyName));
                }
                return sb.ToString();
            }
        }

        public virtual bool IsValid
        {
            get { return true; }
        }

        public void AddUIValidationError(UIValidationError e)
        {
            _objUIValidationErrorDictionary.Add(e.Key, e);
            RaisePropertyChanged("UIValidationErrorUserMessages");
            RaisePropertyChanged("UIValidationErrorCount");
            RaisePropertyChanged("IsValid");
        }

        protected void ClearUIValidationErrors()
        {
            _objUIValidationErrorDictionary.Clear();
            RaisePropertyChanged("UIValidationErrorUserMessages");
            RaisePropertyChanged("UIValidationErrorCount");
            RaisePropertyChanged("IsValid");
        }

        public void RemoveUIValidationError(UIValidationError e)
        {
            _objUIValidationErrorDictionary.Remove(e.Key);
            RaisePropertyChanged("UIValidationErrorUserMessages");
            RaisePropertyChanged("UIValidationErrorCount");
            RaisePropertyChanged("IsValid");
        }
        #endregion

        #region IDataErrorInfo Members

        public string Error
        {
            get;
            set;
        }


        protected Dictionary<string, string> errors = new Dictionary<string, string>();
        public IDictionary<string, string> Errors
        {
            get { return errors; }
        }

        /// <summary>
        /// You will need to override this method. In the derived class you should call the base method
        /// and then carry on with your own processing. You can also use a snippet for this. 
        /// </summary>
        public virtual string this[string columnName]
        {
            get
            {
                Error = string.Empty;
                Errors.Remove(columnName);
                return Error;
            }
        }

        #endregion

        #region ReflectionMagicToGetPropertyName
        /// <summary>
        /// A great tutorial on this kind of reflection:
        /// http://abdullin.com/journal/2008/12/13/how-to-find-out-variable-or-parameter-name-in-c.html
        /// </summary>        
        public new string GetPropertyName<T>(Expression<Func<T>> expr)
        {
            var body = ((MemberExpression)expr.Body);
            return body.Member.Name;
        }
        #endregion
    }
}
