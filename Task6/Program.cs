using Task6.Problem1;
using Task6.Problem2;

namespace Task6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Problem 1
            Quarter quarter = new Quarter();
            try
            {
                using StreamReader reader = new StreamReader(@"D:\C# projects\SigmaHomework\Task6\Problem1\ReportOnElectricityConsumption.txt");
                quarter = new Quarter(reader, 1.44m);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            PersonData data = new PersonData();

            Console.WriteLine("Printing all report to file...");
            quarter.PrintReportToFile(@"D:\C# projects\SigmaHomework\Task6\Problem1\UserFriendlyReport.txt", false);
            Console.WriteLine("Success!");
            Console.ReadLine();

            Console.WriteLine("Printing report for specific apartment (apartment number 31)...");
            quarter.PrintRecordToFile(@"D:\C# projects\SigmaHomework\Task6\Problem1\UserFriendlyReport.txt", false, 31);
            Console.WriteLine("Success!");
            Console.ReadLine();

            Console.WriteLine("Printing record where owner of the apartment has the largest debt...");
            quarter.PrintRecordWithLargestDebt(@"D:\C# projects\SigmaHomework\Task6\Problem1\UserFriendlyReport.txt", false);
            Console.WriteLine("Success!");
            Console.ReadLine();

            Console.WriteLine("Printing records where electricity was not used in apartment during current quarter...");
            quarter.PrintRecordsWithoutElectricityUsage(@"D:\C# projects\SigmaHomework\Task6\Problem1\UserFriendlyReport.txt", false);
            Console.WriteLine("Success!");
            Console.ReadLine();

            Console.WriteLine("Printing expense report...");
            quarter.PrintExpenseReport(@"D:\C# projects\SigmaHomework\Task6\Problem1\UserFriendlyReport.txt", false);
            Console.WriteLine("Success!");
            Console.ReadLine();

            Console.WriteLine("Printing activity report...");
            quarter.PrintActivityReport(@"D:\C# projects\SigmaHomework\Task6\Problem1\UserFriendlyReport.txt", false);
            Console.WriteLine("Success!");
            Console.ReadLine();

            //Problem 2
            TextHandler textHandler = new TextHandler(@"D:\C# projects\SigmaHomework\Task6\Problem2\TextFile.txt");

            textHandler.WriteTextBySentencesInFile(@"D:\C# projects\SigmaHomework\Task6\Problem2\Result.txt", false);

            textHandler.WriteLongestAndShortestWords();

            Console.ReadLine();
        }
    }
}