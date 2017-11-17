using System.Collections.Generic;

namespace ctWebApi.Models
{
    interface ISponsorRepository
    {
        IEnumerable<Sponsor> GetAll(string lang);
        Sponsor Get(int id, string lang);
    }
}
