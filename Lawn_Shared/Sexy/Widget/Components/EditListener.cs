using System;

namespace Sexy
{
	public interface EditListener
	{
		void EditWidgetText(int theId, string theString);

		bool AllowChar(int theId, char theChar);

		bool AllowText(int theId, ref string theText);

		bool ShouldClear();
	}
}
