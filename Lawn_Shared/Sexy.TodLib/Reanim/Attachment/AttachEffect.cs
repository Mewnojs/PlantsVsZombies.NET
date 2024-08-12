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
            Reset();
            AttachEffect.unusedObjects.Push(this);
        }

        private AttachEffect()
        {
            Reset();
        }

        public void Reset()
        {
            mEffectID = null;
            mEffectType = EffectType.Particle;
            mOffset = default(SexyTransform2D);
            mDontDrawIfParentHidden = false;
            mDontPropogateColor = false;
        }

        public object mEffectID;

        public int mEffectID_Save;

        public EffectType mEffectType;

        public SexyTransform2D mOffset = default(SexyTransform2D);

        public bool mDontDrawIfParentHidden;

        public bool mDontPropogateColor;

        private static Stack<AttachEffect> unusedObjects = new Stack<AttachEffect>(100);
    }
}
