using System.Collections.Generic;
using CodingChallenge.Core.People;
using MediatR;

namespace CodingChallenge.Core.Calculators.Requests
{
    public class CalculateEmployeeCostRequest : IRequest<IEnumerable<EmployeeCostSummary>>
    {
        public List<Employee> Employees { get; set; }
    }
}