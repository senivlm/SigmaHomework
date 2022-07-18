namespace Task14.GoodsModels.GoodsInterfaces
{
    public interface IExpirableGoods : IGoods
    {
        public DateTime ExpirationDate { get; }
    }
}
