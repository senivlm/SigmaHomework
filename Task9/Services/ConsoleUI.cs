using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task9.ExchangeRates;

namespace Task9.Services
{
    public static class ConsoleUI
    {
        public static bool TryAddInformationToPriceKurant(string ingreadientName, out decimal ingredientPrice)
        {
            Console.Write($"Enter price per kilogram in UAH for {ingreadientName}: ");
            if (!decimal.TryParse(Console.ReadLine()?.Replace('.', ','), out decimal pricePerKilo))
            {
                ingredientPrice = 0.0m;
                return false;
            }
            ingredientPrice = pricePerKilo;
            return true;
        }
        public static string ChooseCurrency(Rates rates)
        {
            var currencyNames = rates.GetCurrencyNames().ToArray();
            for (int i = 0; i < currencyNames.Length; i++)
            {
                Console.WriteLine($"{i + 1}) {currencyNames[i]}");
            }
            int numberOfCurrencyName;
            while (true)
            {
                Console.Write("Enter number of currency name that will be used: ");
                if (int.TryParse(Console.ReadLine(), out numberOfCurrencyName) &&
                    numberOfCurrencyName > 0 &&
                    numberOfCurrencyName <= currencyNames.Length)
                {
                    break;
                }
            }
            return currencyNames[numberOfCurrencyName - 1];
        }
    }
}
