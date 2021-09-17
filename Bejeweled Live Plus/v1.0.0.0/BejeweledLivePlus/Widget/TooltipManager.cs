using System;
using System.Collections.Generic;
using BejeweledLivePlus.Misc;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace BejeweledLivePlus.Widget
{
	public class TooltipManager : Widget
	{
		public TooltipManager()
		{
			this.mTooltips = new List<Tooltip>();
			this.mMouseVisible = false;
			this.mClip = false;
			this.mHasAlpha = true;
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eTOOLTIP_ALPHA, this.mAlpha);
			this.mArrowOffset.SetConstant(-16.0);
		}

		public int GetNOfTooltips()
		{
			return this.mTooltips.size<Tooltip>();
		}

		public Tooltip GetTooltip(int index)
		{
			return this.mTooltips[index];
		}

		public void ClearTooltips()
		{
			this.mTooltips.Clear();
		}

		public void RequestTooltip(Widget theCallingWidget, string theHeaderText, string theBodyText, Point thePos, int theWidth, int theArrowDir, int theTimer, Font theFontTitle, Font theFont, int theHeightAdj, int theColor)
		{
			foreach (Widget widget in GlobalMembers.gApp.mDialogList)
			{
				if (widget != theCallingWidget && GlobalMembers.gApp.mWidgetManager.IsBelow(theCallingWidget, widget))
				{
					return;
				}
			}
			for (int i = 0; i < this.mTooltips.size<Tooltip>(); i++)
			{
				if (thePos == this.mTooltips[i].mRequestedPos && theBodyText == this.mTooltips[i].mBodyText)
				{
					this.mTooltips[i].mAppearing = true;
					return;
				}
			}
			Tooltip tooltip = new Tooltip();
			tooltip.mHeaderText = theHeaderText;
			tooltip.mBodyText = theBodyText;
			tooltip.mRequestedPos = thePos;
			tooltip.mArrowDir = theArrowDir;
			tooltip.mTimer = theTimer;
			tooltip.mAppearing = true;
			tooltip.mAlphaPct = 0f;
			tooltip.mWidth = theWidth;
			tooltip.mColor = new Color(theColor);
			tooltip.mFontTitle = ((theFontTitle != null) ? theFontTitle : GlobalMembersResources.FONT_SUBHEADER);
			tooltip.mFont = ((theFont != null) ? theFont : GlobalMembersResources.FONT_DIALOG);
			tooltip.mHeight = GlobalMembersResourcesWP.IMAGE_TOOLTIP.GetHeight() - ConstantsWP.TOOLTIP_HEIGHT_OFFSET;
			Graphics graphics = new Graphics();
			graphics.SetFont(tooltip.mFontTitle);
			int num = 0;
			string text = "";
			if (tooltip.mHeaderText != "")
			{
				text += tooltip.mHeaderText;
				num += base.GetWordWrappedHeight(graphics, tooltip.mWidth - GlobalMembers.MS(80), tooltip.mHeaderText, tooltip.mFontTitle.GetLineSpacing());
				if (tooltip.mBodyText != "")
				{
					text += "\n";
				}
			}
			graphics.SetFont(tooltip.mFont);
			if (tooltip.mBodyText != "")
			{
				if (num > 0)
				{
					text += tooltip.mBodyText;
				}
				num += base.GetWordWrappedHeight(graphics, tooltip.mWidth - GlobalMembers.MS(80), tooltip.mBodyText, tooltip.mFont.GetLineSpacing());
			}
			if (tooltip.mHeight < num + GlobalMembers.MS(75))
			{
				tooltip.mHeight = num + GlobalMembers.MS(75);
			}
			tooltip.mHeight += theHeightAdj;
			tooltip.mOffsetPos = new Point(tooltip.mRequestedPos);
			int num2 = GlobalMembers.MS(20);
			int num3 = GlobalMembers.MS(5);
			switch (tooltip.mArrowDir)
			{
			case 0:
				tooltip.mOffsetPos += new Point(-tooltip.mWidth / 2, num2);
				break;
			case 1:
				tooltip.mOffsetPos += new Point(-tooltip.mWidth / 2, -tooltip.mHeight - num2);
				break;
			case 2:
				tooltip.mOffsetPos += new Point(num3, -tooltip.mHeight / 2);
				break;
			case 3:
				tooltip.mOffsetPos += new Point(-tooltip.mWidth - num3, -tooltip.mHeight / 2);
				break;
			}
			this.mTooltips.Add(tooltip);
			this.mCurrentRequestedPos = new Point(thePos);
			GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_TOOLTIP);
		}

		public override void Update()
		{
			for (int i = 0; i < this.mTooltips.size<Tooltip>(); i++)
			{
				Tooltip tooltip = this.mTooltips[i];
				tooltip.mTimer--;
				if (tooltip.mAppearing)
				{
					tooltip.mAlphaPct = GlobalMembers.MAX(1f, tooltip.mAlphaPct + 0.1f);
				}
				else
				{
					tooltip.mAlphaPct -= 0.1f;
					if (tooltip.mAlphaPct <= 0f)
					{
						this.mTooltips.RemoveAt(i);
						i = GlobalMembers.MAX(0, i - 1);
					}
				}
				tooltip.mAppearing = tooltip.mTimer > 0;
			}
		}

		public override void Draw(Graphics g)
		{
			GlobalMembers.gApp.mWidgetManager.FlushDeferredOverlayWidgets(int.MaxValue);
			for (int i = 0; i < this.mTooltips.size<Tooltip>(); i++)
			{
				Tooltip tooltip = this.mTooltips[i];
				float num = (float)this.mAlpha.GetOutVal((double)tooltip.mAlphaPct);
				g.SetColorizeImages(true);
				g.SetColor(new Color(255, 255, 255, (int)num * 255));
				Bej3Widget.DrawImageBoxTileCenter(g, new Rect(tooltip.mOffsetPos.mX, tooltip.mOffsetPos.mY, tooltip.mWidth, tooltip.mHeight), GlobalMembersResourcesWP.IMAGE_TOOLTIP, false, 0, ConstantsWP.TOOLTIP_BOX_OFFSET_Y);
				int tooltip_ARROW_SIZE = ConstantsWP.TOOLTIP_ARROW_SIZE;
				int tooltip_ARROW_SIZE2 = ConstantsWP.TOOLTIP_ARROW_SIZE;
				Point impliedObject = new Point
				{
					mX = tooltip.mRequestedPos.mX,
					mY = tooltip.mRequestedPos.mY
				} + new Point(-tooltip_ARROW_SIZE / 2, -tooltip_ARROW_SIZE2 / 2);
				int tooltip_ARROW_DISTANCE = ConstantsWP.TOOLTIP_ARROW_DISTANCE;
				bool flag = false;
				Image theImage = new Image();
				switch (tooltip.mArrowDir)
				{
				case 0:
					theImage = GlobalMembersResourcesWP.IMAGE_TOOLTIP_ARROW_ARROW_UP;
					impliedObject += new Point(0, (int)(GlobalMembers.S(this.mArrowOffset) + (double)tooltip_ARROW_DISTANCE));
					break;
				case 1:
					theImage = GlobalMembersResourcesWP.IMAGE_TOOLTIP_ARROW_ARROW_DOWN;
					impliedObject += new Point(0, (int)(-GlobalMembers.S(this.mArrowOffset) - (double)tooltip_ARROW_DISTANCE + (double)ConstantsWP.TOOLTIP_ARROW_OFFSET_DOWN));
					break;
				case 2:
					theImage = GlobalMembersResourcesWP.IMAGE_TOOLTIP_ARROW_ARROW_LEFT;
					impliedObject += new Point((int)(GlobalMembers.S(this.mArrowOffset) + (double)tooltip_ARROW_DISTANCE + (double)ConstantsWP.TOOLTIP_ARROW_OFFSET_LEFT), 0);
					break;
				case 3:
					theImage = GlobalMembersResourcesWP.IMAGE_TOOLTIP_ARROW_ARROW_RIGHT;
					impliedObject += new Point((int)(-GlobalMembers.S(this.mArrowOffset) - (double)tooltip_ARROW_DISTANCE + (double)ConstantsWP.TOOLTIP_ARROW_OFFSET_RIGHT), 0);
					break;
				case 4:
					flag = true;
					break;
				}
				this.mArrowOffset;
				g.SetColor(new Color(255, 255, 255, (int)(num * num * 255f)));
				if (!flag)
				{
					g.DrawImage(theImage, impliedObject.mX, impliedObject.mY);
				}
				Rect theRect = new Rect(tooltip.mOffsetPos.mX + GlobalMembers.MS(40), tooltip.mOffsetPos.mY + GlobalMembers.MS(30), tooltip.mWidth - GlobalMembers.MS(80), tooltip.mHeight - GlobalMembers.MS(50));
				g.SetFont(tooltip.mFontTitle);
				Utils.SetFontLayerColor((ImageFont)tooltip.mFontTitle, 0, Color.White);
				Utils.SetFontLayerColor((ImageFont)tooltip.mFontTitle, 1, Bej3Widget.COLOR_TOOLTIP_FILL);
				int num2 = g.WriteWordWrapped(theRect, tooltip.mHeaderText, -1, 0);
				theRect.mY += num2;
				Utils.SetFontLayerColor((ImageFont)tooltip.mFont, 0, Bej3Widget.COLOR_DIALOG_WHITE);
				g.SetFont(tooltip.mFont);
				g.WriteWordWrapped(theRect, tooltip.mBodyText, -1, 0);
			}
			g.SetColor(Color.White);
		}

		public void ClearTooltipsWithAnimation()
		{
			for (int i = 0; i < this.mTooltips.size<Tooltip>(); i++)
			{
				this.mTooltips[i].mTimer = 0;
			}
			if (this.mTooltips.size<Tooltip>() > 0)
			{
				this.mTooltips.RemoveAt(this.mTooltips.size<Tooltip>() - 1);
			}
		}

		public bool HasToolTips()
		{
			return this.mTooltips.size<Tooltip>() > 0;
		}

		public Point mCurrentRequestedPos = default(Point);

		public int mCurrentTooltipIdx;

		public List<Tooltip> mTooltips = new List<Tooltip>();

		public CurvedVal mAlpha = new CurvedVal();

		public CurvedVal mArrowOffset = new CurvedVal();

		public enum TOOLTIP_ARROW
		{
			TOOLTIP_ARROW_UP,
			TOOLTIP_ARROW_DOWN,
			TOOLTIP_ARROW_LEFT,
			TOOLTIP_ARROW_RIGHT,
			TOOLTIP_NONE
		}
	}
}
