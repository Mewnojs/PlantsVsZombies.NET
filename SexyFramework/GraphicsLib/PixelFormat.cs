using System;

namespace Sexy.GraphicsLib
{
	public enum PixelFormat
	{
		PixelFormat_Unknown,
		PixelFormat_A8R8G8B8,
		PixelFormat_A4R4G4B4,
		PixelFormat_R5G6B5 = 4,
		PixelFormat_Palette8 = 8,
		PixelFormat_X8R8G8B8 = 16,
		PixelFormat_DXT5 = 32
	}
}
