using System;
using System.ComponentModel;
using System.Web.Services;
using MadNeptun.DistributedSystemManager.Core.AbstractEntities;
using MadNeptun.DistributedSystemManager.Core.Objects;

namespace MadNeptun.SoapService
{
    /// <summary>
    /// Summary description for HttpComunicationService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class HttpComunicationService : WebService
    {
        private NetworkComponent<int, string> _networkComponent;
        public void SetupNetworkComponent(NetworkComponent<int, string> networkComponent)
        {
            _networkComponent = networkComponent;
        }

        [WebMethod]
        public void Recieve(Message<string> message, NodeId<int> sender)
        {
            if(_networkComponent != null)
                _networkComponent.Receive(message, sender);
        }
    }
}
