using Task7.Purchase;
using Task7.Products;
using Task7.Products.ProductPriceChanges;

namespace Task7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var storage = new Storage();

            storage.AddProductsFromFile(@"D:\C# projects\SigmaHomework\Task7\StorageDB.txt");//Valid path: D:\C# projects\SigmaHomework\Task7\StorageDB.txt

            storage.OutputStorageInformation();

            storage.ChangePriceForProducts(-5.0m);

            storage.OutputStorageInformation();

            LogAnalyser logAnalyser = new LogAnalyser(@"D:\C# projects\SigmaHomework\Task7\ErrorLog.txt",
                @"D:\C# projects\SigmaHomework\Task7\StorageDB.txt");

            logAnalyser.UpdateRecordAfterSpecifiedDate(new DateOnly(2022, 6, 5));

            Console.ReadLine();
        }
    }
}