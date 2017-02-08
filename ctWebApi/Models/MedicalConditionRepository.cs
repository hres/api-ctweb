using clinical;
using System.Collections.Generic;

namespace ctWebApi.Models
{
    public class MedicalConditionRepository : IMedicalConditionRepository
    {
        
        private List<MedicalCondition> medicalconditions = new List<MedicalCondition>();
        private MedicalCondition medicalcondition        = new MedicalCondition();
        
    public IEnumerable<MedicalCondition> GetAll(string lang)
    {
        DBConnection dbConnection = new DBConnection(lang);
        medicalconditions = dbConnection.GetAllMedicalCondition();

        return medicalconditions;
    }


    public MedicalCondition Get(int id, string lang)
    {
        DBConnection dbConnection = new DBConnection(lang);
        medicalcondition = dbConnection.GetMedicalConditionById(id);

       return medicalcondition;
    }


    }
}