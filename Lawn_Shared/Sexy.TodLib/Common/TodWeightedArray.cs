using System;
using System.Collections.Generic;

namespace Sexy.TodLib
{
	public/*internal*/ class TodWeightedArray
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
			Reset();
			TodWeightedArray.unusedObjects.Push(this);
		}

		private TodWeightedArray()
		{
		}

		public void Reset()
		{
			mItem = null;
			mWeight = 0;
		}

		public object mItem;

		public int mWeight;

		private static Stack<TodWeightedArray> unusedObjects = new Stack<TodWeightedArray>();
	}
}
