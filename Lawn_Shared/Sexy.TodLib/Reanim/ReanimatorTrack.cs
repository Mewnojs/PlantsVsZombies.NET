using System;

namespace Sexy.TodLib
{
	public/*internal*/ class ReanimatorTrack
	{
		public string mName
		{
			get
			{
				return name;
			}
			set
			{
				name = value;
				IsAttacher = name.StartsWith("attacher__");
			}
		}

		public ReanimatorTrack(string name, int transformCount)
		{
			mName = name;
			mTransformCount = (short)transformCount;
			mTransforms = new ReanimatorTransform[mTransformCount];
		}

		public override string ToString()
		{
			return name;
		}

		public void ExtractImages()
		{
			for (int i = 0; i < mTransforms.Length; i++)
			{
				mTransforms[i].ExtractImages();
			}
		}

		private string name;

		public ReanimatorTransform[] mTransforms;

		public short mTransformCount;

		public bool IsAttacher;
	}
}
