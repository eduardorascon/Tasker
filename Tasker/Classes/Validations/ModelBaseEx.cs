using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;

// ReSharper disable CheckNamespace
namespace GalaSoft.MvvmLight
// ReSharper restore CheckNamespace
{
    public class ModelBaseEx : ObservableObject, IDataErrorInfo
    {



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


        protected Dictionary<string, string> _errors = new Dictionary<string, string>();
        public IDictionary<string, string> Errors
        {
            get { return _errors; }
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
                this.Errors.Remove(columnName);
                return Error;
            }
        }

        #endregion

        /// <summary>
        /// The <see cref="IsNew" /> property's name.
        /// </summary>
        public const string IsNewPropertyName = "IsNew";

        private bool _isNew = false;

        /// <summary>
        /// Sets and gets the IsNew property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsNew
        {
            get
            {
                return _isNew;
            }

            set
            {
                if (_isNew == value)
                {
                    return;
                }

                _isNew = value;
                RaisePropertyChanged(IsNewPropertyName);
            }
        }


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
