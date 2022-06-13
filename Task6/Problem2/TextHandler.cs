using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Task6.Problem2
{
    public class TextHandler
    {
        public string Text { get; private set; } = "";

        #region Constructors
        public TextHandler() { }
        public TextHandler(string streamReader)
        {
            using var reader = new StreamReader(streamReader);
            Initialize(reader);
        }
        public TextHandler(StreamReader streamReader)
        {
            Initialize(streamReader);
        }
        #endregion

        #region Methods
        private void Initialize(StreamReader streamReader)
        {
            Text = Regex
                .Replace(streamReader.ReadToEnd(), @"\s+", " ")
                .Trim();
        }
        public IEnumerable<string> DivideTextIntoSentences()
        {
            return Regex.Split(Text, @"(?<=[\.!\?]+['\""]*)\s+");
        }
        public void WriteTextBySentencesInFile(StreamWriter target)
        {
            foreach (var sentence in DivideTextIntoSentences())
            {
                target.WriteLine($"\t{sentence}");
            }
        }
        public void WriteTextBySentencesInFile(string targetPath, bool append)
        {
            using var target = new StreamWriter(targetPath, append);
            WriteTextBySentencesInFile(target);
        }
        public void WriteLongestAndShortestWords()
        {
            var sentences = DivideTextIntoSentences().ToArray();
            var punctuation = Text
                .Where(char.IsPunctuation)
                .Distinct()
                .ToArray();//Get all punctuation from text
            for (int i = 0; i < sentences.Length; i++)
            {
                Console.WriteLine($"Sentence number {i + 1}");

                var words = Regex
                    .Split(sentences[i], @"\d*\s+\d*")//Split sentence by space symbol (i think that digit is not a word,
                                                      //so i splited also by digits)
                    .Select(w => w.Trim(punctuation))//Trim punctuation signs on words
                    .Where(w => w != "")//Delete all "empty" words
                    .Distinct();

                var longestWords = string
                    .Join(", ", words.Where(w => w.Length == words.Max(w => w.Length)));

                var shortestWords = string
                    .Join(", ", words.Where(w => w.Length == words.Min(w => w.Length)));

                Console.WriteLine($"Longest words in sentence: {longestWords}");
                Console.WriteLine($"Shortest words in sentence: {shortestWords}");

                Console.WriteLine(new string('-', 50));
            }
        }
        public override string ToString()
        {
            return Text;
        }
        #endregion
    }
}
