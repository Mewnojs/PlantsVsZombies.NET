using System;

namespace Lawn
{
	public class ChallengeDefinition
	{
		public ChallengeDefinition(GameMode mode, int index, ChallengePage page, int row, int col, string name)
		{
			mChallengeMode = mode;
			mChallengeIconIndex = index;
			mPage = page;
			mRow = row;
			mCol = col;
			mChallengeName = name;
		}

		public override string ToString()
		{
			return mChallengeName;
		}

		public GameMode mChallengeMode;

		public int mChallengeIconIndex;

		public ChallengePage mPage;

		public int mRow;

		public int mCol;

		public string mChallengeName;
	}
}
