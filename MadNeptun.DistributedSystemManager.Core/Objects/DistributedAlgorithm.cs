using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MadNeptun.DistributedSystemManager.Core.Objects
{
    public abstract class DistributedAlgorithm
    {
        public abstract OperationResult Init(Message message, IEnumerable<NodeId> neighbors);

        public abstract OperationResult RecieveMessage(Message message, NodeId sender, IEnumerable<NodeId> neighbors);
    }
}
