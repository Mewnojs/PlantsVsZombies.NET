using System;
using System.Collections.Generic;

namespace Sexy.GraphicsLib
{
	public static class GraphicsStatePool
	{
		public static GraphicsState CreateState()
		{
			GraphicsState result;
			if (GraphicsStatePool.mFreeStates.Count > 0)
			{
				result = GraphicsStatePool.mFreeStates.Pop();
			}
			else
			{
				result = new GraphicsState();
			}
			return result;
		}

		public static void ReleaseState(GraphicsState state)
		{
			GraphicsStatePool.mFreeStates.Push(state);
		}

		private static Stack<GraphicsState> mFreeStates = new Stack<GraphicsState>();
	}
}
