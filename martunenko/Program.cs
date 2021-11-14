using System;
using System.IO;

namespace martunenko
{
    class Program
    {
        static void Main(string[] args)
        {
            GermanDictionary germanDictionary = new GermanDictionary(@"C:\Users\User\Desktop\martunenko\txt\de-dictionary.tsv",
                                                                     @"C:\Users\User\Desktop\martunenko\txt\de-test-words.tsv");

            string pathResult = "pathResult";
            germanDictionary.SplitWord(pathResult);
        }
        public class GermanDictionary
        {
            string[] dictionary;
            string[] testWords;

            public GermanDictionary (string pathDictionary, string pathTestWords)
            {
                dictionary = arrayToLowerCase(File.ReadAllLines(pathDictionary));
                testWords = arrayToLowerCase(File.ReadAllLines(pathTestWords));
            }

            private string[] arrayToLowerCase(string[] a)
            {
                string[] result = new string[a.Length];

                for (int i = 0; i < a.Length; i++)
                {
                    result[i] = a[i].ToLower();
                }
                return result;
            }

            public void SplitWord (string pathResult)
            {
                foreach (string testWord in testWords)
                {
                    if (true)
                    {
                        Console.WriteLine(testWord);
                    }
                    PrintResutlFile("path", "result");
                }
            }

            public void PrintResutlFile(string path, string result)
            {
                Console.WriteLine(path);
                Console.WriteLine(result);
            }
        }
    }
}
