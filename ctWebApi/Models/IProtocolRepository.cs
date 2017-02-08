using System.Collections.Generic;

namespace ctWebApi.Models
{
    interface IProtocolRepository
    {
        IEnumerable<Protocol> GetAll(string lang);
        Protocol Get(int id, string lang);
    }
}
