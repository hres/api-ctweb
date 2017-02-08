using clinical;
using System.Collections.Generic;

namespace ctWebApi.Models
{
    public class ManufacturerRepository : IManufacturerRepository
    {
       
        private List<Manufacturer> manufacturers = new List<Manufacturer>();
        private Manufacturer manufacturer = new Manufacturer();
        
    public IEnumerable<Manufacturer> GetAll(string lang)
    {
        DBConnection dbConnection = new DBConnection(lang);
        manufacturers = dbConnection.GetAllManufacturer();

        return manufacturers;
    }


    public Manufacturer Get(int id, string lang)
    {
        DBConnection dbConnection = new DBConnection(lang);
        manufacturer = dbConnection.GetManufacturerById(id);

        return manufacturer;
    }


    }
}