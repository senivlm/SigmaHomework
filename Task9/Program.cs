namespace Task9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Потрібно передати список всіх продуктів відносно страв, які треба приготувати.
            //Сказати скільки сумарно нада купити чого і яка ціна буде



            var studentData = new Dictionary<string, int>() {
                { "Pavlenko", 2 },
                {"Grigorov", 2 },
                {"Petrov", 3 },
                {"Ivanenko", 1 },
                {"Glebenko", 2 }
            };

            var groupData = new Dictionary<int, List<string>>();

            foreach (var element in studentData)
            {
                if (!groupData.ContainsKey(element.Value))
                {
                    groupData.Add(element.Value, new List<string>());
                }
                groupData[element.Value].Add(element.Key);
            }

            foreach (var element in groupData)
            {
                Console.WriteLine($"Group: {element.Key}");
                foreach (var listElement in element.Value)
                {
                    Console.WriteLine($"Student: {listElement}");
                }
            }
            Console.ReadLine();
        }
    }
}