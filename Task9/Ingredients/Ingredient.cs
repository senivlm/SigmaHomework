using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task9.Ingredients
{
    public class Ingredient : IIngredient
    {
        public string Name { get; private set; }
        public Ingredient() : this("") { }
        public Ingredient(string name)
        {
            Name = name ?? "";
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
        public override bool Equals(object? obj)
        {
            if (obj != null && obj is Ingredient ingredient)
            {
                return Name.Equals(ingredient.Name);
            }
            return false;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
