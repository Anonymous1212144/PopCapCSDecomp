using System;
using SexyFramework.Graphics;

namespace SexyFramework.Widget
{
	public class ProxyWidget : Widget
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

		public ProxyWidgetListener mListener;
	}
}
