using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Resource;

namespace SexyFramework.Drivers.Graphics
{
	public class XNAGraphicsDriver : IGraphicsDriver
	{
		public override int GetVersion()
		{
			return 0;
		}

		public override bool Is3D()
		{
			return true;
		}

		public void ClearColorBuffer(Color color)
		{
			this.mXNARenderDevice.ClearColorBuffer(color);
		}

		public override ulong GetRenderModeFlags()
		{
			throw new NotImplementedException();
		}

		public override void SetRenderModeFlags(ulong flag)
		{
			throw new NotImplementedException();
		}

		public override IGraphicsDriver.ERenderMode GetRenderMode()
		{
			throw new NotImplementedException();
		}

		public override void SetRenderMode(IGraphicsDriver.ERenderMode inRenderMode)
		{
			throw new NotImplementedException();
		}

		public override string GetRenderModeString(IGraphicsDriver.ERenderMode inRenderMode, ulong inRenderModeFlags, bool inIgnoreMode, bool inIgnoreFlags)
		{
			throw new NotImplementedException();
		}

		public override void AddDeviceImage(DeviceImage theDDImage)
		{
			throw new NotImplementedException();
		}

		public override void RemoveDeviceImage(DeviceImage theDDImage)
		{
			throw new NotImplementedException();
		}

		public override void Remove3DData(MemoryImage theImage)
		{
			if (theImage == null)
			{
				return;
			}
			this.mXNARenderDevice.RemoveImageRenderData(theImage);
		}

		public override DeviceImage GetScreenImage()
		{
			return null;
		}

		public override int GetScreenWidth()
		{
			return this.mWidth;
		}

		public override int GetScreenHeight()
		{
			return this.mHeight;
		}

		public override void WindowResize(int theWidth, int theHeight)
		{
			throw new NotImplementedException();
		}

		public override bool Redraw(Rect theClipRect)
		{
			Rect rect = new Rect(0, 0, this.mWidth, this.mHeight);
			this.mXNARenderDevice.Present(rect, rect);
			return true;
		}

		public override void RemapMouse(ref int theX, ref int theY)
		{
			throw new NotImplementedException();
		}

		public override bool SetCursorImage(Image theImage)
		{
			throw new NotImplementedException();
		}

		public override void SetCursorPos(int theCursorX, int theCursorY)
		{
			throw new NotImplementedException();
		}

		public override void RemoveShader(object theShader)
		{
			throw new NotImplementedException();
		}

		public override DeviceSurface CreateDeviceSurface()
		{
			throw new NotImplementedException();
		}

		public override NativeDisplay GetNativeDisplayInfo()
		{
			throw new NotImplementedException();
		}

		public override RenderDevice GetRenderDevice()
		{
			return this.mXNARenderDevice;
		}

		public override RenderDevice3D GetRenderDevice3D()
		{
			return this.mXNARenderDevice;
		}

		public override Ratio GetAspectRatio()
		{
			return new Ratio(4, 3);
		}

		public override int GetDisplayWidth()
		{
			return this.mWidth;
		}

		public override int GetDisplayHeight()
		{
			return this.mHeight;
		}

		public override CritSect GetCritSect()
		{
			throw new NotImplementedException();
		}

		public override Mesh LoadMesh(string thePath, MeshListener theListener)
		{
			throw new NotImplementedException();
		}

		public override void AddMesh(Mesh theMesh)
		{
			throw new NotImplementedException();
		}

		public XNAGraphicsDriver(Game game, SexyAppBase app)
		{
			this.mMainGame = game;
			this.mApp = app;
			this.mWidth = this.mApp.mWidth;
			this.mHeight = this.mApp.mHeight;
			this.mXNARenderDevice = new BaseXNARenderDevice(this.mMainGame);
		}

		public override void Dispose()
		{
			this.mXNARenderDevice = null;
		}

		public Game GetMainGame()
		{
			return this.mMainGame;
		}

		public virtual void Init()
		{
			this.mXNARenderDevice.Init(this.mWidth, this.mHeight);
		}

		public void Update(long gameTime)
		{
		}

		public void Draw(long gameTime)
		{
		}

		public virtual object GetOptimizedRenderData(string theFileName)
		{
			PFILE pfile = new PFILE(theFileName, "");
			pfile.Open<Texture2D>();
			return pfile.GetObject();
		}

		public virtual DeviceImage GetOptimizedImage(string theFileName, bool commitBits, bool allowTriReps)
		{
			Texture2D texture2D = this.mMainGame.Content.Load<Texture2D>(theFileName);
			texture2D.Name = theFileName;
			return this.mXNARenderDevice.GetOptimizedImage(texture2D, commitBits, allowTriReps);
		}

		public virtual DeviceImage GetOptimizedImage(Stream stream, bool commitBits, bool allowTriReps)
		{
			if (stream != null)
			{
				try
				{
					Texture2D texture = Texture2D.FromStream(this.mXNARenderDevice.mDevice.GraphicsDevice, stream);
					return this.mXNARenderDevice.GetOptimizedImage(texture, commitBits, allowTriReps);
				}
				catch (Exception)
				{
					return null;
				}
			}
			return null;
		}

		public override void SetRenderRect(int theX, int theY, int theWidth, int theHeight)
		{
			this.mXNARenderDevice.SetRenderRect(theX, theY, theWidth, theHeight);
		}

		private Game mMainGame;

		private SexyAppBase mApp;

		public BaseXNARenderDevice mXNARenderDevice;

		private DeviceImage mScreenImage;

		public int mWidth;

		public int mHeight;
	}
}
