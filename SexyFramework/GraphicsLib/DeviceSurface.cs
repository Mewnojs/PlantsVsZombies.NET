using System;

namespace Sexy.GraphicsLib
{
	public abstract class DeviceSurface
	{
		public virtual void Dispose()
		{
		}

		public abstract int Lock(_DEVICESURFACEDESC NamelessParameter);

		public abstract void Unlock(IntPtr NamelessParameter);

		public abstract int GetVersion();

		public abstract bool GenerateDeviceSurface(DeviceImage theImage);

		public abstract int HasSurface();

		public abstract IntPtr GetSurfacePtr();

		public abstract void AddRef();

		public abstract void Release();

		public abstract uint GetBits(DeviceImage theImage);

		public abstract void SetSurface(IntPtr theSurface);

		public abstract void GetDimensions(int theWidth, int theHeight);

		public uint mImageFlags;
	}
}
