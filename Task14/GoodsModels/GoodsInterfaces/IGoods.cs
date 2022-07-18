namespace Task14.GoodsModels.GoodsInterfaces
{
    public interface IGoods
    {
        public string Name { get; }
        public decimal Price { get; }
        public void ChangePrice(decimal percent);
    }
}
