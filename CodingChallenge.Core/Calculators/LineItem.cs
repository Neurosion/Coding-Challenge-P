using System.Collections.Generic;

namespace CodingChallenge.Core.Calculators
{
    public class LineItem
    {
        public string Description { get; set; }
        public List<decimal> PerPayPeriod { get; set; }
        public decimal PerAnnum { get; set; }
    }
}