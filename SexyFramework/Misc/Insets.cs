using System;

namespace Sexy.Misc
{
	public class Insets
	{
		public Insets()
		{
		}

		public Insets(int theLeft, int theTop, int theRight, int theBottom)
		{
			this.mLeft = theLeft;
			this.mTop = theTop;
			this.mRight = theRight;
			this.mBottom = theBottom;
		}

		public Insets(Insets rhs)
		{
			this.mLeft = rhs.mLeft;
			this.mTop = rhs.mTop;
			this.mRight = rhs.mRight;
			this.mBottom = rhs.mBottom;
		}

		public int mLeft;

		public int mTop;

		public int mRight;

		public int mBottom;
	}
}
