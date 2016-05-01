using System;
using System.Diagnostics;

namespace MadNeptun.ExampleImplementation
{
    internal class EventLogHelper
    {

        public static void WriteErrorToEventLog(Exception ex)
        {
            var source = "Distributed Network Component Implementation";
            if (!EventLog.SourceExists(source))
                EventLog.CreateEventSource(source, "Application");

            EventLog.WriteEntry(source, ex.ToString(), EventLogEntryType.Error);
        }        
    }
}
