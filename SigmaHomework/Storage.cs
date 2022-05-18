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

        public void AddDialogData()
        {
            Console.Write("Enter number of products you want to add to storage: ");
            if (int.TryParse(Console.ReadLine(), out int numberOfProducts))
            {
                while (numberOfProducts > 0)
                {
                    Console.Write("1) Meat;\n2) Dairy product;\nSelect product type: ");
                    if (int.TryParse(Console.ReadLine(), out int number) && number <= 2 && number >= 1)
                    {
                        Product product = null;
                        switch (number)
                        {
                            case 1:
                                product = UserDialogInitializer.InitializeMeat();
                                break;
                            case 2:
                                product = UserDialogInitializer.InitializeDairyProduct();
                                break;
                        }
                        _products.Add(product);
                        numberOfProducts--;
                    }
                }
            }
        }
        public void AddInitializedData(Product[] products)
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
        public IEnumerable<DairyProduct> FindAllDairyProducts()
        {
            return _products.Where(p => p is DairyProduct).Select(p => (DairyProduct)p);
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
