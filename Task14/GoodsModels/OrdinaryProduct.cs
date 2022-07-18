using System.Runtime.Serialization;
using Task14.GoodsModels.GoodsAbstractions;

namespace Task14.GoodsModels
{
    [DataContract(Name = "OrdinaryProduct")]
    public class OrdinaryProduct : FoodProductBase
    {
        public OrdinaryProduct() : base("", 0.0m, 0.1)
        {

        }
        public OrdinaryProduct(string name, decimal price, double weight) : base(name, price, weight)
        {

        }
    }
}
