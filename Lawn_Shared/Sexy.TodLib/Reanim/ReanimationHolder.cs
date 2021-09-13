using System;
using System.Collections.Generic;

namespace Sexy.TodLib
{
	public/*internal*/ class ReanimationHolder
	{
		public void InitializeHolder()
		{
		}

		public void DisposeHolder()
		{
		}

		public Reanimation AllocReanimation(float theX, float theY, int theRenderOrder, ReanimationType theReanimationType)
		{
			Reanimation newReanimation = Reanimation.GetNewReanimation();
			newReanimation.mReanimationHolder = this;
			newReanimation.mRenderOrder = theRenderOrder;
			newReanimation.ReanimationInitializeType(theX, theY, theReanimationType);
			newReanimation.mActive = true;
			this.mReanimations.Add(newReanimation);
			return newReanimation;
		}

		public List<Reanimation> mReanimations = new List<Reanimation>();
	}
}
