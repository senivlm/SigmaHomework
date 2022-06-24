namespace Task11.Problem1.Products
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
        public override bool Equals(object? otherProduct)
        {
            if (otherProduct != null && otherProduct is DairyProduct product)
            {
                return base.Equals(product) &&
                    ExpirationDate.Equals(product.ExpirationDate);
            }
            return false;
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
        #endregion
    }
}
