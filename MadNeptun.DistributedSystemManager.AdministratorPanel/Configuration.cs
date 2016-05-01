using System.Collections.Generic;

namespace MadNeptun.DistributedSystemManager.AdministratorPanel
{
    public class Configuration
    {
        public string LogServiceAddress { get; set; }

        public bool UseLogFile { get; set; }

        public List<Node> Nodes { get; set; } 
    }
}
