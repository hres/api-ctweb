using clinical;
using System.Collections.Generic;

namespace ctWebApi.Models
{
    public class ProtocolRepository : IProtocolRepository
    {
        private List<Protocol> protocols = new List<Protocol>();
        private Protocol protocol        = new Protocol();
        
    public IEnumerable<Protocol> GetAll(string lang)
    {
        DBConnection dbConnection = new DBConnection(lang);
        protocols = dbConnection.GetAllProtocol();

        return protocols;
    }

       
    public Protocol Get(int id, string lang)
    {
        DBConnection dbConnection = new DBConnection(lang);
        protocol = dbConnection.GetProtocolById(id);

        return protocol;
    }


    }
}