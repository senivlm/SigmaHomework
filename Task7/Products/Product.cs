using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Task7.Products
{
    public class Product
    {
        #region Fields
        private string _name;
        private decimal _price;
        private decimal _weight;
        #endregion

        #region Properties
        public string Name
        {
            get => _name;
            protected set => _name = value ?? "";
        }
        public decimal Price
        {
            get => _price;
            protected set
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
            protected set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Weight can not be negative or zero!");
                }
                _weight = value;
            }
        }
        #endregion

        #region Constructors
        public Product() : this("", 0.0m, 0.1m)
        {

        }
        public Product(string name, decimal price, decimal weight)
        {
            Name = name;
            Price = price;
            Weight = weight;
        }
        #endregion

        #region Methods
        public void Deconstruct(out string name, out decimal price, out decimal weight)
        {
            name = Name;
            price = Price;
            weight = Weight;
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
        public virtual void Parse(string stringToParse)
        {
            var arrayString = stringToParse.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (arrayString.Length != 3)
            {
                throw new ArgumentException("Incorrect number of arguments for product!");
            }

            if (!char.IsUpper(arrayString[0][0]))
            {
                arrayString[0] = char.ToUpper(arrayString[0][0]) + arrayString[0][1..];
            }

            if (!decimal.TryParse(arrayString[1].Replace('.', ','), out decimal price))
            {
                throw new FormatException("Invalid product price!");
            }

            if (!decimal.TryParse(arrayString[2].Replace('.', ','), out decimal weight))
            {
                throw new FormatException("Invalid product weight!");
            }

            Name = arrayString[0];
            Price = price;
            Weight = weight;
        }
        public virtual void ChangePrice(decimal percent)
        {
            Price += Price * (percent / 100);
        }
        #endregion
    }
}
