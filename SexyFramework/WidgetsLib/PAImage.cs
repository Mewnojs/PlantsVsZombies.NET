using System;
using System.Collections.Generic;
using Sexy.GraphicsLib;

namespace Sexy.WidgetsLib
{
	public class PAImage
	{
		public List<SharedImageRef> mImages = new List<SharedImageRef>();

		public int mOrigWidth;

		public int mOrigHeight;

		public int mCols;

		public int mRows;

		public string mImageName;

		public int mDrawMode;

		public PATransform mTransform = new PATransform();
	}
}
