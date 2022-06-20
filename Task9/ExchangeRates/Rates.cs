using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task9.ExchangeRates
{
    public class Rates
    {
        private HashSet<ExchangeRate> _exchangeRates;
        public Rates() : this(new HashSet<ExchangeRate>())
        {

        }
        public Rates(HashSet<ExchangeRate> exchangeRates)
        {
            _exchangeRates = exchangeRates;
        }
        public ExchangeRate? this[string currencyName]
        {
            get => _exchangeRates.FirstOrDefault(er => er.CurrencyName == currencyName);
        }
        public IEnumerable<string> GetCurrencyNames() => _exchangeRates.Select(er => er.CurrencyName);
    }
}
