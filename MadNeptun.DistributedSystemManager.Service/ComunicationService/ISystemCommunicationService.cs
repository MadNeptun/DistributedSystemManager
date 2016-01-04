using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Xml;

namespace MadNeptun.DistributedSystemManager.Service.ComunicationService
{
    [ServiceContract(Namespace = "http://SystemCommunicationService")]
    interface ISystemCommunicationService<TIdType, TValueType>
    {
        [OperationContract]
        void Receive(MadNeptun.DistributedSystemManager.Core.Objects.Message<TValueType> message,
            MadNeptun.DistributedSystemManager.Core.Objects.NodeId<TIdType> sender);

        [OperationContract]
        void Init(TValueType message);

        [OperationContract]
        void ClearExpiredAlorithms();
    }
}
