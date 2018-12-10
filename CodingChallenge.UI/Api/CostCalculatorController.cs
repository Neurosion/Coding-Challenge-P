using System.Linq;
using System.Web.Http;
using CodingChallenge.Core.Calculators.Requests;
using CodingChallenge.UI.Models;
using MediatR;

namespace CodingChallenge.UI.Api
{
    [RoutePrefix("api/Calculate")]
    public class CostCalculatorController : ApiController
    {
        private readonly IMediator _mediator;

        public CostCalculatorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("CostSummary")]
        public EmployeeCostSummary[] CostSummary(CostSummaryRequest costSummaryRequest)
        {
            var request = new CalculateEmployeeCostRequest
            {
                Employees = costSummaryRequest.Employees.Select(x => (Core.People.Employee)x).ToList(),
            };
            var costSummaries = _mediator.Send(request).Result;

            var result = costSummaries.Select(x => (EmployeeCostSummary)x).ToArray();

            return result;
        }
    }
}