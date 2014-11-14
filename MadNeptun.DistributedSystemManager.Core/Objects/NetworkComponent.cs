using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadNeptun.DistributedSystemManager.Core.Objects
{
    public abstract class NetworkComponent
    {
        public abstract void Send(Message message, List<NodeId> recievers, NodeId sender);

        public delegate void MessageRecieved(object sender, MessageRecievedEventArgs e);

        public event MessageRecieved OnMessageRecieved;

        /// <summary>
        /// After recieving and formatting data function must call InformNode method in order to relay this message to node.
        /// </summary>
        public abstract void Recieve(Message message, NodeId sender);

        protected void InformNode(MessageRecievedEventArgs data)
        {
            if(OnMessageRecieved!=null)
                OnMessageRecieved(this, data); 
        }
    }
}
