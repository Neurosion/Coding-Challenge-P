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

        [HttpPost]
        [Route("CostSummary")]
        public EmployeeCostSummary[] CostSummary(CostSummaryRequest costSummaryRequest)
        {
        }
    }
}