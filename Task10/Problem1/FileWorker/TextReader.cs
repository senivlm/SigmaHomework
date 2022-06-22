using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task10.FileWorker
{
    public class TextReader : IFileReader<string?>
    {
        public StreamReader Reader { get; }
        public TextReader() : this(@"D:\C# projects\SigmaHomework\Task10\Problem1\Text.txt") { }
        public TextReader(string path) : this(
            File.Exists(path) ?
            new StreamReader(path) :
            throw new FileNotFoundException($"File with path {path} does not exist!"))
        { }
        public TextReader(StreamReader reader)
        {
            Reader = reader;
        }

        public string? GetLineFromFile()
        {
            return Reader.ReadLine();
        }
        public void Dispose()
        {
            Reader.Dispose();
        }
    }
}
