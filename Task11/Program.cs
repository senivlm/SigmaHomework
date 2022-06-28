using Task11.Problem2;
using Task11.Problem1.Services;
using Task11.Problem1.StorageInformation;
using Task11.Problem1.Logger;
using Task11.Problem1.Purchase;

namespace Task11
{
    internal class Program
    {
        static void Main()
        {
            try
            {
                Problem1Testing();

                Problem2Testing();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void Problem1Testing()
        {
            var storageNormatives = new StorageNormatives(
                    @"D:\C# projects\SigmaHomework\Task11\Problem1\StorageInformation\MeatPriceChanges.txt",
                    @"D:\C# projects\SigmaHomework\Task11\Problem1\StorageInformation\DairyProductsPriceChanges.txt");

            LogAnalyser logAnalyser = new LogAnalyser(
                @"D:\C# projects\SigmaHomework\Task11\Problem1\Logger\ErrorLog.txt",
                @"D:\C# projects\SigmaHomework\Task11\Problem1\StorageInformation\StorageDB.txt");

            ReaderFromFile.OnExceptionAction += Console.WriteLine;//Навішую обробник події при читанні з файлу
            ReaderFromFile.OnExceptionAction += logAnalyser.AddLog;//Навішую обробник події при читанні з файлу
            var storage = new Storage(storageNormatives,
                ReaderFromFile.ReadGoodsFromFile(@"D:\C# projects\SigmaHomework\Task11\Problem1\StorageInformation\StorageDB.txt", 3)
                );//При невірному шляху користувачу дається можливість 3 рази ввести вірний

            logAnalyser.UpdateRecordAfterSpecifiedDate(new DateOnly(2022, 5, 12));//Користувач може змінити некоректні дані
                                                                                  //зі складу, які потрапили в логи після
                                                                                  //вказаної дати

            Console.WriteLine(storage);

            var purchase = new Buy();//Створення покупки
            purchase.AddGood(storage[0], 3);//Додання товарів до покупки
            purchase.AddGood(storage[3], 1);
            purchase.AddGood(storage[5], 1);

            Check.GetCheck(purchase);//Отримання чеку про покупки

            storage.AddGoods(UserDialogInitializer.InitializeStorageData());//Користувач може додати до складу
                                                                            //нові продукти через консоль
            Console.WriteLine(storage);

            storage.ChangePriceForAllProducts(-5.0m);//Активація знижки на продукти, враховуючи нормативи складу

            Console.WriteLine(storage);
        }
        private static void Problem2Testing()
        {
            var genericList = new GenericList<int>(1, 2, 3, 4);//Не певен, що це саме той клас,
                                                               //для якого потрібно було зробити узагальнення

            Console.WriteLine($"Index 0: {genericList[0]}");
            Console.WriteLine($"Count: {genericList.Count}");

            genericList.Add(9);

            Console.WriteLine($"GenericList: {string.Join(' ', genericList)}");

            Console.WriteLine($"Contains number 3: {genericList.Contains(3)}");

            var array = new int[] { 10, 11, 12, 13, 14, 15, 16, 17 };

            Console.WriteLine($"Array before CopyTo: {string.Join(' ', array)}");

            genericList.CopyTo(array, 2);

            Console.WriteLine($"Array after CopyTo: {string.Join(' ', array)}");

            foreach (var item in genericList)
            {
                Console.Write($"{item} ->");
            }

            var genericList1 = new GenericList<int>(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11);

            Console.WriteLine($"\nBinary search element \"5\". Index: {genericList1.BinarySearch(5)}");

            var genericString = new GenericList<string>("aa", "abs", "bbc", "bcx", "fmg", "png");

            Console.WriteLine($"\nBinary search element \"bbc\". Index: {genericString.BinarySearch("bbc")}");
        }
    }
}