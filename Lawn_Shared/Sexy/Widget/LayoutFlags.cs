using System;

namespace Sexy
{
	public enum LayoutFlags
	{
		LAY_SameWidth = 1,
		LAY_SameHeight,
		LAY_SetLeft = 16,
		LAY_SetTop = 32,
		LAY_SetWidth = 64,
		LAY_SetHeight = 128,
		LAY_Above = 256,
		LAY_Below = 512,
		LAY_Right = 1024,
		LAY_Left = 2048,
		LAY_SameLeft = 4096,
		LAY_SameRight = 8192,
		LAY_SameTop = 16384,
		LAY_SameBottom = 32768,
		LAY_GrowToRight = 65536,
		LAY_GrowToLeft = 131072,
		LAY_GrowToTop = 262144,
		LAY_GrowToBottom = 524288,
		LAY_HCenter = 1048576,
		LAY_VCenter = 2097152,
		LAY_Max = 4194304,
		LAY_SameSize = 3,
		LAY_SameCorner = 20480,
		LAY_SetPos = 48,
		LAY_SetSize = 192
	}
}
