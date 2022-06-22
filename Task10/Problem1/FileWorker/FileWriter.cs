using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task10.FileWorker
{
    public class FileWriter : IFileWriter<string>
    {
        public StreamWriter Writer { get; }
        public FileWriter() : this(@"D:\C# projects\SigmaHomework\Task10\Problem1\Result.txt", false) { }
        public FileWriter(string path, bool append = false) : this(
            File.Exists(path) ?
            new StreamWriter(path, append) :
            throw new FileNotFoundException($"File with path {path} does not exist!"))
        { }
        public FileWriter(StreamWriter writer)
        {
            Writer = writer;
        }
        
        public void WriteLineToFile(string line)
        {
            Writer.WriteLine(line);
        }
        public void Dispose()
        {
            Writer.Dispose();
        }
    }
}
