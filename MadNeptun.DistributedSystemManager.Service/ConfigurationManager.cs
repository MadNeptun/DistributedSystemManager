﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
            if (args.Length == 0)
                args = ReadFromAppConfig();
            Instance.RawConfiguration = args;
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
                        case "-s":
                            if (i + 1 < args.Length)
                            {
                                Instance.SystemServiceUrl = args[i + 1];
                            }
                            break;
                        case "-h":
                            if (i + 1 < args.Length)
                            {
                                Instance.AdministratorUrl = args[i + 1];
                            }
                            break;
                    }
                }
            }
        }

        private static string[] ReadFromAppConfig()
        {
            var settings = System.Configuration.ConfigurationManager.AppSettings;
            return
                settings.AllKeys.Select(e => new[] {e, settings[e]})
                    .Aggregate(new List<string>(), (acc, next) => acc.Union(next).ToList())
                    .ToArray();
        }

        public double BackgroundOperationInterval { get; private set; }

        public string ImplementationDllPath { get; private set; }

        public string AlgorithmClassName { get; private set; }

        public string NetworkComponentName { get; private set; }

        public string NetworkComponentConfiguration { get; private set; }

        public string SystemServiceUrl { get; private set; }

        public int NodeId { get; private set; }

        public string Neigborhood { get; private set; }

        public string AdministratorUrl { get; private set; }

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
                    t.ConnectionConfiguration.Add("url", d.Url);
                    list.Add(t);
                }
            }
            return list;
        }

        public Type GetAlgorithmType()
        {
            var assembly = Assembly.LoadFrom(ImplementationDllPath);
            return assembly.GetTypes().First(p => p.Name == AlgorithmClassName);
        }

        public Type GetNetworkComponentType()
        {
            var assembly = Assembly.LoadFrom(ImplementationDllPath);
            return assembly.GetTypes().First(p => p.Name == NetworkComponentName);
        }

        public string[] RawConfiguration { get; private set; }
    }
}
