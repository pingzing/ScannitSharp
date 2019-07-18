using System;
using System.Runtime.InteropServices;

namespace ScannitSharp.Bindings
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RustStringBuffer
    {
        internal IntPtr Data;
        internal UIntPtr Len;

        public string[] GetData()
        {
            uint length = Len.ToUInt32();
            string[] strings = new string[length];
            for (int i = 0; i < length; i++)
            {
                var stringPointer = Marshal.ReadIntPtr(Data, i * IntPtr.Size);
                var rustString = new RustString(stringPointer);
                strings[i] = rustString.AsCSharpString();
            }

            return strings;
        }
    }

    public static class Native
    {
        [DllImport("native/scannit_core_ffi")]
        internal static extern RustString get_string();
        [DllImport("native/scannit_core_ffi")]
        internal static extern void free_string(IntPtr rustString);

        [DllImport("native/scannit_core_ffi")]
        public static extern RustStringBuffer get_vector();
        [DllImport("native/scannit_core_ffi")]
        public static extern void free_vector(RustStringBuffer buffer);

        public static string GetString()
        {
            using (var rustString = Native.get_string())
            {
                return rustString.ToString();
            }
        }
    }
}
