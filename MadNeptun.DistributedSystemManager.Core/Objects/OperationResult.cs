using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadNeptun.DistributedSystemManager.Core.Objects
{
    public class OperationResult
    {
        public List<NodeId> SendTo { get; set; }

        public Message Message { get; set; }
    }
}
