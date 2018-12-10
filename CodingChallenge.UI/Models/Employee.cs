using System.Collections.Generic;
using System.Linq;

namespace CodingChallenge.UI.Models
{
    public class Employee : Person
    {
        public List<Person> Dependents { get; set; }
    }
}