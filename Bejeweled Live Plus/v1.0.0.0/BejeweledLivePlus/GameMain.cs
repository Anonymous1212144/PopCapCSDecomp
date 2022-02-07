using System;
using System.Globalization;
using BejeweledLivePlus.Localization;
using Microsoft.Phone.Shell;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using SexyFramework;
using SexyFramework.Drivers.App;
using SexyFramework.Misc;

namespace BejeweledLivePlus
{
	public class GameMain : Game
	{
		public GamerServicesComponent GamerService
		{
			get
			{
				return this.mGamerService;
			}
		}

		public GameMain()
		{
			base.Content = new WP7ContentManager(base.Services);
			base.Content.RootDirectory = "Content";
			base.IsFixedTimeStep = true;
			this.theApp = new BejeweledLivePlusApp(this);
			GlobalMembers.gSexyApp = this.theApp;
			GlobalMembers.gSexyAppBase = this.theApp;
			GlobalMembers.gApp = this.theApp;
			this.mGamerService = new GamerServicesComponent(this);
			base.Components.Add(this.mGamerService);
			Guide.SimulateTrialMode = false;
			Guide.SimulateTrialMode = false;
			this.mAppService = PhoneApplicationService.Current;
			this.mAppService.Activated += new EventHandler<ActivatedEventArgs>(this.OnServiceActivated);
			this.mAppService.Deactivated += new EventHandler<DeactivatedEventArgs>(this.OnServiceDeactivated);
		}

		protected override void Initialize()
		{
			base.Initialize();
			Strings.Culture = CultureInfo.CurrentCulture;
			this.mSpriteBatch = new SpriteBatch(base.GraphicsDevice);
			this.mSplash = base.Content.Load<Texture2D>("Default-Landscape");
			this.mSplashCopyRight = base.Content.Load<Texture2D>("copyright/" + Strings.Legal);
			this.mSpriteFont = base.Content.Load<SpriteFont>("Arial_20");
			this.preTime = DateTime.Now;
		}

		protected override void LoadContent()
		{
		}

		protected override void UnloadContent()
		{
		}

		protected override void Update(GameTime gameTime)
		{
			if (this.theApp.WantExit)
			{
				base.Exit();
			}
			try
			{
				if (!Guide.IsVisible)
				{
					base.Update(gameTime);
				}
			}
			catch (GameUpdateRequiredException ex)
			{
				this.theApp.HandleGameUpdateRequired(ex);
			}
			try
			{
				this.UpdateInput(gameTime);
			}
			catch (GameUpdateRequiredException ex2)
			{
				this.theApp.HandleGameUpdateRequired(ex2);
			}
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
			if (!this.mIsLoading)
			{
				try
				{
					this.theApp.Update(gameTime.ElapsedGameTime.Seconds);
					return;
				}
				catch (GameUpdateRequiredException ex3)
				{
					this.theApp.HandleGameUpdateRequired(ex3);
					return;
				}
			}
			this.mElipseTime += gameTime.ElapsedGameTime.TotalSeconds;
			if (!this.mInitBegin)
			{
				GC.Collect();
				this.theApp.ReadFromRegistry();
				this.theApp.Init();
				this.theApp.Start();
				this.mInitBegin = true;
				return;
			}
			if (this.mElipseTime >= 3.0)
			{
				this.mIsLoading = false;
				DateTime now = DateTime.Now;
				now - this.preTime;
			}
		}

		protected override void Draw(GameTime gameTime)
		{
			if (this.mIsLoading)
			{
				this.mSpriteBatch.Begin();
				this.mSpriteBatch.Draw(this.mSplash, new Rectangle(0, 0, 480, 800), Color.White);
				this.mSpriteBatch.Draw(this.mSplashCopyRight, new Rectangle(0, 736, 480, 64), Color.White);
				this.mSpriteBatch.End();
			}
			else
			{
				if (this.mSplash != null)
				{
					this.mSplash.Dispose();
					this.mSplash = null;
					this.mSplashCopyRight.Dispose();
					this.mSplashCopyRight = null;
				}
				this.theApp.Draw(0);
			}
			base.Draw(gameTime);
		}

		protected override void OnActivated(object sender, EventArgs args)
		{
			this.theApp.OnActivated();
			base.OnActivated(sender, args);
		}

		protected override void OnDeactivated(object sender, EventArgs args)
		{
			if (this.mIsLoading)
			{
				this.mElipseTime -= 2.0;
			}
			this.theApp.OnDeactivated();
			base.OnDeactivated(sender, args);
		}

		protected override void OnExiting(object sender, EventArgs args)
		{
			if (this.theApp.IsLoadingCompleted())
			{
				this.theApp.OnExiting();
				this.theApp.RegistrySave();
			}
		}

		protected void OnServiceActivated(object sender, EventArgs args)
		{
			this.theApp.OnServiceActivated();
		}

		protected void OnServiceDeactivated(object sender, EventArgs args)
		{
			this.theApp.OnServiceDeactivated();
		}

		private void UpdateInput(GameTime gameTime)
		{
			bool flag = GamePad.GetState(0).Buttons.Back == 1;
			this.subTime += gameTime.ElapsedGameTime.TotalSeconds;
			if (flag)
			{
				if (this.subTime > 0.40000000596046448)
				{
					this.subTime = 0.0;
					if (this.mIsLoading)
					{
						base.Exit();
					}
					else
					{
						this.theApp.OnHardwardBackButtonPressed();
					}
				}
			}
			this.theApp.GetTouchInputOffset(ref this.mGameOffsetX, ref this.mGameOffsetY);
			TouchCollection state = TouchPanel.GetState();
			if (!this.mIsTracking)
			{
				using (TouchCollection.Enumerator enumerator = state.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						TouchLocation touchLocation = enumerator.Current;
						if (touchLocation.State == 2)
						{
							this.mIsTracking = true;
							this.mTouchID = touchLocation.Id;
							this.mTouchX = touchLocation.Position.X;
							this.mTouchY = touchLocation.Position.Y;
							float num = (this.mTouchX - (float)this.mGameOffsetX) * this.mGameScaleRatio;
							float num2 = (this.mTouchY - (float)this.mGameOffsetY) * this.mGameScaleRatio;
							this.mTouch.SetTouchInfo(new Point((int)num, (int)num2), _TouchPhase.TOUCH_BEGAN, DateTime.Now.TimeOfDay.TotalMilliseconds);
							this.theApp.TouchBegan(this.mTouch);
							break;
						}
					}
					return;
				}
			}
			TouchLocation touchLocation2 = default(TouchLocation);
			bool flag2 = false;
			foreach (TouchLocation touchLocation3 in state)
			{
				if (touchLocation3.Id == this.mTouchID)
				{
					flag2 = true;
					touchLocation2 = touchLocation3;
				}
			}
			bool flag3 = true;
			if (flag2)
			{
				switch (touchLocation2.State)
				{
				case 1:
					this.mTouchX = touchLocation2.Position.X;
					this.mTouchY = touchLocation2.Position.Y;
					break;
				case 2:
				case 3:
					flag3 = false;
					this.mTouchX = touchLocation2.Position.X;
					this.mTouchY = touchLocation2.Position.Y;
					break;
				}
			}
			if (flag3)
			{
				this.mIsTracking = false;
			}
			float num3 = (this.mTouchX - (float)this.mGameOffsetX) * this.mGameScaleRatio;
			float num4 = (this.mTouchY - (float)this.mGameOffsetY) * this.mGameScaleRatio;
			this.mTouch.SetTouchInfo(new Point((int)num3, (int)num4), flag3 ? _TouchPhase.TOUCH_ENDED : _TouchPhase.TOUCH_MOVED, DateTime.Now.TimeOfDay.TotalMilliseconds);
			if (flag3)
			{
				this.theApp.TouchEnded(this.mTouch);
				return;
			}
			this.theApp.TouchMoved(this.mTouch);
		}

		private BejeweledLivePlusApp theApp;

		private SpriteBatch mSpriteBatch;

		private Texture2D mSplash;

		private Texture2D mSplashCopyRight;

		private SpriteFont mSpriteFont;

		private bool mIsLoading = true;

		private double mElipseTime;

		private bool mInitBegin;

		public PhoneApplicationService mAppService;

		private GamerServicesComponent mGamerService;

		private DateTime preTime;

		private int mGameOffsetX;

		private int mGameOffsetY;

		private float mGameScaleRatio = 1.333f;

		private SexyAppBase.Touch mTouch = new SexyAppBase.Touch();

		private bool mIsTracking;

		private int mTouchID = -1;

		private float mTouchX;

		private float mTouchY;

		private double subTime;

		public class TestData
		{
			public string str = string.Empty;
		}
	}
}
