using Task10.Problem1;
using Task10.Problem2;

namespace Task10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;
            try
            {
                //Problem1
                //Дана програма може бути використана для перекладу великих файлів і обробки великих словників,
                //оскільки зчитування і переклад відбувається пострічково. Ціною цієї переваги є той недолік,
                //що програма буде працювати повільніше, ніж могла б
                using (var translator = new Translator(
                    @"D:\C# projects\SigmaHomework\Task10\Problem1\Text.txt",
                    @"D:\C# projects\SigmaHomework\Task10\Problem1\Dictionary.txt",
                    @"D:\C# projects\SigmaHomework\Task10\Problem1\Result.txt", false))
                {
                    translator.TranslateText(2);
                }

                //Problem2
                //Я зробив 2 реалізації завдання: одну, як і вказано в умові, в класі, який був створений раніше.
                //Оскільки не можна в одному класі двічі реалізувати IEnumerable, то я вирішив також зробити
                //реалізацію цього завдання через абстрактний клас, яка, на мій погляд, є доречнішою
                Console.WriteLine("\n" + new string('-', 50));
                Console.WriteLine("Diagonal snake matrix in DiagonalSnakeMatrix class: ");
                AbstractMatrix betterRealization = new DiagonalSnakeMatrix(
                    new int[,] {
                    { 1, 2, 3, 4 },
                    { 5, 6, 7, 8 },
                    { 9, 10, 11, 12 },
                    { 13, 14, 15, 16 }
                });
                betterRealization.OutputMatrix();
                foreach (var element in betterRealization)
                {
                    Console.Write($"{element} ");
                }

                Console.WriteLine("\n" + new string('-', 50));
                Console.WriteLine("Horizontal snake matrix in HorizontalSnakeMatrix class: ");
                betterRealization = new HorizontalSnakeMatrix(
                    new int[,] {
                    { 1, 2, 3, 4 },
                    { 5, 6, 7, 8 },
                    { 9, 10, 11, 12 },
                    { 13, 14, 15, 16 }
                });
                betterRealization.OutputMatrix();
                foreach (var element in betterRealization)
                {
                    Console.Write($"{element} ");
                }

                Console.WriteLine("\n" + new string('-', 50));
                Console.WriteLine("Diagonal snake matrix in MatrixChanger class (was created earlier): ");
                MatrixChanger worseRealization = new MatrixChanger(new int[,] {
                    { 1, 2, 3, 4 },
                    { 5, 6, 7, 8 },
                    { 9, 10, 11, 12 },
                    { 13, 14, 15, 16 }
                });
                worseRealization.OutputMatrix();
                foreach (var element in worseRealization)
                {
                    Console.Write($"{element} ");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
    }
}