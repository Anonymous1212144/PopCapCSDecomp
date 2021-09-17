using System;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	public class DarkFrogBulletFX
	{
		public DarkFrogBulletFX()
		{
		}

		public DarkFrogBulletFX(int id)
		{
			this.mBulletId = id;
		}

		public PIEffect mBallEffect;

		public PIEffect mBallExplosion;

		public float mTwirlAngle;

		public float mX = -1000f;

		public float mY = -1000f;

		public bool mExploding;

		public int mBulletId = -1;
	}
}
