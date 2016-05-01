using System;
using System.Collections.Generic;

namespace MadNeptun.DistributedSystemManager.AdministratorPanel.LogService
{
    internal class NewLogEntryEventArgs : EventArgs
    {
        public List<LogEntry> CurrentLog { get; private set; }
        public NewLogEntryEventArgs(List<LogEntry> currentLog)
            : base()
        {
            CurrentLog = currentLog;
        }
    }
}
