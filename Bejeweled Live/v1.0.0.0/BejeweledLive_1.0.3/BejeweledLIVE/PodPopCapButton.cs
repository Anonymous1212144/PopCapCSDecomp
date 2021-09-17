using System;
using Microsoft.Xna.Framework;
using Sexy;

namespace BejeweledLIVE
{
	public class PodPopCapButton : PodRoundButton
	{
		public PodPopCapButton(int id, ButtonListener listener)
			: base(id, AtlasResources.IMAGE_POPCAP_BUTTON, listener)
		{
			this.mBadgeImage = null;
		}

		public void SetNewContent(bool newContent)
		{
			int num = base.DefaultWidth();
			int num2 = base.DefaultHeight();
			if (newContent)
			{
				this.mBadgeImage = AtlasResources.IMAGE_MORE_GAMES_NEW_CONTENT;
				this.mBadgePos.mX = num - this.mBadgeImage.GetWidth() * 3 / 4;
				this.mBadgePos.mY = num2 - this.mBadgeImage.GetHeight();
				num = this.mBadgePos.mX + this.mBadgeImage.GetWidth();
				num2 = this.mBadgePos.mY + this.mBadgeImage.GetHeight();
			}
			else
			{
				this.mBadgeImage = null;
			}
			this.Resize(this.mX, this.mY, num, num2);
		}

		public override void Draw(Graphics g)
		{
			base.Draw(g);
			if (this.mBadgeImage != null)
			{
				Image image_MORE_GAMES_NEW_CONTENT = AtlasResources.IMAGE_MORE_GAMES_NEW_CONTENT;
				g.SetColorizeImages(true);
				g.SetColor(new Color(255, 255, 255, (int)(255f * this.mOpacity)));
				g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
				g.DrawImage(image_MORE_GAMES_NEW_CONTENT, this.mBadgePos.mX, this.mBadgePos.mY);
			}
		}

		protected Image mBadgeImage;

		protected TPoint mBadgePos = default(TPoint);
	}
}
