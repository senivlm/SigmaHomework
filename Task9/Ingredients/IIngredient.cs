using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task9.Ingredients
{
    public interface IIngredient
    {
        public string Name { get; }
        int GetHashCode();
        bool Equals(object? obj);
        string ToString();
    }
}
