using System;
using Microsoft.Xna.Framework;
using Sexy.GraphicsLib;

namespace Sexy.Misc
{
	public class Bezier
	{
		protected void SubdivideRender(Graphics g, Vector2 P0, Vector2 P1, Vector2 P2, Vector2 P3)
		{
			g.FillRect((int)P0.X, (int)P0.Y, 1, 1);
			float num = 0f;
			if (Common._eq(Common.DistFromPointToLine(P0, P3, P1, ref num), 0f) && Common._eq(Common.DistFromPointToLine(P0, P3, P2, ref num), 0f))
			{
				return;
			}
			Vector2 vector = (P0 + P1) * 0.5f;
			Vector2 vector2 = (P1 + P2) * 0.5f;
			Vector2 vector3 = (vector + vector2) * 0.5f;
			Vector2 vector4 = (P2 + P3) * 0.5f;
			Vector2 vector5 = (vector2 + vector4) * 0.5f;
			Vector2 vector6 = (vector3 + vector5) * 0.5f;
			this.SubdivideRender(g, P0, vector, vector3, vector6);
			this.SubdivideRender(g, vector6, vector5, vector4, P3);
		}

		protected float SubdivideLength(Vector2 P0, Vector2 P1, Vector2 P2, Vector2 P3)
		{
			float num = Bezier.Distance(P0, P3, true);
			float num2 = Bezier.Distance(P0, P1, true) + Bezier.Distance(P1, P2, true) + Bezier.Distance(P2, P3, true);
			float num3 = num - num2;
			if (num3 * num3 < 0.001f)
			{
				return 0.5f * (num + num2);
			}
			Vector2 vector = (P0 + P1) * 0.5f;
			Vector2 vector2 = (P1 + P2) * 0.5f;
			Vector2 vector3 = (vector + vector2) * 0.5f;
			Vector2 vector4 = (P2 + P3) * 0.5f;
			Vector2 vector5 = (vector2 + vector4) * 0.5f;
			Vector2 vector6 = (vector3 + vector5) * 0.5f;
			return this.SubdivideLength(P0, vector, vector3, vector6) + this.SubdivideLength(vector6, vector5, vector4, P3);
		}

		protected float SegmentArcLength(int i, float u1, float u2)
		{
			if (u2 <= u1)
			{
				return 0f;
			}
			if (u1 < 0f)
			{
				u1 = 0f;
			}
			if (u2 > 1f)
			{
				u2 = 1f;
			}
			Vector2 vector = this.mPositions[i];
			Vector2 vector2 = this.mControls[2 * i];
			Vector2 vector3 = this.mControls[2 * i + 1];
			Vector2 vector4 = this.mPositions[i + 1];
			float num = 1f - u2;
			Vector2 vector5 = vector * num + vector2 * u2;
			Vector2 vector6 = vector2 * num + vector3 * u2;
			Vector2 vector7 = vector5 * num + vector6 * u2;
			Vector2 vector8 = vector7 * num + (vector6 * num + (vector3 * num + vector4 * u2) * u2) * u2;
			float num2 = 1f - u1;
			vector6 = vector5 * num2 + vector7 * u1;
			Vector2 p = vector8;
			Vector2 vector9 = vector7 * num2 + vector8 * u1;
			Vector2 vector10 = vector6 * num2 + vector9 * u1;
			Vector2 p2 = ((vector * num2 + vector5 * u1) * num2 + vector6 * u1) * num2 + vector10 * u1;
			return this.SubdivideLength(p2, vector10, vector9, p);
		}

		public Bezier()
		{
			this.mTimes = null;
			this.mLengths = null;
			this.mTotalLength = 0f;
			this.mCount = 0;
			this.mControls = null;
			this.mPositions = null;
			this.mCurveDetail = -1;
			this.mImage = null;
			this.mImageX = 0;
			this.mImageY = 0;
		}

		public Bezier(Bezier rhs)
			: this()
		{
			this.CopyFrom(rhs);
		}

		public virtual void Dispose()
		{
			this.Clean();
		}

		public void CopyFrom(Bezier rhs)
		{
			if (this == rhs || rhs == null)
			{
				return;
			}
			this.Clean();
			this.mCount = rhs.mCount;
			this.mTotalLength = rhs.mTotalLength;
			this.mImageX = rhs.mImageX;
			this.mImageY = rhs.mImageY;
			this.mCurveDetail = rhs.mCurveDetail;
			this.mCurveColor = new SexyColor(rhs.mCurveColor);
			if (rhs.mImage != null)
			{
				this.mImage = new MemoryImage(rhs.mImage);
			}
			if (this.mCount > 0)
			{
				this.mTimes = new float[this.mCount];
				this.mPositions = new Vector2[this.mCount];
				this.mControls = new Vector2[2 * (this.mCount - 1)];
				this.mLengths = new float[this.mCount - 1];
				for (int i = 0; i < 2 * (this.mCount - 1); i++)
				{
					if (i < this.mCount)
					{
						this.mTimes[i] = rhs.mTimes[i];
						this.mPositions[i] = rhs.mPositions[i];
					}
					if (i < this.mCount - 1)
					{
						this.mLengths[i] = rhs.mLengths[i];
					}
					this.mControls[i] = rhs.mControls[i];
				}
			}
		}

		public bool Init(Vector2[] positions, Vector2[] controls, float[] times, int count)
		{
			if (this.mCount != 0)
			{
				return false;
			}
			if (count < 2 || positions == null || times == null || controls == null)
			{
				return false;
			}
			this.mPositions = new Vector2[count];
			this.mControls = new Vector2[2 * (count - 1)];
			this.mTimes = new float[count];
			this.mCount = count;
			for (int i = 0; i < count; i++)
			{
				this.mPositions[i] = positions[i];
				this.mTimes[i] = times[i];
			}
			for (int j = 0; j < 2 * (count - 1); j++)
			{
				this.mControls[j] = controls[j];
			}
			this.mLengths = new float[count - 1];
			this.mTotalLength = 0f;
			for (int k = 0; k < count - 1; k++)
			{
				this.mLengths[k] = this.SegmentArcLength(k, 0f, 1f);
				this.mTotalLength += this.mLengths[k];
			}
			return true;
		}

		public bool Init(Vector2[] positions, float[] times, int count)
		{
			if (this.mCount != 0)
			{
				return false;
			}
			if (count < 2 || positions == null || times == null)
			{
				return false;
			}
			this.mPositions = new Vector2[count];
			this.mControls = new Vector2[2 * (count - 1)];
			this.mTimes = new float[count];
			this.mCount = count;
			for (int i = 0; i < count; i++)
			{
				this.mPositions[i] = positions[i];
				this.mTimes[i] = times[i];
			}
			for (int j = 0; j < count - 1; j++)
			{
				if (j > 0)
				{
					this.mControls[2 * j] = this.mPositions[j] + (this.mPositions[j + 1] - this.mPositions[j - 1]) / 3f;
				}
				if (j < count - 2)
				{
					this.mControls[2 * j + 1] = this.mPositions[j + 1] - (this.mPositions[j + 2] - this.mPositions[j]) / 3f;
				}
			}
			this.mControls[0] = this.mControls[1] - (this.mPositions[1] - this.mPositions[0]) / 3f;
			this.mControls[2 * count - 3] = this.mControls[2 * count - 4] + (this.mPositions[count - 1] - this.mPositions[count - 2]) / 3f;
			this.mLengths = new float[count - 1];
			this.mTotalLength = 0f;
			for (int k = 0; k < count - 1; k++)
			{
				this.mLengths[k] = this.SegmentArcLength(k, 0f, 1f);
				this.mTotalLength += this.mLengths[k];
			}
			return true;
		}

		public Vector2 Evaluate(float t)
		{
			if (this.mCount < 2)
			{
				return new Vector2(0f, 0f);
			}
			if (t <= this.mTimes[0])
			{
				return this.mPositions[0];
			}
			if (t >= this.mTimes[this.mCount - 1])
			{
				return this.mPositions[this.mCount - 1];
			}
			int num = 0;
			while (num < this.mCount - 1 && t >= this.mTimes[num + 1])
			{
				num++;
			}
			float num2 = this.mTimes[num];
			float num3 = this.mTimes[num + 1];
			float num4 = (t - num2) / (num3 - num2);
			Vector2 vector = this.mPositions[num + 1] - this.mControls[2 * num + 1] * 3f + this.mControls[2 * num] * 3f - this.mPositions[num];
			Vector2 vector2 = this.mControls[2 * num + 1] * 3f - this.mControls[2 * num] * 6f + this.mPositions[num] * 3f;
			Vector2 vector3 = this.mControls[2 * num] * 3f - this.mPositions[num] * 3f;
			return this.mPositions[num] + (vector3 + (vector2 + vector * num4) * num4) * num4;
		}

		public void Serialize(SexyBuffer b)
		{
			b.WriteFloat(this.mTotalLength);
			b.WriteLong((long)this.mCount);
			b.WriteLong((long)this.mCurveDetail);
			b.WriteLong((long)this.mCurveColor.ToInt());
			for (int i = 0; i < this.mCount; i++)
			{
				b.WriteFloat(this.mTimes[i]);
				b.WriteFloat(this.mPositions[i].X);
				b.WriteFloat(this.mPositions[i].Y);
			}
			for (int j = 0; j < 2 * (this.mCount - 1); j++)
			{
				b.WriteFloat(this.mControls[j].X);
				b.WriteFloat(this.mControls[j].Y);
			}
			for (int k = 0; k < this.mCount - 1; k++)
			{
				b.WriteFloat(this.mLengths[k]);
			}
		}

		public void Deserialize(SexyBuffer b)
		{
			this.Clean();
			this.mTotalLength = b.ReadFloat();
			this.mCount = (int)b.ReadLong();
			this.mCurveDetail = (int)b.ReadLong();
			this.mCurveColor = new SexyColor((int)b.ReadLong());
			if (this.mCount > 0)
			{
				this.mTimes = new float[this.mCount];
				this.mPositions = new Vector2[this.mCount];
				this.mControls = new Vector2[2 * (this.mCount - 1)];
				this.mLengths = new float[this.mCount - 1];
				for (int i = 0; i < this.mCount; i++)
				{
					this.mTimes[i] = b.ReadFloat();
					this.mPositions[i].X = b.ReadFloat();
					this.mPositions[i].Y = b.ReadFloat();
				}
				for (int j = 0; j < 2 * (this.mCount - 1); j++)
				{
					this.mControls[j].X = b.ReadFloat();
					this.mControls[j].Y = b.ReadFloat();
				}
				for (int k = 0; k < this.mCount - 1; k++)
				{
					this.mLengths[k] = b.ReadFloat();
				}
			}
			if (this.mCurveDetail > 0)
			{
				this.GenerateCurveImage(this.mCurveColor, this.mCurveDetail);
			}
		}

		public void GenerateCurveImage(SexyColor curve_color, int detail)
		{
			this.mCurveDetail = detail;
			this.mCurveColor = curve_color;
			this.mImage = null;
			this.mImage = new MemoryImage();
			float num = float.MaxValue;
			float num2 = float.MaxValue;
			float num3 = 0f;
			float num4 = 0f;
			float num5 = 0f;
			for (int i = 0; i < this.mCount; i++)
			{
				num5 += this.mTimes[i];
			}
			Vector2[] array = new Vector2[detail];
			for (int j = 0; j < detail; j++)
			{
				float num6 = (float)j / (float)detail;
				array[j] = this.Evaluate(num6 * num5);
				if (array[j].X < num)
				{
					num = array[j].X;
				}
				if (array[j].X > num3)
				{
					num3 = array[j].X;
				}
				if (array[j].Y < num2)
				{
					num2 = array[j].Y;
				}
				if (array[j].Y > num4)
				{
					num4 = array[j].Y;
				}
			}
			int num7 = (int)num3 - (int)num;
			int num8 = (int)num4 - (int)num2;
			this.mImageX = (int)num;
			this.mImageY = (int)num2;
			this.mImage.Create(num7, num8);
			this.mImage.Clear();
			Graphics graphics = new Graphics(this.mImage);
			graphics.SetColor(0, 0, 0, 0);
			graphics.FillRect(0, 0, num7, num8);
			graphics.SetColor(curve_color);
			for (int k = 0; k < detail; k++)
			{
				float num9 = array[k].X - num;
				if (num9 < 0f)
				{
					num9 = 0f;
				}
				else if (num9 >= (float)num7)
				{
					num9 = (float)(num7 - 1);
				}
				float num10 = array[k].Y - num2;
				if (num10 < 0f)
				{
					num10 = 0f;
				}
				else if (num10 >= (float)num8)
				{
					num10 = (float)(num8 - 1);
				}
				graphics.FillRect((int)num9, (int)num10, 1, 1);
			}
			graphics.ClearRenderContext();
		}

		public void Draw(Graphics g, float scale)
		{
			if (this.mImage == null)
			{
				g.SetColor(255, 0, 255);
				for (int i = 0; i < this.mCount - 1; i++)
				{
					this.SubdivideRender(g, this.mPositions[i], this.mControls[2 * i], this.mControls[2 * i + 1], this.mPositions[i + 1]);
				}
				g.FillRect((int)(this.mPositions[this.mCount - 1].X - 2f), (int)(this.mPositions[this.mCount - 1].Y - 2f), 4, 4);
				g.SetColor(255, 0, 0);
				for (int j = 0; j < this.mCount; j++)
				{
					g.FillRect((int)(this.mPositions[j].X - 2f), (int)(this.mPositions[j].Y - 2f), 4, 4);
				}
				g.SetColor(255, 255, 0);
				for (int k = 0; k < 2 * this.mCount - 2; k++)
				{
					g.FillRect((int)(this.mControls[k].X - 2f), (int)(this.mControls[k].Y - 2f), 4, 4);
				}
				return;
			}
			if (Common._eq(scale, 1f))
			{
				g.DrawImage(this.mImage, this.mImageX, this.mImageY);
				return;
			}
			g.DrawImage(this.mImage, (int)((float)this.mImageX * scale), (int)((float)this.mImageY * scale), (int)((float)this.mImage.mWidth * scale), (int)((float)this.mImage.mHeight * scale));
		}

		public Vector2 Velocity(float t, bool clamp)
		{
			if (this.mCount < 2)
			{
				return new Vector2(0f, 0f);
			}
			if (t <= this.mTimes[0])
			{
				if (!clamp)
				{
					return new Vector2(0f, 0f);
				}
				return this.mPositions[0];
			}
			else
			{
				if (t < this.mTimes[this.mCount - 1])
				{
					int num = 0;
					while (num < this.mCount - 1 && t >= this.mTimes[num + 1])
					{
						num++;
					}
					float num2 = this.mTimes[num];
					float num3 = this.mTimes[num + 1];
					float num4 = (t - num2) / (num3 - num2);
					Vector2 vector = this.mPositions[num + 1] - this.mControls[2 * num + 1] * 3f + this.mControls[2 * num] * 3f - this.mPositions[num];
					Vector2 vector2 = this.mControls[2 * num + 1] * 6f - this.mControls[2 * num] * 12f + this.mPositions[num] * 6f;
					Vector2 vector3 = this.mControls[2 * num] * 3f - this.mPositions[num] * 3f;
					return vector3 + (vector2 + vector * num4 * 3f) * num4;
				}
				if (!clamp)
				{
					return new Vector2(0f, 0f);
				}
				return this.mPositions[this.mCount - 1];
			}
		}

		public Vector2 Velocity(float t)
		{
			return this.Velocity(t, true);
		}

		public Vector2 Acceleration(float t)
		{
			if (this.mCount < 2)
			{
				return new Vector2(0f, 0f);
			}
			if (t <= this.mTimes[0])
			{
				return this.mPositions[0];
			}
			if (t >= this.mTimes[this.mCount - 1])
			{
				return this.mPositions[this.mCount - 1];
			}
			int num = 0;
			while (num < this.mCount - 1 && t >= this.mTimes[num + 1])
			{
				num++;
			}
			float num2 = this.mTimes[num];
			float num3 = this.mTimes[num + 1];
			float num4 = (t - num2) / (num3 - num2);
			Vector2 vector = this.mPositions[num + 1] - this.mControls[2 * num + 1] * 3f + this.mControls[2 * num] * 3f - this.mPositions[num];
			Vector2 vector2 = this.mControls[2 * num + 1] * 6f - this.mControls[2 * num] * 12f + this.mPositions[num] * 6f;
			return vector2 + vector * num4 * 6f;
		}

		public float ArcLength(float t1, float t2)
		{
			if (t2 <= t1)
			{
				return 0f;
			}
			if (t1 < this.mTimes[0])
			{
				t1 = this.mTimes[0];
			}
			if (t2 > this.mTimes[this.mCount - 1])
			{
				t2 = this.mTimes[this.mCount - 1];
			}
			int num = 0;
			while (num < this.mCount - 1 && t1 >= this.mTimes[num + 1])
			{
				num++;
			}
			float u = (t1 - this.mTimes[num]) / (this.mTimes[num + 1] - this.mTimes[num]);
			int num2 = 0;
			while (num2 < this.mCount - 1 && t2 > this.mTimes[num2 + 1])
			{
				num2++;
			}
			float u2 = (t2 - this.mTimes[num2]) / (this.mTimes[num2 + 1] - this.mTimes[num2]);
			float num3;
			if (num == num2)
			{
				num3 = this.SegmentArcLength(num, u, u2);
			}
			else
			{
				num3 = this.SegmentArcLength(num, u, 1f);
				for (int i = num + 1; i < num2; i++)
				{
					num3 += this.mLengths[i];
				}
				num3 += this.SegmentArcLength(num2, 0f, u2);
			}
			return num3;
		}

		public float GetTotalLength()
		{
			return this.mTotalLength;
		}

		public bool IsInitialized()
		{
			return this.mCount > 0;
		}

		public int GetNumPoints()
		{
			return this.mCount;
		}

		public void Clean()
		{
			this.mTimes = null;
			this.mLengths = null;
			this.mControls = null;
			this.mPositions = null;
			this.mImage = null;
			this.mCount = 0;
			this.mTotalLength = 0f;
		}

		private static float Distance(Vector2 p1, Vector2 p2, bool sqrt)
		{
			float num = p2.X - p1.X;
			float num2 = p2.Y - p1.Y;
			float num3 = num * num + num2 * num2;
			if (!sqrt)
			{
				return num3;
			}
			return (float)Math.Sqrt((double)num3);
		}

		public float[] mTimes;

		public float[] mLengths;

		public float mTotalLength;

		public int mCount;

		public Vector2[] mControls;

		public Vector2[] mPositions;

		public int mCurveDetail;

		public SexyColor mCurveColor = default(SexyColor);

		public MemoryImage mImage;

		public int mImageX;

		public int mImageY;
	}
}
