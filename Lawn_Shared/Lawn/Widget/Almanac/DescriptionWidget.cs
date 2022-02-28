using System;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
    public/*internal*/ class DescriptionWidget : Widget
    {
        public DescriptionWidget()
        {
            mHeight = 0;
        }

        public void SetText(ref string theText)
        {
            mText = theText;
            Graphics @new = Graphics.GetNew();
            @new.SetFont(Resources.FONT_BRIANNETOD12);
            mHeight = TodStringFile.TodDrawStringWrappedHeight(@new, mText, new Rect(0, 0, mWidth - SCROLLBAR_PAD, 0), Resources.FONT_BRIANNETOD12, new SexyColor(40, 50, 90), DrawStringJustification.Left);
            @new.PrepareForReuse();
        }

        public override void Draw(Graphics g)
        {
            g.HardwareClip();
            TodStringFile.TodDrawStringWrapped(g, mText, new Rect(0, 0, mWidth - SCROLLBAR_PAD, mHeight), Resources.FONT_BRIANNETOD12, new SexyColor(40, 50, 90), DrawStringJustification.Left);
            g.EndHardwareClip();
        }

        public int SCROLLBAR_PAD = Constants.DescriptionWidget_ScrollBar_Padding;

        private string mText = string.Empty;
    }
}
