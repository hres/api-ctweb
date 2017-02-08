using ctWebApi.Models;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace ctWebApi.Controllers
{
    public class ManufacturerController : ApiController
    {
        static readonly IManufacturerRepository databasePlaceholder = new ManufacturerRepository();

        public IEnumerable<Manufacturer> GetAllManufacturer(string lang)
        {

            return databasePlaceholder.GetAll(lang);
        }


        public Manufacturer GetManufacturerByID(int id, string lang)
        {
            Manufacturer manufacturer = databasePlaceholder.Get(id, lang);
            if (manufacturer == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return manufacturer;
        }
    }
}
