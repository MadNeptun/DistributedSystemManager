using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using MadNeptun.DistributedSystemManager.Core.AbstractEntities;
using MadNeptun.DistributedSystemManager.Core.Objects;

namespace MadNeptun.DistributedSystemManager.VisualSimulator
{
    class NetworkSimulator : NetworkComponent<int, string>
    {
        public override void Send(Message<string> message, List<NodeId<int>> recievers, NodeId<int> sender)
        {
                var data = NodesManager.Instance.Nodes.
                    Where(n => recievers.Select(s => s.Id).Contains(n.GetId().Id)).ToList();
                if (data.Any())
                {
                    foreach (var node in data)
                    {
                        var r = new Random();
                        Thread.Sleep(r.Next(300, 2000));
                        var t = new Thread(Act);
                        t.Start(new List<object>() { node.GetNetworkComponent(), sender, message });
                    }
                }
        }

        private static void Act(object args)
        {        
            var data = (List<object>)args;
            ((NetworkComponent<int, string>)data[0]).Recieve((Message<string>)data[2], (NodeId<int>)data[1]);
        }

        public NodeId<int> CurrentNodeId { private get; set; }

        public override void Recieve(Message<string> message, NodeId<int> sender)
        {
            var senderColor = MainForm.Objects.First(o => o.Id == sender.Id).BgColor;
            var resultColor = MainForm.Colors.Contains(senderColor) ? Color.FromArgb(Math.Min(255, senderColor.R + 120), Math.Min(255, senderColor.G + 120), Math.Min(255, senderColor.B + 120)) : senderColor;
            lock (MainForm.VisitedNodes)
            {
                MainForm.VisitedNodes.Add(new KeyValuePair<int, Color>(CurrentNodeId.Id, resultColor));
            }
            InformNode(new MessageRecievedEventArgs<int, string>() { Message = message, NodeId = sender });
        }

        public override void Run(string configuration)
        {
            
        }

        public override void ShutDown()
        {
            
        }
    }
}
