using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task9.FileWorker
{
    public class PriceKurantFileReader : IFileReader<PriceKurant>
    {
        public string FilePath { get; }
        public PriceKurantFileReader() : this("Prices.txt")
        {

        }
        public PriceKurantFileReader(string filePath)
        {
            FilePath = filePath;
        }
        public PriceKurant ReadFromFile()
        {
            if (!File.Exists(FilePath))
            {
                throw new FileNotFoundException($"File with path {FilePath} does not exist!");
            }
            using var streamReader = new StreamReader(FilePath);
            var result = new Dictionary<string, decimal>();

            for (int i = 1; !streamReader.EndOfStream; i++)
            {
                try
                {
                    var partsOfLine = streamReader.ReadLine()?.Split(" - ", StringSplitOptions.TrimEntries);
                    if (partsOfLine == null || partsOfLine.Length != 2)
                    {
                        throw new ArgumentException("Incorrect number of arguments for price kurant!");
                    }
                    if (!decimal.TryParse(partsOfLine[1].Replace('.', ','), out decimal pricePerKilo) || pricePerKilo < 0)
                    {
                        throw new ArgumentException("Invalid price per kilogram!");
                    }
                    if (!result.ContainsKey(partsOfLine[0]))
                    {
                        result.Add(partsOfLine[0], pricePerKilo);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message} Line number {i}");
                }
            }

            return new PriceKurant(result);
        }
    }
}
