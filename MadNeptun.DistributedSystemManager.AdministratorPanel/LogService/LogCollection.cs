using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace MadNeptun.DistributedSystemManager.AdministratorPanel.LogService
{
    internal class LogCollection
    {
        private readonly List<LogEntry> _collection;

        private readonly bool _allowLogFile;

        public delegate void NewLogEntry(object sender, NewLogEntryEventArgs e);

        private event NewLogEntry OnNewLogEntry;

        public LogCollection(bool allowLogFile, NewLogEntry newLogEvent)
        {
            _collection = new List<LogEntry>();
            _allowLogFile = allowLogFile;
            OnNewLogEntry += newLogEvent;
        }

        public void AddLogEntry(int senderId, string log)
        {
            if(_collection.Count > 1000)
                DeallocateLogs(false);
            _collection.Add(new LogEntry(senderId,log));
            var thread = new Thread(ExecuteEvent);
            thread.Start();
        }
    
        public void DumpWholeLog()
        {
            DeallocateLogs(true);
            ExecuteEvent();
        }

        private void ExecuteEvent()
        {
            if (OnNewLogEntry != null)
                OnNewLogEntry(this, new NewLogEntryEventArgs(_collection));
        }

        private void DeallocateLogs(bool fullDump)
        {
            var archiveLogs = _collection.Take(fullDump ? _collection.Count : 200);
            if(fullDump)
                _collection.Clear();
            else
                _collection.RemoveRange(0,200);

            if (_allowLogFile)
            {
                var date = DateTime.Now;
                var logFile = string.Format("{0}{1}{2}{3}{4}{5}{6}_log_dump_{7}.txt", date.Year, date.Month, date.Day,
                    date.Hour,
                    date.Minute, date.Second, date.Millisecond, fullDump ? "full" : string.Empty);
                System.IO.File.WriteAllLines(logFile, archiveLogs.Select(e => e.ToString()));
            }
        }
    }
}
