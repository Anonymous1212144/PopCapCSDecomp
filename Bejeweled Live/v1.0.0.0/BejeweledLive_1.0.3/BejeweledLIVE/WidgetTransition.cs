using System;
using Sexy;

namespace BejeweledLIVE
{
	public class WidgetTransition : Widget
	{
		public WidgetTransition()
		{
			this.mOutgoingWidget = null;
			this.mIncomingWidget = null;
			this.mOutgoingWidgetOffset = new TPoint(0, 0);
			this.mIncomingWidgetOffset = new TPoint(0, 0);
			this.mOutgoingCacheImage = null;
			this.mIncomingCacheImage = null;
			this.mDurationTicks = 0;
			this.mElapsedTicks = 0;
		}

		public override void Dispose()
		{
			this.mOutgoingCacheImage.Dispose();
			this.mIncomingCacheImage.Dispose();
			base.Dispose();
		}

		public void Swap(Widget outgoing, Widget incoming, WidgetTransitionType type, WidgetTransitionSubType subType, float startSeconds, float endSeconds)
		{
			this.Setup(outgoing, incoming, endSeconds);
			this.mType = type;
			this.mSubType = subType;
			this.mParent.PutInfront(this, this.mOutgoingWidget);
			int tick = SexyAppFrameworkConstants.ticksForSeconds(startSeconds);
			int tick2 = this.mDurationTicks / 2;
			int tick3 = this.mDurationTicks;
			int mWidth = this.mWidth;
			int mHeight = this.mHeight;
			switch (this.mType)
			{
			case WidgetTransitionType.WIDGET_FADE:
				this.mIncomingOpacity.SetKey(tick, 0f, false, true);
				this.mIncomingOpacity.SetKey(tick3, 1f, false, true);
				this.mOutgoingOpacity.SetKey(tick, 1f, false, false);
				this.mOutgoingPosition.SetKey(tick3, new TPoint(0, 0), true, true);
				this.mIncomingPosition.SetKey(tick, new TPoint(0, 0), true, true);
				return;
			case WidgetTransitionType.WIDGET_MOVE_IN:
			case WidgetTransitionType.WIDGET_PUSH:
			case WidgetTransitionType.WIDGET_REVEAL:
				this.mIncomingOpacity.SetKey(tick, 0f, false, true);
				this.mIncomingOpacity.SetKey(tick2, 1f, false, true);
				this.mIncomingOpacity.SetKey(tick3, 1f, false, true);
				this.mOutgoingOpacity.SetKey(tick, 1f, false, true);
				this.mOutgoingOpacity.SetKey(tick2, 1f, false, true);
				this.mOutgoingOpacity.SetKey(tick3, 0f, false, true);
				this.mOutgoingPosition.SetKey(tick, new TPoint(0, 0), true, true);
				switch (this.mSubType)
				{
				case WidgetTransitionSubType.WIDGET_FROM_RIGHT:
					if (this.mType != WidgetTransitionType.WIDGET_REVEAL)
					{
						this.mIncomingPosition.SetKey(tick, new TPoint(mWidth, 0), true, true);
					}
					if (this.mType != WidgetTransitionType.WIDGET_MOVE_IN)
					{
						this.mOutgoingPosition.SetKey(tick3, new TPoint(-mWidth, 0), true, true);
					}
					break;
				case WidgetTransitionSubType.WIDGET_FROM_LEFT:
					if (this.mType != WidgetTransitionType.WIDGET_REVEAL)
					{
						this.mIncomingPosition.SetKey(tick, new TPoint(-mWidth, 0), true, true);
					}
					if (this.mType != WidgetTransitionType.WIDGET_MOVE_IN)
					{
						this.mOutgoingPosition.SetKey(tick3, new TPoint(mWidth, 0), true, true);
					}
					break;
				case WidgetTransitionSubType.WIDGET_FROM_TOP:
					if (this.mType != WidgetTransitionType.WIDGET_REVEAL)
					{
						this.mIncomingPosition.SetKey(tick, new TPoint(0, -mHeight), true, true);
					}
					if (this.mType != WidgetTransitionType.WIDGET_MOVE_IN)
					{
						this.mOutgoingPosition.SetKey(tick3, new TPoint(0, mHeight), true, true);
					}
					break;
				case WidgetTransitionSubType.WIDGET_FROM_BOTTOM:
					if (this.mType != WidgetTransitionType.WIDGET_REVEAL)
					{
						this.mIncomingPosition.SetKey(tick, new TPoint(0, mHeight), true, true);
					}
					if (this.mType != WidgetTransitionType.WIDGET_MOVE_IN)
					{
						this.mOutgoingPosition.SetKey(tick3, new TPoint(0, -mHeight), true, true);
					}
					break;
				}
				this.mIncomingPosition.SetKey(tick3, new TPoint(0, 0), true, true);
				break;
			case WidgetTransitionType.WIDGET_FLIP:
				break;
			default:
				return;
			}
		}

		public void SlideIn(Widget incoming, WidgetTransitionSubType direction, float startSeconds, float endSeconds)
		{
			this.Setup(null, incoming, endSeconds);
			this.mParent.PutInfront(this, this.mIncomingWidget);
			TPoint theTPoint = new TPoint(this.mIncomingWidget.mX, this.mIncomingWidget.mY);
			TPoint theTPoint2 = this.OutPosForDirection(this.mIncomingWidget, direction);
			int tick = SexyAppFrameworkConstants.ticksForSeconds(startSeconds);
			int tick2 = this.mDurationTicks;
			this.mIncomingOpacity.Clear();
			this.mIncomingPosition.Clear();
			this.mIncomingPosition.SetKey(tick, new TPoint(theTPoint2), false, true);
			this.mIncomingPosition.SetKey(tick2, new TPoint(theTPoint), true, true);
		}

		public void FadeIn(Widget incoming, float startSeconds, float endSeconds)
		{
			this.Setup(null, incoming, endSeconds);
			this.mParent.PutInfront(this, this.mIncomingWidget);
			int tick = SexyAppFrameworkConstants.ticksForSeconds(startSeconds);
			int tick2 = this.mDurationTicks;
			this.mIncomingPosition.Clear();
			this.mIncomingOpacity.Clear();
			this.mIncomingOpacity.SetKey(tick, 0f, false, true);
			this.mIncomingOpacity.SetKey(tick2, 1f, false, true);
		}

		public void FadeOut(Widget outgoing, float startSeconds, float endSeconds)
		{
			this.Setup(outgoing, null, endSeconds);
			this.mParent.PutInfront(this, this.mOutgoingWidget);
			int tick = SexyAppFrameworkConstants.ticksForSeconds(startSeconds);
			int tick2 = this.mDurationTicks;
			this.mOutgoingPosition.Clear();
			this.mOutgoingOpacity.Clear();
			this.mOutgoingOpacity.SetKey(tick, 1f, false, true);
			this.mOutgoingOpacity.SetKey(tick2, 0f, false, true);
		}

		public bool IsTransitioning()
		{
			return this.mIncomingWidget != null;
		}

		public bool TransitioningWidget(Widget widget)
		{
			return widget == this.mOutgoingWidget;
		}

		public override void Update()
		{
			this.mElapsedTicks++;
			if (this.mElapsedTicks == this.mDurationTicks)
			{
				this.TransitionComplete();
				if (this.mIncomingWidget != null)
				{
					this.mIncomingWidget.SetDisabled(this.mDisabled);
				}
				if (this.mOutgoingWidget != null)
				{
					this.mOutgoingWidget.SetDisabled(this.mDisabled);
				}
				this.mIncomingWidget = (this.mOutgoingWidget = null);
			}
			if (this.mIncomingWidget == null && !this.mOutgoingPosition.Empty())
			{
				TPoint tpoint = this.mOutgoingPosition.Tick((float)this.mElapsedTicks);
				this.Move(tpoint.mX, tpoint.mY);
			}
			if (this.mOutgoingWidget == null && !this.mIncomingPosition.Empty())
			{
				TPoint tpoint2 = this.mIncomingPosition.Tick((float)this.mElapsedTicks);
				this.Move(tpoint2.mX, tpoint2.mY);
			}
		}

		public override void Draw(Graphics g)
		{
			float tick = (float)this.mElapsedTicks;
			if (this.mIncomingWidget == null)
			{
				if (!this.mOutgoingOpacity.Empty())
				{
					g.SetColor(new SexyColor(255, 255, 255, (int)(255f * this.mOutgoingOpacity.Tick(tick))));
					g.SetColorizeImages(true);
				}
				g.DrawImage(this.mOutgoingCacheImage, 0, 0);
				return;
			}
			if (this.mOutgoingWidget == null)
			{
				if (!this.mIncomingOpacity.Empty())
				{
					g.SetColor(new SexyColor(255, 255, 255, (int)(255f * this.mIncomingOpacity.Tick(tick))));
					g.SetColorizeImages(true);
				}
				GlobalStaticVars.g.DrawImage(this.mIncomingCacheImage, 0, 0);
				return;
			}
			if (WidgetTransitionType.WIDGET_FLIP == this.mType)
			{
				this.DrawFlip(g);
				return;
			}
			SexyColor aColor = new SexyColor(255, 255, 255);
			SexyColor aColor2 = new SexyColor(255, 255, 255);
			aColor.mAlpha = (int)(255f * this.mIncomingOpacity.Tick(tick));
			aColor2.mAlpha = (int)(255f * this.mOutgoingOpacity.Tick(tick));
			TPoint tpoint = this.mIncomingPosition.Tick(tick);
			TPoint tpoint2 = this.mOutgoingPosition.Tick(tick);
			g.SetColorizeImages(true);
			if (WidgetTransitionType.WIDGET_REVEAL == this.mType)
			{
				g.SetColor(new SexyColor(aColor));
				g.DrawImage(this.mIncomingCacheImage, tpoint.mX, tpoint.mY);
				g.SetColor(new SexyColor(aColor2));
				g.DrawImage(this.mOutgoingCacheImage, tpoint2.mX, tpoint2.mY);
				return;
			}
			g.SetColor(aColor2);
			g.DrawImage(this.mOutgoingCacheImage, tpoint2.mX, tpoint2.mY);
			g.SetColor(aColor);
			g.DrawImage(this.mIncomingCacheImage, tpoint.mX, tpoint.mY);
		}

		protected void Setup(Widget outgoing, Widget incoming, float duration)
		{
			this.mOutgoingWidget = outgoing;
			this.mIncomingWidget = incoming;
			this.mElapsedTicks = 0;
			this.mDurationTicks = SexyAppFrameworkConstants.ticksForSeconds(duration);
			if (this.mOutgoingWidget == null)
			{
				this.mIncomingWidgetOffset.mX = 0;
				this.mIncomingWidgetOffset.mY = 0;
				this.Resize(this.mIncomingWidget.mX, this.mIncomingWidget.mY, this.mIncomingWidget.mWidth, this.mIncomingWidget.mHeight);
			}
			else if (this.mIncomingWidget == null)
			{
				this.mOutgoingWidgetOffset.mX = 0;
				this.mOutgoingWidgetOffset.mY = 0;
				this.Resize(this.mOutgoingWidget.mX, this.mOutgoingWidget.mY, this.mOutgoingWidget.mWidth, this.mOutgoingWidget.mHeight);
			}
			else
			{
				int num = Math.Min(outgoing.Left(), incoming.Left());
				int num2 = Math.Max(outgoing.Right(), incoming.Right());
				int num3 = Math.Min(outgoing.Top(), incoming.Top());
				int num4 = Math.Max(outgoing.Bottom(), incoming.Bottom());
				this.mOutgoingWidgetOffset.mX = outgoing.mX - num;
				this.mOutgoingWidgetOffset.mY = outgoing.mY - num3;
				this.mIncomingWidgetOffset.mX = incoming.mX - num;
				this.mIncomingWidgetOffset.mY = incoming.mY - num3;
				this.Resize(num, num3, num2 - num, num4 - num3);
			}
			if (this.mOutgoingWidget != null)
			{
				this.CacheWidgetImage(this.mOutgoingWidget, new TPoint(this.mOutgoingWidgetOffset), ref this.mOutgoingCacheImage);
				this.mOutgoingWidget.SetVisible(false);
			}
			if (this.mIncomingWidget != null)
			{
				this.CacheWidgetImage(this.mIncomingWidget, new TPoint(this.mIncomingWidgetOffset), ref this.mIncomingCacheImage);
				this.mIncomingWidget.SetVisible(false);
			}
			this.SetVisible(true);
		}

		protected void TransitionComplete()
		{
			if (this.mIncomingWidget != null)
			{
				this.mIncomingWidget.SetVisible(true);
			}
			this.SetVisible(false);
		}

		protected void ReplaceWidget(Widget oldWidget, Widget newWidget)
		{
			WidgetContainer mParent = oldWidget.mParent;
			mParent.PutBehind(newWidget, oldWidget);
			newWidget.SetVisible(true);
			oldWidget.SetVisible(false);
		}

		protected void CacheWidgetImage(Widget widget, TPoint offset, ref MemoryImage image)
		{
			if (image != null)
			{
				if (image.GetWidth() < this.mWidth || image.GetHeight() < this.mHeight)
				{
					image.Dispose();
					image = new MemoryImage();
					image.Create(this.mWidth, this.mHeight, PixelFormat.kPixelFormat_RGBA8888);
				}
			}
			else
			{
				image = new MemoryImage();
				image.Create(this.mWidth, this.mHeight, PixelFormat.kPixelFormat_RGBA8888);
			}
			ModalFlags theFlags = default(ModalFlags);
			theFlags.mIsOver = true;
			theFlags.mOverFlags = 12;
			theFlags.mUnderFlags = 12;
			Graphics @new = Graphics.GetNew(image);
			@new.Translate(offset.mX, offset.mY);
			@new.Clear(new SexyColor(0, 0, 0, 0));
			@new.BeginFrame();
			widget.DrawAll(theFlags, @new);
			@new.EndFrame();
			@new.SetRenderTarget(null);
			@new.PrepareForReuse();
		}

		protected TPoint OutPosForDirection(Widget widget, WidgetTransitionSubType direction)
		{
			TPoint result = new TPoint(widget.mX, widget.mY);
			switch (direction)
			{
			case WidgetTransitionSubType.WIDGET_FROM_RIGHT:
				result.mX = widget.mParent.mWidth;
				break;
			case WidgetTransitionSubType.WIDGET_FROM_LEFT:
				result.mX = -widget.mWidth;
				break;
			case WidgetTransitionSubType.WIDGET_FROM_TOP:
				result.mY = -widget.mHeight;
				break;
			case WidgetTransitionSubType.WIDGET_FROM_BOTTOM:
				result.mY = widget.mParent.mHeight;
				break;
			}
			return result;
		}

		protected void DrawFlip(Graphics g)
		{
			if (this.mIncomingCacheImage == null || this.mOutgoingCacheImage == null)
			{
				return;
			}
			float num = (float)this.mElapsedTicks / (float)this.mDurationTicks;
			float num2 = 180f * num;
			bool flag = ((int)num2 + 90) / 180 % 2 > 0;
			Image image = (flag ? this.mIncomingCacheImage : this.mOutgoingCacheImage);
			if (this.mSubType == WidgetTransitionSubType.WIDGET_FROM_LEFT)
			{
				num2 = 180f - num2;
			}
			float num3 = GlobalMembersWidgetTransition.DegToRad(num2);
			float num4 = (float)Math.Cos((double)num3);
			float num5 = (float)Math.Sin((double)num3);
			float[] array = new float[17];
			float[] array2 = new float[17];
			for (int i = 0; i < 16; i++)
			{
				array2[i] = (float)(i * image.mHeight / 16);
				array[i] = (float)(i * image.mWidth / 16);
			}
			array[16] = (float)image.mWidth;
			array2[16] = (float)image.mHeight;
			float num6 = (float)(this.mWidth / image.mWidth);
			float num7 = (float)(this.mHeight / image.mHeight);
			float num8 = 800f;
			int num9 = this.mWidth / 2;
			int num10 = this.mHeight / 2;
			g.Translate(num9, num10);
			for (int j = 0; j < 16; j++)
			{
				for (int k = 0; k < 16; k++)
				{
					float num11 = (array[k] - (float)(image.mWidth / 2)) * num6;
					float y = (array2[j] - (float)(image.mHeight / 2)) * num7;
					float num12 = (array[k + 1] - (float)(image.mWidth / 2)) * num6;
					float y2 = (array2[j + 1] - (float)(image.mHeight / 2)) * num7;
					float u;
					float u2;
					if (flag)
					{
						u = 1f - array[k] / (float)image.mWidth;
						u2 = 1f - array[k + 1] / (float)image.mWidth;
					}
					else
					{
						u = array[k] / (float)image.mWidth;
						u2 = array[k + 1] / (float)image.mWidth;
					}
					float v = array2[j] / (float)image.mHeight;
					float v2 = array2[j + 1] / (float)image.mHeight;
					TriVertex v3 = GlobalMembersWidgetTransition.Project2D(num11 * num4, y, num8 - num11 * num5, num8, u, v);
					TriVertex v4 = GlobalMembersWidgetTransition.Project2D(num12 * num4, y, num8 - num12 * num5, num8, u2, v);
					TriVertex triVertex = GlobalMembersWidgetTransition.Project2D(num11 * num4, y2, num8 - num11 * num5, num8, u, v2);
					TriVertex v5 = GlobalMembersWidgetTransition.Project2D(num12 * num4, y2, num8 - num12 * num5, num8, u2, v2);
					g.DrawTriangleTex(image, v3, v4, triVertex);
					g.DrawTriangleTex(image, triVertex, v4, v5);
				}
			}
			g.Translate(-num9, -num10);
		}

		protected Widget mOutgoingWidget;

		protected Widget mIncomingWidget;

		protected TPoint mOutgoingWidgetOffset = default(TPoint);

		protected TPoint mIncomingWidgetOffset = default(TPoint);

		protected MemoryImage mOutgoingCacheImage;

		protected MemoryImage mIncomingCacheImage;

		protected KeyInterpolatorTPoint mOutgoingPosition = KeyInterpolatorTPoint.GetNewKeyInterpolatorTPoint();

		protected KeyInterpolatorTPoint mIncomingPosition = KeyInterpolatorTPoint.GetNewKeyInterpolatorTPoint();

		protected KeyInterpolatorFloat mOutgoingOpacity = KeyInterpolatorFloat.GetNewKeyInterpolatorFloat();

		protected KeyInterpolatorFloat mIncomingOpacity = KeyInterpolatorFloat.GetNewKeyInterpolatorFloat();

		protected int mDurationTicks;

		protected int mElapsedTicks;

		protected WidgetTransitionType mType;

		protected WidgetTransitionSubType mSubType;
	}
}
