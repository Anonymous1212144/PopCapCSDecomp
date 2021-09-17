using System;
using System.Collections.Generic;
using SexyFramework.Misc;
using SexyFramework.Resource;

namespace SexyFramework.Graphics
{
	public class FontData : DescParser
	{
		public override bool Error(string theError)
		{
			return false;
		}

		public bool GetColorFromDataElement(DataElement theElement, ref Color theColor)
		{
			if (theElement.mIsList)
			{
				List<double> list = new List<double>();
				if (!base.DataToDoubleVector(theElement, ref list) && list.Count == 4)
				{
					return false;
				}
				theColor = new Color((int)(list[0] * 255.0), (int)(list[1] * 255.0), (int)(list[2] * 255.0), (int)(list[3] * 255.0));
				return true;
			}
			else
			{
				int theColor2 = 0;
				if (!Common.StringToInt(((SingleDataElement)theElement).mString.ToString(), ref theColor2))
				{
					return false;
				}
				theColor = new Color(theColor2);
				return true;
			}
		}

		public bool DataToLayer(DataElement theSource, ref FontLayer theFontLayer)
		{
			theFontLayer = null;
			if (theSource.mIsList)
			{
				return false;
			}
			string text = ((SingleDataElement)theSource).mString.ToString().ToUpper();
			if (!this.mFontLayerMap.ContainsKey(text))
			{
				return false;
			}
			theFontLayer = this.mFontLayerMap[text];
			return true;
		}

		public override bool HandleCommand(ListDataElement theParams)
		{
			string text = ((SingleDataElement)theParams.mElementVector[0]).mString.ToString();
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			if (text == "Define")
			{
				if (theParams.mElementVector.Count == 3)
				{
					if (!theParams.mElementVector[1].mIsList)
					{
						string text2 = ((SingleDataElement)theParams.mElementVector[1]).mString.ToString().ToUpper();
						if (!base.IsImmediate(text2))
						{
							if (this.mDefineMap.ContainsKey(text2))
							{
								this.mDefineMap.Remove(text2);
							}
							if (theParams.mElementVector[2].mIsList)
							{
								ListDataElement listDataElement = new ListDataElement();
								if (!base.GetValues((ListDataElement)theParams.mElementVector[2], listDataElement))
								{
									if (listDataElement != null)
									{
										listDataElement.Dispose();
									}
									return false;
								}
								this.mDefineMap.Add(text2, listDataElement);
							}
							else
							{
								SingleDataElement singleDataElement = (SingleDataElement)theParams.mElementVector[2];
								DataElement dataElement = this.Dereference(singleDataElement.mString.ToString());
								if (dataElement != null)
								{
									this.mDefineMap.Add(text2, dataElement.Duplicate());
								}
								else
								{
									this.mDefineMap.Add(text2, singleDataElement.Duplicate());
								}
							}
						}
						else
						{
							flag2 = true;
						}
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (text == "CreateHorzSpanRectList")
			{
				if (theParams.mElementVector.Count == 4)
				{
					List<int> list = new List<int>();
					List<int> list2 = new List<int>();
					if (!theParams.mElementVector[1].mIsList && base.DataToIntVector(theParams.mElementVector[2], ref list) && list.Count == 4 && base.DataToIntVector(theParams.mElementVector[3], ref list2))
					{
						string text3 = ((SingleDataElement)theParams.mElementVector[1]).mString.ToString().ToUpper();
						int num = 0;
						ListDataElement listDataElement2 = new ListDataElement();
						for (int i = 0; i < list2.Count; i++)
						{
							ListDataElement listDataElement3 = new ListDataElement();
							listDataElement2.mElementVector.Add(listDataElement3);
							string theString = (list[0] + num).ToString();
							listDataElement3.mElementVector.Add(new SingleDataElement(theString));
							theString = list[1].ToString();
							listDataElement3.mElementVector.Add(new SingleDataElement(theString));
							theString = list2[i].ToString();
							listDataElement3.mElementVector.Add(new SingleDataElement(theString));
							theString = list[3].ToString();
							listDataElement3.mElementVector.Add(new SingleDataElement(theString));
							num += list2[i];
						}
						if (this.mDefineMap.ContainsKey(text3))
						{
							this.mDefineMap.Remove(text3);
						}
						this.mDefineMap.Add(text3, listDataElement2);
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (text == "SetDefaultPointSize")
			{
				if (theParams.mElementVector.Count == 2)
				{
					int num2 = 0;
					if (!theParams.mElementVector[1].mIsList && Common.StringToInt(((SingleDataElement)theParams.mElementVector[1]).mString.ToString(), ref num2))
					{
						this.mDefaultPointSize = num2;
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (text == "SetCharMap")
			{
				if (theParams.mElementVector.Count == 3)
				{
					List<string> list3 = new List<string>();
					List<string> list4 = new List<string>();
					if (base.DataToStringVector(theParams.mElementVector[1], ref list3) && base.DataToStringVector(theParams.mElementVector[2], ref list4))
					{
						if (list3.Count == list4.Count)
						{
							for (int j = 0; j < list3.Count; j++)
							{
								if (list3[j].Length == 1 && list4[j].Length == 1)
								{
									this.mCharMap.Add(list3[j].get_Chars(0), list4[j].get_Chars(0));
								}
								else
								{
									flag2 = true;
								}
							}
						}
						else
						{
							flag4 = true;
						}
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (text == "CreateLayer")
			{
				if (theParams.mElementVector.Count == 2)
				{
					if (!theParams.mElementVector[1].mIsList)
					{
						string text4 = ((SingleDataElement)theParams.mElementVector[1]).mString.ToString().ToUpper();
						this.mFontLayerList.AddLast(new FontLayer(this));
						FontLayer value = this.mFontLayerList.Last.Value;
						value.mLayerName = text4;
						value.mBaseOrder = this.mFontLayerList.Count - 1;
						this.mFontLayerMap.Add(text4, value);
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (text == "CreateLayerFrom")
			{
				if (theParams.mElementVector.Count == 3)
				{
					FontLayer theFontLayer = new FontLayer();
					if (!theParams.mElementVector[1].mIsList && this.DataToLayer(theParams.mElementVector[2], ref theFontLayer))
					{
						string text5 = ((SingleDataElement)theParams.mElementVector[1]).mString.ToString().ToUpper();
						this.mFontLayerList.AddLast(new FontLayer(theFontLayer));
						FontLayer value2 = this.mFontLayerList.Last.Value;
						value2.mLayerName = text5;
						value2.mBaseOrder = this.mFontLayerList.Count - 1;
						this.mFontLayerMap.Add(text5, value2);
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (text == "LayerRequireTags")
			{
				if (theParams.mElementVector.Count == 3)
				{
					FontLayer fontLayer = null;
					List<string> list5 = new List<string>();
					if (this.DataToLayer(theParams.mElementVector[1], ref fontLayer) && base.DataToStringVector(theParams.mElementVector[2], ref list5))
					{
						for (int k = 0; k < list5.Count; k++)
						{
							fontLayer.mRequiredTags.Add(list5[k].ToUpper());
						}
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (text == "LayerExcludeTags")
			{
				if (theParams.mElementVector.Count == 3)
				{
					FontLayer fontLayer2 = null;
					List<string> list6 = new List<string>();
					if (this.DataToLayer(theParams.mElementVector[1], ref fontLayer2) && base.DataToStringVector(theParams.mElementVector[2], ref list6))
					{
						for (int l = 0; l < list6.Count; l++)
						{
							fontLayer2.mExcludedTags.Add(list6[l].ToUpper());
						}
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (text == "LayerPointRange")
			{
				if (theParams.mElementVector.Count == 4)
				{
					FontLayer fontLayer3 = null;
					if (this.DataToLayer(theParams.mElementVector[1], ref fontLayer3) && !theParams.mElementVector[2].mIsList && !theParams.mElementVector[3].mIsList)
					{
						int mMinPointSize = 0;
						int mMaxPointSize = 0;
						if (Common.StringToInt(((SingleDataElement)theParams.mElementVector[2]).mString.ToString(), ref mMinPointSize) && Common.StringToInt(((SingleDataElement)theParams.mElementVector[3]).mString.ToString(), ref mMaxPointSize))
						{
							fontLayer3.mMinPointSize = mMinPointSize;
							fontLayer3.mMaxPointSize = mMaxPointSize;
						}
						else
						{
							flag2 = true;
						}
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (text == "LayerSetPointSize")
			{
				if (theParams.mElementVector.Count == 3)
				{
					FontLayer fontLayer4 = null;
					if (this.DataToLayer(theParams.mElementVector[1], ref fontLayer4) && !theParams.mElementVector[2].mIsList)
					{
						int mPointSize = 0;
						if (Common.StringToInt(((SingleDataElement)theParams.mElementVector[2]).mString.ToString(), ref mPointSize))
						{
							fontLayer4.mPointSize = mPointSize;
						}
						else
						{
							flag2 = true;
						}
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (text == "LayerSetHeight")
			{
				if (theParams.mElementVector.Count == 3)
				{
					FontLayer fontLayer5 = null;
					if (this.DataToLayer(theParams.mElementVector[1], ref fontLayer5) && !theParams.mElementVector[2].mIsList)
					{
						int mHeight = 0;
						if (Common.StringToInt(((SingleDataElement)theParams.mElementVector[2]).mString.ToString(), ref mHeight))
						{
							fontLayer5.mHeight = mHeight;
						}
						else
						{
							flag2 = true;
						}
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (text == "LayerSetImage")
			{
				if (theParams.mElementVector.Count == 3)
				{
					FontLayer fontLayer6 = null;
					string theRelPath = "";
					if (this.DataToLayer(theParams.mElementVector[1], ref fontLayer6) && base.DataToString(theParams.mElementVector[2], ref theRelPath))
					{
						string pathFrom = Common.GetPathFrom(theRelPath, Common.GetFileDir(this.mSourceFile, false));
						bool mWriteToSexyCache = GlobalMembers.gSexyAppBase.mWriteToSexyCache;
						GlobalMembers.gSexyAppBase.mWriteToSexyCache = false;
						bool flag5 = false;
						bool flag6 = false;
						SharedImageRef sharedImageRef = new SharedImageRef();
						if (GlobalMembers.gSexyAppBase.mResourceManager != null && string.IsNullOrEmpty(this.mImagePathPrefix))
						{
							string idByPath = GlobalMembers.gSexyAppBase.mResourceManager.GetIdByPath(pathFrom);
							if (!string.IsNullOrEmpty(idByPath))
							{
								sharedImageRef = GlobalMembers.gSexyAppBase.mResourceManager.GetImage(idByPath);
								if (sharedImageRef.GetDeviceImage() == null)
								{
									sharedImageRef = GlobalMembers.gSexyAppBase.mResourceManager.LoadImage(idByPath);
								}
								if (sharedImageRef.GetDeviceImage() != null)
								{
									flag6 = true;
								}
							}
						}
						if (!flag6)
						{
							sharedImageRef = GlobalMembers.gSexyAppBase.GetSharedImage(this.mImagePathPrefix + pathFrom, "", ref flag5, false, false);
						}
						fontLayer6.mImageFileName = pathFrom;
						GlobalMembers.gSexyAppBase.mWriteToSexyCache = mWriteToSexyCache;
						if (sharedImageRef.GetImage() == null)
						{
							this.Error("Failed to Load Image");
							return false;
						}
						if (!flag5 && sharedImageRef.GetMemoryImage().mColorTable != null)
						{
							fontLayer6.mImageIsWhite = true;
							for (int m = 0; m < 256; m++)
							{
								if ((sharedImageRef.GetMemoryImage().mColorTable[m] & 16777215U) != 16777215U && sharedImageRef.GetMemoryImage().mColorTable[m] != 0U)
								{
									fontLayer6.mImageIsWhite = false;
									break;
								}
							}
						}
						fontLayer6.mImage = new SharedImageRef(sharedImageRef);
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (text.Equals("LayerSetDrawMode"))
			{
				if (theParams.mElementVector.Count == 3)
				{
					FontLayer fontLayer7 = new FontLayer();
					if (this.DataToLayer(theParams.mElementVector[1], ref fontLayer7) && !theParams.mElementVector[2].mIsList)
					{
						int num3 = 0;
						if (Common.StringToInt(((SingleDataElement)theParams.mElementVector[2]).mString.ToString(), ref num3) && num3 >= 0 && num3 <= 1)
						{
							fontLayer7.mDrawMode = num3;
						}
						else
						{
							flag2 = true;
						}
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (text.Equals("LayerSetColorMult"))
			{
				if (theParams.mElementVector.Count == 3)
				{
					FontLayer fontLayer8 = new FontLayer();
					if (this.DataToLayer(theParams.mElementVector[1], ref fontLayer8))
					{
						if (!this.GetColorFromDataElement(theParams.mElementVector[2], ref fontLayer8.mColorMult))
						{
							flag2 = true;
						}
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (text.Equals("LayerSetColorAdd"))
			{
				if (theParams.mElementVector.Count == 3)
				{
					FontLayer fontLayer9 = new FontLayer();
					if (this.DataToLayer(theParams.mElementVector[1], ref fontLayer9))
					{
						if (!this.GetColorFromDataElement(theParams.mElementVector[2], ref fontLayer9.mColorAdd))
						{
							flag2 = true;
						}
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (text.Equals("LayerSetAscent"))
			{
				if (theParams.mElementVector.Count == 3)
				{
					FontLayer fontLayer10 = new FontLayer();
					if (this.DataToLayer(theParams.mElementVector[1], ref fontLayer10) && !theParams.mElementVector[2].mIsList)
					{
						int mAscent = 0;
						if (Common.StringToInt(((SingleDataElement)theParams.mElementVector[2]).mString.ToString(), ref mAscent))
						{
							fontLayer10.mAscent = mAscent;
						}
						else
						{
							flag2 = true;
						}
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (text.Equals("LayerSetAscentPadding"))
			{
				if (theParams.mElementVector.Count == 3)
				{
					FontLayer fontLayer11 = new FontLayer();
					if (this.DataToLayer(theParams.mElementVector[1], ref fontLayer11) && !theParams.mElementVector[2].mIsList)
					{
						int mAscentPadding = 0;
						if (Common.StringToInt(((SingleDataElement)theParams.mElementVector[2]).mString.ToString(), ref mAscentPadding))
						{
							fontLayer11.mAscentPadding = mAscentPadding;
						}
						else
						{
							flag2 = true;
						}
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (text.Equals("LayerSetLineSpacingOffset"))
			{
				if (theParams.mElementVector.Count == 3)
				{
					FontLayer fontLayer12 = new FontLayer();
					if (this.DataToLayer(theParams.mElementVector[1], ref fontLayer12) && !theParams.mElementVector[2].mIsList)
					{
						int mLineSpacingOffset = 0;
						if (Common.StringToInt(((SingleDataElement)theParams.mElementVector[2]).mString.ToString(), ref mLineSpacingOffset))
						{
							fontLayer12.mLineSpacingOffset = mLineSpacingOffset;
						}
						else
						{
							flag2 = true;
						}
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (text.Equals("LayerSetOffset"))
			{
				if (theParams.mElementVector.Count == 3)
				{
					FontLayer fontLayer13 = new FontLayer();
					List<int> list7 = new List<int>();
					if (this.DataToLayer(theParams.mElementVector[1], ref fontLayer13) && base.DataToIntVector(theParams.mElementVector[2], ref list7) && list7.Count == 2)
					{
						fontLayer13.mOffset.mX = list7[0];
						fontLayer13.mOffset.mY = list7[1];
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (text.Equals("LayerSetCharWidths"))
			{
				if (theParams.mElementVector.Count == 4)
				{
					FontLayer fontLayer14 = new FontLayer();
					List<string> list8 = new List<string>();
					List<int> list9 = new List<int>();
					if (this.DataToLayer(theParams.mElementVector[1], ref fontLayer14) && base.DataToStringVector(theParams.mElementVector[2], ref list8) && base.DataToIntVector(theParams.mElementVector[3], ref list9))
					{
						if (list8.Count == list9.Count)
						{
							for (int n = 0; n < list8.Count; n++)
							{
								if (list8[n].Length == 1)
								{
									fontLayer14.GetCharData(list8[n].get_Chars(0)).mWidth = list9[n];
								}
								else
								{
									flag2 = true;
								}
							}
						}
						else
						{
							flag4 = true;
						}
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (text.Equals("LayerSetSpacing"))
			{
				if (theParams.mElementVector.Count == 3)
				{
					FontLayer fontLayer15 = new FontLayer();
					new List<int>();
					if (this.DataToLayer(theParams.mElementVector[1], ref fontLayer15) && !theParams.mElementVector[2].mIsList)
					{
						int mSpacing = 0;
						if (Common.StringToInt(((SingleDataElement)theParams.mElementVector[2]).mString.ToString(), ref mSpacing))
						{
							fontLayer15.mSpacing = mSpacing;
						}
						else
						{
							flag2 = true;
						}
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (text.Equals("LayerSetImageMap"))
			{
				if (theParams.mElementVector.Count == 4)
				{
					FontLayer fontLayer16 = new FontLayer();
					List<string> list10 = new List<string>();
					ListDataElement listDataElement4 = new ListDataElement();
					if (this.DataToLayer(theParams.mElementVector[1], ref fontLayer16) && base.DataToStringVector(theParams.mElementVector[2], ref list10) && base.DataToList(theParams.mElementVector[3], ref listDataElement4))
					{
						if (list10.Count == listDataElement4.mElementVector.Count)
						{
							if (fontLayer16.mImage.GetMemoryImage() == null)
							{
								this.Error("Layer image not set");
								return false;
							}
							int width = fontLayer16.mImage.GetMemoryImage().GetWidth();
							int height = fontLayer16.mImage.GetMemoryImage().GetHeight();
							for (int num4 = 0; num4 < list10.Count; num4++)
							{
								List<int> list11 = new List<int>();
								if (list10[num4].Length == 1 && base.DataToIntVector(listDataElement4.mElementVector[num4], ref list11) && list11.Count == 4)
								{
									Rect mImageRect = new Rect(list11[0], list11[1], list11[2], list11[3]);
									if (mImageRect.mWidth > 0 && (mImageRect.mX < 0 || mImageRect.mY < 0 || mImageRect.mX + mImageRect.mWidth > width || mImageRect.mY + mImageRect.mHeight > height))
									{
										this.Error("Image rectangle out of bounds");
										return false;
									}
									fontLayer16.GetCharData(list10[num4].get_Chars(0)).mImageRect = mImageRect;
								}
								else
								{
									flag2 = true;
								}
							}
							fontLayer16.mDefaultHeight = 0;
							int count = fontLayer16.mCharDataHashTable.mCharData.Count;
							CharData[] array = fontLayer16.mCharDataHashTable.mCharData.ToArray();
							for (int num5 = 0; num5 < count; num5++)
							{
								if (array[num5].mImageRect.mHeight + array[num5].mOffset.mY > fontLayer16.mDefaultHeight)
								{
									fontLayer16.mDefaultHeight = array[num5].mImageRect.mHeight + array[num5].mOffset.mY;
								}
							}
						}
						else
						{
							flag4 = true;
						}
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (text.Equals("LayerSetCharOffsets"))
			{
				if (theParams.mElementVector.Count == 4)
				{
					FontLayer fontLayer17 = new FontLayer();
					List<string> list12 = new List<string>();
					ListDataElement listDataElement5 = new ListDataElement();
					if (this.DataToLayer(theParams.mElementVector[1], ref fontLayer17) && base.DataToStringVector(theParams.mElementVector[2], ref list12) && base.DataToList(theParams.mElementVector[3], ref listDataElement5))
					{
						if (list12.Count == listDataElement5.mElementVector.Count)
						{
							for (int num6 = 0; num6 < list12.Count; num6++)
							{
								List<int> list13 = new List<int>();
								if (list12[num6].Length == 1 && base.DataToIntVector(listDataElement5.mElementVector[num6], ref list13) && list13.Count == 2)
								{
									fontLayer17.GetCharData(list12[num6].get_Chars(0)).mOffset = new Point(list13[0], list13[1]);
								}
								else
								{
									flag2 = true;
								}
							}
						}
						else
						{
							flag4 = true;
						}
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (text.Equals("LayerSetKerningPairs"))
			{
				if (theParams.mElementVector.Count == 4)
				{
					FontLayer fontLayer18 = new FontLayer();
					List<string> list14 = new List<string>();
					List<int> list15 = new List<int>();
					if (this.DataToLayer(theParams.mElementVector[1], ref fontLayer18) && base.DataToStringVector(theParams.mElementVector[2], ref list14) && base.DataToIntVector(theParams.mElementVector[3], ref list15))
					{
						if (list14.Count == list15.Count)
						{
							List<SortedKern> list16 = new List<SortedKern>();
							for (int num7 = 0; num7 < list14.Count; num7++)
							{
								if (list14[num7].Length == 2)
								{
									list16.Add(new SortedKern(list14[num7].get_Chars(0), list14[num7].get_Chars(1), list15[num7]));
								}
								else
								{
									flag2 = true;
								}
							}
							if (list16.Count != 0)
							{
								list16.Sort();
							}
							List<FontLayer.KerningValue> list17 = new List<FontLayer.KerningValue>();
							for (int num8 = 0; num8 < list16.Count; num8++)
							{
								SortedKern sortedKern = list16[num8];
								FontLayer.KerningValue kerningValue = default(FontLayer.KerningValue);
								kerningValue.mChar = (ushort)sortedKern.mValue;
								kerningValue.mOffset = (short)sortedKern.mOffset;
								kerningValue.mInt = ((int)kerningValue.mChar << 16) | (int)((ushort)kerningValue.mOffset);
								list17.Add(kerningValue);
								CharData charData = fontLayer18.GetCharData(sortedKern.mKey);
								if (charData.mKerningCount == 0)
								{
									charData.mKerningFirst = (ushort)num8;
								}
								CharData charData2 = charData;
								charData2.mKerningCount += 1;
							}
							fontLayer18.mKerningData = list17.ToArray();
						}
						else
						{
							flag4 = true;
						}
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (text.Equals("LayerSetBaseOrder"))
			{
				if (theParams.mElementVector.Count == 3)
				{
					FontLayer fontLayer19 = new FontLayer();
					if (this.DataToLayer(theParams.mElementVector[1], ref fontLayer19) && !theParams.mElementVector[2].mIsList)
					{
						int mBaseOrder = 0;
						if (Common.StringToInt(((SingleDataElement)theParams.mElementVector[2]).mString.ToString(), ref mBaseOrder))
						{
							fontLayer19.mBaseOrder = mBaseOrder;
						}
						else
						{
							flag2 = true;
						}
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (text.Equals("LayerSetCharOrders"))
			{
				if (theParams.mElementVector.Count == 4)
				{
					FontLayer fontLayer20 = new FontLayer();
					List<string> list18 = new List<string>();
					List<int> list19 = new List<int>();
					if (this.DataToLayer(theParams.mElementVector[1], ref fontLayer20) && base.DataToStringVector(theParams.mElementVector[2], ref list18) && base.DataToIntVector(theParams.mElementVector[3], ref list19))
					{
						if (list18.Count == list19.Count)
						{
							for (int num9 = 0; num9 < list18.Count; num9++)
							{
								if (list18[num9].Length == 1)
								{
									fontLayer20.GetCharData(list18[num9].get_Chars(0)).mOrder = list19[num9];
								}
								else
								{
									flag2 = true;
								}
							}
						}
						else
						{
							flag4 = true;
						}
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (text.Equals("LayerSetExInfo"))
			{
				if (theParams.mElementVector.Count == 4)
				{
					FontLayer fontLayer21 = new FontLayer();
					List<string> list20 = new List<string>();
					List<string> list21 = new List<string>();
					if (this.DataToLayer(theParams.mElementVector[1], ref fontLayer21) && base.DataToStringVector(theParams.mElementVector[2], ref list20) && base.DataToStringVector(theParams.mElementVector[3], ref list21))
					{
						if (list20.Count == list21.Count)
						{
							for (int num10 = 0; num10 < list20.Count; num10++)
							{
								fontLayer21.mExtendedInfo.Add(list20[num10], list21[num10]);
							}
						}
						else
						{
							flag4 = true;
						}
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else
			{
				if (!text.Equals("LayerSetAlphaCorrection"))
				{
					this.Error("Unknown Command");
					return false;
				}
				if (theParams.mElementVector.Count == 3)
				{
					FontLayer fontLayer22 = new FontLayer();
					int num11 = 0;
					if (this.DataToLayer(theParams.mElementVector[1], ref fontLayer22) && base.DataToInt(theParams.mElementVector[2], ref num11))
					{
						fontLayer22.mUseAlphaCorrection = num11 != 0;
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			if (flag)
			{
				this.Error("Invalid Number of Parameters");
				return false;
			}
			if (flag2)
			{
				this.Error("Invalid Paramater Type");
				return false;
			}
			if (flag3)
			{
				this.Error("Undefined Value");
				return false;
			}
			if (flag4)
			{
				this.Error("List Size Mismatch");
				return false;
			}
			return true;
		}

		public FontData()
		{
			this.mInitialized = false;
			this.mApp = null;
			this.mRefCount = 0;
			this.mDefaultPointSize = 0;
		}

		public override void Dispose()
		{
			foreach (KeyValuePair<string, DataElement> keyValuePair in this.mDefineMap)
			{
				string key = keyValuePair.Key;
				Dictionary<string, DataElement>.Enumerator enumerator;
				KeyValuePair<string, DataElement> keyValuePair2 = enumerator.Current;
				DataElement value = keyValuePair2.Value;
				if (value != null)
				{
					value.Dispose();
				}
			}
		}

		public void Ref()
		{
			this.mRefCount++;
		}

		public void DeRef()
		{
			if (--this.mRefCount == 0)
			{
				this.Dispose();
			}
		}

		public bool Load(byte[] buffer)
		{
			if (this.mInitialized)
			{
				return false;
			}
			bool flag = false;
			this.mCurrentLine.Clear();
			this.mFontErrorHeader = "Font Descriptor Error in Load\r\n";
			this.mSourceFile = "";
			this.mInitialized = this.LoadDescriptor(buffer);
			return !flag;
		}

		public bool Load(SexyAppBase theSexyApp, string theFontDescFileName)
		{
			if (this.mInitialized)
			{
				return false;
			}
			this.mApp = theSexyApp;
			bool flag = false;
			this.mCurrentLine.Clear();
			this.mFontErrorHeader = "Font Descriptor Error in " + theFontDescFileName + "\r\n";
			this.mSourceFile = theFontDescFileName;
			this.mInitialized = this.LoadDescriptor(theFontDescFileName);
			return !flag;
		}

		public bool LoadLegacy(Image theFontImage, string theFontDescFileName)
		{
			if (this.mInitialized)
			{
				return false;
			}
			this.mFontLayerList.AddLast(new FontLayer(this));
			FontLayer value = this.mFontLayerList.Last.Value;
			this.mFontLayerMap.Add("MAIN", value);
			value.mImage.mUnsharedImage = (MemoryImage)theFontImage;
			value.mDefaultHeight = value.mImage.GetImage().GetHeight();
			value.mAscent = value.mImage.GetImage().GetHeight();
			int num = 0;
			PFILE pfile = new PFILE(theFontDescFileName, "r");
			if (pfile == null)
			{
				return false;
			}
			this.mSourceFile = theFontDescFileName;
			pfile.Open();
			byte[] data = pfile.GetData();
			int i = 0;
			value.GetCharData(' ').mWidth = BitConverter.ToInt32(data, i);
			i += 4;
			value.mAscent = BitConverter.ToInt32(data, i);
			i += 4;
			while (i < data.Length)
			{
				byte b = data[i];
				i++;
				int num2 = BitConverter.ToInt32(data, i);
				i += 4;
				byte b2 = b;
				if (b2 == 0)
				{
					break;
				}
				value.GetCharData((char)b2).mImageRect = new Rect(num, 0, num2, value.mImage.GetImage().GetHeight());
				value.GetCharData((char)b2).mWidth = num2;
				num += num2;
			}
			for (char c = 'A'; c <= 'Z'; c += '\u0001')
			{
				if (value.GetCharData(c).mWidth == 0 && value.GetCharData(c - 'A' + 'a').mWidth != 0)
				{
					this.mCharMap.Add(c, c - 'A' + 'a');
				}
			}
			for (char c = 'a'; c <= 'z'; c += '\u0001')
			{
				if (value.GetCharData(c).mWidth == 0 && value.GetCharData(c - 'a' + 'A').mWidth != 0)
				{
					this.mCharMap.Add(c, c - 'a' + 'A');
				}
			}
			this.mInitialized = true;
			pfile.Close();
			return true;
		}

		public bool mInitialized;

		public int mRefCount;

		public SexyAppBase mApp;

		public int mDefaultPointSize;

		public Dictionary<char, char> mCharMap = new Dictionary<char, char>();

		public LinkedList<FontLayer> mFontLayerList = new LinkedList<FontLayer>();

		public Dictionary<string, FontLayer> mFontLayerMap = new Dictionary<string, FontLayer>();

		public string mSourceFile;

		public string mFontErrorHeader;

		public string mImagePathPrefix;
	}
}
