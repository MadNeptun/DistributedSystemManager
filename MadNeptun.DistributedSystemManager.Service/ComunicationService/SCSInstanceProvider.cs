using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using MadNeptun.DistributedSystemManager.Core.Objects;

namespace MadNeptun.DistributedSystemManager.Service.ComunicationService
{
    internal class SCSInstanceProvider : IInstanceProvider, IContractBehavior
    {
        private readonly Node<int,string> _node;

    public SCSInstanceProvider(Node<int,string> node)
    {
        _node = node;
    }

    public object GetInstance(InstanceContext instanceContext, Message message)
    {
        return this.GetInstance(instanceContext);
    }

    public object GetInstance(InstanceContext instanceContext)
    {
        return new SystemCommunicationService(_node);
    }

    public void ReleaseInstance(InstanceContext instanceContext, object instance)
    {
        var disposable = instance as IDisposable;
        if (disposable != null)
        {
            disposable.Dispose();
        }
    }

    public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
    {
    }

    public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
    {
    }

    public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
    {
        dispatchRuntime.InstanceProvider = this;
    }

    public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
    {
    }

    }
}
