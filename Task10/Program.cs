namespace Task10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var text = Reader.ReadText(@"D:\C# projects\SigmaHomework\Task10\Text.txt");
                var dictionary = Reader.ReadDictionary(@"D:\C# projects\SigmaHomework\Task10\Dictionary.txt");
                var translator = new Translator(dictionary);
                foreach (var item in text)
                {
                    translator.ChangeText(item);
                    Console.WriteLine(translator.ChangeWords());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}