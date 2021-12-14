using System;

namespace Sexy
{
	public/*internal*/ class PageControl : Widget
	{
		public PageControl(Image partsImage)
		{
			mPartsImage = partsImage;
			mNumberOfPages = 0;
			mCurrentPage = 0;
		}

		public override void Dispose()
		{
			base.Dispose();
		}

		public void SetNumberOfPages(int count)
		{
			if (count != mNumberOfPages)
			{
				mNumberOfPages = count;
				int theWidth = mPartsImage.GetCelWidth() * count;
				int celHeight = mPartsImage.GetCelHeight();
				Resize(mX, mY, theWidth, celHeight);
			}
		}

		public void SetCurrentPage(int page)
		{
			mCurrentPage = page;
			MarkDirtyFull();
		}

		public int GetCurrentPage()
		{
			return mCurrentPage;
		}

		public new void Draw(Graphics g)
		{
			int celWidth = mPartsImage.GetCelWidth();
			int celHeight = mPartsImage.GetCelHeight();
			int num = celWidth * mNumberOfPages;
			int num2 = (mWidth - num) / 2;
			int theY = (mHeight - celHeight) / 2;
			for (int i = 0; i < mNumberOfPages; i++)
			{
				int theCel = (mCurrentPage == i) ? 0 : 1;
				g.DrawImageCel(mPartsImage, num2, theY, theCel);
				num2 += celWidth;
			}
		}

		protected Image mPartsImage;

		protected int mNumberOfPages;

		protected int mCurrentPage;
	}
}
