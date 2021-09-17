using System;
using SexyFramework.Graphics;

namespace SexyFramework.Widget
{
	public interface ProxyWidgetListener
	{
		void DrawProxyWidget(Graphics g, ProxyWidget proxyWidget);
	}
}
