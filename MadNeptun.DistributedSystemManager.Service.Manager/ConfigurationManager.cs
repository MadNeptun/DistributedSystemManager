using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MadNeptun.DistributedSystemManager.Core;

namespace MadNeptun.DistributedSystemManager.Service.Manager
{
    class ConfigurationManager
    {
        private ConfigurationManager()
        {

        }

        private static object _lockObject = new object();

        private static ConfigurationManager _instance;

        public static ConfigurationManager Instance
        {
            get
            {
                lock (_lockObject)
                {
                    if (_instance == null) _instance = new ConfigurationManager();
                    return _instance;
                }
            }
        }

        public static void LoadConfiguartion(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].ToLowerInvariant().StartsWith("-"))
                {
                    switch (args[i].ToLowerInvariant())
                    {
                        case "-m":
                            if (i + 1 < args.Length)
                            {
                                Instance.Mode = ParseMode(args[i + 1]);
                            }
                            break;
                        case "-f":
                            if (i + 1 < args.Length)
                            {
                                Instance.Method = ParseMethod(args[i + 1]);
                            }
                            break;
                        case "-e":
                            if (i + 1 < args.Length)
                            {
                                Instance.ExchangeFile = args[i + 1];
                            }
                            break;
                        case "-v":
                            if (i + 1 < args.Length)
                            {
                                Instance.InitMessage = args[i + 1];
                            }
                            break;
                        case "-s":
                            if (i + 1 < args.Length)
                            {
                                Instance.ServiceName = args[i + 1];
                            }
                            break;
                    }
                }
            }
        }

        public string ServiceName { get; private set; }

        public Mode Mode { get; private set; }

        public string ExchangeFile { get; private set; }

        public string InitMessage { get; private set; }

        public Method Method { get; private set; }

        private static Mode ParseMode(string mode)
        {
            switch (mode.ToLowerInvariant())
            {
                case "i":
                case "interactive":
                case "user":
                    return Mode.Interactive;
                case "r":
                case "automatic":
                case "automation":
                    return Mode.OneRun;
                default:
                    return Mode.Interactive;
            }
        }

        private static Method ParseMethod(string method)
        {
            switch (method.ToLowerInvariant())
            {
                case "init":
                    return Method.Init;
                default:
                    return Method.Init;
            }
        }
    }
}
