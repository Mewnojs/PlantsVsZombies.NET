using System;
using System.Collections.Generic;
using System.Linq;
using Sexy.GraphicsLib;
using Sexy.Misc;

namespace Sexy.PIL
{
	public class ColorKeyManager
	{
		public ColorKeyManager()
		{
			this.mColorMode = 0;
			this.mLife = 0;
			this.mUpdateCount = 0;
			this.mUpdateImagePosColor = false;
			this.mGradientRepeat = 1;
			this.mCurrentColor = new SexyColor(SexyColor.White);
		}

		public void CopyFrom(ColorKeyManager rhs)
		{
			if (rhs == null)
			{
				return;
			}
			this.mCurrentColor = rhs.mCurrentColor.Clone();
			this.mImage = rhs.mImage;
			this.mImgFileName = rhs.mImgFileName;
			this.mImgVariant = rhs.mImgVariant;
			this.mColorMode = rhs.mColorMode;
			this.mLife = rhs.mLife;
			this.mUpdateCount = rhs.mUpdateCount;
			this.mUpdateImagePosColor = rhs.mUpdateImagePosColor;
			this.mGradientRepeat = rhs.mGradientRepeat;
			this.mTimeline.Clear();
			this.mTimeline.AddRange(rhs.mTimeline.ToArray());
		}

		public virtual void Dispose()
		{
			this.mTimeline.Clear();
		}

		public void Serialize(SexyBuffer b)
		{
			b.WriteLong((long)this.mColorMode);
			b.WriteLong((long)this.mLife);
			b.WriteLong((long)this.mUpdateCount);
			b.WriteLong((long)this.mCurrentColor.ToInt());
			b.WriteLong((long)this.mTimeline.Count);
			for (int i = 0; i < this.mTimeline.Count; i++)
			{
				b.WriteFloat(this.mTimeline[i].first);
				this.mTimeline[i].second.Serialize(b);
			}
			b.WriteString(this.mImgFileName);
			b.WriteString(this.mImgVariant);
		}

		public void Deserialize(SexyBuffer b)
		{
			this.mColorMode = (int)b.ReadLong();
			this.mLife = (int)b.ReadLong();
			this.mUpdateCount = (int)b.ReadLong();
			long num = b.ReadLong();
			this.mCurrentColor = new SexyColor((int)num);
			int num2 = (int)b.ReadLong();
			this.mTimeline.Clear();
			for (int i = 0; i < num2; i++)
			{
				float pt = b.ReadFloat();
				ColorKey colorKey = new ColorKey();
				colorKey.Deserialize(b);
				this.mTimeline.Add(new ColorKeyTimeEntry(pt, colorKey));
			}
			this.mImgFileName = b.ReadString();
			this.mImgVariant = b.ReadString();
			if (this.mImgFileName.Length > 0)
			{
				bool flag = false;
				this.mImage = GlobalMembers.gSexyAppBase.GetSharedImage(this.mImgFileName, this.mImgVariant, ref flag, true, false);
			}
		}

		public void Update(float x, float y)
		{
			if (++this.mUpdateCount >= this.mLife || this.mColorMode == 0)
			{
				return;
			}
			if (this.mUpdateCount == 1 && this.mColorMode == 2)
			{
				int num = Common.Rand() % this.mTimeline.size<ColorKeyTimeEntry>();
				int num2 = num;
				if (this.mTimeline.size<ColorKeyTimeEntry>() > 1)
				{
					while (num2 == num)
					{
						num2 = Common.Rand() % this.mTimeline.size<ColorKeyTimeEntry>();
					}
				}
				this.mCurrentColor = this.mTimeline[num].second.GetInterpolatedColor(this.mTimeline[num2].second, Common.FloatRange(0f, 1f));
				return;
			}
			if (this.mUpdateCount == 1 && this.mColorMode == 3)
			{
				this.mCurrentColor = this.mTimeline[Common.Rand() % this.mTimeline.size<ColorKeyTimeEntry>()].second.GetColor();
				return;
			}
			if (this.mColorMode == 4)
			{
				if (this.mUpdateCount == 1)
				{
					return;
				}
				if (this.mUpdateImagePosColor)
				{
					return;
				}
			}
			if (this.mColorMode == 1)
			{
				float num3 = (float)this.mUpdateCount / (float)this.mLife;
				float num4 = 1f / (float)this.mGradientRepeat;
				float num5 = num3 - (float)((int)(num3 / num4)) * num4;
				num5 = Math.Max(Math.Min(num4, num5), 0f);
				int num6 = -1;
				int num7 = -1;
				for (int i = 0; i < this.mTimeline.size<ColorKeyTimeEntry>(); i++)
				{
					if (num5 < this.mTimeline[i].first / (float)this.mGradientRepeat)
					{
						num7 = i;
						break;
					}
					num6 = i;
				}
				if (num7 == -1)
				{
					num7 = num6;
				}
				float num8 = this.mTimeline[num6].first / (float)this.mGradientRepeat;
				float num9 = this.mTimeline[num7].first / (float)this.mGradientRepeat;
				float pct = (num5 - num8) / (num9 - num8);
				this.mCurrentColor = this.mTimeline[num6].second.GetInterpolatedColor(this.mTimeline[num7].second, pct);
			}
		}

		public void SetFixedColor(SexyColor c)
		{
			this.mCurrentColor = c;
			this.mColorMode = 3;
			this.mUpdateCount = 2;
		}

		public void AddColorKey(float pct, SexyColor c)
		{
			this.mTimeline.Add(new ColorKeyTimeEntry(pct, new ColorKey(c)));
			this.mTimeline.Sort(new SortColorKeys());
			this.mCurrentColor = this.mTimeline[0].second.GetColor();
		}

		public void AddAlphaKey(float pct, int alpha)
		{
			this.AddColorKey(pct, new SexyColor(255, 255, 255, alpha));
		}

		public void ForceTransition(int new_life, SexyColor final_color)
		{
			this.mUpdateCount = 0;
			this.mLife = new_life;
			this.mColorMode = 1;
			this.AddColorKey(0f, this.mCurrentColor);
			this.AddColorKey(1f, final_color);
		}

		public SexyColor GetColor()
		{
			return this.mCurrentColor;
		}

		public void SetLife(int l)
		{
			this.mLife = l;
		}

		public void SetColorMode(int m)
		{
			this.mColorMode = m;
		}

		public void SetImage(SharedImageRef r, string filename, string variant)
		{
			this.mImage = r;
			this.mImgFileName = filename;
			this.mImgVariant = variant;
		}

		public bool HasMaxIndex()
		{
			return this.mTimeline.size<ColorKeyTimeEntry>() != 0 && Common._eq(Enumerable.Last<ColorKeyTimeEntry>(this.mTimeline).first, 1f, 1E-06f);
		}

		public int GetNumKeys()
		{
			return this.mTimeline.size<ColorKeyTimeEntry>();
		}

		public int GetColorMode()
		{
			return this.mColorMode;
		}

		public SexyColor GetColorByIndex(int i)
		{
			return this.mTimeline[i].second.GetColor();
		}

		protected List<ColorKeyTimeEntry> mTimeline = new List<ColorKeyTimeEntry>();

		protected SexyColor mCurrentColor = default(SexyColor);

		protected SharedImageRef mImage;

		protected string mImgFileName = "";

		protected string mImgVariant = "";

		protected int mColorMode;

		protected int mLife;

		protected int mUpdateCount;

		public bool mUpdateImagePosColor;

		public int mGradientRepeat;
	}
}
