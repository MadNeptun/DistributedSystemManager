using System.ServiceModel;
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
