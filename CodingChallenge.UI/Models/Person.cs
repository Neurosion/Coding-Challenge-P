namespace CodingChallenge.UI.Models
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public static implicit operator Person(Core.People.Person source)
        {
            if (source == null)
                return null;

            var result = new Person
            {
                FirstName = source.FirstName,
                LastName = source.LastName,
            };

            return result;
        }

        public static implicit operator Core.People.Person(Person source)
        {
            if (source == null)
                return null;

            var result = new Core.People.Person
            {
                FirstName = source.FirstName,
                LastName = source.LastName,
            };

            return result;
        }
    }
}