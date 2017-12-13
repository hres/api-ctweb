using ctWebApi.Models;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace ctWebApi.Controllers
{
    public class ProtocolController : ApiController
    {
        static readonly IProtocolRepository databasePlaceholder = new ProtocolRepository();

        public IEnumerable<Protocol> GetAllProtocol(string lang = "en")
        {

            return databasePlaceholder.GetAll(lang);
        }
        

        public Protocol GetProtocolByID(int id, string lang = "en")
        {
            Protocol protocol = databasePlaceholder.Get(id, lang);
            if (protocol == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return protocol;
        }
    }
}
