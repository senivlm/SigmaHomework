using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Task10.FileWorker;

namespace Task10.Problem1
{
    public class Translator : IDisposable
    {
        private FileWorker.TextReader _textReader;
        private DictionaryReader _dictionaryReader;
        private FileWriter _writer;
        public Translator() :
            this(new FileWorker.TextReader(),
                new DictionaryReader(),
                new FileWriter())
        {

        }
        public Translator(string textPath, string dictionaryPath, string resultPath, bool appendResult) :
            this(new FileWorker.TextReader(textPath),
                new DictionaryReader(dictionaryPath),
                new FileWriter(resultPath, appendResult))
        {

        }
        public Translator(FileWorker.TextReader textReader, DictionaryReader dictionaryReader, FileWriter writer)
        {
            _textReader = textReader;
            _dictionaryReader = dictionaryReader;
            _writer = writer;
        }
        public void TranslateText(int attemptsToAddTranslation)
        {
            var lineFromText = _textReader.GetLineFromFile();//get first line of text

            while (lineFromText != null)//do while not end of text file
            {
                var wordsFromLine = GetWordsFromLine(lineFromText);//Split sentence by words
                var wordReplacements = new Dictionary<string, string>();//Dictionary with word from sentence-translation pairs

                foreach (var word in wordsFromLine)
                {
                    var isUpper = char.IsUpper(word, 0);//Variable to check if first letter of word upper case
                    KeyValuePair<string, string>? dictionaryLine = null;
                    try
                    {
                        dictionaryLine = _dictionaryReader.GetLineFromFile();//get first line from dictionary
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                        continue;
                    }
                    string? wordEquivalent = null;//translation of word

                    while (dictionaryLine != null && wordEquivalent == null)
                    {
                        if (string.Equals(dictionaryLine.Value.Key, word, StringComparison.InvariantCultureIgnoreCase))
                        {
                            wordEquivalent = dictionaryLine.Value.Value;
                        }

                        try
                        {
                            dictionaryLine = _dictionaryReader.GetLineFromFile();
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine(ex.Message);
                            continue;
                        }
                    }
                    ((IFileReader<KeyValuePair<string, string>?>)_dictionaryReader).RestartStreamReader();//to reuse StreamReader

                    if (wordEquivalent == null)
                    {
                        try
                        {
                            //exception is thrown when user write wrong translation
                            wordEquivalent = ConsoleUI.AddWordTranslation(word.ToLower(), attemptsToAddTranslation);

                            var dictionaryPath = ((IFileReader<KeyValuePair<string, string>?>)_dictionaryReader).GetPath();

                            if (dictionaryPath != null)
                            {
                                //Dispose dictionary to write new translation
                                _dictionaryReader.Dispose();
                                using (var writerOfNewTranslation = new StreamWriter(dictionaryPath, true))
                                {
                                    writerOfNewTranslation.WriteLine($"{word} - {wordEquivalent}");
                                }
                                //Create new reader for dictionary
                                _dictionaryReader = new DictionaryReader(dictionaryPath);
                            }
                        }
                        catch (ArgumentNullException)
                        {
                            Console.WriteLine("Unable to translate! Some words has no translation!");
                            throw;
                        }
                    }

                    wordReplacements.Add(word, isUpper ? char.ToUpper(wordEquivalent[0]) + wordEquivalent[1..] : wordEquivalent);
                }

                //write translated line to file
                _writer.WriteLineToFile(TranslateLine(lineFromText, wordReplacements));

                lineFromText = _textReader.GetLineFromFile();
            }

            ((IFileReader<string?>)_textReader).RestartStreamReader();
            Console.WriteLine("Text was successfully translated!");
        }
        private static string TranslateLine(string lineToTranslate, Dictionary<string, string> translates)
        {// стрічка буде постійно змінюватись, тому можна працювати з StringBuilder.
        // Якщо слова в словнику не буде, то не виконаєм уиову задачі.
            var newLine = lineToTranslate;
            foreach (var wordTranslation in translates)
            {
                newLine = Regex.Replace(newLine, $@"\b{wordTranslation.Key}\b", wordTranslation.Value);
            }
            return newLine;
        }
        private static HashSet<string> GetWordsFromLine(string line)
        {
            return Regex.Matches(line, @"\b[\p{L}]+[']?\b").Select(m => m.Value).ToHashSet();
        }

        public void Dispose()
        {
            _writer.Dispose();
            _textReader.Dispose();
            _dictionaryReader.Dispose();
        }
    }
}
