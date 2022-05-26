using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1;

namespace Task1.Purchase
{
    public class ProductQuantityPair
    {
        private int _quantity;
        public Product Product { get; }
        public int Quantity
        {
            get => _quantity;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Quantity of products that is bought, cannot be zero or negative!");
                }
                _quantity = value;
            }
        }
        public ProductQuantityPair(Product product = null, int quantity = 1)
        {
            Product = product == null ? new Product() : product;
            Quantity = quantity;
        }
        public void IncreaseQuantity(int number)
        {
            if (number <= 0)
            {
                throw new ArgumentException("Quantity of products cannot be zero or negative!");
            }
            Quantity += number;
        }
        public void DecreaseQuantity(int number)
        {
            if (number <= 0)
            {
                throw new ArgumentException("Quantity of products cannot be zero or negative!");
            }
            Quantity -= number;
        }
        public override bool Equals(object? obj)
        {
            return Product.Equals(((ProductQuantityPair)obj).Product);
        }
        public override int GetHashCode()
        {
            return Product.GetHashCode();
        }
        public override string ToString()
        {
            return Product.ToString() + $"Product quantity: {Quantity}\n";
        }
    }
}
