using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task8.Problem3
{
    public class LogAnalyser
    {
        public string ErrorLogFilePath { get; }
        public string SourceFilePath { get; }
        public LogAnalyser() : this(@"D:\C# projects\SigmaHomework\Task8\Problem3\ErrorLog.txt",
            @"D:\C# projects\SigmaHomework\Task8\Problem3\StorageDB.txt")
        { }
        public LogAnalyser(string errorLogFilePath, string sourceFilePath)
        {
            ErrorLogFilePath = File.Exists(errorLogFilePath) ?
                errorLogFilePath :
                @"D:\C# projects\SigmaHomework\Task8\Problem3\ErrorLog.txt";
            SourceFilePath = File.Exists(sourceFilePath) ?
                sourceFilePath :
                @"D:\C# projects\SigmaHomework\Task8\Problem3\StorageDB.txt";
        }
        public IEnumerable<string> GetLogsAfterSpecifiedDate(DateOnly date)
        {
            var logs = new HashSet<string>();

            using (var logReader = new StreamReader(ErrorLogFilePath))
            {
                while (!logReader.EndOfStream)
                {
                    var partsOfLine = logReader.ReadLine()?.Split(new string[] { " Line: ", " Inspection date: " }, 3, StringSplitOptions.None);

                    if (partsOfLine != null && partsOfLine.Length == 3)
                    {
                        if (DateTime.TryParse(partsOfLine[2], out DateTime logDateTime))
                        {
                            var logDate = DateOnly.FromDateTime(logDateTime);

                            if (logDate > date)
                            {
                                logs.Add(partsOfLine[1]);
                            }
                        }
                    }
                }
            }
            return logs;
        }
        public IEnumerable<string> GetExistingLogs(IEnumerable<string> logsEnumerable)
        {
            var logs = logsEnumerable.ToDictionary(str => str, str => false);

            using (var source = new StreamReader(SourceFilePath))
            {
                while (!source.EndOfStream)
                {
                    string? line = source.ReadLine();

                    if (line != null && logs.ContainsKey(line))
                    {
                        logs[line] = true;
                    }
                }
            }

            return logs.Where(kv => kv.Value == true).Select(kv => kv.Key);
        }
        public void UpdateRecordAfterSpecifiedDate(DateOnly date)
        {
            var existingLogs = GetExistingLogs(GetLogsAfterSpecifiedDate(date));

            UpdateStorageFileRecord(existingLogs);
        }

        public void UpdateStorageFileRecord(IEnumerable<string> invalidRecords)
        {
            var recordToChange = ConsoleUI.SelectRecordToChange(invalidRecords);

            if (recordToChange != null)
            {
                var StorageDB = new List<string>();

                using (var sourceReader = new StreamReader(SourceFilePath))
                {
                    while (!sourceReader.EndOfStream)
                    {
                        StorageDB.Add(sourceReader.ReadLine());
                    }
                }

                if (StorageDB.Contains(recordToChange))
                {
                    var replacementLine = ConsoleUI.LineReplacement(recordToChange);

                    StorageDB[StorageDB.IndexOf(recordToChange)] = replacementLine;

                    using (var sourceWriter = new StreamWriter(SourceFilePath, false))
                    {
                        foreach (var element in StorageDB)
                        {
                            sourceWriter.WriteLine(element);
                        }
                    }

                    using var logWriter = new StreamWriter(ErrorLogFilePath, true);
                    logWriter.WriteLine($"Line \"{recordToChange}\" was successfully replaced with line \"{replacementLine}\"");
                }
            }
        }
    }
}
