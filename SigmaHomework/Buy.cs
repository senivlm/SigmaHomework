using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigmaHomework
{
    public class Buy
    {
        private List<Product> _products;
        public int QuantityOfProducts => _products.Count();
        public decimal PurchasePrice => _products.Sum(p => p.Price);
        public decimal PurchaseWeight => _products.Sum(p => p.Weight);

        public void AddProduct(Product product) => _products.Add(product);
        public void RemoveProduct(Product product) => _products.Remove(product);
        public Product[] GetProducts() => _products.ToArray();
        public override string ToString()
        {
            return string.Join("-----------------------------------\n", _products) +
                $"-----------------------------------\n" +
                $"Quantity of goods: {QuantityOfProducts}\n" +
                $"Price of purchase: {PurchasePrice}\n" +
                $"Weight of purchase: {PurchaseWeight}\n";
        }
        public Buy(params Product[] products)
        {
            _products = products == null ? new List<Product>() : new List<Product>(products);
        }
    }
}
