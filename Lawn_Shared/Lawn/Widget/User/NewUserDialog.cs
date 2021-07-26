using System;
using Sexy;

namespace Lawn
{
	internal class NewUserDialog : LawnDialog, EditListener
	{
		public NewUserDialog(LawnApp theApp, bool allowCancel) : this(theApp, false, allowCancel)
		{
		}

		public NewUserDialog(LawnApp theApp, bool isRename, bool allowCancel) : base(theApp, null, isRename ? 32 : 30, true, isRename ? "[RENAME_USER]" : "[NEW_USER]", "[PLEASE_ENTER_NAME]", "[DIALOG_BUTTON_OK]", allowCancel ? 2 : 3)
		{
			this.mIsRename = isRename;
			this.mApp = theApp;
			this.mVerticalCenterText = false;
			this.mNameEditWidget = LawnCommon.CreateEditWidget(0, this, this, isRename ? "[RENAME_USER]" : "[NEW_USER]", "[PLEASE_ENTER_NAME]");
			this.mNameEditWidget.mMaxChars = 12;
			this.mNameEditWidget.mAcceptsEmptyText = true;
			this.mNameEditWidget.SetFont(Resources.FONT_BRIANNETOD16);
			base.CalcSize((int)Constants.InvertAndScale(110f), (int)Constants.InvertAndScale(50f));
		}

		public override int GetPreferredHeight(int theWidth)
		{
			return this.GetPreferredHeight(theWidth) + (int)Constants.InvertAndScale(40f);
		}

		public override void Resize(int theX, int theY, int theWidth, int theHeight)
		{
			base.Resize(theX, theY, theWidth, theHeight);
			this.mNameEditWidget.Resize(this.mContentInsets.mLeft + (int)Constants.InvertAndScale(12f), (int)Constants.InvertAndScale((float)(this.mIsRename ? 143 : 170)), this.mWidth - this.mContentInsets.mLeft - this.mContentInsets.mRight - (int)Constants.InvertAndScale(24f), (int)Constants.InvertAndScale(28f));
		}

		public override void AddedToManager(WidgetManager theWidgetManager)
		{
			this.AddWidget(this.mNameEditWidget);
			theWidgetManager.SetFocus(this.mNameEditWidget);
			this.RemoveWidget(this.mLawnNoButton);
			this.RemoveWidget(this.mLawnYesButton);
		}

		public override void RemovedFromManager(WidgetManager theWidgetManager)
		{
			base.RemovedFromManager(theWidgetManager);
			this.RemoveWidget(this.mNameEditWidget);
		}

		public override void Draw(Graphics g)
		{
		}

		public override void Update()
		{
			base.Update();
		}

		public virtual void EditWidgetText(int theId, string theString)
		{
			this.mApp.ButtonDepress(2000 + this.mId + theId);
		}

		public virtual bool AllowChar(int theId, SexyChar theChar)
		{
			return true;
		}

		public override void Dispose()
		{
			this.mNameEditWidget.Dispose();
			base.Dispose();
		}

		public string GetName()
		{
			if (this.mNameEditWidget.mString == null)
			{
				return string.Empty;
			}
			return this.mNameEditWidget.mString;
		}

		public void SetName(string theName)
		{
			this.mNameEditWidget.SetText(theName);
		}

		public bool AllowChar(int theId, char theChar)
		{
			return false;
		}

		public bool AllowText(int theId, ref string theText)
		{
			return false;
		}

		public bool ShouldClear()
		{
			return false;
		}

		public EditWidget mNameEditWidget;

		private bool mIsRename;
	}
}
