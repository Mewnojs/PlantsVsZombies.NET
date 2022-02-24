using System;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
    public/*internal*/ class SeedChooserScreen : Widget, StoreListener, AlmanacListener, LawnMessageBoxListener, SeedPacketsWidgetListener
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
            mApp = (LawnApp)GlobalStaticVars.gSexyAppBase;
            mBoard = mApp.mBoard;
            mClip = false;
            mSeedsInFlight = 0;
            mSeedsInBank = 0;
            mLastMouseX = -1;
            mLastMouseY = -1;
            mChooseState = SeedChooserState.Normal;
            mViewLawnTime = 0;
            mDoStartButton = false;
            mSeedPacketsWidget = new SeedPacketsWidget(mApp, Has12Rows() ? 12 : 11, false, this);
            mScrollWidget = new ScrollWidget();
            mScrollWidget.Resize(Constants.SCROLL_AREA_OFFSET_X, Constants.SCROLL_AREA_OFFSET_Y, mSeedPacketsWidget.mWidth + (int)Constants.InvertAndScale(10f), (int)Constants.InvertAndScale(227f));
            mScrollWidget.AddWidget(mSeedPacketsWidget);
            mScrollWidget.EnableIndicators(AtlasResources.IMAGE_SCROLL_INDICATOR);
            mSeedPacketsWidget.Move(0, 0);
            AddWidget(mScrollWidget);
            mStartButton = new GameButton(100, this);
            mStartButton.SetLabel("[PLAY_BUTTON]");
            mStartButton.mButtonImage = AtlasResources.IMAGE_SEEDCHOOSER_SMALL_BUTTON;
            mStartButton.mOverImage = null;
            mStartButton.mDownImage = AtlasResources.IMAGE_SEEDCHOOSER_SMALL_BUTTON_PRESSED;
            mStartButton.mDisabledImage = AtlasResources.IMAGE_SEEDCHOOSER_SMALL_BUTTON_DISABLED;
            mStartButton.mOverOverlayImage = null;
            mStartButton.SetFont(Resources.FONT_DWARVENTODCRAFT15);
            mStartButton.mColors[1] = new SexyColor(255, 231, 26);
            mStartButton.mColors[0] = new SexyColor(255, 231, 26);
            mStartButton.Resize((int)Constants.InvertAndScale(147f), (int)Constants.InvertAndScale(280f), (int)Constants.InvertAndScale(67f), mStartButton.mButtonImage.GetHeight());
            mStartButton.mTextOffsetX = 0;
            mStartButton.mTextOffsetX = 0;
            mStartButton.mTextPushOffsetX = 1;
            mStartButton.mTextPushOffsetY = 3;
            EnableStartButton(false);
            mMenuButton = new GameButton(104, this);
            mMenuButton.SetLabel("[MENU_BUTTON]");
            mMenuButton.Resize(Constants.UIMenuButtonPosition.X + Constants.Board_Offset_AspectRatio_Correction, Constants.UIMenuButtonPosition.Y, Constants.UIMenuButtonWidth, AtlasResources.IMAGE_BUTTON_LEFT.mHeight);
            mMenuButton.mDrawStoneButton = true;
            mRandomButton = new GameButton(101, this);
            mRandomButton.SetLabel("(Debug Play)");
            mRandomButton.mButtonImage = AtlasResources.IMAGE_BLANK;
            mRandomButton.mOverImage = AtlasResources.IMAGE_BLANK;
            mRandomButton.mDownImage = AtlasResources.IMAGE_BLANK;
            mRandomButton.SetFont(Resources.FONT_BRIANNETOD12);
            mRandomButton.mColors[0] = new SexyColor(255, 240, 0);
            mRandomButton.mColors[1] = new SexyColor(200, 200, 255);
            mRandomButton.Resize((int)Constants.InvertAndScale(332f), (int)Constants.InvertAndScale(546f), (int)Constants.InvertAndScale(100f), (int)Constants.InvertAndScale(30f));
            if (!mApp.mTodCheatKeys)
            {
                mRandomButton.mBtnNoDraw = true;
                mRandomButton.mDisabled = true;
            }
            mViewLawnButton = new GameButton(102, this);
            mViewLawnButton.SetLabel("[VIEW_LAWN]");
            mViewLawnButton.mButtonImage = AtlasResources.IMAGE_SEEDCHOOSER_BUTTON2;
            mViewLawnButton.mOverImage = AtlasResources.IMAGE_SEEDCHOOSER_BUTTON2_GLOW;
            mViewLawnButton.mDownImage = null;
            mViewLawnButton.SetFont(Resources.FONT_BRIANNETOD12);
            mViewLawnButton.mColors[0] = new SexyColor(42, 42, 90);
            mViewLawnButton.mColors[1] = new SexyColor(42, 42, 90);
            mViewLawnButton.Resize((int)Constants.InvertAndScale(22f), (int)Constants.InvertAndScale(300f), (int)Constants.InvertAndScale(111f), (int)Constants.InvertAndScale(26f));
            mViewLawnButton.mParentWidget = this;
            mViewLawnButton.mTextOffsetY = 1;
            if (!mBoard.mCutScene.IsSurvivalRepick())
            {
                mViewLawnButton.mBtnNoDraw = true;
                mViewLawnButton.mDisabled = true;
            }
            mAlmanacButton = new GameButton(103, this);
            mAlmanacButton.SetLabel("[ALMANAC_BUTTON]");
            mAlmanacButton.mButtonImage = AtlasResources.IMAGE_SEEDCHOOSER_BUTTON2;
            mAlmanacButton.mOverImage = AtlasResources.IMAGE_SEEDCHOOSER_BUTTON2_GLOW;
            mAlmanacButton.mDownImage = null;
            mAlmanacButton.SetFont(Resources.FONT_BRIANNETOD12);
            mAlmanacButton.mColors[0] = new SexyColor(42, 42, 90);
            mAlmanacButton.mColors[1] = new SexyColor(42, 42, 90);
            mAlmanacButton.Resize((int)Constants.InvertAndScale(63f), (int)Constants.InvertAndScale(286f), AtlasResources.IMAGE_SEEDCHOOSER_BUTTON2.mWidth, (int)Constants.InvertAndScale(26f));
            mAlmanacButton.mParentWidget = this;
            mAlmanacButton.mTextOffsetY = (int)Constants.InvertAndScale(2f);
            mStoreButton = new GameButton(105, this);
            mStoreButton.SetLabel("[SHOP_BUTTON]");
            mStoreButton.mButtonImage = AtlasResources.IMAGE_SEEDCHOOSER_BUTTON2;
            mStoreButton.mOverImage = AtlasResources.IMAGE_SEEDCHOOSER_BUTTON2_GLOW;
            mStoreButton.mDownImage = null;
            mStoreButton.SetFont(Resources.FONT_BRIANNETOD12);
            mStoreButton.mColors[0] = new SexyColor(42, 42, 90);
            mStoreButton.mColors[1] = new SexyColor(42, 42, 90);
            mStoreButton.Resize((int)Constants.InvertAndScale(218f), (int)Constants.InvertAndScale(286f), AtlasResources.IMAGE_SEEDCHOOSER_BUTTON2.mWidth, (int)Constants.InvertAndScale(26f));
            mStoreButton.mParentWidget = this;
            mStoreButton.mTextOffsetY = (int)Constants.InvertAndScale(2f);
            mImitaterButton = new GameButton(106, this);
            mImitaterButton.mButtonImage = null;
            mImitaterButton.mOverImage = null;
            mImitaterButton.mDownImage = null;
            mImitaterButton.mDisabledImage = null;
            mImitaterButton.Resize((int)Constants.InvertAndScale(310f), (int)Constants.InvertAndScale(27f), Constants.SMALL_SEEDPACKET_WIDTH, Constants.SMALL_SEEDPACKET_HEIGHT);
            mImitaterButton.mParentWidget = this;
            if (!mApp.CanShowAlmanac())
            {
                mAlmanacButton.mBtnNoDraw = true;
                mAlmanacButton.mDisabled = true;
            }
            if (!mApp.CanShowStore())
            {
                mStoreButton.mBtnNoDraw = true;
                mStoreButton.mDisabled = true;
            }
            for (SeedType i = 0; i < SeedType.SeedsInChooserCount; i++)
            {
                ChosenSeed chosenSeed = new ChosenSeed();
                chosenSeed.mSeedType = i;
                GetSeedPositionInChooser((int)i, ref chosenSeed.mX, ref chosenSeed.mY);
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
                chosenSeed.mImitaterType = SeedType.None;
                chosenSeed.mCrazyDavePicked = false;
                if (i == SeedType.Imitater)
                {
                    chosenSeed.mSeedState = ChosenSeedState.SEED_PACKET_HIDDEN;
                }
                mChosenSeeds[(int)i] = chosenSeed;
            }
            if (mBoard.mCutScene.IsSurvivalRepick())
            {
                for (int j = 0; j < mBoard.mSeedBank.mNumPackets; j++)
                {
                    SeedPacket seedPacket = mBoard.mSeedBank.mSeedPackets[j];
                    SeedType packetType = seedPacket.mPacketType;
                    ChosenSeed chosenSeed2 = mChosenSeeds[(int)packetType];
                    chosenSeed2.mRefreshing = seedPacket.mRefreshing;
                    chosenSeed2.mRefreshCounter = seedPacket.mRefreshCounter;
                }
                mBoard.mSeedBank.mNumPackets = 0;
            }
            if (mApp.mGameMode == GameMode.ChallengeSeeingStars)
            {
                ChosenSeed chosenSeed3 = mChosenSeeds[29];
                chosenSeed3.mY = mBoard.GetSeedPacketPositionY(0);
                chosenSeed3.mX = 0;
                chosenSeed3.mEndX = chosenSeed3.mX;
                chosenSeed3.mEndY = chosenSeed3.mY;
                chosenSeed3.mStartX = chosenSeed3.mX;
                chosenSeed3.mStartY = chosenSeed3.mY;
                chosenSeed3.mSeedState = ChosenSeedState.SEED_IN_BANK;
                chosenSeed3.mSeedIndexInBank = 0;
                mSeedsInBank++;
            }
            if (mApp.IsAdventureMode() && !mApp.IsFirstTimeAdventureMode())
            {
                CrazyDavePickSeeds();
            }
            UpdateImitaterButton();
            for (int k = 0; k < mPickWarningsWaved.Length; k++)
            {
                mPickWarningsWaved[k] = false;
            }
        }

        public override void Dispose()
        {
            mStartButton.Dispose();
            mRandomButton.Dispose();
            mViewLawnButton.Dispose();
            mAlmanacButton.Dispose();
            mImitaterButton.Dispose();
            mStoreButton.Dispose();
            mMenuButton.Dispose();
            RemoveAllWidgets(true);
        }

        public override void Update()
        {
            base.Update();
            mLastMouseX = mApp.mWidgetManager.mLastMouseX;
            mLastMouseY = mApp.mWidgetManager.mLastMouseY;
            mSeedChooserAge += 3;
            for (SeedType i = 0; i < SeedType.SeedsInChooserCount; i++)
            {
                if (mApp.HasSeedType(i))
                {
                    ChosenSeed chosenSeed = mChosenSeeds[(int)i];
                    if (chosenSeed.mSeedState == ChosenSeedState.SEED_FLYING_TO_BANK || chosenSeed.mSeedState == ChosenSeedState.SEED_FLYING_TO_CHOOSER)
                    {
                        chosenSeed.mX = TodCommon.TodAnimateCurve(chosenSeed.mTimeStartMotion, chosenSeed.mTimeEndMotion, mSeedChooserAge, chosenSeed.mStartX, chosenSeed.mEndX, TodCurves.EaseInOut);
                        chosenSeed.mY = TodCommon.TodAnimateCurve(chosenSeed.mTimeStartMotion, chosenSeed.mTimeEndMotion, mSeedChooserAge, chosenSeed.mStartY, chosenSeed.mEndY, TodCurves.EaseInOut);
                    }
                    if (chosenSeed.mSeedState == ChosenSeedState.SEED_FLYING_TO_BANK && mSeedChooserAge >= chosenSeed.mTimeEndMotion)
                    {
                        LandFlyingSeed(ref chosenSeed);
                    }
                    if (chosenSeed.mSeedState == ChosenSeedState.SEED_FLYING_TO_CHOOSER && mSeedChooserAge >= chosenSeed.mTimeEndMotion)
                    {
                        LandFlyingSeed(ref chosenSeed);
                    }
                }
            }
            mStartButton.Update();
            mRandomButton.Update();
            mViewLawnButton.Update();
            mAlmanacButton.Update();
            mImitaterButton.Update();
            mStoreButton.Update();
            mMenuButton.Update();
            UpdateViewLawn();
            if (mDoStartButton)
            {
                mDoStartButton = false;
                OnStartButton();
            }
            MarkDirty();
        }

        public override void Draw(Graphics g)
        {
            if (mApp.GetDialog(Dialogs.DIALOG_STORE) != null || mApp.GetDialog(Dialogs.DIALOG_ALMANAC) != null)
            {
                return;
            }
            g.SetLinearBlend(true);
            if (!mBoard.ChooseSeedsOnCurrentLevel())
            {
                return;
            }
            if (mBoard.mCutScene != null && mBoard.mCutScene.IsBeforePreloading())
            {
                return;
            }
            g.DrawImage(AtlasResources.IMAGE_SEEDCHOOSER_BACKGROUND_TOP, Constants.SeedChooserScreen_Background_Top.X, Constants.SeedChooserScreen_Background_Top.Y);
            g.DrawImage(AtlasResources.IMAGE_SEEDCHOOSER_BACKGROUND_MIDDLE, Constants.SeedChooserScreen_Background_Middle.X, Constants.SeedChooserScreen_Background_Middle.Y, AtlasResources.IMAGE_SEEDCHOOSER_BACKGROUND_MIDDLE.mWidth, Constants.SeedChooserScreen_Background_Middle_Height);
            g.DrawImage(AtlasResources.IMAGE_SEEDCHOOSER_BACKGROUND_BOTTOM, Constants.SeedChooserScreen_Background_Bottom.X, Constants.SeedChooserScreen_Background_Bottom.Y);
            if (mApp.HasSeedType(SeedType.Imitater))
            {
                g.DrawImage(AtlasResources.IMAGE_SEEDCHOOSER_IMITATERADDON, (int)Constants.InvertAndScale(304f), (int)Constants.InvertAndScale(18f));
                if (!mImitaterButton.mDisabled)
                {
                    g.DrawImageCel(AtlasResources.IMAGE_SEEDPACKETS, mImitaterButton.mX, mImitaterButton.mY, 48);
                }
            }
            int numPackets = mBoard.mSeedBank.mNumPackets;
            for (int i = 0; i < numPackets; i++)
            {
                SeedType seedType = FindSeedInBank(i);
                int num = 0;
                int num2 = 0;
                GetSeedPositionInBank(i, ref num, ref num2);
                if (seedType != SeedType.None)
                {
                    ChosenSeed chosenSeed = mChosenSeeds[(int)seedType];
                    SeedPacket.DrawSmallSeedPacket(g, num, num2, seedType, chosenSeed.mImitaterType, 0f, 255, true, false, true, true);
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
            mStartButton.Draw(g);
            mRandomButton.Draw(g);
            mViewLawnButton.Draw(g);
            mAlmanacButton.Draw(g);
            mStoreButton.Draw(g);
            Graphics @new = Graphics.GetNew(g);
            @new.mTransX -= mX;
            @new.mTransY -= mY;
            mMenuButton.Draw(@new);
            @new.PrepareForReuse();
        }

        public override void DrawOverlay(Graphics g)
        {
            g.SetColor(Constants.SeedChooserScreen_BackColour);
            g.SetColorizeImages(true);
            if (mSeedPacketsWidget.mY < 0)
            {
                g.DrawImage(AtlasResources.IMAGE_ALMANAC_PLANTS_TOPGRADIENT, mScrollWidget.mX + Constants.SeedChooserScreen_Gradient_Top.X, Constants.SeedChooserScreen_Gradient_Top.Y, Constants.SeedChooserScreen_Gradient_Top.Width, Constants.SeedChooserScreen_Gradient_Top.Height);
            }
            if (mSeedPacketsWidget.mY + mSeedPacketsWidget.mHeight > Constants.InvertAndScale(227f))
            {
                g.DrawImage(AtlasResources.IMAGE_ALMANAC_PLANTS_BOTTOMGRADIENT, mScrollWidget.mX + Constants.SeedChooserScreen_Gradient_Bottom.X, Constants.SeedChooserScreen_Gradient_Bottom.Y, Constants.SeedChooserScreen_Gradient_Bottom.Width, Constants.SeedChooserScreen_Gradient_Bottom.Height);
            }
            g.SetColorizeImages(false);
            bool flag = false;
            for (SeedType i = 0; i < SeedType.SeedsInChooserCount; i++)
            {
                if (mApp.HasSeedType(i))
                {
                    ChosenSeed chosenSeed = mChosenSeeds[(int)i];
                    if (chosenSeed.mSeedState == ChosenSeedState.SEED_FLYING_TO_BANK)
                    {
                        g.SetClipRect(0, 0, mWidth, mHeight);
                    }
                    else
                    {
                        if (chosenSeed.mSeedState != ChosenSeedState.SEED_FLYING_TO_CHOOSER)
                        {
                            goto IL_170;
                        }
                        g.SetClipRect(0, 0, mWidth, mScrollWidget.mY + mScrollWidget.mHeight);
                        g.HardwareClip();
                        flag = true;
                    }
                    SeedPacket.DrawSmallSeedPacket(g, chosenSeed.mX, chosenSeed.mY, chosenSeed.mSeedType, chosenSeed.mImitaterType, 0f, 255, true, false, true, true);
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
            int id = mMenuButton.mId;
            ButtonPress(id);
            ButtonDepress(id);
            return true;
        }

        public virtual void ButtonDepress(int theId)
        {
            if (mSeedsInFlight > 0 || mChooseState == SeedChooserState.ViewLawn)
            {
                return;
            }
            if (!mMouseVisible)
            {
                return;
            }
            if (theId == 102)
            {
                mChooseState = SeedChooserState.ViewLawn;
                mMenuButton.mDisabled = true;
                mViewLawnTime = 0;
            }
            else if (theId == 103)
            {
                mApp.DoAlmanacDialog(SeedType.None, ZombieType.Invalid, this);
            }
            else
            {
                if (theId == 105)
                {
                    StoreScreen storeScreen = mApp.ShowStoreScreen(this);
                    storeScreen.mBackButton.mButtonImage = AtlasResources.IMAGE_STORE_CONTINUEBUTTON;
                    storeScreen.mBackButton.mDownImage = AtlasResources.IMAGE_STORE_CONTINUEBUTTONDOWN;
                    return;
                }
                if (theId == 104)
                {
                    mMenuButton.mIsOver = false;
                    mMenuButton.mIsDown = false;
                    mApp.DoNewOptions(false);
                }
            }
            if (mApp.GetSeedsAvailable() < mBoard.mSeedBank.mNumPackets)
            {
                return;
            }
            if (theId == 100)
            {
                OnStartButton();
                return;
            }
            if (theId == 101)
            {
                PickRandomSeeds();
            }
        }

        public override void MouseDown(int x, int y, int theClickCount)
        {
            base.MouseDown(x, y, theClickCount);
            if (mSeedsInFlight > 0)
            {
                for (SeedType i = 0; i < SeedType.SeedsInChooserCount; i++)
                {
                    ChosenSeed chosenSeed = mChosenSeeds[(int)i];
                    LandFlyingSeed(ref chosenSeed);
                }
            }
            if (mChooseState == SeedChooserState.ViewLawn)
            {
                CancelLawnView();
                return;
            }
            mStartButton.Update();
            mRandomButton.Update();
            mViewLawnButton.Update();
            mAlmanacButton.Update();
            mImitaterButton.Update();
            mStoreButton.Update();
            mMenuButton.Update();
            if (mRandomButton.IsMouseOver())
            {
                mApp.PlaySample(Resources.SOUND_TAP);
                ButtonDepress(101);
                return;
            }
            if (mViewLawnButton.IsMouseOver())
            {
                mApp.PlaySample(Resources.SOUND_TAP);
                ButtonDepress(102);
                return;
            }
            if (mMenuButton.IsMouseOver())
            {
                mApp.PlaySample(Resources.SOUND_GRAVEBUTTON);
            }
            if (mStartButton.IsMouseOver())
            {
                mApp.PlaySample(Resources.SOUND_TAP);
            }
            if (mAlmanacButton.IsMouseOver())
            {
                mApp.PlaySample(Resources.SOUND_TAP);
            }
            if (mStoreButton.IsMouseOver())
            {
                mApp.PlaySample(Resources.SOUND_TAP);
            }
            if (!mImitaterButton.IsMouseOver())
            {
                for (SeedType j = 0; j < SeedType.SeedsInChooserCount; j++)
                {
                    ChosenSeed chosenSeed2 = mApp.mSeedChooserScreen.mChosenSeeds[(int)j];
                    if (chosenSeed2.mSeedState == ChosenSeedState.SEED_IN_BANK && x >= chosenSeed2.mX && y >= chosenSeed2.mY && x < chosenSeed2.mX + Constants.SMALL_SEEDPACKET_WIDTH && y < chosenSeed2.mY + Constants.SMALL_SEEDPACKET_HEIGHT)
                    {
                        ClickedSeedInBank(ref chosenSeed2);
                        return;
                    }
                }
                return;
            }
            if (mSeedsInBank == mBoard.mSeedBank.mNumPackets)
            {
                return;
            }
            mApp.PlaySample(Resources.SOUND_TAP);
            ImitaterDialog imitaterDialog = new ImitaterDialog();
            mApp.AddDialog(imitaterDialog.mId, imitaterDialog);
            imitaterDialog.Resize((mWidth - imitaterDialog.mWidth) / 2, (mHeight - imitaterDialog.mHeight) / 2, imitaterDialog.mWidth, imitaterDialog.mHeight);
            mApp.mWidgetManager.SetFocus(imitaterDialog);
        }

        public override void MouseUp(int x, int y, int theClickCount)
        {
            if (theClickCount != 1)
            {
                return;
            }
            if (mMenuButton.IsMouseOver())
            {
                ButtonDepress(104);
                return;
            }
            if (mStartButton.IsMouseOver())
            {
                ButtonDepress(100);
                return;
            }
            if (mAlmanacButton.IsMouseOver())
            {
                ButtonDepress(103);
                return;
            }
            if (mStoreButton.IsMouseOver())
            {
                ButtonDepress(105);
                return;
            }
            if (mImitaterButton.IsMouseOver())
            {
                ButtonDepress(106);
            }
        }

        public override void KeyChar(SexyChar theChar)
        {
            if (mChooseState == SeedChooserState.ViewLawn && (theChar.value_type == ' ' || theChar.value_type == '\r' || theChar.value_type == '\u001b'))
            {
                CancelLawnView();
                return;
            }
            if (mApp.mTodCheatKeys && theChar == KeyCode.Escape)
            {
                PickRandomSeeds();
                return;
            }
            mBoard.KeyChar(theChar);
        }

        public override void KeyDown(KeyCode theKey)
        {
            mBoard.DoTypingCheck(theKey);
        }

        public void GetSeedPositionInChooser(int theIndex, ref int x, ref int y)
        {
            if (theIndex == 48)
            {
                x = mImitaterButton.mX;
                y = mImitaterButton.mY;
                return;
            }
            mSeedPacketsWidget.GetSeedPosition((SeedType)theIndex, ref x, ref y);
            x += Constants.SCROLL_AREA_OFFSET_X;
            y += Constants.SCROLL_AREA_OFFSET_Y;
        }

        public void GetSeedPositionInBank(int theIndex, ref int x, ref int y)
        {
            y = mBoard.mSeedBank.mY + mBoard.GetSeedPacketPositionY(theIndex) - mY;
            x = mBoard.mSeedBank.mX;
        }

        public void ClickedSeedInChooser(ref ChosenSeed theChosenSeed)
        {
            if (mSeedsInBank == mBoard.mSeedBank.mNumPackets || !mApp.HasSeedType(theChosenSeed.mSeedType))
            {
                return;
            }
            if (mApp.mPlayerInfo.mNeedsGrayedPlantWarning)
            {
                uint num = SeedNotRecommendedToPick(theChosenSeed.mSeedType);
                if (num != 0U)
                {
                    mApp.mPlayerInfo.mNeedsGrayedPlantWarning = false;
                    mApp.DoDialog(16, true, "[DIALOG_WARNING]", "[NOT_RECOMMENDED_FOR_LEVEL]", "[DIALOG_BUTTON_OK]", 3);
                    return;
                }
            }
            theChosenSeed.mTimeStartMotion = mSeedChooserAge;
            theChosenSeed.mTimeEndMotion = mSeedChooserAge + 25;
            GetSeedPositionInChooser((int)theChosenSeed.mSeedType, ref theChosenSeed.mStartX, ref theChosenSeed.mStartY);
            if (theChosenSeed.mSeedType != SeedType.Imitater)
            {
                theChosenSeed.mStartY += mSeedPacketsWidget.mY;
            }
            GetSeedPositionInBank(mSeedsInBank, ref theChosenSeed.mEndX, ref theChosenSeed.mEndY);
            theChosenSeed.mSeedState = ChosenSeedState.SEED_FLYING_TO_BANK;
            theChosenSeed.mSeedIndexInBank = mSeedsInBank;
            mSeedsInFlight++;
            mSeedsInBank++;
            mApp.PlaySample(Resources.SOUND_TAP);
            if (mSeedsInBank == mBoard.mSeedBank.mNumPackets)
            {
                EnableStartButton(true);
            }
        }

        public void ClickedSeedInBank(ref ChosenSeed theChosenSeed)
        {
            if (theChosenSeed.mSeedState == ChosenSeedState.SEED_IN_BANK && theChosenSeed.mCrazyDavePicked)
            {
                mApp.PlaySample(Resources.SOUND_BUZZER);
                return;
            }
            for (int i = theChosenSeed.mSeedIndexInBank + 1; i < mBoard.mSeedBank.mNumPackets; i++)
            {
                SeedType seedType = FindSeedInBank(i);
                if (seedType != SeedType.None)
                {
                    ChosenSeed chosenSeed = mChosenSeeds[(int)seedType];
                    chosenSeed.mTimeStartMotion = mSeedChooserAge;
                    chosenSeed.mTimeEndMotion = mSeedChooserAge + 15;
                    chosenSeed.mStartX = chosenSeed.mX;
                    chosenSeed.mStartY = chosenSeed.mY;
                    GetSeedPositionInBank(i - 1, ref chosenSeed.mEndX, ref chosenSeed.mEndY);
                    chosenSeed.mSeedState = ChosenSeedState.SEED_FLYING_TO_BANK;
                    chosenSeed.mSeedIndexInBank = i - 1;
                    mSeedsInFlight++;
                }
            }
            theChosenSeed.mTimeStartMotion = mSeedChooserAge;
            theChosenSeed.mTimeEndMotion = mSeedChooserAge + 25;
            theChosenSeed.mStartX = theChosenSeed.mX;
            theChosenSeed.mStartY = theChosenSeed.mY;
            GetSeedPositionInChooser((int)theChosenSeed.mSeedType, ref theChosenSeed.mEndX, ref theChosenSeed.mEndY);
            if (theChosenSeed.mSeedType != SeedType.Imitater)
            {
                theChosenSeed.mEndY += mSeedPacketsWidget.mY;
            }
            theChosenSeed.mSeedState = ChosenSeedState.SEED_FLYING_TO_CHOOSER;
            theChosenSeed.mSeedIndexInBank = 0;
            mSeedsInFlight++;
            mSeedsInBank--;
            EnableStartButton(false);
            mApp.PlaySample(Resources.SOUND_TAP);
        }

        public SeedType FindSeedInBank(int theIndexInBank)
        {
            for (SeedType i = 0; i < SeedType.SeedsInChooserCount; i++)
            {
                if (mApp.HasSeedType(i))
                {
                    ChosenSeed chosenSeed = mChosenSeeds[(int)i];
                    if (chosenSeed.mSeedState == ChosenSeedState.SEED_IN_BANK && chosenSeed.mSeedIndexInBank == theIndexInBank)
                    {
                        return i;
                    }
                }
            }
            return SeedType.None;
        }

        public void EnableStartButton(bool theEnabled)
        {
            mStartButton.SetDisabled(!theEnabled);
            if (theEnabled)
            {
                mStartButton.mColors[0] = new SexyColor(255, 231, 26);
                return;
            }
            mStartButton.mColors[0] = new SexyColor(64, 64, 64);
        }

        public uint SeedNotRecommendedToPick(SeedType theSeedType)
        {
            uint num = mBoard.SeedNotRecommendedForLevel(theSeedType);
            if (TodCommon.TestBit(num, 0) && PickedPlantType(SeedType.InstantCoffee))
            {
                TodCommon.SetBit(ref num, 0, 0);
            }
            return num;
        }

        public bool SeedNotAllowedToPick(SeedType theSeedType)
        {
            return mApp.mGameMode == GameMode.ChallengeLastStand && (theSeedType == SeedType.Sunflower || theSeedType == SeedType.Sunshroom || theSeedType == SeedType.Twinsunflower || theSeedType == SeedType.Seashroom || theSeedType == SeedType.Puffshroom);
        }

        public void CloseSeedChooser()
        {
            Debug.ASSERT(mBoard.mSeedBank.mNumPackets == mBoard.GetNumSeedsInBank());
            for (int i = 0; i < mBoard.mSeedBank.mNumPackets; i++)
            {
                SeedType seedType = FindSeedInBank(i);
                ChosenSeed chosenSeed = mChosenSeeds[(int)seedType];
                SeedPacket seedPacket = mBoard.mSeedBank.mSeedPackets[i];
                seedPacket.SetPacketType(seedType, chosenSeed.mImitaterType);
                if (chosenSeed.mRefreshing)
                {
                    seedPacket.mRefreshCounter = mChosenSeeds[(int)seedType].mRefreshCounter;
                    seedPacket.mRefreshTime = Plant.GetRefreshTime(seedPacket.mPacketType, seedPacket.mImitaterType);
                    seedPacket.mRefreshing = true;
                    seedPacket.mActive = false;
                }
            }
            mBoard.mCutScene.EndSeedChooser();
        }

        public bool PickedPlantType(SeedType theSeedType)
        {
            for (SeedType i = 0; i < SeedType.SeedsInChooserCount; i++)
            {
                ChosenSeed chosenSeed = mChosenSeeds[(int)i];
                if (chosenSeed.mSeedState == ChosenSeedState.SEED_IN_BANK)
                {
                    if (chosenSeed.mSeedType == theSeedType)
                    {
                        return true;
                    }
                    if (chosenSeed.mSeedType == SeedType.Imitater && chosenSeed.mImitaterType == theSeedType)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void OnStartButton()
        {
            if (mApp.mGameMode == GameMode.ChallengeSeeingStars && !PickedPlantType(SeedType.Starfruit) && !DisplayRepickWarningDialog(0, "[SEED_CHOOSER_SEEING_STARS_WARNING]"))
            {
                return;
            }
            if (mApp.IsFirstTimeAdventureMode() && mBoard.mLevel == 11 && !PickedPlantType(SeedType.Puffshroom) && !DisplayRepickWarningDialog(1, "[SEED_CHOOSER_PUFFSHROOM_WARNING]"))
            {
                return;
            }
            if (!PickedPlantType(SeedType.Sunflower) && !PickedPlantType(SeedType.Twinsunflower) && !PickedPlantType(SeedType.Sunshroom) && !mBoard.mCutScene.IsSurvivalRepick() && mApp.mGameMode != GameMode.ChallengeLastStand)
            {
                if (mApp.IsFirstTimeAdventureMode() && mBoard.mLevel == 11)
                {
                    if (!DisplayRepickWarningDialog(2, "[SEED_CHOOSER_NIGHT_SUN_WARNING]"))
                    {
                        return;
                    }
                }
                else if (!DisplayRepickWarningDialog(3, "[SEED_CHOOSER_SUN_WARNING]"))
                {
                    return;
                }
            }
            if (mBoard.StageHasPool() && !PickedPlantType(SeedType.Lilypad) && !PickedPlantType(SeedType.Seashroom) && !PickedPlantType(SeedType.Tanglekelp) && !mBoard.mCutScene.IsSurvivalRepick())
            {
                if (mApp.IsFirstTimeAdventureMode() && mBoard.mLevel == 21)
                {
                    if (!DisplayRepickWarningDialog(4, "[SEED_CHOOSER_LILY_WARNING]"))
                    {
                        return;
                    }
                }
                else if (!DisplayRepickWarningDialog(5, "[SEED_CHOOSER_POOL_WARNING]"))
                {
                    return;
                }
            }
            if (mBoard.StageHasRoof() && !PickedPlantType(SeedType.Flowerpot) && mApp.HasSeedType(SeedType.Flowerpot) && !DisplayRepickWarningDialog(6, "[SEED_CHOOSER_ROOF_WARNING]"))
            {
                return;
            }
            if (mApp.mGameMode == GameMode.ChallengeArtChallenge1 && !PickedPlantType(SeedType.Wallnut) && !DisplayRepickWarningDialog(7, "[SEED_CHOOSER_ART_WALLNUT_WARNING]"))
            {
                return;
            }
            if (mApp.mGameMode == GameMode.ChallengeArtChallenge2 && (!PickedPlantType(SeedType.Starfruit) || !PickedPlantType(SeedType.Umbrella) || !PickedPlantType(SeedType.Wallnut)) && !DisplayRepickWarningDialog(8, "[SEED_CHOOSER_ART_2_WARNING]"))
            {
                return;
            }
            if (FlyersAreComing() && !FlyProtectionCurrentlyPlanted() && !PickedPlantType(SeedType.Cattail) && !PickedPlantType(SeedType.Cactus) && !PickedPlantType(SeedType.Blover) && !DisplayRepickWarningDialog(9, "[SEED_CHOOSER_FLYER_WARNING]"))
            {
                return;
            }
            if (!CheckSeedUpgrade(10, SeedType.Gatlingpea, SeedType.Repeater) || !CheckSeedUpgrade(11, SeedType.Wintermelon, SeedType.Melonpult) || !CheckSeedUpgrade(12, SeedType.Twinsunflower, SeedType.Sunflower) || !CheckSeedUpgrade(13, SeedType.Spikerock, SeedType.Spikeweed) || !CheckSeedUpgrade(14, SeedType.Cobcannon, SeedType.Kernelpult) || !CheckSeedUpgrade(15, SeedType.GoldMagnet, SeedType.Magnetshroom) || !CheckSeedUpgrade(16, SeedType.Gloomshroom, SeedType.Fumeshroom) || !CheckSeedUpgrade(17, SeedType.Cattail, SeedType.Lilypad))
            {
                return;
            }
            CloseSeedChooser();
        }

        public bool DisplayRepickWarningDialog(int theWarningId, string theMessage)
        {
            if (mPickWarningsWaved[theWarningId])
            {
                return true;
            }
            mPendingWarningId = theWarningId;
            mApp.LawnMessageBox(28, "[DIALOG_WARNING]", theMessage, "[DIALOG_BUTTON_YES]", "[REPICK_BUTTON]", 1, this);
            return false;
        }

        public void UpdateViewLawn()
        {
            if (mChooseState != SeedChooserState.ViewLawn)
            {
                return;
            }
            mViewLawnTime++;
            if (mViewLawnTime == 100)
            {
                mBoard.DisplayAdviceAgain("[CLICK_TO_CONTINUE]", MessageStyle.HintStay, AdviceType.ClickToContinue);
            }
            else if (mViewLawnTime == 251)
            {
                mViewLawnTime = 250;
            }
            int num = 800;
            if (mViewLawnTime <= 100)
            {
                int thePositionStart = -Constants.BOARD_OFFSET + Constants.ImageWidth - num;
                int thePositionEnd = 0;
                int num2 = TodCommon.TodAnimateCurve(0, 100, mViewLawnTime, thePositionStart, thePositionEnd, TodCurves.EaseInOut);
                mBoard.Move((int)((float)(-num2) * Constants.S), 0);
                int thePositionStart2 = Constants.SEED_CHOOSER_OFFSETSCREEN_OFFSET - 265;
                int seed_CHOOSER_OFFSETSCREEN_OFFSET = Constants.SEED_CHOOSER_OFFSETSCREEN_OFFSET;
                int num3 = TodCommon.TodAnimateCurve(0, 40, mViewLawnTime, thePositionStart2, seed_CHOOSER_OFFSETSCREEN_OFFSET, TodCurves.EaseInOut);
                Move(0, (int)(num3 * Constants.S));
                return;
            }
            if (mViewLawnTime > 100 && mViewLawnTime <= 250)
            {
                mBoard.Move(0, 0);
                Move(0, Constants.SEED_CHOOSER_OFFSETSCREEN_OFFSET);
                return;
            }
            if (mViewLawnTime > 250 && mViewLawnTime <= 350)
            {
                mBoard.ClearAdvice(AdviceType.ClickToContinue);
                int thePositionStart3 = 0;
                int thePositionEnd2 = -Constants.BOARD_OFFSET + Constants.ImageWidth - num;
                int num4 = TodCommon.TodAnimateCurve(250, 350, mViewLawnTime, thePositionStart3, thePositionEnd2, TodCurves.EaseInOut);
                mBoard.Move((int)((float)(-num4) * Constants.S), 0);
                int seed_CHOOSER_OFFSETSCREEN_OFFSET2 = Constants.SEED_CHOOSER_OFFSETSCREEN_OFFSET;
                int thePositionEnd3 = Constants.SEED_CHOOSER_OFFSETSCREEN_OFFSET - 265;
                int num5 = TodCommon.TodAnimateCurve(310, 350, mViewLawnTime, seed_CHOOSER_OFFSETSCREEN_OFFSET2, thePositionEnd3, TodCurves.EaseInOut);
                Move(0, (int)(num5 * Constants.S));
                return;
            }
            mChooseState = SeedChooserState.Normal;
            mViewLawnTime = 0;
            mMenuButton.mDisabled = false;
        }

        public void CancelLawnView()
        {
            if (mChooseState == SeedChooserState.ViewLawn && mViewLawnTime > 100 && mViewLawnTime <= 250)
            {
                mViewLawnTime = 251;
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
                mSeedsInFlight--;
                return;
            }
            if (theChosenSeed.mSeedState == ChosenSeedState.SEED_FLYING_TO_CHOOSER)
            {
                theChosenSeed.mTimeStartMotion = 0;
                theChosenSeed.mTimeEndMotion = 0;
                theChosenSeed.mSeedState = ChosenSeedState.SEED_IN_CHOOSER;
                theChosenSeed.mX = theChosenSeed.mEndX;
                theChosenSeed.mY = theChosenSeed.mEndY;
                mSeedsInFlight--;
                if (theChosenSeed.mSeedType == SeedType.Imitater)
                {
                    theChosenSeed.mSeedState = ChosenSeedState.SEED_PACKET_HIDDEN;
                    theChosenSeed.mImitaterType = SeedType.None;
                    UpdateImitaterButton();
                }
            }
        }

        public bool FlyProtectionCurrentlyPlanted()
        {
            int count = mBoard.mPlants.Count;
            for (int i = 0; i < count; i++)
            {
                Plant plant = mBoard.mPlants[i];
                if (!plant.mDead && (plant.mSeedType == SeedType.Cattail || plant.mSeedType == SeedType.Cactus))
                {
                    return true;
                }
            }
            return false;
        }

        public bool FlyersAreComing()
        {
            for (int i = 0; i < mBoard.mNumWaves; i++)
            {
                for (int j = 0; j < 50; j++)
                {
                    ZombieType zombieType = mBoard.mZombiesInWave[i, j];
                    if (zombieType == ZombieType.Invalid)
                    {
                        break;
                    }
                    if (zombieType == ZombieType.Balloon)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void UpdateAfterPurchase()
        {
            for (SeedType i = 0; i < SeedType.SeedsInChooserCount; i++)
            {
                ChosenSeed chosenSeed = mChosenSeeds[(int)i];
                if (chosenSeed.mSeedState == ChosenSeedState.SEED_IN_BANK)
                {
                    GetSeedPositionInBank(chosenSeed.mSeedIndexInBank, ref chosenSeed.mX, ref chosenSeed.mY);
                    chosenSeed.mStartX = chosenSeed.mX;
                    chosenSeed.mStartY = chosenSeed.mY;
                    chosenSeed.mEndX = chosenSeed.mX;
                    chosenSeed.mEndY = chosenSeed.mY;
                }
                else if (chosenSeed.mSeedState == ChosenSeedState.SEED_IN_CHOOSER)
                {
                    GetSeedPositionInChooser((int)i, ref chosenSeed.mX, ref chosenSeed.mY);
                    chosenSeed.mStartX = chosenSeed.mX;
                    chosenSeed.mStartY = chosenSeed.mY;
                    chosenSeed.mEndX = chosenSeed.mX;
                    chosenSeed.mEndY = chosenSeed.mY;
                }
            }
            if (mSeedsInBank == mBoard.mSeedBank.mNumPackets)
            {
                EnableStartButton(true);
            }
            else
            {
                EnableStartButton(false);
            }
            UpdateImitaterButton();
        }

        public void UpdateImitaterButton()
        {
            if (!mApp.HasSeedType(SeedType.Imitater))
            {
                mImitaterButton.mBtnNoDraw = true;
                mImitaterButton.mDisabled = true;
                return;
            }
            mImitaterButton.mBtnNoDraw = false;
            bool disabled = false;
            ChosenSeed chosenSeed = mChosenSeeds[48];
            if (chosenSeed.mSeedState != ChosenSeedState.SEED_PACKET_HIDDEN)
            {
                disabled = true;
            }
            mImitaterButton.SetDisabled(disabled);
        }

        public void CrazyDavePickSeeds()
        {
            for (int i = 0; i < SeedChooserScreen.aSeedArray.Length; i++)
            {
                SeedChooserScreen.aSeedArray[i].Reset();
            }
            for (SeedType j = 0; j < SeedType.SeedsInChooserCount; j++)
            {
                SeedType seedType = (SeedType)j;
                SeedChooserScreen.aSeedArray[(int)j].mItem = (int)seedType;
                uint num = SeedNotRecommendedToPick(seedType);
                if (!mApp.HasSeedType(seedType) || num != 0U || SeedNotAllowedToPick(seedType) || Plant.IsUpgrade(seedType) || seedType == SeedType.Imitater || seedType == SeedType.Umbrella || seedType == SeedType.Blover)
                {
                    SeedChooserScreen.aSeedArray[(int)j].mWeight = 0;
                }
                else
                {
                    SeedChooserScreen.aSeedArray[(int)j].mWeight = 1;
                }
            }
            if (mBoard.mZombieAllowed[22] || mBoard.mZombieAllowed[20])
            {
                SeedChooserScreen.aSeedArray[37].mWeight = 1;
            }
            if (mBoard.mZombieAllowed[16] || mBoard.StageHasFog())
            {
                SeedChooserScreen.aSeedArray[27].mWeight = 1;
            }
            if (mBoard.StageHasRoof())
            {
                SeedChooserScreen.aSeedArray[22].mWeight = 0;
            }
            int levelRandSeed = mBoard.GetLevelRandSeed();
            RandomNumbers.Seed(levelRandSeed);
            for (int k = 0; k < 3; k++)
            {
                SeedType seedType2 = (SeedType)PickFromWeightedArrayUsingSpecialRandSeed(SeedChooserScreen.aSeedArray, (int)SeedType.SeedsInChooserCount);
                SeedChooserScreen.aSeedArray[(int)seedType2].mWeight = 0;
                ChosenSeed chosenSeed = mChosenSeeds[(int)seedType2];
                chosenSeed.mY = mBoard.GetSeedPacketPositionY(k);
                chosenSeed.mX = 0;
                chosenSeed.mEndX = chosenSeed.mX;
                chosenSeed.mEndY = chosenSeed.mY;
                chosenSeed.mStartX = chosenSeed.mX;
                chosenSeed.mStartY = chosenSeed.mY;
                chosenSeed.mSeedState = ChosenSeedState.SEED_IN_BANK;
                chosenSeed.mSeedIndexInBank = k;
                chosenSeed.mCrazyDavePicked = true;
                mSeedsInBank++;
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
            int num2 = RandomNumbers.NextNumber(num);
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
            if (mApp.IsSurvivalMode() || mPickWarningsWaved[theWarningId])
            {
                return true;
            }
            if (PickedPlantType(theSeedTypeTo) && !PickedPlantType(theSeedTypeFrom))
            {
                string text = TodStringFile.TodStringTranslate("[SEED_CHOOSER_UPGRADE_WARNING]");
                string nameString = Plant.GetNameString(theSeedTypeTo, SeedType.None);
                string nameString2 = Plant.GetNameString(theSeedTypeFrom, SeedType.None);
                text = TodCommon.TodReplaceString(text, "{UPGRADE_TO}", nameString);
                text = TodCommon.TodReplaceString(text, "{UPGRADE_FROM}", nameString2);
                if (!DisplayRepickWarningDialog(theWarningId, text))
                {
                    return false;
                }
            }
            return true;
        }

        public void PickRandomSeeds()
        {
            for (int i = mSeedsInBank; i < mBoard.mSeedBank.mNumPackets; i++)
            {
                int num;
                SeedType seedType;
                do
                {
                    num = RandomNumbers.NextNumber(mApp.GetSeedsAvailable());
                    seedType = (SeedType)num;
                }
                while (!mApp.HasSeedType(seedType) || seedType == SeedType.Imitater || mChosenSeeds[num].mSeedState != ChosenSeedState.SEED_IN_CHOOSER);
                ChosenSeed chosenSeed = mChosenSeeds[num];
                chosenSeed.mTimeStartMotion = 0;
                chosenSeed.mTimeEndMotion = 0;
                chosenSeed.mStartX = chosenSeed.mX;
                chosenSeed.mStartY = chosenSeed.mY;
                GetSeedPositionInBank(i, ref chosenSeed.mEndX, ref chosenSeed.mEndY);
                chosenSeed.mSeedState = ChosenSeedState.SEED_IN_BANK;
                chosenSeed.mSeedIndexInBank = i;
                mSeedsInBank++;
            }
            for (SeedType j = 0; j < SeedType.SeedsInChooserCount; j++)
            {
                ChosenSeed chosenSeed2 = mChosenSeeds[(int)j];
                LandFlyingSeed(ref chosenSeed2);
            }
            CloseSeedChooser();
        }

        public bool Has12Rows()
        {
            return mApp.HasFinishedAdventure() || (mApp.HasSeedType(SeedType.Gatlingpea) || mApp.HasSeedType(SeedType.Twinsunflower) || mApp.HasSeedType(SeedType.Gloomshroom) || mApp.HasSeedType(SeedType.Cattail) || mApp.HasSeedType(SeedType.Wintermelon) || mApp.HasSeedType(SeedType.Gatlingpea) || mApp.HasSeedType(SeedType.GoldMagnet) || mApp.HasSeedType(SeedType.Cobcannon));
        }

        public bool SeedNotAllowedDuringTrial(SeedType theSeedType)
        {
            return mApp.IsTrialStageLocked() && (theSeedType == SeedType.Squash || theSeedType == SeedType.Threepeater);
        }

        public void BackFromStore()
        {
            StoreScreen storeScreen = (StoreScreen)mApp.GetDialog(Dialogs.DIALOG_STORE);
            mApp.KillDialog(4);
            mApp.mMusic.MakeSureMusicIsPlaying(MusicTune.ChooseYourSeeds);
        }

        public void BackFromAlmanac()
        {
            mApp.mMusic.MakeSureMusicIsPlaying(MusicTune.ChooseYourSeeds);
        }

        public void LawnMessageBoxDone(int theResult)
        {
            if (theResult == 1000)
            {
                mPickWarningsWaved[mPendingWarningId] = true;
                mDoStartButton = true;
            }
        }

        public void SeedSelected(SeedType theSeedType)
        {
            if (theSeedType == SeedType.None)
            {
                return;
            }
            if (SeedNotAllowedToPick(theSeedType))
            {
                mApp.DoDialog(16, true, string.Empty, "[NOT_ALLOWED_ON_THIS_LEVEL]", "[DIALOG_BUTTON_OK]", 3);
                return;
            }
            if (theSeedType >= SeedType.Peashooter && theSeedType < (SeedType)mChosenSeeds.Length)
            {
                ChosenSeed chosenSeed = mChosenSeeds[(int)theSeedType];
                if (chosenSeed.mSeedState == ChosenSeedState.SEED_IN_CHOOSER)
                {
                    ClickedSeedInChooser(ref chosenSeed);
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

        public ChosenSeed[] mChosenSeeds = new ChosenSeed[(int)SeedType.SeedTypeCount];

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

        private static TodWeightedArray[] aSeedArray = new TodWeightedArray[(int)SeedType.SeedTypeCount];
    }
}
