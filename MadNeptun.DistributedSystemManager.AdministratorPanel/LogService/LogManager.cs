using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MadNeptun.DistributedSystemManager.AdministratorPanel.LogService
{
    internal class LogManager : IDisposable
    {
        private LogServiceHost _logServiceHost;

        private readonly LogCollection _logCollection;

        public LogManager(bool allowLogFile, string serviceUrl, LogCollection.NewLogEntry newLogEntryDelegate)
        {
            _logCollection = new LogCollection(allowLogFile, newLogEntryDelegate);
            RunService(serviceUrl);
        }

        private void DumpAllLogs()
        {
            _logCollection.DumpWholeLog();
        }

        public void Dispose()
        {
            StopService();
            DumpAllLogs();
        }

        private void RunService(string url)
        {
            StopService();
            _logServiceHost = new LogServiceHost(_logCollection, url);
            _logServiceHost.Open();
        }

        private void StopService()
        {
            if (_logServiceHost != null)
            {
                _logServiceHost.Close();
                _logServiceHost = null;
            }
        }   
    }
}
