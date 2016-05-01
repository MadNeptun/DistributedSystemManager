﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using MadNeptun.DistributedSystemManager.Core.Objects;

namespace MadNeptun.ExampleImplementation.SoapService {
    
    
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    [ServiceContract(ConfigurationName="SoapService.ISoapComunicationService")]
    public interface ISoapComunicationService {
        
        [OperationContract(IsOneWay=true, Action="http://tempuri.org/ISoapComunicationService/Recieve")]
        void Recieve(NodeId<int> sender, Message<string> message);
    }
    
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    public interface ISoapComunicationServiceChannel : ISoapComunicationService, IClientChannel {
    }
    
    [DebuggerStepThrough()]
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    public partial class SoapComunicationServiceClient : ClientBase<ISoapComunicationService>, ISoapComunicationService {
        
        public SoapComunicationServiceClient() {
        }
        
        public SoapComunicationServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SoapComunicationServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SoapComunicationServiceClient(string endpointConfigurationName, EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SoapComunicationServiceClient(Binding binding, EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void Recieve(NodeId<int> sender, Message<string> message) {
            base.Channel.Recieve(sender, message);
        }
    }
}
