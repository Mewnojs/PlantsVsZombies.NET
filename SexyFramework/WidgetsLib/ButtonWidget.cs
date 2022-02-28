using System;
using Sexy.Drivers;
using Sexy.GraphicsLib;
using Sexy.Misc;

namespace Sexy.WidgetsLib
{
	public class ButtonWidget : Widget
	{
		public ButtonWidget(int theId, ButtonListener theButtonListener)
		{
			this.mId = theId;
			this.mFont = null;
			this.mLabelJustify = 0;
			this.mButtonImage = null;
			this.mIconImage = null;
			this.mOverImage = null;
			this.mDownImage = null;
			this.mDisabledImage = null;
			this.mInverted = false;
			this.mBtnNoDraw = false;
			this.mFrameNoDraw = false;
			this.mButtonListener = theButtonListener;
			this.mHasAlpha = true;
			this.mOverAlpha = 0.0;
			this.mOverAlphaSpeed = 0.0;
			this.mOverAlphaFadeInSpeed = 0.0;
			this.mLabelOffsetX = (this.mLabelOffsetY = 0);
			this.SetColors3(GlobalMembers.gButtonWidgetColors, 6);
			this.mLastPressedBy = -1;
		}

		public override void Dispose()
		{
			if (this.mFont != null)
			{
				this.mFont.Dispose();
			}
			base.Dispose();
		}

		public virtual void SetFont(Font theFont)
		{
			if (this.mFont != null)
			{
				this.mFont.Dispose();
			}
			this.mFont = theFont.Duplicate();
		}

		public virtual bool IsButtonDown()
		{
			return this.mIsDown && this.mIsOver && !this.mDisabled;
		}

		public override void Draw(Graphics g)
		{
			if (this.mBtnNoDraw)
			{
				return;
			}
			bool flag = this.IsButtonDown();
			flag ^= this.mInverted;
			int num = this.mLabelOffsetX;
			int num2 = this.mLabelOffsetY;
			if (this.mFont != null)
			{
				if (this.mLabelJustify == 0)
				{
					num += (this.mWidth - this.mFont.StringWidth(this.mLabel)) / 2;
				}
				else if (this.mLabelJustify == 1)
				{
					num += this.mWidth - this.mFont.StringWidth(this.mLabel);
				}
				num2 += (this.mHeight + this.mFont.GetAscent() - this.mFont.GetAscent() / 6 - 1) / 2;
			}
			int theX = 0;
			int theY = 0;
			if (this.mIconImage != null)
			{
				if (this.mLabelJustify == 0)
				{
					theX = (this.mWidth - this.mIconImage.GetWidth()) / 2 + this.mLabelOffsetX;
				}
				else if (this.mLabelJustify == 1)
				{
					theX = this.mWidth - this.mIconImage.GetWidth();
				}
				theY = (this.mHeight - this.mIconImage.GetHeight()) / 2 + this.mLabelOffsetY;
			}
			g.SetFont(this.mFont);
			if (this.mButtonImage == null && this.mDownImage == null)
			{
				if (!this.mFrameNoDraw)
				{
					g.SetColor(this.mColors[5]);
					g.FillRect(0, 0, this.mWidth, this.mHeight);
				}
				if (flag)
				{
					if (!this.mFrameNoDraw)
					{
						g.SetColor(this.mColors[2]);
						g.FillRect(0, 0, this.mWidth - 1, 1);
						g.FillRect(0, 0, 1, this.mHeight - 1);
						g.SetColor(this.mColors[3]);
						g.FillRect(0, this.mHeight - 1, this.mWidth, 1);
						g.FillRect(this.mWidth - 1, 0, 1, this.mHeight);
						g.SetColor(this.mColors[4]);
						g.FillRect(1, 1, this.mWidth - 3, 1);
						g.FillRect(1, 1, 1, this.mHeight - 3);
					}
					if (this.mIsOver)
					{
						g.SetColor(this.mColors[1]);
					}
					else
					{
						g.SetColor(this.mColors[0]);
					}
					if (this.mIconImage == null)
					{
						g.DrawString(this.mLabel, num, num2);
						return;
					}
					g.DrawImage(this.mIconImage, theX, theY);
					return;
				}
				else
				{
					if (!this.mFrameNoDraw)
					{
						g.SetColor(this.mColors[3]);
						g.FillRect(0, 0, this.mWidth - 1, 1);
						g.FillRect(0, 0, 1, this.mHeight - 1);
						g.SetColor(this.mColors[2]);
						g.FillRect(0, this.mHeight - 1, this.mWidth, 1);
						g.FillRect(this.mWidth - 1, 0, 1, this.mHeight);
						g.SetColor(this.mColors[4]);
						g.FillRect(1, this.mHeight - 2, this.mWidth - 2, 1);
						g.FillRect(this.mWidth - 2, 1, 1, this.mHeight - 2);
					}
					if (this.mIsOver)
					{
						g.SetColor(this.mColors[1]);
					}
					else
					{
						g.SetColor(this.mColors[0]);
					}
					if (this.mIconImage == null)
					{
						g.DrawString(this.mLabel, num, num2);
						return;
					}
					g.DrawImage(this.mIconImage, theX, theY);
					return;
				}
			}
			else if (!flag)
			{
				if (this.mDisabled && this.HaveButtonImage(this.mDisabledImage, this.mDisabledRect))
				{
					this.DrawButtonImage(g, this.mDisabledImage, this.mDisabledRect, 0, 0);
				}
				else if (this.mOverAlpha > 0.0 && this.HaveButtonImage(this.mOverImage, this.mOverRect))
				{
					if (this.HaveButtonImage(this.mButtonImage, this.mNormalRect) && this.mOverAlpha < 1.0)
					{
						this.DrawButtonImage(g, this.mButtonImage, this.mNormalRect, 0, 0);
					}
					g.SetColorizeImages(true);
					g.SetColor(new SexyColor(255, 255, 255, (int)(this.mOverAlpha * 255.0)));
					this.DrawButtonImage(g, this.mOverImage, this.mOverRect, 0, 0);
					g.SetColorizeImages(false);
				}
				else if ((this.mIsOver || this.mIsDown) && this.HaveButtonImage(this.mOverImage, this.mOverRect))
				{
					this.DrawButtonImage(g, this.mOverImage, this.mOverRect, 0, 0);
				}
				else if (this.HaveButtonImage(this.mButtonImage, this.mNormalRect))
				{
					this.DrawButtonImage(g, this.mButtonImage, this.mNormalRect, 0, 0);
				}
				if (this.mIsOver)
				{
					g.SetColor(this.mColors[1]);
				}
				else
				{
					g.SetColor(this.mColors[0]);
				}
				if (this.mIconImage == null)
				{
					g.DrawString(this.mLabel, num, num2);
					return;
				}
				g.DrawImage(this.mIconImage, theX, theY);
				return;
			}
			else
			{
				if (this.HaveButtonImage(this.mDownImage, this.mDownRect))
				{
					this.DrawButtonImage(g, this.mDownImage, this.mDownRect, 0, 0);
				}
				else if (this.HaveButtonImage(this.mOverImage, this.mOverRect))
				{
					this.DrawButtonImage(g, this.mOverImage, this.mOverRect, 0, 0);
				}
				else
				{
					this.DrawButtonImage(g, this.mButtonImage, this.mNormalRect, 0, 0);
				}
				g.SetColor(this.mColors[1]);
				if (this.mIconImage == null)
				{
					g.DrawString(this.mLabel, num, num2);
					return;
				}
				g.DrawImage(this.mIconImage, theX, theY);
				return;
			}
		}

		public override void SetDisabled(bool isDisabled)
		{
			base.SetDisabled(isDisabled);
			if (this.HaveButtonImage(this.mDisabledImage, this.mDisabledRect))
			{
				this.MarkDirty();
			}
		}

		public override void MouseEnter()
		{
			base.MouseEnter();
			if (this.mOverAlphaFadeInSpeed == 0.0 && this.mOverAlpha > 0.0)
			{
				this.mOverAlpha = 0.0;
			}
			if (this.mIsDown || this.HaveButtonImage(this.mOverImage, this.mOverRect) || this.mColors[1] != this.mColors[0])
			{
				this.MarkDirty();
			}
			this.MarkDirty();
			this.mButtonListener.ButtonMouseEnter(this.mId);
		}

		public override void MouseLeave()
		{
			base.MouseLeave();
			if (this.mOverAlphaSpeed == 0.0 && this.mOverAlpha > 0.0)
			{
				this.mOverAlpha = 0.0;
			}
			else if (this.mOverAlphaSpeed > 0.0 && this.mOverAlpha == 0.0 && this.mWidgetManager.mApp.mHasFocus)
			{
				this.mOverAlpha = Math.Min(1.0, this.mOverAlphaSpeed * 10.0);
			}
			if (this.mIsDown || this.HaveButtonImage(this.mOverImage, this.mOverRect) || this.mColors[1] != this.mColors[0])
			{
				this.MarkDirty();
			}
			this.mButtonListener.ButtonMouseLeave(this.mId);
		}

		public override void MouseMove(int theX, int theY)
		{
			base.MouseMove(theX, theY);
			this.mButtonListener.ButtonMouseMove(this.mId, theX, theY);
		}

		public override void MouseDown(int theX, int theY, int theClickCount)
		{
			base.MouseDown(theX, theY, theClickCount);
		}

		public override void MouseDown(int theX, int theY, int theBtnNum, int theClickCount)
		{
			base.MouseDown(theX, theY, theBtnNum, theClickCount);
			this.mButtonListener.ButtonPress(this.mId, theClickCount);
			this.MarkDirty();
		}

		public override void MouseUp(int theX, int theY)
		{
			base.MouseUp(theX, theY);
		}

		public override void MouseUp(int theX, int theY, int theClickCount)
		{
			base.MouseUp(theX, theY, theClickCount);
		}

		public override void MouseUp(int theX, int theY, int theBtnNum, int theClickCount)
		{
			base.MouseUp(theX, theY, theBtnNum, theClickCount);
			if (this.mIsOver && this.mWidgetManager.mHasFocus)
			{
				this.mButtonListener.ButtonDepress(this.mId);
			}
			this.MarkDirty();
		}

		public override void Update()
		{
			base.Update();
			if (this.mIsDown && this.mIsOver)
			{
				this.mButtonListener.ButtonDownTick(this.mId);
			}
			if (!this.mIsDown && !this.mIsOver && this.mOverAlpha > 0.0)
			{
				if (this.mOverAlphaSpeed > 0.0)
				{
					this.mOverAlpha -= this.mOverAlphaSpeed;
					if (this.mOverAlpha < 0.0)
					{
						this.mOverAlpha = 0.0;
					}
				}
				else
				{
					this.mOverAlpha = 0.0;
				}
				this.MarkDirty();
				return;
			}
			if (this.mIsOver && this.mOverAlphaFadeInSpeed > 0.0 && this.mOverAlpha < 1.0)
			{
				this.mOverAlpha += this.mOverAlphaFadeInSpeed;
				if (this.mOverAlpha > 1.0)
				{
					this.mOverAlpha = 1.0;
				}
				this.MarkDirty();
			}
		}

		public override void GotGamepadSelection(WidgetLinkDir theDirection)
		{
			base.GotGamepadSelection(theDirection);
			this.mIsOver = true;
		}

		public override void LostGamepadSelection()
		{
			base.LostGamepadSelection();
			this.mIsOver = false;
			this.mIsDown = false;
		}

		public override void GamepadButtonDown(GamepadButton theButton, int thePlayer, uint theFlags)
		{
			if (theButton != GamepadButton.GAMEPAD_BUTTON_A)
			{
				if (this.mIsDown)
				{
					if (this.mGamepadParent != null)
					{
						this.mGamepadParent.GamepadButtonDown(theButton, thePlayer, theFlags);
						return;
					}
				}
				else
				{
					base.GamepadButtonDown(theButton, thePlayer, theFlags);
				}
				return;
			}
			if ((theFlags & 1U) != 0U)
			{
				return;
			}
			this.mLastPressedBy = thePlayer;
			this.OnPressed();
			this.mIsDown = true;
			if (this.mButtonListener != null)
			{
				this.mButtonListener.ButtonPress(this.mId, 1);
			}
			this.MarkDirty();
		}

		public override void GamepadButtonUp(GamepadButton theButton, int thePlayer, uint theFlags)
		{
			if (theButton == GamepadButton.GAMEPAD_BUTTON_A)
			{
				if (this.mIsDown)
				{
					this.mLastPressedBy = thePlayer;
					if (this.mButtonListener != null)
					{
						this.mButtonListener.ButtonDepress(this.mId);
					}
					this.mIsDown = false;
					this.MarkDirty();
					return;
				}
			}
			else
			{
				base.GamepadButtonUp(theButton, thePlayer, theFlags);
			}
		}

		public virtual void OnPressed()
		{
		}

		public virtual bool HaveButtonImage(Image theImage, Rect theRect)
		{
			return theImage != null || theRect.mWidth != 0;
		}

		public virtual void DrawButtonImage(Graphics g, Image theImage, Rect theRect, int x, int y)
		{
			if (theRect.mWidth != 0)
			{
				g.DrawImage(theImage, x, y, theRect);
				return;
			}
			g.DrawImage(theImage, x, y);
		}

		public int mId;

		public string mLabel;

		public int mLabelJustify;

		public Font mFont;

		public Image mButtonImage;

		public Image mIconImage;

		public Image mOverImage;

		public Image mDownImage;

		public Image mDisabledImage;

		public Rect mNormalRect = default(Rect);

		public Rect mOverRect = default(Rect);

		public Rect mDownRect = default(Rect);

		public Rect mDisabledRect = default(Rect);

		public bool mInverted;

		public bool mBtnNoDraw;

		public bool mFrameNoDraw;

		public ButtonListener mButtonListener;

		public int mLastPressedBy;

		public double mOverAlpha;

		public double mOverAlphaSpeed;

		public double mOverAlphaFadeInSpeed;

		public int mLabelOffsetX;

		public int mLabelOffsetY;

		public enum ButtonLabel
		{
			BUTTON_LABEL_LEFT = -1,
			BUTTON_LABEL_CENTER,
			BUTTON_LABEL_RIGHT
		}

		public enum ButtonColor
		{
			COLOR_LABEL,
			COLOR_LABEL_HILITE,
			COLOR_DARK_OUTLINE,
			COLOR_LIGHT_OUTLINE,
			COLOR_MEDIUM_OUTLINE,
			COLOR_BKG,
			NUM_COLORS
		}
	}
}
