// ReSharper disable CheckNamespace
namespace GalaSoft.MvvmLight
// ReSharper restore CheckNamespace
{
    /// <summary>
    /// This is class by Karl for MVVM data validation:
    /// http://karlshifflett.wordpress.com/mvvm/input-validation-ui-exceptions-model-validation-errors/
    /// </summary>
    public class UIValidationError
    {
        #region " Declarations "

        private string _dataItemName = string.Empty;
        private string _errorMessage = string.Empty;
        private string _propertyName = string.Empty;

        #endregion

        #region " Properties "

        public string DataItemName
        {
            get { return _dataItemName; }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
        }

        public string Key
        {
            get { return string.Format("{0}:{1}", _dataItemName, _propertyName); }
        }

        public string PropertyName
        {
            get { return _propertyName; }
        }

        #endregion

        #region " Constructors "

        public UIValidationError(string strDataItemName, string propertyName, string errorMessage)
        {
            _dataItemName = strDataItemName;
            _propertyName = propertyName;
            _errorMessage = errorMessage;
        }

        #endregion

        #region " Methods "

        public string ToErrorMessage()
        {
            return string.Concat(CamelCaseString.GetWords(PropertyName), " ", ErrorMessage);
        }

        public string ToFriendlyErrorMessage()
        {

            string errorMessage;

            if (ErrorMessage.Contains("not recognized as a valid DateTime"))
            {
                errorMessage = "La fecha esta en formato incorrecto";
            }

            else if (ErrorMessage.Contains("not in a correct format."))
            {
                errorMessage = "El valor ingresado no es del tipo correcto.";
            }

            else
            {
                //TODO add more tests here 
                errorMessage = ErrorMessage;
            }

            string propertyName = null;

            if (PropertyName.Length > PropertyName.LastIndexOf(".", System.StringComparison.Ordinal) + 1)
                propertyName = CamelCaseString.GetWords(PropertyName.Contains(".") ? PropertyName.Substring(PropertyName.LastIndexOf(".", System.StringComparison.Ordinal) + 1) : PropertyName);

            return string.Concat(propertyName, " ", errorMessage);
        }

        public override string ToString()
        {
            return string.Format("DataItem: {0}, PropertyName: {1}, Error: {2}", _dataItemName, _propertyName, _errorMessage);
        }

        #endregion 
    }
}
