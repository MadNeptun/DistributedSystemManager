using System;

namespace MadNeptun.DistributedSystemManager.Core.Objects
{
    public class Message<TValue>
    {
        public TValue Value { get; set; }

        public Guid ExecutionId { get; set; }
    }
}
