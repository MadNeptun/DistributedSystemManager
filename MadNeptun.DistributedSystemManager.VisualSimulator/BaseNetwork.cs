using System.Collections.Generic;
using MadNeptun.DistributedSystemManager.Core.AbstractEntities;
using MadNeptun.DistributedSystemManager.Core.Objects;

namespace MadNeptun.DistributedSystemManager.VisualSimulator
{
    abstract class BaseNetwork
    {
        public abstract string Name();

        public abstract List<Node<string, string>> GetNetwork(DistributedAlgorithm<string, string> algorithm, NetworkComponent<string, string> component, Node<string, string>.NodeMessage function);
    }
}
