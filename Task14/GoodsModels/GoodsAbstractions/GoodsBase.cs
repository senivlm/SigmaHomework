using System.Runtime.Serialization;
using Task14.GoodsModels.GoodsInterfaces;

namespace Task14.GoodsModels.GoodsAbstractions
{
    [KnownType(typeof(Table))]
    [KnownType(typeof(Chair))]
    [KnownType(typeof(MeatProduct))]
    [KnownType(typeof(OrdinaryProduct))]
    [KnownType(typeof(DairyProduct))]
    [DataContract]
    public abstract class GoodsBase : IGoods
    {
        #region Fields

        private string _name;
        private decimal _price;
        #endregion

        #region Properties
        [DataMember(Name = "Name")]
        public string Name
        {
            get => _name;
            protected set => _name = value ?? "";
        }
        [DataMember(Name = "Price")]
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
        #endregion

        #region Constructors
        public GoodsBase(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
        #endregion

        #region Methods
        public override bool Equals(object? otherProduct)
        {
            if (otherProduct != null && otherProduct is GoodsBase product)
            {
                return Name.Equals(product.Name) &&
                        Price.Equals(product.Price);
            }
            return false;
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode() ^
                Price.GetHashCode();
        }
        public override string ToString()
        {
            return $"Name: {Name}\n" +
                $"Price: {Price}\n";
        }
        public virtual void ChangePrice(decimal percent)
        {
            Price += Price * (percent / 100);
        }
        #endregion
    }
}
