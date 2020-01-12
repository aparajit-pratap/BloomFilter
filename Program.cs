using System;
using System.IO;
using System.Collections.Generic;

namespace bloomfilter
{
    class Program
    {
        static void Main(string[] args)
        {
            int bitArraySize = 9000000;
            int dictionarySize = 340000;

            var filter = new BloomFilter(bitArraySize);

            using (FileStream fs = File.Open("./wordlist.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (BufferedStream bs = new BufferedStream(fs))
            using (StreamReader sr = new StreamReader(bs))
            {
                string word;
                var words = new List<string>(dictionarySize);
                while ((word = sr.ReadLine()) != null)
                {
                    filter.Add(word);
                    words.Add(word);
                }
                System.Console.WriteLine("Done adding words to bloom filter");

                foreach(var w in words)
                {
                    var check = filter.Contains(w);
                    if(!check)
                    {
                        System.Console.WriteLine("we have a problem!");
                        System.Console.WriteLine(w);
                    }
                }
                System.Console.WriteLine("Done checking if all words belong to dictionary");

                if(!filter.Contains("Aparajit"))
                {
                    Console.WriteLine("Success!");
                }
            }
        }
    }
}
