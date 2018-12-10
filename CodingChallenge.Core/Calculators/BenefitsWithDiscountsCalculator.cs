using System;
using System.Collections.Generic;
using System.Linq;
using CodingChallenge.Core.Extensions;
using CodingChallenge.Core.People;

namespace CodingChallenge.Core.Calculators
{
    public class BenefitsWithDiscountsCalculator : ICostCalculator<Employee>
    {
        public const decimal EmployeeBenefitDeduction = -1000m;
        public const decimal DependentBenefitDeduction = -500m;
        public const decimal NameStartsWithLetterADiscountMultiplier = .1m;

        public static class Descriptions
        {
            public const string EmployeeStandardBenefitDeduction = "Standard benefit deduction for employee";
            public const string DependentStandardBenefitDeduction = "Standard benefit deduction for dependent";
            public const string NameStartsWithADiscount = "Discount for name starting with 'A'";
        }

        public IEnumerable<LineItem> Calculate(Employee target)
        {
            var benefits = GetEmployeeBenefits(target)
                .Concat(GetDependentBenefits(target));

            return benefits;
        }

        private static IEnumerable<LineItem> GetEmployeeBenefits(Employee employee)
        {
            if (!employee?.IsValid() ?? true)
                yield break;

            var employeeBenefit = new LineItem
            {
                Description = Descriptions.EmployeeStandardBenefitDeduction,
                PerAnnum = EmployeeBenefitDeduction,
                PerPayPeriod = EmployeeBenefitDeduction.FromAnnumToPayPeriods(Constants.NumberOfPayPeriodsPerYear).ToList()
            };
            yield return employeeBenefit;

            var discount = GetDiscount(employeeBenefit, employee);

            if (discount != null)
                yield return discount;
        }

        private static IEnumerable<LineItem> GetDependentBenefits(Employee employee)
        {
            if (!employee?.Dependents?.Any() ?? true)
                yield break;

            foreach (var currentDependent in employee.Dependents)
            {
                if (!currentDependent?.IsValid() ?? true)
                    continue;

                var dependentBenefit = new LineItem
                {
                    Description = Descriptions.DependentStandardBenefitDeduction,
                    PerAnnum = DependentBenefitDeduction,
                    PerPayPeriod = DependentBenefitDeduction.FromAnnumToPayPeriods(Constants.NumberOfPayPeriodsPerYear).ToList()
                };

                yield return dependentBenefit;

                var discount = GetDiscount(dependentBenefit, currentDependent);

                if (discount != null)
                    yield return discount;
            }
        }

        private static LineItem GetDiscount(LineItem lineItem, Person person)
        {
            if (!person?.FirstName?.StartsWith("A", StringComparison.CurrentCultureIgnoreCase) ?? true)
                return null;

            var discountPerAnnum = lineItem.PerAnnum * -NameStartsWithLetterADiscountMultiplier;
            var result = new LineItem
            {
                Description = Descriptions.NameStartsWithADiscount,
                PerAnnum = discountPerAnnum,
                PerPayPeriod = discountPerAnnum.FromAnnumToPayPeriods(Constants.NumberOfPayPeriodsPerYear).ToList()
            };

            return result;
        }
    }
}