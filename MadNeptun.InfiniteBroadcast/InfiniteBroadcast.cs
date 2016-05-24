using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MadNeptun.DistributedSystemManager.Core.AbstractEntities;
using MadNeptun.DistributedSystemManager.Core.Objects;

namespace MadNeptun.InfiniteBroadcast
{
    public class InfiniteBroadcast : DistributedAlgorithm<int,string>
    {
        public override DistributedSystemManager.Core.Objects.OperationResult<int, string> Init(DistributedSystemManager.Core.Objects.Message<string> message, IEnumerable<DistributedSystemManager.Core.Objects.NodeId<int>> neighbors, DistributedSystemManager.Core.Objects.NodeId<int> current)
        {
            return new OperationResult<int, string>() { SendTo = neighbors.Select(e => new KeyValuePair<NodeId<int>, Message<string>>(e, message)).ToList() };
        }

        public override DistributedSystemManager.Core.Objects.OperationResult<int, string> ReceiveMessage(DistributedSystemManager.Core.Objects.Message<string> message, DistributedSystemManager.Core.Objects.NodeId<int> sender, IEnumerable<DistributedSystemManager.Core.Objects.NodeId<int>> neighbors, DistributedSystemManager.Core.Objects.NodeId<int> current)
        {
            return new OperationResult<int, string>() { SendTo = neighbors.Select( e=> new KeyValuePair<NodeId<int>, Message<string>>(e,message)).ToList() };
        }
    }
}
