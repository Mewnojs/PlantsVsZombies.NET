using System;
using Microsoft.Xna.Framework;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
	internal class GameButton
	{
		public string mLabel
		{
			get
			{
				return this.label;
			}
			set
			{
				this.label = value;
				this.CalculateTextScale();
			}
		}

		public GameButton(int theId, Widget parent)
		{
			this.mApp = (LawnApp)GlobalStaticVars.gSexyAppBase;
			this.mId = theId;
			this.mFont = null;
			this.mLabelJustify = 0;
			this.mButtonImage = null;
			this.mOverImage = null;
			this.mOverOverlayImage = null;
			this.mDownImage = null;
			this.mDisabledImage = null;
			this.mInverted = false;
			this.mBtnNoDraw = false;
			this.mFrameNoDraw = false;
			this.mDisabled = false;
			this.mIsDown = false;
			this.mIsOver = false;
			this.mX = 0;
			this.mY = 0;
			this.mWidth = 0;
			this.mHeight = 0;
			this.mParentWidget = parent;
			this.mDrawStoneButton = false;
			this.mTextOffsetX = 0;
			this.mTextOffsetY = 0;
			this.mButtonOffsetX = 0;
			this.mButtonOffsetY = 0;
			this.mOverAlpha = 0.0;
			this.mOverAlphaSpeed = 0.0;
			this.mOverAlphaFadeInSpeed = 0.0;
			for (int i = 0; i < 6; i++)
			{
				this.mColors[i] = GameConstants.gGameButtonColors[i];
			}
			this.mFont = Resources.FONT_DWARVENTODCRAFT15;
		}

		public void Dispose()
		{
			if (this.mFont != null)
			{
				this.mFont.Dispose();
			}
		}

		public bool HaveButtonImage(Image theImage, TRect theRect)
		{
			return theImage != null || theRect.mWidth != 0;
		}

		public void DrawButtonImage(Graphics g, Image theImage, TRect theRect, int x, int y)
		{
			if (theRect.mWidth != 0)
			{
				g.DrawImage(this.mButtonImage, x + this.mButtonOffsetX, y + this.mButtonOffsetY, theRect);
				return;
			}
			g.DrawImage(theImage, x + this.mButtonOffsetX, y + this.mButtonOffsetY);
		}

		public void SetFont(Font theFont)
		{
			if (this.mFont != null)
			{
				this.mFont.Dispose();
			}
			this.mFont = theFont.Duplicate();
		}

		public bool IsButtonDown()
		{
			return this.mIsDown && this.mIsOver && !this.mDisabled && !this.mBtnNoDraw;
		}

		public void Draw(Graphics g)
		{
			if (this.mBtnNoDraw)
			{
				return;
			}
			bool flag = this.mIsDown && this.mIsOver && !this.mDisabled;
			flag ^= this.mInverted;
			bool flag2 = this.mIsOver && !this.mDisabled;
			if (this.mDrawStoneButton)
			{
				GameButton.DrawStoneButton(g, this.mX, this.mY, this.mWidth, this.mHeight, flag, flag2, this.mLabel, this.mFont, this.mFontScale);
				return;
			}
			g.mTransX += this.mX;
			g.mTransY += this.mY;
			int num = this.mTextOffsetX;
			int num2 = this.mTextOffsetY;
			if (this.mFont != null && this.mLabelJustify == 1)
			{
				num += this.mWidth - this.mFont.StringWidth(this.mLabel);
			}
			g.SetFont(this.mFont);
			if (!flag)
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
				else if (flag2 && this.HaveButtonImage(this.mOverImage, this.mOverRect))
				{
					this.DrawButtonImage(g, this.mOverImage, this.mOverRect, 0, 0);
				}
				else if (this.HaveButtonImage(this.mButtonImage, this.mNormalRect))
				{
					this.DrawButtonImage(g, this.mButtonImage, this.mNormalRect, 0, 0);
				}
				if (flag2)
				{
					g.SetColor(this.mColors[1]);
				}
				else
				{
					g.SetColor(this.mColors[0]);
				}
				g.SetScale(this.mFontScale);
				if (this.mLabelJustify == 0)
				{
					num += (this.mWidth - this.mFont.StringWidth(this.mLabel)) / 2 + 1;
				}
				num2 += (this.mHeight + this.mFont.GetAscent()) / 2;
				g.DrawString(this.mLabel, num, num2);
				g.SetScale(1f);
				if (flag2 && this.mOverOverlayImage != null)
				{
					g.SetDrawMode(Graphics.DrawMode.DRAWMODE_ADDITIVE);
					this.DrawButtonImage(g, this.mOverOverlayImage, this.mNormalRect, 0, 0);
					g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
				}
			}
			else
			{
				if (this.HaveButtonImage(this.mDownImage, this.mDownRect))
				{
					this.DrawButtonImage(g, this.mDownImage, this.mDownRect, 0, 0);
				}
				else if (this.HaveButtonImage(this.mOverImage, this.mOverRect))
				{
					this.DrawButtonImage(g, this.mOverImage, this.mOverRect, 1, 1);
				}
				else
				{
					this.DrawButtonImage(g, this.mButtonImage, this.mNormalRect, 1, 1);
				}
				g.SetColor(this.mColors[1]);
				g.SetScale(this.mFontScale);
				if (this.mLabelJustify == 0)
				{
					num += (this.mWidth - this.mFont.StringWidth(this.mLabel)) / 2 + 1;
				}
				num2 += (this.mHeight + this.mFont.GetAscent()) / 2;
				g.DrawString(this.mLabel, num + this.mTextPushOffsetX, num2 + this.mTextPushOffsetY);
				g.SetScale(1f);
				if (flag2 && this.mOverOverlayImage != null)
				{
					g.SetDrawMode(Graphics.DrawMode.DRAWMODE_ADDITIVE);
					this.DrawButtonImage(g, this.mOverOverlayImage, this.mNormalRect, 0, 0);
					g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
				}
			}
			g.mTransX -= this.mX;
			g.mTransY -= this.mY;
		}

		public void SetDisabled(bool isDisabled)
		{
			this.mDisabled = isDisabled;
		}

		public bool IsMouseOver()
		{
			return this.mIsOver && !this.mDisabled && !this.mBtnNoDraw;
		}

		public void Update()
		{
			int num = this.mApp.mWidgetManager.mLastMouseX;
			int num2 = this.mApp.mWidgetManager.mLastMouseY;
			if (this.mParentWidget != null)
			{
				CGPoint absPos = this.mParentWidget.GetAbsPos();
				num -= (int)absPos.mX;
				num2 -= (int)absPos.mY;
			}
			if (num >= this.mX && num < this.mX + this.mWidth && num2 >= this.mY && num2 < this.mY + this.mHeight)
			{
				this.mIsOver = true;
			}
			else
			{
				this.mIsOver = false;
			}
			if ((this.mApp.mWidgetManager.mDownButtons & 5) != 0)
			{
				this.mIsDown = true;
			}
			else
			{
				this.mIsDown = false;
			}
			if ((this.mApp.mWidgetManager.mFocusWidget == null || this.mApp.mWidgetManager.mFocusWidget != this.mParentWidget) && this.mApp.GetDialogCount() > 0)
			{
				this.mIsDown = false;
				this.mIsOver = false;
			}
			if (!this.mIsDown && !this.mIsOver && this.mOverAlpha > 0.0)
			{
				if (this.mOverAlphaSpeed <= 0.0)
				{
					this.mOverAlpha = 0.0;
					return;
				}
				this.mOverAlpha -= this.mOverAlphaSpeed;
				if (this.mOverAlpha < 0.0)
				{
					this.mOverAlpha = 0.0;
					return;
				}
			}
			else if (this.mIsOver && this.mOverAlphaFadeInSpeed > 0.0 && this.mOverAlpha < 1.0)
			{
				if (this.mOverAlphaFadeInSpeed > 0.0)
				{
					this.mOverAlpha += this.mOverAlphaFadeInSpeed;
					if (this.mOverAlpha > 1.0)
					{
						this.mOverAlpha = 1.0;
						return;
					}
				}
				else
				{
					this.mOverAlpha = 1.0;
				}
			}
		}

		public void Resize(int theX, int theY, int theWidth, int theHeight)
		{
			this.mX = theX;
			this.mY = theY;
			this.mWidth = theWidth;
			this.mHeight = theHeight;
			this.CalculateTextScale();
		}

		private void CalculateTextScale()
		{
			this.mFontScale = 1f;
			if (this.mFont != null)
			{
				Vector2 vector = this.mFont.MeasureString(this.mLabel);
				this.mTextHeight = vector.Y;
				if (this.mWidth > 0)
				{
					int num = (int)((float)this.mWidth - Constants.S * 30f);
					if (vector.X > (float)num)
					{
						this.mFontScale = (float)num / vector.X;
					}
				}
			}
		}

		public void SetLabel(string theLabel)
		{
			this.mLabel = TodStringFile.TodStringTranslate(theLabel);
			this.CalculateTextScale();
		}

		public static NewLawnButton MakeNewButton(int theId, ButtonListener theListener, string theText, Font theFont, Image theImageNormal, Image theImageOver, Image theImageDown)
		{
			NewLawnButton newLawnButton = new NewLawnButton(null, theId, theListener);
			newLawnButton.SetFont((theFont != null) ? theFont : Resources.FONT_BRIANNETOD12);
			newLawnButton.SetLabel(theText);
			if (theImageNormal != null)
			{
				newLawnButton.mWidth = theImageNormal.mWidth;
				newLawnButton.mHeight = theImageNormal.mHeight;
			}
			else
			{
				newLawnButton.mWidth = theImageDown.mWidth;
				newLawnButton.mHeight = theImageDown.mHeight;
			}
			newLawnButton.mButtonImage = theImageNormal;
			newLawnButton.mOverImage = theImageOver;
			newLawnButton.mDownImage = theImageDown;
			newLawnButton.mHasAlpha = true;
			newLawnButton.mHasTransparencies = true;
			if (theImageDown == theImageOver)
			{
				newLawnButton.mTranslateX = 1;
				newLawnButton.mTranslateY = 1;
			}
			else
			{
				newLawnButton.mTranslateX = 1;
				newLawnButton.mTranslateY = 1;
			}
			return newLawnButton;
		}

		public static void DrawStoneButton(Graphics g, int x, int y, int theWidth, int theHeight, bool isDown, bool isHighLighted, string theLabel)
		{
			GameButton.DrawStoneButton(g, x, y, theWidth, theHeight, isDown, isHighLighted, theLabel, Resources.FONT_DWARVENTODCRAFT15, 1f);
		}

		public static void DrawStoneButton(Graphics g, int x, int y, int theWidth, int theHeight, bool isDown, bool isHighLighted, string theLabel, Font theFont, float fontScale)
		{
			int num = x;
			int num2 = y;
			int num3 = x;
			Image image = AtlasResources.IMAGE_BUTTON_LEFT;
			Image theImage = AtlasResources.IMAGE_BUTTON_MIDDLE;
			Image image2 = AtlasResources.IMAGE_BUTTON_RIGHT;
			if (isDown)
			{
				image = AtlasResources.IMAGE_BUTTON_DOWN_LEFT;
				theImage = AtlasResources.IMAGE_BUTTON_DOWN_MIDDLE;
				image2 = AtlasResources.IMAGE_BUTTON_DOWN_RIGHT;
				num++;
				num2++;
				num3++;
			}
			int num4 = theWidth - image.mWidth - image2.mWidth;
			g.DrawImage(image, num3, y);
			num3 += image.mWidth;
			LawnCommon.TileImageHorizontally(g, theImage, num3, y, num4);
			num3 += num4;
			g.DrawImage(image2, num3, y);
			g.SetFont(theFont);
			g.SetScale(fontScale);
			g.SetColor(new SexyColor(21, 175, 0));
			num += (theWidth - theFont.StringWidth(theLabel)) / 2 + 1;
			num2 += (theHeight + theFont.GetAscent()) / 2;
			g.DrawString(theLabel, num, num2);
			g.SetColor(SexyColor.White);
			g.SetScale(1f);
		}

		public static LawnStoneButton MakeButton(int theId, ButtonListener theListener, string theText)
		{
			LawnStoneButton lawnStoneButton = new LawnStoneButton(null, theId, theListener);
			lawnStoneButton.SetLabel(theText);
			lawnStoneButton.mHeight = (int)Constants.InvertAndScale(33f);
			lawnStoneButton.mTranslateX = 1;
			lawnStoneButton.mTranslateY = 1;
			lawnStoneButton.mHasAlpha = true;
			lawnStoneButton.mHasTransparencies = true;
			return lawnStoneButton;
		}

		public LawnApp mApp;

		public Widget mParentWidget;

		public int mX;

		public int mY;

		public int mWidth;

		public int mHeight;

		public bool mIsOver;

		public bool mIsDown;

		public bool mDisabled;

		public SexyColor[] mColors = new SexyColor[6];

		public int mId;

		private string _label = string.Empty;

		private float mFontScale;

		private float mTextHeight;

		private Font _font;

		public int mLabelJustify;

		public Font mFont;

		public Image mButtonImage;

		public Image mOverImage;

		public Image mDownImage;

		public Image mDisabledImage;

		public Image mOverOverlayImage;

		public TRect mNormalRect = default(TRect);

		public TRect mOverRect = default(TRect);

		public TRect mDownRect = default(TRect);

		public TRect mDisabledRect = default(TRect);

		public bool mInverted;

		public bool mBtnNoDraw;

		public bool mFrameNoDraw;

		public double mOverAlpha;

		public double mOverAlphaSpeed;

		public double mOverAlphaFadeInSpeed;

		public bool mDrawStoneButton;

		public int mTextOffsetX;

		public int mTextOffsetY;

		public int mButtonOffsetX;

		public int mButtonOffsetY;

		public int mTextPushOffsetX = 1;

		public int mTextPushOffsetY = 1;

		private string label = string.Empty;

		public enum ButtonLabelJustify
		{
			BUTTON_LABEL_LEFT = -1,
			BUTTON_LABEL_CENTER,
			BUTTON_LABEL_RIGHT
		}

		public enum ButtonColours
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
