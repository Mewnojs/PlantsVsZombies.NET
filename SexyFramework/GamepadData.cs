using System;

namespace Sexy
{
	internal class GamepadData
	{
		public float[] mAxis = new float[4];

		public bool[] mButton = new bool[20];

		private float[] mButtonPressure = new float[20];

		public int[] mLastRepeat = new int[20];

		public int[] mStartRepeat = new int[20];

		public int[] mRepeatMax = new int[20];
	}
}
