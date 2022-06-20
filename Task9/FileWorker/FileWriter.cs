using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task9.FileWorker
{
    public class FileWriter
    {
        public string FilePath { get; }
        public FileWriter() : this(@"D:\C# projects\SigmaHomework\Task9\Result.txt")
        {

        }
        public FileWriter(string filePath)
        {
            FilePath = filePath;
        }

        public void WriteToFile(string lineToWrite, bool append = false)
        {
            if (!File.Exists(FilePath))
            {
                throw new FileNotFoundException($"File with path {FilePath} does not exist!");
            }
            using var streamWriter = new StreamWriter(FilePath, append);
            streamWriter.WriteLine(lineToWrite);
            Console.WriteLine($"String was successfully written to the file {FilePath}");
        }
    }
}
