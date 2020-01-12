using System;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
public class BloomFilter
{
    private int arraySize;
    private int hashCount;

    private readonly int setSize = 340000;
    private BitArray bitmap;

    private MD5 hashAlgorithm;
    
    public BloomFilter(int size, int numHashes)
    {
        arraySize = size;
        hashCount = numHashes;

        bitmap = new BitArray(arraySize);
        hashAlgorithm = MD5.Create();
    }

    public BloomFilter(int size)
    {
        arraySize = size;
        hashCount = OptimalNumberOfHashes();
        Console.WriteLine("hash count: {0}", hashCount);

        bitmap = new BitArray(arraySize);
        hashAlgorithm = MD5.Create();

        Console.WriteLine("bit size: {0}", arraySize);
        Console.WriteLine("set size: {0}", setSize);
    }

    public void Add(string word)
    {
        if(string.IsNullOrEmpty(word)) return;

        //var bytes = Encoding.UTF8.GetBytes(word);
        // var bytes = BitConverter.GetBytes(item.GetHashCode());
        /* var md5Hash = hashAlgorithm.ComputeHash(bytes);
        var bitArray = new BitArray(md5Hash);
        int len = arraySize/hashCount; */
        var rand = new Random(word.GetHashCode());
        for(int i=0; i < hashCount; i++)
        {
            var idx = rand.Next(arraySize);
            bitmap[idx] = true;
        }
    }

    public bool Contains(string word)
    {
        var rand = new Random(word.GetHashCode());
        for(int i=0; i < hashCount; i++)
        {
            var idx = rand.Next(arraySize);
            if(!bitmap[idx])
            {
                return false;
            }
        }
        return true;
    }

        /// <summary>
        /// Calculates the optimal number of hashes based on bloom filter
        /// bit size and set size.
        /// </summary>
        /// <param name="bitSize">Size of the bloom filter in bits (m)</param>
        /// <param name="setSize">Size of the set (n)</param>
        /// <returns>The optimal number of hashes</returns>
        private int OptimalNumberOfHashes()
        {
            return (int)Math.Ceiling((arraySize / setSize) * Math.Log(2.0));
        }
}