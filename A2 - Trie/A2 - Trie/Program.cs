using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A2Trie
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //init the Trie
            Trie<string, string> testTrie = new Trie<string, string>();

            //test add 
            testTrie.Add("black", "noir");
            testTrie.Add("blue", "bleu");
            testTrie.Add("red", "rouge");
            testTrie.Add("yellow", "jaune");
            testTrie.Add("white", "blanc");

            //test translate            
            //Console.WriteLine(testTrie.Translate("black"));
            //Console.WriteLine(testTrie.Translate("blue"));
            //Console.WriteLine(testTrie.Translate("red"));
            //Console.WriteLine(testTrie.Translate("white"));
           // Console.WriteLine(testTrie.Translate("yellow"));

            //test word does not exist when translating
            //Console.WriteLine(testTrie.Translate("HelloWorld"));

            //test removal of word
            testTrie.Remove("black");

            //test translate of now - removed word
            Console.WriteLine(testTrie.Translate("black"));

            //test translate again of blue so that removing black didn't destroy the trie
            Console.WriteLine(testTrie.Translate("blue"));

            //test removal of word that never existed
            testTrie.Remove("HelloWorld");

            //test Print
            //testTrie.Print();

            // end of program
            Console.Write("press any key to exit...");
            Console.ReadKey(); 
        }
    }
}
