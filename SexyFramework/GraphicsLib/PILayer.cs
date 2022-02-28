using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Sexy.Misc;

namespace Sexy.GraphicsLib
{
	public class PILayer
	{
		public PILayer()
		{
			this.mVisible = true;
			this.mColor = new SexyColor(SexyColor.White);
			this.mBkgImage = null;
			this.mBkgTransform.LoadIdentity();
			this.mEmitterInstanceVector.Capacity = 10;
		}

		public void SetVisible(bool isVisible)
		{
			this.mVisible = isVisible;
		}

		public PIEmitterInstance GetEmitter()
		{
			return this.GetEmitter(0);
		}

		public PIEmitterInstance GetEmitter(int theIdx)
		{
			if (theIdx < this.mEmitterInstanceVector.Count)
			{
				return this.mEmitterInstanceVector[theIdx];
			}
			return null;
		}

		public PIEmitterInstance GetEmitter(string theName)
		{
			for (int i = 0; i < this.mEmitterInstanceVector.Count; i++)
			{
				if (theName.Length == 0 || this.mEmitterInstanceVector[i].mEmitterInstanceDef.mName == theName)
				{
					return this.mEmitterInstanceVector[i];
				}
			}
			return null;
		}

		public PILayerDef mLayerDef;

		public List<PIEmitterInstance> mEmitterInstanceVector = new List<PIEmitterInstance>();

		public bool mVisible;

		public SexyColor mColor = default(SexyColor);

		public DeviceImage mBkgImage;

		public Vector2 mBkgImgDrawOfs = default(Vector2);

		public SexyTransform2D mBkgTransform = new SexyTransform2D(false);
	}
}
