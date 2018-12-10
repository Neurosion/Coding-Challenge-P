using System.Linq;
using CodingChallenge.Core.Calculators;
using CodingChallenge.Core.People;
using NUnit.Framework;
using Should;

namespace CodingChallenge.Core.Tests.Calculators
{
    public class SalaryCalculatorTests
    {
        [Test]
        public void Calculate_WhenProvidedNull_ReturnsEmpty()
        {
            var calculator = new SalaryCalculator();

            var result = calculator.Calculate(null);

            result.ShouldBeEmpty();
        }

        [Test]
        public void Calculate_WhenProvidedInvalidEmployee_ReturnsEmpty()
        {
            var employee = new Employee();
            var calculator = new SalaryCalculator();

            var result = calculator.Calculate(employee);

            employee.IsValid().ShouldBeFalse();
            result.ShouldBeEmpty();
        }

        [Test]
        public void Calculate_WhenProvidedValidEmployee_ReturnsOneResult()
        {
            var employee = new Employee
            {
                FirstName = "John",
                LastName = "Smith"
            };
            var calculator = new SalaryCalculator();

            var result = calculator.Calculate(employee);
            
            result.Count().ShouldEqual(1);
        }

        [Test]
        public void Calculate_WhenProvidedValidEmployee_ReturnExpectedValues()
        {
            var expectedPerAnnum = SalaryCalculator.EmployeeSalaryPerPayPeriod * Constants.NumberOfPayPeriodsPerYear;
            var expectedPerPayPeriod = SalaryCalculator.EmployeeSalaryPerPayPeriod;
            var employee = new Employee
            {
                FirstName = "John",
                LastName = "Smith"
            };
            var calculator = new SalaryCalculator();

            var result = calculator.Calculate(employee).FirstOrDefault();

            result.ShouldNotBeNull();
            result.Description.ShouldEqual(SalaryCalculator.Descriptions.EmployeeSalary);
            result.PerAnnum.ShouldEqual(expectedPerAnnum);

            result.PerPayPeriod.All(x => x == expectedPerPayPeriod).ShouldBeTrue();
        }
    }
}