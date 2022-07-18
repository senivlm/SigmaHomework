using System.Runtime.Serialization;
using Task14.GoodsModels.GoodsInterfaces;

namespace Task14.GoodsModels.GoodsAbstractions
{
    [KnownType(typeof(Table))]
    [KnownType(typeof(Chair))]
    [DataContract]
    public abstract class IndustrialProductBase : GoodsBase, IIndustrialProduct
    {
        public IndustrialProductBase(string name, decimal price) : base(name, price)
        {

        }
    }
}
