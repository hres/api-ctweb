using clinical;
using System.Collections.Generic;

namespace ctWebApi.Models
{
    public class DrugProductRepository : IDrugProductRepository
    {
       
        private List<DrugProduct> drugproducts = new List<DrugProduct>();
        private DrugProduct drugproduct = new DrugProduct();
        
    public IEnumerable<DrugProduct> GetAll(string lang)
    {
        DBConnection dbConnection = new DBConnection(lang);
            drugproducts = dbConnection.GetAllDrugProduct();

        return drugproducts;
    }

       
    public DrugProduct Get(int id, string lang)
    {
        DBConnection dbConnection = new DBConnection(lang);
            drugproduct = dbConnection.GetDrugProductById(id);

        return drugproduct;
    }


    }
}