using System;
using Microsoft.Xna.Framework;
using Sexy;

namespace BejeweledLIVE
{
	public class BackgroundWidget : InterfaceWidget
	{
		public BackgroundWidget(GameApp app)
			: base(app)
		{
			this.mImage = null;
			this.mImage = Resources.IMAGE_MAINMENU_BKG;
			this.mLayer1 = Resources.IMAGE_MAINMENU_BKG_LAYER1;
			this.mLayer2 = Resources.IMAGE_MAINMENU_BKG_LAYER2;
			this.mLayer3 = Resources.IMAGE_MAINMENU_BKG_LAYER3;
			this.SetVisible(false);
			this.SetDisabled(true);
		}

		public override void Dispose()
		{
		}

		public override void SetupForState(int uiState, int uiStateParam, int uiStateLayout)
		{
			base.SetupForState(uiState, uiStateParam, uiStateLayout);
			this.Resize(0, 0, this.mApp.mWidth, this.mApp.mHeight);
			if (uiStateParam != this.mInterfaceStateParam || uiStateLayout != this.mInterfaceStateLayout)
			{
				this.ResetBackgroundImages();
			}
			if (this.mInterfaceStateParam != 0 || uiStateParam != 0)
			{
			}
		}

		public override void InterfaceOrientationChanged(UI_ORIENTATION toOrientation)
		{
			InterfaceLayouts interfaceLayouts = (this.mApp.OrientationIsLandscape(toOrientation) ? InterfaceLayouts.UI_LAYOUT_LANDSCAPE : InterfaceLayouts.UI_LAYOUT_PORTRAIT);
			bool flag = this.mInterfaceStateLayout != (int)interfaceLayouts;
			base.InterfaceOrientationChanged(toOrientation);
			if (flag)
			{
				this.ResetBackgroundImages();
			}
		}

		private void ResetBackgroundImages()
		{
			this.mLayer1XPos = (float)((int)((float)this.mWidth - (float)this.mLayer1.mWidth * Constants.mConstants.BackgroundWidget_Image_Scale));
			this.mLayer2XPos = (float)((int)((float)this.mWidth - (float)this.mLayer2.mWidth * Constants.mConstants.BackgroundWidget_Image_Scale));
			this.mLayer3XPos = (float)((int)((float)this.mWidth - (float)this.mLayer3.mWidth * Constants.mConstants.BackgroundWidget_Image_Scale));
		}

		public override void TransitionInToState(int uiState, int uiStateParam, int uiStateLayout)
		{
			base.TransitionInToState(uiState, uiStateParam, uiStateLayout);
			if (this.isFirstShow)
			{
				this.FadeIn(0.5f, 1f);
				this.isFirstShow = false;
				return;
			}
			this.FadeIn(0f, 0.5f);
		}

		public override void TransitionOutToState(int uiState, int uiStateParam, int uiStateLayout)
		{
			base.TransitionOutToState(uiState, uiStateParam, uiStateLayout);
		}

		public override void InterfaceTransactionEnd(int uiState)
		{
			base.InterfaceTransactionEnd(uiState);
		}

		public override void Update()
		{
			base.Update();
		}

		private void MoveBackground()
		{
			if ((int)this.mLayer1XPos >= this.mWidth)
			{
				this.mLayer1XPos = (float)((int)((float)this.mWidth - (float)this.mLayer1.mWidth * Constants.mConstants.BackgroundWidget_Image_Scale));
			}
			else
			{
				this.mLayer1XPos += 0.3f;
			}
			if ((int)this.mLayer2XPos >= this.mWidth)
			{
				this.mLayer2XPos = (float)((int)((float)this.mWidth - (float)this.mLayer2.mWidth * Constants.mConstants.BackgroundWidget_Image_Scale));
			}
			else
			{
				this.mLayer2XPos += 0.6f;
			}
			if ((int)this.mLayer3XPos >= this.mWidth)
			{
				this.mLayer3XPos = (float)((int)((float)this.mWidth - (float)this.mLayer3.mWidth * Constants.mConstants.BackgroundWidget_Image_Scale));
				return;
			}
			this.mLayer3XPos += 0.900000036f;
		}

		public override void Draw(Graphics g)
		{
			this.MoveBackground();
			base.Draw(g);
			if (this.mImage != null)
			{
				g.SetColor(new Color(255, 255, 255, (int)(255f * this.mOpacity)));
				g.SetColorizeImages(true);
				g.DrawImage(this.mImage, 0, 0, this.mWidth, this.mHeight);
				g.DrawImage(this.mLayer3, (int)this.mLayer3XPos, this.mHeight - (int)((float)this.mLayer3.mHeight * Constants.mConstants.BackgroundWidget_Image_Scale), (int)((float)this.mLayer3.mWidth * Constants.mConstants.BackgroundWidget_Image_Scale), (int)((float)this.mLayer3.mHeight * Constants.mConstants.BackgroundWidget_Image_Scale));
				g.DrawImage(this.mLayer3, (int)(this.mLayer3XPos - (float)this.mLayer3.mWidth * Constants.mConstants.BackgroundWidget_Image_Scale), this.mHeight - (int)((float)this.mLayer3.mHeight * Constants.mConstants.BackgroundWidget_Image_Scale), (int)((float)this.mLayer3.mWidth * Constants.mConstants.BackgroundWidget_Image_Scale), (int)((float)this.mLayer3.mHeight * Constants.mConstants.BackgroundWidget_Image_Scale));
				g.DrawImage(this.mLayer2, (int)this.mLayer2XPos, this.mHeight - (int)((float)this.mLayer2.mHeight * Constants.mConstants.BackgroundWidget_Image_Scale), (int)((float)this.mLayer2.mWidth * Constants.mConstants.BackgroundWidget_Image_Scale), (int)((float)this.mLayer2.mHeight * Constants.mConstants.BackgroundWidget_Image_Scale));
				g.DrawImage(this.mLayer2, (int)(this.mLayer2XPos - (float)this.mLayer2.mWidth * Constants.mConstants.BackgroundWidget_Image_Scale), this.mHeight - (int)((float)this.mLayer2.mHeight * Constants.mConstants.BackgroundWidget_Image_Scale), (int)((float)this.mLayer2.mWidth * Constants.mConstants.BackgroundWidget_Image_Scale), (int)((float)this.mLayer2.mHeight * Constants.mConstants.BackgroundWidget_Image_Scale));
				g.DrawImage(this.mLayer1, (int)this.mLayer1XPos, this.mHeight - (int)((float)this.mLayer1.mHeight * Constants.mConstants.BackgroundWidget_Image_Scale), (int)((float)this.mLayer1.mWidth * Constants.mConstants.BackgroundWidget_Image_Scale), (int)((float)this.mLayer1.mHeight * Constants.mConstants.BackgroundWidget_Image_Scale));
				g.DrawImage(this.mLayer1, (int)(this.mLayer1XPos - (float)this.mLayer1.mWidth * Constants.mConstants.BackgroundWidget_Image_Scale), this.mHeight - (int)((float)this.mLayer1.mHeight * Constants.mConstants.BackgroundWidget_Image_Scale), (int)((float)this.mLayer1.mWidth * Constants.mConstants.BackgroundWidget_Image_Scale), (int)((float)this.mLayer1.mHeight * Constants.mConstants.BackgroundWidget_Image_Scale));
			}
		}

		private const float mLayer1Speed = 0.3f;

		private const float mLayer2Speed = 0.6f;

		private const float mLayer3Speed = 0.900000036f;

		protected Image mImage;

		protected Image mLayer1;

		protected Image mLayer2;

		protected Image mLayer3;

		protected float mLayer1XPos;

		protected float mLayer2XPos;

		protected float mLayer3XPos;

		private bool isFirstShow = true;
	}
}
