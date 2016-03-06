using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using MadNeptun.DistributedSystemManager.Core.Objects;

namespace MadNeptun.DistributedSystemManager.Service.ComunicationService
{
    internal class SCSServiceHost : ServiceHost
    {
        internal SCSServiceHost(Node<int, string> node, string baseAddress)
            : base(typeof(SystemCommunicationService), new Uri(baseAddress))
        {
            foreach (var cd in this.ImplementedContracts.Values)
            {
                cd.Behaviors.Add(new SCSInstanceProvider(node));
            }
        }
    }
}
