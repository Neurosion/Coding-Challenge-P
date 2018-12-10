using System.Collections.Generic;
using System.Linq;

namespace CodingChallenge.UI.Models
{
    public class Employee : Person
    {
        public List<Person> Dependents { get; set; }

        public static implicit operator Employee(Core.People.Employee source)
        {
            if (source == null)
                return null;

            var result = new Employee
            {
                FirstName = source.FirstName,
                LastName = source.LastName,
                Dependents = source.Dependents?.Select(x => (Person)x).ToList()
            };

            return result;
        }

        public static implicit operator Core.People.Employee(Employee source)
        {
            if (source == null)
                return null;

            var result = new Core.People.Employee
            {
                FirstName = source.FirstName,
                LastName = source.LastName,
                Dependents = source.Dependents?.Select(x => (Core.People.Person)x).ToList()
            };

            return result;
        }
    }
}