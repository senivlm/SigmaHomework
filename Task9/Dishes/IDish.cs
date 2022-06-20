using Task9.Ingredients;

namespace Task9.Dishes
{
    public interface IDish
    {
        string Name { get; }
        int this[IIngredient key] { get; }
        IEnumerable<IIngredient> GetDishIngredients();
        bool Equals(object? obj);
        int GetHashCode();
    }
}