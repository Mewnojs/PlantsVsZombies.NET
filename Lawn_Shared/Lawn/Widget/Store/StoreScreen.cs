using System;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
	internal class StoreScreen : Dialog
	{
		static StoreScreen()
		{
			for (int i = 0; i < StoreScreen.aPickArray.Length; i++)
			{
				StoreScreen.aPickArray[i] = TodWeightedArray.GetNewTodWeightedArray();
			}
		}

		public StoreScreen(LawnApp theApp, StoreListener theListener) : base(null, null, 4, true, "[STORE]", "", "", 0)
		{
			this.mListener = theListener;
			this.mApp = theApp;
			this.mClip = false;
			this.mStoreTime = 0;
			this.mBubbleCountDown = 0;
			this.mBubbleClickToContinue = false;
			this.mBubbleAutoAdvance = false;
			this.mAmbientSpeechCountDown = 200;
			this.mPreviousAmbientSpeechIndex = -1;
			this.mPage = StorePage.STORE_PAGE_SLOT_UPGRADES;
			this.mMouseOverItem = StoreItem.STORE_ITEM_INVALID;
			this.mHatchTimer = 0;
			this.mShakeX = 0;
			this.mShakeY = 0;
			this.mStartDialog = -1;
			this.mHatchOpen = true;
			this.mEasyBuyingCheat = false;
			StoreScreen.mCoins.DataArrayInitialize(1024U, "coins");
			this.mApp.DelayLoadStoreResource("DelayLoad_Store");
			this.Resize(0, 0, Constants.BOARD_WIDTH, Constants.BOARD_HEIGHT);
			this.mPottedPlantSpecs.InitializePottedPlant(SeedType.SEED_MARIGOLD);
			this.mPottedPlantSpecs.mDrawVariation = (DrawVariation)TodCommon.RandRangeInt(2, 12);
			this.mBackButton = new NewLawnButton(null, 100, this);
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
			this.mPrevButton = new NewLawnButton(null, 101, this);
			this.mPrevButton.mDoFinger = true;
			this.mPrevButton.mLabel = "";
			this.mPrevButton.mButtonImage = AtlasResources.IMAGE_STORE_PREVBUTTON;
			this.mPrevButton.mOverImage = AtlasResources.IMAGE_STORE_PREVBUTTONHIGHLIGHT;
			this.mPrevButton.mDownImage = AtlasResources.IMAGE_STORE_PREVBUTTONHIGHLIGHT;
			this.mPrevButton.mColors[0] = new SexyColor(255, 240, 0);
			this.mPrevButton.mColors[1] = new SexyColor(200, 200, 255);
			this.mPrevButton.Resize(Constants.StoreScreen_PrevButton_X, Constants.StoreScreen_PrevButton_Y, AtlasResources.IMAGE_STORE_PREVBUTTON.mWidth, AtlasResources.IMAGE_STORE_PREVBUTTON.mHeight);
			this.mNextButton = new NewLawnButton(null, 102, this);
			this.mNextButton.mDoFinger = true;
			this.mNextButton.mLabel = "";
			this.mNextButton.mButtonImage = AtlasResources.IMAGE_STORE_NEXTBUTTON;
			this.mNextButton.mOverImage = AtlasResources.IMAGE_STORE_NEXTBUTTONHIGHLIGHT;
			this.mNextButton.mDownImage = AtlasResources.IMAGE_STORE_NEXTBUTTONHIGHLIGHT;
			this.mNextButton.mColors[0] = new SexyColor(255, 240, 0);
			this.mNextButton.mColors[1] = new SexyColor(200, 200, 255);
			this.mNextButton.Resize(Constants.StoreScreen_NextButton_X, Constants.StoreScreen_NextButton_Y, AtlasResources.IMAGE_STORE_NEXTBUTTON.mWidth, AtlasResources.IMAGE_STORE_NEXTBUTTON.mHeight);
			this.mOverlayWidget = new StoreScreenOverlay(this);
			this.mOverlayWidget.Resize(0, 0, Constants.BOARD_WIDTH, Constants.BOARD_HEIGHT);
			if (!this.IsPageShown(StorePage.STORE_PAGE_PLANT_UPGRADES))
			{
				this.mPrevButton.mDisabledImage = AtlasResources.IMAGE_STORE_PREVBUTTONDISABLED;
				this.mPrevButton.SetDisabled(true);
				this.mNextButton.mDisabledImage = AtlasResources.IMAGE_STORE_NEXTBUTTONDISABLED;
				this.mNextButton.SetDisabled(true);
			}
			this.mDrawnOnce = false;
			this.mGoToTreeNow = false;
			this.mPurchasedFullVersion = false;
			this.mTrialLockedWhenStoreOpened = this.mApp.IsTrialStageLocked();
		}

		public override void Dispose()
		{
			StoreScreen.mCoins.DataArrayDispose();
			this.mBackButton.Dispose();
			this.mPrevButton.Dispose();
			this.mNextButton.Dispose();
			this.mOverlayWidget.Dispose();
			this.mApp.DelayLoadStoreResource(string.Empty);
		}

		public override void KeyDown(KeyCode theKey)
		{
		}

		public override void Update()
		{
			this.mApp.mMusic.MakeSureMusicIsPlaying(MusicTune.MUSIC_TUNE_TITLE_CRAZY_DAVE_MAIN_THEME);
			if (this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN || this.mApp.mBoard == null)
			{
				this.mApp.UpdateCrazyDave();
			}
			if (this.IsWaitingForDialog())
			{
				return;
			}
			if (this.mApp.mCrazyDaveState == CrazyDaveState.CRAZY_DAVE_OFF)
			{
				if (this.mDrawnOnce)
				{
					this.StorePreLoad();
				}
				return;
			}
			this.mStoreTime += 3;
			if (this.mApp.mCrazyDaveState != CrazyDaveState.CRAZY_DAVE_OFF && this.mApp.mCrazyDaveState != CrazyDaveState.CRAZY_DAVE_ENTERING)
			{
				if (this.mHatchTimer > 0)
				{
					this.mHatchTimer -= 3;
					this.mBackButton.mX -= this.mShakeX;
					this.mBackButton.mY -= this.mShakeY;
					this.mPrevButton.mX -= this.mShakeX;
					this.mPrevButton.mY -= this.mShakeY;
					this.mNextButton.mX -= this.mShakeX;
					this.mNextButton.mY -= this.mShakeY;
					if (this.mHatchTimer >= 0 && this.mHatchTimer < 3)
					{
						if (!this.mBubbleClickToContinue)
						{
							this.EnableButtons(true);
						}
						this.mShakeX = 0;
						this.mShakeY = 0;
					}
					else if (this.mHatchTimer > 35)
					{
						this.mShakeX = 0;
						this.mShakeY = RandomNumbers.NextNumber(3) - 1;
					}
					else
					{
						this.mShakeX = 0;
						this.mShakeY = 0;
					}
					this.mBackButton.mX += this.mShakeX;
					this.mBackButton.mY += this.mShakeY;
					this.mPrevButton.mX += this.mShakeX;
					this.mPrevButton.mY += this.mShakeY;
					this.mNextButton.mX += this.mShakeX;
					this.mNextButton.mY += this.mShakeY;
				}
				else if (this.mStartDialog != -1)
				{
					this.SetBubbleText(this.mStartDialog, 0, true);
					this.mStartDialog = -1;
				}
				else if (!this.mBubbleClickToContinue)
				{
					if (this.mBubbleCountDown > 0)
					{
						this.mBubbleCountDown -= 3;
						if (this.mBubbleCountDown >= 0 && this.mBubbleCountDown < 3)
						{
							if (this.mApp.mSoundSystem.IsFoleyPlaying(FoleyType.FOLEY_CRAZYDAVESHORT) || this.mApp.mSoundSystem.IsFoleyPlaying(FoleyType.FOLEY_CRAZYDAVELONG) || this.mApp.mSoundSystem.IsFoleyPlaying(FoleyType.FOLEY_CRAZYDAVEEXTRALONG))
							{
								this.mBubbleCountDown = 3;
							}
							else if (this.mBubbleAutoAdvance)
							{
								this.AdvanceCrazyDaveDialog();
							}
							else
							{
								this.mApp.CrazyDaveStopTalking();
							}
						}
					}
					else
					{
						this.mAmbientSpeechCountDown -= 3;
						if (this.mAmbientSpeechCountDown <= 0)
						{
							for (int i = 0; i < 4; i++)
							{
								StoreScreen.aPickArray[i].Reset();
							}
							for (int j = 0; j < 4; j++)
							{
								StoreScreen.aPickArray[j].mItem = 2015 + j;
								if (this.mPreviousAmbientSpeechIndex == (int)StoreScreen.aPickArray[j].mItem)
								{
									StoreScreen.aPickArray[j].mWeight = 0;
								}
								else if (j == 3)
								{
									if (!this.mApp.HasFinishedAdventure())
									{
										StoreScreen.aPickArray[j].mWeight = 0;
									}
									else
									{
										StoreScreen.aPickArray[j].mWeight = 20;
									}
								}
								else
								{
									StoreScreen.aPickArray[j].mWeight = 100;
								}
							}
							int theCrazyDaveMessage = (int)TodCommon.TodPickFromWeightedArray(StoreScreen.aPickArray, 4);
							this.mPreviousAmbientSpeechIndex = theCrazyDaveMessage;
							this.SetBubbleText(theCrazyDaveMessage, 800, false);
							this.mAmbientSpeechCountDown = TodCommon.RandRangeInt(500, 1000);
						}
					}
				}
			}
			this.UpdateMouse();
			if (this.CanInteractWithButtons() && this.mTrialLockedWhenStoreOpened && !this.mApp.IsTrialStageLocked())
			{
				this.mPurchasedFullVersion = true;
				this.mResult = 1000;
				return;
			}
			base.Update();
			this.MarkDirty();
		}

		public override void Draw(Graphics g)
		{
			g.SetLinearBlend(true);
			this.mDrawnOnce = true;
			int theY = TodCommon.TodAnimateCurve(50, 110, this.mStoreTime, Constants.StoreScreen_StoreSign_Y_Min, Constants.StoreScreen_StoreSign_Y_Max, TodCurves.CURVE_EASE_IN_OUT);
			if (this.mApp.IsNight())
			{
				g.DrawImage(Resources.IMAGE_STORE_BACKGROUNDNIGHT, 0, 0);
			}
			else
			{
				g.DrawImage(Resources.IMAGE_STORE_BACKGROUND, 0, 0);
			}
			g.DrawImage(Resources.IMAGE_STORE_CAR, Constants.StoreScreen_Car_X + this.mShakeX, Constants.StoreScreen_Car_Y + this.mShakeY);
			if (this.mHatchTimer == 0 && this.mHatchOpen)
			{
				g.DrawImage(Resources.IMAGE_STORE_HATCHBACKOPEN, Constants.StoreScreen_HatchOpen_X + this.mShakeX, Constants.StoreScreen_HatchOpen_Y + this.mShakeY);
				if (this.mApp.IsNight())
				{
					g.DrawImage(Resources.IMAGE_STORE_CAR_NIGHT, Constants.StoreScreen_CarNight_X + this.mShakeX, Constants.StoreScreen_CarNight_Y + this.mShakeY);
				}
			}
			else
			{
				g.DrawImage(Resources.IMAGE_STORE_CARCLOSED, Constants.StoreScreen_HatchClosed_X + this.mShakeX, Constants.StoreScreen_HatchClosed_Y + this.mShakeY);
				if (this.mApp.IsNight())
				{
					g.DrawImage(Resources.IMAGE_STORE_CAR_NIGHT, Constants.StoreScreen_CarNight_X + this.mShakeX, Constants.StoreScreen_CarNight_Y + this.mShakeY);
					g.DrawImage(Resources.IMAGE_STORE_CARCLOSED_NIGHT, Constants.StoreScreen_HatchClosed_X + this.mShakeX, Constants.StoreScreen_HatchClosed_Y + this.mShakeY);
				}
			}
			g.DrawImage(AtlasResources.IMAGE_STORE_SIGN, Constants.StoreScreen_StoreSign_X, theY);
			Graphics @new = Graphics.GetNew(g);
			@new.mTransX += Constants.StoreScreen_RetardedDave_Offset_X;
			Graphics graphics = @new;
			graphics.mTransY = graphics.mTransY;
			this.mApp.DrawCrazyDave(@new);
			@new.PrepareForReuse();
			if (this.mHatchTimer == 0 && this.mHatchOpen)
			{
				for (int i = 0; i < 8; i++)
				{
					StoreItem storeItemType = this.GetStoreItemType(i);
					if (storeItemType != StoreItem.STORE_ITEM_INVALID)
					{
						this.DrawItem(g, i, storeItemType);
					}
				}
			}
			int storeScreen_Coinbank_X = Constants.StoreScreen_Coinbank_X;
			int storeScreen_Coinbank_Y = Constants.StoreScreen_Coinbank_Y;
			g.DrawImage(AtlasResources.IMAGE_COINBANK, storeScreen_Coinbank_X, storeScreen_Coinbank_Y);
			g.SetColor(new SexyColor(180, 255, 90));
			g.SetFont(Resources.FONT_CONTINUUMBOLD14);
			string moneyString = LawnApp.GetMoneyString(this.mApp.mPlayerInfo.mCoins);
			int theX = storeScreen_Coinbank_X + Constants.StoreScreen_Coinbank_TextOffset.X - Resources.FONT_CONTINUUMBOLD14.StringWidth(moneyString);
			g.DrawString(moneyString, theX, storeScreen_Coinbank_Y + Constants.StoreScreen_Coinbank_TextOffset.Y);
		}

		public override void AddedToManager(WidgetManager theWidgetManager)
		{
			base.AddedToManager(theWidgetManager);
			this.AddWidget(this.mBackButton);
			this.AddWidget(this.mPrevButton);
			this.AddWidget(this.mNextButton);
			this.AddWidget(this.mOverlayWidget);
		}

		public override void RemovedFromManager(WidgetManager theWidgetManager)
		{
			base.RemovedFromManager(theWidgetManager);
			this.RemoveWidget(this.mBackButton);
			this.RemoveWidget(this.mPrevButton);
			this.RemoveWidget(this.mNextButton);
			this.RemoveWidget(this.mOverlayWidget);
			this.mApp.CrazyDaveDie();
		}

		public override void ButtonPress(int theId)
		{
			if (theId != 101 && theId != 102)
			{
				this.mApp.PlaySample(Resources.SOUND_BUTTONCLICK);
			}
		}

		public override bool BackButtonPress()
		{
			this.ButtonDepress(this.mBackButton.mId);
			return true;
		}

		public override void ButtonDepress(int theId)
		{
			if (theId == 100)
			{
				this.mResult = 1000;
				if (this.mListener != null)
				{
					this.mListener.BackFromStore();
				}
				return;
			}
			if (theId == 101)
			{
				this.mHatchTimer = 51;
				this.mApp.PlaySample(Resources.SOUND_HATCHBACK_CLOSE);
				this.mBubbleCountDown = 1;
				this.EnableButtons(false);
				do
				{
					this.mPage--;
					if (this.mPage < StorePage.STORE_PAGE_SLOT_UPGRADES)
					{
						this.mPage = StorePage.STORE_PAGE_ZEN2;
					}
				}
				while (!this.IsPageShown(this.mPage));
			}
			if (theId == 102)
			{
				this.mHatchTimer = 51;
				this.mApp.PlaySample(Resources.SOUND_HATCHBACK_CLOSE);
				this.mBubbleCountDown = 1;
				this.EnableButtons(false);
				do
				{
					this.mPage++;
					if (this.mPage >= StorePage.NUM_STORE_PAGES)
					{
						this.mPage = StorePage.STORE_PAGE_SLOT_UPGRADES;
					}
				}
				while (!this.IsPageShown(this.mPage));
			}
		}

		public virtual void KeyChar(char theChar)
		{
			if (this.mBubbleClickToContinue && (theChar == ' ' || theChar == '\r'))
			{
				this.AdvanceCrazyDaveDialog();
				return;
			}
			if (theChar == 'c' || theChar == 'C')
			{
				this.mEasyBuyingCheat = true;
				this.mNextButton.mMouseVisible = true;
				this.mNextButton.SetDisabled(false);
				this.mPrevButton.mMouseVisible = true;
				this.mPrevButton.SetDisabled(false);
			}
			if (theChar == '0')
			{
				this.mApp.mPlayerInfo.AddCoins(50000);
				this.mApp.PlaySample(Resources.SOUND_BUTTONCLICK);
			}
			if (theChar == '$')
			{
				this.mApp.mPlayerInfo.AddCoins(100);
				this.mApp.PlaySample(Resources.SOUND_BUTTONCLICK);
			}
			if (theChar == 'l')
			{
				this.mApp.mDebugTrialLocked = !this.mApp.mDebugTrialLocked;
				this.mApp.mTrialType = TrialType.TRIAL_STAGELOCKED;
			}
		}

		public override void MouseDown(int x, int y, int theClickCount)
		{
			if (this.mBubbleClickToContinue)
			{
				this.AdvanceCrazyDaveDialog();
				return;
			}
			this.UpdateMouse();
			if (!this.CanInteractWithButtons())
			{
				return;
			}
			for (int i = 0; i < 8; i++)
			{
				StoreItem storeItemType = this.GetStoreItemType(i);
				if (storeItemType != StoreItem.STORE_ITEM_INVALID)
				{
					int theX = 0;
					int theY = 0;
					this.GetStorePosition(i, ref theX, ref theY);
					TRect trect = new TRect(theX, theY, Constants.StoreScreen_MouseRegion.X, Constants.StoreScreen_MouseRegion.Y);
					if (trect.Contains(x, y))
					{
						if (this.IsItemSoldOut(storeItemType) || this.IsItemUnavailable(storeItemType) || this.IsComingSoon(storeItemType))
						{
							return;
						}
						this.PurchaseItem(storeItemType);
					}
				}
			}
		}

		public void SetBubbleText(int theCrazyDaveMessage, int theTime, bool theClickToContinue)
		{
			this.mApp.CrazyDaveTalkIndex(theCrazyDaveMessage);
			this.mBubbleCountDown = theTime;
			this.mBubbleClickToContinue = theClickToContinue;
		}

		public void GetStorePosition(int theSpotIndex, ref int thePosX, ref int thePosY)
		{
			if (theSpotIndex <= 3)
			{
				thePosX = Constants.StoreScreen_ItemOffset_1_X + Constants.StoreScreen_ItemSize * theSpotIndex;
				thePosY = Constants.StoreScreen_ItemOffset_1_Y;
				return;
			}
			thePosX = Constants.StoreScreen_ItemOffset_2_X + Constants.StoreScreen_ItemSize * (theSpotIndex - Constants.StoreScreen_ItemSize_Offset);
			thePosY = Constants.StoreScreen_ItemOffset_2_Y;
		}

		public StoreItem GetStoreItemType(int theSpotIndex)
		{
			if (this.mPage == StorePage.STORE_PAGE_SLOT_UPGRADES)
			{
				if (theSpotIndex == 0)
				{
					return StoreItem.STORE_ITEM_PACKET_UPGRADE;
				}
				if (theSpotIndex == 1)
				{
					return StoreItem.STORE_ITEM_POOL_CLEANER;
				}
				if (theSpotIndex == 2)
				{
					return StoreItem.STORE_ITEM_RAKE;
				}
				if (theSpotIndex == 3)
				{
					return StoreItem.STORE_ITEM_ROOF_CLEANER;
				}
				if (theSpotIndex == 4)
				{
					return StoreItem.STORE_ITEM_PLANT_GATLINGPEA;
				}
				if (theSpotIndex == 5)
				{
					return StoreItem.STORE_ITEM_PLANT_TWINSUNFLOWER;
				}
				if (theSpotIndex == 6)
				{
					return StoreItem.STORE_ITEM_PLANT_GLOOMSHROOM;
				}
				if (theSpotIndex == 7)
				{
					return StoreItem.STORE_ITEM_PLANT_CATTAIL;
				}
				return StoreItem.STORE_ITEM_INVALID;
			}
			else if (this.mPage == StorePage.STORE_PAGE_PLANT_UPGRADES)
			{
				if (theSpotIndex == 0)
				{
					return StoreItem.STORE_ITEM_PLANT_SPIKEROCK;
				}
				if (theSpotIndex == 1)
				{
					return StoreItem.STORE_ITEM_PLANT_GOLD_MAGNET;
				}
				if (theSpotIndex == 2)
				{
					return StoreItem.STORE_ITEM_PLANT_WINTERMELON;
				}
				if (theSpotIndex == 3)
				{
					return StoreItem.STORE_ITEM_PLANT_COBCANNON;
				}
				if (theSpotIndex == 4)
				{
					return StoreItem.STORE_ITEM_PLANT_IMITATER;
				}
				if (theSpotIndex == 5)
				{
					return StoreItem.STORE_ITEM_FIRSTAID;
				}
				return StoreItem.STORE_ITEM_INVALID;
			}
			else if (this.mPage == StorePage.STORE_PAGE_ZEN1)
			{
				if (theSpotIndex == 0)
				{
					return StoreItem.STORE_ITEM_POTTED_MARIGOLD_1;
				}
				if (theSpotIndex == 1)
				{
					return StoreItem.STORE_ITEM_POTTED_MARIGOLD_2;
				}
				if (theSpotIndex == 2)
				{
					return StoreItem.STORE_ITEM_POTTED_MARIGOLD_3;
				}
				if (theSpotIndex == 3)
				{
					return StoreItem.STORE_ITEM_GOLD_WATERINGCAN;
				}
				if (theSpotIndex == 4)
				{
					return StoreItem.STORE_ITEM_FERTILIZER;
				}
				if (theSpotIndex == 5)
				{
					return StoreItem.STORE_ITEM_BUG_SPRAY;
				}
				if (theSpotIndex == 6)
				{
					return StoreItem.STORE_ITEM_PHONOGRAPH;
				}
				if (theSpotIndex == 7)
				{
					return StoreItem.STORE_ITEM_GARDENING_GLOVE;
				}
				return StoreItem.STORE_ITEM_INVALID;
			}
			else
			{
				if (this.mPage != StorePage.STORE_PAGE_ZEN2)
				{
					Debug.ASSERT(false);
					return StoreItem.STORE_ITEM_INVALID;
				}
				if (theSpotIndex == 0)
				{
					return StoreItem.STORE_ITEM_MUSHROOM_GARDEN;
				}
				if (theSpotIndex == 1)
				{
					return StoreItem.STORE_ITEM_AQUARIUM_GARDEN;
				}
				if (theSpotIndex == 2)
				{
					return StoreItem.STORE_ITEM_WHEEL_BARROW;
				}
				if (theSpotIndex == 3)
				{
					return StoreItem.STORE_ITEM_STINKY_THE_SNAIL;
				}
				return StoreItem.STORE_ITEM_INVALID;
			}
		}

		public void UpdateMouse()
		{
		}

		public bool IsItemUnavailable(StoreItem theStoreItem)
		{
			if (this.mEasyBuyingCheat)
			{
				return false;
			}
			if (theStoreItem == StoreItem.STORE_ITEM_ROOF_CLEANER)
			{
				if (this.mApp.IsTrialStageLocked())
				{
					return true;
				}
				if (!this.mApp.HasFinishedAdventure() && this.mApp.mPlayerInfo.mLevel < 42)
				{
					return true;
				}
			}
			if (theStoreItem == StoreItem.STORE_ITEM_PLANT_GLOOMSHROOM)
			{
				if (this.mApp.IsTrialStageLocked())
				{
					return true;
				}
				if (!this.mApp.HasFinishedAdventure() && this.mApp.mPlayerInfo.mLevel < 35)
				{
					return true;
				}
			}
			if (theStoreItem == StoreItem.STORE_ITEM_PLANT_CATTAIL)
			{
				if (this.mApp.IsTrialStageLocked())
				{
					return true;
				}
				if (!this.mApp.HasFinishedAdventure() && this.mApp.mPlayerInfo.mLevel < 35)
				{
					return true;
				}
			}
			return (theStoreItem == StoreItem.STORE_ITEM_PLANT_SPIKEROCK && !this.mApp.HasFinishedAdventure() && this.mApp.mPlayerInfo.mLevel < 41) || (theStoreItem == StoreItem.STORE_ITEM_PLANT_GOLD_MAGNET && !this.mApp.HasFinishedAdventure() && this.mApp.mPlayerInfo.mLevel < 41) || ((theStoreItem == StoreItem.STORE_ITEM_PLANT_WINTERMELON || theStoreItem == StoreItem.STORE_ITEM_PLANT_COBCANNON || theStoreItem == StoreItem.STORE_ITEM_PLANT_IMITATER || theStoreItem == StoreItem.STORE_ITEM_FIRSTAID) && !this.mApp.HasFinishedAdventure());
		}

		public bool IsItemSoldOut(StoreItem theStoreItem)
		{
			if (theStoreItem == StoreItem.STORE_ITEM_INVALID)
			{
				return false;
			}
			if (theStoreItem == StoreItem.STORE_ITEM_PACKET_UPGRADE)
			{
				return this.mApp.mPlayerInfo.mPurchases[21] >= 3;
			}
			if (theStoreItem == StoreItem.STORE_ITEM_FERTILIZER || theStoreItem == StoreItem.STORE_ITEM_BUG_SPRAY)
			{
				return this.mApp.mPlayerInfo.mPurchases[(int)theStoreItem] - 1000 > 15;
			}
			if (theStoreItem == StoreItem.STORE_ITEM_BONUS_LAWN_MOWER)
			{
				return this.mApp.mPlayerInfo.mPurchases[9] >= 2;
			}
			if (StoreScreen.IsPottedPlant(theStoreItem))
			{
				int currentDaysSince = LawnCommon.GetCurrentDaysSince2000();
				return this.mApp.mZenGarden.IsZenGardenFull(true) || this.mApp.mPlayerInfo.mPurchases[(int)theStoreItem] == currentDaysSince;
			}
			Debug.ASSERT(theStoreItem >= StoreItem.STORE_ITEM_PLANT_GATLINGPEA && theStoreItem < (StoreItem)80);
			return this.mApp.mPlayerInfo.mPurchases[(int)theStoreItem] != 0;
		}

		public static int GetItemCost(StoreItem theStoreItem)
		{
			LawnApp lawnApp = (LawnApp)GlobalStaticVars.gSexyAppBase;
			if (theStoreItem != StoreItem.STORE_ITEM_BONUS_LAWN_MOWER)
			{
				switch (theStoreItem)
				{
				case StoreItem.STORE_ITEM_PLANT_GATLINGPEA:
					return 500;
				case StoreItem.STORE_ITEM_PLANT_TWINSUNFLOWER:
					return 500;
				case StoreItem.STORE_ITEM_PLANT_GLOOMSHROOM:
					return 750;
				case StoreItem.STORE_ITEM_PLANT_CATTAIL:
					return 1000;
				case StoreItem.STORE_ITEM_PLANT_WINTERMELON:
					return 1000;
				case StoreItem.STORE_ITEM_PLANT_GOLD_MAGNET:
					return 300;
				case StoreItem.STORE_ITEM_PLANT_SPIKEROCK:
					return 750;
				case StoreItem.STORE_ITEM_PLANT_COBCANNON:
					return 2000;
				case StoreItem.STORE_ITEM_PLANT_IMITATER:
					return 3000;
				case StoreItem.STORE_ITEM_POTTED_MARIGOLD_1:
					return 250;
				case StoreItem.STORE_ITEM_POTTED_MARIGOLD_2:
					return 250;
				case StoreItem.STORE_ITEM_POTTED_MARIGOLD_3:
					return 250;
				case StoreItem.STORE_ITEM_GOLD_WATERINGCAN:
					return 1000;
				case StoreItem.STORE_ITEM_FERTILIZER:
					return 75;
				case StoreItem.STORE_ITEM_BUG_SPRAY:
					return 100;
				case StoreItem.STORE_ITEM_PHONOGRAPH:
					return 1500;
				case StoreItem.STORE_ITEM_GARDENING_GLOVE:
					return 100;
				case StoreItem.STORE_ITEM_MUSHROOM_GARDEN:
					return 3000;
				case StoreItem.STORE_ITEM_WHEEL_BARROW:
					return 20;
				case StoreItem.STORE_ITEM_STINKY_THE_SNAIL:
					return 300;
				case StoreItem.STORE_ITEM_PACKET_UPGRADE:
					if (lawnApp.mPlayerInfo.mPurchases[21] + 1 == 1)
					{
						return 75;
					}
					if (lawnApp.mPlayerInfo.mPurchases[21] + 1 == 2)
					{
						return 500;
					}
					if (lawnApp.mPlayerInfo.mPurchases[21] + 1 == 3)
					{
						return 2000;
					}
					return 2000;
				case StoreItem.STORE_ITEM_POOL_CLEANER:
					return 100;
				case StoreItem.STORE_ITEM_ROOF_CLEANER:
					return 300;
				case StoreItem.STORE_ITEM_RAKE:
					return 20;
				case StoreItem.STORE_ITEM_AQUARIUM_GARDEN:
					return 3000;
				case StoreItem.STORE_ITEM_FIRSTAID:
					return 200;
				}
				Debug.ASSERT(false);
				return 0;
			}
			if (lawnApp.mPlayerInfo.mPurchases[9] == 0)
			{
				return 200;
			}
			return 500;
		}

		public bool CanAffordItem(StoreItem theStoreItem)
		{
			int itemCost = StoreScreen.GetItemCost(theStoreItem);
			return this.mApp.mPlayerInfo.mCoins >= itemCost;
		}

		public void PurchaseItem(StoreItem theItemType)
		{
			this.mBubbleCountDown = 0;
			this.mApp.CrazyDaveStopTalking();
			if (!this.CanAffordItem(theItemType))
			{
				this.mApp.DoDialog(25, true, "[NOT_ENOUGH_MONEY]", "[CANNOT_AFFORD_ITEM]", "[DIALOG_BUTTON_OK]", 3);
				return;
			}
			this.mPendingPurchaseItem = theItemType;
			int theMessageIndex = 0;
			switch (theItemType)
			{
			case StoreItem.STORE_ITEM_PLANT_GATLINGPEA:
				theMessageIndex = 2000;
				goto IL_23F;
			case StoreItem.STORE_ITEM_PLANT_TWINSUNFLOWER:
				theMessageIndex = 2001;
				goto IL_23F;
			case StoreItem.STORE_ITEM_PLANT_GLOOMSHROOM:
				theMessageIndex = 2002;
				goto IL_23F;
			case StoreItem.STORE_ITEM_PLANT_CATTAIL:
				theMessageIndex = 2003;
				goto IL_23F;
			case StoreItem.STORE_ITEM_PLANT_WINTERMELON:
				theMessageIndex = 2004;
				goto IL_23F;
			case StoreItem.STORE_ITEM_PLANT_GOLD_MAGNET:
				theMessageIndex = 2005;
				goto IL_23F;
			case StoreItem.STORE_ITEM_PLANT_SPIKEROCK:
				theMessageIndex = 2006;
				goto IL_23F;
			case StoreItem.STORE_ITEM_PLANT_COBCANNON:
				theMessageIndex = 2007;
				goto IL_23F;
			case StoreItem.STORE_ITEM_PLANT_IMITATER:
				theMessageIndex = 2008;
				goto IL_23F;
			case StoreItem.STORE_ITEM_BONUS_LAWN_MOWER:
				theMessageIndex = 2009;
				goto IL_23F;
			case StoreItem.STORE_ITEM_POTTED_MARIGOLD_1:
				theMessageIndex = 2010;
				goto IL_23F;
			case StoreItem.STORE_ITEM_POTTED_MARIGOLD_2:
				theMessageIndex = 2010;
				goto IL_23F;
			case StoreItem.STORE_ITEM_POTTED_MARIGOLD_3:
				theMessageIndex = 2010;
				goto IL_23F;
			case StoreItem.STORE_ITEM_GOLD_WATERINGCAN:
				theMessageIndex = 2019;
				goto IL_23F;
			case StoreItem.STORE_ITEM_FERTILIZER:
				theMessageIndex = 2020;
				goto IL_23F;
			case StoreItem.STORE_ITEM_BUG_SPRAY:
				theMessageIndex = 2022;
				goto IL_23F;
			case StoreItem.STORE_ITEM_PHONOGRAPH:
				theMessageIndex = 2021;
				goto IL_23F;
			case StoreItem.STORE_ITEM_GARDENING_GLOVE:
				theMessageIndex = 2023;
				goto IL_23F;
			case StoreItem.STORE_ITEM_MUSHROOM_GARDEN:
				theMessageIndex = 2032;
				goto IL_23F;
			case StoreItem.STORE_ITEM_WHEEL_BARROW:
				theMessageIndex = 2024;
				goto IL_23F;
			case StoreItem.STORE_ITEM_STINKY_THE_SNAIL:
				theMessageIndex = 2025;
				goto IL_23F;
			case StoreItem.STORE_ITEM_PACKET_UPGRADE:
				if (this.mApp.mPlayerInfo.mPurchases[21] + 1 == 1)
				{
					theMessageIndex = 2011;
					goto IL_23F;
				}
				if (this.mApp.mPlayerInfo.mPurchases[21] + 1 == 2)
				{
					theMessageIndex = 2012;
					goto IL_23F;
				}
				if (this.mApp.mPlayerInfo.mPurchases[21] + 1 == 3)
				{
					theMessageIndex = 2013;
					goto IL_23F;
				}
				theMessageIndex = 2014;
				goto IL_23F;
			case StoreItem.STORE_ITEM_POOL_CLEANER:
				theMessageIndex = 2026;
				goto IL_23F;
			case StoreItem.STORE_ITEM_ROOF_CLEANER:
				theMessageIndex = 2027;
				goto IL_23F;
			case StoreItem.STORE_ITEM_RAKE:
				theMessageIndex = 2028;
				goto IL_23F;
			case StoreItem.STORE_ITEM_AQUARIUM_GARDEN:
				theMessageIndex = 2029;
				goto IL_23F;
			case StoreItem.STORE_ITEM_FIRSTAID:
				theMessageIndex = 2033;
				goto IL_23F;
			}
			Debug.ASSERT(false);
			IL_23F:
			string crazyDaveText = this.mApp.GetCrazyDaveText(theMessageIndex);
			int aQuantity = this.mApp.mPlayerInfo.mPurchases[21] + 7;
			ConfirmPurchaseDialog confirmPurchaseDialog = new ConfirmPurchaseDialog(this.mApp, theItemType, aQuantity, StoreScreen.GetItemCost(theItemType), crazyDaveText);
			confirmPurchaseDialog.Resize(Constants.StoreScreen_Dialog.X, Constants.StoreScreen_Dialog.Y, Constants.StoreScreen_Dialog.Width, Constants.StoreScreen_Dialog.Height);
			this.mApp.AddDialog(46, confirmPurchaseDialog);
		}

		public void PurchasePendingItem()
		{
			this.mApp.KillDialog(46);
			StoreItem storeItem = this.mPendingPurchaseItem;
			int itemCost = StoreScreen.GetItemCost(storeItem);
			this.mApp.mPlayerInfo.AddCoins(-itemCost);
			this.mApp.mPlayerInfo.mMoneySpent += itemCost;
			if (this.mApp.mPlayerInfo.mMoneySpent >= 2500)
			{
				ReportAchievement.GiveAchievement(AchievementId.Shopaholic);
			}
			if (storeItem == StoreItem.STORE_ITEM_PACKET_UPGRADE)
			{
				this.mApp.mPlayerInfo.mPurchases[21]++;
				string theDialogLines = Common.StrFormat_(TodStringFile.TodStringTranslate("[NOW_YOU_CAN_CHOOSE_X_SEEDS]"), 6 + this.mApp.mPlayerInfo.mPurchases[21]);
				LawnDialog lawnDialog = this.mApp.DoDialog(26, true, "[MORE_SLOTS]", theDialogLines, "[DIALOG_BUTTON_OK]", 3);
				lawnDialog.CalcSize(0, 0);
				if (this.mApp.mBoard != null)
				{
					this.mApp.mBoard.mSeedBank.UpdateHeight();
				}
			}
			else if (storeItem == StoreItem.STORE_ITEM_BONUS_LAWN_MOWER)
			{
				this.mApp.mPlayerInfo.mPurchases[9]++;
			}
			else if (storeItem == StoreItem.STORE_ITEM_RAKE)
			{
				this.mApp.mPlayerInfo.mPurchases[24] = 3;
			}
			else if (storeItem == StoreItem.STORE_ITEM_FERTILIZER || storeItem == StoreItem.STORE_ITEM_BUG_SPRAY)
			{
				if (this.mApp.mPlayerInfo.mPurchases[(int)storeItem] < 1000)
				{
					this.mApp.mPlayerInfo.mPurchases[(int)storeItem] = 1000;
				}
				this.mApp.mPlayerInfo.mPurchases[(int)storeItem] += 5;
			}
			else if (StoreScreen.IsPottedPlant(storeItem))
			{
				this.mApp.mZenGarden.AddPottedPlant(this.mPottedPlantSpecs);
				this.mPottedPlantSpecs.InitializePottedPlant(SeedType.SEED_MARIGOLD);
				this.mPottedPlantSpecs.mDrawVariation = (DrawVariation)TodCommon.RandRangeInt(2, 12);
				this.mApp.mPlayerInfo.mPurchases[(int)storeItem] = LawnCommon.GetCurrentDaysSince2000();
			}
			else
			{
				Debug.ASSERT(storeItem >= StoreItem.STORE_ITEM_PLANT_GATLINGPEA && storeItem < (StoreItem)80);
				this.mApp.mPlayerInfo.mPurchases[(int)storeItem] = 1;
			}
			if (storeItem == StoreItem.STORE_ITEM_FIRSTAID)
			{
				this.SetBubbleText(3400, 800, false);
			}
			if (this.mApp.mSeedChooserScreen != null)
			{
				this.mApp.mSeedChooserScreen.UpdateAfterPurchase();
			}
			int i;
			for (i = 0; i < 49; i++)
			{
				if (!this.mApp.HasSeedType((SeedType)i))
				{
					i = -1;
					break;
				}
			}
			if (i == 49 && !this.mApp.mPlayerInfo.mShownAchievements[1])
			{
				ReportAchievement.GiveAchievement(AchievementId.ACHIEVEMENT_MORTICULTURALIST);
				this.mApp.mPlayerInfo.mShownAchievements[1] = true;
				this.SetBubbleText(4000, 150, false);
				this.mBubbleAutoAdvance = true;
			}
			this.mApp.WriteCurrentUserConfig();
		}

		public void FinishTreeOfWisdomDialog(bool isYes)
		{
			this.mApp.KillDialog(47);
			if (this.mApp.mSeedChooserScreen != null)
			{
				this.mApp.mSeedChooserScreen.UpdateAfterPurchase();
			}
			this.mApp.WriteCurrentUserConfig();
			if (isYes)
			{
				this.mGoToTreeNow = true;
				this.mResult = 1000;
				if (this.mListener != null)
				{
					this.mListener.BackFromStore();
				}
			}
		}

		public void DrawItem(Graphics g, int theItemPosition, StoreItem theItemType)
		{
			if (this.IsItemUnavailable(theItemType))
			{
				return;
			}
			int aPosX = 0;
			int aPosY = 0;
			this.GetStorePosition(theItemPosition, ref aPosX, ref aPosY);
			GlobalMembersStoreScreen.DrawStoreItem(g, aPosX, aPosY, theItemType, this.IsComingSoon(theItemType), this.IsItemSoldOut(theItemType), this.mApp.mPlayerInfo.mPurchases[21] + 7, StoreScreen.GetItemCost(theItemType));
		}

		public void EnableButtons(bool theEnable)
		{
			bool flag = true;
			if (!this.mEasyBuyingCheat && !this.IsPageShown(StorePage.STORE_PAGE_PLANT_UPGRADES))
			{
				flag = false;
			}
			if (flag || !theEnable)
			{
				this.mNextButton.mMouseVisible = theEnable;
				this.mNextButton.SetDisabled(!theEnable);
				this.mPrevButton.mMouseVisible = theEnable;
				this.mPrevButton.SetDisabled(!theEnable);
			}
			this.mBackButton.mMouseVisible = theEnable;
			this.mBackButton.SetDisabled(!theEnable);
		}

		public void SetupForIntro(int theDialogIndex)
		{
			this.mStartDialog = theDialogIndex;
			this.mHatchOpen = false;
			this.SetupBackButtonForZenGarden();
			this.EnableButtons(false);
		}

		public void SetupBackButtonForZenGarden()
		{
			this.mBackButton.mButtonImage = AtlasResources.IMAGE_STORE_CONTINUEBUTTON;
			this.mBackButton.mDownImage = AtlasResources.IMAGE_STORE_CONTINUEBUTTONDOWN;
		}

		public bool CanInteractWithButtons()
		{
			return this.mStoreTime >= 120 && !this.mBubbleClickToContinue && !this.mBubbleAutoAdvance && this.mHatchTimer <= 0 && !this.IsWaitingForDialog();
		}

		public static bool IsPottedPlant(StoreItem theStoreItem)
		{
			return theStoreItem == StoreItem.STORE_ITEM_POTTED_MARIGOLD_1 || theStoreItem == StoreItem.STORE_ITEM_POTTED_MARIGOLD_2 || theStoreItem == StoreItem.STORE_ITEM_POTTED_MARIGOLD_3;
		}

		public void AdvanceCrazyDaveDialog()
		{
			if (!this.mBubbleClickToContinue && !this.mBubbleAutoAdvance)
			{
				return;
			}
			if (this.mApp.mCrazyDaveMessageIndex == 3100)
			{
				this.mHatchTimer = 150;
				this.mHatchOpen = true;
				this.mApp.PlaySample(Resources.SOUND_HATCHBACK_OPEN);
			}
			if (!this.mApp.AdvanceCrazyDaveText())
			{
				this.mApp.CrazyDaveStopTalking();
				this.mBubbleClickToContinue = false;
				this.mBubbleAutoAdvance = false;
				this.mAmbientSpeechCountDown = 500;
				if (this.mHatchTimer == 0)
				{
					this.EnableButtons(true);
				}
			}
			else
			{
				int theTime = 0;
				if (this.mApp.mCrazyDaveMessageIndex == 4001)
				{
					theTime = 200;
				}
				else if (this.mApp.mCrazyDaveMessageIndex == 4002)
				{
					theTime = 400;
				}
				else if (this.mApp.mCrazyDaveMessageIndex == 4003)
				{
					theTime = 150;
				}
				else if (this.mApp.mCrazyDaveMessageIndex == 4004)
				{
					theTime = 500;
				}
				this.SetBubbleText(this.mApp.mCrazyDaveMessageIndex, theTime, this.mBubbleClickToContinue);
			}
			if (this.mApp.mCrazyDaveMessageIndex == 303 || this.mApp.mCrazyDaveMessageIndex == 606 || this.mApp.mCrazyDaveMessageIndex == 2105 || this.mApp.mCrazyDaveMessageIndex == 2601)
			{
				this.mHatchTimer = 150;
				this.mHatchOpen = true;
				this.mApp.PlaySample(Resources.SOUND_HATCHBACK_OPEN);
			}
			if (this.mApp.mCrazyDaveMessageIndex == 603)
			{
				this.mApp.mPlayerInfo.AddCoins(100);
				this.mApp.mPlayerInfo.mNeedsMagicTacoReward = false;
				this.mApp.WriteCurrentUserConfig();
			}
			if (this.mApp.mCrazyDaveMessageIndex == 2103)
			{
				this.mApp.mPlayerInfo.mNeedsMagicBaconReward = false;
				this.mApp.WriteCurrentUserConfig();
				this.mApp.PlaySample(Resources.SOUND_DIAMOND);
				Coin coin = StoreScreen.mCoins.DataArrayAlloc();
				coin.CoinInitialize(80, 520, CoinType.COIN_DIAMOND, CoinMotion.COIN_MOTION_FROM_PRESENT);
				coin.mVelX = 0f;
				coin.mVelY = -5f;
			}
			if (this.mApp.mCrazyDaveMessageIndex == 902)
			{
				this.mApp.mPlayerInfo.AddCoins(100);
			}
			if (this.mApp.mCrazyDaveMessageIndex == 1002)
			{
				this.mApp.mPlayerInfo.AddCoins(100);
			}
		}

		public bool IsComingSoon(StoreItem theStoreItem)
		{
			return this.IsFullVersionOnly(theStoreItem) || (StoreScreen.IsPottedPlant(theStoreItem) && !this.mApp.HasFinishedAdventure());
		}

		public void StorePreLoad()
		{
			ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.REANIM_CRAZY_DAVE, true);
			this.mApp.CrazyDaveEnter();
			Plant.PreloadPlantResources(SeedType.SEED_GATLINGPEA);
			Plant.PreloadPlantResources(SeedType.SEED_TWINSUNFLOWER);
			if (this.mApp.HasFinishedAdventure())
			{
				Plant.PreloadPlantResources(SeedType.SEED_TWINSUNFLOWER);
				Plant.PreloadPlantResources(SeedType.SEED_GLOOMSHROOM);
				Plant.PreloadPlantResources(SeedType.SEED_CATTAIL);
				Plant.PreloadPlantResources(SeedType.SEED_WINTERMELON);
				Plant.PreloadPlantResources(SeedType.SEED_GOLD_MAGNET);
				Plant.PreloadPlantResources(SeedType.SEED_SPIKEROCK);
				Plant.PreloadPlantResources(SeedType.SEED_COBCANNON);
				Plant.PreloadPlantResources(SeedType.SEED_IMITATER);
			}
		}

		public bool IsPageShown(StorePage thePage)
		{
			if (this.mApp.IsTrialStageLocked())
			{
				return thePage == StorePage.STORE_PAGE_SLOT_UPGRADES;
			}
			return (thePage != StorePage.STORE_PAGE_ZEN1 || this.mApp.mPlayerInfo.mZenGardenTutorialComplete || this.mApp.mZenGarden.mIsTutorial) && (thePage != StorePage.STORE_PAGE_ZEN2 || this.mApp.mPlayerInfo.mZenGardenTutorialComplete || this.mApp.HasFinishedAdventure()) && (this.mApp.HasFinishedAdventure() || thePage != StorePage.STORE_PAGE_PLANT_UPGRADES || this.mApp.mPlayerInfo.mLevel >= 42);
		}

		public override void DrawOverlay(Graphics g)
		{
		}

		public bool IsFullVersionOnly(StoreItem theStoreItem)
		{
			if (this.mApp.IsTrialStageLocked())
			{
				if (theStoreItem == StoreItem.STORE_ITEM_PACKET_UPGRADE && this.mApp.mPlayerInfo.mPurchases[21] >= 2)
				{
					return true;
				}
				if (theStoreItem == StoreItem.STORE_ITEM_PLANT_TWINSUNFLOWER)
				{
					return true;
				}
			}
			return false;
		}

		public bool IsWaitingForDialog()
		{
			return this.mApp.GetDialog(Dialogs.DIALOG_STORE_PURCHASE) != null || this.mApp.GetDialog(Dialogs.DIALOG_NOT_ENOUGH_MONEY) != null || this.mApp.GetDialog(Dialogs.DIALOG_VISIT_TREE_OF_WISDOM) != null || this.mApp.GetDialog(Dialogs.DIALOG_UPGRADED) != null;
		}

		private const int aNumpicks = 4;

		public LawnApp mApp;

		public NewLawnButton mBackButton;

		public NewLawnButton mPrevButton;

		public NewLawnButton mNextButton;

		public Widget mOverlayWidget;

		public int mStoreTime;

		public string mBubbleText = string.Empty;

		public int mBubbleCountDown;

		public bool mBubbleClickToContinue;

		public bool mBubbleAutoAdvance;

		public int mAmbientSpeechCountDown;

		public int mPreviousAmbientSpeechIndex;

		public StorePage mPage;

		public StoreItem mMouseOverItem;

		public int mHatchTimer;

		public bool mHatchOpen;

		public int mShakeX;

		public int mShakeY;

		public int mStartDialog;

		public bool mEasyBuyingCheat;

		public PottedPlant mPottedPlantSpecs = new PottedPlant();

		public static DataArray<Coin> mCoins = new DataArray<Coin>();

		public bool mDrawnOnce;

		public bool mGoToTreeNow;

		public bool mPurchasedFullVersion;

		public bool mTrialLockedWhenStoreOpened;

		public StoreListener mListener;

		public StoreItem mPendingPurchaseItem;

		private static TodWeightedArray[] aPickArray = new TodWeightedArray[4];
	}
}
