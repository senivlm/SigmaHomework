using Task13.Services;

namespace Task13.Models
{
    public class Person
    {
        public Coordinates Coordinates { get; }
        public string Name { get; }
        public int Age { get; }
        public string Status { get; }
        public int AvailableTime { get; }//Час буде в мілісекундах
        public Person(string name, int age, string status, int availableTime)
        {
            Coordinates = GenerationService.GenerateCoordinates();
            Name = name;
            Age = age;
            Status = status;
            AvailableTime = availableTime;
        }
        public override string ToString()
        {
            return $"Name: {Name}\nAge: {Age}\nStatus: {Status}\nTime spent (in miliseconds): {AvailableTime}\n";
        }
    }
}
