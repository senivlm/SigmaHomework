namespace Task13
{
    public interface IGoods
    {
        public string Name { get; }
        public decimal Price { get; }
        public void ChangePrice(decimal percent);
    }
}
