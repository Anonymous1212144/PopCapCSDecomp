using System;
using BejeweledLivePlus.Widget;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace BejeweledLivePlus.UI
{
	internal class BadgeMenuContainer : Bej3Widget
	{
		private Point GetBadgePosition(int badgeId)
		{
			Point absoluteBadgePosition = this.GetAbsoluteBadgePosition(badgeId, false);
			int badgePage = BadgeMenuContainer.GetBadgePage(badgeId);
			absoluteBadgePosition.mX += ConstantsWP.PROFILEMENU_BADGE_SINGLE_SECTION_WIDTH * badgePage;
			return absoluteBadgePosition;
		}

		public BadgeMenuContainer(BadgeMenu menu)
			: base(Menu_Type.MENU_BADGEMENU, false, Bej3ButtonType.TOP_BUTTON_TYPE_NONE)
		{
			this.mMenu = menu;
			this.Resize(0, 0, ConstantsWP.BADGEMENU_CONTAINER_PAGE_WIDTH * 3, ConstantsWP.BADGEMENU_CONTAINER_HEIGHT);
			this.mDoesSlideInFromBottom = false;
		}

		public void RenderBadges(Graphics g, bool isGrayscale, int onlyPage)
		{
			int mAlpha = g.GetFinalColor().mAlpha;
			for (int i = 0; i < 20; i++)
			{
				int badgePage = BadgeMenuContainer.GetBadgePage(i);
				if (onlyPage == -1 || onlyPage == badgePage)
				{
					Point badgePosition = this.GetBadgePosition(i);
					int num = this.mMenu.mBadgeLevels[i];
					if ((num == 0 && isGrayscale) || (num > 0 && !isGrayscale))
					{
						Bej3Widget.DrawImageCentered(g, BadgeMenu.GetSmallBadgeImage(i), 0, badgePosition.mX, badgePosition.mY);
					}
					if (!isGrayscale)
					{
						if (num > 0 && Profile.IsEliteBadge(i))
						{
							Bej3Widget.DrawImageCentered(g, GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_RINGS, 5, badgePosition.mX, badgePosition.mY);
						}
						else
						{
							Bej3Widget.DrawImageCentered(g, GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_RINGS, num, badgePosition.mX, badgePosition.mY);
						}
					}
				}
			}
			g.SetColor(new Color(255, 255, 255, mAlpha));
		}

		public void RenderBadges(Graphics g, bool isGrayscale)
		{
			int num = -1;
			int mAlpha = g.GetFinalColor().mAlpha;
			for (int i = 0; i < 20; i++)
			{
				int badgePage = BadgeMenuContainer.GetBadgePage(i);
				if (num == -1 || num == badgePage)
				{
					Point badgePosition = this.GetBadgePosition(i);
					Badge badgeByIndex = BadgeManager.GetBadgeManagerInstance().GetBadgeByIndex(i);
					if (this.mMenu.mBadgeStatus[i])
					{
						Bej3Widget.DrawImageCentered(g, badgeByIndex.mSmallIcon, 0, badgePosition.mX, badgePosition.mY);
						Bej3Widget.DrawImageCentered(g, GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_RINGS, (int)badgeByIndex.mLevel, badgePosition.mX, badgePosition.mY);
					}
					else
					{
						Bej3Widget.DrawImageCentered(g, GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_RINGS, 0, badgePosition.mX, badgePosition.mY);
					}
				}
			}
			g.SetColor(new Color(255, 255, 255, mAlpha));
		}

		public void RenderGrayBadges(Graphics g)
		{
			float mScaleX = g.mScaleX;
			float mScaleY = g.mScaleY;
			for (int i = 0; i < 20; i++)
			{
				Point badgePosition = this.GetBadgePosition(i);
				Badge badgeByIndex = BadgeManager.GetBadgeManagerInstance().GetBadgeByIndex(i);
				g.SetScale(1.3f, 1.3f, (float)badgePosition.mX, (float)badgePosition.mY);
				if (!this.mMenu.mBadgeStatus[i])
				{
					Bej3Widget.DrawImageCentered(g, badgeByIndex.mSmallIconGray, 0, badgePosition.mX, badgePosition.mY);
					Bej3Widget.DrawImageCentered(g, GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_RINGS, 0, badgePosition.mX, badgePosition.mY);
				}
			}
			g.mScaleX = mScaleX;
			g.mScaleY = mScaleY;
		}

		public void RenderNormalBadges(Graphics g)
		{
			g.SetColorizeImages(true);
			this.RenderBadges(g, false);
		}

		public Point GetAbsoluteBadgePosition(int badgeId, bool offsetY)
		{
			Point result = default(Point);
			int num = (GlobalMembers.gApp.mWidth - ConstantsWP.BADGE_MENU_BADGES_PADDING_X * 2) / (ConstantsWP.BADGE_MENU_BADGES_PER_ROW + 1);
			int num2 = ConstantsWP.BADGE_MENU_BADGES_PADDING_X + num;
			int num3 = badgeId % ConstantsWP.BADGES_FOR_PAGE;
			BadgeMenuContainer.GetBadgePage(badgeId);
			int num4;
			if (Profile.IsEliteBadge(badgeId))
			{
				num4 = num3 % ConstantsWP.BADGE_MENU_BADGES_PER_ROW;
				num4 = num2 - ConstantsWP.BADGE_MENU_ELITE_BADGE_PADDING + num4 * (num + ConstantsWP.BADGE_MENU_ELITE_BADGE_PADDING);
				result.mY = ConstantsWP.BADGE_MENU_ELITE_BADGES_POS_Y;
			}
			else
			{
				int num5;
				if (num3 <= 2)
				{
					if (num3 == 0)
					{
						num4 = ConstantsWP.PROFILEMENU_BADGE_SINGLE_SECTION_WIDTH / 2 - ConstantsWP.BADGE_MENU_BADGES_POS_1;
					}
					else if (num3 == 2)
					{
						num4 = ConstantsWP.PROFILEMENU_BADGE_SINGLE_SECTION_WIDTH / 2 + ConstantsWP.BADGE_MENU_BADGES_POS_1;
					}
					else
					{
						num4 = ConstantsWP.PROFILEMENU_BADGE_SINGLE_SECTION_WIDTH / 2;
					}
					num5 = 0;
				}
				else if (num3 <= 5)
				{
					if (num3 == 3)
					{
						num4 = ConstantsWP.PROFILEMENU_BADGE_SINGLE_SECTION_WIDTH / 2 - ConstantsWP.BADGE_MENU_BADGES_POS_2;
					}
					else if (num3 == 5)
					{
						num4 = ConstantsWP.PROFILEMENU_BADGE_SINGLE_SECTION_WIDTH / 2 + ConstantsWP.BADGE_MENU_BADGES_POS_2;
					}
					else
					{
						num4 = ConstantsWP.PROFILEMENU_BADGE_SINGLE_SECTION_WIDTH / 2;
					}
					num5 = 1;
				}
				else
				{
					if (num3 == 6)
					{
						num4 = ConstantsWP.PROFILEMENU_BADGE_SINGLE_SECTION_WIDTH / 2 - ConstantsWP.BADGE_MENU_BADGES_POS_1;
					}
					else if (num3 == 8)
					{
						num4 = ConstantsWP.PROFILEMENU_BADGE_SINGLE_SECTION_WIDTH / 2 + ConstantsWP.BADGE_MENU_BADGES_POS_1;
					}
					else
					{
						num4 = ConstantsWP.PROFILEMENU_BADGE_SINGLE_SECTION_WIDTH / 2;
					}
					num5 = 2;
				}
				result.mY = ConstantsWP.BADGE_MENU_BADGES_POS_Y + num5 * ConstantsWP.BADGE_MENU_BADGES_POS_Y_DELTA;
			}
			result.mX = num4;
			if (offsetY)
			{
				result.mY += this.GetAbsPos().mY;
			}
			return result;
		}

		public override void TouchEnded(SexyAppBase.Touch touch)
		{
			base.TouchEnded(touch);
			if (this.mMenu.ContainerTouchEnded(touch))
			{
				return;
			}
			Tooltip tooltip = null;
			if (GlobalMembers.gApp.mTooltipManager.GetNOfTooltips() > 0)
			{
				tooltip = GlobalMembers.gApp.mTooltipManager.GetTooltip(0);
			}
			if (tooltip != null)
			{
				int mX = tooltip.mOffsetPos.mX;
				int mY = tooltip.mOffsetPos.mY;
				int mWidth = tooltip.mWidth;
				int mHeight = tooltip.mHeight;
				int num = touch.location.mX + this.mMenu.mScrollWidget.mX - this.mMenu.mScrollWidget.mWidth * this.mMenu.mCurrentPage;
				int num2 = touch.location.mY + this.mMenu.mScrollWidget.mY;
				if (num > mX && num < mX + mWidth && num2 > mY && num2 < mY + mHeight)
				{
					GlobalMembers.gApp.mTooltipManager.ClearTooltipsWithAnimation();
					return;
				}
			}
			GlobalMembers.gApp.mTooltipManager.ClearTooltipsWithAnimation();
			int num3 = -1;
			for (int i = 0; i < 20; i++)
			{
				Point badgePosition = this.GetBadgePosition(i);
				if (Math.Abs(badgePosition.mX - touch.location.mX) < ConstantsWP.BADGE_MENU_TOOLTIP_TOLERANCE && Math.Abs(badgePosition.mY - touch.location.mY) < ConstantsWP.BADGE_MENU_TOOLTIP_TOLERANCE)
				{
					num3 = i;
					break;
				}
			}
			if (num3 == -1)
			{
				return;
			}
			Badge badge = this.mMenu.mBadgeManager.mBadgeClass[num3];
			Point badgePosition2 = this.GetBadgePosition(num3);
			badgePosition2.mY += this.GetAbsPos().mY;
			int badgePage = BadgeMenuContainer.GetBadgePage(num3);
			int num4 = badgePosition2.mX - badgePage * ConstantsWP.BADGEMENU_CONTAINER_PAGE_WIDTH;
			int theArrowDir;
			if (num4 < GlobalMembers.gApp.mWidth / 2)
			{
				theArrowDir = 2;
				badgePosition2.mX += ConstantsWP.BADGE_MENU_TOOLTIP_OFFSET;
			}
			else if (num4 > GlobalMembers.gApp.mWidth / 2)
			{
				theArrowDir = 3;
				badgePosition2.mX -= ConstantsWP.BADGE_MENU_TOOLTIP_OFFSET;
			}
			else if (badgePosition2.mY > this.mHeight / 2)
			{
				theArrowDir = 1;
				badgePosition2.mY -= ConstantsWP.BADGE_MENU_TOOLTIP_OFFSET;
			}
			else
			{
				theArrowDir = 0;
				badgePosition2.mY += ConstantsWP.BADGE_MENU_TOOLTIP_OFFSET;
			}
			int num5 = this.mWidth / 3;
			int num6 = ConstantsWP.BADGE_MENU_TOOLTIP_WIDTH_WIDE;
			if (badgePosition2.mX - num6 / 2 < badgePage * num5 || badgePosition2.mX + num6 / 2 > badgePage * num5 + num5)
			{
				int num7 = badgePosition2.mX - badgePage * num5;
				if (num7 > num5 / 2)
				{
					num7 = num5 - num7;
				}
				num6 = num7 * 2;
			}
			badgePosition2.mX -= badgePage * ConstantsWP.BADGEMENU_CONTAINER_PAGE_WIDTH;
			GlobalMembers.gApp.mTooltipManager.RequestTooltip(this, badge.GetTooltipHeader(), badge.GetTooltipBody() + string.Format(" ({0}G)", badge.mGPoints), badgePosition2, num6, theArrowDir, 500, null, null, 0, -1);
		}

		public override void TouchBegan(SexyAppBase.Touch touch)
		{
			base.TouchBegan(touch);
			GlobalMembers.gApp.mTooltipManager.ClearTooltipsWithAnimation();
		}

		public override void Draw(Graphics g)
		{
			this.RenderGrayBadges(g);
			this.RenderNormalBadges(g);
		}

		public override void Update()
		{
			base.Update();
		}

		public override void Show()
		{
			base.Show();
			this.mY = 0;
		}

		public static int GetBadgePage(int badgeId)
		{
			return badgeId / ConstantsWP.BADGES_FOR_PAGE;
		}

		public override void Hide()
		{
		}

		private BadgeMenu mMenu;

		private static float[] Params;
	}
}
