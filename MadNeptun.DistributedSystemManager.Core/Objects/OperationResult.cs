using System.Collections.Generic;

namespace MadNeptun.DistributedSystemManager.Core.Objects
{
    public class OperationResult<TIdType, TValue>
    {
        public List<KeyValuePair<NodeId<TIdType>, Message<TValue>>> SendTo { get; set; }
    }
}
