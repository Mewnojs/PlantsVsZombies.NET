using System;

namespace Lawn
{
	public class ChallengeDefinition
	{
		public ChallengeDefinition(GameMode mode, int index, ChallengePage page, int row, int col, string name)
		{
			this.mChallengeMode = mode;
			this.mChallengeIconIndex = index;
			this.mPage = page;
			this.mRow = row;
			this.mCol = col;
			this.mChallengeName = name;
		}

		public override string ToString()
		{
			return this.mChallengeName;
		}

		public GameMode mChallengeMode;

		public int mChallengeIconIndex;

		public ChallengePage mPage;

		public int mRow;

		public int mCol;

		public string mChallengeName;
	}
}
