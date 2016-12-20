using clinical;
using System.Collections.Generic;

namespace ctWebApi.Models
{
    public class StatusRepository : IStatusRepository
    {
       
        private List<Status> statuses = new List<Status>();
        private Status status = new Status();
        
    public IEnumerable<Status> GetAll(string lang)
    {
        DBConnection dbConnection = new DBConnection(lang);
        statuses = dbConnection.GetAllStatus();

        return statuses;
    }


    public Status Get(int id, string lang)
    {
        DBConnection dbConnection = new DBConnection(lang);
        status = dbConnection.GetStatusById(id);
        return status;
    }


    }
}