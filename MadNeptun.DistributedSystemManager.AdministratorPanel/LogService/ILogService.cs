using System.ServiceModel;

namespace MadNeptun.DistributedSystemManager.AdministratorPanel.LogService
{
    [ServiceContract()]
    internal interface ILogService
    {
        [OperationContract(IsOneWay = true)]
        void RecieveLog(int senderId, string log);
    }
}
