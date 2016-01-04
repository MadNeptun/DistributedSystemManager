using System.Collections.Generic;
using MadNeptun.DistributedSystemManager.Core.Objects;

namespace MadNeptun.DistributedSystemManager.Core.AbstractEntities
{
    public abstract class NetworkComponent<TIdType, TValue>
    {
        public abstract void Run(string configuration);

        public abstract void ShutDown();

        public abstract void Send(List<KeyValuePair<NodeId<TIdType>,Message<TValue>>> data, NodeId<TIdType> sender);

        public delegate void MessageReceived(object sender, MessageReceivedEventArgs<TIdType, TValue> e);

        public event MessageReceived OnMessageReceived;

        /// <summary>
        /// Warning! After recieving and formatting data function must call InformNode method in order to relay this message to node.
        /// </summary>
        public abstract void Receive(Message<TValue> message, NodeId<TIdType> sender);

        protected void InformNode(MessageReceivedEventArgs<TIdType, TValue> data)
        {
            if(OnMessageReceived!=null)
                OnMessageReceived(this, data); 
        }
    }
}
