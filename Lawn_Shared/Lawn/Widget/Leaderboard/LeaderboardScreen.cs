using System;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
	public/*internal*/ class LeaderboardScreen : Widget
	{
		public LeaderboardScreen(LawnApp theApp)
		{
			mApp = theApp;
			mClip = false;
			mApp.DelayLoadMainMenuResource(false);
			mApp.DelayLoadZenGardenResources(false);
			mApp.DelayLoadLeaderboardResource(true);
			mZombiePileWidget = new ZombiePileWidget(mApp);
			mPileScrollWidget = new ScrollWidget();
			mPileScrollWidget.EnableBounce(false);
			mPileScrollWidget.Resize(0, 0, mZombiePileWidget.mWidth, Constants.BOARD_HEIGHT);
			mPileScrollWidget.AddWidget(mZombiePileWidget);
			AddWidget(mPileScrollWidget);
			mPileScrollWidget.ScrollToBottom(false);
			mApp.DelayLoadBackgroundResource("DelayLoad_Leaderboard_Background");
			LeaderBoardComm.RecordResult(LeaderboardGameMode.Adventure, (int)mApp.mPlayerInfo.mZombiesKilled);
		}

		public void UnloadResources()
		{
			mApp.DelayLoadLeaderboardResource(false);
		}

		public void SetGrayed(bool aGray)
		{
			mZombiePileWidget.SetGray(aGray);
		}

		public override bool BackButtonPress()
		{
			mApp.KillLeaderboardScreen();
			mApp.DoBackToMain(false);
			return true;
		}

		public override void Draw(Graphics g)
		{
		}

		public override void DrawOverlay(Graphics g)
		{
		}

		public void SlideTo(int theX, int theY)
		{
			mSlideCounter = 75;
			mDestX = theX;
			mDestY = theY;
			mStartX = mX;
			mStartY = mY;
		}

		public override void Update()
		{
			base.Update();
			if (mSlideCounter > 0)
			{
				int theNewX = TodCommon.TodAnimateCurve(75, 0, mSlideCounter, mStartX, mDestX, TodCurves.EaseInOut);
				int theNewY = TodCommon.TodAnimateCurve(75, 0, mSlideCounter, mStartY, mDestY, TodCurves.EaseInOut);
				Move(theNewX, theNewY);
				mSlideCounter -= 3;
			}
		}

		public override void AddedToManager(WidgetManager theWidgetManager)
		{
			base.AddedToManager(theWidgetManager);
		}

		public override void RemovedFromManager(WidgetManager theWidgetManager)
		{
			base.RemovedFromManager(theWidgetManager);
		}

		public LawnApp mApp;

		public ScrollWidget mPileScrollWidget;

		public ZombiePileWidget mZombiePileWidget;

		public int mSlideCounter;

		public int mStartX;

		public int mStartY;

		public int mDestX;

		public int mDestY;

		public static int mPileStart = -80;
	}
}
