using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task8.Problem3.Products.ProductPriceChanges
{
    public static class PriceReader
    {
        public static Dictionary<MeatCategory, decimal> GetMeatPriceChanges()
        {
            Dictionary<MeatCategory, decimal> result = new Dictionary<MeatCategory, decimal>();
            foreach (var line in File.ReadLines(@"D:\C# projects\SigmaHomework\Task8\Problem3\Products\ProductPriceChanges\MeatPriceChanges.txt"))
            {
                string[] parts = line.Split(' ');
                if (parts.Length == 2)
                {
                    result.Add((MeatCategory)Enum.Parse(typeof(MeatCategory), parts[0], true), decimal.Parse(parts[1]));
                }
            }
            return result;
        }
        public static Dictionary<int, decimal> GetDairyProductsPriceChanges()
        {
            Dictionary<int, decimal> result = new Dictionary<int, decimal>();
            foreach (var line in File.ReadLines(@"D:\C# projects\SigmaHomework\Task8\Problem3\Products\ProductPriceChanges\DairyProductsPriceChanges.txt"))
            {
                string[] parts = line.Split(' ');
                if (parts.Length == 2)
                {
                    result.Add(int.Parse(parts[0]), decimal.Parse(parts[1]));
                }
            }
            return result;
        }
    }
}
