using System;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
	internal class TitleScreen : Widget, ButtonListener
	{
		public void ButtonPress(int theId, int theClickCount)
		{
		}

		public void ButtonDownTick(int theId)
		{
		}

		public void ButtonMouseEnter(int theId)
		{
		}

		public void ButtonMouseLeave(int theId)
		{
		}

		public void ButtonMouseMove(int theId, int theX, int theY)
		{
		}

		public TitleScreen(LawnApp theApp)
		{
			this.mApp = theApp;
			this.mCurBarWidth = 0f;
			this.mTotalBarWidth = Constants.InvertAndScale(314f);
			this.mBarVel = 0.2f;
			this.mBarStartProgress = 0f;
			this.mPrevLoadingPercent = 0f;
			this.mTitleAge = 0;
			this.mNeedRegister = false;
			this.mRegisterClicked = false;
			this.mNeedShowRegisterBox = false;
			this.mLoadingThreadComplete = false;
			this.mNeedToInit = true;
			this.mQuickLoadKey = KeyCode.KEYCODE_UNKNOWN;
			this.mTitleState = TitleState.TITLESTATE_WAITING_FOR_FIRST_DRAW;
			this.mTitleStateDuration = 0;
			this.mTitleStateCounter = 0;
			this.mLoaderScreenIsLoaded = false;
			this.mNeedToUnpackAtlas = true;
			this.mStartButton = new HyperlinkWidget(0, this);
			this.mStartButton.mColor = new SexyColor(218, 184, 33);
			this.mStartButton.mOverColor = new SexyColor(250, 90, 15);
			this.mStartButton.mUnderlineSize = 0;
			this.mStartButton.mDisabled = true;
			this.mStartButton.mVisible = false;
			this.mNextImageIndex = 0;
			TitleScreen.PreflightImages = new Image[]
			{
				Resources.IMAGE_PLANTSZOMBIES,
				Resources.IMAGE_CHARREDZOMBIES,
				Resources.IMAGE_ALMANACUI,
				Resources.IMAGE_SEEDATLAS,
				Resources.IMAGE_DAVE,
				Resources.IMAGE_CACHED,
				Resources.IMAGE_DIALOG,
				Resources.IMAGE_PARTICLES,
				Resources.IMAGE_CONVEYORBELT_BACKDROP,
				Resources.IMAGE_CONVEYORBELT_BELT,
				Resources.IMAGE_QUICKPLAY,
				Resources.IMAGE_SPEECHBUBBLE,
				Resources.IMAGE_GOODIES,
				Resources.IMAGE_ZOMBIE_NOTE_SMALL,
				Resources.IMAGE_SELECTORSCREEN_ACHIEVEMENTS_TOP_BACKGROUND,
				Resources.IMAGE_SELECTORSCREEN_MOREGAMES_BACKGROUND,
				Resources.IMAGE_SELECTORSCREEN_MAIN_BACKGROUND,
				Resources.IMAGE_SELECTORSCREEN_ACHIEVEMENTS_HOLE,
				Resources.IMAGE_SELECTORSCREEN_ACHIEVEMENTS_HOLE_CHINA,
				Resources.IMAGE_SELECTORSCREEN_QUICKPLAY_BACKGROUND,
				Resources.IMAGE_REANIM_ZOMBIESWON,
				Resources.IMAGE_SCARY_POT
			};
			this.aTriggerPoint[0] = 0.11f * this.mTotalBarWidth;
			this.aTriggerPoint[1] = 0.32f * this.mTotalBarWidth;
			this.aTriggerPoint[2] = 0.52f * this.mTotalBarWidth;
			this.aTriggerPoint[3] = 0.72f * this.mTotalBarWidth;
			this.aTriggerPoint[4] = 0.906f * this.mTotalBarWidth;
		}

		public override void Dispose()
		{
			if (Main.LOW_MEMORY_DEVICE)
			{
				this.mApp.mResourceManager.DeleteResources("Init");
				this.mApp.mResourceManager.DeleteResources("LoaderBar");
			}
			this.mStartButton.Dispose();
		}

		public override void Update()
		{
			base.Update();
			if (this.mApp.mShutdown)
			{
				return;
			}
			this.MarkDirty();
			if (this.mTitleState == TitleState.TITLESTATE_WAITING_FOR_FIRST_DRAW)
			{
				this.mApp.mMusic.MusicTitleScreenInit();
				this.mApp.StartLoadingThread();
				this.mTitleState = TitleState.TITLESTATE_POPCAP_LOGO;
				this.mTitleStateDuration = 200;
				this.mTitleStateCounter = this.mTitleStateDuration;
			}
			if (this.mQuickLoadKey != KeyCode.KEYCODE_UNKNOWN && this.mTitleState != TitleState.TITLESTATE_SCREEN)
			{
				this.mTitleState = TitleState.TITLESTATE_SCREEN;
				this.mTitleStateDuration = 0;
				this.mTitleStateCounter = 100;
			}
			this.mTitleAge += 3;
			if (this.mTitleStateCounter > 0)
			{
				this.mTitleStateCounter -= 3;
			}
			if (this.mTitleState == TitleState.TITLESTATE_POPCAP_LOGO)
			{
				if (this.mTitleStateCounter <= 0)
				{
					this.mTitleState = TitleState.TITLESTATE_SCREEN;
					this.mTitleStateDuration = 100;
					this.mTitleStateCounter = this.mTitleStateDuration;
				}
				return;
			}
			if (!this.mLoaderScreenIsLoaded)
			{
				return;
			}
			if (this.mNeedToUnpackAtlas)
			{
				AtlasResources.mAtlasResources.UnpackLoadingAtlasImages();
				this.mNeedToUnpackAtlas = false;
			}
			float num = (float)this.mApp.GetLoadingThreadProgress();
			if (this.mNeedToInit)
			{
				this.mNeedToInit = false;
				this.mStartButton.mLabel = TodStringFile.TodStringTranslate("[LOADING]");
				this.mStartButton.SetFont(Resources.FONT_BRIANNETOD16);
				this.mStartButton.Resize(this.mWidth / 2 - AtlasResources.IMAGE_LOADBAR_DIRT.mWidth / 2, (int)(Constants.S * 650f), (int)this.mTotalBarWidth, (int)Constants.InvertAndScale(18f));
				this.mStartButton.mVisible = true;
				float num2;
				if (num > 1E-06f)
				{
					num2 = (float)this.mTitleAge / num;
				}
				else
				{
					num2 = 3000f;
				}
				float num3 = num2 * (1f - num);
				num3 = TodCommon.ClampFloat(num3, 100f, 3000f);
				this.mBarVel = this.mTotalBarWidth / num3;
				this.mBarStartProgress = Math.Min(num, 0.9f);
			}
			float num4 = (num - this.mBarStartProgress) / (1f - this.mBarStartProgress);
			int theY;
			if (this.mTitleStateCounter > 10)
			{
				theY = TodCommon.TodAnimateCurve(60, 10, this.mTitleStateCounter, (int)(Constants.S * 731f), (int)(Constants.S * 532f), TodCurves.CURVE_EASE_IN);
			}
			else
			{
				theY = TodCommon.TodAnimateCurve(10, 0, this.mTitleStateCounter, (int)(Constants.S * 532f), (int)(Constants.S * 523f), TodCurves.CURVE_BOUNCE);
			}
			this.mStartButton.Resize(this.mStartButton.mX, theY, (int)this.mTotalBarWidth, this.mStartButton.mHeight);
			if (this.mTitleStateCounter > 0)
			{
				return;
			}
			this.mApp.mEffectSystem.Update();
			float num5 = this.mCurBarWidth;
			this.mCurBarWidth += this.mBarVel;
			if (!this.mLoadingThreadComplete)
			{
				if (this.mCurBarWidth > this.mTotalBarWidth * 0.99f)
				{
					this.mCurBarWidth = this.mTotalBarWidth * 0.99f;
				}
			}
			else if (this.mCurBarWidth > this.mTotalBarWidth)
			{
				if (this.mApp.mRestoreLocation == RestoreLocation.RESTORE_BOARD)
				{
					this.mApp.LoadingCompleted();
				}
				else
				{
					this.mStartButton.mLabel = TodStringFile.TodStringTranslate("[CLICK_TO_START]");
					this.mCurBarWidth = this.mTotalBarWidth;
				}
			}
			if (num4 > this.mPrevLoadingPercent + 0.01f || this.mLoadingThreadComplete)
			{
				float num6 = TodCommon.TodAnimateCurveFloatTime(0f, 1f, num4, 0f, this.mTotalBarWidth, TodCurves.CURVE_EASE_IN);
				float num7 = num6 - this.mCurBarWidth;
				float num8 = TodCommon.TodAnimateCurveFloatTime(0f, 1f, num4, 0.0001f, 1E-05f, TodCurves.CURVE_LINEAR);
				if (this.mLoadingThreadComplete)
				{
					num8 = 0.0001f;
				}
				this.mBarVel += num7 * Math.Abs(num7) * num8;
				float num9 = TodCommon.TodAnimateCurveFloatTime(0f, 1f, num4, 0.2f, 0.01f, TodCurves.CURVE_LINEAR);
				float num10 = 2f;
				if (this.mApp.mTodCheatKeys)
				{
					num9 = 0f;
					num10 = 100f;
				}
				if (this.mBarVel < num9)
				{
					this.mBarVel = num9;
				}
				else if (this.mBarVel > num10)
				{
					this.mBarVel = num10;
				}
				this.mPrevLoadingPercent = num4;
			}
			if (GameConstants.TESTING_LOAD_BAR && this.mBarVel > 0.5f)
			{
				this.mBarVel = 0.5f;
			}
			if (!this.mLoadingThreadComplete && this.mApp.mLoadingThreadCompleted)
			{
				LawnApp.PreallocateMemory();
				this.mLoadingThreadComplete = true;
				this.mStartButton.SetDisabled(false);
				this.mStartButton.SetVisible(true);
			}
			for (int i = 0; i < 5; i++)
			{
				if (this.aTriggerPoint[i] > num5 && this.aTriggerPoint[i] <= this.mCurBarWidth)
				{
					ReanimationType theReanimationType = ReanimationType.REANIM_LOADBAR_SPROUT;
					if (i == 4)
					{
						theReanimationType = ReanimationType.REANIM_LOADBAR_ZOMBIEHEAD;
					}
					float num11 = (float)Constants.TitleScreen_ReanimStart_X + this.aTriggerPoint[i];
					float num12 = (float)this.mStartButton.mY - Constants.InvertAndScale(42f);
					Reanimation reanimation = this.mApp.AddReanimation(num11, num12, 0, theReanimationType, false);
					reanimation.mLoopType = ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD;
					reanimation.mAnimRate = 18f;
					if (i == 1 || i == 3)
					{
						reanimation.OverrideScale(-1f, 1f);
					}
					else if (i == 2)
					{
						reanimation.SetPosition(num11, num12 - 5f);
						reanimation.OverrideScale(1.1f, 1.2f);
					}
					else if (i == 4)
					{
						reanimation.SetPosition(num11 - Constants.InvertAndScale(20f), num12);
					}
					if (i == 4)
					{
						this.mApp.PlaySample(Resources.SOUND_LOADINGBAR_FLOWER);
						this.mApp.PlaySample(Resources.SOUND_LOADINGBAR_ZOMBIE);
					}
					else
					{
						this.mApp.PlaySample(Resources.SOUND_LOADINGBAR_FLOWER);
					}
				}
			}
		}

		public override void Draw(Graphics g)
		{
			g.SetLinearBlend(true);
			base.Draw(g);
			if (this.mTitleState == TitleState.TITLESTATE_POPCAP_LOGO)
			{
				g.SetColor(SexyColor.Black);
				g.FillRect(0, 0, this.mWidth, this.mHeight);
				int num = 50;
				int theAlpha = 255;
				if (this.mTitleStateCounter < this.mTitleStateDuration - num)
				{
					theAlpha = TodCommon.TodAnimateCurve(num, 0, this.mTitleStateCounter, 255, 0, TodCurves.CURVE_LINEAR);
				}
				g.SetColorizeImages(true);
				g.SetColor(new SexyColor(255, 255, 255, theAlpha));
				if (Constants.Language != Constants.LanguageIndex.de)
				{
					g.DrawImage(Resources.IMAGE_POPCAP_LOGO, (this.mWidth - Resources.IMAGE_POPCAP_LOGO.mWidth) / 2, (this.mHeight - Resources.IMAGE_POPCAP_LOGO.mHeight) / 2);
				}
				else
				{
					g.DrawImage(Resources.IMAGE_POPCAP_LOGO_REGISTERED, (this.mWidth - Resources.IMAGE_POPCAP_LOGO_REGISTERED.mWidth) / 2, (this.mHeight - Resources.IMAGE_POPCAP_LOGO_REGISTERED.mHeight) / 2);
				}
				g.SetColorizeImages(false);
				return;
			}
			if (!this.mLoaderScreenIsLoaded)
			{
				g.SetColor(SexyColor.Black);
				g.FillRect(0, 0, this.mWidth, this.mHeight);
				return;
			}
			this.PreflightNextImage(g);
			g.DrawImage(Resources.IMAGE_TITLESCREEN, 0, 0);
			if (this.mNeedToInit)
			{
				return;
			}
			int theY;
			if (this.mTitleStateCounter > 60)
			{
				theY = TodCommon.TodAnimateCurve(100, 60, this.mTitleStateCounter, (int)Constants.InvertAndScale(-150f), (int)Constants.InvertAndScale(10f), TodCurves.CURVE_EASE_IN);
			}
			else
			{
				theY = TodCommon.TodAnimateCurve(60, 50, this.mTitleStateCounter, (int)Constants.InvertAndScale(10f), (int)Constants.InvertAndScale(15f), TodCurves.CURVE_BOUNCE);
			}
			g.DrawImage(Resources.IMAGE_PVZ_LOGO, this.mWidth / 2 - Resources.IMAGE_PVZ_LOGO.mWidth / 2, theY);
			int mX = this.mStartButton.mX;
			int num2 = this.mStartButton.mY - (int)Constants.InvertAndScale(34f);
			g.DrawImage(AtlasResources.IMAGE_LOADBAR_DIRT, (float)mX, (float)num2 + Constants.InvertAndScale(18f));
			if (this.mCurBarWidth >= this.mTotalBarWidth)
			{
				g.DrawImage(AtlasResources.IMAGE_LOADBAR_GRASS, mX, num2);
				if (this.mLoadingThreadComplete)
				{
					this.DrawToPreload(g);
				}
			}
			else
			{
				Graphics @new = Graphics.GetNew(g);
				@new.ClipRect(mX, num2, (int)this.mCurBarWidth, AtlasResources.IMAGE_LOADBAR_GRASS.mHeight);
				@new.DrawImage(AtlasResources.IMAGE_LOADBAR_GRASS, mX, num2);
				float num3 = this.mCurBarWidth * 0.94f;
				float rad = -num3 / 180f * 3.1415927f * 2f;
				float num4 = TodCommon.TodAnimateCurveFloatTime(0f, this.mTotalBarWidth, this.mCurBarWidth, 1f, 0.5f, TodCurves.CURVE_LINEAR);
				SexyTransform2D sexyTransform2D = default(SexyTransform2D);
				TodCommon.TodScaleRotateTransformMatrix(ref sexyTransform2D.mMatrix, (float)mX + Constants.InvertAndScale(11f) + num3, (float)num2 - Constants.InvertAndScale(3f) - Constants.InvertAndScale(35f) * num4 + Constants.InvertAndScale(35f), rad, num4, num4);
				TRect theSrcRect = new TRect(0, 0, AtlasResources.IMAGE_REANIM_LOAD_SODROLLCAP.mWidth, AtlasResources.IMAGE_REANIM_LOAD_SODROLLCAP.mHeight);
				TodCommon.TodBltMatrix(g, AtlasResources.IMAGE_REANIM_LOAD_SODROLLCAP, sexyTransform2D.mMatrix, ref g.mClipRect, SexyColor.White, g.mDrawMode, theSrcRect);
				@new.PrepareForReuse();
			}
			foreach (Reanimation reanimation in this.mApp.mEffectSystem.mReanimationHolder.mReanimations)
			{
				reanimation.Draw(g);
			}
		}

		public override void Resize(int theX, int theY, int theWidth, int theHeight)
		{
			base.Resize(theX, theY, theWidth, theHeight);
		}

		public override void AddedToManager(WidgetManager theWidgetManager)
		{
			base.AddedToManager(theWidgetManager);
			theWidgetManager.AddWidget(this.mStartButton);
		}

		public override void RemovedFromManager(WidgetManager theWidgetManager)
		{
			base.RemovedFromManager(theWidgetManager);
			theWidgetManager.RemoveWidget(this.mStartButton);
		}

		public virtual void ButtonPress(int theId)
		{
			this.mApp.PlaySample(Resources.SOUND_BUTTONCLICK);
		}

		public override bool BackButtonPress()
		{
			this.mApp.AppExit();
			return true;
		}

		public virtual void ButtonDepress(int theId)
		{
			switch (theId)
			{
			case 0:
				this.mApp.LoadingCompleted();
				return;
			case 1:
				this.mRegisterClicked = true;
				return;
			default:
				return;
			}
		}

		public override void MouseDown(int x, int y, int theClickCount)
		{
			if (this.mLoadingThreadComplete)
			{
				this.mApp.PlaySample(Resources.SOUND_BUTTONCLICK);
				this.mApp.LoadingCompleted();
			}
		}

		public override void KeyDown(KeyCode theKey)
		{
			if (this.mLoadingThreadComplete)
			{
				this.mApp.PlaySample(Resources.SOUND_BUTTONCLICK);
				this.mApp.LoadingCompleted();
				return;
			}
			if (this.mApp.mTodCheatKeys && this.mApp.mPlayerInfo != null)
			{
				this.mQuickLoadKey = theKey;
			}
		}

		public void SetRegistered()
		{
		}

		public void DrawToPreload(Graphics g)
		{
		}

		public void PreflightNextImage(Graphics g)
		{
			if (this.mNextImageIndex < TitleScreen.PreflightImages.Length && TitleScreen.PreflightImages[this.mNextImageIndex] != null)
			{
				g.DrawImage(TitleScreen.PreflightImages[this.mNextImageIndex], 0, 0, new TRect(0, 0, 1, 1));
				this.mNextImageIndex++;
			}
		}

		private const int NUM_TRIGGERS = 5;

		public HyperlinkWidget mStartButton;

		public float mCurBarWidth;

		public float mTotalBarWidth;

		public float mBarVel;

		public float mBarStartProgress;

		public bool mRegisterClicked;

		public bool mLoadingThreadComplete;

		public int mTitleAge;

		public KeyCode mQuickLoadKey;

		public bool mNeedRegister;

		public bool mNeedShowRegisterBox;

		public bool mNeedToInit;

		public float mPrevLoadingPercent;

		public TitleState mTitleState;

		public int mTitleStateCounter;

		public int mTitleStateDuration;

		public bool mLoaderScreenIsLoaded;

		public bool mNeedToUnpackAtlas;

		public int mNextImageIndex;

		public LawnApp mApp;

		private static Image[] PreflightImages;

		private float[] aTriggerPoint = new float[5];
	}
}
