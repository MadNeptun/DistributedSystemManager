using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MadNeptun.DistributedSystemManager.Core.AbstractEntities;
using MadNeptun.DistributedSystemManager.Core.Objects;

namespace MadNeptun.SoapService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SoapComunicationService" in both code and config file together.
    public class SoapComunicationService : ISoapComunicationService
    {
        private readonly NetworkComponent<int, string> _networkComponent; 

        public SoapComunicationService(NetworkComponent<int, string> networkComponent)
        {
            _networkComponent = networkComponent;
        }

        public void Recieve(NodeId<int> sender, Message<string> message)
        {
            if(_networkComponent != null)
                _networkComponent.Receive(message,sender);
        }
    }
}
