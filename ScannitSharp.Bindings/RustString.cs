using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ScannitSharp.Bindings
{
    internal class RustString : SafeHandle
    {
        private string _cSharpString;

        internal RustString(IntPtr ptr) : base(IntPtr.Zero, true)
        {
            this.handle = ptr;
        }

        internal RustString() : base(IntPtr.Zero, true) { }

        public override bool IsInvalid => false;
        protected override bool ReleaseHandle()
        {
            Native.free_string(handle);
            return true;
        }

        internal string AsCSharpString()
        {
            int len = 0;
            while (Marshal.ReadByte(handle, len) != 0)
            {
                ++len;
            }

            byte[] buffer = new byte[len];
            Marshal.Copy(handle, buffer, 0, buffer.Length);
            return Encoding.UTF8.GetString(buffer);
        }

        public override string ToString()
        {
            if (_cSharpString == null)
            {
                _cSharpString = AsCSharpString();
            }

            return _cSharpString;
        }
    }
}
