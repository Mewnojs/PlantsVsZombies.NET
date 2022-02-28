using System;
using Sexy.Drivers;
using Sexy.GraphicsLib;
using Sexy.Misc;

namespace Sexy.WidgetsLib
{
	public class Slider : Widget
	{
		public Slider(Image theTrackImage, Image theThumbImage, int theId, SliderListener theListener)
		{
			this.mTrackImage = theTrackImage;
			this.mThumbImage = theThumbImage;
			this.mId = theId;
			this.mListener = theListener;
			this.mVal = 0.0;
			this.mOutlineColor = new SexyColor(SexyColor.White);
			this.mBkgColor = new SexyColor(80, 80, 80);
			this.mSliderColor = new SexyColor(SexyColor.White);
			this.mKnobSize = 5;
			this.mDragging = false;
			this.mHorizontal = true;
			this.mRelX = (this.mRelY = 0);
			this.mSlideSpeed = 0.01f;
			this.mSlidingLeft = false;
			this.mSlidingRight = false;
			this.mStepSound = -1;
			this.mStepMode = false;
			this.mNumSteps = 1;
			this.mCurStep = 0;
		}

		public virtual void SetValue(double theValue)
		{
			double num = this.mVal;
			this.mVal = theValue;
			if (this.mVal < 0.0)
			{
				this.mVal = 0.0;
			}
			else if (this.mVal > 1.0)
			{
				this.mVal = 1.0;
			}
			if (this.mVal != num)
			{
				this.mListener.SliderVal(this.mId, this.mVal);
			}
			this.MarkDirtyFull();
		}

		public virtual void SetStepMode(int num_steps, int cur_step, int step_sound)
		{
			this.mStepMode = true;
			this.mNumSteps = num_steps;
			this.SetStepValue(cur_step);
			this.mStepSound = step_sound;
		}

		public virtual void SetStepValue(int cur_step)
		{
			if (cur_step < 0)
			{
				cur_step = 0;
			}
			if (cur_step > this.mNumSteps)
			{
				cur_step = this.mNumSteps;
			}
			if (this.mCurStep != cur_step)
			{
				this.mCurStep = cur_step;
				this.SetValue((double)cur_step / (double)this.mNumSteps);
				if (this.mStepSound != -1)
				{
					GlobalMembers.gSexyApp.PlaySample(this.mStepSound);
				}
			}
		}

		public override void Update()
		{
			base.Update();
			if (this.mIsGamepadSelection)
			{
				if (this.mSlidingLeft)
				{
					this.SetValue(this.mVal - (double)this.mSlideSpeed);
				}
				if (this.mSlidingRight)
				{
					this.SetValue(this.mVal + (double)this.mSlideSpeed);
					return;
				}
			}
			else
			{
				this.mSlidingLeft = false;
				this.mSlidingRight = false;
			}
		}

		public virtual bool HasTransparencies()
		{
			return true;
		}

		public override void Draw(Graphics g)
		{
			if (this.mTrackImage != null)
			{
				int num = (this.mHorizontal ? (this.mTrackImage.GetWidth() / 3) : this.mTrackImage.GetWidth());
				int num2 = (this.mHorizontal ? this.mTrackImage.GetHeight() : (this.mTrackImage.GetHeight() / 3));
				Rect theSrcRect = new Rect(0, 0, num, num2);
				if (this.mHorizontal)
				{
					int theY = (this.mHeight - num2) / 2;
					g.DrawImage(this.mTrackImage, 0, theY, theSrcRect);
					g.PushState();
					g.ClipRect(num, theY, this.mWidth - num * 2, num2);
					for (int i = 0; i < (this.mWidth - num * 2 + num - 1) / num; i++)
					{
						g.DrawImage(this.mTrackImage, num + i * num, theY, new Rect(num, 0, num, num2));
					}
					g.PopState();
					g.DrawImage(this.mTrackImage, this.mWidth - num, theY, new Rect(num * 2, 0, num, num2));
				}
				else
				{
					int theX = (this.mWidth - num) / 2;
					g.DrawImage(this.mTrackImage, theX, 0, theSrcRect);
					g.PushState();
					g.ClipRect(theX, num2, num, this.mHeight - num2 * 2);
					for (int j = 0; j < (this.mHeight - num2 * 2 + num2 - 1) / num2; j++)
					{
						g.DrawImage(this.mTrackImage, theX, num2 + j * num2, theSrcRect);
					}
					g.PopState();
					g.DrawImage(this.mTrackImage, theX, this.mHeight - num2, theSrcRect);
				}
			}
			else if (this.mTrackImage == null)
			{
				g.SetColor(this.mOutlineColor);
				g.FillRect(0, 0, this.mWidth, this.mHeight);
				g.SetColor(this.mBkgColor);
				g.FillRect(1, 1, this.mWidth - 2, this.mHeight - 2);
			}
			if (this.mHorizontal && this.mThumbImage != null)
			{
				g.DrawImage(this.mThumbImage, (int)(this.mVal * (double)(this.mWidth - this.mThumbImage.GetCelWidth())), (this.mHeight - this.mThumbImage.GetCelHeight()) / 2);
				return;
			}
			if (!this.mHorizontal && this.mThumbImage != null)
			{
				g.DrawImage(this.mThumbImage, (this.mWidth - this.mThumbImage.GetCelWidth()) / 2, (int)(this.mVal * (double)(this.mHeight - this.mThumbImage.GetCelHeight())));
				return;
			}
			if (this.mThumbImage == null)
			{
				g.SetColor(this.mSliderColor);
				if (this.mHorizontal)
				{
					g.FillRect((int)(this.mVal * (double)(this.mWidth - this.mKnobSize)), 0, this.mKnobSize, this.mHeight);
					return;
				}
				g.FillRect(0, (int)(this.mVal * (double)(this.mHeight - this.mKnobSize)), this.mWidth, this.mKnobSize);
			}
		}

		public override Rect GetInsetRect()
		{
			return new Rect(this.mX + this.mMouseInsets.mLeft - 6, this.mY + this.mMouseInsets.mTop - 7, this.mWidth - this.mMouseInsets.mLeft - this.mMouseInsets.mRight + 12, this.mHeight - this.mMouseInsets.mTop - this.mMouseInsets.mBottom + 14);
		}

		public override void MouseMove(int x, int y)
		{
			if (this.mHorizontal)
			{
				int num = ((this.mThumbImage == null) ? this.mKnobSize : this.mThumbImage.GetCelWidth());
				int num2 = (int)(this.mVal * (double)(this.mWidth - num));
				if (x >= num2 && x < num2 + num)
				{
					this.mWidgetManager.mApp.SetCursor(ECURSOR.CURSOR_DRAGGING);
					return;
				}
				this.mWidgetManager.mApp.SetCursor(ECURSOR.CURSOR_POINTER);
				return;
			}
			else
			{
				int num3 = ((this.mThumbImage == null) ? this.mKnobSize : this.mThumbImage.GetCelHeight());
				int num4 = (int)(this.mVal * (double)(this.mHeight - num3));
				if (y >= num4 && y < num4 + num3)
				{
					this.mWidgetManager.mApp.SetCursor(ECURSOR.CURSOR_DRAGGING);
					return;
				}
				this.mWidgetManager.mApp.SetCursor(ECURSOR.CURSOR_POINTER);
				return;
			}
		}

		public override void MouseDown(int x, int y, int theClickCount)
		{
			if (this.mHorizontal)
			{
				int num = ((this.mThumbImage == null) ? this.mKnobSize : this.mThumbImage.GetCelWidth());
				int num2 = (int)(this.mVal * (double)(this.mWidth - num));
				this.mDragging = true;
				if (x >= num2 - 2 && x < num2 + num + 2)
				{
					this.mWidgetManager.mApp.SetCursor(ECURSOR.CURSOR_DRAGGING);
					this.mRelX = x - num2;
					return;
				}
				double value = (double)x / (double)this.mWidth;
				this.SetValue(value);
				return;
			}
			else
			{
				int num3 = ((this.mThumbImage == null) ? this.mKnobSize : this.mThumbImage.GetCelHeight());
				int num4 = (int)(this.mVal * (double)(this.mHeight - num3));
				if (y >= num4 && y < num4 + num3)
				{
					this.mWidgetManager.mApp.SetCursor(ECURSOR.CURSOR_DRAGGING);
					this.mDragging = true;
					this.mRelY = y - num4;
					return;
				}
				double value2 = (double)y / (double)this.mHeight;
				this.SetValue(value2);
				return;
			}
		}

		public override void MouseDrag(int x, int y)
		{
			if (this.mDragging)
			{
				int num = ((this.mThumbImage == null) ? this.mKnobSize : this.mThumbImage.GetCelWidth());
				int mWidth = this.mWidth;
				double num2 = this.mVal;
				if (this.mHorizontal)
				{
					this.mVal = (double)(x - this.mRelX) / (double)(this.mWidth - num);
				}
				else
				{
					int num3 = ((this.mThumbImage == null) ? this.mKnobSize : this.mThumbImage.GetCelHeight());
					this.mVal = (double)(y - this.mRelY) / (double)(this.mHeight - num3);
				}
				if (this.mVal < 0.0)
				{
					this.mVal = 0.0;
				}
				if (this.mVal > 1.0)
				{
					this.mVal = 1.0;
				}
				if (this.mVal != num2)
				{
					this.mListener.SliderVal(this.mId, this.mVal);
					this.MarkDirtyFull();
				}
			}
		}

		public override void MouseUp(int x, int y)
		{
			this.mDragging = false;
			this.mWidgetManager.mApp.SetCursor(ECURSOR.CURSOR_POINTER);
			this.mListener.SliderVal(this.mId, this.mVal);
			this.mListener.SliderReleased(this.mId, this.mVal);
		}

		public override void MouseLeave()
		{
			if (!this.mDragging)
			{
				this.mWidgetManager.mApp.SetCursor(ECURSOR.CURSOR_POINTER);
			}
		}

		public override void GamepadButtonDown(GamepadButton theButton, int player, uint flags)
		{
			switch (theButton)
			{
			case GamepadButton.GAMEPAD_BUTTON_LEFT:
				if (this.mStepMode)
				{
					this.SetStepValue(this.mCurStep - 1);
					return;
				}
				this.mSlidingLeft = true;
				return;
			case GamepadButton.GAMEPAD_BUTTON_RIGHT:
				if (this.mStepMode)
				{
					this.SetStepValue(this.mCurStep + 1);
					return;
				}
				this.mSlidingRight = true;
				return;
			default:
				base.GamepadButtonDown(theButton, player, flags);
				return;
			}
		}

		public override void GamepadButtonUp(GamepadButton theButton, int player, uint flags)
		{
			switch (theButton)
			{
			case GamepadButton.GAMEPAD_BUTTON_LEFT:
				this.mSlidingLeft = false;
				return;
			case GamepadButton.GAMEPAD_BUTTON_RIGHT:
				this.mSlidingRight = false;
				return;
			default:
				base.GamepadButtonUp(theButton, player, flags);
				return;
			}
		}

		public SliderListener mListener;

		public double mVal;

		public int mId;

		public Image mTrackImage;

		public Image mThumbImage;

		public bool mDragging;

		public int mRelX;

		public int mRelY;

		public bool mHorizontal;

		public float mSlideSpeed;

		public bool mSlidingLeft;

		public bool mSlidingRight;

		public bool mStepMode;

		public int mNumSteps;

		public int mCurStep;

		public int mStepSound;

		public int mKnobSize;

		public SexyColor mOutlineColor = default(SexyColor);

		public SexyColor mBkgColor = default(SexyColor);

		public SexyColor mSliderColor = default(SexyColor);
	}
}
