using System;
using System.IO;

namespace Sexy.AELib
{
	internal class ParseByteArray
	{
		public ParseByteArray(byte[] d)
		{
			if (d != null)
			{
				this.stream = new MemoryStream(d);
				this.reader = new BinaryReader(this.stream);
			}
		}

		public bool isEnd()
		{
			return this.stream.Position >= this.stream.Length;
		}

		public bool readBoolean(ref bool value)
		{
			value = this.reader.ReadBoolean();
			return true;
		}

		public bool readInt32(ref int value)
		{
			value = this.reader.ReadInt32();
			return true;
		}

		public bool readLong(ref long value)
		{
			value = (long)this.reader.ReadInt32();
			return true;
		}

		public bool readDouble(ref double value)
		{
			value = this.reader.ReadDouble();
			return true;
		}

		public bool readString(ref string value, int len)
		{
			char[] array = this.reader.ReadChars(len);
			string text = "";
			int num = 0;
			while (num < array.Length && array[num] != '\0')
			{
				text += array[num];
				num++;
			}
			value = text;
			return true;
		}

		private MemoryStream stream;

		private BinaryReader reader;
	}
}
