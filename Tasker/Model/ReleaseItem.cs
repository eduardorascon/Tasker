using System;
using GalaSoft.MvvmLight;
using Tasker.Helpers;

namespace Tasker.Model
{
    public class ReleaseItem : ModelBaseEx
    {
        #region Fields

        readonly Functions _oFx = new Functions();
        #endregion
        public ReleaseItem(int releaseId,
                        int releaseItemId,
                        string externalReference,  
                        string title,
                        string description, 
                        DateTime createDate,
                        string status,      
                        string userName,
                        string tags,
            string stringDate)
        {
            ReleaseId = releaseId;
            ReleaseItemId = releaseItemId;
            ExternalReference = externalReference;
            Title = title;
            Description = description;
            Status = status;
            UserName = userName;
            CreatedDate = createDate;
            Tags = tags;
            StringDate = stringDate;
        }

        public ReleaseItem()
        {
            
        }


        
        #region ReleaseId
        /// <summary>
        /// The <see cref="ReleaseId" /> property's name.
        /// </summary>
        public const string ReleaseIdPropertyName = "ReleaseId";

        private int _releaseId;

        /// <summary>
        /// Sets and gets the ReleaseId property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int ReleaseId
        {
            get
            {
                return _releaseId;
            }

            set
            {
                if (_releaseId == value)
                {
                    return;
                }

                RaisePropertyChanging(ReleaseIdPropertyName);
                _releaseId = value;
                RaisePropertyChanged(ReleaseIdPropertyName);
            }
        }
        #endregion

        #region ReleaseItemId
        /// <summary>
        /// The <see cref="ReleaseItemId" /> property's name.
        /// </summary>
        public const string ReleaseItemIdPropertyName = "ReleaseItemId";

        private int _releaseItemId;

        /// <summary>
        /// Sets and gets the ReleaseItemId property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int ReleaseItemId
        {
            get
            {
                return _releaseItemId;
            }

            set
            {
                if (_releaseItemId == value)
                {
                    return;
                }

                RaisePropertyChanging(ReleaseItemIdPropertyName);
                _releaseItemId = value;
                RaisePropertyChanged(ReleaseItemIdPropertyName);
            }
        }
        #endregion

        #region ExternalReference
        /// <summary>
        /// The <see cref="ExternalReference" /> property's name.
        /// </summary>
        public const string ExternalReferencePropertyName = "ExternalReference";

        private string _externalReference = string.Empty;

        /// <summary>
        /// Sets and gets the ExternalReference property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string ExternalReference
        {
            get
            {
                return _externalReference;
            }

            set
            {
                if (_externalReference == value)
                {
                    return;
                }

                RaisePropertyChanging(ExternalReferencePropertyName);
                _externalReference = value;
                RaisePropertyChanged(ExternalReferencePropertyName);
            }
        }
        #endregion

        #region Title
        /// <summary>
        /// The <see cref="Title" /> property's name.
        /// </summary>
        public const string TitlePropertyName = "Title";

        private string _title = string.Empty;

        /// <summary>
        /// Sets and gets the Title property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Title
        {
            get
            {
                return _title;
            }

            set
            {
                if (_title == value)
                {
                    return;
                }

                RaisePropertyChanging(TitlePropertyName);
                _title = value;
                RaisePropertyChanged(TitlePropertyName);
            }
        }
        #endregion

        #region Description
        /// <summary>
        /// The <see cref="Description" /> property's name.
        /// </summary>
        public const string DescriptionPropertyName = "Description";

        private string _description = string.Empty;

        /// <summary>
        /// Sets and gets the Description property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Description
        {
            get
            {
                return _description;
            }

            set
            {
                if (_description == value)
                {
                    return;
                }

                RaisePropertyChanging(DescriptionPropertyName);
                _description = value;
                RaisePropertyChanged(DescriptionPropertyName);
            }
        }
        #endregion

        #region UserName
        /// <summary>
        /// The <see cref="UserName" /> property's name.
        /// </summary>
        public const string UserNamePropertyName = "UserName";

        private string _userName = string.Empty;

        /// <summary>
        /// Sets and gets the UserName property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string UserName
        {
            get
            {
                return _userName;
            }

            set
            {
                if (_userName == value)
                {
                    return;
                }

                RaisePropertyChanging(UserNamePropertyName);
                _userName = value;
                RaisePropertyChanged(UserNamePropertyName);
            }
        }
        #endregion

        #region Tags
        /// <summary>
        /// The <see cref="Tags" /> property's name.
        /// </summary>
        public const string TagsPropertyName = "Tags";

        private string _tags = string.Empty;

        /// <summary>
        /// Sets and gets the Tags property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Tags
        {
            get
            {
                return _tags;
            }

            set
            {
                if (_tags == value)
                {
                    return;
                }

                RaisePropertyChanging(TagsPropertyName);
                _tags = value;
                RaisePropertyChanged(TagsPropertyName);
            }
        }
        #endregion

        #region CreatedDate
        /// <summary>
        /// The <see cref="CreatedDate" /> property's name.
        /// </summary>
        public const string CreatedDatePropertyName = "CreatedDate";

        private DateTime _createdDate = DateTime.Now;

        /// <summary>
        /// Sets and gets the CreatedDate property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public DateTime CreatedDate
        {
            get
            {
                return _createdDate;
            }

            set
            {
                if (_createdDate == value)
                {
                    return;
                }

                _createdDate = value;
                StringDate = _oFx.MakeDateString(_createdDate);
                RaisePropertyChanged(CreatedDatePropertyName);
            }
        }

        #endregion

        
        #region StringDate
        /// <summary>
        /// The <see cref="StringDate" /> property's name.
        /// </summary>
        public const string StringDatePropertyName = "StringDate";

        private string _stringDate = string.Empty;

        /// <summary>
        /// Sets and gets the StringDate property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string StringDate
        {
            get
            {
                return _stringDate;
            }

            set
            {
                if (_stringDate == value)
                {
                    return;
                }

                RaisePropertyChanging(StringDatePropertyName);
                _stringDate = value;
                RaisePropertyChanged(StringDatePropertyName);
            }
        }
        #endregion

        #region Status
        /// <summary>
        /// The <see cref="Status" /> property's name.
        /// </summary>
        public const string StatusPropertyName = "Status";

        private string _status = string.Empty;

        /// <summary>
        /// Sets and gets the Status property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Status
        {
            get
            {
                return _status;
            }

            set
            {
                if (_status == value)
                {
                    return;
                }

                _status = value;
                RaisePropertyChanged(StatusPropertyName);
            }
        }
        #endregion

        #region IsNew
        /// <summary>
        /// The <see cref="IsNew" /> property's name.
        /// </summary>
        public const string IsNewPropertyName = "IsNew";

        private bool _isNew;

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

        #endregion


        #region IDataErrorInfo
        /// <summary>
        /// Implementation of IDataErrorInfo
        /// </summary>
        /// <param name="columnName">The name of the property that is being validated</param>
        /// <returns>The last validation error</returns>
        public override string this[string columnName]
        {
            get
            {
                //Set the error message on Error property. 
                //This property is from IDataErrorInfo and will contain the last Error in any validation.
                Error = string.Empty;
                Errors.Remove(columnName);

                //Property name to validate
                if (columnName == GetPropertyName(() => Title))
                {
                    //Validate the property value
                    if (string.IsNullOrEmpty(Title))
                    {
                        Error = "Title cannot be left blank";
                    }
                }


                //Property name to validate
                if (columnName == GetPropertyName(() => Description))
                {
                    //Validate the property value
                    if (string.IsNullOrEmpty(Title))
                    {
                        Error = "Description cannot be left blank";
                    }
                }


                //This stays as it is 
                if (!string.IsNullOrEmpty(Error))
                {
                    Errors.Add(columnName, Error);
                }
                return Error;
            }
        }
        #endregion
      
    }
}
