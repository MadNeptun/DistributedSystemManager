namespace MadNeptun.DistributedSystemManager.AdministratorPanel.LogService
{
    internal class LogService : ILogService
    {
        private readonly LogCollection _collection;
        public LogService(LogCollection collection)
        {
            _collection = collection;
        }

        public void RecieveLog(int senderId, string log)
        {
            _collection.AddLogEntry(senderId,log);
        }
    }
}
