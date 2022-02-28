using System;
using System.Runtime.InteropServices;

namespace Sexy.GraphicsLib
{
	public class _DEVICEPIXELFORMAT
	{
		public uint dwSize;

		public uint dwFlags;

		public uint dwFourCC;

		[StructLayout(LayoutKind.Explicit)]
		public struct AnonymousStruct8
		{
			[FieldOffset(0)]
			public uint dwRGBBitCount;

			[FieldOffset(0)]
			public uint dwYUVBitCount;

			[FieldOffset(0)]
			public uint dwZBufferBitDepth;

			[FieldOffset(0)]
			public uint dwAlphaBitDepth;

			[FieldOffset(0)]
			public uint dwLuminanceBitCount;

			[FieldOffset(0)]
			public uint dwBumpBitCount;

			[FieldOffset(0)]
			public uint dwPrivateFormatBitCount;
		}

		[StructLayout(LayoutKind.Explicit)]
		public struct AnonymousStruct9
		{
			[FieldOffset(0)]
			public uint dwRBitMask;

			[FieldOffset(0)]
			public uint dwYBitMask;

			[FieldOffset(0)]
			public uint dwStencilBitDepth;

			[FieldOffset(0)]
			public uint dwLuminanceBitMask;

			[FieldOffset(0)]
			public uint dwBumpDuBitMask;

			[FieldOffset(0)]
			public uint dwOperations;
		}

		[StructLayout(LayoutKind.Explicit)]
		public struct AnonymousStruct10
		{
			[FieldOffset(0)]
			public uint dwGBitMask;

			[FieldOffset(0)]
			public uint dwUBitMask;

			[FieldOffset(0)]
			public uint dwZBitMask;

			[FieldOffset(0)]
			public uint dwBumpDvBitMask;

			[FieldOffset(0)]
			public _DEVICEPIXELFORMAT.AnonymousStruct10.AnonymousClass10 MultiSampleCaps;

			public struct AnonymousClass10
			{
				public ushort wFlipMSTypes;

				public ushort wBltMSTypes;
			}
		}

		[StructLayout(LayoutKind.Explicit)]
		public struct AnonymousStruct11
		{
			[FieldOffset(0)]
			public uint dwBBitMask;

			[FieldOffset(0)]
			public uint dwVBitMask;

			[FieldOffset(0)]
			public uint dwStencilBitMask;

			[FieldOffset(0)]
			public uint dwBumpLuminanceBitMask;
		}

		[StructLayout(LayoutKind.Explicit)]
		public struct AnonymousStruct12
		{
			[FieldOffset(0)]
			public uint dwRGBAlphaBitMask;

			[FieldOffset(0)]
			public uint dwYUVAlphaBitMask;

			[FieldOffset(0)]
			public uint dwLuminanceAlphaBitMask;

			[FieldOffset(0)]
			public uint dwRGBZBitMask;

			[FieldOffset(0)]
			public uint dwYUVZBitMask;
		}
	}
}
