using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	public class CurveData
	{
		public CurveData()
		{
			this.Clear();
		}

		public virtual void Dispose()
		{
			this.mPointList = null;
			this.mVals = null;
		}

		protected bool Fail(string theString)
		{
			this.mErrorString = theString;
			return false;
		}

		public bool Save(string theFilePath)
		{
			return false;
		}

		public bool Load(string theFilePath)
		{
			this.Clear();
			Buffer buffer = new Buffer();
			if (!GameApp.gApp.ReadBufferFromStream(theFilePath + ".dat", ref buffer))
			{
				return false;
			}
			MemoryStream memoryStream = new MemoryStream(buffer.GetDataPtr());
			BinaryReader binaryReader = new BinaryReader(memoryStream);
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < 4; i++)
			{
				stringBuilder.Append((char)binaryReader.ReadByte());
			}
			if (stringBuilder.ToString() != "CURV")
			{
				Console.WriteLine("Invalid file header");
				return false;
			}
			this.mVersion = binaryReader.ReadInt32();
			if (this.mVersion < 1 || this.mVersion > CurveData.gVersion)
			{
				Console.WriteLine("Invalid file header");
				return false;
			}
			if (this.mVersion >= 8)
			{
				this.mLinear = binaryReader.ReadBoolean();
			}
			if (this.mVersion >= 7)
			{
				this.mVals.mStartDistance = (int)binaryReader.ReadUInt32();
				this.mVals.mNumBalls = (int)binaryReader.ReadUInt32();
				this.mVals.mBallRepeat = (int)binaryReader.ReadUInt32();
				this.mVals.mMaxSingle = (int)binaryReader.ReadUInt32();
				this.mVals.mNumColors = (int)binaryReader.ReadUInt32();
				if (this.mVersion <= 10)
				{
					binaryReader.ReadInt32();
					binaryReader.ReadSingle();
				}
				this.mVals.mSpeed = binaryReader.ReadSingle();
				this.mVals.mSlowDistance = (int)binaryReader.ReadUInt32();
				this.mVals.mAccelerationRate = binaryReader.ReadSingle();
				this.mVals.mOrgAccelerationRate = this.mVals.mAccelerationRate;
				this.mVals.mMaxSpeed = binaryReader.ReadSingle();
				this.mVals.mOrgMaxSpeed = this.mVals.mMaxSpeed;
				this.mVals.mScoreTarget = (int)binaryReader.ReadUInt32();
				this.mVals.mSkullRotation = (int)binaryReader.ReadUInt32();
				this.mVals.mZumaBack = (int)binaryReader.ReadUInt32();
				this.mVals.mZumaSlow = (int)binaryReader.ReadUInt32();
				if (this.mVersion >= 13)
				{
					this.mVals.mSlowFactor = binaryReader.ReadSingle();
				}
				else
				{
					this.mVals.mSlowFactor = 4f;
				}
				if (this.mVersion >= 14)
				{
					this.mVals.mMaxClumpSize = (int)binaryReader.ReadUInt32();
				}
				else
				{
					this.mVals.mMaxClumpSize = 10;
				}
				int num = (int)binaryReader.ReadUInt32();
				for (int j = 0; j < 14; j++)
				{
					this.mVals.mPowerUpFreq[j] = 0;
					this.mVals.mMaxNumPowerUps[j] = 100000000;
				}
				int num2 = 0;
				while (num2 < num && num2 < 14)
				{
					if (Common.IsDeprecatedPowerUp((PowerType)num2))
					{
						this.mVals.mMaxNumPowerUps[num2] = 0;
						binaryReader.ReadInt32();
						if (this.mVersion >= 12)
						{
							binaryReader.ReadInt32();
						}
					}
					else
					{
						this.mVals.mPowerUpFreq[num2] = (int)binaryReader.ReadUInt32();
						if (this.mVersion >= 12)
						{
							this.mVals.mMaxNumPowerUps[num2] = (int)binaryReader.ReadUInt32();
						}
					}
					num2++;
				}
				if (this.mVersion >= 12)
				{
					this.mVals.mPowerUpChance = (int)binaryReader.ReadUInt32();
				}
				else
				{
					this.mVals.mPowerUpChance = 0;
				}
				this.mDrawCurve = binaryReader.ReadBoolean();
				this.mVals.mDrawTunnels = binaryReader.ReadBoolean();
				this.mVals.mDestroyAll = binaryReader.ReadBoolean();
				if (this.mVersion > 8)
				{
					this.mVals.mDrawPit = binaryReader.ReadBoolean();
				}
				if (this.mVersion > 9)
				{
					this.mVals.mDieAtEnd = binaryReader.ReadBoolean();
				}
			}
			bool flag = false;
			bool flag2 = true;
			if (this.mVersion >= 3)
			{
				flag = binaryReader.ReadBoolean();
				flag2 = binaryReader.ReadBoolean();
			}
			if (!flag)
			{
				this.mEditType = (int)binaryReader.ReadUInt32();
				int num3 = (int)binaryReader.ReadUInt32();
				if (num3 > 1000000)
				{
					Console.WriteLine("File is corrupt");
					return false;
				}
				binaryReader.ReadBytes(num3);
			}
			else
			{
				this.mEditType = 0;
			}
			int num4 = (int)binaryReader.ReadUInt32();
			if (num4 <= 0)
			{
				return true;
			}
			if (this.mVersion < 2)
			{
				for (int k = 0; k < num4; k++)
				{
					PathPoint pathPoint = new PathPoint();
					pathPoint.x = binaryReader.ReadSingle();
					pathPoint.y = binaryReader.ReadSingle();
					pathPoint.mInTunnel = binaryReader.ReadBoolean();
					pathPoint.mPriority = binaryReader.ReadByte();
					this.mPointList.Add(pathPoint);
				}
			}
			else if (this.mVersion < 4)
			{
				PathPoint pathPoint2 = new PathPoint();
				pathPoint2.x = binaryReader.ReadSingle();
				pathPoint2.y = binaryReader.ReadSingle();
				if (flag2)
				{
					pathPoint2.mInTunnel = binaryReader.ReadBoolean();
					pathPoint2.mPriority = binaryReader.ReadByte();
				}
				num4--;
				float x = pathPoint2.x;
				float y = pathPoint2.y;
				this.mPointList.Add(pathPoint2);
				for (int l = 0; l < num4; l++)
				{
					PathPoint pathPoint3 = new PathPoint();
					sbyte b = binaryReader.ReadSByte();
					sbyte b2 = binaryReader.ReadSByte();
					pathPoint3.x = x + (float)b * CurveData.INV_SUBPIXEL_MULT;
					pathPoint3.y = y + (float)b2 * CurveData.INV_SUBPIXEL_MULT;
					if (flag2)
					{
						pathPoint3.mInTunnel = binaryReader.ReadBoolean();
						pathPoint3.mPriority = binaryReader.ReadByte();
					}
					x = pathPoint3.x;
					y = pathPoint3.y;
					this.mPointList.Add(pathPoint3);
				}
			}
			else
			{
				float num5 = 0f;
				float num6 = 0f;
				for (int m = 0; m < num4; m++)
				{
					PathPoint pathPoint4 = new PathPoint();
					this.mPointList.Add(pathPoint4);
					byte b3 = binaryReader.ReadByte();
					pathPoint4.mInTunnel = (b3 & 1) != 0;
					bool flag3 = (b3 & 2) != 0;
					if (flag2 || this.mVersion >= 15)
					{
						pathPoint4.mPriority = binaryReader.ReadByte();
					}
					if (flag3)
					{
						pathPoint4.x = binaryReader.ReadSingle();
						pathPoint4.y = binaryReader.ReadSingle();
					}
					else
					{
						sbyte b4 = binaryReader.ReadSByte();
						sbyte b5 = binaryReader.ReadSByte();
						pathPoint4.x = num5 + (float)b4 * CurveData.INV_SUBPIXEL_MULT;
						pathPoint4.y = num6 + (float)b5 * CurveData.INV_SUBPIXEL_MULT;
					}
					num5 = pathPoint4.x;
					num6 = pathPoint4.y;
				}
			}
			return true;
		}

		public void Copy(CurveData dest)
		{
			dest.mDrawCurve = this.mDrawCurve;
			dest.mVals = this.mVals;
		}

		public void Clear()
		{
			this.mPointList.Clear();
			this.mEditType = 0;
			this.mLinear = false;
			this.mVals.mDieAtEnd = true;
			this.mVals.mStartDistance = 50;
			this.mVals.mNumBalls = 0;
			this.mVals.mBallRepeat = 50;
			this.mVals.mMaxSingle = 10;
			this.mVals.mNumColors = 4;
			this.mVals.mSlowDistance = 200;
			this.mVals.mScoreTarget = 1000;
			this.mVals.mSkullRotation = -1;
			this.mVals.mZumaBack = 300;
			this.mVals.mZumaSlow = 1100;
			this.mVals.mSlowFactor = 4f;
			this.mVals.mMaxClumpSize = 10;
			this.mVals.mSpeed = 0.5f;
			this.mVals.mAccelerationRate = 0f;
			this.mVals.mMaxSpeed = 100f;
			for (int i = 0; i < 14; i++)
			{
				this.mVals.mPowerUpFreq[i] = 0;
				this.mVals.mMaxNumPowerUps[i] = 100000000;
			}
			this.mVals.mPowerUpChance = 1200;
			this.mVals.mDrawPit = true;
			this.mDrawCurve = true;
			this.mVals.mDrawTunnels = true;
			this.mVals.mDestroyAll = true;
		}

		public static int gVersion = 15;

		public static float SUBPIXEL_MULT = 100f;

		public static float INV_SUBPIXEL_MULT = 1f / CurveData.SUBPIXEL_MULT;

		public List<PathPoint> mPointList = new List<PathPoint>();

		public int mEditType;

		public int mVersion = 268435455;

		public string mErrorString;

		public BasicCurveVals mVals = new BasicCurveVals();

		public bool mDrawCurve;

		public bool mLinear;
	}
}
