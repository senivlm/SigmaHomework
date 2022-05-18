using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigmaHomework.Products
{
    public class DairyProduct : Product
    {
        private int _expirationDate;
        public int ExpirationDate
        {
            get => _expirationDate;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Expiration date can not be negative!");
                }
                _expirationDate = value;
            }
        }

        public override void ChangePrice(decimal percent)
        {
            var expirationDatePercent = ExpirationDate switch
            {
                < 7 => -7,
                >= 7 and < 14 => -4,
                _ => 0
            };
            base.ChangePrice(percent + expirationDatePercent);
        }
        public override bool Equals(object? otherProduct)
        {
            return base.Equals(otherProduct) &&
                ExpirationDate.Equals(((DairyProduct)otherProduct).ExpirationDate);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode() ^
                ExpirationDate.GetHashCode();
        }
        public override string ToString()
        {
            return base.ToString() +
                $"Product expiration date: {ExpirationDate}\n";
        }
        public override void Parse(string stringToParse)
        {
            if (stringToParse == null || stringToParse.Split(' ').Length != 4)
            {
                throw new ArgumentException("Wrong string to parse!");
            }
            var splitedString = stringToParse.Split(' ');
            base.Parse(string.Join(' ', splitedString[0..3]));
            ExpirationDate = int.Parse(splitedString[3]);
        }
        public DairyProduct() : this("", 0.0m, 0.1m, 0)
        {

        }
        public DairyProduct(string name, decimal price, decimal weight, int expirationDate) : base(name, price, weight)
        {
            ExpirationDate = expirationDate;
        }
        public void Deconstruct(out string name, out decimal price, out decimal weight, out int expirationDate)
        {
            Deconstruct(out name, out price, out weight);
            expirationDate = ExpirationDate;
        }
    }
}
