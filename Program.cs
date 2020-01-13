using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bloomfilter
{
    class Program
    {
        static void Main(string[] args)
        {
            // ~1MB (1*1024*1024*8)
            int bitArraySize = 9000000;
            int dictionarySize = 340000;

            Console.WriteLine("Bloom filter stats:");
            var logger = new DefaultLogger();
            var filter = new BloomFilter<string>(dictionarySize, bitArraySize, logger);

            // word cache only created for testing purposes
            var words = new HashSet<string>(dictionarySize);
            
            Console.WriteLine();

            Console.WriteLine("type 'q' to quit from program");
            Console.WriteLine("type 'a' to add dictionary to Bloom filter");
            Console.WriteLine("type 'c' to check if a word exists in the Bloom filter");
            Console.WriteLine("type 'w' to test if all words in the dictionary exist in the Bloom filter");
            Console.WriteLine("type 'f' to test false-positive % returned by Bloom filter");
            Console.WriteLine("type 'p' to test sanity of Bloom filter map by printing it as Binary");
            Console.WriteLine();
            string input;
            do {
                input = Console.ReadLine();
                if(input == "a")
                {
                    using (FileStream fs = File.Open("./wordlist.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (BufferedStream bs = new BufferedStream(fs))
                    using (StreamReader sr = new StreamReader(bs))
                    {
                        string word;
                        while ((word = sr.ReadLine()) != null)
                        {
                            filter.Add(word);
                            words.Add(word);
                        }
                        Console.WriteLine("Done adding words to bloom filter.");
                    }
                }
                else if(input == "c")
                {
                    Console.WriteLine("Enter word:");
                    var word = System.Console.ReadLine();
                    if(filter.Contains(word))
                    {
                        Console.WriteLine($"{word} probably exists in bloom filter.");
                    }
                    else
                    {
                        Console.WriteLine($"{word} does not exist in bloom filter.");
                    }
                }
                else if(input == "w")
                {
                    TestAllWordsBelongToDictionary(filter, words);
                }
                else if(input == "f")
                {
                    TestPercentageFalsePositives(filter, words);
                }
                else if(input == "p")
                {
                    filter.PrintBitmap();
                }
            } while(input != "q");
        }

        /// <summary>
        /// Tests if all words in input dictionary exist in Bloom filter.
        /// </summary>
        static void TestAllWordsBelongToDictionary(BloomFilter<string> filter, IEnumerable<string> words)
        {
            if(!words.Any())
            {
                Console.WriteLine("Bloom filter is uninitialized. Choose option 'a' to add dictionary to Bloom filter.");
                return;
            }
            foreach(var w in words)
            {
                var check = filter.Contains(w);
                if(!check)
                {
                    Console.WriteLine("Error! This word actually belongs to the dictionary but the bloom filter reports false.");
                    Console.WriteLine(w);
                }
            }
            Console.WriteLine("Success! All words belong to dictionary.");
        }



        /// <summary>
        /// Test computes the % of false positives in Bloom filter and prints the result to console.
        /// </summary>
        private static void TestPercentageFalsePositives(BloomFilter<string> filter, HashSet<string> words)
        {
            var wordCount = words.Count();
            int falsePositive = 0;
            foreach(var word in words)
            {
                Random r = new Random();
                string random = 
                    new string(word.ToCharArray().OrderBy(s => r.Next()).ToArray());

                if(filter.Contains(random) && !words.Contains(random))
                {
                    Console.WriteLine(random);
                    falsePositive++;
                }
            }
            var pctg = (double)falsePositive*100/wordCount;
            Console.WriteLine($"False positive % of Bloom filter: {pctg}");
            
        }
    }
}
