using System;
using Microsoft.Phone.Shell;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace Sexy
{
	public class Main : Game
	{
		public Main()
		{
			Main.SetupTileSchedule();
			this.graphics = Graphics.GetNew(this);
			try
			{
				XNAMusicInterface.UserMusicVolume = MediaPlayer.Volume;
			}
			catch
			{
			}
			this.graphics.IsFullScreen = true;
			this.graphics.PreferredBackBufferWidth = Constants.StartWidth;
			this.graphics.PreferredBackBufferHeight = Constants.StartHeight;
			GraphicsState.mGraphicsDeviceManager.SupportedOrientations = Constants.SupportedOrientations;
			GraphicsState.mGraphicsDeviceManager.DeviceCreated += new EventHandler<EventArgs>(this.graphics_DeviceCreated);
			GraphicsState.mGraphicsDeviceManager.DeviceReset += new EventHandler<EventArgs>(this.graphics_DeviceReset);
			GraphicsState.mGraphicsDeviceManager.PreparingDeviceSettings += new EventHandler<PreparingDeviceSettingsEventArgs>(this.mGraphicsDeviceManager_PreparingDeviceSettings);
			base.IsFixedTimeStep = true;
			base.TargetElapsedTime = TimeSpan.FromSeconds(0.01);
			base.Exiting += new EventHandler<EventArgs>(this.Main_Exiting);
			PhoneApplicationService.Current.UserIdleDetectionMode = 1;
			PhoneApplicationService.Current.Launching += new EventHandler<LaunchingEventArgs>(this.Game_Launching);
			PhoneApplicationService.Current.Activated += new EventHandler<ActivatedEventArgs>(this.Game_Activated);
			PhoneApplicationService.Current.Closing += new EventHandler<ClosingEventArgs>(this.Current_Closing);
			PhoneApplicationService.Current.Deactivated += new EventHandler<DeactivatedEventArgs>(this.Current_Deactivated);
		}

		private void Current_Deactivated(object sender, DeactivatedEventArgs e)
		{
			GlobalStaticVars.gSexyAppBase.Tombstoned();
		}

		private void Current_Closing(object sender, ClosingEventArgs e)
		{
			PhoneApplicationService.Current.State.Clear();
		}

		private void Game_Activated(object sender, ActivatedEventArgs e)
		{
		}

		private void Game_Launching(object sender, LaunchingEventArgs e)
		{
			PhoneApplicationService.Current.State.Clear();
		}

		public static bool RunWhenLocked
		{
			get
			{
				return PhoneApplicationService.Current.ApplicationIdleDetectionMode == 1;
			}
			set
			{
				try
				{
					PhoneApplicationService.Current.ApplicationIdleDetectionMode = (value ? 1 : 0);
				}
				catch
				{
				}
			}
		}

		private static void SetupTileSchedule()
		{
		}

		private void mGraphicsDeviceManager_PreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
		{
		}

		private void graphics_DeviceReset(object sender, EventArgs e)
		{
		}

		private void graphics_DeviceCreated(object sender, EventArgs e)
		{
		}

		private void Main_Exiting(object sender, EventArgs e)
		{
			GlobalStaticVars.gSexyAppBase.AppExit();
			try
			{
				MediaPlayer.Volume = XNAMusicInterface.UserMusicVolume;
			}
			catch
			{
			}
		}

		protected override void Initialize()
		{
			GlobalStaticVars.initialize(this);
			base.Window.OrientationChanged += new EventHandler<EventArgs>(this.Window_OrientationChanged);
			base.Initialize();
		}

		protected override void LoadContent()
		{
			GraphicsState.Init();
			GlobalStaticVars.mGlobalContent.LoadSplashScreen();
			GlobalStaticVars.gSexyAppBase.StartLoadingThread();
		}

		protected override void UnloadContent()
		{
			GlobalStaticVars.mGlobalContent.cleanUp();
		}

		protected override void BeginRun()
		{
			base.BeginRun();
			try
			{
				XNAMusicInterface.PlayingUserMusic = !MediaPlayer.GameHasControl;
			}
			catch (Exception)
			{
				XNAMusicInterface.PlayingUserMusic = true;
			}
		}

		protected override void Update(GameTime gameTime)
		{
			if (!base.IsActive)
			{
				return;
			}
			if (GlobalStaticVars.gSexyAppBase.WantsToExit)
			{
				base.Exit();
			}
			this.HandleInput(gameTime);
			GlobalStaticVars.gSexyAppBase.UpdateApp();
			if (this.mFrameCnt % 3 != 0 || Main.wantToSuppressDraw)
			{
				Main.wantToSuppressDraw = false;
				base.SuppressDraw();
			}
			this.mFrameCnt++;
			if (!Main.trialModeChecked)
			{
				bool flag = Main.trialModeCachedValue;
				Main.trialModeCachedValue = Guide.IsTrialMode;
				if (flag != Main.trialModeCachedValue && flag)
				{
					this.LeftTrialMode();
				}
				Main.trialModeChecked = true;
			}
			try
			{
				base.Update(gameTime);
			}
			catch (GameUpdateRequiredException ex)
			{
				string message = ex.Message;
				GlobalStaticVars.gSexyAppBase.ShowUpdateRequiredMessage();
			}
			catch (Exception ex2)
			{
				string message2 = ex2.Message;
			}
		}

		private void LeftTrialMode()
		{
			if (GlobalStaticVars.gSexyAppBase != null)
			{
				GlobalStaticVars.gSexyAppBase.LeftTrialMode();
			}
			this.Window_OrientationChanged(null, null);
		}

		public static void SuppressNextDraw()
		{
			Main.wantToSuppressDraw = true;
		}

		public static SignedInGamer GetGamer()
		{
			if (Gamer.SignedInGamers.Count == 0)
			{
				return null;
			}
			return Gamer.SignedInGamers[0];
		}

		public static void NeedToSetUpOrientationMatrix(UI_ORIENTATION orientation)
		{
			Main.orientationUsed = orientation;
			Main.newOrientation = true;
		}

		private static void SetupOrientationMatrix(UI_ORIENTATION orientation)
		{
			Main.newOrientation = false;
		}

		private void Window_OrientationChanged(object sender, EventArgs e)
		{
			this.SetupInterfaceOrientation();
		}

		private void SetupInterfaceOrientation()
		{
			if (GlobalStaticVars.gSexyAppBase != null)
			{
				if (base.Window.CurrentOrientation == 1 || base.Window.CurrentOrientation == 2)
				{
					GlobalStaticVars.gSexyAppBase.InterfaceOrientationChanged(UI_ORIENTATION.UI_ORIENTATION_LANDSCAPE_LEFT);
					return;
				}
				GlobalStaticVars.gSexyAppBase.InterfaceOrientationChanged(UI_ORIENTATION.UI_ORIENTATION_PORTRAIT);
			}
		}

		protected override void Draw(GameTime gameTime)
		{
			if (Main.newOrientation)
			{
				Main.SetupOrientationMatrix(Main.orientationUsed);
			}
			base.GraphicsDevice.Clear(Color.Black);
			GlobalStaticVars.gSexyAppBase.DrawGame(gameTime);
			base.Draw(gameTime);
		}

		public void HandleInput(GameTime gameTime)
		{
			GamePadState state = GamePad.GetState(0);
			if (state.Buttons.Back == 1 && this.previousGamepadState.Buttons.Back == null)
			{
				GlobalStaticVars.gSexyAppBase.BackButtonPress();
			}
			TouchCollection state2 = TouchPanel.GetState();
			bool flag = false;
			foreach (TouchLocation touchLocation in state2)
			{
				_Touch touch = default(_Touch);
				touch.location.mX = touchLocation.Position.X;
				touch.location.mY = touchLocation.Position.Y;
				touch.timestamp = gameTime.TotalGameTime.TotalSeconds;
				if (touchLocation.State == 2 && !flag)
				{
					GlobalStaticVars.gSexyAppBase.TouchBegan(touch);
					flag = true;
				}
				else if (touchLocation.State == 3)
				{
					GlobalStaticVars.gSexyAppBase.TouchMoved(touch);
				}
				else if (touchLocation.State == 1)
				{
					GlobalStaticVars.gSexyAppBase.TouchEnded(touch);
				}
				else if (touchLocation.State == null)
				{
					GlobalStaticVars.gSexyAppBase.TouchesCanceled();
				}
			}
			this.previousGamepadState = state;
		}

		protected override void OnActivated(object sender, EventArgs args)
		{
			Main.trialModeChecked = false;
			GlobalStaticVars.gSexyAppBase.GotFocus();
			if (!XNAMusicInterface.PlayingUserMusic)
			{
				GlobalStaticVars.gSexyAppBase.mMusicInterface.ResumeMusic();
			}
			base.OnActivated(sender, args);
			double mMusicVolume = GlobalStaticVars.gSexyAppBase.mMusicVolume;
			GlobalStaticVars.gSexyAppBase.mMusicVolume = mMusicVolume;
		}

		protected override void OnDeactivated(object sender, EventArgs args)
		{
			GlobalStaticVars.gSexyAppBase.LostFocus();
			if (!XNAMusicInterface.PlayingUserMusic)
			{
				GlobalStaticVars.gSexyAppBase.mMusicInterface.PauseMusic();
			}
			base.OnDeactivated(sender, args);
		}

		public static bool IsInTrialMode
		{
			get
			{
				return Main.trialModeCachedValue;
			}
		}

		private static SexyTransform2D orientationTransform;

		private static UI_ORIENTATION orientationUsed;

		private static bool newOrientation;

		private static bool trialModeChecked = false;

		private static bool trialModeCachedValue;

		public Graphics graphics;

		public static GamerServicesComponent GamerServicesComp;

		private int mFrameCnt;

		private static bool wantToSuppressDraw;

		private GamePadState previousGamepadState = default(GamePadState);
	}
}
