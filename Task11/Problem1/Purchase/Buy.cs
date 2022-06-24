using Task11.Problem1.Products;

namespace Task11.Problem1.Purchase
{
    public class Buy
    {
        #region Fields
        private List<GoodsQuantityPair> _goods;
        #endregion

        #region Properties
        public decimal PurchasePrice => _goods.Sum(g => g.Goods.Price * g.Quantity);
        public decimal ProductsPurchaseWeight => _goods.Sum(g =>
        {
            if (g.Goods is Product product)
            {
                return product.Weight * g.Quantity;
            }
            return 0.0m;
        });
        #endregion

        #region Constructors
        public Buy(params GoodsQuantityPair[] goods)
        {
            _goods = new List<GoodsQuantityPair>();
            if (goods != null)
            {
                foreach (var good in goods)
                {
                    if (good != null)
                    {
                        if (_goods.Contains(good))
                        {
                            _goods[_goods.FindIndex(g => g.Equals(good))].IncreaseQuantity(good.Quantity);
                        }
                        else
                        {
                            _goods.Add(good);
                        }
                    }
                }
            }
        }
        #endregion

        #region Methods
        public void AddGood(IGoods good, int quantity = 1)
        {
            if (good != null)
            {
                var goods = _goods.Select(g => g.Goods).ToList();
                if (goods.Contains(good))
                {
                    _goods[goods.FindIndex(g => g.Equals(good))].IncreaseQuantity(quantity);
                }
                else
                {
                    _goods.Add(new GoodsQuantityPair(good, quantity));
                }
            }
        }
        public void RemoveGood(IGoods good, int quantity = 1)
        {
            var goods = _goods.Select(g => g.Goods).ToList();
            if (goods.Contains(good))
            {
                int index = goods.FindIndex(g => g.Equals(good));
                if (quantity >= _goods[index].Quantity)
                {
                    _goods.RemoveAt(index);
                }
                else
                {
                    _goods[index].DecreaseQuantity(quantity);
                }
            }
        }
        public override string ToString()
        {
            return string.Join("-----------------------------------\n", _goods) +
                $"-----------------------------------\n" +
                $"Price of purchase: {PurchasePrice}\n" +
                $"Weight of products in purchase: {ProductsPurchaseWeight}\n";
        }
        #endregion
    }
}
