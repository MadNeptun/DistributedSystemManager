using MadNeptun.DistributedSystemManager.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MadNeptun.DistributedSystemManager.VisualSimulator.ExampleAlgorithms
{
    class DistanceFromStart : DistributedAlgorithm
    {

        public DistanceFromStart()
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
            Status = State.Sent;
            var result = new OperationResult();
            result.Message = message;
            result.Message.Value = "1";
            result.SendTo = neighbors.ToList();
            return result;
        }

        public override OperationResult RecieveMessage(Message message, NodeId sender, IEnumerable<NodeId> neighbors)
        {
            if(Status == State.Sent)
            {
                return new OperationResult() { SendTo = new List<NodeId>(), Message = message };
            }
            else
            {
                Status = State.Sent;
                int number = Int32.Parse(message.Value);
                number++;
                var msg = new Message() { Value = number.ToString() };
                return new OperationResult() { SendTo = neighbors.Where(n => n.Id != sender.Id).ToList(), Message = msg };
            }
        }

        public override string ToString()
        {
            return "Distance from start";
        }

    }
}
