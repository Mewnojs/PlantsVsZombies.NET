using System;
using Sexy;

namespace Lawn
{
	internal class CheatDialog : LawnDialog, EditListener
	{
		public CheatDialog(LawnApp theApp) : base(theApp, null, 35, true, "CHEAT", "Enter New Level:", "", 2)
		{
			mApp = theApp;
			mVerticalCenterText = false;
			mLevelEditWidget = LawnCommon.CreateEditWidget(0, this, this, "Cheat", "");
			mLevelEditWidget.mMaxChars = 12;
			mLevelEditWidget.SetFont(Resources.FONT_BRIANNETOD12);
			string text = string.Empty;
			if (mApp.mGameMode != GameMode.GAMEMODE_ADVENTURE)
			{
				text = Common.StrFormat_("C{0}", mApp.mGameMode);
			}
			else if (mApp.HasFinishedAdventure())
			{
				int level = theApp.mPlayerInfo.GetLevel();
				text = Common.StrFormat_("F{0}", mApp.GetStageString(level));
			}
			else
			{
				int level2 = theApp.mPlayerInfo.GetLevel();
				text = mApp.GetStageString(level2);
			}
			mLevelEditWidget.SetText(text);
			base.CalcSize(110, 40);
		}

		public override void Dispose()
		{
			mLevelEditWidget.Dispose();
			base.Dispose();
		}

		public override int GetPreferredHeight(int theWidth)
		{
			return base.GetPreferredHeight(theWidth) + 40;
		}

		public override void Resize(int theX, int theY, int theWidth, int theHeight)
		{
			base.Resize(theX, theY, theWidth, theHeight);
			mLevelEditWidget.Resize(mContentInsets.mLeft + 12, mHeight - 155, mWidth - mContentInsets.mLeft - mContentInsets.mRight - 24, 28);
		}

		public override void AddedToManager(WidgetManager theWidgetManager)
		{
			base.AddedToManager(theWidgetManager);
			AddWidget(mLevelEditWidget);
			theWidgetManager.SetFocus(mLevelEditWidget);
		}

		public override void RemovedFromManager(WidgetManager theWidgetManager)
		{
			base.RemovedFromManager(theWidgetManager);
			RemoveWidget(mLevelEditWidget);
		}

		public override void Draw(Graphics g)
		{
			base.Draw(g);
			LawnCommon.DrawEditBox(g, mLevelEditWidget);
		}

		public virtual void EditWidgetText(int theId, string theString)
		{
			mApp.ButtonDepress(2000 + mId);
		}

		public virtual bool AllowChar(int theId, char theChar)
		{
			return char.IsDigit(theChar) || theChar == '-' || theChar == 'c' || theChar == 'C' || theChar == 'f' || theChar == 'F';
		}

		public bool ApplyCheat()
		{
			int num = -1;
			int finishedAdventure = 0;
			string text = mLevelEditWidget.mString;
			int num2 = text.ToLower().IndexOf("f");
			if (num2 >= 0)
			{
				finishedAdventure = 1;
			}
			text = text.ToLower().Replace("f", "");
			string[] array = text.Split(new char[]
			{
				'-'
			});
			if (array.Length > 1)
			{
				int num3;
				int.TryParse(array[0], out num3);
				int num4;
				int.TryParse(array[1], out num4);
				num = (num3 - 1) * 10 + num4;
			}
			else
			{
				int.TryParse(text, out num);
			}
			if (num <= 0)
			{
				mApp.DoDialog(36, true, "Enter Level", "Invalid Level. Do 'number' or 'area-subarea' or 'Cnumber' or 'Farea-subarea'.", "OK", 3);
				return false;
			}
			mApp.mGameMode = GameMode.GAMEMODE_ADVENTURE;
			mApp.mPlayerInfo.SetLevel(num);
			mApp.mPlayerInfo.mFinishedAdventure = finishedAdventure;
			mApp.WriteCurrentUserConfig();
			mApp.mBoard.PickBackground();
			return true;
		}

		public bool ShouldClear()
		{
			return false;
		}

		public bool AllowText(int theId, ref string theText)
		{
			return false;
		}

		public EditWidget mLevelEditWidget;
	}
}
