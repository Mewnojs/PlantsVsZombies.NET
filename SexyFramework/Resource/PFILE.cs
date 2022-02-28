using System;
using Sexy.Drivers;

namespace Sexy.Resource
{
	public class PFILE
	{
		public PFILE(string theFilename, string mode)
		{
			this.mFilename = theFilename;
		}

		internal void Close()
		{
			try
			{
				if (this.mFileHandler != null)
				{
					this.mFileHandler.Close();
					this.mFileHandler = null;
				}
			}
			catch (Exception)
			{
			}
		}

		public bool Open()
		{
			bool result;
			try
			{
				this.mFileHandler = Common.GetGameFileDriver().CreateFile(this.mFilename);
				this.mFileHandler.ForceLoad();
				result = !this.mFileHandler.HasError();
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		public bool Open<T>()
		{
			bool result;
			try
			{
				this.mFileHandler = Common.GetGameFileDriver().CreateFile(this.mFilename);
				this.mFileHandler.ForceLoadObject<T>();
				result = true;
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		public byte[] GetData()
		{
			if (this.mFileHandler != null)
			{
				return this.mFileHandler.GetBuffer();
			}
			return null;
		}

		public object GetObject()
		{
			if (this.mFileHandler != null)
			{
				return this.mFileHandler.GetObject();
			}
			return null;
		}

		public bool IsEndOfFile()
		{
			return true;
		}

		private string mFilename;

		private IFile mFileHandler;
	}
}
