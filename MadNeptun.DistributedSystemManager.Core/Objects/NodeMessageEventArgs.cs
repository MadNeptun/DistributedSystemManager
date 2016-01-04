using System;

namespace MadNeptun.DistributedSystemManager.Core.Objects
{
    public class NodeMessageEventArgs<TIdType, TValue> : EventArgs
    {
        public TValue Message { get; set; }

        public NodeId<TIdType> Sender { get; set; }

        public NodeId<TIdType> Receiver { get; set; }
    }
}
