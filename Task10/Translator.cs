using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task10
{
    public class Translator
    {
        private Dictionary<string, string> _vocabulary;
        public string Text { get; private set; }
        public string TextPath { get; private set; }
        public string DictionaryPath { get; private set; }
        public Translator() : this(new Dictionary<string, string>(), "")
        {

        }
        public Translator(Dictionary<string, string> vocabulary,
            string text = "",
            string textPath = @"D:\C# projects\SigmaHomework\Task10\Text.txt",
            string dictionaryPath = @"D:\C# projects\SigmaHomework\Task10\Dictionary.txt")
        {
            _vocabulary = vocabulary;
            Text = text;
            TextPath = textPath;
            DictionaryPath = dictionaryPath;
        }
        public void ChangeText(string text)
        {
            Text = text;
        }
        public void ChangeVocabulary(Dictionary<string, string> vocabulary)
        {
            _vocabulary = vocabulary;
        }
        public string ChangeWords()
        {
            string result = "";

            var words = Text.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            foreach (var word in words)
            {
                string temporaryWord = "";
                string punctuationSign = "";
                if (char.IsPunctuation(word[word.Length - 1]))
                {
                    punctuationSign += word[word.Length - 1];
                    temporaryWord = word[0..(word.Length - 1)];
                }
                else
                {
                    temporaryWord = word;
                }
                if (!_vocabulary.ContainsKey(temporaryWord))
                {
                    AddWordToDictionaryConsole(temporaryWord);
                }
                result += $"{_vocabulary[temporaryWord]}{punctuationSign} ";
            }
            return result;
        }

        private void AddWordToDictionaryConsole(string keyWord)
        {
            Console.Write($"Enter replacement word for the word {keyWord}: ");
            _vocabulary.Add(keyWord, Console.ReadLine());
        }
    }
}
