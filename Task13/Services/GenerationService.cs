using Task13.Models;
using RandomDataGenerator.Randomizers;
using RandomDataGenerator.FieldOptions;

namespace Task13.Services
{
    public static class GenerationService
    {
        private static readonly Random _randomizer = new Random();
        public static IEnumerable<string> GeneratePersonsString(
            int personCount,
            int minAge, int maxAge,
            int statusLettersCount,
            int minTime, int maxTime)
        {
            var persons = new List<string>();
            var firstNameRandomizer = RandomizerFactory.GetRandomizer(new FieldOptionsFirstName());
            var statusRandomizer = RandomizerFactory.GetRandomizer(new FieldOptionsTextRegex { Pattern = $@"^[A-Z]{{{statusLettersCount}}}$" });

            for (int counter = 0; counter < personCount; counter++)
            {
                var name = firstNameRandomizer.Generate();
                var age = _randomizer.Next(minAge, maxAge);
                var status = statusRandomizer.Generate();
                var time = _randomizer.Next(minTime, maxTime);
                persons.Add(string.Join(' ', name, age, status, time));
            }

            return persons;
        }
        public static Coordinates GenerateCoordinates(int minimum = 0, int maximum = 100)
        {
            return new Coordinates(_randomizer.Next(minimum, maximum), _randomizer.Next(minimum, maximum));
        }
        public static Coordinates GenerateXCoordinates(int y, int minimum = 0, int maximum = 100)
        {
            return new Coordinates(_randomizer.Next(minimum, maximum), y);
        }
        public static Coordinates GenerateY(int x, int minimum = 0, int maximum = 100)
        {
            return new Coordinates(x, _randomizer.Next(minimum, maximum));
        }
    }
}
