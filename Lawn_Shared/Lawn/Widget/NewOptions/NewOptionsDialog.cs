using System;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
	internal class NewOptionsDialog : LawnDialog, SliderListener, CheckboxListener
	{
		public NewOptionsDialog(LawnApp theApp, bool theFromGameSelector) : base(theApp, null, 2, true, "[OPTIONS_DIALOG_TITLE]", "", "", 0)
		{
			this.mApp = theApp;
			this.mFromGameSelector = theFromGameSelector;
			this.mMusicSliderOn = this.mApp.mMusicEnabled;
			this.SetColor(3, new SexyColor(255, 255, 100));
			this.mAlmanacButton = GameButton.MakeButton(0, this, "[VIEW_ALMANAC_BUTTON]");
			this.mRestartButton = GameButton.MakeButton(2, this, "[RESTART_LEVEL_BUTTON]");
			this.mBackToMainButton = GameButton.MakeButton(1, this, "[MAIN_MENU_BUTTON]");
			this.mBackToGameButton = GameButton.MakeButton(1000, this, "[BACK_TO_GAME]");
			this.mHelpButton = GameButton.MakeButton(6, this, "[HELP]");
			this.mAboutButton = GameButton.MakeButton(7, this, "[ABOUT]");
			this.mMusicVolumeSlider = new Slider(AtlasResources.IMAGE_OPTIONS_SLIDERSLOT, AtlasResources.IMAGE_OPTIONS_SLIDERKNOB2, 4, this);
			double num = theApp.GetMusicVolume();
			num = Math.Max(0.0, Math.Min(1.0, num));
			this.mMusicVolumeSlider.SetValue(num);
			this.mSfxVolumeSlider = new Slider(AtlasResources.IMAGE_OPTIONS_SLIDERSLOT, AtlasResources.IMAGE_OPTIONS_SLIDERKNOB2, 5, this);
			this.mSfxVolumeSlider.SetValue(theApp.GetSfxVolume());
			this.mVibrateCheckbox = LawnCommon.MakeNewCheckbox(8, this, this.mApp.mPlayerInfo.mDoVibration);
			this.mRunWhileLocked = LawnCommon.MakeNewCheckbox(11, this, this.mApp.mPlayerInfo.mRunWhileLocked);
			this.mLinkCredits = new HyperlinkWidget(10, this);
			this.mLinkCredits.SetFont(Resources.FONT_BRIANNETOD12);
			this.mLinkCredits.mColor = new SexyColor(255, 255, 136);
			this.mLinkCredits.mOverColor = new SexyColor(0, 0, 255);
			this.mLinkCredits.mDoFinger = true;
			this.mLinkCredits.mLabel = TodStringFile.TodStringTranslate("[OPTIONS_CREDITS_LINK]");
			this.mLinkCredits.mUnderlineSize = 1;
			this.mLinkCredits.mUnderlineOffset = 1;
			if (this.mFromGameSelector)
			{
				this.mRestartButton.SetVisible(false);
				this.mBackToMainButton.SetVisible(false);
				this.mBackToGameButton.SetLabel("[DIALOG_BUTTON_OK]");
			}
			else
			{
				this.mTallBottom = (this.mApp.mGameScene != GameScenes.SCENE_LEVEL_INTRO);
				this.mLinkCredits.SetVisible(false);
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ICE || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN || this.mApp.mGameMode == GameMode.GAMEMODE_TREE_OF_WISDOM)
			{
				this.mRestartButton.SetVisible(false);
			}
			if (this.mApp.mGameScene == GameScenes.SCENE_LEVEL_INTRO && !this.mApp.mBoard.mCutScene.IsSurvivalRepick())
			{
				this.mRestartButton.SetVisible(false);
			}
			if (!this.mApp.CanShowAlmanac() || this.mApp.mGameScene == GameScenes.SCENE_LEVEL_INTRO || this.mFromGameSelector)
			{
				this.mAlmanacButton.SetVisible(false);
			}
			base.CalcSize(0, 0);
		}

		public override void Dispose()
		{
			this.mHelpButton.Dispose();
			this.mAboutButton.Dispose();
			this.mRunWhileLocked.Dispose();
			this.mMusicVolumeSlider.Dispose();
			this.mSfxVolumeSlider.Dispose();
			this.mVibrateCheckbox.Dispose();
			this.mAlmanacButton.Dispose();
			this.mRestartButton.Dispose();
			this.mBackToMainButton.Dispose();
			this.mBackToGameButton.Dispose();
			base.Dispose();
		}

		public override int GetPreferredHeight(int theWidth)
		{
			return (int)Constants.InvertAndScale((float)((this.mFromGameSelector || this.mApp.mGameScene == GameScenes.SCENE_LEVEL_INTRO) ? 310 : 340));
		}

		public override void AddedToManager(WidgetManager theWidgetManager)
		{
			base.AddedToManager(theWidgetManager);
			this.AddWidget(this.mAlmanacButton);
			this.AddWidget(this.mRestartButton);
			this.AddWidget(this.mBackToMainButton);
			this.AddWidget(this.mMusicVolumeSlider);
			this.AddWidget(this.mSfxVolumeSlider);
			this.AddWidget(this.mVibrateCheckbox);
			this.AddWidget(this.mRunWhileLocked);
			this.AddWidget(this.mBackToGameButton);
			this.AddWidget(this.mLinkCredits);
			if (this.mFromGameSelector)
			{
				this.AddWidget(this.mAboutButton);
				this.AddWidget(this.mHelpButton);
			}
		}

		public override void RemovedFromManager(WidgetManager theWidgetManager)
		{
			base.RemovedFromManager(theWidgetManager);
			this.RemoveWidget(this.mAlmanacButton);
			this.RemoveWidget(this.mMusicVolumeSlider);
			this.RemoveWidget(this.mSfxVolumeSlider);
			this.RemoveWidget(this.mVibrateCheckbox);
			this.RemoveWidget(this.mRunWhileLocked);
			this.RemoveWidget(this.mBackToMainButton);
			this.RemoveWidget(this.mBackToGameButton);
			this.RemoveWidget(this.mRestartButton);
			this.RemoveWidget(this.mLinkCredits);
			if (this.mFromGameSelector)
			{
				this.RemoveWidget(this.mAboutButton);
				this.RemoveWidget(this.mHelpButton);
			}
		}

		public override void Resize(int theX, int theY, int theWidth, int theHeight)
		{
			base.Resize(theX, theY, theWidth, theHeight);
			int mHeight = AtlasResources.IMAGE_BUTTON_LEFT.mHeight;
			int theX2 = (int)Constants.InvertAndScale(244f);
			this.mMusicVolumeSlider.Resize(theX2, (int)Constants.InvertAndScale(66f), (int)Constants.InvertAndScale(135f), (int)Constants.InvertAndScale(40f));
			this.mSfxVolumeSlider.Resize(theX2, (int)Constants.InvertAndScale(93f), (int)Constants.InvertAndScale(135f), (int)Constants.InvertAndScale(40f));
			this.mVibrateCheckbox.Resize(theX2, (int)Constants.InvertAndScale(125f), (int)Constants.InvertAndScale(46f), (int)Constants.InvertAndScale(45f));
			this.mRunWhileLocked.Resize(theX2, (int)Constants.InvertAndScale(195f), (int)Constants.InvertAndScale(46f), (int)Constants.InvertAndScale(45f));
			this.mMusicVolumeSlider.mY += (int)Constants.InvertAndScale(5f);
			this.mSfxVolumeSlider.mY += (int)Constants.InvertAndScale(15f);
			this.mVibrateCheckbox.mY += (int)Constants.InvertAndScale(25f);
			if (this.mFromGameSelector)
			{
				int num = (int)Constants.InvertAndScale(100f);
				this.mBackToGameButton.Resize(this.mWidth * 4 / 5 - num / 2, theHeight - (int)Constants.InvertAndScale(50f), num, mHeight);
				this.mHelpButton.Resize(this.mWidth / 2 - num / 2, theHeight - (int)Constants.InvertAndScale(50f), num, mHeight);
				this.mAboutButton.Resize(this.mWidth / 5 - num / 2, theHeight - (int)Constants.InvertAndScale(50f), num, mHeight);
				this.mLinkCredits.Resize(theWidth - (int)Constants.InvertAndScale(175f), theHeight - (int)Constants.InvertAndScale(90f), (int)Constants.InvertAndScale(190f), this.mLinkCredits.mFont.GetHeight() + (int)Constants.InvertAndScale(8f));
				this.mVersionY = Constants.NewOptionsDialog_Version_Low_Y;
				return;
			}
			if (this.mApp.mGameScene == GameScenes.SCENE_LEVEL_INTRO)
			{
				int num2 = (int)Constants.InvertAndScale(150f);
				this.mBackToGameButton.Resize(this.mWidth / 4 - num2 / 2, theHeight - (int)Constants.InvertAndScale(50f), num2, mHeight);
				this.mBackToMainButton.Resize(this.mWidth * 3 / 4 - num2 / 2, theHeight - (int)Constants.InvertAndScale(50f), num2, mHeight);
				this.mVersionY = Constants.NewOptionsDialog_Version_Low_Y;
				return;
			}
			int num3 = (int)Constants.InvertAndScale(160f);
			int theY2 = theHeight - (int)Constants.InvertAndScale(79f);
			int theY3 = theHeight - (int)Constants.InvertAndScale(46f);
			int theX3 = this.mWidth / 4 - num3 / 2;
			int theX4 = this.mWidth * 3 / 4 - num3 / 2 - (int)Constants.InvertAndScale(4f);
			if (this.mApp.CanShowAlmanac())
			{
				this.mBackToGameButton.Resize(theX3, theY2, num3, mHeight);
				this.mRestartButton.Resize(theX4, theY2, num3, mHeight);
				this.mBackToMainButton.Resize(theX3, theY3, num3, mHeight);
				this.mAlmanacButton.Resize(theX4, theY3, num3, mHeight);
			}
			else
			{
				this.mBackToGameButton.Resize(theX3, theY2, num3, mHeight);
				this.mRestartButton.Resize(theX4, theY2, num3, mHeight);
				this.mBackToMainButton.Resize(this.mWidth / 2 - num3 / 2 - (int)Constants.InvertAndScale(4f), theY3, num3, mHeight);
			}
			this.mVersionY = Constants.NewOptionsDialog_Version_High_Y;
		}

		public override void Draw(Graphics g)
		{
			base.Draw(g);
			int newOptionsDialog_Music_Offset = Constants.NewOptionsDialog_Music_Offset;
			int newOptionsDialog_FX_Offset = Constants.NewOptionsDialog_FX_Offset;
			int newOptionsDialog_FullScreenOffset = Constants.NewOptionsDialog_FullScreenOffset;
			SexyColor theColor = new SexyColor(107, 109, 145);
			int num = this.mMusicSliderOn ? Constants.NewOptionsDialog_MusicLabel_On_Y : Constants.NewOptionsDialog_MusicLabel_Off_Y;
			TodCommon.TodDrawString(g, this.mMusicSliderOn ? "[OPTIONS_MUSIC_VOLUME]" : "[OPTIONS_MUSIC_OFF]", Constants.NewOptionsDialog_MusicLabel_X, num + newOptionsDialog_Music_Offset, Resources.FONT_DWARVENTODCRAFT18, theColor, Constants.NewOptionsDialog_VibrationLabel_MaxWidth, DrawStringJustification.DS_ALIGN_RIGHT);
			TodCommon.TodDrawString(g, "[OPTIONS_SOUND_FX]", Constants.NewOptionsDialog_FXLabel_X, Constants.NewOptionsDialog_FXLabel_Y + newOptionsDialog_FX_Offset, Resources.FONT_DWARVENTODCRAFT18, theColor, Constants.NewOptionsDialog_VibrationLabel_MaxWidth, DrawStringJustification.DS_ALIGN_RIGHT);
			TodCommon.TodDrawString(g, "[OPTIONS_VABRATION]", Constants.NewOptionsDialog_VibrationLabel_X, Constants.NewOptionsDialog_VibrationLabel_Y + newOptionsDialog_FullScreenOffset, Resources.FONT_DWARVENTODCRAFT18, theColor, Constants.NewOptionsDialog_VibrationLabel_MaxWidth, DrawStringJustification.DS_ALIGN_RIGHT);
			TodCommon.TodDrawString(g, "[OPTIONS_RUN_LOCKED]", Constants.NewOptionsDialog_VibrationLabel_X, Constants.NewOptionsDialog_LockedLabel_Y + newOptionsDialog_FullScreenOffset, Resources.FONT_DWARVENTODCRAFT18, theColor, Constants.NewOptionsDialog_VibrationLabel_MaxWidth, DrawStringJustification.DS_ALIGN_RIGHT);
			TodCommon.TodDrawString(g, LawnApp.AppVersionNumber, this.mWidth / 2, this.mVersionY, Resources.FONT_PICO129, theColor, DrawStringJustification.DS_ALIGN_CENTER);
		}

		public virtual void SliderVal(int theId, double theVal)
		{
			switch (theId)
			{
			case 4:
				if (theVal > GameConstants.MUSIC_SLIDER_THRESHOLD && !this.mApp.mMusicEnabled)
				{
					this.mApp.EnableMusic(true);
				}
				else if (theVal < GameConstants.MUSIC_SLIDER_THRESHOLD && this.mApp.mMusicEnabled)
				{
					this.mApp.EnableMusic(false);
				}
				this.mMusicSliderOn = this.mApp.mMusicEnabled;
				this.mApp.SetMusicVolume(theVal);
				this.mApp.mSoundSystem.RehookupSoundWithMusicVolume();
				return;
			case 5:
				this.mApp.SetSfxVolume(theVal);
				this.mApp.mSoundSystem.RehookupSoundWithMusicVolume();
				if (!this.mSfxVolumeSlider.mDragging)
				{
					this.mApp.PlaySample(Resources.SOUND_BUTTONCLICK);
				}
				return;
			default:
				return;
			}
		}

		public override void CheckboxChecked(int theId, bool check)
		{
			this.mApp.PlaySample(Resources.SOUND_BUTTONCLICK);
			if (theId == 8)
			{
				this.mApp.mPlayerInfo.mDoVibration = check;
				return;
			}
			if (theId != 11)
			{
				return;
			}
			this.SetRunWhenLocked(check);
			string theDialogHeader = string.Empty;
			string theDialogLines = string.Empty;
			theDialogHeader = TodStringFile.TodStringTranslate("[WARNING]");
			theDialogLines = TodStringFile.TodStringTranslate("[OPTIONS_RUN_LOCKED_MSG]");
			LawnDialog lawnDialog = this.mApp.DoDialog(53, true, theDialogHeader, theDialogLines, "", 3);
			lawnDialog.mLawnYesButton.mLabel = TodStringFile.TodStringTranslate("[DIALOG_BUTTON_OK]");
		}

		public override void ButtonPress(int theId)
		{
			this.mApp.PlaySample(Resources.SOUND_GRAVEBUTTON);
		}

		public override bool BackButtonPress()
		{
			int mId = this.mBackToGameButton.mId;
			this.ButtonPress(mId);
			this.ButtonDepress(mId);
			return true;
		}

		public override void ButtonDepress(int theId)
		{
			base.ButtonDepress(theId);
			switch (theId)
			{
			case 0:
				this.mApp.DoAlmanacDialog(SeedType.SEED_NONE, ZombieType.ZOMBIE_INVALID, null);
				return;
			case 1:
				if (this.mApp.mBoard != null && this.mApp.mBoard.NeedSaveGame())
				{
					this.mApp.DoConfirmBackToMain();
					return;
				}
				if (this.mApp.mBoard != null && this.mApp.mBoard.mCutScene != null && this.mApp.mBoard.mCutScene.IsSurvivalRepick())
				{
					this.mApp.DoConfirmBackToMain();
					return;
				}
				this.mApp.mBoardResult = BoardResult.BOARDRESULT_QUIT;
				this.mApp.DoBackToMain();
				return;
			case 2:
				if (this.mApp.mBoard != null)
				{
					string theDialogHeader = string.Empty;
					string theDialogLines = string.Empty;
					if (this.mApp.IsPuzzleMode())
					{
						theDialogHeader = "[RESTART_PUZZLE_HEADER]";
						theDialogLines = "[RESTART_PUZZLE_BODY]";
					}
					else if (this.mApp.IsChallengeMode())
					{
						theDialogHeader = "[RESTART_CHALLENGE_HEADER]";
						theDialogLines = "[RESTART_CHALLENGE_BODY]";
					}
					else if (this.mApp.IsSurvivalMode())
					{
						theDialogHeader = "[RESTART_SURVIVAL_HEADER]";
						theDialogLines = "[RESTART_SURVIVAL_BODY]";
					}
					else
					{
						theDialogHeader = "[RESTART_LEVEL_HEADER]";
						theDialogLines = "[RESTART_LEVEL_BODY]";
					}
					LawnDialog lawnDialog = this.mApp.DoDialog(23, true, theDialogHeader, theDialogLines, "", 1);
					lawnDialog.mLawnYesButton.mLabel = TodStringFile.TodStringTranslate("[RESTART_LABEL]");
					lawnDialog.mLawnNoButton.mLabel = TodStringFile.TodStringTranslate("[DIALOG_BUTTON_CANCEL]");
					lawnDialog.mSpaceAfterHeader = (int)Constants.InvertAndScale(20f);
					lawnDialog.mMinWidth = (int)Constants.InvertAndScale(350f);
					lawnDialog.CalcSize(0, 0);
					return;
				}
				break;
			case 3:
				this.mApp.CheckForUpdates();
				break;
			case 4:
			case 5:
			case 8:
			case 9:
				break;
			case 6:
				this.mApp.KillNewOptionsDialog();
				this.mApp.KillGameSelector();
				this.mApp.ShowAwardScreen(AwardType.AWARD_HELP_ZOMBIE_NOTE, false);
				return;
			case 7:
			{
				string empty = string.Empty;
				string theDialogLines2 = string.Empty;
				empty = string.Empty;
				theDialogLines2 = TodStringFile.TodStringTranslate("[ABOUT_1]") + LawnApp.AppVersionNumber;
				LawnDialog lawnDialog2 = this.mApp.DoDialog(52, true, empty, theDialogLines2, "", 3);
				lawnDialog2.mMinWidth = (int)Constants.InvertAndScale(350f);
				lawnDialog2.mLawnYesButton.mLabel = TodStringFile.TodStringTranslate("[BACK]");
				lawnDialog2.CalcSize(0, 0);
				return;
			}
			case 10:
				this.mApp.KillNewOptionsDialog();
				this.mApp.KillGameSelector();
				this.mApp.ShowCreditScreen();
				return;
			default:
				if (theId != 1000)
				{
					return;
				}
				this.mApp.WriteCurrentUserConfig();
				return;
			}
		}

		public override void KeyDown(KeyCode theKey)
		{
			if (this.mApp.mBoard != null)
			{
				this.mApp.mBoard.DoTypingCheck(theKey);
			}
			if (theKey == KeyCode.KEYCODE_SPACE || theKey == KeyCode.KEYCODE_RETURN)
			{
				base.ButtonDepress(1000);
				return;
			}
			if (theKey == KeyCode.KEYCODE_ESCAPE)
			{
				base.ButtonDepress(1001);
			}
		}

		public override void Update()
		{
			base.Update();
			if (this.mMusicSliderOn && (!this.mApp.mMusicEnabled || this.mApp.GetMusicVolume() == 0.0))
			{
				this.mMusicSliderOn = false;
				this.mMusicVolumeSlider.SetValue(0.0);
			}
		}

		private void SetRunWhenLocked(bool value)
		{
			Main.RunWhenLocked = value;
			this.mApp.mPlayerInfo.mRunWhileLocked = value;
		}

		public Slider mMusicVolumeSlider;

		public Slider mSfxVolumeSlider;

		public Checkbox mVibrateCheckbox;

		public Checkbox mRunWhileLocked;

		public string mVersion = string.Empty;

		public int mVersionY;

		public LawnStoneButton mAlmanacButton;

		public LawnStoneButton mBackToMainButton;

		public LawnStoneButton mRestartButton;

		public LawnStoneButton mBackToGameButton;

		public HyperlinkWidget mLinkCredits;

		public LawnStoneButton mAboutButton;

		public LawnStoneButton mHelpButton;

		public bool mFromGameSelector;

		public bool mMusicSliderOn;
	}
}
