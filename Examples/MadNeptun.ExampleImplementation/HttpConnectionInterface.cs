using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using MadNeptun.DistributedSystemManager.Core.AbstractEntities;
using MadNeptun.DistributedSystemManager.Core.Objects;
using MadNeptun.ExampleImplementation.SoapWebservice;
using MadNeptun.SoapService;

namespace MadNeptun.ExampleImplementation
{
    class HttpConnectionInterface : NetworkComponent<int, string>
    {
        private ServiceHost _serviceHost;

        public override void Run(string configuration)
        {
            if (_serviceHost != null)
            {
                _serviceHost.Close();
                _serviceHost = null;
            }

            _serviceHost = new ServiceHost(typeof(HttpComunicationService));
            ((HttpComunicationService)_serviceHost.SingletonInstance).SetupNetworComponent(this);
            //todo setup endpoint configuration
            _serviceHost.Open();
        }

        public override void Send(List<KeyValuePair<NodeId<int>, Message<string>>> data, NodeId<int> sender)
        {
            foreach (var package in data.
                Select( 
                    e => new KeyValuePair<NodeIdOfInt32, MessageOfString> 
                        (
                        new NodeIdOfInt32() { Id = e.Key.Id },
                        new MessageOfString() { Value = e.Value.Value }
                        )
                    )
                )
            {
                var client = new HttpComunicationServiceSoapClient();
                //todo setup connection data 
                client.Recieve(package.Value, package.Key);
                client.Close();
            }
        }

        public override void ShutDown()
        {
            if (_serviceHost != null)
            {
                _serviceHost.Close();
                _serviceHost = null;
            }
        }

        public override void Receive(Message<string> message, NodeId<int> sender)
        {
            InformNode(new MessageReceivedEventArgs<int, string> { Message = message, NodeId = sender });
        }
    }
}
