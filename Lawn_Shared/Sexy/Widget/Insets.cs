using System;

namespace Sexy
{
	public struct Insets
	{
		public Insets(int theLeft, int theTop, int theRight, int theBottom)
		{
			mLeft = theLeft;
			mTop = theTop;
			mRight = theRight;
			mBottom = theBottom;
		}

		public Insets(Insets theInsets)
		{
			mLeft = theInsets.mLeft;
			mTop = theInsets.mTop;
			mRight = theInsets.mRight;
			mBottom = theInsets.mBottom;
		}

		public int mLeft;

		public int mTop;

		public int mRight;

		public int mBottom;
	}
}
