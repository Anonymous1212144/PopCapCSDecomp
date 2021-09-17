using System;
using Sexy;

namespace BejeweledLIVE
{
	public class FrameWidget : InterfaceControl
	{
		public FrameWidget()
		{
			this.mHasLabel = false;
			this.mHasColor = false;
			this.mBackgroundColor = new SexyColor(0, 0, 0, 255);
			this.mMouseVisible = false;
		}

		public FrameWidget(string aLabel)
		{
			this.mLabel = aLabel;
			this.mHasLabel = true;
			this.mHasColor = false;
			this.mBackgroundColor = new SexyColor(0, 0, 0, 255);
			this.mMouseVisible = false;
		}

		public FrameWidget(string aLabel, SexyColor aColor)
		{
			this.mLabel = aLabel;
			this.mHasLabel = true;
			this.mHasColor = true;
			this.mBackgroundColor = aColor;
			this.mMouseVisible = false;
		}

		public void SetLabel(string aLabel)
		{
			this.mLabel = aLabel;
			this.mHasLabel = true;
		}

		public void RemoveLabel()
		{
			this.mLabel = "";
			this.mHasLabel = false;
		}

		public void SetColor(SexyColor aColor)
		{
			this.mBackgroundColor = aColor;
			this.mHasColor = true;
		}

		public void RemoveColor()
		{
			this.mBackgroundColor = SexyColor.White;
			this.mHasColor = false;
		}

		public void SetImage(Image aImage)
		{
			this.mBackgroundImage = aImage;
			this.mHasImage = true;
		}

		public void RemoveImage()
		{
			this.mBackgroundImage = null;
			this.mHasImage = false;
		}

		public override void FadeIn(float startSeconds, float endSeconds)
		{
			base.FadeIn(startSeconds, endSeconds);
		}

		public override void FadeOut(float startSeconds, float endSeconds)
		{
			base.FadeOut(startSeconds, endSeconds);
		}

		public override void Draw(Graphics g)
		{
			base.Draw(g);
			if (this.mOpacity == 0f)
			{
				return;
			}
			if (this.mHasColor)
			{
				g.SetColorizeImages(this.mHasColor);
				g.SetColor(new SexyColor(this.mBackgroundColor.mRed, this.mBackgroundColor.mGreen, this.mBackgroundColor.mBlue, (int)(255f * this.mOpacity)));
				g.FillRect(new TRect((int)Constants.mConstants.S(28f), this.mHasLabel ? (AtlasResources.IMAGE_SUB_HEADER_MID.GetHeight() + (int)Constants.mConstants.S(14f)) : ((int)Constants.mConstants.S(38f)), this.mWidth - (int)Constants.mConstants.S(56f), this.mHeight - (this.mHasLabel ? (AtlasResources.IMAGE_SUB_HEADER_MID.GetHeight() + (int)Constants.mConstants.S(50f)) : ((int)Constants.mConstants.S(76f)))));
			}
			g.SetColorizeImages(true);
			SexyColor aColor = new SexyColor(255, 255, 255, (int)(255f * this.mOpacity));
			g.SetColor(aColor);
			if (this.mHasImage)
			{
				g.DrawImage(this.mBackgroundImage, new TRect((int)Constants.mConstants.S(28f), this.mHasLabel ? (AtlasResources.IMAGE_SUB_HEADER_MID.GetHeight() + (int)Constants.mConstants.S(14f)) : ((int)Constants.mConstants.S(38f)), this.mWidth - (int)Constants.mConstants.S(56f), this.mHeight - (this.mHasLabel ? (AtlasResources.IMAGE_SUB_HEADER_MID.GetHeight() + (int)Constants.mConstants.S(36f)) : ((int)Constants.mConstants.S(76f)))), new TRect(0, 0, this.mBackgroundImage.mWidth, this.mBackgroundImage.mHeight));
			}
			if (this.mHasLabel)
			{
				TRect theDest = new TRect(0, AtlasResources.IMAGE_SUB_HEADER_MID.GetHeight() - (int)Constants.mConstants.S(31f), this.mWidth, this.mHeight - AtlasResources.IMAGE_SUB_HEADER_MID.GetHeight() + (int)Constants.mConstants.S(22f));
				g.DrawImageBox(theDest, AtlasResources.IMAGE_SUB_BORDER);
				g.SetFont(Resources.FONT_HEADING);
				this.DrawFrameHeadingAndText(g, this.mLabel, this.mWidth / 2, 0, this.RightLabelImage);
				return;
			}
			g.DrawImageBox(new TRect(0, 0, this.mWidth, this.mHeight), AtlasResources.IMAGE_SUB_BORDER);
		}

		private string mLabel = string.Empty;

		private bool mHasLabel;

		private SexyColor mBackgroundColor;

		private bool mHasColor;

		private Image mBackgroundImage;

		private bool mHasImage;

		public Image RightLabelImage;
	}
}
