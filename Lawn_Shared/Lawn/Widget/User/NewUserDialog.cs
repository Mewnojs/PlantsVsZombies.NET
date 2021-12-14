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
			mIsRename = isRename;
			mApp = theApp;
			mVerticalCenterText = false;
			mNameEditWidget = LawnCommon.CreateEditWidget(0, this, this, isRename ? "[RENAME_USER]" : "[NEW_USER]", "[PLEASE_ENTER_NAME]");
			mNameEditWidget.mMaxChars = 12;
			mNameEditWidget.mAcceptsEmptyText = true;
			mNameEditWidget.SetFont(Resources.FONT_BRIANNETOD16);
			base.CalcSize((int)Constants.InvertAndScale(110f), (int)Constants.InvertAndScale(50f));
		}

		public override int GetPreferredHeight(int theWidth)
		{
			return GetPreferredHeight(theWidth) + (int)Constants.InvertAndScale(40f);
		}

		public override void Resize(int theX, int theY, int theWidth, int theHeight)
		{
			base.Resize(theX, theY, theWidth, theHeight);
			mNameEditWidget.Resize(mContentInsets.mLeft + (int)Constants.InvertAndScale(12f), (int)Constants.InvertAndScale((float)(mIsRename ? 143 : 170)), mWidth - mContentInsets.mLeft - mContentInsets.mRight - (int)Constants.InvertAndScale(24f), (int)Constants.InvertAndScale(28f));
		}

		public override void AddedToManager(WidgetManager theWidgetManager)
		{
			AddWidget(mNameEditWidget);
			theWidgetManager.SetFocus(mNameEditWidget);
			RemoveWidget(mLawnNoButton);
			RemoveWidget(mLawnYesButton);
		}

		public override void RemovedFromManager(WidgetManager theWidgetManager)
		{
			base.RemovedFromManager(theWidgetManager);
			RemoveWidget(mNameEditWidget);
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
			mApp.ButtonDepress(2000 + mId + theId);
		}

		public virtual bool AllowChar(int theId, SexyChar theChar)
		{
			return true;
		}

		public override void Dispose()
		{
			mNameEditWidget.Dispose();
			base.Dispose();
		}

		public string GetName()
		{
			if (mNameEditWidget.mString == null)
			{
				return string.Empty;
			}
			return mNameEditWidget.mString;
		}

		public void SetName(string theName)
		{
			mNameEditWidget.SetText(theName);
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
