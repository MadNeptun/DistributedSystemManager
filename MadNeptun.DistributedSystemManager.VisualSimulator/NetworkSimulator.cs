using MadNeptun.DistributedSystemManager.Core.Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MadNeptun.DistributedSystemManager.VisualSimulator
{
    class NetworkSimulator : NetworkComponent
    {
        public override void Send(Message message, List<NodeId> recievers, NodeId sender)
        {
                var data = NodesManager.Instance.Nodes.
                    Where(n => recievers.Select(s => s.Id).Contains(n.GetId().Id)).ToList();
                if (data.Count() > 0)
                {
                    foreach (var node in data)
                    {
                        Random r = new Random();
                        Thread.Sleep(r.Next(300, 2000));
                        Thread t = new Thread(Act);
                        t.Start(new List<object>() { node.GetNetworkComponent(), sender, message });
                        //t.Join(1);
                    }
                }
        }

        private static void Act(object args)
        {        
            var data = (List<object>)args;
            ((NetworkComponent)data[0]).Recieve((Message)data[2], (NodeId)data[1]);
        }

        public NodeId CurrentNodeId { get; set; }

        public override void Recieve(Message message, NodeId sender)
        {
            var senderColor = MainForm.Objects.First(o => o.Id == Int32.Parse(sender.Id)).BgColor;
            var resultColor = MainForm.Colors.Contains(senderColor) ? Color.FromArgb(Math.Min(255, senderColor.R + 120), Math.Min(255, senderColor.G + 120), Math.Min(255, senderColor.B + 120)) : senderColor;

            MainForm._visitedNodes.Add(new KeyValuePair<int, Color>(Int32.Parse(CurrentNodeId.Id), resultColor));
            this.InformNode(new MessageRecievedEventArgs() { Message = message, NodeId = sender });
        }
    }
}
