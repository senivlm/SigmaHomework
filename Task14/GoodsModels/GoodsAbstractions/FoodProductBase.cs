using System.Runtime.Serialization;
using Task14.GoodsModels.GoodsInterfaces;

namespace Task14.GoodsModels.GoodsAbstractions
{
    [KnownType(typeof(MeatProduct))]
    [KnownType(typeof(OrdinaryProduct))]
    [KnownType(typeof(DairyProduct))]
    [DataContract]
    public abstract class FoodProductBase : GoodsBase, IFoodProduct
    {
        #region Fields
        private double _weight;
        #endregion

        #region Properties
        [DataMember(Name = "Weight")]
        public double Weight
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
        public FoodProductBase(string name, decimal price, double weight) : base(name, price)
        {
            Weight = weight;
        }
        #endregion

        #region Methods
        public override bool Equals(object? otherProduct)
        {
            if (otherProduct != null && otherProduct is FoodProductBase product)
            {
                return base.Equals(product) &&
                        Weight.Equals(product.Weight);
            }
            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode() ^
                Weight.GetHashCode();
        }
        public override string ToString()
        {
            return base.ToString() +
                $"Weight: {Weight}\n";
        }
        #endregion
    }
}
