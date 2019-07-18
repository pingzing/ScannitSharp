using System;
using System.Runtime.InteropServices;

namespace ScannitSharp.Bindings
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RustStringBuffer : IDisposable
    {
        private IntPtr Data;
        private UIntPtr Len;

        internal string[] AsStringArray()
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

        public void Dispose()
        {
            Native.free_vector(this);
        }
    }
}
