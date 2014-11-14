using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadNeptun.DistributedSystemManager.Core.Objects
{
    public class NodeMessageEventArgs : EventArgs
    {
        public string Message { get; set; }
    }
}
