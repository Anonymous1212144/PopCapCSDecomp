using System;
using System.Collections.Generic;
using JeffLib;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	public class ZumaTip
	{
		public ZumaTip(string text, int width, int height, Rect cutout_region, int id)
		{
			this.mCutoutX = cutout_region.mX;
			this.mCutoutY = cutout_region.mY;
			this.mCutoutW = cutout_region.mWidth;
			this.mCutoutH = cutout_region.mHeight;
			this.mText = text;
			this.mId = id;
			this.mWidth = width + ZumasRevenge.Common._DS(100);
			this.mHeight = height + ZumasRevenge.Common._DS(20);
			if (this.mCutoutX < 0 && id != ZumaProfile.FRUIT_HINT)
			{
				this.mCutoutX = 0;
			}
			if (id != ZumaProfile.CHALLENGE_HINT)
			{
				if (id == ZumaProfile.FIRST_SHOT_HINT)
				{
					this.mMaskImage = Res.GetImageByID(ResID.IMAGE_UI_CONE);
					this.mCutoutW = this.mMaskImage.mWidth * 4;
					this.mCutoutH = this.mMaskImage.mHeight * 4;
				}
				else if (id == ZumaProfile.ZUMA_BAR_HINT)
				{
					this.SetZumaBarBoundingBox();
					this.CreateCutoutImage();
				}
				else
				{
					this.mMaskImage = Res.GetImageByID(ResID.IMAGE_UI_CIRCLE);
				}
			}
			int num = 0;
			Graphics graphics = new Graphics();
			graphics.SetFont(Res.GetFontByID(ResID.FONT_MAIN22));
			this.mTextHeight = graphics.GetWordWrappedHeight(this.mWidth - ZumasRevenge.Common._DS(100), this.mText, -1, ref num, ref num);
			CommonGraphics.SetNonMaskedArea(this.mCutoutX, this.mCutoutY, this.mCutoutW, this.mCutoutH, this.mMaskedRects, ZumaTip.MAX_ALPHA);
			if (this.mMaskedRects.Count == 4)
			{
				this.mMaskedRects[0].r.mX = -GameApp.gApp.mBoardOffsetX;
				MaskedRect maskedRect = this.mMaskedRects[0];
				maskedRect.r.mWidth = maskedRect.r.mWidth + GameApp.gApp.mBoardOffsetX;
				return;
			}
			if (this.mMaskedRects.Count == 3)
			{
				this.mMaskedRects.Add(new MaskedRect(new Rect(-GameApp.gApp.mBoardOffsetX, 0, GameApp.gApp.mBoardOffsetX, GlobalMembers.gSexyApp.mScreenBounds.mHeight), ZumaTip.MAX_ALPHA));
			}
		}

		public virtual void Dispose()
		{
			this.mCutoutImage = null;
		}

		public void PointAt(int x, int y, int dir)
		{
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GUI_ARROW);
			int num = ZumasRevenge.Common._DS(ZumasRevenge.Common._M(175));
			if (dir == 0)
			{
				this.mArrowAngle = 3.14159274f;
				this.mArrowX = x + ZumasRevenge.Common._DS(ZumasRevenge.Common._M(24));
				this.mArrowY = y - imageByID.mHeight / 2;
				this.mBoxRect = new Rect(x + num, y - this.mHeight / 2, this.mWidth, this.mHeight);
				if (this.mBoxRect.mY < 0)
				{
					this.mBoxRect.mY = 0;
					return;
				}
				if (this.mBoxRect.mY + this.mBoxRect.mHeight > GlobalMembers.gSexyApp.mHeight)
				{
					this.mBoxRect.mY = GlobalMembers.gSexyApp.mHeight - this.mBoxRect.mHeight;
					return;
				}
			}
			else if (dir == 1)
			{
				this.mArrowAngle = 0f;
				this.mArrowX = x - imageByID.mWidth - ZumasRevenge.Common._DS(ZumasRevenge.Common._M(24));
				this.mArrowY = y - imageByID.mHeight / 2;
				this.mBoxRect = new Rect(x - num - this.mWidth, y - this.mHeight / 2, this.mWidth, this.mHeight);
				if (this.mBoxRect.mY < 0)
				{
					this.mBoxRect.mY = 0;
					return;
				}
				if (this.mBoxRect.mY + this.mBoxRect.mHeight > GlobalMembers.gSexyApp.mHeight)
				{
					this.mBoxRect.mY = GlobalMembers.gSexyApp.mHeight - this.mBoxRect.mHeight;
					return;
				}
			}
			else if (dir == 2)
			{
				this.mArrowAngle = 1.57079637f;
				this.mArrowX = x - imageByID.mWidth / 2;
				this.mArrowY = y + ZumasRevenge.Common._DS(ZumasRevenge.Common._M(48));
				this.mBoxRect = new Rect(x - this.mWidth / 2, y + num, this.mWidth, this.mHeight);
				if (this.mBoxRect.mX < 0)
				{
					this.mBoxRect.mX = 0;
					return;
				}
				if (this.mBoxRect.mX + this.mBoxRect.mWidth > GlobalMembers.gSexyApp.mWidth)
				{
					this.mBoxRect.mX = GlobalMembers.gSexyApp.mWidth - this.mBoxRect.mWidth;
					return;
				}
			}
			else if (dir == 3)
			{
				this.mArrowAngle = -1.57079637f;
				this.mArrowX = x - imageByID.mWidth / 2;
				this.mArrowY = y - imageByID.mHeight - ZumasRevenge.Common._DS(ZumasRevenge.Common._M(46));
				this.mBoxRect = new Rect(x - this.mWidth / 2, y - num - this.mHeight, this.mWidth, this.mHeight);
				if (this.mBoxRect.mX < 0)
				{
					this.mBoxRect.mX = 0;
					return;
				}
				if (this.mBoxRect.mX + this.mBoxRect.mWidth > GlobalMembers.gSexyApp.mWidth)
				{
					this.mBoxRect.mX = GlobalMembers.gSexyApp.mWidth - this.mBoxRect.mWidth;
				}
			}
		}

		public void AutoPointAt(int x, int y, int region_w, int region_h)
		{
			int num = GlobalMembers.gSexyApp.mWidth - (x + region_w);
			int num2 = GlobalMembers.gSexyApp.mHeight - (y + region_h);
			int[] array = new int[] { num, x, num2, y };
			int num3 = 0;
			for (int i = 1; i < 4; i++)
			{
				if (array[i] > array[num3])
				{
					num3 = i;
				}
			}
			if (num3 == 0)
			{
				this.PointAt(x + region_w, y + region_h / 2, num3);
				return;
			}
			if (num3 == 1)
			{
				this.PointAt(x, y + region_h / 2, num3);
				return;
			}
			if (num3 == 2)
			{
				this.PointAt(x + region_w / 2, y + region_h, num3);
				return;
			}
			if (num3 == 3)
			{
				this.PointAt(x + region_w / 2, y, num3);
			}
		}

		public void AutoPointAtCutoutRegion()
		{
			this.AutoPointAt(this.mCutoutX, this.mCutoutY, this.mCutoutW, this.mCutoutH);
		}

		public void Draw(Graphics g)
		{
			if (this.mUpdateCount < this.mAppearDelay)
			{
				return;
			}
			if (this.mCutoutImage != null)
			{
				g.DrawImage(this.mCutoutImage, this.mCutoutX, this.mCutoutY);
			}
			else if (this.mMaskImage != null)
			{
				g.DrawImage(this.mMaskImage, this.mCutoutX, this.mCutoutY, this.mCutoutW, this.mCutoutH);
			}
			if (this.mMaskImage != null || this.mCutoutImage != null)
			{
				if (this.mCutoutX >= 0)
				{
					ZumasRevenge.Common._S(80);
				}
				else
				{
					ZumasRevenge.Common._S(80);
				}
				g.SetColor(0, 0, 0, ZumaTip.MAX_ALPHA);
				for (int i = 0; i < this.mMaskedRects.size<MaskedRect>(); i++)
				{
					g.FillRect(this.mMaskedRects[i].r);
				}
			}
			ZumasRevenge.Common.DrawCommonDialogBacking(g, this.mBoxRect.mX, this.mBoxRect.mY, this.mBoxRect.mWidth, this.mBoxRect.mHeight);
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GUI_ARROW);
			if (this.mDrawArrow)
			{
				g.DrawImageRotated(imageByID, this.mArrowX, (int)((float)this.mArrowY + this.mArrowYOff), (double)this.mArrowAngle);
				if (this.mDoArrowAnim)
				{
					g.PushState();
					g.SetColorizeImages(true);
					g.SetDrawMode(1);
					g.SetColor(255, 255, 255, (int)this.mArrowAlpha);
					g.DrawImageRotated(imageByID, this.mArrowX, (int)((float)this.mArrowY + this.mArrowYOff), (double)this.mArrowAngle);
					g.PopState();
					if (this.mId == ZumaProfile.FIRST_SHOT_HINT)
					{
						g.SetFont(Res.GetFontByID(ResID.FONT_SHAGLOUNGE45_GAUNTLET));
						g.SetColor(255, 253, 99);
						g.DrawString(TextManager.getInstance().getString(824), ZumasRevenge.Common._DS(ZumasRevenge.Common._M(140)), ZumasRevenge.Common._DS(ZumasRevenge.Common._M1(540)));
					}
				}
			}
			g.SetColor(255, 220, 135);
			g.SetFont(Res.GetFontByID(ResID.FONT_MAIN22));
			int num = ZumasRevenge.Common._M(50);
			int num2 = ZumasRevenge.Common._M(0);
			num = ZumasRevenge.Common._DS(num);
			num2 = ZumasRevenge.Common._DS(num2);
			Rect theRect = new Rect(this.mBoxRect.mX + num, this.mBoxRect.mY + num2, this.mBoxRect.mWidth - num * 2, this.mBoxRect.mHeight - num2 * 2);
			theRect.mY += (theRect.mHeight - this.mTextHeight) / 2;
			g.WriteWordWrapped(theRect, this.mText, -1, 0);
		}

		public void Update()
		{
			this.mUpdateCount++;
			if (this.mDoArrowAnim)
			{
				float num = ZumasRevenge.Common._M(10.5f);
				float num2 = ZumasRevenge.Common._M(0.5f);
				float num3 = (float)ZumasRevenge.Common._M(10);
				this.mArrowAlpha += num * (float)this.mArrowAlphaDir;
				if (this.mArrowAlpha >= 255f && this.mArrowAlphaDir == 1)
				{
					this.mArrowAlpha = 255f;
					this.mArrowAlphaDir = -1;
				}
				else if (this.mArrowAlpha <= 0f && this.mArrowAlphaDir == -1)
				{
					this.mArrowAlphaDir = 1;
					this.mArrowAlpha = 0f;
				}
				this.mArrowYOff += num2 * (float)this.mArrowYOffDir;
				if (this.mArrowYOff >= num3 && this.mArrowYOffDir == 1)
				{
					this.mArrowYOff = num3;
					this.mArrowYOffDir = -1;
					return;
				}
				if (this.mArrowYOff <= 0f && this.mArrowYOffDir == -1)
				{
					this.mArrowYOff = 0f;
					this.mArrowYOffDir = 1;
				}
			}
		}

		public bool CutoutContainsPoint(int x, int y)
		{
			Rect rect = new Rect(this.mCutoutX, this.mCutoutY, this.mCutoutW, this.mCutoutH);
			return rect.Contains(x, y);
		}

		private void SetZumaBarBoundingBox()
		{
			GameApp gApp = GameApp.gApp;
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GUI_INGAME_UI_WOOD);
			Image imageByID2 = Res.GetImageByID(ResID.IMAGE_GUI_INGAME_UI_RIGHT_MOUTH_UPPER);
			Image imageByID3 = Res.GetImageByID(ResID.IMAGE_GUI_INGAME_UI_LEFT_MOUTH_LOWER);
			int num = (gApp.IsWideScreen() ? 0 : ((int)((float)imageByID.mWidth * 0.05f)));
			int wideScreenAdjusted = gApp.GetWideScreenAdjusted(ZumasRevenge.Common._DS(Res.GetOffsetXByID(ResID.IMAGE_GUI_INGAME_UI_LEFT_MOUTH_LOWER)) + num);
			int wideScreenAdjusted2 = gApp.GetWideScreenAdjusted(ZumasRevenge.Common._DS(Res.GetOffsetXByID(ResID.IMAGE_GUI_INGAME_UI_RIGHT_MOUTH_UPPER)) - num);
			this.mCutoutX = wideScreenAdjusted + ZumasRevenge.Common._DS(25);
			this.mCutoutY = ZumasRevenge.Common._DS(Res.GetOffsetYByID(ResID.IMAGE_GUI_INGAME_UI_LEFT_MOUTH_LOWER));
			this.mCutoutW = wideScreenAdjusted2 - wideScreenAdjusted + imageByID2.mWidth - ZumasRevenge.Common._DS(50);
			this.mCutoutH = imageByID3.mHeight;
		}

		private void CreateCutoutImage()
		{
			this.mCutoutImage = new DeviceImage();
			this.mCutoutImage.mApp = GameApp.gApp;
			this.mCutoutImage.SetImageMode(true, true);
			this.mCutoutImage.AddImageFlags(16U);
			this.mCutoutImage.Create(this.mCutoutW, this.mCutoutH);
			Graphics graphics = new Graphics(this.mCutoutImage);
			graphics.Get3D().ClearColorBuffer(new Color(0, 0));
			float num = (float)ZumaTip.MAX_ALPHA;
			float num2 = num / (float)ZumaTip.NUM_LINES;
			int num3 = 0;
			while (num > 0f)
			{
				graphics.SetColor(0, 0, 0, (int)num);
				graphics.FillRect(num3, num3, this.mCutoutW - num3 * 2, 1);
				graphics.FillRect(num3, num3 + 1, 1, this.mCutoutH - 1 - num3 * 2);
				graphics.FillRect(num3 + 1, this.mCutoutH - 1 - num3, this.mCutoutW - 1 - num3 * 2, 1);
				graphics.FillRect(this.mCutoutW - 1 - num3, num3 + 1, 1, this.mCutoutH - 2 - num3 * 2);
				num -= num2;
				num3++;
			}
			graphics.ClearRenderContext();
		}

		public static readonly int MAX_ALPHA = 128;

		private static readonly int NUM_LINES = 10;

		protected List<MaskedRect> mMaskedRects = new List<MaskedRect>();

		protected MemoryImage mCutoutImage;

		protected Image mMaskImage;

		protected string mText = "";

		protected Rect mBoxRect = default(Rect);

		protected float mArrowAngle;

		protected int mArrowX;

		protected int mArrowY;

		protected int mTextHeight;

		protected int mWidth;

		protected int mHeight;

		protected int mCutoutX;

		protected int mCutoutY;

		protected int mCutoutW;

		protected int mCutoutH;

		protected float mArrowAlpha;

		protected int mArrowAlphaDir = 1;

		protected float mArrowYOff;

		protected int mArrowYOffDir = 1;

		public bool mDoArrowAnim;

		public bool mBlockUpdates = true;

		public bool mClickDismiss = true;

		public bool mDrawArrow = true;

		public int mId;

		public int mUpdateCount;

		public int mAppearDelay;

		public enum Dir
		{
			Left,
			Right,
			Up,
			Down
		}
	}
}
