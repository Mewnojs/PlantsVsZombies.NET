using System;

namespace Sexy
{
	internal class ProxyWidget : Widget
	{
		public ProxyWidget(ProxyWidgetListener listener)
		{
			this.mListener = listener;
		}

		public override void Draw(Graphics g)
		{
			if (this.mListener != null)
			{
				this.mListener.DrawProxyWidget(g, this);
			}
		}

		private ProxyWidgetListener mListener;
	}
}
