using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SigmaHomework.Products;

namespace SigmaHomework
{
    public class Storage
    {
        private List<Product> _products;

        public void InputDialogData()
        {
            Console.Write("Enter number of products you want to add to storage: ");
            if (int.TryParse(Console.ReadLine(), out int numberOfProducts) && numberOfProducts > 0)
            {
                while (numberOfProducts > 0)
                {
                    Console.Write("1) Product;\n2) Meat;\n3) Dairy product;\nSelect product type: ");
                    if (int.TryParse(Console.ReadLine(), out int number) && number < 4)
                    {
                        Console.Write("Enter product name: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter product weight: ");
                        decimal weight = decimal.Parse(Console.ReadLine());
                        Console.Write("Enter product price: ");
                        decimal price = decimal.Parse(Console.ReadLine());
                        Product product = null;
                        switch (number)
                        {
                            case 1:
                                product = new Product(name, price, weight);
                                break;
                            case 2:
                                Console.Write("1) Top grade;\n2) First grade;\n3) Second grade;\nSelect meat category: ");
                                MeatCategory meatCategory = (MeatCategory)(int.Parse(Console.ReadLine()) - 1);
                                Console.Write("1) Mutton;\n2) Veal;\n3) Pork;\n4) Chicken;\nSelect meat type: ");
                                MeatType meatType = (MeatType)(int.Parse(Console.ReadLine()) - 1);
                                product = new Meat(meatCategory, meatType, name, price, weight);
                                break;
                            case 3:
                                Console.Write("Enter expiration date of the product (in days): ");
                                product = new DairyProduct(int.Parse(Console.ReadLine()), name, price, weight);
                                break;
                        }
                        _products.Add(product);
                        numberOfProducts--;
                    }
                }
            }
        }
        public void InputInitializedData(Product[] products)
        {
            if (products != null)
            {
                foreach (var product in products)
                {
                    _products.Add(product);
                }
            }
        }
        public void OutputStorageInformation()
        {
            Console.WriteLine(string.Join("-----------------------------------\n", _products));
        }
        public IEnumerable<Meat> FindAllMeat()
        {
            return _products.Where(p => p is Meat).Select(p => (Meat)p);
        }
        public void ChangePriceForProducts(decimal percent)
        {
            foreach (var product in _products)
            {
                product.ChangePrice(percent);
            }
        }
        public void AddProduct(Product product) => _products.Add(product);
        public void RemoveProduct(Product product) => _products.Remove(product);
        public Product this[int index]
        {
            get => index >= 0 && index < _products.Count ? _products[index] : throw new ArgumentOutOfRangeException();
            private set
            {
                if (index >= 0 && index < _products.Count)
                {
                    _products[index] = value;
                }
            }
        }
        public Storage(params Product[] products)
        {
            _products = products == null ? new List<Product>() : new List<Product>(products);
        }
    }
}
