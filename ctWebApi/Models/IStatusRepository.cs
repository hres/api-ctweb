using System.Collections.Generic;

namespace ctWebApi.Models
{
    interface IStatusRepository
    {
        IEnumerable<Status> GetAll(string lang);
        Status Get(int id, string lang);
    }
}
