using System;
using GalaSoft.MvvmLight.Messaging;

namespace Tasker.Helpers
{
    public static class  TaskerHelper
    {
        public static bool SetStatusBarMessage(string message)
        {
            //Sending the message
            Messenger.Default.Send<String>(message,"STATUS_BAR_MESSAGE");
            return true;
        }


    }
}
