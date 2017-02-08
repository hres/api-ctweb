using clinical;
using System.Collections.Generic;

namespace ctWebApi.Models
{
    public class StudyPopulationRepository : IStudyPopulationRepository
    {
        
        private List<StudyPopulation> studypopulations = new List<StudyPopulation>();
        private StudyPopulation studypopulation        = new StudyPopulation();
        
    public IEnumerable<StudyPopulation> GetAll(string lang)
    {
        DBConnection dbConnection = new DBConnection(lang);
        studypopulations          = dbConnection.GetAllStudyPopulation();

        return studypopulations;
    }


    public StudyPopulation Get(int id, string lang)
    {
        DBConnection dbConnection = new DBConnection(lang);
        studypopulation           = dbConnection.GetStudyPopulationById(id);

       return studypopulation;
    }


    }
}