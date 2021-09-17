using System;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace JeffLib
{
	public class DialogEx : Dialog
	{
		public DialogEx(Image theComponentImage, Image theButtonComponentImage, int theId, bool isModal, string theDialogHeader, string theDialogLines, string theDialogFooter, int theButtonMode)
			: base(theComponentImage, theButtonComponentImage, theId, isModal, theDialogHeader, theDialogLines, theDialogFooter, theButtonMode)
		{
			this.mFlushPriority = -1;
			this.mDrawScale.SetConstant(1.0);
		}

		public virtual void PreDraw(Graphics g)
		{
			this.mWidgetManager.FlushDeferredOverlayWidgets(this.mFlushPriority);
			Graphics3D graphics3D = ((g != null) ? g.Get3D() : null);
			if (this.mDrawScale != 1.0 && graphics3D != null)
			{
				SexyTransform2D theTransform = new SexyTransform2D(false);
				theTransform.Translate(-g.mTransX - (float)(this.mWidth / 2), -g.mTransY - (float)(this.mHeight / 2));
				theTransform.Scale((float)this.mDrawScale, (float)this.mDrawScale);
				theTransform.Translate(g.mTransX + (float)(this.mWidth / 2), g.mTransY + (float)(this.mHeight / 2));
				graphics3D.PushTransform(theTransform);
			}
		}

		public override void DrawAll(ModalFlags theFlags, Graphics g)
		{
			this.PreDraw(g);
			base.DrawAll(theFlags, g);
			this.PostDraw(g);
		}

		public virtual void PostDraw(Graphics g)
		{
			Graphics3D graphics3D = ((g != null) ? g.Get3D() : null);
			if (this.mDrawScale != 1.0 && graphics3D != null)
			{
				graphics3D.PopTransform();
			}
		}

		public override void Update()
		{
			base.Update();
			if (!this.mDrawScale.HasBeenTriggered())
			{
				this.MarkDirty();
			}
			if (!this.mDrawScale.IncInVal() && this.mDrawScale == 0.0)
			{
				GlobalMembers.gSexyAppBase.KillDialog(this);
			}
		}

		public int mFlushPriority;

		public CurvedVal mDrawScale = new CurvedVal();
	}
}
