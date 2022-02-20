using System;
using System.Collections.Generic;

namespace Sexy.TodLib
{
    internal class ReanimatorXnaHelpers
    {
        public static string ReanimatorTrackNameToId(string theName)
        {
            string text = Common.StringToLower(ref theName);
            int num = ReanimatorXnaHelpers.gReanimTrackIds.IndexOf(text);
            if (num == -1)
            {
                ReanimatorXnaHelpers.gReanimTrackIds.Add(text);
            }
            return text;
        }

        public static void ReanimationFillInMissingData(ref float thePrev, ref float theValue)
        {
            if (theValue == ReanimatorXnaHelpers.DEFAULT_FIELD_PLACEHOLDER)
            {
                theValue = thePrev;
                return;
            }
            thePrev = theValue;
        }

        public static bool ReanimationLoadDefinition(string theFilename, ref ReanimatorDefinition theDefinition)
        {
            if (!GlobalStaticVars.gSexyAppBase.mResourceManager.LoadReanimation(theFilename, ref theDefinition))
            {
                return false;
            }
            for (int i = 0; i < theDefinition.mTrackCount; i++)
            {
                ReanimatorTrack reanimatorTrack = theDefinition.mTracks[i];
                if (reanimatorTrack.mName == "zombie_butter")
                {
                    int num = 0;
                    num++;
                }
                float num2 = 0f;
                float num3 = 0f;
                float num4 = 0f;
                float num5 = 0f;
                float num6 = 1f;
                float num7 = 1f;
                float num8 = 0f;
                float num9 = 1f;
                Image anImage = null;
                string anImageName = string.Empty;
                Font aFont = null;
                string aText = string.Empty;
                for (int j = 0; j < reanimatorTrack.mTransformCount; j++)
                {
                    ReanimatorTransform reanimatorTransform = reanimatorTrack.mTransforms[j];
                    ReanimatorXnaHelpers.ReanimationFillInMissingData(ref num2, ref reanimatorTransform.mTransX);
                    ReanimatorXnaHelpers.ReanimationFillInMissingData(ref num3, ref reanimatorTransform.mTransY);
                    ReanimatorXnaHelpers.ReanimationFillInMissingData(ref num4, ref reanimatorTransform.mSkewX);
                    ReanimatorXnaHelpers.ReanimationFillInMissingData(ref num5, ref reanimatorTransform.mSkewY);
                    ReanimatorXnaHelpers.ReanimationFillInMissingData(ref num6, ref reanimatorTransform.mScaleX);
                    ReanimatorXnaHelpers.ReanimationFillInMissingData(ref num7, ref reanimatorTransform.mScaleY);
                    ReanimatorXnaHelpers.ReanimationFillInMissingData(ref num8, ref reanimatorTransform.mFrame);
                    ReanimatorXnaHelpers.ReanimationFillInMissingData(ref num9, ref reanimatorTransform.mAlpha);
                    reanimatorTransform.mSkewXCos = (float)Math.Cos((double)(reanimatorTransform.mSkewX * -(double)TodCommon.DEG_TO_RAD));
                    reanimatorTransform.mSkewXSin = (float)Math.Sin((double)(reanimatorTransform.mSkewX * -(double)TodCommon.DEG_TO_RAD));
                    reanimatorTransform.mSkewYCos = (float)Math.Cos((double)(reanimatorTransform.mSkewY * -(double)TodCommon.DEG_TO_RAD));
                    reanimatorTransform.mSkewYSin = (float)Math.Sin((double)(reanimatorTransform.mSkewY * -(double)TodCommon.DEG_TO_RAD));
                    if (reanimatorTransform.mImage == null)
                    {
                        reanimatorTransform.mImage = anImage;
                        reanimatorTransform.mImageName = anImageName;
                    }
                    else
                    {
                        anImage = reanimatorTransform.mImage;
                        anImageName = reanimatorTransform.mImageName;
                    }
                    if (reanimatorTransform.mFont == null)
                    {
                        reanimatorTransform.mFont = aFont;
                    }
                    else
                    {
                        aFont = reanimatorTransform.mFont;
                    }
                    if (string.IsNullOrEmpty(reanimatorTransform.mText))
                    {
                        reanimatorTransform.mText = aText;
                    }
                    else
                    {
                        aText = reanimatorTransform.mText;
                    }
                    reanimatorTrack.mTransforms[j] = reanimatorTransform;
                }
            }
            return true;
        }

        public static void ReanimationFreeDefinition(ref ReanimatorDefinition theDefinition)
        {
            if (theDefinition.mReanimAtlas != null)
            {
                theDefinition.mReanimAtlas.ReanimAtlasDispose();
                theDefinition.mReanimAtlas = null;
            }
            for (int i = 0; i < theDefinition.mTrackCount; i++)
            {
                ReanimatorTrack reanimatorTrack = theDefinition.mTracks[i];
                string text = null;
                for (int j = 0; j < reanimatorTrack.mTransformCount; j++)
                {
                    ReanimatorTransform reanimatorTransform = reanimatorTrack.mTransforms[j];
                    if (!string.IsNullOrEmpty(reanimatorTransform.mText) && reanimatorTransform.mText == text)
                    {
                        reanimatorTransform.mText = "";
                    }
                    else
                    {
                        text = reanimatorTransform.mText;
                    }
                    reanimatorTrack.mTransforms[j] = reanimatorTransform;
                }
            }
        }

        public static void ReanimatorLoadDefinitions(ref ReanimationParams[] theReanimationParamArray, int theReanimationParamArraySize)
        {
            ReanimatorXnaHelpers.gReanimationParamArraySize = theReanimationParamArraySize;
            ReanimatorXnaHelpers.gReanimationParamArray = theReanimationParamArray;
            ReanimatorXnaHelpers.gReanimatorDefCount = theReanimationParamArraySize;
            Array.Clear(ReanimatorXnaHelpers.gReanimatorDefArray, 0, ReanimatorXnaHelpers.gReanimatorDefArray.Length);
            for (int i = 0; i < ReanimatorXnaHelpers.gReanimationParamArraySize - 1; i++)
            {
                ReanimationParams reanimationParams = theReanimationParamArray[i];
                ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(reanimationParams.mReanimationType, true);
                ReanimatorXnaHelpers.mLoadingProgress = i / (double)(ReanimatorXnaHelpers.gReanimationParamArraySize - 1);
                ReanimatorXnaHelpers.mLoadedResources++;
            }
        }

        public static void ReanimatorFreeDefinitions()
        {
            for (int i = 0; i < ReanimatorXnaHelpers.gReanimatorDefCount; i++)
            {
                ReanimatorXnaHelpers.gReanimatorDefArray[i] = null;
            }
            ReanimatorXnaHelpers.gReanimatorDefArray = null;
            ReanimatorXnaHelpers.gReanimatorDefCount = 0;
            ReanimatorXnaHelpers.gReanimationParamArray = null;
            ReanimatorXnaHelpers.gReanimationParamArraySize = 0;
        }

        public static void ReanimatorEnsureDefinitionLoaded(ReanimationType theReanimType, bool theIsPreloading)
        {
            ReanimatorDefinition reanimatorDefinition = ReanimatorXnaHelpers.gReanimatorDefArray[(int)theReanimType];
            if (reanimatorDefinition != null && reanimatorDefinition.mTracks != null && reanimatorDefinition.mTrackCount != 0)
            {
                return;
            }
            ReanimationParams reanimationParams = ReanimatorXnaHelpers.gReanimationParamArray[(int)theReanimType];
            if (theIsPreloading && GlobalStaticVars.gSexyAppBase.mShutdown)
            {
                return;
            }
            PerfTimer perfTimer = default(PerfTimer);
            perfTimer.Start();
            ReanimatorXnaHelpers.ReanimationLoadDefinition(reanimationParams.mReanimFileName, ref reanimatorDefinition);
            int num = Math.Max((int)perfTimer.GetDuration(), 0);
            ReanimatorXnaHelpers.gReanimatorDefArray[(int)theReanimType] = reanimatorDefinition;
        }

        public static void ReanimationPreload(ReanimationType theReanimationType)
        {
        }

        public static void BlendTransform(out ReanimatorTransform theResult, ref ReanimatorTransform theTransform1, ref ReanimatorTransform theTransform2, float theBlendFactor)
        {
            theResult = ReanimatorTransform.GetNewReanimatorTransform();
            theResult.mTransX = TodCommon.FloatLerp(theTransform1.mTransX, theTransform2.mTransX, theBlendFactor);
            theResult.mTransY = TodCommon.FloatLerp(theTransform1.mTransY, theTransform2.mTransY, theBlendFactor);
            theResult.mScaleX = TodCommon.FloatLerp(theTransform1.mScaleX, theTransform2.mScaleX, theBlendFactor);
            theResult.mScaleY = TodCommon.FloatLerp(theTransform1.mScaleY, theTransform2.mScaleY, theBlendFactor);
            theResult.mAlpha = TodCommon.FloatLerp(theTransform1.mAlpha, theTransform2.mAlpha, theBlendFactor);
            float num = theTransform2.mSkewX;
            float num2 = theTransform2.mSkewY;
            while (num > theTransform1.mSkewX + 180f)
            {
                num -= 360f;
                num = theTransform1.mSkewX;
            }
            while (num < theTransform1.mSkewX - 180f)
            {
                num += 360f;
                num = theTransform1.mSkewX;
            }
            while (num2 > theTransform1.mSkewY + 180f)
            {
                num2 -= 360f;
                num2 = theTransform1.mSkewY;
            }
            while (num2 < theTransform1.mSkewY - 180f)
            {
                num2 += 360f;
                num2 = theTransform1.mSkewY;
            }
            theResult.mSkewX = TodCommon.FloatLerp(theTransform1.mSkewX, num, theBlendFactor);
            theResult.mSkewY = TodCommon.FloatLerp(theTransform1.mSkewY, num2, theBlendFactor);
            theResult.mSkewXCos = (float)Math.Cos((double)(theResult.mSkewX * -(double)TodCommon.DEG_TO_RAD));
            theResult.mSkewXSin = (float)Math.Sin((double)(theResult.mSkewX * -(double)TodCommon.DEG_TO_RAD));
            theResult.mSkewYCos = (float)Math.Cos((double)(theResult.mSkewY * -(double)TodCommon.DEG_TO_RAD));
            theResult.mSkewYSin = (float)Math.Sin((double)(theResult.mSkewY * -(double)TodCommon.DEG_TO_RAD));
            theResult.mFrame = theTransform1.mFrame;
            theResult.mFont = theTransform1.mFont;
            theResult.mText = theTransform1.mText;
            theResult.mImage = theTransform1.mImage;
        }

        public static float DEFAULT_FIELD_PLACEHOLDER = -99999f;

        public static short NO_BASE_POSE = -2;

        public static float EPSILON = 0.001f;

        public static int RENDER_GROUP_HIDDEN = -1;

        public static int RENDER_GROUP_NORMAL = 0;

        //public static float SECONDS_PER_UPDATE = 0.033333f;
        public static float SECONDS_PER_UPDATE = 0.01f;

        public static ReanimatorDefinition[] gReanimatorDefArray = new ReanimatorDefinition[119];

        public static ReanimationParams[] gReanimationParamArray = null;

        public static int gReanimatorDefCount = 0;

        public static int gReanimationParamArraySize = 0;

        public static List<string> gReanimTrackIds = new List<string>();

        public static double mLoadingProgress;

        public static int mTotalResources = 118;

        public static int mLoadedResources;
    }
}
