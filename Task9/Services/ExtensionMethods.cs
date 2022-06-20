using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task9.Services
{
    public static class ExtensionMethods
    {
        public static StringBuilder CreateStringBuilderWithCurrencyValue<TKey, TValue>(this Dictionary<TKey, TValue> keyValuePairs,
            string currencyName) where TKey : notnull
        {
            var stringBuilder = new StringBuilder();

            foreach (var keyValuePair in keyValuePairs)
            {
                stringBuilder.AppendLine($"{keyValuePair.Key} - {keyValuePair.Value} {currencyName}");
            }
            stringBuilder.Length -= 2;//delete last "\n" character

            return stringBuilder;
        }
    }
}
