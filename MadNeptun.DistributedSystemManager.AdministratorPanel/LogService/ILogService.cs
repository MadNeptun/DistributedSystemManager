using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;

namespace MadNeptun.DistributedSystemManager.AdministratorPanel.LogService
{
    [ServiceContract()]
    internal interface ILogService
    {
        [OperationContract(IsOneWay = true)]
        void RecieveLog(int senderId, string log);
    }
}
