using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task7
{
    public static class ConsoleUI
    {
        public static string? SelectRecordToChange(IEnumerable<string> logs)
        {
            if (logs.Any())
            {
                var invalidRecordsArray = logs.ToArray();

                for (int i = 0; i < invalidRecordsArray.Length; i++)
                {
                    Console.WriteLine($"{i + 1}) {invalidRecordsArray[i]}");
                }

                while (true)
                {
                    Console.Write("Select record number you want to change: ");

                    if (int.TryParse(Console.ReadLine(), out int recordNumberToChange) &&
                        recordNumberToChange >= 1 &&
                        recordNumberToChange <= invalidRecordsArray.Length)
                    {
                        return invalidRecordsArray[recordNumberToChange - 1];
                    }
                }
            }
            return null;
        }
        public static string LineReplacement(string? lineToReplace)
        {
            Console.Write($"Write the line that will replace the line \"{lineToReplace}\": ");
            return Console.ReadLine() ?? "";
        }
    }
}
