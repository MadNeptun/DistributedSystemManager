using System.Collections.Generic;
using MadNeptun.DistributedSystemManager.Core.Objects;

namespace MadNeptun.DistributedSystemManager.Core.AbstractEntities
{
    public abstract class NetworkComponent<TIdType, TValue>
    {
        public abstract void Run(string configuration);

        public abstract void ShutDown();

        public abstract void Send(Message<TValue> message, List<NodeId<TIdType>> recievers, NodeId<TIdType> sender);

        public delegate void MessageRecieved(object sender, MessageRecievedEventArgs<TIdType, TValue> e);

        public event MessageRecieved OnMessageRecieved;

        /// <summary>
        /// Warning! After recieving and formatting data function must call InformNode method in order to relay this message to node.
        /// </summary>
        public abstract void Recieve(Message<TValue> message, NodeId<TIdType> sender);

        protected void InformNode(MessageRecievedEventArgs<TIdType, TValue> data)
        {
            if(OnMessageRecieved!=null)
                OnMessageRecieved(this, data); 
        }
    }
}
