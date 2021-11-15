using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace martunenko
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("pathDictionary");
            //string pathDictionary = Console.ReadLine();

            //Console.WriteLine("pathTestWords");
            //string pathTestWords = Console.ReadLine();

            //GermanDictionary germanDictionary = new GermanDictionary(pathDictionary, pathTestWords);

            GermanDictionary germanDictionary = new GermanDictionary(@"C:\Users\User\Desktop\martunenko\txt\de-dictionary.tsv",
            @"C:\Users\User\Desktop\martunenko\txt\de-test-words.tsv");

            string pathResult = @"C:\Users\User\Desktop\martunenko\txt\result.txt";
            germanDictionary.SplitWord(pathResult);
        }
        public class GermanDictionary
        {
            string[] dictionary;
            string[] testWords;

            public GermanDictionary(string pathDictionary, string pathTestWords)
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

            public void SplitWord(string pathResult)
            {
                foreach (string testWord in testWords)
                {
                    List<string> listResult = new List<string>();
                    if (!CanSplitWord(testWord, listResult))
                    {
                        listResult.Add(testWord);
                    }


                    StringBuilder sb = new StringBuilder($"(in) {testWord} -> (out)");

                    foreach (var word in listResult)
                    {
                        sb.Append($" {word},");
                    }
                    sb.Remove(sb.Length - 1, 1);

                    PrintResutlFile(pathResult, sb.ToString());
                }
            }

            private bool CanSplitWord(string testWord, List<string> listResult)
            {
                int symbolAfterI = 0;
                for (byte i = (byte)(testWord.Length - 1); i > 0; i--)
                {
                    symbolAfterI++;
                    if (dictionary.Contains(testWord.Substring(0, i)))
                    {
                        if (CanSplitWord(testWord.Substring(i, symbolAfterI), listResult))
                        {
                            listResult.Add(testWord.Substring(0, i));
                            return true;
                        }

                        else
                            return false;
                    }
                }
                if (dictionary.Contains(testWord))
                {
                    listResult.Add(testWord);
                    return true;
                }
                else
                    return false;
            }

            public void PrintResutlFile(string path, string result)
            {
                using StreamWriter stream = new StreamWriter(path, true);
                stream.WriteLine(result);
            }
        }
    }
}