using System;
using System.Runtime.InteropServices;

namespace ScannitSharp.Bindings
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RustBuffer
    {
        private IntPtr Data;
        private UIntPtr Len;
        private UIntPtr Capacity;

        internal byte[] AsByteArray()
        {
            uint length = Len.ToUInt32();
            byte[] bytes = new byte[length];
            for (int i = 0; i < length; i++)
            {
                bytes[i] = Marshal.ReadByte(Data);
            }

            return bytes;
        }

        internal string AsString()
        {
            var rustString = new RustString(Data, (int)Len.ToUInt32());
            return rustString.ToString();
        }

        internal FFIHistory[] AsFFIHistoryArray()
        {
            //uint length = Len.ToUInt32();
            //FFIHistory[] ffiHistories = new FFIHistory[length];
            //for (int i = 0; i < length; i++)
            //{
            //    Marshal.Read
            //}
            return null;
        }
    }
}
