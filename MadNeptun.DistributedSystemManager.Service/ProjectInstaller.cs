﻿using System.ComponentModel;
using System.Configuration.Install;

namespace MadNeptun.DistributedSystemManager.Service
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }
    }
}
