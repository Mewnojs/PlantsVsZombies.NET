using System;
using System.Collections.Generic;

namespace Sexy.TodLib
{
	internal class TodWeightedArray
	{
		public static TodWeightedArray GetNewTodWeightedArray()
		{
			if (TodWeightedArray.unusedObjects.Count > 0)
			{
				return TodWeightedArray.unusedObjects.Pop();
			}
			return new TodWeightedArray();
		}

		public void PrepareForReuse()
		{
			this.Reset();
			TodWeightedArray.unusedObjects.Push(this);
		}

		private TodWeightedArray()
		{
		}

		public void Reset()
		{
			this.mItem = null;
			this.mWeight = 0;
		}

		public object mItem;

		public int mWeight;

		private static Stack<TodWeightedArray> unusedObjects = new Stack<TodWeightedArray>();
	}
}
