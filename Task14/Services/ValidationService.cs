using Task14.Enums;
using Task14.GoodsModels;
using Task14.GoodsModels.GoodsInterfaces;

namespace Task14.Services
{
    public static class ValidationService
    {
        public static IGoods ValidateGoods(string lineToValidate, Type goodsType)
        {
            var good = Activator.CreateInstance(goodsType);

            good = good switch
            {
                OrdinaryProduct => ValidateOrdinaryProduct(lineToValidate),
                MeatProduct => ValidateMeat(lineToValidate),
                DairyProduct => ValidateDairyProduct(lineToValidate),
                _ => throw new NotImplementedException($"Can not validate object of type {goodsType}!"),
            };

            return (IGoods)good;
        }
        private static (string name, decimal price, double weight) ValidateProduct(string productLine)
        {
            var partsOfProductLine = productLine?.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (partsOfProductLine == null || partsOfProductLine.Length != 3)
            {
                throw new ArgumentException("Incorrect number of arguments for product!");
            }

            if (!char.IsUpper(partsOfProductLine[0][0]))
            {
                partsOfProductLine[0] = char.ToUpper(partsOfProductLine[0][0]) + partsOfProductLine[0][1..];
            }

            if (!decimal.TryParse(partsOfProductLine[1].Replace('.', ','), out decimal price))
            {
                throw new FormatException("Invalid product price!");
            }

            if (!double.TryParse(partsOfProductLine[2].Replace('.', ','), out double weight))
            {
                throw new FormatException("Invalid product weight!");
            }

            return (partsOfProductLine[0], price, weight);
        }
        private static OrdinaryProduct ValidateOrdinaryProduct(string ordinaryProductLine)
        {
            var (name, price, weight) = ValidateProduct(ordinaryProductLine);
            return new OrdinaryProduct(name, price, weight);
        }
        private static MeatProduct ValidateMeat(string meatLine)
        {
            var partsOfMeatLine = meatLine?.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (partsOfMeatLine == null || partsOfMeatLine.Length != 5)
            {
                throw new ArgumentException("Incorrect number of arguments for meat product!");
            }

            if (!Enum.TryParse(partsOfMeatLine[3], true, out MeatCategory category))
            {
                throw new FormatException("Invalid meat category!");
            }

            if (!Enum.TryParse(partsOfMeatLine[4], true, out MeatType type))
            {
                throw new FormatException("Invalid meat type!");
            }

            var (name, price, weight) = ValidateProduct(string.Join(' ', partsOfMeatLine[0..3]));
            return new MeatProduct(name, price, weight, category, type);
        }
        private static DairyProduct ValidateDairyProduct(string dairyProductLine)
        {
            var partsOfDairyProductLine = dairyProductLine?.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (partsOfDairyProductLine == null || partsOfDairyProductLine.Length != 4)
            {
                throw new ArgumentException("Incorrect number of arguments for dairy product!");
            }

            if (!DateTime.TryParse(partsOfDairyProductLine[3], out DateTime expirationDate))
            {
                throw new FormatException("Invalid expiration date of dairy product!");
            }

            var (name, price, weight) = ValidateProduct(string.Join(' ', partsOfDairyProductLine[0..3]));
            return new DairyProduct(name, price, weight, expirationDate);
        }
        public static void ValidatePath(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"File {path} was not found!");
            }
        }
    }
}
