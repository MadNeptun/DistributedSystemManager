using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using MadNeptun.DistributedSystemManager.Core.AbstractEntities;
using MadNeptun.SoapService;

namespace MadNeptun.ExampleImplementation.ServiceHostInfrastructure
{
    internal class HttpServiceHost : ServiceHost
    {
        internal HttpServiceHost(NetworkComponent<int, string> component, string baseAddress)
            : base(typeof(SoapComunicationService), new Uri(baseAddress))
        {
            foreach (var cd in this.ImplementedContracts.Values)
            {
                cd.Behaviors.Add(new HttpServiceInstanceProvider(component));
            }
        }
    }
}
