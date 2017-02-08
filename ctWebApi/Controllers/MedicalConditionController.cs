using ctWebApi.Models;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace ctWebApi.Controllers
{
    public class MedicalConditionController : ApiController
    {
        static readonly IMedicalConditionRepository databasePlaceholder = new MedicalConditionRepository();

        public IEnumerable<MedicalCondition> GetAllMedicalCondition(string lang)
        {
            return databasePlaceholder.GetAll(lang);
        }


        public MedicalCondition GetMedicalConditionByID(int id, string lang)
        {
            MedicalCondition medicalcondition = databasePlaceholder.Get(id, lang);
            if (medicalcondition == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return medicalcondition;
        }
    }
}
