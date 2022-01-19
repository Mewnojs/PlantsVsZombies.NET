using System;

namespace Sexy
{
    internal class EaseFunction
    {
        public EaseFunction()
        {
            setup(1f, 1f, 0.5f, 0.5f);
        }

        public EaseFunction(float v0, float v1, float a, float b)
        {
            setup(v0, v1, a, b);
        }

        public void setup(float v0, float v1, float a, float b)
        {
            m_v0 = v0;
            m_v1 = v1;
            m_a = a;
            m_b = b;
            m_vc = (2f - m_a * m_v0 - (1f - m_b) * m_v1) / (m_b - m_a + 1f);
            m_afactor = ((0f == m_a) ? 0f : ((m_vc - m_v0) / (2f * m_a)));
            m_bfactor = ((1f == m_b) ? 0f : ((m_v1 - m_vc) / (2f * (1f - m_b))));
            m_atotal = m_a * (m_a * m_afactor + m_v0);
            m_btotal = (m_b - m_a) * m_vc + m_atotal;
        }

        public float Tick(float t)
        {
            if (t < m_a)
            {
                return t * (t * m_afactor + m_v0);
            }
            if (t < m_b)
            {
                return (t - m_a) * m_vc + m_atotal;
            }
            float num = t - m_b;
            return num * (num * m_bfactor + m_vc) + m_btotal;
        }

        public float Tick(float t, float tmax)
        {
            return Tick(t / tmax);
        }

        public float Tick(float t, float tmin, float tmax)
        {
            return Tick(t / (tmax - tmin));
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
