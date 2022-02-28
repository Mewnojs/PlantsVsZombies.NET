using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
    public/*internal*/ class CreditScreen : Widget, ButtonListener
    {
        internal static int DrawLeftText(Graphics g, string theText, int theY)
        {
            Rect theRect = new Rect(Constants.CreditScreen_LeftText_X, theY, Constants.CreditScreen_LeftRight_Text_Width, Constants.BOARD_HEIGHT);
            return TodStringFile.TodDrawStringWrapped(g, theText, theRect, Resources.FONT_HOUSEOFTERROR16, SexyColor.White, DrawStringJustification.Right);
        }

        internal static int DrawRightText(Graphics g, string theText, int theY)
        {
            Rect theRect = new Rect(Constants.CreditScreen_RightText_X, theY, Constants.CreditScreen_LeftRight_Text_Width, Constants.BOARD_HEIGHT);
            return TodStringFile.TodDrawStringWrapped(g, theText, theRect, Resources.FONT_HOUSEOFTERROR16, SexyColor.White, DrawStringJustification.Left, ',');
        }

        internal static int DrawSection(Graphics g, string theLeftText, string theRightText, int theY)
        {
            int num = 0;
            int num2 = 0;
            if (theLeftText.Length > 0 && theLeftText[0] == '^')
            {
                int num3 = (int)Constants.InvertAndScale(20f);
                string text;
                if (!CreditScreen.sectionSubstrings.TryGetValue(theLeftText, out text))
                {
                    text = theLeftText.Substring(1);
                    CreditScreen.sectionSubstrings.Add(theLeftText, text);
                }
                TodCommon.TodDrawString(g, text, Constants.BOARD_WIDTH / 2, theY + num3, Resources.FONT_HOUSEOFTERROR16, SexyColor.White, DrawStringJustification.Center, 1f);
                num = (int)Constants.InvertAndScale(29f);
            }
            else
            {
                int num4;
                if (!CreditScreen.cachedHeightsLeft.TryGetValue(theLeftText, out num4))
                {
                    num4 = CreditScreen.DrawLeftText(g, theLeftText, theY) - num;
                    CreditScreen.cachedHeightsLeft.Add(theLeftText, num4);
                }
                else if (theY >= -num4)
                {
                    num4 = CreditScreen.DrawLeftText(g, theLeftText, theY);
                }
                num += num4;
                if (!CreditScreen.cachedHeightsRight.TryGetValue(theRightText, out num4))
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
            mApp = theApp;
            mMainMenuButton = GameButton.MakeButton(1, this, "[CREDITS_MAIN_MENU_BUTTON]");
            mMainMenuButton.Resize(Constants.CreditScreen_MainMenu.X, Constants.CreditScreen_MainMenu.Y, Constants.CreditScreen_MainMenu.Width, Constants.CreditScreen_MainMenu.Height);
            mMainMenuButton.SetVisible(false);
            mReplayButton = GameButton.MakeNewButton(0, this, "[CREDITS_REPLAY_BUTTON]", Resources.FONT_HOUSEOFTERROR16, AtlasResources.IMAGE_CREDITS_PLAYBUTTON, null, null);
            mReplayButton.mTextDownOffsetX = 1;
            mReplayButton.mTextDownOffsetY = 1;
            mReplayButton.mColors[0] = new SexyColor(255, 255, 255);
            mReplayButton.mColors[1] = new SexyColor(213, 159, 43);
            mReplayButton.Resize(Constants.CreditScreen_ReplayButton.X, Constants.CreditScreen_ReplayButton.Y, Constants.CreditScreen_ReplayButton.Width, Constants.CreditScreen_ReplayButton.Height);
            mReplayButton.mTextOffsetX = Constants.CreditScreen_ReplayButton_TextOffset.X;
            mReplayButton.mTextOffsetY = Constants.CreditScreen_ReplayButton_TextOffset.Y;
            mReplayButton.SetVisible(false);
            mNumSections = 0;
            for (;;)
            {
                Debug.ASSERT(mNumSections < 57);
                string theString = Common.StrFormat_("[CREDITS_ROLES{0}]", mNumSections + 1);
                if (!TodStringFile.TodStringListExists(theString))
                {
                    break;
                }
                mRoles[mNumSections] = TodStringFile.TodStringTranslate(theString);
                if (mRoles[mNumSections] == "-")
                {
                    mRoles[mNumSections] = " ";
                }
                theString = Common.StrFormat_("[CREDITS_NAMES{0}]", mNumSections + 1);
                mNames[mNumSections] = TodStringFile.TodStringTranslate(theString);
                if (mNames[mNumSections] == "-")
                {
                    mNames[mNumSections] = " ";
                }
                mNumSections++;
            }
            mCreditsHeight = GetCreditsHeight();
            RestartScroll();
            if (mApp.HasFinishedAdventure())
            {
                mApp.mMusic.StopAllMusic();
                mVideoLoading = true;
                mVideoFinished = false;
                mDidInitialDraw = false;
                mNeedToStartPlaying = true;
                return;
            }
            mVideoLoading = false;
            mVideoFinished = true;
            mDidInitialDraw = false;
            mNeedToStartPlaying = false;
            mMainMenuButton.SetVisible(true);
            mMainMenuButton.Move(mApp.mWidth / 2 - mMainMenuButton.mWidth / 2, mMainMenuButton.mY);
        }

        public override void Dispose()
        {
            mReplayButton.Dispose();
            mMainMenuButton.Dispose();
            RemoveAllWidgets(true);
        }

        public void AppGotFocus()
        {
            if (!mNeedToStartPlaying && !videoDone)
            {
                mApp.MoviePlayerPlaybackDidFinish();
                videoDone = true;
            }
        }

        public void DrawCredits(Graphics g)
        {
            g.SetClipRect(0, 0, mWidth, Constants.CreditScreen_TextClip);
            g.HardwareClip();
            Resources.FONT_HOUSEOFTERROR16.EnableLayer(0, false);
            int num = (int)mCreditsY;
            int num2 = 0;
            while (num2 < mNumSections && num <= Constants.BOARD_HEIGHT)
            {
                num = CreditScreen.DrawSection(g, mRoles[num2], mNames[num2], num) + (int)(Constants.S * 5f);
                if (mRoles[num2].Length == 0 || mRoles[num2][0] != '^')
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
            for (int i = 0; i < mNumSections; i++)
            {
                num = CreditScreen.DrawSection(@new, mRoles[i], mNames[i], num) + (int)(Constants.S * 5f);
                if (mRoles[i].Length == 0 || mRoles[i][0] != '^')
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
            if (!mVideoLoading)
            {
                DrawCredits(g);
            }
            mDidInitialDraw = true;
        }

        public override void MouseUp(int x, int y, int theClickCount)
        {
        }

        public override void TouchBegan(_Touch touch)
        {
            touching = true;
            touchStartPosition = touch.location;
            base.TouchBegan(touch);
        }

        public override void TouchEnded(_Touch touch)
        {
            touching = false;
            base.TouchEnded(touch);
        }

        public override void TouchMoved(_Touch touch)
        {
            if (touching)
            {
                mCreditsY += touch.location.y - touchStartPosition.y;
                touchStartPosition = touch.location;
            }
            base.TouchMoved(touch);
        }

        public override void AddedToManager(WidgetManager theWidgetManager)
        {
            base.AddedToManager(theWidgetManager);
            AddWidget(mMainMenuButton);
            AddWidget(mReplayButton);
        }

        public override void RemovedFromManager(WidgetManager theWidgetManager)
        {
            base.RemovedFromManager(theWidgetManager);
            RemoveWidget(mMainMenuButton);
            RemoveWidget(mReplayButton);
        }

        public void ButtonPress(int theId)
        {
            touching = false;
            if (theId == 1)
            {
                mApp.PlaySample(Resources.SOUND_GRAVEBUTTON);
            }
            if (theId == 0)
            {
                mApp.PlaySample(Resources.SOUND_TAP);
            }
        }

        public void ButtonDepress(int theId)
        {
            touching = false;
            if (theId == 1)
            {
                mApp.KillCreditScreen();
                mApp.DoBackToMain();
            }
            if (theId == 0)
            {
                mApp.KillCreditScreen();
                mApp.ShowCreditScreen();
            }
        }

        public override bool BackButtonPress()
        {
            mApp.KillCreditScreen();
            mApp.ShowGameSelectorWithOptions();
            return true;
        }

        public override void Update()
        {
            if (touching)
            {
                return;
            }
            mCreditsY -= 3f * Constants.InvertAndScale(0.25f);
            if (mCreditsY < (float)(-(mCreditsHeight + Constants.CreditScreen_TextStart)))
            {
                RestartScroll();
            }
            if (mCreditsY > Constants.CreditScreen_TextEnd)
            {
                RestartScroll();
            }
            if (mDidInitialDraw && mNeedToStartPlaying)
            {
                mApp.LostFocus();
                if (mApp.PlayMovie(VideoType.Credits, MovieScalingMode.None, MovieControllMode.Default, SexyColor.Black))
                {
                    videoDone = false;
                }
                else
                {
                    videoDone = true;
                    VideoFinished();
                }
                mNeedToStartPlaying = false;
            }
        }

        public void RestartScroll()
        {
            mCreditsY = Constants.CreditScreen_TextEnd;
        }

        public void VideoLoaded(bool succeeded)
        {
            mVideoLoading = false;
            if (!succeeded)
            {
                VideoFinished();
            }
        }

        public void VideoFinished()
        {
            mVideoFinished = true;
            mVideoLoading = false;
            mReplayButton.SetVisible(true);
            mMainMenuButton.SetVisible(true);
            if (MusicInterface.USER_MUSIC_PLAYING)
            {
                MediaPlayer.Resume();
            }
            else
            {
                mApp.mMusic.MakeSureMusicIsPlaying(MusicTune.DayGrasswalk);
            }
            RestartScroll();
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
