using System;
using System.Collections.Generic;
using Sexy;
using Sexy.GraphicsLib;
using Sexy.Misc;

namespace JeffLib
{
	public class Animator
	{
		protected void UpdateFadeData()
		{
			if (this.mFadeData.mFadeState == 2)
			{
				if ((this.mFadeData.mVal += this.mFadeData.mFadeInRate) >= this.mFadeData.mFadeInTarget)
				{
					this.mFadeData.mVal = this.mFadeData.mFadeInTarget;
					if (this.mFadeData.mFadeCount > 0)
					{
						this.mFadeData.mFadeCount--;
					}
					this.mFadeData.mFadeState = 1;
				}
			}
			else if ((this.mFadeData.mVal -= this.mFadeData.mFadeOutRate) <= this.mFadeData.mFadeOutTarget)
			{
				this.mFadeData.mVal = this.mFadeData.mFadeOutTarget;
				if (this.mFadeData.mFadeCount > 0)
				{
					this.mFadeData.mFadeCount--;
				}
				this.mFadeData.mFadeState = 2;
			}
			if (this.mFadeData.mFadeCount == 0)
			{
				this.mFadeData.mFadeState = 0;
				if (this.mFadeData.mStopWhenDone)
				{
					this.mDone = true;
				}
			}
		}

		protected void _Init()
		{
		}

		public Animator()
		{
			this._Init();
		}

		public Animator(Animator a)
		{
			a.CopyTo(this);
		}

		public virtual bool UpdateAnim(bool change_loop_count)
		{
			this.mUpdateCnt++;
			this.mFrameChanged = false;
			if (this.mTimeLimit > 0 && !this.mPaused && !this.mDone && ++this.mCurrentTime >= this.mTimeLimit)
			{
				this.mPaused = true;
				this.mDone = true;
			}
			if (!this.mPaused && !this.mDone)
			{
				int num;
				if (this.mPingPong)
				{
					num = (this.mAnimForward ? (-1) : 0);
				}
				else
				{
					num = ((this.mLoopDir > 0) ? 0 : (-1));
				}
				int num2 = this.mMaxFrames;
				int num3 = ((this.mFrameDelays.Count > 0 && this.mMaxFrames > 1) ? this.mFrameDelays[this.mCurrentFrame] : this.mFrameDelay);
				if (this.mLoopSubsection)
				{
					num = this.mLoopStart - 1;
					num2 = this.mLoopEnd + 1;
				}
				if (num3 != 0 && this.mUpdateCnt % num3 == 0 && (this.mNumIterations == 0 || this.mLoopCount <= this.mNumIterations))
				{
					this.mFrameChanged = true;
					if (!this.mPingPong || this.mDrawRandomly)
					{
						if (!this.mDrawRandomly)
						{
							if (this.mLoopDir >= 0)
							{
								if ((this.mCurrentFrame += this.mStepAmt) >= num2)
								{
									if (this.mLoopSubsection)
									{
										num = this.mLoopStart;
									}
									this.mCurrentFrame = num;
									if (change_loop_count)
									{
										this.mLoopCount++;
									}
									if (this.mStopWhenDone || (this.mLoopCount >= this.mNumIterations && this.mNumIterations > 0))
									{
										this.mPaused = true;
										this.mDone = true;
									}
								}
							}
							else if ((this.mCurrentFrame -= this.mStepAmt) <= num)
							{
								if (this.mLoopSubsection)
								{
									num2 = this.mLoopEnd;
								}
								this.mCurrentFrame = num2 - 1;
								if (change_loop_count)
								{
									this.mLoopCount++;
								}
								if (this.mStopWhenDone || (this.mLoopCount >= this.mNumIterations && this.mNumIterations > 0))
								{
									this.mPaused = true;
									this.mDone = true;
								}
							}
						}
						else
						{
							if (this.mRandomFrames.Count == 0)
							{
								if (change_loop_count)
								{
									this.mLoopCount++;
								}
								if (this.mStopWhenDone || (this.mLoopCount >= this.mNumIterations && this.mNumIterations > 0))
								{
									this.mPaused = true;
									this.mDone = true;
								}
								for (int i = 0; i < this.mMaxFrames; i++)
								{
									this.mRandomFrames.Add(i);
								}
							}
							this.mCurrentFrame = Sexy.Common.Rand() % this.mRandomFrames.Count;
							this.mRandomFrames.RemoveAt(this.mCurrentFrame);
						}
					}
					else if (!this.mAnimForward)
					{
						if ((this.mCurrentFrame += this.mStepAmt) >= num2)
						{
							if (change_loop_count)
							{
								this.mLoopCount++;
							}
							this.mCurrentFrame = num2 - 2;
							if (this.mMaxFrames == 1)
							{
								this.mCurrentFrame = 0;
							}
							this.mAnimForward = true;
							if (this.mLoopCount >= this.mNumIterations && this.mNumIterations > 0)
							{
								this.mPaused = (this.mDone = true);
							}
						}
					}
					else if (this.mAnimForward && (this.mCurrentFrame -= this.mStepAmt) <= num)
					{
						if (change_loop_count)
						{
							this.mLoopCount++;
						}
						if (this.mLoopSubsection)
						{
							this.mCurrentFrame = num + 2;
						}
						else
						{
							this.mCurrentFrame = ((num < 0) ? (num + 2) : (num + 1));
						}
						if (this.mMaxFrames == 1 && this.mCurrentFrame == 1)
						{
							this.mCurrentFrame--;
						}
						this.mAnimForward = false;
						if (this.mStopWhenDone || (this.mLoopCount >= this.mNumIterations && this.mNumIterations > 0))
						{
							this.mCurrentFrame = num;
							this.mPaused = true;
							this.mDone = true;
						}
					}
				}
				if (this.mFadeData.mFadeState != 0)
				{
					this.UpdateFadeData();
				}
			}
			if (this.mMaxFrames == 1)
			{
				this.mFrameChanged = false;
			}
			return this.mFrameChanged;
		}

		public virtual bool UpdateAnim()
		{
			return this.UpdateAnim(true);
		}

		public virtual void PauseAnim(bool pPause)
		{
			this.mPaused = pPause;
		}

		public virtual void ResetAnim()
		{
			this.mPaused = false;
			this.mCurrentFrame = ((this.mLoopDir >= 0) ? 0 : (this.mMaxFrames - 1));
			this.mDone = false;
			this.mLoopCount = 0;
			this.mLoopStart = 0;
			this.mLoopEnd = this.mMaxFrames;
			this.mLoopSubsection = false;
			this.mCurrentTime = 0;
			this.mUpdateCnt = 0;
		}

		public virtual void Clear()
		{
			this.mPaused = true;
			this.mUpdateCnt = (this.mCurrentFrame = 0);
			this.mFrameDelay = 1;
			this.mMaxFrames = 1;
			this.mLoopDir = 1;
			this.mPingPong = false;
			this.mAnimForward = true;
			this.mLoopSubsection = false;
			this.mStopWhenDone = false;
			this.mDone = false;
			this.mFrameChanged = true;
			this.mLoopCount = (this.mNumIterations = 0);
			this.mStepAmt = 1;
			this.mImage = null;
			this.mXOff = (this.mYOff = 0f);
			this.mLoopStart = (this.mLoopEnd = 0);
			this.mId = -1;
			this.mPriority = 0;
			this.mResetOnStart = false;
			this.mCanRotate = false;
			this.mDrawAdditive = false;
			this.mDrawColorized = false;
			this.mDrawRandomly = false;
			this.mTimeLimit = (this.mCurrentTime = -1);
			this.mFadeData = new FadeData();
		}

		public virtual void LoopSubsection(int pStartFrame, int pEndFrame)
		{
			this.mLoopSubsection = true;
			this.mLoopStart = pStartFrame;
			this.mLoopEnd = pEndFrame;
		}

		public virtual void SetLoopDir(int pDir)
		{
			this.mLoopDir = pDir;
			if (pDir < 0)
			{
				this.mCurrentFrame = this.mMaxFrames - 1;
			}
		}

		public virtual void SetTimeLimit(int t)
		{
			this.mTimeLimit = t;
			this.mCurrentTime = 0;
		}

		private void START_ADDITIVE(Graphics g, SexyColor val)
		{
			g.SetDrawMode(1);
			if (this.mFadeData.mFadeState == 0)
			{
				g.SetColor(val);
			}
			else
			{
				g.SetColor(val.mRed, val.mGreen, val.mBlue, this.mFadeData.mVal);
			}
			g.SetColorizeImages(true);
		}

		private void START_ADDITIVE(Graphics g)
		{
			g.SetDrawMode(0);
			g.SetColorizeImages(false);
		}

		private void START_COLORIZED(Graphics g, SexyColor val)
		{
			g.SetColorizeImages(true);
			if (this.mFadeData.mFadeState == 0)
			{
				g.SetColor(val);
				return;
			}
			g.SetColor(val.mRed, val.mGreen, val.mBlue, this.mFadeData.mVal);
		}

		private void STOP_COLORIZED(Graphics g)
		{
			g.SetColorizeImages(false);
		}

		private void START_FADE(Graphics g)
		{
			if (!this.mDrawAdditive && !this.mDrawColorized && this.mFadeData.mFadeState != 0)
			{
				g.SetColor(255, 255, 255, this.mFadeData.mVal);
				g.SetColorizeImages(true);
			}
		}

		private void STOP_FADE(Graphics g)
		{
			if (!this.mDrawAdditive && !this.mDrawColorized && this.mFadeData.mFadeState != 0)
			{
				g.SetColorizeImages(false);
			}
		}

		private void SETUP_DRAWING(Graphics g)
		{
			this.START_FADE(g);
			if (this.mDrawAdditive)
			{
				this.START_ADDITIVE(g, this.mAdditiveColor);
				return;
			}
			if (this.mDrawColorized)
			{
				this.START_COLORIZED(g, this.mColorizeVal);
			}
		}

		private void END_DRAWING(Graphics g)
		{
			this.STOP_FADE(g);
			if (this.mDrawAdditive)
			{
				this.START_ADDITIVE(g, this.mAdditiveColor);
				return;
			}
			if (this.mDrawColorized)
			{
				this.START_COLORIZED(g, this.mColorizeVal);
			}
		}

		public virtual void DrawAdditively(SexyColor pColor)
		{
			this.StopDrawingColorized();
			this.mDrawAdditive = true;
			this.mAdditiveColor = pColor;
		}

		public virtual void StopDrawingAdditively()
		{
			this.mDrawAdditive = false;
		}

		public virtual void DrawColorized(SexyColor pColor)
		{
			this.StopDrawingAdditively();
			this.mDrawColorized = true;
			this.mColorizeVal = pColor;
		}

		public virtual void StopDrawingColorized()
		{
			this.mDrawColorized = false;
		}

		public virtual void SetMaxFrames(int pMax)
		{
			this.mMaxFrames = pMax;
			this.mCurrentFrame = 0;
			if (this.mLoopDir < 0)
			{
				this.mCurrentFrame = this.mMaxFrames - 1;
			}
			this.mFrameDelays.Clear();
			for (int i = 0; i < this.mFrameDelays.Count; i++)
			{
				this.mFrameDelays.Add(this.mFrameDelay);
			}
		}

		public virtual void SetImage(Image pImage)
		{
			this.mImage = null;
			this.mCurrentFrame = 0;
			if (pImage != null)
			{
				this.SetMaxFrames((pImage.mNumCols > pImage.mNumRows) ? pImage.mNumCols : pImage.mNumRows);
			}
			this.mImage = pImage;
		}

		public virtual void Draw(Graphics g, int pX, int pY)
		{
			if (this.mImage == null || this.IsPaused() || this.IsDone())
			{
				return;
			}
			this.SETUP_DRAWING(g);
			g.DrawImageCel(this.mImage, pX + (int)this.mXOff, pY + (int)this.mYOff, this.GetFrame());
			this.END_DRAWING(g);
		}

		public virtual void Draw(Graphics g, float pX, float pY, bool pSmooth)
		{
			if (this.mImage == null || this.IsPaused() || this.IsDone())
			{
				return;
			}
			this.SETUP_DRAWING(g);
			if (pSmooth)
			{
				int theX = 0;
				int theY = 0;
				if (this.mImage.mNumCols > this.mImage.mNumRows)
				{
					theX = this.GetFrame() * this.mImage.GetCelWidth();
					theY = 0;
				}
				else if (this.mImage.mNumRows > this.mImage.mNumCols)
				{
					theX = 0;
					theY = this.GetFrame() * this.mImage.GetCelHeight();
				}
				g.DrawImageF(this.mImage, pX + this.mXOff, pY + this.mYOff, new Rect(theX, theY, this.mImage.GetCelWidth(), this.mImage.GetCelHeight()));
			}
			else
			{
				g.DrawImageCel(this.mImage, (int)(pX + this.mXOff), (int)(pY + this.mYOff), this.GetFrame());
			}
			this.END_DRAWING(g);
		}

		public virtual void DrawStretched(Graphics g, float pX, float pY, float pPct)
		{
			if (this.mImage == null || this.IsPaused() || this.IsDone())
			{
				return;
			}
			this.SETUP_DRAWING(g);
			float num = (float)this.mImage.GetCelWidth() * pPct;
			float num2 = (float)this.mImage.GetCelHeight() * pPct;
			Rect theDestRect = new Rect((int)(pX + this.mXOff), (int)(pY + this.mYOff), (int)num, (int)num2);
			g.DrawImageCel(this.mImage, theDestRect, this.GetFrame());
			this.END_DRAWING(g);
		}

		public virtual void DrawStretched(Graphics g, float pX, float pY, int pWidth, int pHeight)
		{
			if (this.mImage == null || this.IsPaused() || this.IsDone())
			{
				return;
			}
			this.SETUP_DRAWING(g);
			Rect theDestRect = new Rect((int)(pX + this.mXOff), (int)(pY + this.mYOff), pWidth, pHeight);
			g.DrawImageCel(this.mImage, theDestRect, this.GetFrame());
			this.END_DRAWING(g);
		}

		public virtual void DrawRotated(Graphics g, float pX, float pY, float pAngle, bool pSmooth, float pCenterX, float pCenterY)
		{
			if (this.mImage == null || this.IsPaused() || this.IsDone())
			{
				return;
			}
			this.SETUP_DRAWING(g);
			if (pSmooth)
			{
				int theX = 0;
				int theY = 0;
				if (this.mImage.mNumCols > this.mImage.mNumRows)
				{
					theX = this.GetFrame() * this.mImage.GetCelWidth();
					theY = 0;
				}
				else if (this.mImage.mNumRows > this.mImage.mNumCols)
				{
					theX = 0;
					theY = this.GetFrame() * this.mImage.GetCelHeight();
				}
				Rect theSrcRect = new Rect(theX, theY, this.mImage.GetCelWidth(), this.mImage.GetCelHeight());
				g.DrawImageRotatedF(this.mImage, pX + this.mXOff, pY + this.mYOff, (double)pAngle, pCenterX, pCenterY, theSrcRect);
			}
			else
			{
				Rect celRect = this.mImage.GetCelRect(this.GetFrame());
				g.DrawImageRotated(this.mImage, (int)(pX + this.mXOff), (int)(pY + this.mYOff), (double)pAngle, (int)pCenterX, (int)pCenterY, celRect);
			}
			this.END_DRAWING(g);
		}

		public virtual void DrawRandomly(bool pRandom)
		{
			this.mDrawRandomly = pRandom;
			if (this.mDrawRandomly)
			{
				this.mRandomFrames.Clear();
				for (int i = 0; i < this.mMaxFrames; i++)
				{
					this.mRandomFrames.Add(i);
				}
			}
		}

		public virtual void SetNumIterations(int aIt)
		{
			this.mNumIterations = aIt;
			this.mLoopCount = 0;
		}

		public virtual void SetFrame(int pFrame)
		{
			this.mCurrentFrame = pFrame;
		}

		public virtual void SetDelay(int pDelay)
		{
			this.mFrameDelay = pDelay;
			if (this.mFrameDelays.Count > 0)
			{
				for (int i = 0; i < this.mFrameDelays.Count; i++)
				{
					this.mFrameDelays[i] = pDelay;
				}
			}
		}

		public virtual void SetDelay(int pDelay, int pFrame)
		{
			this.mFrameDelays[pFrame] = pDelay;
			this.mFrameDelay = pDelay;
		}

		public virtual bool FrameChanged()
		{
			if (this.mFrameChanged)
			{
				this.mFrameChanged = false;
				return true;
			}
			return false;
		}

		public virtual void CopyTo(Animator rhs)
		{
			if (this == rhs)
			{
				return;
			}
			this.mImage = null;
			if (rhs.mFadeData == null)
			{
				this.mFadeData = null;
			}
			else
			{
				this.mFadeData = new FadeData(rhs.mFadeData);
			}
			this.mTimeLimit = rhs.mTimeLimit;
			this.mCurrentTime = rhs.mCurrentTime;
			this.mUpdateCnt = rhs.mUpdateCnt;
			this.mAnimForward = rhs.mAnimForward;
			this.mDone = rhs.mDone;
			this.mFrameChanged = rhs.mFrameChanged;
			this.mFrameDelay = rhs.mFrameDelay;
			this.SetMaxFrames(rhs.mMaxFrames);
			this.mPaused = rhs.mPaused;
			this.mPingPong = rhs.mPingPong;
			this.mStopWhenDone = rhs.mStopWhenDone;
			if (rhs.mImage != null)
			{
				this.SetImage(rhs.mImage);
			}
			this.mNumIterations = rhs.mNumIterations;
			this.mLoopCount = rhs.mLoopCount;
			this.mLoopStart = rhs.mLoopStart;
			this.mLoopEnd = rhs.mLoopEnd;
			this.mLoopDir = rhs.mLoopDir;
			this.mStepAmt = rhs.mStepAmt;
			this.mLoopSubsection = rhs.mLoopSubsection;
			this.mXOff = rhs.mXOff;
			this.mYOff = rhs.mYOff;
			this.mPriority = rhs.mPriority;
			this.mId = rhs.mId;
			this.mDrawAdditive = rhs.mDrawAdditive;
			this.mAdditiveColor = rhs.mAdditiveColor;
		}

		public bool IsDone()
		{
			return this.mDone || (this.mNumIterations > 0 && this.mLoopCount >= this.mNumIterations);
		}

		public bool IsPaused()
		{
			return this.mPaused;
		}

		public bool PingPongs()
		{
			return this.mPingPong;
		}

		public bool IsPlaying()
		{
			return !this.IsDone() && !this.IsPaused();
		}

		public bool StopWhenDone()
		{
			return this.mStopWhenDone;
		}

		public int GetFrame()
		{
			if (this.mMaxFrames != 1)
			{
				return this.mCurrentFrame;
			}
			return 0;
		}

		public int GetMaxFrames()
		{
			return this.mMaxFrames;
		}

		public int GetDelay()
		{
			return this.mFrameDelay;
		}

		public int GetStepAmt()
		{
			return this.mStepAmt;
		}

		public int GetId()
		{
			return this.mId;
		}

		public int GetPriority()
		{
			return this.mPriority;
		}

		public int GetTimeLimit()
		{
			return this.mTimeLimit;
		}

		public int GetLoopStart()
		{
			return this.mLoopStart;
		}

		public int GetLoopEnd()
		{
			return this.mLoopEnd;
		}

		public float GetXOff()
		{
			return this.mXOff;
		}

		public float GetYOff()
		{
			return this.mYOff;
		}

		public Image GetImage()
		{
			return this.mImage;
		}

		public void SetPingPong(bool pPong)
		{
			this.mPingPong = pPong;
		}

		public void StopWhenDone(bool pStop)
		{
			this.mStopWhenDone = pStop;
		}

		public void SetStepAmount(int pStep)
		{
			this.mStepAmt = pStep;
		}

		public void SetXOffset(float x)
		{
			this.mXOff = x;
		}

		public void SetYOffset(float y)
		{
			this.mYOff = y;
		}

		public void SetXYOffset(float x, float y)
		{
			this.SetXOffset(x);
			this.SetYOffset(y);
		}

		public void SetId(int id)
		{
			this.mId = id;
		}

		public void SetPriority(int p)
		{
			this.mPriority = p;
		}

		public void SetDone()
		{
			this.mDone = true;
		}

		public void ResetTime()
		{
			this.mCurrentTime = 0;
		}

		public FadeData GetFadeData()
		{
			return this.mFadeData;
		}

		public int GetFadeOutRate()
		{
			return this.mFadeData.mFadeOutRate;
		}

		public int GetFadeOutTarget()
		{
			return this.mFadeData.mFadeOutTarget;
		}

		public int GetFadeInRate()
		{
			return this.mFadeData.mFadeInRate;
		}

		public int GetFadeInTarget()
		{
			return this.mFadeData.mFadeInTarget;
		}

		public int GetFadeVal()
		{
			return this.mFadeData.mVal;
		}

		public int GetFadeCount()
		{
			return this.mFadeData.mFadeCount;
		}

		public bool GetFadeStopWhenDone()
		{
			return this.mFadeData.mStopWhenDone;
		}

		public void SetFadeData(FadeData fd)
		{
			this.mFadeData = fd;
		}

		public void SetFadeOutRate(int r)
		{
			this.mFadeData.mFadeOutRate = r;
		}

		public void SetFadeOutTarget(int t)
		{
			this.mFadeData.mFadeOutTarget = t;
		}

		public void SetFadeInRate(int r)
		{
			this.mFadeData.mFadeInRate = r;
		}

		public void SetFadeInTarget(int t)
		{
			this.mFadeData.mFadeInTarget = t;
		}

		public void SetFadeVal(int v)
		{
			this.mFadeData.mVal = v;
		}

		public void SetFadeCount(int c)
		{
			this.mFadeData.mFadeCount = c;
		}

		public void SetFadeStopWhenDone(bool d)
		{
			this.mFadeData.mStopWhenDone = d;
		}

		public void FadeIn()
		{
			this.mFadeData.mFadeState = 2;
		}

		public void FadeIn(int rate, int target)
		{
			this.FadeIn();
			this.SetFadeInRate(rate);
			this.SetFadeInTarget(target);
		}

		public void FadeOut()
		{
			this.mFadeData.mFadeState = 1;
		}

		public void FadeOut(int rate, int target)
		{
			this.FadeOut();
			this.SetFadeOutRate(rate);
			this.SetFadeOutTarget(target);
		}

		public void StopFading()
		{
			this.mFadeData.mFadeState = 0;
		}

		protected int mCurrentFrame;

		protected int mMaxFrames;

		protected int mFrameDelay;

		protected int mNumIterations;

		protected int mLoopCount;

		protected int mLoopStart;

		protected int mLoopEnd;

		protected int mLoopDir;

		protected int mStepAmt;

		protected int mId;

		protected int mPriority;

		protected int mTimeLimit;

		protected int mCurrentTime;

		protected float mXOff;

		protected float mYOff;

		protected bool mPaused;

		protected bool mPingPong;

		protected bool mAnimForward;

		protected bool mStopWhenDone;

		protected bool mDone;

		protected bool mFrameChanged;

		protected bool mLoopSubsection;

		protected bool mDrawAdditive;

		protected bool mDrawColorized;

		protected bool mDrawRandomly;

		protected SexyColor mAdditiveColor;

		protected SexyColor mColorizeVal;

		protected Image mImage;

		protected FadeData mFadeData = new FadeData();

		protected List<int> mFrameDelays = new List<int>();

		protected List<int> mRandomFrames = new List<int>();

		public int mUpdateCnt;

		public bool mCanRotate;

		public bool mResetOnStart;
	}
}
