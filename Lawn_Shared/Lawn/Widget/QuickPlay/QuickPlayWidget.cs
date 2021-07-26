using System;
using System.Collections.Generic;
using Sexy;

namespace Lawn
{
	internal class QuickPlayWidget : Widget
	{
		static QuickPlayWidget()
		{
			for (int i = 0; i < QuickPlayWidget.ZombieThumbTab.Length; i++)
			{
				QuickPlayWidget.ZombieThumbTab[i].x = (int)Constants.InvertAndScale((float)QuickPlayWidget.ZombieThumbTab[i].x);
				QuickPlayWidget.ZombieThumbTab[i].y = (int)Constants.InvertAndScale((float)QuickPlayWidget.ZombieThumbTab[i].y);
			}
		}

		public QuickPlayWidget(LawnApp theApp, QuickPlayWidgetListener theListener)
		{
			this.mApp = theApp;
			this.mListener = theListener;
			this.mWidth = 0;
			this.mHeight = AtlasResources.IMAGE_MINI_GAME_FRAME.mHeight;
		}

		public override void Draw(Graphics g)
		{
			int num = 10;
			for (int i = 0; i < this.mLevels.Count; i++)
			{
				int num2 = this.mLevels[i];
				this.DrawBackgroundThumbnailForLevel(g, num + Constants.QuickPlayWidget_Thumb_X, Constants.QuickPlayWidget_Thumb_Y, num2);
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
			for (int j = 0; j < this.mLevels.Count; j++)
			{
				int num3 = this.mLevels[j];
				int num4 = num3 * 5;
				while (QuickPlayWidget.ZombieThumbTab[num4].type != ZombieType.ZOMBIE_INVALID)
				{
					int num5 = QuickPlayWidget.ZombieThumbTab[num4].x;
					bool mirror = false;
					if (num5 < 0)
					{
						num5 = -num5;
						mirror = true;
					}
					if (QuickPlayWidget.ZombieThumbTab[num4].type == ZombieType.ZOMBIE_BUNGEE)
					{
						g.DrawImage(AtlasResources.IMAGE_BUNGEECORD, (float)(num + num5) + Constants.InvertAndScale(33f), (float)(QuickPlayWidget.ZombieThumbTab[num4].y + Constants.QuickPlayWidget_Bungee_Y));
					}
					this.DrawZombieThumbnail(g, QuickPlayWidget.ZombieThumbTab[num4].type, num + num5, QuickPlayWidget.ZombieThumbTab[num4].y, mirror);
					num4++;
				}
				num += AtlasResources.IMAGE_MINI_GAME_FRAME.mWidth + 10;
			}
			num = 10;
			for (int k = 0; k < this.mLevels.Count; k++)
			{
				g.DrawImage(AtlasResources.IMAGE_MINI_GAME_FRAME, num, 0);
				num += AtlasResources.IMAGE_MINI_GAME_FRAME.mWidth + 10;
			}
		}

		public override void MouseUp(int x, int y, int theClickCount)
		{
			if (this.mLevels.Count != 0)
			{
				int num = x / (AtlasResources.IMAGE_MINI_GAME_FRAME.mWidth + 10);
				this.mListener.QuickPlayStageSelected(this.mLevels[num]);
			}
		}

		public void AddLevel(int theLevel)
		{
			this.mLevels.Add(theLevel);
		}

		public void Clear()
		{
			this.mLevels.Clear();
		}

		public void SizeToFit()
		{
			this.mWidth = (10 + AtlasResources.IMAGE_MINI_GAME_FRAME.mWidth) * this.mLevels.Count;
			this.mHeight = AtlasResources.IMAGE_MINI_GAME_FRAME.mHeight;
		}

		public void DrawBackgroundThumbnailForLevel(Graphics g, int theX, int theY, int theLevel)
		{
			Image theImage = null;
			if (theLevel == 5)
			{
				theImage = AtlasResources.IMAGE_QUICKPLAY_BOWLING;
			}
			else if (theLevel <= 10)
			{
				theImage = AtlasResources.IMAGE_QUICKPLAY_BACKGROUND1_THUMB;
			}
			else if (theLevel == 15)
			{
				theImage = AtlasResources.IMAGE_QUICKPLAY_WACK;
			}
			else if (theLevel <= 20)
			{
				theImage = AtlasResources.IMAGE_QUICKPLAY_BACKGROUND2_THUMB;
			}
			else if (theLevel == 25)
			{
				theImage = AtlasResources.IMAGE_QUICKPLAY_LITTLE_TROUBLE;
			}
			else if (theLevel <= 30)
			{
				theImage = AtlasResources.IMAGE_QUICKPLAY_BACKGROUND3_THUMB;
			}
			else if (theLevel == 35)
			{
				theImage = AtlasResources.IMAGE_QUICKPLAY_VASES;
			}
			else if (theLevel <= 40)
			{
				theImage = AtlasResources.IMAGE_QUICKPLAY_BACKGROUND4_THUMB;
			}
			else if (theLevel <= 49)
			{
				theImage = AtlasResources.IMAGE_QUICKPLAY_BACKGROUND5_THUMB;
			}
			else if (theLevel == 50)
			{
				theImage = AtlasResources.IMAGE_QUICKPLAY_ZOMBOSS;
			}
			g.DrawImage(theImage, theX, theY);
		}

		public void DrawZombieThumbnail(Graphics g, ZombieType theZombieType, int theX, int theY)
		{
			this.DrawZombieThumbnail(g, theZombieType, theX, theY, false);
		}

		public void DrawZombieThumbnail(Graphics g, ZombieType theZombieType, int theX, int theY, bool mirror)
		{
			float mScaleX = g.mScaleX;
			if (mirror)
			{
				g.mScaleX = -g.mScaleX;
			}
			this.mApp.mReanimatorCache.DrawCachedZombie(g, (float)theX, (float)theY, theZombieType);
			g.mScaleX = mScaleX;
		}

		public LawnApp mApp;

		public QuickPlayWidgetListener mListener;

		private List<int> mLevels = new List<int>();

		private static readonly ZombieDescriptor[] ZombieThumbTab = new ZombieDescriptor[]
		{
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_NORMAL, 110, 80),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_TRAFFIC_CONE, 110, 70),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_NORMAL, 110, 80),
			new ZombieDescriptor(ZombieType.ZOMBIE_NORMAL, 135, 80),
			new ZombieDescriptor(ZombieType.ZOMBIE_NORMAL, 160, 80),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_POLEVAULTER, 30, 60),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_PAIL, 100, 65),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_NEWSPAPER, 100, 70),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_DOOR, 100, 70),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_FOOTBALL, 90, 50),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_DANCER, 90, 60),
			new ZombieDescriptor(ZombieType.ZOMBIE_BACKUP_DANCER, 140, 50),
			new ZombieDescriptor(ZombieType.ZOMBIE_BACKUP_DANCER, -89, 50),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_DUCKY_TUBE, 110, 70),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_SNORKEL, 110, 60),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_BOBSLED, 110, 60),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_DOLPHIN_RIDER, 110, 60),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_JACK_IN_THE_BOX, 110, 60),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_BALLOON, 80, 30),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_DIGGER, 110, 50),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_POGO, 120, 60),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_BUNGEE, 80, 35),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_LADDER, 50, 30),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_BUNGEE, 35, 54),
			new ZombieDescriptor(ZombieType.ZOMBIE_BUNGEE, 115, 45),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_CATAPULT, 100, 70),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_GARGANTUAR, 100, 50),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0),
			new ZombieDescriptor(ZombieType.ZOMBIE_INVALID, 0, 0)
		};
	}
}
