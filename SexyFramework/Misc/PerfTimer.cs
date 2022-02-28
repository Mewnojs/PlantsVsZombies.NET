using System;

namespace Sexy.Misc
{
	public class PerfTimer
	{
		protected void CalcDuration()
		{
			this.mDuration = (ulong)Common.SexyTime() - this.mStart;
		}

		public PerfTimer()
		{
			this.mRunning = false;
		}

		public void Start()
		{
			this.mStart = (ulong)Common.SexyTime();
			this.mRunning = true;
		}

		public void Stop()
		{
			this.CalcDuration();
			this.mRunning = false;
		}

		public double GetDuration()
		{
			if (this.mRunning)
			{
				this.CalcDuration();
			}
			return this.mDuration;
		}

		public bool IsRunning()
		{
			return this.mRunning;
		}

		protected ulong mStart;

		protected double mDuration;

		protected bool mRunning;
	}
}
