using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web.Core.DbContext;
using Web.Models;

namespace Web.Controllers
{
    public class RrpSituationReportController : Controller
    {
        private RrpSituationReportDbContext db = new RrpSituationReportDbContext();

        // GET: RrpSituationReports
        public ActionResult Index()
        {

            var result = from data in db.Database.SqlQuery<RrpSituationReport>("exec dbo.GetRrpStats @ProductListId=@ProductListId", new SqlParameter() { ParameterName = "@ProductListId", Value = 1 }) select data;

            return View(result.ToList());
        }

        // GET: RrpSituationReports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RrpSituationReport rrpSituationReport = db.DbSet.Find(id);
            if (rrpSituationReport == null)
            {
                return HttpNotFound();
            }
            return View(rrpSituationReport);
        }

      

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
