using ScannitSharp.Bindings;
using System;

namespace ScannitSharp.CliExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Native.GetString());

            var vector = Native.get_vector();
            vector.GetData();
            Native.free_vector(vector);
        }
    }
}
