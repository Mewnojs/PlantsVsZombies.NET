using System;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
	internal class LeaderboardScreen : Widget
	{
		public LeaderboardScreen(LawnApp theApp)
		{
			this.mApp = theApp;
			this.mClip = false;
			this.mApp.DelayLoadMainMenuResource(false);
			this.mApp.DelayLoadZenGardenResources(false);
			this.mApp.DelayLoadLeaderboardResource(true);
			this.mZombiePileWidget = new ZombiePileWidget(this.mApp);
			this.mPileScrollWidget = new ScrollWidget();
			this.mPileScrollWidget.EnableBounce(false);
			this.mPileScrollWidget.Resize(0, 0, this.mZombiePileWidget.mWidth, Constants.BOARD_HEIGHT);
			this.mPileScrollWidget.AddWidget(this.mZombiePileWidget);
			this.AddWidget(this.mPileScrollWidget);
			this.mPileScrollWidget.ScrollToBottom(false);
			this.mApp.DelayLoadBackgroundResource("DelayLoad_Leaderboard_Background");
			LeaderBoardComm.RecordResult(LeaderboardGameMode.Adventure, (int)this.mApp.mPlayerInfo.mZombiesKilled);
		}

		public void UnloadResources()
		{
			this.mApp.DelayLoadLeaderboardResource(false);
		}

		public void SetGrayed(bool aGray)
		{
			this.mZombiePileWidget.SetGray(aGray);
		}

		public override bool BackButtonPress()
		{
			this.mApp.KillLeaderboardScreen();
			this.mApp.DoBackToMain(false);
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
			this.mSlideCounter = 75;
			this.mDestX = theX;
			this.mDestY = theY;
			this.mStartX = this.mX;
			this.mStartY = this.mY;
		}

		public override void Update()
		{
			base.Update();
			if (this.mSlideCounter > 0)
			{
				int theNewX = TodCommon.TodAnimateCurve(75, 0, this.mSlideCounter, this.mStartX, this.mDestX, TodCurves.CURVE_EASE_IN_OUT);
				int theNewY = TodCommon.TodAnimateCurve(75, 0, this.mSlideCounter, this.mStartY, this.mDestY, TodCurves.CURVE_EASE_IN_OUT);
				this.Move(theNewX, theNewY);
				this.mSlideCounter -= 3;
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
