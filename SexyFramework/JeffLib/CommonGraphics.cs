using System;
using System.Collections.Generic;
using Sexy;
using Sexy.GraphicsLib;
using Sexy.Misc;

namespace JeffLib
{
	public static class CommonGraphics
	{
		public static void SetNonMaskedArea(int x, int y, int w, int h, List<MaskedRect> v, int starting_alpha)
		{
			SexyApp gSexyApp = GlobalMembers.gSexyApp;
			if (x > gSexyApp.mScreenBounds.mX)
			{
				v.Add(new MaskedRect(new Rect(gSexyApp.mScreenBounds.mX, gSexyApp.mScreenBounds.mY, x - gSexyApp.mScreenBounds.mX, gSexyApp.mScreenBounds.mHeight), starting_alpha));
			}
			if (y > gSexyApp.mScreenBounds.mY)
			{
				v.Add(new MaskedRect(new Rect(x, gSexyApp.mScreenBounds.mY, gSexyApp.mScreenBounds.mWidth - x, y - gSexyApp.mScreenBounds.mY), starting_alpha));
			}
			if (x + w < gSexyApp.mScreenBounds.mX + gSexyApp.mScreenBounds.mWidth)
			{
				v.Add(new MaskedRect(new Rect(x + w, y, gSexyApp.mScreenBounds.mX + gSexyApp.mScreenBounds.mWidth - (x + w), h), starting_alpha));
			}
			if (y + h < gSexyApp.mScreenBounds.mY + gSexyApp.mScreenBounds.mHeight)
			{
				v.Add(new MaskedRect(new Rect(x, y + h, gSexyApp.mScreenBounds.mX + gSexyApp.mScreenBounds.mWidth - x, gSexyApp.mScreenBounds.mY + gSexyApp.mScreenBounds.mHeight - (y + h)), starting_alpha));
			}
		}

		public static void DrawCircle(Graphics g, float center_x, float center_y, float r, int num_segments)
		{
			CommonGraphics.DrawCircle(g, center_x, center_y, r, num_segments, true, 6.2831855f);
		}

		public static void DrawCircle(Graphics g, float center_x, float center_y, float r, int num_segments, bool fill)
		{
			CommonGraphics.DrawCircle(g, center_x, center_y, r, num_segments, fill, 6.2831855f);
		}

		public static void DrawCircle(Graphics g, float center_x, float center_y, float r, int num_segments, bool fill, float max_angle)
		{
			float num = max_angle / (float)num_segments;
			float num2 = 0f;
			SexyPoint[] array = new SexyPoint[num_segments];
			for (int i = 0; i < num_segments; i++)
			{
				float num3 = r * (float)Math.Cos((double)num2);
				float num4 = r * (float)Math.Sin((double)num2);
				array[i].mX = (int)(center_x + num3);
				array[i].mY = (int)(center_y + num4);
				num2 += num;
			}
			if (fill)
			{
				g.PolyFill(array, num_segments, false);
				return;
			}
			CommonGraphics.DrawPoly(g, array, num_segments);
		}

		public static void DrawPoly(Graphics g, SexyPoint[] vertices, int num_vertices)
		{
			for (int i = 0; i < num_vertices; i++)
			{
				SexyPoint point = vertices[(i == num_vertices - 1) ? 0 : (i + 1)];
				g.DrawLine(vertices[i].mX, vertices[i].mY, point.mX, point.mY);
			}
		}
	}
}
