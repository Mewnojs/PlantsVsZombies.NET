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
			this.mApp = theApp;
			this.mDuration = 0;
			this.mLabel = string.Empty;
			this.mMessageStyle = MessageStyle.MESSAGE_STYLE_OFF;
			this.mLabelNext[0] = char.MaxValue;
			this.mMessageStyleNext = MessageStyle.MESSAGE_STYLE_OFF;
			this.mSlideOffTime = 100;
			this.mIcon = null;
			for (int i = 0; i < 128; i++)
			{
				this.mTextReanimID[i] = null;
			}
		}

		public void Dispose()
		{
			this.ClearReanim();
		}

		public void SetLabel(string theNewLabel, MessageStyle theMessageStyle)
		{
			this.SetLabel(theNewLabel, theMessageStyle, null);
		}

		public void SetLabel(string theNewLabel, MessageStyle theMessageStyle, Image theIcon)
		{
			string text = TodStringFile.TodStringTranslate(theNewLabel);
			Debug.ASSERT(text.length() < 127);
			if (this.mReanimType != ReanimationType.REANIM_NONE && this.mDuration > 0)
			{
				this.mMessageStyleNext = theMessageStyle;
				this.mLabelNext = string.Copy(text).ToCharArray();
				this.mDuration = Math.Min(this.mDuration, 100 + this.mSlideOffTime + 1);
				return;
			}
			this.ClearReanim();
			this.mLabel = text;
			this.mLabelString = string.Copy(text);
			this.mLabelStringList.Clear();
			for (int i = 0; i < this.mLabel.Length; i++)
			{
				this.mLabelStringList.Add(this.mLabel[i].ToString());
			}
			this.mMessageStyle = theMessageStyle;
			this.mReanimType = ReanimationType.REANIM_NONE;
			if (theMessageStyle == MessageStyle.MESSAGE_STYLE_HINT_LONG || theMessageStyle == MessageStyle.MESSAGE_STYLE_BIG_MIDDLE || theMessageStyle == MessageStyle.MESSAGE_STYLE_ZEN_GARDEN_LONG || theMessageStyle == MessageStyle.MESSAGE_STYLE_HINT_TALL_LONG)
			{
				this.mDuration = 1500;
			}
			else if (theMessageStyle == MessageStyle.MESSAGE_STYLE_HINT_TALL_UNLOCKMESSAGE)
			{
				this.mDuration = 500;
			}
			else if (theMessageStyle == MessageStyle.MESSAGE_STYLE_HINT_FAST || theMessageStyle == MessageStyle.MESSAGE_STYLE_HINT_TALL_FAST || theMessageStyle == MessageStyle.MESSAGE_STYLE_BIG_MIDDLE_FAST || theMessageStyle == MessageStyle.MESSAGE_STYLE_TUTORIAL_LEVEL1 || theMessageStyle == MessageStyle.MESSAGE_STYLE_TUTORIAL_LEVEL2 || theMessageStyle == MessageStyle.MESSAGE_STYLE_TUTORIAL_LATER)
			{
				this.mDuration = 500;
			}
			else if (theMessageStyle == MessageStyle.MESSAGE_STYLE_ACHIEVEMENT)
			{
				this.mDuration = 250;
			}
			else if (theMessageStyle == MessageStyle.MESSAGE_STYLE_HINT_STAY || theMessageStyle == MessageStyle.MESSAGE_STYLE_TUTORIAL_LEVEL1_STAY || theMessageStyle == MessageStyle.MESSAGE_STYLE_TUTORIAL_LATER_STAY)
			{
				this.mDuration = 10000;
			}
			else if (theMessageStyle == MessageStyle.MESSAGE_STYLE_HOUSE_NAME)
			{
				this.mDuration = 250;
			}
			else if (theMessageStyle == MessageStyle.MESSAGE_STYLE_HUGE_WAVE)
			{
				this.mDuration = 750;
				this.mReanimType = ReanimationType.REANIM_TEXT_FADE_ON;
			}
			else if (theMessageStyle == MessageStyle.MESSAGE_STYLE_SLOT_MACHINE)
			{
				this.mDuration = 750;
			}
			else
			{
				Debug.ASSERT(false);
			}
			if (this.mReanimType != ReanimationType.REANIM_NONE)
			{
				this.LayoutReanimText();
			}
			this.mDisplayTime = this.mDuration;
			this.mIcon = theIcon;
		}

		public void Update()
		{
			if (this.mApp.mBoard == null || this.mApp.mBoard.mPaused)
			{
				return;
			}
			if (this.mDuration < 10000 && this.mDuration > 0)
			{
				this.mDuration -= 3;
				if (this.mDuration >= 0 && this.mDuration < 3)
				{
					this.mMessageStyle = MessageStyle.MESSAGE_STYLE_OFF;
					this.mIcon = null;
					if (this.mMessageStyleNext != MessageStyle.MESSAGE_STYLE_OFF)
					{
						this.SetLabel(new string(this.mLabelNext), this.mMessageStyleNext);
						this.mMessageStyleNext = MessageStyle.MESSAGE_STYLE_OFF;
					}
				}
			}
			for (int i = 0; i < this.mLabel.Length; i++)
			{
				Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mTextReanimID[i]);
				if (reanimation == null)
				{
					return;
				}
				int theTimeEnd = 50;
				int num = 1;
				if (this.mReanimType == ReanimationType.REANIM_TEXT_FADE_ON)
				{
					num = 100;
				}
				if (this.mDuration > this.mSlideOffTime)
				{
					if (this.mReanimType == ReanimationType.REANIM_TEXT_FADE_ON)
					{
						reanimation.mAnimRate = 60f;
					}
					else
					{
						int num2 = (this.mDisplayTime - this.mDuration) * num;
						reanimation.mAnimRate = TodCommon.TodAnimateCurveFloat(0, theTimeEnd, num2 - i, 0f, 40f, TodCurves.CURVE_LINEAR);
					}
				}
				else
				{
					if (this.mDuration >= this.mSlideOffTime && this.mDuration < this.mSlideOffTime + 3)
					{
						reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_leave, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 0, 0f);
					}
					int num3 = (this.mSlideOffTime - this.mDuration) * num;
					reanimation.mAnimRate = TodCommon.TodAnimateCurveFloat(0, theTimeEnd, num3 - i, 0f, 40f, TodCurves.CURVE_LINEAR);
				}
				reanimation.Update();
			}
		}

		public void Draw(Graphics g)
		{
			if (this.mDuration <= 3)
			{
				return;
			}
			Font font = this.GetFont();
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
			switch (this.mMessageStyle)
			{
			case MessageStyle.MESSAGE_STYLE_TUTORIAL_LEVEL1:
			case MessageStyle.MESSAGE_STYLE_TUTORIAL_LEVEL1_STAY:
				num2 = 476;
				num4 = (int)Constants.InvertAndScale(60f);
				theColor = new SexyColor(253, 245, 173);
				num3 = 192;
				break;
			case MessageStyle.MESSAGE_STYLE_TUTORIAL_LEVEL2:
			case MessageStyle.MESSAGE_STYLE_TUTORIAL_LATER:
			case MessageStyle.MESSAGE_STYLE_TUTORIAL_LATER_STAY:
				num2 = 476;
				num4 = (int)Constants.InvertAndScale(60f);
				theColor = new SexyColor(253, 245, 173);
				num3 = 192;
				break;
			case MessageStyle.MESSAGE_STYLE_HINT_LONG:
			case MessageStyle.MESSAGE_STYLE_HINT_FAST:
			case MessageStyle.MESSAGE_STYLE_HINT_STAY:
				num2 = 527;
				num4 = (int)Constants.InvertAndScale(40f);
				theColor = new SexyColor(253, 245, 173);
				num3 = 192;
				break;
			case MessageStyle.MESSAGE_STYLE_HINT_TALL_FAST:
			case MessageStyle.MESSAGE_STYLE_HINT_TALL_UNLOCKMESSAGE:
			case MessageStyle.MESSAGE_STYLE_HINT_TALL_LONG:
				num2 = 476;
				num4 = (int)Constants.InvertAndScale(70f);
				theColor = new SexyColor(253, 245, 173);
				num3 = 192;
				num5 = 30;
				break;
			case MessageStyle.MESSAGE_STYLE_BIG_MIDDLE:
			case MessageStyle.MESSAGE_STYLE_BIG_MIDDLE_FAST:
				num2 = 476;
				num4 = (int)Constants.InvertAndScale(60f);
				theColor = new SexyColor(253, 245, 173);
				num3 = 192;
				break;
			case MessageStyle.MESSAGE_STYLE_HOUSE_NAME:
				num2 = 550;
				theColor = new SexyColor(255, 255, 255, 255);
				flag = true;
				break;
			case MessageStyle.MESSAGE_STYLE_HUGE_WAVE:
				num2 = 290;
				theColor = new SexyColor(255, 0, 0);
				break;
			case MessageStyle.MESSAGE_STYLE_SLOT_MACHINE:
				num = this.mApp.mWidth / 2 + Constants.Board_Offset_AspectRatio_Correction;
				num2 = Constants.MessageWidget_SlotMachine_Y;
				num3 = 64;
				break;
			case MessageStyle.MESSAGE_STYLE_ZEN_GARDEN_LONG:
				num2 = 514;
				num4 = (int)Constants.InvertAndScale(40f);
				theColor = new SexyColor(253, 245, 173);
				num3 = 192;
				break;
			case MessageStyle.MESSAGE_STYLE_ACHIEVEMENT:
				num2 = 466;
				num4 = (int)Constants.InvertAndScale(40f);
				theColor = new SexyColor(253, 245, 173);
				num3 = 192;
				break;
			default:
				Debug.ASSERT(false);
				break;
			}
			num2 = (int)((float)num2 * Constants.S);
			if (this.mReanimType != ReanimationType.REANIM_NONE)
			{
				if (font == Resources.FONT_CONTINUUMBOLD14)
				{
					this.DrawReanimatedText(g, Resources.FONT_CONTINUUMBOLD14OUTLINE, SexyColor.Black, (float)num2);
				}
				this.DrawReanimatedText(g, font, theColor, (float)num2);
				return;
			}
			if (num3 != 255)
			{
				theColor.mAlpha = TodCommon.TodAnimateCurve(75, 0, this.mApp.mBoard.mMainCounter % 75, num3, 255, TodCurves.CURVE_BOUNCE_SLOW_MIDDLE);
				theColor2.mAlpha = theColor.mAlpha;
			}
			if (flag)
			{
				theColor.mAlpha = TodCommon.ClampInt(this.mDuration * 15, 0, 255);
				theColor2.mAlpha = theColor.mAlpha;
			}
			if (num4 > 0)
			{
				num2 -= (int)Constants.InvertAndScale(30f);
				TRect theRect = new TRect(-this.mApp.mBoard.mX, num2, Constants.BOARD_WIDTH, num4);
				g.SetColor(new SexyColor(0, 0, 0, 128));
				g.SetColorizeImages(true);
				g.FillRect(theRect);
				theRect.mY -= (int)Constants.InvertAndScale(3f);
				theRect.mX += num5;
				TodStringFile.TodDrawStringWrapped(g, this.mLabel, theRect, font, theColor, DrawStringJustification.DS_ALIGN_CENTER_VERTICAL_MIDDLE);
			}
			else
			{
				TRect theRect2 = new TRect(num - this.mApp.mWidth / 2 - this.mApp.mBoard.mX, num2 + font.GetAscent(), this.mApp.mWidth, this.mApp.mHeight);
				if (font2 != null)
				{
					TodStringFile.TodDrawStringWrapped(g, this.mLabel, theRect2, font2, theColor2, DrawStringJustification.DS_ALIGN_CENTER);
				}
				TodStringFile.TodDrawStringWrapped(g, this.mLabel, theRect2, font, theColor, DrawStringJustification.DS_ALIGN_CENTER);
			}
			if (this.mMessageStyle == MessageStyle.MESSAGE_STYLE_HOUSE_NAME)
			{
				string text = string.Empty;
				if (this.mApp.IsSurvivalMode() && this.mApp.mBoard.mChallenge.mSurvivalStage > 0)
				{
					int numWavesPerFlag = this.mApp.mBoard.GetNumWavesPerFlag();
					int num6 = this.mApp.mBoard.mChallenge.mSurvivalStage * this.mApp.mBoard.GetNumWavesPerSurvivalStage() / numWavesPerFlag;
					string theStringToSubstitute = this.mApp.Pluralize(num6, "[ONE_FLAG]", "[COUNT_FLAGS]");
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
					TodCommon.TodDrawString(g, text, this.mApp.mWidth / 2 - this.mApp.mBoard.mX, num2 + 26, Resources.FONT_HOUSEOFTERROR16, new SexyColor(224, 187, 98, theColor.mAlpha), DrawStringJustification.DS_ALIGN_CENTER);
				}
			}
			if (this.mMessageStyle == MessageStyle.MESSAGE_STYLE_ACHIEVEMENT && this.mIcon != null)
			{
				g.DrawImage(this.mIcon, 10, num2 - 10);
			}
			g.SetColor(SexyColor.White);
			g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
		}

		public void ClearReanim()
		{
			for (int i = 0; i < 128; i++)
			{
				Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mTextReanimID[i]);
				if (reanimation != null)
				{
					reanimation.ReanimationDie();
					this.mTextReanimID[i] = null;
				}
			}
		}

		public void ClearLabel()
		{
			if (this.mReanimType != ReanimationType.REANIM_NONE)
			{
				if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_SPEED)
				{
					this.mDuration = Math.Min(this.mDuration, 51);
				}
				else
				{
					this.mDuration = Math.Min(this.mDuration, 100 + this.mSlideOffTime + 1);
				}
			}
			else
			{
				this.mDuration = 0;
			}
			this.mIcon = null;
		}

		public bool IsBeingDisplayed()
		{
			return this.mDuration < 0 || this.mDuration >= 3;
		}

		public Font GetFont()
		{
			return Resources.FONT_HOUSEOFTERROR16;
		}

		public void DrawReanimatedText(Graphics g, Font theFont, SexyColor theColor, float thePosY)
		{
			int length = this.mLabel.Length;
			for (int i = 0; i < this.mLabelStringList.Count; i++)
			{
				if (!string.IsNullOrEmpty(this.mLabelStringList[i]))
				{
					Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mTextReanimID[i]);
					if (reanimation == null)
					{
						return;
					}
					ReanimatorTransform reanimatorTransform;
					reanimation.GetCurrentTransform(2, out reanimatorTransform, false);
					int num = TodCommon.ClampInt(TodCommon.FloatRoundToInt(reanimatorTransform.mAlpha * (float)theColor.mAlpha), 0, 255);
					if (num <= 0)
					{
						return;
					}
					SexyColor theColor2 = theColor;
					theColor2.mAlpha = num;
					reanimatorTransform.mTransX += reanimation.mOverlayMatrix.mMatrix.M41 + (float)Constants.ReanimTextCenterOffsetX - (float)(Constants.Board_Offset_AspectRatio_Correction / 2);
					reanimatorTransform.mTransY += reanimation.mOverlayMatrix.mMatrix.M42 + thePosY - 300f * Constants.S;
					if (this.mReanimType == ReanimationType.REANIM_TEXT_FADE_ON && this.mDisplayTime - this.mDuration < this.mSlideOffTime)
					{
						float num2 = 1f - reanimation.mAnimTime;
						reanimatorTransform.mTransX += reanimation.mOverlayMatrix.mMatrix.M41 * num2;
					}
					Matrix theMatrix = default(Matrix);
					Reanimation.MatrixFromTransform(reanimatorTransform, out theMatrix);
					TodCommon.TodDrawStringMatrix(g, theFont, theMatrix, this.mLabelStringList[i], theColor2);
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
			Font font = this.GetFont();
			int length = this.mLabel.Length;
			this.mSlideOffTime = 100 + length;
			for (int i = 0; i <= length; i++)
			{
				if (i == length || this.mLabel[i] == '\n')
				{
					Debug.ASSERT(num < 5);
					int num4 = i - num3;
					int num5 = num3;
					num3 = i + 1;
					string theString = new string(this.mLabel[num5], num4);
					array[num] = (float)font.StringWidth(theString);
					num2 = Math.Max(num2, array[num]);
					num++;
				}
			}
			num = 0;
			float num6 = -array[num] * 0.5f;
			float num7 = 0f;
			for (int j = 0; j < length; j++)
			{
				Reanimation reanimation = this.mApp.AddReanimation(num6 * Constants.IS, num7 * Constants.IS, 0, this.mReanimType);
				reanimation.mIsAttachment = true;
				reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_enter, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 0, 0f);
				this.mTextReanimID[j] = this.mApp.ReanimationGetID(reanimation);
				num6 += (float)font.CharWidth(this.mLabel[j]);
				if (this.mLabel[j] == '\n')
				{
					num++;
					Debug.ASSERT(num < 5);
					num6 = -array[num] * 0.5f;
					num7 += (float)font.GetLineSpacing();
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
