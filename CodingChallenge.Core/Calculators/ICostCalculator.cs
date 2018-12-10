using System.Collections.Generic;

namespace CodingChallenge.Core.Calculators
{
    public interface ICostCalculator<in T>
    {
        IEnumerable<LineItem> Calculate(T target);
    }
}