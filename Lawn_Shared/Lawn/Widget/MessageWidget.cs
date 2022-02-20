using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
    public/*internal*/ class MessageWidget
    {
        public MessageWidget(LawnApp theApp)
        {
            mApp = theApp;
            mDuration = 0;
            mLabel = string.Empty;
            mMessageStyle = MessageStyle.Off;
            mLabelNext[0] = char.MaxValue;
            mMessageStyleNext = MessageStyle.Off;
            mSlideOffTime = 100;
            mIcon = null;
            for (int i = 0; i < 128; i++)
            {
                mTextReanimID[i] = null;
            }
        }

        public void Dispose()
        {
            ClearReanim();
        }

        public void SetLabel(string theNewLabel, MessageStyle theMessageStyle)
        {
            SetLabel(theNewLabel, theMessageStyle, null);
        }

        public void SetLabel(string theNewLabel, MessageStyle theMessageStyle, Image theIcon)
        {
            string text = TodStringFile.TodStringTranslate(theNewLabel);
            Debug.ASSERT(text.length() < 127);
            if (mReanimType != ReanimationType.None && mDuration > 0)
            {
                mMessageStyleNext = theMessageStyle;
                mLabelNext = text.ToCharArray();//string.Copy(text).ToCharArray();
                mDuration = Math.Min(mDuration, 100 + mSlideOffTime + 1);
                return;
            }
            ClearReanim();
            mLabel = text;
            mLabelString = text;//string.Copy(text);
            mLabelStringList.Clear();
            for (int i = 0; i < mLabel.Length; i++)
            {
                mLabelStringList.Add(mLabel[i].ToString());
            }
            mMessageStyle = theMessageStyle;
            mReanimType = ReanimationType.None;
            if (theMessageStyle == MessageStyle.HintLong || theMessageStyle == MessageStyle.BigMiddle || theMessageStyle == MessageStyle.ZenGardenLong || theMessageStyle == MessageStyle.HintTallLong)
            {
                mDuration = 1500;
            }
            else if (theMessageStyle == MessageStyle.HintTallUnlockmessage)
            {
                mDuration = 500;
            }
            else if (theMessageStyle == MessageStyle.HintFast || theMessageStyle == MessageStyle.HintTallFast || theMessageStyle == MessageStyle.BigMiddleFast || theMessageStyle == MessageStyle.TutorialLevel1 || theMessageStyle == MessageStyle.TutorialLevel2 || theMessageStyle == MessageStyle.TutorialLater)
            {
                mDuration = 500;
            }
            else if (theMessageStyle == MessageStyle.Achievement)
            {
                mDuration = 250;
            }
            else if (theMessageStyle == MessageStyle.HintStay || theMessageStyle == MessageStyle.TutorialLevel1Stay || theMessageStyle == MessageStyle.TutorialLaterStay)
            {
                mDuration = 10000;
            }
            else if (theMessageStyle == MessageStyle.HouseName)
            {
                mDuration = 250;
            }
            else if (theMessageStyle == MessageStyle.HugeWave)
            {
                mDuration = 750;
                mReanimType = ReanimationType.TextFadeOn;
            }
            else if (theMessageStyle == MessageStyle.SlotMachine)
            {
                mDuration = 750;
            }
            else
            {
                Debug.ASSERT(false);
            }
            if (mReanimType != ReanimationType.None)
            {
                LayoutReanimText();
            }
            mDisplayTime = mDuration;
            mIcon = theIcon;
        }

        public void Update()//3update
        {
            if (mApp.mBoard == null || mApp.mBoard.mPaused)
            {
                return;
            }
            if (mDuration < 10000 && mDuration > 0)
            {
                //mDuration -= 3;
                mDuration--;
                //if (mDuration >= 0 && mDuration < 3)
                if (mDuration == 0)
                {
                    mMessageStyle = MessageStyle.Off;
                    mIcon = null;
                    if (mMessageStyleNext != MessageStyle.Off)
                    {
                        SetLabel(new string(mLabelNext), mMessageStyleNext);
                        mMessageStyleNext = MessageStyle.Off;
                    }
                }
            }
            for (int i = 0; i < mLabel.Length; i++)
            {
                Reanimation reanimation = mApp.ReanimationTryToGet(mTextReanimID[i]);
                if (reanimation == null)
                {
                    return;
                }
                int theTimeEnd = 50;
                int num = 1;
                if (mReanimType == ReanimationType.TextFadeOn)
                {
                    num = 100;
                }
                if (mDuration > mSlideOffTime)
                {
                    if (mReanimType == ReanimationType.TextFadeOn)
                    {
                        reanimation.mAnimRate = 60f;
                    }
                    else
                    {
                        int num2 = (mDisplayTime - mDuration) * num;
                        reanimation.mAnimRate = TodCommon.TodAnimateCurveFloat(0, theTimeEnd, num2 - i, 0f, 40f, TodCurves.Linear);
                    }
                }
                else
                {
                    //if (mDuration >= mSlideOffTime && mDuration < mSlideOffTime + 3)
                    if (mDuration == mSlideOffTime)
                    {
                        reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_leave, ReanimLoopType.PlayOnceAndHold, 0, 0f);
                    }
                    int num3 = (mSlideOffTime - mDuration) * num;
                    reanimation.mAnimRate = TodCommon.TodAnimateCurveFloat(0, theTimeEnd, num3 - i, 0f, 40f, TodCurves.Linear);
                }
                reanimation.Update();
            }
        }

        public void Draw(Graphics g)
        {
            if (mDuration <= 3)
            {
                return;
            }
            Font font = GetFont();
            Font font2 = null;
            int num = Constants.BOARD_WIDTH / 2;
            int num2 = 596;
            int num3 = 255;
            SexyColor theColor = new SexyColor(250, 250, 0, 255);
            SexyColor theColor2 = new SexyColor(0, 0, 0, 255);
            bool flag = false;
            int num4 = 0;
            int num5 = 0;
            if (font == Resources.FONT_CONTINUUMBOLD14)
            {
                font2 = Resources.FONT_CONTINUUMBOLD14OUTLINE;
            }
            switch (mMessageStyle)
            {
            case MessageStyle.TutorialLevel1:
            case MessageStyle.TutorialLevel1Stay:
                num2 = 476;
                num4 = (int)Constants.InvertAndScale(60f);
                theColor = new SexyColor(253, 245, 173);
                num3 = 192;
                break;
            case MessageStyle.TutorialLevel2:
            case MessageStyle.TutorialLater:
            case MessageStyle.TutorialLaterStay:
                num2 = 476;
                num4 = (int)Constants.InvertAndScale(60f);
                theColor = new SexyColor(253, 245, 173);
                num3 = 192;
                break;
            case MessageStyle.HintLong:
            case MessageStyle.HintFast:
            case MessageStyle.HintStay:
                num2 = 527;
                num4 = (int)Constants.InvertAndScale(40f);
                theColor = new SexyColor(253, 245, 173);
                num3 = 192;
                break;
            case MessageStyle.HintTallFast:
            case MessageStyle.HintTallUnlockmessage:
            case MessageStyle.HintTallLong:
                num2 = 476;
                num4 = (int)Constants.InvertAndScale(70f);
                theColor = new SexyColor(253, 245, 173);
                num3 = 192;
                num5 = 30;
                break;
            case MessageStyle.BigMiddle:
            case MessageStyle.BigMiddleFast:
                num2 = 476;
                num4 = (int)Constants.InvertAndScale(60f);
                theColor = new SexyColor(253, 245, 173);
                num3 = 192;
                break;
            case MessageStyle.HouseName:
                num2 = 550;
                theColor = new SexyColor(255, 255, 255, 255);
                flag = true;
                break;
            case MessageStyle.HugeWave:
                num2 = 290;
                theColor = new SexyColor(255, 0, 0);
                break;
            case MessageStyle.SlotMachine:
                num = mApp.mWidth / 2 + Constants.Board_Offset_AspectRatio_Correction;
                num2 = Constants.MessageWidget_SlotMachine_Y;
                num3 = 64;
                break;
            case MessageStyle.ZenGardenLong:
                num2 = 514;
                num4 = (int)Constants.InvertAndScale(40f);
                theColor = new SexyColor(253, 245, 173);
                num3 = 192;
                break;
            case MessageStyle.Achievement:
                num2 = 466;
                num4 = (int)Constants.InvertAndScale(40f);
                theColor = new SexyColor(253, 245, 173);
                num3 = 192;
                break;
            default:
                Debug.ASSERT(false);
                break;
            }
            num2 = (int)(num2 * Constants.S);
            if (mReanimType != ReanimationType.None)
            {
                if (font == Resources.FONT_CONTINUUMBOLD14)
                {
                    DrawReanimatedText(g, Resources.FONT_CONTINUUMBOLD14OUTLINE, SexyColor.Black, num2);
                }
                DrawReanimatedText(g, font, theColor, num2);
                return;
            }
            if (num3 != 255)
            {
                theColor.mAlpha = TodCommon.TodAnimateCurve(75, 0, mApp.mBoard.mMainCounter % 75, num3, 255, TodCurves.BounceSlowMiddle);
                theColor2.mAlpha = theColor.mAlpha;
            }
            if (flag)
            {
                theColor.mAlpha = TodCommon.ClampInt(mDuration * 15, 0, 255);
                theColor2.mAlpha = theColor.mAlpha;
            }
            if (num4 > 0)
            {
                num2 -= (int)Constants.InvertAndScale(30f);
                TRect theRect = new TRect(-mApp.mBoard.mX, num2, Constants.BOARD_WIDTH, num4);
                g.SetColor(new SexyColor(0, 0, 0, 128));
                g.SetColorizeImages(true);
                g.FillRect(theRect);
                theRect.mY -= (int)Constants.InvertAndScale(3f);
                theRect.mX += num5;
                TodStringFile.TodDrawStringWrapped(g, mLabel, theRect, font, theColor, DrawStringJustification.CenterVerticalMiddle);
            }
            else
            {
                TRect theRect2 = new TRect(num - mApp.mWidth / 2 - mApp.mBoard.mX, num2 + font.GetAscent(), mApp.mWidth, mApp.mHeight);
                if (font2 != null)
                {
                    TodStringFile.TodDrawStringWrapped(g, mLabel, theRect2, font2, theColor2, DrawStringJustification.Center);
                }
                TodStringFile.TodDrawStringWrapped(g, mLabel, theRect2, font, theColor, DrawStringJustification.Center);
            }
            if (mMessageStyle == MessageStyle.HouseName)
            {
                string text = string.Empty;
                if (mApp.IsSurvivalMode() && mApp.mBoard.mChallenge.mSurvivalStage > 0)
                {
                    int numWavesPerFlag = mApp.mBoard.GetNumWavesPerFlag();
                    int num6 = mApp.mBoard.mChallenge.mSurvivalStage * mApp.mBoard.GetNumWavesPerSurvivalStage() / numWavesPerFlag;
                    string theStringToSubstitute = mApp.Pluralize(num6, "[ONE_FLAG]", "[COUNT_FLAGS]");
                    if (num6 == 1)
                    {
                        text = TodCommon.TodReplaceString("[FLAGS_COMPLETED]", "{FLAGS}", theStringToSubstitute);
                    }
                    else
                    {
                        text = TodCommon.TodReplaceString("[FLAGS_COMPLETED_PLURAL]", "{FLAGS}", theStringToSubstitute);
                    }
                }
                if (text.length() > 0)
                {
                    TodCommon.TodDrawString(g, text, mApp.mWidth / 2 - mApp.mBoard.mX, num2 + 26, Resources.FONT_HOUSEOFTERROR16, new SexyColor(224, 187, 98, theColor.mAlpha), DrawStringJustification.Center);
                }
            }
            if (mMessageStyle == MessageStyle.Achievement && mIcon != null)
            {
                g.DrawImage(mIcon, 10, num2 - 10);
            }
            g.SetColor(SexyColor.White);
            g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
        }

        public void ClearReanim()
        {
            for (int i = 0; i < 128; i++)
            {
                Reanimation reanimation = mApp.ReanimationTryToGet(mTextReanimID[i]);
                if (reanimation != null)
                {
                    reanimation.ReanimationDie();
                    mTextReanimID[i] = null;
                }
            }
        }

        public void ClearLabel()
        {
            if (mReanimType != ReanimationType.None)
            {
                if (mApp.mGameMode == GameMode.ChallengeSpeed)
                {
                    mDuration = Math.Min(mDuration, 51);
                }
                else
                {
                    mDuration = Math.Min(mDuration, 100 + mSlideOffTime + 1);
                }
            }
            else
            {
                mDuration = 0;
            }
            mIcon = null;
        }

        public bool IsBeingDisplayed()
        {
            //return mDuration < 0 || mDuration >= 3;
            return mDuration != 0;
        }

        public Font GetFont()
        {
            return Resources.FONT_HOUSEOFTERROR16;
        }

        public void DrawReanimatedText(Graphics g, Font theFont, SexyColor theColor, float thePosY)
        {
            int length = mLabel.Length;
            for (int i = 0; i < mLabelStringList.Count; i++)
            {
                if (!string.IsNullOrEmpty(mLabelStringList[i]))
                {
                    Reanimation reanimation = mApp.ReanimationTryToGet(mTextReanimID[i]);
                    if (reanimation == null)
                    {
                        return;
                    }
                    ReanimatorTransform reanimatorTransform;
                    reanimation.GetCurrentTransform(2, out reanimatorTransform, false);
                    int num = TodCommon.ClampInt(TodCommon.FloatRoundToInt(reanimatorTransform.mAlpha * theColor.mAlpha), 0, 255);
                    if (num <= 0)
                    {
                        return;
                    }
                    SexyColor theColor2 = theColor;
                    theColor2.mAlpha = num;
                    reanimatorTransform.mTransX += reanimation.mOverlayMatrix.mMatrix.M41 + Constants.ReanimTextCenterOffsetX - Constants.Board_Offset_AspectRatio_Correction / 2;
                    reanimatorTransform.mTransY += reanimation.mOverlayMatrix.mMatrix.M42 + thePosY - 300f * Constants.S;
                    if (mReanimType == ReanimationType.TextFadeOn && mDisplayTime - mDuration < mSlideOffTime)
                    {
                        float num2 = 1f - reanimation.mAnimTime;
                        reanimatorTransform.mTransX += reanimation.mOverlayMatrix.mMatrix.M41 * num2;
                    }
                    Matrix theMatrix = default(Matrix);
                    Reanimation.MatrixFromTransform(reanimatorTransform, out theMatrix);
                    TodCommon.TodDrawStringMatrix(g, theFont, theMatrix, mLabelStringList[i], theColor2);
                    reanimatorTransform.PrepareForReuse();
                }
            }
        }

        public void LayoutReanimText()
        {
            float[] array = new float[5];
            int num = 0;
            float num2 = 0f;
            int num3 = 0;
            Font font = GetFont();
            int length = mLabel.Length;
            mSlideOffTime = 100 + length;
            for (int i = 0; i <= length; i++)
            {
                if (i == length || mLabel[i] == '\n')
                {
                    Debug.ASSERT(num < 5);
                    int num4 = i - num3;
                    int num5 = num3;
                    num3 = i + 1;
                    string theString = new string(mLabel[num5], num4);
                    array[num] = font.StringWidth(theString);
                    num2 = Math.Max(num2, array[num]);
                    num++;
                }
            }
            num = 0;
            float num6 = -array[num] * 0.5f;
            float num7 = 0f;
            for (int j = 0; j < length; j++)
            {
                Reanimation reanimation = mApp.AddReanimation(num6 * Constants.IS, num7 * Constants.IS, 0, mReanimType);
                reanimation.mIsAttachment = true;
                reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_enter, ReanimLoopType.PlayOnceAndHold, 0, 0f);
                mTextReanimID[j] = mApp.ReanimationGetID(reanimation);
                num6 += font.CharWidth(mLabel[j]);
                if (mLabel[j] == '\n')
                {
                    num++;
                    Debug.ASSERT(num < 5);
                    num6 = -array[num] * 0.5f;
                    num7 += font.GetLineSpacing();
                }
            }
        }

        public LawnApp mApp;

        public string mLabel = string.Empty;

        public string mLabelString;

        public List<string> mLabelStringList = new List<string>();

        public int mDisplayTime;

        public int mDuration;

        public MessageStyle mMessageStyle;

        public Reanimation[] mTextReanimID = new Reanimation[128];

        public ReanimationType mReanimType;

        public int mSlideOffTime;

        public Image mIcon;

        public char[] mLabelNext = new char[128];

        public MessageStyle mMessageStyleNext;
    }
}
