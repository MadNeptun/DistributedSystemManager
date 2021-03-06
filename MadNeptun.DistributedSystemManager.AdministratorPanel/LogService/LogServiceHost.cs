﻿using System;
using System.ServiceModel;

namespace MadNeptun.DistributedSystemManager.AdministratorPanel.LogService
{
    internal class LogServiceHost : ServiceHost
    {
        internal LogServiceHost(LogCollection collection, string baseAddress)
            : base(typeof(LogService), new Uri(baseAddress))
        {
            foreach (var cd in this.ImplementedContracts.Values)
            {
                cd.Behaviors.Add(new LogServiceProvider(collection));
            }
        }
    }
}
