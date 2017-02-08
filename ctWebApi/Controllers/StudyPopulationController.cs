using ctWebApi.Models;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace ctWebApi.Controllers
{
    public class StudyPopulationController : ApiController
    {
        static readonly IStudyPopulationRepository databasePlaceholder = new StudyPopulationRepository();

        public IEnumerable<StudyPopulation> GetAllStudyPopulation(string lang)
        {
            return databasePlaceholder.GetAll(lang);
        }
        


        public StudyPopulation GetStudyPopulationByID(int id, string lang)
        {
            StudyPopulation studypopulation = databasePlaceholder.Get(id, lang);
            if (studypopulation == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return studypopulation;
        }
    }
}
