using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Mvc;
using PriceAggregator.Core.Libraries.Logging;

namespace Web.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index(int id)
        {
            var violation = GetViolationReport(id);
            var situation = GetSituationReport(id);
            
            ViewData["ViolationReport"] = violation;
            ViewData["SituationReport"] = situation;


            var log= new NLogLoggingService();

            log.Error("====================================");
            log.Error(violation);
            log.Error(situation);
            log.Error("====================================");


            return View();
        }


        private static string GetViolationReport(int categoryId)
        {
            string result = String.Empty;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // New code:
                HttpResponseMessage response = client.GetAsync(
                    new Uri(String.Format("http://localhost:54919/api/report/violation/{0}", categoryId))).Result;
                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.ReadAsStringAsync().Result;
                }

                return result;
            }
        }

        private static string GetSituationReport(int categoryId)
        {
            string result = String.Empty;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // New code:
                HttpResponseMessage response = client.GetAsync(
                    new Uri(String.Format("http://localhost:54919/api/report/situation/{0}", categoryId))).Result;
                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.ReadAsStringAsync().Result;
                }

                return result;
            }
        }
    }
}