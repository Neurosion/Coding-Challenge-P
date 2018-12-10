using System.Collections.Generic;
using System.Linq;
using CodingChallenge.Core.People;

namespace CodingChallenge.Core.Calculators
{
    public class SalaryCalculator : ICostCalculator<Employee>
    {
        public const decimal EmployeeSalaryPerPayPeriod = 2000m;

        public static class Descriptions
        {
            public const string EmployeeSalary = "Employee salary";
        }

        public IEnumerable<LineItem> Calculate(Employee target)
        {
            if (!target?.IsValid() ?? true)
                yield break;

            yield return new LineItem
            {
                PerAnnum = EmployeeSalaryPerPayPeriod * Constants.NumberOfPayPeriodsPerYear,
                PerPayPeriod = Enumerable.Repeat(EmployeeSalaryPerPayPeriod, Constants.NumberOfPayPeriodsPerYear).ToList(),
                Description = Descriptions.EmployeeSalary
            };
        }
    }
}