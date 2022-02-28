using System;

namespace Sexy.Drivers
{
	public abstract class IFile
	{
		public virtual void Dispose()
		{
		}

		public abstract bool IsLoaded();

		public abstract bool HasError();

		public abstract void AsyncLoad();

		public abstract bool ForceLoad();

		public abstract bool ForceLoadObject<T>();

		public abstract byte[] GetBuffer();

		public abstract object GetObject();

		public abstract uint GetSize();

		public abstract void Close();

		public abstract void DirectSeek(long theSeekPoint);

		public abstract bool DirectRead(byte theBuffer, long theReadSize);

		public abstract IFile.Status DirectReadStatus();

		public abstract long DirectReadBlockSize();

		public enum Status
		{
			READ_COMPLETE,
			READ_PENDING,
			READ_ERROR
		}
	}
}
