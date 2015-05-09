using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MadNeptun.DistributedSystemManager.Service
{
    internal class ConfigurationManager
    {
        private ConfigurationManager()
        {
            
        }

        private static object _lockObject = new object();

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
                    }
                }
            }
        }

        public string InitFile { get; private set; }

        public string ComunicationExchangeFolder { get; private set; }

        public string ImplementationDllPath { get; private set; }

        public string AlgorithmClassName { get; private set; }

        public string NetworkComponentName { get; private set; }

        public string NetworkComponentConfiguration { get; private set; }

        public int NodeId { get; private set; }
    }
}
