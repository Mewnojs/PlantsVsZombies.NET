using System;
using Microsoft.Xna.Framework;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
	internal class LeaderboardDialog : LawnDialog
	{
		static LeaderboardDialog()
		{
			string text = TodStringFile.TodStringTranslate("[LOADING]");
			LeaderboardDialog.loadingStrings = new string[3];
			LeaderboardDialog.loadingStrings[2] = text;
			LeaderboardDialog.loadingStrings[1] = text.Substring(0, text.Length - 1);
			LeaderboardDialog.loadingStrings[0] = text.Substring(0, text.Length - 2);
		}

		public LeaderboardDialog(LawnApp theApp, LeaderBoardType aType) : base(theApp, null, 2, true, "", "", "", 0)
		{
			mApp = theApp;
			mLeaderboardType = aType;
			switch (aType)
			{
			case LeaderBoardType.LEADERBOARD_TYPE_IZOMBIE:
				mTitle = "[I_ZOMBIE_1]";
				mLeaderboardState = LeaderboardState.IZombie;
				mGameMode = LeaderboardGameMode.IZombie;
				break;
			case LeaderBoardType.LEADERBOARD_TYPE_VASEBREAKER:
				mTitle = "[SCARY_POTTER_1]";
				mLeaderboardState = LeaderboardState.Vasebreaker;
				mGameMode = LeaderboardGameMode.Vasebreaker;
				break;
			case LeaderBoardType.LEADERBOARD_TYPE_KILLED:
				mTitle = "[ZOMBIES_KILLED]";
				mLeaderboardState = LeaderboardState.Adventure;
				mGameMode = LeaderboardGameMode.Adventure;
				break;
			}
			mBackButton = GameButton.MakeButton(1000, this, "[STORE_BACK_TO_ZEN]");
			LeaderBoardComm.LoadResults(mGameMode);
			mScrollWidget = new ScrollWidget();
			mScrollWidget.Resize(320 - AtlasResources.IMAGE_PILE_LEADERBOARDSCREEN_BANNER.mWidth / 2, 100, AtlasResources.IMAGE_PILE_LEADERBOARDSCREEN_BANNER.mWidth, Constants.BOARD_HEIGHT - 200);
			mGradientWidget = new LeaderboardGradientWidget();
			mGradientWidget.Resize(320 - AtlasResources.IMAGE_PILE_LEADERBOARDSCREEN_BANNER.mWidth / 2, 100, AtlasResources.IMAGE_PILE_LEADERBOARDSCREEN_BANNER.mWidth, Constants.BOARD_HEIGHT - 200);
		}

		public override int GetPreferredHeight(int theWidth)
		{
			return Constants.BOARD_HEIGHT;
		}

		public override void Resize(int theX, int theY, int theWidth, int theHeight)
		{
			base.Resize(theX, theY, theWidth, theHeight);
			int num = 150;
			int btnHeight = AtlasResources.IMAGE_BUTTON_LEFT.mHeight;
			mBackButton.Resize(mWidth / 2 - num / 2, theHeight - (int)Constants.InvertAndScale(50f), num, btnHeight);
		}

		public override void AddedToManager(WidgetManager theWidgetManager)
		{
			AddWidget(mBackButton);
			AddWidget(mScrollWidget);
			base.AddedToManager(theWidgetManager);
		}

		public override void RemovedFromManager(WidgetManager theWidgetManager)
		{
			RemoveWidget(mBackButton);
			RemoveWidget(mScrollWidget);
			base.RemovedFromManager(theWidgetManager);
		}

		public override bool BackButtonPress()
		{
			int id = mBackButton.mId;
			ButtonPress(id);
			ButtonDepress(id);
			return true;
		}

		public override void ButtonPress(int theId)
		{
			mApp.PlaySample(Resources.SOUND_GRAVEBUTTON);
		}

		public override void ButtonDepress(int theId)
		{
			base.ButtonDepress(theId);
			if (theId != 1000)
			{
				return;
			}
			mApp.KillLeaderboardDialog();
		}

		public override void Draw(Graphics g)
		{
			TRect clipRect = g.mClipRect;
			g.SetClipRect(new TRect(-g.mTransX, -g.mTransY, Constants.BOARD_WIDTH, Constants.BOARD_HEIGHT));
			g.SetColor(new Color(0, 0, 0, 150));
			g.FillRect(new TRect(-g.mTransX, -g.mTransY, Constants.BOARD_WIDTH, Constants.BOARD_HEIGHT));
			g.mClipRect = clipRect;
			g.SetColor(SexyColor.White);
			base.Draw(g);
			TodCommon.TodDrawString(g, mTitle, mWidth / 2, 60, Resources.FONT_DWARVENTODCRAFT15, SexyColor.White, DrawStringJustification.DS_ALIGN_CENTER);
			int num = LeaderBoardComm.LoadResults(mGameMode);
			if (num == -1)
			{
				TodCommon.TodDrawStringCenterBy(g, LeaderboardDialog.loadingStrings[loadingTimer], mWidth / 2, (int)Constants.InvertAndScale(150f), Resources.FONT_HOUSEOFTERROR16, SexyColor.White, DrawStringJustification.DS_ALIGN_CENTER, 1f, LeaderboardDialog.loadingStrings[0]);
				return;
			}
			if (num == -2)
			{
				g.SetColor(Color.White);
				g.SetFont(Resources.FONT_HOUSEOFTERROR16);
				g.WriteWordWrapped(Constants.LeaderboardDialog_CannotConnect_Rect, TodStringFile.TodStringTranslate("[CANNOT_CONNECT]"), 0, 0, true);
				return;
			}
			if (num > -1 && mListWidget == null)
			{
				mListWidget = new LeaderboardList(mApp, mLeaderboardType);
				mScrollWidget.AddWidget(mListWidget);
			}
		}

		public void DrawList(Graphics g)
		{
			LeaderBoardComm.GetMaxEntries(mLeaderboardState);
		}

		public override void Update()
		{
			base.Update();
			loadingStep++;
			if (loadingStep % 10 == 0)
			{
				loadingTimer = (loadingTimer + 1) % LeaderboardDialog.loadingStrings.Length;
			}
		}

		public override void Dispose()
		{
			mBackButton.Dispose();
			base.Dispose();
		}

		public LawnStoneButton mBackButton;

		public string mTitle;

		public LeaderboardState mLeaderboardState;

		public LeaderboardGameMode mGameMode;

		public ScrollWidget mScrollWidget;

		public LeaderboardList mListWidget;

		public LeaderBoardType mLeaderboardType;

		public LeaderboardGradientWidget mGradientWidget;

		private int loadingTimer;

		private int loadingStep;

		private static string[] loadingStrings;
	}
}
