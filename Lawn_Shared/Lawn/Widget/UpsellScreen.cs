using System;
using Sexy;

namespace Lawn
{
	public/*internal*/ class UpsellScreen : Dialog
	{
		public UpsellScreen(LawnApp theApp) : base(null, null, 54, true, "", "", "", 0)
		{
			this.mApp = theApp;
			this.mClip = false;
			this.mApp.DelayLoadUpsellResource("");
			this.Resize(0, 0, Constants.BOARD_WIDTH, Constants.BOARD_HEIGHT);
			this.mBackButton = new NewLawnButton(null, 1001, this);
			this.mBackButton.mDoFinger = true;
			this.mBackButton.mLabel = "";
			this.mBackButton.mButtonImage = AtlasResources.IMAGE_STORE_MAINMENUBUTTON;
			this.mBackButton.mDownImage = AtlasResources.IMAGE_STORE_MAINMENUBUTTONDOWN;
			this.mBackButton.mColors[0] = new SexyColor(98, 153, 235);
			this.mBackButton.mColors[1] = new SexyColor(167, 192, 235);
			this.mBackButton.Resize(Constants.StoreScreen_BackButton_X, Constants.StoreScreen_BackButton_Y, AtlasResources.IMAGE_STORE_MAINMENUBUTTON.mWidth, AtlasResources.IMAGE_STORE_MAINMENUBUTTON.mHeight);
			this.mBackButton.mTextOffsetX = -7;
			this.mBackButton.mTextOffsetY = 1;
			this.mBackButton.mTextDownOffsetX = 2;
			this.mBackButton.mTextDownOffsetY = 1;
			this.mBuyButton = new NewLawnButton(null, 1000, this);
			this.mBuyButton.mDoFinger = true;
			this.mBuyButton.mLabel = "";
			this.mBuyButton.mButtonImage = AtlasResources.IMAGE_STORE_PREVBUTTON;
			this.mBuyButton.mOverImage = AtlasResources.IMAGE_STORE_PREVBUTTONHIGHLIGHT;
			this.mBuyButton.mDownImage = AtlasResources.IMAGE_STORE_PREVBUTTONHIGHLIGHT;
			this.mBuyButton.mColors[0] = new SexyColor(255, 240, 0);
			this.mBuyButton.mColors[1] = new SexyColor(200, 200, 255);
			this.mBuyButton.Resize(Constants.StoreScreen_PrevButton_X, Constants.StoreScreen_PrevButton_Y, AtlasResources.IMAGE_STORE_PREVBUTTON.mWidth, AtlasResources.IMAGE_STORE_PREVBUTTON.mHeight);
		}

		public override void Dispose()
		{
			this.mBackButton.Dispose();
			this.mBuyButton.Dispose();
			this.mApp.DelayLoadUpsellResource(string.Empty);
		}

		public override void Update()
		{
			this.mApp.mMusic.MakeSureMusicIsPlaying(MusicTune.MUSIC_TUNE_MINIGAME_LOONBOON);
			this.mApp.UpdateCrazyDave();
			base.Update();
		}

		public override void Draw(Graphics g)
		{
		}

		public override void AddedToManager(WidgetManager theWidgetManager)
		{
			base.AddedToManager(theWidgetManager);
			this.AddWidget(this.mBackButton);
			this.AddWidget(this.mBuyButton);
		}

		public override void RemovedFromManager(WidgetManager theWidgetManager)
		{
			base.RemovedFromManager(theWidgetManager);
			this.RemoveWidget(this.mBackButton);
			this.RemoveWidget(this.mBuyButton);
			this.mApp.CrazyDaveDie();
		}

		public override void ButtonPress(int theId)
		{
			this.mApp.PlaySample(Resources.SOUND_BUTTONCLICK);
		}

		public override void ButtonDepress(int theId)
		{
			switch (theId)
			{
			case 1000:
				this.mApp.BuyGame();
				break;
			case 1001:
				break;
			default:
				return;
			}
		}

		public override void MouseDown(int x, int y, int theClickCount)
		{
			this.AdvanceCrazyDaveDialog();
		}

		public void SetBubbleText(int theCrazyDaveMessage)
		{
			this.mApp.CrazyDaveTalkIndex(theCrazyDaveMessage);
		}

		public void AdvanceCrazyDaveDialog()
		{
		}

		public LawnApp mApp;

		public NewLawnButton mBackButton;

		public NewLawnButton mBuyButton;
	}
}
