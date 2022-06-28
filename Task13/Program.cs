namespace Task13
{
    public delegate T OperationDelegate<T>(List<T> values);
    internal class Program
    {
        static void Main()
        {
            var goodsList = new List<IGoods>
            {
                new Meat("Chicken",30.99m,1.0m,MeatCategory.TopGrade,MeatType.Chicken),
                new Meat("Meat",11.90m,1.0m,MeatCategory.SecondGrade,MeatType.Mutton),
                new Meat("Meat2",12.00m,1.0m,MeatCategory.SecondGrade,MeatType.Mutton),
                new Meat("Meat3",15.12m,1.0m,MeatCategory.SecondGrade,MeatType.Mutton),
                new Meat("Meat4",45.90m,1.0m,MeatCategory.SecondGrade,MeatType.Mutton)
            };

            var intList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            OperationDelegate<int> operation = (List<int> list) => list.Max();

            Console.WriteLine(operation?.Invoke(intList));

            Console.ReadLine();
        }
        static int GetMax(List<int> values)
        {
            return values.Max();
        }
        static IGoods GetMaxPriceGood(List<IGoods> goods)
        {
            return goods.MaxBy(g => g.Price);
        }
        static int GetMin(List<int> values)
        {
            return values.Min();
        }
    }
}