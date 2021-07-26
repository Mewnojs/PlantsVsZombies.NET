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
			this.mApp = theApp;
			this.mLeaderboardType = aType;
			switch (aType)
			{
			case LeaderBoardType.LEADERBOARD_TYPE_IZOMBIE:
				this.mTitle = "[I_ZOMBIE_1]";
				this.mLeaderboardState = LeaderboardState.IZombie;
				this.mGameMode = LeaderboardGameMode.IZombie;
				break;
			case LeaderBoardType.LEADERBOARD_TYPE_VASEBREAKER:
				this.mTitle = "[SCARY_POTTER_1]";
				this.mLeaderboardState = LeaderboardState.Vasebreaker;
				this.mGameMode = LeaderboardGameMode.Vasebreaker;
				break;
			case LeaderBoardType.LEADERBOARD_TYPE_KILLED:
				this.mTitle = "[ZOMBIES_KILLED]";
				this.mLeaderboardState = LeaderboardState.Adventure;
				this.mGameMode = LeaderboardGameMode.Adventure;
				break;
			}
			this.mBackButton = GameButton.MakeButton(1000, this, "[STORE_BACK_TO_ZEN]");
			LeaderBoardComm.LoadResults(this.mGameMode);
			this.mScrollWidget = new ScrollWidget();
			this.mScrollWidget.Resize(320 - AtlasResources.IMAGE_PILE_LEADERBOARDSCREEN_BANNER.mWidth / 2, 100, AtlasResources.IMAGE_PILE_LEADERBOARDSCREEN_BANNER.mWidth, Constants.BOARD_HEIGHT - 200);
			this.mGradientWidget = new LeaderboardGradientWidget();
			this.mGradientWidget.Resize(320 - AtlasResources.IMAGE_PILE_LEADERBOARDSCREEN_BANNER.mWidth / 2, 100, AtlasResources.IMAGE_PILE_LEADERBOARDSCREEN_BANNER.mWidth, Constants.BOARD_HEIGHT - 200);
		}

		public override int GetPreferredHeight(int theWidth)
		{
			return Constants.BOARD_HEIGHT;
		}

		public override void Resize(int theX, int theY, int theWidth, int theHeight)
		{
			base.Resize(theX, theY, theWidth, theHeight);
			int num = 150;
			int mHeight = AtlasResources.IMAGE_BUTTON_LEFT.mHeight;
			this.mBackButton.Resize(this.mWidth / 2 - num / 2, theHeight - (int)Constants.InvertAndScale(50f), num, mHeight);
		}

		public override void AddedToManager(WidgetManager theWidgetManager)
		{
			this.AddWidget(this.mBackButton);
			this.AddWidget(this.mScrollWidget);
			base.AddedToManager(theWidgetManager);
		}

		public override void RemovedFromManager(WidgetManager theWidgetManager)
		{
			this.RemoveWidget(this.mBackButton);
			this.RemoveWidget(this.mScrollWidget);
			base.RemovedFromManager(theWidgetManager);
		}

		public override bool BackButtonPress()
		{
			int mId = this.mBackButton.mId;
			this.ButtonPress(mId);
			this.ButtonDepress(mId);
			return true;
		}

		public override void ButtonPress(int theId)
		{
			this.mApp.PlaySample(Resources.SOUND_GRAVEBUTTON);
		}

		public override void ButtonDepress(int theId)
		{
			base.ButtonDepress(theId);
			if (theId != 1000)
			{
				return;
			}
			this.mApp.KillLeaderboardDialog();
		}

		public override void Draw(Graphics g)
		{
			TRect mClipRect = g.mClipRect;
			g.SetClipRect(new TRect(-g.mTransX, -g.mTransY, Constants.BOARD_WIDTH, Constants.BOARD_HEIGHT));
			g.SetColor(new Color(0, 0, 0, 150));
			g.FillRect(new TRect(-g.mTransX, -g.mTransY, Constants.BOARD_WIDTH, Constants.BOARD_HEIGHT));
			g.mClipRect = mClipRect;
			g.SetColor(SexyColor.White);
			base.Draw(g);
			TodCommon.TodDrawString(g, this.mTitle, this.mWidth / 2, 60, Resources.FONT_DWARVENTODCRAFT15, SexyColor.White, DrawStringJustification.DS_ALIGN_CENTER);
			int num = LeaderBoardComm.LoadResults(this.mGameMode);
			if (num == -1)
			{
				TodCommon.TodDrawStringCenterBy(g, LeaderboardDialog.loadingStrings[this.loadingTimer], this.mWidth / 2, (int)Constants.InvertAndScale(150f), Resources.FONT_HOUSEOFTERROR16, SexyColor.White, DrawStringJustification.DS_ALIGN_CENTER, 1f, LeaderboardDialog.loadingStrings[0]);
				return;
			}
			if (num == -2)
			{
				g.SetColor(Color.White);
				g.SetFont(Resources.FONT_HOUSEOFTERROR16);
				g.WriteWordWrapped(Constants.LeaderboardDialog_CannotConnect_Rect, TodStringFile.TodStringTranslate("[CANNOT_CONNECT]"), 0, 0, true);
				return;
			}
			if (num > -1 && this.mListWidget == null)
			{
				this.mListWidget = new LeaderboardList(this.mApp, this.mLeaderboardType);
				this.mScrollWidget.AddWidget(this.mListWidget);
			}
		}

		public void DrawList(Graphics g)
		{
			LeaderBoardComm.GetMaxEntries(this.mLeaderboardState);
		}

		public override void Update()
		{
			base.Update();
			this.loadingStep++;
			if (this.loadingStep % 10 == 0)
			{
				this.loadingTimer = (this.loadingTimer + 1) % LeaderboardDialog.loadingStrings.Length;
			}
		}

		public override void Dispose()
		{
			this.mBackButton.Dispose();
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
