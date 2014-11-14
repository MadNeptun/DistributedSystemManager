using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadNeptun.DistributedSystemManager.Core.Objects
{
    public class MessageRecievedEventArgs : EventArgs
    {
        public Message Message { get; set; }

        public NodeId NodeId { get; set; }
    }
}
