using System;
using Sexy;
using Sexy.GraphicsLib;
using Sexy.Misc;
using Sexy.TodLib;
using Sexy.WidgetsLib;

namespace Lawn
{
    public/*internal*/ class AchievementsWidget : Widget
    {
        public AchievementsWidget(LawnApp theApp)
        {
            mApp = theApp;
            mWidth = Constants.BOARD_WIDTH;
            mHeight = Resources.IMAGE_SELECTORSCREEN_ACHIEVEMENTS_HOLE.mHeight * Constants.AchievementWidget_HOLE_DEPTH + Resources.IMAGE_SELECTORSCREEN_ACHIEVEMENTS_HOLE_CHINA.mHeight + Constants.AchievementWidget_Background_Offset_Y;
        }

        public override void Draw(Graphics g)
        {
            // custom version
            int theY = Constants.AchievementWidget_Background_Offset_Y;
            for (int i = 0; i < Constants.AchievementWidget_HOLE_DEPTH; i++)
            {
                g.DrawImage(Resources.IMAGE_SELECTORSCREEN_ACHIEVEMENTS_HOLE, 0, theY);
                theY += Resources.IMAGE_SELECTORSCREEN_ACHIEVEMENTS_HOLE.mHeight;
            }
            int num3 = 0x10;
            g.DrawImage(AtlasResources.IMAGE_PIPE, Constants.AchievementWidget_Pipe_Offset.X, (num3 * AtlasResources.IMAGE_PIPE.mHeight) + Constants.AchievementWidget_Pipe_Offset.Y, new Rect(0, 0, AtlasResources.IMAGE_PIPE.mWidth, Resources.IMAGE_SELECTORSCREEN_ACHIEVEMENTS_HOLE.mHeight - 1));
            int num4 = 0x15;
            g.DrawImage(AtlasResources.IMAGE_WORM, Constants.AchievementWidget_Worm_Offset.X, (num4 * Resources.IMAGE_SELECTORSCREEN_ACHIEVEMENTS_HOLE.mHeight) + Constants.AchievementWidget_Worm_Offset.Y, new Rect(0, 0, AtlasResources.IMAGE_WORM.mWidth - 1, AtlasResources.IMAGE_WORM.mHeight - 1));
            g.DrawImage(AtlasResources.IMAGE_ZOMBIE_WORM, Constants.AchievementWidget_ZombieWorm_Offset.X, (num4 * Resources.IMAGE_SELECTORSCREEN_ACHIEVEMENTS_HOLE.mHeight) + Constants.AchievementWidget_ZombieWorm_Offset.Y);
            int num5 = 0x35;
            g.DrawImage(AtlasResources.IMAGE_GEMS_LEFT, Constants.AchievementWidget_GemLeft_Offset.X, (num5 * Resources.IMAGE_SELECTORSCREEN_ACHIEVEMENTS_HOLE.mHeight) + Constants.AchievementWidget_GemLeft_Offset.Y);
            g.DrawImage(AtlasResources.IMAGE_GEMS_RIGHT, Constants.AchievementWidget_GemRight_Offset.X, (num5 * Resources.IMAGE_SELECTORSCREEN_ACHIEVEMENTS_HOLE.mHeight) + Constants.AchievementWidget_GemRight_Offset.Y);
            int num6 = 90;
            g.DrawImage(AtlasResources.IMAGE_FOSSIL, Constants.AchievementWidget_Fossile_Offset.X, (num6 * Resources.IMAGE_SELECTORSCREEN_ACHIEVEMENTS_HOLE.mHeight) + Constants.AchievementWidget_Fossile_Offset.Y, new Rect(0, 0, AtlasResources.IMAGE_FOSSIL.mWidth - 1, AtlasResources.IMAGE_FOSSIL.mHeight - 1));
            g.DrawImage(Resources.IMAGE_SELECTORSCREEN_ACHIEVEMENTS_HOLE_CHINA, 0, theY);
            g.DrawImage(Resources.IMAGE_SELECTORSCREEN_ACHIEVEMENTS_TOP_BACKGROUND, 0, 0);
            Image theImage = (!base.mIsDown || !BackButtonRect.Contains(new SexyPoint(base.mWidgetManager.mLastMouseX, base.mWidgetManager.mLastMouseY))) ? AtlasResources.IMAGE_REANIM_SELECTORSCREEN_ACHIEVEMENTS_BACK_BUTTON : AtlasResources.IMAGE_REANIM_SELECTORSCREEN_ACHIEVEMENTS_BACK_HIGHLIGHT;
            g.DrawImage(theImage, Constants.AchievementWidget_BackButton_X, Constants.AchievementWidget_BackButton_Y);
            g.SetScale(1f);

            for (int i = 0; i < 13; i++)
            {
                int destY = i * Constants.AchievementWidget_ROW_HEIGHT+ Constants.AchievementWidget_ROW_START;
                g.SetColorizeImages(true);
                if (Achievements.GetAchievementItem((AchievementId)i) == null) 
                {
                    g.SetColor(new SexyColor(255, 255, 255, 100));
                }
                g.DrawImage(AtlasResources.GetImageInAtlasById(mApp.GetAchievementIcon((AchievementId)i)),
                    Constants.AchievementWidget_Image_Pos.X, destY + Constants.AchievementWidget_Image_Pos.Y,
                    Constants.AchievementWidget_Image_Size, Constants.AchievementWidget_Image_Size);
                g.SetColorizeImages(false);
                g.SetFont(Resources.FONT_DWARVENTODCRAFT15);
                g.SetColor(new SexyColor(224, 187, 98));
                string theString = mApp.GetAchievementName((AchievementId)i);
                g.DrawString(theString, Constants.AchievementWidget_Name_Pos.X, destY + Constants.AchievementWidget_Name_Pos.Y);
                g.SetFont(Resources.FONT_DWARVENTODCRAFT12);
                g.SetColor(new SexyColor(255, 255, 255));
                Rect theRect = new Rect(Constants.AchievementWidget_Description_Box.X, destY + Constants.AchievementWidget_Description_Box.Y, Constants.AchievementWidget_Description_Box.Width, Constants.AchievementWidget_Description_Box.Height);
                g.WriteWordWrapped(theRect, mApp.GetAchievementDescription((AchievementId)i), 0, -1, false);
            }
            /*//original version
            int num = Constants.AchievementWidget_Background_Offset_Y;
            for (int i = 0; i < Constants.AchievementWidget_HOLE_DEPTH; i++)
            {
                g.DrawImage(Resources.IMAGE_SELECTORSCREEN_ACHIEVEMENTS_HOLE, 0, num);
                num += Resources.IMAGE_SELECTORSCREEN_ACHIEVEMENTS_HOLE.mHeight;
            }
            int num2 = 16;
            g.DrawImage(AtlasResources.IMAGE_PIPE, Constants.AchievementWidget_Pipe_Offset.X, num2 * AtlasResources.IMAGE_PIPE.mHeight + Constants.AchievementWidget_Pipe_Offset.Y, new Rect(0, 0, AtlasResources.IMAGE_PIPE.mWidth, Resources.IMAGE_SELECTORSCREEN_ACHIEVEMENTS_HOLE.mHeight - 1));
            int num3 = 21;
            g.DrawImage(AtlasResources.IMAGE_WORM, Constants.AchievementWidget_Worm_Offset.X, num3 * Resources.IMAGE_SELECTORSCREEN_ACHIEVEMENTS_HOLE.mHeight + Constants.AchievementWidget_Worm_Offset.Y, new Rect(0, 0, AtlasResources.IMAGE_WORM.mWidth - 1, AtlasResources.IMAGE_WORM.mHeight - 1));
            g.DrawImage(AtlasResources.IMAGE_ZOMBIE_WORM, Constants.AchievementWidget_ZombieWorm_Offset.X, num3 * Resources.IMAGE_SELECTORSCREEN_ACHIEVEMENTS_HOLE.mHeight + Constants.AchievementWidget_ZombieWorm_Offset.Y);
            int num4 = 53;
            g.DrawImage(AtlasResources.IMAGE_GEMS_LEFT, Constants.AchievementWidget_GemLeft_Offset.X, num4 * Resources.IMAGE_SELECTORSCREEN_ACHIEVEMENTS_HOLE.mHeight + Constants.AchievementWidget_GemLeft_Offset.Y);
            g.DrawImage(AtlasResources.IMAGE_GEMS_RIGHT, Constants.AchievementWidget_GemRight_Offset.X, num4 * Resources.IMAGE_SELECTORSCREEN_ACHIEVEMENTS_HOLE.mHeight + Constants.AchievementWidget_GemRight_Offset.Y);
            int num5 = 90;
            g.DrawImage(AtlasResources.IMAGE_FOSSIL, Constants.AchievementWidget_Fossile_Offset.X, num5 * Resources.IMAGE_SELECTORSCREEN_ACHIEVEMENTS_HOLE.mHeight + Constants.AchievementWidget_Fossile_Offset.Y, new Rect(0, 0, AtlasResources.IMAGE_FOSSIL.mWidth - 1, AtlasResources.IMAGE_FOSSIL.mHeight - 1));
            g.DrawImage(Resources.IMAGE_SELECTORSCREEN_ACHIEVEMENTS_HOLE_CHINA, 0, num);
            g.DrawImage(Resources.IMAGE_SELECTORSCREEN_ACHIEVEMENTS_TOP_BACKGROUND, 0, 0);
            Image theImage;
            if (this.mIsDown && AchievementsWidget.BackButtonRect.Contains(new SexyPoint(this.mWidgetManager.mLastMouseX, this.mWidgetManager.mLastMouseY)))
            {
                theImage = AtlasResources.IMAGE_REANIM_SELECTORSCREEN_ACHIEVEMENTS_BACK_HIGHLIGHT;
            }
            else
            {
                theImage = AtlasResources.IMAGE_REANIM_SELECTORSCREEN_ACHIEVEMENTS_BACK_BUTTON;
            }
            g.DrawImage(theImage, Constants.AchievementWidget_BackButton_X, Constants.AchievementWidget_BackButton_Y);
            num = Constants.AchievementWidget_ROW_START;
            g.SetColorizeImages(true);
            for (int j = 0; j < 18; j++)
            {
                AchievementItem achievementItem = Achievements.GetAchievementItem((AchievementId)j);
                if (achievementItem.IsEarned)
                {
                    g.SetColor(SexyColor.White);
                }
                else
                {
                    g.SetColor(new SexyColor(255, 255, 255, 100, false));
                }
                g.DrawImage(achievementItem.AchievementImage, Constants.AchievementWidget_Image_Pos.X, Constants.AchievementWidget_Image_Pos.Y + num, Constants.AchievementWidget_Image_Size, Constants.AchievementWidget_Image_Size);
                num += Constants.AchievementWidget_ROW_HEIGHT;
            }
            num = Constants.AchievementWidget_ROW_START;
            g.SetFont(Resources.FONT_DWARVENTODCRAFT15);
            g.SetColor(new SexyColor(21, 175, 0));
            for (int k = 0; k < g.GetFont().LayerCount; k++)
            {
                for (int l = 0; l < 18; l++)
                {
                    AchievementItem achievementItem2 = Achievements.GetAchievementItem((AchievementId)l);
                    g.DrawStringLayer(achievementItem2.Name, Constants.AchievementWidget_Name_Pos.X, Constants.AchievementWidget_Name_Pos.Y + num, k, Constants.AchievementWidget_Name_MaxWidth);
                    num += Constants.AchievementWidget_ROW_HEIGHT;
                }
            }
            num = Constants.AchievementWidget_ROW_START;
            g.SetFont(Resources.FONT_DWARVENTODCRAFT12);
            g.SetColor(new SexyColor(255, 255, 255));
            g.SetScale(0.8f);
            for (int m = 0; m < g.GetFont().LayerCount; m++)
            {
                for (int n = 0; n < 18; n++)
                {
                    Rect theRect = new Rect(Constants.AchievementWidget_Description_Box.X, num + Constants.AchievementWidget_Description_Box.Y, Constants.AchievementWidget_Description_Box.Width, Constants.AchievementWidget_Description_Box.Height);
                    AchievementItem achievementItem3 = Achievements.GetAchievementItem((AchievementId)n);
                    g.WriteWordWrappedLayer(theRect, achievementItem3.Description, 0, -1, m, true);
                    num += Constants.AchievementWidget_ROW_HEIGHT;
                }
            }
            num = Constants.AchievementWidget_GAMERSCORE_POS.Y;
            g.SetFont(Resources.FONT_DWARVENTODCRAFT12);
            g.SetColor(new SexyColor(255, 255, 255));
            g.SetScale(1f);
            for (int num6 = 0; num6 < g.GetFont().LayerCount; num6++)
            {
                for (int num7 = 0; num7 < 18; num7++)
                {
                    AchievementItem achievementItem4 = Achievements.GetAchievementItem((AchievementId)num7);
                    g.DrawStringLayer(LawnApp.ToString(achievementItem4.GamerScore), Constants.AchievementWidget_GAMERSCORE_POS.X, num, num6);
                    num += Constants.AchievementWidget_ROW_HEIGHT;
                }
            }
            g.SetScale(1f);*/
        }



        public override void MouseDown(int x, int y, int theClickCount)
        {
            if (AchievementsWidget.BackButtonRect.Contains(x, y))
            {
                mApp.PlaySample(Resources.SOUND_GRAVEBUTTON);
            }
        }

        public override void MouseUp(int x, int y, int theClickCount)
        {
            if (AchievementsWidget.BackButtonRect.Contains(x, y))
            {
                mApp.mGameSelector.ButtonDepress(118);
                return;
            }
            ScrollWidget scrollWidget = (ScrollWidget)mParent;
            scrollWidget.ScrollToMin(true);
        }

        public LawnApp mApp;

        public static Rect BackButtonRect = Constants.AchievementWidget_BackButton_Rect;
    }
}
