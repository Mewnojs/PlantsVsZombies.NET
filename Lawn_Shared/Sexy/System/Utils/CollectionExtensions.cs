using System;
using System.Collections.Generic;

namespace Sexy
{
	internal static class CollectionExtensions
	{
		public static bool empty<T>(this IList<T> col)
		{
			return col.Count == 0;
		}

		public static bool empty<T, U>(this Dictionary<T, U> col)
		{
			return col.Count == 0;
		}

		public static int size<T>(this List<T> col)
		{
			return col.Count;
		}

		public static T Last<T>(this List<T> col)
		{
			return col[col.Count - 1];
		}

		public static void RemoveLast<T>(this List<T> col)
		{
			col.RemoveAt(col.Count - 1);
		}

		public static bool empty<T>(this Stack<T> col)
		{
			return col.Count == 0;
		}

		public static void push_back<T>(this Stack<T> col, T element)
		{
			col.Push(element);
		}

		public static T pop_back<T>(this Stack<T> col)
		{
			return col.Pop();
		}

		public static T back<T>(this Stack<T> col)
		{
			return col.Peek();
		}

		public static void insert<T, U>(this List<KeyValuePair<T, U>> col, T key, U value)
		{
			col.Add(new KeyValuePair<T, U>(key, value));
		}
	}
}
