using System.Collections.Generic;

namespace ctWebApi.Models
{
    interface IStudyPopulationRepository
    {
        IEnumerable<StudyPopulation> GetAll(string lang);
        StudyPopulation Get(int id, string lang);
    }
}
