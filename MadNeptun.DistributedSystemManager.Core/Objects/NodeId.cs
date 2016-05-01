using System.Collections.Generic;
using System.Xml.Serialization;

namespace MadNeptun.DistributedSystemManager.Core.Objects
{
    public class NodeId<TIdType>
    {
        public TIdType Id { get; set; }

        [XmlIgnore]
        private readonly Dictionary<string, string> _connectionConfiguration = new Dictionary<string, string>();

        [XmlIgnore]
        public Dictionary<string, string> ConnectionConfiguration {
            get { return _connectionConfiguration; }
        } 
    }
}
