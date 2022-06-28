namespace Task13
{
    public abstract class Product : IGoods
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
        public override bool Equals(object? otherProduct)
        {
            if (otherProduct != null && otherProduct is Product product)
            {
                return Name.Equals(product.Name) &&
                        Price.Equals(product.Price) &&
                        Weight.Equals(product.Weight);
            }
            return false;
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
        public virtual void ChangePrice(decimal percent)
        {
            Price += Price * (percent / 100);
        }
        #endregion
    }
}
