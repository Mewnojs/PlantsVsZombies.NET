using System;

public struct PerfTimer
{
	private void CalcDuration()
	{
		this.mEnd = Environment.TickCount;
		this.mDuration = this.mEnd - this.mStart;
	}

	public void Start()
	{
		this.mRunning = true;
		this.mStart = Environment.TickCount;
	}

	public void Stop()
	{
		if (this.mRunning)
		{
			this.CalcDuration();
			this.mRunning = false;
		}
	}

	public double GetDuration()
	{
		if (this.mRunning)
		{
			this.CalcDuration();
		}
		return (double)this.mDuration;
	}

	private bool mRunning;

	private int mStart;

	private int mEnd;

	private int mDuration;
}
