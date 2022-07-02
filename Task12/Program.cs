using Task12.Problem1.Logger;
using Task12.Problem1.Services;
using Task12.Problem1.StorageInformation;
using Task12.Problem1.Products;
using Task12.Problem3;
using Task12.Problem3.StackOperations;

namespace Task12
{
    internal class Program
    {
        static void Main()
        {
            try
            {
                var storageNormatives = new StorageNormatives(
                    @"D:\C# projects\SigmaHomework\Task12\Problem1\StorageInformation\MeatPriceChanges.txt",
                    @"D:\C# projects\SigmaHomework\Task12\Problem1\StorageInformation\DairyProductsPriceChanges.txt");

                var logAnalyser = new LogAnalyser(
                    @"D:\C# projects\SigmaHomework\Task12\Problem1\Logger\ErrorLog.txt",
                    @"D:\C# projects\SigmaHomework\Task12\Problem1\StorageInformation\StorageDB.txt");

                var utilizationFileWriter = new UtilizationFileWriter(
                    @"D:\C# projects\SigmaHomework\Task12\Problem1\StorageInformation\Utilization.txt");

                ReaderFromFile.OnExceptionAction += Console.WriteLine;//Навішую обробник події при читанні з файлу
                ReaderFromFile.OnExceptionAction += logAnalyser.AddLog;//Навішую обробник події при читанні з файлу

                //Problem1
                var storage = new Storage(storageNormatives,
                    ReaderFromFile.ReadGoodsFromFile(@"D:\C# projects\SigmaHomework\Task12\Problem1\StorageInformation\StorageDB.txt", 3),
                    utilizationFileWriter.AddUtilizationRecord//Навішую оброник події при доданні простроченого продукту
                    );//При невірному шляху користувачу дається можливість 3 рази ввести вірний

                Console.WriteLine(storage);

                //Problem2
                foreach (var storageItem in storage.FindAllGoods(g => g.Price < 35.00m))//Знайти продукти, ціна яких менша 35
                {
                    Console.WriteLine($"{storageItem}{new string('-', 50)}");
                }

                //Знайти перший м'ясний продукт, який має найвищу категорію
                Console.WriteLine($"***\n{storage.FindGood(g => g is Meat meat && meat.Category == MeatCategory.TopGrade)}***");

                //Problem3
                var formula = "( 4 + 3 ) ^ 2 / ( 2 - 6 * sin ( -8 - 2 ) ) + cos ( 12 * ( 16 + 2 ) )";
                var calculator = new FormulaCalculator(formula);
                Console.WriteLine($"Before adding square root operation: {calculator.Calculate()}");

                FormulaCalculator.AddOperation(new UnaryStackOperation("sqrt", 4, v => Math.Sqrt(v)));//Додавання нової функції/операції

                formula += " * sqrt ( 64 )";
                calculator = new FormulaCalculator(formula);
                Console.WriteLine($"After adding square root operation: {calculator.Calculate()}");

                var calculator2 = new FormulaInitializerService().InitializeFromConsole();//Користувач конфігурує формулу через консоль
                Console.WriteLine($"Calculation result: {calculator2.Calculate()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
    }
}