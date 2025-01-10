using System;
using System.Collections.Generic;

namespace Sexy.TodLib
{
    public/*internal*/ class ReanimationHolder
    {
        public void InitializeHolder()
        {
            DUMMY = Reanimation.GetNewReanimation();
            DUMMY.mDefinition = new ReanimatorDefinition();
        }

        public void DisposeHolder()
        {
            DUMMY.ReanimationDie();
            DUMMY.ReanimationDelete();
            DUMMY = null;
        }

        public Reanimation AllocReanimation(float theX, float theY, int theRenderOrder, ReanimationType theReanimationType)
        {
            Reanimation newReanimation = Reanimation.GetNewReanimation();
            newReanimation.mReanimationHolder = this;
            newReanimation.mRenderOrder = theRenderOrder;
            newReanimation.ReanimationInitializeType(theX, theY, theReanimationType);
            newReanimation.mActive = true;
            mReanimations.Add(newReanimation);
            return newReanimation;
        }

        public List<Reanimation> mReanimations = new List<Reanimation>();

        public Reanimation DUMMY;
    }
}
