using System.Runtime.Serialization;
using Task14.GoodsModels.GoodsAbstractions;
using Task14.Enums;

namespace Task14.GoodsModels
{
    [DataContract(Name = "Table")]
    public class Table : FurnitureProductBase
    {
        public Table() : base("", 0.0m, 1, 1, 1, default)
        {

        }
        public Table(string name, decimal price, int length, int width, int height, Material material) : base(name, price, length, width, height, material)
        {

        }
    }
}
