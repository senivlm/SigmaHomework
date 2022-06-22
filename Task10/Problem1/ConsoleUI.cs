using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Task10.Problem1
{
    public static class ConsoleUI
    {
        public static string AddWordTranslation(string wordToTranslate, int attemptsToTranslate)
        {
            string? translation = null;
            for (int i = 0; i < attemptsToTranslate && translation == null; i++)
            {
                Console.Write($"Enter translation of word \"{wordToTranslate}\": ");
                translation = Console.ReadLine();
            }
            if (translation == null)
            {
                throw new ArgumentNullException($"Translation of word \"{wordToTranslate}\" can not be null!");
            }
            return translation;
        }
    }
}
