using System;
using System.Collections.Generic;
using Sexy;

namespace Lawn
{
    public/*internal*/ class MoreGamesListWidget : Widget
    {
        public MoreGamesListWidget(LawnApp theApp)
        {
            mApp = theApp;
            mWidth = Constants.MORE_GAMES_PLANK_WIDTH + 20;
            mNextY = 52;
        }

        public override void Dispose()
        {
        }

        public int GetPreferredHeight()
        {
            if (mGames.Count == 0)
            {
                return 0;
            }
            return mGames[mGames.Count - 1].mY + Constants.MORE_GAMES_PLANK_HEIGHT;
        }

        public void AddRow(Image image, TRect theSrcRect, string link)
        {
            MoreGamesListWidget.GameInfo gameInfo = new MoreGamesListWidget.GameInfo();
            gameInfo.mImage = image;
            gameInfo.mLink = link;
            gameInfo.mY = mNextY;
            gameInfo.mSrcRect = theSrcRect;
            mNextY += Constants.MORE_GAMES_PLANK_HEIGHT + Constants.MORE_GAMES_ITEM_GAP;
            mGames.Add(gameInfo);
        }

        public override void Draw(Graphics g)
        {
            for (int i = 0; i < mGames.Count; i++)
            {
                Image itemImage = mGames[i].mImage;
                int theX = mWidth / 2 - mGames[i].mSrcRect.mWidth / 2;
                int num = mGames[i].mY;
                LawnCommon.DrawImageBox(g, new TRect(7, num, Constants.MORE_GAMES_PLANK_WIDTH, Constants.MORE_GAMES_PLANK_HEIGHT), AtlasResources.IMAGE_REANIM_SELECTORSCREEN_MOREGAMES_PLANK);
                num += Constants.MORE_GAMES_PLANK_HEIGHT / 2 - mGames[i].mSrcRect.mHeight / 2;
                g.DrawImage(itemImage, theX, num, mGames[i].mSrcRect);
            }
        }

        public override void MouseUp(int theX, int theY, int theClickCount)
        {
            if (mApp.mGameSelector.mSlideCounter != 0)
            {
                return;
            }
            for (int i = 0; i < mGames.Count; i++)
            {
                int y = mGames[i].mY;
                if (theY >= y && theY < y + Constants.MORE_GAMES_PLANK_HEIGHT)
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
