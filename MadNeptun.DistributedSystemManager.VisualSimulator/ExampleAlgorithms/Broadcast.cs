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

        public override OperationResult<int, string> Init(Message<string> message, IEnumerable<NodeId<int>> neighbors, NodeId<int> current)
        {
            Status = State.Sent;
            return new OperationResult<int, string>() { SendTo = neighbors.Select(f => new KeyValuePair<NodeId<int>,Message<string>>(f, message)).ToList() };
        }

        public override OperationResult<int, string> RecieveMessage(Message<string> message, NodeId<int> sender, IEnumerable<NodeId<int>> neighbors, NodeId<int> current)
        {
            if (Status == State.Sent)
                return new OperationResult<int, string>() { SendTo = new List<KeyValuePair<NodeId<int>, Message<string>>>() };
            else
            {
                Status = State.Sent;
                return new OperationResult<int, string>() { SendTo = neighbors.Where(f=>f.Id != sender.Id).Select(f => new KeyValuePair<NodeId<int>, Message<string>>(f, message)).ToList() };
            }
        }

        public override string ToString()
        {
            return "Broadcast message";
        }
    }
}
