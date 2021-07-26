using System;

namespace Sexy
{
	public struct TypedKey<T> where T : struct
	{
		public int tick;

		public bool ease;

		public bool tween;

		public int KeyIdentifier;

		public T value;
	}
}
