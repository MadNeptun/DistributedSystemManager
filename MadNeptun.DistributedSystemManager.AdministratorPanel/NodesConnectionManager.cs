using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using MadNeptun.DistributedSystemManager.AdministratorPanel.SystemService;

namespace MadNeptun.DistributedSystemManager.AdministratorPanel
{
    public class NodesConnectionManager : IDisposable
    {
        private readonly List<KeyValuePair<Node,SystemCommunicationServiceClient>> _nodes;

        private readonly Thread _workerThread;

        private bool _checkStatus;

        public NodesConnectionManager(List<Node> currentNodes)
        {
            _checkStatus = false;
            _nodes = new List<KeyValuePair<Node, SystemCommunicationServiceClient>>();
            foreach (var node in currentNodes)
            {
                _nodes.Add(new KeyValuePair<Node, SystemCommunicationServiceClient>(node, GetWcfClient(node)));
            }
            _workerThread = new Thread(ThreadMethod);
            _workerThread.Start();
        }

        private void ThreadMethod()
        {
            while (true)
            {
                Thread.Sleep(4500);
                if (_checkStatus)
                {
                    foreach (var node in _nodes)
                    {
                        try
                        {
                            if (node.Value.Alive())
                                node.Key.Alive();
                        }
                        catch (Exception)
                        {
                            node.Key.Dead();
                        }
                    }
                }
            }
        }

        private SystemCommunicationServiceClient GetWcfClient(Node node)
        {
            return new SystemCommunicationServiceClient("WSHttpBinding_ISystemCommunicationService", node.NodeAddress);
        }

        public void Dispose()
        {
            try
            {
                _workerThread.Abort();
            }
            catch (ThreadAbortException)
            {
                //continue
            }
           
            foreach (var node in _nodes)
            {
                node.Value.Close();
            }
        }

        internal void StartStatusCheck()
        {
            _checkStatus = true;
        }

        internal void StopStatusCheck()
        {
            _checkStatus = false;
            foreach (var node in _nodes)
            {
                node.Key.ClearStatus();
            }
        }

        internal void SendInitToActiveNode(Node node, string message)
        {
            try
            {
                _nodes.First(e => e.Key == node).Value.Init(message);
                MessageBox.Show("Init message sent.");
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to send init message.");
            }
            
        }
    }
}
