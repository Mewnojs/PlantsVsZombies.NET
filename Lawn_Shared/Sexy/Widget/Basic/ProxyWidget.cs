using System;

namespace Sexy
{
	public/*internal*/ class ProxyWidget : Widget
	{
		public ProxyWidget(ProxyWidgetListener listener)
		{
			mListener = listener;
		}

		public override void Draw(Graphics g)
		{
			if (mListener != null)
			{
				mListener.DrawProxyWidget(g, this);
			}
		}

		private ProxyWidgetListener mListener;
	}
}
