using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Products;

namespace Task2.Purchase
{
    public class Buy
    {
        #region Fields
        private List<ProductQuantityPair> _products;
        #endregion

        #region Properties
        public decimal PurchasePrice => _products.Sum(p => p.Product.Price * p.Quantity);
        public decimal PurchaseWeight => _products.Sum(p => p.Product.Weight * p.Quantity);
        #endregion

        #region Constructors
        public Buy(params ProductQuantityPair[] products)
        {
            _products = new List<ProductQuantityPair>();
            if (products != null)
            {
                foreach (var prod in products)
                {
                    if (prod != null)
                    {
                        if (_products.Contains(prod))
                        {
                            _products[_products.FindIndex(p => p.Equals(prod))].IncreaseQuantity(prod.Quantity);
                        }
                        else
                        {
                            _products.Add(prod);
                        }
                    }
                }
            }
        }
        #endregion

        #region Methods
        public void AddProduct(Product product, int quantity = 1)
        {
            if (product != null)
            {
                var products = _products.Select(p => p.Product).ToList();
                if (products.Contains(product))
                {
                    _products[products.FindIndex(p => p.Equals(product))].IncreaseQuantity(quantity);
                }
                else
                {
                    _products.Add(new ProductQuantityPair(product, quantity));
                }
            }
        }
        public void RemoveProduct(Product product, int quantity = 1)
        {
            var products = _products.Select(p => p.Product).ToList();
            if (products.Contains(product))
            {
                int index = products.FindIndex(p => p.Equals(product));
                if (quantity >= _products[index].Quantity)
                {
                    _products.RemoveAt(index);
                }
                else
                {
                    _products[index].DecreaseQuantity(quantity);
                }
            }
        }
        public override string ToString()
        {
            return string.Join("-----------------------------------\n", _products) +
                $"-----------------------------------\n" +
                $"Price of purchase: {PurchasePrice}\n" +
                $"Weight of purchase: {PurchaseWeight}\n";
        }
        #endregion
    }
}
