using Task13.Models;
using Task13.Services;

namespace Task13
{
    internal class Program
    {
        static async Task Main()
        {
            var servedCustomersFileWriter = new FileWriter(@"D:\C# projects\SigmaHomework\Task13\Files\ServedCustomers.txt");
            try
            {
                //Генерація людей і їх запис у файл
                /*using (var generatesWriter = new FileWriter(@"D:\C# projects\SigmaHomework\Task13\Files\PeopleInformation.txt"))
                {
                    generatesWriter.WriteCollection(GenerationService.GeneratePersonsString(10, 18, 90, 6, 1000, 5000));
                }*/

                //Додаю обробники для події обслуговування клієнта (запис інформації про обслужених клієнтів
                //і каси якими вони обслуговувались)
                Paydesk.OnServedCustomerEvent += Console.WriteLine;
                Paydesk.OnServedCustomerEvent += servedCustomersFileWriter.WriteLine;

                //Створюю симулятор і запускаю всі каси
                var paydesksLoad = new PaydesksLoadSimulator(
                    new List<Paydesk>() {
                        new Paydesk(1, 0, 20, true, 10),
                        new Paydesk(3, 0, 30, true, 10),
                        new Paydesk(4, 0, 40, true, 5),
                        new Paydesk(5, 0, 50, true, 10)
                    });

                //Додаю обробник події коли яка-небудь каса буде переповнена
                paydesksLoad.OnQueueOverflowAction += paydesksLoad.RepurposePaydeskAsync;

                //Додаю клієнтів з часовим інтервалом в 300 мілісекунд
                var customersAdding = paydesksLoad.AddCustomersWithTimeIntervalAsync(
                    ValidationService.ValidatePeople(File.ReadAllLines(@"D:\C# projects\SigmaHomework\Task13\Files\PeopleInformation.txt")),
                    300);

                //Чекаю поки всі клієнти будуть додані
                await customersAdding;
                Console.WriteLine("All customers was added!");

                //Отримую контрольну групу пасажирів
                var controlGroup = await paydesksLoad.GetPassangersControlGroupAsync("XIFHXY");
                //Записую її в файл
                using (var writer = new FileWriter(@"D:\C# projects\SigmaHomework\Task13\Files\PassengersControlGroup.txt"))
                {
                    writer.WriteCollection(controlGroup);
                }
                Console.WriteLine("Control group of passangers was written!");

                //Змінюю тип обслуговування 3 каси на "ZQYZDZ"
                Console.WriteLine("Repurposing desk number 3 on ZQYZDZ...");
                await paydesksLoad.RepurposePaydeskAsync(3, "ZQYZDZ");
                Console.WriteLine("Repurposed successfully...");

                //Закриваю касу номер 5 і одночасно з цим перерозприділяю людей з цієї каси в інші каси
                Console.WriteLine("Closing paydesk number 5...");
                await paydesksLoad.ClosePaydeskAsync(5);
                Console.WriteLine("Closed successfully...");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
            servedCustomersFileWriter.Dispose();
        }
    }
}