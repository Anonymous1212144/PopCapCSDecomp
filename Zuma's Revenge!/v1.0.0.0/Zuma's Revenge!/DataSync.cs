using System;
using System.Collections.Generic;
using JeffLib;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	public class DataSync : DataSyncBase
	{
		public DataSync(Buffer buffer, bool isRead)
		{
			this.ResetPointerTable();
			this.m_buffer = buffer;
			this.m_isRead = isRead;
		}

		public Buffer GetBuffer()
		{
			return this.m_buffer;
		}

		public bool isRead()
		{
			return this.m_isRead;
		}

		public bool isWrite()
		{
			return !this.isRead();
		}

		public void SyncBoolean(ref bool theBool)
		{
			if (this.m_isRead)
			{
				theBool = this.m_buffer.ReadBoolean();
				return;
			}
			this.m_buffer.WriteBoolean(theBool);
		}

		public void SyncShort(ref short theInt)
		{
			if (this.m_isRead)
			{
				theInt = this.m_buffer.ReadShort();
				return;
			}
			this.m_buffer.WriteShort(theInt);
		}

		public override void SyncLong(ref int theInt)
		{
			if (this.m_isRead)
			{
				theInt = (int)this.m_buffer.ReadLong();
				return;
			}
			this.m_buffer.WriteLong((long)theInt);
		}

		public void SyncLong(ref uint theInt)
		{
			if (this.m_isRead)
			{
				theInt = (uint)this.m_buffer.ReadLong();
				return;
			}
			this.m_buffer.WriteLong((long)((ulong)theInt));
		}

		public void SyncLong(ref ushort theInt)
		{
			if (this.m_isRead)
			{
				theInt = (ushort)this.m_buffer.ReadLong();
				return;
			}
			this.m_buffer.WriteLong((long)((ulong)theInt));
		}

		public void SyncLong(ref long theLong)
		{
			if (this.m_isRead)
			{
				theLong = this.m_buffer.ReadLong();
				return;
			}
			this.m_buffer.WriteLong(theLong);
		}

		public override void SyncFloat(ref float theFloat)
		{
			if (this.m_isRead)
			{
				theFloat = this.m_buffer.ReadFloat();
				return;
			}
			this.m_buffer.WriteFloat(theFloat);
		}

		public override void SyncListInt(List<int> theList)
		{
			if (this.m_isRead)
			{
				theList.Clear();
				long num = this.m_buffer.ReadLong();
				int num2 = 0;
				while ((long)num2 < num)
				{
					theList.Add((int)this.m_buffer.ReadLong());
					num2++;
				}
				return;
			}
			this.m_buffer.WriteLong((long)theList.Count);
			foreach (int num3 in theList)
			{
				this.m_buffer.WriteLong((long)num3);
			}
		}

		public override void SyncListFloat(List<float> theList)
		{
			if (this.m_isRead)
			{
				theList.Clear();
				long num = this.m_buffer.ReadLong();
				int num2 = 0;
				while ((long)num2 < num)
				{
					theList.Add(this.m_buffer.ReadFloat());
					num2++;
				}
				return;
			}
			this.m_buffer.WriteLong((long)theList.Count);
			foreach (float num3 in theList)
			{
				float theFloat = num3;
				this.m_buffer.WriteFloat(theFloat);
			}
		}

		private void ResetPointerTable()
		{
			this.mCurPointerIndex = 2;
			this.mIntToPointerMap_CurveMgr.Clear();
			this.mIntToPointerMap_Ball.Clear();
			this.mIntToPointerMap_Bullet.Clear();
			this.mPointerToIntMap_CurveMgr.Clear();
			this.mPointerToIntMap_Ball.Clear();
			this.mPointerToIntMap_Bullet.Clear();
			this.mPointerSyncList_ReversePowerEffect.Clear();
			this.mPointerSyncList_Ball.Clear();
			this.mPointerSyncList_Bullet.Clear();
			this.mIntToPointerMap_CurveMgr.Add(0, null);
			this.mIntToPointerMap_Ball.Add(0, null);
			this.mIntToPointerMap_Bullet.Add(0, null);
		}

		public bool RegisterPointer(CurveMgr thePtr)
		{
			if (!this.mPointerToIntMap_CurveMgr.ContainsKey(thePtr))
			{
				int num = this.mCurPointerIndex++;
				this.mPointerToIntMap_CurveMgr.Add(thePtr, num);
				this.mIntToPointerMap_CurveMgr.Add(num, thePtr);
				return true;
			}
			return false;
		}

		public bool RegisterPointer(Ball thePtr)
		{
			if (!this.mPointerToIntMap_Ball.ContainsKey(thePtr))
			{
				int num = this.mCurPointerIndex++;
				this.mPointerToIntMap_Ball.Add(thePtr, num);
				this.mIntToPointerMap_Ball.Add(num, thePtr);
				return true;
			}
			return false;
		}

		public bool RegisterPointer(Bullet thePtr)
		{
			if (!this.mPointerToIntMap_Bullet.ContainsKey(thePtr))
			{
				int num = this.mCurPointerIndex++;
				this.mPointerToIntMap_Bullet.Add(thePtr, num);
				this.mIntToPointerMap_Bullet.Add(num, thePtr);
				return true;
			}
			return false;
		}

		public void SyncPointer(ReversePowerEffect thePtr)
		{
			this.mPointerSyncList_ReversePowerEffect.Add(thePtr);
		}

		public void SyncPointer(Ball thePtr)
		{
			this.mPointerSyncList_Ball.Add(thePtr);
		}

		public void SyncPointer(Bullet thePtr)
		{
			this.mPointerSyncList_Bullet.Add(thePtr);
		}

		public void SyncPointers()
		{
			if (this.m_isRead)
			{
				foreach (ReversePowerEffect reversePowerEffect in this.mPointerSyncList_ReversePowerEffect)
				{
					int num = (int)this.m_buffer.ReadLong();
					reversePowerEffect.mCurve = this.mIntToPointerMap_CurveMgr[num];
				}
				foreach (Ball ball in this.mPointerSyncList_Ball)
				{
					int num2 = (int)this.m_buffer.ReadLong();
					ball.mBullet = this.mIntToPointerMap_Bullet[num2];
				}
				using (List<Bullet>.Enumerator enumerator3 = this.mPointerSyncList_Bullet.GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						Bullet bullet = enumerator3.Current;
						int num3 = (int)this.m_buffer.ReadLong();
						bullet.mHitBall = this.mIntToPointerMap_Ball[num3];
					}
					goto IL_258;
				}
			}
			foreach (ReversePowerEffect reversePowerEffect2 in this.mPointerSyncList_ReversePowerEffect)
			{
				int num4 = 0;
				if (reversePowerEffect2.mCurve != null && this.mPointerToIntMap_CurveMgr.ContainsKey(reversePowerEffect2.mCurve))
				{
					num4 = this.mPointerToIntMap_CurveMgr[reversePowerEffect2.mCurve];
				}
				this.m_buffer.WriteLong((long)num4);
			}
			foreach (Ball ball2 in this.mPointerSyncList_Ball)
			{
				int num5 = 0;
				if (ball2.mBullet != null && this.mPointerToIntMap_Bullet.ContainsKey(ball2.mBullet))
				{
					num5 = this.mPointerToIntMap_Bullet[ball2.mBullet];
				}
				this.m_buffer.WriteLong((long)num5);
			}
			foreach (Bullet bullet2 in this.mPointerSyncList_Bullet)
			{
				int num6 = 0;
				if (bullet2.mHitBall != null && this.mPointerToIntMap_Ball.ContainsKey(bullet2.mHitBall))
				{
					num6 = this.mPointerToIntMap_Ball[bullet2.mHitBall];
				}
				this.m_buffer.WriteLong((long)num6);
			}
			IL_258:
			this.ResetPointerTable();
		}

		private Buffer m_buffer;

		private bool m_isRead = true;

		private int mCurPointerIndex;

		private Dictionary<CurveMgr, int> mPointerToIntMap_CurveMgr = new Dictionary<CurveMgr, int>();

		private Dictionary<int, CurveMgr> mIntToPointerMap_CurveMgr = new Dictionary<int, CurveMgr>();

		private List<ReversePowerEffect> mPointerSyncList_ReversePowerEffect = new List<ReversePowerEffect>();

		private Dictionary<Ball, int> mPointerToIntMap_Ball = new Dictionary<Ball, int>();

		private Dictionary<int, Ball> mIntToPointerMap_Ball = new Dictionary<int, Ball>();

		private List<Bullet> mPointerSyncList_Bullet = new List<Bullet>();

		private Dictionary<Bullet, int> mPointerToIntMap_Bullet = new Dictionary<Bullet, int>();

		private Dictionary<int, Bullet> mIntToPointerMap_Bullet = new Dictionary<int, Bullet>();

		private List<Ball> mPointerSyncList_Ball = new List<Ball>();
	}
}
