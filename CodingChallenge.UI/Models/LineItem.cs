using System.Collections.Generic;

namespace CodingChallenge.UI.Models
{
    public class LineItem
    {
        public List<decimal> PerPayPeriod { get; set; }
        public decimal PerAnnum { get; set; }
        public string Description { get; set; }

        public static implicit operator LineItem(Core.Calculators.LineItem source)
        {
            if (source == null)
                return null;

            var result = new LineItem
            {
                PerPayPeriod = new List<decimal>(source.PerPayPeriod),
                PerAnnum = source.PerAnnum,
                Description = source.Description
            };

            return result;
        }
    }
}