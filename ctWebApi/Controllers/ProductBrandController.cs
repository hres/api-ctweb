using ctWebApi.Models;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace ctWebApi.Controllers
{
    public class ProductBrandController : ApiController
    {
        static readonly IProductBrandRepository databasePlaceholder = new ProductBrandRepository();

        public IEnumerable<ProductBrand> GetAllProductBrand(string lang)
        {

            return databasePlaceholder.GetAll(lang);
        }
        

        public ProductBrand GetProductBrandByID(int id, string lang)
        {
            ProductBrand productbrand = databasePlaceholder.Get(id, lang);
            if (productbrand == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return productbrand;
        }
    }
}
