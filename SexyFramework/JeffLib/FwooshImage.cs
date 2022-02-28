using System;
using Sexy;
using Sexy.GraphicsLib;
using Sexy.Misc;

namespace JeffLib
{
	public class FwooshImage
	{
		public FwooshImage()
		{
			this.mImage = null;
			this.mIsDelaying = false;
			this.mAlpha = 255f;
			this.mX = 0;
			this.mY = 0;
			this.mDelay = 0;
			this.mSize = 0f;
			this.mIncText = true;
			this.mAlphaDec = 2f;
			this.mSizeInc = 0.07f;
			this.mMaxSize = 1.2f;
			this.mForward = true;
		}

		public void Update()
		{
			if (this.mAlpha < 255f && this.mAlpha > 0f)
			{
				this.mAlpha -= this.mAlphaDec;
				if (this.mAlpha < 0f)
				{
					this.mAlpha = 0f;
					return;
				}
			}
			else if (this.mIncText && this.mSize < this.mMaxSize)
			{
				this.mSize += this.mSizeInc;
				if (this.mSize >= this.mMaxSize)
				{
					this.mIncText = false;
					return;
				}
			}
			else if (!this.mIncText && (this.mSize > 1f || !this.mForward || this.mDelay > 0))
			{
				if ((this.mSize > 1f || this.mDelay == 0 || !this.mForward) && !this.mIsDelaying)
				{
					if (!this.mForward && this.mDelay > 0)
					{
						this.mDelay--;
					}
					if (this.mForward || this.mDelay <= 0)
					{
						this.mSize -= this.mSizeInc;
					}
					if (!this.mForward && this.mSize < 0f)
					{
						this.mSize = 0f;
					}
					else if (this.mDelay > 0 && this.mSize <= 1f)
					{
						this.mSize = 1f;
						this.mIsDelaying = true;
					}
				}
				else if (this.mDelay > 0)
				{
					this.mDelay--;
					if (this.mDelay == 0)
					{
						this.mIsDelaying = false;
					}
				}
				if (this.mSize <= 1f && this.mDelay == 0 && this.mForward)
				{
					this.mSize = Math.Min(1f, this.mMaxSize);
					this.mAlpha -= this.mAlphaDec;
				}
			}
		}

		public void Draw(Graphics g)
		{
			if (this.mImage == null || this.mAlpha <= 0f || this.mSize <= 0f)
			{
				return;
			}
			this.mGlobalTransform.Reset();
			if (!Sexy.Common._eq(this.mSize, 1f))
			{
				this.mGlobalTransform.Scale(this.mSize, this.mSize);
			}
			g.PushState();
			if (this.mAlpha != 255f)
			{
				g.SetColor(255, 255, 255, (int)this.mAlpha);
				g.SetColorizeImages(true);
			}
			if (!g.Is3D())
			{
				g.DrawImageTransform(this.mImage, this.mGlobalTransform, (float)this.mX, (float)this.mY);
			}
			else
			{
				g.DrawImageTransformF(this.mImage, this.mGlobalTransform, (float)this.mX, (float)this.mY);
			}
			g.SetColorizeImages(false);
			g.PopState();
		}

		public void Reverse()
		{
			this.mForward = false;
			this.mIncText = true;
		}

		public static void inlineUpper(ref string theData)
		{
			theData = theData.ToUpper();
		}

		public static void inlineLower(ref string theData)
		{
			theData = theData.ToLower();
		}

		public static void inlineLTrim(ref string theData)
		{
			FwooshImage.inlineLTrim(ref theData, " \t\r\n");
		}

		public static void inlineLTrim(ref string theData, string theChars)
		{
			for (int i = 0; i < theData.Length; i++)
			{
				if (theChars.IndexOf(theData[i]) < 0)
				{
					theData = theData.Remove(0, i);
					return;
				}
			}
		}

		public static void inlineRTrim(ref string theData)
		{
			FwooshImage.inlineRTrim(ref theData, " \t\r\n");
		}

		public static void inlineRTrim(ref string theData, string theChars)
		{
			for (int i = theData.Length - 1; i >= 0; i--)
			{
				if (theChars.IndexOf(theData[i]) < 0)
				{
					theData = theData.Remove(i + 1);
					return;
				}
			}
		}

		public static void inlineTrim(ref string theData)
		{
			FwooshImage.inlineTrim(ref theData, " \t\r\n");
		}

		public static void inlineTrim(ref string theData, string theChars)
		{
			FwooshImage.inlineRTrim(ref theData, theChars);
			FwooshImage.inlineLTrim(ref theData, theChars);
		}

		public const uint SEXY_RAND_MAX = 2147483647U;

		public MemoryImage mImage;

		public float mAlpha;

		public int mX;

		public int mY;

		public int mDelay;

		public float mSize;

		public bool mIncText;

		public bool mIsDelaying;

		public float mSizeInc;

		public float mAlphaDec;

		public float mMaxSize;

		public bool mForward;

		public Transform mGlobalTransform = new Transform();
	}
}
