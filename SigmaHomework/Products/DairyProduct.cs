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
                < 7 => 7,
                >= 7 and < 14 => 4,
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
        public DairyProduct(int expirationDate, string name, decimal price, decimal weight) : base(name, price, weight)
        {
            ExpirationDate = expirationDate;
        }
    }
}
