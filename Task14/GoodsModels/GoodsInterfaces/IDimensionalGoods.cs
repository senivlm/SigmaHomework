namespace Task14.GoodsModels.GoodsInterfaces
{
    public interface IDimensionalGoods : IGoods
    {
        public int Length { get; }
        public int Width { get; }
        public int Height { get; }
    }
}
