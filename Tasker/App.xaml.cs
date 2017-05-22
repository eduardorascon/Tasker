using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using FirstFloor.ModernUI.Presentation;
using GalaSoft.MvvmLight.Threading;
using Tasker.Configuracion;
using Tasker.Helpers;

namespace Tasker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        static App()
        {
            DispatcherHelper.Initialize();

            WindowsIdentity winUser =
            WindowsIdentity.GetCurrent();

            InitializeAutoMapper.InitializarAutoMapper();

            if (winUser != null)
            {
                AppVariables.SetValue("WinUser", (winUser.Name).Split('\\')[1]);
            }


        }
    }
}
