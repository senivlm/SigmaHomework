using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task9.ExchangeRates;
using Task9.Services;

namespace Task9.FileWorker
{
    internal class RatesFileReader : IFileReader<Rates>
    {
        public string FilePath { get; }
        private event Func<string?, ExchangeRate> RateParser;
        public RatesFileReader() : this("Course.txt", ModelsValidator.ParseExchangeRate)
        {

        }
        public RatesFileReader(string filePath, Func<string?, ExchangeRate> rateParser)
        {
            FilePath = filePath;
            RateParser += rateParser;
        }

        public Rates ReadFromFile()
        {
            if (!File.Exists(FilePath))
            {
                throw new FileNotFoundException($"File with path {FilePath} does not exist!");
            }
            using var reader = new StreamReader(FilePath);
            var result = new HashSet<ExchangeRate>();

            for (int i = 1; !reader.EndOfStream; i++)
            {
                try
                {
                    var exchangeRate = RateParser(reader.ReadLine());
                    result.Add(exchangeRate);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message} Line number {i}");
                }
            }

            return new Rates(result);
        }
    }
}
