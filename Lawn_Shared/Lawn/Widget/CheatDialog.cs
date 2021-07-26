using System;
using Sexy;

namespace Lawn
{
	internal class CheatDialog : LawnDialog, EditListener
	{
		public CheatDialog(LawnApp theApp) : base(theApp, null, 35, true, "CHEAT", "Enter New Level:", "", 2)
		{
			this.mApp = theApp;
			this.mVerticalCenterText = false;
			this.mLevelEditWidget = LawnCommon.CreateEditWidget(0, this, this, "Cheat", "");
			this.mLevelEditWidget.mMaxChars = 12;
			this.mLevelEditWidget.SetFont(Resources.FONT_BRIANNETOD12);
			string text = string.Empty;
			if (this.mApp.mGameMode != GameMode.GAMEMODE_ADVENTURE)
			{
				text = Common.StrFormat_("C{0}", this.mApp.mGameMode);
			}
			else if (this.mApp.HasFinishedAdventure())
			{
				int level = theApp.mPlayerInfo.GetLevel();
				text = Common.StrFormat_("F{0}", this.mApp.GetStageString(level));
			}
			else
			{
				int level2 = theApp.mPlayerInfo.GetLevel();
				text = this.mApp.GetStageString(level2);
			}
			this.mLevelEditWidget.SetText(text);
			base.CalcSize(110, 40);
		}

		public override void Dispose()
		{
			this.mLevelEditWidget.Dispose();
			base.Dispose();
		}

		public override int GetPreferredHeight(int theWidth)
		{
			return base.GetPreferredHeight(theWidth) + 40;
		}

		public override void Resize(int theX, int theY, int theWidth, int theHeight)
		{
			base.Resize(theX, theY, theWidth, theHeight);
			this.mLevelEditWidget.Resize(this.mContentInsets.mLeft + 12, this.mHeight - 155, this.mWidth - this.mContentInsets.mLeft - this.mContentInsets.mRight - 24, 28);
		}

		public override void AddedToManager(WidgetManager theWidgetManager)
		{
			base.AddedToManager(theWidgetManager);
			this.AddWidget(this.mLevelEditWidget);
			theWidgetManager.SetFocus(this.mLevelEditWidget);
		}

		public override void RemovedFromManager(WidgetManager theWidgetManager)
		{
			base.RemovedFromManager(theWidgetManager);
			this.RemoveWidget(this.mLevelEditWidget);
		}

		public override void Draw(Graphics g)
		{
			base.Draw(g);
			LawnCommon.DrawEditBox(g, this.mLevelEditWidget);
		}

		public virtual void EditWidgetText(int theId, string theString)
		{
			this.mApp.ButtonDepress(2000 + this.mId);
		}

		public virtual bool AllowChar(int theId, char theChar)
		{
			return char.IsDigit(theChar) || theChar == '-' || theChar == 'c' || theChar == 'C' || theChar == 'f' || theChar == 'F';
		}

		public bool ApplyCheat()
		{
			int num = -1;
			int mFinishedAdventure = 0;
			string text = this.mLevelEditWidget.mString;
			int num2 = text.ToLower().IndexOf("f");
			if (num2 >= 0)
			{
				mFinishedAdventure = 1;
			}
			text = text.ToLower().Replace("f", "");
			string[] array = text.Split(new char[]
			{
				'-'
			});
			if (array.Length > 1)
			{
				int num3;
				int.TryParse(array[0], ref num3);
				int num4;
				int.TryParse(array[1], ref num4);
				num = (num3 - 1) * 10 + num4;
			}
			else
			{
				int.TryParse(text, ref num);
			}
			if (num <= 0)
			{
				this.mApp.DoDialog(36, true, "Enter Level", "Invalid Level. Do 'number' or 'area-subarea' or 'Cnumber' or 'Farea-subarea'.", "OK", 3);
				return false;
			}
			this.mApp.mGameMode = GameMode.GAMEMODE_ADVENTURE;
			this.mApp.mPlayerInfo.SetLevel(num);
			this.mApp.mPlayerInfo.mFinishedAdventure = mFinishedAdventure;
			this.mApp.WriteCurrentUserConfig();
			this.mApp.mBoard.PickBackground();
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
