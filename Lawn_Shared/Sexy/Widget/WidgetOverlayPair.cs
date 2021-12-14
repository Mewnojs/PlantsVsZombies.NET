using System;
using System.Collections.Generic;

namespace Sexy
{
	public/*internal*/ class WidgetOverlayPair
	{
		public static WidgetOverlayPair GetNewWidgetOverlayPair(Widget w, int p)
		{
			if (WidgetOverlayPair.unusedObjects.Count > 0)
			{
				WidgetOverlayPair widgetOverlayPair = WidgetOverlayPair.unusedObjects.Pop();
				widgetOverlayPair.Reset(w, p);
				return widgetOverlayPair;
			}
			return new WidgetOverlayPair(w, p);
		}

		public void PrepareForReuse()
		{
			WidgetOverlayPair.unusedObjects.Push(this);
		}

		private WidgetOverlayPair(Widget w, int p)
		{
			Reset(w, p);
		}

		private void Reset(Widget w, int p)
		{
			aWidget = w;
			aPriority = p;
		}

		public void Clear()
		{
			aWidget = null;
			aPriority = 0;
		}

		public override int GetHashCode()
		{
			return aPriority;
		}

		public override bool Equals(object obj)
		{
			WidgetOverlayPair widgetOverlayPair = obj as WidgetOverlayPair;
			return widgetOverlayPair != null && this == widgetOverlayPair;
		}

		public Widget aWidget;

		public int aPriority;

		private static Stack<WidgetOverlayPair> unusedObjects = new Stack<WidgetOverlayPair>(10);
	}
}
