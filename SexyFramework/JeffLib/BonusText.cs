using System;
using Sexy;
using Sexy.GraphicsLib;
using Sexy.Misc;

namespace JeffLib
{
	public class BonusText
	{
		public BonusText(string pText, Font pFont, float pX, float pY, float pSpeed, int pAlphaDelay)
		{
			this.mBulgePct = 1f;
			this.mBulgeAmt = (this.mBulgeDec = 0f);
			this.mBulgeDir = 0;
			this.mAlphaFadeDelay = pAlphaDelay;
			this.mUseSolidColor = false;
			this.mAlpha = 255f;
			this.mHue = 0;
			this.mLife = 250;
			this.mSpeed = pSpeed;
			this.mAlphaDecRate = 1f;
			this.mSolidColor = new SexyColor(SexyColor.White);
			this.mX = ((pX <= -1f) ? ((float)((GlobalMembers.gSexyAppBase.mWidth - pFont.StringWidth(pText)) / 2)) : pX);
			this.mY = ((pY <= -1f) ? ((float)((GlobalMembers.gSexyAppBase.mHeight - pFont.GetHeight()) / 2)) : pY);
			this.mText = pText;
			this.mFont = pFont;
			this.mImage = null;
			this.mLeftJustifyImg = true;
			this.mImageUsesSolidColor = true;
			this.mImageDrawMode = 0;
			this.mSpace = 5;
			this.mImageColor = new SexyColor(SexyColor.White);
			this.mImageCel = 0;
			this.mDone = false;
			this.mUpdateCnt = 0;
			this.mSize = 1f;
			int num = this.mFont.StringWidth(pText);
			this.mTextWidth = num;
		}

		public BonusText()
		{
			this.mSize = 1f;
			this.mBulgePct = 1f;
			this.mBulgeAmt = (this.mBulgeDec = 0f);
			this.mBulgeDir = 0;
			this.mX = (this.mY = (float)(this.mUpdateCnt = (this.mTextWidth = (this.mHue = (this.mLife = (this.mAlphaFadeDelay = (this.mImageDrawMode = 0)))))));
			this.mLeftJustifyImg = (this.mDone = (this.mUseSolidColor = (this.mImageUsesSolidColor = false)));
			this.mFont = null;
			this.mImage = null;
			this.mAlpha = (this.mAlphaDecRate = (this.mSpeed = 0f));
			this.mSpace = (this.mImageCel = 0);
		}

		public void Bulge(float pct, float rate, int count)
		{
			this.mSize = 1f;
			this.mBulgePct = pct;
			this.mBulgeAmt = rate;
			this.mBulgeDir = 1;
			this.mBulgeDec = (pct - 1f) / (float)count;
		}

		public void Update()
		{
			this.mUpdateCnt++;
			if (this.mDone)
			{
				return;
			}
			if (this.mBulgeDir != 0)
			{
				this.mSize += (float)this.mBulgeDir * this.mBulgeAmt;
				if (this.mBulgeDir > 0 && this.mSize >= this.mBulgePct)
				{
					this.mSize = this.mBulgePct;
					this.mBulgeDir = -1;
					this.mBulgePct -= this.mBulgeDec;
				}
				else if (this.mBulgeDir < 0 && this.mSize <= 1f)
				{
					this.mSize = 1f;
					this.mBulgeDir = 1;
				}
				if (Sexy.Common._eq(this.mSize, 1f) && Sexy.Common._leq(this.mBulgePct, 1f))
				{
					this.mSize = 1f;
					this.mBulgeDir = 0;
				}
			}
			this.mY -= this.mSpeed;
			if (this.mY < (float)(-(float)this.mFont.mHeight))
			{
				this.mDone = true;
			}
			if (!this.mUseSolidColor || !this.mImageUsesSolidColor)
			{
				this.mHue = (this.mHue + 7) % 255;
				if (--this.mLife <= 0)
				{
					this.mLife = 0;
					if (this.mAlpha <= 0f)
					{
						this.mDone = true;
					}
				}
			}
			if (--this.mAlphaFadeDelay <= 0)
			{
				this.mAlpha -= this.mAlphaDecRate;
				if (this.mAlpha < 0f)
				{
					this.mAlpha = 0f;
					if (this.mUseSolidColor || this.mLife <= 0)
					{
						this.mDone = true;
					}
				}
			}
		}

		public void Draw(Graphics g)
		{
			if (!this.mDone)
			{
				float num = (float)this.mLife / 18f;
				if (num > 1f)
				{
					num = 1f;
				}
				int theY = 0;
				int theX = 0;
				int num2;
				int num3;
				if (this.mImage != null)
				{
					if (this.mImage.mHeight > this.mFont.GetHeight())
					{
						num2 = (int)(this.mY + (float)((this.mImage.GetCelHeight() - this.mFont.GetHeight()) / 2) + (float)this.mFont.GetAscent());
						theY = (int)this.mY;
					}
					else
					{
						num2 = (int)(this.mY + (float)this.mFont.GetAscent());
						theY = (int)(this.mY + (float)((this.mFont.GetHeight() - this.mImage.mHeight) / 2));
					}
					num3 = (int)(this.mLeftJustifyImg ? (this.mX + (float)this.mImage.GetCelWidth() + (float)this.mSpace) : this.mX);
					theX = (int)(this.mLeftJustifyImg ? this.mX : (this.mX + (float)this.mTextWidth + (float)this.mSpace));
				}
				else
				{
					num2 = (int)(this.mY + (float)this.mFont.GetAscent());
					num3 = (int)this.mX;
				}
				Graphics3D graphics3D = g.Get3D();
				if (!Sexy.Common._eq(this.mSize, 1f) && graphics3D != null)
				{
					int num4 = 0;
					if (this.mWidth == 0 || this.mHeight == 0)
					{
						this.mWidth = g.GetFont().StringWidth(this.mText);
						this.mHeight = g.GetWordWrappedHeight(1000000, this.mText, -1, ref num4, ref num4);
					}
					SexyTransform2D sexyTransform2D = new SexyTransform2D(false);
					sexyTransform2D.Translate((float)(-(float)num3 - this.mWidth / 2 + GlobalMembers.gSexyAppBase.mScreenBounds.mX), (float)(-(float)num2 - this.mHeight / 2));
					sexyTransform2D.Scale(this.mSize, this.mSize);
					sexyTransform2D.Translate((float)(num3 + this.mWidth / 2 - GlobalMembers.gSexyAppBase.mScreenBounds.mX), (float)(num2 + this.mHeight / 2));
					graphics3D.PushTransform(sexyTransform2D);
				}
				g.SetFont(this.mFont);
				SexyColor color = ((!this.mUseSolidColor) ? new SexyColor((int)((GlobalMembers.gSexyAppBase.HSLToRGB(this.mHue, 255, 128) & 16777215UL) | (ulong)((ulong)((uint)(num * 255f)) << 24))) : new SexyColor(this.mSolidColor));
				color.mAlpha = (int)this.mAlpha;
				g.SetColor(color);
				g.DrawString(this.mText, num3, num2);
				if (this.mImage != null)
				{
					g.PushState();
					g.SetDrawMode(this.mImageDrawMode);
					if (!this.mImageUsesSolidColor)
					{
						g.SetColorizeImages(true);
						g.SetColor(color);
					}
					else if (this.mImageColor != SexyColor.White)
					{
						g.SetColorizeImages(true);
						g.SetColor(new SexyColor(this.mImageColor)
						{
							mAlpha = (int)num
						});
					}
					g.DrawImageCel(this.mImage, theX, theY, this.mImageCel);
					g.PopState();
				}
				if (!Sexy.Common._eq(this.mSize, 1f) && graphics3D != null)
				{
					graphics3D.PopTransform();
				}
			}
		}

		public void AddImage(Image img, bool solid_color, bool left_justify, int img_draw_mode)
		{
			this.mImage = img;
			this.mImageUsesSolidColor = solid_color;
			this.mLeftJustifyImg = left_justify;
			this.mImageDrawMode = img_draw_mode;
		}

		public void AddImage(Image img, bool solid_color, bool left_justify)
		{
			this.AddImage(img, solid_color, left_justify, 0);
		}

		public bool IsDone()
		{
			return this.mDone;
		}

		public void SetAlpha(int a)
		{
			this.mAlpha = (float)a;
		}

		public void SetAlphaDelay(int d)
		{
			this.mAlphaFadeDelay = d;
		}

		public void SetAlphaDecRate(float d)
		{
			this.mAlphaDecRate = d;
		}

		public void SetMaxLife(int l)
		{
			this.mLife = l;
		}

		public void NoHSL()
		{
			this.mUseSolidColor = true;
		}

		public void SetX(float x)
		{
			this.mX = x;
		}

		public void SetY(float y)
		{
			this.mY = y;
		}

		public int GetWidth()
		{
			if (this.mImage == null)
			{
				return this.mTextWidth;
			}
			return this.mTextWidth + this.mImage.GetCelWidth() + this.mSpace;
		}

		public float GetX()
		{
			return this.mX;
		}

		public float GetY()
		{
			return this.mY;
		}

		public Font GetFont()
		{
			return this.mFont;
		}

		public string GetString()
		{
			return this.mText;
		}

		public const int CHANGE_AMOUNT = 8;

		protected float mBulgePct;

		protected float mBulgeAmt;

		protected float mBulgeDec;

		protected float mSize;

		protected int mBulgeDir;

		protected float mX;

		protected float mY;

		protected int mUpdateCnt;

		protected int mTextWidth;

		protected int mHue;

		protected int mLife;

		protected int mAlphaFadeDelay;

		protected int mImageDrawMode;

		protected bool mLeftJustifyImg;

		protected bool mDone;

		protected bool mUseSolidColor;

		protected bool mImageUsesSolidColor;

		protected string mText;

		protected int mWidth;

		protected int mHeight;

		protected Font mFont;

		protected Image mImage;

		protected float mAlpha;

		protected float mAlphaDecRate;

		protected float mSpeed;

		public int mSpace;

		public int mImageCel;

		public SexyColor mImageColor;

		public SexyColor mSolidColor;
	}
}
