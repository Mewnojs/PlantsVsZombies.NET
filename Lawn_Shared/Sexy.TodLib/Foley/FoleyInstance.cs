using System;

namespace Sexy.TodLib
{
	internal class FoleyInstance
	{
		public bool mPaused
		{
			get
			{
				return this._paused && !this.mInstance.IsReleased();
			}
			set
			{
				this._paused = value;
			}
		}

		public FoleyInstance()
		{
			this.mInstance = null;
			this.mRefCount = 0;
			this.mPaused = false;
			this.mStartTime = 0;
			this.mPauseOffset = 0;
		}

		public XNASoundInstance mInstance;

		public int mRefCount;

		private bool _paused;

		public int mStartTime;

		public int mPauseOffset;
	}
}
