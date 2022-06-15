using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task9
{
    internal class Menu
    {
        private List<Dish> _dishes;
        public Menu()
        {
            _dishes = new List<Dish>();
        }
        public Menu(List<Dish> dishes)
        {
            _dishes = dishes;
        }
    }
}
