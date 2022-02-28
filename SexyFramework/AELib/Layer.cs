using System;
using Sexy.GraphicsLib;
using Sexy.Misc;

namespace Sexy.AELib
{
	public class Layer
	{
		public Layer()
		{
		}

		public Layer(Layer other)
		{
			this.CopyFrom(other);
		}

		public void CopyFrom(Layer rhs)
		{
			this.mAdditive = rhs.mAdditive;
			this.mLayerName = rhs.mLayerName;
			this.mWidth = rhs.mWidth;
			this.mHeight = rhs.mHeight;
			this.mImage = rhs.mImage;
			this.mXOff = rhs.mXOff;
			this.mYOff = rhs.mYOff;
			this.mAnchorPoint = new Timeline(rhs.mAnchorPoint);
			this.mPosition = new Timeline(rhs.mPosition);
			this.mScale = new Timeline(rhs.mScale);
			this.mRotation = new Timeline(rhs.mRotation);
			this.mOpacity = new Timeline(rhs.mOpacity);
		}

		public virtual void Reset()
		{
			this.mAnchorPoint.Reset();
			this.mPosition.Reset();
			this.mScale.Reset();
			this.mRotation.Reset();
			this.mOpacity.Reset();
		}

		public void AddAnchorPoint(int frame, float x, float y)
		{
			this.mAnchorPoint.AddKeyframe(frame, x, y);
		}

		public void AddPosition(int frame, float x, float y)
		{
			this.mPosition.AddKeyframe(frame, x, y);
		}

		public void AddScale(int frame, float sx, float sy)
		{
			this.mScale.AddKeyframe(frame, sx, sy);
		}

		public void AddRotation(int frame, float angle_radians)
		{
			this.mRotation.AddKeyframe(frame, angle_radians);
		}

		public void AddOpacity(int frame, float pct)
		{
			this.mOpacity.AddKeyframe(frame, pct);
		}

		public void EnsureTimelineDefaults(long comp_width, long comp_height)
		{
			if (!this.mOpacity.HasInitialValue())
			{
				this.mOpacity.AddKeyframe(0, 1f);
			}
			if (!this.mRotation.HasInitialValue())
			{
				this.mRotation.AddKeyframe(0, 0f);
			}
			if (!this.mScale.HasInitialValue())
			{
				this.mScale.AddKeyframe(0, 1f, 1f);
			}
			if (!this.mPosition.HasInitialValue())
			{
				this.mPosition.AddKeyframe(0, (float)comp_width / 2f, (float)comp_height / 2f);
			}
			if (this.mImage != null && this.mImage.GetImage() != null && !this.mAnchorPoint.HasInitialValue())
			{
				this.mAnchorPoint.AddKeyframe(0, (float)this.mImage.GetImage().GetCelWidth() / 2f, (float)this.mImage.GetImage().GetCelHeight() / 2f);
			}
		}

		public virtual Layer Duplicate()
		{
			return new Layer(this);
		}

		public virtual Image GetImage()
		{
			return this.mImage.GetImage();
		}

		public virtual bool IsLayerBase()
		{
			return true;
		}

		public virtual bool NeedsTranslatedFrame()
		{
			return false;
		}

		public void SetImage(SharedImageRef img)
		{
			this.mImage = img;
		}

		public virtual void Draw(Graphics g)
		{
			this.Draw(g, null);
		}

		public virtual void Draw(Graphics g, CumulativeTransform ctrans)
		{
			this.Draw(g, ctrans, -1);
		}

		public virtual void Draw(Graphics g, CumulativeTransform ctrans, int frame)
		{
			this.Draw(g, ctrans, frame, 1f);
		}

		public virtual bool isValid()
		{
			return this.mImage.mSharedImage != null || this.mImage.mUnsharedImage != null;
		}

		public virtual void Draw(Graphics g, CumulativeTransform ctrans, int frame, float scale)
		{
			float num = 0f;
			this.mOpacity.GetValue(frame, ref num);
			float num2 = 255f * num;
			if (ctrans != null)
			{
				num2 *= ctrans.mOpacity;
			}
			if (num2 <= 0f)
			{
				return;
			}
			if (num2 != 255f)
			{
				g.SetColorizeImages(true);
				g.SetColor(255, 255, 255, (int)num2);
			}
			float num3 = 0f;
			float num4 = 0f;
			this.mAnchorPoint.GetValue(frame, ref num3, ref num4);
			num3 *= scale;
			num4 *= scale;
			int num5 = this.mImage.mWidth / 2;
			int num6 = this.mImage.mHeight / 2;
			num3 -= (float)num5;
			num4 -= (float)num6;
			float sx = 0f;
			float sy = 0f;
			this.mScale.GetValue(frame, ref sx, ref sy);
			float num7 = 0f;
			this.mRotation.GetValue(frame, ref num7);
			float num8 = 0f;
			float num9 = 0f;
			this.mPosition.GetValue(frame, ref num8, ref num9);
			num8 *= scale;
			num9 *= scale;
			SexyTransform2D sexyTransform2D = new SexyTransform2D(false);
			sexyTransform2D.Translate(-num3 + (float)this.mXOff, -num4 + (float)this.mYOff);
			sexyTransform2D.Scale(sx, sy);
			if (num7 != 0f)
			{
				sexyTransform2D.RotateRad(-num7);
			}
			sexyTransform2D.Translate(num8, num9);
			if (this.mAdditive || ctrans.mForceAdditive)
			{
				g.SetDrawMode(1);
			}
			sexyTransform2D = ctrans.mTrans * sexyTransform2D;
			g.DrawImageMatrix(this.mImage.GetImage(), sexyTransform2D);
			g.SetDrawMode(0);
			g.SetColorizeImages(false);
		}

		protected SharedImageRef mImage;

		public Timeline mAnchorPoint = new Timeline();

		public Timeline mPosition = new Timeline();

		public Timeline mScale = new Timeline();

		public Timeline mRotation = new Timeline();

		public Timeline mOpacity = new Timeline();

		public string mLayerName;

		public int mWidth;

		public int mHeight;

		public int mXOff;

		public int mYOff;

		public bool mAdditive;
	}
}
