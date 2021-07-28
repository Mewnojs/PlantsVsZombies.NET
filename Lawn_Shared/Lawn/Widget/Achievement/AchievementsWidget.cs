using System;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
	internal class AchievementsWidget : Widget
	{
		public AchievementsWidget(LawnApp theApp)
		{
			this.mApp = theApp;
			this.mWidth = Constants.BOARD_WIDTH;
			this.mHeight = Resources.IMAGE_SELECTORSCREEN_ACHIEVEMENTS_HOLE.mHeight * Constants.AchievementWidget_HOLE_DEPTH + Resources.IMAGE_SELECTORSCREEN_ACHIEVEMENTS_HOLE_CHINA.mHeight + Constants.AchievementWidget_Background_Offset_Y;
		}

		public override void Draw(Graphics g)
		{
			int theY = Constants.AchievementWidget_Background_Offset_Y;
			for (int i = 0; i < Constants.AchievementWidget_HOLE_DEPTH; i++)
			{
				g.DrawImage(Resources.IMAGE_SELECTORSCREEN_ACHIEVEMENTS_HOLE, 0, theY);
				theY += Resources.IMAGE_SELECTORSCREEN_ACHIEVEMENTS_HOLE.mHeight;
			}
			int num3 = 0x10;
			g.DrawImage(AtlasResources.IMAGE_PIPE, Constants.AchievementWidget_Pipe_Offset.X, (num3 * AtlasResources.IMAGE_PIPE.mHeight) + Constants.AchievementWidget_Pipe_Offset.Y, new TRect(0, 0, AtlasResources.IMAGE_PIPE.mWidth, Resources.IMAGE_SELECTORSCREEN_ACHIEVEMENTS_HOLE.mHeight - 1));
			int num4 = 0x15;
			g.DrawImage(AtlasResources.IMAGE_WORM, Constants.AchievementWidget_Worm_Offset.X, (num4 * Resources.IMAGE_SELECTORSCREEN_ACHIEVEMENTS_HOLE.mHeight) + Constants.AchievementWidget_Worm_Offset.Y, new TRect(0, 0, AtlasResources.IMAGE_WORM.mWidth - 1, AtlasResources.IMAGE_WORM.mHeight - 1));
			g.DrawImage(AtlasResources.IMAGE_ZOMBIE_WORM, Constants.AchievementWidget_ZombieWorm_Offset.X, (num4 * Resources.IMAGE_SELECTORSCREEN_ACHIEVEMENTS_HOLE.mHeight) + Constants.AchievementWidget_ZombieWorm_Offset.Y);
			int num5 = 0x35;
			g.DrawImage(AtlasResources.IMAGE_GEMS_LEFT, Constants.AchievementWidget_GemLeft_Offset.X, (num5 * Resources.IMAGE_SELECTORSCREEN_ACHIEVEMENTS_HOLE.mHeight) + Constants.AchievementWidget_GemLeft_Offset.Y);
			g.DrawImage(AtlasResources.IMAGE_GEMS_RIGHT, Constants.AchievementWidget_GemRight_Offset.X, (num5 * Resources.IMAGE_SELECTORSCREEN_ACHIEVEMENTS_HOLE.mHeight) + Constants.AchievementWidget_GemRight_Offset.Y);
			int num6 = 90;
			g.DrawImage(AtlasResources.IMAGE_FOSSIL, Constants.AchievementWidget_Fossile_Offset.X, (num6 * Resources.IMAGE_SELECTORSCREEN_ACHIEVEMENTS_HOLE.mHeight) + Constants.AchievementWidget_Fossile_Offset.Y, new TRect(0, 0, AtlasResources.IMAGE_FOSSIL.mWidth - 1, AtlasResources.IMAGE_FOSSIL.mHeight - 1));
			g.DrawImage(Resources.IMAGE_SELECTORSCREEN_ACHIEVEMENTS_HOLE_CHINA, 0, theY);
			g.DrawImage(Resources.IMAGE_SELECTORSCREEN_ACHIEVEMENTS_TOP_BACKGROUND, 0, 0);
			Image theImage = (!base.mIsDown || !BackButtonRect.Contains(new TPoint(base.mWidgetManager.mLastMouseX, base.mWidgetManager.mLastMouseY))) ? AtlasResources.IMAGE_REANIM_SELECTORSCREEN_ACHIEVEMENTS_BACK_BUTTON : AtlasResources.IMAGE_REANIM_SELECTORSCREEN_ACHIEVEMENTS_BACK_HIGHLIGHT;
			g.DrawImage(theImage, Constants.AchievementWidget_BackButton_X, Constants.AchievementWidget_BackButton_Y);
			g.SetScale(1f);

			for (int i = 0; i < 13; i++)
			{
				int destY = /*114 - 13 * 76 / 2 + 20 + */i * Constants.AchievementWidget_ROW_HEIGHT+ Constants.AchievementWidget_ROW_START;
				g.SetColorizeImages(true);
				if (Achievements.GetAchievementItem((AchievementId)i) == null) 
				{
					g.SetColor(new SexyColor(255, 255, 255, 100));
				}
				g.DrawImage(AtlasResources.GetImageInAtlasById(this.mApp.GetAchievementIcon((AchievementId)i)),
					Constants.AchievementWidget_Image_Pos.X, destY + Constants.AchievementWidget_Image_Pos.Y,
					Constants.AchievementWidget_Image_Size, Constants.AchievementWidget_Image_Size);
				g.SetColorizeImages(false);
				g.SetFont(Resources.FONT_DWARVENTODCRAFT15);
				g.SetColor(new SexyColor(224, 187, 98));
				string theString = this.mApp.GetAchievementName((AchievementId)i);
				g.DrawString(theString, Constants.AchievementWidget_Name_Pos.X, destY + Constants.AchievementWidget_Name_Pos.Y);
				g.SetFont(Resources.FONT_DWARVENTODCRAFT12);
				g.SetColor(new SexyColor(255, 255, 255));
				TRect theRect = new TRect(Constants.AchievementWidget_Description_Box.X, destY + Constants.AchievementWidget_Description_Box.Y, Constants.AchievementWidget_Description_Box.Width, Constants.AchievementWidget_Description_Box.Height);
				g.WriteWordWrapped(theRect, this.mApp.GetAchievementDescription((AchievementId)i), 0, -1, false);
			}
		}



		public override void MouseDown(int x, int y, int theClickCount)
		{
			if (AchievementsWidget.BackButtonRect.Contains(x, y))
			{
				this.mApp.PlaySample(Resources.SOUND_GRAVEBUTTON);
			}
		}

		public override void MouseUp(int x, int y, int theClickCount)
		{
			if (AchievementsWidget.BackButtonRect.Contains(x, y))
			{
				this.mApp.mGameSelector.ButtonDepress(118);
				return;
			}
			ScrollWidget scrollWidget = (ScrollWidget)this.mParent;
			scrollWidget.ScrollToMin(true);
		}

		public LawnApp mApp;

		public static TRect BackButtonRect = Constants.AchievementWidget_BackButton_Rect;
	}
}
