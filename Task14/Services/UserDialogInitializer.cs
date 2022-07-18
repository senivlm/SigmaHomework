using System.Text;
using Task14.GoodsModels.GoodsInterfaces;
using Task14.GoodsModels;

namespace Task14.Services
{
    public static class UserDialogInitializer
    {
        private static StringBuilder InitializeProduct()
        {
            StringBuilder stringBuilder = new StringBuilder();

            Console.Write("Enter product name: ");
            stringBuilder.Append(Console.ReadLine() + " ");

            Console.Write("Enter product weight: ");
            stringBuilder.Append(Console.ReadLine() + " ");

            Console.Write("Enter product price: ");
            stringBuilder.Append(Console.ReadLine());

            return stringBuilder;
        }
        public static IGoods InitializeOrdinaryProduct()
        {
            StringBuilder stringBuilder = InitializeProduct();

            return ValidationService.ValidateGoods(stringBuilder.ToString(), typeof(OrdinaryProduct));
        }
        public static IGoods InitializeMeat()
        {
            StringBuilder stringBuilder = InitializeProduct();

            Console.Write("TopGrade;\nFirstGrade;\nSecondGrade;\nWrite meat category: ");
            stringBuilder.Append(" " + Console.ReadLine()?.Replace(" ", ""));

            Console.Write("Mutton;\nVeal;\nPork;\nChicken;\nWrite meat type: ");
            stringBuilder.Append(" " + Console.ReadLine());

            return ValidationService.ValidateGoods(stringBuilder.ToString(), typeof(MeatProduct));
        }
        public static IGoods InitializeDairyProduct()
        {
            StringBuilder stringBuilder = InitializeProduct();

            Console.Write("Enter expiration date of the product (in days): ");
            stringBuilder.Append(" " + Console.ReadLine());

            return ValidationService.ValidateGoods(stringBuilder.ToString(), typeof(DairyProduct));
        }
        public static IEnumerable<IGoods> InitializeStorageData()
        {
            List<IGoods> goods = new List<IGoods>();

            Console.Write("Enter number of goods you want to add to storage: ");

            if (int.TryParse(Console.ReadLine(), out int numberOfGoods))
            {
                while (numberOfGoods > 0)
                {
                    Console.Write("1) Meat;\n2) Dairy product;\n3) Ordinary product\nSelect product type: ");
                    if (int.TryParse(Console.ReadLine(), out int number) && number <= 3 && number >= 1)
                    {
                        IGoods good = null;

                        try
                        {
                            switch (number)
                            {
                                case 1:
                                    good = InitializeMeat();
                                    break;
                                case 2:
                                    good = InitializeDairyProduct();
                                    break;
                                case 3:
                                    good = InitializeOrdinaryProduct();
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            continue;
                        }

                        goods.Add(good);
                        numberOfGoods--;
                    }
                }
            }
            return goods;
        }
    }
}
