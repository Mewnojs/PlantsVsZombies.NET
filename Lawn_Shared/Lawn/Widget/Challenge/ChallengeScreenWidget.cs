using System;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
	public/*internal*/ class ChallengeScreenWidget : Widget
	{
		public ChallengeScreenWidget(LawnApp theApp)
		{
			mApp = theApp;
			mWidth = 280;
			mHeight = 220;
		}

		public void SetSize(int width, int height)
		{
			mWidth = width;
			mHeight = height;
		}

		public override void Draw(Graphics g)
		{
			g.HardwareClip();
			for (int i = 0; i < ChallengeScreen.gChallengeDefs.Length; i++)
			{
				ChallengeDefinition challengeDefinition = ChallengeScreen.gChallengeDefs[i];
				ButtonWidget buttonWidget = mApp.mChallengeScreen.mChallengeButton[i];
				if (buttonWidget.mVisible)
				{
					int num = buttonWidget.mX;
					int num2 = buttonWidget.mY;
					if (buttonWidget.mIsDown)
					{
						num++;
						num2++;
					}
					int num3 = mApp.mChallengeScreen.AccomplishmentsNeeded(i);
					if (num3 <= 1)
					{
						if (buttonWidget.mDisabled)
						{
							g.SetColor(new SexyColor(92, 92, 92));
							g.SetColorizeImages(true);
						}
						if (i == mApp.mChallengeScreen.mUnlockChallengeIndex)
						{
							if (mApp.mChallengeScreen.mUnlockState == UnlockingState.Shaking)
							{
								g.SetColor(new SexyColor(92, 92, 92));
							}
							else if (mApp.mChallengeScreen.mUnlockState == UnlockingState.Fading)
							{
								int num4 = TodCommon.TodAnimateCurve(50, 25, mApp.mChallengeScreen.mUnlockStateCounter, 92, 255, TodCurves.Linear);
								g.SetColor(new SexyColor(num4, num4, num4));
							}
							g.SetColorizeImages(true);
						}
						ChallengePage pageIndex = mApp.mChallengeScreen.mPageIndex;
						g.SetColorizeImages(false);
						if (buttonWidget.mIsOver)
						{
							int unlockChallengeIndex = mApp.mChallengeScreen.mUnlockChallengeIndex;
						}
					}
					SexyColor theColor = new SexyColor(42, 42, 90);
					if (buttonWidget.mIsOver && i != mApp.mChallengeScreen.mUnlockChallengeIndex)
					{
						theColor = new SexyColor(250, 40, 40);
					}
					string text = TodStringFile.TodStringTranslate(challengeDefinition.mChallengeName);
					if (i == mApp.mChallengeScreen.mUnlockChallengeIndex && mApp.mChallengeScreen.mUnlockState == UnlockingState.Shaking)
					{
						text = "?";
					}
					else if (buttonWidget.mDisabled)
					{
						text = "?";
					}
					int num5 = text.length();
					if (num5 < 13)
					{
						TodCommon.TodDrawString(g, text, num + base.M(22), num2 + base.M1(56), Resources.FONT_BRIANNETOD12, theColor, DrawStringJustification.Center);
					}
					else
					{
						int num6 = num5 / 2 - 1;
						if (mApp.mChallengeScreen.mPageIndex == ChallengePage.Survival && !buttonWidget.mDisabled)
						{
							num6 = 7;
						}
						int num7 = text.IndexOf(' ', num6);
						if (num7 == -1)
						{
							num7 = text.IndexOf(' ');
						}
						int num8 = num5;
						int num9 = 0;
						if (num7 != -1)
						{
							num8 = num7;
							num9 = num5 - num8 - 1;
						}
						string theText = text.Substring(0, num8);
						TodCommon.TodDrawString(g, theText, num + 22, num2 + 48, Resources.FONT_BRIANNETOD12, theColor, DrawStringJustification.Center);
						if (num9 > 0)
						{
							string theText2 = text.Substring(num8 + 1, num9);
							TodCommon.TodDrawString(g, theText2, num + 22, num2 + 65, Resources.FONT_BRIANNETOD12, theColor, DrawStringJustification.Center);
						}
					}
					int num10 = mApp.mPlayerInfo.mChallengeRecords[i];
					if (i == mApp.mChallengeScreen.mUnlockChallengeIndex)
					{
						if (mApp.mChallengeScreen.mUnlockState == UnlockingState.Fading)
						{
							int theAlpha = TodCommon.TodAnimateCurve(25, 0, mApp.mChallengeScreen.mUnlockStateCounter, 255, 0, TodCurves.Linear);
							g.SetColor(new SexyColor(255, 255, 255, theAlpha));
							g.SetColorizeImages(true);
						}
						g.SetColorizeImages(false);
					}
					else if (num10 > 0)
					{
						if (mApp.HasBeatenChallenge(challengeDefinition.mChallengeMode))
						{
							g.DrawImage(AtlasResources.IMAGE_TROPHY, num - base.M(3), num2 - base.M1(2));
						}
						else if (mApp.IsEndlessScaryPotter(challengeDefinition.mChallengeMode) || mApp.IsEndlessIZombie(challengeDefinition.mChallengeMode))
						{
							string theText3 = TodCommon.TodReplaceNumberString("[LONGEST_STREAK]", "{STREAK}", num10);
							TRect theRect = new TRect(num, num2 + 15, 96, 200);
							TodStringFile.TodDrawStringWrapped(g, theText3, theRect, Resources.FONT_CONTINUUMBOLD14OUTLINE, new SexyColor(255, 255, 255), DrawStringJustification.Center);
							TodStringFile.TodDrawStringWrapped(g, theText3, theRect, Resources.FONT_CONTINUUMBOLD14, new SexyColor(255, 0, 0), DrawStringJustification.Center);
						}
					}
					else
					{
						bool disabled = buttonWidget.mDisabled;
					}
				}
			}
			g.EndHardwareClip();
		}

		public LawnApp mApp;
	}
}
