using System;

namespace MadNeptun.DistributedSystemManager.Core.Objects
{
    public class NodeMessageEventArgs<TValue> : EventArgs
    {
        public TValue Message { get; set; }
    }
}
