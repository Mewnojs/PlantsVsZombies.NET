using System;
using System.Collections.Generic;
using Sexy;

namespace Lawn
{
	public/*internal*/ class MoreGamesListWidget : Widget
	{
		public MoreGamesListWidget(LawnApp theApp)
		{
			this.mApp = theApp;
			this.mWidth = Constants.MORE_GAMES_PLANK_WIDTH + 20;
			this.mNextY = 52;
		}

		public override void Dispose()
		{
		}

		public int GetPreferredHeight()
		{
			if (this.mGames.Count == 0)
			{
				return 0;
			}
			return this.mGames[this.mGames.Count - 1].mY + Constants.MORE_GAMES_PLANK_HEIGHT;
		}

		public void AddRow(Image image, TRect theSrcRect, string link)
		{
			MoreGamesListWidget.GameInfo gameInfo = new MoreGamesListWidget.GameInfo();
			gameInfo.mImage = image;
			gameInfo.mLink = link;
			gameInfo.mY = this.mNextY;
			gameInfo.mSrcRect = theSrcRect;
			this.mNextY += Constants.MORE_GAMES_PLANK_HEIGHT + Constants.MORE_GAMES_ITEM_GAP;
			this.mGames.Add(gameInfo);
		}

		public override void Draw(Graphics g)
		{
			for (int i = 0; i < this.mGames.Count; i++)
			{
				Image mImage = this.mGames[i].mImage;
				int theX = this.mWidth / 2 - this.mGames[i].mSrcRect.mWidth / 2;
				int num = this.mGames[i].mY;
				LawnCommon.DrawImageBox(g, new TRect(7, num, Constants.MORE_GAMES_PLANK_WIDTH, Constants.MORE_GAMES_PLANK_HEIGHT), AtlasResources.IMAGE_REANIM_SELECTORSCREEN_MOREGAMES_PLANK);
				num += Constants.MORE_GAMES_PLANK_HEIGHT / 2 - this.mGames[i].mSrcRect.mHeight / 2;
				g.DrawImage(mImage, theX, num, this.mGames[i].mSrcRect);
			}
		}

		public override void MouseUp(int x, int y, int theClickCount)
		{
			if (this.mApp.mGameSelector.mSlideCounter != 0)
			{
				return;
			}
			for (int i = 0; i < this.mGames.Count; i++)
			{
				int mY = this.mGames[i].mY;
				if (y >= mY && y < mY + Constants.MORE_GAMES_PLANK_HEIGHT)
				{
					return;
				}
			}
		}

		public LawnApp mApp;

		public int mNextY;

		public List<MoreGamesListWidget.GameInfo> mGames = new List<MoreGamesListWidget.GameInfo>();

		public/*internal*/ class GameInfo
		{
			public Image mImage = new Image();

			public int mY;

			public TRect mSrcRect = default(TRect);

			public string mLink;
		}
	}
}
