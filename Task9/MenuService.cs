using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task9
{
    public static class MenuService
    {
        public static bool TryGetMenuTotalSum(Menu menu, PriceKurant priceKurant, out decimal totalPrice)
        {

        }
        public static bool TryGetDishPrice(Dish dish, PriceKurant priceKurant, out decimal totalPrice)
        {
            totalPrice = 0.0m;
            foreach(var key in dish.Keys)
            {

            }
        }
    }
}
