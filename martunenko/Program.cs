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
            //Console.WriteLine("Укажите путь к словарю");
            //string pathDictionary = Console.ReadLine();

            //Console.WriteLine("Укажите путь к словам для обработки");
            //string pathTestWords = Console.ReadLine();

            //Console.WriteLine("Укажить путь для записи результата");
            //string pathResult = Console.ReadLine();

            //GermanDictionary germanDictionary = new GermanDictionary(pathDictionary, pathTestWords);

            GermanDictionary germanDictionary = new GermanDictionary(@"C:\Users\User\Desktop\martunenko\txt\de-dictionary.tsv",
            @"C:\Users\User\Desktop\martunenko\txt\de-test-words.tsv");
            string pathResult = @"C:\Users\User\Desktop\martunenko\txt\result martunenko.txt";

            germanDictionary.SplitTestWord(pathResult);
        }


        public class GermanDictionary
        {
            string[] dictionary;
            string[] testWords;
            int a1 = 0;
            int a2 = 0;
            int a3 = 0;
            int a4 = 0;
            int a5 = 0;

            public GermanDictionary(string pathDictionary, string pathTestWords)
            {
                dictionary = ArrayToLowerCase(File.ReadAllLines(pathDictionary));
                testWords = ArrayToLowerCase(File.ReadAllLines(pathTestWords));
            }

            private string[] ArrayToLowerCase(string[] a)
            {
                string[] result = new string[a.Length];

                for (int i = 0; i < a.Length; i++)
                {
                    result[i] = a[i].ToLower();
                }
                return result;
            }

            public void SplitTestWord(string pathResult)
            {
                foreach (string testWord in testWords)
                {
                    List<string> listResult = new List<string>();

                    if (!CanSplitWord(testWord, listResult))
                    {
                        listResult.Add(testWord);
                    }
                    listResult.Reverse();

                    StringBuilder sb = new StringBuilder($"(in) {testWord} -> (out)");

                    foreach (var word in listResult)
                    {
                        sb.Append($" {word},");
                    }
                    sb.Remove(sb.Length - 1, 1);                                            // удаление последней запятой

                    PrintResutlFile(pathResult, sb.ToString());
                    
                }
                Console.WriteLine(a1);
                Console.WriteLine(a2);
                Console.WriteLine(a3);
                Console.WriteLine(a4);
                Console.WriteLine(a5);
            }

            private bool CanSplitWord(string testWord, List<string> listResult)             // if true разбитие произошло, listResult.Add записал составние
            {


                int symbolAfterI = 0;                                                       // кол-во букв в "слове остатке"
                for (byte i = (byte)(testWord.Length - 1); i > 0; i--)
                {
                    symbolAfterI++;
                    if (dictionary.Contains(testWord.Substring(0, i)))
                    {
                        if (CanSplitWord(testWord.Substring(i, symbolAfterI), listResult))
                        {
                            listResult.Add(testWord.Substring(0, i));
                            a1++;
                            return true;
                        }
                        else if (dictionary.Contains(testWord))
                        {
                            listResult.Add(testWord);
                            a2++;
                            return true;
                        }
                        else
                            a3++;
                        return false;
                    }
                }
                if (dictionary.Contains(testWord))
                {
                    listResult.Add(testWord);
                    a4++;
                    return true;
                }
                else
                    a5++;
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