using System;

namespace Sexy
{
	internal class EaseFunction
	{
		public EaseFunction()
		{
			this.setup(1f, 1f, 0.5f, 0.5f);
		}

		public EaseFunction(float v0, float v1, float a, float b)
		{
			this.setup(v0, v1, a, b);
		}

		public void setup(float v0, float v1, float a, float b)
		{
			this.m_v0 = v0;
			this.m_v1 = v1;
			this.m_a = a;
			this.m_b = b;
			this.m_vc = (2f - this.m_a * this.m_v0 - (1f - this.m_b) * this.m_v1) / (this.m_b - this.m_a + 1f);
			this.m_afactor = ((0f == this.m_a) ? 0f : ((this.m_vc - this.m_v0) / (2f * this.m_a)));
			this.m_bfactor = ((1f == this.m_b) ? 0f : ((this.m_v1 - this.m_vc) / (2f * (1f - this.m_b))));
			this.m_atotal = this.m_a * (this.m_a * this.m_afactor + this.m_v0);
			this.m_btotal = (this.m_b - this.m_a) * this.m_vc + this.m_atotal;
		}

		public float Tick(float t)
		{
			if (t < this.m_a)
			{
				return t * (t * this.m_afactor + this.m_v0);
			}
			if (t < this.m_b)
			{
				return (t - this.m_a) * this.m_vc + this.m_atotal;
			}
			float num = t - this.m_b;
			return num * (num * this.m_bfactor + this.m_vc) + this.m_btotal;
		}

		public float Tick(float t, float tmax)
		{
			return this.Tick(t / tmax);
		}

		public float Tick(float t, float tmin, float tmax)
		{
			return this.Tick(t / (tmax - tmin));
		}

		public static readonly EaseFunction EaseNone = new EaseFunction(1f, 1f, 0.5f, 0.5f);

		public static readonly EaseFunction EaseIn = new EaseFunction(0f, 1f, 1f, 1f);

		public static readonly EaseFunction EaseOut = new EaseFunction(1f, 0f, 0f, 0f);

		public static readonly EaseFunction EaseInOut = new EaseFunction(1f, 1f, 0.5f, 0.5f);

		private float m_v0;

		private float m_v1;

		private float m_a;

		private float m_b;

		private float m_vc;

		private float m_afactor;

		private float m_bfactor;

		private float m_atotal;

		private float m_btotal;
	}
}
