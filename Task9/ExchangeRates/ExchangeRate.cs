using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task9.ExchangeRates
{
    public class ExchangeRate
    {
        public string CurrencyName { get; private set; }
        public decimal Rate { get; private set; }
        public ExchangeRate() : this("", 0.0m)
        {

        }
        public ExchangeRate(string currencyName, decimal rate)
        {
            CurrencyName = currencyName;
            Rate = rate;
        }
        public override int GetHashCode()
        {
            return CurrencyName.GetHashCode();
        }
        public override bool Equals(object? obj)
        {
            if (obj != null && obj is ExchangeRate rate)
            {
                return CurrencyName.Equals(rate.CurrencyName);
            }
            return false;
        }
        public override string ToString()
        {
            return $"{Rate} {CurrencyName}";
        }
    }
}
