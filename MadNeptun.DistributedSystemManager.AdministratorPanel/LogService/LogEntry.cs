using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MadNeptun.DistributedSystemManager.AdministratorPanel.LogService
{
    internal class LogEntry
    {
        public LogEntry(int senderId, string log)
        {
            SenderId = senderId;
            Log = log;
            RecieveDateTime = DateTime.Now;
        }

        public int SenderId { get; private set; }

        public string Log { get; private set; }

        public DateTime RecieveDateTime { get; private set; }

        public override string ToString()
        {
            return string.Format("{0}\t{1}\t{2}", RecieveDateTime, SenderId, Log);
        }
    }
}
