using System.Runtime.Serialization;
using Task14.GoodsModels.GoodsInterfaces;
using Task14.GoodsModels;

namespace Task14.StorageInformation
{
    [KnownType(typeof(Table))]
    [KnownType(typeof(Chair))]
    [KnownType(typeof(MeatProduct))]
    [KnownType(typeof(OrdinaryProduct))]
    [KnownType(typeof(DairyProduct))]
    [DataContract]
    public class Storage
    {
        #region Events
        public static event Action<Storage, IGoods> OnAddingExpiredGoodsEvent;
        #endregion 

        #region Fields
        [DataMember(Name = "Goods")]
        private List<IGoods> _goods;
        [DataMember(Name = "StorageNormatives")]
        private StorageNormatives _storageNormatives;
        private static Storage _storage;
        #endregion

        #region Constructors
        private Storage(StorageNormatives storageNormatives = null,
            IEnumerable<IGoods> goods = null,
            Action<Storage, IGoods> onAddingExpiredGoodsEvent = null)
        {
            OnAddingExpiredGoodsEvent += onAddingExpiredGoodsEvent;
            _storageNormatives = storageNormatives ?? new StorageNormatives();
            _goods = new List<IGoods>();
            AddGoods(goods ?? new List<IGoods>());
        }
        #endregion

        #region Methods
        public static Storage GetInstance(StorageNormatives storageNormatives = null,
            IEnumerable<IGoods> goods = null,
            Action<Storage, IGoods> onAddingExpiredGoodsEvent = null)
        {
            if (_storage == null)
            {
                _storage = new Storage(storageNormatives, goods, onAddingExpiredGoodsEvent);
            }
            if (onAddingExpiredGoodsEvent != null)
            {
                OnAddingExpiredGoodsEvent += onAddingExpiredGoodsEvent;
            }
            if (goods != null)
            {
                _storage.AddGoods(goods);
            }
            _storage._storageNormatives = storageNormatives ?? _storage._storageNormatives;
            return _storage;
        }
        public void Deconstruct(out List<IGoods> goods, out StorageNormatives storageNormatives)
        {
            goods = new List<IGoods>(_goods);
            storageNormatives = _storageNormatives.Clone() as StorageNormatives;
        }
        public void AddGood(IGoods good)
        {
            if (!_goods.Contains(good))
            {
                _goods.Add(good);
                if (good is IExpirableGoods expirableProduct && expirableProduct.ExpirationDate < DateTime.Now)
                {
                    OnAddingExpiredGoodsEvent?.Invoke(this, good);
                }
            }
        }
        public void RemoveGood(IGoods good) => _goods.Remove(good);
        public void AddGoods(IEnumerable<IGoods> goods)
        {
            foreach (var good in goods)
            {
                AddGood(good);
            }
        }

        #region Problem2
        public IGoods? FindGood(Predicate<IGoods> match)
        {
            return _goods.Find(match);
        }
        public IGoods? FindLastGood(Predicate<IGoods> match)
        {
            return _goods.FindLast(match);
        }
        public IEnumerable<IGoods> FindAllGoods(Predicate<IGoods> match)
        {
            return _goods.FindAll(match);
        }
        #endregion

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
        public IEnumerable<MeatProduct> FindAllMeat()
        {
            return _goods.Where(p => p is MeatProduct).Select(p => (MeatProduct)p);
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
            foreach (var good in _goods.Where(g => g is IFoodProduct))
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
