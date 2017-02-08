using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ctWebApi.Models
{
    public class StudyPopulation
    {
        public int study_population_id { get; set; }
        public string study_population { get; set; }
        // Note: In the DB there is FR & EN fields.
    }
}