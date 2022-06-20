using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task9.Ingredients;

namespace Task9.Dishes
{
    public class Dish : IDish
    {
        public string Name { get; private set; }
        private Dictionary<IIngredient, int> _ingredients;
        public Dish() : this("", new Dictionary<IIngredient, int>())
        {

        }
        public Dish(string name, Dictionary<IIngredient, int> ingredients)
        {
            Name = name;
            _ingredients = ingredients;
        }
        public IEnumerable<IIngredient> GetDishIngredients() => _ingredients.Keys;
        public int this[IIngredient key]
        {
            get => _ingredients[key];
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
        public override bool Equals(object? obj)
        {
            if (obj != null && obj is Dish dish)
            {
                return Name.Equals(dish.Name);
            }
            return false;
        }
    }
}
