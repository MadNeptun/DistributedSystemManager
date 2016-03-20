using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MadNeptun.DistributedSystemManager.Service
{
    internal class EventLogHelper
    {

        public static void WriteErrorToEventLog(Exception ex)
        {
            var source = "Distributed Network Node Service";
            if (!EventLog.SourceExists(source))
                EventLog.CreateEventSource(source, "Application");

            EventLog.WriteEntry(source, ex.ToString(), EventLogEntryType.Error);
        }        
    }
}
