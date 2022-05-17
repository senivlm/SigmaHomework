using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigmaHomework
{
    public class Product
    {
        private decimal _price;
        private decimal _weight;
        public string Name { get; private set; } = "Undefined name";
        public decimal Price
        {
            get => _price;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Price can not be negative!");
                }
                _price = value;
            }
        }
        public decimal Weight
        {
            get => _weight;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Weight can not be negative or zero!");
                }
                _weight = value;
            }
        }

        public virtual void ChangePrice(decimal percent)
        {
            Price -= Price * (percent / 100);
        }
        public override bool Equals(object? otherProduct)
        {
            return Name.Equals(((Product)otherProduct).Name) &&
                Price.Equals(((Product)otherProduct).Price) &&
                Weight.Equals(((Product)otherProduct).Weight);
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode() ^
                Price.GetHashCode() ^
                Weight.GetHashCode();
        }
        public override string ToString()
        {
            return $"Product name: {Name}\n" +
                $"Product price: {Price}\n" +
                $"Product weight: {Weight}\n";
        }
        public Product(string name, decimal price, decimal weight)
        {
            Name = name;
            Price = price;
            Weight = weight;
        }
    }
}
