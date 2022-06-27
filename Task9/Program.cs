using Task9.FileWorker;
using Task9.Services;

namespace Task9
{
    internal class Program
    {
        static void Main(string[] args)
        {// не виведено загальну суму
            try
            {
                var rates = new RatesFileReader("Course.txt", ModelsValidator.ParseExchangeRate)
                    .ReadFromFile();
                var priceKurant = new PriceKurantFileReader("Prices.txt")
                    .ReadFromFile();
                var menu = new MenuFileReader("Menu.txt", ModelsValidator.ParseIngredient, ModelsValidator.ParseDish)
                    .ReadFromFile();

                var fileWriter = new FileWriter(@"D:\C# projects\SigmaHomework\Task9\Result.txt");
                fileWriter.WriteToFile(menu.GetRestaurantDemands(priceKurant, 2, rates[ConsoleUI.ChooseCurrency(rates)]!), false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();

            //Some lecture things
            /*var studentData = new Dictionary<string, int>() {
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
            }*/
        }
    }
}
