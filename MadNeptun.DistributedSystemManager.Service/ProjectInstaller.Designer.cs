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
            this.DsmNodeService = new System.ServiceProcess.ServiceInstaller();
            // 
            // DsmNodeService
            // 
            this.DsmNodeService.Description = "Service that hosts node of distributed network";
            this.DsmNodeService.DisplayName = "DSM Node Service";
            this.DsmNodeService.ServiceName = "NetworkService";
            this.DsmNodeService.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.DsmNodeService});

        }

        #endregion

        private ServiceInstaller DsmNodeService;

    }
}