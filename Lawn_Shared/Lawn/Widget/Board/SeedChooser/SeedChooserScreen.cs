using System;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
	internal class SeedChooserScreen : Widget, StoreListener, AlmanacListener, LawnMessageBoxListener, SeedPacketsWidgetListener
	{
		static SeedChooserScreen()
		{
			for (int i = 0; i < SeedChooserScreen.aSeedArray.Length; i++)
			{
				SeedChooserScreen.aSeedArray[i] = TodWeightedArray.GetNewTodWeightedArray();
			}
		}

		public SeedChooserScreen()
		{
			this.mApp = (LawnApp)GlobalStaticVars.gSexyAppBase;
			this.mBoard = this.mApp.mBoard;
			this.mClip = false;
			this.mSeedsInFlight = 0;
			this.mSeedsInBank = 0;
			this.mLastMouseX = -1;
			this.mLastMouseY = -1;
			this.mChooseState = SeedChooserState.CHOOSE_NORMAL;
			this.mViewLawnTime = 0;
			this.mDoStartButton = false;
			this.mSeedPacketsWidget = new SeedPacketsWidget(this.mApp, this.Has12Rows() ? 12 : 11, false, this);
			this.mScrollWidget = new ScrollWidget();
			this.mScrollWidget.Resize(Constants.SCROLL_AREA_OFFSET_X, Constants.SCROLL_AREA_OFFSET_Y, this.mSeedPacketsWidget.mWidth + (int)Constants.InvertAndScale(10f), (int)Constants.InvertAndScale(227f));
			this.mScrollWidget.AddWidget(this.mSeedPacketsWidget);
			this.mScrollWidget.EnableIndicators(AtlasResources.IMAGE_SCROLL_INDICATOR);
			this.mSeedPacketsWidget.Move(0, 0);
			this.AddWidget(this.mScrollWidget);
			this.mStartButton = new GameButton(100, this);
			this.mStartButton.SetLabel("[PLAY_BUTTON]");
			this.mStartButton.mButtonImage = AtlasResources.IMAGE_SEEDCHOOSER_SMALL_BUTTON;
			this.mStartButton.mOverImage = null;
			this.mStartButton.mDownImage = AtlasResources.IMAGE_SEEDCHOOSER_SMALL_BUTTON_PRESSED;
			this.mStartButton.mDisabledImage = AtlasResources.IMAGE_SEEDCHOOSER_SMALL_BUTTON_DISABLED;
			this.mStartButton.mOverOverlayImage = null;
			this.mStartButton.SetFont(Resources.FONT_DWARVENTODCRAFT15);
			this.mStartButton.mColors[1] = new SexyColor(255, 231, 26);
			this.mStartButton.mColors[0] = new SexyColor(255, 231, 26);
			this.mStartButton.Resize((int)Constants.InvertAndScale(147f), (int)Constants.InvertAndScale(280f), (int)Constants.InvertAndScale(67f), this.mStartButton.mButtonImage.GetHeight());
			this.mStartButton.mTextOffsetX = 0;
			this.mStartButton.mTextOffsetX = 0;
			this.mStartButton.mTextPushOffsetX = 1;
			this.mStartButton.mTextPushOffsetY = 3;
			this.EnableStartButton(false);
			this.mMenuButton = new GameButton(104, this);
			this.mMenuButton.SetLabel("[MENU_BUTTON]");
			this.mMenuButton.Resize(Constants.UIMenuButtonPosition.X + Constants.Board_Offset_AspectRatio_Correction, Constants.UIMenuButtonPosition.Y, Constants.UIMenuButtonWidth, AtlasResources.IMAGE_BUTTON_LEFT.mHeight);
			this.mMenuButton.mDrawStoneButton = true;
			this.mRandomButton = new GameButton(101, this);
			this.mRandomButton.SetLabel("(Debug Play)");
			this.mRandomButton.mButtonImage = AtlasResources.IMAGE_BLANK;
			this.mRandomButton.mOverImage = AtlasResources.IMAGE_BLANK;
			this.mRandomButton.mDownImage = AtlasResources.IMAGE_BLANK;
			this.mRandomButton.SetFont(Resources.FONT_BRIANNETOD12);
			this.mRandomButton.mColors[0] = new SexyColor(255, 240, 0);
			this.mRandomButton.mColors[1] = new SexyColor(200, 200, 255);
			this.mRandomButton.Resize((int)Constants.InvertAndScale(332f), (int)Constants.InvertAndScale(546f), (int)Constants.InvertAndScale(100f), (int)Constants.InvertAndScale(30f));
			if (!this.mApp.mTodCheatKeys)
			{
				this.mRandomButton.mBtnNoDraw = true;
				this.mRandomButton.mDisabled = true;
			}
			this.mViewLawnButton = new GameButton(102, this);
			this.mViewLawnButton.SetLabel("[VIEW_LAWN]");
			this.mViewLawnButton.mButtonImage = AtlasResources.IMAGE_SEEDCHOOSER_BUTTON2;
			this.mViewLawnButton.mOverImage = AtlasResources.IMAGE_SEEDCHOOSER_BUTTON2_GLOW;
			this.mViewLawnButton.mDownImage = null;
			this.mViewLawnButton.SetFont(Resources.FONT_BRIANNETOD12);
			this.mViewLawnButton.mColors[0] = new SexyColor(42, 42, 90);
			this.mViewLawnButton.mColors[1] = new SexyColor(42, 42, 90);
			this.mViewLawnButton.Resize((int)Constants.InvertAndScale(22f), (int)Constants.InvertAndScale(300f), (int)Constants.InvertAndScale(111f), (int)Constants.InvertAndScale(26f));
			this.mViewLawnButton.mParentWidget = this;
			this.mViewLawnButton.mTextOffsetY = 1;
			if (!this.mBoard.mCutScene.IsSurvivalRepick())
			{
				this.mViewLawnButton.mBtnNoDraw = true;
				this.mViewLawnButton.mDisabled = true;
			}
			this.mAlmanacButton = new GameButton(103, this);
			this.mAlmanacButton.SetLabel("[ALMANAC_BUTTON]");
			this.mAlmanacButton.mButtonImage = AtlasResources.IMAGE_SEEDCHOOSER_BUTTON2;
			this.mAlmanacButton.mOverImage = AtlasResources.IMAGE_SEEDCHOOSER_BUTTON2_GLOW;
			this.mAlmanacButton.mDownImage = null;
			this.mAlmanacButton.SetFont(Resources.FONT_BRIANNETOD12);
			this.mAlmanacButton.mColors[0] = new SexyColor(42, 42, 90);
			this.mAlmanacButton.mColors[1] = new SexyColor(42, 42, 90);
			this.mAlmanacButton.Resize((int)Constants.InvertAndScale(63f), (int)Constants.InvertAndScale(286f), AtlasResources.IMAGE_SEEDCHOOSER_BUTTON2.mWidth, (int)Constants.InvertAndScale(26f));
			this.mAlmanacButton.mParentWidget = this;
			this.mAlmanacButton.mTextOffsetY = (int)Constants.InvertAndScale(2f);
			this.mStoreButton = new GameButton(105, this);
			this.mStoreButton.SetLabel("[SHOP_BUTTON]");
			this.mStoreButton.mButtonImage = AtlasResources.IMAGE_SEEDCHOOSER_BUTTON2;
			this.mStoreButton.mOverImage = AtlasResources.IMAGE_SEEDCHOOSER_BUTTON2_GLOW;
			this.mStoreButton.mDownImage = null;
			this.mStoreButton.SetFont(Resources.FONT_BRIANNETOD12);
			this.mStoreButton.mColors[0] = new SexyColor(42, 42, 90);
			this.mStoreButton.mColors[1] = new SexyColor(42, 42, 90);
			this.mStoreButton.Resize((int)Constants.InvertAndScale(218f), (int)Constants.InvertAndScale(286f), AtlasResources.IMAGE_SEEDCHOOSER_BUTTON2.mWidth, (int)Constants.InvertAndScale(26f));
			this.mStoreButton.mParentWidget = this;
			this.mStoreButton.mTextOffsetY = (int)Constants.InvertAndScale(2f);
			this.mImitaterButton = new GameButton(106, this);
			this.mImitaterButton.mButtonImage = null;
			this.mImitaterButton.mOverImage = null;
			this.mImitaterButton.mDownImage = null;
			this.mImitaterButton.mDisabledImage = null;
			this.mImitaterButton.Resize((int)Constants.InvertAndScale(310f), (int)Constants.InvertAndScale(27f), Constants.SMALL_SEEDPACKET_WIDTH, Constants.SMALL_SEEDPACKET_HEIGHT);
			this.mImitaterButton.mParentWidget = this;
			if (!this.mApp.CanShowAlmanac())
			{
				this.mAlmanacButton.mBtnNoDraw = true;
				this.mAlmanacButton.mDisabled = true;
			}
			if (!this.mApp.CanShowStore())
			{
				this.mStoreButton.mBtnNoDraw = true;
				this.mStoreButton.mDisabled = true;
			}
			for (int i = 0; i < 49; i++)
			{
				SeedType mSeedType = (SeedType)i;
				ChosenSeed chosenSeed = new ChosenSeed();
				chosenSeed.mSeedType = mSeedType;
				this.GetSeedPositionInChooser(i, ref chosenSeed.mX, ref chosenSeed.mY);
				chosenSeed.mTimeStartMotion = 0;
				chosenSeed.mTimeEndMotion = 0;
				chosenSeed.mStartX = chosenSeed.mX;
				chosenSeed.mStartY = chosenSeed.mY;
				chosenSeed.mEndX = chosenSeed.mX;
				chosenSeed.mEndY = chosenSeed.mY;
				chosenSeed.mSeedState = ChosenSeedState.SEED_IN_CHOOSER;
				chosenSeed.mSeedIndexInBank = 0;
				chosenSeed.mRefreshCounter = 0;
				chosenSeed.mRefreshing = false;
				chosenSeed.mImitaterType = SeedType.SEED_NONE;
				chosenSeed.mCrazyDavePicked = false;
				if (i == 48)
				{
					chosenSeed.mSeedState = ChosenSeedState.SEED_PACKET_HIDDEN;
				}
				this.mChosenSeeds[i] = chosenSeed;
			}
			if (this.mBoard.mCutScene.IsSurvivalRepick())
			{
				for (int j = 0; j < this.mBoard.mSeedBank.mNumPackets; j++)
				{
					SeedPacket seedPacket = this.mBoard.mSeedBank.mSeedPackets[j];
					SeedType mPacketType = seedPacket.mPacketType;
					ChosenSeed chosenSeed2 = this.mChosenSeeds[(int)mPacketType];
					chosenSeed2.mRefreshing = seedPacket.mRefreshing;
					chosenSeed2.mRefreshCounter = seedPacket.mRefreshCounter;
				}
				this.mBoard.mSeedBank.mNumPackets = 0;
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_SEEING_STARS)
			{
				ChosenSeed chosenSeed3 = this.mChosenSeeds[29];
				chosenSeed3.mY = this.mBoard.GetSeedPacketPositionY(0);
				chosenSeed3.mX = 0;
				chosenSeed3.mEndX = chosenSeed3.mX;
				chosenSeed3.mEndY = chosenSeed3.mY;
				chosenSeed3.mStartX = chosenSeed3.mX;
				chosenSeed3.mStartY = chosenSeed3.mY;
				chosenSeed3.mSeedState = ChosenSeedState.SEED_IN_BANK;
				chosenSeed3.mSeedIndexInBank = 0;
				this.mSeedsInBank++;
			}
			if (this.mApp.IsAdventureMode() && !this.mApp.IsFirstTimeAdventureMode())
			{
				this.CrazyDavePickSeeds();
			}
			this.UpdateImitaterButton();
			for (int k = 0; k < this.mPickWarningsWaved.Length; k++)
			{
				this.mPickWarningsWaved[k] = false;
			}
		}

		public override void Dispose()
		{
			this.mStartButton.Dispose();
			this.mRandomButton.Dispose();
			this.mViewLawnButton.Dispose();
			this.mAlmanacButton.Dispose();
			this.mImitaterButton.Dispose();
			this.mStoreButton.Dispose();
			this.mMenuButton.Dispose();
			this.RemoveAllWidgets(true);
		}

		public override void Update()
		{
			base.Update();
			this.mLastMouseX = this.mApp.mWidgetManager.mLastMouseX;
			this.mLastMouseY = this.mApp.mWidgetManager.mLastMouseY;
			this.mSeedChooserAge += 3;
			for (int i = 0; i < 49; i++)
			{
				SeedType theSeedType = (SeedType)i;
				if (this.mApp.HasSeedType(theSeedType))
				{
					ChosenSeed chosenSeed = this.mChosenSeeds[i];
					if (chosenSeed.mSeedState == ChosenSeedState.SEED_FLYING_TO_BANK || chosenSeed.mSeedState == ChosenSeedState.SEED_FLYING_TO_CHOOSER)
					{
						chosenSeed.mX = TodCommon.TodAnimateCurve(chosenSeed.mTimeStartMotion, chosenSeed.mTimeEndMotion, this.mSeedChooserAge, chosenSeed.mStartX, chosenSeed.mEndX, TodCurves.CURVE_EASE_IN_OUT);
						chosenSeed.mY = TodCommon.TodAnimateCurve(chosenSeed.mTimeStartMotion, chosenSeed.mTimeEndMotion, this.mSeedChooserAge, chosenSeed.mStartY, chosenSeed.mEndY, TodCurves.CURVE_EASE_IN_OUT);
					}
					if (chosenSeed.mSeedState == ChosenSeedState.SEED_FLYING_TO_BANK && this.mSeedChooserAge >= chosenSeed.mTimeEndMotion)
					{
						this.LandFlyingSeed(ref chosenSeed);
					}
					if (chosenSeed.mSeedState == ChosenSeedState.SEED_FLYING_TO_CHOOSER && this.mSeedChooserAge >= chosenSeed.mTimeEndMotion)
					{
						this.LandFlyingSeed(ref chosenSeed);
					}
				}
			}
			this.mStartButton.Update();
			this.mRandomButton.Update();
			this.mViewLawnButton.Update();
			this.mAlmanacButton.Update();
			this.mImitaterButton.Update();
			this.mStoreButton.Update();
			this.mMenuButton.Update();
			this.UpdateViewLawn();
			if (this.mDoStartButton)
			{
				this.mDoStartButton = false;
				this.OnStartButton();
			}
			this.MarkDirty();
		}

		public override void Draw(Graphics g)
		{
			if (this.mApp.GetDialog(Dialogs.DIALOG_STORE) != null || this.mApp.GetDialog(Dialogs.DIALOG_ALMANAC) != null)
			{
				return;
			}
			g.SetLinearBlend(true);
			if (!this.mBoard.ChooseSeedsOnCurrentLevel())
			{
				return;
			}
			if (this.mBoard.mCutScene != null && this.mBoard.mCutScene.IsBeforePreloading())
			{
				return;
			}
			g.DrawImage(AtlasResources.IMAGE_SEEDCHOOSER_BACKGROUND_TOP, Constants.SeedChooserScreen_Background_Top.X, Constants.SeedChooserScreen_Background_Top.Y);
			g.DrawImage(AtlasResources.IMAGE_SEEDCHOOSER_BACKGROUND_MIDDLE, Constants.SeedChooserScreen_Background_Middle.X, Constants.SeedChooserScreen_Background_Middle.Y, AtlasResources.IMAGE_SEEDCHOOSER_BACKGROUND_MIDDLE.mWidth, Constants.SeedChooserScreen_Background_Middle_Height);
			g.DrawImage(AtlasResources.IMAGE_SEEDCHOOSER_BACKGROUND_BOTTOM, Constants.SeedChooserScreen_Background_Bottom.X, Constants.SeedChooserScreen_Background_Bottom.Y);
			if (this.mApp.HasSeedType(SeedType.SEED_IMITATER))
			{
				g.DrawImage(AtlasResources.IMAGE_SEEDCHOOSER_IMITATERADDON, (int)Constants.InvertAndScale(304f), (int)Constants.InvertAndScale(18f));
				if (!this.mImitaterButton.mDisabled)
				{
					g.DrawImageCel(AtlasResources.IMAGE_SEEDPACKETS, this.mImitaterButton.mX, this.mImitaterButton.mY, 48);
				}
			}
			int mNumPackets = this.mBoard.mSeedBank.mNumPackets;
			for (int i = 0; i < mNumPackets; i++)
			{
				SeedType seedType = this.FindSeedInBank(i);
				int num = 0;
				int num2 = 0;
				this.GetSeedPositionInBank(i, ref num, ref num2);
				if (seedType != SeedType.SEED_NONE)
				{
					ChosenSeed chosenSeed = this.mChosenSeeds[(int)seedType];
					SeedPacket.DrawSmallSeedPacket(g, (float)num, (float)num2, seedType, chosenSeed.mImitaterType, 0f, 255, true, false, true, true);
					if (chosenSeed.mCrazyDavePicked)
					{
						g.DrawImage(AtlasResources.IMAGE_LOCK, num, num2 + (int)Constants.InvertAndScale(13f));
					}
				}
				else
				{
					g.DrawImage(AtlasResources.IMAGE_SEEDPACKETSILHOUETTE, num, num2);
				}
			}
			base.DeferOverlay();
			this.mStartButton.Draw(g);
			this.mRandomButton.Draw(g);
			this.mViewLawnButton.Draw(g);
			this.mAlmanacButton.Draw(g);
			this.mStoreButton.Draw(g);
			Graphics @new = Graphics.GetNew(g);
			@new.mTransX -= this.mX;
			@new.mTransY -= this.mY;
			this.mMenuButton.Draw(@new);
			@new.PrepareForReuse();
		}

		public override void DrawOverlay(Graphics g)
		{
			g.SetColor(Constants.SeedChooserScreen_BackColour);
			g.SetColorizeImages(true);
			if (this.mSeedPacketsWidget.mY < 0)
			{
				g.DrawImage(AtlasResources.IMAGE_ALMANAC_PLANTS_TOPGRADIENT, this.mScrollWidget.mX + Constants.SeedChooserScreen_Gradient_Top.X, Constants.SeedChooserScreen_Gradient_Top.Y, Constants.SeedChooserScreen_Gradient_Top.Width, Constants.SeedChooserScreen_Gradient_Top.Height);
			}
			if ((float)(this.mSeedPacketsWidget.mY + this.mSeedPacketsWidget.mHeight) > Constants.InvertAndScale(227f))
			{
				g.DrawImage(AtlasResources.IMAGE_ALMANAC_PLANTS_BOTTOMGRADIENT, this.mScrollWidget.mX + Constants.SeedChooserScreen_Gradient_Bottom.X, Constants.SeedChooserScreen_Gradient_Bottom.Y, Constants.SeedChooserScreen_Gradient_Bottom.Width, Constants.SeedChooserScreen_Gradient_Bottom.Height);
			}
			g.SetColorizeImages(false);
			bool flag = false;
			for (int i = 0; i < 49; i++)
			{
				SeedType theSeedType = (SeedType)i;
				if (this.mApp.HasSeedType(theSeedType))
				{
					ChosenSeed chosenSeed = this.mChosenSeeds[i];
					if (chosenSeed.mSeedState == ChosenSeedState.SEED_FLYING_TO_BANK)
					{
						g.SetClipRect(0, 0, this.mWidth, this.mHeight);
					}
					else
					{
						if (chosenSeed.mSeedState != ChosenSeedState.SEED_FLYING_TO_CHOOSER)
						{
							goto IL_170;
						}
						g.SetClipRect(0, 0, this.mWidth, this.mScrollWidget.mY + this.mScrollWidget.mHeight);
						g.HardwareClip();
						flag = true;
					}
					SeedPacket.DrawSmallSeedPacket(g, (float)chosenSeed.mX, (float)chosenSeed.mY, chosenSeed.mSeedType, chosenSeed.mImitaterType, 0f, 255, true, false, true, true);
				}
				IL_170:;
			}
			if (flag)
			{
				g.EndHardwareClip();
			}
			g.SetColorizeImages(false);
		}

		public virtual void ButtonPress(int theId)
		{
		}

		public override bool BackButtonPress()
		{
			int mId = this.mMenuButton.mId;
			this.ButtonPress(mId);
			this.ButtonDepress(mId);
			return true;
		}

		public virtual void ButtonDepress(int theId)
		{
			if (this.mSeedsInFlight > 0 || this.mChooseState == SeedChooserState.CHOOSE_VIEW_LAWN)
			{
				return;
			}
			if (!this.mMouseVisible)
			{
				return;
			}
			if (theId == 102)
			{
				this.mChooseState = SeedChooserState.CHOOSE_VIEW_LAWN;
				this.mMenuButton.mDisabled = true;
				this.mViewLawnTime = 0;
			}
			else if (theId == 103)
			{
				this.mApp.DoAlmanacDialog(SeedType.SEED_NONE, ZombieType.ZOMBIE_INVALID, this);
			}
			else
			{
				if (theId == 105)
				{
					StoreScreen storeScreen = this.mApp.ShowStoreScreen(this);
					storeScreen.mBackButton.mButtonImage = AtlasResources.IMAGE_STORE_CONTINUEBUTTON;
					storeScreen.mBackButton.mDownImage = AtlasResources.IMAGE_STORE_CONTINUEBUTTONDOWN;
					return;
				}
				if (theId == 104)
				{
					this.mMenuButton.mIsOver = false;
					this.mMenuButton.mIsDown = false;
					this.mApp.DoNewOptions(false);
				}
			}
			if (this.mApp.GetSeedsAvailable() < this.mBoard.mSeedBank.mNumPackets)
			{
				return;
			}
			if (theId == 100)
			{
				this.OnStartButton();
				return;
			}
			if (theId == 101)
			{
				this.PickRandomSeeds();
			}
		}

		public override void MouseDown(int x, int y, int theClickCount)
		{
			base.MouseDown(x, y, theClickCount);
			if (this.mSeedsInFlight > 0)
			{
				for (int i = 0; i < 49; i++)
				{
					ChosenSeed chosenSeed = this.mChosenSeeds[i];
					this.LandFlyingSeed(ref chosenSeed);
				}
			}
			if (this.mChooseState == SeedChooserState.CHOOSE_VIEW_LAWN)
			{
				this.CancelLawnView();
				return;
			}
			this.mStartButton.Update();
			this.mRandomButton.Update();
			this.mViewLawnButton.Update();
			this.mAlmanacButton.Update();
			this.mImitaterButton.Update();
			this.mStoreButton.Update();
			this.mMenuButton.Update();
			if (this.mRandomButton.IsMouseOver())
			{
				this.mApp.PlaySample(Resources.SOUND_TAP);
				this.ButtonDepress(101);
				return;
			}
			if (this.mViewLawnButton.IsMouseOver())
			{
				this.mApp.PlaySample(Resources.SOUND_TAP);
				this.ButtonDepress(102);
				return;
			}
			if (this.mMenuButton.IsMouseOver())
			{
				this.mApp.PlaySample(Resources.SOUND_GRAVEBUTTON);
			}
			if (this.mStartButton.IsMouseOver())
			{
				this.mApp.PlaySample(Resources.SOUND_TAP);
			}
			if (this.mAlmanacButton.IsMouseOver())
			{
				this.mApp.PlaySample(Resources.SOUND_TAP);
			}
			if (this.mStoreButton.IsMouseOver())
			{
				this.mApp.PlaySample(Resources.SOUND_TAP);
			}
			if (!this.mImitaterButton.IsMouseOver())
			{
				for (int j = 0; j < 49; j++)
				{
					ChosenSeed chosenSeed2 = this.mApp.mSeedChooserScreen.mChosenSeeds[j];
					if (chosenSeed2.mSeedState == ChosenSeedState.SEED_IN_BANK && x >= chosenSeed2.mX && y >= chosenSeed2.mY && x < chosenSeed2.mX + Constants.SMALL_SEEDPACKET_WIDTH && y < chosenSeed2.mY + Constants.SMALL_SEEDPACKET_HEIGHT)
					{
						this.ClickedSeedInBank(ref chosenSeed2);
						return;
					}
				}
				return;
			}
			if (this.mSeedsInBank == this.mBoard.mSeedBank.mNumPackets)
			{
				return;
			}
			this.mApp.PlaySample(Resources.SOUND_TAP);
			ImitaterDialog imitaterDialog = new ImitaterDialog();
			this.mApp.AddDialog(imitaterDialog.mId, imitaterDialog);
			imitaterDialog.Resize((this.mWidth - imitaterDialog.mWidth) / 2, (this.mHeight - imitaterDialog.mHeight) / 2, imitaterDialog.mWidth, imitaterDialog.mHeight);
			this.mApp.mWidgetManager.SetFocus(imitaterDialog);
		}

		public override void MouseUp(int x, int y, int theClickCount)
		{
			if (theClickCount != 1)
			{
				return;
			}
			if (this.mMenuButton.IsMouseOver())
			{
				this.ButtonDepress(104);
				return;
			}
			if (this.mStartButton.IsMouseOver())
			{
				this.ButtonDepress(100);
				return;
			}
			if (this.mAlmanacButton.IsMouseOver())
			{
				this.ButtonDepress(103);
				return;
			}
			if (this.mStoreButton.IsMouseOver())
			{
				this.ButtonDepress(105);
				return;
			}
			if (this.mImitaterButton.IsMouseOver())
			{
				this.ButtonDepress(106);
			}
		}

		public override void KeyChar(SexyChar theChar)
		{
			if (this.mChooseState == SeedChooserState.CHOOSE_VIEW_LAWN && (theChar.value_type == ' ' || theChar.value_type == '\r' || theChar.value_type == '\u001b'))
			{
				this.CancelLawnView();
				return;
			}
			if (this.mApp.mTodCheatKeys && theChar == KeyCode.KEYCODE_ESCAPE)
			{
				this.PickRandomSeeds();
				return;
			}
			this.mBoard.KeyChar(theChar);
		}

		public override void KeyDown(KeyCode theKey)
		{
			this.mBoard.DoTypingCheck(theKey);
		}

		public void GetSeedPositionInChooser(int theIndex, ref int x, ref int y)
		{
			if (theIndex == 48)
			{
				x = this.mImitaterButton.mX;
				y = this.mImitaterButton.mY;
				return;
			}
			this.mSeedPacketsWidget.GetSeedPosition((SeedType)theIndex, ref x, ref y);
			x += Constants.SCROLL_AREA_OFFSET_X;
			y += Constants.SCROLL_AREA_OFFSET_Y;
		}

		public void GetSeedPositionInBank(int theIndex, ref int x, ref int y)
		{
			y = this.mBoard.mSeedBank.mY + this.mBoard.GetSeedPacketPositionY(theIndex) - this.mY;
			x = this.mBoard.mSeedBank.mX;
		}

		public void ClickedSeedInChooser(ref ChosenSeed theChosenSeed)
		{
			if (this.mSeedsInBank == this.mBoard.mSeedBank.mNumPackets || !this.mApp.HasSeedType(theChosenSeed.mSeedType))
			{
				return;
			}
			if (this.mApp.mPlayerInfo.mNeedsGrayedPlantWarning)
			{
				uint num = this.SeedNotRecommendedToPick(theChosenSeed.mSeedType);
				if (num != 0U)
				{
					this.mApp.mPlayerInfo.mNeedsGrayedPlantWarning = false;
					this.mApp.DoDialog(16, true, "[DIALOG_WARNING]", "[NOT_RECOMMENDED_FOR_LEVEL]", "[DIALOG_BUTTON_OK]", 3);
					return;
				}
			}
			theChosenSeed.mTimeStartMotion = this.mSeedChooserAge;
			theChosenSeed.mTimeEndMotion = this.mSeedChooserAge + 25;
			this.GetSeedPositionInChooser((int)theChosenSeed.mSeedType, ref theChosenSeed.mStartX, ref theChosenSeed.mStartY);
			if (theChosenSeed.mSeedType != SeedType.SEED_IMITATER)
			{
				theChosenSeed.mStartY += this.mSeedPacketsWidget.mY;
			}
			this.GetSeedPositionInBank(this.mSeedsInBank, ref theChosenSeed.mEndX, ref theChosenSeed.mEndY);
			theChosenSeed.mSeedState = ChosenSeedState.SEED_FLYING_TO_BANK;
			theChosenSeed.mSeedIndexInBank = this.mSeedsInBank;
			this.mSeedsInFlight++;
			this.mSeedsInBank++;
			this.mApp.PlaySample(Resources.SOUND_TAP);
			if (this.mSeedsInBank == this.mBoard.mSeedBank.mNumPackets)
			{
				this.EnableStartButton(true);
			}
		}

		public void ClickedSeedInBank(ref ChosenSeed theChosenSeed)
		{
			if (theChosenSeed.mSeedState == ChosenSeedState.SEED_IN_BANK && theChosenSeed.mCrazyDavePicked)
			{
				this.mApp.PlaySample(Resources.SOUND_BUZZER);
				return;
			}
			for (int i = theChosenSeed.mSeedIndexInBank + 1; i < this.mBoard.mSeedBank.mNumPackets; i++)
			{
				SeedType seedType = this.FindSeedInBank(i);
				if (seedType != SeedType.SEED_NONE)
				{
					ChosenSeed chosenSeed = this.mChosenSeeds[(int)seedType];
					chosenSeed.mTimeStartMotion = this.mSeedChooserAge;
					chosenSeed.mTimeEndMotion = this.mSeedChooserAge + 15;
					chosenSeed.mStartX = chosenSeed.mX;
					chosenSeed.mStartY = chosenSeed.mY;
					this.GetSeedPositionInBank(i - 1, ref chosenSeed.mEndX, ref chosenSeed.mEndY);
					chosenSeed.mSeedState = ChosenSeedState.SEED_FLYING_TO_BANK;
					chosenSeed.mSeedIndexInBank = i - 1;
					this.mSeedsInFlight++;
				}
			}
			theChosenSeed.mTimeStartMotion = this.mSeedChooserAge;
			theChosenSeed.mTimeEndMotion = this.mSeedChooserAge + 25;
			theChosenSeed.mStartX = theChosenSeed.mX;
			theChosenSeed.mStartY = theChosenSeed.mY;
			this.GetSeedPositionInChooser((int)theChosenSeed.mSeedType, ref theChosenSeed.mEndX, ref theChosenSeed.mEndY);
			if (theChosenSeed.mSeedType != SeedType.SEED_IMITATER)
			{
				theChosenSeed.mEndY += this.mSeedPacketsWidget.mY;
			}
			theChosenSeed.mSeedState = ChosenSeedState.SEED_FLYING_TO_CHOOSER;
			theChosenSeed.mSeedIndexInBank = 0;
			this.mSeedsInFlight++;
			this.mSeedsInBank--;
			this.EnableStartButton(false);
			this.mApp.PlaySample(Resources.SOUND_TAP);
		}

		public SeedType FindSeedInBank(int theIndexInBank)
		{
			for (int i = 0; i < 49; i++)
			{
				SeedType seedType = (SeedType)i;
				if (this.mApp.HasSeedType(seedType))
				{
					ChosenSeed chosenSeed = this.mChosenSeeds[i];
					if (chosenSeed.mSeedState == ChosenSeedState.SEED_IN_BANK && chosenSeed.mSeedIndexInBank == theIndexInBank)
					{
						return seedType;
					}
				}
			}
			return SeedType.SEED_NONE;
		}

		public void EnableStartButton(bool theEnabled)
		{
			this.mStartButton.SetDisabled(!theEnabled);
			if (theEnabled)
			{
				this.mStartButton.mColors[0] = new SexyColor(255, 231, 26);
				return;
			}
			this.mStartButton.mColors[0] = new SexyColor(64, 64, 64);
		}

		public uint SeedNotRecommendedToPick(SeedType theSeedType)
		{
			uint num = this.mBoard.SeedNotRecommendedForLevel(theSeedType);
			if (TodCommon.TestBit(num, 0) && this.PickedPlantType(SeedType.SEED_INSTANT_COFFEE))
			{
				TodCommon.SetBit(ref num, 0, 0);
			}
			return num;
		}

		public bool SeedNotAllowedToPick(SeedType theSeedType)
		{
			return this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_LAST_STAND && (theSeedType == SeedType.SEED_SUNFLOWER || theSeedType == SeedType.SEED_SUNSHROOM || theSeedType == SeedType.SEED_TWINSUNFLOWER || theSeedType == SeedType.SEED_SEASHROOM || theSeedType == SeedType.SEED_PUFFSHROOM);
		}

		public void CloseSeedChooser()
		{
			Debug.ASSERT(this.mBoard.mSeedBank.mNumPackets == this.mBoard.GetNumSeedsInBank());
			for (int i = 0; i < this.mBoard.mSeedBank.mNumPackets; i++)
			{
				SeedType seedType = this.FindSeedInBank(i);
				ChosenSeed chosenSeed = this.mChosenSeeds[(int)seedType];
				SeedPacket seedPacket = this.mBoard.mSeedBank.mSeedPackets[i];
				seedPacket.SetPacketType(seedType, chosenSeed.mImitaterType);
				if (chosenSeed.mRefreshing)
				{
					seedPacket.mRefreshCounter = this.mChosenSeeds[(int)seedType].mRefreshCounter;
					seedPacket.mRefreshTime = Plant.GetRefreshTime(seedPacket.mPacketType, seedPacket.mImitaterType);
					seedPacket.mRefreshing = true;
					seedPacket.mActive = false;
				}
			}
			this.mBoard.mCutScene.EndSeedChooser();
		}

		public bool PickedPlantType(SeedType theSeedType)
		{
			for (int i = 0; i < 49; i++)
			{
				ChosenSeed chosenSeed = this.mChosenSeeds[i];
				if (chosenSeed.mSeedState == ChosenSeedState.SEED_IN_BANK)
				{
					if (chosenSeed.mSeedType == theSeedType)
					{
						return true;
					}
					if (chosenSeed.mSeedType == SeedType.SEED_IMITATER && chosenSeed.mImitaterType == theSeedType)
					{
						return true;
					}
				}
			}
			return false;
		}

		public void OnStartButton()
		{
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_SEEING_STARS && !this.PickedPlantType(SeedType.SEED_STARFRUIT) && !this.DisplayRepickWarningDialog(0, "[SEED_CHOOSER_SEEING_STARS_WARNING]"))
			{
				return;
			}
			if (this.mApp.IsFirstTimeAdventureMode() && this.mBoard.mLevel == 11 && !this.PickedPlantType(SeedType.SEED_PUFFSHROOM) && !this.DisplayRepickWarningDialog(1, "[SEED_CHOOSER_PUFFSHROOM_WARNING]"))
			{
				return;
			}
			if (!this.PickedPlantType(SeedType.SEED_SUNFLOWER) && !this.PickedPlantType(SeedType.SEED_TWINSUNFLOWER) && !this.PickedPlantType(SeedType.SEED_SUNSHROOM) && !this.mBoard.mCutScene.IsSurvivalRepick() && this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_LAST_STAND)
			{
				if (this.mApp.IsFirstTimeAdventureMode() && this.mBoard.mLevel == 11)
				{
					if (!this.DisplayRepickWarningDialog(2, "[SEED_CHOOSER_NIGHT_SUN_WARNING]"))
					{
						return;
					}
				}
				else if (!this.DisplayRepickWarningDialog(3, "[SEED_CHOOSER_SUN_WARNING]"))
				{
					return;
				}
			}
			if (this.mBoard.StageHasPool() && !this.PickedPlantType(SeedType.SEED_LILYPAD) && !this.PickedPlantType(SeedType.SEED_SEASHROOM) && !this.PickedPlantType(SeedType.SEED_TANGLEKELP) && !this.mBoard.mCutScene.IsSurvivalRepick())
			{
				if (this.mApp.IsFirstTimeAdventureMode() && this.mBoard.mLevel == 21)
				{
					if (!this.DisplayRepickWarningDialog(4, "[SEED_CHOOSER_LILY_WARNING]"))
					{
						return;
					}
				}
				else if (!this.DisplayRepickWarningDialog(5, "[SEED_CHOOSER_POOL_WARNING]"))
				{
					return;
				}
			}
			if (this.mBoard.StageHasRoof() && !this.PickedPlantType(SeedType.SEED_FLOWERPOT) && this.mApp.HasSeedType(SeedType.SEED_FLOWERPOT) && !this.DisplayRepickWarningDialog(6, "[SEED_CHOOSER_ROOF_WARNING]"))
			{
				return;
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ART_CHALLENGE_1 && !this.PickedPlantType(SeedType.SEED_WALLNUT) && !this.DisplayRepickWarningDialog(7, "[SEED_CHOOSER_ART_WALLNUT_WARNING]"))
			{
				return;
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ART_CHALLENGE_2 && (!this.PickedPlantType(SeedType.SEED_STARFRUIT) || !this.PickedPlantType(SeedType.SEED_UMBRELLA) || !this.PickedPlantType(SeedType.SEED_WALLNUT)) && !this.DisplayRepickWarningDialog(8, "[SEED_CHOOSER_ART_2_WARNING]"))
			{
				return;
			}
			if (this.FlyersAreComing() && !this.FlyProtectionCurrentlyPlanted() && !this.PickedPlantType(SeedType.SEED_CATTAIL) && !this.PickedPlantType(SeedType.SEED_CACTUS) && !this.PickedPlantType(SeedType.SEED_BLOVER) && !this.DisplayRepickWarningDialog(9, "[SEED_CHOOSER_FLYER_WARNING]"))
			{
				return;
			}
			if (!this.CheckSeedUpgrade(10, SeedType.SEED_GATLINGPEA, SeedType.SEED_REPEATER) || !this.CheckSeedUpgrade(11, SeedType.SEED_WINTERMELON, SeedType.SEED_MELONPULT) || !this.CheckSeedUpgrade(12, SeedType.SEED_TWINSUNFLOWER, SeedType.SEED_SUNFLOWER) || !this.CheckSeedUpgrade(13, SeedType.SEED_SPIKEROCK, SeedType.SEED_SPIKEWEED) || !this.CheckSeedUpgrade(14, SeedType.SEED_COBCANNON, SeedType.SEED_KERNELPULT) || !this.CheckSeedUpgrade(15, SeedType.SEED_GOLD_MAGNET, SeedType.SEED_MAGNETSHROOM) || !this.CheckSeedUpgrade(16, SeedType.SEED_GLOOMSHROOM, SeedType.SEED_FUMESHROOM) || !this.CheckSeedUpgrade(17, SeedType.SEED_CATTAIL, SeedType.SEED_LILYPAD))
			{
				return;
			}
			this.CloseSeedChooser();
		}

		public bool DisplayRepickWarningDialog(int theWarningId, string theMessage)
		{
			if (this.mPickWarningsWaved[theWarningId])
			{
				return true;
			}
			this.mPendingWarningId = theWarningId;
			this.mApp.LawnMessageBox(28, "[DIALOG_WARNING]", theMessage, "[DIALOG_BUTTON_YES]", "[REPICK_BUTTON]", 1, this);
			return false;
		}

		public void UpdateViewLawn()
		{
			if (this.mChooseState != SeedChooserState.CHOOSE_VIEW_LAWN)
			{
				return;
			}
			this.mViewLawnTime++;
			if (this.mViewLawnTime == 100)
			{
				this.mBoard.DisplayAdviceAgain("[CLICK_TO_CONTINUE]", MessageStyle.MESSAGE_STYLE_HINT_STAY, AdviceType.ADVICE_CLICK_TO_CONTINUE);
			}
			else if (this.mViewLawnTime == 251)
			{
				this.mViewLawnTime = 250;
			}
			int num = 800;
			if (this.mViewLawnTime <= 100)
			{
				int thePositionStart = -Constants.BOARD_OFFSET + Constants.BACKGROUND_IMAGE_WIDTH - num;
				int thePositionEnd = 0;
				int num2 = TodCommon.TodAnimateCurve(0, 100, this.mViewLawnTime, thePositionStart, thePositionEnd, TodCurves.CURVE_EASE_IN_OUT);
				this.mBoard.Move((int)((float)(-(float)num2) * Constants.S), 0);
				int thePositionStart2 = Constants.SEED_CHOOSER_OFFSETSCREEN_OFFSET - 265;
				int seed_CHOOSER_OFFSETSCREEN_OFFSET = Constants.SEED_CHOOSER_OFFSETSCREEN_OFFSET;
				int num3 = TodCommon.TodAnimateCurve(0, 40, this.mViewLawnTime, thePositionStart2, seed_CHOOSER_OFFSETSCREEN_OFFSET, TodCurves.CURVE_EASE_IN_OUT);
				this.Move(0, (int)((float)num3 * Constants.S));
				return;
			}
			if (this.mViewLawnTime > 100 && this.mViewLawnTime <= 250)
			{
				this.mBoard.Move(0, 0);
				this.Move(0, Constants.SEED_CHOOSER_OFFSETSCREEN_OFFSET);
				return;
			}
			if (this.mViewLawnTime > 250 && this.mViewLawnTime <= 350)
			{
				this.mBoard.ClearAdvice(AdviceType.ADVICE_CLICK_TO_CONTINUE);
				int thePositionStart3 = 0;
				int thePositionEnd2 = -Constants.BOARD_OFFSET + Constants.BACKGROUND_IMAGE_WIDTH - num;
				int num4 = TodCommon.TodAnimateCurve(250, 350, this.mViewLawnTime, thePositionStart3, thePositionEnd2, TodCurves.CURVE_EASE_IN_OUT);
				this.mBoard.Move((int)((float)(-(float)num4) * Constants.S), 0);
				int seed_CHOOSER_OFFSETSCREEN_OFFSET2 = Constants.SEED_CHOOSER_OFFSETSCREEN_OFFSET;
				int thePositionEnd3 = Constants.SEED_CHOOSER_OFFSETSCREEN_OFFSET - 265;
				int num5 = TodCommon.TodAnimateCurve(310, 350, this.mViewLawnTime, seed_CHOOSER_OFFSETSCREEN_OFFSET2, thePositionEnd3, TodCurves.CURVE_EASE_IN_OUT);
				this.Move(0, (int)((float)num5 * Constants.S));
				return;
			}
			this.mChooseState = SeedChooserState.CHOOSE_NORMAL;
			this.mViewLawnTime = 0;
			this.mMenuButton.mDisabled = false;
		}

		public void CancelLawnView()
		{
			if (this.mChooseState == SeedChooserState.CHOOSE_VIEW_LAWN && this.mViewLawnTime > 100 && this.mViewLawnTime <= 250)
			{
				this.mViewLawnTime = 251;
			}
		}

		public void LandFlyingSeed(ref ChosenSeed theChosenSeed)
		{
			if (theChosenSeed.mSeedState == ChosenSeedState.SEED_FLYING_TO_BANK)
			{
				theChosenSeed.mTimeStartMotion = 0;
				theChosenSeed.mTimeEndMotion = 0;
				theChosenSeed.mSeedState = ChosenSeedState.SEED_IN_BANK;
				theChosenSeed.mX = theChosenSeed.mEndX;
				theChosenSeed.mY = theChosenSeed.mEndY;
				this.mSeedsInFlight--;
				return;
			}
			if (theChosenSeed.mSeedState == ChosenSeedState.SEED_FLYING_TO_CHOOSER)
			{
				theChosenSeed.mTimeStartMotion = 0;
				theChosenSeed.mTimeEndMotion = 0;
				theChosenSeed.mSeedState = ChosenSeedState.SEED_IN_CHOOSER;
				theChosenSeed.mX = theChosenSeed.mEndX;
				theChosenSeed.mY = theChosenSeed.mEndY;
				this.mSeedsInFlight--;
				if (theChosenSeed.mSeedType == SeedType.SEED_IMITATER)
				{
					theChosenSeed.mSeedState = ChosenSeedState.SEED_PACKET_HIDDEN;
					theChosenSeed.mImitaterType = SeedType.SEED_NONE;
					this.UpdateImitaterButton();
				}
			}
		}

		public bool FlyProtectionCurrentlyPlanted()
		{
			int count = this.mBoard.mPlants.Count;
			for (int i = 0; i < count; i++)
			{
				Plant plant = this.mBoard.mPlants[i];
				if (!plant.mDead && (plant.mSeedType == SeedType.SEED_CATTAIL || plant.mSeedType == SeedType.SEED_CACTUS))
				{
					return true;
				}
			}
			return false;
		}

		public bool FlyersAreComing()
		{
			for (int i = 0; i < this.mBoard.mNumWaves; i++)
			{
				for (int j = 0; j < 50; j++)
				{
					ZombieType zombieType = this.mBoard.mZombiesInWave[i, j];
					if (zombieType == ZombieType.ZOMBIE_INVALID)
					{
						break;
					}
					if (zombieType == ZombieType.ZOMBIE_BALLOON)
					{
						return true;
					}
				}
			}
			return false;
		}

		public void UpdateAfterPurchase()
		{
			for (int i = 0; i < 49; i++)
			{
				ChosenSeed chosenSeed = this.mChosenSeeds[i];
				if (chosenSeed.mSeedState == ChosenSeedState.SEED_IN_BANK)
				{
					this.GetSeedPositionInBank(chosenSeed.mSeedIndexInBank, ref chosenSeed.mX, ref chosenSeed.mY);
					chosenSeed.mStartX = chosenSeed.mX;
					chosenSeed.mStartY = chosenSeed.mY;
					chosenSeed.mEndX = chosenSeed.mX;
					chosenSeed.mEndY = chosenSeed.mY;
				}
				else if (chosenSeed.mSeedState == ChosenSeedState.SEED_IN_CHOOSER)
				{
					this.GetSeedPositionInChooser(i, ref chosenSeed.mX, ref chosenSeed.mY);
					chosenSeed.mStartX = chosenSeed.mX;
					chosenSeed.mStartY = chosenSeed.mY;
					chosenSeed.mEndX = chosenSeed.mX;
					chosenSeed.mEndY = chosenSeed.mY;
				}
			}
			if (this.mSeedsInBank == this.mBoard.mSeedBank.mNumPackets)
			{
				this.EnableStartButton(true);
			}
			else
			{
				this.EnableStartButton(false);
			}
			this.UpdateImitaterButton();
		}

		public void UpdateImitaterButton()
		{
			if (!this.mApp.HasSeedType(SeedType.SEED_IMITATER))
			{
				this.mImitaterButton.mBtnNoDraw = true;
				this.mImitaterButton.mDisabled = true;
				return;
			}
			this.mImitaterButton.mBtnNoDraw = false;
			bool disabled = false;
			ChosenSeed chosenSeed = this.mChosenSeeds[48];
			if (chosenSeed.mSeedState != ChosenSeedState.SEED_PACKET_HIDDEN)
			{
				disabled = true;
			}
			this.mImitaterButton.SetDisabled(disabled);
		}

		public void CrazyDavePickSeeds()
		{
			for (int i = 0; i < SeedChooserScreen.aSeedArray.Length; i++)
			{
				SeedChooserScreen.aSeedArray[i].Reset();
			}
			for (int j = 0; j < 49; j++)
			{
				SeedType seedType = (SeedType)j;
				SeedChooserScreen.aSeedArray[j].mItem = (int)seedType;
				uint num = this.SeedNotRecommendedToPick(seedType);
				if (!this.mApp.HasSeedType(seedType) || num != 0U || this.SeedNotAllowedToPick(seedType) || Plant.IsUpgrade(seedType) || seedType == SeedType.SEED_IMITATER || seedType == SeedType.SEED_UMBRELLA || seedType == SeedType.SEED_BLOVER)
				{
					SeedChooserScreen.aSeedArray[j].mWeight = 0;
				}
				else
				{
					SeedChooserScreen.aSeedArray[j].mWeight = 1;
				}
			}
			if (this.mBoard.mZombieAllowed[22] || this.mBoard.mZombieAllowed[20])
			{
				SeedChooserScreen.aSeedArray[37].mWeight = 1;
			}
			if (this.mBoard.mZombieAllowed[16] || this.mBoard.StageHasFog())
			{
				SeedChooserScreen.aSeedArray[27].mWeight = 1;
			}
			if (this.mBoard.StageHasRoof())
			{
				SeedChooserScreen.aSeedArray[22].mWeight = 0;
			}
			int levelRandSeed = this.mBoard.GetLevelRandSeed();
			RandomNumbers.Seed(levelRandSeed);
			for (int k = 0; k < 3; k++)
			{
				SeedType seedType2 = (SeedType)this.PickFromWeightedArrayUsingSpecialRandSeed(SeedChooserScreen.aSeedArray, 49);
				SeedChooserScreen.aSeedArray[(int)seedType2].mWeight = 0;
				ChosenSeed chosenSeed = this.mChosenSeeds[(int)seedType2];
				chosenSeed.mY = this.mBoard.GetSeedPacketPositionY(k);
				chosenSeed.mX = 0;
				chosenSeed.mEndX = chosenSeed.mX;
				chosenSeed.mEndY = chosenSeed.mY;
				chosenSeed.mStartX = chosenSeed.mX;
				chosenSeed.mStartY = chosenSeed.mY;
				chosenSeed.mSeedState = ChosenSeedState.SEED_IN_BANK;
				chosenSeed.mSeedIndexInBank = k;
				chosenSeed.mCrazyDavePicked = true;
				this.mSeedsInBank++;
			}
		}

		public int PickFromWeightedArrayUsingSpecialRandSeed(TodWeightedArray[] theArray, int theCount)
		{
			int num = 0;
			for (int i = 0; i < theCount; i++)
			{
				num += theArray[i].mWeight;
			}
			Debug.ASSERT(num > 0);
			int num2 = (int)RandomNumbers.NextNumber(num);
			int num3 = 0;
			for (int j = 0; j < theCount; j++)
			{
				num3 += theArray[j].mWeight;
				if (num2 < num3)
				{
					return (int)theArray[j].mItem;
				}
			}
			Debug.ASSERT(false);
			return -666;
		}

		public bool CheckSeedUpgrade(int theWarningId, SeedType theSeedTypeTo, SeedType theSeedTypeFrom)
		{
			if (this.mApp.IsSurvivalMode() || this.mPickWarningsWaved[theWarningId])
			{
				return true;
			}
			if (this.PickedPlantType(theSeedTypeTo) && !this.PickedPlantType(theSeedTypeFrom))
			{
				string text = TodStringFile.TodStringTranslate("[SEED_CHOOSER_UPGRADE_WARNING]");
				string nameString = Plant.GetNameString(theSeedTypeTo, SeedType.SEED_NONE);
				string nameString2 = Plant.GetNameString(theSeedTypeFrom, SeedType.SEED_NONE);
				text = TodCommon.TodReplaceString(text, "{UPGRADE_TO}", nameString);
				text = TodCommon.TodReplaceString(text, "{UPGRADE_FROM}", nameString2);
				if (!this.DisplayRepickWarningDialog(theWarningId, text))
				{
					return false;
				}
			}
			return true;
		}

		public void PickRandomSeeds()
		{
			for (int i = this.mSeedsInBank; i < this.mBoard.mSeedBank.mNumPackets; i++)
			{
				int num;
				SeedType seedType;
				do
				{
					num = RandomNumbers.NextNumber(this.mApp.GetSeedsAvailable());
					seedType = (SeedType)num;
				}
				while (!this.mApp.HasSeedType(seedType) || seedType == SeedType.SEED_IMITATER || this.mChosenSeeds[num].mSeedState != ChosenSeedState.SEED_IN_CHOOSER);
				ChosenSeed chosenSeed = this.mChosenSeeds[num];
				chosenSeed.mTimeStartMotion = 0;
				chosenSeed.mTimeEndMotion = 0;
				chosenSeed.mStartX = chosenSeed.mX;
				chosenSeed.mStartY = chosenSeed.mY;
				this.GetSeedPositionInBank(i, ref chosenSeed.mEndX, ref chosenSeed.mEndY);
				chosenSeed.mSeedState = ChosenSeedState.SEED_IN_BANK;
				chosenSeed.mSeedIndexInBank = i;
				this.mSeedsInBank++;
			}
			for (int j = 0; j < 49; j++)
			{
				ChosenSeed chosenSeed2 = this.mChosenSeeds[j];
				this.LandFlyingSeed(ref chosenSeed2);
			}
			this.CloseSeedChooser();
		}

		public bool Has12Rows()
		{
			return this.mApp.HasFinishedAdventure() || (this.mApp.HasSeedType(SeedType.SEED_GATLINGPEA) || this.mApp.HasSeedType(SeedType.SEED_TWINSUNFLOWER) || this.mApp.HasSeedType(SeedType.SEED_GLOOMSHROOM) || this.mApp.HasSeedType(SeedType.SEED_CATTAIL) || this.mApp.HasSeedType(SeedType.SEED_WINTERMELON) || this.mApp.HasSeedType(SeedType.SEED_GATLINGPEA) || this.mApp.HasSeedType(SeedType.SEED_GOLD_MAGNET) || this.mApp.HasSeedType(SeedType.SEED_COBCANNON));
		}

		public bool SeedNotAllowedDuringTrial(SeedType theSeedType)
		{
			return this.mApp.IsTrialStageLocked() && (theSeedType == SeedType.SEED_SQUASH || theSeedType == SeedType.SEED_THREEPEATER);
		}

		public void BackFromStore()
		{
			StoreScreen storeScreen = (StoreScreen)this.mApp.GetDialog(Dialogs.DIALOG_STORE);
			this.mApp.KillDialog(4);
			this.mApp.mMusic.MakeSureMusicIsPlaying(MusicTune.MUSIC_TUNE_CHOOSE_YOUR_SEEDS);
		}

		public void BackFromAlmanac()
		{
			this.mApp.mMusic.MakeSureMusicIsPlaying(MusicTune.MUSIC_TUNE_CHOOSE_YOUR_SEEDS);
		}

		public void LawnMessageBoxDone(int theResult)
		{
			if (theResult == 1000)
			{
				this.mPickWarningsWaved[this.mPendingWarningId] = true;
				this.mDoStartButton = true;
			}
		}

		public void SeedSelected(SeedType theSeedType)
		{
			if (theSeedType == SeedType.SEED_NONE)
			{
				return;
			}
			if (this.SeedNotAllowedToPick(theSeedType))
			{
				this.mApp.DoDialog(16, true, string.Empty, "[NOT_ALLOWED_ON_THIS_LEVEL]", "[DIALOG_BUTTON_OK]", 3);
				return;
			}
			if (theSeedType >= SeedType.SEED_PEASHOOTER && theSeedType < (SeedType)this.mChosenSeeds.Length)
			{
				ChosenSeed chosenSeed = this.mChosenSeeds[(int)theSeedType];
				if (chosenSeed.mSeedState == ChosenSeedState.SEED_IN_CHOOSER)
				{
					this.ClickedSeedInChooser(ref chosenSeed);
				}
			}
		}

		public GameButton mStartButton;

		public GameButton mRandomButton;

		public GameButton mViewLawnButton;

		public GameButton mStoreButton;

		public GameButton mAlmanacButton;

		public GameButton mMenuButton;

		public GameButton mImitaterButton;

		public SeedPacketsWidget mSeedPacketsWidget;

		public ScrollWidget mScrollWidget;

		public ChosenSeed[] mChosenSeeds = new ChosenSeed[53];

		public LawnApp mApp;

		public Board mBoard;

		public int mNumSeedsToChoose;

		public int mSeedChooserAge;

		public int mSeedsInFlight;

		public int mSeedsInBank;

		public int mLastMouseX;

		public int mLastMouseY;

		public SeedChooserState mChooseState;

		public int mViewLawnTime;

		public bool mDoStartButton;

		public bool[] mPickWarningsWaved = new bool[20];

		public int mPendingWarningId;

		private static TodWeightedArray[] aSeedArray = new TodWeightedArray[53];
	}
}
