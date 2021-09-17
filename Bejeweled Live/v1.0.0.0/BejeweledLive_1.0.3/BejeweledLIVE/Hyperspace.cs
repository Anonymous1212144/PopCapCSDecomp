using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Sexy;

namespace BejeweledLIVE
{
	public class Hyperspace : Widget
	{
		public bool IsRunning { get; private set; }

		public new void Reset()
		{
			base.Reset();
			this.mMouseVisible = false;
			this.mIsDone = false;
			this.mDoneDelay = 0;
			this.mFlashState = 0;
			this.mFlashDelay = 120;
			this.mFlashPercent = 0f;
			this.mShowBkg = false;
			this.mRingRotAcc = 0f;
			this.mRingRotAcc2 = 0f;
			this.mTransPercent = 0f;
			this.mRingXAcc = 0f;
			this.mRingXAcc2 = 0f;
			this.mRingYAcc = 0f;
			this.mRingYAcc2 = 0f;
			this.mScoreFloaterX = 0f;
			this.mScoreFloaterY = 0f;
			this.mCameraX = 0f;
			this.mCameraY = 0f;
			this.mPortalDelay = 200;
			this.mEndSoundDelay = 100;
			this.mPortalPercent = 0f;
			for (int i = 0; i < 18; i++)
			{
				HyperRing hyperRing = Hyperspace.mHyperRings[i];
				hyperRing.mFromRot = 0f;
				hyperRing.mToRot = 0f;
				hyperRing.mFromX = 0f;
				hyperRing.mFromY = 0f;
				hyperRing.mToX = 0f;
				hyperRing.mToY = 0f;
				hyperRing.mCurX = 0f;
				hyperRing.mCurY = 0f;
				hyperRing.mCurScreenX = 0f;
				hyperRing.mCurScreenY = 0f;
				for (int j = 0; j < 20; j++)
				{
					HyperPoint hyperPoint = hyperRing.mHyperPoints[j];
					hyperPoint.mU = (float)j / 20f;
					hyperPoint.mV = (float)i * 0.02f - 0.359999985f;
				}
			}
			this.mEndTextPos = 800;
			this.mIs3d = this.mApp.Is3DAccelerated();
			this.mYOffset = 4f;
			this.mYOffsetVel = 0.3f;
			this.mYOffset2 = 0f;
			this.mYOffset2Vel = 0f;
			this.mEffectUpdate = 0;
			this.mWarpLineAcc = 0f;
			this.mWarpLineAdd = 0f;
			this.mWarpLineSpeed = 0.12f;
			this.mWarpLineVector.Clear();
			this.mShakeFactor = 0f;
			this.mStretchFactor = 1f;
			this.mStretchVel = 0f;
			this.mFirstShowBkg = true;
			if (this.mIs3d)
			{
				return;
			}
			this.mFlashState = 2;
		}

		public void Start()
		{
			this.Reset();
			this.mApp.LockOrientation(true);
			this.IsRunning = true;
		}

		public void Stop()
		{
			this.mApp.LockOrientation(false);
			this.mIsDone = false;
			this.IsRunning = false;
		}

		public Hyperspace(GameApp theApp)
		{
			this.mApp = theApp;
			for (int i = 0; i < 18; i++)
			{
				Hyperspace.mHyperRings[i] = new HyperRing();
				HyperRing hyperRing = Hyperspace.mHyperRings[i];
				hyperRing.mFromRot = 0f;
				hyperRing.mToRot = 0f;
				hyperRing.mFromX = 0f;
				hyperRing.mFromY = 0f;
				hyperRing.mToX = 0f;
				hyperRing.mToY = 0f;
				hyperRing.mCurX = 0f;
				hyperRing.mCurY = 0f;
				hyperRing.mCurScreenX = 0f;
				hyperRing.mCurScreenY = 0f;
				for (int j = 0; j < 20; j++)
				{
					hyperRing.mHyperPoints[j] = new HyperPoint();
					HyperPoint hyperPoint = hyperRing.mHyperPoints[j];
					hyperPoint.mU = (float)j / 20f;
					hyperPoint.mV = (float)i * 0.02f - 0.359999985f;
				}
			}
			this.Reset();
		}

		protected void Update3d()
		{
		}

		protected void DoUpdate()
		{
			if (this.mApp.mBoard.mPauseCount > 0)
			{
				return;
			}
			base.Update();
			if (this.mFlashDelay > 0)
			{
				if (--this.mFlashDelay == 0)
				{
					this.mFlashState = 1;
					this.mFlashPercent = 1f;
					this.mShowBkg = true;
				}
				if (this.mFlashDelay < 20)
				{
					this.mFlashPercent = (float)(20 - this.mFlashDelay) / 20f;
					this.MarkDirty();
				}
			}
			else if ((double)this.mFlashPercent > 0.0)
			{
				this.mFlashPercent -= 0.015f;
				this.MarkDirty();
			}
			if (this.mShowBkg)
			{
				if (this.mPortalDelay > 0)
				{
					this.mPortalDelay--;
				}
				else if ((double)this.mPortalPercent < 1.0)
				{
					float num = this.mPortalPercent / 200f + 0.001f;
					float num2 = (1.001f - this.mPortalPercent) * 5f;
					if (num2 < 1f)
					{
						num *= num2;
					}
					this.mPortalPercent += num;
				}
				if (this.mEndSoundDelay > 0 && --this.mEndSoundDelay == 0)
				{
					this.mApp.PlaySample(Resources.SOUND_WHIRLPOOL_END);
				}
				if (this.mDoneDelay > 0)
				{
					if (--this.mDoneDelay == 0)
					{
						this.mIsDone = true;
					}
				}
				else if ((double)this.mPortalPercent >= 1.0)
				{
					this.mPortalPercent = 1f;
					this.mDoneDelay = 2;
				}
				this.mRingRotAcc += 0.006f;
				this.mRingRotAcc2 += 0.0093f;
				this.mRingXAcc += 0.006f;
				this.mRingXAcc += 0.0017f;
				this.mRingYAcc += 0.01f;
				this.mRingYAcc += 0.003f;
				HyperRing hyperRing = Hyperspace.mHyperRings[17];
				hyperRing.mFromRot += (float)(Math.Sin((double)this.mRingRotAcc) * 0.0020000000949949026);
				hyperRing.mFromRot += (float)(Math.Sin((double)this.mRingRotAcc2) * 0.0010000000474974513);
				hyperRing.mToRot = hyperRing.mFromRot;
				hyperRing.mFromX += (float)(Math.Sin((double)this.mRingXAcc) * 4.0 + Math.Sin((double)this.mRingXAcc2) * 5.0);
				hyperRing.mToX = hyperRing.mFromX;
				hyperRing.mFromY += (float)(Math.Sin((double)this.mRingYAcc) * 4.0 + Math.Sin((double)this.mRingYAcc2) * 5.0);
				hyperRing.mToY = hyperRing.mFromY;
			}
			this.mTransPercent += 0.25f;
			if ((double)this.mTransPercent >= 1.0)
			{
				for (int i = 0; i < 17; i++)
				{
					HyperRing hyperRing2 = Hyperspace.mHyperRings[i];
					HyperRing hyperRing3 = Hyperspace.mHyperRings[i + 1];
					hyperRing2.mFromX = hyperRing2.mToX;
					hyperRing2.mToX = hyperRing3.mFromX;
					hyperRing2.mFromY = hyperRing2.mToY;
					hyperRing2.mToY = hyperRing3.mFromY;
					hyperRing2.mFromRot = hyperRing2.mToRot;
					hyperRing2.mToRot = hyperRing3.mFromRot;
				}
				this.mTransPercent -= 1f;
			}
			float num3 = Hyperspace.mHyperRings[0].mCurX - this.mCameraX;
			float num4 = Hyperspace.mHyperRings[0].mCurY - this.mCameraY;
			this.mCameraX += (float)((double)num3 * 0.09);
			this.mCameraY += (float)((double)num4 * 0.09);
			float num5 = Hyperspace.mHyperRings[1].mCurX - this.mScoreFloaterX;
			float num6 = Hyperspace.mHyperRings[1].mCurY - this.mScoreFloaterY;
			this.mScoreFloaterX += (float)((double)num5 * 0.08);
			this.mScoreFloaterY += (float)((double)num6 * 0.08);
			int num7 = (this.mApp.IsLandscape() ? 1024 : 768);
			int num8 = (this.mApp.IsLandscape() ? 768 : 1024);
			if ((double)this.mPortalPercent < 1.0)
			{
				for (int j = 0; j < Hyperspace.mHyperRings.Length; j++)
				{
					HyperRing hyperRing4 = Hyperspace.mHyperRings[j];
					float num9 = hyperRing4.mFromRot * (1f - this.mTransPercent) + hyperRing4.mToRot * this.mTransPercent;
					float num10 = (hyperRing4.mCurX = hyperRing4.mFromX * (1f - this.mTransPercent) + hyperRing4.mToX * this.mTransPercent);
					float num11 = (hyperRing4.mCurY = hyperRing4.mFromY * (1f - this.mTransPercent) + hyperRing4.mToY * this.mTransPercent);
					int num12 = (int)(num9 * 4096f / 3.14159274f * 2f);
					if (num12 < 0)
					{
						num12 = 4096 - num12;
					}
					float num13 = 1f - this.mPortalPercent;
					float num14 = 800f - (float)j * num13 * 5000f / 18f;
					float num15 = 1024f / (1024f - num14);
					int num16 = (int)(200f - (float)j * num13 * 150f / 18f);
					float num17 = 1f - this.mPortalPercent;
					hyperRing4.mCurScreenX = (num10 - this.mCameraX) * num17 * num15 + (float)(num7 / 2);
					hyperRing4.mCurScreenY = (num11 - this.mCameraY) * num17 * num15 + (float)(num8 / 2);
					hyperRing4.mCurScreenRadius = (float)num16 * num15;
					for (int k = 0; k < 20; k++)
					{
						HyperPoint hyperPoint = hyperRing4.mHyperPoints[k];
						int num18 = (4096 * k / 20 + num12) % 4096;
						hyperPoint.mX = (Board.COS_TAB[num18] * (float)num16 + (num10 - this.mCameraX) * num17) * num15 + (float)(num7 / 2);
						hyperPoint.mY = (Board.SIN_TAB[num18] * (float)num16 + (num11 - this.mCameraY) * num17) * num15 + (float)(num8 / 2);
						hyperPoint.mV += (this.mShowBkg ? 0.003f : 0.006f);
						if (hyperPoint.mV > 1f)
						{
							hyperPoint.mV -= 1f;
						}
					}
				}
			}
			if (this.mShowBkg)
			{
				this.MarkDirty();
				return;
			}
			this.MarkDirtyFull();
		}

		protected void Draw3dPortal(Graphics g)
		{
			if (this.mFlashState == 1)
			{
				this.mFlashState = 2;
			}
			int num;
			int num2;
			int num3;
			int num4;
			if (this.mApp.IsLandscape())
			{
				num = 0;
				num2 = Constants.mConstants.HyperSpace_WarpOffset;
				num3 = 1024;
				num4 = 768;
			}
			else
			{
				num = Constants.mConstants.HyperSpace_WarpOffset;
				num2 = 0;
				num3 = 768;
				num4 = 1024;
			}
			TRect theDestRect = default(TRect);
			if (this.mShowBkg)
			{
				g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
				int num5 = (int)(this.mPortalPercent * 300f);
				if (num5 > 255)
				{
					num5 = 255;
				}
				else if (num5 < 0)
				{
					num5 = 0;
				}
				int num6 = Math.Min((int)((double)num3 * ((double)(this.mPortalPercent * this.mPortalPercent * this.mPortalPercent) * Constants.mConstants.HyperSpace_PortalSize_Scale + Constants.mConstants.HyperSpace_PortalSize_ScaleOffset)), num3);
				int num7 = Math.Min((int)((double)num4 * ((double)(this.mPortalPercent * this.mPortalPercent * this.mPortalPercent) * Constants.mConstants.HyperSpace_PortalSize_Scale + Constants.mConstants.HyperSpace_PortalSize_ScaleOffset)), num4);
				float num8 = this.mPortalPercent * (float)num3 / 2f + (1f - this.mPortalPercent) * Hyperspace.mHyperRings[17].mCurScreenX;
				float num9 = this.mPortalPercent * (float)num4 / 2f + (1f - this.mPortalPercent) * Hyperspace.mHyperRings[17].mCurScreenY;
				Image theImage = (this.mApp.IsLandscape() ? GameConstants.BGH_TEXTURE : GameConstants.BGV_TEXTURE);
				if (num6 > 0 && num7 > 0)
				{
					g.SetColorizeImages(true);
					g.SetColor(new Color(num5, num5, num5));
					g.DrawImage(theImage, new TRect((int)((num8 - (float)(num6 / 2)) * Constants.mConstants.HyperSpace_TunnelEnd_Scale), (int)((num9 - (float)(num7 / 2)) * Constants.mConstants.HyperSpace_TunnelEnd_Scale), (int)((float)num6 * Constants.mConstants.HyperSpace_TunnelEnd_Scale), (int)((float)num7 * Constants.mConstants.HyperSpace_TunnelEnd_Scale)), new TRect(0, 0, (int)((float)num3 * Constants.mConstants.HyperSpace_TunnelEnd_Scale), (int)((float)num4 * Constants.mConstants.HyperSpace_TunnelEnd_Scale)));
					g.SetColorizeImages(false);
				}
				int num10 = (int)((double)Hyperspace.mHyperRings[17].mCurScreenRadius * 2.5);
				theDestRect = new TRect((int)((Hyperspace.mHyperRings[17].mCurScreenX - (float)(num10 / 2)) * Constants.mConstants.HyperSpace_TunnelEnd_Scale) + num, (int)((Hyperspace.mHyperRings[17].mCurScreenY - (float)(num10 / 2)) * Constants.mConstants.HyperSpace_TunnelEnd_Scale) + num2, (int)((float)num10 * Constants.mConstants.HyperSpace_TunnelEnd_Scale), (int)((float)num10 * Constants.mConstants.HyperSpace_TunnelEnd_Scale));
				if ((double)this.mPortalPercent < 1.0)
				{
					g.DrawImage(AtlasResources.IMAGE_TUNNEL_END, theDestRect, Constants.mConstants.HyperSpace_TunnelEnd_Source);
				}
			}
			if ((double)this.mPortalPercent < 1.0)
			{
				int num11 = 0;
				for (int i = 16; i >= 0; i--)
				{
					int num12 = Math.Min(255, 384 - i * 360 / 18);
					for (int j = 0; j < 20; j++)
					{
						HyperPoint hyperPoint = Hyperspace.mHyperRings[i].mHyperPoints[j];
						HyperPoint hyperPoint2 = Hyperspace.mHyperRings[i].mHyperPoints[(j + 1) % 20];
						HyperPoint hyperPoint3 = Hyperspace.mHyperRings[i + 1].mHyperPoints[j];
						HyperPoint hyperPoint4 = Hyperspace.mHyperRings[i + 1].mHyperPoints[(j + 1) % 20];
						TriVertex triVertex = new TriVertex(hyperPoint.mX * Constants.mConstants.HyperSpace_HyperPoint_Scale + (float)num, hyperPoint.mY * Constants.mConstants.HyperSpace_HyperPoint_Scale + (float)num2, hyperPoint.mU, hyperPoint.mV);
						TriVertex triVertex2 = new TriVertex(hyperPoint2.mX * Constants.mConstants.HyperSpace_HyperPoint_Scale + (float)num, hyperPoint2.mY * Constants.mConstants.HyperSpace_HyperPoint_Scale + (float)num2, hyperPoint2.mU, hyperPoint2.mV);
						TriVertex triVertex3 = new TriVertex(hyperPoint3.mX * Constants.mConstants.HyperSpace_HyperPoint_Scale + (float)num, hyperPoint3.mY * Constants.mConstants.HyperSpace_HyperPoint_Scale + (float)num2, hyperPoint3.mU, hyperPoint3.mV);
						TriVertex triVertex4 = new TriVertex(hyperPoint4.mX * Constants.mConstants.HyperSpace_HyperPoint_Scale + (float)num, hyperPoint4.mY * Constants.mConstants.HyperSpace_HyperPoint_Scale + (float)num2, hyperPoint4.mU, hyperPoint4.mV);
						if (triVertex.v >= 0f)
						{
							if (j == 19)
							{
								triVertex2.u += 1f;
								triVertex4.u += 1f;
							}
							if (triVertex.v > triVertex3.v)
							{
								triVertex3.v += 1f;
								triVertex4.v += 1f;
							}
							Color color;
							color..ctor(num12, num12, num12, 255);
							Hyperspace.triangleBuffer[num11, 0] = triVertex;
							Hyperspace.triangleBuffer[num11, 1] = triVertex2;
							Hyperspace.triangleBuffer[num11, 2] = triVertex3;
							for (int k = 0; k < 3; k++)
							{
								Hyperspace.triangleBuffer[num11, k].color = color;
							}
							num11++;
							Hyperspace.triangleBuffer[num11, 0] = triVertex2;
							Hyperspace.triangleBuffer[num11, 1] = triVertex3;
							Hyperspace.triangleBuffer[num11, 2] = triVertex4;
							for (int l = 0; l < 3; l++)
							{
								Hyperspace.triangleBuffer[num11, l].color = color;
							}
							num11++;
						}
					}
				}
				int theDrawMode = (this.mShowBkg ? 0 : 1);
				Image theTexture = (this.mShowBkg ? Resources.IMAGE_HYPERSPACE : Resources.IMAGE_HYPERSPACE_INITIAL);
				g.DrawTrianglesTex(Graphics.WrapSamplerState, theTexture, Hyperspace.triangleBuffer, num11, default(Color?), theDrawMode);
			}
			if (this.mShowBkg && (double)this.mPortalPercent < 1.0)
			{
				g.SetDrawMode(Graphics.DrawMode.DRAWMODE_ADDITIVE);
				g.SetColorizeImages(true);
				int num13 = (int)(this.mPortalPercent * 400f);
				if (num13 > 255)
				{
					num13 = 255;
				}
				if (num13 > 0)
				{
					g.SetColor(new Color(num13, num13, num13));
					int num10 = (int)(Hyperspace.mHyperRings[17].mCurScreenRadius * 5f);
					theDestRect = new TRect((int)((Hyperspace.mHyperRings[17].mCurScreenX - (float)(num10 / 2)) * Constants.mConstants.HyperSpace_TunnelEnd_Scale) + num, (int)((Hyperspace.mHyperRings[17].mCurScreenY - (float)(num10 / 2)) * Constants.mConstants.HyperSpace_TunnelEnd_Scale) + num2, Constants.mConstants.HyperSpace_FireRing_Width(num10), Constants.mConstants.HyperSpace_FireRing_Width(num10));
					g.DrawImage(AtlasResources.IMAGE_FIRE_RING, theDestRect, new TRect(this.mUpdateCnt / 2 % 10 * Constants.mConstants.HyperSpace_FireRing_Size, 0, Constants.mConstants.HyperSpace_FireRing_Size, Constants.mConstants.HyperSpace_FireRing_Size));
				}
				g.SetColorizeImages(false);
				g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
			}
			if (this.mFlashPercent > 0f)
			{
				int num14 = Math.Min((int)(255.0 * (double)this.mFlashPercent), 255);
				g.SetColorizeImages(true);
				g.SetColor(new Color(255, 255, 255, num14));
				g.FillRect(0, 0, this.mWidth, this.mHeight);
			}
		}

		public override void Update()
		{
			this.DoUpdate();
			if (this.mShowBkg)
			{
				this.DoUpdate();
				this.DoUpdate();
			}
			if (this.mApp.mBoard.mDoubleSpeed)
			{
				this.DoUpdate();
			}
		}

		public override void Draw(Graphics g)
		{
			this.Draw3dPortal(g);
		}

		public override void InterfaceOrientationChanged(UI_ORIENTATION theOrientation)
		{
			this.Resize(0, 0, this.mApp.mWidth, this.mApp.mHeight);
		}

		public bool mIs3d;

		public static FloatingThing[] mFloatingThings = new FloatingThing[2];

		public GameApp mApp;

		public static HyperRing[] mHyperRings = new HyperRing[18];

		private static TriVertex[,] triangleBuffer = new TriVertex[680, 3];

		public float mRingRotAcc;

		public float mRingRotAcc2;

		public float mRingXAcc;

		public float mRingXAcc2;

		public float mRingYAcc;

		public float mRingYAcc2;

		public float mCameraX;

		public float mCameraY;

		public float mScoreFloaterX;

		public float mScoreFloaterY;

		public int mPortalDelay;

		public float mPortalPercent;

		public bool mShowBkg;

		public int mFlashState;

		public int mFlashDelay;

		public float mFlashPercent;

		public float mTransPercent;

		public bool mIsDone;

		public int mDoneDelay;

		public int mEndSoundDelay;

		public int mEndTextPos;

		public float mPictureRingPos;

		public float mYOffset;

		public float mYOffsetVel;

		public float mYOffset2;

		public float mYOffset2Vel;

		public int mEffectUpdate;

		public float mWarpLineAcc;

		public float mWarpLineAdd;

		public float mWarpLineSpeed;

		public List<WarpLine> mWarpLineVector = new List<WarpLine>();

		public float mShakeFactor;

		public float mStretchFactor;

		public float mStretchVel;

		public bool mFirstShowBkg;
	}
}
