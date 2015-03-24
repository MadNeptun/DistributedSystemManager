using System.Collections.Generic;
using System.Linq;
using MadNeptun.DistributedSystemManager.Core.AbstractEntities;
using MadNeptun.DistributedSystemManager.Core.Objects;

namespace MadNeptun.DistributedSystemManager.VisualSimulator.ExampleAlgorithms
{
    class Broadcast : DistributedAlgorithm<string, string>
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

        public override OperationResult<string, string> Init(Message<string> message, IEnumerable<NodeId<string>> neighbors)
        {
            Status = State.Sent;
            return new OperationResult<string, string>() { SendTo = neighbors.ToList(), Message = message };
        }

        public override OperationResult<string, string> RecieveMessage(Message<string> message, NodeId<string> sender, IEnumerable<NodeId<string>> neighbors)
        {
            if (Status == State.Sent)
                return new OperationResult<string, string>() { SendTo = new List<NodeId<string>>(), Message = message };
            else
            {
                Status = State.Sent;
                return new OperationResult<string, string>() { SendTo = neighbors.Where(n => n.Id != sender.Id).ToList(), Message = message };
            }
        }

        public override string ToString()
        {
            return "Broadcast message";
        }
    }
}
