using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Resource;
using SexyFramework.Widget;

namespace BejeweledLivePlus.Misc
{
	public class FrameAnimation : Widget
	{
		public FrameAnimation()
		{
		}

		public override void Update()
		{
			base.Update();
			if (this.mRectInAtlas == null)
			{
				return;
			}
			if (this.mRectInAtlas.Count <= 0)
			{
				return;
			}
			if (this.mUpdateCnt % 15 == 0)
			{
				this.mCurFrame++;
				this.mCurFrame %= this.mRectInAtlas.Count;
			}
		}

		public override void Draw(Graphics g)
		{
			if (this.mImage != null)
			{
				g.DrawImage(this.mImage, this.mRectDest, this.mRectInAtlas[this.mCurFrame]);
			}
		}

		public override void Resize(int theX, int theY, int theWidth, int theHeight)
		{
			base.Resize(theX, theY, theX + theWidth, theY + theHeight);
			this.mRectDest = new Rect(theX, theY, theWidth, theHeight);
		}

		public FrameAnimation(Image atlasImage, string atlasPlist)
		{
			XMLParser xmlparser = new XMLParser();
			try
			{
				Stream stream = TitleContainer.OpenStream("Content\\" + atlasPlist);
				byte[] array = new byte[stream.Length];
				stream.Read(array, 0, (int)stream.Length);
				stream.Close();
				xmlparser.checkEncodingType(array);
				xmlparser.SetBytes(array);
			}
			catch (Exception)
			{
				return;
			}
			this.mImage = atlasImage;
			this.mRectDest = new Rect(this.mX, this.mY, this.mWidth, this.mHeight);
			this.mRectInAtlas = new List<Rect>();
			bool flag = false;
			XMLElement xmlelement = new XMLElement();
			while (!xmlparser.HasFailed())
			{
				if (!xmlparser.NextElement(xmlelement))
				{
					return;
				}
				if (xmlelement.mType == XMLElement.XMLElementType.TYPE_ELEMENT)
				{
					string text = xmlelement.mValue.ToString();
					if (text == "frame")
					{
						flag = true;
					}
					else if (flag)
					{
						Rect rect = FrameAnimation.RectFromString(text);
						this.mRectInAtlas.Add(rect);
						flag = false;
					}
				}
			}
		}

		protected void Parse(string filename)
		{
		}

		protected static Rect RectFromString(string pszContent)
		{
			Rect zero_RECT = Rect.ZERO_RECT;
			if (pszContent != null)
			{
				int num = pszContent.IndexOf('{');
				int num2 = pszContent.IndexOf('}');
				int num3 = 1;
				while (num3 < 3 && num2 != -1)
				{
					num2 = pszContent.IndexOf('}', num2 + 1);
					num3++;
				}
				if (num != -1 && num2 != -1)
				{
					string text = pszContent.Substring(num + 1, num2 - num - 1);
					int num4 = text.IndexOf('}');
					if (num4 != -1)
					{
						num4 = text.IndexOf(',', num4);
						if (num4 != -1)
						{
							string pStr = text.Substring(0, num4);
							string pStr2 = text.Substring(num4 + 1);
							List<string> list = new List<string>();
							if (FrameAnimation.splitWithForm(pStr, ref list))
							{
								List<string> list2 = new List<string>();
								if (FrameAnimation.splitWithForm(pStr2, ref list2))
								{
									float num5 = (float)Convert.ToDouble(list[0]);
									float num6 = (float)Convert.ToDouble(list[1]);
									float num7 = (float)Convert.ToDouble(list2[0]);
									float num8 = (float)Convert.ToDouble(list2[1]);
									zero_RECT = new Rect((int)num5, (int)num6, (int)num7, (int)num8);
								}
							}
						}
					}
				}
			}
			return zero_RECT;
		}

		protected static bool splitWithForm(string pStr, ref List<string> strs)
		{
			bool result = false;
			if (pStr != null && pStr.Length != 0)
			{
				int num = pStr.IndexOf('{');
				int num2 = pStr.IndexOf('}');
				if (num != -1 && num2 != -1 && num <= num2)
				{
					string text = pStr.Substring(num + 1, num2 - num - 1);
					if (text.Length != 0)
					{
						int num3 = text.IndexOf('{');
						int num4 = text.IndexOf('}');
						if (num3 == -1 && num4 == -1)
						{
							FrameAnimation.split(text, ",", ref strs);
							if (strs.Count != 2 || strs[0].Length == 0 || strs[1].Length == 0)
							{
								strs.Clear();
							}
							else
							{
								result = true;
							}
						}
					}
				}
			}
			return result;
		}

		protected static void split(string src, string token, ref List<string> vect)
		{
			string[] array = src.Split(token.ToCharArray());
			foreach (string text in array)
			{
				vect.Add(text);
			}
		}

		protected Image mImage;

		protected List<Rect> mRectInAtlas;

		protected Rect mRectDest;

		protected int mCurFrame;
	}
}
