using System;
using System.Diagnostics;

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
