using System;

namespace Sexy.TodLib
{
	public/*internal*/ class FoleyInstance
	{
		public bool mPaused
		{
			get
			{
				return _paused && !mInstance.IsReleased();
			}
			set
			{
				_paused = value;
			}
		}

		public FoleyInstance()
		{
			mInstance = null;
			mRefCount = 0;
			mPaused = false;
			mStartTime = 0;
			mPauseOffset = 0;
		}

		public XNASoundInstance mInstance;

		public int mRefCount;

		private bool _paused;

		public int mStartTime;

		public int mPauseOffset;
	}
}
