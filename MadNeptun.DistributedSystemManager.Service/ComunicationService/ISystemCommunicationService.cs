using System.ServiceModel;
using MadNeptun.DistributedSystemManager.Core.Objects;

namespace MadNeptun.DistributedSystemManager.Service.ComunicationService
{
    [ServiceContract(Namespace = "http://SystemCommunicationService")]
    interface ISystemCommunicationService<TIdType, TValueType>
    {
        [OperationContract]
        void Receive(Message<TValueType> message,
            NodeId<TIdType> sender);

        [OperationContract]
        void Init(TValueType message);

        [OperationContract]
        void ClearExpiredAlorithms();
    }
}
