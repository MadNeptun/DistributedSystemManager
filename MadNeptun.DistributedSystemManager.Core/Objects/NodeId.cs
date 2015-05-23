using System.Collections.Generic;

namespace MadNeptun.DistributedSystemManager.Core.Objects
{
    public class NodeId<TIdType>
    {
        public TIdType Id { get; set; }

        private readonly Dictionary<string, string> _connectionConfiguration = new Dictionary<string, string>();

        public Dictionary<string, string> ConnectionConfiguration {
            get { return _connectionConfiguration; }
        } 
    }
}
