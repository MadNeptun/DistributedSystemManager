using System.Collections.Generic;

namespace MadNeptun.DistributedSystemManager.Core.Objects
{
    public class OperationResult<TIdType, TValue>
    {
        public List<NodeId<TIdType>> SendTo { get; set; }

        public Message<TValue> Message { get; set; }
    }
}
