using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using CodingChallenge.Core.People;

namespace CodingChallenge.Core.Calculators.Requests
{
    public class CalculateEmployeeCostRequestHandler : IRequestHandler<CalculateEmployeeCostRequest, IEnumerable<EmployeeCostSummary>>
    {
        private readonly List<ICostCalculator<Employee>> _calculators;

        public CalculateEmployeeCostRequestHandler(IEnumerable<ICostCalculator<Employee>> calculators)
        {
            _calculators = calculators.ToList();
        }

        Task<IEnumerable<EmployeeCostSummary>> IRequestHandler<CalculateEmployeeCostRequest, IEnumerable<EmployeeCostSummary>>.Handle(CalculateEmployeeCostRequest request, CancellationToken cancellationToken)
        {
            var result = request.Employees.Select(x => new EmployeeCostSummary
            {
                Employee = x,
                LineItems = _calculators.SelectMany(c => c.Calculate(x)).ToList()
            });

            return Task.FromResult(result);
        }
    }
}