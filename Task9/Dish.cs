using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task9
{
    public class Dish
    {
        private Dictionary<string, decimal> _ingredients;
        public IEnumerable<string> Keys => _ingredients.Keys;
        public Dish()
        {
            _ingredients = new Dictionary<string, decimal>();
        }
        public Dish(Dictionary<string, decimal> ingredients)
        {
            _ingredients = ingredients;
        }
        public decimal this[string key]
        {
            get => _ingredients[key];
        }
        /*public bool TryGetPrice(PriceKurant priceKurant, out decimal price)
        {
            price = 0.0m;
            foreach(var ingredient in _ingredients)
            {

            }
        }*/
    }
}
