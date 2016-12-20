using ctWebApi.Models;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace ctWebApi.Controllers
{
    public class StatusController : ApiController
    {
        static readonly IStatusRepository databasePlaceholder = new StatusRepository();

        public IEnumerable<Status> GetAllStatus(string lang)
        {

            return databasePlaceholder.GetAll(lang);
        }


        public Status GetStatusByID(int id, string lang)
        {
            Status status = databasePlaceholder.Get(id, lang);
            if (status == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return status;
        }
    }
}
