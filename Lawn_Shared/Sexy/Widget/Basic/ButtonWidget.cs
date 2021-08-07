using System;
using Microsoft.Xna.Framework;

namespace Sexy
{
	internal class ButtonWidget : Widget
	{
		public virtual string mLabel
		{
			get
			{
				return this.mLabelP;
			}
			set
			{
				this.mLabelP = value;
			}
		}

		public bool HaveButtonImage(Image theImage, TRect theRect)
		{
			return theImage != null || theRect.mWidth != 0;
		}

		public virtual void DrawButtonImage(Graphics g, Image theImage, TRect theRect, int x, int y)
		{
			if (theRect.mWidth != 0)
			{
				g.DrawImage(this.mButtonImage, x, y, new TRect(theRect));
				return;
			}
			g.DrawImage(theImage, x, y);
		}

		protected virtual void Reset(int theId, ButtonListener theButtonListener)
		{
			this.Reset();
			this.mId = theId;
			this.mFont = null;
			this.mLabelJustify = 0;
			this.mButtonImage = null;
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
			this.SetColors(GlobalMembersButtonWidget.gButtonWidgetColors, 6);
		}

		public ButtonWidget(int theId, ButtonListener theButtonListener)
		{
			this.Reset(theId, theButtonListener);
		}

		public virtual void SetFont(Font theFont)
		{
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
			bool flag = this.mIsDown && this.mIsOver && !this.mDisabled;
			flag ^= this.mInverted;
			int num = 0;
			int num2 = 0;
			int num3 = this.mTranslateWhenDown ? 1 : 0;
			if (this.mFont != null)
			{
				if (this.mLabelJustify == 0)
				{
					num = (this.mWidth - this.mFont.StringWidth(this.mLabel)) / 2;
				}
				else if (this.mLabelJustify == 1)
				{
					num = this.mWidth - this.mFont.StringWidth(this.mLabel);
				}
				num2 = (this.mHeight + this.mFont.GetAscent() - this.mFont.GetAscent() / 6 - 1) / 2;
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
					g.DrawString(this.mLabel, num + num3, num2 + num3);
					return;
				}
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
				g.DrawString(this.mLabel, num, num2);
				return;
			}
			else
			{
				if (!flag)
				{
					if (this.mDisabled && this.HaveButtonImage(this.mDisabledImage, new TRect(this.mDisabledRect)))
					{
						this.DrawButtonImage(g, this.mDisabledImage, new TRect(this.mDisabledRect), 0, 0);
					}
					else if (this.mOverAlpha > 0.0 && this.HaveButtonImage(this.mOverImage, new TRect(this.mOverRect)))
					{
						if (this.HaveButtonImage(this.mButtonImage, new TRect(this.mNormalRect)) && this.mOverAlpha < 1.0)
						{
							this.DrawButtonImage(g, this.mButtonImage, new TRect(this.mNormalRect), 0, 0);
						}
						g.SetColorizeImages(true);
						g.SetColor(new Color(255, 255, 255, (int)(this.mOverAlpha * 255.0)));
						this.DrawButtonImage(g, this.mOverImage, new TRect(this.mOverRect), 0, 0);
						g.SetColorizeImages(false);
					}
					else if ((this.mIsOver || this.mIsDown) && this.HaveButtonImage(this.mOverImage, new TRect(this.mOverRect)))
					{
						this.DrawButtonImage(g, this.mOverImage, new TRect(this.mOverRect), 0, 0);
					}
					else if (this.HaveButtonImage(this.mButtonImage, new TRect(this.mNormalRect)))
					{
						this.DrawButtonImage(g, this.mButtonImage, new TRect(this.mNormalRect), 0, 0);
					}
					if (this.mIsOver)
					{
						g.SetColor(this.mColors[1]);
					}
					else
					{
						g.SetColor(this.mColors[0]);
					}
					g.DrawString(this.mLabel, num, num2);
					return;
				}
				if (this.HaveButtonImage(this.mDownImage, new TRect(this.mDownRect)))
				{
					this.DrawButtonImage(g, this.mDownImage, new TRect(this.mDownRect), 0, 0);
				}
				else if (this.HaveButtonImage(this.mOverImage, new TRect(this.mOverRect)))
				{
					this.DrawButtonImage(g, this.mOverImage, new TRect(this.mOverRect), num3, num3);
				}
				else
				{
					this.DrawButtonImage(g, this.mButtonImage, new TRect(this.mNormalRect), num3, num3);
				}
				g.SetColor(this.mColors[1]);
				g.DrawString(this.mLabel, num + num3, num2 + num3);
				return;
			}
		}

		public override void SetDisabled(bool isDisabled)
		{
			base.SetDisabled(isDisabled);
			if (this.HaveButtonImage(this.mDisabledImage, new TRect(this.mDisabledRect)))
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
			if (this.mIsDown || this.HaveButtonImage(this.mOverImage, new TRect(this.mOverRect)) || this.mColors[1] != this.mColors[0])
			{
				this.MarkDirty();
			}
			this.mButtonListener.ButtonMouseEnter(this.mId);
		}

		public override void MouseLeave()
		{
			base.MouseLeave();
			if (this.mOverAlphaSpeed == 0.0 && this.mOverAlpha > 0.0)
			{
				this.mOverAlpha = 0.0;
			}
			else if (this.mOverAlphaSpeed > 0.0 && this.mOverAlpha == 0.0)
			{
				this.mOverAlpha = 1.0;
			}
			if (this.mIsDown || this.HaveButtonImage(this.mOverImage, new TRect(this.mOverRect)) || this.mColors[1] != this.mColors[0])
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

		public override void Dispose()
		{
			this.mFont = null;
		}

		public int mId;

		private string mLabelP = string.Empty;

		public int mLabelJustify;

		public Font mFont;

		public Image mButtonImage;

		public Image mOverImage;

		public Image mDownImage;

		public Image mDisabledImage;

		public TRect mNormalRect = default(TRect);

		public TRect mOverRect = default(TRect);

		public TRect mDownRect = default(TRect);

		public TRect mDisabledRect = default(TRect);

		public bool mInverted;

		public bool mBtnNoDraw;

		public bool mFrameNoDraw;

		public ButtonListener mButtonListener;

		public double mOverAlpha;

		public double mOverAlphaSpeed;

		public double mOverAlphaFadeInSpeed;

		public bool mTranslateWhenDown = true;

		public enum ButtonPosition
		{
			BUTTON_LABEL_LEFT = -1,
			BUTTON_LABEL_CENTER,
			BUTTON_LABEL_RIGHT
		}

		public enum ColorType
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
