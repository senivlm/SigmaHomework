using Task14.Enums;

namespace Task14.GoodsModels.GoodsInterfaces
{
    public interface IMaterialGoods : IGoods
    {
        public Material Material { get; }
    }
}
