using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task9.Dishes;
using Task9.ExchangeRates;
using Task9.Ingredients;

namespace Task9.Services
{
    public static class ModelsValidator
    {
        public static ExchangeRate ParseExchangeRate(string? lineToParse)
        {
            string[]? partsOfLine = lineToParse?.Split(' ', StringSplitOptions.TrimEntries);

            if (partsOfLine == null || partsOfLine.Length != 2)
            {
                throw new ArgumentException("Incorrect number of arguments for exchange rate!");
            }
            if (!decimal.TryParse(partsOfLine[0].Replace('.', ','), out decimal rate))
            {
                throw new ArgumentException("Invalid exchange rate!");
            }

            return new ExchangeRate(partsOfLine[1], rate);
        }
        public static IDish ParseDish(string? lineToParse, Dictionary<IIngredient, int> ingredients)
        {
            if (lineToParse == null || ingredients == null)
            {
                throw new ArgumentNullException("Can't parse null to dish!");
            }

            return new Dish(lineToParse, ingredients);
        }
        public static IIngredient ParseIngredient(string? lineToParse)
        {
            if (lineToParse == null)
            {
                throw new ArgumentNullException("Can't parse null to ingredient!");
            }

            return new Ingredient(lineToParse);
        }
    }
}
