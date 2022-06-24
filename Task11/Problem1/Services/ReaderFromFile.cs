using Task11.Problem1.Products;

namespace Task11.Problem1.Services
{
    public static class ReaderFromFile
    {
        public static event Action<string>? OnExceptionAction;
        public static Dictionary<MeatCategory, decimal> GetMeatPriceChanges(string sourceFile)
        {
            ValidationService.ValidatePath(sourceFile);
            Dictionary<MeatCategory, decimal> result = new Dictionary<MeatCategory, decimal>();
            foreach (var line in File.ReadLines(sourceFile))
            {
                string[] parts = line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 2)
                {
                    OnExceptionAction?.Invoke("Wrong number of arguments for meat price changes!");
                    continue;
                }
                if (!Enum.TryParse(parts[0], true, out MeatCategory category))
                {
                    OnExceptionAction?.Invoke("Wrong name of meat category!");
                    continue;
                }
                if (!decimal.TryParse(parts[1], out decimal percantаge))
                {
                    OnExceptionAction?.Invoke("Wrong percentage for meat category!");
                    continue;
                }
                result.Add(category, percantаge);
            }
            return result;
        }
        public static SortedDictionary<int, decimal> GetDairyProductsPriceChanges(string sourceFile)
        {
            ValidationService.ValidatePath(sourceFile);
            SortedDictionary<int, decimal> result = new SortedDictionary<int, decimal>();
            foreach (var line in File.ReadLines(sourceFile))
            {
                string[] parts = line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 2)
                {
                    OnExceptionAction?.Invoke("Wrong number of arguments for dairy products changes!");
                    continue;
                }
                if (!int.TryParse(parts[0], out int expirationDate))
                {
                    OnExceptionAction?.Invoke("Wrong expiration date!");
                    continue;
                }
                if (!decimal.TryParse(parts[1], out decimal percantаge))
                {
                    OnExceptionAction?.Invoke("Wrong percentage for dairy product!");
                    continue;
                }
                result.Add(expirationDate, percantаge);
            }
            return result;
        }
        public static IEnumerable<IGoods> ReadGoodsFromFile(string sourceFilePath, int changePathAttempts = 0)
        {
            var goodsList = new List<IGoods>();

            for (int attempt = 0; attempt < changePathAttempts && !File.Exists(sourceFilePath ?? ""); attempt++)
            {
                Console.WriteLine("Invalid source file path! Enter valid path: ");
                sourceFilePath = Console.ReadLine();
            }

            using var sourceReader = new StreamReader(File.Exists(sourceFilePath) ?
                sourceFilePath :
                @"D:\C# projects\SigmaHomework\Task11\Problem1\StorageInformation\StorageDB.txt");

            while (!sourceReader.EndOfStream)
            {
                var line = sourceReader.ReadLine();
                IGoods good = null;
                Type? type = null;

                try
                {
                    var elementsInLine = line?.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (elementsInLine == null || elementsInLine.Length == 0)
                    {
                        throw new ArgumentNullException("Line is empty!");
                    }

                    type = Type.GetType($"Task11.Problem1.Products.{elementsInLine[0]}", false, true);
                    if (type == null)
                    {
                        throw new ArgumentNullException("Invalid type parameter!");
                    }

                    good = ValidationService.ValidateGoods(string.Join(' ', elementsInLine[1..]), type);
                }
                catch (Exception exception)
                {
                    OnExceptionAction?.Invoke($"{exception.GetType().Name}: {exception.Message} " +
                        $"Line: {line} " +
                        $"Inspection date: {DateTime.Now}");
                }

                if (good != null)
                {
                    goodsList.Add(good);
                }
            }

            return goodsList;
        }
    }
}
