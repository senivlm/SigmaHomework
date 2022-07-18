using System.Runtime.Serialization;
using Task14.GoodsModels.GoodsAbstractions;
using Task14.GoodsModels.GoodsInterfaces;

namespace Task14.GoodsModels
{
    [DataContract(Name = "DairyProduct")]
    public class DairyProduct : FoodProductBase, IExpirableGoods
    {
        #region Fields
        private DateTime _expirationDate;
        #endregion

        #region Properties
        [DataMember(Name = "ExpirationDate")]
        public DateTime ExpirationDate
        {
            get => _expirationDate;
            protected set
            {
                /*if (value < 0)
                {
                    throw new ArgumentException("Expiration date can not be negative!");
                }*/
                _expirationDate = value;
            }
        }
        #endregion

        #region Constructors
        public DairyProduct() : base("", 0.0m, 0.1)
        {

        }
        public DairyProduct(string name, decimal price, double weight, DateTime expirationDate) : base(name, price, weight)
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
                $"Expiration date: {ExpirationDate}\n";
        }
        #endregion
    }
}
