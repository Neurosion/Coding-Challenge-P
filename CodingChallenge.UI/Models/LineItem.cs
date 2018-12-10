using System.Collections.Generic;

namespace CodingChallenge.UI.Models
{
    public class LineItem
    {
        public List<decimal> PerPayPeriod { get; set; }
        public decimal PerAnnum { get; set; }
        public string Description { get; set; }
    }
}