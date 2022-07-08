using Task13.Models;

namespace Task13.Services
{
    public static class ValidationService
    {
        public static Person ValidatePerson(string lineToValidate)
        {
            var partsOfLine = lineToValidate.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (partsOfLine == null || partsOfLine.Length != 4)
            {
                throw new ArgumentException("Incorrect number of arguments for person!");
            }

            if (!int.TryParse(partsOfLine[1], out int age))
            {
                throw new FormatException("Invalid person age!");
            }

            if (!int.TryParse(partsOfLine[3], out int time))
            {
                throw new FormatException("Invalid time that person can spend at the paydesk!");
            }

            return new Person(partsOfLine[0], age, partsOfLine[2], time);
        }
        public static IEnumerable<Person> ValidatePeople(IEnumerable<string> linesToValidate)
        {
            var people = new List<Person>();

            int lineNumber = 1;
            foreach (var line in linesToValidate)
            {
                try
                {
                    var person = ValidatePerson(line);
                    people.Add(person);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + $" Line number {lineNumber}");
                }
                finally
                {
                    lineNumber++;
                }
            }

            return people;
        }
    }
}
