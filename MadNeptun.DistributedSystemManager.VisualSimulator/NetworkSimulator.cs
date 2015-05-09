using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using MadNeptun.DistributedSystemManager.Core.AbstractEntities;
using MadNeptun.DistributedSystemManager.Core.Objects;

namespace MadNeptun.DistributedSystemManager.VisualSimulator
{
    class NetworkSimulator : NetworkComponent<string, string>
    {
        public override void Send(Message<string> message, List<NodeId<string>> recievers, NodeId<string> sender)
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
            ((NetworkComponent<string, string>)data[0]).Recieve((Message<string>)data[2], (NodeId<string>)data[1]);
        }

        public NodeId<string> CurrentNodeId { private get; set; }

        public override void Recieve(Message<string> message, NodeId<string> sender)
        {
            var senderColor = MainForm.Objects.First(o => o.Id == Int32.Parse(sender.Id)).BgColor;
            var resultColor = MainForm.Colors.Contains(senderColor) ? Color.FromArgb(Math.Min(255, senderColor.R + 120), Math.Min(255, senderColor.G + 120), Math.Min(255, senderColor.B + 120)) : senderColor;
            lock (MainForm.VisitedNodes)
            {
                MainForm.VisitedNodes.Add(new KeyValuePair<int, Color>(Int32.Parse(CurrentNodeId.Id), resultColor));
            }
            InformNode(new MessageRecievedEventArgs<string, string>() { Message = message, NodeId = sender });
        }

        public override void Run(string configuration)
        {
            
        }

        public override void ShutDown()
        {
            
        }
    }
}
