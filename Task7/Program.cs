using Task7.Purchase;
using Task7.Products;
using Task7.Products.ProductPriceChanges;

namespace Task7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var storage = new Storage();

                storage.AddProductsFromFile(@"D:\C# projects\SigmaHomework\Task7\Stor.txt");//Valid path: D:\C# projects\SigmaHomework\Task7\StorageDB.txt

                storage.OutputStorageInformation();

                storage.ChangePriceForProducts(-5.0m);

                storage.OutputStorageInformation();

                LogAnalyser logAnalyser = new LogAnalyser(@"D:\C# projects\SigmaHomework\Task7\ErrorLog.txt",
                    @"D:\C# projects\SigmaHomework\Task7\StorageDB.txt");

                Console.Write("Enter date after which you want to change logs: ");
                if (!DateOnly.TryParse(Console.ReadLine(), out DateOnly date))
                {
                    throw new ArgumentException("Invalid input date!");
                }
                logAnalyser.UpdateRecordAfterSpecifiedDate(date);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
    }
}