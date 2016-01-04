using System.Collections.Generic;
using MadNeptun.DistributedSystemManager.Core.AbstractEntities;
using MadNeptun.DistributedSystemManager.Core.Objects;

namespace MadNeptun.DistributedSystemManager.VisualSimulator
{
    abstract class BaseNetwork
    {
        public abstract string Name();

        public abstract List<Node<int, string>> GetNetwork(DistributedAlgorithm<int, string> algorithm, NetworkComponent<int, string> component, Node<int, string>.NodeMessage function);
    }
}
