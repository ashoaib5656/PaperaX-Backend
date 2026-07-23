using System;
using System.Net;
using System.Web;

class Program
{
    static void Main()
    {
        string original = "abc+def/ghi=";
        string urlEncoded = Uri.EscapeDataString(original);
        Console.WriteLine("Original: " + original);
        Console.WriteLine("Encoded: " + urlEncoded);
        Console.WriteLine("Decoded: " + Uri.UnescapeDataString(urlEncoded));
    }
}
