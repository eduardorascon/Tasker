using System;
using GalaSoft.MvvmLight;
using Tasker.Helpers;

namespace Tasker.Model
{
    public class IssueItem : ModelBaseEx
    {
        #region Fields

        readonly Functions _oFx = new Functions();
        #endregion
        public IssueItem(string issueId,
                        string title,
                        string description, 
                        DateTime lastDate,
                        DateTime createdDate,
                        string status,      
                        string userName,
                        string tags,
            string stringDate,
            string issueType,
            string link)
        {
            IssueId = issueId;
            Title = title;
            Description = description;
            Status = status;
            UserName = userName;
            LastDate = lastDate;
            CreatedDate = createdDate;
            Tags = tags;
            StringDate = stringDate;
            IssueType = issueType;
            Link = link;
        }

        public IssueItem()
        {
            
        }


        
        #region IssueId
        /// <summary>
        /// The <see cref="IssueId" /> property's name.
        /// </summary>
        public const string IssueIdPropertyName = "IssueId";

        private string _issueId = string.Empty;

        /// <summary>
        /// Sets and gets the IssueId property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string IssueId
        {
            get
            {
                return _issueId;
            }

            set
            {
                if (_issueId == value)
                {
                    return;
                }

                RaisePropertyChanging(IssueIdPropertyName);
                _issueId = value;
                IssueLink = String.Format("[url={0}]{1}[/url]",Link, _issueId);
                RaisePropertyChanged(IssueIdPropertyName);
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

        #region LastDate
        /// <summary>
        /// The <see cref="LastDate" /> property's name.
        /// </summary>
        public const string LastDatePropertyName = "LastDate";

        private DateTime _lastDate;

        /// <summary>
        /// Sets and gets the LastDate property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public DateTime LastDate
        {
            get
            {
                return _lastDate;
            }

            set
            {
                if (_lastDate == value)
                {
                    return;
                }

                RaisePropertyChanging(LastDatePropertyName);
                _lastDate = value;
                RaisePropertyChanged(LastDatePropertyName);
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

        #region IssueType
        /// <summary>
        /// The <see cref="IssueType" /> property's name.
        /// </summary>
        public const string IssueTypePropertyName = "IssueType";

        private string  _issueType = string.Empty;

        /// <summary>
        /// Sets and gets the IssueType property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string  IssueType
        {
            get
            {
                return _issueType;
            }

            set
            {
                if (_issueType == value)
                {
                    return;
                }

                RaisePropertyChanging(IssueTypePropertyName);
                _issueType = value;
                RaisePropertyChanged(IssueTypePropertyName);
            }
        }
        #endregion

        #region Link
        /// <summary>
        /// The <see cref="Link" /> property's name.
        /// </summary>
        public const string LinkPropertyName = "Link";

        private string _link = string.Empty;

        /// <summary>
        /// Sets and gets the Link property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Link
        {
            get
            {
                return _link;
            }

            set
            {
                if (_link == value)
                {
                    return;
                }

                RaisePropertyChanging(LinkPropertyName);
                _link = value;
                IssueLink = String.Format("[url={0}]{1}[/url]", _link, IssueId);
                RaisePropertyChanged(LinkPropertyName);
            }
        }
        #endregion
        
        #region IssueLink
        /// <summary>
        /// The <see cref="IssueLink" /> property's name.
        /// </summary>
        public const string IssueLinkPropertyName = "IssueLink";

        private string _issueLink = string.Empty;

        /// <summary>
        /// Sets and gets the IssueLink property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string IssueLink
        {
            get
            {
                return _issueLink;
            }

            set
            {
                if (_issueLink == value)
                {
                    return;
                }

                RaisePropertyChanging(IssueLinkPropertyName);
                _issueLink = value;
                RaisePropertyChanged(IssueLinkPropertyName);
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
