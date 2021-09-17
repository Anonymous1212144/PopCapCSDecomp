using System;
using System.Collections.Generic;
using BejeweledLivePlus.Misc;
using BejeweledLivePlus.Widget;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace BejeweledLivePlus.UI
{
	public class HelpWindow : Widget
	{
		public HelpWindow()
		{
			this.mSeenByUser = false;
		}

		public override void Dispose()
		{
			this.mPopAnims.Clear();
			this.RemoveAllWidgets(true, false);
		}

		public override void Update()
		{
			base.Update();
			if (this.mSeenByUser)
			{
				for (int i = 0; i < this.mPopAnims.size<PopAnim>(); i++)
				{
					this.mPopAnims[i].Update();
				}
			}
			HelpWindow.mHelpAlpha.IncInVal();
			this.MarkDirty();
		}

		public override void Draw(Graphics g)
		{
			int num = (int)((float)this.mWidth / ((float)this.mPopAnims.size<PopAnim>() + GlobalMembers.M(0.1f)));
			int num2 = 0;
			for (int i = 0; i < this.mPopAnims.size<PopAnim>(); i++)
			{
				Rect rect = new Rect(0, 0, (int)((float)this.mPopAnims[i].mAnimRect.mWidth * ConstantsWP.HELPDIALOG_POPANIM_SCALE), (int)((float)this.mPopAnims[i].mAnimRect.mHeight * ConstantsWP.HELPDIALOG_POPANIM_SCALE));
				num2 = Math.Max(num2, rect.mHeight);
			}
			g.SetFont(GlobalMembersResources.FONT_DIALOG);
			Utils.SetFontLayerColor((ImageFont)GlobalMembersResources.FONT_DIALOG, 0, Bej3Widget.COLOR_DIALOG_WHITE);
			g.SetColor(HelpWindow.mHelpAlpha);
			int helpdialog_WINDOW_ANIMATION_Y_OFFSET = ConstantsWP.HELPDIALOG_WINDOW_ANIMATION_Y_OFFSET;
			for (int j = 0; j < this.mPopAnims.size<PopAnim>(); j++)
			{
				float helpdialog_POPANIM_SCALE = ConstantsWP.HELPDIALOG_POPANIM_SCALE;
				Rect rect2 = new Rect(0, 0, (int)((float)this.mPopAnims[j].mAnimRect.mWidth * helpdialog_POPANIM_SCALE), (int)((float)this.mPopAnims[j].mAnimRect.mHeight * helpdialog_POPANIM_SCALE));
				g.mClipRect.mY = g.mClipRect.mY + ConstantsWP.HELPDIALOG_WINDOW_CLIP_OFFSET_TOP;
				Transform transform = new Transform();
				Rect rect3 = rect2;
				rect3.Inflate(ConstantsWP.HELPDIALOG_WINDOW_BACKGROUND_EXTRA_SIZE, ConstantsWP.HELPDIALOG_WINDOW_BACKGROUND_EXTRA_SIZE);
				transform.Scale((float)rect3.mWidth / ConstantsWP.HELPDIALOG_WINDOW_BACKGROUND_SCALE, (float)rect3.mHeight / ConstantsWP.HELPDIALOG_WINDOW_BACKGROUND_SCALE);
				g.DrawImageTransformF(GlobalMembersResourcesWP.IMAGE_DIALOG_HELP_GLOW, transform, (float)num * ((float)j + GlobalMembers.M(0.56f)) + (float)this.mXOfs[j] + (float)ConstantsWP.HELPDIALOG_WINDOW_BACKGROUND_OFFSET, (float)(this.mHeight / 2 + helpdialog_WINDOW_ANIMATION_Y_OFFSET));
				g.PushState();
				PopAnim popAnim = this.mPopAnims[j];
				g.Translate((int)((float)num * ((float)j + GlobalMembers.M(0.56f)) - (float)GlobalMembers.S(rect2.mWidth / 2 + this.mXOfs[j])), this.mHeight / 2 - GlobalMembers.S(rect2.mHeight / 2) + helpdialog_WINDOW_ANIMATION_Y_OFFSET);
				Rect theRect = GlobalMembers.S(this.mPopAnims[j].mAnimRect);
				theRect.mX = (int)((float)theRect.mX * ConstantsWP.HELPDIALOG_POPANIM_CLIP_SCALE);
				theRect.mY = (int)((float)theRect.mY * ConstantsWP.HELPDIALOG_POPANIM_CLIP_SCALE);
				theRect.mWidth = (int)((float)theRect.mWidth * ConstantsWP.HELPDIALOG_POPANIM_CLIP_SCALE);
				theRect.mHeight = (int)((float)theRect.mHeight * ConstantsWP.HELPDIALOG_POPANIM_CLIP_SCALE);
				g.ClipRect(theRect);
				popAnim.mColor = HelpWindow.mHelpAlpha;
				popAnim.Draw(g);
				g.ClearClipRect();
				g.PopState();
				g.SetFont(GlobalMembersResources.FONT_DIALOG);
				g.SetColor(Color.White);
				int num3 = 0;
				int num4 = 0;
				int wordWrappedHeight = g.GetWordWrappedHeight(ConstantsWP.HELPDIALOG_TEXT_WIDTH, this.mCaptions[j], -1, ref num3, ref num4);
				Rect theRect2 = new Rect(ConstantsWP.HELPDIALOG_TEXT_X, ConstantsWP.HELPDIALOG_TEXT_Y - wordWrappedHeight / 2, ConstantsWP.HELPDIALOG_TEXT_WIDTH, ConstantsWP.HELPDIALOG_TEXT_HEIGHT);
				g.WriteWordWrapped(theRect2, this.mCaptions[j], -1, 0);
			}
		}

		public override void SetVisible(bool isVisible)
		{
			base.SetVisible(isVisible);
			this.ResetAnimation();
		}

		public void ResetAnimation()
		{
			this.ResetAnimation(false);
		}

		public void ResetAnimation(bool fullReset)
		{
			for (int i = 0; i < this.mPopAnims.size<PopAnim>(); i++)
			{
				this.mPopAnims[i].ResetAnim();
				this.mPopAnims[i].Play();
			}
		}

		public void PlayAnimation()
		{
			for (int i = 0; i < this.mPopAnims.size<PopAnim>(); i++)
			{
				this.mPopAnims[i].ResetAnim();
				this.mPopAnims[i].Play();
			}
		}

		public List<PopAnim> mPopAnims = new List<PopAnim>();

		public List<string> mCaptions = new List<string>();

		public List<int> mXOfs = new List<int>();

		public List<float> mTextWidthScale = new List<float>();

		public string mHeaderText;

		public bool mSeenByUser;

		public static CurvedVal mHelpAlpha = new CurvedVal();
	}
}
