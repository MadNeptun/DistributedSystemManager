using System;
using System.ServiceModel.Channels;
using System.Windows;
using System.Windows.Threading;

namespace MadNeptun.DistributedSystemManager.AdministratorPanel
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            base.OnStartup(e);
        }

        private void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
        {
            MessageBox.Show(unhandledExceptionEventArgs.ExceptionObject.ToString());
        }
    }
}
