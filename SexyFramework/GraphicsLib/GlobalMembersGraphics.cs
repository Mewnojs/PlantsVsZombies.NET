using System;

namespace Sexy.GraphicsLib
{
	public static class GlobalMembersGraphics
	{
		public static int WriteWordWrappedHelper(Graphics g, string theString, int theX, int theY, int theWidth, int theJustification, bool drawString, int theOffset, int theLength, int theOldColor, int theMaxChars)
		{
			if (theOffset + theLength > theMaxChars)
			{
				theLength = theMaxChars - theOffset;
				if (theLength <= 0)
				{
					return -1;
				}
			}
			return g.WriteString(theString, theX, theY, theWidth, theJustification, drawString, theOffset, theLength, theOldColor);
		}

		public const int MAX_TEMP_SPANS = 8192;
	}
}
