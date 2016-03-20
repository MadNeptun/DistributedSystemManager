using System.ServiceModel;
using MadNeptun.DistributedSystemManager.Core.Objects;

namespace MadNeptun.DistributedSystemManager.Service.ComunicationService
{
    [ServiceContract(Namespace = "http://SystemCommunicationService")]
    interface ISystemCommunicationService
    {
        [OperationContract(IsOneWay = true)]
        void Receive(Message<string> message,
            NodeId<int> sender);

        [OperationContract(IsOneWay = true)]
        void Init(string message);

        [OperationContract(IsOneWay = true)]
        void ClearExpiredAlorithms();

        [OperationContract]
        bool Alive();

        [OperationContract]
        void Reconfigure(string[] configuration);

        [OperationContract]
        void SaveFileOnNode(string fullPathToSaveFile, byte[] file);

        [OperationContract]
        string[] GetConfiguration();
    }
}
