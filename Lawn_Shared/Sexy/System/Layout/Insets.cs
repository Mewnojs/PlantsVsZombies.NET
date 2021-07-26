using System;

namespace Sexy
{
	public struct Insets
	{
		public Insets(int theLeft, int theTop, int theRight, int theBottom)
		{
			this.mLeft = theLeft;
			this.mTop = theTop;
			this.mRight = theRight;
			this.mBottom = theBottom;
		}

		public Insets(Insets theInsets)
		{
			this.mLeft = theInsets.mLeft;
			this.mTop = theInsets.mTop;
			this.mRight = theInsets.mRight;
			this.mBottom = theInsets.mBottom;
		}

		public int mLeft;

		public int mTop;

		public int mRight;

		public int mBottom;
	}
}
