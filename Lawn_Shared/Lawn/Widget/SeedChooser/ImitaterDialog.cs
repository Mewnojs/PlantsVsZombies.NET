using System;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
    internal class ImitaterDialog : LawnDialog, SeedPacketsWidgetListener
    {
        public ImitaterDialog() : base(GlobalStaticVars.gLawnApp, null, 50, true, "[CHOOSE_SEED_TO_COPY]", "", "[DIALOG_BUTTON_OK]", 3)
        {
            base.CalcSize(Constants.ImitaterDialog_Size.X, Constants.ImitaterDialog_Size.Y);
            mSeedPacketsWidget = new SeedPacketsWidget(mApp, 10, true, this);
            mScrollWidget = new ScrollWidget();
            AddWidget(mScrollWidget);
            mScrollWidget.AddWidget(mSeedPacketsWidget);
            mScrollWidget.Resize(mWidth / 2 - mSeedPacketsWidget.mWidth / 2 - Constants.ImitaterDialog_ScrollWidget_Offset_X, Constants.ImitaterDialog_ScrollWidget_Y, mSeedPacketsWidget.mWidth + Constants.ImitaterDialog_ScrollWidget_ExtraWidth, Constants.ImitaterDialog_Height);
            mScrollWidget.EnableIndicators(AtlasResources.IMAGE_SCROLL_INDICATOR);
            mSeedPacketsWidget.Move(0, 0);
            mClip = false;
            mLawnYesButton.mLabel = TodStringFile.TodStringTranslate("[DIALOG_BUTTON_CANCEL]");
        }

        public override void Dispose()
        {
            RemoveAllWidgets(true, true);
            base.Dispose();
        }

        public override void Draw(Graphics g)
        {
            base.Draw(g);
            base.DeferOverlay();
        }

        public override void DrawOverlay(Graphics g)
        {
            g.SetColor(new SexyColor(16, 16, 33));
            g.SetColorizeImages(true);
            if (mSeedPacketsWidget.mY < 0)
            {
                g.DrawImage(AtlasResources.IMAGE_ALMANAC_PLANTS_TOPGRADIENT, mScrollWidget.mX, mScrollWidget.mY + (int)Constants.InvertAndScale(-2f), (int)Constants.InvertAndScale(222f), (int)Constants.InvertAndScale(12f));
            }
            if (mSeedPacketsWidget.mY + mSeedPacketsWidget.mHeight > mScrollWidget.mHeight)
            {
                g.DrawImage(AtlasResources.IMAGE_ALMANAC_PLANTS_BOTTOMGRADIENT, mScrollWidget.mX + (int)Constants.InvertAndScale(-2f), mScrollWidget.mY + Constants.ImitaterDialog_BottomGradient_Y, (int)Constants.InvertAndScale(225f), (int)Constants.InvertAndScale(12f));
            }
            g.SetColorizeImages(false);
        }

        public virtual void SeedSelected(SeedType theSeedType)
        {
            if (theSeedType != SeedType.None && !mApp.mSeedChooserScreen.SeedNotAllowedToPick(theSeedType))
            {
                ChosenSeed chosenSeed = mApp.mSeedChooserScreen.mChosenSeeds[48];
                chosenSeed.mSeedState = ChosenSeedState.SEED_IN_CHOOSER;
                chosenSeed.mImitaterType = theSeedType;
                mApp.mSeedChooserScreen.GetSeedPositionInChooser(48, ref chosenSeed.mX, ref chosenSeed.mY);
                mApp.mSeedChooserScreen.ClickedSeedInChooser(ref chosenSeed);
                mApp.mSeedChooserScreen.UpdateImitaterButton();
                mApp.KillDialog(mId);
            }
        }

        public SeedPacketsWidget mSeedPacketsWidget;

        public ScrollWidget mScrollWidget;
    }
}
