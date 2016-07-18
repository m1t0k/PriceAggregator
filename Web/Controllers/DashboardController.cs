using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using PagedList;
using PriceAggergator.Core.Logging.Inteface;
using PriceAggregator.Core.DataEntity;
using PriceAggregator.Core.DictionaryProvider.Interfaces;
using PriceAggregator.Core.Logging;

namespace PriceAggregator.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDictionaryProvider<Category> _categoryProvider;
        private ILoggingService _logger;

        public DashboardController(IDictionaryProvider<Category> categoryProvider, ILoggingService logger)
        {
            if (categoryProvider == null)
                throw new ArgumentNullException(nameof(categoryProvider));

            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            _categoryProvider = categoryProvider;
            _logger = logger;
        }

        // GET: Categories
        public async Task<ActionResult> Index(int? page)
        {
            /*const int pageSize = 10;
            var pageNumber = page ?? 1;
            var list = await _categoryProvider.GetListAsync(pageNumber, pageSize, "");

            ViewBag.CategoryListPage = list.ToPagedList(pageNumber, pageSize);*/

            return View("Index");
        }

        // GET: Categories/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return RedirectToAction("Index", "Dashboard", new {categoryId = id});
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryProvider.CreateItem(category);
                return RedirectToAction("Index");
            }

            return View("Create");
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int id)
        {
            var category = _categoryProvider.GetItem(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View("Edit",category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryProvider.UpdateItem(category);
                return RedirectToAction("Index");
            }
            return View("Index");
        }


        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var logger = new NLogLoggingService();

            logger.Debug("Delete categiry Id = " + id);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _categoryProvider.DeleteItem(id);
            return RedirectToAction("Index");
        }
    }
}