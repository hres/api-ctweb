using System.Collections.Generic;

namespace ctWebApi.Models
{
    interface IDrugProductRepository
    {
        IEnumerable<DrugProduct> GetAll(string lang);
        DrugProduct Get(int id, string lang);
    }
}
