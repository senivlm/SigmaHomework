using Task12.Problem1.Products;
using Task12.Problem1.Services;

namespace Task12.Problem1.StorageInformation
{
    public class StorageNormatives
    {
        private Dictionary<MeatCategory, decimal> _meatNormatives;
        private SortedDictionary<int, decimal> _dairyProductsNormatives;
        public StorageNormatives()
        {
            _meatNormatives = new Dictionary<MeatCategory, decimal>();
            _dairyProductsNormatives = new SortedDictionary<int, decimal>();
        }
        public StorageNormatives(Dictionary<MeatCategory, decimal> meatNormatives, SortedDictionary<int, decimal> dairyProductsNormatives)
        {
            _meatNormatives = meatNormatives;
            _dairyProductsNormatives = dairyProductsNormatives;
        }
        public StorageNormatives(string meatNormativesPath, string dairyProductsNormativesPath)
        {
            _meatNormatives = ReaderFromFile.GetMeatPriceChanges(meatNormativesPath);
            _dairyProductsNormatives = ReaderFromFile.GetDairyProductsPriceChanges(dairyProductsNormativesPath);
        }
        public decimal GetGoodsAdditionalDiscount(IGoods goods)
        {
            switch (goods)
            {
                case Meat meat:
                    try
                    {
                        return _meatNormatives[meat.Category];
                    }
                    catch (KeyNotFoundException)
                    {
                        return 0.0m;
                    }
                case DairyProduct dairy:
                    foreach (var discount in _dairyProductsNormatives)
                    {
                        if (dairy.ExpirationDate < discount.Key)
                        {
                            return discount.Value;
                        }
                    }
                    return 0.0m;
                default:
                    return 0.0m;
            }
        }
    }
}
