using System;

namespace Sexy.GraphicsLib
{
	public enum ImageFlags
	{
		ImageFlag_NONE,
		ImageFlag_MinimizeNumSubdivisions,
		ImageFlag_Use64By64Subdivisions,
		ImageFlag_UseA4R4G4B4 = 4,
		ImageFlag_UseA8R8G8B8 = 8,
		ImageFlag_RenderTarget = 16,
		ImageFlag_CubeMap = 32,
		ImageFlag_VolumeMap = 64,
		ImageFlag_NoTriRep = 128,
		ImageFlag_NoQuadRep = 128,
		ImageFlag_RTUseDefaultRenderMode = 256,
		ImageFlag_Atlas = 512,
		REFLECT_ATTR_ENUM_FLAGS
	}
}
