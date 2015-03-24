using System;
using System.Collections.Generic;
using System.Linq;
using MadNeptun.DistributedSystemManager.Core.AbstractEntities;
using MadNeptun.DistributedSystemManager.Core.Objects;

namespace MadNeptun.DistributedSystemManager.VisualSimulator.ExampleAlgorithms
{
    class DistanceFromStart : DistributedAlgorithm<string,string>
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

        public override OperationResult<string,string> Init(Message<string> message, IEnumerable<NodeId<string>> neighbors)
        {
            Status = State.Sent;
            var result = new OperationResult<string, string> {Message = message};
            result.Message.Value = "1";
            result.SendTo = neighbors.ToList();
            return result;
        }

        public override OperationResult<string,string> RecieveMessage(Message<string> message, NodeId<string> sender, IEnumerable<NodeId<string>> neighbors)
        {
            if(Status == State.Sent)
            {
                return new OperationResult<string,string>() { SendTo = new List<NodeId<string>>(), Message = message };
            }
            else
            {
                Status = State.Sent;
                var number = Int32.Parse(message.Value) + 1;
                var msg = new Message<string>() { Value = number.ToString() };
                return new OperationResult<string,string>() { SendTo = neighbors.Where(n => n.Id != sender.Id).ToList(), Message = msg };
            }
        }

        public override string ToString()
        {
            return "Distance from start";
        }

    }
}
