using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ScannitSharp.Bindings
{
    internal class RustString
    {
        private IntPtr _ptr;
        private int _length;
        private string _cSharpString;

        /// <summary>
        /// Prepares to read a pointer to a sequence of C-style chars, and discovers its length
        /// by walking the string until it encounters a NUL terminator.
        /// </summary>
        /// <param name="ptr"></param>
        internal RustString(IntPtr ptr)
        {
            _ptr = ptr;
            while (Marshal.ReadByte(ptr, _length) != 0)
            {
                ++_length;
            }
        }

        /// <summary>
        /// Prepares to read a pointer to a sequence of C-style chars.
        /// </summary>
        /// <param name="ptr">Pointer to C-style bytes, terminated with a NUL char.</param>
        /// <param name="length">Length of the array of chars (includes the NUL terminator).</param>
        internal RustString(IntPtr ptr, int length)
        {
            _ptr = ptr;
            _length = length;
        }

        internal string AsCSharpString()
        {
            byte[] buffer = new byte[_length];
            Marshal.Copy(_ptr, buffer, 0, buffer.Length);
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
