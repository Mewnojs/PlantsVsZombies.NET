using System;
using Sexy.Misc;

namespace JeffLib
{
	public class MaskedRect
	{
		public MaskedRect()
		{
			this.r = default(Rect);
			this.a = 0;
		}

		public MaskedRect(Rect _r)
		{
			this.r = new Rect(_r);
			this.a = 0;
		}

		public MaskedRect(Rect _r, int alpha)
		{
			this.r = new Rect(_r);
			this.a = alpha;
		}

		public Rect r;

		public int a;
	}
}
