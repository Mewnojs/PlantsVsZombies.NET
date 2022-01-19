using System;
using System.Collections.Generic;
using Sexy;

namespace Lawn
{
    public/*internal*/ class BeghouledBoardState
    {
        private BeghouledBoardState()
        {
        }

        public static BeghouledBoardState GetNewBeghouledBoardState()
        {
            if (BeghouledBoardState.unusedObjects.Count > 0)
            {
                return BeghouledBoardState.unusedObjects.Pop();
            }
            return new BeghouledBoardState();
        }

        public void PrepareForReuse()
        {
            BeghouledBoardState.unusedObjects.Push(this);
        }

        public SeedType[,] mSeedType = new SeedType[Constants.GRIDSIZEX, Constants.MAX_GRIDSIZEY];

        private static Stack<BeghouledBoardState> unusedObjects = new Stack<BeghouledBoardState>();
    }
}
