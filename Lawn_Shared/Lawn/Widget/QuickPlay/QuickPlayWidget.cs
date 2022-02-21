using System;
using System.Collections.Generic;
using Sexy;

namespace Lawn
{
    public/*internal*/ class QuickPlayWidget : Widget
    {
        static QuickPlayWidget()
        {
            for (int i = 0; i < QuickPlayWidget.ZombieThumbTab.Length; i++)
            {
                QuickPlayWidget.ZombieThumbTab[i].x = (int)Constants.InvertAndScale(ZombieThumbTab[i].x);
                QuickPlayWidget.ZombieThumbTab[i].y = (int)Constants.InvertAndScale(ZombieThumbTab[i].y);
            }
        }

        public QuickPlayWidget(LawnApp theApp, QuickPlayWidgetListener theListener)
        {
            mApp = theApp;
            mListener = theListener;
            mWidth = 0;
            mHeight = AtlasResources.IMAGE_MINI_GAME_FRAME.mHeight;
        }

        public override void Draw(Graphics g)
        {
            int num = 10;
            for (int i = 0; i < mLevels.Count; i++)
            {
                int num2 = mLevels[i];
                DrawBackgroundThumbnailForLevel(g, num + Constants.QuickPlayWidget_Thumb_X, Constants.QuickPlayWidget_Thumb_Y, num2);
                if (num2 == 10 || num2 == 20 || num2 == 30 || num2 == 40 || num2 == 45)
                {
                    g.SetColor(new SexyColor(0, 0, 0));
                    g.FillRect(num + (int)Constants.InvertAndScale(25f), (int)Constants.InvertAndScale(24f), (int)Constants.InvertAndScale(27f), (int)Constants.InvertAndScale(139f));
                    g.DrawImage(Resources.IMAGE_CONVEYORBELT_BACKDROP, num + (int)Constants.InvertAndScale(25f), (int)Constants.InvertAndScale(24f), (int)Constants.InvertAndScale(27f), (int)Constants.InvertAndScale(139f));
                }
                if (num2 == 40)
                {
                    g.DrawImage(AtlasResources.IMAGE_RAIN, num + (int)Constants.InvertAndScale(33f), (int)Constants.InvertAndScale(19f));
                    g.DrawImage(AtlasResources.IMAGE_RAIN, num + (int)Constants.InvertAndScale(93f), (int)Constants.InvertAndScale(30f));
                    g.DrawImage(AtlasResources.IMAGE_RAIN, num + (int)Constants.InvertAndScale(60f), (int)Constants.InvertAndScale(60f), (int)Constants.InvertAndScale(120f), (int)Constants.InvertAndScale(120f));
                }
                num += AtlasResources.IMAGE_MINI_GAME_FRAME.mWidth + 10;
            }
            num = 10;
            for (int j = 0; j < mLevels.Count; j++)
            {
                int num3 = mLevels[j];
                int num4 = num3 * 5;
                while (QuickPlayWidget.ZombieThumbTab[num4].type != ZombieType.Invalid)
                {
                    int num5 = QuickPlayWidget.ZombieThumbTab[num4].x;
                    bool mirror = false;
                    if (num5 < 0)
                    {
                        num5 = -num5;
                        mirror = true;
                    }
                    if (QuickPlayWidget.ZombieThumbTab[num4].type == ZombieType.Bungee)
                    {
                        g.DrawImage(AtlasResources.IMAGE_BUNGEECORD, num + num5 + Constants.InvertAndScale(33f), QuickPlayWidget.ZombieThumbTab[num4].y + Constants.QuickPlayWidget_Bungee_Y);
                    }
                    DrawZombieThumbnail(g, QuickPlayWidget.ZombieThumbTab[num4].type, num + num5, QuickPlayWidget.ZombieThumbTab[num4].y, mirror);
                    num4++;
                }
                num += AtlasResources.IMAGE_MINI_GAME_FRAME.mWidth + 10;
            }
            num = 10;
            for (int k = 0; k < mLevels.Count; k++)
            {
                g.DrawImage(AtlasResources.IMAGE_MINI_GAME_FRAME, num, 0);
                num += AtlasResources.IMAGE_MINI_GAME_FRAME.mWidth + 10;
            }
        }

        public override void MouseUp(int x, int y, int theClickCount)
        {
            if (mLevels.Count != 0)
            {
                int num = x / (AtlasResources.IMAGE_MINI_GAME_FRAME.mWidth + 10);
                mListener.QuickPlayStageSelected(mLevels[num]);
            }
        }

        public void AddLevel(int theLevel)
        {
            mLevels.Add(theLevel);
        }

        public void Clear()
        {
            mLevels.Clear();
        }

        public void SizeToFit()
        {
            mWidth = (10 + AtlasResources.IMAGE_MINI_GAME_FRAME.mWidth) * mLevels.Count;
            mHeight = AtlasResources.IMAGE_MINI_GAME_FRAME.mHeight;
        }

        public void DrawBackgroundThumbnailForLevel(Graphics g, int theX, int theY, int theLevel)
        {
            Image theImage = null;
            if (theLevel == 5)
            {
                theImage = AtlasResources.IMAGE_QUICKPLAY_BOWLING;
            }
            else if (theLevel <= 1 * GameConstants.LEVELS_PER_AREA)
            {
                theImage = AtlasResources.IMAGE_QUICKPLAY_BACKGROUND1_THUMB;
            }
            else if (theLevel == 1 * GameConstants.LEVELS_PER_AREA + 5)
            {
                theImage = AtlasResources.IMAGE_QUICKPLAY_WACK;
            }
            else if (theLevel <= 2 * GameConstants.LEVELS_PER_AREA)
            {
                theImage = AtlasResources.IMAGE_QUICKPLAY_BACKGROUND2_THUMB;
            }
            else if (theLevel == 2 * GameConstants.LEVELS_PER_AREA + 5)
            {
                theImage = AtlasResources.IMAGE_QUICKPLAY_LITTLE_TROUBLE;
            }
            else if (theLevel <= 3 * GameConstants.LEVELS_PER_AREA)
            {
                theImage = AtlasResources.IMAGE_QUICKPLAY_BACKGROUND3_THUMB;
            }
            else if (theLevel == 3 * GameConstants.LEVELS_PER_AREA + 5)
            {
                theImage = AtlasResources.IMAGE_QUICKPLAY_VASES;
            }
            else if (theLevel <= 4 * GameConstants.LEVELS_PER_AREA)
            {
                theImage = AtlasResources.IMAGE_QUICKPLAY_BACKGROUND4_THUMB;
            }
            else if (theLevel < GameConstants.FINAL_LEVEL)
            {
                theImage = AtlasResources.IMAGE_QUICKPLAY_BACKGROUND5_THUMB;
            }
            else if (theLevel == GameConstants.FINAL_LEVEL)
            {
                theImage = AtlasResources.IMAGE_QUICKPLAY_ZOMBOSS;
            }
            g.DrawImage(theImage, theX, theY);
        }

        public void DrawZombieThumbnail(Graphics g, ZombieType theZombieType, int theX, int theY)
        {
            DrawZombieThumbnail(g, theZombieType, theX, theY, false);
        }

        public void DrawZombieThumbnail(Graphics g, ZombieType theZombieType, int theX, int theY, bool mirror)
        {
            float scaleX = g.mScaleX;
            if (mirror)
            {
                g.mScaleX = -g.mScaleX;
            }
            mApp.mReanimatorCache.DrawCachedZombie(g, theX, theY, theZombieType);
            g.mScaleX = scaleX;
        }

        public LawnApp mApp;

        public QuickPlayWidgetListener mListener;

        private List<int> mLevels = new List<int>();

        private static readonly ZombieDescriptor[] ZombieThumbTab = new ZombieDescriptor[]
        {
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Normal, 110, 80),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.TrafficCone, 110, 70),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Normal, 110, 80),
            new ZombieDescriptor(ZombieType.Normal, 135, 80),
            new ZombieDescriptor(ZombieType.Normal, 160, 80),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Polevaulter, 30, 60),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Pail, 100, 65),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Newspaper, 100, 70),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Door, 100, 70),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Football, 90, 50),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Dancer, 90, 60),
            new ZombieDescriptor(ZombieType.BackupDancer, 140, 50),
            new ZombieDescriptor(ZombieType.BackupDancer, -89, 50),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.DuckyTube, 110, 70),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Snorkel, 110, 60),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Bobsled, 110, 60),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.DolphinRider, 110, 60),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.JackInTheBox, 110, 60),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Balloon, 80, 30),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Digger, 110, 50),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Pogo, 120, 60),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Bungee, 80, 35),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Ladder, 50, 30),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Bungee, 35, 54),
            new ZombieDescriptor(ZombieType.Bungee, 115, 45),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Catapult, 100, 70),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Gargantuar, 100, 50),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0),
            new ZombieDescriptor(ZombieType.Invalid, 0, 0)
        };
    }
}
