using System.Collections.Generic;

namespace ctWebApi.Models
{
    interface IProductBrandRepository
    {
        IEnumerable<ProductBrand> GetAll(string lang);
        ProductBrand Get(int id, string lang);
    }
}
