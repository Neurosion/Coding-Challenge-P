using System.Collections.Generic;

namespace CodingChallenge.Core.People
{
    public class Employee : Person
    {
        public List<Person> Dependents { get; set; }
    }
}