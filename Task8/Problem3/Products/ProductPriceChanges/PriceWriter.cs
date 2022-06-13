using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task8.Problem3.Products.ProductPriceChanges
{
    public static class PriceWriter
    {
        public static void SetMeatPriceChanges()
        {
            using StreamWriter writer = new StreamWriter(@"D:\C# projects\SigmaHomework\Task8\Problem3\Products\ProductPriceChanges\MeatPriceChanges.txt", false);
            foreach (var category in Enum.GetValues(typeof(MeatCategory)))
            {
                Console.Write($"Enter a constant percentage of storage for {category} " +
                    $"(positive number if the price should be increased, negative - if reduced): ");
                if (decimal.TryParse(Console.ReadLine(), out decimal percentage))
                {
                    writer.WriteLine($"{category} {percentage}");
                }
            }
        }
        public static void SetMeatPriceChanges(Dictionary<MeatCategory, decimal> meatPrices)
        {
            if (meatPrices != null)
            {
                using StreamWriter writer = new StreamWriter(@"D:\C# projects\SigmaHomework\Task8\Problem3\Products\ProductPriceChanges\MeatPriceChanges.txt", false);
                foreach (var category in meatPrices)
                {
                    writer.WriteLine($"{category.Key} {category.Value}");
                }
            }
        }
        public static void SetDairyProductsPriceChanges()
        {
            Console.Write("Enter the number of expiration date ranges (excluding the range from a certain number to \"infinity\"): ");
            int numberOfRanges = int.Parse(Console.ReadLine());
            using StreamWriter writer = new StreamWriter(@"D:\C# projects\SigmaHomework\Task8\Problem3\Products\ProductPriceChanges\DairyProductsPriceChanges.txt", false);
            for (int i = 0; i < numberOfRanges; i++)
            {
                Console.Write($"Enter the upper limit of the expiration date range №{i + 1} (will not be included in this range): ");
                if (int.TryParse(Console.ReadLine(), out int upperLimit))
                {
                    Console.Write($"Enter a constant percentage of storage for the expiration date range №{i} " +
                        $"(positive number if the price should be increased, negative - if reduced): ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal percentage))
                    {
                        writer.WriteLine($"{upperLimit} {percentage}");
                    }
                }
            }
        }
        public static void SetDairyProductsPriceChanges(Dictionary<int, decimal> dairyProductPrices)
        {
            if (dairyProductPrices != null)
            {
                using StreamWriter writer = new StreamWriter(@"D:\C# projects\SigmaHomework\Task8\Problem3\Products\ProductPriceChanges\DairyProductsPriceChanges.txt", false);
                foreach (var product in dairyProductPrices)
                {
                    writer.WriteLine($"{product.Key} {product.Value}");
                }
            }
        }
    }
}
