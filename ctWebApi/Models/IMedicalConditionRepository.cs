using System.Collections.Generic;

namespace ctWebApi.Models
{
    interface IMedicalConditionRepository
    {
        IEnumerable<MedicalCondition> GetAll(string lang);
        MedicalCondition Get(int id, string lang);
    }
}
