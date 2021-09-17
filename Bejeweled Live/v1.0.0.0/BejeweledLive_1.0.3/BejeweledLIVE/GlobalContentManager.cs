using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sexy;

namespace BejeweledLIVE
{
	public class GlobalContentManager
	{
		public GlobalContentManager(Main m)
		{
			this.main = m;
			this.content = this.main.Content;
			this.graphicsDevice = this.main.GraphicsDevice;
			this.content.RootDirectory = "Content";
		}

		public void cleanUp()
		{
		}

		public void initialize()
		{
			this.splashScreenSpriteBatch = new SpriteBatch(this.graphicsDevice);
		}

		public void LoadSplashScreen()
		{
			this.loadingScreenContent = new ContentManager(this.main.Services);
			this.loadingScreenContent.RootDirectory = this.content.RootDirectory;
			this.splashScreen_texture = this.loadingScreenContent.Load<Texture2D>("POP_CAP_LOGO");
			this.splashScreen_ring = this.loadingScreenContent.Load<Texture2D>("POP_CAP_LOGO_RING");
			this.splashScreen_text = this.loadingScreenContent.Load<Texture2D>("POP_CAP_LOGO_TEXT");
			this.progressBarTexture = new Texture2D(this.graphicsDevice, 4, 1);
			this.progressBarTexture.SetData<Color>(new Color[]
			{
				Color.White,
				Color.White,
				Color.White,
				new Color(0, 0, 0, 0)
			});
		}

		public void UnloadSplashScreen()
		{
			this.loadingScreenContent.Unload();
			this.splashScreen_texture = null;
			this.splashScreen_ring = null;
		}

		public void LoadGameContent()
		{
			this.LoadFonts();
		}

		public void LoadFonts()
		{
		}

		public void LoadSounds()
		{
		}

		public virtual void LoadLevelBackdrops()
		{
		}

		public void ResetloadingScreen()
		{
			this.loadingProgress = 0f;
		}

		public void DrawSplashScreen()
		{
			lock (SexyAppBase.SplashScreenDrawLock)
			{
				this.splashScreenSpriteBatch.GraphicsDevice.Clear(Color.Black);
				if (this.splashScreen_texture != null && this.splashScreen_ring != null && this.splashScreen_text != null)
				{
					float num = (float)GlobalStaticVars.gSexyAppBase.mResourceManager.mProgress;
					float num2 = num * 6.28318548f;
					if (this.loadingProgress < num2)
					{
						this.loadingProgress += (num2 - this.loadingProgress) * 0.1f;
					}
					float num3 = this.loadingProgress / 6.28318548f;
					int num4 = 255;
					if (num3 > 0.95f)
					{
						num4 = (int)(255f * ((1f - num3) / 0.0500000119f));
					}
					Color color;
					color..ctor(num4, num4, num4, num4);
					this.splashScreenSpriteBatch.Begin(0, BlendState.AlphaBlend);
					this.splashScreenSpriteBatch.Draw(this.splashScreen_texture, new Rectangle(this.graphicsDevice.Viewport.Width / 2 - Constants.mConstants.SplashScreen_Logo_Width / 2, this.graphicsDevice.Viewport.Height / 2 - Constants.mConstants.SplashScreen_Logo_Height / 2, Constants.mConstants.SplashScreen_Logo_Width, Constants.mConstants.SplashScreen_Logo_Height), color);
					this.splashScreenSpriteBatch.Draw(this.splashScreen_ring, new Rectangle(this.graphicsDevice.Viewport.Width / 2, this.graphicsDevice.Viewport.Height / 2, Constants.mConstants.SplashScreen_Logo_Width, Constants.mConstants.SplashScreen_Logo_Height), default(Rectangle?), color, this.loadingProgress, new Vector2((float)(this.splashScreen_ring.Bounds.Width / 2), (float)(this.splashScreen_ring.Bounds.Height / 2)), 0, 0f);
					this.splashScreenSpriteBatch.Draw(this.splashScreen_text, new Vector2((float)(this.graphicsDevice.Viewport.Width / 2 - this.splashScreen_text.Width / 2), (float)(this.graphicsDevice.Viewport.Height - this.splashScreen_text.Height)), color);
					this.splashScreenSpriteBatch.End();
				}
			}
		}

		public void DrawGameLoadingScreen()
		{
			lock (SexyAppBase.SplashScreenDrawLock)
			{
				this.splashScreenSpriteBatch.GraphicsDevice.Clear(Color.Black);
				if (this.splashScreen_texture != null && this.splashScreen_ring != null && this.splashScreen_text != null)
				{
					float num = (float)GlobalStaticVars.gSexyAppBase.mResourceManager.mProgress;
					if (this.loadingProgress < num)
					{
						this.loadingProgress += (num - this.loadingProgress) * 0.1f;
					}
					int num2 = 255;
					if (this.loadingProgress > 0.9f)
					{
						num2 = (int)(255f * ((1f - this.loadingProgress) / 0.100000024f));
					}
					Color color;
					color..ctor(num2, num2, num2, num2);
					this.splashScreenSpriteBatch.Begin(0, BlendState.AlphaBlend);
					int num3 = (int)Constants.mConstants.S(20f);
					int num4 = (int)((float)(this.graphicsDevice.Viewport.Width / 2) * this.loadingProgress);
					int num5 = this.graphicsDevice.Viewport.Width / 4;
					int num6 = this.graphicsDevice.Viewport.Height / 2;
					int num7 = (int)Constants.mConstants.S(20f);
					this.splashScreenSpriteBatch.Draw(this.progressBarTexture, new Rectangle(num5, num6, num4 - num3, num7), new Rectangle?(new Rectangle(0, 0, 1, 1)), color);
					this.splashScreenSpriteBatch.End();
				}
			}
		}

		public Main main;

		public ContentManager content;

		public GraphicsDevice graphicsDevice;

		public SpriteBatch splashScreenSpriteBatch;

		public SpriteFont DEFAULT_FONT;

		public SpriteFont LOCALIZED_FONT_ARIAL;

		public Texture2D splashScreen_texture;

		public Texture2D splashScreen_text;

		public Texture2D splashScreen_ring;

		public Texture2D cursor_texture;

		private Texture2D progressBarTexture;

		private ContentManager loadingScreenContent;

		private float loadingProgress;
	}
}
