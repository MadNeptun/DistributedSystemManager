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
        private readonly List<Node> _nodes;

        private readonly Thread _workerThread;

        private bool _checkStatus;

        public NodesConnectionManager(List<Node> currentNodes)
        {
            _checkStatus = false;
            _nodes = currentNodes;
            
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
                        var client = GetWcfClient(node);
                        try
                        {
                            if (client.Alive())
                                node.Alive();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                            node.Dead();
                        }
                        finally
                        {
                            if(client!=null)
                                client.Close();
                        }
                    }
                }
            }
        }

        private SystemCommunicationServiceClient GetWcfClient(Node node)
        {
            return new SystemCommunicationServiceClient("SCS", node.NodeAddress);
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
                node.ClearStatus();
            }
        }

        internal void SendInitToActiveNode(Node node, string message)
        {
            var client = GetWcfClient(_nodes.First(e => e == node));
            try
            {
                client.Init(message);
                MessageBox.Show("Init message sent.");
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to send init message.");
            }
            finally
            {
                if(client != null)
                    client.Close();
            }
            
        }
    }
}
