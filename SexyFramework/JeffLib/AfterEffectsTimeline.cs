using System;
using System.Collections.Generic;
using Sexy;
using Sexy.GraphicsLib;
using Sexy.Misc;

namespace JeffLib
{
	public class AfterEffectsTimeline
	{
		public void AddPosX(Component x)
		{
			x.mStartFrame += this.mStartFrame;
			x.mEndFrame += this.mStartFrame;
			this.mPosX.Add(x);
		}

		public void AddPosY(Component y)
		{
			y.mStartFrame += this.mStartFrame;
			y.mEndFrame += this.mStartFrame;
			this.mPosY.Add(y);
		}

		public void AddScaleX(Component c)
		{
			c.mStartFrame += this.mStartFrame;
			c.mEndFrame += this.mStartFrame;
			this.mScaleX.Add(c);
		}

		public void AddScaleY(Component c)
		{
			c.mStartFrame += this.mStartFrame;
			c.mEndFrame += this.mStartFrame;
			this.mScaleY.Add(c);
		}

		public void AddAngle(Component c)
		{
			c.mStartFrame += this.mStartFrame;
			c.mEndFrame += this.mStartFrame;
			this.mAngle.Add(c);
		}

		public void AddOpacity(Component c)
		{
			c.mStartFrame += this.mStartFrame;
			c.mEndFrame += this.mStartFrame;
			this.mOpacity.Add(c);
		}

		public void Update()
		{
			if (this.mUpdateCount <= this.mEndFrame)
			{
				this.mUpdateCount++;
			}
			if (this.mUpdateCount < this.mStartFrame || this.mUpdateCount > this.mEndFrame)
			{
				return;
			}
			Component.UpdateComponentVec(this.mPosX, this.mUpdateCount);
			Component.UpdateComponentVec(this.mPosY, this.mUpdateCount);
			Component.UpdateComponentVec(this.mScaleX, this.mUpdateCount);
			Component.UpdateComponentVec(this.mScaleY, this.mUpdateCount);
			Component.UpdateComponentVec(this.mAngle, this.mUpdateCount);
			Component.UpdateComponentVec(this.mOpacity, this.mUpdateCount);
		}

		public void Draw(Graphics g, int force_alpha)
		{
			if (this.mImage == null)
			{
				return;
			}
			if (this.mUpdateCount < this.mStartFrame || (this.mUpdateCount > this.mEndFrame && !this.mHoldLastFrame))
			{
				return;
			}
			int num = (int)(Component.GetComponentValue(this.mOpacity, 1f, this.mUpdateCount) * this.mOverallAlphaPct * 255f);
			if (num <= 0)
			{
				return;
			}
			if (num > 255)
			{
				num = 255;
			}
			if (force_alpha >= 0)
			{
				num = force_alpha;
			}
			if (num != 255)
			{
				g.SetColorizeImages(true);
				g.SetColor(255, 255, 255, num);
			}
			Transform transform = new Transform();
			float componentValue = Component.GetComponentValue(this.mAngle, 0f, this.mUpdateCount);
			if (componentValue != 0f)
			{
				transform.RotateRad(componentValue);
			}
			float num3;
			float num2 = (num3 = Component.GetComponentValue(this.mScaleX, 1f, this.mUpdateCount) * this.mOverallXScale);
			if (this.mScaleY.size<Component>() > 0)
			{
				num2 = Component.GetComponentValue(this.mScaleY, 1f, this.mUpdateCount) * this.mOverallYScale;
			}
			if (this.mMirror)
			{
				num3 *= -1f;
			}
			if (!Sexy.Common._eq(num3, 1f) || !Sexy.Common._eq(num2, 1f))
			{
				transform.Scale(num3, num2);
			}
			float componentValue2 = Component.GetComponentValue(this.mPosX, 0f, this.mUpdateCount);
			float componentValue3 = Component.GetComponentValue(this.mPosY, 0f, this.mUpdateCount);
			Rect celRect = this.mImage.GetCelRect(this.mCel);
			if (g.Is3D())
			{
				g.DrawImageTransformF(this.mImage, transform, celRect, componentValue2, componentValue3);
			}
			else
			{
				g.DrawImageTransform(this.mImage, transform, celRect, componentValue2, componentValue3);
			}
			g.SetColorizeImages(false);
		}

		public void Draw(Graphics g)
		{
			this.Draw(g, -1);
		}

		public bool Done()
		{
			return this.mUpdateCount > this.mEndFrame;
		}

		public void Reset()
		{
			this.mUpdateCount = 0;
			for (int i = 0; i < this.mPosX.size<Component>(); i++)
			{
				this.mPosX[i].mValue = this.mPosX[i].mOriginalValue;
			}
			for (int j = 0; j < this.mPosY.size<Component>(); j++)
			{
				this.mPosY[j].mValue = this.mPosY[j].mOriginalValue;
			}
			for (int k = 0; k < this.mScaleX.size<Component>(); k++)
			{
				this.mScaleX[k].mValue = this.mScaleX[k].mOriginalValue;
			}
			for (int l = 0; l < this.mScaleY.size<Component>(); l++)
			{
				this.mScaleY[l].mValue = this.mScaleY[l].mOriginalValue;
			}
			for (int m = 0; m < this.mAngle.size<Component>(); m++)
			{
				this.mAngle[m].mValue = this.mAngle[m].mOriginalValue;
			}
			for (int n = 0; n < this.mOpacity.size<Component>(); n++)
			{
				this.mOpacity[n].mValue = this.mOpacity[n].mOriginalValue;
			}
		}

		public int GetUpdateCount()
		{
			return this.mUpdateCount;
		}

		protected int mUpdateCount;

		protected List<Component> mPosX = new List<Component>();

		protected List<Component> mPosY = new List<Component>();

		protected List<Component> mScaleX = new List<Component>();

		protected List<Component> mScaleY = new List<Component>();

		protected List<Component> mAngle = new List<Component>();

		protected List<Component> mOpacity = new List<Component>();

		public Image mImage;

		public float mOverallAlphaPct = 1f;

		public float mOverallXScale = 1f;

		public float mOverallYScale = 1f;

		public int mCel;

		public bool mMirror;

		public bool mHoldLastFrame;

		public int mStartFrame;

		public int mEndFrame;
	}
}
