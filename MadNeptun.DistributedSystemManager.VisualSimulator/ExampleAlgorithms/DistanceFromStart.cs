using System;
using System.Collections.Generic;
using System.Linq;
using MadNeptun.DistributedSystemManager.Core.AbstractEntities;
using MadNeptun.DistributedSystemManager.Core.Objects;

namespace MadNeptun.DistributedSystemManager.VisualSimulator.ExampleAlgorithms
{
    class DistanceFromStart : DistributedAlgorithm<int,string>
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

        public override OperationResult<int, string> Init(Message<string> message, IEnumerable<NodeId<int>> neighbors, NodeId<int> current)
        {
            Status = State.Sent;
            var result = new OperationResult<int, string>();
            message.Value = "1";
            result.SendTo = neighbors.Select( f => new KeyValuePair<NodeId<int>,Message<string>>(f,message)).ToList();
            return result;
        }

        public override OperationResult<int, string> ReceiveMessage(Message<string> message, NodeId<int> sender, IEnumerable<NodeId<int>> neighbors, NodeId<int> current)
        {
            if(Status == State.Sent)
            {
                return new OperationResult<int, string>() { SendTo = new List<KeyValuePair<NodeId<int>, Message<string>>>() };
            }
            else
            {
                Status = State.Sent;
                var number = Int32.Parse(message.Value) + 1;
                message.Value = number.ToString();
                return new OperationResult<int, string>() { SendTo = neighbors.Where(n => n.Id != sender.Id).Select(f=> new KeyValuePair<NodeId<int>,Message<string>>(f,message)).ToList() };
            }
        }

        public override string ToString()
        {
            return "Distance from start";
        }

    }
}
