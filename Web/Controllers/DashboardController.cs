using System;
using System.Web.Mvc;
using PriceAggergator.Core.Logging.Inteface;

namespace PriceAggregator.Web.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private ILoggingService _logger;

        public DashboardController(ILoggingService logger)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            _logger = logger;
        }

        public ActionResult Index()
        {
            return View("Index");
        }
    }
}