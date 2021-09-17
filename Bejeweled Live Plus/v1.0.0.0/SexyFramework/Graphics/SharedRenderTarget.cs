using System;
using System.Collections.Generic;
using SexyFramework.Drivers;

namespace SexyFramework.Graphics
{
	public class SharedRenderTarget : IDisposable
	{
		public SharedRenderTarget()
		{
			this.mImage = null;
			this.mScreenSurface = null;
			this.mLockHandle = 0U;
		}

		public void Dispose()
		{
			DeviceImage deviceImage = this.mImage;
		}

		public DeviceImage Lock(int theWidth, int theHeight, uint additionalD3DFlags)
		{
			return this.Lock(theWidth, theHeight, additionalD3DFlags, string.Empty);
		}

		public DeviceImage Lock(int theWidth, int theHeight)
		{
			return this.Lock(theWidth, theHeight, 0U, string.Empty);
		}

		public DeviceImage Lock(int theWidth, int theHeight, uint additionalD3DFlags, string debugTag)
		{
			this.Unlock();
			uint num = 16U;
			GlobalMembers.gSexyAppBase.GetSharedRenderTargetPool().Acquire(this, theWidth, theHeight, num | additionalD3DFlags, debugTag);
			return this.mImage;
		}

		public DeviceImage LockScreenImage(string debugTag)
		{
			return this.LockScreenImage(debugTag, 0U);
		}

		public DeviceImage LockScreenImage()
		{
			return this.LockScreenImage(string.Empty, 0U);
		}

		public DeviceImage LockScreenImage(string debugTag, uint flags)
		{
			IGraphicsDriver mGraphicsDriver = GlobalMembers.gSexyAppBase.mGraphicsDriver;
			if (((mGraphicsDriver.GetRenderDevice3D().GetCapsFlags() & 256U) == 0U || (flags & 1U) == 0U) && this.Lock(mGraphicsDriver.GetScreenImage().mWidth, mGraphicsDriver.GetScreenImage().mHeight, 0U, debugTag) == null)
			{
				return null;
			}
			if (mGraphicsDriver.GetRenderDevice3D() != null)
			{
				if ((mGraphicsDriver.GetRenderDevice3D().GetCapsFlags() & 128U) != 0U)
				{
					mGraphicsDriver.GetRenderDevice3D().CopyScreenImage(this.mImage, flags);
				}
				else
				{
					Image image = mGraphicsDriver.GetRenderDevice3D().SwapScreenImage(ref this.mImage, ref this.mScreenSurface, flags);
					DeviceImage deviceImage = this.mImage;
				}
			}
			GlobalMembers.gSexyAppBase.GetSharedRenderTargetPool().UpdateEntry(this);
			return this.mImage;
		}

		public bool Unlock()
		{
			if (this.mLockHandle == 0U)
			{
				return false;
			}
			GlobalMembers.gSexyAppBase.GetSharedRenderTargetPool().Unacquire(this);
			return true;
		}

		public DeviceImage GetCurrentLockImage()
		{
			if (this.mLockHandle == 0U)
			{
				return null;
			}
			return this.mImage;
		}

		protected DeviceImage mImage;

		protected RenderSurface mScreenSurface;

		protected uint mLockHandle;

		public class Pool : IDisposable
		{
			public void Dispose()
			{
				int count = this.mEntries.Count;
				for (int i = 0; i < count; i++)
				{
					SharedRenderTarget.Pool.Entry entry = this.mEntries[i];
					SharedRenderTarget mLockOwner = entry.mLockOwner;
					if (entry.mImage != null && entry.mImage != null)
					{
						entry.mImage.Dispose();
					}
					if (entry.mScreenSurface != null)
					{
						entry.mScreenSurface.Release();
					}
				}
				this.mEntries.Clear();
			}

			public void Acquire(SharedRenderTarget outTarget, int theWidth, int theHeight, uint theD3DFlags, string debugTag)
			{
				int count = this.mEntries.Count;
				int i;
				SharedRenderTarget.Pool.Entry entry;
				for (i = 0; i < count; i++)
				{
					entry = this.mEntries[i];
					if (entry.mLockOwner == null && entry.mImage.mWidth == theWidth && entry.mImage.mHeight == theHeight && entry.mImage.GetImageFlags() == (ulong)theD3DFlags)
					{
						outTarget.mImage = entry.mImage;
						outTarget.mScreenSurface = entry.mScreenSurface;
						outTarget.mLockHandle = (uint)(i + 1);
						entry.mLockOwner = outTarget;
						entry.mLockDebugTag = ((debugTag != string.Empty) ? debugTag : "NULL");
						return;
					}
				}
				this.mEntries.Add(new SharedRenderTarget.Pool.Entry());
				entry = this.mEntries[this.mEntries.Count - 1];
				i = count++;
				entry.mImage = new DeviceImage();
				entry.mImage.AddImageFlags(theD3DFlags);
				entry.mImage.Create(theWidth, theHeight);
				entry.mImage.SetImageMode(false, false);
				entry.mImage.CreateRenderData();
				entry.mScreenSurface = null;
				Graphics graphics = new Graphics(entry.mImage);
				Graphics3D graphics3D = graphics.Get3D();
				if (graphics3D != null)
				{
					graphics3D.ClearColorBuffer(new Color(0, 0, 0, 0));
				}
				outTarget.mImage = entry.mImage;
				outTarget.mScreenSurface = entry.mScreenSurface;
				outTarget.mLockHandle = (uint)(i + 1);
				entry.mLockOwner = outTarget;
				entry.mLockDebugTag = ((debugTag != string.Empty) ? debugTag : "NULL");
			}

			public void UpdateEntry(SharedRenderTarget inTarget)
			{
				if (inTarget.mLockHandle == 0U)
				{
					return;
				}
				int num = (int)(inTarget.mLockHandle - 1U);
				int count = this.mEntries.Count;
				SharedRenderTarget.Pool.Entry entry = this.mEntries[num];
				SharedRenderTarget mLockOwner = entry.mLockOwner;
				DeviceImage mImage = inTarget.mImage;
				DeviceImage mImage2 = entry.mImage;
				entry.mScreenSurface = inTarget.mScreenSurface;
			}

			public void Unacquire(SharedRenderTarget ioTarget)
			{
				if (ioTarget.mLockHandle == 0U)
				{
					return;
				}
				int num = (int)(ioTarget.mLockHandle - 1U);
				int count = this.mEntries.Count;
				SharedRenderTarget.Pool.Entry entry = this.mEntries[num];
				SharedRenderTarget mLockOwner = entry.mLockOwner;
				DeviceImage mImage = ioTarget.mImage;
				DeviceImage mImage2 = entry.mImage;
				ioTarget.mImage = null;
				ioTarget.mScreenSurface = null;
				ioTarget.mLockHandle = 0U;
				entry.mLockOwner = null;
				entry.mLockDebugTag = "";
			}

			public void InvalidateSurfaces()
			{
				int count = this.mEntries.Count;
				for (int i = 0; i < count; i++)
				{
					SharedRenderTarget.Pool.Entry entry = this.mEntries[i];
					if (entry.mScreenSurface != null)
					{
						entry.mScreenSurface.Release();
						entry.mScreenSurface = null;
					}
					if (entry.mLockOwner != null)
					{
						entry.mLockOwner.mScreenSurface = null;
					}
				}
			}

			public void InvalidateDevice()
			{
				List<SharedRenderTarget> list = new List<SharedRenderTarget>();
				int count = this.mEntries.Count;
				for (int i = 0; i < count; i++)
				{
					SharedRenderTarget.Pool.Entry entry = this.mEntries[i];
					if (entry.mImage != null)
					{
						GlobalMembers.gSexyAppBase.Remove3DData(entry.mImage);
					}
					if (entry.mLockOwner != null)
					{
						list.Add(entry.mLockOwner);
					}
				}
				for (int j = 0; j < list.Count; j++)
				{
					list[j].Unlock();
				}
				this.InvalidateSurfaces();
			}

			public string GetInfoString()
			{
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				int num5 = 0;
				int count = this.mEntries.Count;
				for (int i = 0; i < count; i++)
				{
					SharedRenderTarget.Pool.Entry entry = this.mEntries[i];
					if (entry.mLockOwner != null)
					{
						num++;
					}
					int mWidth = entry.mImage.mWidth;
					int mHeight = entry.mImage.mHeight;
					int mWidth2 = GlobalMembers.gSexyAppBase.mWidth;
					int mHeight2 = GlobalMembers.gSexyAppBase.mHeight;
					if (mWidth == mWidth2 && mHeight == mHeight2)
					{
						num2++;
					}
					else if (mWidth == mWidth2 / 2 && mHeight == mHeight2 / 2)
					{
						num3++;
					}
					else if (mWidth == mWidth2 / 4 && mHeight == mHeight2 / 4)
					{
						num4++;
					}
					else
					{
						num5++;
					}
				}
				return string.Format("Total:{0} ({1} Full, {2} Half, {3} Quarter, {4} Other); Locked:{5}", new object[] { count, num2, num3, num4, num5, num });
			}

			protected List<SharedRenderTarget.Pool.Entry> mEntries = new List<SharedRenderTarget.Pool.Entry>();

			protected class Entry
			{
				public DeviceImage mImage;

				public RenderSurface mScreenSurface;

				public SharedRenderTarget mLockOwner;

				public string mLockDebugTag;
			}
		}

		public enum FLAGS
		{
			FLAGS_HINT_LAST_LOCK_SCREEN_IMAGE = 1
		}
	}
}
