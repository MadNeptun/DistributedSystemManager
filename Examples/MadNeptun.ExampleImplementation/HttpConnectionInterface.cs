﻿using System;
using System.Collections.Generic;
using System.ServiceModel;
using MadNeptun.DistributedSystemManager.Core.AbstractEntities;
using MadNeptun.DistributedSystemManager.Core.Objects;
using MadNeptun.ExampleImplementation.ServiceHostInfrastructure;
using MadNeptun.ExampleImplementation.SoapService;

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

            _serviceHost = new HttpServiceHost(this, configuration);
            _serviceHost.Open();
        }

        public override void Send(List<KeyValuePair<NodeId<int>, Message<string>>> data, NodeId<int> sender)
        {

            foreach (var package in data)
            {
                try
                {
                    var address = new EndpointAddress(package.Key.ConnectionConfiguration["url"]);
                    var client = new SoapComunicationServiceClient("SCS", address);
                    client.Recieve(package.Key,package.Value);
                    client.Close();
                }
                catch (Exception ex)
                {
                    EventLogHelper.WriteErrorToEventLog(ex);
                }
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
