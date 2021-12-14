using System;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
	internal class LoadingScreen : Widget
	{
		public override void Draw(Graphics g)
		{
			base.Draw(g);
			int num = mWidth / 2;
			int num2 = mHeight / 2 - (int)Constants.InvertAndScale(34f);
			g.DrawImage(AtlasResources.IMAGE_LOADBAR_DIRT, num, num2 + Constants.InvertAndScale(18f));
			if (mCurBarWidth >= mTotalBarWidth)
			{
				g.DrawImage(AtlasResources.IMAGE_LOADBAR_GRASS, num, num2);
				return;
			}
			Graphics @new = Graphics.GetNew(g);
			@new.ClipRect(num, num2, (int)mCurBarWidth, AtlasResources.IMAGE_LOADBAR_GRASS.mHeight);
			@new.DrawImage(AtlasResources.IMAGE_LOADBAR_GRASS, num, num2);
			float num3 = mCurBarWidth * 0.94f;
			float rad = -num3 / 180f * 3.1415927f * 2f;
			float num4 = TodCommon.TodAnimateCurveFloatTime(0f, mTotalBarWidth, mCurBarWidth, 1f, 0.5f, TodCurves.CURVE_LINEAR);
			SexyTransform2D sexyTransform2D = default(SexyTransform2D);
			TodCommon.TodScaleRotateTransformMatrix(ref sexyTransform2D.mMatrix, num + Constants.InvertAndScale(11f) + num3, num2 - Constants.InvertAndScale(3f) - Constants.InvertAndScale(35f) * num4 + Constants.InvertAndScale(35f), rad, num4, num4);
			TRect theSrcRect = new TRect(0, 0, AtlasResources.IMAGE_REANIM_LOAD_SODROLLCAP.mWidth, AtlasResources.IMAGE_REANIM_LOAD_SODROLLCAP.mHeight);
			TodCommon.TodBltMatrix(g, AtlasResources.IMAGE_REANIM_LOAD_SODROLLCAP, sexyTransform2D.mMatrix, ref g.mClipRect, SexyColor.White, g.mDrawMode, theSrcRect);
			@new.PrepareForReuse();
		}

		private float mCurBarWidth;

		private float mTotalBarWidth;

		public static bool IsLoading;

		public static LoadingScreen gLoadingScreen = new LoadingScreen();
	}
}
