using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task8.Problem3.Products;

namespace Task8.Problem3
{
    public class Storage
    {
        #region Fields
        private List<Product> _products;
        #endregion

        #region Constructors
        public Storage(params Product[] products)
        {
            _products = products?.ToList() ?? new List<Product>();
        }
        #endregion

        #region Methods
        public void AddDialogData()
        {
            foreach (var product in UserDialogInitializer.InitializeStorageData())
            {
                if (!_products.Contains(product))
                {
                    _products.Add(product);
                }
            }
        }
        public void AddInitializedData(Product[] products)
        {
            if (products != null)
            {
                foreach (var product in products)
                {
                    if (product != null && !_products.Contains(product))
                    {
                        _products.Add(product);
                    }
                }
            }
        }
        public void AddProductsFromFile(StreamReader source, StreamWriter errorLogWriter)
        {
            while (!source.EndOfStream)
            {
                var line = source.ReadLine();
                Product product = null;
                Type? type = null;

                try
                {
                    var elementsInLine = line?.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (elementsInLine == null || elementsInLine.Length == 0)
                    {
                        throw new ArgumentNullException("Line is empty!");
                    }

                    type = Type.GetType($"Task8.Problem3.Products.{elementsInLine[0]}", false, true);
                    if (type == null)
                    {
                        throw new ArgumentNullException("Invalid type parameter!");
                    }
                    product = (Product)Activator.CreateInstance(type);

                    product.Parse(string.Join(' ', elementsInLine[1..]));
                }
                catch (Exception exception)
                {
                    errorLogWriter.WriteLine($"{exception.GetType().Name}: {exception.Message} " +
                        $"Line: {line} " +
                        $"Inspection date: {DateTime.Now}");
                }

                if (product != null &&
                    !product.Equals((Product)Activator.CreateInstance(type)) &&
                    !_products.Contains(product))
                {
                    _products.Add(product);
                }
            }
        }
        public void AddProductsFromFile(string sourceFilePath,
            string errorLogFilePath = @"D:\C# projects\SigmaHomework\Task8\Problem3\ErrorLog.txt",
            bool append = true)
        {
            for (int attempt = 0; attempt < 3 && !File.Exists(sourceFilePath ?? ""); attempt++)
            {
                Console.WriteLine("Invalid source file path! Enter valid path: ");
                sourceFilePath = Console.ReadLine();
            }

            using var sourceReader = new StreamReader(File.Exists(sourceFilePath) ?
                sourceFilePath :
                @"D:\C# projects\SigmaHomework\Task8\Problem3\StorageDB.txt");

            using var errorLogWriter = new StreamWriter(errorLogFilePath, append);

            AddProductsFromFile(sourceReader, errorLogWriter);
        }
        public void OutputStorageInformation()
        {
            Console.WriteLine(string.Join("-----------------------------------\n", _products));
        }
        public static IEnumerable<Product> StorageExcept(Storage storage1, Storage storage2)
        {
            if (storage1 == null || storage2 == null)
            {
                throw new ArgumentNullException("Unable to perform \"Except\" operation for Storages! One of them was null!");
            }
            return storage1._products.Except(storage2._products);
        }
        public static IEnumerable<Product> StorageIntersect(Storage storage1, Storage storage2)
        {
            if (storage1 == null || storage2 == null)
            {
                throw new ArgumentNullException("Unable to perform \"Intersect\" operation for Storages! One of them was null!");
            }
            return storage1._products.Intersect(storage2._products);
        }
        public static IEnumerable<Product> StorageUnion(Storage storage1, Storage storage2)
        {
            if (storage1 == null || storage2 == null)
            {
                throw new ArgumentNullException("Unable to perform \"Union\" operation for Storages! One of them was null!");
            }
            return storage1._products.Union(storage2._products);
        }
        public IEnumerable<Meat> FindAllMeat()
        {
            return _products.Where(p => p is Meat).Select(p => (Meat)p);
        }
        public IEnumerable<DairyProduct> FindAllDairyProducts()
        {
            return _products.Where(p => p is DairyProduct).Select(p => (DairyProduct)p);
        }
        public void ChangePriceForProducts(decimal percent)
        {
            foreach (var product in _products)
            {
                product.ChangePrice(percent);
            }
        }
        public void AddProduct(Product product) => _products.Add(product);
        public void RemoveProduct(Product product) => _products.Remove(product);
        public Product this[int index]
        {
            get => index >= 0 && index < _products.Count ? _products[index] : throw new ArgumentOutOfRangeException();
            set
            {
                if (index >= 0 && index < _products.Count)
                {
                    _products[index] = value;
                }
            }
        }
        #endregion
    }
}
