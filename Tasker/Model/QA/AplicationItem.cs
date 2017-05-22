using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Helpers;

namespace Tasker.Model.QA
{
    public class AplicationItem : ModelBaseEx
    {
        #region Fields
        Functions oFx = new Functions();
        #endregion

        #region Constructor
        /// <summary>
        /// Sobrecargar de Constructor
        /// </summary>
        /// <param name="aplicacionId">Codigo de la aplicación</param>
        /// <param name="descripcion">Descripción</param>
        public AplicationItem(int aplicationId, string description)
        {
            AplicationId = aplicationId;
            Descriptions = description;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        public AplicationItem()
        {
            
        }

        #endregion



        #region AplicationId
        /// <summary>
        /// The <see cref="AplicationId" /> property's name.
        /// </summary>
        public const string AplicationIdPropertyName = "AplicationId";

        private int _aplicationId = 9999999;

        /// <summary>
        /// Sets and gets the AplicationId property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int AplicationId
        {
            get
            {
                return _aplicationId;
            }

            set
            {
                if (_aplicationId == value)
                {
                    return;
                }

                RaisePropertyChanging(AplicationIdPropertyName);
                _aplicationId = value;
                RaisePropertyChanged(AplicationIdPropertyName);
            }
        }
        
        #endregion

        #region Description
        /// <summary>
        /// The <see cref="Description" /> property's name.
        /// </summary>
        public const string DescriptionPropertyName = "Descriptions";

        private string _descripction = string.Empty;

        /// <summary>
        /// Sets and gets the Description property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Descriptions
        {
            get
            {
                return _descripction;
            }

            set
            {
                if (_descripction == value)
                {
                    return;
                }

                RaisePropertyChanging(DescriptionPropertyName);
                _descripction = value;
                RaisePropertyChanged(DescriptionPropertyName);
            }
        }
        #endregion

        ///// <summary>
        ///// The <see cref="Descripcion" /> property's name.
        ///// </summary>
        //public const string DescriptionPropertyName = "Description";

        //private string _description = string.Empty;

        ///// <summary>
        ///// Sets and gets the Title property.
        ///// Changes to that property's value raise the PropertyChanged event. 
        ///// </summary>
        //public string Description
        //{
        //    get
        //    {
        //        return _description;
        //    }

        //    set
        //    {
        //        if (_description == value)
        //        {
        //            return;
        //        }

        //        _description = value;
        //        RaisePropertyChanged(DescriptionPropertyName);
        //    }
        //}
        

        ///// <summary>
        ///// The <see cref="AplicacionId" /> property's name.
        ///// </summary>
        //public const string AplicacionIdPropertyName = "AplicationId";

        //private int _aplicationId = 99999999;

        ///// <summary>  
        ///// Sets and gets the Category property.
        ///// Changes to that property's value raise the PropertyChanged event. 
        ///// </summary>
        //public int AplicationId
        //{
        //    get
        //    {
        //        return _aplicationId;
        //    }

        //    set
        //    {
        //        if (_aplicationId == value)
        //        {
        //            return;
        //        }

        //        _aplicationId = value;
        //        RaisePropertyChanged(AplicacionIdPropertyName);
        //    }
        //}

        //Pendiente ver como funciona 
        //#region IDataErrorInfo
        ///// <summary>
        ///// Implementation of IDataErrorInfo
        ///// </summary>
        ///// <param name="columnName">The name of the property that is being validated</param>
        ///// <returns>The last validation error</returns>
        //public override string this[string columnName]
        //{
        //    get
        //    {
        //        //Set the error message on Error property. 
        //        //This property is from IDataErrorInfo and will contain the last Error in any validation.
        //        Error = string.Empty;
        //        this.Errors.Remove(columnName);


        //        //Property name to validate
        //        if (columnName == GetPropertyName<string>(() => Descripcion))
        //        {
        //            //Validate the property value
        //            if (string.IsNullOrEmpty(Descripcion))
        //            {
        //                Error = "Category cannot be left blank";
        //            }
        //        }


        //        //This stays as it is 
        //        if (!string.IsNullOrEmpty(Error))
        //        {
        //            this.Errors.Add(columnName, Error);
        //        }
        //        return Error;
        //    }
        //}
        //#endregion

    }
}
