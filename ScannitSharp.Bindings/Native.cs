using System;
using System.Runtime.InteropServices;

namespace ScannitSharp.Bindings
{
    public static class Native
    {
        [DllImport("native/scannit_core_ffi")]
        internal static extern RustString get_string();
        [DllImport("native/scannit_core_ffi")]
        internal static extern void free_string(IntPtr rustString);

        [DllImport("native/scannit_core_ffi")]
        internal static extern RustStringBuffer get_vector();
        [DllImport("native/scannit_core_ffi")]
        internal static extern void free_vector(RustStringBuffer buffer);

        public static string GetString()
        {
            using (var rustString = Native.get_string())
            {
                return rustString.ToString();
            }
        }

        public static string[] GetStringArray()
        {
            using (var rustBuffer = Native.get_vector())
            {
                return rustBuffer.AsStringArray();
            }
        }
    }
}
