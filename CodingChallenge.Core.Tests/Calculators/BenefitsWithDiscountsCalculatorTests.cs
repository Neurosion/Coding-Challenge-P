using System.Collections.Generic;
using System.Linq;
using CodingChallenge.Core.Calculators;
using CodingChallenge.Core.Extensions;
using CodingChallenge.Core.People;
using NUnit.Framework;
using Should;

namespace CodingChallenge.Core.Tests.Calculators
{
    public class BenefitsWithDiscountsCalculatorTests
    {
        [Test]
        public void Calculate_WhenProvidedNull_ReturnsEmpty()
        {
            var calculator = new BenefitsWithDiscountsCalculator();

            var result = calculator.Calculate(null);

            result.ShouldBeEmpty();
        }

        [Test]
        public void Calculate_WhenProvidedEmployee_WithNonEmptyName_ReturnsStandardBenefitDeduction()
        {
            var calculator = new BenefitsWithDiscountsCalculator();
            var employee = new Employee
            {
                FirstName = "first",
                LastName = "name"
            };

            var result = calculator.Calculate(employee).ToList();

            var standardBenefit = result.FirstOrDefault(x => x.Description == BenefitsWithDiscountsCalculator.Descriptions.EmployeeStandardBenefitDeduction);

            standardBenefit.ShouldNotBeNull();
        }

        [Test]
        public void Calculate_WhenProvidedEmployee_WithEmptyName_DoesNotReturnStandardBenefitDeduction()
        {
            var calculator = new BenefitsWithDiscountsCalculator();
            var employee = new Employee();

            var result = calculator.Calculate(employee).ToList();

            result.ShouldBeEmpty();
        }

        [Test]
        public void Calculate_WhenEmployeeElegibleForLetterADiscount_ReturnsNameDiscount()
        {
            var calculator = new BenefitsWithDiscountsCalculator();
            var employee = new Employee
            {
                FirstName = "Alan"
            };

            var result = calculator.Calculate(employee).ToList();

            var standardBenefit = result.FirstOrDefault(x => x.Description == BenefitsWithDiscountsCalculator.Descriptions.NameStartsWithADiscount);

            standardBenefit.ShouldNotBeNull();
        }

        [Test]
        public void Calculate_WhenEmployeeNotElegibleForLetterADiscount_DoesNotReturnNameDiscount()
        {
            var calculator = new BenefitsWithDiscountsCalculator();
            var employee = new Employee
            {
                FirstName = "Bill"
            };

            var result = calculator.Calculate(employee).ToList();

            var standardBenefit = result.FirstOrDefault(x => x.Description == BenefitsWithDiscountsCalculator.Descriptions.NameStartsWithADiscount);

            standardBenefit.ShouldBeNull();
        }

        [Test]
        public void Calculate_WhenEmployeeHasDependents_AndNamesAreNotEmpty_ReturnsBenefitDeductionPerDependent()
        {
            var calculator = new BenefitsWithDiscountsCalculator();
            var employee = new Employee
            {
                FirstName = "Bill",
                Dependents = new List<Person>
                {
                    new Person { FirstName = "John" },
                    new Person { FirstName = "Jill" }
                }
            };

            var result = calculator.Calculate(employee);

            var dependentDeduction = result.Where(x => x.Description == BenefitsWithDiscountsCalculator.Descriptions.DependentStandardBenefitDeduction).ToList();

            dependentDeduction.Count.ShouldEqual(employee.Dependents.Count);
        }

        [Test]
        public void Calculate_WhenEmployeeHasDependents_AndNamesAreEmpty_DoesNotReturnBenefitDeductionPerDependent()
        {
            var calculator = new BenefitsWithDiscountsCalculator();
            var employee = new Employee
            {
                FirstName = "Bill",
                Dependents = new List<Person>
                {
                    new Person(),
                    new Person()
                }
            };

            var result = calculator.Calculate(employee);

            var dependentDeduction = result.Where(x => x.Description == BenefitsWithDiscountsCalculator.Descriptions.DependentStandardBenefitDeduction).ToList();

            dependentDeduction.ShouldBeEmpty();
        }

        [Test]
        public void Calculate_WhenDependentElegibleForLetterADiscount_ReturnsNameDiscount()
        {
            var calculator = new BenefitsWithDiscountsCalculator();
            var employee = new Employee
            {
                FirstName = "Bill",
                Dependents = new List<Person>
                {
                    new Person
                    {
                        FirstName = "Alice"
                    }
                }
            };

            var result = calculator.Calculate(employee);

            var nameDiscount = result.Where(x => x.Description == BenefitsWithDiscountsCalculator.Descriptions.NameStartsWithADiscount).ToList();

            nameDiscount.ShouldNotBeNull();
        }

        [Test]
        public void Calculate_WhenNameStartsWithADiscountIsAvailable_ReturnsLineItemsWithCorrectTotal()
        {
            var calculator = new BenefitsWithDiscountsCalculator();
            var employee = new Employee
            {
                FirstName = "Ann"
            };

            var result = calculator.Calculate(employee).ToList();

            var expectedBenefitDeduction = -BenefitsWithDiscountsCalculator.EmployeeBenefitDeduction;
            var expectedNameDiscountPerAnnum = expectedBenefitDeduction * BenefitsWithDiscountsCalculator.NameStartsWithLetterADiscountMultiplier;
            var expectedNameDiscountPerPayPeriod = expectedNameDiscountPerAnnum.FromAnnumToPayPeriods(Constants.NumberOfPayPeriodsPerYear).ToList();
            var nameDiscount = result.FirstOrDefault(x => x.Description == BenefitsWithDiscountsCalculator.Descriptions.NameStartsWithADiscount);
            nameDiscount.PerAnnum.ShouldEqual(expectedNameDiscountPerAnnum);
            
            nameDiscount.PerPayPeriod.Count.ShouldEqual(expectedNameDiscountPerPayPeriod.Count);

            for (var i = 0; i < expectedNameDiscountPerPayPeriod.Count; i++)
                nameDiscount.PerPayPeriod[i].ShouldEqual(expectedNameDiscountPerPayPeriod[i]);
        }

        public static IEnumerable<object[]> DeductionTotalTestCases
        {
            get
            {
                // employee, no dependents
                yield return new object[]
                {
                    new Employee
                    {
                        FirstName = "Sally"
                    },
                    BenefitsWithDiscountsCalculator.EmployeeBenefitDeduction
                };

                // employee with name discount, no dependents
                yield return new object[]
                {
                    new Employee
                    {
                        FirstName = "Albert"
                    },
                    BenefitsWithDiscountsCalculator.EmployeeBenefitDeduction 
                  - BenefitsWithDiscountsCalculator.NameStartsWithLetterADiscountMultiplier*BenefitsWithDiscountsCalculator.EmployeeBenefitDeduction
                };

                // employee, dependents
                yield return new object[]
                {
                    new Employee
                    {
                        FirstName = "Jim",
                        Dependents = new List<Person>
                        {
                            new Person
                            {
                                FirstName = "Ellen"
                            }
                        }
                    },
                    BenefitsWithDiscountsCalculator.EmployeeBenefitDeduction
                  + BenefitsWithDiscountsCalculator.DependentBenefitDeduction
                };

                // employee with name discount, dependents
                yield return new object[]
                {
                    new Employee
                    {
                        FirstName = "Ajax",
                        Dependents = new List<Person>
                        {
                            new Person
                            {
                                FirstName = "Jill"
                            }
                        }
                    },
                    BenefitsWithDiscountsCalculator.EmployeeBenefitDeduction
                  - BenefitsWithDiscountsCalculator.NameStartsWithLetterADiscountMultiplier*BenefitsWithDiscountsCalculator.EmployeeBenefitDeduction
                  + BenefitsWithDiscountsCalculator.DependentBenefitDeduction
                };

                // employee with name discount, dependents with name discount
                yield return new object[]
                {
                    new Employee
                    {
                        FirstName = "Ajax",
                        Dependents = new List<Person>
                        {
                            new Person
                            {
                                FirstName = "Aaron"
                            }
                        }
                    },
                    BenefitsWithDiscountsCalculator.EmployeeBenefitDeduction
                  - BenefitsWithDiscountsCalculator.NameStartsWithLetterADiscountMultiplier*BenefitsWithDiscountsCalculator.EmployeeBenefitDeduction
                  + BenefitsWithDiscountsCalculator.DependentBenefitDeduction
                  - BenefitsWithDiscountsCalculator.NameStartsWithLetterADiscountMultiplier*BenefitsWithDiscountsCalculator.DependentBenefitDeduction
                };


                // employee, dependents with name discount
                yield return new object[]
                {
                    new Employee
                    {
                        FirstName = "Tim",
                        Dependents = new List<Person>
                        {
                            new Person
                            {
                                FirstName = "Aaron"
                            }
                        }
                    },
                    BenefitsWithDiscountsCalculator.EmployeeBenefitDeduction
                  + BenefitsWithDiscountsCalculator.DependentBenefitDeduction
                  - BenefitsWithDiscountsCalculator.NameStartsWithLetterADiscountMultiplier*BenefitsWithDiscountsCalculator.DependentBenefitDeduction
                };

                // employee, some dependents with name discount
                yield return new object[]
                {
                    new Employee
                    {
                        FirstName = "Bill",
                        Dependents = new List<Person>
                        {
                            new Person
                            {
                                FirstName = "Iris"
                            },
                            new Person
                            {
                                FirstName = "Aaron"
                            }
                        }
                    },
                    BenefitsWithDiscountsCalculator.EmployeeBenefitDeduction
                  + BenefitsWithDiscountsCalculator.DependentBenefitDeduction
                  + BenefitsWithDiscountsCalculator.DependentBenefitDeduction
                  - BenefitsWithDiscountsCalculator.NameStartsWithLetterADiscountMultiplier*BenefitsWithDiscountsCalculator.DependentBenefitDeduction
                };
            }
        }

        [TestCaseSource(nameof(DeductionTotalTestCases))]
        public void Calculate_WhenProvidedEmployeesAndOrDependents_ReturnsExpectedTotal(Employee employee, decimal expectedTotal)
        {
            var calculator = new BenefitsWithDiscountsCalculator();

            var result = calculator.Calculate(employee).ToList();
            var actualTotal = result.Sum(x => x.PerAnnum);

            actualTotal.ShouldEqual(expectedTotal);
        }
    }
}