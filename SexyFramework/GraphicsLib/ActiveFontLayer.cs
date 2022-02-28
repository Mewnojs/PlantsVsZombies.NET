using System;
using System.Collections.Generic;
using Sexy.Misc;

namespace Sexy.GraphicsLib
{
	public class ActiveFontLayer
	{
		public ActiveFontLayer()
		{
		}

		public ActiveFontLayer(ActiveFontLayer theActiveFontLayer)
		{
			this.mBaseFontLayer = theActiveFontLayer.mBaseFontLayer;
			this.mUseAlphaCorrection = theActiveFontLayer.mUseAlphaCorrection;
			this.mScaledCharImageRects = theActiveFontLayer.mScaledCharImageRects;
			this.mColorStack = theActiveFontLayer.mColorStack;
			for (int i = 0; i < 8; i++)
			{
				this.mScaledImages[i] = theActiveFontLayer.mScaledImages[i];
			}
		}

		public virtual void Dispose()
		{
			this.mScaledCharImageRects.Clear();
		}

		public SharedImageRef GenerateAlphaCorrectedImage(int thePalette)
		{
			bool flag = false;
			this.mScaledImages[thePalette] = GlobalMembers.gSexyAppBase.GetSharedImage("!" + this.mScaledImages[7].GetMemoryImage().mFilePath, string.Format("AltFontImage{0}", thePalette), ref flag, true, false);
			this.mScaledImages[thePalette].GetMemoryImage().Create(this.mScaledImages[7].mWidth, this.mScaledImages[7].mHeight);
			int num = this.mScaledImages[7].mWidth * this.mScaledImages[7].mHeight;
			this.mScaledImages[thePalette].GetMemoryImage().mColorTable = new uint[256];
			this.mScaledImages[thePalette].GetMemoryImage().mColorIndices = new byte[num];
			if (this.mScaledImages[7].GetMemoryImage().mColorTable != null)
			{
				Array.Copy(this.mScaledImages[thePalette].GetMemoryImage().mColorIndices, this.mScaledImages[7].GetMemoryImage().mColorIndices, num);
			}
			else
			{
				uint[] bits = this.mScaledImages[7].GetMemoryImage().GetBits();
				for (int i = 0; i < num; i++)
				{
					this.mScaledImages[thePalette].GetMemoryImage().mColorIndices[i] = (byte)(bits[i] >> 24);
				}
			}
			Array.Copy(this.mScaledImages[thePalette].GetMemoryImage().mColorTable, GlobalImageFont.FONT_PALETTES[thePalette], 1024);
			return this.mScaledImages[thePalette];
		}

		public void PushColor(SexyColor theColor)
		{
			if (this.mColorStack.Count == 0)
			{
				this.mColorStack.Add(theColor);
				return;
			}
			SexyColor color = this.mColorStack[this.mColorStack.Count - 1];
			SexyColor color2 = new SexyColor(theColor.mRed * color.mRed / 255, theColor.mGreen * color.mGreen / 255, theColor.mBlue * color.mBlue / 255, theColor.mAlpha * color.mAlpha / 255);
			this.mColorStack.Add(color2);
		}

		public void PopColor()
		{
			if (this.mColorStack.Count != 0)
			{
				this.mColorStack.RemoveAt(this.mColorStack.Count - 1);
			}
		}

		public FontLayer mBaseFontLayer;

		public SharedImageRef[] mScaledImages = new SharedImageRef[8];

		public bool mUseAlphaCorrection;

		public bool mOwnsImage;

		public Dictionary<char, Rect> mScaledCharImageRects = new Dictionary<char, Rect>();

		public List<SexyColor> mColorStack = new List<SexyColor>();
	}
}
