using System;
using System.Collections.Generic;
using System.Linq;
using CodingChallenge.Core.Extensions;
using NUnit.Framework;
using Should;

namespace CodingChallenge.Core.Tests.Extensions
{
    public class DecimalExtensionsTests
    {
        private static decimal GetRandomDecimal(int scale, int precision = Constants.PayPrecision, Func<decimal, bool> filter = null)
        {
            var random = new Random();
            var result = 0m;

            do
            {
                result = (decimal)Math.Round(random.NextDouble() * scale, precision);
            } while (!filter?.Invoke(result) ?? false);

            return result;
        }

        private static decimal GetEvenDivisor(int precision = Constants.PayPrecision)
        {
            var divisor = precision != 0
                            ? 1m / (decimal)Math.Pow(10, precision) * 2m
                            : 2m;

            return divisor;
        }

        private static decimal GetRandomEvenDecimal(int scale, int precision = Constants.PayPrecision)
        {
            var divisor = GetEvenDivisor(precision);

            return GetRandomDecimal(scale, precision, x => x % divisor == 0);
        }

        private static decimal GetRandomOddDecimal(int scale, int precision = Constants.PayPrecision)
        {
            var divisor = GetEvenDivisor(precision);

            return GetRandomDecimal(scale, precision, x => x % divisor != 0);
        }

        [Test]
        public void FromAnnumToPayPeriods_WhenNumberOfPayPeriodsIsLessThanZero_ReturnsEmpty()
        {
            var value = GetRandomDecimal(100);
            var result = value.FromAnnumToPayPeriods(-1);

            result.ShouldBeEmpty();
        }

        private static IEnumerable<object[]> PayPeriodTestCases
        {
            get
            {
                yield return new object[] { 0 }; 
                yield return new object[] { 1 };
                yield return new object[] { 2 };
                yield return new object[] { 10 };
                yield return new object[] { new Random().Next(11, 1000) };
            }
        }

        [TestCaseSource(nameof(PayPeriodTestCases))]
        public void FromAnnumToPayPeriods_WhenNumberOfPayPeriodsIsZeroOrMore_ReturnsAResultPerPayPeriod(int payPeriods)
        {
            const decimal value = 12345m;
            var result = value.FromAnnumToPayPeriods(payPeriods);

            result.Count().ShouldEqual(payPeriods);
        }

        [TestCaseSource(nameof(PayPeriodTestCases))]
        public void FromAnnumToPayPeriods_WhenSourceIsZero_ReturnsZeroPayPayPeriod(int payPeriods)
        {
            const decimal value = 0m;
            var result = value.FromAnnumToPayPeriods(payPeriods);

            result.All(x => x == value).ShouldBeTrue();
        }

        public static IEnumerable<object[]> DivisionIntoPayPeriodsTestCases
        {
            get
            {
                yield return new object[] { 1m, 1 };
                yield return new object[] { 2m, 2 };
                yield return new object[] { GetRandomEvenDecimal(1000), (int)GetRandomEvenDecimal(100, 0) };
                yield return new object[] { GetRandomEvenDecimal(1000), (int)GetRandomOddDecimal(100, 0) };
                yield return new object[] { GetRandomOddDecimal(1000), (int)GetRandomEvenDecimal(100, 0) };
                yield return new object[] { GetRandomOddDecimal(1000), (int)GetRandomOddDecimal(100, 0) };
            }
        }

        [TestCaseSource(nameof(DivisionIntoPayPeriodsTestCases))]
        public void FromAnnumToPayPeriods_WhenSourceIsNonZero_ReturnsValuesTotallingSource(decimal perAnnum, int payPeriods)
        {
            var result = perAnnum.FromAnnumToPayPeriods(payPeriods);

            var actualSum = result.Sum();
            actualSum.ShouldEqual(perAnnum);
        }
    }
}