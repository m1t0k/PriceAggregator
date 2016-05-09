using System.Collections.Generic;
using System.Web.Http;
using PriceAggregator.Controllers.Base;
using PriceAggregator.Core.Entities;
using PriceAggregator.Core.Repository.Interfaces;

namespace PriceAggregator.Controllers
{
    [RoutePrefix("api/report")]
    public class ReportController : BaseController
    {
        private readonly IReportRepository _repository;

        public ReportController(IReportRepository repository)
        {
            _repository = repository;
        }

        [Route("situation/{id}")]
        [HttpGet]
        public IHttpActionResult GetRrpSituationReport(int id)
        {
            List<RrpSituationReport> result = _repository.GetRrpSituationReport(id);

            if (result.Count == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [Route("violation/{id}")]
        [HttpGet]
        public IHttpActionResult GetRrpViolationReport(int id)
        {
            List<RrpViolationReport> result = _repository.GetRrpViolationReport(id);

            if (result.Count == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}