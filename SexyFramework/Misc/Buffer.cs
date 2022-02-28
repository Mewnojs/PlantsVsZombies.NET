using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Sexy.Misc
{
	public class SexyBuffer
	{
		public SexyBuffer()
		{
			this.mData = new List<byte>();
		}

		public void SeekFront()
		{
			this.mReadBitPos = 0;
		}

		public void Clear()
		{
			this.mReadBitPos = 0;
			this.mWriteBitPos = 0;
			this.mDataBitSize = 0;
		}

		public void Resize(int bytes)
		{
			this.Clear();
			this.mDataBitSize = bytes * 8;
			this.mData.Resize(bytes);
		}

		public void FromWebString(string theString)
		{
			throw new NotImplementedException();
		}

		public string ToWebString()
		{
			throw new NotImplementedException();
		}

		public string UTF8ToWideString()
		{
			byte[] dataPtr = this.GetDataPtr();
			int i = this.GetDataLen();
			bool flag = true;
			string text = "";
			int num = 0;
			while (i > 0)
			{
				string text2 = "";
				int nextUTF8CharFromStream = SexyBuffer.GetNextUTF8CharFromStream(dataPtr, num, i, ref text2);
				if (nextUTF8CharFromStream == 0)
				{
					break;
				}
				i -= nextUTF8CharFromStream;
				num += nextUTF8CharFromStream;
				if (flag)
				{
					flag = false;
					if (text2[0] == '\ufeff')
					{
						continue;
					}
				}
				text += text2;
			}
			return text;
		}

		public void WriteByte(byte theByte)
		{
			if (this.mWriteBitPos % 8 == 0)
			{
				this.mData.Add(theByte);
			}
			else
			{
				int num = this.mWriteBitPos % 8;
				List<byte> list;
				int num2;
				(list = this.mData)[num2 = this.mWriteBitPos / 8] = (byte)(list[num2] | (byte)(theByte << num));
				this.mData.Add((byte)(theByte >> 8 - num));
			}
			this.mWriteBitPos += 8;
			if (this.mWriteBitPos > this.mDataBitSize)
			{
				this.mDataBitSize = this.mWriteBitPos;
			}
		}

		public void WriteNumBits(int theNum, int theBits)
		{
			for (int i = 0; i < theBits; i++)
			{
				if (this.mWriteBitPos % 8 == 0)
				{
					this.mData.Add(0);
				}
				if ((theNum & (1 << i)) != 0)
				{
					List<byte> list;
					int num;
					(list = this.mData)[num = this.mWriteBitPos / 8] = (byte)(list[num] | (byte)(1 << this.mWriteBitPos % 8));
				}
				this.mWriteBitPos++;
			}
			if (this.mWriteBitPos > this.mDataBitSize)
			{
				this.mDataBitSize = this.mWriteBitPos;
			}
		}

		public static int GetBitsRequired(int theNum, bool isSigned)
		{
			if (theNum < 0)
			{
				theNum = -theNum - 1;
			}
			int num = 0;
			while (theNum >= 1 << num)
			{
				num++;
			}
			if (isSigned)
			{
				num++;
			}
			return num;
		}

		public void WriteBoolean(bool theBool)
		{
			this.WriteByte((byte)(theBool ? 1 : 0));
		}

		public void WriteShort(short theShort)
		{
			this.WriteByte((byte)theShort);
			this.WriteByte((byte)(theShort >> 8));
		}

		public void WriteLong(long theLong)
		{
			this.WriteByte((byte)theLong);
			this.WriteByte((byte)(theLong >> 8));
			this.WriteByte((byte)(theLong >> 16));
			this.WriteByte((byte)(theLong >> 24));
		}

		public void WriteFloat(float theFloat)
		{
			byte[] bytes = BitConverter.GetBytes(theFloat);
			this.WriteBytes(bytes, 4);
		}

		public void WriteDouble(double theDouble)
		{
			byte[] bytes = BitConverter.GetBytes(theDouble);
			this.WriteBytes(bytes, 8);
		}

		public void WriteInt8(int theInt8)
		{
			this.WriteByte((byte)theInt8);
		}

		public void WriteInt16(short theInt16)
		{
			this.WriteByte((byte)theInt16);
			this.WriteByte((byte)(theInt16 >> 8));
		}

		public void WriteInt32(int theInt32)
		{
			this.WriteByte((byte)theInt32);
			this.WriteByte((byte)(theInt32 >> 8));
			this.WriteByte((byte)(theInt32 >> 16));
			this.WriteByte((byte)(theInt32 >> 24));
		}

		public void WriteInt64(long theInt64)
		{
			this.WriteByte((byte)theInt64);
			this.WriteByte((byte)(theInt64 >> 8));
			this.WriteByte((byte)(theInt64 >> 16));
			this.WriteByte((byte)(theInt64 >> 24));
			this.WriteByte((byte)(theInt64 >> 32));
			this.WriteByte((byte)(theInt64 >> 40));
			this.WriteByte((byte)(theInt64 >> 48));
			this.WriteByte((byte)(theInt64 >> 56));
		}

		public void WriteTransform2D(SexyTransform2D theTrans)
		{
			this.WriteFloat(theTrans.m00);
			this.WriteFloat(theTrans.m01);
			this.WriteFloat(theTrans.m02);
			this.WriteFloat(theTrans.m10);
			this.WriteFloat(theTrans.m11);
			this.WriteFloat(theTrans.m12);
			this.WriteFloat(theTrans.m20);
			this.WriteFloat(theTrans.m21);
			this.WriteFloat(theTrans.m22);
		}

		public void WriteFPoint(Vector2 thePoint)
		{
			this.WriteDouble((double)thePoint.X);
			this.WriteDouble((double)thePoint.Y);
		}

		public void WriteString(string theString)
		{
			this.WriteShort((short)theString.Length);
			for (int i = 0; i < theString.Length; i++)
			{
				this.WriteByte((byte)theString[i]);
			}
		}

		public void WriteUTF8String(string theString)
		{
			throw new NotSupportedException();
		}

		public void WriteSexyString(string theString)
		{
			throw new NotSupportedException();
		}

		public void WriteLine(string theString)
		{
			throw new NotSupportedException();
		}

		public void WriteBuffer(List<byte> theBuffer)
		{
			this.WriteLong((long)theBuffer.Count);
			for (int i = 0; i < theBuffer.Count; i++)
			{
				this.WriteByte(theBuffer[i]);
			}
		}

		public void WriteBuffer(SexyBuffer theBuffer)
		{
			this.WriteBuffer(theBuffer.mData);
		}

		public void WriteBytes(byte[] theByte, int theCount)
		{
			for (int i = 0; i < theCount; i++)
			{
				this.WriteByte(theByte[i]);
			}
		}

		public void SetData(List<byte> theBuffer)
		{
			this.mData.Clear();
			this.mData = null;
			this.mData = new List<byte>(theBuffer);
			this.mDataBitSize = this.mData.Count * 8;
		}

		public void SetData(byte[] thePtr, int theCount)
		{
			this.mData.Clear();
			for (int i = 0; i < theCount; i++)
			{
				this.WriteByte(thePtr[i]);
			}
		}

		public byte ReadByte()
		{
			if ((this.mReadBitPos + 7) / 8 >= this.mData.Count)
			{
				return 0;
			}
			if (this.mReadBitPos % 8 == 0)
			{
				byte result = this.mData[this.mReadBitPos / 8];
				this.mReadBitPos += 8;
				return result;
			}
			int num = this.mReadBitPos % 8;
			byte b = (byte)(this.mData[this.mReadBitPos / 8] >> num);
			b |= (byte)(this.mData[this.mReadBitPos / 8 + 1] << 8 - num);
			this.mReadBitPos += 8;
			return b;
		}

		public int ReadNumBits(int theBits, bool isSigned)
		{
			int count = this.mData.Count;
			int num = 0;
			bool flag = false;
			for (int i = 0; i < theBits; i++)
			{
				int num2 = this.mReadBitPos / 8;
				if (num2 >= count)
				{
					break;
				}
				if (flag = ((int)this.mData[num2] & (1 << this.mReadBitPos % 8)) != 0)
				{
					num |= 1 << i;
				}
				this.mReadBitPos++;
			}
			if (isSigned && flag)
			{
				for (int j = theBits; j < 32; j++)
				{
					num |= 1 << j;
				}
			}
			return num;
		}

		public bool ReadBoolean()
		{
			return this.ReadByte() != 0;
		}

		public short ReadShort()
		{
			byte[] array = new byte[2];
			this.ReadBytes(ref array, 2);
			return BitConverter.ToInt16(array, 0);
		}

		public int/*long*/ ReadLong()
		{
			return /*(long)*/this.ReadInt32();
		}

		public float ReadFloat()
		{
			byte[] array = new byte[4];
			this.ReadBytes(ref array, 4);
			return BitConverter.ToSingle(array, 0);
		}

		public double ReadDouble()
		{
			byte[] array = new byte[8];
			this.ReadBytes(ref array, 8);
			return BitConverter.ToDouble(array, 0);
		}

		public byte ReadInt8()
		{
			return this.ReadByte();
		}

		public short ReadInt16()
		{
			return this.ReadShort();
		}

		public int ReadInt32()
		{
			byte[] array = new byte[4];
			this.ReadBytes(ref array, 4);
			return BitConverter.ToInt32(array, 0);
		}

		public long ReadInt64()
		{
			byte[] array = new byte[8];
			this.ReadBytes(ref array, 8);
			return BitConverter.ToInt64(array, 0);
		}

		public SexyTransform2D ReadTransform2D()
		{
			return new SexyTransform2D(false)
			{
				m00 = this.ReadFloat(),
				m01 = this.ReadFloat(),
				m02 = this.ReadFloat(),
				m10 = this.ReadFloat(),
				m11 = this.ReadFloat(),
				m12 = this.ReadFloat(),
				m20 = this.ReadFloat(),
				m21 = this.ReadFloat(),
				m22 = this.ReadFloat()
			};
		}

		public FPoint ReadFPoint()
		{
			return new FPoint
			{
				mX = (float)this.ReadDouble(),
				mY = (float)this.ReadDouble()
			};
		}

		public Vector2 ReadVector2()
		{
			Vector2 result = default(Vector2);
			result.X = (float)this.ReadDouble();
			result.Y = (float)this.ReadDouble();
			return result;
		}

		public string ReadString()
		{
			string text = "";
			int num = (int)this.ReadShort();
			for (int i = 0; i < num; i++)
			{
				text += (char)this.ReadByte();
			}
			return text;
		}

		public string ReadUTF8String()
		{
			if ((this.mReadBitPos & 7) != 0)
			{
				this.mReadBitPos = (this.mReadBitPos + 8) & -8;
			}
			string text = "";
			int num = (int)this.ReadShort();
			if (num == 0)
			{
				return "";
			}
			int num2 = this.mReadBitPos / 8;
			byte[] theBuffer = this.mData.ToArray();
			int num3 = (this.mDataBitSize - this.mReadBitPos) / 8;
			int num4 = 0;
			while (num3 > 0 && num4 < num)
			{
				string text2 = "";
				int nextUTF8CharFromStream = SexyBuffer.GetNextUTF8CharFromStream(theBuffer, num2, num3, ref text2);
				if (nextUTF8CharFromStream == 0)
				{
					break;
				}
				num2 += nextUTF8CharFromStream;
				num3 -= nextUTF8CharFromStream;
				this.mReadBitPos += 8 * nextUTF8CharFromStream;
				text += text2;
				num4++;
			}
			return text;
		}

		public string ReadSexyString()
		{
			throw new NotSupportedException();
		}

		public string ReadLine()
		{
			string text = "";
			for (;;)
			{
				byte b = this.ReadByte();
				if (b == 0 || b == 10)
				{
					break;
				}
				if (b != 13)
				{
					text += (char)b;
				}
			}
			return text;
		}

		public void ReadBytes(ref byte[] theData, int theLen)
		{
			for (int i = 0; i < theLen; i++)
			{
				theData[i] = this.ReadByte();
			}
		}

		public void ReadBuffer(List<byte> theByteVector)
		{
			theByteVector.Clear();
			long num = this.ReadLong();
			if (num > 0L)
			{
				theByteVector.AddRange(this.mData);
			}
		}

		public void ReadBuffer(SexyBuffer theBuffer)
		{
			this.ReadBuffer(theBuffer.mData);
			theBuffer.mDataBitSize = theBuffer.mData.Count * 8;
		}

		public byte[] GetDataPtr()
		{
			if (this.mData.Count == 0)
			{
				return null;
			}
			return this.mData.ToArray();
		}

		public int GetDataLen()
		{
			return (this.mDataBitSize + 7) / 8;
		}

		public int GetDataLenBits()
		{
			return this.mDataBitSize;
		}

		public ulong GetCRC32(ulong theSeed)
		{
			throw new NotImplementedException();
		}

		public bool AtEnd()
		{
			return this.mReadBitPos >= this.mDataBitSize;
		}

		public bool PastEnd()
		{
			return this.mReadBitPos > this.mDataBitSize;
		}

		public int GetBitsAvailable()
		{
			if (!this.AtEnd())
			{
				return this.mDataBitSize - this.mReadBitPos;
			}
			return 0;
		}

		public int GetBytesAvailable()
		{
			return this.GetBitsAvailable() / 8;
		}

		private static int GetNextUTF8CharFromStream(byte[] theBuffer, int theLen, ref string theChar)
		{
			return SexyBuffer.GetNextUTF8CharFromStream(theBuffer, 0, theLen, ref theChar);
		}

		private static int GetNextUTF8CharFromStream(byte[] theBuffer, int start, int theLen, ref string theChar)
		{
			if (theLen == 0)
			{
				return 0;
			}
			int num = 0;
			int num2 = 0;
			int num3 = (int)theBuffer[start + num++];
			if ((num3 & 128) != 0)
			{
				if ((num3 & 192) != 192)
				{
					return 0;
				}
				int[] array = new int[6];
				int num4 = 0;
				array[num4++] = num3;
				int num5 = 0;
				while (num5 < SexyBuffer.aMaskData.Length && (num3 & (int)SexyBuffer.aMaskData[num5]) != (((int)SexyBuffer.aMaskData[num5] << 1) & (int)SexyBuffer.aMaskData[num5]))
				{
					num5++;
				}
				if (num5 >= SexyBuffer.aMaskData.Length)
				{
					return 0;
				}
				num3 &= (int)(~(int)SexyBuffer.aMaskData[num5]);
				int num6 = num5 + 1;
				if (num6 < 2 || num6 > 6)
				{
					return 0;
				}
				while (num5 > 0 && num2 < theLen)
				{
					int num7 = (int)theBuffer[start + num++];
					if ((num7 & 192) != 128)
					{
						return 0;
					}
					array[num4++] = num7;
					num3 = (num3 << 6) | (num7 & 63);
					num5--;
					num2++;
				}
				if (num5 > 0)
				{
					return 0;
				}
				bool flag = true;
				switch (num6)
				{
				case 2:
					flag = (array[0] & 62) != 0;
					break;
				case 3:
					flag = (array[0] & 31) != 0 || (array[1] & 32) != 0;
					break;
				case 4:
					flag = (array[0] & 15) != 0 || (array[1] & 48) != 0;
					break;
				case 5:
					flag = (array[0] & 7) != 0 || (array[1] & 56) != 0;
					break;
				case 6:
					flag = (array[0] & 3) != 0 || (array[1] & 60) != 0;
					break;
				}
				if (!flag)
				{
					return 0;
				}
			}
			int result = num2;
			if ((num3 >= 55296 && num3 <= 57343) || (num3 >= 65534 && num3 <= 65535))
			{
				return 0;
			}
			theChar += num3;
			return result;
		}

		public int mDataBitSize;

		public int mReadBitPos;

		public int mWriteBitPos;

		public List<byte> mData;

		private static ushort[] aMaskData = new ushort[] { 192, 224, 240, 248, 252 };
	}
}
