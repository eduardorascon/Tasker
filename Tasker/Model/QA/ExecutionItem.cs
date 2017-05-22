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
    public class ExecutionItem : ModelBaseEx
    {
        #region Fields
        Functions _oFx = new Functions();
        public int ExecId;
        public string Description;
        public DateTime CreatedDate;
        public string Status;
        public bool EnableModify;
        public double Porcent;
        #endregion

        #region Constructor

        public ExecutionItem()
        {

        }

        public ExecutionItem(int execId, string description, DateTime createdDate, string status, bool enableModify, double porcent)
        {
            ExecId = execId;
            Description = description;
            CreatedDate = createdDate;
            Status = status;
            EnableModify = enableModify;
            Porcent = porcent;
        }

        public ExecutionItem(int execId, string description, DateTime createdDate, string status, bool enableModify)
        {
            ExecId = execId;
            Description = description;
            CreatedDate = createdDate;
            Status = status;
            EnableModify = enableModify;
        }
        
        #endregion

    }
}
