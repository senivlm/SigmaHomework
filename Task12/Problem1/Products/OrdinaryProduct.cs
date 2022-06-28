namespace Task12.Problem1.Products
{
    public class OrdinaryProduct : Product
    {
        public OrdinaryProduct() : this("", 0.0m, 0.1m)
        {

        }
        public OrdinaryProduct(string name, decimal price, decimal weight) : base(name, price, weight)
        {

        }
    }
}
