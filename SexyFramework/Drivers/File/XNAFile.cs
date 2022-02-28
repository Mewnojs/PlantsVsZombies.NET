using System;
using Sexy.Drivers.App;

namespace Sexy.Drivers.File
{
	public class XNAFile : IFile
	{
		public XNAFile(string name, XNAFileDriver driver)
		{
			this.mFileName = name;
			this.mIsLoaded = false;
			this.mData = null;
			this.mStatus = IFile.Status.READ_PENDING;
			this.mFileDriver = driver;
			this.mDataObject = null;
		}

		public override void Dispose()
		{
			this.mData = null;
		}

		public override bool IsLoaded()
		{
			return this.mIsLoaded;
		}

		public override bool HasError()
		{
			return this.mStatus == IFile.Status.READ_ERROR;
		}

		public override void AsyncLoad()
		{
		}

		public override bool ForceLoad()
		{
			bool result;
			try
			{
				this.mData = this.mFileDriver.GetContentManager().Load<byte[]>(this.mFileName);
				this.mStatus = IFile.Status.READ_COMPLETE;
				result = true;
			}
			catch (Exception)
			{
				this.mStatus = IFile.Status.READ_ERROR;
				result = false;
			}
			return result;
		}

		public override byte[] GetBuffer()
		{
			return this.mData;
		}

		public override bool ForceLoadObject<T>()
		{
			bool result;
			try
			{
				this.mDataObject = ((WP7ContentManager)this.mFileDriver.GetContentManager()).LoadResDirectly<T>(this.mFileName);
				this.mStatus = IFile.Status.READ_COMPLETE;
				result = true;
			}
			catch (Exception)
			{
				this.mStatus = IFile.Status.READ_ERROR;
				result = false;
			}
			return result;
		}

		public override object GetObject()
		{
			return this.mDataObject;
		}

		public override uint GetSize()
		{
			return (uint)this.mData.Length;
		}

		public override void Close()
		{
		}

		public override void DirectSeek(long theSeekPoint)
		{
		}

		public override bool DirectRead(byte theBuffer, long theReadSize)
		{
			return false;
		}

		public override IFile.Status DirectReadStatus()
		{
			return this.mStatus;
		}

		public override long DirectReadBlockSize()
		{
			return 0L;
		}

		protected string mFileName;

		protected bool mIsLoaded;

		protected byte[] mData;

		protected IFile.Status mStatus;

		protected object mDataObject;

		protected XNAFileDriver mFileDriver;
	}
}
