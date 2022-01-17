using System;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
	internal class NewOptionsDialog : LawnDialog, SliderListener, CheckboxListener
	{
		public NewOptionsDialog(LawnApp theApp, bool theFromGameSelector) : base(theApp, null, 2, true, "[OPTIONS_DIALOG_TITLE]", "", "", 0)
		{
			mApp = theApp;
			mFromGameSelector = theFromGameSelector;
			mMusicSliderOn = mApp.mMusicEnabled;
			SetColor(3, new SexyColor(255, 255, 100));
			mAlmanacButton = GameButton.MakeButton(0, this, "[VIEW_ALMANAC_BUTTON]");
			mRestartButton = GameButton.MakeButton(2, this, "[RESTART_LEVEL_BUTTON]");
			mBackToMainButton = GameButton.MakeButton(1, this, "[MAIN_MENU_BUTTON]");
			mBackToGameButton = GameButton.MakeButton(1000, this, "[BACK_TO_GAME]");
			mHelpButton = GameButton.MakeButton(6, this, "[HELP]");
			mAboutButton = GameButton.MakeButton(7, this, "[ABOUT]");
			mMusicVolumeSlider = new Slider(AtlasResources.IMAGE_OPTIONS_SLIDERSLOT, AtlasResources.IMAGE_OPTIONS_SLIDERKNOB2, 4, this);
			double num = theApp.GetMusicVolume();
			num = Math.Max(0.0, Math.Min(1.0, num));
			mMusicVolumeSlider.SetValue(num);
			mSfxVolumeSlider = new Slider(AtlasResources.IMAGE_OPTIONS_SLIDERSLOT, AtlasResources.IMAGE_OPTIONS_SLIDERKNOB2, 5, this);
			mSfxVolumeSlider.SetValue(theApp.GetSfxVolume());
			mVibrateCheckbox = LawnCommon.MakeNewCheckbox(8, this, mApp.mPlayerInfo.mDoVibration);
			mRunWhileLocked = LawnCommon.MakeNewCheckbox(11, this, mApp.mPlayerInfo.mRunWhileLocked);
			mEnableCheat = LawnCommon.MakeNewCheckbox(12, this, mApp.mDebugKeysEnabled);
			mLinkCredits = new HyperlinkWidget(10, this);
			mLinkCredits.SetFont(Resources.FONT_BRIANNETOD12);
			mLinkCredits.mColor = new SexyColor(255, 255, 136);
			mLinkCredits.mOverColor = new SexyColor(0, 0, 255);
			mLinkCredits.mDoFinger = true;
			mLinkCredits.mLabel = TodStringFile.TodStringTranslate("[OPTIONS_CREDITS_LINK]");
			mLinkCredits.mUnderlineSize = 1;
			mLinkCredits.mUnderlineOffset = 1;
			if (mFromGameSelector)
			{
				mRestartButton.SetVisible(false);
				mBackToMainButton.SetVisible(false);
				mBackToGameButton.SetLabel("[DIALOG_BUTTON_OK]");
			}
			else
			{
				mTallBottom = (mApp.mGameScene != GameScenes.LevelIntro);
				mLinkCredits.SetVisible(false);
			}
			if (mApp.mGameMode == GameMode.ChallengeIce || mApp.mGameMode == GameMode.ChallengeZenGarden || mApp.mGameMode == GameMode.TreeOfWisdom)
			{
				mRestartButton.SetVisible(false);
			}
			if (mApp.mGameScene == GameScenes.LevelIntro && !mApp.mBoard.mCutScene.IsSurvivalRepick())
			{
				mRestartButton.SetVisible(false);
			}
			if (!mApp.CanShowAlmanac() || mApp.mGameScene == GameScenes.LevelIntro || mFromGameSelector)
			{
				mAlmanacButton.SetVisible(false);
			}
			base.CalcSize(0, 0);
		}

		public override void Dispose()
		{
			mHelpButton.Dispose();
			mAboutButton.Dispose();
			mRunWhileLocked.Dispose();
			mMusicVolumeSlider.Dispose();
			mSfxVolumeSlider.Dispose();
			mVibrateCheckbox.Dispose();
			mAlmanacButton.Dispose();
			mRestartButton.Dispose();
			mBackToMainButton.Dispose();
			mBackToGameButton.Dispose();
			base.Dispose();
		}

		public override int GetPreferredHeight(int theWidth)
		{
			return (int)Constants.InvertAndScale((mFromGameSelector || mApp.mGameScene == GameScenes.LevelIntro) ? 310 : 340);
		}

		public override void AddedToManager(WidgetManager theWidgetManager)
		{
			base.AddedToManager(theWidgetManager);
			AddWidget(mAlmanacButton);
			AddWidget(mRestartButton);
			AddWidget(mBackToMainButton);
			AddWidget(mMusicVolumeSlider);
			AddWidget(mSfxVolumeSlider);
			AddWidget(mVibrateCheckbox);
			//AddWidget(mRunWhileLocked);
			AddWidget(mEnableCheat);
			AddWidget(mBackToGameButton);
			AddWidget(mLinkCredits);
			if (mFromGameSelector)
			{
				AddWidget(mAboutButton);
				AddWidget(mHelpButton);
			}
		}

		public override void RemovedFromManager(WidgetManager theWidgetManager)
		{
			base.RemovedFromManager(theWidgetManager);
			RemoveWidget(mAlmanacButton);
			RemoveWidget(mMusicVolumeSlider);
			RemoveWidget(mSfxVolumeSlider);
			RemoveWidget(mVibrateCheckbox);
			//RemoveWidget(mRunWhileLocked);
			RemoveWidget(mEnableCheat);
			RemoveWidget(mBackToMainButton);
			RemoveWidget(mBackToGameButton);
			RemoveWidget(mRestartButton);
			RemoveWidget(mLinkCredits);
			if (mFromGameSelector)
			{
				RemoveWidget(mAboutButton);
				RemoveWidget(mHelpButton);
			}
		}

		public override void Resize(int theX, int theY, int theWidth, int theHeight)
		{
			base.Resize(theX, theY, theWidth, theHeight);
			int btnHeight = AtlasResources.IMAGE_BUTTON_LEFT.mHeight;
			int theX2 = (int)Constants.InvertAndScale(244f);
			mMusicVolumeSlider.Resize(theX2, (int)Constants.InvertAndScale(66f), (int)Constants.InvertAndScale(135f), (int)Constants.InvertAndScale(40f));
			mSfxVolumeSlider.Resize(theX2, (int)Constants.InvertAndScale(93f), (int)Constants.InvertAndScale(135f), (int)Constants.InvertAndScale(40f));
			mVibrateCheckbox.Resize(theX2, (int)Constants.InvertAndScale(125f), (int)Constants.InvertAndScale(46f), (int)Constants.InvertAndScale(45f));
			//mRunWhileLocked.Resize(theX2, (int)Constants.InvertAndScale(195f), (int)Constants.InvertAndScale(46f), (int)Constants.InvertAndScale(45f));
			mEnableCheat.Resize(theX2, (int)Constants.InvertAndScale(195f), (int)Constants.InvertAndScale(46f), (int)Constants.InvertAndScale(45f));
			mMusicVolumeSlider.mY += (int)Constants.InvertAndScale(5f);
			mSfxVolumeSlider.mY += (int)Constants.InvertAndScale(15f);
			mVibrateCheckbox.mY += (int)Constants.InvertAndScale(25f);
			if (mFromGameSelector)
			{
				int num = (int)Constants.InvertAndScale(100f);
				mBackToGameButton.Resize(mWidth * 4 / 5 - num / 2, theHeight - (int)Constants.InvertAndScale(50f), num, btnHeight);
				mHelpButton.Resize(mWidth / 2 - num / 2, theHeight - (int)Constants.InvertAndScale(50f), num, btnHeight);
				mAboutButton.Resize(mWidth / 5 - num / 2, theHeight - (int)Constants.InvertAndScale(50f), num, btnHeight);
				mLinkCredits.Resize(theWidth - (int)Constants.InvertAndScale(175f), theHeight - (int)Constants.InvertAndScale(90f), (int)Constants.InvertAndScale(190f), mLinkCredits.mFont.GetHeight() + (int)Constants.InvertAndScale(8f));
				mVersionY = Constants.NewOptionsDialog_Version_Low_Y;
				return;
			}
			if (mApp.mGameScene == GameScenes.LevelIntro)
			{
				int num2 = (int)Constants.InvertAndScale(150f);
				mBackToGameButton.Resize(mWidth / 4 - num2 / 2, theHeight - (int)Constants.InvertAndScale(50f), num2, btnHeight);
				mBackToMainButton.Resize(mWidth * 3 / 4 - num2 / 2, theHeight - (int)Constants.InvertAndScale(50f), num2, btnHeight);
				mVersionY = Constants.NewOptionsDialog_Version_Low_Y;
				return;
			}
			int num3 = (int)Constants.InvertAndScale(160f);
			int theY2 = theHeight - (int)Constants.InvertAndScale(79f);
			int theY3 = theHeight - (int)Constants.InvertAndScale(46f);
			int theX3 = mWidth / 4 - num3 / 2;
			int theX4 = mWidth * 3 / 4 - num3 / 2 - (int)Constants.InvertAndScale(4f);
			if (mApp.CanShowAlmanac())
			{
				mBackToGameButton.Resize(theX3, theY2, num3, btnHeight);
				mRestartButton.Resize(theX4, theY2, num3, btnHeight);
				mBackToMainButton.Resize(theX3, theY3, num3, btnHeight);
				mAlmanacButton.Resize(theX4, theY3, num3, btnHeight);
			}
			else
			{
				mBackToGameButton.Resize(theX3, theY2, num3, btnHeight);
				mRestartButton.Resize(theX4, theY2, num3, btnHeight);
				mBackToMainButton.Resize(mWidth / 2 - num3 / 2 - (int)Constants.InvertAndScale(4f), theY3, num3, btnHeight);
			}
			mVersionY = Constants.NewOptionsDialog_Version_High_Y;
		}

		public override void Draw(Graphics g)
		{
			base.Draw(g);
			int newOptionsDialog_Music_Offset = Constants.NewOptionsDialog_Music_Offset;
			int newOptionsDialog_FX_Offset = Constants.NewOptionsDialog_FX_Offset;
			int newOptionsDialog_FullScreenOffset = Constants.NewOptionsDialog_FullScreenOffset;
			SexyColor theColor = new SexyColor(107, 109, 145);
			int num = mMusicSliderOn ? Constants.NewOptionsDialog_MusicLabel_On_Y : Constants.NewOptionsDialog_MusicLabel_Off_Y;
			TodCommon.TodDrawString(g, mMusicSliderOn ? "[OPTIONS_MUSIC_VOLUME]" : "[OPTIONS_MUSIC_OFF]", Constants.NewOptionsDialog_MusicLabel_X, num + newOptionsDialog_Music_Offset, Resources.FONT_DWARVENTODCRAFT18, theColor, Constants.NewOptionsDialog_VibrationLabel_MaxWidth, DrawStringJustification.Right);
			TodCommon.TodDrawString(g, "[OPTIONS_SOUND_FX]", Constants.NewOptionsDialog_FXLabel_X, Constants.NewOptionsDialog_FXLabel_Y + newOptionsDialog_FX_Offset, Resources.FONT_DWARVENTODCRAFT18, theColor, Constants.NewOptionsDialog_VibrationLabel_MaxWidth, DrawStringJustification.Right);
			TodCommon.TodDrawString(g, "[OPTIONS_VABRATION]", Constants.NewOptionsDialog_VibrationLabel_X, Constants.NewOptionsDialog_VibrationLabel_Y + newOptionsDialog_FullScreenOffset, Resources.FONT_DWARVENTODCRAFT18, theColor, Constants.NewOptionsDialog_VibrationLabel_MaxWidth, DrawStringJustification.Right);
			//TodCommon.TodDrawString(g, "[OPTIONS_RUN_LOCKED]", Constants.NewOptionsDialog_VibrationLabel_X, Constants.NewOptionsDialog_LockedLabel_Y + newOptionsDialog_FullScreenOffset, Resources.FONT_DWARVENTODCRAFT18, theColor, Constants.NewOptionsDialog_VibrationLabel_MaxWidth, DrawStringJustification.Right);
			TodCommon.TodDrawString(g, "[OPTIONS_ENABLE_CHEAT]", Constants.NewOptionsDialog_VibrationLabel_X, Constants.NewOptionsDialog_LockedLabel_Y + newOptionsDialog_FullScreenOffset, Resources.FONT_DWARVENTODCRAFT18, theColor, Constants.NewOptionsDialog_VibrationLabel_MaxWidth, DrawStringJustification.Right);
			TodCommon.TodDrawString(g, LawnApp.AppVersionNumber, mWidth / 2, mVersionY, Resources.FONT_PICO129, theColor, DrawStringJustification.Center);
		}

		public virtual void SliderVal(int theId, double theVal)
		{
			switch (theId)
			{
			case 4:
				if (theVal > GameConstants.MUSIC_SLIDER_THRESHOLD && !mApp.mMusicEnabled)
				{
					mApp.EnableMusic(true);
				}
				else if (theVal < GameConstants.MUSIC_SLIDER_THRESHOLD && mApp.mMusicEnabled)
				{
					mApp.EnableMusic(false);
				}
				mMusicSliderOn = mApp.mMusicEnabled;
				mApp.SetMusicVolume(theVal);
				mApp.mSoundSystem.RehookupSoundWithMusicVolume();
				return;
			case 5:
				mApp.SetSfxVolume(theVal);
				mApp.mSoundSystem.RehookupSoundWithMusicVolume();
				if (!mSfxVolumeSlider.mDragging)
				{
					mApp.PlaySample(Resources.SOUND_BUTTONCLICK);
				}
				return;
			default:
				return;
			}
		}

		public override void CheckboxChecked(int theId, bool check)
		{
			mApp.PlaySample(Resources.SOUND_BUTTONCLICK);
            switch (theId)
            {
            case 8:
                mApp.mPlayerInfo.mDoVibration = check;
                return;
            case 11:
            {
                SetRunWhenLocked(check);
                string theDialogHeader = string.Empty;
                string theDialogLines = string.Empty;
                theDialogHeader = TodStringFile.TodStringTranslate("[WARNING]");
                theDialogLines = TodStringFile.TodStringTranslate("[OPTIONS_RUN_LOCKED_MSG]");
                LawnDialog lawnDialog = mApp.DoDialog(53, true, theDialogHeader, theDialogLines, "", 3);
                lawnDialog.mLawnYesButton.mLabel = TodStringFile.TodStringTranslate("[DIALOG_BUTTON_OK]");
                return;
            }
			case 12:
				mApp.mDebugKeysEnabled = check;
				return;
            default:
                return;
            }
        }

		public override void ButtonPress(int theId)
		{
			mApp.PlaySample(Resources.SOUND_GRAVEBUTTON);
		}

		public override bool BackButtonPress()
		{
			int id = mBackToGameButton.mId;
			ButtonPress(id);
			ButtonDepress(id);
			return true;
		}

		public override void ButtonDepress(int theId)
		{
			base.ButtonDepress(theId);
			switch (theId)
			{
			case 0:
				mApp.DoAlmanacDialog(SeedType.None, ZombieType.Invalid, null);
				return;
			case 1:
				if (mApp.mBoard != null && mApp.mBoard.NeedSaveGame())
				{
					mApp.DoConfirmBackToMain();
					return;
				}
				if (mApp.mBoard != null && mApp.mBoard.mCutScene != null && mApp.mBoard.mCutScene.IsSurvivalRepick())
				{
					mApp.DoConfirmBackToMain();
					return;
				}
				mApp.mBoardResult = BoardResult.Quit;
				mApp.DoBackToMain();
				return;
			case 2:
				if (mApp.mBoard != null)
				{
					string theDialogHeader = string.Empty;
					string theDialogLines = string.Empty;
					if (mApp.IsPuzzleMode())
					{
						theDialogHeader = "[RESTART_PUZZLE_HEADER]";
						theDialogLines = "[RESTART_PUZZLE_BODY]";
					}
					else if (mApp.IsChallengeMode())
					{
						theDialogHeader = "[RESTART_CHALLENGE_HEADER]";
						theDialogLines = "[RESTART_CHALLENGE_BODY]";
					}
					else if (mApp.IsSurvivalMode())
					{
						theDialogHeader = "[RESTART_SURVIVAL_HEADER]";
						theDialogLines = "[RESTART_SURVIVAL_BODY]";
					}
					else
					{
						theDialogHeader = "[RESTART_LEVEL_HEADER]";
						theDialogLines = "[RESTART_LEVEL_BODY]";
					}
					LawnDialog lawnDialog = mApp.DoDialog(23, true, theDialogHeader, theDialogLines, "", 1);
					lawnDialog.mLawnYesButton.mLabel = TodStringFile.TodStringTranslate("[RESTART_LABEL]");
					lawnDialog.mLawnNoButton.mLabel = TodStringFile.TodStringTranslate("[DIALOG_BUTTON_CANCEL]");
					lawnDialog.mSpaceAfterHeader = (int)Constants.InvertAndScale(20f);
					lawnDialog.mMinWidth = (int)Constants.InvertAndScale(350f);
					lawnDialog.CalcSize(0, 0);
					return;
				}
				break;
			case 3:
				mApp.CheckForUpdates();
				break;
			case 4:
			case 5:
			case 8:
			case 9:
				break;
			case 6:
				mApp.KillNewOptionsDialog();
				mApp.KillGameSelector();
				mApp.ShowAwardScreen(AwardType.HelpZombieNote, false);
				return;
			case 7:
			{
				string empty = string.Empty;
				string theDialogLines2 = string.Empty;
				empty = string.Empty;
				theDialogLines2 = TodStringFile.TodStringTranslate("[ABOUT_1]") + LawnApp.AppVersionNumber;
				LawnDialog lawnDialog2 = mApp.DoDialog(52, true, empty, theDialogLines2, "", 3);
				lawnDialog2.mMinWidth = (int)Constants.InvertAndScale(350f);
				lawnDialog2.mLawnYesButton.mLabel = TodStringFile.TodStringTranslate("[BACK]");
				lawnDialog2.CalcSize(0, 0);
				return;
			}
			case 10:
				mApp.KillNewOptionsDialog();
				mApp.KillGameSelector();
				mApp.ShowCreditScreen();
				return;
			default:
				if (theId != 1000)
				{
					return;
				}
				mApp.WriteCurrentUserConfig();
				return;
			}
		}

		public override void KeyDown(KeyCode theKey)
		{
			if (mApp.mBoard != null)
			{
				mApp.mBoard.DoTypingCheck(theKey);
			}
			if (theKey == KeyCode.Space || theKey == KeyCode.Return)
			{
				base.ButtonDepress(1000);
				return;
			}
			if (theKey == KeyCode.Escape)
			{
				base.ButtonDepress(1001);
			}
		}

		public override void Update()
		{
			base.Update();
			if (mMusicSliderOn && (!mApp.mMusicEnabled || mApp.GetMusicVolume() == 0.0))
			{
				mMusicSliderOn = false;
				mMusicVolumeSlider.SetValue(0.0);
			}
		}

		private void SetRunWhenLocked(bool value)
		{
			Main.RunWhenLocked = value;
			mApp.mPlayerInfo.mRunWhileLocked = value;
		}

		public Slider mMusicVolumeSlider;

		public Slider mSfxVolumeSlider;

		public Checkbox mVibrateCheckbox;

		public Checkbox mRunWhileLocked;

		public Checkbox mEnableCheat;

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
