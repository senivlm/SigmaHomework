namespace Task12
{
    public delegate int ActionDelegate(int[] array);
    internal class Program
    {
        static void Main()
        {
            /*int[] numbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            ActionDelegate actionDelegate = CalculateElementsSum;
            actionDelegate += CalculateElementsMultiplication;

            var matrix = new Matrix(numbers, actionDelegate);
            Console.WriteLine(matrix.GetStringArr());*/

            List<Person> people = new List<Person>() {
                new Person("n1","p1"),
                new Person("n2","p2"),
                new Person("n3","p3"),
                new Person("n4","p4") };

            PersonCollection personCollection = new PersonCollection(people, "Director");
            Console.WriteLine(personCollection);

            personCollection.IsSameSurname += SameSurnameAction;
            Console.WriteLine(personCollection);

            Console.ReadLine();
        }
        public static int CalculateElementsSum(int[] array)
        {
            return array.Sum();
        }
        public static int CalculateElementsMultiplication(int[] array)
        {
            return array.Aggregate((a, b) => a * b);
        }
        public static void SameSurnameAction()
        {
            Console.WriteLine("List has people with same surname!");
        }
    }
    public delegate void OnSameSurnameDelegate();
    public class PersonCollection
    {
        protected List<Person> _people;
        public string Director { get; }
        public virtual event OnSameSurnameDelegate IsSameSurname;
        public PersonCollection(List<Person> people, string direcor)
        {
            _people = new List<Person>(people);
            Director = direcor;
        }
        public virtual void AddPerson(Person person)
        {
            if (_people.Contains(person))
            {
                IsSameSurname?.Invoke();
            }
            _people.Add(person);
        }
        public override string ToString()
        {
            return string.Join('\n', _people);
        }
    }
    public class AgresivePersonCollection : PersonCollection
    {
        public override event OnSameSurnameDelegate IsSameSurname;
        public AgresivePersonCollection(List<Person> people, string direcor) : base(people, direcor)
        {

        }
        public override void AddPerson(Person person)
        {
            if (_people.Contains(person))
            {
                IsSameSurname?.Invoke();
                return;
            }
            _people.Add(person);
        }
        public override string ToString()
        {
            return string.Join('\n', _people);
        }
    }
    public class Person
    {
        public string Name { get; }
        public string Surname { get; }
        public Person(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }
        public override string ToString()
        {
            return Surname;
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ Surname.GetHashCode();
        }
        public override bool Equals(object? obj)
        {
            if (obj is Person person)
            {
                return Surname.Equals(person.Surname) && Name.Equals(person.Name);
            }
            return false;
        }
    }

    public class Matrix
    {
        private ActionDelegate _actionDelegate;
        public ActionDelegate ActionDelegate { get => _actionDelegate; private set => _actionDelegate += value; }
        private int[] _array;
        public Matrix(int[] array, ActionDelegate actionDelegate)
        {
            _array = array;
            ActionDelegate = actionDelegate;
        }
        public string GetStringArr()
        {
            string result = "";
            result += $"{ActionDelegate(_array)} ";
            return result;
        }
    }
}