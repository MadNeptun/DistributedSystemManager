using System.ComponentModel;
using System.ServiceProcess;

namespace MadNeptun.DistributedSystemManager.Service
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DmsProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.DmsServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // DmsProcessInstaller
            // 
            this.DmsProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.DmsProcessInstaller.Password = null;
            this.DmsProcessInstaller.Username = null;
            // 
            // DmsServiceInstaller
            // 
            this.DmsServiceInstaller.Description = "Service that hosts node infractructure";
            this.DmsServiceInstaller.DisplayName = "DMS Node Host";
            this.DmsServiceInstaller.ServiceName = "NetworkService";
            this.DmsServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.DmsProcessInstaller,
            this.DmsServiceInstaller});

        }

        #endregion

        private ServiceProcessInstaller DmsProcessInstaller;
        private ServiceInstaller DmsServiceInstaller;


    }
}