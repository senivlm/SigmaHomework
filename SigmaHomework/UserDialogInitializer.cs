using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SigmaHomework.Products;

namespace SigmaHomework
{
    public static class UserDialogInitializer
    {
        private static Product InitializeProduct()
        {
            Console.Write("Enter product name: ");
            string name = Console.ReadLine();
            Console.Write("Enter product weight: ");
            decimal weight = decimal.Parse(Console.ReadLine());
            Console.Write("Enter product price: ");
            decimal price = decimal.Parse(Console.ReadLine());
            return new Product(name, price, weight);
        }
        public static Product InitializeMeat()
        {
            var (name, price, weight) = InitializeProduct();
            Console.Write("1) Top grade;\n2) First grade;\n3) Second grade;\nSelect meat category: ");
            MeatCategory meatCategory = (MeatCategory)(int.Parse(Console.ReadLine()) - 1);
            Console.Write("1) Mutton;\n2) Veal;\n3) Pork;\n4) Chicken;\nSelect meat type: ");
            MeatType meatType = (MeatType)(int.Parse(Console.ReadLine()) - 1);
            return new Meat(name, price, weight, meatCategory, meatType);
        }
        public static Product InitializeDairyProduct()
        {
            var (name, price, weight) = InitializeProduct();
            Console.Write("Enter expiration date of the product (in days): ");
            return new DairyProduct(name, price, weight, int.Parse(Console.ReadLine()));
        }
    }
}
