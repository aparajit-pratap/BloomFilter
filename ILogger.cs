using System;

/// <summary>
/// Implement this interface and pass it to the Bloom filter
/// to log Bloom filter parameters for debugging purposes. 
/// </summary>
public interface ILogger
{
    public void WriteToLog(string msg);
}

/// <summary>
/// Default implementation of ILogger to print Bloom filter parameters to Console. 
/// </summary>
public class DefaultLogger : ILogger
{
    public void WriteToLog(string msg)
    {
        Console.WriteLine(msg);
    }
}