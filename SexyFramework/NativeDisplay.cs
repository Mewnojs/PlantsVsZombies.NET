using System;

namespace Sexy
{
	public class NativeDisplay
	{
		public NativeDisplay()
		{
			this.mRedConvTable = new ulong[256];
			this.mGreenConvTable = new ulong[256];
			this.mBlueConvTable = new ulong[256];
			this.mRGBBits = 0;
			this.mRedMask = 0UL;
			this.mGreenMask = 0UL;
			this.mBlueMask = 0UL;
			this.mRedBits = 0;
			this.mGreenBits = 0;
			this.mBlueBits = 0;
			this.mRedShift = 0;
			this.mGreenShift = 0;
			this.mBlueShift = 0;
			this.mRedAddTable = null;
			this.mGreenAddTable = null;
			this.mBlueAddTable = null;
		}

		public int mRGBBits;

		public ulong mRedMask;

		public ulong mGreenMask;

		public ulong mBlueMask;

		public int mRedBits;

		public int mGreenBits;

		public int mBlueBits;

		public int mRedShift;

		public int mGreenShift;

		public int mBlueShift;

		public int[] mRedAddTable;

		public int[] mGreenAddTable;

		public int[] mBlueAddTable;

		public ulong[] mRedConvTable;

		public ulong[] mGreenConvTable;

		public ulong[] mBlueConvTable;
	}
}
