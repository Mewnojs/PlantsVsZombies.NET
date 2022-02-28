using System;
using System.IO.IsolatedStorage;
using System.Text;
using Sexy.Misc;

namespace Sexy.File
{
	public class StorageFile
	{
		public static void DeleteFile(string theFileName)
		{
			IsolatedStorageFile userStoreForApplication = IsolatedStorageFile.GetUserStoreForApplication();
			userStoreForApplication.DeleteFile(theFileName);
		}

		public static bool FileExists(string theFileName)
		{
			IsolatedStorageFile userStoreForApplication = IsolatedStorageFile.GetUserStoreForApplication();
			return userStoreForApplication.FileExists(theFileName);
		}

		public static void MakeDir(string theFolderName)
		{
			IsolatedStorageFile userStoreForApplication = IsolatedStorageFile.GetUserStoreForApplication();
			if (!userStoreForApplication.DirectoryExists(theFolderName))
			{
				userStoreForApplication.CreateDirectory(theFolderName);
			}
		}

		public static bool ReadBufferFromFile(string theFileName, SexyBuffer theBuffer)
		{
			IsolatedStorageFile userStoreForApplication = IsolatedStorageFile.GetUserStoreForApplication();
			if (!userStoreForApplication.FileExists(theFileName))
			{
				return false;
			}
			IsolatedStorageFileStream isolatedStorageFileStream = userStoreForApplication.OpenFile(theFileName, System.IO.FileMode.Open);
			if (isolatedStorageFileStream == null)
			{
				return false;
			}
			int num = (int)isolatedStorageFileStream.Length;
			byte[] array = new byte[num];
			isolatedStorageFileStream.Read(array, 0, num);
			theBuffer.Clear();
			theBuffer.SetData(array, num);
			isolatedStorageFileStream.Close();
			return true;
		}

		public static bool WriteBufferToFile(string theFileName, SexyBuffer theBuffer)
		{
			IsolatedStorageFile userStoreForApplication = IsolatedStorageFile.GetUserStoreForApplication();
			if (!userStoreForApplication.DirectoryExists("/users"))
			{
				userStoreForApplication.CreateDirectory("/users");
			}
			IsolatedStorageFileStream isolatedStorageFileStream = userStoreForApplication.OpenFile(theFileName, System.IO.FileMode.Create);
			if (isolatedStorageFileStream == null)
			{
				return false;
			}
			byte[] dataPtr = theBuffer.GetDataPtr();
			isolatedStorageFileStream.Write(dataPtr, 0, dataPtr.Length);
			isolatedStorageFileStream.Close();
			return true;
		}

		public void clear()
		{
			if (this.m_nMode != FileMode.MODE_NONE)
			{
				this.close();
			}
			this.fp = null;
		}

		public void close()
		{
			if (this.fp != null)
			{
				this.fp.Close();
				this.fp = null;
				this.m_nMode = FileMode.MODE_NONE;
			}
		}

		public bool openRead(string fName)
		{
			return this.openRead(fName, false, true);
		}

		public bool openRead(string fName, bool bSilent, bool bFromDocs)
		{
			this.clear();
			if (!this.m_userStore.FileExists(fName))
			{
				return false;
			}
			this.fp = this.m_userStore.OpenFile(fName, System.IO.FileMode.Open);
			if (this.fp == null)
			{
				return false;
			}
			this.m_nMode = FileMode.MODE_READ;
			return true;
		}

		public bool getBool()
		{
			return this.getChar() == '\u0001';
		}

		public char getChar()
		{
			byte[] array = null;
			this.read(ref array, 1U);
			return (char)array[0];
		}

		public short getShort()
		{
			byte[] array = null;
			this.read(ref array, 2U);
			if (BitConverter.IsLittleEndian)
			{
				array = this.ReverseBytes(array);
			}
			return BitConverter.ToInt16(array, 0);
		}

		public int getInt()
		{
			byte[] array = null;
			this.read(ref array, 4U);
			if (BitConverter.IsLittleEndian)
			{
				array = this.ReverseBytes(array);
			}
			return BitConverter.ToInt32(array, 0);
		}

		public ulong getLong()
		{
			byte[] array = null;
			this.read(ref array, 8U);
			if (BitConverter.IsLittleEndian)
			{
				array = this.ReverseBytes(array);
			}
			return BitConverter.ToUInt64(array, 0);
		}

		public float getFloat()
		{
			byte[] array = null;
			this.read(ref array, 4U);
			if (BitConverter.IsLittleEndian)
			{
				array = this.ReverseBytes(array);
			}
			return BitConverter.ToSingle(array, 0);
		}

		public bool getEof()
		{
			return this.fp.Position == this.fp.Length;
		}

		public int getStr(ref string s, int nBufferSize)
		{
			int num = 0;
			byte[] array = new byte[nBufferSize + 1];
			byte b;
			while ((b = (byte)this.getChar()) != 0 && !this.getEof())
			{
				if (num < nBufferSize - 1)
				{
					array[num] = b;
					num++;
				}
				else
				{
					int num2 = nBufferSize - 1;
				}
			}
			array[num] = 0;
			s = Encoding.UTF8.GetString(array, 0, num);
			return num;
		}

		public bool openWrite(string name)
		{
			return this.openWrite(name, true);
		}

		public bool openWrite(string fName, bool bFromDocs)
		{
			this.clear();
			this.fp = this.m_userStore.OpenFile(fName, System.IO.FileMode.Create);
			if (this.fp == null)
			{
				return false;
			}
			this.m_nMode = FileMode.MODE_WRITE;
			return true;
		}

		public void setBool(bool b)
		{
			if (b)
			{
				this.setChar(1);
				return;
			}
			this.setChar(0);
		}

		public void setChar(byte c)
		{
			this.write(new byte[] { c }, 1U);
		}

		public void setShort(short s)
		{
			byte[] array = BitConverter.GetBytes(s);
			if (BitConverter.IsLittleEndian)
			{
				array = this.ReverseBytes(array);
			}
			this.write(array, 2U);
		}

		public void setInt(int i)
		{
			byte[] array = BitConverter.GetBytes(i);
			if (BitConverter.IsLittleEndian)
			{
				array = this.ReverseBytes(array);
			}
			this.write(array, 4U);
		}

		public void setLong(ulong l)
		{
			byte[] array = BitConverter.GetBytes(l);
			if (BitConverter.IsLittleEndian)
			{
				array = this.ReverseBytes(array);
			}
			this.write(array, 8U);
		}

		public void setFloat(float f)
		{
			byte[] array = BitConverter.GetBytes(f);
			if (BitConverter.IsLittleEndian)
			{
				array = this.ReverseBytes(array);
			}
			this.write(array, 4U);
		}

		public void setStr(string s)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(s);
			bool isLittleEndian = BitConverter.IsLittleEndian;
			int byteCount = Encoding.UTF8.GetByteCount(s);
			byte[] array = new byte[byteCount + 1];
			Array.Copy(bytes, array, byteCount);
			this.write(array, (uint)(byteCount + 1));
		}

		public void useBool(ref bool b)
		{
			if (this.m_nMode == FileMode.MODE_READ)
			{
				b = this.getBool();
				return;
			}
			this.setBool(b);
		}

		public void useShort(ref short s)
		{
			if (this.m_nMode == FileMode.MODE_READ)
			{
				s = this.getShort();
				return;
			}
			this.setShort(s);
		}

		public void useInt(ref int i)
		{
			if (this.m_nMode == FileMode.MODE_READ)
			{
				i = this.getInt();
				return;
			}
			this.setInt(i);
		}

		public void useLong(ref ulong l)
		{
			if (this.m_nMode == FileMode.MODE_READ)
			{
				l = this.getLong();
				return;
			}
			this.setLong(l);
		}

		public void useFloat(ref float f)
		{
			if (this.m_nMode == FileMode.MODE_READ)
			{
				f = this.getFloat();
				return;
			}
			this.setFloat(f);
		}

		public void useStr(ref byte[] str, int nBufferSize)
		{
			throw new InvalidOperationException("TOTO_WP7 useStr() not implement!");
		}

		public void useBuffer(byte[] p, int nSize)
		{
			if (this.m_nMode == FileMode.MODE_READ)
			{
				this.read(ref p, (uint)nSize);
				return;
			}
			this.write(p, (uint)nSize);
		}

		public int getFileSize()
		{
			return (int)(this.fp.Seek(0L, (System.IO.SeekOrigin)(int)this.fp.Length) - 12L);
		}

		private void read(ref byte[] pData, uint nDataSize)
		{
			byte[] array = new byte[nDataSize];
			this.fp.Read(array, 0, (int)nDataSize);
			pData = array;
		}

		private void write(byte[] pData, uint nDataSize)
		{
			byte[] array = new byte[64];
			for (uint num = 0U; num < nDataSize; num += 64U)
			{
				int num2 = (int)(nDataSize - num);
				num2 = ((num2 > 64) ? 64 : num2);
				Array.Copy(pData, array, num2);
				this.fp.Write(array, 0, num2);
			}
		}

		private byte[] ReverseBytes(byte[] inArray)
		{
			int num = inArray.Length - 1;
			for (int i = 0; i < inArray.Length / 2; i++)
			{
				byte b = inArray[i];
				inArray[i] = inArray[num];
				inArray[num] = b;
				num--;
			}
			return inArray;
		}

		private FileMode m_nMode;

		private IsolatedStorageFile m_userStore = IsolatedStorageFile.GetUserStoreForApplication();

		private IsolatedStorageFileStream fp;
	}
}
