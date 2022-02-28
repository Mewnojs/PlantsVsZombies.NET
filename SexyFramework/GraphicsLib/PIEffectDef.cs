using System;
using System.Collections.Generic;

namespace Sexy.GraphicsLib
{
	public class PIEffectDef
	{
		public PIEffectDef()
		{
			this.mRefCount = 1;
		}

		public virtual void Dispose()
		{
			for (int i = 0; i < this.mEmitterVector.Count; i++)
			{
				this.mEmitterVector[i] = null;
			}
			for (int j = 0; j < this.mTextureVector.Count; j++)
			{
				this.mTextureVector[j] = null;
			}
			this.mEmitterVector.Clear();
			this.mTextureVector.Clear();
		}

		public int mRefCount;

		public List<PIEmitter> mEmitterVector = new List<PIEmitter>();

		public List<PITexture> mTextureVector = new List<PITexture>();

		public List<PILayerDef> mLayerDefVector = new List<PILayerDef>();

		public Dictionary<int, int> mEmitterRefMap = new Dictionary<int, int>();
	}
}
