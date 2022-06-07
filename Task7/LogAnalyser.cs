using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task7
{
    public class LogAnalyser
    {
        public string ErrorLogFilePath { get; }
        public string SourceFilePath { get; }
        public LogAnalyser() : this(@"D:\C# projects\SigmaHomework\Task7\ErrorLog.txt",
            @"D:\C# projects\SigmaHomework\Task7\StorageDB.txt")
        { }
        public LogAnalyser(string errorLogFilePath, string sourceFilePath)
        {
            ErrorLogFilePath = File.Exists(errorLogFilePath) ?
                errorLogFilePath :
                @"D:\C# projects\SigmaHomework\Task7\ErrorLog.txt";
            SourceFilePath = File.Exists(sourceFilePath) ?
                sourceFilePath :
                @"D:\C# projects\SigmaHomework\Task7\StorageDB.txt";
        }
        public void UpdateRecordAfterSpecifiedDate(DateOnly date)
        {
            var invalidRecords = new Dictionary<string, bool>();

            using (var log = new StreamReader(ErrorLogFilePath))
            {
                while (!log.EndOfStream)
                {
                    string line = log.ReadLine();
                    var partsOfLine = line.Split(new string[] { " Line: ", " Inspection date: " }, 10, StringSplitOptions.None);

                    if (partsOfLine.Length == 3)
                    {
                        var lineDate = DateOnly.FromDateTime(DateTime.Parse(partsOfLine[2]));

                        if (lineDate > date && !invalidRecords.ContainsKey(partsOfLine[1]))
                        {
                            invalidRecords.Add(partsOfLine[1], false);
                        }
                    }
                }
            }

            using (var source = new StreamReader(SourceFilePath))
            {
                while (!source.EndOfStream)
                {
                    string line = source.ReadLine();

                    if (invalidRecords.ContainsKey(line))
                    {
                        invalidRecords[line] = true;
                    }
                }
            }

            UpdateStorageFileRecord(invalidRecords.Where(kv => kv.Value == true).Select(kv => kv.Key));
        }

        public void UpdateStorageFileRecord(IEnumerable<string> invalidRecords)
        {
            if (invalidRecords.Any())
            {
                var invalidRecordsArray = invalidRecords.ToArray();

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
                        var StorageDB = new List<string>();

                        using (var sourceReader = new StreamReader(SourceFilePath))
                        {
                            while (!sourceReader.EndOfStream)
                            {
                                StorageDB.Add(sourceReader.ReadLine());
                            }
                        }

                        if (StorageDB.Contains(invalidRecordsArray[recordNumberToChange - 1]))
                        {
                            Console.Write($"Write the line that will replace the line \"{invalidRecordsArray[recordNumberToChange - 1]}\": ");
                            string replacementLine = Console.ReadLine() ?? "";

                            StorageDB[StorageDB.IndexOf(invalidRecordsArray[recordNumberToChange - 1])] = replacementLine;

                            using (var sourceWriter = new StreamWriter(SourceFilePath, false))
                            {
                                foreach (var element in StorageDB)
                                {
                                    sourceWriter.WriteLine(element);
                                }
                            }

                            using var logWriter = new StreamWriter(ErrorLogFilePath, true);
                            logWriter.WriteLine($"Line \"{invalidRecordsArray[recordNumberToChange - 1]}\" was successfully replaced with line \"{replacementLine}\"");
                        }

                        break;
                    }
                }
            }
        }
    }
}
