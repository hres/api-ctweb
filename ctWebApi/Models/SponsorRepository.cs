using clinical;
using System.Collections.Generic;

namespace ctWebApi.Models
{
    public class SponsorRepository : ISponsorRepository
    {
       
        private List<Sponsor> sponsors = new List<Sponsor>();
        private Sponsor sponsor = new Sponsor();
        
    public IEnumerable<Sponsor> GetAll(string lang)
    {
        DBConnection dbConnection = new DBConnection(lang);
        sponsors = dbConnection.GetAllSponsor();

        return sponsors;
    }


    public Sponsor Get(int id, string lang)
    {
        DBConnection dbConnection = new DBConnection(lang);
        sponsor = dbConnection.GetSponsorById(id);

        return sponsor;
    }


    }
}