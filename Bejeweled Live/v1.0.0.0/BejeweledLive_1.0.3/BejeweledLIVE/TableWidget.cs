using System;
using System.Collections.Generic;
using Sexy;

namespace BejeweledLIVE
{
	public abstract class TableWidget : InterfaceControl
	{
		public TableWidget(int dialogId, DialogListener listener)
		{
			this.mDialogId = dialogId;
			this.mDialogListener = listener;
		}

		public override void Dispose()
		{
		}

		public void AddRow(int width, int height)
		{
			this.rowHeight = height;
			TRect trect = default(TRect);
			trect.mX = 0;
			if (this.mRows.Count == 0)
			{
				trect.mY = 0;
			}
			else
			{
				TRect trect2 = this.mRows[this.mRows.Count - 1];
				trect.mY = trect2.mY + trect2.mHeight;
			}
			trect.mWidth = width;
			trect.mHeight = height;
			this.mRows.Add(trect);
			this.Resize(0, 0, trect.mWidth, trect.mY + trect.mHeight);
		}

		public override void FadeIn(float startSeconds, float endSeconds)
		{
			base.FadeIn(startSeconds, endSeconds);
		}

		public override void FadeOut(float startSeconds, float endSeconds)
		{
			base.FadeOut(startSeconds, endSeconds);
		}

		public override void Draw(Graphics g)
		{
			base.Draw(g);
			if (this.mRows.Count > 0)
			{
				TRect trect = default(TRect);
				g.HardwareClip();
				int num = Math.Max(0, -this.mY / this.rowHeight);
				int num2 = Math.Min(this.mRows.Count, num + 2 + g.GetClipRect().mHeight / this.rowHeight);
				for (int i = num; i < num2; i++)
				{
					trect = this.mRows[i];
					g.Translate(trect.mX, trect.mY);
					this.DrawRow(i, g, i == this.mTouchHiliteIndex);
					g.Translate(-trect.mX, -trect.mY);
				}
				g.EndHardwareClip();
			}
		}

		public TRect GetRowRect(int rowIndex)
		{
			if (this.mRows.Count == 0)
			{
				return TRect.Empty;
			}
			return this.mRows[rowIndex];
		}

		protected abstract void DrawRow(int rowIndex, Graphics g, bool hilite);

		protected virtual void HiliteRow(int rowIndex)
		{
		}

		protected virtual void RowTapped(int rowIndex)
		{
			if (this.mDialogListener != null)
			{
				this.mDialogListener.DialogButtonDepress(this.mDialogId, rowIndex);
			}
		}

		public override void TouchBegan(_Touch touch)
		{
			int theX = (int)touch.location.x;
			int theY = (int)touch.location.y;
			for (int i = 0; i < this.mRows.Count; i++)
			{
				if (this.mRows[i].Contains(theX, theY))
				{
					this.mTouchTrackingIndex = i;
					this.mTouchHiliteIndex = this.mTouchTrackingIndex;
					this.HiliteRow(this.mTouchHiliteIndex);
					this.mTouchBeganIndex = this.mTouchHiliteIndex;
				}
			}
		}

		public override void TouchMoved(_Touch touch)
		{
			int theX = (int)touch.location.x;
			int theY = (int)touch.location.y;
			if (this.mTouchTrackingIndex >= 0)
			{
				int num = this.mTouchHiliteIndex;
				if (this.mRows.Count > this.mTouchTrackingIndex)
				{
					if (this.mRows[this.mTouchTrackingIndex].Contains(theX, theY))
					{
						this.mTouchHiliteIndex = this.mTouchTrackingIndex;
						if (this.mTouchBeganIndex != this.mTouchHiliteIndex)
						{
							this.mTouchBeganIndex = -1;
						}
					}
					else
					{
						this.mTouchHiliteIndex = -1;
						this.mTouchBeganIndex = -1;
					}
				}
				if (this.mTouchHiliteIndex != num)
				{
					this.HiliteRow(this.mTouchHiliteIndex);
				}
			}
		}

		public override void TouchEnded(_Touch touch)
		{
			int theX = (int)touch.location.x;
			int theY = (int)touch.location.y;
			if (this.mTouchTrackingIndex < this.mRows.Count && this.mTouchTrackingIndex >= 0 && this.mRows[this.mTouchTrackingIndex].Contains(theX, theY))
			{
				this.RowTapped(this.mTouchTrackingIndex);
			}
			if (this.mTouchBeganIndex == this.mTouchHiliteIndex)
			{
				this.RowClicked(this.mTouchBeganIndex);
			}
			this.mTouchTrackingIndex = -1;
			this.mTouchHiliteIndex = -1;
			this.HiliteRow(this.mTouchHiliteIndex);
		}

		protected virtual void RowClicked(int rowIndex)
		{
		}

		public override void TouchesCanceled()
		{
			this.mTouchTrackingIndex = -1;
			this.mTouchHiliteIndex = -1;
			this.mTouchBeganIndex = -1;
			this.HiliteRow(this.mTouchHiliteIndex);
		}

		protected int mDialogId;

		protected DialogListener mDialogListener;

		protected List<TRect> mRows = new List<TRect>();

		protected int mTouchTrackingIndex;

		protected int mTouchHiliteIndex;

		private int mTouchBeganIndex;

		protected int rowHeight;
	}
}
