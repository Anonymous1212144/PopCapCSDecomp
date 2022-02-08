using System;
using Microsoft.Phone.Shell;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using SexyFramework;
using SexyFramework.Drivers.App;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	public class GameMain : Game
	{
		public GameMain()
		{
			base.Content = new WP7ContentManager(base.Services);
			base.Content.RootDirectory = "Content";
			base.TargetElapsedTime = TimeSpan.FromTicks(166666L);
			base.IsFixedTimeStep = true;
			this.SexyZuma = new GameApp(this, false);
			GlobalMembers.gSexyApp = this.SexyZuma;
			GlobalMembers.gSexyAppBase = this.SexyZuma;
			this.gApplicationService = PhoneApplicationService.Current;
			this.gApplicationService.Deactivated += new EventHandler<DeactivatedEventArgs>(this.OnServiceDeactivated);
			this.gApplicationService.Activated += new EventHandler<ActivatedEventArgs>(this.OnServiceActivated);
			base.Components.Add(new GamerServicesComponent(this));
			Guide.SimulateTrialMode = false;
		}

		protected override void Initialize()
		{
			base.Initialize();
			this.spriteBatch = new SpriteBatch(base.GraphicsDevice);
			this.mSpriteFont = base.Content.Load<SpriteFont>("Arial_20");
			base.Window.OrientationChanged += new EventHandler<EventArgs>(this.OrientationChanged);
			this.SexyZuma.InitText();
			if (Localization.GetCurrentLanguage() != Localization.LanguageType.Language_FR)
			{
				this.splash = base.Content.Load<Texture2D>("Default-Landscape");
				return;
			}
			this.splash = base.Content.Load<Texture2D>("LoadingImage_DarkFrog_French");
		}

		protected override void LoadContent()
		{
		}

		protected override void UnloadContent()
		{
		}

		protected override void Update(GameTime gameTime)
		{
			if (GameApp.mExit)
			{
				base.Exit();
			}
			bool isRunningSlowly = gameTime.IsRunningSlowly;
			try
			{
				if (!Guide.IsVisible)
				{
					base.Update(gameTime);
				}
			}
			catch (GameUpdateRequiredException ex)
			{
				if (GameApp.USE_XBOX_SERVICE)
				{
					this.SexyZuma.HandleGameUpdateRequired(ex);
				}
			}
			this.UpdateInput(gameTime);
			try
			{
				if (Guide.IsVisible)
				{
					return;
				}
			}
			catch (Exception)
			{
			}
			if (!this.isLoading)
			{
				this.SexyZuma.Update(gameTime.ElapsedGameTime.Seconds);
				return;
			}
			this.mElipseTime += gameTime.ElapsedGameTime.TotalSeconds;
			if (!this.mInitBegin)
			{
				GC.Collect();
				this.SexyZuma.StartThreadInit();
				this.mInitBegin = true;
				return;
			}
			if (this.SexyZuma.mInitFinished && this.mElipseTime >= 4.0)
			{
				this.SexyZuma.ShowLoadingScreen();
				this.isLoading = false;
			}
		}

		protected override void Draw(GameTime gameTime)
		{
			if (this.isLoading)
			{
				base.GraphicsDevice.Clear(Color.Black);
				this.spriteBatch.Begin();
				this.mColor = new Color((int)((byte)MathHelper.Clamp(this.mAlpha, 0f, 255f)), (int)((byte)MathHelper.Clamp(this.mAlpha, 0f, 255f)), (int)((byte)MathHelper.Clamp(this.mAlpha, 0f, 255f)), (int)((byte)MathHelper.Clamp(this.mAlpha, 0f, 255f)));
				this.spriteBatch.Draw(this.splash, new Rectangle(0, 0, 800, 480), this.mColor);
				this.spriteBatch.End();
			}
			else
			{
				if (this.splash != null)
				{
					this.splash.Dispose();
					this.splash = null;
				}
				this.SexyZuma.Draw(0);
			}
			base.Draw(gameTime);
		}

		protected override void OnExiting(object sender, EventArgs args)
		{
			this.SexyZuma.OnExiting();
		}

		protected override void OnActivated(object sender, EventArgs args)
		{
			this.SexyZuma.OnActivated();
			base.OnActivated(sender, args);
		}

		protected override void OnDeactivated(object sender, EventArgs args)
		{
			if (!this.SexyZuma.mInitFinished)
			{
				this.mElipseTime -= 2.0;
			}
			this.SexyZuma.OnExiting();
			this.SexyZuma.OnDeactivated();
			base.OnDeactivated(sender, args);
		}

		protected void OnServiceActivated(object sender, EventArgs args)
		{
			this.SexyZuma.OnServiceActivated();
		}

		protected void OnServiceDeactivated(object sender, EventArgs args)
		{
			this.SexyZuma.OnServiceDeactivated();
		}

		private void UpdateInput(GameTime gameTime)
		{
			if (GamePad.GetState(0).Buttons.Back == 1)
			{
				if (this.isLoading)
				{
					base.Exit();
				}
				else
				{
					this.SexyZuma.OnHardwareBackButtonPressed();
				}
			}
			TouchCollection state = TouchPanel.GetState();
			this.SexyZuma.GetTouchInputOffset(ref this.GameOffsetX, ref this.GameOffsetY);
			if (state.Count > 0)
			{
				using (TouchCollection.Enumerator enumerator = state.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						TouchLocation touchLocation = enumerator.Current;
						if (this.mCurrentTouchId == -1)
						{
							this.mCurrentTouchId = touchLocation.Id;
						}
						else if (touchLocation.Id != this.mCurrentTouchId)
						{
							continue;
						}
						float num = (touchLocation.Position.X - (float)this.GameOffsetX) * this.GameScaleRatio;
						float num2 = (touchLocation.Position.Y - (float)this.GameOffsetY) * this.GameScaleRatio;
						Point loc = new Point((int)num, (int)num2);
						switch (touchLocation.State)
						{
						case 1:
							this.touch.SetTouchInfo(loc, _TouchPhase.TOUCH_ENDED, DateTime.Now.TimeOfDay.TotalMilliseconds);
							this.SexyZuma.TouchEnded(this.touch);
							this.mCurrentTouchId = -1;
							break;
						case 2:
							this.touch.SetTouchInfo(loc, _TouchPhase.TOUCH_BEGAN, DateTime.Now.TimeOfDay.TotalMilliseconds);
							this.SexyZuma.TouchBegan(this.touch);
							break;
						case 3:
							this.touch.SetTouchInfo(loc, _TouchPhase.TOUCH_MOVED, DateTime.Now.TimeOfDay.TotalMilliseconds);
							this.SexyZuma.TouchMoved(this.touch);
							break;
						}
					}
					return;
				}
			}
			this.mCurrentTouchId = -1;
		}

		public void DrawSysString(string str, float x, float y)
		{
			this.spriteBatch.Begin();
			this.spriteBatch.DrawString(this.mSpriteFont, str, new Vector2(x, y), Color.Yellow);
			this.spriteBatch.End();
		}

		public void OrientationChanged(object sender, EventArgs e)
		{
			if (base.Window.CurrentOrientation == 1)
			{
				if (this.SexyZuma != null)
				{
					this.SexyZuma.SetOrientation(0);
					return;
				}
			}
			else if (this.SexyZuma != null)
			{
				this.SexyZuma.SetOrientation(1);
			}
		}

		private GameApp SexyZuma;

		private Texture2D splashEA;

		private Texture2D splash;

		private Color mColor = new Color(255, 255, 255, 255);

		private float mAlpha = 255f;

		private float mAlphaInc = -6f;

		private double mAlphaDelay = 1.0;

		private int mSplashId = 1;

		private SpriteBatch spriteBatch;

		private bool isLoading = true;

		private bool mInitBegin;

		private int FirstLoad;

		private SpriteFont mSpriteFont;

		private double mElipseTime;

		private int mCurrentTouchId = -1;

		private static int frames = 0;

		private static DateTime now;

		private static DateTime preFPSTime;

		private static string fpsDisplayText = "";

		public PhoneApplicationService gApplicationService;

		private long totalBytes;

		private long currentBytes;

		private long peakBytes;

		private long limitBytes;

		private Vector2 mFPSPos = new Vector2(60f, 10f);

		private int GameOffsetX;

		private int GameOffsetY;

		private float GameScaleRatio = 1.33f;

		private SexyAppBase.Touch touch = new SexyAppBase.Touch();
	}
}
