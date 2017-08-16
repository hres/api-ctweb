using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using clinical;

namespace ctWebApi.Controllers
{
    public class CSVController : ApiController
    {
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage DownloadCSV(string dataType, string lang)
        {
            DBConnection dbConnection = new DBConnection(lang);
            var jsonResult = string.Empty;
            var fileNameDate = string.Format("{0}{1}{2}",
                           DateTime.Now.Year.ToString(),
                           DateTime.Now.Month.ToString().PadLeft(2, '0'),
                           DateTime.Now.Day.ToString().PadLeft(2, '0'));
            var fileName = string.Format(dataType + "_{0}.csv", fileNameDate);
            byte[] outputBuffer = null;
            string resultString = string.Empty;
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);

            var json = string.Empty;

            switch (dataType)
            {
                case "manufacturer":
                    var manufacturers = dbConnection.GetAllManufacturer().ToList();

                    if (manufacturers.Count > 0)
                    {
                        json = JsonConvert.SerializeObject(manufacturers);

                    }
                    break;

                case "condition":
                    var conditions = dbConnection.GetAllMedicalCondition().ToList();

                    if (conditions.Count > 0)
                    {
                        json = JsonConvert.SerializeObject(conditions);

                    }
                    break;

                case "brand":
                    var brands = dbConnection.GetAllProductBrand().ToList();

                    if (brands.Count > 0)
                    {
                        json = JsonConvert.SerializeObject(brands);

                    }
                    break;

                case "protocol":
                    var protocols = dbConnection.GetAllProtocol().ToList();

                    if (protocols.Count > 0)
                    {
                        json = JsonConvert.SerializeObject(protocols);

                    }
                    break;

                case "status":
                    var status = dbConnection.GetAllStatus().ToList();

                    if (status.Count > 0)
                    {
                        json = JsonConvert.SerializeObject(status);

                    }
                    break;

                case "population":
                    var population = dbConnection.GetAllStudyPopulation().ToList();

                    if (population.Count > 0)
                    {
                        json = JsonConvert.SerializeObject(population);

                    }
                    break;  
            }

            if (!string.IsNullOrWhiteSpace(json))
            {
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(json);
                if (dt.Rows.Count > 0)
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        using (StreamWriter writer = new StreamWriter(stream))
                        {
                            UtilityHelper.WriteDataTable(dt, writer, true);
                            outputBuffer = stream.ToArray();
                            resultString = Encoding.UTF8.GetString(outputBuffer, 0, outputBuffer.Length);
                        }
                    }
                    result.Content = new StringContent(resultString);
                    result.Content.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
                    result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = fileName };
                }
            }

            return result;
        }
    }
}
