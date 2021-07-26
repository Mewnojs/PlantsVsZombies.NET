using System;
using Sexy;

namespace Lawn
{
	internal class LawnEditWidget : EditWidget
	{
		public LawnEditWidget(int theId, EditListener theEditListener, Dialog theDialog, string title, string description) : base(theId, theEditListener, title, description)
		{
			this.mDialog = theDialog;
			this.mAutoCapFirstLetter = true;
		}

		public override void KeyDown(KeyCode theKey)
		{
			base.KeyDown(theKey);
			if (theKey == KeyCode.KEYCODE_ESCAPE)
			{
				this.mDialog.KeyDown(theKey);
			}
		}

		public override void KeyChar(SexyChar theChar)
		{
			if (this.mAutoCapFirstLetter && char.IsLetter(theChar.value_type))
			{
				theChar.value_type = char.ToUpper(theChar.value_type);
				this.mAutoCapFirstLetter = false;
			}
			base.KeyChar(theChar);
		}

		public Dialog mDialog;

		public bool mAutoCapFirstLetter;
	}
}
