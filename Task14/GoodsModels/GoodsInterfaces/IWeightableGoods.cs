namespace Task14.GoodsModels.GoodsInterfaces
{
    public interface IWeightableGoods : IGoods
    {
        public double Weight { get; }
    }
}
