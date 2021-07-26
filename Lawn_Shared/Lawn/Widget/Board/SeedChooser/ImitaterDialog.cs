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
			this.mSeedPacketsWidget = new SeedPacketsWidget(this.mApp, 10, true, this);
			this.mScrollWidget = new ScrollWidget();
			this.AddWidget(this.mScrollWidget);
			this.mScrollWidget.AddWidget(this.mSeedPacketsWidget);
			this.mScrollWidget.Resize(this.mWidth / 2 - this.mSeedPacketsWidget.mWidth / 2 - Constants.ImitaterDialog_ScrollWidget_Offset_X, Constants.ImitaterDialog_ScrollWidget_Y, this.mSeedPacketsWidget.mWidth + Constants.ImitaterDialog_ScrollWidget_ExtraWidth, Constants.ImitaterDialog_Height);
			this.mScrollWidget.EnableIndicators(AtlasResources.IMAGE_SCROLL_INDICATOR);
			this.mSeedPacketsWidget.Move(0, 0);
			this.mClip = false;
			this.mLawnYesButton.mLabel = TodStringFile.TodStringTranslate("[DIALOG_BUTTON_CANCEL]");
		}

		public override void Dispose()
		{
			this.RemoveAllWidgets(true, true);
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
			if (this.mSeedPacketsWidget.mY < 0)
			{
				g.DrawImage(AtlasResources.IMAGE_ALMANAC_PLANTS_TOPGRADIENT, this.mScrollWidget.mX, this.mScrollWidget.mY + (int)Constants.InvertAndScale(-2f), (int)Constants.InvertAndScale(222f), (int)Constants.InvertAndScale(12f));
			}
			if (this.mSeedPacketsWidget.mY + this.mSeedPacketsWidget.mHeight > this.mScrollWidget.mHeight)
			{
				g.DrawImage(AtlasResources.IMAGE_ALMANAC_PLANTS_BOTTOMGRADIENT, this.mScrollWidget.mX + (int)Constants.InvertAndScale(-2f), this.mScrollWidget.mY + Constants.ImitaterDialog_BottomGradient_Y, (int)Constants.InvertAndScale(225f), (int)Constants.InvertAndScale(12f));
			}
			g.SetColorizeImages(false);
		}

		public virtual void SeedSelected(SeedType theSeedType)
		{
			if (theSeedType != SeedType.SEED_NONE && !this.mApp.mSeedChooserScreen.SeedNotAllowedToPick(theSeedType))
			{
				ChosenSeed chosenSeed = this.mApp.mSeedChooserScreen.mChosenSeeds[48];
				chosenSeed.mSeedState = ChosenSeedState.SEED_IN_CHOOSER;
				chosenSeed.mImitaterType = theSeedType;
				this.mApp.mSeedChooserScreen.GetSeedPositionInChooser(48, ref chosenSeed.mX, ref chosenSeed.mY);
				this.mApp.mSeedChooserScreen.ClickedSeedInChooser(ref chosenSeed);
				this.mApp.mSeedChooserScreen.UpdateImitaterButton();
				this.mApp.KillDialog(this.mId);
			}
		}

		public SeedPacketsWidget mSeedPacketsWidget;

		public ScrollWidget mScrollWidget;
	}
}
