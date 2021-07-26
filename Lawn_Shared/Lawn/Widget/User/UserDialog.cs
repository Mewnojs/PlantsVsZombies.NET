using System;
using System.Collections.Generic;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
	internal class UserDialog : LawnDialog, ListListener, EditListener
	{
		public UserDialog(LawnApp theApp) : base(theApp, null, 29, true, "[WHO_ARE_YOU]", "", "", 2)
		{
			this.mVerticalCenterText = false;
			this.mUserList = new ListWidget(0, Resources.FONT_BRIANNETOD16, this);
			this.mUserList.SetColors(LawnCommon.gUserListWidgetColors, 5);
			this.mUserList.mDrawOutline = true;
			this.mUserList.mJustify = 1;
			this.mUserList.mItemHeight = (int)Constants.InvertAndScale(24f);
			this.mButtonMinWidth = (int)Constants.InvertAndScale(130f);
			this.mRenameButton = GameButton.MakeButton(0, this, "[RENAME_BUTTON]");
			this.mDeleteButton = GameButton.MakeButton(1, this, "[DELETE_BUTTON]");
			this.mNumUsers = 0;
			PlayerInfo mPlayerInfo = theApp.mPlayerInfo;
			if (mPlayerInfo != null)
			{
				this.mUserList.SetSelect(this.mUserList.AddLine(mPlayerInfo.mName, false));
				this.mNumUsers++;
			}
			Dictionary<string, PlayerInfo> profileMap = theApp.mProfileMgr.GetProfileMap();
			Dictionary<string, PlayerInfo>.Enumerator enumerator = profileMap.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (mPlayerInfo != null)
				{
					KeyValuePair<string, PlayerInfo> keyValuePair = enumerator.Current;
					if (!(keyValuePair.Value.mName != mPlayerInfo.mName))
					{
						continue;
					}
				}
				ListWidget listWidget = this.mUserList;
				KeyValuePair<string, PlayerInfo> keyValuePair2 = enumerator.Current;
				listWidget.AddLine(keyValuePair2.Value.mName, false);
				this.mNumUsers++;
			}
			if (this.mNumUsers < 5)
			{
				this.mUserList.AddLine(TodStringFile.TodStringTranslate("[CREATE_NEW_USER]"), false);
			}
			this.mTallBottom = true;
			int num = Math.Max(Resources.FONT_DWARVENTODCRAFT15.StringWidth("MMMMMMMMMMMM"), Resources.FONT_DWARVENTODCRAFT15.StringWidth(TodStringFile.TodStringTranslate("[CREATE_NEW_USER]")));
			base.CalcSize((int)Constants.InvertAndScale(70f), (int)Constants.InvertAndScale(140f), AtlasResources.IMAGE_DIALOG_TOPLEFT.mWidth + num + AtlasResources.IMAGE_DIALOG_TOPRIGHT.mWidth);
		}

		public override void Dispose()
		{
			this.mUserList.Dispose();
			this.mRenameButton.Dispose();
			this.mDeleteButton.Dispose();
			base.Dispose();
		}

		public override void Resize(int theX, int theY, int theWidth, int theHeight)
		{
			base.Resize(theX, theY, theWidth, theHeight);
			int theX2 = base.GetLeft() + 30;
			int theY2 = base.GetTop() + -20;
			int num = base.GetWidth() - 60;
			int theHeight2 = 5 * this.mUserList.mItemHeight + (int)Constants.InvertAndScale(4f) * 2;
			int num2 = 0;
			this.mUserList.Resize(theX2, theY2, num - num2, theHeight2);
			this.mRenameButton.Layout(4355, this.mLawnYesButton, 0, -3);
			this.mDeleteButton.Layout(4355, this.mLawnNoButton, 0, -3);
		}

		public override int GetPreferredHeight(int theWidth)
		{
			return this.GetPreferredHeight(theWidth) + (int)Constants.InvertAndScale(190f);
		}

		public override void AddedToManager(WidgetManager theWidgetManager)
		{
			base.AddedToManager(theWidgetManager);
			this.AddWidget(this.mUserList);
			this.AddWidget(this.mDeleteButton);
			this.AddWidget(this.mRenameButton);
		}

		public override void RemovedFromManager(WidgetManager theWidgetManager)
		{
			base.RemovedFromManager(theWidgetManager);
			this.RemoveWidget(this.mUserList);
			this.RemoveWidget(this.mDeleteButton);
			this.RemoveWidget(this.mRenameButton);
		}

		public virtual void ListClicked(int theId, int theIdx, int theClickCount)
		{
			if (theIdx == this.mNumUsers)
			{
				this.mApp.DoCreateUserDialog(false);
				return;
			}
			this.mUserList.SetSelect(theIdx);
			if (theClickCount == 2)
			{
				this.mApp.FinishUserDialog(true);
			}
		}

		public override void ButtonDepress(int theId)
		{
			base.ButtonDepress(theId);
			string selName = this.GetSelName();
			if (selName.empty())
			{
				return;
			}
			switch (theId)
			{
			case 0:
				this.mApp.DoRenameUserDialog(selName);
				return;
			case 1:
				this.mApp.DoConfirmDeleteUserDialog(selName);
				return;
			default:
				return;
			}
		}

		public virtual void EditWidgetText(int theId, string theString)
		{
			this.mApp.ButtonDepress(2000 + this.mId);
		}

		public virtual bool AllowChar(int theId, char theChar)
		{
			return char.IsDigit(theChar);
		}

		public override void Draw(Graphics g)
		{
			base.Draw(g);
		}

		public void FinishDeleteUser()
		{
			int num = this.mUserList.mSelectIdx;
			this.mUserList.RemoveLine(this.mUserList.mSelectIdx);
			num--;
			if (num < 0)
			{
				num = 0;
			}
			if (this.mUserList.GetLineCount() > 1)
			{
				this.mUserList.SetSelect(num);
			}
			this.mNumUsers--;
			if (this.mNumUsers == 4)
			{
				this.mUserList.AddLine(TodStringFile.TodStringTranslate("[CREATE_NEW_USER]"), false);
			}
		}

		public void FinishRenameUser(string theNewName)
		{
			int mSelectIdx = this.mUserList.mSelectIdx;
			if (mSelectIdx < this.mNumUsers)
			{
				this.mUserList.SetLine(mSelectIdx, theNewName);
			}
		}

		public string GetSelName()
		{
			if (this.mUserList.mSelectIdx < 0 || this.mUserList.mSelectIdx >= this.mNumUsers)
			{
				return "";
			}
			return this.mUserList.GetStringAt(this.mUserList.mSelectIdx);
		}

		public void EditWidgetText(int theId, ref string theString)
		{
		}

		public bool AllowText(int theId, ref string theText)
		{
			return false;
		}

		public bool ShouldClear()
		{
			return false;
		}

		public void ListClosed(int theId)
		{
		}

		public void ListHiliteChanged(int theId, int theOldIdx, int theNewIdx)
		{
		}

		private const int MAX_USERS = 5;

		public ListWidget mUserList;

		public DialogButton mRenameButton;

		public DialogButton mDeleteButton;

		public int mNumUsers;
	}
}
