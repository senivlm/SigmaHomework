using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task9.Dishes;
using Task9.ExchangeRates;
using Task9.Ingredients;
using Task9.Services;

namespace Task9
{
    public class Menu
    {
        //Dictionary, because the restaurant needs ingredients, their amount and prices not for one dish, but for many
        private Dictionary<IDish, int> _dishes;
        public Menu() : this(new Dictionary<IDish, int>())
        {

        }
        public Menu(Dictionary<IDish, int> dishes)
        {
            _dishes = dishes;
        }
        private class IngredientRequirement//Class for method GetRestaurantDemands()
        {
            public int IngredientAmount { get; set; }
            public decimal IngredientPrice { get; set; }
            public override string ToString()
            {
                return $"{IngredientAmount} grams -> {IngredientPrice}";
            }
        }
        public string GetRestaurantDemands(PriceKurant priceKurant, int numberOfAttempts, ExchangeRate rate)
        {//використання інтерфейса актуальне
            var ingredientInformation = new Dictionary<IIngredient, IngredientRequirement>();

            foreach (var dish in _dishes)
            {
                foreach (var ingredient in dish.Key.GetDishIngredients())
                {
                    var ingredientPrice = 0.0m;
                    for (int i = 0; i < numberOfAttempts; i++)
                    {
                        try
                        {
                            ingredientPrice = priceKurant.GetIngredientPrice(ingredient.Name);
                            break;
                        }
                        catch (KeyNotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                            if (ConsoleUI.TryAddInformationToPriceKurant(ingredient.Name, out decimal pricePerKilo))
                            {
                                priceKurant.AddIngredient(ingredient.Name, pricePerKilo);
                            }
                        }
                    }
                    if (ingredientPrice <= 0.0m)
                    {
                        throw new ArgumentException($"Invalid price of ingredient {ingredient}!");
                    }
                    if (!ingredientInformation.ContainsKey(ingredient))
                    {
                        ingredientInformation.Add(ingredient, new IngredientRequirement { IngredientAmount = 0, IngredientPrice = 0.0m });
                    }
                    ingredientInformation[ingredient].IngredientAmount +=
                            dish.Key[ingredient] * dish.Value;
                    ingredientInformation[ingredient].IngredientPrice =
                        ingredientInformation[ingredient].IngredientAmount *
                        priceKurant.GetIngredientPrice(ingredient.Name) *
                        rate.Rate / 1000.0m;
                }
            }

            return ingredientInformation.CreateStringBuilderWithCurrencyValue(rate.CurrencyName).ToString();
        }
    }
}
