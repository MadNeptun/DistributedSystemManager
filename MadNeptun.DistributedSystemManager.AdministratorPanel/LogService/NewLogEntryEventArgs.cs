using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

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
