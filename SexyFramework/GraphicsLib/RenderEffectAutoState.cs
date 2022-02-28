using System;

namespace Sexy.GraphicsLib
{
	public class RenderEffectAutoState
	{
		public RenderEffectAutoState(Graphics inGraphics, RenderEffect inEffect)
			: this(inGraphics, inEffect, 1)
		{
		}

		public RenderEffectAutoState(Graphics inGraphics)
			: this(inGraphics, null, 1)
		{
		}

		public RenderEffectAutoState()
			: this(null, null, 1)
		{
		}

		public RenderEffectAutoState(Graphics inGraphics, RenderEffect inEffect, int inDefaultPassCount)
		{
			this.mEffect = inEffect;
			this.mPassCount = inDefaultPassCount;
			this.mCurrentPass = 0;
			if (this.mEffect == null)
			{
				return;
			}
			this.mPassCount = this.mEffect.Begin(out this.mRunHandle, (inGraphics != null) ? inGraphics.GetRenderContext() : new HRenderContext());
			if (this.mCurrentPass < this.mPassCount)
			{
				this.mEffect.BeginPass(this.mRunHandle, this.mCurrentPass);
			}
		}

		public virtual void Dispose()
		{
			if (this.mEffect == null)
			{
				return;
			}
			if (this.mCurrentPass < this.mPassCount)
			{
				this.mEffect.EndPass(this.mRunHandle, this.mCurrentPass);
			}
			this.mEffect.End(this.mRunHandle);
		}

		public void Reset(Graphics inGraphics, RenderEffect inEffect)
		{
			this.Reset(inGraphics, inEffect, 1);
		}

		public void Reset(Graphics inGraphics)
		{
			this.Reset(inGraphics, null, 1);
		}

		public void Reset()
		{
			this.Reset(null, null, 1);
		}

		public void Reset(Graphics inGraphics, RenderEffect inEffect, int inDefaultPassCount)
		{
			if (this.mEffect != null)
			{
				if (this.mCurrentPass < this.mPassCount)
				{
					this.mEffect.EndPass(this.mRunHandle, this.mCurrentPass);
				}
				this.mEffect.End(this.mRunHandle);
			}
			this.mEffect = inEffect;
			this.mPassCount = inDefaultPassCount;
			this.mCurrentPass = 0;
			if (this.mEffect != null)
			{
				this.mPassCount = this.mEffect.Begin(out this.mRunHandle, (inGraphics != null) ? inGraphics.GetRenderContext() : new HRenderContext(null));
				if (this.mCurrentPass < this.mPassCount)
				{
					this.mEffect.BeginPass(this.mRunHandle, this.mCurrentPass);
				}
			}
		}

		public void NextPass()
		{
			if (this.mEffect != null && this.mCurrentPass < this.mPassCount)
			{
				this.mEffect.EndPass(this.mRunHandle, this.mCurrentPass);
			}
			this.mCurrentPass++;
			if (this.mEffect != null && this.mCurrentPass < this.mPassCount)
			{
				this.mEffect.BeginPass(this.mRunHandle, this.mCurrentPass);
			}
		}

		public bool IsDone()
		{
			return this.mCurrentPass >= this.mPassCount;
		}

		public bool PassUsesVertexShader()
		{
			return this.mEffect != null && this.mEffect.PassUsesVertexShader(this.mCurrentPass);
		}

		public bool PassUsesPixelShader()
		{
			return this.mEffect != null && this.mEffect.PassUsesPixelShader(this.mCurrentPass);
		}

		public static implicit operator bool(RenderEffectAutoState ImpliedObject)
		{
			return !ImpliedObject.IsDone();
		}

		protected RenderEffect mEffect;

		protected object mRunHandle;

		protected int mPassCount;

		protected int mCurrentPass;
	}
}
