using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Helpers;
using Tasker.ViewModel.QA;

namespace Tasker.Model.QA
{
   public class FaultExecutionItem : ModelBaseEx
    {

        #region Fields
        Functions oFx = new Functions();
        #endregion

        #region Constructor

        public FaultExecutionItem()
        {
        }

        public FaultExecutionItem(int faultExecId, int runExecDetailId, string comment, byte[] image, string status)
        {
            FaultExecId = faultExecId;
            RunExecDetailId = runExecDetailId;
            Comment = comment;
            Image = image;
            Status = status;
        }

        ~FaultExecutionItem()
        {
        }

        #endregion

        #region Properties

        
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

                RaisePropertyChanging(StatusPropertyName);
                _status = value;
                RaisePropertyChanged(StatusPropertyName);
            }
        }

        
        /// <summary>
        /// The <see cref="FaultExecId" /> property's name.
        /// </summary>
        public const string FaultExecIdPropertyName = "FaultExecId";

        private int _faultExecId = 0;

        /// <summary>
        /// Sets and gets the FaultExecId property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int FaultExecId
        {
            get
            {
                return _faultExecId;
            }

            set
            {
                if (_faultExecId == value)
                {
                    return;
                }

                RaisePropertyChanging(FaultExecIdPropertyName);
                _faultExecId = value;
                RaisePropertyChanged(FaultExecIdPropertyName);
            }
        }

        
        /// <summary>
        /// The <see cref="RunExecDetailId" /> property's name.
        /// </summary>
        public const string RunExecDetailIdPropertyName = "RunExecDetailId";

        private int _runExecDetailId = 0;

        /// <summary>
        /// Sets and gets the RunExecDetailId property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int RunExecDetailId
        {
            get
            {
                return _runExecDetailId;
            }

            set
            {
                if (_runExecDetailId == value)
                {
                    return;
                }

                RaisePropertyChanging(RunExecDetailIdPropertyName);
                _runExecDetailId = value;
                RaisePropertyChanged(RunExecDetailIdPropertyName);
            }
        }

        
        /// <summary>
        /// The <see cref="Comment" /> property's name.
        /// </summary>
        public const string CommentPropertyName = "Comment";

        private string _comment = string.Empty;

        /// <summary>
        /// Sets and gets the Comment property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Comment
        {
            get
            {
                return _comment;
            }

            set
            {
                if (_comment == value)
                {
                    return;
                }

                RaisePropertyChanging(CommentPropertyName);
                _comment = value;
                RaisePropertyChanged(CommentPropertyName);
                if (Comment != null)
                {
                    PaqueteMSJ paqueteTemp = new PaqueteMSJ();
                    paqueteTemp.Informacion = Comment;
                    paqueteTemp.NombrePropiedad = "Comment";
                    Messenger.Default.Send<PaqueteMSJ>(paqueteTemp, "SET_FAULTEXECUTIONPROPERTY_IN_RUNEXECUTIONFAULTEXCEPTIONMAINVM_FROMCONSTRUCTOR");
                }
            }
        }

        
        /// <summary>
        /// The <see cref="Image" /> property's name.
        /// </summary>
        public const string ImagePropertyName = "Image";

        private byte[] _image = new byte[0];

        /// <summary>
        /// Sets and gets the Image property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public byte[] Image
        {
            get
            {
                return _image;
            }

            set
            {
                if (_image == value)
                {
                    return;
                }

                RaisePropertyChanging(ImagePropertyName);
                _image = value;
                RaisePropertyChanged(ImagePropertyName);
            }
        }

        #endregion

    }
}
