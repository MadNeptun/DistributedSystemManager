using System.Collections.Generic;
using System.Linq;
using MadNeptun.DistributedSystemManager.Core.AbstractEntities;
using MadNeptun.DistributedSystemManager.Core.Objects;

namespace MadNeptun.DistributedSystemManager.VisualSimulator.ExampleAlgorithms
{
    class Broadcast : DistributedAlgorithm<int, string>
    {
        public Broadcast()
        {
            Status = State.Idle;
        }
        enum State
        {
            Idle,
            Sent
        }

        private State Status { get; set; }

        public override OperationResult<int, string> Init(Message<string> message, IEnumerable<NodeId<int>> neighbors)
        {
            Status = State.Sent;
            return new OperationResult<int, string>() { SendTo = neighbors.ToList(), Message = message };
        }

        public override OperationResult<int, string> RecieveMessage(Message<string> message, NodeId<int> sender, IEnumerable<NodeId<int>> neighbors)
        {
            if (Status == State.Sent)
                return new OperationResult<int, string>() { SendTo = new List<NodeId<int>>(), Message = message };
            else
            {
                Status = State.Sent;
                return new OperationResult<int, string>() { SendTo = neighbors.Where(n => n.Id != sender.Id).ToList(), Message = message };
            }
        }

        public override string ToString()
        {
            return "Broadcast message";
        }
    }
}
