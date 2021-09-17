using System;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	public abstract class Effect : IDisposable
	{
		protected virtual void Init()
		{
		}

		public Effect()
		{
			this.mUpdateCount = 0;
			this.mIs3D = GameApp.gApp.mGraphicsDriver.Is3D();
		}

		public virtual void Dispose()
		{
		}

		public abstract void Update();

		public abstract string GetName();

		public virtual void DrawUnderBalls(Graphics g)
		{
		}

		public virtual void DrawAboveBalls(Graphics g)
		{
		}

		public virtual void DrawUnderBackground(Graphics g)
		{
		}

		public virtual void LevelStarted(bool from_load)
		{
		}

		public virtual void DrawFullScene(Graphics g)
		{
		}

		public virtual void DrawFullSceneNoFrog(Graphics g)
		{
		}

		public virtual void DrawPriority(Graphics g, int priority)
		{
		}

		public virtual bool DrawTunnel(Graphics g, Image img, int x, int y)
		{
			return true;
		}

		public virtual void Reset(string level_id)
		{
			if (level_id.Length == 0)
			{
				return;
			}
			char c = level_id.get_Chars(0);
			if (c >= 'a' && c <= 'z')
			{
				c -= ' ';
				this.mLevelId = c + level_id.Substring(1);
			}
			else
			{
				this.mLevelId = level_id;
			}
			if (this.mResGroup.Length > 0 && GameApp.gApp.mResourceManager.IsGroupLoaded(this.mResGroup))
			{
				this.Init();
			}
		}

		public virtual void LoadResources()
		{
			if (this.mResGroup.Length == 0 || GameApp.gApp.mResourceManager.IsGroupLoaded(this.mResGroup))
			{
				return;
			}
			GameApp.gApp.mResourceManager.LoadResources(this.mResGroup);
			this.Init();
		}

		public virtual void DeleteResources()
		{
			if (this.mResGroup.Length == 0 || !GameApp.gApp.mResourceManager.IsGroupLoaded(this.mResGroup))
			{
				return;
			}
			GameApp.gApp.mResourceManager.DeleteResources(this.mResGroup);
		}

		public virtual void BulletFired(Bullet b)
		{
		}

		public virtual bool DrawSkullPit(Graphics g, HoleMgr hole)
		{
			return false;
		}

		public virtual void UserDied()
		{
		}

		public virtual void NukeParams()
		{
		}

		public virtual void SetParams(string key, string value)
		{
		}

		public virtual void BulletHit(Bullet b)
		{
		}

		public virtual void CopyFrom(Effect e)
		{
			this.Reset(this.mLevelId);
			this.mUpdateCount = e.mUpdateCount;
			this.mIs3D = e.mIs3D;
			this.mResGroup = e.mResGroup;
			this.mLevelId = e.mLevelId;
		}

		protected int mUpdateCount;

		protected bool mIs3D;

		protected string mResGroup;

		protected string mLevelId;
	}
}
