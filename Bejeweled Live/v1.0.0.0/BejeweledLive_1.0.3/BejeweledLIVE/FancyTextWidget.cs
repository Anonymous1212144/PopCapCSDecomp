using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Sexy;

namespace BejeweledLIVE
{
	public class FancyTextWidget : InterfaceControl
	{
		public FancyTextWidget()
		{
			this.Clear();
		}

		public FancyTextWidget(string text, Font font)
			: this()
		{
			this.SetComposeFont(font);
			this.AddText(text);
		}

		public override void Dispose()
		{
		}

		public void Clear()
		{
			this.mComposeAlignment = FancyTextWidget.Alignment.LEFT;
			this.mComposeColor = (SexyColor)Color.Black;
			this.mComposeFont = null;
			this.mActiveColumn = 0;
			this.mColumnCount = 1;
			this.mTopOfSection = 0;
			this.mBottomOfSection = 0;
			this.mTopOfLine = 0;
			this.mTextBaseLine = 0;
			this.mLineWidth = 0;
			this.mFirstChunkOnLine = 0;
			this.mChunks.Clear();
		}

		public void SetComposeColumn(int activeColumn, int columnCount)
		{
			if (columnCount != this.mColumnCount)
			{
				this.mTopOfSection = this.mBottomOfSection;
			}
			else
			{
				this.mTopOfLine = this.mTopOfSection;
			}
			this.mTextBaseLine = this.mTopOfLine;
			this.mActiveColumn = activeColumn;
			this.mColumnCount = columnCount;
		}

		public void SetComposeAlignment(FancyTextWidget.Alignment alignment)
		{
			this.mComposeAlignment = alignment;
		}

		public void SetComposeColor(Color color)
		{
			this.mComposeColor.CopyFrom(color);
		}

		public void SetComposeFont(Font font)
		{
			this.mComposeFont = font;
		}

		public KeyValuePair<int, int> AddWrappedText(string text)
		{
			return this.AddWrappedText(text, 10);
		}

		public KeyValuePair<int, int> AddWrappedText(string text, int extraLineSpacing)
		{
			Color composeColor = this.mComposeColor;
			int num = this.mWidth / this.mColumnCount;
			int num2 = 0;
			int num3 = 0;
			int i = 0;
			int num4 = 0;
			int num5 = 0;
			char thePrevChar = ' ';
			int count = this.mChunks.Count;
			string text2;
			while (i < text.Length - 1)
			{
				char c = text.get_Chars(i++);
				if (text.get_Chars(i) == '^')
				{
					if (text.get_Chars(i) != c)
					{
						continue;
					}
					i++;
				}
				else if ('\n' == c)
				{
					if (i > num5)
					{
						text2 = text.Substring(0, i - num5 - 1);
						if (!string.IsNullOrEmpty(text2))
						{
							this.AddText(text2);
						}
						this.NewLine(extraLineSpacing);
					}
					num5 = i;
					num4 = i;
					num3 = 0;
				}
				else if (char.IsWhiteSpace(c))
				{
					num4 = i;
					num2 = num3;
				}
				num3 += this.mComposeFont.CharWidthKern(c, thePrevChar);
				thePrevChar = c;
				if (this.mLineWidth + num3 > num && num4 > num5)
				{
					text2 = text.Substring(num5, num4 - num5);
					string text3 = text2.Trim();
					this.AddText(text3);
					this.NewLine(extraLineSpacing);
					num5 = num4;
					num3 -= num2;
				}
			}
			text2 = text.Substring(num5, i - num5 + ((text.Length > 0) ? 1 : 0));
			this.AddText(text2);
			this.SetComposeColor(composeColor);
			return new KeyValuePair<int, int>(count, this.mChunks.Count + 1);
		}

		public int AddText(string text)
		{
			int count = this.mChunks.Count;
			FancyTextWidget.Chunk chunk = new FancyTextWidget.Chunk();
			chunk.image = null;
			chunk.font = this.mComposeFont;
			chunk.color = this.mComposeColor;
			chunk.text = text;
			chunk.frame.mWidth = chunk.font.StringWidth(chunk.text);
			chunk.frame.mHeight = chunk.font.GetHeight();
			this.mChunks.Add(chunk);
			this.mLineWidth += chunk.frame.mWidth;
			int num = this.mTopOfLine - this.mComposeFont.GetAscent();
			this.mTextBaseLine = Math.Max(this.mTextBaseLine, num);
			return count;
		}

		public int AddImage(Image image)
		{
			return this.AddImage(image, 1f);
		}

		public int AddImage(Image image, float scale)
		{
			int count = this.mChunks.Count;
			FancyTextWidget.Chunk chunk = new FancyTextWidget.Chunk();
			chunk.image = image;
			chunk.font = this.mComposeFont;
			chunk.color = this.mComposeColor;
			chunk.frame.mWidth = (int)((float)chunk.image.GetWidth() * scale);
			chunk.frame.mHeight = (int)((float)chunk.image.GetHeight() * scale);
			this.mChunks.Add(chunk);
			this.mLineWidth += chunk.frame.mWidth;
			int mHeight = chunk.frame.mHeight;
			this.mTextBaseLine = this.mTopOfLine;
			return count;
		}

		private static int CompareChunksByFont(FancyTextWidget.Chunk a, FancyTextWidget.Chunk b)
		{
			if (a == null && b == null)
			{
				return 0;
			}
			if (a == null)
			{
				return 1;
			}
			if (b == null)
			{
				return -1;
			}
			return Font.FontComparer(a.font, b.font);
		}

		public void Advance(int advance)
		{
			this.mTopOfLine += advance;
			this.mBottomOfSection = Math.Max(this.mBottomOfSection, this.mTopOfLine);
		}

		public void NewLine()
		{
			this.NewLine(0);
		}

		public void NewLine(int extraLineSpacing)
		{
			extraLineSpacing = (int)Constants.mConstants.S((float)extraLineSpacing);
			int num = this.mWidth / this.mColumnCount;
			int num2 = this.mActiveColumn * num;
			int num3 = 0;
			switch (this.mComposeAlignment)
			{
			case FancyTextWidget.Alignment.LEFT:
				num3 = num2;
				break;
			case FancyTextWidget.Alignment.CENTER:
				num3 = num2 + (num - this.mLineWidth) / 2;
				break;
			case FancyTextWidget.Alignment.RIGHT:
				num3 = num2 + num - this.mLineWidth;
				break;
			}
			int num4 = 0;
			for (int i = this.mFirstChunkOnLine; i < this.mChunks.size<FancyTextWidget.Chunk>(); i++)
			{
				FancyTextWidget.Chunk chunk = this.mChunks[i];
				int num5 = ((chunk.image != null) ? 0 : chunk.font.GetAscent());
				int num6 = ((chunk.image != null) ? ((int)((float)chunk.frame.mHeight * 1.3f)) : chunk.font.GetLineSpacing());
				chunk.frame.mX = num3;
				chunk.frame.mY = this.mTextBaseLine + num5;
				num3 += chunk.frame.mWidth;
				num4 = Math.Max(num4, num6);
			}
			if (num4 == 0)
			{
				num4 = this.mComposeFont.GetLineSpacing();
			}
			this.mFirstChunkOnLine = this.mChunks.size<FancyTextWidget.Chunk>();
			this.mTopOfLine += num4 + extraLineSpacing;
			this.mBottomOfSection = Math.Max(this.mBottomOfSection, this.mTopOfLine);
			this.mLineWidth = 0;
		}

		public bool ComposeFinish(FancyTextWidget.FinishOptions option)
		{
			switch (option)
			{
			case FancyTextWidget.FinishOptions.AUTO_HEIGHT:
				this.Resize(this.mX, this.mY, this.mWidth, this.mBottomOfSection);
				this.mChunks.Sort(new Comparison<FancyTextWidget.Chunk>(FancyTextWidget.CompareChunksByFont));
				return true;
			case FancyTextWidget.FinishOptions.VERTICAL_CENTER:
			{
				int num = (this.mHeight - this.mBottomOfSection) / 2;
				foreach (FancyTextWidget.Chunk chunk in this.mChunks)
				{
					FancyTextWidget.Chunk chunk2 = chunk;
					chunk2.frame.mY = chunk2.frame.mY + num;
				}
				break;
			}
			}
			this.mChunks.Sort(new Comparison<FancyTextWidget.Chunk>(FancyTextWidget.CompareChunksByFont));
			return this.mBottomOfSection <= this.mHeight;
		}

		public int GetChunkCount()
		{
			return this.mChunks.Count;
		}

		public TRect GetChunkFrame(int chunkIndex)
		{
			return this.mChunks[chunkIndex].frame;
		}

		public override void Draw(Graphics g)
		{
			if (this.ClippingRegion != null)
			{
				TRect value = this.ClippingRegion.Value;
				g.HardwareClipRect(new TRect(value.mX - this.mX, value.mY - this.mY, value.mWidth, value.mHeight));
			}
			bool flag = true;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			if (this.ClippingRegion != null)
			{
				num2 = this.ClippingRegion.Value.mY + this.ClippingRegion.Value.mHeight - this.mY;
				num3 = this.ClippingRegion.Value.mY - this.mY;
			}
			while (flag)
			{
				flag = false;
				foreach (FancyTextWidget.Chunk chunk in this.mChunks)
				{
					float num4 = 1f;
					if (this.ClippingRegion != null)
					{
						TRect value2 = this.ClippingRegion.Value;
						value2.mY -= this.mY;
						value2.mX -= this.mX;
						if (!value2.Intersects(chunk.frame))
						{
							continue;
						}
						if (this.FadingEnabled)
						{
							num4 = Math.Min(1f, 0.02f * (float)(chunk.frame.mY - num3));
							if (num4 == 1f)
							{
								num4 = Math.Min(1f, 0.02f * (float)(num2 - (chunk.frame.mY + chunk.frame.mHeight)));
							}
						}
					}
					SexyColor color = chunk.color;
					g.SetColor(new SexyColor(color.mRed, color.mGreen, color.mBlue, (int)((float)color.mAlpha * this.mOpacity * num4)));
					g.SetColorizeImages(true);
					if (chunk.image != null && num == 0)
					{
						g.SetColorizeImages(true);
						g.DrawImage(chunk.image, chunk.frame.mX, chunk.frame.mY, chunk.frame.mWidth, chunk.frame.mHeight);
					}
					else
					{
						g.SetFont(chunk.font);
						if (g.GetFont().LayerCount > num)
						{
							flag = true;
							g.DrawStringLayer(chunk.text, chunk.frame.mX, chunk.frame.mY + chunk.font.GetAscent(), num);
						}
					}
				}
				num++;
			}
			if (this.ClippingRegion != null)
			{
				g.EndHardwareClip();
			}
		}

		private const float FADE_START = 0.02f;

		protected List<FancyTextWidget.Chunk> mChunks = new List<FancyTextWidget.Chunk>();

		protected int mFirstChunkOnLine;

		protected FancyTextWidget.Alignment mComposeAlignment;

		protected SexyColor mComposeColor = default(SexyColor);

		protected Font mComposeFont;

		protected int mActiveColumn;

		protected int mColumnCount;

		protected int mTopOfSection;

		protected int mBottomOfSection;

		protected int mTopOfLine;

		protected int mTextBaseLine;

		protected int mLineWidth;

		public TRect? ClippingRegion;

		public bool FadingEnabled = true;

		protected class Chunk : IComparable<FancyTextWidget.Chunk>
		{
			int IComparable<FancyTextWidget.Chunk>.CompareTo(FancyTextWidget.Chunk other)
			{
				return this.text.CompareTo(other.text);
			}

			public TRect frame = default(TRect);

			public Font font;

			public SexyColor color = default(SexyColor);

			public string text = string.Empty;

			public Image image;
		}

		public enum Alignment
		{
			LEFT = -1,
			CENTER,
			RIGHT
		}

		public enum FinishOptions
		{
			AUTO_HEIGHT,
			PRESERVE_HEIGHT,
			VERTICAL_CENTER
		}
	}
}
