using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Xml.Serialization;
using MadNeptun.DistributedSystemManager.Core.AbstractEntities;
using MadNeptun.DistributedSystemManager.Core.Objects;

namespace MadNeptun.ExampleImplementation
{
    public class SocketConnectionInterface : NetworkComponent<int,string>
    {
        private Socket _listenSocket;

        private Thread _listenerThread;

        private void Recieve()
        {
            while (true)
            {
                var thread = new Thread(HandleConnection);
                thread.Start(_listenSocket.Accept());
            }
        }

        private void HandleConnection(object socket)
        {
            Socket connectionSocket = (Socket) socket;
            var buffer = new byte[2048];
            var information = new List<byte>();
            while (true)
            {
                var fragmentLenght = connectionSocket.Receive(buffer, 2048, SocketFlags.None);
                information.AddRange(buffer);
                if (fragmentLenght < 2048)
                    break;
            }
            connectionSocket.Close();
            var r = ParseData(information);
            Recieve(r.Value, r.Key);
        }

        private static KeyValuePair<NodeId<int>, Message<string>> ParseData(List<byte> data)
        {
            SerializableMessage result;
            var serializer = new XmlSerializer(typeof(SerializableMessage));
            using (var memoryStream = new MemoryStream())
            {
                memoryStream.Write(data.ToArray(),0,data.Count);
                memoryStream.Seek(0, SeekOrigin.Begin);
                result = (SerializableMessage)serializer.Deserialize(memoryStream);
            }
            
            return new KeyValuePair<NodeId<int>, Message<string>>(new NodeId<int>() { Id = result.SenderId }, new Message<string>() {ExecutionId = result.Identity, Value = result.Message});
        }

        private static List<byte> ParseData(Message<string> message, NodeId<int> sender)
        {
            byte[] result;
            var data = new SerializableMessage()
            {
                Identity = message.ExecutionId,
                Message = message.Value,
                SenderId = sender.Id
            };         
            using (var memoryStream = new MemoryStream())
            {
                var serializer = new XmlSerializer(typeof (SerializableMessage));
                serializer.Serialize(memoryStream, data);
                result = memoryStream.ToArray();
            }
            return result.ToList();
        }

        public override void Recieve(Message<string> message, NodeId<int> sender)
        {
            InformNode(new MessageRecievedEventArgs<int, string>(){Message = message,NodeId = sender});
        }

        public override void Run(string configuration)
        {
            _listenSocket = new Socket(AddressFamily.InterNetwork,
                                         SocketType.Stream,
                                         ProtocolType.Tcp);
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            var ep = new IPEndPoint(ipAddress, Int32.Parse(configuration));
            _listenSocket.Bind(ep);
            _listenSocket.Listen(500);
            _listenerThread = new Thread(Recieve);
            _listenerThread.Start();
        }

        public override void Send(List<KeyValuePair<NodeId<int>, Message<string>>> data, NodeId<int> sender)
        {
            foreach (var rec in data)
            {
                var address = IPAddress.Parse(rec.Key.ConnectionConfiguration["address"]);
                var port = Int32.Parse(rec.Key.ConnectionConfiguration["port"]);
                var ipe = new IPEndPoint(address, port);
                var tempSocket =
                    new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                tempSocket.Connect(ipe);
                if (!tempSocket.Connected)
                    continue;
                var d = ParseData(rec.Value, sender);
                tempSocket.Send(d.ToArray());
                tempSocket.Close();
            }
        }

        public override void ShutDown()
        {
            _listenerThread.Abort();
            _listenSocket.Shutdown(SocketShutdown.Both);
            _listenSocket.Close();
        }

    }
}
