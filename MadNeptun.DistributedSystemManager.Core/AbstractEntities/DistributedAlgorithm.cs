using System.Collections.Generic;
using MadNeptun.DistributedSystemManager.Core.Objects;

namespace MadNeptun.DistributedSystemManager.Core.AbstractEntities
{
    public abstract class DistributedAlgorithm<TIdType, TValue>
    {
        public abstract OperationResult<TIdType, TValue> Init(Message<TValue> message, IEnumerable<NodeId<TIdType>> neighbors, NodeId<TIdType> current);

        public abstract OperationResult<TIdType, TValue> ReceiveMessage(Message<TValue> message, NodeId<TIdType> sender, IEnumerable<NodeId<TIdType>> neighbors, NodeId<TIdType> current);
    }
}
