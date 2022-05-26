using Task1.Purchase;

namespace Task1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var apples = new Product("Apples", 23.97m, 1.0m);
                var apples2 = new Product("Apples", 23.97m, 1.0m);
                var cola = new Product("Cola", 33.99m, 1.0m);
                var buyProducts = new Buy(
                    new ProductQuantityPair(apples),
                    new ProductQuantityPair(apples, 3),
                    new ProductQuantityPair(new Product("Apples", 23.97m, 1.0m)),
                    new ProductQuantityPair(cola, 2));

                Check.OutputPurchaseInformation(buyProducts);
                Console.WriteLine();

                buyProducts.AddProduct(cola, 2);

                Check.OutputPurchaseInformation(buyProducts);
                Console.WriteLine();

                buyProducts.RemoveProduct(apples, 10);

                Check.OutputPurchaseInformation(buyProducts);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
    }
}