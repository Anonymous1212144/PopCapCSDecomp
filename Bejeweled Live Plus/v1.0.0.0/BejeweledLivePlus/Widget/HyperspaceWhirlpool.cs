using System;
using BejeweledLivePlus.Misc;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace BejeweledLivePlus.Widget
{
	internal class HyperspaceWhirlpool : Hyperspace
	{
		public void EndWhirlpool(bool bFromLoading)
		{
		}

		public void EndWhirlpool()
		{
			this.EndWhirlpool(false);
		}

		public void UpdateWhirlpool()
		{
			if (this.mWhirlpoolFade > 0.0)
			{
				this.mWhirlpoolFrame -= GlobalMembers.M(0.1f);
				if (this.mWhirlpoolFrame < 0f)
				{
					this.mWhirlpoolFrame += 5f;
				}
				this.mWhirlpoolRotAcc += (double)GlobalMembers.M(0.0005f);
				this.mWhirlpoolRot -= this.mWhirlpoolRotAcc;
				this.MarkDirty();
			}
			this.mWhirlpoolFade += 0.02;
			if (this.mWhirlpoolFade > 1.0)
			{
				this.mWhirlpoolFade = 1.0;
			}
			this.mWarpSizeMult += 0.001f;
			if (this.mWarpDelay > 0)
			{
				this.mWarpDelay--;
			}
			else
			{
				this.mWarpSpeed += 0.02f;
			}
			if (this.mUISuckDelay > 0)
			{
				this.mUISuckDelay--;
			}
			else
			{
				this.mUIWarpPercentAdd.IncInVal(0.05000000074505806);
				this.mUIWarpPercent += this.mUIWarpPercentAdd.GetOutVal() * (double)ConstantsWP.WHIRLPOOL_BKG_WARP_SPEED;
				if (this.mUIWarpPercent > 1.0)
				{
					this.mUIWarpPercent = 1.0;
				}
			}
			int mWidth = GlobalMembers.gApp.mWidth;
			int mHeight = GlobalMembers.gApp.mHeight;
			for (int i = 1; i < GlobalMembers.NUM_WARP_ROWS - 1; i++)
			{
				for (int j = 1; j < GlobalMembers.NUM_WARP_COLS - 1; j++)
				{
					GlobalMembers.WarpPoint warpPoint = this.mWarpPoints[i, j];
					int a = j;
					int b = GlobalMembers.NUM_WARP_COLS - 1 - j;
					int a2 = i;
					int b2 = GlobalMembers.NUM_WARP_ROWS - 1 - i;
					int num = GlobalMembers.MIN(GlobalMembers.MIN(a, b), GlobalMembers.MIN(a2, b2));
					if (this.mWarpDelay == 0)
					{
						warpPoint.mDist -= (float)(0.35 * (double)num * (double)this.mWarpSpeed);
						if (warpPoint.mDist < 0f)
						{
							warpPoint.mDist = 0f;
						}
						warpPoint.mRot += (float)(0.001 * (double)num * (double)this.mWarpSpeed);
					}
					if (warpPoint.mRot < 0f)
					{
						warpPoint.mRot += 6.28318548f;
					}
					else if (warpPoint.mRot > 6.28318548f)
					{
						warpPoint.mRot -= 6.28318548f;
					}
					int num2 = (int)((double)(warpPoint.mRot * 4096f) / 6.2831853071795862) % 4096;
					warpPoint.mX = GlobalMembers.COS_TAB[num2] * warpPoint.mDist * this.mWarpSizeMult + (float)(mWidth / 2);
					warpPoint.mY = GlobalMembers.SIN_TAB[num2] * warpPoint.mDist * this.mWarpSizeMult + (float)(mHeight / 2);
					this.mWarpPoints[i, j] = warpPoint;
				}
			}
			this.MarkDirty();
		}

		public void DoWhirlpool()
		{
			bool flag = GlobalMembers.gApp.Is3DAccelerated();
			if (flag)
			{
				int mWidth = GlobalMembers.gApp.mWidth;
				int mHeight = GlobalMembers.gApp.mHeight;
				for (int i = 0; i < GlobalMembers.NUM_WARP_ROWS; i++)
				{
					for (int j = 0; j < GlobalMembers.NUM_WARP_COLS; j++)
					{
						GlobalMembers.WarpPoint warpPoint = this.mWarpPoints[i, j];
						warpPoint.mX = (float)(j * mWidth / (GlobalMembers.NUM_WARP_COLS - 1));
						warpPoint.mY = (float)(i * mHeight / (GlobalMembers.NUM_WARP_ROWS - 1));
						warpPoint.mZ = 0f;
						warpPoint.mVelX = 0f;
						warpPoint.mVelY = 0f;
						warpPoint.mU = warpPoint.mX / (float)mWidth;
						warpPoint.mV = warpPoint.mY / (float)mHeight;
						float num = warpPoint.mX - (float)(mWidth / 2);
						float num2 = warpPoint.mY - (float)(mHeight / 2);
						warpPoint.mRot = (float)(Math.Atan2((double)num2, (double)num) + 6.2831853071795862);
						warpPoint.mDist = (float)Math.Sqrt((double)(num * num + num2 * num2));
						this.mWarpPoints[i, j] = warpPoint;
					}
				}
				this.mWarpSpeed = 0f;
				this.mWarpDelay = ConstantsWP.WHIRLPOOL_BKG_WARP_DELAY;
				this.mUISuckDelay = ConstantsWP.WHIRLPOOL_BKG_WARP_DELAY;
			}
			this.mUIWarpPercent = 0.0;
			this.mUIWarpPercentAdd.SetInVal(0.0);
			this.mWarpSizeMult = 1f;
			this.mWhirlpoolFade = 0.0;
			this.mWhirlpoolFrame = 0f;
			this.mWhirlpoolRot = 0.0;
			this.mWhirlpoolRotAcc = 0.0;
			this.mFirstWhirlDraw = true;
		}

		public void Draw3DWhirlpoolState(Graphics g)
		{
			if (!this.mShowBkg)
			{
				Graphics3D graphics3D = g.Get3D();
				g.PushState();
				graphics3D.SetBackfaceCulling(0, 0);
				int num = 0;
				g.SetDrawMode(Graphics.DrawMode.Normal);
				for (int i = 0; i < GlobalMembers.NUM_WARP_ROWS - 1; i++)
				{
					for (int j = 0; j < GlobalMembers.NUM_WARP_COLS - 1; j++)
					{
						GlobalMembers.WarpPoint warpPoint = this.mWarpPoints[i, j];
						GlobalMembers.WarpPoint warpPoint2 = this.mWarpPoints[i, j + 1];
						GlobalMembers.WarpPoint warpPoint3 = this.mWarpPoints[i + 1, j];
						GlobalMembers.WarpPoint warpPoint4 = this.mWarpPoints[i + 1, j + 1];
						SexyVertex2D sexyVertex2D = HyperspaceWhirlpool.DWS_aTriVertices[num++, 0];
						sexyVertex2D.x = warpPoint.mX;
						sexyVertex2D.y = warpPoint.mY;
						sexyVertex2D.u = warpPoint.mU;
						sexyVertex2D.v = warpPoint.mV;
						HyperspaceWhirlpool.DWS_aTriVertices[num - 1, 0] = sexyVertex2D;
						sexyVertex2D = HyperspaceWhirlpool.DWS_aTriVertices[num - 1, 1];
						sexyVertex2D.x = warpPoint2.mX;
						sexyVertex2D.y = warpPoint2.mY;
						sexyVertex2D.u = warpPoint2.mU;
						sexyVertex2D.v = warpPoint2.mV;
						HyperspaceWhirlpool.DWS_aTriVertices[num - 1, 1] = sexyVertex2D;
						sexyVertex2D = HyperspaceWhirlpool.DWS_aTriVertices[num - 1, 2];
						sexyVertex2D.x = warpPoint4.mX;
						sexyVertex2D.y = warpPoint4.mY;
						sexyVertex2D.u = warpPoint4.mU;
						sexyVertex2D.v = warpPoint4.mV;
						HyperspaceWhirlpool.DWS_aTriVertices[num - 1, 2] = sexyVertex2D;
						sexyVertex2D = HyperspaceWhirlpool.DWS_aTriVertices[num++, 0];
						sexyVertex2D.x = warpPoint.mX;
						sexyVertex2D.y = warpPoint.mY;
						sexyVertex2D.u = warpPoint.mU;
						sexyVertex2D.v = warpPoint.mV;
						HyperspaceWhirlpool.DWS_aTriVertices[num - 1, 0] = sexyVertex2D;
						sexyVertex2D = HyperspaceWhirlpool.DWS_aTriVertices[num - 1, 1];
						sexyVertex2D.x = warpPoint4.mX;
						sexyVertex2D.y = warpPoint4.mY;
						sexyVertex2D.u = warpPoint4.mU;
						sexyVertex2D.v = warpPoint4.mV;
						HyperspaceWhirlpool.DWS_aTriVertices[num - 1, 1] = sexyVertex2D;
						sexyVertex2D = HyperspaceWhirlpool.DWS_aTriVertices[num - 1, 2];
						sexyVertex2D.x = warpPoint3.mX;
						sexyVertex2D.y = warpPoint3.mY;
						sexyVertex2D.u = warpPoint3.mU;
						sexyVertex2D.v = warpPoint3.mV;
						HyperspaceWhirlpool.DWS_aTriVertices[num - 1, 2] = sexyVertex2D;
					}
				}
				g.DrawTrianglesTex(this.mBoard.mBackground.GetBackgroundImage().GetImage(), HyperspaceWhirlpool.DWS_aTriVertices, num);
				g.PopState();
			}
			if (this.mWhirlpoolFade > 0.0)
			{
				g.SetColorizeImages(true);
				g.SetColor(new Color(255, 255, 255, (int)(255.0 * this.mWhirlpoolFade)));
				Image image_HYPERSPACE_WHIRLPOOL_BLACK_HOLE_COVER = GlobalMembersResourcesWP.IMAGE_HYPERSPACE_WHIRLPOOL_BLACK_HOLE_COVER;
				int num2 = image_HYPERSPACE_WHIRLPOOL_BLACK_HOLE_COVER.GetWidth() / 2;
				int num3 = image_HYPERSPACE_WHIRLPOOL_BLACK_HOLE_COVER.GetHeight() / 2;
				int num4 = this.mWidth / 2 - num2;
				int num5 = this.mHeight / 2 - num3;
				int num6 = (int)g.mScaleX;
				int num7 = (int)g.mScaleY;
				Transform transform = new Transform();
				transform.Scale(ConstantsWP.WHIRLPOOL_IMG_SCALE, ConstantsWP.WHIRLPOOL_IMG_SCALE);
				transform.Translate((float)(num2 + num4), (float)(num3 + num5));
				g.DrawImageTransform(image_HYPERSPACE_WHIRLPOOL_BLACK_HOLE_COVER, transform, 0f, 0f);
				g.mScaleX = (float)num6;
				g.mScaleY = (float)num7;
				g.SetColorizeImages(false);
			}
			if (this.mWhirlpoolFade > 0.0)
			{
				g.SetColorizeImages(true);
				g.SetDrawMode(Graphics.DrawMode.Additive);
				int num8 = (int)this.mWhirlpoolFrame;
				int num9 = num8 + 1;
				if (num9 == 5)
				{
					num9 = 0;
				}
				float num10 = this.mWhirlpoolFrame - (float)num8;
				Rect celRect = GlobalMembersResourcesWP.IMAGE_HYPERSPACE_WHIRLPOOL_BLACK_HOLE.GetCelRect(num8);
				Rect celRect2 = GlobalMembersResourcesWP.IMAGE_HYPERSPACE_WHIRLPOOL_BLACK_HOLE.GetCelRect(num9);
				int num11 = (this.mWidth - celRect.mWidth) / 2;
				int num12 = (this.mHeight - celRect.mHeight) / 2;
				g.SetColor(new Color(255, 255, 255, (int)(255.0 * this.mWhirlpoolFade * (1.0 - (double)num10))));
				Transform transform2 = new Transform();
				if (g.mColor.mAlpha > 0 && g.mColor.mAlpha <= 255)
				{
					transform2.RotateRad((float)this.mWhirlpoolRot);
					transform2.Scale(ConstantsWP.WHIRLPOOL_IMG_SCALE, ConstantsWP.WHIRLPOOL_IMG_SCALE);
					transform2.Translate((float)(num11 + celRect.mWidth / 2), (float)(num12 + celRect.mHeight / 2));
					g.DrawImageTransform(GlobalMembersResourcesWP.IMAGE_HYPERSPACE_WHIRLPOOL_BLACK_HOLE, transform2, celRect, 0f, 0f);
				}
				g.SetColor(new Color(255, 255, 255, (int)(255.0 * this.mWhirlpoolFade * (double)num10)));
				if (g.mColor.mAlpha > 0 && g.mColor.mAlpha <= 255)
				{
					transform2.Reset();
					transform2.RotateRad((float)this.mWhirlpoolRot);
					transform2.Scale(ConstantsWP.WHIRLPOOL_IMG_SCALE, ConstantsWP.WHIRLPOOL_IMG_SCALE);
					transform2.Translate((float)(num11 + celRect2.mWidth / 2), (float)(num12 + celRect2.mHeight / 2));
					g.DrawImageTransform(GlobalMembersResourcesWP.IMAGE_HYPERSPACE_WHIRLPOOL_BLACK_HOLE, transform2, celRect2, 0f, 0f);
				}
				g.SetDrawMode(Graphics.DrawMode.Normal);
				g.SetColorizeImages(false);
			}
		}

		public void DoHyperspace()
		{
		}

		public void UpdateLevelTransition()
		{
		}

		protected void Update3dPortal()
		{
			if (this.mBoard.IsGamePaused())
			{
				return;
			}
			if (this.mFlashDelay > 0)
			{
				if (--this.mFlashDelay == 0)
				{
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
					float num = this.mPortalPercent / 50f + 0.001f;
					float num2 = (1.001f - this.mPortalPercent) * 5f;
					if (num2 < 1f)
					{
						num *= num2;
					}
					this.mPortalPercent += num;
				}
				if (this.mEndSoundDelay > 0)
				{
					int num3 = --this.mEndSoundDelay;
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
				GlobalMembers.HyperRing hyperRing = this.mHyperRings[GlobalMembers.NUM_HYPER_RINGS - 1];
				hyperRing.mFromRot += (float)(Math.Sin((double)this.mRingRotAcc) * 0.0020000000949949026);
				hyperRing.mFromRot += (float)(Math.Sin((double)this.mRingRotAcc2) * 0.0010000000474974513);
				hyperRing.mToRot = hyperRing.mFromRot;
				hyperRing.mFromX += (float)(Math.Sin((double)this.mRingXAcc) * 4.0) + (float)(Math.Sin((double)this.mRingXAcc2) * 5.0);
				hyperRing.mToX = hyperRing.mFromX;
				hyperRing.mFromY += (float)(Math.Sin((double)this.mRingYAcc) * 4.0) + (float)(Math.Sin((double)this.mRingYAcc2) * 5.0);
				hyperRing.mToY = hyperRing.mFromY;
				this.mHyperRings[GlobalMembers.NUM_HYPER_RINGS - 1] = hyperRing;
			}
			this.mTransPercent += 0.25f;
			if ((double)this.mTransPercent >= 1.0)
			{
				for (int i = 0; i < GlobalMembers.NUM_HYPER_RINGS - 1; i++)
				{
					GlobalMembers.HyperRing hyperRing2 = this.mHyperRings[i];
					GlobalMembers.HyperRing hyperRing3 = this.mHyperRings[i + 1];
					hyperRing2.mFromX = hyperRing2.mToX;
					hyperRing2.mToX = hyperRing3.mFromX;
					hyperRing2.mFromY = hyperRing2.mToY;
					hyperRing2.mToY = hyperRing3.mFromY;
					hyperRing2.mFromRot = hyperRing2.mToRot;
					hyperRing2.mToRot = hyperRing3.mFromRot;
					this.mHyperRings[i] = hyperRing2;
					this.mHyperRings[i + 1] = hyperRing3;
				}
				this.mTransPercent -= 1f;
			}
			float num4 = this.mHyperRings[0].mCurX - this.mCameraX;
			float num5 = this.mHyperRings[0].mCurY - this.mCameraY;
			this.mCameraX += num4 * ConstantsWP.HYPERSPACE_TUNNEL_WIDTH_SCALE;
			this.mCameraY += num5 * ConstantsWP.HYPERSPACE_TUNNEL_WIDTH_SCALE;
			float num6 = this.mHyperRings[1].mCurX - this.mScoreFloaterX;
			float num7 = this.mHyperRings[1].mCurY - this.mScoreFloaterY;
			this.mScoreFloaterX += (float)((double)num6 * 0.08);
			this.mScoreFloaterY += (float)((double)num7 * 0.08);
			int num8 = GlobalMembers.RS(this.mWidth);
			int num9 = GlobalMembers.RS(this.mHeight);
			if ((double)this.mPortalPercent < 1.0)
			{
				for (int j = 0; j < GlobalMembers.NUM_HYPER_RINGS; j++)
				{
					GlobalMembers.HyperRing hyperRing4 = this.mHyperRings[j];
					float num10 = hyperRing4.mFromRot * (1f - this.mTransPercent) + hyperRing4.mToRot * this.mTransPercent;
					float num11 = (hyperRing4.mCurX = hyperRing4.mFromX * (1f - this.mTransPercent) + hyperRing4.mToX * this.mTransPercent);
					float num12 = (hyperRing4.mCurY = hyperRing4.mFromY * (1f - this.mTransPercent) + hyperRing4.mToY * this.mTransPercent);
					int num13 = (int)(num10 * 4096f / 3.14159f * 2f);
					if (num13 < 0)
					{
						num13 = 4096 - num13;
					}
					float num14 = 1f - this.mPortalPercent;
					float num15 = 800f - (float)j * num14 * 5000f / (float)GlobalMembers.NUM_HYPER_RINGS;
					float num16 = (float)GlobalMembers.MAX_Z / ((float)GlobalMembers.MAX_Z - num15);
					int num17 = (int)(200f - (float)j * num14 * 150f / (float)GlobalMembers.NUM_HYPER_RINGS);
					float num18 = 1f - this.mPortalPercent;
					hyperRing4.mCurScreenX = (num11 - this.mCameraX) * num18 * num16 + (float)(num8 / 2);
					hyperRing4.mCurScreenY = (num12 - this.mCameraY) * num18 * num16 + (float)(num9 / 2);
					hyperRing4.mCurScreenRadius = (float)num17 * num16;
					for (int k = 0; k < GlobalMembers.NUM_RING_POINTS; k++)
					{
						GlobalMembers.HyperPoint hyperPoint = hyperRing4.mHyperPoints[k];
						int num19 = (4096 * k / GlobalMembers.NUM_RING_POINTS + num13) % 4096;
						hyperPoint.mX = (GlobalMembers.COS_TAB[num19] * (float)num17 + (num11 - this.mCameraX) * num18) * num16 + (float)(num8 / 2);
						hyperPoint.mY = (GlobalMembers.SIN_TAB[num19] * (float)num17 + (num12 - this.mCameraY) * num18) * num16 + (float)(num9 / 2);
						hyperPoint.mV += 0.006f;
						if (hyperPoint.mV > 1f)
						{
							hyperPoint.mV -= 1f;
						}
						hyperRing4.mHyperPoints[k] = hyperPoint;
					}
					this.mHyperRings[j] = hyperRing4;
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
			int num = 0;
			int num2 = 0;
			GlobalMembers.RS(this.mWidth);
			GlobalMembers.RS(this.mHeight);
			if (this.mShowBkg)
			{
				g.SetDrawMode(Graphics.DrawMode.Normal);
				int num3 = (int)((double)this.mHyperRings[GlobalMembers.NUM_HYPER_RINGS - 1].mCurScreenRadius * 2.5);
				Rect theDestRect = new Rect((int)GlobalMembers.S(this.mHyperRings[GlobalMembers.NUM_HYPER_RINGS - 1].mCurScreenX - (float)(num3 / 2)) + num, (int)GlobalMembers.S(this.mHyperRings[GlobalMembers.NUM_HYPER_RINGS - 1].mCurScreenY - (float)(num3 / 2)) + num2, GlobalMembers.S(num3), GlobalMembers.S(num3));
				if ((double)this.mPortalPercent < 1.0)
				{
					g.DrawImage(GlobalMembersResourcesWP.IMAGE_HYPERSPACE_WHIRLPOOL_TUNNELEND, theDestRect, GlobalMembersResourcesWP.IMAGE_HYPERSPACE_WHIRLPOOL_TUNNELEND.GetCelRect(0));
				}
			}
			if ((double)this.mPortalPercent < 1.0)
			{
				g.PushState();
				g.SetDrawMode(this.mShowBkg ? Graphics.DrawMode.Normal : Graphics.DrawMode.Additive);
				g.SetColorizeImages(true);
				Image image_HYPERSPACE_WHIRLPOOL_HYPERSPACE_NORMAL = GlobalMembersResourcesWP.IMAGE_HYPERSPACE_WHIRLPOOL_HYPERSPACE_NORMAL;
				int num4 = 0;
				for (int i = GlobalMembers.NUM_HYPER_RINGS - 2; i >= 0; i--)
				{
					int num5 = GlobalMembers.MIN(255, 384 - i * 360 / GlobalMembers.NUM_HYPER_RINGS);
					Color color = new Color(num5, num5, num5, 255);
					for (int j = 0; j < GlobalMembers.NUM_RING_POINTS; j++)
					{
						GlobalMembers.HyperPoint hyperPoint = this.mHyperRings[i].mHyperPoints[j];
						GlobalMembers.HyperPoint hyperPoint2 = this.mHyperRings[i].mHyperPoints[(j + 1) % GlobalMembers.NUM_RING_POINTS];
						GlobalMembers.HyperPoint hyperPoint3 = this.mHyperRings[i + 1].mHyperPoints[j];
						GlobalMembers.HyperPoint hyperPoint4 = this.mHyperRings[i + 1].mHyperPoints[(j + 1) % GlobalMembers.NUM_RING_POINTS];
						SexyVertex2D sexyVertex2D = new SexyVertex2D(GlobalMembers.S(hyperPoint.mX + (float)num), GlobalMembers.S(hyperPoint.mY + (float)num2), hyperPoint.mU, hyperPoint.mV);
						SexyVertex2D sexyVertex2D2 = new SexyVertex2D(GlobalMembers.S(hyperPoint2.mX + (float)num), GlobalMembers.S(hyperPoint2.mY + (float)num2), hyperPoint2.mU, hyperPoint2.mV);
						SexyVertex2D sexyVertex2D3 = new SexyVertex2D(GlobalMembers.S(hyperPoint3.mX + (float)num), GlobalMembers.S(hyperPoint3.mY + (float)num2), hyperPoint3.mU, hyperPoint3.mV);
						SexyVertex2D sexyVertex2D4 = new SexyVertex2D(GlobalMembers.S(hyperPoint4.mX + (float)num), GlobalMembers.S(hyperPoint4.mY + (float)num2), hyperPoint4.mU, hyperPoint4.mV);
						if (sexyVertex2D.v >= 0f)
						{
							if (j == GlobalMembers.NUM_RING_POINTS - 1)
							{
								sexyVertex2D2.u += 1f;
								sexyVertex2D4.u += 1f;
							}
							if (sexyVertex2D.v > sexyVertex2D3.v)
							{
								sexyVertex2D3.v += 1f;
								sexyVertex2D4.v += 1f;
							}
							HyperspaceWhirlpool.DP_vertices[num4, 0] = sexyVertex2D;
							HyperspaceWhirlpool.DP_vertices[num4, 0].color = color;
							HyperspaceWhirlpool.DP_vertices[num4, 1] = sexyVertex2D2;
							HyperspaceWhirlpool.DP_vertices[num4, 1].color = color;
							HyperspaceWhirlpool.DP_vertices[num4, 2] = sexyVertex2D3;
							HyperspaceWhirlpool.DP_vertices[num4, 2].color = color;
							num4++;
							HyperspaceWhirlpool.DP_vertices[num4, 0] = sexyVertex2D2;
							HyperspaceWhirlpool.DP_vertices[num4, 0].color = color;
							HyperspaceWhirlpool.DP_vertices[num4, 1] = sexyVertex2D3;
							HyperspaceWhirlpool.DP_vertices[num4, 1].color = color;
							HyperspaceWhirlpool.DP_vertices[num4, 2] = sexyVertex2D4;
							HyperspaceWhirlpool.DP_vertices[num4, 2].color = color;
							num4++;
						}
					}
				}
				Image theTexture = (this.mShowBkg ? image_HYPERSPACE_WHIRLPOOL_HYPERSPACE_NORMAL : GlobalMembersResourcesWP.IMAGE_HYPERSPACE_WHIRLPOOL_INITIAL);
				g.DrawTrianglesTex(theTexture, HyperspaceWhirlpool.DP_vertices, num4);
				g.PopState();
			}
			if (this.mShowBkg && (double)this.mPortalPercent < 1.0)
			{
				g.SetDrawMode(Graphics.DrawMode.Additive);
				g.SetColorizeImages(true);
				int num6 = (int)(this.mPortalPercent * 400f);
				if (num6 > 255)
				{
					num6 = 255;
				}
				if (num6 > 0)
				{
					g.SetColor(new Color(num6, num6, num6));
					int num3 = (int)(this.mHyperRings[GlobalMembers.NUM_HYPER_RINGS - 1].mCurScreenRadius * 4f);
					Rect theDestRect = new Rect((int)GlobalMembers.S(this.mHyperRings[GlobalMembers.NUM_HYPER_RINGS - 1].mCurScreenX - (float)(num3 / 2) + (float)num), (int)GlobalMembers.S(this.mHyperRings[GlobalMembers.NUM_HYPER_RINGS - 1].mCurScreenY - (float)(num3 / 2) + (float)num2), GlobalMembers.S(num3), GlobalMembers.S(num3));
					g.DrawImage(GlobalMembersResourcesWP.IMAGE_HYPERSPACE_WHIRLPOOL_FIRERING, theDestRect, GlobalMembersResourcesWP.IMAGE_HYPERSPACE_WHIRLPOOL_FIRERING.GetCelRect(this.mUpdateCnt / 2 % GlobalMembersResourcesWP.IMAGE_HYPERSPACE_WHIRLPOOL_FIRERING.GetCelCount()));
				}
				g.SetColorizeImages(false);
				g.SetDrawMode(Graphics.DrawMode.Normal);
			}
			if (this.mFlashPercent > 0f)
			{
				int theAlpha = GlobalMembers.MIN((int)(255.0 * (double)this.mFlashPercent), 255);
				g.SetColor(new Color(255, 255, 255, theAlpha));
				g.FillRect(0, 0, this.mWidth, this.mHeight);
			}
		}

		public HyperspaceWhirlpool(Board theBoard)
		{
			BejeweledLivePlusApp.LoadContent("HyperspaceWhirlpool_Common");
			if (GlobalMembers.gApp.mCurrentGameMode == GameMode.MODE_ZEN)
			{
				BejeweledLivePlusApp.LoadContent("HyperspaceWhirlpool_Zen");
			}
			else
			{
				BejeweledLivePlusApp.LoadContent("HyperspaceWhirlpool_Normal");
			}
			this.mBoard = theBoard;
			this.mMouseVisible = false;
			this.mIsDone = false;
			this.mDoneDelay = 0;
			this.mHyperRings = new GlobalMembers.HyperRing[GlobalMembers.NUM_HYPER_RINGS];
			for (int i = 0; i < GlobalMembers.NUM_HYPER_RINGS; i++)
			{
				this.mHyperRings[i].Init();
			}
			this.mIs3d = GlobalMembers.gApp.Is3DAccelerated();
			if (!GlobalMembers.gTableInitialized)
			{
				for (int j = 0; j < 4096; j++)
				{
					GlobalMembers.SIN_TAB[j] = (float)Math.Sin((double)j * 3.14159 * 2.0 / 4096.0);
					GlobalMembers.COS_TAB[j] = (float)Math.Cos((double)j * 3.14159 * 2.0 / 4096.0);
				}
				GlobalMembers.gTableInitialized = true;
			}
			this.mState = HyperspaceWhirlpool.HyperSpaceState.HyperSpaceState_Nil;
			this.SetState(HyperspaceWhirlpool.HyperSpaceState.HyperSpaceState_Init);
		}

		public override void Update()
		{
			base.WidgetUpdate();
			HyperspaceWhirlpool.HyperSpaceState hyperSpaceState;
			do
			{
				hyperSpaceState = this.mState;
				switch (this.mState)
				{
				case HyperspaceWhirlpool.HyperSpaceState.HyperSpaceState_Init:
					this.SetState(HyperspaceWhirlpool.HyperSpaceState.HyperSpaceState_CloseBoard);
					break;
				case HyperspaceWhirlpool.HyperSpaceState.HyperSpaceState_CloseBoard:
					if (this.mTransitionBoard)
					{
						this.mBoard.UpdateBoardTransition(true);
					}
					else
					{
						this.SetState(HyperspaceWhirlpool.HyperSpaceState.HyperSpaceState_SlideOver);
					}
					break;
				case HyperspaceWhirlpool.HyperSpaceState.HyperSpaceState_SlideOver:
					if (this.mSlidingHUD)
					{
						this.mBoard.UpdateSlidingHUD(true);
					}
					else
					{
						this.SetState(HyperspaceWhirlpool.HyperSpaceState.HyperSpaceState_Whirlpool);
					}
					break;
				case HyperspaceWhirlpool.HyperSpaceState.HyperSpaceState_Whirlpool:
					this.mBoard.mSideAlpha.SetConstant(1.0);
					this.mBoard.mShowBoard = false;
					this.UpdateWhirlpool();
					if (this.mUIWarpPercent == 1.0)
					{
						this.SetState(HyperspaceWhirlpool.HyperSpaceState.HyperSpaceState_PortalRide);
					}
					break;
				case HyperspaceWhirlpool.HyperSpaceState.HyperSpaceState_PortalRide:
					if (!this.mShowBkg)
					{
						this.UpdateWhirlpool();
					}
					this.Update3dPortal();
					if (this.mShowBkg && !this.mBoard.mShowBoard)
					{
						this.mBoard.HyperspaceEvent(HYPERSPACEEVENT.HYPERSPACEEVENT_Start);
						this.mBoard.HyperspaceEvent(HYPERSPACEEVENT.HYPERSPACEEVENT_OldLevelClear);
						this.mBoard.HyperspaceEvent(HYPERSPACEEVENT.HYPERSPACEEVENT_ZoomIn);
					}
					if (this.mIsDone)
					{
						this.SetState(HyperspaceWhirlpool.HyperSpaceState.HyperSpaceState_SlideBack);
					}
					break;
				case HyperspaceWhirlpool.HyperSpaceState.HyperSpaceState_SlideBack:
					if (this.mSlidingHUD)
					{
						this.mBoard.UpdateSlidingHUD(false);
					}
					else
					{
						this.SetState(HyperspaceWhirlpool.HyperSpaceState.HyperSpaceState_OpenBoard);
					}
					break;
				case HyperspaceWhirlpool.HyperSpaceState.HyperSpaceState_OpenBoard:
					if (this.mTransitionBoard)
					{
						this.mBoard.UpdateBoardTransition(false);
					}
					else
					{
						this.SetState(HyperspaceWhirlpool.HyperSpaceState.HyperSpaceState_Complete);
					}
					break;
				}
			}
			while (this.mState != hyperSpaceState);
		}

		public override void Draw(Graphics g)
		{
			Graphics3D graphics3D = g.Get3D();
			if (graphics3D == null)
			{
				return;
			}
			switch (this.mState)
			{
			case HyperspaceWhirlpool.HyperSpaceState.HyperSpaceState_SlideOver:
			case HyperspaceWhirlpool.HyperSpaceState.HyperSpaceState_SlideBack:
				break;
			case HyperspaceWhirlpool.HyperSpaceState.HyperSpaceState_Whirlpool:
				this.Draw3DWhirlpoolState(g);
				return;
			case HyperspaceWhirlpool.HyperSpaceState.HyperSpaceState_PortalRide:
				if (!this.mShowBkg)
				{
					this.Draw3DWhirlpoolState(g);
				}
				graphics3D.ClearDepthBuffer();
				this.Draw3dPortal(g);
				break;
			default:
				return;
			}
		}

		public void InterfaceOrientationChanged(UI_ORIENTATION theOrientation)
		{
			this.Resize(0, 0, GlobalMembers.gApp.mWidth, GlobalMembers.gApp.mHeight);
		}

		public override float GetPieceAlpha()
		{
			return 1f;
		}

		public override bool IsUsing3DTransition()
		{
			return true;
		}

		public void SetState(HyperspaceWhirlpool.HyperSpaceState state)
		{
			if (state == this.mState)
			{
				return;
			}
			this.mState = state;
			switch (this.mState)
			{
			case HyperspaceWhirlpool.HyperSpaceState.HyperSpaceState_Init:
			{
				GlobalMembers.gApp.DisableOptionsButtons(true);
				GlobalMembers.gApp.mBoard.DisableUI(false);
				this.mWhirlpoolFade = 0.0;
				this.mWhirlpoolRot = 0.0;
				this.mWhirlpoolRotAcc = 0.0;
				this.mInterfaceRestorePercent = 0f;
				this.mBoard.mFirstDraw = true;
				this.mFirstWhirlDraw = false;
				this.mWarpSpeed = 0f;
				this.mUIWarpPercentAdd.SetOutRange(0.0, 1.0);
				this.mUIWarpPercentAdd.SetMode(0);
				this.mUIWarpPercentAdd.SetRamp(2);
				this.DoWhirlpool();
				this.mFlashDelay = ConstantsWP.WHIRLPOOL_TO_HYPERSPACE_FADE_TIME;
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
				this.mPortalDelay = ConstantsWP.HYPERSPACE_TUNNEL_TIME;
				this.mEndSoundDelay = 100;
				this.mPortalPercent = 0f;
				for (int i = 0; i < GlobalMembers.NUM_HYPER_RINGS; i++)
				{
					GlobalMembers.HyperRing hyperRing = this.mHyperRings[i];
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
					for (int j = 0; j < GlobalMembers.NUM_RING_POINTS; j++)
					{
						GlobalMembers.HyperPoint hyperPoint = hyperRing.mHyperPoints[j];
						hyperPoint.mU = (float)j / (float)GlobalMembers.NUM_RING_POINTS;
						hyperPoint.mV = (float)i * 0.02f - (float)GlobalMembers.NUM_HYPER_RINGS * 0.02f;
						hyperRing.mHyperPoints[j] = hyperPoint;
					}
					this.mHyperRings[i] = hyperRing;
				}
				this.mEndTextPos = 800;
				this.mYOffset = 4f;
				this.mYOffsetVel = 0.3f;
				this.mYOffset2 = 0f;
				this.mYOffset2Vel = 0f;
				this.mEffectUpdate = 0;
				this.mWarpLineAcc = 0f;
				this.mWarpLineAdd = 0f;
				this.mWarpLineSpeed = 0.12f;
				this.mShakeFactor = 0f;
				this.mStretchFactor = 1f;
				this.mStretchVel = 0f;
				this.mFirstShowBkg = true;
				this.mSlidingHUD = false;
				this.mTransitionBoard = false;
				return;
			}
			case HyperspaceWhirlpool.HyperSpaceState.HyperSpaceState_CloseBoard:
				this.mTransitionBoard = true;
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_TRANSITION_BOARD_CURVE_CLOSE, this.mBoard.mTransitionBoardCurve);
				return;
			case HyperspaceWhirlpool.HyperSpaceState.HyperSpaceState_SlideOver:
				this.mSlidingHUD = true;
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_SLIDING_HUD_CURVE_OVER, this.mBoard.mSlidingHUDCurve);
				return;
			case HyperspaceWhirlpool.HyperSpaceState.HyperSpaceState_Whirlpool:
				GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_HYPERSPACE);
				return;
			case HyperspaceWhirlpool.HyperSpaceState.HyperSpaceState_PortalRide:
				this.mBoard.RandomizeBoard();
				this.mBoard.MoveGemsOffscreen();
				return;
			case HyperspaceWhirlpool.HyperSpaceState.HyperSpaceState_SlideBack:
				this.mSlidingHUD = true;
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_SLIDING_HUD_CURVE_BACK, this.mBoard.mSlidingHUDCurve);
				return;
			case HyperspaceWhirlpool.HyperSpaceState.HyperSpaceState_OpenBoard:
				this.mTransitionBoard = true;
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_TRANSITION_BOARD_CURVE_OPEN, this.mBoard.mTransitionBoardCurve);
				return;
			case HyperspaceWhirlpool.HyperSpaceState.HyperSpaceState_Complete:
				this.mWidgetManager.SetFocus(this.mBoard);
				this.mBoard.HyperspaceEvent(HYPERSPACEEVENT.HYPERSPACEEVENT_Finish);
				GlobalMembers.gApp.DisableOptionsButtons(false);
				GlobalMembers.gApp.mBoard.DisableUI(false);
				return;
			default:
				return;
			}
		}

		public Board mBoard;

		public bool mSlidingHUD;

		public bool mTransitionBoard;

		public HyperspaceWhirlpool.HyperSpaceState mState;

		public bool mIs3d;

		public GlobalMembers.HyperRing[] mHyperRings;

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

		public float mShakeFactor;

		public float mStretchFactor;

		public float mStretchVel;

		public bool mFirstShowBkg;

		public int mTransitionPos;

		public float mWhirlpoolFrame;

		public double mWhirlpoolFade;

		public double mWhirlpoolRot;

		public double mWhirlpoolRotAcc;

		private GlobalMembers.WarpPoint[,] mWarpPoints = new GlobalMembers.WarpPoint[GlobalMembers.NUM_WARP_ROWS, GlobalMembers.NUM_WARP_COLS];

		public float mWarpSpeed;

		public int mWarpDelay;

		public int mUISuckDelay;

		public float mWarpSizeMult;

		public CurvedVal mUIWarpPercentAdd = new CurvedVal();

		public double mUIWarpPercent;

		public bool mFirstWhirlDraw;

		public int mHyperspaceDelay;

		public float mInterfaceRestorePercent;

		private static SexyVertex2D[,] DWS_aTriVertices = new SexyVertex2D[GlobalMembers.NUM_WARP_COLS * GlobalMembers.NUM_WARP_ROWS * 2, 3];

		private static SexyVertex2D[,] DP_vertices = new SexyVertex2D[GlobalMembers.NUM_HYPER_RINGS * GlobalMembers.NUM_RING_POINTS * 2, 3];

		public enum HyperSpaceState
		{
			HyperSpaceState_Init,
			HyperSpaceState_CloseBoard,
			HyperSpaceState_SlideOver,
			HyperSpaceState_Whirlpool,
			HyperSpaceState_PortalRide,
			HyperSpaceState_SlideBack,
			HyperSpaceState_OpenBoard,
			HyperSpaceState_Complete,
			HyperSpaceState_DebugDrawEveryOther,
			HyperSpaceState_Max,
			HyperSpaceState_Nil = -1
		}
	}
}
