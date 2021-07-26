using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Sexy.TodLib
{
	internal class Reanimation
	{
		public override string ToString()
		{
			return this.mReanimationType.ToString();
		}

		public static string ToLower(string s)
		{
			string text;
			if (!Reanimation.lowercaseCache.TryGetValue(s, ref text))
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
			this.Reset();
			Reanimation.unusedObjects.Push(this);
		}

		protected void Reset()
		{
			for (int i = 0; i < this.mTrackInstances.Length; i++)
			{
				if (this.mTrackInstances[i] != null)
				{
					this.mTrackInstances[i].PrepareForReuse();
				}
				this.mTrackInstances[i] = null;
			}
			this.mClip = false;
			this.mAnimTime = 0f;
			this.mAnimRate = 12f;
			this.mLastFrameTime = -1f;
			this.mDefinition = null;
			this.mLoopType = ReanimLoopType.REANIM_PLAY_ONCE;
			this.mDead = false;
			this.mFrameStart = 0;
			this.mFrameCount = 0;
			this.mFrameBasePose = -1;
			this.mOverlayMatrix.LoadIdentity();
			this.mColorOverride = new SexyColor(Color.White);
			this.mExtraAdditiveColor = new SexyColor(Color.White);
			this.mEnableExtraAdditiveDraw = false;
			this.mExtraOverlayColor = new SexyColor(Color.White);
			this.mEnableExtraOverlayDraw = false;
			this.mLoopCount = 0;
			this.mIsAttachment = false;
			this.mRenderOrder = 0;
			this.mReanimationHolder = null;
			this.mFilterEffect = FilterEffectType.FILTER_EFFECT_NONE;
			this.mReanimationType = ReanimationType.REANIM_NONE;
			this.mActive = false;
			this.mGetFrameTime = true;
		}

		private Reanimation()
		{
			this.Reset();
		}

		public void ReanimationInitialize(float theX, float theY, int theDefinition)
		{
			this.mDefinition = ReanimatorXnaHelpers.gReanimatorDefArray[theDefinition];
			this.mDead = false;
			this.SetPosition(theX, theY);
			this.mAnimRate = this.mDefinition.mFPS;
			this.mLastFrameTime = -1f;
			if (this.mDefinition.mTrackCount != 0)
			{
				this.mFrameCount = this.mDefinition.mTracks[0].mTransformCount;
				for (int i = 0; i < (int)this.mDefinition.mTrackCount; i++)
				{
					ReanimatorTrackInstance newReanimatorTrackInstance = ReanimatorTrackInstance.GetNewReanimatorTrackInstance();
					this.mTrackInstances[i] = newReanimatorTrackInstance;
				}
				return;
			}
			this.mFrameCount = 0;
		}

		public void ReanimationInitializeType(float theX, float theY, ReanimationType theReanimType)
		{
			ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(theReanimType, false);
			this.mReanimationType = theReanimType;
			this.ReanimationInitialize(theX, theY, (int)theReanimType);
		}

		public void ReanimationDie()
		{
			if (this.mDead)
			{
				return;
			}
			this.mActive = false;
			this.mDead = true;
			for (int i = 0; i < (int)this.mDefinition.mTrackCount; i++)
			{
				ReanimatorTrackInstance reanimatorTrackInstance = this.mTrackInstances[i];
				GlobalMembersAttachment.AttachmentDie(ref reanimatorTrackInstance.mAttachmentID);
			}
		}

		public void Update()
		{
			this.mGetFrameTime = true;
			if (this.mFrameCount == 0)
			{
				return;
			}
			if (this.mDead)
			{
				return;
			}
			this.mLastFrameTime = this.mAnimTime;
			this.mAnimTime += ReanimatorXnaHelpers.SECONDS_PER_UPDATE * this.mAnimRate / (float)this.mFrameCount;
			if (this.mAnimRate > 0f)
			{
				if (this.mLoopType != ReanimLoopType.REANIM_LOOP)
				{
					if (this.mLoopType != ReanimLoopType.REANIM_LOOP_FULL_LAST_FRAME)
					{
						if (this.mLoopType == ReanimLoopType.REANIM_PLAY_ONCE || this.mLoopType == ReanimLoopType.REANIM_PLAY_ONCE_FULL_LAST_FRAME)
						{
							if (this.mAnimTime >= 1f)
							{
								this.mAnimTime = 1f;
								this.mLoopCount = 1;
								this.mDead = true;
								goto IL_1C4;
							}
							goto IL_1C4;
						}
						else
						{
							if ((this.mLoopType == ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD || this.mLoopType == ReanimLoopType.REANIM_PLAY_ONCE_FULL_LAST_FRAME_AND_HOLD) && this.mAnimTime >= 1f)
							{
								this.mLoopCount = 1;
								this.mAnimTime = 1f;
								goto IL_1C4;
							}
							goto IL_1C4;
						}
					}
				}
				while (this.mAnimTime >= 1f)
				{
					this.mLoopCount++;
					this.mAnimTime -= 1f;
				}
			}
			else
			{
				if (this.mLoopType != ReanimLoopType.REANIM_LOOP)
				{
					if (this.mLoopType != ReanimLoopType.REANIM_LOOP_FULL_LAST_FRAME)
					{
						if (this.mLoopType == ReanimLoopType.REANIM_PLAY_ONCE || this.mLoopType == ReanimLoopType.REANIM_PLAY_ONCE_FULL_LAST_FRAME)
						{
							if (this.mAnimTime < 0f)
							{
								this.mAnimTime = 0f;
								this.mLoopCount = 1;
								this.mDead = true;
								goto IL_1C4;
							}
							goto IL_1C4;
						}
						else
						{
							if ((this.mLoopType == ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD || this.mLoopType == ReanimLoopType.REANIM_PLAY_ONCE_FULL_LAST_FRAME_AND_HOLD) && this.mAnimTime < 0f)
							{
								this.mLoopCount = 1;
								this.mAnimTime = 0f;
								goto IL_1C4;
							}
							goto IL_1C4;
						}
					}
				}
				while (this.mAnimTime < 0f)
				{
					this.mLoopCount++;
					this.mAnimTime += 1f;
				}
			}
			IL_1C4:
			int mTrackCount = (int)this.mDefinition.mTrackCount;
			for (int i = 0; i < mTrackCount; i++)
			{
				ReanimatorTrackInstance reanimatorTrackInstance = this.mTrackInstances[i];
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
					ReanimatorTrack reanimatorTrack = this.mDefinition.mTracks[i];
					if (reanimatorTrack.IsAttacher)
					{
						this.UpdateAttacherTrack(i);
					}
					if (reanimatorTrackInstance.mAttachmentID != null)
					{
						this.GetAttachmentOverlayMatrix(i, out this.aOverlayMatrix);
						GlobalMembersAttachment.AttachmentUpdateAndSetMatrix(ref reanimatorTrackInstance.mAttachmentID, ref this.aOverlayMatrix);
					}
				}
			}
		}

		public void Draw(Graphics g)
		{
			this.mGetFrameTime = true;
			this.DrawRenderGroup(g, ReanimatorXnaHelpers.RENDER_GROUP_NORMAL);
		}

		public void DrawRenderGroup(Graphics g, int theRenderGroup)
		{
			if (this.mDead)
			{
				return;
			}
			for (int i = 0; i < (int)this.mDefinition.mTrackCount; i++)
			{
				ReanimatorTrackInstance reanimatorTrackInstance = this.mTrackInstances[i];
				if (reanimatorTrackInstance.mRenderGroup == theRenderGroup)
				{
					bool flag = this.DrawTrack(g, i, theRenderGroup);
					if (reanimatorTrackInstance.mAttachmentID != null)
					{
						Attachment mAttachmentID = reanimatorTrackInstance.mAttachmentID;
						for (int j = 0; j < mAttachmentID.mNumEffects; j++)
						{
							AttachEffect attachEffect = mAttachmentID.mEffectArray[j];
							if (attachEffect.mEffectType == EffectType.EFFECT_REANIM)
							{
								Reanimation reanimation = (Reanimation)attachEffect.mEffectID;
								reanimation.mColorOverride = this.mColorOverride;
								reanimation.mExtraAdditiveColor = this.mExtraAdditiveColor;
								reanimation.mExtraOverlayColor = this.mExtraOverlayColor;
							}
						}
						GlobalMembersAttachment.AttachmentDraw(reanimatorTrackInstance.mAttachmentID, g, !flag, false);
					}
				}
			}
		}

		private bool DrawTrack(Graphics g, int theTrackIndex, int theRenderGroup)
		{
			ReanimatorTransform reanimatorTransform;
			this.GetCurrentTransform(theTrackIndex, out reanimatorTransform, true);
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
			ReanimatorTrackInstance reanimatorTrackInstance = this.mTrackInstances[theTrackIndex];
			SexyColor mTrackColor = reanimatorTrackInstance.mTrackColor;
			if (!reanimatorTrackInstance.mIgnoreColorOverride)
			{
				mTrackColor.Color.R = (byte)((float)(this.mColorOverride.mRed * mTrackColor.mRed) / 255f);
				mTrackColor.Color.G = (byte)((float)(this.mColorOverride.mGreen * mTrackColor.mGreen) / 255f);
				mTrackColor.Color.B = (byte)((float)(this.mColorOverride.mBlue * mTrackColor.mBlue) / 255f);
				mTrackColor.Color.A = (byte)((float)(this.mColorOverride.mAlpha * mTrackColor.mAlpha) / 255f);
			}
			if (g.mColorizeImages)
			{
				mTrackColor.Color.R = (byte)((float)((int)g.mColor.R * mTrackColor.mRed) / 255f);
				mTrackColor.Color.G = (byte)((float)((int)g.mColor.G * mTrackColor.mGreen) / 255f);
				mTrackColor.Color.B = (byte)((float)((int)g.mColor.B * mTrackColor.mBlue) / 255f);
				mTrackColor.Color.A = (byte)((float)((int)g.mColor.A * mTrackColor.mAlpha) / 255f);
			}
			int num = TodCommon.ClampInt((int)(reanimatorTransform.mAlpha * (float)mTrackColor.mAlpha + 0.5f), 0, 255);
			if (num <= 0)
			{
				reanimatorTransform.PrepareForReuse();
				return false;
			}
			mTrackColor.mAlpha = num;
			SexyColor theColor;
			if (this.mEnableExtraAdditiveDraw)
			{
				theColor = new SexyColor(this.mExtraAdditiveColor.mRed, this.mExtraAdditiveColor.mGreen, this.mExtraAdditiveColor.mBlue, TodCommon.ColorComponentMultiply(this.mExtraAdditiveColor.mAlpha, num));
			}
			else
			{
				theColor = default(SexyColor);
			}
			Image image = reanimatorTransform.mImage;
			ReanimAtlasImage reanimAtlasImage = null;
			if (this.mDefinition.mReanimAtlas != null && image != null)
			{
				reanimAtlasImage = this.mDefinition.mReanimAtlas.GetEncodedReanimAtlas(image);
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
				float num4 = (float)image.GetCelWidth();
				float num5 = (float)image.GetCelHeight();
				num2 = num4 * 0.5f;
				num3 = num5 * 0.5f;
			}
			else if (reanimatorTransform.mFont != null && !string.IsNullOrEmpty(reanimatorTransform.mText))
			{
				float num6 = (float)reanimatorTransform.mFont.StringWidth(reanimatorTransform.mText);
				num2 = -num6 * 0.5f;
				num3 = (float)reanimatorTransform.mFont.mAscent;
			}
			else
			{
				if (!(this.mDefinition.mTracks[theTrackIndex].mName == "fullscreen"))
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
				M11 = num7 * this.mOverlayMatrix.mMatrix.M11 + num8 * this.mOverlayMatrix.mMatrix.M21,
				M12 = num7 * this.mOverlayMatrix.mMatrix.M12 + num8 * this.mOverlayMatrix.mMatrix.M22,
				M13 = 0f,
				M14 = 0f,
				M21 = num9 * this.mOverlayMatrix.mMatrix.M11 + num10 * this.mOverlayMatrix.mMatrix.M21,
				M22 = num9 * this.mOverlayMatrix.mMatrix.M12 + num10 * this.mOverlayMatrix.mMatrix.M22,
				M23 = 0f,
				M24 = 0f,
				M31 = 0f,
				M32 = 0f,
				M33 = 1f,
				M34 = 0f,
				M41 = num11 * this.mOverlayMatrix.mMatrix.M11 + num12 * this.mOverlayMatrix.mMatrix.M21 + this.mOverlayMatrix.mMatrix.M41 + (float)g.mTransX + reanimatorTrackInstance.mShakeX - 0.5f,
				M42 = num11 * this.mOverlayMatrix.mMatrix.M12 + num12 * this.mOverlayMatrix.mMatrix.M22 + this.mOverlayMatrix.mMatrix.M42 + (float)g.mTransY + reanimatorTrackInstance.mShakeY - 0.5f,
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
					while (i >= image.mNumCols)
					{
						i -= image.mNumCols;
					}
					int num14 = 0;
					int celWidth = image.GetCelWidth();
					int celHeight = image.GetCelHeight();
					TRect theSrcRect = new TRect(celWidth * i, celHeight * num14, celWidth, celHeight);
					this.ReanimBltMatrix(g, image, ref Reanimation.tempMatrix, ref trect, mTrackColor, Graphics.DrawMode.DRAWMODE_NORMAL, theSrcRect);
					if (this.mEnableExtraAdditiveDraw)
					{
						this.ReanimBltMatrix(g, image, ref Reanimation.tempMatrix, ref trect, theColor, Graphics.DrawMode.DRAWMODE_ADDITIVE, theSrcRect);
					}
					TodCommon.OffsetForGraphicsTranslation = true;
				}
				else if (reanimatorTransform.mFont != null && !string.IsNullOrEmpty(reanimatorTransform.mText))
				{
					TodCommon.TodDrawStringMatrix(g, reanimatorTransform.mFont, Reanimation.tempMatrix, reanimatorTransform.mText, mTrackColor);
					if (this.mEnableExtraAdditiveDraw)
					{
						Graphics.DrawMode mDrawMode = g.mDrawMode;
						g.SetDrawMode(Graphics.DrawMode.DRAWMODE_ADDITIVE);
						TodCommon.TodDrawStringMatrix(g, reanimatorTransform.mFont, Reanimation.tempMatrix, reanimatorTransform.mText, theColor);
						g.SetDrawMode(mDrawMode);
					}
				}
				else if (flag)
				{
					Color color = g.GetColor();
					g.SetColor(mTrackColor);
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
			this.GetFrameTime(out theFrameTime);
			this.GetTransformAtTime(theTrackIndex, out aTransformCurrent, theFrameTime, nullIfInvalidFrame);
			if (aTransformCurrent == null)
			{
				return;
			}
			ReanimatorTrackInstance reanimatorTrackInstance = this.mTrackInstances[theTrackIndex];
			int num = (int)(aTransformCurrent.mFrame + 0.5f);
			if (num >= 0 && reanimatorTrackInstance.mBlendCounter > 0)
			{
				float theBlendFactor = (float)reanimatorTrackInstance.mBlendCounter / (float)reanimatorTrackInstance.mBlendTime;
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
			ReanimatorTrack reanimatorTrack = this.mDefinition.mTracks[theTrackIndex];
			ReanimatorTransform reanimatorTransform = reanimatorTrack.mTransforms[(int)theFrameTime.mAnimFrameBeforeInt];
			ReanimatorTransform reanimatorTransform2 = reanimatorTrack.mTransforms[(int)theFrameTime.mAnimFrameAfterInt];
			if (nullIfInvalidFrame && (reanimatorTransform.mFrame == -1f || (reanimatorTransform.mFrame != -1f && reanimatorTransform2.mFrame == -1f && theFrameTime.mFraction > 0f && this.mTrackInstances[theTrackIndex].mTruncateDisappearingFrames)))
			{
				aTransform = null;
				return;
			}
			float mFraction = theFrameTime.mFraction;
			aTransform = ReanimatorTransform.GetNewReanimatorTransform();
			if (Reanimation.mInterpolate)
			{
				aTransform.mTransX = reanimatorTransform.mTransX + mFraction * (reanimatorTransform2.mTransX - reanimatorTransform.mTransX);
				aTransform.mTransY = reanimatorTransform.mTransY + mFraction * (reanimatorTransform2.mTransY - reanimatorTransform.mTransY);
				aTransform.mSkewX = reanimatorTransform.mSkewX + mFraction * (reanimatorTransform2.mSkewX - reanimatorTransform.mSkewX);
				aTransform.mSkewY = reanimatorTransform.mSkewY + mFraction * (reanimatorTransform2.mSkewY - reanimatorTransform.mSkewY);
				aTransform.mScaleX = reanimatorTransform.mScaleX + mFraction * (reanimatorTransform2.mScaleX - reanimatorTransform.mScaleX);
				aTransform.mScaleY = reanimatorTransform.mScaleY + mFraction * (reanimatorTransform2.mScaleY - reanimatorTransform.mScaleY);
				aTransform.mAlpha = reanimatorTransform.mAlpha + mFraction * (reanimatorTransform2.mAlpha - reanimatorTransform.mAlpha);
				aTransform.mSkewXCos = reanimatorTransform.mSkewXCos + mFraction * (reanimatorTransform2.mSkewXCos - reanimatorTransform.mSkewXCos);
				aTransform.mSkewXSin = reanimatorTransform.mSkewXSin + mFraction * (reanimatorTransform2.mSkewXSin - reanimatorTransform.mSkewXSin);
				aTransform.mSkewYCos = reanimatorTransform.mSkewYCos + mFraction * (reanimatorTransform2.mSkewYCos - reanimatorTransform.mSkewYCos);
				aTransform.mSkewYSin = reanimatorTransform.mSkewYSin + mFraction * (reanimatorTransform2.mSkewYSin - reanimatorTransform.mSkewYSin);
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
			if (reanimatorTransform.mFrame != -1f && reanimatorTransform2.mFrame == -1f && theFrameTime.mFraction > 0f && this.mTrackInstances[theTrackIndex].mTruncateDisappearingFrames)
			{
				aTransform.mFrame = -1f;
				return;
			}
			aTransform.mFrame = reanimatorTransform.mFrame;
		}

		public void GetFrameTime(out ReanimatorFrameTime theFrameTime)
		{
			if (!this.mGetFrameTime)
			{
				theFrameTime = this.mFrameTime;
				return;
			}
			this.mGetFrameTime = false;
			theFrameTime = default(ReanimatorFrameTime);
			int num;
			if (this.mLoopType == ReanimLoopType.REANIM_PLAY_ONCE_FULL_LAST_FRAME || this.mLoopType == ReanimLoopType.REANIM_LOOP_FULL_LAST_FRAME || this.mLoopType == ReanimLoopType.REANIM_PLAY_ONCE_FULL_LAST_FRAME_AND_HOLD)
			{
				num = (int)this.mFrameCount;
			}
			else
			{
				num = (int)(this.mFrameCount - 1);
			}
			float num2 = (float)this.mFrameStart + (float)num * this.mAnimTime;
			float num3 = (float)((int)num2);
			theFrameTime.mFraction = num2 - num3;
			theFrameTime.mAnimFrameBeforeInt = (short)(num3 + 0.5f);
			if (theFrameTime.mAnimFrameBeforeInt >= this.mFrameStart + this.mFrameCount - 1)
			{
				theFrameTime.mAnimFrameBeforeInt = this.mFrameStart + this.mFrameCount - 1;
				theFrameTime.mAnimFrameAfterInt = theFrameTime.mAnimFrameBeforeInt;
			}
			else
			{
				theFrameTime.mAnimFrameAfterInt = theFrameTime.mAnimFrameBeforeInt + 1;
			}
			this.mFrameTime = theFrameTime;
		}

		public int FindTrackIndex(string theTrackName)
		{
			for (int i = 0; i < (int)this.mDefinition.mTrackCount; i++)
			{
				string mName = this.mDefinition.mTracks[i].mName;
				string text = Reanimation.ToLower(theTrackName);
				if (mName == text)
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
			this.GetCurrentTransform(theTrackIndex, out reanimatorTransform, false);
			float num = reanimatorTransform.mSkewXCos * reanimatorTransform.mScaleX;
			float num2 = -reanimatorTransform.mSkewXSin * reanimatorTransform.mScaleX;
			float num3 = reanimatorTransform.mSkewYSin * reanimatorTransform.mScaleY;
			float num4 = reanimatorTransform.mSkewYCos * reanimatorTransform.mScaleY;
			float mTransX = reanimatorTransform.mTransX;
			float mTransY = reanimatorTransform.mTransY;
			reanimatorTransform.PrepareForReuse();
			this.GetTrackBasePoseMatrix(theTrackIndex, out this.basePose);
			Matrix.Invert(ref this.basePose.mMatrix, out this.aBasePoseMatrix);
			theOverlayMatrix = Reanimation.identity;
			this.tempOverlayMatrix = new Matrix
			{
				M11 = this.aBasePoseMatrix.M11 * num + this.aBasePoseMatrix.M12 * num3,
				M12 = this.aBasePoseMatrix.M11 * num2 + this.aBasePoseMatrix.M12 * num4,
				M13 = 0f,
				M14 = 0f,
				M21 = this.aBasePoseMatrix.M21 * num + this.aBasePoseMatrix.M22 * num3,
				M22 = this.aBasePoseMatrix.M21 * num2 + this.aBasePoseMatrix.M22 * num4,
				M23 = 0f,
				M24 = 0f,
				M31 = 0f,
				M32 = 0f,
				M33 = 1f,
				M34 = 0f,
				M41 = this.aBasePoseMatrix.M41 * num + this.aBasePoseMatrix.M42 * num3 + mTransX,
				M42 = this.aBasePoseMatrix.M41 * num2 + this.aBasePoseMatrix.M42 * num4 + mTransY,
				M43 = 0f,
				M44 = 1f
			};
			theOverlayMatrix.mMatrix = new Matrix
			{
				M11 = this.tempOverlayMatrix.M11 * this.mOverlayMatrix.mMatrix.M11 + this.tempOverlayMatrix.M12 * this.mOverlayMatrix.mMatrix.M21,
				M12 = this.tempOverlayMatrix.M11 * this.mOverlayMatrix.mMatrix.M12 + this.tempOverlayMatrix.M12 * this.mOverlayMatrix.mMatrix.M22,
				M13 = 0f,
				M14 = 0f,
				M21 = this.tempOverlayMatrix.M21 * this.mOverlayMatrix.mMatrix.M11 + this.tempOverlayMatrix.M22 * this.mOverlayMatrix.mMatrix.M21,
				M22 = this.tempOverlayMatrix.M21 * this.mOverlayMatrix.mMatrix.M12 + this.tempOverlayMatrix.M22 * this.mOverlayMatrix.mMatrix.M22,
				M23 = 0f,
				M24 = 0f,
				M31 = 0f,
				M32 = 0f,
				M33 = 1f,
				M34 = 0f,
				M41 = this.tempOverlayMatrix.M41 * this.mOverlayMatrix.mMatrix.M11 + this.tempOverlayMatrix.M42 * this.mOverlayMatrix.mMatrix.M21 + this.mOverlayMatrix.mMatrix.M41,
				M42 = this.tempOverlayMatrix.M41 * this.mOverlayMatrix.mMatrix.M12 + this.tempOverlayMatrix.M42 * this.mOverlayMatrix.mMatrix.M22 + this.mOverlayMatrix.mMatrix.M42,
				M43 = 0f,
				M44 = 1f
			};
		}

		public void SetFramesForLayer(string theTrackName)
		{
			if (this.mAnimRate >= 0f)
			{
				this.mAnimTime = 0f;
			}
			else
			{
				this.mAnimTime = 0.9999999f;
			}
			this.mLastFrameTime = -1f;
			this.GetFramesForLayer(theTrackName, out this.mFrameStart, out this.mFrameCount);
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
			for (int i = 0; i < (int)this.mDefinition.mTrackCount; i++)
			{
				string text2 = Reanimation.ToLower(this.mDefinition.mTracks[i].mName);
				if (text == text2)
				{
					return true;
				}
			}
			return false;
		}

		public void StartBlend(byte theBlendTime)
		{
			this.mGetFrameTime = true;
			for (int i = 0; i < (int)this.mDefinition.mTrackCount; i++)
			{
				ReanimatorTransform reanimatorTransform;
				this.GetCurrentTransform(i, out reanimatorTransform, true);
				if (reanimatorTransform != null)
				{
					int num = TodCommon.FloatRoundToInt(reanimatorTransform.mFrame);
					if (num < 0)
					{
						reanimatorTransform.PrepareForReuse();
					}
					else
					{
						ReanimatorTrackInstance reanimatorTrackInstance = this.mTrackInstances[i];
						if (reanimatorTrackInstance.mBlendTransform != null)
						{
							reanimatorTrackInstance.mBlendTransform.PrepareForReuse();
						}
						reanimatorTrackInstance.mBlendTransform = reanimatorTransform;
						reanimatorTrackInstance.mBlendCounter = (byte)((float)theBlendTime / 3f);
						reanimatorTrackInstance.mBlendTime = (byte)((float)theBlendTime / 3f);
						reanimatorTrackInstance.mBlendTransform.mFont = null;
						reanimatorTrackInstance.mBlendTransform.mText = string.Empty;
						reanimatorTrackInstance.mBlendTransform.mImage = null;
					}
				}
			}
		}

		public void SetShakeOverride(string theTrackName, float theShakeAmount)
		{
			int num = this.FindTrackIndex(theTrackName);
			ReanimatorTrackInstance reanimatorTrackInstance = this.mTrackInstances[num];
			reanimatorTrackInstance.mShakeOverride = theShakeAmount;
		}

		public void SetPosition(float theX, float theY)
		{
			this.mOverlayMatrix.mMatrix.Translation = new Vector3(theX, theY, 0f);
		}

		public void OverrideScale(float theScaleX, float theScaleY)
		{
			this.mOverlayMatrix.mMatrix.M11 = theScaleX;
			this.mOverlayMatrix.mMatrix.M22 = theScaleY;
		}

		public int GetTrackIndex(string theTrackName)
		{
			return this.FindTrackIndex(theTrackName);
		}

		public float GetTrackVelocity(string theTrackName)
		{
			return this.GetTrackVelocity(this.GetTrackIndex(theTrackName));
		}

		public float GetTrackVelocity(int aTrackIndex)
		{
			ReanimatorFrameTime reanimatorFrameTime;
			this.GetFrameTime(out reanimatorFrameTime);
			ReanimatorTrack reanimatorTrack = this.mDefinition.mTracks[aTrackIndex];
			ReanimatorTransform reanimatorTransform = reanimatorTrack.mTransforms[(int)reanimatorFrameTime.mAnimFrameBeforeInt];
			ReanimatorTransform reanimatorTransform2 = reanimatorTrack.mTransforms[(int)reanimatorFrameTime.mAnimFrameAfterInt];
			return (reanimatorTransform2.mTransX - reanimatorTransform.mTransX) * ReanimatorXnaHelpers.SECONDS_PER_UPDATE * this.mAnimRate;
		}

		public void SetImageOverride(string theTrackName, Image theImage)
		{
			int num = this.FindTrackIndex(theTrackName);
			ReanimatorTrackInstance reanimatorTrackInstance = this.mTrackInstances[num];
			reanimatorTrackInstance.mImageOverride = theImage;
		}

		public Image GetImageOverride(string theTrackName)
		{
			int num = this.FindTrackIndex(theTrackName);
			ReanimatorTrackInstance reanimatorTrackInstance = this.mTrackInstances[num];
			return reanimatorTrackInstance.mImageOverride;
		}

		public void ShowOnlyTrack(string theTrackName)
		{
			string text = theTrackName.ToLower();
			for (int i = 0; i < (int)this.mDefinition.mTrackCount; i++)
			{
				ReanimatorTrack reanimatorTrack = this.mDefinition.mTracks[i];
				ReanimatorTrackInstance reanimatorTrackInstance = this.mTrackInstances[i];
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
			ReanimatorTrackInstance reanimatorTrackInstance = this.mTrackInstances[theTrackIndex];
			this.mGetFrameTime = true;
			ReanimatorTransform reanimatorTransform;
			this.GetCurrentTransform(theTrackIndex, out reanimatorTransform, false);
			int num = TodCommon.FloatRoundToInt(reanimatorTransform.mFrame);
			Image image = reanimatorTransform.mImage;
			if (this.mDefinition.mReanimAtlas != null && image != null)
			{
				ReanimAtlasImage encodedReanimAtlas = this.mDefinition.mReanimAtlas.GetEncodedReanimAtlas(image);
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
				Matrix.CreateTranslation((float)celWidth * 0.5f, (float)celHeight * 0.5f, 0f, out Reanimation.tempMatrix);
			}
			else if (reanimatorTransform.mFont != null && !string.IsNullOrEmpty(reanimatorTransform.mText))
			{
				Matrix.CreateTranslation(0f, (float)reanimatorTransform.mFont.mAscent, 0f, out Reanimation.tempMatrix);
			}
			SexyTransform2D sexyTransform2D = default(SexyTransform2D);
			Reanimation.MatrixFromTransform(reanimatorTransform, out sexyTransform2D.mMatrix);
			TodCommon.SexyMatrix3Multiply(ref Reanimation.tempMatrix, sexyTransform2D.mMatrix, Reanimation.tempMatrix);
			TodCommon.SexyMatrix3Multiply(ref Reanimation.tempMatrix, this.mOverlayMatrix.mMatrix, Reanimation.tempMatrix);
			TodCommon.SexyMatrix3Translation(ref Reanimation.tempMatrix, reanimatorTrackInstance.mShakeX - 0.5f, reanimatorTrackInstance.mShakeY - 0.5f);
			theMatrix.mMatrix = Reanimation.tempMatrix;
			reanimatorTransform.PrepareForReuse();
		}

		public void GetTrackTranslationMatrix(int theTrackIndex, ref SexyTransform2D theMatrix)
		{
			ReanimatorTrackInstance reanimatorTrackInstance = this.mTrackInstances[theTrackIndex];
			this.mGetFrameTime = true;
			ReanimatorTransform reanimatorTransform;
			this.GetCurrentTransform(theTrackIndex, out reanimatorTransform, false);
			int num = TodCommon.FloatRoundToInt(reanimatorTransform.mFrame);
			Image image = reanimatorTransform.mImage;
			if (this.mDefinition.mReanimAtlas != null && image != null)
			{
				ReanimAtlasImage encodedReanimAtlas = this.mDefinition.mReanimAtlas.GetEncodedReanimAtlas(image);
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
				Matrix.CreateTranslation((float)celWidth * 0.5f, (float)celHeight * 0.5f, 0f, out Reanimation.tempMatrix);
			}
			else if (reanimatorTransform.mFont != null && !string.IsNullOrEmpty(reanimatorTransform.mText))
			{
				Matrix.CreateTranslation(0f, (float)reanimatorTransform.mFont.mAscent, 0f, out Reanimation.tempMatrix);
			}
			SexyTransform2D sexyTransform2D = default(SexyTransform2D);
			Reanimation.MatrixFromTransform(reanimatorTransform, out sexyTransform2D.mMatrix);
			Reanimation.tempMatrix.M41 = sexyTransform2D.mMatrix.M41 + this.mOverlayMatrix.mMatrix.M41 + reanimatorTrackInstance.mShakeX - 0.5f;
			Reanimation.tempMatrix.M42 = sexyTransform2D.mMatrix.M42 + this.mOverlayMatrix.mMatrix.M42 + reanimatorTrackInstance.mShakeY - 0.5f;
			theMatrix.mMatrix = Reanimation.tempMatrix;
			reanimatorTransform.PrepareForReuse();
		}

		public void AssignRenderGroupToTrack(string theTrackName, int theRenderGroup)
		{
			string text = Reanimation.ToLower(theTrackName);
			for (int i = 0; i < (int)this.mDefinition.mTrackCount; i++)
			{
				ReanimatorTrack reanimatorTrack = this.mDefinition.mTracks[i];
				string text2 = Reanimation.ToLower(reanimatorTrack.mName);
				if (!(text != text2))
				{
					this.mTrackInstances[i].mRenderGroup = theRenderGroup;
					return;
				}
			}
		}

		public void AssignRenderGroupToPrefix(string theTrackName, int theRenderGroup)
		{
			int length = theTrackName.Length;
			string s = Reanimation.ToLower(theTrackName);
			for (int i = 0; i < (int)this.mDefinition.mTrackCount; i++)
			{
				ReanimatorTrack reanimatorTrack = this.mDefinition.mTracks[i];
				if (reanimatorTrack.mName.Length >= length)
				{
					string contains = Reanimation.ToLower(reanimatorTrack.mName);
					if (s.StartsWithCharLimit(contains, length))
					{
						this.mTrackInstances[i].mRenderGroup = theRenderGroup;
					}
				}
			}
		}

		public void PropogateColorToAttachments()
		{
			for (int i = 0; i < (int)this.mDefinition.mTrackCount; i++)
			{
				ReanimatorTrackInstance reanimatorTrackInstance = this.mTrackInstances[i];
				GlobalMembersAttachment.AttachmentPropogateColor(reanimatorTrackInstance.mAttachmentID, this.mColorOverride, this.mEnableExtraAdditiveDraw, this.mExtraAdditiveColor, this.mEnableExtraOverlayDraw, this.mExtraOverlayColor);
			}
		}

		public bool ShouldTriggerTimedEvent(float theEventTime)
		{
			if (this.mFrameCount == 0)
			{
				return false;
			}
			if (this.mLastFrameTime < 0f)
			{
				return false;
			}
			if (this.mAnimRate <= 0f)
			{
				return false;
			}
			if (this.mAnimTime >= this.mLastFrameTime)
			{
				if (theEventTime >= this.mLastFrameTime && theEventTime < this.mAnimTime)
				{
					return true;
				}
			}
			else if (theEventTime >= this.mLastFrameTime || theEventTime < this.mAnimTime)
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
			int theTrackIndex = this.FindTrackIndex(theTrackName);
			ReanimatorTransform reanimatorTransform;
			this.GetCurrentTransform(theTrackIndex, out reanimatorTransform, false);
			Image image = reanimatorTransform.mImage;
			if (this.mDefinition.mReanimAtlas != null && image != null)
			{
				ReanimAtlasImage encodedReanimAtlas = this.mDefinition.mReanimAtlas.GetEncodedReanimAtlas(image);
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
			int num = this.FindTrackIndex(theTrackName);
			ReanimatorTrackInstance reanimatorTrackInstance = this.mTrackInstances[num];
			SexyTransform2D transform;
			this.GetTrackBasePoseMatrix(num, out transform);
			Vector2 vector = transform * new Vector2(thePosX, thePosY);
			return GlobalMembersAttachment.AttachParticle(ref reanimatorTrackInstance.mAttachmentID, theParticleSystem, vector.X, vector.Y);
		}

		public void GetTrackBasePoseMatrix(int theTrackIndex, out SexyTransform2D theBasePoseMatrix)
		{
			theBasePoseMatrix = default(SexyTransform2D);
			if (this.mFrameBasePose == ReanimatorXnaHelpers.NO_BASE_POSE)
			{
				theBasePoseMatrix.LoadIdentity();
				return;
			}
			short num = this.mFrameBasePose;
			if (num == -1)
			{
				num = this.mFrameStart;
			}
			ReanimatorTransform reanimatorTransform;
			this.GetTransformAtTime(theTrackIndex, out reanimatorTransform, new ReanimatorFrameTime
			{
				mFraction = 0f,
				mAnimFrameBeforeInt = num,
				mAnimFrameAfterInt = num + 1
			}, false);
			Reanimation.MatrixFromTransform(reanimatorTransform, out theBasePoseMatrix.mMatrix);
			reanimatorTransform.PrepareForReuse();
		}

		public bool IsTrackShowing(string theTrackName)
		{
			ReanimatorFrameTime reanimatorFrameTime;
			this.GetFrameTime(out reanimatorFrameTime);
			int num = this.FindTrackIndex(theTrackName);
			ReanimatorTrack reanimatorTrack = this.mDefinition.mTracks[num];
			ReanimatorTransform reanimatorTransform = reanimatorTrack.mTransforms[(int)reanimatorFrameTime.mAnimFrameAfterInt];
			return reanimatorTransform.mFrame >= 0f;
		}

		public void SetTruncateDisappearingFrames(string theTrackName, bool theTruncateDisappearingFrames)
		{
			if (string.IsNullOrEmpty(theTrackName))
			{
				for (int i = 0; i < (int)this.mDefinition.mTrackCount; i++)
				{
					ReanimatorTrackInstance reanimatorTrackInstance = this.mTrackInstances[i];
					reanimatorTrackInstance.mTruncateDisappearingFrames = theTruncateDisappearingFrames;
				}
				return;
			}
			int num = this.FindTrackIndex(theTrackName);
			ReanimatorTrackInstance reanimatorTrackInstance2 = this.mTrackInstances[num];
			reanimatorTrackInstance2.mTruncateDisappearingFrames = theTruncateDisappearingFrames;
		}

		public void PlayReanim(string theTrackName, ReanimLoopType theLoopType, byte theBlendTime, float theAnimRate)
		{
			if (theBlendTime > 0)
			{
				this.StartBlend(theBlendTime);
			}
			if (theAnimRate != 0f)
			{
				this.mAnimRate = theAnimRate;
			}
			this.mLoopType = theLoopType;
			this.mLoopCount = 0;
			this.SetFramesForLayer(theTrackName);
		}

		public void ReanimationDelete()
		{
			if (this.mTrackInstances != null)
			{
				for (int i = 0; i < this.mTrackInstances.Length; i++)
				{
					this.mTrackInstances[i] = null;
				}
				this.mTrackInstances = null;
			}
		}

		public ReanimatorTrackInstance GetTrackInstanceByName(string theTrackName)
		{
			int num = this.FindTrackIndex(theTrackName);
			return this.mTrackInstances[num];
		}

		public void GetFramesForLayer(string theTrackName, out short theFrameStart, out short theFrameCount)
		{
			if (this.mDefinition.mTrackCount == 0)
			{
				theFrameStart = 0;
				theFrameCount = 0;
				return;
			}
			int num = this.FindTrackIndex(theTrackName);
			ReanimatorTrack reanimatorTrack = this.mDefinition.mTracks[num];
			theFrameStart = 0;
			theFrameCount = 1;
			short num2;
			for (num2 = 0; num2 < reanimatorTrack.mTransformCount; num2 += 1)
			{
				ReanimatorTransform reanimatorTransform = reanimatorTrack.mTransforms[(int)num2];
				if (reanimatorTransform.mFrame >= 0f)
				{
					theFrameStart = num2;
					break;
				}
			}
			for (int i = (int)(reanimatorTrack.mTransformCount - 1); i >= (int)num2; i--)
			{
				ReanimatorTransform reanimatorTransform2 = reanimatorTrack.mTransforms[i];
				if (reanimatorTransform2.mFrame >= 0f)
				{
					theFrameCount = (short)(i - (int)theFrameStart + 1);
					return;
				}
			}
		}

		public void UpdateAttacherTrack(int theTrackIndex)
		{
			ReanimatorTrackInstance reanimatorTrackInstance = this.mTrackInstances[theTrackIndex];
			ReanimatorTransform reanimatorTransform;
			this.GetCurrentTransform(theTrackIndex, out reanimatorTransform, false);
			AttacherInfo attacherInfo;
			Reanimation.ParseAttacherTrack(reanimatorTransform, out attacherInfo);
			ReanimationType reanimationType = ReanimationType.REANIM_NONE;
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
			if (reanimationType == ReanimationType.REANIM_NONE)
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
				this.mFrameBasePose = ReanimatorXnaHelpers.NO_BASE_POSE;
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
					this.AttacherSynchWalkSpeed(theTrackIndex, ref reanimation, attacherInfo);
				}
				else
				{
					reanimation.mAnimRate = attacherInfo.mAnimRate;
				}
				reanimation.mLoopType = attacherInfo.mLoopType;
			}
			SexyColor theColor = TodCommon.ColorsMultiply(this.mColorOverride, reanimatorTrackInstance.mTrackColor);
			theColor.mAlpha = TodCommon.ClampInt(TodCommon.FloatRoundToInt(reanimatorTransform.mAlpha * (float)theColor.mAlpha), 0, 255);
			GlobalMembersAttachment.AttachmentPropogateColor(reanimatorTrackInstance.mAttachmentID, theColor, this.mEnableExtraAdditiveDraw, this.mExtraAdditiveColor, this.mEnableExtraOverlayDraw, this.mExtraOverlayColor);
		}

		public static void ParseAttacherTrack(ReanimatorTransform theTransform, out AttacherInfo theAttacherInfo)
		{
			theAttacherInfo = new AttacherInfo();
			theAttacherInfo.mReanimName = "";
			theAttacherInfo.mTrackName = "";
			theAttacherInfo.mAnimRate = 12f;
			theAttacherInfo.mLoopType = ReanimLoopType.REANIM_LOOP;
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
				if (float.TryParse(text, ref num5))
				{
					theAttacherInfo.mAnimRate = num5;
				}
				else if (text == "hold")
				{
					theAttacherInfo.mLoopType = ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD;
				}
				else if (text == "once")
				{
					theAttacherInfo.mLoopType = ReanimLoopType.REANIM_PLAY_ONCE;
				}
				num2 = theTransform.mText.IndexOf("[", num4 + 1);
			}
		}

		public void AttacherSynchWalkSpeed(int theTrackIndex, ref Reanimation theAttachReanim, AttacherInfo theAttacherInfo)
		{
			ReanimatorTrack reanimatorTrack = this.mDefinition.mTracks[theTrackIndex];
			ReanimatorFrameTime reanimatorFrameTime;
			this.GetFrameTime(out reanimatorFrameTime);
			int num = (int)reanimatorFrameTime.mAnimFrameBeforeInt;
			while (num > (int)this.mFrameStart && !(reanimatorTrack.mTransforms[num - 1].mText != reanimatorTrack.mTransforms[num].mText))
			{
				num--;
			}
			int num2 = (int)reanimatorFrameTime.mAnimFrameBeforeInt;
			while (num2 < (int)(this.mFrameStart + this.mFrameCount - 1) && !(reanimatorTrack.mTransforms[num2 + 1].mText != reanimatorTrack.mTransforms[num2].mText))
			{
				num2++;
			}
			int num3 = num2 - num;
			ReanimatorTransform reanimatorTransform = reanimatorTrack.mTransforms[num];
			ReanimatorTransform reanimatorTransform2 = reanimatorTrack.mTransforms[num + num3 - 1];
			if (TodCommon.FloatApproxEqual(this.mAnimRate, 0f))
			{
				theAttachReanim.mAnimRate = 0f;
				return;
			}
			float num4 = -(reanimatorTransform2.mTransX - reanimatorTransform.mTransX);
			float num5 = (float)num3 / this.mAnimRate;
			if (TodCommon.FloatApproxEqual(num5, 0f))
			{
				theAttachReanim.mAnimRate = 0f;
				return;
			}
			int num6 = theAttachReanim.FindTrackIndex("_ground");
			ReanimatorTrack reanimatorTrack2 = theAttachReanim.mDefinition.mTracks[num6];
			ReanimatorTransform reanimatorTransform3 = reanimatorTrack2.mTransforms[(int)theAttachReanim.mFrameStart];
			ReanimatorTransform reanimatorTransform4 = reanimatorTrack2.mTransforms[(int)(theAttachReanim.mFrameStart + theAttachReanim.mFrameCount - 1)];
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
			ReanimatorTrackInstance reanimatorTrackInstance = this.mTrackInstances[theTrackIndex];
			AttachEffect attachEffect = GlobalMembersAttachment.FindFirstAttachment(reanimatorTrackInstance.mAttachmentID);
			if (attachEffect != null)
			{
				attachEffect.mOffset.mMatrix.M13 = num10 - num9;
			}
			theAttachReanim.mAnimRate = num8 * (float)theAttachReanim.mFrameCount / num5;
		}

		public bool IsAnimPlaying(string theTrackName)
		{
			short num;
			short num2;
			this.GetFramesForLayer(theTrackName, out num, out num2);
			return this.mFrameStart == num && this.mFrameCount == num2;
		}

		public void SetBasePoseFromAnim(string theTrackName)
		{
			short num;
			short num2;
			this.GetFramesForLayer(theTrackName, out num, out num2);
			this.mFrameBasePose = num;
		}

		public void ReanimBltMatrix(Graphics g, Image theImage, ref Matrix theTransform, ref TRect theClipRect, SexyColor theColor, Graphics.DrawMode theDrawMode, TRect theSrcRect)
		{
			ReanimationParams reanimationParams = ReanimatorXnaHelpers.gReanimationParamArray[(int)this.mReanimationType];
			if (!GlobalStaticVars.gSexyAppBase.Is3DAccelerated() && TodCommon.TestBit((uint)reanimationParams.mReanimParamFlags, 1) && TodCommon.FloatApproxEqual(theTransform.M12, 0f) && TodCommon.FloatApproxEqual(theTransform.M21, 0f) && theTransform.M11 > 0f && theTransform.M22 > 0f && theColor == SexyColor.White)
			{
				float m = theTransform.M11;
				float m2 = theTransform.M22;
				int theX = TodCommon.FloatRoundToInt(theTransform.M41 - m * (float)theSrcRect.mWidth * 0.5f);
				int theY = TodCommon.FloatRoundToInt(theTransform.M42 - m2 * (float)theSrcRect.mHeight * 0.5f);
				Graphics.DrawMode drawMode = g.GetDrawMode();
				g.SetDrawMode(theDrawMode);
				TRect mClipRect = g.mClipRect;
				g.SetClipRect(ref theClipRect);
				if (TodCommon.FloatApproxEqual(m, 1f) && TodCommon.FloatApproxEqual(m2, 1f))
				{
					g.DrawImage(theImage, theX, theY, theSrcRect);
				}
				else
				{
					int theWidth = TodCommon.FloatRoundToInt(m * (float)theSrcRect.mWidth);
					int theHeight = TodCommon.FloatRoundToInt(m2 * (float)theSrcRect.mHeight);
					TRect theDestRect = new TRect(theX, theY, theWidth, theHeight);
					g.DrawImage(theImage, theDestRect, theSrcRect);
				}
				g.SetDrawMode(drawMode);
				g.SetClipRect(ref mClipRect);
				return;
			}
			TodCommon.TodBltMatrix(g, theImage, ref theTransform, theClipRect, theColor, theDrawMode, theSrcRect, this.mClip);
		}

		public Reanimation FindSubReanim(ReanimationType theReanimType)
		{
			if (this.mReanimationType == theReanimType)
			{
				return this;
			}
			for (int i = 0; i < (int)this.mDefinition.mTrackCount; i++)
			{
				ReanimatorTrackInstance reanimatorTrackInstance = this.mTrackInstances[i];
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
