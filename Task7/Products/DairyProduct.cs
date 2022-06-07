using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task7.Products.ProductPriceChanges;

namespace Task7.Products
{
    public class DairyProduct : Product
    {
        #region Fields
        private int _expirationDate;
        #endregion

        #region Properties
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
        #endregion

        #region Constructors
        public DairyProduct() : this("", 0.0m, 0.1m, 0)
        {

        }
        public DairyProduct(string name, decimal price, decimal weight, int expirationDate) : base(name, price, weight)
        {
            ExpirationDate = expirationDate;
        }
        #endregion

        #region Methods
        public void Deconstruct(out string name, out decimal price, out decimal weight, out int expirationDate)
        {
            Deconstruct(out name, out price, out weight);
            expirationDate = ExpirationDate;
        }
        public override void ChangePrice(decimal percent)
        {
            var percentStandards = PriceReader.GetDairyProductsPriceChanges().OrderBy(kv => kv.Key);
            foreach (var item in percentStandards)
            {
                if (ExpirationDate < item.Key)
                {
                    base.ChangePrice(percent + item.Value);
                    return;
                }
            }
            base.ChangePrice(percent);
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
            var arrayString = stringToParse.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (arrayString.Length != 4)
            {
                throw new ArgumentException("Incorrect number of arguments for dairy product!");
            }

            if (!int.TryParse(arrayString[3], out int expirationDate))
            {
                throw new FormatException("Invalid expiration date of dairy product!");
            }

            base.Parse(string.Join(' ', arrayString[0..3]));
            ExpirationDate = expirationDate;
        }
        #endregion
    }
}
