using System.Runtime.Serialization;
using Task14.GoodsModels.GoodsAbstractions;
using Task14.Enums;

namespace Task14.GoodsModels
{
    [DataContract(Name = "MeatProduct")]
    public class MeatProduct : FoodProductBase
    {
        #region Properties
        [DataMember(Name = "Category")]
        public MeatCategory Category { get; protected set; }
        [DataMember(Name = "Type")]
        public MeatType Type { get; protected set; }
        #endregion

        #region Constructors
        public MeatProduct() : base("", 0.0m, 0.1)
        {

        }
        public MeatProduct(string name, decimal price, double weight, MeatCategory category, MeatType type) : base(name, price, weight)
        {
            Category = category;
            Type = type;
        }
        #endregion

        #region Methods
        public override bool Equals(object? otherProduct)
        {
            if (otherProduct != null && otherProduct is MeatProduct product)
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
                $"Category: {Category}\n" +
                $"Type: {Type}\n";
        }
        #endregion
    }
}
