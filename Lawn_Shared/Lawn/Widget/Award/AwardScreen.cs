using System;
using System.Collections.Generic;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
	internal class AwardScreen : Widget, AlmanacListener, StoreListener, ButtonListener
	{
		public AwardScreen(LawnApp theApp, AwardType theAwardType, bool theShowAchievements)
		{
			theShowAchievements = false;
			this.mApp = theApp;
			this.mClip = false;
			this.mFadeInCounter = 180;
			this.mAwardType = theAwardType;
			this.mShowingAchievements = theShowAchievements;
			this.mAchievementAnimTime = 0;
			this.mApp.DelayLoadMainMenuResource(false);
			if (this.mShowingAchievements)
			{
				for (int i = 0; i < 18; i++)
				{
					if (this.mApp.mPlayerInfo.mEarnedAchievements[i] && !this.mApp.mPlayerInfo.mShownAchievements[i])
					{
						this.mApp.mPlayerInfo.mShownAchievements[i] = true;
						AwardScreen.AchievementScreenItem achievementScreenItem = new AwardScreen.AchievementScreenItem();
						achievementScreenItem.mId = (AchievementId)i;
						achievementScreenItem.mStartAnimTime = 100 * this.mAchievementItems.Count + 50;
						achievementScreenItem.mEndAnimTime = achievementScreenItem.mStartAnimTime + 100;
						achievementScreenItem.mStartY = Constants.BOARD_HEIGHT;
						achievementScreenItem.mY = achievementScreenItem.mStartY;
						this.mAchievementItems.Add(achievementScreenItem);
					}
				}
				if (this.mAchievementItems.Count == 0)
				{
					this.mShowingAchievements = false;
				}
				else
				{
					int num = 114 - this.mAchievementItems.Count * 76 / 2 + 20;
					for (int j = 0; j < this.mAchievementItems.Count; j++)
					{
						this.mAchievementItems[j].mDestY = num;
						num += 76;
					}
				}
				this.mApp.WriteCurrentUserConfig();
			}
			int level = this.mApp.mPlayerInfo.GetLevel();
			if (this.mAwardType == AwardType.AWARD_CREDITS_ZOMBIE_NOTE)
			{
				this.mApp.DelayLoadBackgroundResource("DelayLoad_Background6");
				this.mApp.DelayLoadZombieNotePaperResource("DelayLoad_ZombieNote");
				this.mApp.DelayLoadZombieNoteResource("DelayLoad_Credits");
			}
			else if (this.mAwardType == AwardType.AWARD_HELP_ZOMBIE_NOTE)
			{
				this.mApp.DelayLoadBackgroundResource("DelayLoad_Background1");
				this.mApp.DelayLoadZombieNotePaperResource("DelayLoad_ZombieNote");
				this.mApp.DelayLoadZombieNoteResource("DelayLoad_ZombieNoteHelp");
			}
			else if (this.mApp.IsAdventureMode())
			{
				if (level == 10)
				{
					this.mApp.DelayLoadBackgroundResource("DelayLoad_Background1");
					this.mApp.DelayLoadZombieNotePaperResource("DelayLoad_ZombieNote");
					this.mApp.DelayLoadZombieNoteResource("DelayLoad_ZombieNote1");
				}
				else if (level == 20)
				{
					this.mApp.DelayLoadBackgroundResource("DelayLoad_Background2");
					this.mApp.DelayLoadZombieNotePaperResource("DelayLoad_ZombieNote");
					this.mApp.DelayLoadZombieNoteResource("DelayLoad_ZombieNote2");
				}
				else if (level == 30)
				{
					this.mApp.DelayLoadBackgroundResource("DelayLoad_Background1");
					this.mApp.DelayLoadZombieNotePaperResource("DelayLoad_ZombieNote");
					this.mApp.DelayLoadZombieNoteResource("DelayLoad_ZombieNote3");
				}
				else if (level == 40)
				{
					this.mApp.DelayLoadBackgroundResource("DelayLoad_Background2");
					this.mApp.DelayLoadZombieNotePaperResource("DelayLoad_ZombieNote");
					this.mApp.DelayLoadZombieNoteResource("DelayLoad_ZombieNote4");
				}
				else if (level == 50)
				{
					this.mApp.DelayLoadBackgroundResource("DelayLoad_Background1");
					this.mApp.DelayLoadZombieNotePaperResource("DelayLoad_ZombieNote");
					this.mApp.DelayLoadZombieNoteResource("DelayLoad_ZombieFinalNote");
				}
			}
			this.mStartButton = new GameButton(100, this);
			this.mStartButton.mButtonImage = AtlasResources.IMAGE_SEEDCHOOSER_BUTTON;
			this.mStartButton.mOverImage = null;
			this.mStartButton.mDownImage = null;
			this.mStartButton.mDisabledImage = AtlasResources.IMAGE_SEEDCHOOSER_BUTTON_DISABLED;
			this.mStartButton.mOverOverlayImage = AtlasResources.IMAGE_SEEDCHOOSER_BUTTON_GLOW;
			this.mStartButton.SetFont(Resources.FONT_DWARVENTODCRAFT15);
			this.mStartButton.mColors[0] = new SexyColor(213, 159, 43);
			this.mStartButton.mColors[1] = new SexyColor(213, 159, 43);
			this.mContinueButton = new GameButton(100, this);
			this.mContinueButton.mButtonImage = AtlasResources.IMAGE_SEEDCHOOSER_BUTTON;
			this.mContinueButton.mOverImage = null;
			this.mContinueButton.mDownImage = null;
			this.mContinueButton.mDisabledImage = AtlasResources.IMAGE_SEEDCHOOSER_BUTTON_DISABLED;
			this.mContinueButton.mOverOverlayImage = AtlasResources.IMAGE_SEEDCHOOSER_BUTTON_GLOW;
			this.mContinueButton.SetFont(Resources.FONT_DWARVENTODCRAFT15);
			this.mContinueButton.mColors[0] = new SexyColor(213, 159, 43);
			this.mContinueButton.mColors[1] = new SexyColor(213, 159, 43);
			this.mContinueButton.mTextOffsetY = -1;
			this.mContinueButton.mParentWidget = this;
			this.mContinueButton.mBtnNoDraw = true;
			this.mContinueButton.mDisabled = true;
			this.mMenuButton = new GameButton(101, this);
			this.mMenuButton.SetLabel("[AWARD_MAIN_MENU_BUTTON]");
			this.mMenuButton.mButtonImage = AtlasResources.IMAGE_SEEDCHOOSER_BUTTON2;
			this.mMenuButton.mOverImage = AtlasResources.IMAGE_SEEDCHOOSER_BUTTON2_GLOW;
			this.mMenuButton.mDownImage = null;
			this.mMenuButton.SetFont(Resources.FONT_BRIANNETOD12);
			this.mMenuButton.mColors[0] = new SexyColor(42, 42, 90);
			this.mMenuButton.mColors[1] = new SexyColor(42, 42, 90);
			this.mMenuButton.mParentWidget = this;
			this.mMenuButton.mTextOffsetY = (int)Constants.InvertAndScale(2f);
			if (this.mApp.IsFirstTimeAdventureMode() && level <= 3)
			{
				this.mMenuButton.mBtnNoDraw = true;
				this.mMenuButton.mDisabled = true;
			}
			this.mCreditsButton = null;
			if (this.mAwardType == AwardType.AWARD_CREDITS_ZOMBIE_NOTE)
			{
				this.mCreditsButton = GameButton.MakeNewButton(100, this, "[ROLL_CREDITS]", Resources.FONT_HOUSEOFTERROR16, AtlasResources.IMAGE_CREDITS_PLAYBUTTON, null, null);
				this.mCreditsButton.mTextDownOffsetX = 1;
				this.mCreditsButton.mTextDownOffsetY = 1;
				this.mCreditsButton.mColors[0] = new SexyColor(255, 255, 255);
				this.mCreditsButton.mColors[1] = new SexyColor(213, 159, 43);
				this.mCreditsButton.Resize(Constants.AwardScreen_CreditsButton.mX, Constants.AwardScreen_CreditsButton.mY, Constants.AwardScreen_CreditsButton.mWidth, Constants.AwardScreen_CreditsButton.mHeight);
				this.mCreditsButton.mTextOffsetX = Constants.AwardScreen_CreditsButton_Offset.X;
				this.mCreditsButton.mTextOffsetY = Constants.AwardScreen_CreditsButton_Offset.Y;
				this.AddWidget(this.mCreditsButton);
				this.mStartButton.mBtnNoDraw = true;
				this.mStartButton.mDisabled = true;
			}
			else if (this.mAwardType == AwardType.AWARD_HELP_ZOMBIE_NOTE)
			{
				this.mStartButton.SetLabel("[MAIN_MENU_BUTTON]");
				this.mMenuButton.mBtnNoDraw = true;
				this.mMenuButton.mDisabled = true;
			}
			else if (!this.mApp.IsAdventureMode())
			{
				this.mStartButton.SetLabel("[MAIN_MENU_BUTTON]");
				this.mMenuButton.mBtnNoDraw = true;
				this.mMenuButton.mDisabled = true;
			}
			else if (level == 1 && this.mApp.HasFinishedAdventure())
			{
				this.mStartButton.SetLabel("[CONTINUE_BUTTON]");
				this.mMenuButton.mBtnNoDraw = true;
				this.mMenuButton.mDisabled = true;
			}
			else if (level == 15)
			{
				this.mStartButton.SetLabel("[VIEW_ALMANAC_BUTTON]");
			}
			else if (level == 25 || level == 35 || level == 45)
			{
				this.mStartButton.SetLabel("[CONTINUE_BUTTON]");
			}
			else
			{
				this.mStartButton.SetLabel("[NEXT_LEVEL_BUTTON]");
			}
			if (this.mApp.IsFirstTimeAdventureMode() && level == 25 && this.mApp.IsTrialStageLocked() && !this.mApp.mPlayerInfo.mHasSeenUpsell)
			{
				this.mMenuButton.mBtnNoDraw = true;
				this.mMenuButton.mDisabled = true;
			}
			if (this.mShowingAchievements)
			{
				this.mShowStartButtonAfterAchievements = !this.mStartButton.mBtnNoDraw;
				this.mShowMenuButtonAfterAchievements = !this.mMenuButton.mBtnNoDraw;
				this.mStartButton.mBtnNoDraw = true;
				this.mStartButton.mDisabled = true;
				this.mMenuButton.mBtnNoDraw = true;
				this.mMenuButton.mDisabled = true;
				this.mContinueButton.SetLabel("[CONTINUE_BUTTON]");
			}
			if (this.IsPaperNote())
			{
				this.mApp.mMusic.StopAllMusic();
				this.mStartButton.mY += 20;
				this.mMenuButton.mY += 20;
				this.mApp.PlayFoley(FoleyType.FOLEY_PAPER);
				return;
			}
			this.mApp.mMusic.StopAllMusic();
		}

		public override void Resize(int theX, int theY, int theWidth, int theHeight)
		{
			base.Resize(theX, theY, theWidth, theHeight);
			int width = this.mStartButton.mButtonImage.GetWidth();
			int height = this.mStartButton.mButtonImage.GetHeight();
			this.mStartButton.Resize(this.mWidth / 2 - width / 2, this.mHeight - height - (int)(Constants.S * (float)Constants.AwardScreen_ContinueButton_Offset), width, height);
			this.mStartButton.mTextOffsetY = (int)Constants.InvertAndScale(1f);
			this.mStartButton.mParentWidget = this;
			width = this.mContinueButton.mButtonImage.GetWidth();
			height = this.mContinueButton.mButtonImage.GetHeight();
			this.mContinueButton.Resize(this.mWidth / 2 - width / 2, this.mHeight - height - (int)(Constants.S * (float)Constants.AwardScreen_ContinueButton_Offset), width, height);
			this.mMenuButton.Resize(Constants.AwardScreen_MenuButton.X, Constants.AwardScreen_MenuButton.Y, AtlasResources.IMAGE_SEEDCHOOSER_BUTTON2.mWidth, AtlasResources.IMAGE_SEEDCHOOSER_BUTTON2.mHeight);
		}

		public override void Dispose()
		{
			this.mApp.DelayLoadBackgroundResource(string.Empty);
			this.mApp.DelayLoadZombieNotePaperResource(string.Empty);
			this.mApp.DelayLoadZombieNoteResource(string.Empty);
			this.mStartButton.Dispose();
			this.mMenuButton.Dispose();
			this.RemoveAllWidgets();
		}

		public override bool BackButtonPress()
		{
			if (this.mAwardType == AwardType.AWARD_HELP_ZOMBIE_NOTE)
			{
				this.mApp.KillAwardScreen();
				this.mApp.ShowGameSelectorWithOptions();
				return true;
			}
			if (this.mAwardType == AwardType.AWARD_FOR_LEVEL || this.mAwardType == AwardType.AWARD_PRE_CREDITS_ZOMBIE_NOTE || this.mAwardType == AwardType.AWARD_CREDITS_ZOMBIE_NOTE)
			{
				this.mApp.KillAwardScreen();
				this.mApp.ShowGameSelector();
				return true;
			}
			this.ButtonPress(this.mStartButton.mId);
			this.ButtonDepress(this.mStartButton.mId);
			return true;
		}

		public override void Update()
		{
			base.Update();
			if (this.mShowingAchievements)
			{
				this.mAchievementAnimTime++;
				for (int i = 0; i < this.mAchievementItems.Count; i++)
				{
					if (this.mAchievementAnimTime >= this.mAchievementItems[i].mStartAnimTime)
					{
						bool flag = this.mAchievementItems[i].mY == this.mAchievementItems[i].mDestY;
						this.mAchievementItems[i].mY = TodCommon.TodAnimateCurve(this.mAchievementItems[i].mStartAnimTime, this.mAchievementItems[i].mEndAnimTime, this.mAchievementAnimTime, this.mAchievementItems[i].mStartY, this.mAchievementItems[i].mDestY, TodCurves.CURVE_EASE_IN_OUT);
						if (!flag && this.mAchievementItems[i].mY == this.mAchievementItems[i].mDestY)
						{
							this.mApp.PlaySample(Resources.SOUND_ACHIEVEMENT);
						}
					}
				}
				if (this.mAchievementItems[this.mAchievementItems.Count - 1].mY == this.mAchievementItems[this.mAchievementItems.Count - 1].mDestY)
				{
					this.mContinueButton.mBtnNoDraw = false;
					this.mContinueButton.mDisabled = false;
				}
			}
			if (this.mApp.GetDialogCount() > 0)
			{
				return;
			}
			this.mStartButton.Update();
			this.mMenuButton.Update();
			this.mContinueButton.Update();
			if (this.mFadeInCounter > 0)
			{
				this.mFadeInCounter -= 3;
				if (this.mFadeInCounter < 0)
				{
					this.mFadeInCounter = 0;
				}
			}
		}

		public override void Draw(Graphics g)
		{
			if (this.mApp.GetDialog(Dialogs.DIALOG_STORE) != null)
			{
				return;
			}
			int level = this.mApp.mPlayerInfo.GetLevel();
			g.SetLinearBlend(true);
			if (this.mShowingAchievements)
			{
				this.DrawAchievements(g);
			}
			else if (this.mAwardType == AwardType.AWARD_CREDITS_ZOMBIE_NOTE)
			{
				g.SetColor(new SexyColor(125, 200, 255, 255));
				g.SetColorizeImages(true);
				g.DrawImage(Resources.IMAGE_BACKGROUND6BOSS, Constants.AwardScreen_Note_Credits_Background.X, Constants.AwardScreen_Note_Credits_Background.Y, Constants.AwardScreen_Note_Credits_Background.Width, Constants.AwardScreen_Note_Credits_Background.Height);
				g.SetColorizeImages(false);
				g.DrawImage(Resources.IMAGE_ZOMBIE_NOTE, Constants.AwardScreen_Note_Credits_Paper.X, Constants.AwardScreen_Note_Credits_Paper.Y);
				g.DrawImage(Resources.IMAGE_CREDITS_ZOMBIENOTE, Constants.AwardScreen_Note_Credits_Text.X, Constants.AwardScreen_Note_Credits_Text.Y, Constants.AwardScreen_Note_Credits_Text.Width, Constants.AwardScreen_Note_Credits_Text.Height);
			}
			else if (this.mAwardType == AwardType.AWARD_HELP_ZOMBIE_NOTE)
			{
				g.DrawImage(Resources.IMAGE_BACKGROUND1, Constants.AwardScreen_Note_Help_Background.X, Constants.AwardScreen_Note_Help_Background.Y, Constants.AwardScreen_Note_Help_Background.Width, Constants.AwardScreen_Note_Help_Background.Height);
				g.DrawImage(Resources.IMAGE_ZOMBIE_NOTE, Constants.AwardScreen_Note_Help_Paper.X, Constants.AwardScreen_Note_Help_Paper.Y);
				g.DrawImage(Resources.IMAGE_ZOMBIE_NOTE_HELP, Constants.AwardScreen_Note_Help_Text.X, Constants.AwardScreen_Note_Help_Text.Y);
			}
			else if (this.mAwardType != AwardType.AWARD_ACHIEVEMENT_ONLY)
			{
				if (!this.mApp.IsAdventureMode())
				{
					if (this.mApp.EarnedGoldTrophy())
					{
						this.DrawBottom(g, "[BEAT_GAME_MESSAGE1]", "[GOLD_SUNFLOWER_TROPHY]", "[BEAT_GAME_MESSAGE2]");
					}
					else if (this.mApp.IsSurvivalMode())
					{
						int numTrophies = this.mApp.GetNumTrophies(ChallengePage.CHALLENGE_PAGE_SURVIVAL);
						if (numTrophies <= 7)
						{
							this.DrawBottom(g, "[GOT_TROPHY]", "[TROPHY]", "[YOU_UNLOCKED_A_SURVIVAL]");
							g.DrawImage(AtlasResources.IMAGE_TROPHY_HI_RES, Constants.AwardScreen_Trophy.X - AtlasResources.IMAGE_TROPHY_HI_RES.mWidth / 2, Constants.AwardScreen_Trophy.Y);
						}
						else if (numTrophies == 10)
						{
							this.DrawBottom(g, "[GOT_TROPHY]", "[TROPHY]", "[YOU_UNLOCKED_ENDLESS_SURVIVAL]");
							g.DrawImage(AtlasResources.IMAGE_TROPHY_HI_RES, Constants.AwardScreen_Trophy.X - AtlasResources.IMAGE_TROPHY_HI_RES.mWidth / 2, Constants.AwardScreen_Trophy.Y);
						}
						else
						{
							this.DrawBottom(g, "[GOT_TROPHY]", "[TROPHY]", "[EARN_MORE_TROPHIES_FOR_ENDLESS_SURVIVAL]");
							g.DrawImage(AtlasResources.IMAGE_TROPHY_HI_RES, Constants.AwardScreen_Trophy.X - AtlasResources.IMAGE_TROPHY_HI_RES.mWidth / 2, Constants.AwardScreen_Trophy.Y);
						}
					}
					else if (this.mApp.IsScaryPotterLevel())
					{
						this.DrawBottom(g, "[GOT_TROPHY]", "[TROPHY]", "[UNLOCKED_VASEBREAKER_LEVEL]");
						g.DrawImage(AtlasResources.IMAGE_TROPHY_HI_RES, Constants.AwardScreen_Trophy.X - AtlasResources.IMAGE_TROPHY_HI_RES.mWidth / 2, Constants.AwardScreen_Trophy.Y);
					}
					else if (this.mApp.IsPuzzleMode())
					{
						this.DrawBottom(g, "[GOT_TROPHY]", "[TROPHY]", "[UNLOCKED_I_ZOMBIE_LEVEL]");
						g.DrawImage(AtlasResources.IMAGE_TROPHY_HI_RES, Constants.AwardScreen_Trophy.X - AtlasResources.IMAGE_TROPHY_HI_RES.mWidth / 2, Constants.AwardScreen_Trophy.Y);
					}
					else
					{
						int numTrophies2 = this.mApp.GetNumTrophies(ChallengePage.CHALLENGE_PAGE_CHALLENGE);
						if (numTrophies2 <= 17)
						{
							this.DrawBottom(g, "[GOT_TROPHY]", "[TROPHY]", "[CHALLENGE_UNLOCKED]");
							g.DrawImage(AtlasResources.IMAGE_TROPHY_HI_RES, Constants.AwardScreen_Trophy.X - AtlasResources.IMAGE_TROPHY_HI_RES.mWidth / 2, Constants.AwardScreen_Trophy.Y);
						}
						else
						{
							this.DrawBottom(g, "[GOT_TROPHY]", "[TROPHY]", "[GET_MORE_TROPHIES]");
							g.DrawImage(AtlasResources.IMAGE_TROPHY_HI_RES, Constants.AwardScreen_Trophy.X - AtlasResources.IMAGE_TROPHY_HI_RES.mWidth / 2, Constants.AwardScreen_Trophy.Y);
						}
					}
				}
				else if (level == 5)
				{
					this.DrawBottom(g, "[GOT_SHOVEL]", "[SHOVEL]", "[SHOVEL_DESCRIPTION]");
					g.DrawImage(AtlasResources.IMAGE_SHOVEL_HI_RES, Constants.AwardScreen_Shovel.X - AtlasResources.IMAGE_SHOVEL_HI_RES.mWidth / 2, Constants.AwardScreen_Shovel.Y);
				}
				else if (level == 10)
				{
					g.DrawImage(Resources.IMAGE_BACKGROUND1, Constants.AwardScreen_Note_1_Background.X, Constants.AwardScreen_Note_1_Background.Y, Constants.AwardScreen_Note_1_Background.Width, Constants.AwardScreen_Note_1_Background.Height);
					g.DrawImage(Resources.IMAGE_ZOMBIE_NOTE, Constants.AwardScreen_Note_1_Paper.X, Constants.AwardScreen_Note_1_Paper.Y);
					g.DrawImage(Resources.IMAGE_ZOMBIE_NOTE1, Constants.AwardScreen_Note_1_Text.X, Constants.AwardScreen_Note_1_Text.Y);
					TodCommon.TodDrawString(g, "[FOUND_NOTE]", Constants.AwardScreen_Note_Message.X, Constants.AwardScreen_Note_Message.Y, Resources.FONT_DWARVENTODCRAFT15, new SexyColor(255, 200, 0, 255), DrawStringJustification.DS_ALIGN_CENTER);
				}
				else if (level == 15)
				{
					this.DrawBottom(g, "[FOUND_SUBURBAN_ALMANAC]", "[SUBURBAN_ALMANAC]", "[SUBURBAN_ALMANAC_DESCRIPTION]");
					g.DrawImage(AtlasResources.IMAGE_ALMANAC, Constants.AwardScreen_Almanac.X - AtlasResources.IMAGE_ALMANAC.mWidth / 2, Constants.AwardScreen_Almanac.Y);
				}
				else if (level == 20)
				{
					g.DrawImage(Resources.IMAGE_BACKGROUND2, Constants.AwardScreen_Note_2_Background.X, Constants.AwardScreen_Note_2_Background.Y, Constants.AwardScreen_Note_2_Background.Width, Constants.AwardScreen_Note_2_Background.Height);
					g.DrawImage(Resources.IMAGE_ZOMBIE_NOTE, Constants.AwardScreen_Note_2_Paper.X, Constants.AwardScreen_Note_2_Paper.Y);
					g.DrawImage(Resources.IMAGE_ZOMBIE_NOTE2, Constants.AwardScreen_Note_2_Text.X, Constants.AwardScreen_Note_2_Text.Y);
					TodCommon.TodDrawString(g, "[FOUND_NOTE]", Constants.AwardScreen_Note_Message.X, Constants.AwardScreen_Note_Message.Y, Resources.FONT_DWARVENTODCRAFT15, new SexyColor(255, 200, 0, 255), DrawStringJustification.DS_ALIGN_CENTER);
				}
				else if (level == 25)
				{
					this.DrawBottom(g, "[FOUND_KEYS]", "[KEYS]", "[KEYS_DESCRIPTION]");
					g.DrawImage(AtlasResources.IMAGE_CARKEYS, Constants.AwardScreen_CarKeys.X - AtlasResources.IMAGE_CARKEYS.mWidth / 2, Constants.AwardScreen_CarKeys.Y);
				}
				else if (level == 30)
				{
					g.DrawImage(Resources.IMAGE_BACKGROUND1, Constants.AwardScreen_Note_3_Background.X, Constants.AwardScreen_Note_3_Background.Y, Constants.AwardScreen_Note_3_Background.Width, Constants.AwardScreen_Note_3_Background.Height);
					g.DrawImage(Resources.IMAGE_ZOMBIE_NOTE, Constants.AwardScreen_Note_3_Paper.X, Constants.AwardScreen_Note_3_Paper.Y);
					g.DrawImage(Resources.IMAGE_ZOMBIE_NOTE3, Constants.AwardScreen_Note_3_Text.X, Constants.AwardScreen_Note_3_Text.Y);
					TodCommon.TodDrawString(g, "[FOUND_NOTE]", Constants.AwardScreen_Note_Message.X, Constants.AwardScreen_Note_Message.Y, Resources.FONT_DWARVENTODCRAFT15, new SexyColor(255, 200, 0, 255), DrawStringJustification.DS_ALIGN_CENTER);
				}
				else if (level == 35)
				{
					this.DrawBottom(g, "[FOUND_TACO]", "[TACO]", "[TACO_DESCRIPTION]");
					g.DrawImage(AtlasResources.IMAGE_TACO, Constants.AwardScreen_Taco.X - AtlasResources.IMAGE_TACO.mWidth / 2, Constants.AwardScreen_Taco.Y);
				}
				else if (level == 40)
				{
					g.DrawImage(Resources.IMAGE_BACKGROUND2, Constants.AwardScreen_Note_4_Background.X, Constants.AwardScreen_Note_4_Background.Y, Constants.AwardScreen_Note_4_Background.Width, Constants.AwardScreen_Note_4_Background.Height);
					g.DrawImage(Resources.IMAGE_ZOMBIE_NOTE, Constants.AwardScreen_Note_4_Paper.X, Constants.AwardScreen_Note_4_Paper.Y);
					g.DrawImage(Resources.IMAGE_ZOMBIE_NOTE4, Constants.AwardScreen_Note_4_Text.X, Constants.AwardScreen_Note_4_Text.Y);
					TodCommon.TodDrawString(g, "[FOUND_NOTE]", Constants.AwardScreen_Note_Message.X, Constants.AwardScreen_Note_Message.Y, Resources.FONT_DWARVENTODCRAFT15, new SexyColor(255, 200, 0, 255), DrawStringJustification.DS_ALIGN_CENTER);
				}
				else if (level == 45)
				{
					this.DrawBottom(g, "[FOUND_WATERING_CAN]", "[WATERING_CAN]", "[WATERING_CAN_DESCRIPTION]");
					g.DrawImage(AtlasResources.IMAGE_REANIM_ZENGARDEN_WATERINGCAN1, Constants.AwardScreen_WateringCan.X - AtlasResources.IMAGE_REANIM_ZENGARDEN_WATERINGCAN1.mWidth / 2, Constants.AwardScreen_WateringCan.Y);
				}
				else if (level == 50)
				{
					g.DrawImage(Resources.IMAGE_BACKGROUND1, Constants.AwardScreen_Note_Final_Background.X, Constants.AwardScreen_Note_Final_Background.Y, Constants.AwardScreen_Note_Final_Background.Width, Constants.AwardScreen_Note_Final_Background.Height);
					g.DrawImage(Resources.IMAGE_ZOMBIE_NOTE, Constants.AwardScreen_Note_Final_Paper.X, Constants.AwardScreen_Note_Final_Paper.Y);
					g.DrawImage(Resources.IMAGE_ZOMBIE_FINAL_NOTE, Constants.AwardScreen_Note_Final_Text.X, Constants.AwardScreen_Note_Final_Text.Y);
					TodCommon.TodDrawString(g, "[FOUND_NOTE]", Constants.AwardScreen_Note_Message.X, Constants.AwardScreen_Note_Message.Y, Resources.FONT_DWARVENTODCRAFT15, new SexyColor(255, 200, 0, 255), DrawStringJustification.DS_ALIGN_CENTER);
				}
				else if ((level == 1 && this.mApp.HasFinishedAdventure()) || this.mAwardType == AwardType.AWARD_PRE_CREDITS_ZOMBIE_NOTE)
				{
					this.DrawBottom(g, "[WIN_MESSAGE1]", "", "[WIN_MESSAGE2]");
				}
				else
				{
					this.DrawAwardSeed(g);
				}
			}
			this.mStartButton.Draw(g);
			this.mMenuButton.Draw(g);
			this.mContinueButton.Draw(g);
			base.DeferOverlay();
		}

		public override void DrawOverlay(Graphics g)
		{
			base.DrawOverlay(g);
			int theAlpha = TodCommon.TodAnimateCurve(180, 0, this.mFadeInCounter, 255, 0, TodCurves.CURVE_LINEAR);
			if (this.IsPaperNote())
			{
				g.SetColor(new SexyColor(0, 0, 0, theAlpha, false));
			}
			else
			{
				g.SetColor(new SexyColor(255, 255, 255, theAlpha, false));
			}
			g.SetColorizeImages(true);
			g.FillRect(0, 0, Constants.BOARD_WIDTH, Constants.BOARD_HEIGHT);
		}

		public virtual void KeyChar(char theChar)
		{
			if (theChar == ' ' || theChar == '\r' || theChar == '\u001b')
			{
				this.StartButtonPressed();
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
		}

		public override void MouseUp(int x, int y, int theClickCount)
		{
			if (theClickCount != 1)
			{
				return;
			}
			if (this.mStartButton.IsMouseOver())
			{
				this.StartButtonPressed();
			}
			if (this.mContinueButton.IsMouseOver())
			{
				this.AchievementsContinuePressed();
			}
			if (this.mMenuButton.IsMouseOver())
			{
				this.mApp.KillAwardScreen();
				this.mApp.ShowGameSelector();
			}
		}

		public override void MouseDown(int x, int y, int theClickCount)
		{
			if (theClickCount != 1)
			{
				return;
			}
			this.mStartButton.Update();
			this.mMenuButton.Update();
			this.mContinueButton.Update();
			if (this.mStartButton.IsMouseOver())
			{
				this.mApp.PlaySample(Resources.SOUND_TAP);
			}
			if (this.mMenuButton.IsMouseOver())
			{
				this.mApp.PlaySample(Resources.SOUND_TAP);
			}
			if (this.mContinueButton.IsMouseOver())
			{
				this.mApp.PlaySample(Resources.SOUND_TAP);
			}
		}

		public void DrawAwardSeed(Graphics g)
		{
			int level = this.mApp.mPlayerInfo.GetLevel();
			SeedType awardSeedForLevel = this.mApp.GetAwardSeedForLevel(level - 1);
			string nameString = Plant.GetNameString(awardSeedForLevel, SeedType.SEED_NONE);
			string theMessage = string.Empty;
			if (this.mApp.IsTrialStageLocked() && awardSeedForLevel >= SeedType.SEED_SQUASH && awardSeedForLevel != SeedType.SEED_TANGLEKELP)
			{
				theMessage = "[AVAILABLE_IN_FULL_VERSION]";
			}
			else
			{
				theMessage = Plant.GetToolTip(awardSeedForLevel);
			}
			this.DrawBottom(g, "[NEW_PLANT]", nameString, theMessage);
			int x = Constants.AwardScreen_Seed_Pos.X;
			int y = Constants.AwardScreen_Seed_Pos.Y;
			SeedPacket.DrawSmallSeedPacket(g, (float)x, (float)y, awardSeedForLevel, SeedType.SEED_NONE, 0f, 255, true, false, true, true);
		}

		public void DrawAchievements(Graphics g)
		{
			LawnCommon.DrawImageBox(g, new TRect(0, 0, this.mWidth, this.mHeight), AtlasResources.IMAGE_ALMANAC_ROUNDED_OUTLINE);
			for (int i = 0; i < this.mAchievementItems.Count; i++)
			{
				AchievementItem achievementItem = Achievements.GetAchievementItem(this.mAchievementItems[i].mId);
				g.DrawImage(achievementItem.AchievementImage, Constants.AwardScreen_AchievementImage_Pos.X, this.mAchievementItems[i].mY + Constants.AwardScreen_AchievementImage_Pos.Y);
				g.SetFont(Resources.FONT_DWARVENTODCRAFT15);
				g.SetColor(new SexyColor(224, 187, 98));
				string theString = TodCommon.TodReplaceString("[ACHIEVEMENT_EARNED]", "{ACHIEVEMENT}", this.mApp.GetAchievementName(this.mAchievementItems[i].mId));
				g.DrawString(theString, Constants.AwardScreen_AchievementName_Pos.X, this.mAchievementItems[i].mY + Constants.AwardScreen_AchievementName_Pos.Y);
				g.SetFont(Resources.FONT_DWARVENTODCRAFT12);
				g.SetColor(new SexyColor(255, 255, 255));
				TRect theRect = new TRect(Constants.AwardScreen_AchievementDescription_Rect.X, this.mAchievementItems[i].mY + Constants.AwardScreen_AchievementDescription_Rect.Y, Constants.AwardScreen_AchievementDescription_Rect.Width, Constants.AwardScreen_AchievementDescription_Rect.Height);
				g.WriteWordWrapped(theRect, this.mApp.GetAchievementDescription(this.mAchievementItems[i].mId), 0, -1, true);
			}
		}

		public void DrawBottom(Graphics g, string theTitle, string theAward, string theMessage)
		{
			LawnCommon.DrawImageBox(g, new TRect(0, 0, this.mWidth, this.mHeight), AtlasResources.IMAGE_ALMANAC_ROUNDED_OUTLINE);
			LawnCommon.DrawImageBox(g, Constants.AwardScreen_ClayTablet, AtlasResources.IMAGE_ALMANAC_CLAY_TABLET);
			TodCommon.TodDrawString(g, theTitle, Constants.AwardScreen_TitlePos.X, Constants.AwardScreen_TitlePos.Y, Resources.FONT_DWARVENTODCRAFT18, Constants.GreenFontColour, Constants.BOARD_WIDTH - 60, DrawStringJustification.DS_ALIGN_CENTER);
			TRect trect = default(TRect);
			if (!theAward.empty())
			{
				g.DrawImage(AtlasResources.IMAGE_ALMANAC_GROUNDDAY, Constants.AwardScreen_GroundDay_Pos.X, Constants.AwardScreen_GroundDay_Pos.Y);
				LawnCommon.DrawImageBox(g, Constants.AwardScreen_BrownRect, AtlasResources.IMAGE_ALMANAC_BROWN_RECT, false);
				TodCommon.TodDrawString(g, theAward, Constants.AwardScreen_BottomMessage_Pos.X, Constants.AwardScreen_BottomMessage_Pos.Y, Resources.FONT_DWARVENTODCRAFT18, Constants.YellowFontColour, DrawStringJustification.DS_ALIGN_CENTER);
				trect = Constants.AwardScreen_BottomText_Rect_Text;
			}
			else
			{
				trect = Constants.AwardScreen_BottomText_Rect_NoText;
			}
			g.SetColorizeImages(true);
			g.SetColor(new SexyColor(253, 186, 117));
			LawnCommon.DrawImageBox(g, trect, AtlasResources.IMAGE_ALMANAC_PAPER);
			g.SetColorizeImages(false);
			trect.Inflate((int)Constants.InvertAndScale(-6f), (int)Constants.InvertAndScale(-6f));
			TodStringFile.TodDrawStringWrapped(g, theMessage, trect, Resources.FONT_BRIANNETOD16, new SexyColor(40, 50, 90), DrawStringJustification.DS_ALIGN_CENTER_VERTICAL_MIDDLE, true);
		}

		public void StartButtonPressed()
		{
			if (this.mApp.GetDialog(4) != null)
			{
				return;
			}
			if (this.mAwardType == AwardType.AWARD_PRE_CREDITS_ZOMBIE_NOTE)
			{
				this.mApp.KillAwardScreen();
				this.mApp.ShowAwardScreen(AwardType.AWARD_CREDITS_ZOMBIE_NOTE, false);
				return;
			}
			if (this.mAwardType == AwardType.AWARD_CREDITS_ZOMBIE_NOTE)
			{
				this.mApp.KillAwardScreen();
				this.mApp.ShowCreditScreen();
				return;
			}
			if (this.mAwardType == AwardType.AWARD_HELP_ZOMBIE_NOTE)
			{
				this.mApp.KillAwardScreen();
				this.mApp.ShowGameSelector();
				return;
			}
			if (this.mApp.IsSurvivalMode())
			{
				this.mApp.KillAwardScreen();
				this.mApp.ShowChallengeScreen(ChallengePage.CHALLENGE_PAGE_SURVIVAL);
				return;
			}
			if (this.mApp.IsPuzzleMode())
			{
				this.mApp.KillAwardScreen();
				this.mApp.ShowChallengeScreen(ChallengePage.CHALLENGE_PAGE_PUZZLE);
				return;
			}
			if (this.mApp.IsChallengeMode())
			{
				this.mApp.KillAwardScreen();
				this.mApp.ShowChallengeScreen(ChallengePage.CHALLENGE_PAGE_CHALLENGE);
				return;
			}
			int level = this.mApp.mPlayerInfo.GetLevel();
			if (level == 15)
			{
				this.mApp.DoAlmanacDialog(SeedType.SEED_NONE, ZombieType.ZOMBIE_INVALID, this);
				return;
			}
			if (level == 25)
			{
				StoreScreen storeScreen = this.mApp.ShowStoreScreen(this);
				storeScreen.SetupForIntro(301);
				return;
			}
			if (level == 35)
			{
				StoreScreen storeScreen2 = this.mApp.ShowStoreScreen(this);
				storeScreen2.SetupForIntro(601);
				return;
			}
			if (level == 42)
			{
				StoreScreen storeScreen3 = this.mApp.ShowStoreScreen(this);
				storeScreen3.SetupForIntro(3100);
				return;
			}
			if (level == 45)
			{
				this.mApp.KillAwardScreen();
				this.mApp.WriteCurrentUserConfig();
				this.mApp.PreNewGame(GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN, false);
				this.mApp.mZenGarden.SetupForZenTutorial();
				return;
			}
			this.mApp.KillAwardScreen();
			this.mApp.PreNewGame(GameMode.GAMEMODE_ADVENTURE, false);
		}

		public void AchievementsContinuePressed()
		{
			if (this.mAwardType != AwardType.AWARD_ACHIEVEMENT_ONLY)
			{
				this.mStartButton.mBtnNoDraw = !this.mShowStartButtonAfterAchievements;
				this.mStartButton.mDisabled = !this.mShowStartButtonAfterAchievements;
				this.mMenuButton.mBtnNoDraw = !this.mShowMenuButtonAfterAchievements;
				this.mMenuButton.mDisabled = !this.mShowMenuButtonAfterAchievements;
				this.mContinueButton.mDisabled = true;
				this.mContinueButton.mBtnNoDraw = true;
				this.mShowingAchievements = false;
				int level = this.mApp.mPlayerInfo.GetLevel();
				if (level == 1 && this.mApp.HasFinishedAdventure())
				{
					this.mApp.KillAwardScreen();
					this.mApp.ShowAwardScreen(AwardType.AWARD_CREDITS_ZOMBIE_NOTE, false);
				}
				return;
			}
			this.mApp.KillAwardScreen();
			if (this.mApp.IsQuickPlayMode())
			{
				this.mApp.ShowGameSelectorQuickPlay(false);
				return;
			}
			this.mApp.PreNewGame(GameMode.GAMEMODE_ADVENTURE, false);
		}

		public bool IsPaperNote()
		{
			if (this.mAwardType == AwardType.AWARD_CREDITS_ZOMBIE_NOTE || this.mAwardType == AwardType.AWARD_HELP_ZOMBIE_NOTE)
			{
				return true;
			}
			if (this.mApp.IsAdventureMode())
			{
				int level = this.mApp.mPlayerInfo.GetLevel();
				if (level == 10 || level == 20 || level == 30 || level == 40 || level == 50)
				{
					return true;
				}
			}
			return false;
		}

		public virtual void ButtonPress(int theId)
		{
			this.mApp.PlaySample(Resources.SOUND_TAP);
		}

		public virtual void ButtonDepress(int theId)
		{
			this.StartButtonPressed();
		}

		public void BackFromAlmanac()
		{
			this.mApp.KillAwardScreen();
			this.mApp.PreNewGame(GameMode.GAMEMODE_ADVENTURE, false);
		}

		public void BackFromStore()
		{
			this.mApp.KillDialog(4);
			this.mApp.KillAwardScreen();
			this.mApp.PreNewGame(GameMode.GAMEMODE_ADVENTURE, false);
		}

		public void ButtonMouseMove(int id, int x, int y)
		{
		}

		public void ButtonMouseLeave(int id)
		{
		}

		public void ButtonMouseEnter(int id)
		{
		}

		public void ButtonMouseTick(int id)
		{
		}

		public void ButtonPress(int id, int id2)
		{
		}

		public void ButtonDownTick(int id)
		{
		}

		public NewLawnButton mCreditsButton;

		public GameButton mStartButton;

		public GameButton mMenuButton;

		public GameButton mContinueButton;

		public bool mShowStartButtonAfterAchievements;

		public bool mShowMenuButtonAfterAchievements;

		public LawnApp mApp;

		public int mFadeInCounter;

		public AwardType mAwardType;

		public int mAchievementAnimTime;

		public List<AwardScreen.AchievementScreenItem> mAchievementItems = new List<AwardScreen.AchievementScreenItem>();

		public bool mShowingAchievements;

		internal class AchievementScreenItem
		{
			public AchievementId mId;

			public int mStartAnimTime;

			public int mEndAnimTime;

			public int mDestY;

			public int mStartY;

			public int mY;
		}
	}
}
