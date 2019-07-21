using ScannitSharp.Bindings;
using System;

namespace ScannitSharp.CliExample
{
    class Program
    {
        static void Main(string[] args)
        {
            string stringFromRust = Native.GetString();
            Console.WriteLine(stringFromRust);

            string[] strings = Native.GetStringArray();
            Console.WriteLine($"Call to get_vector() returned {strings.Length} strings:");
            foreach (var str in strings)
            {
                Console.WriteLine($"\t{str}");
            }
        }
    }
}
