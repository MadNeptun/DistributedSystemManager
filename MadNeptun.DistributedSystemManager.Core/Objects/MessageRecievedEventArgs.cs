using System;

namespace MadNeptun.DistributedSystemManager.Core.Objects
{
    public class MessageReceivedEventArgs<TIdType, TValue> : EventArgs
    {
        public Message<TValue> Message { get; set; }

        public NodeId<TIdType> NodeId { get; set; }
    }
}
