using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task9.Dishes;
using Task9.Ingredients;
using Task9.Services;

namespace Task9.FileWorker
{
    public class MenuFileReader : IFileReader<Menu>
    {
        public string FilePath { get; }
        private event Func<string?, IIngredient> IngredientParser;
        private event Func<string?, Dictionary<IIngredient, int>, IDish> DishParser;
        public MenuFileReader() : this("Menu.txt", ModelsValidator.ParseIngredient, ModelsValidator.ParseDish)
        {

        }
        public MenuFileReader(string filePath,
            Func<string?, IIngredient> ingredientParser,
            Func<string?, Dictionary<IIngredient, int>, IDish> dishParser)
        {
            FilePath = filePath;
            IngredientParser += ingredientParser;
            DishParser += dishParser;
        }

        public Menu ReadFromFile()
        {
            if (!File.Exists(FilePath))
            {
                throw new FileNotFoundException($"File with path {FilePath} does not exist!");
            }
            using var streamReader = new StreamReader(FilePath);
            var result = new Dictionary<IDish, int>();

            for (int i = 1; !streamReader.EndOfStream; i++)
            {
                var dishInformation = streamReader.ReadLine();
                int dishIndex = i++;
                var ingredients = new Dictionary<IIngredient, int>();
                try
                {
                    while (true)
                    {
                        try
                        {
                            var ingredientInformation = streamReader.ReadLine();

                            if (string.IsNullOrWhiteSpace(ingredientInformation))
                            {
                                break;
                            }

                            var indexOfIngredientSeparator = ingredientInformation.LastIndexOf(", ");
                            if (indexOfIngredientSeparator == -1)
                            {
                                throw new ArgumentException("Incorrect number of arguments for ingredient-weight pair!");
                            }

                            var ingredient = IngredientParser(ingredientInformation[..indexOfIngredientSeparator].Trim());

                            if (!int.TryParse(ingredientInformation[(indexOfIngredientSeparator + 1)..].Trim(), out int weight) ||
                                weight < 0)
                            {
                                throw new ArgumentException("Invalid ingredient weight!");
                            }

                            if (!ingredients.ContainsKey(ingredient))
                            {
                                ingredients.Add(ingredient, 0);
                            }
                            ingredients[ingredient] += weight;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"{ex.Message} Line number {i}");
                        }
                        i++;
                    }

                    var partsOfDishLine = dishInformation?.Split(" - ", StringSplitOptions.TrimEntries);
                    if (partsOfDishLine == null || partsOfDishLine.Length == 0 || partsOfDishLine.Length > 2)
                    {
                        throw new ArgumentException("Incorrect number of arguments for dish-amount pair!");
                    }

                    var dish = DishParser(partsOfDishLine[0], ingredients);
                    var amountOfDishes = 1;
                    if (partsOfDishLine.Length == 2)
                    {
                        if (!int.TryParse(partsOfDishLine[1], out amountOfDishes) ||
                            amountOfDishes < 0)
                        {
                            throw new ArgumentException("Invalid dish amount!");
                        }
                    }

                    if (!result.ContainsKey(dish))
                    {
                        result.Add(dish, 0);
                    }
                    result[dish] += amountOfDishes;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message} Line number {dishIndex}");
                }
            }

            return new Menu(result);
        }
    }
}
