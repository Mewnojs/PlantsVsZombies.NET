using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Sexy.TodLib
{
    public/*internal*/ class Reanimation
    {
        public override string ToString()
        {
            return mReanimationType.ToString();
        }

        public static string ToLower(string s)
        {
            string text;
            if (!Reanimation.lowercaseCache.TryGetValue(s, out text))
            {
                text = s.ToLower();
                Reanimation.lowercaseCache.Add(s, text);
            }
            return text;
        }

        public static void PreallocateMemory()
        {
            for (int i = 0; i < 1000; i++)
            {
                new Reanimation().PrepareForReuse();
            }
        }

        public static Reanimation GetNewReanimation()
        {
            if (Reanimation.unusedObjects.Count > 0)
            {
                return Reanimation.unusedObjects.Pop();
            }
            return new Reanimation();
        }

        public void PrepareForReuse()
        {
            Reset();
            Reanimation.unusedObjects.Push(this);
        }

        protected void Reset()
        {
            for (int i = 0; i < mTrackInstances.Length; i++)
            {
                if (mTrackInstances[i] != null)
                {
                    mTrackInstances[i].PrepareForReuse();
                }
                mTrackInstances[i] = null;
            }
            mClip = false;
            mAnimTime = 0f;
            mAnimRate = 12f;
            mLastFrameTime = -1f;
            mDefinition = null;
            mLoopType = ReanimLoopType.PlayOnce;
            mDead = false;
            mFrameStart = 0;
            mFrameCount = 0;
            mFrameBasePose = -1;
            mOverlayMatrix.LoadIdentity();
            mColorOverride = new SexyColor(Color.White);
            mExtraAdditiveColor = new SexyColor(Color.White);
            mEnableExtraAdditiveDraw = false;
            mExtraOverlayColor = new SexyColor(Color.White);
            mEnableExtraOverlayDraw = false;
            mLoopCount = 0;
            mIsAttachment = false;
            mRenderOrder = 0;
            mReanimationHolder = null;
            mFilterEffect = FilterEffectType.None;
            mReanimationType = ReanimationType.None;
            mActive = false;
            mGetFrameTime = true;
        }

        private Reanimation()
        {
            Reset();
        }

        public void ReanimationInitialize(float theX, float theY, int theDefinition)
        {
            mDefinition = ReanimatorXnaHelpers.gReanimatorDefArray[theDefinition];
            mDead = false;
            SetPosition(theX, theY);
            mAnimRate = mDefinition.mFPS;
            mLastFrameTime = -1f;
            if (mDefinition.mTrackCount != 0)
            {
                mFrameCount = mDefinition.mTracks[0].mTransformCount;
                for (int i = 0; i < mDefinition.mTrackCount; i++)
                {
                    ReanimatorTrackInstance newReanimatorTrackInstance = ReanimatorTrackInstance.GetNewReanimatorTrackInstance();
                    mTrackInstances[i] = newReanimatorTrackInstance;
                }
                return;
            }
            mFrameCount = 0;
        }

        public void ReanimationInitializeType(float theX, float theY, ReanimationType theReanimType)
        {
            ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(theReanimType, false);
            mReanimationType = theReanimType;
            ReanimationInitialize(theX, theY, (int)theReanimType);
        }

        public void ReanimationDie()
        {
            if (mDead)
            {
                return;
            }
            mActive = false;
            mDead = true;
            for (int i = 0; i < mDefinition.mTrackCount; i++)
            {
                ReanimatorTrackInstance reanimatorTrackInstance = mTrackInstances[i];
                GlobalMembersAttachment.AttachmentDie(ref reanimatorTrackInstance.mAttachmentID);
            }
        }

        public void Update()//3update
        {
            mGetFrameTime = true;
            if (mFrameCount == 0)
            {
                return;
            }
            if (mDead)
            {
                return;
            }
            mLastFrameTime = mAnimTime;
            mAnimTime += ReanimatorXnaHelpers.SECONDS_PER_UPDATE * mAnimRate / mFrameCount;
            if (mAnimRate > 0f)
            {
                if (mLoopType != ReanimLoopType.Loop)
                {
                    if (mLoopType != ReanimLoopType.LoopFullLastFrame)
                    {
                        if (mLoopType == ReanimLoopType.PlayOnce || mLoopType == ReanimLoopType.PlayOnceFullLastFrame)
                        {
                            if (mAnimTime >= 1f)
                            {
                                mAnimTime = 1f;
                                mLoopCount = 1;
                                mDead = true;
                                goto IL_1C4;
                            }
                            goto IL_1C4;
                        }
                        else
                        {
                            if ((mLoopType == ReanimLoopType.PlayOnceAndHold || mLoopType == ReanimLoopType.PlayOnceFullLastFrameAndHold) && mAnimTime >= 1f)
                            {
                                mLoopCount = 1;
                                mAnimTime = 1f;
                                goto IL_1C4;
                            }
                            goto IL_1C4;
                        }
                    }
                }
                while (mAnimTime >= 1f)
                {
                    mLoopCount++;
                    mAnimTime -= 1f;
                }
            }
            else
            {
                if (mLoopType != ReanimLoopType.Loop)
                {
                    if (mLoopType != ReanimLoopType.LoopFullLastFrame)
                    {
                        if (mLoopType == ReanimLoopType.PlayOnce || mLoopType == ReanimLoopType.PlayOnceFullLastFrame)
                        {
                            if (mAnimTime < 0f)
                            {
                                mAnimTime = 0f;
                                mLoopCount = 1;
                                mDead = true;
                                goto IL_1C4;
                            }
                            goto IL_1C4;
                        }
                        else
                        {
                            if ((mLoopType == ReanimLoopType.PlayOnceAndHold || mLoopType == ReanimLoopType.PlayOnceFullLastFrameAndHold) && mAnimTime < 0f)
                            {
                                mLoopCount = 1;
                                mAnimTime = 0f;
                                goto IL_1C4;
                            }
                            goto IL_1C4;
                        }
                    }
                }
                while (mAnimTime < 0f)
                {
                    mLoopCount++;
                    mAnimTime += 1f;
                }
            }
            IL_1C4:
            int trackCount = mDefinition.mTrackCount;
            for (int i = 0; i < trackCount; i++)
            {
                ReanimatorTrackInstance reanimatorTrackInstance = mTrackInstances[i];
                if (reanimatorTrackInstance.mRenderGroup != ReanimatorXnaHelpers.RENDER_GROUP_HIDDEN)
                {
                    if (reanimatorTrackInstance.mBlendCounter > 0)
                    {
                        ReanimatorTrackInstance reanimatorTrackInstance2 = reanimatorTrackInstance;
                        reanimatorTrackInstance2.mBlendCounter -= 1;
                    }
                    if (reanimatorTrackInstance.mShakeOverride != 0f)
                    {
                        reanimatorTrackInstance.mShakeX = TodCommon.RandRangeFloat(-reanimatorTrackInstance.mShakeOverride, reanimatorTrackInstance.mShakeOverride);
                        reanimatorTrackInstance.mShakeY = TodCommon.RandRangeFloat(-reanimatorTrackInstance.mShakeOverride, reanimatorTrackInstance.mShakeOverride);
                    }
                    ReanimatorTrack reanimatorTrack = mDefinition.mTracks[i];
                    if (reanimatorTrack.IsAttacher)
                    {
                        UpdateAttacherTrack(i);
                    }
                    if (reanimatorTrackInstance.mAttachmentID != null)
                    {
                        GetAttachmentOverlayMatrix(i, out aOverlayMatrix);
                        GlobalMembersAttachment.AttachmentUpdateAndSetMatrix(ref reanimatorTrackInstance.mAttachmentID, ref aOverlayMatrix);
                    }
                }
            }
        }

        public void Draw(Graphics g, bool isHardwareClipRequired = true)
        {
            mGetFrameTime = true;
            DrawRenderGroup(g, ReanimatorXnaHelpers.RENDER_GROUP_NORMAL, isHardwareClipRequired);
        }

        public void DrawRenderGroup(Graphics g, int theRenderGroup, bool isHardwareClipRequired = true)
        {
            if (mDead)
            {
                return;
            }
            for (int i = 0; i < mDefinition.mTrackCount; i++)
            {
                ReanimatorTrackInstance reanimatorTrackInstance = mTrackInstances[i];
                if (reanimatorTrackInstance.mRenderGroup == theRenderGroup)
                {
                    bool flag = DrawTrack(g, i, theRenderGroup, isHardwareClipRequired);
                    if (reanimatorTrackInstance.mAttachmentID != null)
                    {
                        Attachment attachmentID = reanimatorTrackInstance.mAttachmentID;
                        for (int j = 0; j < attachmentID.mNumEffects; j++)
                        {
                            AttachEffect attachEffect = attachmentID.mEffectArray[j];
                            if (attachEffect.mEffectType == EffectType.Reanim)
                            {
                                Reanimation reanimation = (Reanimation)attachEffect.mEffectID;
                                reanimation.mColorOverride = mColorOverride;
                                reanimation.mExtraAdditiveColor = mExtraAdditiveColor;
                                reanimation.mExtraOverlayColor = mExtraOverlayColor;
                            }
                        }
                        GlobalMembersAttachment.AttachmentDraw(reanimatorTrackInstance.mAttachmentID, g, !flag, false);
                    }
                }
            }
        }

        private bool DrawTrack(Graphics g, int theTrackIndex, int theRenderGroup, bool isHardwareClipRequired)
        {
            ReanimatorTransform reanimatorTransform;
            GetCurrentTransform(theTrackIndex, out reanimatorTransform, true);
            if (reanimatorTransform == null)
            {
                return false;
            }
            if (reanimatorTransform.mFrame < 0f)
            {
                reanimatorTransform.PrepareForReuse();
                return false;
            }
            int i = (int)(reanimatorTransform.mFrame + 0.5f);
            ReanimatorTrackInstance reanimatorTrackInstance = mTrackInstances[theTrackIndex];
            SexyColor trackColor = reanimatorTrackInstance.mTrackColor;
            if (!reanimatorTrackInstance.mIgnoreColorOverride)
            {
                trackColor.Color.R = (byte)(mColorOverride.mRed * trackColor.mRed / 255f);
                trackColor.Color.G = (byte)(mColorOverride.mGreen * trackColor.mGreen / 255f);
                trackColor.Color.B = (byte)(mColorOverride.mBlue * trackColor.mBlue / 255f);
                trackColor.Color.A = (byte)(mColorOverride.mAlpha * trackColor.mAlpha / 255f);
            }
            if (g.mColorizeImages)
            {
                trackColor.Color.R = (byte)(g.mColor.R * trackColor.mRed / 255f);
                trackColor.Color.G = (byte)(g.mColor.G * trackColor.mGreen / 255f);
                trackColor.Color.B = (byte)(g.mColor.B * trackColor.mBlue / 255f);
                trackColor.Color.A = (byte)(g.mColor.A * trackColor.mAlpha / 255f);
            }
            int num = TodCommon.ClampInt((int)(reanimatorTransform.mAlpha * trackColor.mAlpha + 0.5f), 0, 255);
            if (num <= 0)
            {
                reanimatorTransform.PrepareForReuse();
                return false;
            }
            trackColor.mAlpha = num;
            SexyColor theColor;
            if (mEnableExtraAdditiveDraw)
            {
                theColor = new SexyColor(mExtraAdditiveColor.mRed, mExtraAdditiveColor.mGreen, mExtraAdditiveColor.mBlue, TodCommon.ColorComponentMultiply(mExtraAdditiveColor.mAlpha, num));
            }
            else
            {
                theColor = default(SexyColor);
            }
            Image image = reanimatorTransform.mImage;
            ReanimAtlasImage reanimAtlasImage = null;
            if (mDefinition.mReanimAtlas != null && image != null)
            {
                reanimAtlasImage = mDefinition.mReanimAtlas.GetEncodedReanimAtlas(image);
                if (reanimAtlasImage != null)
                {
                    image = reanimAtlasImage.mOriginalImage;
                }
                if (reanimatorTrackInstance.mImageOverride != null)
                {
                    reanimAtlasImage = null;
                }
            }
            bool flag = false;
            float num2 = 0f;
            float num3 = 0f;
            if (image != null)
            {
                float num4 = image.GetCelWidth();
                float num5 = image.GetCelHeight();
                num2 = num4 * 0.5f;
                num3 = num5 * 0.5f;
            }
            else if (reanimatorTransform.mFont != null && !string.IsNullOrEmpty(reanimatorTransform.mText))
            {
                float num6 = reanimatorTransform.mFont.StringWidth(reanimatorTransform.mText);
                num2 = -num6 * 0.5f;
                num3 = reanimatorTransform.mFont.mAscent;
            }
            else
            {
                if (!(mDefinition.mTracks[theTrackIndex].mName == "fullscreen"))
                {
                    reanimatorTransform.PrepareForReuse();
                    return false;
                }
                flag = true;
            }
            TRect trect = g.mClipRect;
            Reanimation.didClipIgnore = false;
            if (reanimatorTrackInstance.mIgnoreClipRect)
            {
                trect = new TRect(0, 0, 800, 600);
                Reanimation.didClipIgnore = true;
            }
            float num7 = reanimatorTransform.mSkewXCos * reanimatorTransform.mScaleX;
            float num8 = -reanimatorTransform.mSkewXSin * reanimatorTransform.mScaleX;
            float num9 = reanimatorTransform.mSkewYSin * reanimatorTransform.mScaleY;
            float num10 = reanimatorTransform.mSkewYCos * reanimatorTransform.mScaleY;
            float num11 = num7 * num2 + num9 * num3 + reanimatorTransform.mTransX;
            float num12 = num8 * num2 + num10 * num3 + reanimatorTransform.mTransY;
            Reanimation.tempMatrix = new Matrix
            {
                M11 = num7 * mOverlayMatrix.mMatrix.M11 + num8 * mOverlayMatrix.mMatrix.M21,
                M12 = num7 * mOverlayMatrix.mMatrix.M12 + num8 * mOverlayMatrix.mMatrix.M22,
                M13 = 0f,
                M14 = 0f,
                M21 = num9 * mOverlayMatrix.mMatrix.M11 + num10 * mOverlayMatrix.mMatrix.M21,
                M22 = num9 * mOverlayMatrix.mMatrix.M12 + num10 * mOverlayMatrix.mMatrix.M22,
                M23 = 0f,
                M24 = 0f,
                M31 = 0f,
                M32 = 0f,
                M33 = 1f,
                M34 = 0f,
                M41 = num11 * mOverlayMatrix.mMatrix.M11 + num12 * mOverlayMatrix.mMatrix.M21 + mOverlayMatrix.mMatrix.M41 + g.mTransX + reanimatorTrackInstance.mShakeX - 0.5f,
                M42 = num11 * mOverlayMatrix.mMatrix.M12 + num12 * mOverlayMatrix.mMatrix.M22 + mOverlayMatrix.mMatrix.M42 + g.mTransY + reanimatorTrackInstance.mShakeY - 0.5f,
                M43 = 0f,
                M44 = 1f
            };
            if (theTrackIndex == 9)
            {
                int num13 = 0;
                num13++;
            }
            if (reanimAtlasImage == null)
            {
                if (image != null)
                {
                    if (reanimatorTrackInstance.mImageOverride != null)
                    {
                        image = reanimatorTrackInstance.mImageOverride;
                    }
                    if (mFilterEffect != FilterEffectType.None)
                    {
                        image = FilterEffect.FilterEffectGetImage(image, mFilterEffect);
                    }
                    while (i >= image.mNumCols)
                    {
                        i -= image.mNumCols;
                    }
                    int num14 = 0;
                    int celWidth = image.GetCelWidth();
                    int celHeight = image.GetCelHeight();
                    TRect theSrcRect = new TRect(celWidth * i, celHeight * num14, celWidth, celHeight);
                    ReanimBltMatrix(g, image, ref Reanimation.tempMatrix, ref trect, trackColor, Graphics.DrawMode.DRAWMODE_NORMAL, theSrcRect, isHardwareClipRequired);
                    if (mEnableExtraAdditiveDraw)
                    {
                        ReanimBltMatrix(g, image, ref Reanimation.tempMatrix, ref trect, theColor, Graphics.DrawMode.DRAWMODE_ADDITIVE, theSrcRect, isHardwareClipRequired);
                    }
                    TodCommon.OffsetForGraphicsTranslation = true;
                }
                else if (reanimatorTransform.mFont != null && !string.IsNullOrEmpty(reanimatorTransform.mText))
                {
                    TodCommon.TodDrawStringMatrix(g, reanimatorTransform.mFont, Reanimation.tempMatrix, reanimatorTransform.mText, trackColor);
                    if (mEnableExtraAdditiveDraw)
                    {
                        Graphics.DrawMode drawMode = g.mDrawMode;
                        g.SetDrawMode(Graphics.DrawMode.DRAWMODE_ADDITIVE);
                        TodCommon.TodDrawStringMatrix(g, reanimatorTransform.mFont, Reanimation.tempMatrix, reanimatorTransform.mText, theColor);
                        g.SetDrawMode(drawMode);
                    }
                }
                else if (flag)
                {
                    Color color = g.GetColor();
                    g.SetColor(trackColor);
                    g.FillRect(-g.mTransX, -g.mTransY, Constants.BOARD_WIDTH, Constants.BOARD_HEIGHT);
                    g.SetColor(color);
                }
            }
            reanimatorTransform.PrepareForReuse();
            return true;
        }


        public void GetCurrentTransform(int theTrackIndex, out ReanimatorTransform aTransformCurrent, bool nullIfInvalidFrame)
        {
            ReanimatorFrameTime theFrameTime;
            GetFrameTime(out theFrameTime);
            GetTransformAtTime(theTrackIndex, out aTransformCurrent, theFrameTime, nullIfInvalidFrame);
            if (aTransformCurrent == null)
            {
                return;
            }
            ReanimatorTrackInstance reanimatorTrackInstance = mTrackInstances[theTrackIndex];
            int num = (int)(aTransformCurrent.mFrame + 0.5f);
            if (num >= 0 && reanimatorTrackInstance.mBlendCounter > 0)
            {
                float theBlendFactor = reanimatorTrackInstance.mBlendCounter / (float)reanimatorTrackInstance.mBlendTime;
                ReanimatorTransform reanimatorTransform;
                ReanimatorXnaHelpers.BlendTransform(out reanimatorTransform, ref aTransformCurrent, ref reanimatorTrackInstance.mBlendTransform, theBlendFactor);
                if (aTransformCurrent != null)
                {
                    aTransformCurrent.PrepareForReuse();
                }
                aTransformCurrent = reanimatorTransform;
            }
        }

        public void GetTransformAtTime(int theTrackIndex, out ReanimatorTransform aTransform, ReanimatorFrameTime theFrameTime, bool nullIfInvalidFrame)
        {
            ReanimatorTrack reanimatorTrack = mDefinition.mTracks[theTrackIndex];
            ReanimatorTransform reanimatorTransform = reanimatorTrack.mTransforms[theFrameTime.mAnimFrameBeforeInt];
            ReanimatorTransform reanimatorTransform2 = reanimatorTrack.mTransforms[theFrameTime.mAnimFrameAfterInt];
            if (nullIfInvalidFrame && (reanimatorTransform.mFrame == -1f || (reanimatorTransform.mFrame != -1f && reanimatorTransform2.mFrame == -1f && theFrameTime.mFraction > 0f && mTrackInstances[theTrackIndex].mTruncateDisappearingFrames)))
            {
                aTransform = null;
                return;
            }
            float fraction = theFrameTime.mFraction;
            aTransform = ReanimatorTransform.GetNewReanimatorTransform();
            if (Reanimation.mInterpolate)
            {
                aTransform.mTransX = reanimatorTransform.mTransX + fraction * (reanimatorTransform2.mTransX - reanimatorTransform.mTransX);
                aTransform.mTransY = reanimatorTransform.mTransY + fraction * (reanimatorTransform2.mTransY - reanimatorTransform.mTransY);
                aTransform.mSkewX = reanimatorTransform.mSkewX + fraction * (reanimatorTransform2.mSkewX - reanimatorTransform.mSkewX);
                aTransform.mSkewY = reanimatorTransform.mSkewY + fraction * (reanimatorTransform2.mSkewY - reanimatorTransform.mSkewY);
                aTransform.mScaleX = reanimatorTransform.mScaleX + fraction * (reanimatorTransform2.mScaleX - reanimatorTransform.mScaleX);
                aTransform.mScaleY = reanimatorTransform.mScaleY + fraction * (reanimatorTransform2.mScaleY - reanimatorTransform.mScaleY);
                aTransform.mAlpha = reanimatorTransform.mAlpha + fraction * (reanimatorTransform2.mAlpha - reanimatorTransform.mAlpha);
                aTransform.mSkewXCos = reanimatorTransform.mSkewXCos + fraction * (reanimatorTransform2.mSkewXCos - reanimatorTransform.mSkewXCos);
                aTransform.mSkewXSin = reanimatorTransform.mSkewXSin + fraction * (reanimatorTransform2.mSkewXSin - reanimatorTransform.mSkewXSin);
                aTransform.mSkewYCos = reanimatorTransform.mSkewYCos + fraction * (reanimatorTransform2.mSkewYCos - reanimatorTransform.mSkewYCos);
                aTransform.mSkewYSin = reanimatorTransform.mSkewYSin + fraction * (reanimatorTransform2.mSkewYSin - reanimatorTransform.mSkewYSin);
            }
            else
            {
                aTransform.mTransX = reanimatorTransform.mTransX;
                aTransform.mTransY = reanimatorTransform.mTransY;
                aTransform.mSkewX = reanimatorTransform.mSkewX;
                aTransform.mSkewY = reanimatorTransform.mSkewY;
                aTransform.mScaleX = reanimatorTransform.mScaleX;
                aTransform.mScaleY = reanimatorTransform.mScaleY;
                aTransform.mAlpha = reanimatorTransform.mAlpha;
                aTransform.mSkewXCos = reanimatorTransform.mSkewXCos;
                aTransform.mSkewXSin = reanimatorTransform.mSkewXSin;
                aTransform.mSkewYCos = reanimatorTransform.mSkewYCos;
                aTransform.mSkewYSin = reanimatorTransform.mSkewYSin;
            }
            aTransform.mImage = reanimatorTransform.mImage;
            aTransform.mFont = reanimatorTransform.mFont;
            aTransform.mText = reanimatorTransform.mText;
            if (reanimatorTransform.mFrame != -1f && reanimatorTransform2.mFrame == -1f && theFrameTime.mFraction > 0f && mTrackInstances[theTrackIndex].mTruncateDisappearingFrames)
            {
                aTransform.mFrame = -1f;
                return;
            }
            aTransform.mFrame = reanimatorTransform.mFrame;
        }

        public void GetFrameTime(out ReanimatorFrameTime theFrameTime)
        {
            if (!mGetFrameTime)
            {
                theFrameTime = mFrameTime;
                return;
            }
            mGetFrameTime = false;
            theFrameTime = default(ReanimatorFrameTime);
            int num;
            if (mLoopType == ReanimLoopType.PlayOnceFullLastFrame || mLoopType == ReanimLoopType.LoopFullLastFrame || mLoopType == ReanimLoopType.PlayOnceFullLastFrameAndHold)
            {
                num = mFrameCount;
            }
            else
            {
                num = mFrameCount - 1;
            }
            float num2 = mFrameStart + num * mAnimTime;
            float num3 = (int)num2;
            theFrameTime.mFraction = num2 - num3;
            theFrameTime.mAnimFrameBeforeInt = (short)(num3 + 0.5f);
            if (theFrameTime.mAnimFrameBeforeInt >= mFrameStart + mFrameCount - 1)
            {
                theFrameTime.mAnimFrameBeforeInt = (short)(mFrameStart + mFrameCount - 1);
                theFrameTime.mAnimFrameAfterInt = theFrameTime.mAnimFrameBeforeInt;
            }
            else
            {
                theFrameTime.mAnimFrameAfterInt = (short)(theFrameTime.mAnimFrameBeforeInt + 1);
            }
            mFrameTime = theFrameTime;
        }

        public int FindTrackIndex(string theTrackName)
        {
            for (int i = 0; i < mDefinition.mTrackCount; i++)
            {
                string trackName = mDefinition.mTracks[i].mName;
                string text = Reanimation.ToLower(theTrackName);
                if (trackName == text)
                {
                    return i;
                }
            }
            return 0;
        }

        public void AttachToAnotherReanimation(ref Reanimation theAttachReanim, string theTrackName)
        {
            if (theAttachReanim.mDefinition.mTrackCount == 0)
            {
                return;
            }
            if (theAttachReanim.mFrameBasePose == -1)
            {
                theAttachReanim.mFrameBasePose = theAttachReanim.mFrameStart;
            }
            int num = theAttachReanim.FindTrackIndex(theTrackName);
            ReanimatorTrackInstance reanimatorTrackInstance = theAttachReanim.mTrackInstances[num];
            GlobalMembersAttachment.AttachReanim(ref reanimatorTrackInstance.mAttachmentID, this, 0f, 0f);
        }

        public void GetAttachmentOverlayMatrix(int theTrackIndex, out SexyTransform2D theOverlayMatrix)
        {
            ReanimatorTransform reanimatorTransform;
            GetCurrentTransform(theTrackIndex, out reanimatorTransform, false);
            float num = reanimatorTransform.mSkewXCos * reanimatorTransform.mScaleX;
            float num2 = -reanimatorTransform.mSkewXSin * reanimatorTransform.mScaleX;
            float num3 = reanimatorTransform.mSkewYSin * reanimatorTransform.mScaleY;
            float num4 = reanimatorTransform.mSkewYCos * reanimatorTransform.mScaleY;
            float transTransX = reanimatorTransform.mTransX;
            float transTransY = reanimatorTransform.mTransY;
            reanimatorTransform.PrepareForReuse();
            GetTrackBasePoseMatrix(theTrackIndex, out basePose);
            Matrix.Invert(ref basePose.mMatrix, out aBasePoseMatrix);
            theOverlayMatrix = Reanimation.identity;
            tempOverlayMatrix = new Matrix
            {
                M11 = aBasePoseMatrix.M11 * num + aBasePoseMatrix.M12 * num3,
                M12 = aBasePoseMatrix.M11 * num2 + aBasePoseMatrix.M12 * num4,
                M13 = 0f,
                M14 = 0f,
                M21 = aBasePoseMatrix.M21 * num + aBasePoseMatrix.M22 * num3,
                M22 = aBasePoseMatrix.M21 * num2 + aBasePoseMatrix.M22 * num4,
                M23 = 0f,
                M24 = 0f,
                M31 = 0f,
                M32 = 0f,
                M33 = 1f,
                M34 = 0f,
                M41 = aBasePoseMatrix.M41 * num + aBasePoseMatrix.M42 * num3 + transTransX,
                M42 = aBasePoseMatrix.M41 * num2 + aBasePoseMatrix.M42 * num4 + transTransY,
                M43 = 0f,
                M44 = 1f
            };
            theOverlayMatrix.mMatrix = new Matrix
            {
                M11 = tempOverlayMatrix.M11 * mOverlayMatrix.mMatrix.M11 + tempOverlayMatrix.M12 * mOverlayMatrix.mMatrix.M21,
                M12 = tempOverlayMatrix.M11 * mOverlayMatrix.mMatrix.M12 + tempOverlayMatrix.M12 * mOverlayMatrix.mMatrix.M22,
                M13 = 0f,
                M14 = 0f,
                M21 = tempOverlayMatrix.M21 * mOverlayMatrix.mMatrix.M11 + tempOverlayMatrix.M22 * mOverlayMatrix.mMatrix.M21,
                M22 = tempOverlayMatrix.M21 * mOverlayMatrix.mMatrix.M12 + tempOverlayMatrix.M22 * mOverlayMatrix.mMatrix.M22,
                M23 = 0f,
                M24 = 0f,
                M31 = 0f,
                M32 = 0f,
                M33 = 1f,
                M34 = 0f,
                M41 = tempOverlayMatrix.M41 * mOverlayMatrix.mMatrix.M11 + tempOverlayMatrix.M42 * mOverlayMatrix.mMatrix.M21 + mOverlayMatrix.mMatrix.M41,
                M42 = tempOverlayMatrix.M41 * mOverlayMatrix.mMatrix.M12 + tempOverlayMatrix.M42 * mOverlayMatrix.mMatrix.M22 + mOverlayMatrix.mMatrix.M42,
                M43 = 0f,
                M44 = 1f
            };
        }

        public void SetFramesForLayer(string theTrackName)
        {
            if (mAnimRate >= 0f)
            {
                mAnimTime = 0f;
            }
            else
            {
                mAnimTime = 0.9999999f;
            }
            mLastFrameTime = -1f;
            GetFramesForLayer(theTrackName, out mFrameStart, out mFrameCount);
        }

        public static void MatrixFromTransform(ReanimatorTransform theTransform, out Matrix theMatrix)
        {
            theMatrix = new Matrix
            {
                M11 = (float)Math.Cos((double)(theTransform.mSkewX * -(double)TodCommon.DEG_TO_RAD)) * theTransform.mScaleX,
                M12 = (float)(-(float)Math.Sin((double)(theTransform.mSkewX * -(double)TodCommon.DEG_TO_RAD))) * theTransform.mScaleX,
                M13 = 0f,
                M14 = 0f,
                M21 = (float)Math.Sin((double)(theTransform.mSkewY * -(double)TodCommon.DEG_TO_RAD)) * theTransform.mScaleY,
                M22 = (float)Math.Cos((double)(theTransform.mSkewY * -(double)TodCommon.DEG_TO_RAD)) * theTransform.mScaleY,
                M23 = 0f,
                M24 = 0f,
                M31 = 0f,
                M32 = 0f,
                M33 = 1f,
                M34 = 0f,
                M41 = theTransform.mTransX,
                M42 = theTransform.mTransY,
                M43 = 0f,
                M44 = 1f
            };
        }

        public bool TrackExists(string theTrackName)
        {
            string text = Reanimation.ToLower(theTrackName);
            for (int i = 0; i < mDefinition.mTrackCount; i++)
            {
                string text2 = Reanimation.ToLower(mDefinition.mTracks[i].mName);
                if (text == text2)
                {
                    return true;
                }
            }
            return false;
        }

        public void StartBlend(byte theBlendTime)
        {
            mGetFrameTime = true;
            for (int i = 0; i < mDefinition.mTrackCount; i++)
            {
                ReanimatorTransform reanimatorTransform;
                GetCurrentTransform(i, out reanimatorTransform, true);
                if (reanimatorTransform != null)
                {
                    int num = TodCommon.FloatRoundToInt(reanimatorTransform.mFrame);
                    if (num < 0)
                    {
                        reanimatorTransform.PrepareForReuse();
                    }
                    else
                    {
                        ReanimatorTrackInstance reanimatorTrackInstance = mTrackInstances[i];
                        if (reanimatorTrackInstance.mBlendTransform != null)
                        {
                            reanimatorTrackInstance.mBlendTransform.PrepareForReuse();
                        }
                        reanimatorTrackInstance.mBlendTransform = reanimatorTransform;
                        reanimatorTrackInstance.mBlendCounter = (byte)(theBlendTime / 3f);
                        reanimatorTrackInstance.mBlendTime = (byte)(theBlendTime / 3f);
                        reanimatorTrackInstance.mBlendTransform.mFont = null;
                        reanimatorTrackInstance.mBlendTransform.mText = string.Empty;
                        reanimatorTrackInstance.mBlendTransform.mImage = null;
                    }
                }
            }
        }

        public void SetShakeOverride(string theTrackName, float theShakeAmount)
        {
            int num = FindTrackIndex(theTrackName);
            ReanimatorTrackInstance reanimatorTrackInstance = mTrackInstances[num];
            reanimatorTrackInstance.mShakeOverride = theShakeAmount;
        }

        public void SetPosition(float theX, float theY)
        {
            mOverlayMatrix.mMatrix.Translation = new Vector3(theX, theY, 0f);
        }

        public void OverrideScale(float theScaleX, float theScaleY)
        {
            mOverlayMatrix.mMatrix.M11 = theScaleX;
            mOverlayMatrix.mMatrix.M22 = theScaleY;
        }

        public int GetTrackIndex(string theTrackName)
        {
            return FindTrackIndex(theTrackName);
        }

        public float GetTrackVelocity(string theTrackName)
        {
            return GetTrackVelocity(GetTrackIndex(theTrackName));
        }

        public float GetTrackVelocity(int aTrackIndex)
        {
            ReanimatorFrameTime reanimatorFrameTime;
            GetFrameTime(out reanimatorFrameTime);
            ReanimatorTrack reanimatorTrack = mDefinition.mTracks[aTrackIndex];
            ReanimatorTransform reanimatorTransform = reanimatorTrack.mTransforms[reanimatorFrameTime.mAnimFrameBeforeInt];
            ReanimatorTransform reanimatorTransform2 = reanimatorTrack.mTransforms[reanimatorFrameTime.mAnimFrameAfterInt];
            return (reanimatorTransform2.mTransX - reanimatorTransform.mTransX) * ReanimatorXnaHelpers.SECONDS_PER_UPDATE * mAnimRate;
        }

        public void SetImageOverride(string theTrackName, Image theImage)
        {
            int num = FindTrackIndex(theTrackName);
            ReanimatorTrackInstance reanimatorTrackInstance = mTrackInstances[num];
            reanimatorTrackInstance.mImageOverride = theImage;
        }

        public Image GetImageOverride(string theTrackName)
        {
            int num = FindTrackIndex(theTrackName);
            ReanimatorTrackInstance reanimatorTrackInstance = mTrackInstances[num];
            return reanimatorTrackInstance.mImageOverride;
        }

        public void ShowOnlyTrack(string theTrackName)
        {
            string text = theTrackName.ToLower();
            for (int i = 0; i < mDefinition.mTrackCount; i++)
            {
                ReanimatorTrack reanimatorTrack = mDefinition.mTracks[i];
                ReanimatorTrackInstance reanimatorTrackInstance = mTrackInstances[i];
                string text2 = reanimatorTrack.mName.ToLower();
                if (text == text2)
                {
                    reanimatorTrackInstance.mRenderGroup = ReanimatorXnaHelpers.RENDER_GROUP_NORMAL;
                }
                else
                {
                    reanimatorTrackInstance.mRenderGroup = ReanimatorXnaHelpers.RENDER_GROUP_HIDDEN;
                }
            }
        }

        public void GetTrackMatrix(int theTrackIndex, ref SexyTransform2D theMatrix)
        {
            ReanimatorTrackInstance reanimatorTrackInstance = mTrackInstances[theTrackIndex];
            mGetFrameTime = true;
            ReanimatorTransform reanimatorTransform;
            GetCurrentTransform(theTrackIndex, out reanimatorTransform, false);
            int num = TodCommon.FloatRoundToInt(reanimatorTransform.mFrame);
            Image image = reanimatorTransform.mImage;
            if (mDefinition.mReanimAtlas != null && image != null)
            {
                ReanimAtlasImage encodedReanimAtlas = mDefinition.mReanimAtlas.GetEncodedReanimAtlas(image);
                if (encodedReanimAtlas != null)
                {
                    image = encodedReanimAtlas.mOriginalImage;
                }
            }
            theMatrix.LoadIdentity();
            Reanimation.tempMatrix = Matrix.Identity;
            if (image != null && num >= 0)
            {
                int celWidth = image.GetCelWidth();
                int celHeight = image.GetCelHeight();
                Matrix.CreateTranslation(celWidth * 0.5f, celHeight * 0.5f, 0f, out Reanimation.tempMatrix);
            }
            else if (reanimatorTransform.mFont != null && !string.IsNullOrEmpty(reanimatorTransform.mText))
            {
                Matrix.CreateTranslation(0f, reanimatorTransform.mFont.mAscent, 0f, out Reanimation.tempMatrix);
            }
            SexyTransform2D sexyTransform2D = default(SexyTransform2D);
            Reanimation.MatrixFromTransform(reanimatorTransform, out sexyTransform2D.mMatrix);
            TodCommon.SexyMatrix3Multiply(ref Reanimation.tempMatrix, sexyTransform2D.mMatrix, Reanimation.tempMatrix);
            TodCommon.SexyMatrix3Multiply(ref Reanimation.tempMatrix, mOverlayMatrix.mMatrix, Reanimation.tempMatrix);
            TodCommon.SexyMatrix3Translation(ref Reanimation.tempMatrix, reanimatorTrackInstance.mShakeX - 0.5f, reanimatorTrackInstance.mShakeY - 0.5f);
            theMatrix.mMatrix = Reanimation.tempMatrix;
            reanimatorTransform.PrepareForReuse();
        }

        public void GetTrackTranslationMatrix(int theTrackIndex, ref SexyTransform2D theMatrix)
        {
            ReanimatorTrackInstance reanimatorTrackInstance = mTrackInstances[theTrackIndex];
            mGetFrameTime = true;
            ReanimatorTransform reanimatorTransform;
            GetCurrentTransform(theTrackIndex, out reanimatorTransform, false);
            int num = TodCommon.FloatRoundToInt(reanimatorTransform.mFrame);
            Image image = reanimatorTransform.mImage;
            if (mDefinition.mReanimAtlas != null && image != null)
            {
                ReanimAtlasImage encodedReanimAtlas = mDefinition.mReanimAtlas.GetEncodedReanimAtlas(image);
                if (encodedReanimAtlas != null)
                {
                    image = encodedReanimAtlas.mOriginalImage;
                }
            }
            theMatrix.LoadIdentity();
            Reanimation.tempMatrix = Matrix.Identity;
            if (image != null && num >= 0)
            {
                int celWidth = image.GetCelWidth();
                int celHeight = image.GetCelHeight();
                Matrix.CreateTranslation(celWidth * 0.5f, celHeight * 0.5f, 0f, out Reanimation.tempMatrix);
            }
            else if (reanimatorTransform.mFont != null && !string.IsNullOrEmpty(reanimatorTransform.mText))
            {
                Matrix.CreateTranslation(0f, reanimatorTransform.mFont.mAscent, 0f, out Reanimation.tempMatrix);
            }
            SexyTransform2D sexyTransform2D = default(SexyTransform2D);
            Reanimation.MatrixFromTransform(reanimatorTransform, out sexyTransform2D.mMatrix);
            Reanimation.tempMatrix.M41 = sexyTransform2D.mMatrix.M41 + mOverlayMatrix.mMatrix.M41 + reanimatorTrackInstance.mShakeX - 0.5f;
            Reanimation.tempMatrix.M42 = sexyTransform2D.mMatrix.M42 + mOverlayMatrix.mMatrix.M42 + reanimatorTrackInstance.mShakeY - 0.5f;
            theMatrix.mMatrix = Reanimation.tempMatrix;
            reanimatorTransform.PrepareForReuse();
        }

        public void AssignRenderGroupToTrack(string theTrackName, int theRenderGroup)
        {
            string text = Reanimation.ToLower(theTrackName);
            for (int i = 0; i < mDefinition.mTrackCount; i++)
            {
                ReanimatorTrack reanimatorTrack = mDefinition.mTracks[i];
                string text2 = Reanimation.ToLower(reanimatorTrack.mName);
                if (!(text != text2))
                {
                    mTrackInstances[i].mRenderGroup = theRenderGroup;
                    return;
                }
            }
        }

        public void AssignRenderGroupToPrefix(string theTrackName, int theRenderGroup)
        {
            int length = theTrackName.Length;
            string s = Reanimation.ToLower(theTrackName);
            for (int i = 0; i < mDefinition.mTrackCount; i++)
            {
                ReanimatorTrack reanimatorTrack = mDefinition.mTracks[i];
                if (reanimatorTrack.mName.Length >= length)
                {
                    string contains = Reanimation.ToLower(reanimatorTrack.mName);
                    if (s.StartsWithCharLimit(contains, length))
                    {
                        mTrackInstances[i].mRenderGroup = theRenderGroup;
                    }
                }
            }
        }

        public void PropogateColorToAttachments()
        {
            for (int i = 0; i < mDefinition.mTrackCount; i++)
            {
                ReanimatorTrackInstance reanimatorTrackInstance = mTrackInstances[i];
                GlobalMembersAttachment.AttachmentPropogateColor(reanimatorTrackInstance.mAttachmentID, mColorOverride, mEnableExtraAdditiveDraw, mExtraAdditiveColor, mEnableExtraOverlayDraw, mExtraOverlayColor);
            }
        }

        public bool ShouldTriggerTimedEvent(float theEventTime)
        {
            if (mFrameCount == 0)
            {
                return false;
            }
            if (mLastFrameTime < 0f)
            {
                return false;
            }
            if (mAnimRate <= 0f)
            {
                return false;
            }
            if (mAnimTime >= mLastFrameTime)
            {
                if (theEventTime >= mLastFrameTime && theEventTime < mAnimTime)
                {
                    return true;
                }
            }
            else if (theEventTime >= mLastFrameTime || theEventTime < mAnimTime)
            {
                return true;
            }
            return false;
        }

        public void TodTriangleGroupDraw(Graphics g, ref TodTriangleGroup theTriangleGroup)
        {
        }

        public Image GetCurrentTrackImage(string theTrackName)
        {
            int theTrackIndex = FindTrackIndex(theTrackName);
            ReanimatorTransform reanimatorTransform;
            GetCurrentTransform(theTrackIndex, out reanimatorTransform, false);
            Image image = reanimatorTransform.mImage;
            if (mDefinition.mReanimAtlas != null && image != null)
            {
                ReanimAtlasImage encodedReanimAtlas = mDefinition.mReanimAtlas.GetEncodedReanimAtlas(image);
                if (encodedReanimAtlas != null)
                {
                    image = encodedReanimAtlas.mOriginalImage;
                }
            }
            reanimatorTransform.PrepareForReuse();
            return image;
        }

        public AttachEffect AttachParticleToTrack(string theTrackName, ref TodParticleSystem theParticleSystem, float thePosX, float thePosY)
        {
            int num = FindTrackIndex(theTrackName);
            ReanimatorTrackInstance reanimatorTrackInstance = mTrackInstances[num];
            SexyTransform2D transform;
            GetTrackBasePoseMatrix(num, out transform);
            Vector2 vector = transform * new Vector2(thePosX, thePosY);
            return GlobalMembersAttachment.AttachParticle(ref reanimatorTrackInstance.mAttachmentID, theParticleSystem, vector.X, vector.Y);
        }

        public void GetTrackBasePoseMatrix(int theTrackIndex, out SexyTransform2D theBasePoseMatrix)
        {
            theBasePoseMatrix = default(SexyTransform2D);
            if (mFrameBasePose == ReanimatorXnaHelpers.NO_BASE_POSE)
            {
                theBasePoseMatrix.LoadIdentity();
                return;
            }
            short num = mFrameBasePose;
            if (num == -1)
            {
                num = mFrameStart;
            }
            ReanimatorTransform reanimatorTransform;
            GetTransformAtTime(theTrackIndex, out reanimatorTransform, new ReanimatorFrameTime
            {
                mFraction = 0f,
                mAnimFrameBeforeInt = num,
                mAnimFrameAfterInt = (short)(num + 1)
            }, false);
            Reanimation.MatrixFromTransform(reanimatorTransform, out theBasePoseMatrix.mMatrix);
            reanimatorTransform.PrepareForReuse();
        }

        public bool IsTrackShowing(string theTrackName)
        {
            ReanimatorFrameTime reanimatorFrameTime;
            GetFrameTime(out reanimatorFrameTime);
            int num = FindTrackIndex(theTrackName);
            ReanimatorTrack reanimatorTrack = mDefinition.mTracks[num];
            ReanimatorTransform reanimatorTransform = reanimatorTrack.mTransforms[reanimatorFrameTime.mAnimFrameAfterInt];
            return reanimatorTransform.mFrame >= 0f;
        }

        public void SetTruncateDisappearingFrames(string theTrackName, bool theTruncateDisappearingFrames)
        {
            if (string.IsNullOrEmpty(theTrackName))
            {
                for (int i = 0; i < mDefinition.mTrackCount; i++)
                {
                    ReanimatorTrackInstance reanimatorTrackInstance = mTrackInstances[i];
                    reanimatorTrackInstance.mTruncateDisappearingFrames = theTruncateDisappearingFrames;
                }
                return;
            }
            int num = FindTrackIndex(theTrackName);
            ReanimatorTrackInstance reanimatorTrackInstance2 = mTrackInstances[num];
            reanimatorTrackInstance2.mTruncateDisappearingFrames = theTruncateDisappearingFrames;
        }

        public void PlayReanim(string theTrackName, ReanimLoopType theLoopType, byte theBlendTime, float theAnimRate)
        {
            if (theBlendTime > 0)
            {
                StartBlend(theBlendTime);
            }
            if (theAnimRate != 0f)
            {
                mAnimRate = theAnimRate;
            }
            mLoopType = theLoopType;
            mLoopCount = 0;
            SetFramesForLayer(theTrackName);
        }

        public void ReanimationDelete()
        {
            if (mTrackInstances != null)
            {
                for (int i = 0; i < mTrackInstances.Length; i++)
                {
                    mTrackInstances[i] = null;
                }
                mTrackInstances = null;
            }
        }

        public ReanimatorTrackInstance GetTrackInstanceByName(string theTrackName)
        {
            int num = FindTrackIndex(theTrackName);
            return mTrackInstances[num];
        }

        public void GetFramesForLayer(string theTrackName, out short theFrameStart, out short theFrameCount)
        {
            if (mDefinition.mTrackCount == 0)
            {
                theFrameStart = 0;
                theFrameCount = 0;
                return;
            }
            int num = FindTrackIndex(theTrackName);
            ReanimatorTrack reanimatorTrack = mDefinition.mTracks[num];
            theFrameStart = 0;
            theFrameCount = 1;
            short num2;
            for (num2 = 0; num2 < reanimatorTrack.mTransformCount; num2 += 1)
            {
                ReanimatorTransform reanimatorTransform = reanimatorTrack.mTransforms[num2];
                if (reanimatorTransform.mFrame >= 0f)
                {
                    theFrameStart = num2;
                    break;
                }
            }
            for (int i = reanimatorTrack.mTransformCount - 1; i >= num2; i--)
            {
                ReanimatorTransform reanimatorTransform2 = reanimatorTrack.mTransforms[i];
                if (reanimatorTransform2.mFrame >= 0f)
                {
                    theFrameCount = (short)(i - theFrameStart + 1);
                    return;
                }
            }
        }

        public void UpdateAttacherTrack(int theTrackIndex)//3update
        {
            ReanimatorTrackInstance reanimatorTrackInstance = mTrackInstances[theTrackIndex];
            ReanimatorTransform reanimatorTransform;
            GetCurrentTransform(theTrackIndex, out reanimatorTransform, false);
            AttacherInfo attacherInfo;
            Reanimation.ParseAttacherTrack(reanimatorTransform, out attacherInfo);
            ReanimationType reanimationType = ReanimationType.None;
            if (attacherInfo.mReanimName.Length != 0)
            {
                string text = string.Format("reanim\\%s.reanim", attacherInfo.mReanimName);
                string text2 = text.ToLower();
                for (int i = 0; i < ReanimatorXnaHelpers.gReanimationParamArraySize; i++)
                {
                    ReanimationParams reanimationParams = ReanimatorXnaHelpers.gReanimationParamArray[i];
                    string text3 = reanimationParams.mReanimFileName.ToLower();
                    if (text2 == text3)
                    {
                        reanimationType = reanimationParams.mReanimationType;
                        break;
                    }
                }
            }
            if (reanimationType == ReanimationType.None)
            {
                GlobalMembersAttachment.AttachmentDie(ref reanimatorTrackInstance.mAttachmentID);
                return;
            }
            Reanimation reanimation = GlobalMembersAttachment.FindReanimAttachment(reanimatorTrackInstance.mAttachmentID);
            if (reanimation == null || reanimation.mReanimationType != reanimationType)
            {
                GlobalMembersAttachment.AttachmentDie(ref reanimatorTrackInstance.mAttachmentID);
                reanimation = EffectSystem.gEffectSystem.mReanimationHolder.AllocReanimation(0f, 0f, 0, reanimationType);
                reanimation.mLoopType = attacherInfo.mLoopType;
                reanimation.mAnimRate = attacherInfo.mAnimRate;
                GlobalMembersAttachment.AttachReanim(ref reanimatorTrackInstance.mAttachmentID, reanimation, 0f, 0f);
                mFrameBasePose = ReanimatorXnaHelpers.NO_BASE_POSE;
            }
            if (attacherInfo.mTrackName.Length != 0)
            {
                short num;
                short num2;
                reanimation.GetFramesForLayer(attacherInfo.mTrackName, out num, out num2);
                if (reanimation.mFrameStart != num || reanimation.mFrameCount != num2)
                {
                    reanimation.StartBlend(20);
                    reanimation.SetFramesForLayer(attacherInfo.mTrackName);
                }
                if (attacherInfo.mAnimRate == 12f && attacherInfo.mTrackName == "anim_walk" && reanimation.TrackExists("_ground"))
                {
                    AttacherSynchWalkSpeed(theTrackIndex, ref reanimation, attacherInfo);
                }
                else
                {
                    reanimation.mAnimRate = attacherInfo.mAnimRate;
                }
                reanimation.mLoopType = attacherInfo.mLoopType;
            }
            SexyColor theColor = TodCommon.ColorsMultiply(mColorOverride, reanimatorTrackInstance.mTrackColor);
            theColor.mAlpha = TodCommon.ClampInt(TodCommon.FloatRoundToInt(reanimatorTransform.mAlpha * theColor.mAlpha), 0, 255);
            GlobalMembersAttachment.AttachmentPropogateColor(reanimatorTrackInstance.mAttachmentID, theColor, mEnableExtraAdditiveDraw, mExtraAdditiveColor, mEnableExtraOverlayDraw, mExtraOverlayColor);
        }

        public static void ParseAttacherTrack(ReanimatorTransform theTransform, out AttacherInfo theAttacherInfo)
        {
            theAttacherInfo = new AttacherInfo();
            theAttacherInfo.mReanimName = "";
            theAttacherInfo.mTrackName = "";
            theAttacherInfo.mAnimRate = 12f;
            theAttacherInfo.mLoopType = ReanimLoopType.Loop;
            if (theTransform.mFrame == -1f)
            {
                return;
            }
            int num = theTransform.mText.IndexOf("__");
            if (num == -1)
            {
                return;
            }
            int num2 = theTransform.mText.IndexOf("[", num + 2);
            int num3 = theTransform.mText.IndexOf("__", num + 2);
            if (num2 != -1 && num3 != -1 && num2 < num3)
            {
                return;
            }
            if (num3 != -1)
            {
                theAttacherInfo.mReanimName = theTransform.mText.Substring(num + 2, num3 - num - 2);
                if (num2 != -1)
                {
                    theAttacherInfo.mTrackName = theTransform.mText.Substring(num3 + 2, num2 - num3 - 2);
                }
                else
                {
                    theAttacherInfo.mTrackName = theTransform.mText.Substring(num3 + 2);
                }
            }
            else if (num2 != -1)
            {
                theAttacherInfo.mReanimName = theTransform.mText.Substring(num + 2, num2 - num - 2);
            }
            else
            {
                theAttacherInfo.mReanimName = theTransform.mText.Substring(num + 2);
            }
            while (num2 != -1)
            {
                int num4 = theTransform.mText.IndexOf("]", num2 + 1);
                if (num4 == -1)
                {
                    return;
                }
                string text = theTransform.mText.Substring(num2 + 1, num4 - num2 - 1);
                float num5;
                if (float.TryParse(text, out num5))
                {
                    theAttacherInfo.mAnimRate = num5;
                }
                else if (text == "hold")
                {
                    theAttacherInfo.mLoopType = ReanimLoopType.PlayOnceAndHold;
                }
                else if (text == "once")
                {
                    theAttacherInfo.mLoopType = ReanimLoopType.PlayOnce;
                }
                num2 = theTransform.mText.IndexOf("[", num4 + 1);
            }
        }

        public void AttacherSynchWalkSpeed(int theTrackIndex, ref Reanimation theAttachReanim, AttacherInfo theAttacherInfo)
        {
            ReanimatorTrack reanimatorTrack = mDefinition.mTracks[theTrackIndex];
            ReanimatorFrameTime reanimatorFrameTime;
            GetFrameTime(out reanimatorFrameTime);
            int num = reanimatorFrameTime.mAnimFrameBeforeInt;
            while (num > mFrameStart && !(reanimatorTrack.mTransforms[num - 1].mText != reanimatorTrack.mTransforms[num].mText))
            {
                num--;
            }
            int num2 = reanimatorFrameTime.mAnimFrameBeforeInt;
            while (num2 < mFrameStart + mFrameCount - 1 && !(reanimatorTrack.mTransforms[num2 + 1].mText != reanimatorTrack.mTransforms[num2].mText))
            {
                num2++;
            }
            int num3 = num2 - num;
            ReanimatorTransform reanimatorTransform = reanimatorTrack.mTransforms[num];
            ReanimatorTransform reanimatorTransform2 = reanimatorTrack.mTransforms[num + num3 - 1];
            if (TodCommon.FloatApproxEqual(mAnimRate, 0f))
            {
                theAttachReanim.mAnimRate = 0f;
                return;
            }
            float num4 = -(reanimatorTransform2.mTransX - reanimatorTransform.mTransX);
            float num5 = num3 / mAnimRate;
            if (TodCommon.FloatApproxEqual(num5, 0f))
            {
                theAttachReanim.mAnimRate = 0f;
                return;
            }
            int num6 = theAttachReanim.FindTrackIndex("_ground");
            ReanimatorTrack reanimatorTrack2 = theAttachReanim.mDefinition.mTracks[num6];
            ReanimatorTransform reanimatorTransform3 = reanimatorTrack2.mTransforms[theAttachReanim.mFrameStart];
            ReanimatorTransform reanimatorTransform4 = reanimatorTrack2.mTransforms[theAttachReanim.mFrameStart + theAttachReanim.mFrameCount - 1];
            float num7 = reanimatorTransform4.mTransX - reanimatorTransform3.mTransX;
            if (num7 < ReanimatorXnaHelpers.EPSILON || num4 < ReanimatorXnaHelpers.EPSILON)
            {
                theAttachReanim.mAnimRate = 0f;
                return;
            }
            float num8 = num4 / num7;
            ReanimatorTransform reanimatorTransform5;
            theAttachReanim.GetCurrentTransform(num6, out reanimatorTransform5, false);
            float num9 = reanimatorTransform5.mTransX - reanimatorTransform3.mTransX;
            float num10 = num7 * theAttachReanim.mAnimTime;
            ReanimatorTrackInstance reanimatorTrackInstance = mTrackInstances[theTrackIndex];
            AttachEffect attachEffect = GlobalMembersAttachment.FindFirstAttachment(reanimatorTrackInstance.mAttachmentID);
            if (attachEffect != null)
            {
                attachEffect.mOffset.mMatrix.M13 = num10 - num9;
            }
            theAttachReanim.mAnimRate = num8 * theAttachReanim.mFrameCount / num5;
        }

        public bool IsAnimPlaying(string theTrackName)
        {
            short num;
            short num2;
            GetFramesForLayer(theTrackName, out num, out num2);
            return mFrameStart == num && mFrameCount == num2;
        }

        public void SetBasePoseFromAnim(string theTrackName)
        {
            short num;
            short num2;
            GetFramesForLayer(theTrackName, out num, out num2);
            mFrameBasePose = num;
        }

        public void ReanimBltMatrix(Graphics g, Image theImage, ref Matrix theTransform, ref TRect theClipRect, SexyColor theColor, Graphics.DrawMode theDrawMode, TRect theSrcRect, bool isHardwareClipRequired)
        {
            ReanimationParams reanimationParams = ReanimatorXnaHelpers.gReanimationParamArray[(int)mReanimationType];
            if (!GlobalStaticVars.gSexyAppBase.Is3DAccelerated() && TodCommon.TestBit((uint)reanimationParams.mReanimParamFlags, 1) && TodCommon.FloatApproxEqual(theTransform.M12, 0f) && TodCommon.FloatApproxEqual(theTransform.M21, 0f) && theTransform.M11 > 0f && theTransform.M22 > 0f && theColor == SexyColor.White)
            {
                float m = theTransform.M11;
                float m2 = theTransform.M22;
                int theX = TodCommon.FloatRoundToInt(theTransform.M41 - m * theSrcRect.mWidth * 0.5f);
                int theY = TodCommon.FloatRoundToInt(theTransform.M42 - m2 * theSrcRect.mHeight * 0.5f);
                Graphics.DrawMode drawMode = g.GetDrawMode();
                g.SetDrawMode(theDrawMode);
                TRect clipRect = g.mClipRect;
                g.SetClipRect(ref theClipRect);
                if (TodCommon.FloatApproxEqual(m, 1f) && TodCommon.FloatApproxEqual(m2, 1f))
                {
                    g.DrawImage(theImage, theX, theY, theSrcRect);
                }
                else
                {
                    int theWidth = TodCommon.FloatRoundToInt(m * theSrcRect.mWidth);
                    int theHeight = TodCommon.FloatRoundToInt(m2 * theSrcRect.mHeight);
                    TRect theDestRect = new TRect(theX, theY, theWidth, theHeight);
                    g.DrawImage(theImage, theDestRect, theSrcRect);
                }
                g.SetDrawMode(drawMode);
                g.SetClipRect(ref clipRect);
                return;
            }
            bool formerHardwareClip;
            if (/*isHardwareClipRequired*/ false) // 2021-7-19	Solving the malfunction of SPRITES on the BOARD.
            {   // 2023-4-27 serious performance overheads, abolishing it
                formerHardwareClip = g.IsHardWareClipping();
                if (!formerHardwareClip) 
                {
                    //g.HardwareClip(); 
                }   //	Enable HardwareClip
                //theClipRect = theClipRect.Intersection(new TRect(0, 0, 800, 480));
                TodCommon.TodBltMatrix(g, theImage, ref theTransform, theClipRect, theColor, theDrawMode, theSrcRect, this.mClip);
                if (formerHardwareClip == false)
                {
                    //g.EndHardwareClip();
                }   //	Disable HardwareClip*/

            }
            else // No hardware clip
            {
                TodCommon.TodBltMatrix(g, theImage, ref theTransform, theClipRect, theColor, theDrawMode, theSrcRect, this.mClip);
            }
        }

        public Reanimation FindSubReanim(ReanimationType theReanimType)
        {
            if (mReanimationType == theReanimType)
            {
                return this;
            }
            for (int i = 0; i < mDefinition.mTrackCount; i++)
            {
                ReanimatorTrackInstance reanimatorTrackInstance = mTrackInstances[i];
                Reanimation reanimation = GlobalMembersAttachment.FindReanimAttachment(reanimatorTrackInstance.mAttachmentID);
                if (reanimation != null)
                {
                    Reanimation reanimation2 = reanimation.FindSubReanim(theReanimType);
                    if (reanimation2 != null)
                    {
                        return reanimation2;
                    }
                }
            }
            return null;
        }

        public const string Attacher = "attacher__";

        public static bool mInterpolate = true;

        public ReanimationType mReanimationType;

        public float mAnimTime;

        public float mAnimRate;

        public ReanimatorDefinition mDefinition;

        public ReanimLoopType mLoopType;

        public bool mDead;

        public short mFrameStart;

        public short mFrameCount;

        public short mFrameBasePose;

        public SexyTransform2D mOverlayMatrix;

        public SexyColor mColorOverride;

        public ReanimatorTrackInstance[] mTrackInstances = new ReanimatorTrackInstance[100];

        public int mLoopCount;

        public ReanimationHolder mReanimationHolder;

        public bool mIsAttachment;

        public int mRenderOrder;

        public SexyColor mExtraAdditiveColor;

        public bool mEnableExtraAdditiveDraw;

        public SexyColor mExtraOverlayColor;

        public bool mEnableExtraOverlayDraw;

        public float mLastFrameTime;

        public FilterEffectType mFilterEffect;

        public bool mClip;

        public bool mActive;

        private bool mGetFrameTime = true;

        private ReanimatorFrameTime mFrameTime;

        private SexyTransform2D aOverlayMatrix;

        public static string ReanimTrackId_fullscreen = ReanimatorXnaHelpers.ReanimatorTrackNameToId("fullscreen");

        public static string ReanimTrackId__ground = ReanimatorXnaHelpers.ReanimatorTrackNameToId("_ground");

        public static string ReanimTrackId_anim_walk = ReanimatorXnaHelpers.ReanimatorTrackNameToId("anim_walk");

        public static string ReanimTrackId_anim_crawl = ReanimatorXnaHelpers.ReanimatorTrackNameToId("anim_crawl");

        public static string ReanimTrackIdEmpty = ReanimatorXnaHelpers.ReanimatorTrackNameToId("");

        private static Matrix tempMatrix;

        private static Dictionary<string, string> lowercaseCache = new Dictionary<string, string>();

        private static Stack<Reanimation> unusedObjects = new Stack<Reanimation>(1000);

        private static bool didClipIgnore = false;

        private Matrix aBasePoseMatrix = default(Matrix);

        private Matrix tempOverlayMatrix = default(Matrix);

        private SexyTransform2D basePose;

        private static readonly SexyTransform2D identity = new SexyTransform2D(true);
    }
}
