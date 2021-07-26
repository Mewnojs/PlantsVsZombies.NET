using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
	internal class CreditScreen : Widget, ButtonListener
	{
		internal static int DrawLeftText(Graphics g, string theText, int theY)
		{
			TRect theRect = new TRect(Constants.CreditScreen_LeftText_X, theY, Constants.CreditScreen_LeftRight_Text_Width, Constants.BOARD_HEIGHT);
			return TodStringFile.TodDrawStringWrapped(g, theText, theRect, Resources.FONT_HOUSEOFTERROR16, SexyColor.White, DrawStringJustification.DS_ALIGN_RIGHT);
		}

		internal static int DrawRightText(Graphics g, string theText, int theY)
		{
			TRect theRect = new TRect(Constants.CreditScreen_RightText_X, theY, Constants.CreditScreen_LeftRight_Text_Width, Constants.BOARD_HEIGHT);
			return TodStringFile.TodDrawStringWrapped(g, theText, theRect, Resources.FONT_HOUSEOFTERROR16, SexyColor.White, DrawStringJustification.DS_ALIGN_LEFT, ',');
		}

		internal static int DrawSection(Graphics g, string theLeftText, string theRightText, int theY)
		{
			int num = 0;
			int num2 = 0;
			if (theLeftText.Length > 0 && theLeftText.get_Chars(0) == '^')
			{
				int num3 = (int)Constants.InvertAndScale(20f);
				string text;
				if (!CreditScreen.sectionSubstrings.TryGetValue(theLeftText, ref text))
				{
					text = theLeftText.Substring(1);
					CreditScreen.sectionSubstrings.Add(theLeftText, text);
				}
				TodCommon.TodDrawString(g, text, Constants.BOARD_WIDTH / 2, theY + num3, Resources.FONT_HOUSEOFTERROR16, SexyColor.White, DrawStringJustification.DS_ALIGN_CENTER, 1f);
				num = (int)Constants.InvertAndScale(29f);
			}
			else
			{
				int num4;
				if (!CreditScreen.cachedHeightsLeft.TryGetValue(theLeftText, ref num4))
				{
					num4 = CreditScreen.DrawLeftText(g, theLeftText, theY) - num;
					CreditScreen.cachedHeightsLeft.Add(theLeftText, num4);
				}
				else if (theY >= -num4)
				{
					num4 = CreditScreen.DrawLeftText(g, theLeftText, theY);
				}
				num += num4;
				if (!CreditScreen.cachedHeightsRight.TryGetValue(theRightText, ref num4))
				{
					num4 = CreditScreen.DrawRightText(g, theRightText, theY) - num2;
					CreditScreen.cachedHeightsRight.Add(theRightText, num4);
				}
				else if (theY >= -num4)
				{
					num4 = CreditScreen.DrawRightText(g, theRightText, theY);
				}
				num2 += num4;
			}
			return Math.Max(num, num2) + theY + (int)(Constants.S * 25f);
		}

		public CreditScreen(LawnApp theApp)
		{
			this.mApp = theApp;
			this.mMainMenuButton = GameButton.MakeButton(1, this, "[CREDITS_MAIN_MENU_BUTTON]");
			this.mMainMenuButton.Resize(Constants.CreditScreen_MainMenu.X, Constants.CreditScreen_MainMenu.Y, Constants.CreditScreen_MainMenu.Width, Constants.CreditScreen_MainMenu.Height);
			this.mMainMenuButton.SetVisible(false);
			this.mReplayButton = GameButton.MakeNewButton(0, this, "[CREDITS_REPLAY_BUTTON]", Resources.FONT_HOUSEOFTERROR16, AtlasResources.IMAGE_CREDITS_PLAYBUTTON, null, null);
			this.mReplayButton.mTextDownOffsetX = 1;
			this.mReplayButton.mTextDownOffsetY = 1;
			this.mReplayButton.mColors[0] = new SexyColor(255, 255, 255);
			this.mReplayButton.mColors[1] = new SexyColor(213, 159, 43);
			this.mReplayButton.Resize(Constants.CreditScreen_ReplayButton.X, Constants.CreditScreen_ReplayButton.Y, Constants.CreditScreen_ReplayButton.Width, Constants.CreditScreen_ReplayButton.Height);
			this.mReplayButton.mTextOffsetX = Constants.CreditScreen_ReplayButton_TextOffset.X;
			this.mReplayButton.mTextOffsetY = Constants.CreditScreen_ReplayButton_TextOffset.Y;
			this.mReplayButton.SetVisible(false);
			this.mNumSections = 0;
			for (;;)
			{
				Debug.ASSERT(this.mNumSections < 57);
				string theString = Common.StrFormat_("[CREDITS_ROLES{0}]", this.mNumSections + 1);
				if (!TodStringFile.TodStringListExists(theString))
				{
					break;
				}
				this.mRoles[this.mNumSections] = TodStringFile.TodStringTranslate(theString);
				if (this.mRoles[this.mNumSections] == "-")
				{
					this.mRoles[this.mNumSections] = " ";
				}
				theString = Common.StrFormat_("[CREDITS_NAMES{0}]", this.mNumSections + 1);
				this.mNames[this.mNumSections] = TodStringFile.TodStringTranslate(theString);
				if (this.mNames[this.mNumSections] == "-")
				{
					this.mNames[this.mNumSections] = " ";
				}
				this.mNumSections++;
			}
			this.mCreditsHeight = this.GetCreditsHeight();
			this.RestartScroll();
			if (this.mApp.HasFinishedAdventure())
			{
				this.mApp.mMusic.StopAllMusic();
				this.mVideoLoading = true;
				this.mVideoFinished = false;
				this.mDidInitialDraw = false;
				this.mNeedToStartPlaying = true;
				return;
			}
			this.mVideoLoading = false;
			this.mVideoFinished = true;
			this.mDidInitialDraw = false;
			this.mNeedToStartPlaying = false;
			this.mMainMenuButton.SetVisible(true);
			this.mMainMenuButton.Move(this.mApp.mWidth / 2 - this.mMainMenuButton.mWidth / 2, this.mMainMenuButton.mY);
		}

		public override void Dispose()
		{
			this.mReplayButton.Dispose();
			this.mMainMenuButton.Dispose();
			this.RemoveAllWidgets(true);
		}

		public void AppGotFocus()
		{
			if (!this.mNeedToStartPlaying && !this.videoDone)
			{
				this.mApp.MoviePlayerPlaybackDidFinish();
				this.videoDone = true;
			}
		}

		public void DrawCredits(Graphics g)
		{
			g.SetClipRect(0, 0, this.mWidth, Constants.CreditScreen_TextClip);
			g.HardwareClip();
			Resources.FONT_HOUSEOFTERROR16.EnableLayer(0, false);
			int num = (int)this.mCreditsY;
			int num2 = 0;
			while (num2 < this.mNumSections && num <= Constants.BOARD_HEIGHT)
			{
				num = CreditScreen.DrawSection(g, this.mRoles[num2], this.mNames[num2], num) + (int)(Constants.S * 5f);
				if (this.mRoles[num2].Length == 0 || this.mRoles[num2].get_Chars(0) != '^')
				{
					num += (int)(Constants.S * 15f);
				}
				num2++;
			}
			Resources.FONT_HOUSEOFTERROR16.EnableLayer(0, true);
			g.EndHardwareClip();
		}

		public int GetCreditsHeight()
		{
			Graphics @new = Graphics.GetNew();
			@new.BeginFrame();
			@new.SetClipRect(0, 0, 0, 0);
			int num = 0;
			for (int i = 0; i < this.mNumSections; i++)
			{
				num = CreditScreen.DrawSection(@new, this.mRoles[i], this.mNames[i], num) + (int)(Constants.S * 5f);
				if (this.mRoles[i].Length == 0 || this.mRoles[i].get_Chars(0) != '^')
				{
					num += (int)(Constants.S * 15f);
				}
			}
			@new.EndFrame();
			@new.PrepareForReuse();
			return num;
		}

		public override void Draw(Graphics g)
		{
			g.SetLinearBlend(true);
			g.SetColor(SexyColor.Black);
			if (!this.mVideoLoading)
			{
				this.DrawCredits(g);
			}
			this.mDidInitialDraw = true;
		}

		public override void MouseUp(int x, int y, int theClickCount)
		{
		}

		public override void TouchBegan(_Touch touch)
		{
			this.touching = true;
			this.touchStartPosition = touch.location;
			base.TouchBegan(touch);
		}

		public override void TouchEnded(_Touch touch)
		{
			this.touching = false;
			base.TouchEnded(touch);
		}

		public override void TouchMoved(_Touch touch)
		{
			if (this.touching)
			{
				this.mCreditsY += touch.location.y - this.touchStartPosition.y;
				this.touchStartPosition = touch.location;
			}
			base.TouchMoved(touch);
		}

		public override void AddedToManager(WidgetManager theWidgetManager)
		{
			base.AddedToManager(theWidgetManager);
			this.AddWidget(this.mMainMenuButton);
			this.AddWidget(this.mReplayButton);
		}

		public override void RemovedFromManager(WidgetManager theWidgetManager)
		{
			base.RemovedFromManager(theWidgetManager);
			this.RemoveWidget(this.mMainMenuButton);
			this.RemoveWidget(this.mReplayButton);
		}

		public void ButtonPress(int theId)
		{
			this.touching = false;
			if (theId == 1)
			{
				this.mApp.PlaySample(Resources.SOUND_GRAVEBUTTON);
			}
			if (theId == 0)
			{
				this.mApp.PlaySample(Resources.SOUND_TAP);
			}
		}

		public void ButtonDepress(int theId)
		{
			this.touching = false;
			if (theId == 1)
			{
				this.mApp.KillCreditScreen();
				this.mApp.DoBackToMain();
			}
			if (theId == 0)
			{
				this.mApp.KillCreditScreen();
				this.mApp.ShowCreditScreen();
			}
		}

		public override bool BackButtonPress()
		{
			this.mApp.KillCreditScreen();
			this.mApp.ShowGameSelectorWithOptions();
			return true;
		}

		public override void Update()
		{
			if (this.touching)
			{
				return;
			}
			this.mCreditsY -= 3f * Constants.InvertAndScale(0.25f);
			if (this.mCreditsY < (float)(-(float)(this.mCreditsHeight + Constants.CreditScreen_TextStart)))
			{
				this.RestartScroll();
			}
			if (this.mCreditsY > (float)Constants.CreditScreen_TextEnd)
			{
				this.RestartScroll();
			}
			if (this.mDidInitialDraw && this.mNeedToStartPlaying)
			{
				this.mApp.LostFocus();
				if (this.mApp.PlayMovie(VideoType.Credits, MOVIESCALINGMODE.MOVIESCALINGMODE_NONE, MOVIECONTROLMODE.MOVIECONTROLMODE_DEFAULT, SexyColor.Black))
				{
					this.videoDone = false;
				}
				else
				{
					this.videoDone = true;
					this.VideoFinished();
				}
				this.mNeedToStartPlaying = false;
			}
		}

		public void RestartScroll()
		{
			this.mCreditsY = (float)Constants.CreditScreen_TextEnd;
		}

		public void VideoLoaded(bool succeeded)
		{
			this.mVideoLoading = false;
			if (!succeeded)
			{
				this.VideoFinished();
			}
		}

		public void VideoFinished()
		{
			this.mVideoFinished = true;
			this.mVideoLoading = false;
			this.mReplayButton.SetVisible(true);
			this.mMainMenuButton.SetVisible(true);
			if (MusicInterface.USER_MUSIC_PLAYING)
			{
				MediaPlayer.Resume();
			}
			else
			{
				this.mApp.mMusic.MakeSureMusicIsPlaying(MusicTune.MUSIC_TUNE_DAY_GRASSWALK);
			}
			this.RestartScroll();
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

		public LawnApp mApp;

		public LawnStoneButton mMainMenuButton;

		public NewLawnButton mReplayButton;

		public bool mVideoLoading;

		public bool mVideoFinished;

		public bool mDidInitialDraw;

		public bool mNeedToStartPlaying;

		public string[] mRoles = new string[57];

		public string[] mNames = new string[57];

		public int mNumSections;

		public int mCreditsHeight;

		public float mCreditsY;

		private bool videoDone;

		private static Dictionary<string, string> sectionSubstrings = new Dictionary<string, string>(100);

		private static Dictionary<string, int> cachedHeightsLeft = new Dictionary<string, int>();

		private static Dictionary<string, int> cachedHeightsRight = new Dictionary<string, int>();

		private bool touching;

		private CGPoint touchStartPosition;
	}
}
