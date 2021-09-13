using System;

namespace Sexy.TodLib
{
	public/*internal*/ class ReanimatorTrack
	{
		public string mName
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
				this.IsAttacher = this.name.StartsWith("attacher__");
			}
		}

		public ReanimatorTrack(string name, int transformCount)
		{
			this.mName = name;
			this.mTransformCount = (short)transformCount;
			this.mTransforms = new ReanimatorTransform[(int)this.mTransformCount];
		}

		public override string ToString()
		{
			return this.name;
		}

		public void ExtractImages()
		{
			for (int i = 0; i < this.mTransforms.Length; i++)
			{
				this.mTransforms[i].ExtractImages();
			}
		}

		private string name;

		public ReanimatorTransform[] mTransforms;

		public short mTransformCount;

		public bool IsAttacher;
	}
}
