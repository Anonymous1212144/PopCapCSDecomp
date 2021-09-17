using System;
using BejeweledLIVE;
using Microsoft.Xna.Framework;
using Sexy;

namespace Bejeweled3
{
	public class MenuHintButton : InterfaceButton
	{
		public MenuHintButton(int menuButtonId, int hintButtonId, int pauseButtonId, bool pauseButtonVersion, ButtonListener listener)
			: base(menuButtonId, listener)
		{
			this.menuButtonId = menuButtonId;
			this.hintButtonId = hintButtonId;
			this.pauseButtonId = pauseButtonId;
			this.DoubleButtonVersion = pauseButtonVersion;
		}

		public override void Draw(Graphics g)
		{
			GameApp gSexyAppBase = GlobalStaticVars.gSexyAppBase;
			int num = (int)(this.mOpacity * 255f);
			g.SetColorizeImages(true);
			g.SetColor(new Color(255, 255, 255, num));
			g.DrawImage(this.buttonImage, 0, 0);
			if (this.doubleButtonVersion)
			{
				g.DrawImageCel(AtlasResources.IMAGE_EMPTY_BUTTON, Constants.mConstants.MenuHintButton_Double_MenuButton_X, Constants.mConstants.MenuHintButton_Double_MenuButton_Y, this.rightButtonPressed ? 1 : 0);
				g.SetFont(this.ButtonFont);
				g.SetScale(Constants.mConstants.MenuHintButton_Menu_Font_Scale);
				Point point;
				point..ctor(g.GetFont().StringWidth(this.menuLabel), g.GetFont().StringHeight(this.menuLabel));
				g.DrawString(this.menuLabel, (int)((float)Constants.mConstants.MenuHintButton_Double_MenuButton_X + (float)AtlasResources.IMAGE_EMPTY_BUTTON.GetCelWidth() / 2f - (float)point.X / 2f), (int)((float)Constants.mConstants.MenuHintButton_Double_MenuButton_Y + (float)AtlasResources.IMAGE_EMPTY_BUTTON.mHeight / 2f - (float)point.Y * 0.5f));
				g.SetScale(1f);
				if (this.LeftButtonUsage == MenuHintButton.LeftButtonType.PlayPause)
				{
					if (!this.isPaused)
					{
						g.DrawImageCel(AtlasResources.IMAGE_PAUSE_BUTTON, Constants.mConstants.MenuHintButton_Double_LeftButton_X, Constants.mConstants.MenuHintButton_Double_LeftButton_Y, this.leftButtonPressed ? 1 : 0);
					}
					else
					{
						g.DrawImageCel(AtlasResources.IMAGE_PLAY_BUTTON, Constants.mConstants.MenuHintButton_Double_LeftButton_X, Constants.mConstants.MenuHintButton_Double_LeftButton_Y, this.leftButtonPressed ? 1 : 0);
					}
				}
				else
				{
					g.DrawImageCel(AtlasResources.IMAGE_EMPTY_BUTTON, Constants.mConstants.MenuHintButton_Double_LeftButton_X, Constants.mConstants.MenuHintButton_Double_LeftButton_Y, this.leftButtonPressed ? 1 : 0);
					int num2 = Constants.mConstants.MenuHintButton_Double_LeftButton_X + Constants.mConstants.MenuHintButton_Double_Endless_X;
					int num3 = Constants.mConstants.MenuHintButton_Double_LeftButton_Y + Constants.mConstants.MenuHintButton_Double_Endless_Y;
					g.SetColorizeImages(true);
					g.SetColor(new Color(255, 255, 255, num));
					if (SexyAppBase.IsInTrialMode)
					{
						int time = (int)this.TrialTimeLeft;
						string timeString = Board.GetTimeString(time);
						Vector2 vector = g.GetFont().MeasureString(timeString) / 2f;
						num2 = Constants.mConstants.MenuHintButton_Double_LeftButton_X + Constants.mConstants.MenuHintButton_Double_Trial_X;
						num3 = Constants.mConstants.MenuHintButton_Double_LeftButton_Y + Constants.mConstants.MenuHintButton_Double_Trial_Y;
						num2 -= (int)vector.X;
						num3 -= (int)vector.Y;
						num3 += Constants.mConstants.BoardBej2_TrialTime_Offset_Y_Portrait;
						g.DrawString(timeString, num2, num3);
					}
					else
					{
						g.DrawImage(AtlasResources.IMAGE_INFINITY, num2, num3);
					}
				}
				g.SetScale(1f);
				g.SetColor(new Color(255, 255, 255, (int)((float)num * this.hintOpacity)));
				g.DrawImageCel(AtlasResources.IMAGE_HINT_BUTTON, Constants.mConstants.MenuHintButton_Double_HintButton_X, Constants.mConstants.MenuHintButton_Double_HintButton_Y, this.topButtonPressed ? 1 : 0);
				g.SetFont(this.ButtonFont);
				Point point2;
				point2..ctor(g.GetFont().StringWidth(this.hintLabel), g.GetFont().StringHeight(this.hintLabel));
				g.DrawString(this.hintLabel, (int)((float)Constants.mConstants.MenuHintButton_Double_HintButton_X + (float)AtlasResources.IMAGE_HINT_BUTTON.GetCelWidth() / 2f - (float)point2.X / 2f), (int)((float)Constants.mConstants.MenuHintButton_Double_HintButton_Y + (float)AtlasResources.IMAGE_HINT_BUTTON.mHeight / 2f - (float)point2.Y * 0.5f));
			}
			else
			{
				g.DrawImageCel(AtlasResources.IMAGE_EMPTY_BUTTON, Constants.mConstants.MenuHintButton_MenuButton_X, Constants.mConstants.MenuHintButton_MenuButton_Y, this.rightButtonPressed ? 1 : 0);
				g.SetFont(this.ButtonFont);
				g.SetScale(Constants.mConstants.MenuHintButton_Menu_Font_Scale);
				Point point3;
				point3..ctor(g.GetFont().StringWidth(this.menuLabel), g.GetFont().StringHeight(this.menuLabel));
				g.DrawString(this.menuLabel, (int)((float)Constants.mConstants.MenuHintButton_MenuButton_X + (float)AtlasResources.IMAGE_EMPTY_BUTTON.GetCelWidth() / 2f - (float)point3.X / 2f), (int)((float)Constants.mConstants.MenuHintButton_MenuButton_Y + (float)AtlasResources.IMAGE_EMPTY_BUTTON.mHeight / 2f - (float)point3.Y * 0.5f));
				g.SetScale(1f);
				g.SetColor(new Color(255, 255, 255, (int)((float)num * this.hintOpacity)));
				g.DrawImageCel(AtlasResources.IMAGE_HINT_BUTTON, Constants.mConstants.MenuHintButton_HintButton_X, Constants.mConstants.MenuHintButton_HintButton_Y, this.topButtonPressed ? 1 : 0);
				g.SetFont(this.ButtonFont);
				Point point4;
				point4..ctor(g.GetFont().StringWidth(this.hintLabel), g.GetFont().StringHeight(this.hintLabel));
				g.DrawString(this.hintLabel, (int)((float)Constants.mConstants.MenuHintButton_HintButton_X + (float)AtlasResources.IMAGE_HINT_BUTTON.GetCelWidth() / 2f - (float)point4.X / 2f), (int)((float)Constants.mConstants.MenuHintButton_HintButton_Y + (float)AtlasResources.IMAGE_HINT_BUTTON.mHeight / 2f - (float)point4.Y * 0.5f));
			}
			g.SetScale(1f);
		}

		private Font ButtonFont
		{
			get
			{
				return Resources.FONT_TINY_TEXT;
			}
		}

		public void SetHintOpacity(float hintOpacity)
		{
			this.hintOpacity = hintOpacity;
		}

		public void SetHintEnabled(bool enabled)
		{
			this.hintEnabled = enabled;
		}

		public override void FadeIn(float startSeconds, float endSeconds)
		{
			base.FadeIn(startSeconds, endSeconds);
			this.SetDisabled(false);
		}

		public override void FadeOut(float startSeconds, float endSeconds)
		{
			base.FadeOut(startSeconds, endSeconds);
			this.SetDisabled(true);
		}

		public override void MouseDown(int theX, int theY, int theBtnNum, int theClickCount)
		{
			bool flag = true;
			this.rightButtonPressed = (this.leftButtonPressed = (this.topButtonPressed = false));
			if (this.doubleButtonVersion)
			{
				if (theY > Constants.mConstants.MenuHintButton_Menu_Y)
				{
					if (theX > this.mWidth / 2)
					{
						this.rightButtonPressed = true;
					}
					else
					{
						if (this.LeftButtonUsage == MenuHintButton.LeftButtonType.PlayPause)
						{
							this.leftButtonPressed = true;
						}
						flag = false;
					}
				}
				else if (this.hintEnabled)
				{
					this.topButtonPressed = true;
				}
				else
				{
					flag = false;
				}
			}
			else if (theY > Constants.mConstants.MenuHintButton_Menu_Y)
			{
				this.rightButtonPressed = true;
			}
			else if (this.hintEnabled)
			{
				this.topButtonPressed = true;
			}
			else
			{
				flag = false;
			}
			if (flag)
			{
				base.MouseDown(theX, theY, theBtnNum, theClickCount);
			}
		}

		public override void MouseDown(int theX, int theY, int theClickCount)
		{
			base.MouseDown(theX, theY, theClickCount);
		}

		public override void MouseUp(int theX, int theY, int theBtnNum, int theClickCount)
		{
			this.rightButtonPressed = (this.leftButtonPressed = (this.topButtonPressed = false));
			if (this.doubleButtonVersion)
			{
				if (theY > Constants.mConstants.MenuHintButton_Menu_Y)
				{
					if (theX > this.mWidth / 2)
					{
						this.mButtonListener.ButtonDepress(this.menuButtonId);
						return;
					}
					if (this.LeftButtonUsage == MenuHintButton.LeftButtonType.PlayPause)
					{
						this.mButtonListener.ButtonDepress(this.pauseButtonId);
						return;
					}
				}
				else if (this.hintEnabled)
				{
					this.mButtonListener.ButtonDepress(this.hintButtonId);
					return;
				}
			}
			else
			{
				if (theY > Constants.mConstants.MenuHintButton_Menu_Y)
				{
					this.mButtonListener.ButtonDepress(this.menuButtonId);
					return;
				}
				if (this.hintEnabled)
				{
					this.mButtonListener.ButtonDepress(this.hintButtonId);
				}
			}
		}

		public override void MouseUp(int theX, int theY, int theClickCount)
		{
			this.MouseUp(theX, theY, 0, theClickCount);
		}

		private void SetUpForVersion()
		{
			if (this.doubleButtonVersion)
			{
				this.buttonImage = AtlasResources.IMAGE_WIDGET_BOTTOM_DOUBLE;
			}
			else
			{
				this.buttonImage = AtlasResources.IMAGE_WIDGET_BOTTOM;
			}
			this.mWidth = this.buttonImage.mWidth;
			this.mHeight = this.buttonImage.mHeight;
		}

		public bool DoubleButtonVersion
		{
			get
			{
				return this.doubleButtonVersion;
			}
			set
			{
				this.doubleButtonVersion = value;
				this.SetUpForVersion();
			}
		}

		public void Pause(bool paused)
		{
			this.isPaused = paused;
		}

		private string hintLabel = Strings.HintButton_Text;

		private string menuLabel = Strings.MenuButton_Text;

		public MenuHintButton.LeftButtonType LeftButtonUsage;

		public double TrialTimeLeft;

		private int menuButtonId;

		private int hintButtonId;

		private int pauseButtonId;

		private Image buttonImage;

		private new int mId;

		private bool doubleButtonVersion;

		private bool hintEnabled = true;

		private float hintOpacity = 1f;

		private bool topButtonPressed;

		private bool leftButtonPressed;

		private bool rightButtonPressed;

		private bool isPaused;

		public enum LeftButtonType
		{
			PlayPause,
			Empty
		}
	}
}
