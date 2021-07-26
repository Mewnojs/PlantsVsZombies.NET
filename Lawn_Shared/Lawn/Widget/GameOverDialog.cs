using System;
using Sexy;

namespace Lawn
{
	internal class GameOverDialog : LawnDialog
	{
		public GameOverDialog(string theMessage, bool theShowChallengeName) : base((LawnApp)GlobalStaticVars.gSexyAppBase, null, 17, true, "[GAME_OVER]", theMessage, "", 3)
		{
			this.mMenuButton = null;
			this.mLawnYesButton.SetLabel("[TRY_AGAIN]");
			this.mLawnYesButton.mId = 1;
			if (theMessage.length() == 0)
			{
				this.mContentInsets.mTop = this.mContentInsets.mTop + 15;
			}
			base.CalcSize(30, 20);
			this.Resize((Constants.BOARD_WIDTH - this.mWidth) / 2, (Constants.BOARD_HEIGHT - this.mHeight) / 2, this.mWidth, this.mHeight);
			this.mClip = false;
			this.mMenuButton = GameButton.MakeButton(1000, this, "[MAIN_MENU_BUTTON]");
			this.mMenuButton.Resize(Constants.UIMenuButtonPosition.X - this.mX + Constants.Board_Offset_AspectRatio_Correction, Constants.UIMenuButtonPosition.Y - this.mY, Constants.UIMenuButtonWidth, AtlasResources.IMAGE_BUTTON_LEFT.mHeight);
			GlobalStaticVars.gLawnApp.mBoard.mShowShovel = false;
			GlobalStaticVars.gLawnApp.mBoard.mMenuButton.mBtnNoDraw = true;
		}

		public override void ButtonDepress(int theId)
		{
			if (theId != 1)
			{
				if (theId == 1000)
				{
					this.mApp.KillDialog(17);
					this.mApp.EndLevel();
					this.mApp.ShowGameSelector();
				}
				return;
			}
			this.mApp.KillDialog(17);
			this.mApp.KillBoard();
			if (this.mApp.IsSurvivalMode())
			{
				this.mApp.ShowChallengeScreen(ChallengePage.CHALLENGE_PAGE_SURVIVAL);
				return;
			}
			if (this.mApp.IsPuzzleMode())
			{
				this.mApp.FinishInGameRestartConfirmDialog(true);
				return;
			}
			if (this.mApp.IsAdventureMode() || this.mApp.IsQuickPlayMode())
			{
				this.mApp.FinishInGameRestartConfirmDialog(true);
				return;
			}
			this.mApp.FinishInGameRestartConfirmDialog(true);
		}

		public override void AddedToManager(WidgetManager theWidgetManager)
		{
			base.AddedToManager(theWidgetManager);
			if (this.mMenuButton != null)
			{
				this.AddWidget(this.mMenuButton);
			}
		}

		public override void RemovedFromManager(WidgetManager theWidgetManager)
		{
			base.RemovedFromManager(theWidgetManager);
			if (this.mMenuButton != null)
			{
				this.RemoveWidget(this.mMenuButton);
			}
		}

		public DialogButton mMenuButton;
	}
}
