using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
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
            protected set => _name = value == null ? "" : value;
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
            if (stringToParse == null || stringToParse.Split(' ').Length != 3)
            {
                throw new ArgumentException("Wrong string to parse!");
            }
            var arrayString = stringToParse.Split(' ');
            Name = arrayString[0];
            Price = decimal.Parse(arrayString[1]);
            Weight = decimal.Parse(arrayString[2]);
        }
        #endregion
    }
}
