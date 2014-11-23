using MadNeptun.DistributedSystemManager.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadNeptun.DistributedSystemManager.VisualSimulator
{
    abstract class BaseNetwork
    {
        public abstract string Name();

        public abstract List<Node> GetNetwork(DistributedAlgorithm algorithm, NetworkComponent component, MadNeptun.DistributedSystemManager.Core.Objects.Node.NodeMessage function);
    }
}
