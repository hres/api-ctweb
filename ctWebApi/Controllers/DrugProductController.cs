using ctWebApi.Models;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace ctWebApi.Controllers
{
    public class DrugProductController : ApiController
    {
        static readonly IDrugProductRepository databasePlaceholder = new DrugProductRepository();

        public IEnumerable<DrugProduct> GetAllDrugProduct(string lang = "en")
        {

            return databasePlaceholder.GetAll(lang);
        }
        

        public DrugProduct GetDrugProductByID(int id, string lang = "en")
        {
            DrugProduct drugproduct = databasePlaceholder.Get(id, lang);
            if (drugproduct == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return drugproduct;
        }
    }
}
