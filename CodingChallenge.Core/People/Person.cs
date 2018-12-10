namespace CodingChallenge.Core.People
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public bool IsValid()
        {
            var result = !string.IsNullOrWhiteSpace(FirstName)
                      || !string.IsNullOrWhiteSpace(LastName);

            return result;
        }
    }
}