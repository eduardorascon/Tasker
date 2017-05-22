using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Data;

// ReSharper disable CheckNamespace
namespace GalaSoft.MvvmLight
// ReSharper restore CheckNamespace
{
    public class ViewBaseEx : UserControl
    {
        public ViewBaseEx()
        {

            Loaded += ViewBaseLoaded;
            Unloaded += ViewBaseUnLoaded;
        }

        #region " Declarations "

        private RoutedEventHandler _errorEventRoutedEventHandler;

        #endregion

        #region " Methods "

        private void ExceptionValidationErrorHandler(object sender, RoutedEventArgs e)
        {

            var args = (ValidationErrorEventArgs)e;

            if (args.Error.RuleInError is ExceptionValidationRule)
            {

                var viewModelBase = DataContext as ViewModelBaseEx;

                if (viewModelBase != null)
                {

                    //we only want to work with validation errors that are Exceptions because the business object has already recorded the business rule violations using IDataErrorInfo. 
                    var bindingExpression = (BindingExpression)args.Error.BindingInError;
                    var dataItemName = bindingExpression.DataItem.ToString();
                    var propertyPath = bindingExpression.ParentBinding.Path.Path;
                    var sb = new System.Text.StringBuilder(1024);

                    foreach (var validationError in Validation.GetErrors((DependencyObject)args.OriginalSource).Where(validationError => validationError.RuleInError is ExceptionValidationRule))
                    {
                        sb.AppendFormat("{0} has error ", propertyPath);

                        if (validationError.Exception == null || validationError.Exception.InnerException == null)
                        {
                            sb.AppendLine(validationError.ErrorContent.ToString());
                        }

                        else
                        {
                            sb.AppendLine(validationError.Exception.InnerException.Message);
                        }
                    }


                    if (args.Action == ValidationErrorEventAction.Added)
                    {
                        viewModelBase.AddUIValidationError(new UIValidationError(dataItemName, propertyPath, sb.ToString()));
                    }

                    else if (args.Action == ValidationErrorEventAction.Removed)
                    {
                        viewModelBase.RemoveUIValidationError(new UIValidationError(dataItemName, propertyPath, sb.ToString()));
                    }

                    else
                    {
                        //this can only happen if the .NET Framework changes. Better to put a sanity check in now that have a very hard to find bug later. 
                        throw new ArgumentOutOfRangeException("sender", args.Action, "Action value was not programmed.");
                    }
                }

            }


        }

        private void ViewBaseLoaded(object sender, RoutedEventArgs e)
        {
            //Only add this if it is null : 
            if (_errorEventRoutedEventHandler != null)
                return;
            //this adds a form level handler and will listen for each control that has the NotifyOnValidationError=True and a ValidationError occurs. 
            _errorEventRoutedEventHandler = ExceptionValidationErrorHandler;
            AddHandler(Validation.ErrorEvent, _errorEventRoutedEventHandler, true);
        }

        private void ViewBaseUnLoaded(object sender, RoutedEventArgs e)
        {
            if (_errorEventRoutedEventHandler == null)
                return;
            RemoveHandler(Validation.ErrorEvent, _errorEventRoutedEventHandler);
            _errorEventRoutedEventHandler = null;
        }

        #endregion
    } 

}
