using System;
using System.Collections.Generic;
using Sexy;
using Sexy.GraphicsLib;
using Sexy.Sound;
using Sexy.WidgetsLib;

namespace JeffLib
{
	public class ExtraSexyButton : ButtonWidget
	{
		public ExtraSexyButton(int theId, ButtonListener theButtonListener)
			: base(theId, theButtonListener)
		{
			this.mUsesAnimators = true;
			this.mGIFMaskIgnoreColor = 0;
			this.mGIFMask = null;
			this.mDraw = true;
			this.mBlink = false;
			this.mSyncFrames = false;
			this.mStopExcludedSounds = true;
			this.mButtonAnimation.PauseAnim(false);
			this.mButtonAnimation.SetPingPong(false);
			this.mButtonAnimation.SetDelay(10);
			this.mButtonAnimation.SetMaxFrames(1);
			this.mButtonAnimation.StopWhenDone(false);
			this.mOverAnimation.PauseAnim(true);
			this.mOverAnimation.SetPingPong(false);
			this.mOverAnimation.SetDelay(10);
			this.mOverAnimation.SetMaxFrames(1);
			this.mOverAnimation.StopWhenDone(false);
			this.mDownAnimation.PauseAnim(true);
			this.mDownAnimation.SetPingPong(false);
			this.mDownAnimation.SetDelay(10);
			this.mDownAnimation.SetMaxFrames(1);
			this.mDownAnimation.StopWhenDone(false);
			this.mMouseOverSnd = null;
			this.mMaskWidth = 0;
			this.mMouseOverSndID = -1;
			this.mDownImage = null;
			this.mOverImage = null;
			this.mButtonImage = null;
			this.mDoFinger = true;
			this.mPitchShift = int.MaxValue;
			this.mAdditiveDown = (this.mAdditiveOver = false);
			this.mOverColor = (this.mDownColor = new SexyColor(0, 0, 0, 0));
		}

		public override void Dispose()
		{
			base.Dispose();
		}

		public override bool IsPointVisible(int pX, int pY)
		{
			if (pX >= this.mWidth || pY >= this.mHeight || pX < 0 || pY < 0)
			{
				return false;
			}
			if (this.mMask == null && this.mGIFMask == null)
			{
				return true;
			}
			if (this.mMask != null)
			{
				int num = pY * this.mMaskWidth + pX;
				uint num2 = this.mMask[num];
				if (num2 == 4294967295U)
				{
					return true;
				}
			}
			else if (this.mGIFMask != null)
			{
				int num3 = pY * this.mGIFMask.mWidth + pX;
				uint num4;
				if (this.mGIFMask.mColorIndices != null)
				{
					byte b = this.mGIFMask.mColorIndices[num3];
					num4 = this.mGIFMask.mColorTable[(int)b];
				}
				else
				{
					num4 = this.mGIFMask.GetBits()[num3];
				}
				if ((ulong)num4 != (ulong)((long)this.mGIFMaskIgnoreColor))
				{
					return true;
				}
			}
			return false;
		}

		public override void MouseEnter()
		{
			base.MouseEnter();
			if (this.mButtonListener != null)
			{
				this.mButtonListener.ButtonMouseEnter(this.mId);
			}
			if (this.mMouseOverSnd != null && !this.mMouseOverSnd.IsPlaying())
			{
				bool flag = true;
				for (int i = 0; i < this.mMouseOverExclusionList.Count; i++)
				{
					if (this.mMouseOverExclusionList[i].IsPlaying())
					{
						if (!this.mStopExcludedSounds)
						{
							flag = false;
							break;
						}
						this.mMouseOverExclusionList[i].Stop();
					}
				}
				if (flag || this.mStopExcludedSounds)
				{
					if (this.mPitchShift != 2147483647)
					{
						this.mMouseOverSnd.AdjustPitch((double)this.mPitchShift);
					}
					this.mMouseOverSnd.Play(false, false);
				}
			}
			else if (this.mMouseOverSndID != -1)
			{
				if (this.mPitchShift != 2147483647)
				{
					SoundInstance soundInstance = GlobalMembers.gSexyAppBase.mSoundManager.GetSoundInstance(this.mMouseOverSndID);
					if (soundInstance != null)
					{
						soundInstance.AdjustPitch((double)this.mPitchShift);
						soundInstance.Play(false, true);
					}
				}
				else
				{
					GlobalMembers.gSexyApp.PlaySample(this.mMouseOverSndID);
				}
			}
			if (this.mOverAnimation.IsPaused() && !this.mIsDown)
			{
				this.mOverAnimation.ResetAnim();
				this.mOverAnimation.PauseAnim(false);
				if (this.mSyncFrames && this.mOverAnimation.GetMaxFrames() > this.mButtonAnimation.GetFrame())
				{
					this.mOverAnimation.mUpdateCnt = this.mButtonAnimation.mUpdateCnt;
					this.mOverAnimation.SetFrame(this.mButtonAnimation.GetFrame());
				}
				this.mButtonAnimation.PauseAnim(true);
				return;
			}
			if (this.mIsDown && this.mDownAnimation.IsPaused())
			{
				if (this.mDownImage != null)
				{
					this.mOverAnimation.PauseAnim(true);
				}
				else
				{
					this.mOverAnimation.ResetAnim();
					this.mOverAnimation.PauseAnim(false);
					if (this.mSyncFrames && this.mOverAnimation.GetMaxFrames() > this.mButtonAnimation.GetFrame())
					{
						this.mOverAnimation.mUpdateCnt = this.mButtonAnimation.mUpdateCnt;
						this.mOverAnimation.SetFrame(this.mButtonAnimation.GetFrame());
					}
				}
				this.mButtonAnimation.PauseAnim(true);
				this.mDownAnimation.ResetAnim();
				this.mDownAnimation.PauseAnim(false);
				if (this.mSyncFrames && this.mDownAnimation.GetMaxFrames() > this.mOverAnimation.GetFrame())
				{
					this.mDownAnimation.mUpdateCnt = this.mButtonAnimation.mUpdateCnt;
					this.mDownAnimation.SetFrame(this.mButtonAnimation.GetFrame());
				}
			}
		}

		public override void MouseLeave()
		{
			base.MouseLeave();
			if (this.mButtonListener != null)
			{
				this.mButtonListener.ButtonMouseLeave(this.mId);
			}
			Animator animator = null;
			if (!this.mOverAnimation.IsPaused())
			{
				animator = this.mOverAnimation;
			}
			else if (!this.mDownAnimation.IsPaused())
			{
				animator = this.mDownAnimation;
			}
			this.mOverAnimation.PauseAnim(true);
			this.mDownAnimation.PauseAnim(true);
			this.mButtonAnimation.ResetAnim();
			this.mButtonAnimation.PauseAnim(false);
			if (this.mSyncFrames && this.mButtonAnimation.GetMaxFrames() > animator.GetFrame())
			{
				this.mButtonAnimation.mUpdateCnt = animator.mUpdateCnt;
				this.mButtonAnimation.SetFrame(animator.GetFrame());
			}
		}

		public override void MouseDown(int pX, int pY, int pClickCount)
		{
			base.MouseDown(pX, pY, pClickCount);
			if (this.mDownImage != null)
			{
				this.mOverAnimation.PauseAnim(true);
			}
			else
			{
				this.mOverAnimation.ResetAnim();
				this.mOverAnimation.PauseAnim(false);
				if (this.mSyncFrames && this.mOverAnimation.GetMaxFrames() > this.mButtonAnimation.GetFrame())
				{
					this.mOverAnimation.mUpdateCnt = this.mButtonAnimation.mUpdateCnt;
					this.mOverAnimation.SetFrame(this.mButtonAnimation.GetFrame());
				}
			}
			this.mButtonAnimation.PauseAnim(true);
			this.mDownAnimation.ResetAnim();
			this.mDownAnimation.PauseAnim(false);
			if (this.mSyncFrames && this.mDownAnimation.GetMaxFrames() > this.mOverAnimation.GetFrame())
			{
				this.mDownAnimation.mUpdateCnt = this.mOverAnimation.mUpdateCnt;
				this.mDownAnimation.SetFrame(this.mOverAnimation.GetFrame());
			}
		}

		public override void MouseUp(int pX, int pY)
		{
			base.MouseUp(pX, pY);
			if (this.mIsOver)
			{
				Animator animator = null;
				if (!this.mButtonAnimation.IsPaused())
				{
					animator = this.mButtonAnimation;
				}
				else if (!this.mDownAnimation.IsPaused())
				{
					animator = this.mDownAnimation;
				}
				this.mDownAnimation.PauseAnim(true);
				this.mButtonAnimation.PauseAnim(true);
				this.mOverAnimation.ResetAnim();
				this.mOverAnimation.PauseAnim(false);
				if (this.mSyncFrames && animator != null && this.mOverAnimation.GetMaxFrames() > animator.GetFrame())
				{
					this.mOverAnimation.mUpdateCnt = animator.mUpdateCnt;
					this.mOverAnimation.SetFrame(animator.GetFrame());
				}
			}
		}

		public override void Update()
		{
			base.Update();
			if (this.mDisabled || !GlobalMembers.gSexyApp.mHasFocus)
			{
				return;
			}
			this.mButtonAnimation.UpdateAnim();
			this.mOverAnimation.UpdateAnim();
			this.mDownAnimation.UpdateAnim();
			if (this.mButtonAnimation.FrameChanged() || this.mOverAnimation.FrameChanged() || this.mDownAnimation.FrameChanged() || this.mBlink)
			{
				this.MarkDirty();
			}
		}

		public override void Draw(Graphics pGfx)
		{
			if (!this.mDraw)
			{
				return;
			}
			if (!this.mUsesAnimators)
			{
				base.Draw(pGfx);
				return;
			}
			int theX = 0;
			int theY = 0;
			if (this.mFont != null)
			{
				theX = (this.mWidth - this.mFont.StringWidth(this.mLabel)) / 2;
				theY = (this.mHeight + this.mFont.GetAscent() - this.mFont.GetAscent() / 6 - 1) / 2;
			}
			Image image = ((this.mOverImage == null) ? this.mButtonImage : this.mOverImage);
			if (this.mDisabled)
			{
				base.Draw(pGfx);
			}
			else if (!this.mButtonAnimation.IsPaused() && this.mButtonImage != null)
			{
				pGfx.DrawImageCel(this.mButtonImage, 0, 0, this.mButtonAnimation.GetFrame());
			}
			else if (!this.mDownAnimation.IsPaused() && this.mDownImage != null)
			{
				if (!this.mBlink && this.mAdditiveDown)
				{
					pGfx.SetDrawMode(0);
					pGfx.SetColorizeImages(true);
					pGfx.SetColor(this.mDownColor);
				}
				pGfx.DrawImageCel(this.mDownImage, 0, 0, this.mDownAnimation.GetFrame());
				if (!this.mBlink && this.mAdditiveDown)
				{
					pGfx.SetDrawMode(0);
					pGfx.SetColorizeImages(false);
				}
			}
			else if (!this.mDownAnimation.IsPaused() && this.mDownImage == null)
			{
				if (image != null)
				{
					if (!this.mBlink && this.mAdditiveDown)
					{
						pGfx.SetDrawMode(1);
						pGfx.SetColorizeImages(true);
						pGfx.SetColor(this.mDownColor);
					}
					pGfx.DrawImageCel(image, 1, 1, this.mOverAnimation.GetFrame());
					if (!this.mBlink && this.mAdditiveDown)
					{
						pGfx.SetDrawMode(0);
						pGfx.SetColorizeImages(false);
					}
				}
			}
			else if (!this.mOverAnimation.IsPaused() && image != null)
			{
				if (!this.mBlink && this.mAdditiveOver)
				{
					pGfx.SetDrawMode(1);
					pGfx.SetColorizeImages(true);
					pGfx.SetColor(this.mOverColor);
				}
				pGfx.DrawImageCel(image, 0, 0, this.mOverAnimation.GetFrame());
				if (!this.mBlink && this.mAdditiveOver)
				{
					pGfx.SetDrawMode(0);
					pGfx.SetColorizeImages(false);
				}
			}
			if (this.mBlink && !this.mIsOver)
			{
				this.mIsOver = true;
				int num = this.mUpdateCnt % 100;
				if (num > 50)
				{
					num = 100 - num;
				}
				pGfx.SetColor(255, 255, 255, 255 * num / 50);
				pGfx.SetColorizeImages(true);
				pGfx.SetDrawMode(1);
				if (this.mDisabled)
				{
					base.Draw(pGfx);
				}
				else if (!this.mButtonAnimation.IsPaused() && this.mButtonImage != null)
				{
					pGfx.DrawImageCel(this.mButtonImage, 0, 0, this.mButtonAnimation.GetFrame());
				}
				else if (!this.mButtonAnimation.IsPaused() && this.mButtonImage == null && this.mOverImage != null)
				{
					num = this.mUpdateCnt % 254;
					if (num > 127)
					{
						num = 254 - num;
					}
					pGfx.SetColor(255, 255, 255, num);
					pGfx.DrawImageCel(this.mOverImage, 0, 0, 0);
				}
				else if (!this.mDownAnimation.IsPaused() && this.mDownImage != null)
				{
					pGfx.DrawImageCel(this.mDownImage, 0, 0, this.mDownAnimation.GetFrame());
				}
				else if (!this.mDownAnimation.IsPaused() && this.mDownImage == null && image != null)
				{
					pGfx.DrawImageCel(image, 1, 1, this.mOverAnimation.GetFrame());
				}
				else if (!this.mOverAnimation.IsPaused() && image != null)
				{
					pGfx.DrawImageCel(image, 0, 0, this.mOverAnimation.GetFrame());
				}
				pGfx.SetDrawMode(0);
				pGfx.SetColorizeImages(false);
				this.mIsOver = false;
			}
			if (this.mFont != null)
			{
				if (this.mIsOver)
				{
					pGfx.SetColor(this.mColors[1]);
				}
				else
				{
					pGfx.SetColor(this.mColors[0]);
				}
				pGfx.SetFont(this.mFont);
				pGfx.DrawString(this.mLabel, theX, theY);
			}
		}

		public void DrawBoundingBox(Graphics pGfx)
		{
			pGfx.SetColor(255, 255, 255, 128);
			pGfx.DrawRect(0, 0, this.mWidth, this.mHeight);
		}

		public void SetMask(uint[] pMask, int pWidth)
		{
			this.mMask = pMask;
			this.mMaskWidth = pWidth;
		}

		public void SetMask(MemoryImage gif_mask, int ignore_color)
		{
			this.mGIFMask = gif_mask;
			this.mGIFMaskIgnoreColor = ignore_color;
		}

		public void SetDraw(bool pDraw)
		{
			this.mDraw = pDraw;
		}

		public void SetStopExcludedSounds(bool pS)
		{
			this.mStopExcludedSounds = pS;
		}

		public void SetBlink(bool pBlink)
		{
			this.mBlink = pBlink;
		}

		protected uint[] mMask;

		protected MemoryImage mGIFMask;

		protected int mGIFMaskIgnoreColor;

		protected int mMaskWidth;

		protected bool mDraw;

		protected bool mStopExcludedSounds;

		protected bool mBlink;

		protected List<SoundInstance> mMouseOverExclusionList = new List<SoundInstance>();

		public Animator mOverAnimation = new Animator();

		public Animator mDownAnimation = new Animator();

		public Animator mButtonAnimation = new Animator();

		public SexyColor mOverColor = default(SexyColor);

		public SexyColor mDownColor = default(SexyColor);

		public SoundInstance mMouseOverSnd;

		public int mMouseOverSndID;

		public int mPitchShift;

		public bool mAdditiveOver;

		public bool mAdditiveDown;

		public bool mSyncFrames;

		public bool mUsesAnimators;
	}
}
