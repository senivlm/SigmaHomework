using Task14.Logger;
using Task14.Services;
using Task14.StorageInformation;
using Task14.Factories;
using Task14.GoodsModels;
using Task14.GoodsModels.GoodsInterfaces;
using Task14.GoodsModels.GoodsAbstractions;

namespace Task14
{
    internal class Program
    {
        static void Main()
        {
            try
            {
                var utilizationFileWriter = new UtilizationFileWriter(
                    @"D:\C# projects\SigmaHomework\Task14\Files\Utilization.txt");

                //Цей клас мені необхідно ще доопрацювати в контексті серіалізації/десеріалізації
                var logAnalyser = new LogAnalyser(
                    @"D:\C# projects\SigmaHomework\Task14\Files\ErrorLog.txt",
                    @"D:\C# projects\SigmaHomework\Task14\Files\StorageDB.txt");
                
                //Створюю сервіс для серіалізації складу, куди передаю фабрику для серіалізації XML
                var serializationService = new SerializationService<Storage>(new XmlSerializationFactory<Storage>());

                //Додаю необхідні обробники до різних класів
                Storage.OnAddingExpiredGoodsEvent += utilizationFileWriter.AddUtilizationRecord;
                serializationService.OnExceptionAction += logAnalyser.AddLog;
                serializationService.OnExceptionAction += Console.WriteLine;

                //Десеріалізую склад, для якого застосовується паттерн Singleton
                var storage = serializationService.Deserialize(@"D:\C# projects\SigmaHomework\Task14\Files\StorageDB.xml");

                Console.WriteLine($"Hash: {storage?.GetHashCode()}\n{storage}");

                var chair = new Chair("Chair", 20.00m, 20, 30, 40, Enums.Material.MDF);

                //Серіалізую промисловий товар (для серіалізації використовувався паттерн "Абстрактна фабрика")
                //Аналогічно можна серіалізувати і продовольчі товари, і склад
                var chairSerailizer = new SerializationService<GoodsBase>(new XmlSerializationFactory<GoodsBase>());
                chairSerailizer.Serialize(chair, @"D:\C# projects\SigmaHomework\Task14\Files\Test.xml");
                var newChair = chairSerailizer.Deserialize(@"D:\C# projects\SigmaHomework\Task14\Files\Test.xml");
                Console.WriteLine(newChair);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
    }
}