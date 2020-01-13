using System;
using System.Collections;
using System.Text;
public class BloomFilter<T>
{
    private int arraySize;
    private int hashCount;

    // number of words in wordlist.txt (dictionary)
    private readonly int setSize;
    private BitArray bitmap;

    private ILogger logger;
    
    public BloomFilter(int setSize, int size, int numHashes, ILogger logger = null)
    {
        this.setSize = setSize;
        arraySize = size;
        hashCount = numHashes;

        bitmap = new BitArray(arraySize);

        this.logger = logger;
        if(this.logger != null)
        {
            this.logger.WriteToLog($"hash count: {hashCount}");
            this.logger.WriteToLog($"bit size: {arraySize}");
            this.logger.WriteToLog($"set size: {setSize}");
        }
        
    }

    public BloomFilter(int setSize, int size, ILogger logger = null)
    {
        this.setSize = setSize;
        arraySize = size;
        hashCount = OptimalNumberOfHashes();
        

        bitmap = new BitArray(arraySize);

        this.logger = logger;
        if(this.logger != null)
        {
            this.logger.WriteToLog($"hash count: {hashCount}");
            this.logger.WriteToLog($"bit size: {arraySize}");
            this.logger.WriteToLog($"set size: {setSize}");
        }
    }

    /// <summary>
    /// Adds a given item to Bloom filter.
    /// </summary>
    /// <returns></returns>
    public void Add(T item)
    {
        if(item == null) return;

        var rand = new Random(item.GetHashCode());
        for(int i=0; i < hashCount; i++)
        {
            var idx = rand.Next(arraySize);
            bitmap[idx] = true;
        }
    }

    /// <summary>
    /// Checks if a given item exists in the Bloom filter.
    /// </summary>
    /// <returns>True if item probably exists, False if it does not exist.</returns>
    public bool Contains(T item)
    {
        if(bitmap == null) 
        {
            this.logger.WriteToLog("Bloom filter is uninitialized.");
            return false;
        }
        var rand = new Random(item.GetHashCode());
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
    /// Method for testing purposes to log bitmap as binary.
    /// Needs ILogger supplied to Bloom filter to work.
    /// </summary>
    /// <returns></returns>
    public void PrintBitmap()
    {
        if(logger == null) return;

        StringBuilder sb = new StringBuilder(arraySize);
        foreach(var bit in bitmap)
        {
            sb.Append(Convert.ToString((bool)bit ? 1 : 0));
        }
        logger.WriteToLog(sb.ToString());
    }

    /// <summary>
    /// Calculates the optimal number of hashes based on bloom filter
    /// bit size and set size.
    /// Reference https://hur.st/bloomfilter/.
    /// </summary>
    /// <returns>The optimal number of hashes</returns>
    private int OptimalNumberOfHashes()
    {
        return (int)Math.Round((arraySize / setSize) * Math.Log(2.0));
    }
}