
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceProcess;

namespace MadNeptun.DistributedSystemManager.Service.Manager
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigurationManager.LoadConfiguartion(args);
            if (ConfigurationManager.Instance.Mode == Mode.OneRun)
            {
                RunCommand();
                return;
            }
            while (true)
            {
                string line = Console.ReadLine();
                if (line.ToLowerInvariant() == "quit" || line.ToLowerInvariant() == "exit") return;
                ConfigurationManager.LoadConfiguartion(ParseEntryString(line));
                RunCommand();
            }
        }

        private static string[] ParseEntryString(string entry)
        {
            var outcome = new List<string>();
            var temp = entry.Split(' ');
            var temp2 = new List<string>();
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i].StartsWith("-"))
                {
                    string result = "";
                    temp2.ForEach(t=>result+=" "+t);
                    outcome.Add(result.Substring(1));
                    outcome.Add(temp[i]);
                    continue;
                }
                temp2.Add(temp[i]);
            }
            string result2 = "";
            temp2.ForEach(t => result2 += " "+t);
            outcome.Add(result2.Substring(1));
            outcome = outcome.Where(t => t != "").ToList();
            for (int i = 0; i < outcome.Count; i++)
            {
                outcome[i] = outcome[i].Replace('"', '');
            }
            return outcome.ToArray();
        }

        private static void RunCommand()
        {
            File.WriteAllText(ConfigurationManager.Instance.ExchangeFile, ConfigurationManager.Instance.InitMessage);
            var controller = new ServiceController(ConfigurationManager.Instance.ServiceName);
            controller.ExecuteCommand((int) ConfigurationManager.Instance.Method);
        }
    }
}
