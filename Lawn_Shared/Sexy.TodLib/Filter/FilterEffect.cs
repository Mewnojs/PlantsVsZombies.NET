using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Sexy.TodLib
{
	class FilterEffect
	{
		public static void FilterEffectInitForApp()
		{
			for (int i = 0; i < (int)FilterEffectType.FilterEffectCount; i++) 
			{
				gFilterMap.Add(new Dictionary<Texture2D, Texture2D>());
			}
		}

		public static void FilterEffectDisposeForApp()
		{
			gFilterMap = null;
		}

		public static void FilterEffectInitTexture(Texture2D texture, FilterEffectType theFilterEffect) 
		{
			if (!gFilterMap[(int)theFilterEffect].ContainsKey(texture))
				gFilterMap[(int)theFilterEffect][texture] = FilterEffectCreateTexture(texture, theFilterEffect);
		}

		public static Image FilterEffectGetImage(Image theImage, FilterEffectType theFilterEffect)
		{
			if (theFilterEffect == FilterEffectType.None)
				return theImage;
			else 
			{
				FilterEffectInitTexture(theImage.Texture, theFilterEffect);
				MemoryImage memoryImage = new MemoryImage();
				memoryImage.mS = theImage.mS;
				memoryImage.mT = theImage.mT;
				memoryImage.mWidth = theImage.mWidth;
				memoryImage.mHeight = theImage.mHeight;
				memoryImage.mNumCols = theImage.mNumCols;
				memoryImage.mNumRows = theImage.mNumRows;
				memoryImage.Texture = gFilterMap[(int)theFilterEffect][theImage.Texture];
				return memoryImage;
			}
		}

		private static Texture2D FilterEffectCreateTexture(Texture2D theTexture, FilterEffectType theFilterEffect)
		{
			MemoryImage memoryImage = new MemoryImage();
			memoryImage.Create(theTexture.Width, theTexture.Height);
			lock (ResourceManager.DrawLocker)
			{
				GraphicsDevice graphicsDevice = GlobalStaticVars.g.GraphicsDevice;
				graphicsDevice.SetRenderTarget(memoryImage.RenderTarget);
				SpriteBatch spriteBatch = new SpriteBatch(graphicsDevice);

				BlendState imageLoadBlendAlpha = new BlendState
				{
					ColorWriteChannels = ColorWriteChannels.Alpha,
					AlphaDestinationBlend = Blend.Zero,
					ColorDestinationBlend = Blend.Zero,
					AlphaSourceBlend = Blend.One,
					ColorSourceBlend = Blend.One
				};

				BlendState blendColorLoadState = new BlendState
				{
					AlphaDestinationBlend = Blend.Zero,
					ColorDestinationBlend = Blend.Zero,
					AlphaSourceBlend = Blend.SourceAlpha,
					ColorSourceBlend = Blend.SourceAlpha
				};
				graphicsDevice.Clear(Color.Transparent);
				spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
				spriteBatch.Draw(theTexture, theTexture.Bounds, Color.White);
				spriteBatch.End();
				//spriteBatch.Begin(SpriteSortMode.Immediate, imageLoadBlendAlpha);
				//spriteBatch.Draw(theTexture, theTexture.Bounds, Color.White);
				//spriteBatch.End();
				graphicsDevice.SetRenderTarget(null);
			}
			switch (theFilterEffect)
			{
			case FilterEffectType.WashedOut:
				FilterEffect.FilterEffectDoWashedOut(memoryImage);
				break;
			case FilterEffectType.LessWashedOut:
				FilterEffect.FilterEffectDoLessWashedOut(memoryImage);
				break;
			case FilterEffectType.White:
				FilterEffect.FilterEffectDoWhite(memoryImage);
				break;
			}
			Texture2D result = memoryImage.Texture;
			return result;
		}

		private static void FilterEffectDoWashedOut(MemoryImage theImage)
		{
			FilterEffect.FilterEffectDoLumSat(theImage, 1.8f, 0.2f);
		}

		private static void FilterEffectDoLessWashedOut(MemoryImage theImage)
		{
			FilterEffect.FilterEffectDoLumSat(theImage, 1.2f, 0.3f);
		}

		private static void FilterEffectDoWhite(MemoryImage theImage)
		{
			int[] array = new int[theImage.mWidth * theImage.mHeight];
			theImage.Texture.GetData<int>(array);
			for (int i = 0; i < theImage.mHeight; i++)
			{
				for (int j = 0; j < theImage.mWidth; j++)
				{
					array[j + i * theImage.mWidth] |= 16777215;
				}
			}
			theImage.Texture.SetData<int>(array);
		}

		private static void FilterEffectDoLumSat(MemoryImage theImage, float aLum, float aSat)
		{
			int[] array = new int[theImage.Texture.Width * theImage.Texture.Height];
			theImage.Texture.GetData<int>(array);
			for (int i = 0; i < theImage.Texture.Height; i++)
			{
				for (int j = 0; j < theImage.Texture.Width; j++)
				{
					int num = array[j + i * theImage.Texture.Width];
					char c = (char)(num & 255);
					char c2 = (char)(num >> 8 & 255);
					char c3 = (char)(num >> 16 & 255);
					char c4 = (char)(num >> 24);
					float num2 = c / 255f;
					float num3 = c2 / 255f;
					float num4 = c3 / 255f;
					float h;
					float num5;
					float num6;
					FilterEffect.RGB_to_HSL(num2, num3, num4, out h, out num5, out num6);
					num5 *= aSat;
					num6 *= aLum;
					FilterEffect.HSL_to_RGB(h, num5, num6, out num2, out num3, out num4);
					int num7 = TodCommon.ClampInt((int)(num2 * 255f), 0, 255);
					int num8 = TodCommon.ClampInt((int)(num3 * 255f), 0, 255);
					int num9 = TodCommon.ClampInt((int)(num4 * 255f), 0, 255);
					array[j + i * theImage.Texture.Width] = (c4 << 24 | num9 << 16 | num8 << 8 | num7);
				}
			}
			theImage.Texture.SetData<int>(array);
		}

		private static void RGB_to_HSL(float r, float g, float b, out float h, out float s, out float l)
		{
			float num = Math.Max(r, g);
			num = Math.Max(num, b);
			float num2 = Math.Min(r, g);
			num2 = Math.Min(num2, b);
			h = (l = (s = 0f));
			if ((l = (num2 + num) / 2f) <= 0f)
			{
				return;
			}
			float num3;
			if ((s = (num3 = num - num2)) > 0f)
			{
				s /= ((l <= 0.5f) ? (num + num2) : (2f - num - num2));
				float num4 = (num - r) / num3;
				float num5 = (num - g) / num3;
				float num6 = (num - b) / num3;
				if (r == num)
				{
					h = ((g == num2) ? (5f + num6) : (1f - num5));
				}
				else if (g == num)
				{
					h = ((b == num2) ? (1f + num4) : (3f - num6));
				}
				else
				{
					h = ((r == num2) ? (3f + num5) : (5f - num4));
				}
				h /= 6f;
				return;
			}
		}

		private static void HSL_to_RGB(float h, float sl, float l, out float r, out float g, out float b)
		{
			r = (g = (b = 0f));
			float num = (l <= 0.5f) ? (l * (1f + sl)) : (l + sl - l * sl);
			if (num <= 0f)
			{
				r = (g = (b = 0f));
				return;
			}
			float num2 = l + l - num;
			float num3 = (num - num2) / num;
			h *= 6f;
			int num4 = TodCommon.ClampInt((int)h, 0, 5);
			float num5 = h - num4;
			float num6 = num * num3 * num5;
			float num7 = num2 + num6;
			float num8 = num - num6;
			switch (num4)
			{
			case 0:
				r = num;
				g = num7;
				b = num2;
				return;
			case 1:
				r = num8;
				g = num;
				b = num2;
				return;
			case 2:
				r = num2;
				g = num;
				b = num7;
				return;
			case 3:
				r = num2;
				g = num8;
				b = num;
				return;
			case 4:
				r = num7;
				g = num2;
				b = num;
				return;
			case 5:
				r = num;
				g = num2;
				b = num8;
				return;
			default:
				return;
			}
		}

		public static List<Dictionary<Texture2D, Texture2D>> gFilterMap = new List<Dictionary<Texture2D, Texture2D>>();
	}

	public enum FilterEffectType
	{
		None = -1,
		WashedOut,
		LessWashedOut,
		White,
		FilterEffectCount
	}
}
