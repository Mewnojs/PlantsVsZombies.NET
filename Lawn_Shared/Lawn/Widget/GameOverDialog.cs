using System;
using Sexy;

namespace Lawn
{
    internal class GameOverDialog : LawnDialog
    {
        public GameOverDialog(string theMessage, bool theShowChallengeName) : base((LawnApp)GlobalStaticVars.gSexyAppBase, null, 17, true, "[GAME_OVER]", theMessage, "", 3)
        {
            mMenuButton = null;
            mLawnYesButton.SetLabel("[TRY_AGAIN]");
            mLawnYesButton.mId = 1;
            if (theMessage.length() == 0)
            {
                mContentInsets.mTop = mContentInsets.mTop + 15;
            }
            base.CalcSize(30, 20);
            Resize((Constants.BOARD_WIDTH - mWidth) / 2, (Constants.BOARD_HEIGHT - mHeight) / 2, mWidth, mHeight);
            mClip = false;
            mMenuButton = GameButton.MakeButton(1000, this, "[MAIN_MENU_BUTTON]");
            mMenuButton.Resize(Constants.UIMenuButtonPosition.X - mX + Constants.Board_Offset_AspectRatio_Correction, Constants.UIMenuButtonPosition.Y - mY, Constants.UIMenuButtonWidth, AtlasResources.IMAGE_BUTTON_LEFT.mHeight);
            GlobalStaticVars.gLawnApp.mBoard.mShowShovel = false;
            GlobalStaticVars.gLawnApp.mBoard.mMenuButton.mBtnNoDraw = true;
        }

        public override void ButtonDepress(int theId)
        {
            if (theId != 1)
            {
                if (theId == 1000)
                {
                    mApp.KillDialog(17);
                    mApp.EndLevel();
                    mApp.ShowGameSelector();
                }
                return;
            }
            mApp.KillDialog(17);
            mApp.KillBoard();
            if (mApp.IsSurvivalMode())
            {
                mApp.ShowChallengeScreen(ChallengePage.Survival);
                return;
            }
            if (mApp.IsPuzzleMode())
            {
                mApp.FinishInGameRestartConfirmDialog(true);
                return;
            }
            if (mApp.IsAdventureMode() || mApp.IsQuickPlayMode())
            {
                mApp.FinishInGameRestartConfirmDialog(true);
                return;
            }
            mApp.FinishInGameRestartConfirmDialog(true);
        }

        public override void MouseDrag(int x, int y)
        {
            base.MouseDrag(x, y);
            if (mMenuButton != null)
            {
                mMenuButton.Resize(Constants.UIMenuButtonPosition.X - mX + Constants.Board_Offset_AspectRatio_Correction, Constants.UIMenuButtonPosition.Y - mY, Constants.UIMenuButtonWidth, AtlasResources.IMAGE_BUTTON_LEFT.mHeight);
            }
        }

        public override void AddedToManager(WidgetManager theWidgetManager)
        {
            base.AddedToManager(theWidgetManager);
            if (mMenuButton != null)
            {
                AddWidget(mMenuButton);
            }
        }

        public override void RemovedFromManager(WidgetManager theWidgetManager)
        {
            base.RemovedFromManager(theWidgetManager);
            if (mMenuButton != null)
            {
                RemoveWidget(mMenuButton);
            }
        }

        public DialogButton mMenuButton;
    }
}
