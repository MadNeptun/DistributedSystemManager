using System.ServiceModel;
using MadNeptun.DistributedSystemManager.Core.Objects;

namespace MadNeptun.DistributedSystemManager.Service.ComunicationService
{
    [ServiceContract(Namespace = "http://SystemCommunicationService")]
    interface ISystemCommunicationService
    {
        [OperationContract]
        void Receive(Message<string> message,
            NodeId<int> sender);

        [OperationContract]
        void Init(string message);

        [OperationContract]
        void ClearExpiredAlorithms();
    }
}
