using System;
using System.Collections.Generic;
using Sexy.GraphicsLib;

namespace Sexy.WidgetsLib
{
	public class PASpriteInst : IDisposable
	{
		public virtual void Dispose()
		{
			for (int i = 0; i < this.mChildren.Count; i++)
			{
				if (this.mChildren[i].mSpriteInst != null)
				{
					this.mChildren[i].mSpriteInst.Dispose();
				}
			}
			while (this.mParticleEffectVector.Count > 0)
			{
				if (this.mParticleEffectVector[this.mParticleEffectVector.Count - 1].mEffect != null)
				{
					this.mParticleEffectVector[this.mParticleEffectVector.Count - 1].mEffect.Dispose();
				}
				this.mParticleEffectVector.RemoveAt(this.mParticleEffectVector.Count - 1);
			}
		}

		public PAObjectInst GetObjectInst(string theName)
		{
			string theName2 = "";
			int num = theName.IndexOf('\\');
			string text;
			if (num != -1)
			{
				text = theName.Substring(0, num);
				theName2 = theName.Substring(num + 1);
			}
			else
			{
				text = theName;
			}
			int i = 0;
			while (i < this.mChildren.Count)
			{
				PAObjectInst paobjectInst = this.mChildren[i];
				if (paobjectInst.mName != null && paobjectInst.mName == text)
				{
					if (num == -1)
					{
						return paobjectInst;
					}
					if (paobjectInst.mSpriteInst == null)
					{
						return null;
					}
					return paobjectInst.mSpriteInst.GetObjectInst(theName2);
				}
				else
				{
					i++;
				}
			}
			return null;
		}

		public PASpriteInst mParent;

		public int mDelayFrames;

		public float mFrameNum;

		public int mFrameRepeats;

		public bool mOnNewFrame;

		public int mLastUpdated;

		public PATransform mCurTransform = new PATransform();

		public SexyColor mCurColor = default(SexyColor);

		public List<PAObjectInst> mChildren = new List<PAObjectInst>();

		public PASpriteDef mDef;

		public List<PAParticleEffect> mParticleEffectVector = new List<PAParticleEffect>();
	}
}
