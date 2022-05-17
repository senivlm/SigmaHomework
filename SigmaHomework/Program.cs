using SigmaHomework.Products;

namespace SigmaHomework
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Task 1
                var apples = new Product("Apples", 23.97m, 1.0m);
                var apples2 = new Product("Apples", 23.97m, 1.0m);
                var cola = new Product("Cola", 33.99m, 1.0m);
                var meat = new Meat(MeatCategory.TopGrade, MeatType.Chicken, "Chicken", 45.90m, 1.0m);
                var milk = new DairyProduct(7, "Milk", 32.00m, 0.5m);
                var buyProducts = new Buy(apples, apples, apples2, cola, meat, milk);

                /*Check.OutputProductInformation(apples);
                Check.OutputProductInformation(meat);
                Check.OutputProductInformation(milk);
                Check.OutputPurchaseInformation(buyProducts);*/


                //Task 2
                var storage = new Storage();
                //storage.InputDialogData();
                storage.InputInitializedData(new Product[] { apples, meat, milk });
                foreach (var m in storage.FindAllMeat())
                {
                    Console.WriteLine(m);
                }
                storage.OutputStorageInformation();
                storage.ChangePriceForProducts(10);
                storage.OutputStorageInformation();
                Console.WriteLine(storage[0]);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
    }
}
