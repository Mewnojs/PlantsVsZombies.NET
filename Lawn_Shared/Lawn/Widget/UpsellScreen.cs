using System;
using Sexy;

namespace Lawn
{
    public/*internal*/ class UpsellScreen : Dialog
    {
        public UpsellScreen(LawnApp theApp) : base(null, null, 54, true, "", "", "", 0)
        {
            mApp = theApp;
            mClip = false;
            mApp.DelayLoadUpsellResource("");
            Resize(0, 0, Constants.BOARD_WIDTH, Constants.BOARD_HEIGHT);
            mBackButton = new NewLawnButton(null, 1001, this);
            mBackButton.mDoFinger = true;
            mBackButton.mLabel = "";
            mBackButton.mButtonImage = AtlasResources.IMAGE_STORE_MAINMENUBUTTON;
            mBackButton.mDownImage = AtlasResources.IMAGE_STORE_MAINMENUBUTTONDOWN;
            mBackButton.mColors[0] = new SexyColor(98, 153, 235);
            mBackButton.mColors[1] = new SexyColor(167, 192, 235);
            mBackButton.Resize(Constants.StoreScreen_BackButton_X, Constants.StoreScreen_BackButton_Y, AtlasResources.IMAGE_STORE_MAINMENUBUTTON.mWidth, AtlasResources.IMAGE_STORE_MAINMENUBUTTON.mHeight);
            mBackButton.mTextOffsetX = -7;
            mBackButton.mTextOffsetY = 1;
            mBackButton.mTextDownOffsetX = 2;
            mBackButton.mTextDownOffsetY = 1;
            mBuyButton = new NewLawnButton(null, 1000, this);
            mBuyButton.mDoFinger = true;
            mBuyButton.mLabel = "";
            mBuyButton.mButtonImage = AtlasResources.IMAGE_STORE_PREVBUTTON;
            mBuyButton.mOverImage = AtlasResources.IMAGE_STORE_PREVBUTTONHIGHLIGHT;
            mBuyButton.mDownImage = AtlasResources.IMAGE_STORE_PREVBUTTONHIGHLIGHT;
            mBuyButton.mColors[0] = new SexyColor(255, 240, 0);
            mBuyButton.mColors[1] = new SexyColor(200, 200, 255);
            mBuyButton.Resize(Constants.StoreScreen_PrevButton_X, Constants.StoreScreen_PrevButton_Y, AtlasResources.IMAGE_STORE_PREVBUTTON.mWidth, AtlasResources.IMAGE_STORE_PREVBUTTON.mHeight);
        }

        public override void Dispose()
        {
            mBackButton.Dispose();
            mBuyButton.Dispose();
            mApp.DelayLoadUpsellResource(string.Empty);
        }

        public override void Update()
        {
            mApp.mMusic.MakeSureMusicIsPlaying(MusicTune.MinigameLoonboon);
            mApp.UpdateCrazyDave();
            base.Update();
        }

        public override void Draw(Graphics g)
        {
        }

        public override void AddedToManager(WidgetManager theWidgetManager)
        {
            base.AddedToManager(theWidgetManager);
            AddWidget(mBackButton);
            AddWidget(mBuyButton);
        }

        public override void RemovedFromManager(WidgetManager theWidgetManager)
        {
            base.RemovedFromManager(theWidgetManager);
            RemoveWidget(mBackButton);
            RemoveWidget(mBuyButton);
            mApp.CrazyDaveDie();
        }

        public override void ButtonPress(int theId)
        {
            mApp.PlaySample(Resources.SOUND_BUTTONCLICK);
        }

        public override void ButtonDepress(int theId)
        {
            switch (theId)
            {
            case 1000:
                mApp.BuyGame();
                break;
            case 1001:
                break;
            default:
                return;
            }
        }

        public override void MouseDown(int x, int y, int theClickCount)
        {
            AdvanceCrazyDaveDialog();
        }

        public void SetBubbleText(int theCrazyDaveMessage)
        {
            mApp.CrazyDaveTalkIndex(theCrazyDaveMessage);
        }

        public void AdvanceCrazyDaveDialog()
        {
        }

        public LawnApp mApp;

        public NewLawnButton mBackButton;

        public NewLawnButton mBuyButton;
    }
}
