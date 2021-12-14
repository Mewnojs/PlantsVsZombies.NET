using System;

namespace Sexy.TodLib
{
	internal class ReanimationParams
	{
		public ReanimationParams(ReanimationType aReanimationType, string aReanimFilename) : this(aReanimationType, aReanimFilename, 0)
		{
		}

		public ReanimationParams(ReanimationType aReanimationType, string aReanimFilename, int aReanimparamFlags)
		{
			mReanimationType = aReanimationType;
			mReanimFileName = aReanimFilename;
			mReanimParamFlags = aReanimparamFlags;
		}

		public ReanimationType mReanimationType;

		public string mReanimFileName;

		public int mReanimParamFlags;
	}
}
