using System.Collections.Generic;
using System.Linq;

namespace CodingChallenge.UI.Models
{
    public class EmployeeCostSummary
    {
        public Employee Employee { get; set; }
        public List<LineItem> LineItems { get; set; }

        public static implicit operator EmployeeCostSummary(Core.Calculators.EmployeeCostSummary source)
        {
            if (source == null)
                return null;

            var result = new EmployeeCostSummary
            {
                Employee = source.Employee,
                LineItems = source.LineItems.Select(x => (LineItem)x).ToList()
            };

            return result;
        }
    }
}