using clinical;
using System.Collections.Generic;

namespace ctWebApi.Models
{
    public class ProductBrandRepository : IProductBrandRepository
    {
       
        private List<ProductBrand> productbrands = new List<ProductBrand>();
        private ProductBrand productbrand        = new ProductBrand();
        
    public IEnumerable<ProductBrand> GetAll(string lang)
    {
        DBConnection dbConnection = new DBConnection(lang);
        productbrands = dbConnection.GetAllProductBrand();

        return productbrands;
    }

       
    public ProductBrand Get(int id, string lang)
    {
        DBConnection dbConnection = new DBConnection(lang);
        productbrand = dbConnection.GetProductBrandById(id);

        return productbrand;
    }


    }
}