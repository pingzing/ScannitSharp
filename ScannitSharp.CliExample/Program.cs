using ScannitSharp.Bindings;
using System;

namespace ScannitSharp.CliExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Native.GetString());

            var strings = Native.GetStringArray();
            Console.WriteLine($"Call to get_vector() returned {strings.Length} strings:");
            foreach (var str in strings)
            {
                Console.WriteLine($"\t{str}");
            }
        }
    }
}
