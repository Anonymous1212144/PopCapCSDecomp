using System;
using Sexy;

namespace BejeweledLIVE
{
	public static class WidgetExtensions
	{
		public static void DrawFrameHeadingAndText(this Widget w, Graphics g, string heading, int x, int y, Image imageOnRight)
		{
			int num = Math.Max(0, g.GetFont().StringWidth(heading) / 2 - Constants.mConstants.SubHeader_Stretch_Offset);
			int num2 = 0;
			if (imageOnRight != null)
			{
				num += imageOnRight.GetWidth() / 2;
				num2 = imageOnRight.GetWidth() / 2;
			}
			int num3 = AtlasResources.IMAGE_SUB_HEADER_MID.GetWidth() / 2;
			int num4 = AtlasResources.IMAGE_SUB_HEADER_MID.GetWidth() - num3;
			int height = AtlasResources.IMAGE_SUB_HEADER_MID.GetHeight();
			g.DrawImage(AtlasResources.IMAGE_SUB_HEADER_MID, x - num3, y);
			g.DrawImage(AtlasResources.IMAGE_SUB_HEADER_LEFT_MID, x - num - num3, y + height - AtlasResources.IMAGE_SUB_HEADER_LEFT_MID.GetHeight(), num, AtlasResources.IMAGE_SUB_HEADER_LEFT_MID.mHeight);
			g.DrawImage(AtlasResources.IMAGE_SUB_HEADER_LEFT, x - num - num3 - AtlasResources.IMAGE_SUB_HEADER_LEFT.mWidth, y + height - AtlasResources.IMAGE_SUB_HEADER_LEFT.GetHeight());
			g.DrawImage(AtlasResources.IMAGE_SUB_HEADER_RIGHT, x + num + num4, y + height - AtlasResources.IMAGE_SUB_HEADER_RIGHT.GetHeight());
			g.DrawImage(AtlasResources.IMAGE_SUB_HEADER_RIGHT_MID, x + num4, y + height - AtlasResources.IMAGE_SUB_HEADER_RIGHT_MID.GetHeight(), num, AtlasResources.IMAGE_SUB_HEADER_RIGHT_MID.mHeight);
			int num5 = g.GetFont().StringWidth(heading) / 2;
			int theY = y + Constants.mConstants.FrameWidget_Text_Y + g.GetFont().StringHeight(heading) / 2;
			g.DrawString(heading, x - num5 - num2, theY);
			if (imageOnRight != null)
			{
				g.DrawImage(imageOnRight, x + num5 + Constants.mConstants.FrameWidget_Image_Offset_X - num2, y + height - imageOnRight.GetHeight() - Constants.mConstants.FrameWidget_Image_Offset_Y);
			}
		}
	}
}
