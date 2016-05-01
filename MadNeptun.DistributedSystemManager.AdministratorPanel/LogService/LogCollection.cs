using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace MadNeptun.DistributedSystemManager.AdministratorPanel.LogService
{
    internal class LogCollection
    {
        private readonly ObservableCollection<LogEntry> _collection;

        private readonly bool _allowLogFile;

        public delegate void NewLogEntry(object sender, NewLogEntryEventArgs e);

        public ObservableCollection<LogEntry> Collection { get { return _collection; } } 

        public LogCollection(bool allowLogFile)
        {
            _collection = new ObservableCollection<LogEntry>();
            _allowLogFile = allowLogFile;
        }

        public void AddLogEntry(int senderId, string log)
        {
            if(_collection.Count > 1000)
                DeallocateLogs(false);
            _collection.Add(new LogEntry(senderId,log));
        }
    
        public void DumpWholeLog()
        {
            DeallocateLogs(true);
        }

        private void DeallocateLogs(bool fullDump)
        {
            var archiveLogs = _collection.Take(fullDump ? _collection.Count : 200);
            if (fullDump)
                _collection.Clear();
            else
            {
                var i = 0;
                while (i++ < 200 && _collection.Count > 200)
                    _collection.RemoveAt(0);
            }
            if (_allowLogFile)
            {
                var date = DateTime.Now;
                var logFile = string.Format("{0}{1}{2}{3}{4}{5}{6}_log_dump_{7}.txt", date.Year, date.Month, date.Day,
                    date.Hour,
                    date.Minute, date.Second, date.Millisecond, fullDump ? "full" : string.Empty);
                File.WriteAllLines(logFile, archiveLogs.Select(e => e.ToString()));
            }
        }
    }
}
