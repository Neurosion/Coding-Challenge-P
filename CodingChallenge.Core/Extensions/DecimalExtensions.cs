using System;
using System.Collections.Generic;

namespace CodingChallenge.Core.Extensions
{
    public static class DecimalExtensions
    {
        public static IEnumerable<decimal> FromAnnumToPayPeriods(this decimal source, int numberOfPayPeriods)
        {
            if (numberOfPayPeriods == 0)
                yield break;

            var roundedPayPeriod = Math.Round(source / numberOfPayPeriods, 2);
            var roundedPerAnnum = roundedPayPeriod * numberOfPayPeriods;
            var perAnnumDifference = source - roundedPerAnnum;

            var minimumAdjustment = 1m / (decimal)Math.Pow(10, Constants.PayPrecision);
            var numberOfPayPeriodsToAdjust = (int)Math.Abs(perAnnumDifference / minimumAdjustment);
            var payPeriodsPerAdjustment = numberOfPayPeriodsToAdjust > 0 
                                            ? numberOfPayPeriods / numberOfPayPeriodsToAdjust
                                            : 0;
            var remainingAdjustments = numberOfPayPeriodsToAdjust;
            var adjustment = perAnnumDifference > 0
                                ? minimumAdjustment 
                                : -minimumAdjustment;

            for (var i = 1; i <= numberOfPayPeriods; i++)
            {
                var currentPayPeriodValue = roundedPayPeriod;

                if (payPeriodsPerAdjustment > 0
                 && i % payPeriodsPerAdjustment == 0
                 && remainingAdjustments-- > 0)
                    currentPayPeriodValue += adjustment;

                yield return currentPayPeriodValue;
            }
        }
    }
}