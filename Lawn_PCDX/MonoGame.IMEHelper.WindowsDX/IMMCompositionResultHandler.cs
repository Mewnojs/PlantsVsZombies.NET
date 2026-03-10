using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;

namespace MonoGame.IMEHelper
{
    internal abstract class IMMCompositionResultHandler
    {
        internal IntPtr IMEHandle { get; set; }

        public int Flag { get; private set; }

        internal IMMCompositionResultHandler(int flag)
        {
            this.Flag = flag;
            this.IMEHandle = IntPtr.Zero;
        }

        internal virtual void Update() { }

        internal bool Update(int lParam)
        {
            if ((lParam & Flag) == Flag)
            {
                Update();
                return true;
            }
            return false;
        }
    }

    internal class IMMCompositionString : IMMCompositionResultHandler, IEnumerable<byte>
    {
        private byte[] _values;

        public int Length { get; private set; }

        public byte[] Values { get { return _values; } }

        public byte this[int index] { get { return _values[index]; } }

        internal IMMCompositionString(int flag) : base(flag)
        {
            Clear();
        }

        public IEnumerator<byte> GetEnumerator()
        {
            foreach (byte b in _values)
                yield return b;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public override string ToString()
        {
            if (Length <= 0) return String.Empty;
            return Encoding.Unicode.GetString(_values, 0, Length);
        }

        internal void Clear()
        {
            _values = new byte[0];
            Length = 0;
        }

        internal override void Update()
        {
            Length = IMM.ImmGetCompositionString(IMEHandle, Flag, IntPtr.Zero, 0);
            IntPtr pointer = Marshal.AllocHGlobal(Length);
            try
            {
                IMM.ImmGetCompositionString(IMEHandle, Flag, pointer, Length);
                _values = new byte[Length];
                Marshal.Copy(pointer, _values, 0, Length);
            }
            finally
            {
                Marshal.FreeHGlobal(pointer);
            }
        }
    }

    internal class IMMCompositionInt : IMMCompositionResultHandler
    {
        public int Value { get; private set; }

        internal IMMCompositionInt(int flag) : base(flag) { }

        public override string ToString()
        {
            return Value.ToString();
        }

        internal override void Update()
        {
            Value = IMM.ImmGetCompositionString(IMEHandle, Flag, IntPtr.Zero, 0);
        }
    }
}
