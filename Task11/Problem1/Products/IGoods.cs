namespace Task11.Problem1.Products
{
    public interface IGoods
    {
        public string Name { get; }
        public decimal Price { get; }
        public void ChangePrice(decimal percent);
    }
}
