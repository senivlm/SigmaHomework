using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Products;

namespace Task2
{
    public class Storage
    {
        #region Fields
        private List<Product> _products;
        #endregion

        #region Constructors
        public Storage(params Product[] products)
        {
            _products = products?.ToList() ?? new List<Product>();
        }
        #endregion

        #region Methods
        public void AddDialogData()
        {
            foreach (var product in UserDialogInitializer.InitializeStorageData())
            {
                if (!_products.Contains(product))
                {
                    _products.Add(product);
                }
            }
        }
        public void AddInitializedData(Product[] products)
        {
            if (products != null)
            {
                foreach (var product in products)
                {
                    if (product != null && !_products.Contains(product))
                    {
                        _products.Add(product);
                    }
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
            set
            {
                if (index >= 0 && index < _products.Count)
                {
                    _products[index] = value;
                }
            }
        }
        #endregion
    }
}
