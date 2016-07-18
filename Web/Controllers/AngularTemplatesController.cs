using System.Web.Mvc;

namespace PriceAggregator.Web.Controllers
{
    public class AngularTemplatesController : Controller
    {
        public ActionResult Inline(string templateName)
        {
            ViewBag.TemplateName = templateName;
            return View("Inline");
        }
    }
}