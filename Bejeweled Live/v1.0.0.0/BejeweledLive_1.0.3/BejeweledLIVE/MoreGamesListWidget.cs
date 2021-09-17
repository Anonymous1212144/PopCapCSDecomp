using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Sexy;

namespace BejeweledLIVE
{
	public class MoreGamesListWidget : Widget
	{
		public MoreGamesListWidget(GameApp theApp, int dialogId, DialogListener listener)
		{
			this.mApp = theApp;
			this.mDialogId = dialogId;
			this.mDialogListener = listener;
			this.mTapTargetIndex = -1;
			this.mHiliteIndex = -1;
		}

		public override void Dispose()
		{
			base.Dispose();
		}

		public void AddRow(Image image, string link)
		{
			MoreGamesListWidget.Row row = new MoreGamesListWidget.Row();
			row.image = image;
			row.link = link;
			row.frame.mX = 0;
			row.frame.mWidth = this.mTrayBg.GetWidth();
			row.frame.mHeight = this.mTrayBg.GetHeight();
			if (this.mRows.Count == 0)
			{
				row.frame.mY = 0;
			}
			else
			{
				MoreGamesListWidget.Row row2 = this.mRows[this.mRows.Count - 1];
				row.frame.mY = row2.frame.mY + row2.frame.mHeight;
			}
			this.mRows.Add(row);
			this.Resize(0, 0, this.mWidth, row.frame.mY + row.frame.mHeight);
		}

		public string GetLinkForRow(int index)
		{
			return this.mRows[index].link;
		}

		public override void Resize(TRect frame)
		{
			base.Resize(frame);
		}

		public override void Resize(int x, int y, int width, int height)
		{
			base.Resize(x, y, width, height);
			foreach (MoreGamesListWidget.Row row in this.mRows)
			{
				row.frame.mWidth = width;
			}
		}

		public override void Draw(Graphics g)
		{
			base.Draw(g);
			foreach (MoreGamesListWidget.Row row in this.mRows)
			{
				g.SetColorizeImages(false);
				g.DrawImage(this.mTrayBg, row.frame.mX, row.frame.mY);
				if (row.image != null)
				{
					int theX = row.frame.mX + (row.frame.mWidth - row.image.GetWidth()) / 2;
					int theY = row.frame.mY + (row.frame.mHeight - row.image.GetHeight()) / 2;
					g.DrawImage(row.image, theX, theY);
				}
				List<MoreGamesListWidget.Row>.Enumerator enumerator;
				if (this.mHiliteIndex == this.mRows.IndexOf(enumerator.Current) - this.mRows.IndexOf(this.mRows.GetEnumerator().Current))
				{
					g.SetColorizeImages(false);
				}
				else
				{
					g.SetColor(new Color(255, 255, 255, 127));
					g.SetColorizeImages(true);
				}
				g.DrawImage(this.mTrayOv, row.frame.mX, row.frame.mY);
			}
		}

		public override void TouchBegan(_Touch touch)
		{
			int theX = (int)touch.location.x;
			int theY = (int)touch.location.y;
			foreach (MoreGamesListWidget.Row row in this.mRows)
			{
				if (row.frame.Contains(theX, theY))
				{
					List<MoreGamesListWidget.Row>.Enumerator enumerator;
					this.mTapTargetIndex = this.mRows.IndexOf(enumerator.Current) - this.mRows.IndexOf(this.mRows.GetEnumerator().Current);
					this.mHiliteIndex = this.mTapTargetIndex;
				}
			}
		}

		public override void TouchMoved(_Touch touch)
		{
			int theX = (int)touch.location.x;
			int theY = (int)touch.location.y;
			if (this.mTapTargetIndex >= 0)
			{
				if (this.mRows[this.mTapTargetIndex].frame.Contains(theX, theY))
				{
					this.mHiliteIndex = this.mTapTargetIndex;
					return;
				}
				this.mHiliteIndex = -1;
			}
		}

		public override void TouchEnded(_Touch touch)
		{
			int theX = (int)touch.location.x;
			int theY = (int)touch.location.y;
			if (this.mTapTargetIndex >= 0 && this.mRows[this.mTapTargetIndex].frame.Contains(theX, theY))
			{
				this.mDialogListener.DialogButtonDepress(this.mDialogId, this.mTapTargetIndex);
			}
			this.mTapTargetIndex = -1;
			this.mHiliteIndex = -1;
		}

		public override void TouchesCanceled()
		{
			this.mTapTargetIndex = -1;
			this.mHiliteIndex = -1;
		}

		protected GameApp mApp;

		protected int mDialogId;

		protected DialogListener mDialogListener;

		protected Image mTrayBg;

		protected Image mTrayOv;

		protected List<MoreGamesListWidget.Row> mRows = new List<MoreGamesListWidget.Row>();

		protected int mTapTargetIndex;

		protected int mHiliteIndex;

		protected class Row
		{
			public TRect frame = default(TRect);

			public Image image;

			public string link;
		}
	}
}
