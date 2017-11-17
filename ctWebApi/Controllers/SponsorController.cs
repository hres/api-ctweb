using ctWebApi.Models;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace ctWebApi.Controllers
{
    public class SponsorController : ApiController
    {
        static readonly ISponsorRepository databasePlaceholder = new SponsorRepository();

        public IEnumerable<Sponsor> GetAllSponsor(string lang="en")
        {

            return databasePlaceholder.GetAll(lang);
        }


        public Sponsor GetManufacturerByID(int id, string lang = "en")
        {
            Sponsor sponsor = databasePlaceholder.Get(id, lang);
            if (sponsor == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return sponsor;
        }
    }
}
