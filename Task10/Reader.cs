using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task10
{
    public static class Reader
    {
        public static IEnumerable<string> ReadText(string path)
        {
            var resultList = new List<string>();
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"File with path {path} was not found");
            }
            using var reader = new StreamReader(path);
            while (!reader.EndOfStream)
            {
                resultList.Add(reader.ReadLine());
            }
            return resultList;
        }
        public static Dictionary<string, string> ReadDictionary(string path)
        {
            var resultDictionary = new Dictionary<string, string>();
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"File with path {path} was not found");
            }
            using var reader = new StreamReader(path);
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var partsOfLine = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (partsOfLine.Length != 2)
                {
                    throw new ArgumentException("Invalid number of arguments for dictionary!");
                }
                resultDictionary.Add(partsOfLine[0], partsOfLine[1]);
            }
            return resultDictionary;
        }
    }
}
