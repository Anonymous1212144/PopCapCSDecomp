using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace BejeweledLivePlus.Misc
{
	public class Utils
	{
		public static void SetFontLayerColor(ImageFont theFont, string theName, Color theColor)
		{
			theFont.PopLayerColor(theName);
			theFont.PushLayerColor(theName, theColor);
		}

		public static Color GetFontLayerColor(ImageFont theFont, string theName)
		{
			for (int i = 0; i < theFont.mActiveLayerList.Length; i++)
			{
				ActiveFontLayer activeFontLayer = theFont.mActiveLayerList[i];
				if (string.Compare(activeFontLayer.mBaseFontLayer.mLayerName, theName) != 0)
				{
					return activeFontLayer.mBaseFontLayer.mColorMult.Clone();
				}
			}
			return new Color(0, 0, 0, 0);
		}

		public static void SetFontLayerColor(ImageFont theFont, int theLayer, Color theColor)
		{
			theFont.PopLayerColor(theLayer);
			LinkedList<FontLayer>.Enumerator enumerator = theFont.mFontData.mFontLayerList.GetEnumerator();
			int num = 0;
			while (enumerator.MoveNext())
			{
				FontLayer fontLayer = enumerator.Current;
				if (fontLayer.mLayerName.Length < 6 || !fontLayer.mLayerName.Contains("__MOD"))
				{
					if (num == theLayer)
					{
						fontLayer.mColorMult = theColor;
						if (!enumerator.MoveNext())
						{
							break;
						}
						FontLayer fontLayer2 = enumerator.Current;
						if (fontLayer2.mLayerName.StartsWith(fontLayer.mLayerName) && fontLayer2.mLayerName.EndsWith("__MOD") && fontLayer2.mLayerName.Length == fontLayer.mLayerName.Length + 5)
						{
							fontLayer2.mColorMult = theColor;
							return;
						}
						break;
					}
					else
					{
						num++;
					}
				}
			}
		}

		public static void SetFontLayerColorAdd(ImageFont theFont, int theLayer, Color theColor)
		{
			LinkedList<FontLayer>.Enumerator enumerator = theFont.mFontData.mFontLayerList.GetEnumerator();
			int num = 0;
			while (enumerator.MoveNext())
			{
				FontLayer fontLayer = enumerator.Current;
				if (fontLayer.mLayerName.Length < 6 || !fontLayer.mLayerName.Contains("__MOD"))
				{
					if (num == theLayer)
					{
						fontLayer.mColorMult = theColor.Clone();
						if (!enumerator.MoveNext())
						{
							break;
						}
						FontLayer fontLayer2 = enumerator.Current;
						if (fontLayer2.mLayerName.StartsWith(fontLayer.mLayerName) && fontLayer2.mLayerName.EndsWith("__MOD") && fontLayer2.mLayerName.Length == fontLayer.mLayerName.Length + 5)
						{
							fontLayer2.mColorMult = theColor.Clone();
							return;
						}
						break;
					}
					else
					{
						num++;
					}
				}
			}
		}

		public static Color GetFontLayerColor(ImageFont theFont, int theLayer)
		{
			LinkedList<FontLayer>.Enumerator enumerator = theFont.mFontData.mFontLayerList.GetEnumerator();
			int num = 0;
			while (enumerator.MoveNext())
			{
				FontLayer fontLayer = enumerator.Current;
				if (fontLayer.mLayerName.Length < 6 || !fontLayer.mLayerName.Contains("__MOD"))
				{
					if (num == theLayer)
					{
						return fontLayer.mColorMult.Clone();
					}
					num++;
				}
			}
			return new Color(0, 0, 0, 0);
		}

		public static float Round(float i_n)
		{
			return (float)((i_n >= 0f) ? ((int)(i_n + 0.501f)) : ((int)(i_n - 0.501f)));
		}

		public static int GetFontLayerCount(ImageFont theFont)
		{
			LinkedList<FontLayer>.Enumerator enumerator = theFont.mFontData.mFontLayerList.GetEnumerator();
			int num = 0;
			while (enumerator.MoveNext())
			{
				FontLayer fontLayer = enumerator.Current;
				if (fontLayer.mLayerName.Length < 6 || !fontLayer.mLayerName.Contains("__MOD"))
				{
					num++;
				}
			}
			return num;
		}

		public static void MyDrawImageRotatedSubdivide(Graphics g, Image theImage, Rect theSrcRect, float theX, float theY, double theRot, float theScaleX, float theScaleY, int theDivisions)
		{
			g.SetLinearBlend(true);
			float num = (float)theImage.mWidth * theScaleX;
			float num2 = (float)theImage.mHeight * theScaleY;
			theDivisions = Math.Min(theDivisions, 10);
			SexyVertex2D[,] array = new SexyVertex2D[theDivisions * theDivisions * 2, 3];
			for (int i = 0; i < theDivisions; i++)
			{
				for (int j = 0; j < theDivisions; j++)
				{
					float num3 = (float)j / (float)theDivisions;
					float num4 = (float)(j + 1) / (float)theDivisions;
					float num5 = (float)i / (float)theDivisions;
					float num6 = (float)(i + 1) / (float)theDivisions;
					int num7 = (i * theDivisions + j) * 2;
					array[num7, 0].color = g.mColor;
					array[num7, 0].x = (float)((double)theX + Math.Cos(theRot) * (double)(num3 - 0.5f) * (double)num + Math.Sin(theRot) * (double)(num5 - 0.5f) * (double)num2);
					array[num7, 0].y = (float)((double)theY + Math.Cos(theRot) * (double)(num5 - 0.5f) * (double)num2 - Math.Sin(theRot) * (double)(num3 - 0.5f) * (double)num);
					array[num7, 0].u = num3;
					array[num7, 0].v = num5;
					array[num7, 1].color = g.mColor;
					array[num7, 1].color = g.mColor;
					array[num7, 1].x = (float)((double)theX + Math.Cos(theRot) * (double)(num4 - 0.5f) * (double)num + Math.Sin(theRot) * (double)(num5 - 0.5f) * (double)num2);
					array[num7, 1].y = (float)((double)theY + Math.Cos(theRot) * (double)(num5 - 0.5f) * (double)num2 - Math.Sin(theRot) * (double)(num4 - 0.5f) * (double)num);
					array[num7, 1].u = num4;
					array[num7, 1].v = num5;
					array[num7, 2].color = g.mColor;
					array[num7, 2].color = g.mColor;
					array[num7, 2].x = (float)((double)theX + Math.Cos(theRot) * (double)(num3 - 0.5f) * (double)num + Math.Sin(theRot) * (double)(num6 - 0.5f) * (double)num2);
					array[num7, 2].y = (float)((double)theY + Math.Cos(theRot) * (double)(num6 - 0.5f) * (double)num2 - Math.Sin(theRot) * (double)(num3 - 0.5f) * (double)num);
					array[num7, 2].u = num3;
					array[num7, 2].v = num6;
					array[num7 + 1, 0].color = g.mColor;
					array[num7 + 1, 0].color = g.mColor;
					array[num7 + 1, 0].x = (float)((double)theX + Math.Cos(theRot) * (double)(num4 - 0.5f) * (double)num + Math.Sin(theRot) * (double)(num6 - 0.5f) * (double)num2);
					array[num7 + 1, 0].y = (float)((double)theY + Math.Cos(theRot) * (double)(num6 - 0.5f) * (double)num2 - Math.Sin(theRot) * (double)(num4 - 0.5f) * (double)num);
					array[num7 + 1, 0].u = num4;
					array[num7 + 1, 0].v = num6;
					array[num7 + 1, 1] = array[num7, 2];
					array[num7 + 1, 2] = array[num7, 1];
				}
			}
			g.DrawTrianglesTex(theImage, array, theDivisions * theDivisions * 2);
		}

		public static void MyDrawImageRotatedSubdivide(Graphics g, Image theImage, Rect theSrcRect, float theX, float theY, double theRot)
		{
			Utils.MyDrawImageRotatedSubdivide(g, theImage, theSrcRect, theX, theY, theRot, 1f, 1f, 1);
		}

		public static void MyDrawImageRotated(Graphics g, Image theImage, Rect theSrcRect, float theX, float theY, double theRot, float theScaleX, float theScaleY)
		{
			SexyTransform2D theMatrix = new SexyTransform2D(true);
			theMatrix.Scale(theScaleX, theScaleY);
			theMatrix.RotateRad((float)theRot);
			g.DrawImageMatrix(theImage, theMatrix, theSrcRect, theX, theY);
		}

		public static void MyDrawImageRotated(Graphics g, Image theImage, Rect theSrcRect, float theX, float theY, double theRot)
		{
			Utils.MyDrawImageRotated(g, theImage, theSrcRect, theX, theY, theRot, 1f, 1f);
		}

		public static void MyDrawImageRotated(Graphics g, Image theImage, float theX, float theY, double theRot, float theScaleX, float theScaleY)
		{
			Rect theSrcRect = new Rect(0, 0, theImage.mWidth, theImage.mHeight);
			Utils.MyDrawImageRotated(g, theImage, theSrcRect, theX, theY, theRot, theScaleX, theScaleY);
		}

		public static void MyDrawImageRotated(Graphics g, Image theImage, float theX, float theY, double theRot)
		{
			Utils.MyDrawImageRotated(g, theImage, theX, theY, theRot, 1f, 1f);
		}

		public static Color ColorLerp(Color theColor1, Color theColor2, float theT)
		{
			Color result = new Color(theColor1);
			result.mRed += (int)((float)(theColor2.mRed - theColor1.mRed) * theT);
			result.mGreen += (int)((float)(theColor2.mGreen - theColor1.mGreen) * theT);
			result.mBlue += (int)((float)(theColor2.mBlue - theColor1.mBlue) * theT);
			return result;
		}

		public static string GetEllipsisString(Graphics g, string theString, int theWidth)
		{
			if (g.StringWidth(theString) <= theWidth)
			{
				return theString;
			}
			StringBuilder stringBuilder = new StringBuilder(theString);
			stringBuilder.Append("...");
			int num = theString.Length - 1;
			while (stringBuilder.Length > 3 && g.StringWidth(stringBuilder.ToString()) > theWidth)
			{
				stringBuilder.Remove(num--, 1);
			}
			return stringBuilder.ToString();
		}

		public static void DrawImageCentered(Graphics g, Image theImage, float theCX, float theCY, float theScaleX, float theScaleY)
		{
			Utils.DrawImageCentered(g, theImage, theCX, theCY, 3, theScaleX, theScaleY);
		}

		public static void DrawImageCentered(Graphics g, Image theImage, float theX, float theY)
		{
			Utils.DrawImageCentered(g, theImage, theX, theY, 3, 1f, 1f);
		}

		public static void DrawImageCentered(Graphics g, Image theImage, float theX, float theY, int theJust, float theScaleX, float theScaleY)
		{
			if (theScaleX != 1f && theScaleY != 1f && (theJust & 3) != 0)
			{
				Transform transform = new Transform();
				transform.Scale(theScaleX, theScaleY);
				transform.Translate(theX, theY);
				GlobalMembers.gGR.DrawImageTransformF(g, theImage, transform, 0f, 0f);
				return;
			}
			float num = (float)theImage.mWidth * theScaleX;
			float num2 = (float)theImage.mHeight * theScaleY;
			if ((theJust & 1) != 0)
			{
				theX -= num / 2f;
			}
			else if ((theJust & 16) != 0)
			{
				theX -= num;
			}
			if ((theJust & 2) != 0)
			{
				theY -= num2 / 2f;
			}
			else if ((theJust & 4) != 0)
			{
				theY -= num2;
			}
			if (theScaleX != 1f || theScaleY != 1f)
			{
				GlobalMembers.gGR.DrawImage(g, theImage, (int)theX, (int)theY, (int)num, (int)num2);
				return;
			}
			if (((float)((int)theX) == theX && (float)((int)theY) == theY) || GlobalMembers.gIs3D)
			{
				GlobalMembers.gGR.DrawImage(g, theImage, (int)theX, (int)theY);
				return;
			}
			GlobalMembers.gGR.DrawImageF(g, theImage, theX, theY);
		}

		public static void PushScale(Graphics g, float theScaleX, float theScaleY, float theOrigX, float theOrigY)
		{
			if (GlobalMembers.gIs3D)
			{
				SexyTransform2D theTransform = default(SexyTransform2D);
				theTransform.LoadIdentity();
				theTransform.Translate(-(g.mTransX + theOrigX), -(g.mTransY + theOrigY));
				theTransform.Scale(theScaleX, theScaleY);
				theTransform.Translate(g.mTransX + theOrigX, g.mTransY + theOrigY);
				g.Get3D().PushTransform(theTransform, true);
				return;
			}
			g.PushState();
			g.SetScale(theScaleX, theScaleY, theOrigX, theOrigY);
		}

		public static void PopScale(Graphics g)
		{
			if (GlobalMembers.gIs3D)
			{
				g.Get3D().PopTransform();
				return;
			}
			g.PopState();
		}

		public static int WriteStringF(Graphics g, string theString, float theX, float theY, int theWidth, int theJustification, bool drawString, int theOffset, int theLength, int theOldColor)
		{
			if (GlobalMembers.gIs3D)
			{
				SexyTransform2D theTransform = new SexyTransform2D(true);
				theTransform.Translate(theX - (float)((int)theX), theY - (float)((int)theY));
				g.Get3D().PushTransform(theTransform);
			}
			int result = g.WriteString(theString, (int)theX, (int)theY, theWidth, theJustification, drawString, theOffset, theLength, theOldColor);
			if (GlobalMembers.gIs3D)
			{
				g.Get3D().PopTransform();
			}
			return result;
		}

		public static int WriteStringF(Graphics g, string theString, float theX, float theY)
		{
			return Utils.WriteStringF(g, theString, theX, theY, -1, 0, true, 0, -1, -1);
		}

		public static string GetTimeString(int aTime)
		{
			int num = aTime / 3600;
			int num2 = (aTime - num * 3600) / 60;
			int num3 = aTime - num * 3600 - num2 * 60;
			StringBuilder stringBuilder = new StringBuilder();
			if (num > 0)
			{
				if (num < 10)
				{
					stringBuilder.AppendFormat("0{0}:", new object[] { num });
				}
				else
				{
					stringBuilder.AppendFormat("{0}:", new object[] { num });
				}
			}
			if (num2 < 10)
			{
				stringBuilder.AppendFormat("0{0}:", new object[] { num2 });
			}
			else
			{
				stringBuilder.AppendFormat("{0}:", new object[] { num2 });
			}
			if (num3 < 10)
			{
				stringBuilder.AppendFormat("0{0}", new object[] { num3 });
			}
			else
			{
				stringBuilder.AppendFormat("{0}", new object[] { num3 });
			}
			return stringBuilder.ToString();
		}

		public static void SplitAndConvertStr(string theStr, List<string> outVals, char theSplitChar, bool theTrimLeadingWhitespace, int theMaxEntries)
		{
			if (theStr == string.Empty)
			{
				return;
			}
			string[] array = theStr.Split(new char[] { theSplitChar });
			outVals.Clear();
			int num = 0;
			while ((theMaxEntries < 0 || (theMaxEntries >= 0 && num < theMaxEntries)) && num < array.Length)
			{
				string text = array[num];
				if (theTrimLeadingWhitespace)
				{
					text.TrimStart(new char[] { ' ' });
				}
				outVals.Add(array[num]);
				num++;
			}
		}

		public static void SplitAndConvertStr(string theStr, List<string> outVals)
		{
			Utils.SplitAndConvertStr(theStr, outVals, ',', false, -1);
		}

		public static void SplitAndConvertStr(string theStr, List<int> outVals, char theSplitChar, bool theTrimLeadingWhitespace, int theMaxEntries)
		{
			if (theStr == string.Empty)
			{
				return;
			}
			string[] array = theStr.Split(new char[] { theSplitChar });
			outVals.Clear();
			int num = 0;
			while ((theMaxEntries < 0 || (theMaxEntries >= 0 && num < theMaxEntries)) && num < array.Length)
			{
				string text = array[num];
				if (theTrimLeadingWhitespace)
				{
					text.TrimStart(new char[] { ' ' });
				}
				int num2 = 0;
				int.TryParse(array[num], ref num2);
				outVals.Add(num2);
				num++;
			}
		}

		public static void SplitAndConvertStr(string theStr, List<int> outVals)
		{
			Utils.SplitAndConvertStr(theStr, outVals, ',', false, -1);
		}

		public static void SplitAndConvertStr(string theStr, List<float> outVals, char theSplitChar, bool theTrimLeadingWhitespace, int theMaxEntries)
		{
			if (theStr == string.Empty)
			{
				return;
			}
			string[] array = theStr.Split(new char[] { theSplitChar });
			outVals.Clear();
			int num = 0;
			while ((theMaxEntries < 0 || (theMaxEntries >= 0 && num < theMaxEntries)) && num < array.Length)
			{
				string text = array[num];
				if (theTrimLeadingWhitespace)
				{
					text.TrimStart(new char[] { ' ' });
				}
				float num2 = 0f;
				float.TryParse(array[num], 511, CultureInfo.InvariantCulture, ref num2);
				outVals.Add(num2);
				num++;
			}
		}

		public static void SplitAndConvertStr(string theStr, List<float> outVals)
		{
			Utils.SplitAndConvertStr(theStr, outVals, ',', false, -1);
		}

		public static bool Is2DaysInSameWeek(DateTime earlyDay, DateTime laterDay)
		{
			return (laterDay - earlyDay).Days <= 7 && earlyDay.DayOfWeek <= laterDay.DayOfWeek;
		}

		public enum JUST
		{
			JUST_CENTERX = 1,
			JUST_CENTERY,
			JUST_BOTTOM = 4,
			JUST_TOP = 8,
			JUST_RIGHT = 16,
			JUST_LEFT = 32
		}
	}
}
