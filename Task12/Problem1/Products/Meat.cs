namespace Task12.Problem1.Products
{
    public enum MeatCategory
    {
        TopGrade,
        FirstGrade,
        SecondGrade
    }
    public enum MeatType
    {
        Mutton,
        Veal,
        Pork,
        Chicken
    }

    public class Meat : Product
    {
        #region Properties
        public MeatCategory Category { get; private set; }
        public MeatType Type { get; private set; }
        #endregion

        #region Constructors
        public Meat() : this("", 0.0m, 0.1m, default, default)
        {

        }
        public Meat(string name, decimal price, decimal weight, MeatCategory category, MeatType type) : base(name, price, weight)
        {
            Category = category;
            Type = type;
        }
        #endregion

        #region Methods
        public override bool Equals(object? otherProduct)
        {
            if (otherProduct != null && otherProduct is Meat product)
            {
                return base.Equals(product) &&
                Category.Equals(product.Category) &&
                Type.Equals(product.Type);
            }
            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode() ^
                Category.GetHashCode() ^
                Type.GetHashCode();
        }
        public override string ToString()
        {
            return base.ToString() +
                $"Product category: {Category}\n" +
                $"Product type: {Type}\n";
        }
        #endregion
    }
}
