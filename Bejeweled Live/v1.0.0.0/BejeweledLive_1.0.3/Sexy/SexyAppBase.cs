﻿using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;

namespace Sexy
{
	public class SexyAppBase : SexyAppBaseInterface, ButtonListener, DialogListener, IDisposable
	{
		public static bool UseLiveServers { get; protected set; }

		public static string mLanguage
		{
			get
			{
				return Constants.mLanguage.ToString();
			}
		}

		public bool WantsToExit { get; private set; }

		public double mMusicVolume
		{
			get
			{
				return (double)this.mMusicInterface.GetVolume();
			}
			set
			{
				this.mMusicInterface.SetVolume((float)value);
			}
		}

		public virtual void GotoInterfaceState(int state)
		{
		}

		internal virtual void NewGame()
		{
			throw new NotImplementedException();
		}

		public void PlaySong(SongType song, bool loop)
		{
			if (song == this.currentSong)
			{
				return;
			}
			this.mMusicInterface.PlayMusic((int)song, 0, 0.5f, 0.5f, loop);
			this.currentSong = song;
		}

		public virtual void PlaySample(int theSoundNum)
		{
			this.PlaySample(theSoundNum, 0);
		}

		public virtual void PlaySample(int theSoundNum, int thePan)
		{
			if (this.mSoundManager == null)
			{
				return;
			}
			SoundInstance soundInstance = this.mSoundManager.GetSoundInstance((uint)theSoundNum);
			if (soundInstance != null)
			{
				if (thePan != 0)
				{
					soundInstance.SetPan(thePan);
				}
				soundInstance.Play(false);
			}
		}

		internal static void GetWidthHeightForOrientation(UI_ORIENTATION theOrientation, ref int theWidth, ref int theHeight)
		{
			if (theOrientation == UI_ORIENTATION.UI_ORIENTATION_PORTRAIT || theOrientation == UI_ORIENTATION.UI_ORIENTATION_PORTRAIT_UPSIDE_DOWN)
			{
				if (Constants.mConstants != null)
				{
					theWidth = Constants.mConstants.BackBufferSize.X;
					theHeight = Constants.mConstants.BackBufferSize.Y;
					return;
				}
				theWidth = GlobalStaticVars.g.GraphicsDevice.PresentationParameters.BackBufferWidth;
				theHeight = GlobalStaticVars.g.GraphicsDevice.PresentationParameters.BackBufferHeight;
				return;
			}
			else
			{
				if (Constants.mConstants != null)
				{
					theWidth = Constants.mConstants.BackBufferSize.Y;
					theHeight = Constants.mConstants.BackBufferSize.X;
					return;
				}
				theWidth = GlobalStaticVars.g.GraphicsDevice.PresentationParameters.BackBufferWidth;
				theHeight = GlobalStaticVars.g.GraphicsDevice.PresentationParameters.BackBufferHeight;
				return;
			}
		}

		public SexyAppBase(Main m)
		{
			this.XnaGame = m;
			this.gSexyAppBase = this;
			this.mProdName = "ProductName";
			this.mShutdown = false;
			this.mInterfaceOrientation = UI_ORIENTATION.UI_ORIENTATION_PORTRAIT;
			this.mPaused = false;
			this.mLoaded = false;
			this.mLoadingFailed = false;
			this.mAutoStartLoadingThread = true;
			this.mLoadingThreadStarted = false;
			this.mLoadingThreadCompleted = false;
			this.mInitialized = false;
			this.mMusicEnabled = true;
			this.mContentManager = m.Content;
			SexyAppBase.GetWidthHeightForOrientation(this.mInterfaceOrientation, ref this.mWidth, ref this.mHeight);
			this.mWidgetManager = new WidgetManager(this);
			this.mResourceManager = new ResourceManager(this);
			this.mSoundManager = new XNASoundManager(this);
			this.mMusicInterface = new XNAMusicInterface(this);
			this.mMusicInterface.SetDefaultFadeIn(0f);
			this.mMusicInterface.SetDefaultFadeOut(0.006f);
			this.mMusicVolume = 0.85;
			this.mSfxVolume = 0.85;
		}

		~SexyAppBase()
		{
			this.Dispose(false);
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			this.gSexyAppBase = null;
		}

		public bool EraseFile(string theFileName)
		{
			IsolatedStorageFile userStoreForApplication = IsolatedStorageFile.GetUserStoreForApplication();
			if (userStoreForApplication.FileExists(theFileName))
			{
				userStoreForApplication.DeleteFile(theFileName);
				return !userStoreForApplication.FileExists(theFileName);
			}
			return true;
		}

		public void SetBoolean(string theId, bool theValue)
		{
			if (this.mBoolProperties.ContainsKey(theId))
			{
				this.mBoolProperties[theId] = theValue;
				return;
			}
			this.mBoolProperties.Add(theId, theValue);
		}

		public void SetInteger(string theId, int theValue)
		{
			if (this.mIntProperties.ContainsKey(theId))
			{
				this.mIntProperties[theId] = theValue;
				return;
			}
			this.mIntProperties.Add(theId, theValue);
		}

		public int GetInteger(string theId, int theDefault)
		{
			if (this.mIntProperties.ContainsKey(theId))
			{
				return this.mIntProperties[theId];
			}
			return theDefault;
		}

		public void SetDouble(string theId, double theValue)
		{
			if (this.mDoubleProperties.ContainsKey(theId))
			{
				this.mDoubleProperties[theId] = theValue;
				return;
			}
			this.mDoubleProperties.Add(theId, theValue);
		}

		public virtual void Init()
		{
			this.mWidgetManager.Resize(this.mWidth, this.mHeight);
			this.InitHook();
		}

		public virtual void InitHook()
		{
		}

		public virtual void SaveGame()
		{
		}

		public void StartLoadingThread()
		{
			if (!this.mLoadingThreadStarted)
			{
				new Thread(new ThreadStart(this.LoadingThreadProcStub))
				{
					IsBackground = true
				}.Start();
				this.mLoadingThreadStarted = true;
			}
		}

		public void LoadingThreadProcStub()
		{
			this.LoadingThreadProc();
			this.mLoadingThreadCompleted = true;
		}

		public virtual void LoadingThreadProc()
		{
		}

		public virtual void LoadingThreadCompleted()
		{
		}

		public bool FileExists(string filename)
		{
			IsolatedStorageFile userStoreForApplication = IsolatedStorageFile.GetUserStoreForApplication();
			return userStoreForApplication.FileExists(filename);
		}

		public void UpdateInput()
		{
			InputController.HandleTouchInput();
		}

		public void UpdateAudio()
		{
			this.mSoundManager.Update();
			this.mMusicInterface.Update();
		}

		public bool UpdateApp()
		{
			this.UpdateAudio();
			this.UpdateInput();
			if (SexyAppBase.FirstRun && this.ShowRunWhenLockedMessage())
			{
				SexyAppBase.FirstRun = false;
			}
			this.DoUpdateFrames();
			return true;
		}

		protected virtual bool ShowRunWhenLockedMessage()
		{
			return true;
		}

		public virtual bool DoUpdateFrames()
		{
			if (this.mLoadingThreadCompleted && !this.mLoaded)
			{
				this.mLoaded = true;
				this.LoadingThreadCompleted();
			}
			this.UpdateFrames();
			return true;
		}

		public virtual void UpdateFrames()
		{
			this.mWidgetManager.UpdateFrame();
		}

		public virtual void DrawGame(GameTime gameTime)
		{
			GlobalStaticVars.g.BeginFrame();
			this.mWidgetManager.DrawScreen();
			GlobalStaticVars.g.EndFrame();
		}

		public void DeviceOrientationChanged(UI_ORIENTATION toOrientation)
		{
		}

		public virtual void InterfaceOrientationChanged(UI_ORIENTATION toOrientation)
		{
			this.nextOrientation = new UI_ORIENTATION?(toOrientation);
			if (this.mIsOrientationLocked)
			{
				return;
			}
			SexyAppBase.GetWidthHeightForOrientation(toOrientation, ref this.mWidth, ref this.mHeight);
			this.mInterfaceOrientation = toOrientation;
			this.mWidgetManager.InterfaceOrientationChanged(toOrientation);
			Main.NeedToSetUpOrientationMatrix(toOrientation);
			Graphics.OrientationChanged();
		}

		public bool OrientationIsLandscape(UI_ORIENTATION orientation)
		{
			return orientation == UI_ORIENTATION.UI_ORIENTATION_LANDSCAPE_LEFT || orientation == UI_ORIENTATION.UI_ORIENTATION_LANDSCAPE_RIGHT;
		}

		public void AccelerometerDidAccelerate(double timestamp, double ax, double ay, double az)
		{
		}

		internal bool Is3DAccelerated()
		{
			return true;
		}

		public double GetMusicVolume()
		{
			return this.mMusicVolume;
		}

		public void SetMusicVolume(double theVolume)
		{
			this.mMusicVolume = theVolume;
		}

		public double GetSfxVolume()
		{
			return this.mSfxVolume;
		}

		public virtual void SetSfxVolume(double theVolume)
		{
			this.mSfxVolume = theVolume;
			this.mSoundManager.SetVolume(theVolume);
		}

		public void EnableMusic(bool enable)
		{
			this.mMusicEnabled = enable;
		}

		public virtual void ModalOpen()
		{
		}

		public virtual void ModalClose()
		{
		}

		public void SafeDeleteWidget(Widget theWidget)
		{
			theWidget.Dispose();
		}

		public bool KillDialog(int theDialogId, bool removeWidget, bool deleteWidget)
		{
			if (this.mDialogMap.ContainsKey(theDialogId))
			{
				Dialog dialog = this.mDialogMap[theDialogId];
				this.mDialogList.Remove(dialog);
				this.mDialogMap.Remove(theDialogId);
				if (removeWidget || deleteWidget)
				{
					this.mWidgetManager.RemoveWidget(dialog);
				}
				if (dialog.IsModal())
				{
					this.ModalClose();
					this.mWidgetManager.RemoveBaseModal(dialog);
				}
				if (deleteWidget)
				{
					this.SafeDeleteWidget(dialog);
				}
				return true;
			}
			return false;
		}

		public virtual void ShowResourceError(bool boolean)
		{
		}

		public virtual void Start()
		{
			if (this.mAutoStartLoadingThread)
			{
				this.StartLoadingThread();
			}
		}

		public virtual bool KillDialog(int theDialogId)
		{
			return this.KillDialog(theDialogId, true, true);
		}

		public virtual bool KillDialog(Dialogs theDialogId)
		{
			return this.KillDialog((int)theDialogId);
		}

		public virtual bool KillDialog(Dialog theDialog)
		{
			return this.KillDialog(theDialog.mId);
		}

		public void AddDialog(int theDialogId, Dialog theDialog)
		{
			this.KillDialog(theDialogId);
			if (theDialog.mWidth == 0)
			{
				int num = this.mWidth / 2;
				theDialog.Resize((this.mWidth - num) / 2, this.mHeight / 5, num, theDialog.GetPreferredHeight(num));
			}
			this.mDialogMap.Add(theDialogId, theDialog);
			this.mDialogList.AddLast(theDialog);
			this.mWidgetManager.AddWidget(theDialog);
			if (theDialog.IsModal())
			{
				this.mWidgetManager.AddBaseModal(theDialog);
				this.ModalOpen();
			}
		}

		public void AddDialog(Dialog theDialog)
		{
			this.AddDialog(theDialog.mId, theDialog);
		}

		public virtual Dialog DoDialog(Dialog theDialog, int theDialogId)
		{
			this.KillDialog(theDialogId);
			this.AddDialog(theDialogId, theDialog);
			return theDialog;
		}

		public int GetDialogCount()
		{
			return Enumerable.Count<KeyValuePair<int, Dialog>>(this.mDialogMap);
		}

		public Dialog GetDialog(int theDialogId)
		{
			foreach (KeyValuePair<int, Dialog> keyValuePair in this.mDialogMap)
			{
				if (keyValuePair.Key == theDialogId)
				{
					return keyValuePair.Value;
				}
			}
			return null;
		}

		public Dialog GetDialog(Dialogs theDialogId)
		{
			return this.GetDialog((int)theDialogId);
		}

		public void SetString(string theId, string theValue)
		{
			if (this.mStringProperties.ContainsKey(theId))
			{
				this.mStringProperties[theId] = theValue;
				return;
			}
			this.mStringProperties.Add(theId, theValue);
		}

		public string GetString(string theId)
		{
			if (this.mStringProperties.ContainsKey(theId))
			{
				return this.mStringProperties[theId];
			}
			return "";
		}

		public double GetLoadingThreadProgress()
		{
			if (this.mLoaded)
			{
				return 1.0;
			}
			if (!this.mLoadingThreadStarted)
			{
				return 0.0;
			}
			return 0.5;
		}

		public SexyColor HSLToRGB(int h, int s, int l)
		{
			return new SexyColor(h, s, l);
		}

		public virtual void LeftTrialMode()
		{
		}

		public bool WriteBufferToFile(string theFileName, Buffer theBuffer)
		{
			try
			{
				IsolatedStorageFile userStoreForApplication = IsolatedStorageFile.GetUserStoreForApplication();
				if (!userStoreForApplication.DirectoryExists(GlobalStaticVars.GetDocumentsDir()))
				{
					userStoreForApplication.CreateDirectory(GlobalStaticVars.GetDocumentsDir());
				}
				IsolatedStorageFileStream isolatedStorageFileStream = new IsolatedStorageFileStream(theFileName, 4, userStoreForApplication);
				isolatedStorageFileStream.Write(theBuffer.Data, 0, theBuffer.Data.Length);
				isolatedStorageFileStream.Close();
				userStoreForApplication.Dispose();
			}
			catch (Exception)
			{
				return false;
			}
			return true;
		}

		public bool ReadBufferFromFile(string theFileName, ref Buffer theBuffer, bool dontWriteToDemo)
		{
			try
			{
				IsolatedStorageFile userStoreForApplication = IsolatedStorageFile.GetUserStoreForApplication();
				if (!userStoreForApplication.FileExists(theFileName))
				{
					return false;
				}
				IsolatedStorageFileStream isolatedStorageFileStream = userStoreForApplication.OpenFile(theFileName, 4);
				byte[] array = new byte[isolatedStorageFileStream.Length];
				isolatedStorageFileStream.Read(array, 0, (int)isolatedStorageFileStream.Length);
				theBuffer.Data = array;
				isolatedStorageFileStream.Close();
				userStoreForApplication.Dispose();
			}
			catch (Exception)
			{
				return false;
			}
			return true;
		}

		private void TransformTouch(_Touch touch)
		{
		}

		public void TouchBegan(_Touch touch)
		{
			this.mWidgetManager.TouchBegan(touch);
		}

		public void TouchMoved(_Touch touch)
		{
			this.mWidgetManager.TouchMoved(touch);
		}

		public void TouchEnded(_Touch touch)
		{
			this.mWidgetManager.TouchEnded(touch);
		}

		public void TouchesCanceled()
		{
			this.mWidgetManager.TouchesCanceled();
		}

		public void ShakeBegan(double timestamp)
		{
		}

		public void ShakeEnded(double timestamp)
		{
		}

		public void ShakeCancelled(double timestamp)
		{
		}

		public virtual void BuyGame()
		{
			Guide.ShowMarketplace(0);
		}

		public virtual void ShowUpdateRequiredMessage()
		{
		}

		public virtual void DialogButtonPress(int theDialogId, int theButtonId)
		{
			if (theButtonId == 1000)
			{
				this.ButtonPress(2000 + theDialogId);
				return;
			}
			if (theButtonId == 1001)
			{
				this.ButtonPress(3000 + theDialogId);
			}
		}

		public virtual void DialogButtonDepress(int theDialogId, int theButtonId)
		{
			if (theButtonId == 1000)
			{
				this.ButtonDepress(2000 + theDialogId);
				return;
			}
			if (theButtonId == 1001)
			{
				this.ButtonDepress(3000 + theDialogId);
			}
		}

		public virtual void ButtonPress(int theId)
		{
		}

		public virtual void ButtonPress(int theId, int theClickCount)
		{
		}

		public virtual void ButtonDepress(int theId)
		{
		}

		public void ButtonDownTick(int theId)
		{
		}

		public void ButtonMouseEnter(int theId)
		{
		}

		public void ButtonMouseLeave(int theId)
		{
		}

		public void ButtonMouseMove(int theId, int theX, int theY)
		{
		}

		public virtual void GotFocus()
		{
		}

		public virtual void LostFocus()
		{
		}

		public virtual void Tombstoned()
		{
		}

		public UI_ORIENTATION GetOrientation()
		{
			return this.mInterfaceOrientation;
		}

		public virtual bool ShouldAutorotateToInterfaceOrientation(UI_ORIENTATION theOrientation)
		{
			return !this.mIsOrientationLocked;
		}

		public void WriteToRegistry()
		{
		}

		public bool RegistryWriteString(string theValueName, string theString)
		{
			return true;
		}

		public int RegistryReadInteger(string theValueName)
		{
			return 0;
		}

		public void RegistryWriteInteger(string theValueName, int theValue)
		{
		}

		public void DoVibration()
		{
		}

		public void LockOrientation(bool theFlag)
		{
			if (theFlag)
			{
				GraphicsState.mGraphicsDeviceManager.SupportedOrientations = this.XnaGame.Window.CurrentOrientation;
			}
			else
			{
				GraphicsState.mGraphicsDeviceManager.SupportedOrientations = Constants.SupportedOrientations;
			}
			if (this.XnaGame.Window.CurrentOrientation == 4)
			{
				GraphicsState.mGraphicsDeviceManager.PreferredBackBufferWidth = Constants.mConstants.BackBufferSize.X;
				GraphicsState.mGraphicsDeviceManager.PreferredBackBufferHeight = Constants.mConstants.BackBufferSize.Y;
			}
			else
			{
				GraphicsState.mGraphicsDeviceManager.PreferredBackBufferWidth = Constants.mConstants.BackBufferSize.Y;
				GraphicsState.mGraphicsDeviceManager.PreferredBackBufferHeight = Constants.mConstants.BackBufferSize.X;
			}
			GraphicsState.mGraphicsDeviceManager.ApplyChanges();
		}

		protected void ProcessSafeDeleteList()
		{
		}

		public bool IsLandscape()
		{
			return this.mInterfaceOrientation == UI_ORIENTATION.UI_ORIENTATION_LANDSCAPE_LEFT || this.mInterfaceOrientation == UI_ORIENTATION.UI_ORIENTATION_LANDSCAPE_RIGHT;
		}

		public bool IsPortrait()
		{
			return !this.IsLandscape();
		}

		public virtual bool BackButtonPress()
		{
			if (!this.mLoadingThreadCompleted)
			{
				this.WantsToExit = true;
			}
			return this.mWidgetManager.BackButtonPress();
		}

		public virtual void AppExit()
		{
			this.WantsToExit = true;
		}

		public static bool IsInTrialMode
		{
			get
			{
				return Main.IsInTrialMode;
			}
		}

		public Main XnaGame;

		protected UI_ORIENTATION? nextOrientation;

		public static object SplashScreenDrawLock = new object();

		public static bool FirstRun;

		public SexyAppBase gSexyAppBase;

		public string mCompanyName;

		public string mFullCompanyName;

		public string mProdName;

		public string mTitle;

		public int mWidth;

		public int mHeight;

		public UI_ORIENTATION mInterfaceOrientation;

		public bool mMusicEnabled;

		public double mSfxVolume;

		public bool mShutdown;

		public bool mInitialized;

		public MusicInterface mMusicInterface;

		public WidgetManager mWidgetManager;

		public ResourceManager mResourceManager;

		public ContentManager mContentManager;

		public SoundManager mSoundManager;

		public bool mPaused;

		public bool mAutoStartLoadingThread;

		public bool mLoadingThreadStarted;

		public bool mLoadingThreadCompleted;

		public bool mLoaded;

		public bool mLoadingFailed;

		public bool mIsOrientationLocked;

		public int mUpdateCount;

		public Dictionary<int, Dialog> mDialogMap = new Dictionary<int, Dialog>();

		public int mUpdateAppDepth;

		public int mNumLoadingThreadTasks;

		public int mMuteCount;

		private Dictionary<string, Image> mRegisteredImages = new Dictionary<string, Image>();

		public Dictionary<string, string> mStringProperties = new Dictionary<string, string>();

		public Dictionary<string, List<string>> mStringVectorProperties = new Dictionary<string, List<string>>();

		public Dictionary<string, bool> mBoolProperties = new Dictionary<string, bool>();

		public Dictionary<string, int> mIntProperties = new Dictionary<string, int>();

		public Dictionary<string, double> mDoubleProperties = new Dictionary<string, double>();

		public bool mReadFromRegistry;

		public LinkedList<Dialog> mDialogList = new LinkedList<Dialog>();

		public int mCompletedLoadingThreadTasks;

		private static Random rand = new Random(DateTime.Now.Millisecond);

		private SongType currentSong = SongType.MAX;
	}
}
