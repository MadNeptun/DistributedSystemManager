using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MadNeptun.DistributedSystemManager.Core.AbstractEntities;
using MadNeptun.DistributedSystemManager.Core.Objects;

namespace MadNeptun.ExampleImplementation
{
    public class Algorithm : DistributedAlgorithm<int,string>
    {
        public Algorithm()
        {
            _state = State.Idle;
            _sentMessageCount = 0;
            _returnMessageCount = 0;
            _messages = new List<string>();
        }

        private int _sender;

        private List<string> _messages; 

        private int _sentMessageCount;
        private int _returnMessageCount;

        private State _state;

        enum State
        {
            Idle,
            Relay,
            Sent
        }
        public override OperationResult<int, string> Init(Message<string> message, IEnumerable<NodeId<int>> neighbors, NodeId<int> current)
        {
            if (State.Idle == _state)
            {
                _state = State.Relay;
                _sender = 0;
                _sentMessageCount += neighbors.Count();
                return new OperationResult<int, string>() { SendTo = neighbors.Select( n=> new KeyValuePair<NodeId<int>,Message<string>>(n,message)).ToList() };
            }
            return new OperationResult<int, string>() {SendTo = new List<KeyValuePair<NodeId<int>, Message<string>>>()};
        }

        public override OperationResult<int, string> RecieveMessage(Message<string> message, NodeId<int> sender, IEnumerable<NodeId<int>> neighbors, NodeId<int> current)
        {
            if (State.Idle == _state)
            {
                _state = State.Relay;
                _sender = sender.Id;
                _sentMessageCount = neighbors.Count() - 1;
                if (_sentMessageCount == 0)
                {
                    _state = State.Sent;
                    message.Value = ProduceResult(message.Value,current.Id);
                    return new OperationResult<int, string>() {SendTo = new List<KeyValuePair<NodeId<int>, Message<string>>>() { new KeyValuePair<NodeId<int>, Message<string>>(neighbors.First(),message)}};
                }
            }
            else if(State.Relay == _state)
            {
                _returnMessageCount++;
                _messages.Add(message.Value);
                if (_sender == 0)
                {
                    if (_sentMessageCount == _returnMessageCount)
                    {
                        _state = State.Sent;
                        File.WriteAllLines("C:\\Temp\\result.txt",_messages);
                    }
                }
                else
                {
                    if (_sentMessageCount == _returnMessageCount)
                    {
                        _state = State.Sent;
                        var result = "";
                        foreach (var msg in _messages)
                        {
                            result += ";" + msg;
                        }
                        result = result.Substring(1);
                        message.Value = result;
                        return new OperationResult<int, string>() { SendTo = new List<KeyValuePair<NodeId<int>, Message<string>>>() { new KeyValuePair<NodeId<int>, Message<string>>(neighbors.First(n=>n.Id == _sender),message)} };
                    }
                }
            }
            return new OperationResult<int, string>() { SendTo = new List<KeyValuePair<NodeId<int>, Message<string>>>() };
        }

        private string ProduceResult(string p, int id)
        {
            return (Int32.Parse(p)*id).ToString();
        }
    }
}
