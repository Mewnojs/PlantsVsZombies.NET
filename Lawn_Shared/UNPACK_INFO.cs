using System;
using Sexy;

internal class UNPACK_INFO
{
	public UNPACK_INFO(Image mpImage, int mX, int mY, int mWidth, int mHeight, int mRows, int mCols, AnimType mAnimType, int mFrameDelay, int mBeginDelay, int mEndDelay)
	{
		this.mpImage = mpImage;
		this.mX = mX;
		this.mY = mY;
		this.mWidth = mWidth;
		this.mHeight = mHeight;
		this.mRows = mRows;
		this.mCols = mCols;
		this.mAnimType = mAnimType;
		this.mFrameDelay = mFrameDelay;
		this.mBeginDelay = mBeginDelay;
		this.mEndDelay = mEndDelay;
	}

	public Image mpImage;

	public int mX;

	public int mY;

	public int mWidth;

	public int mHeight;

	public int mRows;

	public int mCols;

	public AnimType mAnimType;

	public int mFrameDelay;

	public int mBeginDelay;

	public int mEndDelay;
}
