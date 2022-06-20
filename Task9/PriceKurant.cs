using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task9
{
    public class PriceKurant
    {
        //key is string (not IIngredient), because i think that it is strange to write full ingredient information in price kurant
        private Dictionary<string, decimal> _ingredientPrices;
        public PriceKurant() : this(new Dictionary<string, decimal>())
        {

        }
        public PriceKurant(Dictionary<string, decimal> ingredientPrices)
        {
            _ingredientPrices = ingredientPrices;
        }
        public void AddIngredient(string ingredientName, decimal ingredientPrice)
        {
            if (_ingredientPrices.ContainsKey(ingredientName))
            {
                throw new ArgumentException($"Ingredient with name {ingredientName} is already exists in price kurant!");
            }
            _ingredientPrices.Add(ingredientName, ingredientPrice);
        }
        public decimal GetIngredientPrice(string ingredientName)
        {
            if (ingredientName == null || !_ingredientPrices.ContainsKey(ingredientName))
            {
                throw new KeyNotFoundException($"There is no ingredient {ingredientName} in price kurant!");
            }
            return _ingredientPrices[ingredientName];
        }
    }
}
