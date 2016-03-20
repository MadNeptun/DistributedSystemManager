using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MadNeptun.DistributedSystemManager.Core.Objects;

namespace MadNeptun.SoapService
{
    [ServiceContract]
    public interface ISoapComunicationService
    {
        [OperationContract(IsOneWay = true)]
        void Recieve(NodeId<int> sender, Message<string> message);
    }

}
