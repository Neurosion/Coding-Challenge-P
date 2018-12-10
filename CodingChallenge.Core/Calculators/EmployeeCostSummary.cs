using System.Collections.Generic;
using CodingChallenge.Core.People;

namespace CodingChallenge.Core.Calculators
{
    public class EmployeeCostSummary
    {
        public Employee Employee { get; set; }
        public List<LineItem> LineItems { get; set; }
    }
}