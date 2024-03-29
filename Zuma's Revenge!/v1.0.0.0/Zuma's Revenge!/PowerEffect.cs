﻿using System;
using System.Collections.Generic;
using System.Linq;
using JeffLib;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	public class PowerEffect
	{
		protected EffectItem AddItem(Image img, Color c, int cel)
		{
			EffectItem effectItem = new EffectItem();
			this.mItems.Add(effectItem);
			effectItem.mImage = img;
			effectItem.mCel = cel;
			effectItem.mColor = new Color(c);
			return effectItem;
		}

		protected EffectItem AddItem(Image img, Color c)
		{
			return this.AddItem(img, c, 0);
		}

		public PowerEffect(float x, float y)
		{
			this.mX = x;
			this.mY = y;
			this.mUpdateCount = 0;
			this.mDone = false;
			this.mDrawReverse = false;
			this.mType = -1;
			this.mColorType = -1;
		}

		public PowerEffect()
			: this(0f, 0f)
		{
		}

		public virtual void AddDefaultEffectType(int eff_type, int color_type, float init_rotation)
		{
			Color c = default(Color);
			Color c2 = default(Color);
			switch (color_type)
			{
			case 0:
				c = new Color(Common._M(150), Common._M1(150), Common._M2(255));
				c2 = new Color(Common._M3(75), Common._M4(75), Common._M5(255));
				break;
			case 1:
				c = new Color(Common._M(255), Common._M1(255), Common._M2(50));
				c2 = new Color(Common._M3(255), Common._M4(255), Common._M5(0));
				break;
			case 2:
				c = new Color(Common._M(250), Common._M1(140), Common._M2(0));
				c2 = new Color(Common._M3(250), Common._M4(50), Common._M5(1));
				break;
			case 3:
				c = new Color(Common._M(200), Common._M1(200), Common._M2(0));
				c2 = new Color(Common._M3(0), Common._M4(185), Common._M5(118));
				break;
			case 4:
				c = new Color(Common._M(255), Common._M1(100), Common._M2(255));
				c2 = new Color(Common._M3(255), Common._M4(50), Common._M5(255));
				break;
			case 5:
				c = new Color(Common._M(255), Common._M1(255), Common._M2(255));
				c2 = new Color(Common._M3(200), Common._M4(200), Common._M5(200));
				break;
			}
			this.mType = eff_type;
			this.mColorType = color_type;
			Image imageByID = Res.GetImageByID(ResID.IMAGE_POWERUPS_PULSES);
			Image imageByID2 = Res.GetImageByID(ResID.IMAGE_BALL_GLOW);
			Image imageByID3 = Res.GetImageByID(ResID.IMAGE_BALL_RING);
			Image imageByID4 = Res.GetImageByID(ResID.IMAGE_BLOOM_STOP_OUTLINE);
			float num = 2f;
			if (eff_type == 0)
			{
				int num2 = 83;
				float num3 = 4f;
				EffectItem effectItem = this.AddItem(imageByID, c2, 3);
				effectItem.mScale.Add(new Component(1f * num, 1.63f * num, 83 - num2, 115 - num2));
				effectItem.mOpacity.Add(new Component(255f, 0f, 100 - num2, 130 - num2));
				Image imageByID5 = Res.GetImageByID(ResID.IMAGE_BLOOM_BLAST_BLUE + color_type);
				effectItem = this.AddItem(imageByID5, Color.White, 0);
				effectItem.mScale.Add(new Component(0.2f * num3, 1f * num3, 83 - num2, 105 - num2));
				effectItem.mAngle.Add(new Component(init_rotation, init_rotation + 3.14159f, 83 - num2, 105 - num2));
				effectItem.mOpacity.Add(new Component(0f, 255f, 83 - num2, 105 - num2));
				effectItem.mOpacity.Add(new Component(255f, 0f, 106 - num2, 120 - num2));
				effectItem = this.AddItem(imageByID5, Color.White, 0);
				effectItem.mScale.Add(new Component(0.2f, 1f, 83 - num2, 131 - num2));
				effectItem.mAngle.Add(new Component(init_rotation, init_rotation - 3.14159f, 83 - num2, 105 - num2));
				effectItem.mOpacity.Add(new Component(0f, 128f, 83 - num2, 105 - num2));
				effectItem.mOpacity.Add(new Component(128f, 0f, 106 - num2, 145 - num2));
				return;
			}
			if (eff_type == 1)
			{
				float num4 = 4f;
				int num5 = 35;
				EffectItem effectItem2 = this.AddItem(imageByID2, c, 0);
				effectItem2.mOpacity.Add(new Component(128f, 255f, 35 - num5, 50 - num5));
				effectItem2.mOpacity.Add(new Component(255f, 0f, 51 - num5, 95 - num5));
				effectItem2 = this.AddItem(imageByID, c, 0);
				effectItem2.mScale.Add(new Component(0.1f * num, 2f * num, 50 - num5, 65 - num5));
				effectItem2.mAngle.Add(new Component(init_rotation, init_rotation + 3.14159f, 55 - num5, 75 - num5));
				effectItem2.mOpacity.Add(new Component(25f, 255f, 35 - num5, 50 - num5));
				effectItem2.mOpacity.Add(new Component(255f, 0f, 36 - num5, 95 - num5));
				effectItem2 = this.AddItem(Res.GetImageByID(ResID.IMAGE_BLOOM_ACCURACY_BLUE + color_type), c, 0);
				effectItem2.mScale.Add(new Component(0.1f * num4, 1.1f * num4, 50 - num5, 95 - num5));
				effectItem2.mScale.Add(new Component(1.1f * num4, 1f * num4, 96 - num5, 101 - num5));
				effectItem2.mAngle.Add(new Component(init_rotation, init_rotation + 1.570795f, 55 - num5, 95 - num5));
				effectItem2.mOpacity.Add(new Component(0f, 0f, 35 - num5, 49 - num5));
				effectItem2.mOpacity.Add(new Component(25f, 255f, 50 - num5, 75 - num5));
				effectItem2.mOpacity.Add(new Component(255f, 0f, 115 - num5, 135 - num5));
				effectItem2 = this.AddItem(imageByID3, c, 0);
				effectItem2.mOpacity.Add(new Component(0f, 0f, 35 - num5, 79 - num5));
				effectItem2.mOpacity.Add(new Component(128f, 255f, 80 - num5, 90 - num5));
				effectItem2.mOpacity.Add(new Component(255f, 0f, 91 - num5, 135 - num5));
				effectItem2.mScale.Add(new Component(1f, 10f, 80 - num5, 135 - num5));
				return;
			}
			if (eff_type == 2)
			{
				float num6 = 4f;
				float num7 = (float)Common._M(8);
				int num8 = (int)(70f / num7);
				EffectItem effectItem3 = this.AddItem(imageByID2, c, 0);
				effectItem3.mOpacity.Add(new Component(128f, 255f, (int)(70f / num7 - (float)num8), (int)(210f / num7 - (float)num8)));
				effectItem3.mOpacity.Add(new Component(255f, 0f, (int)(211f / num7 - (float)num8), (int)(310f / num7 - (float)num8)));
				effectItem3 = this.AddItem(imageByID, c2, 4);
				effectItem3.mScale.Add(new Component(1f * num, 2f * num, (int)(109f / num7 - (float)num8), (int)(385f / num7 - (float)num8)));
				effectItem3.mOpacity.Add(new Component(0f, 0f, (int)(70f / num7 - (float)num8), (int)(108f / num7 - (float)num8)));
				effectItem3.mOpacity.Add(new Component(255f, 255f, (int)(109f / num7 - (float)num8), (int)(360f / num7 - (float)num8)));
				effectItem3.mOpacity.Add(new Component(255f, 0f, (int)(361f / num7 - (float)num8), (int)(485f / num7 - (float)num8)));
				Image imageByID6 = Res.GetImageByID(ResID.IMAGE_BLOOM_BACKWARDS_BLUE + color_type);
				effectItem3 = this.AddItem(imageByID6, Color.White, 0);
				effectItem3.mOpacity.Add(new Component(0f, 0f, (int)(70f / num7 - (float)num8), (int)(160f / num7 - (float)num8)));
				effectItem3.mOpacity.Add(new Component(0f, 128f, (int)(161f / num7 - (float)num8), (int)(360f / num7 - (float)num8)));
				effectItem3.mOpacity.Add(new Component(128f, 153f, (int)(361f / num7 - (float)num8), (int)(485f / num7 - (float)num8)));
				effectItem3.mOpacity.Add(new Component(153f, 0f, (int)(486f / num7 - (float)num8), (int)(560f / num7 - (float)num8)));
				effectItem3.mScale.Add(new Component(0.2f * num6, 1f * num6, (int)(160f / num7 - (float)num8), (int)(360f / num7 - (float)num8)));
				effectItem3 = this.AddItem(imageByID6, Color.White, 0);
				effectItem3.mOpacity.Add(new Component(0f, 0f, (int)(70f / num7 - (float)num8), (int)(335f / num7 - (float)num8)));
				effectItem3.mOpacity.Add(new Component(0f, 255f, (int)(336f / num7 - (float)num8), (int)(585f / num7 - (float)num8)));
				effectItem3.mScale.Add(new Component(0.2f, 1f, (int)(335f / num7 - (float)num8), (int)(535f / num7 - (float)num8)));
				return;
			}
			if (eff_type == 3)
			{
				float num9 = init_rotation - 1.570795f;
				float target;
				if (num9 > 3.14159f)
				{
					target = 6.28318f;
				}
				else
				{
					target = 0f;
				}
				EffectItem effectItem4 = this.AddItem(imageByID, Color.White, 2);
				effectItem4.mOpacity.Add(new Component(255f, 255f, 0, 15));
				effectItem4.mOpacity.Add(new Component(255f, 0f, 16, 21));
				effectItem4.mScale.Add(new Component(1f * num, 1f * num, 0, 9));
				effectItem4.mScale.Add(new Component(1f * num, 2f * num, 10, 21));
				effectItem4.mAngle.Add(new Component(num9, target, 0, 20));
				float num10 = 2f;
				effectItem4 = this.AddItem(Res.GetImageByID(ResID.IMAGE_BLOOM_STOP_BLUE + color_type), Color.White, 0);
				effectItem4.mOpacity.Add(new Component(0f, 0f, 0, 9));
				effectItem4.mOpacity.Add(new Component(128f, 255f, 10, 20));
				effectItem4.mOpacity.Add(new Component(255f, 0f, 40, 50));
				effectItem4.mScale.Add(new Component(0.5f * num10, 1.1f * num10, 10, 22));
				effectItem4.mScale.Add(new Component(1.1f * num10, 1f * num10, 23, 30));
				effectItem4.mScale.Add(new Component(1f * num10, 0.5f * num10, 40, 50));
				effectItem4.mYOffset.Add(new Component(0f, Common._M(-10f), 10, 20));
				effectItem4.mAngle.Add(new Component(num9, target, 0, 20));
				effectItem4 = this.AddItem(Res.GetImageByID(ResID.IMAGE_BLOOM_STOP_BLUE + color_type), Color.White, 0);
				effectItem4.mOpacity.Add(new Component(0f, 0f, 0, 20));
				effectItem4.mOpacity.Add(new Component(0f, 255f, 21, 26));
				effectItem4.mOpacity.Add(new Component(255f, 0f, 27, 37));
				effectItem4.mScale.Add(new Component(1f * num10, 1.1f * num10, 20, 22));
				effectItem4.mScale.Add(new Component(1.1f * num10, 1f * num10, 23, 27));
				effectItem4.mYOffset.Add(new Component(-10f, -10f, 20, 20));
				effectItem4.mAngle.Add(new Component(num9, target, 0, 20));
				effectItem4 = this.AddItem(imageByID4, Color.White, 0);
				effectItem4.mOpacity.Add(new Component(0f, 0f, 0, 24));
				effectItem4.mOpacity.Add(new Component(255f, 0f, 25, 50));
				effectItem4.mScale.Add(new Component(1f * num10, 3f * num10, 25, 50));
				effectItem4.mAngle.Add(new Component(num9, target, 0, 20));
				return;
			}
			if (eff_type == 5)
			{
				float num11 = init_rotation - 1.570795f;
				float num12 = 4f;
				EffectItem effectItem5 = this.AddItem(imageByID2, c, 0);
				effectItem5.mOpacity.Add(new Component(128f, 255f, 0, 15));
				effectItem5.mOpacity.Add(new Component(255f, 0f, 16, 35));
				effectItem5 = this.AddItem(Res.GetImageByID(ResID.IMAGE_POWERUPS_BLUE + color_type), Color.White, 6);
				effectItem5.mOpacity.Add(new Component(25f, 255f, 0, 15));
				effectItem5.mOpacity.Add(new Component(255f, 0f, 16, 35));
				effectItem5.mAngle.Add(new Component(num11, num11 + 6.28318f, 15, 35));
				effectItem5.mScale.Add(new Component(1f, 2f, 15, 20));
				effectItem5 = this.AddItem(Res.GetImageByID(ResID.IMAGE_BLOOM_BLAST_BLUE + color_type), Color.White, 0);
				effectItem5.mOpacity.Add(new Component(0f, 255f, 0, 59));
				effectItem5.mOpacity.Add(new Component(255f, 0f, 60, 80));
				effectItem5.mAngle.Add(new Component(num11, num11, 0, 14));
				effectItem5.mAngle.Add(new Component(num11, num11 + 6.28318f, 15, 35));
				effectItem5.mScale.Add(new Component(0.4f * num12, 1f * num12, 15, 35));
				effectItem5.mScale.Add(new Component(1f * num12, 0.1f * num12, 60, 80));
				effectItem5 = this.AddItem(Res.GetImageByID(ResID.IMAGE_BLOOM_BLAST_BLUE + color_type), Color.White, 0);
				effectItem5.mAngle.Add(new Component(num11, num11, 0, 75));
				effectItem5.mOpacity.Add(new Component(0f, 0f, 0, 34));
				effectItem5.mOpacity.Add(new Component(25f, 255f, 35, 60));
				effectItem5.mOpacity.Add(new Component(255f, 0f, 61, 75));
				effectItem5.mScale.Add(new Component(2f * num12, 1f * num12, 30, 60));
				effectItem5.mScale.Add(new Component(1f * num12, 0f * num12, 61, 71));
			}
		}

		public virtual void AddDefaultEffectType(int eff_type, int color_type)
		{
			this.AddDefaultEffectType(eff_type, color_type, 0f);
		}

		public virtual void Update()
		{
			if (this.mDone)
			{
				return;
			}
			this.mUpdateCount++;
			bool flag = true;
			for (int i = 0; i < Enumerable.Count<EffectItem>(this.mItems); i++)
			{
				EffectItem effectItem = this.mItems[i];
				bool flag2 = Component.UpdateComponentVec(effectItem.mScale, this.mUpdateCount);
				bool flag3 = Component.UpdateComponentVec(effectItem.mAngle, this.mUpdateCount);
				bool flag4 = Component.UpdateComponentVec(effectItem.mOpacity, this.mUpdateCount);
				bool flag5 = Component.UpdateComponentVec(effectItem.mXOffset, this.mUpdateCount);
				bool flag6 = Component.UpdateComponentVec(effectItem.mYOffset, this.mUpdateCount);
				flag = flag && flag2 && flag3 && flag4 && flag5 && flag6;
			}
			this.mDone = flag;
		}

		public virtual void Draw(Graphics g)
		{
			if (this.mDone)
			{
				return;
			}
			g.PushState();
			g.SetColorizeImages(true);
			g.SetDrawMode(1);
			int num = (this.mDrawReverse ? (Enumerable.Count<EffectItem>(this.mItems) - 1) : 0);
			int num2 = (this.mDrawReverse ? 0 : Enumerable.Count<EffectItem>(this.mItems));
			int num3 = num;
			while (this.mDrawReverse ? (num3 >= num2) : (num3 < num2))
			{
				EffectItem effectItem = this.mItems[num3];
				Color mColor = effectItem.mColor;
				mColor.mAlpha = (int)Component.GetComponentValue(effectItem.mOpacity, 255f, this.mUpdateCount);
				if (mColor.mAlpha != 0)
				{
					float componentValue = Component.GetComponentValue(effectItem.mAngle, 0f, this.mUpdateCount);
					float componentValue2 = Component.GetComponentValue(effectItem.mScale, 1f, this.mUpdateCount);
					float tx = Common._S(Component.GetComponentValue(effectItem.mXOffset, 0f, this.mUpdateCount));
					float ty = Common._S(Component.GetComponentValue(effectItem.mYOffset, 0f, this.mUpdateCount));
					g.SetColor(mColor);
					this.mGlobalTranform.Reset();
					this.mGlobalTranform.RotateRad(componentValue);
					this.mGlobalTranform.Translate(tx, ty);
					this.mGlobalTranform.Scale(componentValue2, componentValue2);
					Rect celRect = effectItem.mImage.GetCelRect(effectItem.mCel);
					if (g.Is3D())
					{
						g.DrawImageTransformF(effectItem.mImage, this.mGlobalTranform, celRect, Common._S(this.mX), Common._S(this.mY));
					}
					else
					{
						g.DrawImageTransform(effectItem.mImage, this.mGlobalTranform, celRect, Common._S(this.mX), Common._S(this.mY));
					}
				}
				num3 += (this.mDrawReverse ? (-1) : 1);
			}
			g.PopState();
		}

		public virtual bool IsDone()
		{
			return this.mDone;
		}

		public int GetUpdateCount()
		{
			return this.mUpdateCount;
		}

		public int GetType()
		{
			return this.mType;
		}

		public virtual void SyncState(DataSync sync)
		{
			sync.SyncLong(ref this.mType);
			sync.SyncLong(ref this.mColorType);
			if (sync.isRead())
			{
				this.mItems.Clear();
				this.AddDefaultEffectType(this.mType, this.mColorType);
			}
			sync.SyncBoolean(ref this.mDrawReverse);
			sync.SyncFloat(ref this.mX);
			sync.SyncFloat(ref this.mY);
			sync.SyncLong(ref this.mUpdateCount);
			sync.SyncBoolean(ref this.mDone);
			for (int i = 0; i < this.mItems.Count; i++)
			{
				this.mItems[i].SyncState(sync);
			}
		}

		public bool mDrawReverse;

		protected List<EffectItem> mItems = new List<EffectItem>();

		protected float mX;

		protected float mY;

		protected int mUpdateCount;

		protected bool mDone;

		protected int mType;

		protected int mColorType;

		protected Transform mGlobalTranform = new Transform();

		public enum Type
		{
			Bomb,
			Accuracy,
			Reverse,
			Stop,
			Cannon,
			Laser
		}
	}
}
