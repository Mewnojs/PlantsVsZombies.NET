using System;
using System.Collections.Generic;

namespace Sexy.TodLib
{
	public/*internal*/ class AttachEffect
	{
		public static AttachEffect GetNewAttachEffect()
		{
			if (AttachEffect.unusedObjects.Count > 0)
			{
				return AttachEffect.unusedObjects.Pop();
			}
			return new AttachEffect();
		}

		public void PrepareForReuse()
		{
			this.Reset();
			AttachEffect.unusedObjects.Push(this);
		}

		private AttachEffect()
		{
			this.Reset();
		}

		public void Reset()
		{
			this.mEffectID = null;
			this.mEffectType = EffectType.EFFECT_PARTICLE;
			this.mOffset = default(SexyTransform2D);
			this.mDontDrawIfParentHidden = false;
			this.mDontPropogateColor = false;
		}

		public object mEffectID;

		public EffectType mEffectType;

		public SexyTransform2D mOffset = default(SexyTransform2D);

		public bool mDontDrawIfParentHidden;

		public bool mDontPropogateColor;

		private static Stack<AttachEffect> unusedObjects = new Stack<AttachEffect>(100);
	}
}
