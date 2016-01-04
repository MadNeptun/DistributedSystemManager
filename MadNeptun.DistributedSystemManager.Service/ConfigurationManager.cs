using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using MadNeptun.DistributedSystemManager.Core.Objects;

namespace MadNeptun.DistributedSystemManager.Service
{
    internal class ConfigurationManager
    {
        private ConfigurationManager()
        {
            
        }

        private static readonly object _lockObject = new object();

        private static ConfigurationManager _instance;

        public static ConfigurationManager Instance { get {
            lock (_lockObject)
            {
                if (_instance == null) _instance = new ConfigurationManager();
                return _instance; 
            }
        } }

        public static void LoadConfiguartion(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].ToLowerInvariant().StartsWith("-"))
                {
                    switch (args[i].ToLowerInvariant())
                    {
                        case "-a":
                            if (i + 1 < args.Length)
                            {
                                Instance.AlgorithmClassName = args[i + 1];
                            }
                            break;
                        case "-d":
                            if (i + 1 < args.Length)
                            {
                                Instance.ImplementationDllPath = args[i + 1];
                            }
                            break;
                        case "-n":
                            if (i + 1 < args.Length)
                            {
                                Instance.NetworkComponentName = args[i + 1];
                            }
                            break;
                        case "-c":
                            if (i + 1 < args.Length)
                            {
                                Instance.NetworkComponentConfiguration = args[i + 1];
                            }
                            break;
                        case "-i":
                            if (i + 1 < args.Length)
                            {
                                Instance.NodeId = Int32.Parse(args[i + 1]);
                            }
                            break;
                        case "-f":
                            if (i + 1 < args.Length)
                            {
                                Instance.InitFile = args[i + 1];
                            }
                            break;
                        case "-t":
                            if (i + 1 < args.Length)
                            {
                                Instance.BackgroundOperationInterval = Double.Parse(args[i + 1]);
                            }
                            break;
                        case "-p":
                            if (i + 1 < args.Length)
                            {
                                Instance.Neigborhood = args[i + 1];
                            }
                            break;
                        case "-w":
                            if (i + 1 < args.Length)
                            {
                                Instance.ComunicationExchangeFolder = args[i + 1];
                            }
                            break;
                    }
                }
            }
        }

        public double BackgroundOperationInterval { get; private set; }

        public string InitFile { get; private set; }

        public string ComunicationExchangeFolder { get; private set; }

        public string ImplementationDllPath { get; private set; }

        public string AlgorithmClassName { get; private set; }

        public string NetworkComponentName { get; private set; }

        public string NetworkComponentConfiguration { get; private set; }

        public int NodeId { get; private set; }

        private string Neigborhood { get; set; }

        public List<NodeId<int>> GetNeigborhood()
        {
            List<NodeId<int>> list = new List<NodeId<int>>();
            using (var fileStream = new FileStream(Neigborhood, FileMode.Open))
            {
                var serializer = new XmlSerializer(typeof (List<Neighbour>));
                var data = (List<Neighbour>)serializer.Deserialize(fileStream);
                foreach (var d in data)
                {
                    var t = new NodeId<int> {Id = d.Id};
                    t.ConnectionConfiguration.Add("address", d.Address);
                    t.ConnectionConfiguration.Add("port",d.Port.ToString());
                    list.Add(t);
                }
            }
            return list;
        }
    }
}
