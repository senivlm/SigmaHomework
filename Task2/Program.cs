using Task2.Purchase;
using Task2.Products;
using Task2;
using Task2.Products.ProductPriceChanges;

namespace Task2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Problem 1 testing
                var apples = new Product("Apples", 23.97m, 1.0m);
                var apples2 = new Product("Apples", 23.97m, 1.0m);
                var cola = new Product("Cola", 33.99m, 1.0m);
                var meat = new Meat("Chicken", 45.90m, 1.0m, MeatCategory.TopGrade, MeatType.Chicken);
                var milk = new DairyProduct("Milk", 32.00m, 0.5m, 7);
                var buyProducts = new Buy(
                    new ProductQuantityPair(apples),
                    new ProductQuantityPair(apples, 3),
                    new ProductQuantityPair(new Product("Apples", 23.97m, 1.0m)),
                    new ProductQuantityPair(cola, 2),
                    null,
                    new ProductQuantityPair(meat),
                    new ProductQuantityPair(milk));

                Check.OutputPurchaseInformation(buyProducts);
                Console.WriteLine();

                buyProducts.AddProduct(milk, 2);

                Check.OutputPurchaseInformation(buyProducts);
                Console.WriteLine();

                buyProducts.RemoveProduct(cola, 10);

                Check.OutputPurchaseInformation(buyProducts);

                var storage = new Storage();

                //storage.AddDialogData();
                storage.AddProduct(cola);
                storage.AddInitializedData(new Product[] { apples, apples, meat, milk });

                foreach (var m in storage.FindAllMeat())
                {
                    Console.WriteLine(m);
                }
                storage.OutputStorageInformation();

                //PriceWriter.SetMeatPriceChanges();
                //PriceWriter.SetDairyProductsPriceChanges();

                storage.ChangePriceForProducts(-5.0m);

                storage.OutputStorageInformation();

                Console.WriteLine(storage[0]);

                //Problem 2 testing
                Console.Write("Enter number of rows: ");
                int rows = int.Parse(Console.ReadLine());
                Console.Write("Enter number of columns: ");
                int cols = int.Parse(Console.ReadLine());

                MatrixChanger matrixChanger = new MatrixChanger(rows, cols);
                matrixChanger.VerticalSnakeMatrix();
                Console.WriteLine();
                matrixChanger.SpiralSnakeMatrix();
                Console.WriteLine();
                matrixChanger.DiagonalSnakeMatrix(TurnSide.Right);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
    }
}