﻿using Task11.Problem1.Products;

namespace Task11.Problem1.StorageInformation
{// У Вас подія задіюється тільки при читанні, а при додаванні не спрацює.
    public class Storage
    {
        #region Fields
        private List<IGoods> _goods;
        private StorageNormatives _storageNormatives;
        #endregion

        #region Constructors
        public Storage(StorageNormatives storageNormatives = null, IEnumerable<IGoods> goods = null)
        {
            _storageNormatives = storageNormatives ?? new StorageNormatives();
            _goods = new List<IGoods>();
            AddGoods(goods ?? new List<IGoods>());
        }
        #endregion

        #region Methods
        public void AddGood(IGoods good)
        {
            if (!_goods.Contains(good))
            {
                _goods.Add(good);
            }
        }
        public void RemoveGood(IGoods good) => _goods.Remove(good);
        public void AddGoods(IEnumerable<IGoods> goods)
        {
            foreach (var good in goods)
            {
                if (!_goods.Contains(good))
                {
                    _goods.Add(good);
                }
            }
        }
        public static IEnumerable<IGoods> StorageExcept(Storage storage1, Storage storage2)
        {
            if (storage1 == null || storage2 == null)
            {
                throw new ArgumentNullException("Unable to perform \"Except\" operation for Storages! One of them was null!");
            }
            return storage1._goods.Except(storage2._goods);
        }
        public static IEnumerable<IGoods> StorageIntersect(Storage storage1, Storage storage2)
        {
            if (storage1 == null || storage2 == null)
            {
                throw new ArgumentNullException("Unable to perform \"Intersect\" operation for Storages! One of them was null!");
            }
            return storage1._goods.Intersect(storage2._goods);
        }
        public static IEnumerable<IGoods> StorageUnion(Storage storage1, Storage storage2)
        {
            if (storage1 == null || storage2 == null)
            {
                throw new ArgumentNullException("Unable to perform \"Union\" operation for Storages! One of them was null!");
            }
            return storage1._goods.Union(storage2._goods);
        }
        public IEnumerable<Meat> FindAllMeat()
        {
            return _goods.Where(p => p is Meat).Select(p => (Meat)p);
        }
        public IEnumerable<DairyProduct> FindAllDairyProducts()
        {
            return _goods.Where(p => p is DairyProduct).Select(p => (DairyProduct)p);
        }
        public void ChangePriceForAllGoods(decimal percent)
        {
            foreach (var good in _goods)
            {
                good.ChangePrice(percent + _storageNormatives.GetGoodsAdditionalDiscount(good));
            }
        }
        public void ChangePriceForAllProducts(decimal percent)
        {
            foreach (var good in _goods.Where(g => g is Product))
            {
                good.ChangePrice(percent + _storageNormatives.GetGoodsAdditionalDiscount(good));
            }
        }
        public IGoods this[int index]
        {
            get => (index >= 0 && index < _goods.Count) ?
                _goods[index] : throw new ArgumentOutOfRangeException($"No goods with index {index} in storage");
            set
            {
                if (index >= 0 && index < _goods.Count)
                {
                    _goods[index] = value;
                }
            }
        }
        public override string ToString()
        {
            return string.Join("-----------------------------------\n", _goods);
        }
        #endregion
    }
}
