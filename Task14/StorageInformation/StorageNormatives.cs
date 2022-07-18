using System.Runtime.Serialization;
using Task14.Enums;
using Task14.GoodsModels;
using Task14.GoodsModels.GoodsInterfaces;
using Task14.Services;

namespace Task14.StorageInformation
{
    [DataContract]
    public class StorageNormatives : ICloneable
    {
        [DataMember(Name = "MeatProductNormatives")]
        private Dictionary<MeatCategory, decimal> _meatNormatives;
        [DataMember(Name = "DairyProductNormatives")]
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

        public object Clone()
        {
            return new StorageNormatives(_meatNormatives, _dairyProductsNormatives);
        }

        public decimal GetGoodsAdditionalDiscount(IGoods goods)
        {
            switch (goods)
            {
                case MeatProduct meat:
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
                        if ((DateTime.Now - dairy.ExpirationDate).Days < discount.Key)
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
