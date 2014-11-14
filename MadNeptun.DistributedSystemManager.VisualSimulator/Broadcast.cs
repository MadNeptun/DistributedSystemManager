using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadNeptun.DistributedSystemManager.Core.Objects;
namespace MadNeptun.DistributedSystemManager.VisualSimulator
{
    class Broadcast : DistributedAlgorithm
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

        public override OperationResult Init(Message message, IEnumerable<NodeId> neighbors)
        {
            Status = Broadcast.State.Sent;
            return new OperationResult() { SendTo = neighbors.ToList(), Message = message };
        }

        public override OperationResult RecieveMessage(Message message, NodeId sender, IEnumerable<NodeId> neighbors)
        {
            if (Status == State.Sent)
                return new OperationResult() { SendTo = new List<NodeId>(), Message = message };
            else
            {
                Status = State.Sent;
                return new OperationResult() { SendTo = neighbors.Where(n => n.Id != sender.Id).ToList(), Message = message };
            }
        }
    }
}
