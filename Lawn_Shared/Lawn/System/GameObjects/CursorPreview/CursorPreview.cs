using System;
using Sexy;

namespace Lawn
{
	public/*internal*/ class CursorPreview : GameObject
	{
		public CursorPreview()
		{
			mX = 0;
			mY = 0;
			mWidth = 80;
			mHeight = 80;
			mGridX = 0;
			mGridY = 0;
			mVisible = false;
		}

		public void Update()
		{
		}

		public override bool LoadFromFile(Sexy.Buffer b)
		{
			return false;
		}

		public override bool SaveToFile(Sexy.Buffer b)
		{
			return false;
		}

		public void Draw(Graphics g)
		{
			SeedType seedTypeInCursor = mBoard.GetSeedTypeInCursor();
		}

		public int mGridX;

		public int mGridY;
	}
}
