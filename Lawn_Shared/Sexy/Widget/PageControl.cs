using System;

namespace Sexy
{
	internal class PageControl : Widget
	{
		public PageControl(Image partsImage)
		{
			this.mPartsImage = partsImage;
			this.mNumberOfPages = 0;
			this.mCurrentPage = 0;
		}

		public override void Dispose()
		{
			base.Dispose();
		}

		public void SetNumberOfPages(int count)
		{
			if (count != this.mNumberOfPages)
			{
				this.mNumberOfPages = count;
				int theWidth = this.mPartsImage.GetCelWidth() * count;
				int celHeight = this.mPartsImage.GetCelHeight();
				this.Resize(this.mX, this.mY, theWidth, celHeight);
			}
		}

		public void SetCurrentPage(int page)
		{
			this.mCurrentPage = page;
			this.MarkDirtyFull();
		}

		public int GetCurrentPage()
		{
			return this.mCurrentPage;
		}

		public new void Draw(Graphics g)
		{
			int celWidth = this.mPartsImage.GetCelWidth();
			int celHeight = this.mPartsImage.GetCelHeight();
			int num = celWidth * this.mNumberOfPages;
			int num2 = (this.mWidth - num) / 2;
			int theY = (this.mHeight - celHeight) / 2;
			for (int i = 0; i < this.mNumberOfPages; i++)
			{
				int theCel = (this.mCurrentPage == i) ? 0 : 1;
				g.DrawImageCel(this.mPartsImage, num2, theY, theCel);
				num2 += celWidth;
			}
		}

		protected Image mPartsImage;

		protected int mNumberOfPages;

		protected int mCurrentPage;
	}
}
