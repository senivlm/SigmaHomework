using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task10.FileWorker
{
    public class DictionaryReader : IFileReader<KeyValuePair<string, string>?>
    {
        public StreamReader Reader { get; }
        public DictionaryReader() : this(@"D:\C# projects\SigmaHomework\Task10\Problem1\Dictionary.txt") { }
        public DictionaryReader(string path) : this(
            File.Exists(path) ?
            new StreamReader(path) :
            throw new FileNotFoundException($"File with path {path} does not exist!"))
        { }
        public DictionaryReader(StreamReader reader)
        {
            Reader = reader;
        }
        public KeyValuePair<string, string>? GetLineFromFile()
        {
            var line = Reader.ReadLine();
            if (line == null)
            {
                return null;
            }
            var partsOfLine = line.Split(" - ", StringSplitOptions.TrimEntries);
            if (partsOfLine.Length != 2)
            {
                throw new ArgumentException($"Invalid number of arguments in dictionary! Line: \"{line}\"");
            }
            return new KeyValuePair<string, string>(partsOfLine[0].ToLower(), partsOfLine[1].ToLower());
        }

        public void Dispose()
        {
            Reader.Dispose();
        }
    }
}
