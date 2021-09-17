using System;
using System.Collections.Generic;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace BejeweledLivePlus.Widget
{
	public class Bej3SlideSelector : ScrollWidget, ScrollWidgetListener, Bej3ButtonListener, ButtonListener
	{
		private void DrawImagePart(Graphics g, Image img, Rect dest, Rect src)
		{
			for (int i = dest.mY; i < dest.mY + dest.mHeight; i += src.mHeight)
			{
				g.DrawImage(img, new Rect(dest.mX, i, src.mHeight, dest.mWidth), src);
			}
		}

		public Bej3SlideSelector(int theId, Bej3SlideSelectorListener theListener, Bej3ButtonListener theButtonListener, int itemSize, int containerWidth)
		{
			base.Init(this);
			this.mId = theId;
			this.mSlideListener = theListener;
			this.mButtonListener = theButtonListener;
			this.mItemSize = itemSize;
			this.mCachedItemPos = -999f;
			this.mSelectedItem = null;
			this.mContainerWidth = containerWidth;
			this.mItemBaseId = -1;
			this.mItemScale = 1f;
			this.mForceCenteringItem = -1;
			this.mLocked = false;
			this.mDownPos = new Point(-1, -1);
			this.mThreshold = new Point(0, 0);
			this.mContainer = new Bej3SlideSelectorContainer();
			this.AddWidget(this.mContainer);
			base.EnableBounce(true);
		}

		public override void Update()
		{
			base.Update();
			if (this.mVisible && !this.mDisabled)
			{
				int mX = this.mContainer.mX;
				float num = -99999f;
				Bej3Button bej3Button = null;
				bool flag = this.mCachedItemPos != this.mScrollOffset.mX;
				if (flag)
				{
					for (int i = 0; i < this.mItems.Count; i++)
					{
						Bej3Button bej3Button2 = this.mItems[i];
						if (bej3Button2.mButtonImage == null)
						{
							return;
						}
						int num2 = mX + this.GetItemXPos(bej3Button2) + bej3Button2.mButtonImage.mWidth / 2;
						float num3 = 1f - (float)(this.mWidth / 2 - num2) / (float)this.mWidth;
						bool flag2 = num3 <= 1f;
						if (num3 > 1f)
						{
							num3 = 1f - (num3 - 1f);
						}
						if (num3 < 0f)
						{
							num3 = 0f;
						}
						if (num3 > 0.96f)
						{
							num3 = 1f;
						}
						num2 = this.GetItemXPos(bej3Button2);
						if (flag2)
						{
							num2 += (int)((float)this.mItemSize * (1f - num3) * ConstantsWP.BEJ3SLIDESELECTOR_ITEM_MULT * (1f - num3));
						}
						else
						{
							num2 -= (int)((float)this.mItemSize * (1f - num3) * ConstantsWP.BEJ3SLIDESELECTOR_ITEM_MULT * (1f - num3));
						}
						bej3Button2.Resize(num2, this.mHeight / 2 - bej3Button2.mHeight / 2, 0, 0);
						if (num3 > num)
						{
							bej3Button = bej3Button2;
							num = num3;
						}
						bej3Button2.mAlpha = num3 * num3 * num3;
						bej3Button2.mZenSize = this.mItemScale;
					}
					if (bej3Button != this.mSelectedItem && this.mForceCenteringItem == -1 && this.CenterOnItem(bej3Button.mId))
					{
						this.mSelectedItem = bej3Button;
						this.mSlideListener.SlideSelectorChanged(this.mId, this.mSelectedItem.mId);
					}
					if (num == 1f)
					{
						this.mCachedItemPos = this.mScrollOffset.mX;
						return;
					}
				}
				else
				{
					this.mForceCenteringItem = -1;
				}
			}
		}

		public override void Draw(Graphics g)
		{
			g.PushState();
			this.DrawUnSelectedButtons(g);
			g.PopState();
			g.SetColorizeImages(true);
			g.SetColor(Color.White);
			this.DrawButton(g, this.mSelectedItem);
			g.ClearClipRect();
			base.DeferOverlay(0);
			this.mLastAlpha = g.GetFinalColor().mAlpha;
			this.mLastDrawPos = (int)g.mTransY;
		}

		public override void DrawOverlay(Graphics g)
		{
			g.Translate(this.GetAbsPos().mX, this.mLastDrawPos);
			if (this.mLastAlpha >= 200)
			{
				g.SetColorizeImages(true);
				g.SetColor(new Color(255, 255, 255, this.mLastAlpha));
			}
		}

		public void DrawButton(Graphics g, Bej3Button btn)
		{
			if (btn != null)
			{
				g.SetScale(btn.mZenSize, btn.mZenSize, (float)(this.mWidth / 2), (float)(this.mHeight / 2));
				g.DrawImage(btn.mButtonImage, btn.GetAbsPos().mX + (btn.mWidth - btn.mButtonImage.mWidth) / 2, (btn.mHeight - btn.mButtonImage.mHeight) / 2);
			}
		}

		public void DrawUnSelectedButtons(Graphics g)
		{
			g.SetColorizeImages(false);
			Graphics3D graphics3D = g.Get3D();
			float num = 0.75f;
			Bej3SlideSelector.Params[0] = 0.3f;
			Bej3SlideSelector.Params[1] = 0.59f;
			Bej3SlideSelector.Params[2] = 0.11f;
			Bej3SlideSelector.Params[3] = num;
			graphics3D.GetEffect(GlobalMembersResourcesWP.EFFECT_BADGE_GRAYSCALE);
			g.SetColorizeImages(true);
			g.SetColor(new Color(90, 80, 0, (int)(GlobalMembers.gApp.mDialogObscurePct * 255f)));
			for (int i = 0; i < this.mItems.Count; i++)
			{
				Bej3Button bej3Button = this.mItems[i];
				if (bej3Button != this.mSelectedItem)
				{
					this.DrawButton(g, bej3Button);
				}
			}
		}

		public virtual void ScrollTargetReached(ScrollWidget scrollWidget)
		{
		}

		public virtual void ScrollTargetInterrupted(ScrollWidget scrollWidget)
		{
		}

		public override void SetDisabled(bool isDisabled)
		{
			base.SetDisabled(isDisabled);
			foreach (Widget widget in this.mWidgets)
			{
				widget.SetDisabled(isDisabled);
			}
			for (int i = 0; i < this.mItems.Count; i++)
			{
				this.mItems[i].SetDisabled(isDisabled);
			}
		}

		public void AddItem(int itemId, int itemImageId)
		{
			Bej3Button bej3Button = new Bej3Button(itemId, this, Bej3ButtonType.BUTTON_TYPE_ZEN_SLIDE);
			bej3Button.SetImageId(itemImageId);
			bej3Button.mBtnNoDraw = true;
			this.mContainer.AddWidget(bej3Button);
			this.mItems.Add(bej3Button);
			if (this.mItemBaseId == -1)
			{
				this.mItemBaseId = itemId;
			}
		}

		public bool CenterOnItem(int itemId)
		{
			return this.CenterOnItem(itemId, false);
		}

		public bool CenterOnItem(int itemId, bool immediate)
		{
			if (immediate)
			{
				this.mForceCenteringItem = itemId;
				this.mCachedItemPos = -999f;
			}
			else
			{
				this.mForceCenteringItem = -1;
				this.mCachedItemPos = -999f;
			}
			bool flag = false;
			int i = 0;
			while (i < this.mItems.Count)
			{
				Bej3Button bej3Button = this.mItems[i];
				if (bej3Button.mId == itemId)
				{
					if (bej3Button.mButtonImage == null)
					{
						return false;
					}
					int num = bej3Button.mX + bej3Button.mButtonImage.mWidth / 2 - this.mWidth / 2;
					base.SetScrollOffset(new FPoint((float)(-(float)num), 0f), !this.mDisabled || !immediate);
					flag = true;
					if (immediate)
					{
						this.mSelectedItem = bej3Button;
						this.mSlideListener.SlideSelectorChanged(this.mId, this.mSelectedItem.mId);
						break;
					}
					break;
				}
				else
				{
					i++;
				}
			}
			if (!flag)
			{
				Bej3Button bej3Button2 = this.mItems[0];
				if (bej3Button2.mButtonImage == null)
				{
					return false;
				}
				int theX = bej3Button2.mX + bej3Button2.mButtonImage.mWidth / 2 - this.mWidth / 2;
				base.ScrollToPoint(new Point(theX, 0), !this.mDisabled || !immediate);
				this.mSelectedItem = bej3Button2;
				this.mSlideListener.SlideSelectorChanged(this.mId, this.mSelectedItem.mId);
			}
			return true;
		}

		public int GetItemXPos(Bej3Button item)
		{
			int num = ConstantsWP.ZENOPTIONS_AMBIENCE_ITEM_SIZE * 2;
			return num + (int)((float)((item.mId - this.mItemBaseId) * this.mItemSize) * this.mItemScale);
		}

		public override bool Intersects(WidgetContainer theWidget)
		{
			return base.Intersects(theWidget);
		}

		public override void Resize(int theX, int theY, int theWidth, int theHeight)
		{
			base.Resize(theX, theY, theWidth, theHeight);
			this.mContainer.Resize(0, 0, this.mContainerWidth, this.mHeight);
			base.SetScrollMode(ScrollWidget.ScrollMode.SCROLL_HORIZONTAL);
		}

		public override void TouchBegan(SexyAppBase.Touch touch)
		{
			this.mDownPos = new Point(touch.location.mX, touch.location.mY);
			base.TouchBegan(touch);
		}

		public override void TouchMoved(SexyAppBase.Touch touch)
		{
			Point point = default(Point);
			if (this.mDownPos.mX >= 0 && this.mDownPos.mY >= 0)
			{
				point = this.mDownPos - new Point(touch.location.mX, touch.location.mY);
			}
			if (Math.Abs(point.mX) > this.mThreshold.mX)
			{
				this.mLocked = true;
			}
			if (this.mLocked)
			{
				base.TouchMoved(touch);
			}
		}

		public override void TouchEnded(SexyAppBase.Touch touch)
		{
			this.mLocked = false;
			bool mScrollTracking = this.mScrollTracking;
			base.TouchEnded(touch);
			if (mScrollTracking && this.mSelectedItem != null)
			{
				this.CenterOnItem(this.mSelectedItem.mId);
			}
		}

		public void LinkUpAssets()
		{
			int mX = this.mContainer.mX;
			for (int i = 0; i < this.mItems.Count; i++)
			{
				Bej3Button bej3Button = this.mItems[i];
				bej3Button.LinkUpAssets();
				if (bej3Button.mButtonImage != null)
				{
					int num = mX + this.GetItemXPos(bej3Button) + bej3Button.mButtonImage.mWidth / 2;
					float num2 = 1f - (float)(this.mWidth / 2 - num) / (float)this.mWidth;
					bool flag = num2 <= 1f;
					if (num2 > 1f)
					{
						num2 = 1f - (num2 - 1f);
					}
					if (num2 < 0f)
					{
						num2 = 0f;
					}
					if (num2 > 0.96f)
					{
						num2 = 1f;
					}
					num = this.GetItemXPos(bej3Button);
					if (flag)
					{
						num += (int)((float)this.mItemSize * (1f - num2) * ConstantsWP.BEJ3SLIDESELECTOR_ITEM_MULT * (1f - num2));
					}
					else
					{
						num -= (int)((float)this.mItemSize * (1f - num2) * ConstantsWP.BEJ3SLIDESELECTOR_ITEM_MULT * (1f - num2));
					}
					bej3Button.Resize(num, this.mHeight / 2 - bej3Button.mHeight / 2, 0, 0);
					bej3Button.mAlpha = num2 * num2 * num2;
				}
			}
			this.mCachedItemPos = -999f;
		}

		public virtual void ButtonMouseEnter(int theId)
		{
		}

		public void ButtonDepress(int theId)
		{
			this.CenterOnItem(theId, true);
			this.mSlideListener.SlideSelectorChanged(this.mId, this.mSelectedItem.mId);
		}

		public void SetItemScale(float scale)
		{
			this.mItemScale = scale;
			this.LinkUpAssets();
		}

		public void SetThreshold(int x, int y)
		{
			this.mThreshold.mX = x;
			this.mThreshold.mY = y;
		}

		public int GetSelectedId()
		{
			if (this.mSelectedItem != null)
			{
				return this.mSelectedItem.mId;
			}
			return -1;
		}

		public void ButtonPress(int theId)
		{
		}

		public void ButtonMouseLeave(int theId)
		{
		}

		public bool ButtonsEnabled()
		{
			return true;
		}

		public void ButtonPress(int theId, int theClickCount)
		{
		}

		public void ButtonDownTick(int theId)
		{
		}

		public void ButtonMouseMove(int theId, int theX, int theY)
		{
			throw new NotImplementedException();
		}

		private Bej3SlideSelectorListener mSlideListener;

		private Bej3ButtonListener mButtonListener;

		private List<Bej3Button> mItems = new List<Bej3Button>();

		private int mItemSize;

		private float mItemScale;

		private float mCachedItemPos;

		private int mContainerWidth;

		private int mItemBaseId;

		private int mLastAlpha;

		private int mLastDrawPos;

		private int mForceCenteringItem;

		private Point mDownPos = default(Point);

		private Point mThreshold = default(Point);

		public int mId;

		public bool mLocked;

		public Bej3Button mSelectedItem;

		public Bej3Checkbox mEnabledCheckbox;

		public Bej3SlideSelectorContainer mContainer;

		public static float[] Params = new float[4];
	}
}
