using System;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
	internal class ContinueDialog : LawnDialog
	{
		public ContinueDialog(LawnApp theApp) : base(theApp, null, 37, true, "[CONTINUE_GAME_HEADER]", "", "[DIALOG_BUTTON_CANCEL]", 3)
		{
			if (theApp.IsAdventureMode() || theApp.IsQuickPlayMode())
			{
				this.mDialogLines = TodStringFile.TodStringTranslate("[CONTINUE_GAME_OR_RESTART]");
				this.mContinueButton = GameButton.MakeButton(0, this, "[CONTINUE_BUTTON]");
				this.mNewGameButton = GameButton.MakeButton(1, this, "[RESTART_BUTTON]");
			}
			else
			{
				this.mDialogLines = TodStringFile.TodStringTranslate("[CONTINUE_GAME]");
				this.mContinueButton = GameButton.MakeButton(0, this, "[CONTINUE_BUTTON]");
				this.mNewGameButton = GameButton.MakeButton(1, this, "[NEW_GAME_BUTTON]");
			}
			this.mTallBottom = true;
			base.CalcSize(50, 70);
		}

		public override void Dispose()
		{
			this.mContinueButton.Dispose();
			this.mNewGameButton.Dispose();
			base.Dispose();
		}

		public override int GetPreferredHeight(int theWidth)
		{
			int preferredHeight = base.GetPreferredHeight(theWidth);
			return preferredHeight + (int)Constants.InvertAndScale(40f);
		}

		public override void Resize(int theX, int theY, int theWidth, int theHeight)
		{
			base.Resize(theX, theY, theWidth, theHeight);
			int num = AtlasResources.IMAGE_BUTTON_LEFT.mWidth + AtlasResources.IMAGE_BUTTON_MIDDLE.mWidth * 4 + AtlasResources.IMAGE_BUTTON_RIGHT.mWidth;
			int mHeight = this.mLawnYesButton.mHeight;
			int theY2 = this.mLawnYesButton.mY - mHeight - 2;
			int num2 = this.mLawnYesButton.mX - 20;
			int num3 = this.mLawnYesButton.mX + this.mLawnYesButton.mWidth - num + 24;
			while (num2 + num > num3)
			{
				num2 -= 20;
				num3 += 20;
			}
			this.mContinueButton.Resize(num2, theY2, num, mHeight);
			this.mNewGameButton.Resize(num3, this.mContinueButton.mY, num, mHeight);
			this.mLawnYesButton.Resize(theWidth / 2 - num / 2, this.mLawnYesButton.mY, num, mHeight);
		}

		public override void AddedToManager(WidgetManager theWidgetManager)
		{
			base.AddedToManager(theWidgetManager);
			this.AddWidget(this.mContinueButton);
			this.AddWidget(this.mNewGameButton);
		}

		public override void RemovedFromManager(WidgetManager theWidgetManager)
		{
			base.RemovedFromManager(theWidgetManager);
			this.RemoveWidget(this.mContinueButton);
			this.RemoveWidget(this.mNewGameButton);
		}

		public override void ButtonDepress(int theId)
		{
			if (theId == 0)
			{
				if (this.mApp.mBoard.mNextSurvivalStageCounter != 1)
				{
					string savedGameName = LawnCommon.GetSavedGameName(this.mApp.mGameMode, (int)this.mApp.mPlayerInfo.mId);
					this.mApp.EraseFile(savedGameName);
				}
				this.mApp.RestartLoopingSounds();
				this.mApp.KillDialog(this.mId);
				this.mApp.mMusic.StartGameMusic();
				return;
			}
			if (theId == 1)
			{
				if (this.mApp.IsAdventureMode() || this.mApp.IsQuickPlayMode())
				{
					LawnDialog lawnDialog = this.mApp.DoDialog(39, true, "[RESTART_LEVEL_HEADER]", "[RESTART_LEVEL]", "", 2);
					lawnDialog.mMinWidth = (int)Constants.InvertAndScale(350f);
					lawnDialog.mLawnYesButton.mLabel = TodStringFile.TodStringTranslate("[RESTART_BUTTON]");
					lawnDialog.CalcSize(0, 0);
					return;
				}
				LawnDialog lawnDialog2 = this.mApp.DoDialog(39, true, "[NEW_GAME_HEADER]", "[NEW_GAME]", "", 2);
				lawnDialog2.mMinWidth = (int)Constants.InvertAndScale(250f);
				lawnDialog2.mLawnYesButton.mLabel = TodStringFile.TodStringTranslate("[NEW_GAME_BUTTON]");
				lawnDialog2.CalcSize(0, 0);
				return;
			}
			else
			{
				this.mApp.KillDialog(this.mId);
				if (this.mApp.IsAdventureMode() || this.mApp.IsQuickPlayMode())
				{
					this.mApp.ShowGameSelector();
					return;
				}
				if (this.mApp.IsSurvivalMode())
				{
					this.mApp.KillBoard();
					this.mApp.ShowGameSelectorQuickPlay(false);
					return;
				}
				if (this.mApp.IsPuzzleMode())
				{
					this.mApp.KillBoard();
					this.mApp.ShowGameSelectorQuickPlay(false);
					return;
				}
				this.mApp.KillBoard();
				this.mApp.ShowGameSelectorQuickPlay(false);
				return;
			}
		}

		public DialogButton mContinueButton;

		public DialogButton mNewGameButton;
	}
}
