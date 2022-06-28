using Task12.Problem1.Products;
using Task12.Problem1.Services;

namespace Task12.Problem1.StorageInformation
{
    public class UtilizationFileWriter
    {
        private readonly string _utilizationFilePath;
        public UtilizationFileWriter(string utilizationFilePath =
            @"D:\C# projects\SigmaHomework\Task12\Problem1\StorageInformation\Utilization.txt")
        {
            ValidationService.ValidatePath(utilizationFilePath);
            _utilizationFilePath = utilizationFilePath;
        }
        public void AddUtilizationRecord(Storage storage, IGoods goods)
        {
            storage.RemoveGood(goods);
            using var writer = new StreamWriter(_utilizationFilePath, false);
            writer.WriteLine($"Product addition date: {DateTime.Now}\n{goods}{new string('-', 50)}");
        }
    }
}
