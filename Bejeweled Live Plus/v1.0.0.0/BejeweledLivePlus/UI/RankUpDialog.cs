using System;
using BejeweledLivePlus.Misc;
using BejeweledLivePlus.Widget;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace BejeweledLivePlus.UI
{
	public class RankUpDialog : Bej3Dialog
	{
		public RankUpDialog(Board theBoard)
			: base(43, true, GlobalMembers._ID("RANK UP", 429), "", "", 3, Bej3ButtonType.BUTTON_TYPE_LONG, Bej3ButtonType.BUTTON_TYPE_LONG, Bej3ButtonType.TOP_BUTTON_TYPE_CLOSED)
		{
			this.mRankUpAnimPct.SetConstant(0.0);
			this.mBoard = theBoard;
			this.mContentInsets.mTop = GlobalMembers.MS(17);
			this.mRankBarWidget = null;
			this.Resize(0, ConstantsWP.MENU_Y_POS_HIDDEN, GlobalMembers.gApp.mWidth, ConstantsWP.RANKUPDIALOG_HEIGHT);
			this.mYesButton.mLabel = GlobalMembers._ID("CONTINUE", 3069);
			this.mYesButton.mX = ConstantsWP.RANKUPDIALOG_BUTTON_OK_X;
			this.mYesButton.mY = ConstantsWP.RANKUPDIALOG_BUTTON_OK_Y;
			((Bej3Button)this.mYesButton).SetType(Bej3ButtonType.BUTTON_TYPE_LONG_GREEN);
			this.mYesButton = null;
			this.mRankBarWidget = new RankBarWidget(ConstantsWP.RANKUPDIALOG_RANKBAR_WIDTH, this.mBoard, this, true);
			this.mRankBarWidget.Move((this.mWidth - ConstantsWP.RANKUPDIALOG_RANKBAR_WIDTH) / 2, ConstantsWP.RANKUPDIALOG_RANKBAR_Y);
			this.mRankBarWidget.mDrawCrown = true;
			this.mRankBarWidget.mDrawRankName = false;
			this.AddWidget(this.mRankBarWidget);
			this.mRankBarWidget.Shown(this.mBoard);
			this.mMessageLabel.SetTextBlock(new Rect(this.mWidth / 2 - ConstantsWP.RANKUPDIALOG_MSG_1_WIDTH / 2, ConstantsWP.RANKUPDIALOG_MSG_1_Y, ConstantsWP.RANKUPDIALOG_MSG_1_WIDTH, ConstantsWP.RANKUPDIALOG_MSG_1_HEIGHT), false);
			this.mMessageLabel2 = new Label(GlobalMembersResources.FONT_SUBHEADER, GlobalMembers._ID("You have been promoted to:", 431));
			this.mMessageLabel2.SetLayerColor(0, Bej3Widget.COLOR_SCORE);
			this.mMessageLabel2.SetLayerColor(1, Color.White);
			this.mMessageLabel2.Resize(this.mWidth / 2, ConstantsWP.RANKUPDIALOG_MSG_2_Y, 0, 0);
			this.AddWidget(this.mMessageLabel2);
			GlobalMembers.gApp.mProfile.UpdateRank(theBoard);
		}

		public override void Dispose()
		{
			this.RemoveAllWidgets(true, false);
			base.Dispose();
		}

		public override void Resize(int theX, int theY, int theWidth, int theHeight)
		{
			base.Resize(theX, theY, theWidth, theHeight);
			this.mHeadingLabel.Resize(this.mWidth / 2, ConstantsWP.DIALOGBOX_HEADING_LABEL_Y - 20, 0, 0);
			this.mY = ConstantsWP.MENU_Y_POS_HIDDEN;
		}

		public override void Draw(Graphics g)
		{
			base.Draw(g);
			g.SetColor(new Color(255, 255, 255, (int)(255f * this.mBoard.GetBoardAlpha())));
			g.SetColorizeImages(true);
			int rank = this.mRankBarWidget.GetRank();
			int num = this.mWidth / 2;
			int rankupdialog_MSG_5_Y = ConstantsWP.RANKUPDIALOG_MSG_5_Y;
			CurvedVal curvedVal = new CurvedVal();
			CurvedVal curvedVal2 = new CurvedVal();
			CurvedVal curvedVal3 = new CurvedVal();
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eRANK_UP_DIALOG_DRAW_TEXT_SCALE, curvedVal, this.mRankUpAnimPct);
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eRANK_UP_DIALOG_DRAW_GLOW_SCALE, curvedVal2, this.mRankUpAnimPct);
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eRANK_UP_DIALOG_DRAW_GLOW_ALPHA, curvedVal3, this.mRankUpAnimPct);
			g.SetFont(GlobalMembersResources.FONT_SUBHEADER);
			g.SetColor(Color.White);
			g.PushState();
			g.SetScale((float)curvedVal, (float)curvedVal, (float)num, (float)rankupdialog_MSG_5_Y - (float)ConstantsWP.RANKUPDIALOG_FONT_SCALE_OFFSET);
			Utils.SetFontLayerColor((ImageFont)GlobalMembersResources.FONT_SUBHEADER, 0, Bej3Widget.COLOR_SUBHEADING_1_STROKE);
			Utils.SetFontLayerColor((ImageFont)GlobalMembersResources.FONT_SUBHEADER, 1, Bej3Widget.COLOR_SUBHEADING_1_FILL);
			g.WriteString(this.mRankBarWidget.GetRankName(rank, false), num, rankupdialog_MSG_5_Y);
			g.PopState();
			float num2 = (float)curvedVal2;
			if (num2 > 0f)
			{
				g.PushState();
				g.SetScale(num2, num2, (float)num, (float)rankupdialog_MSG_5_Y - (float)ConstantsWP.RANKUPDIALOG_FONT_SCALE_OFFSET);
				g.SetDrawMode(Graphics.DrawMode.Additive);
				g.SetColor(new Color(255, 255, 255, (int)(255.0 * curvedVal3)));
				g.WriteString(this.mRankBarWidget.GetRankName(rank, false), num, rankupdialog_MSG_5_Y);
				g.PopState();
			}
		}

		public override void Update()
		{
			base.Update();
		}

		public override void ButtonDepress(int theId)
		{
			base.ButtonDepress(theId);
			this.Kill();
		}

		public void DoRankUp()
		{
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eRANK_UP_DIALOG_ANIM_PCT, this.mRankUpAnimPct);
		}

		public Board mBoard;

		public RankBarWidget mRankBarWidget;

		public CurvedVal mRankUpAnimPct = new CurvedVal();

		public Label mMessageLabel2;
	}
}
