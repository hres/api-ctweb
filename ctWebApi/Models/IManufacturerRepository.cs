using System.Collections.Generic;

namespace ctWebApi.Models
{
    interface IManufacturerRepository
    {
        IEnumerable<Manufacturer> GetAll(string lang);
        Manufacturer Get(int id, string lang);
    }
}
