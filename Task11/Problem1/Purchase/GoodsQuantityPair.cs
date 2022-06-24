using Task11.Problem1.Products;

namespace Task11.Problem1.Purchase
{
    public class GoodsQuantityPair
    {
        #region Fields
        private int _quantity;
        #endregion

        #region Properties
        public IGoods Goods { get; }
        public int Quantity
        {
            get => _quantity;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Quantity of goods that is bought, cannot be zero or negative!");
                }
                _quantity = value;
            }
        }
        #endregion

        #region Constructors
        public GoodsQuantityPair(IGoods goods, int quantity = 1)
        {
            Goods = goods;
            Quantity = quantity;
        }
        #endregion

        #region Methods
        public void IncreaseQuantity(int number)
        {
            if (number <= 0)
            {
                throw new ArgumentException("Quantity of goods cannot be zero or negative!");
            }
            Quantity += number;
        }
        public void DecreaseQuantity(int number)
        {
            if (number <= 0)
            {
                throw new ArgumentException("Quantity of goods cannot be zero or negative!");
            }
            Quantity -= number;
        }
        public override bool Equals(object? obj)
        {
            if (obj != null && obj is IGoods goods)
            {
                return Goods.Equals(goods);
            }
            return false;
        }
        public override int GetHashCode()
        {
            return Goods.GetHashCode();
        }
        public override string ToString()
        {
            return Goods.ToString() + $"Goods quantity: {Quantity}\n";
        }
        #endregion
    }
}
