using System;
using System.Collections.Generic;

namespace Sexy.GraphicsLib
{
	public class RenderEffectDefinition
	{
		public bool LoadFromMem(uint inDataLen, byte[] inData, string inSrcFileName, string inDataFormat)
		{
			throw new NotImplementedException();
		}

		public bool LoadFromFile(string inFileName, string inSrcFileName)
		{
			throw new NotImplementedException();
		}

		public virtual void Dispose()
		{
			this.mData = null;
		}

		public List<byte> mData = new List<byte>();

		public string mSrcFileName;

		public string mDataFormat;
	}
}
