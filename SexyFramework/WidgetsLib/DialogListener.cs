using System;

namespace Sexy.WidgetsLib
{
	public interface DialogListener
	{
		void DialogButtonPress(int theDialogId, int theButtonId);

		void DialogButtonDepress(int theDialogId, int theButtonId);
	}
}
