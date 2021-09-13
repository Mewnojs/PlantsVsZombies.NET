using System;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
	public/*internal*/ class LawnDialog : Dialog
	{
		public int GetLeft()
		{
			return this.mContentInsets.mLeft + this.mBackgroundInsets.mLeft;
		}

		public int GetWidth()
		{
			return this.mWidth - this.mContentInsets.mLeft - this.mContentInsets.mRight - this.mBackgroundInsets.mLeft - this.mBackgroundInsets.mRight;
		}

		public int GetTop()
		{
			return this.mContentInsets.mTop + this.mBackgroundInsets.mTop + (int)Constants.InvertAndScale(54f) + Constants.DIALOG_HEADER_OFFSET;
		}

		public LawnDialog(LawnApp theApp, Image theButtonComponentImage, int theId, bool isModal, string theDialogHeader, string theDialogLines, string theDialogFooter, int theButtonMode) : base(null, theButtonComponentImage, theId, isModal, theDialogHeader, theDialogLines, string.Empty, 0)
		{
			this.mTextAlign = 2;
			this.mApp = theApp;
			this.mButtonDelay = -1;
			this.mReanimation = new ReanimationWidget();
			this.mReanimation.mLawnDialog = this;
			this.mDrawStandardBack = true;
			this.mTallBottom = false;
			this.mVerticalCenterText = true;
			this.mDialogHeader = TodStringFile.TodStringTranslate(theDialogHeader);
			this.mDialogLines = TodStringFile.TodStringTranslate(theDialogLines);
			this.SetColors(GameConstants.gLawnDialogColors, 7);
			this.SetHeaderFont(Resources.FONT_DWARVENTODCRAFT15);
			this.SetLinesFont(Resources.FONT_DWARVENTODCRAFT15);
			this.mContentInsets = Constants.LawnDialog_Insets;
			this.SetColor(0, new SexyColor(224, 187, 98));
			this.SetColor(1, new SexyColor(224, 187, 98));
			this.mSpaceAfterHeader = (int)Constants.InvertAndScale(10f);
			if (theButtonMode == 1)
			{
				this.mLawnYesButton = GameButton.MakeButton(1000, this, "[BUTTON_YES]");
				this.mLawnNoButton = GameButton.MakeButton(1001, this, "[BUTTON_NO]");
				return;
			}
			if (theButtonMode == 2)
			{
				this.mLawnYesButton = GameButton.MakeButton(1000, this, "[BUTTON_OK]");
				this.mLawnNoButton = GameButton.MakeButton(1001, this, "[BUTTON_CANCEL]");
				return;
			}
			if (theButtonMode == 3)
			{
				this.mLawnYesButton = GameButton.MakeButton(1000, this, theDialogFooter);
				this.mLawnNoButton = null;
				return;
			}
			this.mLawnYesButton = null;
			this.mLawnNoButton = null;
		}

		public override void Dispose()
		{
			if (this.mReanimation != null)
			{
				this.mReanimation.Dispose();
			}
			if (this.mLawnYesButton != null)
			{
				this.mLawnYesButton.Dispose();
			}
			if (this.mLawnNoButton != null)
			{
				this.mLawnNoButton.Dispose();
			}
		}

		public virtual void SetButtonDelay(int theDelay)
		{
			this.mButtonDelay = theDelay;
			if (this.mLawnYesButton != null)
			{
				this.mLawnYesButton.SetDisabled(true);
			}
			if (this.mLawnNoButton != null)
			{
				this.mLawnNoButton.SetDisabled(true);
			}
		}

		public override void Update()
		{
			base.Update();
			if (this.mUpdateCnt == this.mButtonDelay)
			{
				if (this.mLawnYesButton != null)
				{
					this.mLawnYesButton.SetDisabled(false);
				}
				if (this.mLawnNoButton != null)
				{
					this.mLawnNoButton.SetDisabled(false);
				}
			}
			this.MarkDirty();
		}

		public override void ButtonPress(int theId)
		{
			this.mApp.PlaySample(Resources.SOUND_GRAVEBUTTON);
		}

		public override bool BackButtonPress()
		{
			int mId;
			if (this.mLawnNoButton != null && this.mId != 55)
			{
				mId = this.mLawnNoButton.mId;
			}
			else
			{
				mId = this.mLawnYesButton.mId;
			}
			this.ButtonPress(mId);
			this.ButtonDepress(mId);
			return true;
		}

		public override void ButtonDepress(int theId)
		{
			if (this.mUpdateCnt > this.mButtonDelay)
			{
				base.ButtonDepress(theId);
			}
		}

		public virtual void CheckboxChecked(int theId, bool cheked)
		{
			this.mApp.PlaySample(Resources.SOUND_BUTTONCLICK);
		}

		public override void KeyDown(KeyCode theKey)
		{
			if (this.mId == 19 && this.mApp.mBoard != null)
			{
				this.mApp.mBoard.DoTypingCheck(theKey);
			}
			if (this.mId == 3)
			{
				return;
			}
			if (theKey == KeyCode.KEYCODE_SPACE || theKey == KeyCode.KEYCODE_RETURN || (ushort)theKey == 121 || (ushort)theKey == 89)
			{
				base.ButtonDepress(1000);
				return;
			}
			if ((theKey == KeyCode.KEYCODE_ESCAPE || (ushort)theKey == 110 || (ushort)theKey == 78) && this.mLawnNoButton != null)
			{
				base.ButtonDepress(1001);
			}
		}

		public override void AddedToManager(WidgetManager theWidgetManager)
		{
			base.AddedToManager(theWidgetManager);
			this.AddWidget(this.mReanimation);
			if (this.mLawnYesButton != null)
			{
				this.AddWidget(this.mLawnYesButton);
			}
			if (this.mLawnNoButton != null)
			{
				this.AddWidget(this.mLawnNoButton);
			}
		}

		public override void RemovedFromManager(WidgetManager theWidgetManager)
		{
			base.RemovedFromManager(theWidgetManager);
			if (this.mLawnYesButton != null)
			{
				this.RemoveWidget(this.mLawnYesButton);
			}
			if (this.mLawnNoButton != null)
			{
				this.RemoveWidget(this.mLawnNoButton);
			}
			this.RemoveWidget(this.mReanimation);
			this.mReanimation.Dispose();
		}

		public override void Resize(int theX, int theY, int theWidth, int theHeight)
		{
			base.Resize(theX, theY, theWidth, theHeight);
			Image image = this.mTallBottom ? AtlasResources.IMAGE_DIALOG_BIGBOTTOMLEFT : AtlasResources.IMAGE_DIALOG_BOTTOMLEFT;
			int num = this.mContentInsets.mLeft + this.mBackgroundInsets.mLeft - (int)Constants.InvertAndScale(5f);
			int num2 = theHeight - image.mHeight;
			if (this.mTallBottom)
			{
				num2 += (int)Constants.InvertAndScale(44f);
			}
			else
			{
				num2 += (int)Constants.InvertAndScale(16f);
			}
			int num3 = this.mWidth - this.mContentInsets.mLeft - this.mBackgroundInsets.mLeft - this.mContentInsets.mRight - this.mBackgroundInsets.mRight + (int)Constants.InvertAndScale(8f);
			int mHeight = AtlasResources.IMAGE_BUTTON_LEFT.mHeight;
			int num4 = (int)Constants.InvertAndScale(10f);
			int num5 = (num3 - num4) / 2;
			int num6 = Math.Max(this.mButtonMinWidth, AtlasResources.IMAGE_BUTTON_LEFT.mWidth + AtlasResources.IMAGE_BUTTON_MIDDLE.mWidth + AtlasResources.IMAGE_BUTTON_RIGHT.mWidth);
			if (this.mLawnYesButton != null && this.mLawnNoButton != null)
			{
				int num7 = (int)(0.8f * (float)Math.Max(this.mLawnYesButton.mFont.StringWidth(this.mLawnYesButton.mLabel), this.mLawnNoButton.mFont.StringWidth(this.mLawnNoButton.mLabel)));
				if (num7 > num6)
				{
					num6 = num7;
				}
			}
			int num8 = num5 - num6 - AtlasResources.IMAGE_BUTTON_MIDDLE.mWidth + 1;
			if (num8 < num6)
			{
				num8 = num6;
			}
			if (this.mLawnYesButton != null && this.mLawnNoButton != null)
			{
				num8 += (int)Constants.InvertAndScale(8f);
				int num9 = num;
				int num10 = num + num3 - num8;
				while (num9 + num8 > num10)
				{
					num9 -= 10;
					num10 += 10;
				}
				this.mLawnYesButton.Resize(num9, num2, num8, mHeight);
				this.mLawnNoButton.Resize(num10, num2, num8, mHeight);
			}
			else if (this.mLawnYesButton != null)
			{
				int num11 = num3 - AtlasResources.IMAGE_BUTTON_MIDDLE.mWidth + 1;
				num += (num3 - num11) / 2;
				this.mLawnYesButton.Resize(num, num2, num11, mHeight);
			}
			if (this.mReanimation.mReanim != null)
			{
				this.mReanimation.Resize((int)this.mReanimation.mPosX, (int)((float)Constants.DIALOG_HEADER_OFFSET + this.mReanimation.mPosY), this.mReanimation.mWidth, this.mReanimation.mHeight);
			}
			this.mX = this.mApp.mWidth / 2 - this.mWidth / 2;
			this.mY = this.mApp.mHeight / 2 - this.mHeight / 2;
		}

		public override void Draw(Graphics g)
		{
			if (!this.mDrawStandardBack)
			{
				return;
			}
			Image image = AtlasResources.IMAGE_DIALOG_BOTTOMLEFT;
			Image image2 = AtlasResources.IMAGE_DIALOG_BOTTOMMIDDLE;
			Image image3 = AtlasResources.IMAGE_DIALOG_BOTTOMRIGHT;
			if (this.mTallBottom)
			{
				image = AtlasResources.IMAGE_DIALOG_BIGBOTTOMLEFT;
				image2 = AtlasResources.IMAGE_DIALOG_BIGBOTTOMMIDDLE;
				image3 = AtlasResources.IMAGE_DIALOG_BIGBOTTOMRIGHT;
			}
			int theWidth = this.mWidth - AtlasResources.IMAGE_DIALOG_TOPLEFT.mWidth - AtlasResources.IMAGE_DIALOG_TOPRIGHT.mWidth;
			int num = Constants.DIALOG_HEADER_OFFSET;
			g.DrawImage(AtlasResources.IMAGE_DIALOG_TOPLEFT, 0, num);
			LawnCommon.TileImageHorizontally(g, AtlasResources.IMAGE_DIALOG_TOPMIDDLE, AtlasResources.IMAGE_DIALOG_TOPLEFT.mWidth, num, theWidth);
			g.DrawImage(AtlasResources.IMAGE_DIALOG_TOPRIGHT, this.mWidth - AtlasResources.IMAGE_DIALOG_TOPRIGHT.mWidth, num);
			num += AtlasResources.IMAGE_DIALOG_TOPRIGHT.mHeight;
			int theWidth2 = this.mWidth - AtlasResources.IMAGE_DIALOG_CENTERLEFT.mWidth - AtlasResources.IMAGE_DIALOG_CENTERRIGHT.mWidth;
			int num2 = this.mHeight - num - image2.mHeight;
			Graphics @new = Graphics.GetNew(g);
			@new.SetClipRect(0, num, this.mWidth, num2);
			int num3 = (num2 + AtlasResources.IMAGE_DIALOG_CENTERLEFT.mHeight - 1) / AtlasResources.IMAGE_DIALOG_CENTERLEFT.mHeight;
			while (num3-- > 0)
			{
				@new.DrawImage(AtlasResources.IMAGE_DIALOG_CENTERLEFT, 0, num);
				LawnCommon.TileImageHorizontally(@new, AtlasResources.IMAGE_DIALOG_CENTERMIDDLE, AtlasResources.IMAGE_DIALOG_CENTERLEFT.mWidth, num, theWidth2);
				@new.DrawImage(AtlasResources.IMAGE_DIALOG_CENTERRIGHT, this.mWidth - AtlasResources.IMAGE_DIALOG_CENTERRIGHT.mWidth, num);
				num += AtlasResources.IMAGE_DIALOG_CENTERLEFT.mHeight;
			}
			@new.PrepareForReuse();
			int theWidth3 = this.mWidth - image.mWidth - image3.mWidth;
			num = this.mHeight - image2.mHeight;
			g.DrawImage(image, 0, num);
			LawnCommon.TileImageHorizontally(g, image2, image.mWidth, num, theWidth3);
			g.DrawImage(image3, this.mWidth - image3.mWidth, num);
			g.DrawImage(AtlasResources.IMAGE_DIALOG_HEADER, (this.mWidth - AtlasResources.IMAGE_DIALOG_HEADER.mWidth) / 2, 0);
			int num4 = Constants.DIALOG_HEADER_OFFSET + this.mContentInsets.mTop + this.mBackgroundInsets.mTop;
			if (this.mDialogHeader.Length > 0)
			{
				g.SetFont(this.mHeaderFont);
				g.SetColor(this.mColors[0]);
				this.WriteCenteredLine(g, num4, this.mDialogHeader);
				num4 += this.mHeaderFont.GetHeight();
				num4 += this.mSpaceAfterHeader;
			}
			g.SetFont(this.mLinesFont);
			g.SetColor(this.mColors[1]);
			int theWidth4 = this.mWidth - this.mContentInsets.mLeft - this.mContentInsets.mRight - this.mBackgroundInsets.mLeft - this.mBackgroundInsets.mRight - (int)Constants.InvertAndScale(4f);
			TRect theRect = new TRect(this.mBackgroundInsets.mLeft + this.mContentInsets.mLeft + (int)Constants.InvertAndScale(2f), num4, theWidth4, 0);
			if (this.mVerticalCenterText)
			{
				int wordWrappedHeight = this.GetWordWrappedHeight(g, theWidth4, this.mDialogLines, this.mLinesFont.GetLineSpacing() + this.mLineSpacingOffset);
				int num5 = this.mHeight - this.mContentInsets.mBottom - this.mBackgroundInsets.mBottom - num4 - this.mButtonHeight - (int)Constants.InvertAndScale(20f);
				if (this.mTallBottom)
				{
					num5 -= (int)Constants.InvertAndScale(40f);
				}
				theRect.mY += (num5 - wordWrappedHeight) / 2;
			}
			num4 += this.WriteWordWrapped(g, theRect, this.mDialogLines, this.mLineSpacingOffset, this.mTextAlign);
		}

		public override int WriteWordWrapped(Graphics g, TRect theRect, string theLine, int theLineSpacing, int theJustification)
		{
			bool mWriteColoredString = g.mWriteColoredString;
			g.mWriteColoredString = Widget.mWriteColoredString;
			int result = TodStringFile.TodDrawStringWrapped(g, theLine, theRect, g.GetFont(), g.mColor, (DrawStringJustification)theJustification);
			g.mWriteColoredString = mWriteColoredString;
			return result;
		}

		public override void MouseDrag(int x, int y)
		{
		}

		public void CalcSize(int theExtraX, int theExtraY)
		{
			this.CalcSize(theExtraX, theExtraY, 0);
		}

		public void CalcSize(int theExtraX, int theExtraY, int theMinWidth)
		{
			int num = this.mContentInsets.mLeft + this.mContentInsets.mRight + this.mBackgroundInsets.mLeft + this.mBackgroundInsets.mRight + theExtraX;
			if (this.mDialogHeader.Length > 0)
			{
				num += this.mHeaderFont.StringWidth(this.mDialogHeader);
				if (num < theMinWidth)
				{
					num = theMinWidth;
				}
			}
			int num2 = Math.Max(this.mMinWidth, AtlasResources.IMAGE_DIALOG_TOPLEFT.mWidth + AtlasResources.IMAGE_DIALOG_TOPRIGHT.mWidth + AtlasResources.IMAGE_DIALOG_TOPMIDDLE.mWidth);
			if (num < num2)
			{
				num = num2;
			}
			int num3 = Constants.DIALOG_HEADER_OFFSET + this.mContentInsets.mTop + this.mContentInsets.mBottom + this.mBackgroundInsets.mTop + this.mBackgroundInsets.mBottom + theExtraY;
			if (!string.IsNullOrEmpty(this.mDialogHeader))
			{
				num3 += this.mHeaderFont.GetHeight();
				num3 += this.mSpaceAfterHeader;
			}
			if (this.mDialogLines.Length > 0)
			{
				num += AtlasResources.IMAGE_DIALOG_TOPMIDDLE.mWidth;
				Graphics @new = Graphics.GetNew();
				@new.SetFont(this.mLinesFont);
				num3 += this.GetWordWrappedHeight(@new, num - this.mContentInsets.mLeft - this.mContentInsets.mRight - this.mBackgroundInsets.mLeft - this.mBackgroundInsets.mRight - (int)Constants.InvertAndScale(4f), this.mDialogLines, this.mLineSpacingOffset);
				@new.PrepareForReuse();
			}
			num3 += AtlasResources.IMAGE_DIALOG_BOTTOMMIDDLE.GetHeight();
			int num4 = Constants.DIALOG_HEADER_OFFSET + AtlasResources.IMAGE_DIALOG_TOPLEFT.mHeight + AtlasResources.IMAGE_DIALOG_BOTTOMLEFT.mHeight;
			if (this.mTallBottom)
			{
				num4 = Constants.DIALOG_HEADER_OFFSET + AtlasResources.IMAGE_DIALOG_TOPLEFT.mHeight + AtlasResources.IMAGE_DIALOG_BIGBOTTOMLEFT.mHeight;
			}
			if (num3 < num4)
			{
				num3 = num4;
			}
			this.Resize(this.mX, this.mY, num, num3);
		}

		public override int GetWordWrappedHeight(Graphics g, int theWidth, string theLine, int aLineSpacing)
		{
			return TodStringFile.TodDrawStringWrappedHeight(g, theLine, new TRect(0, 0, theWidth, 0), g.GetFont(), g.mColor, DrawStringJustification.DS_ALIGN_LEFT);
		}

		public LawnApp mApp;

		public int mButtonDelay;

		public ReanimationWidget mReanimation;

		public bool mDrawStandardBack;

		public LawnStoneButton mLawnYesButton;

		public LawnStoneButton mLawnNoButton;

		public bool mTallBottom;

		public bool mVerticalCenterText;

		protected int mButtonMinWidth;

		public int mMinWidth;
	}
}
