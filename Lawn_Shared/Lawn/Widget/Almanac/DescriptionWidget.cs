using System;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
	internal class DescriptionWidget : Widget
	{
		public DescriptionWidget()
		{
			this.mHeight = 0;
		}

		public void SetText(ref string theText)
		{
			this.mText = theText;
			Graphics @new = Graphics.GetNew();
			@new.SetFont(Resources.FONT_BRIANNETOD12);
			this.mHeight = TodStringFile.TodDrawStringWrappedHeight(@new, this.mText, new TRect(0, 0, this.mWidth - this.SCROLLBAR_PAD, 0), Resources.FONT_BRIANNETOD12, new SexyColor(40, 50, 90), DrawStringJustification.DS_ALIGN_LEFT);
			@new.PrepareForReuse();
		}

		public override void Draw(Graphics g)
		{
			g.HardwareClip();
			TodStringFile.TodDrawStringWrapped(g, this.mText, new TRect(0, 0, this.mWidth - this.SCROLLBAR_PAD, this.mHeight), Resources.FONT_BRIANNETOD12, new SexyColor(40, 50, 90), DrawStringJustification.DS_ALIGN_LEFT);
			g.EndHardwareClip();
		}

		public int SCROLLBAR_PAD = Constants.DescriptionWidget_ScrollBar_Padding;

		private string mText = string.Empty;
	}
}
