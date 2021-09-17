using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SexyFramework.Misc;
using SexyFramework.Resource;

namespace SexyFramework.Graphics
{
	public class ImageFont : Font
	{
		public virtual void GenerateActiveFontLayers()
		{
			if (!this.mFontData.mInitialized)
			{
				return;
			}
			List<ActiveFontLayer> list = new List<ActiveFontLayer>();
			this.mAscent = 0;
			this.mAscentPadding = 0;
			this.mHeight = 0;
			this.mLineSpacingOffset = 0;
			LinkedList<FontLayer>.Enumerator enumerator = this.mFontData.mFontLayerList.GetEnumerator();
			bool flag = true;
			while (enumerator.MoveNext())
			{
				FontLayer fontLayer = enumerator.Current;
				if (this.mPointSize >= fontLayer.mMinPointSize && (this.mPointSize <= fontLayer.mMaxPointSize || fontLayer.mMaxPointSize == -1))
				{
					bool flag2 = true;
					for (int i = 0; i < fontLayer.mRequiredTags.Count; i++)
					{
						if (this.mTagVector.IndexOf(fontLayer.mRequiredTags[i]) == -1)
						{
							flag2 = false;
						}
					}
					for (int i = 0; i < this.mTagVector.Count; i++)
					{
						if (fontLayer.mExcludedTags.IndexOf(this.mTagVector[i]) != -1)
						{
							flag2 = false;
						}
					}
					flag2 |= this.mActivateAllLayers;
					if (flag2)
					{
						list.Add(new ActiveFontLayer());
						ActiveFontLayer activeFontLayer = Enumerable.Last<ActiveFontLayer>(list);
						activeFontLayer.mBaseFontLayer = fontLayer;
						activeFontLayer.mUseAlphaCorrection = this.mWantAlphaCorrection & fontLayer.mImageIsWhite;
						double num = 1.0;
						double num2 = this.mScale;
						if (this.mScale == 1.0 && (fontLayer.mPointSize == 0 || this.mPointSize == fontLayer.mPointSize))
						{
							activeFontLayer.mScaledImages[7] = fontLayer.mImage;
							if (this.mFontImage != null)
							{
								activeFontLayer.mScaledImages[7].mUnsharedImage = this.mFontImage;
							}
							int count = fontLayer.mCharDataHashTable.mCharData.Count;
							CharData[] array = fontLayer.mCharDataHashTable.mCharData.ToArray();
							for (int j = 0; j < count; j++)
							{
								CharDataHashEntry charDataHashEntry = fontLayer.mCharDataHashTable.mHashEntries[array[j].mHashEntryIndex];
								activeFontLayer.mScaledCharImageRects.Add((char)charDataHashEntry.mChar, array[j].mImageRect);
							}
						}
						else
						{
							if (fontLayer.mPointSize != 0)
							{
								num = (double)fontLayer.mPointSize;
								num2 = (double)this.mPointSize * this.mScale;
							}
							MemoryImage memoryImage = new MemoryImage();
							int num3 = 0;
							bool flag3 = true;
							int num4 = 0;
							int num5 = 0;
							int count2 = fontLayer.mCharDataHashTable.mCharData.Count;
							CharData[] array2 = fontLayer.mCharDataHashTable.mCharData.ToArray();
							for (int k = 0; k < count2; k++)
							{
								Rect mImageRect = array2[k].mImageRect;
								int mY = array2[k].mOffset.mY;
								int num6 = mY + mImageRect.mHeight;
								num4 = Math.Min(mY, num4);
								num5 = Math.Max(num6, num5);
								if (num4 != mY || num5 != num6)
								{
									flag3 = false;
								}
								num3 += mImageRect.mWidth + 2;
							}
							if (!flag3)
							{
								MemoryImage memoryImage2 = new MemoryImage();
								memoryImage2.Create(num3, num5 - num4);
								Graphics graphics = new Graphics(memoryImage2);
								num3 = 0;
								count2 = fontLayer.mCharDataHashTable.mCharData.Count;
								array2 = fontLayer.mCharDataHashTable.mCharData.ToArray();
								for (int l = 0; l < count2; l++)
								{
									Rect mImageRect2 = array2[l].mImageRect;
									if (fontLayer.mImage.GetImage() != null)
									{
										graphics.DrawImage(fontLayer.mImage.GetImage(), num3, array2[l].mOffset.mY - num4, mImageRect2);
									}
									array2[l].mOffset.mY = num4;
									CharData charData = array2[l];
									charData.mOffset.mX = charData.mOffset.mX - 1;
									mImageRect2 = new Rect(num3, 0, mImageRect2.mWidth + 2, num5 - num4);
									num3 += mImageRect2.mWidth;
								}
								fontLayer.mImage.mUnsharedImage = memoryImage2;
								fontLayer.mImage.mOwnsUnshared = true;
								graphics.ClearRenderContext();
							}
							num3 = 0;
							int num7 = 0;
							count2 = fontLayer.mCharDataHashTable.mCharData.Count;
							array2 = fontLayer.mCharDataHashTable.mCharData.ToArray();
							for (int m = 0; m < count2; m++)
							{
								Rect mImageRect3 = array2[m].mImageRect;
								int num8 = (int)Math.Floor((double)array2[m].mOffset.mX * num2 / (double)((float)num));
								int num9 = (int)Math.Ceiling((double)(array2[m].mOffset.mX + mImageRect3.mWidth) * num2 / (double)((float)num));
								int theWidth = Math.Max(0, num9 - num8 - 1);
								int num10 = (int)Math.Floor((double)array2[m].mOffset.mY * num2 / (double)((float)num));
								int num11 = (int)Math.Ceiling((double)(array2[m].mOffset.mY + mImageRect3.mHeight) * num2 / (double)((float)num));
								int theHeight = Math.Max(0, num11 - num10 - 1);
								Rect rect = new Rect(num3, 0, theWidth, theHeight);
								if (rect.mHeight > num7)
								{
									num7 = rect.mHeight;
								}
								CharDataHashEntry charDataHashEntry2 = fontLayer.mCharDataHashTable.mHashEntries[array2[m].mHashEntryIndex];
								activeFontLayer.mScaledCharImageRects.Add((char)charDataHashEntry2.mChar, rect);
								num3 += rect.mWidth;
							}
							activeFontLayer.mScaledImages[7].mUnsharedImage = memoryImage;
							activeFontLayer.mScaledImages[7].mOwnsUnshared = true;
							memoryImage.Create(num3, num7);
							Graphics graphics2 = new Graphics(memoryImage);
							count2 = fontLayer.mCharDataHashTable.mCharData.Count;
							array2 = fontLayer.mCharDataHashTable.mCharData.ToArray();
							for (int n = 0; n < count2; n++)
							{
								if (fontLayer.mImage.GetImage() != null)
								{
									CharDataHashEntry charDataHashEntry3 = fontLayer.mCharDataHashTable.mHashEntries[array2[n].mHashEntryIndex];
									graphics2.DrawImage(fontLayer.mImage.GetImage(), activeFontLayer.mScaledCharImageRects[(char)charDataHashEntry3.mChar], array2[n].mImageRect);
								}
							}
							if (this.mForceScaledImagesWhite)
							{
								int num12 = memoryImage.mWidth * memoryImage.mHeight;
								uint[] bits = memoryImage.GetBits();
								for (int num13 = 0; num13 < num12; num13++)
								{
									bits[num13] |= 16777215U;
								}
							}
							memoryImage.AddImageFlags(128U);
							memoryImage.Palletize();
							graphics2.ClearRenderContext();
						}
						int num14 = (((double)fontLayer.mAscent * num2 / (double)((float)num) >= 0.0) ? ((int)((double)fontLayer.mAscent * num2 / (double)((float)num) + 0.501)) : ((int)((double)fontLayer.mAscent * num2 / (double)((float)num) - 0.501)));
						if (num14 > this.mAscent)
						{
							this.mAscent = num14;
						}
						if (fontLayer.mHeight != 0)
						{
							int num15 = (((double)fontLayer.mHeight * num2 / (double)((float)num) >= 0.0) ? ((int)((double)fontLayer.mHeight * num2 / (double)((float)num) + 0.501)) : ((int)((double)fontLayer.mHeight * num2 / (double)((float)num) - 0.501)));
							if (num15 > this.mHeight)
							{
								this.mHeight = num15;
							}
						}
						else
						{
							int num16 = (((double)fontLayer.mDefaultHeight * num2 / (double)((float)num) >= 0.0) ? ((int)((double)fontLayer.mDefaultHeight * num2 / (double)((float)num) + 0.501)) : ((int)((double)fontLayer.mDefaultHeight * num2 / (double)((float)num) - 0.501)));
							if (num16 > this.mHeight)
							{
								this.mHeight = num16;
							}
						}
						int num17 = (((double)fontLayer.mAscentPadding * num2 / (double)((float)num) >= 0.0) ? ((int)((double)fontLayer.mAscentPadding * num2 / (double)((float)num) + 0.501)) : ((int)((double)fontLayer.mAscentPadding * num2 / (double)((float)num) - 0.501)));
						if (flag || num17 < this.mAscentPadding)
						{
							this.mAscentPadding = num17;
						}
						int num18 = (((double)fontLayer.mLineSpacingOffset * num2 / (double)((float)num) >= 0.0) ? ((int)((double)fontLayer.mLineSpacingOffset * num2 / (double)((float)num) + 0.501)) : ((int)((double)fontLayer.mLineSpacingOffset * num2 / (double)((float)num) - 0.501)));
						if (flag || num18 > this.mLineSpacingOffset)
						{
							this.mLineSpacingOffset = num18;
						}
					}
					flag = false;
				}
			}
			this.mActiveLayerList = list.ToArray();
		}

		public virtual void DrawStringEx(Graphics g, int theX, int theY, string theString, Color theColor, Rect theClipRect, LinkedList<Rect> theDrawnAreas, ref int theWidth)
		{
			lock (GlobalMembers.gSexyAppBase.mImageSetCritSect)
			{
				for (int i = 0; i < 256; i++)
				{
					GlobalMembersImageFont.gRenderHead[i] = null;
					GlobalMembersImageFont.gRenderTail[i] = null;
				}
				if (theDrawnAreas != null)
				{
					theDrawnAreas.Clear();
				}
				if (!this.mFontData.mInitialized)
				{
					theWidth = 0;
				}
				else
				{
					this.Prepare();
					bool colorizeImages = g.GetColorizeImages();
					g.SetColorizeImages(true);
					int num = theX;
					int num2 = 0;
					for (int j = 0; j < theString.Length; j++)
					{
						char mappedChar = this.GetMappedChar(theString.get_Chars(j));
						char c = '\0';
						if (j < theString.Length - 1)
						{
							c = this.GetMappedChar(theString.get_Chars(j + 1));
						}
						int num3 = num;
						for (int k = 0; k < this.mActiveLayerList.Length; k++)
						{
							ActiveFontLayer activeFontLayer = this.mActiveLayerList[k];
							CharData charData = activeFontLayer.mBaseFontLayer.GetCharData(mappedChar);
							int num4 = num;
							int num5 = activeFontLayer.mBaseFontLayer.mPointSize;
							double num6 = this.mScale;
							if (num5 != 0)
							{
								num6 *= (double)this.mPointSize / (double)num5;
							}
							int num7;
							int num8;
							int num9;
							int num10;
							if (num6 == 1.0)
							{
								num7 = num4 + activeFontLayer.mBaseFontLayer.mOffset.mX + charData.mOffset.mX;
								num8 = theY - (activeFontLayer.mBaseFontLayer.mAscent - activeFontLayer.mBaseFontLayer.mOffset.mY - charData.mOffset.mY);
								num9 = charData.mWidth;
								if (c != '\0')
								{
									if (c == ' ')
									{
										num10 = 0;
									}
									else
									{
										num10 = activeFontLayer.mBaseFontLayer.mSpacing;
									}
									if (charData.mKerningCount != 0)
									{
										int mKerningCount = (int)charData.mKerningCount;
										for (int l = 0; l < mKerningCount; l++)
										{
											if (activeFontLayer.mBaseFontLayer.mKerningData[l + (int)charData.mKerningFirst].mChar == (ushort)c)
											{
												num10 += (int)activeFontLayer.mBaseFontLayer.mKerningData[l + (int)charData.mKerningFirst].mOffset;
											}
										}
									}
								}
								else
								{
									num10 = 0;
								}
							}
							else
							{
								num7 = num4 + (int)Math.Floor((double)(activeFontLayer.mBaseFontLayer.mOffset.mX + charData.mOffset.mX) * num6);
								num8 = theY - (int)Math.Floor((double)(activeFontLayer.mBaseFontLayer.mAscent - activeFontLayer.mBaseFontLayer.mOffset.mY - charData.mOffset.mY) * num6);
								num9 = (int)((double)charData.mWidth * num6);
								if (c != '\0')
								{
									num10 = activeFontLayer.mBaseFontLayer.mSpacing;
									if (charData.mKerningCount != 0)
									{
										int mKerningCount2 = (int)charData.mKerningCount;
										for (int m = 0; m < mKerningCount2; m++)
										{
											if (activeFontLayer.mBaseFontLayer.mKerningData[m + (int)charData.mKerningFirst].mChar == (ushort)c)
											{
												num10 += (int)((double)activeFontLayer.mBaseFontLayer.mKerningData[m + (int)charData.mKerningFirst].mOffset * num6);
											}
										}
									}
								}
								else
								{
									num10 = 0;
								}
							}
							Color mColor = default(Color);
							if (activeFontLayer.mColorStack.Count == 0)
							{
								mColor.mRed = Math.Min(theColor.mRed * activeFontLayer.mBaseFontLayer.mColorMult.mRed / 255 + activeFontLayer.mBaseFontLayer.mColorAdd.mRed, 255);
								mColor.mGreen = Math.Min(theColor.mGreen * activeFontLayer.mBaseFontLayer.mColorMult.mGreen / 255 + activeFontLayer.mBaseFontLayer.mColorAdd.mGreen, 255);
								mColor.mBlue = Math.Min(theColor.mBlue * activeFontLayer.mBaseFontLayer.mColorMult.mBlue / 255 + activeFontLayer.mBaseFontLayer.mColorAdd.mBlue, 255);
								mColor.mAlpha = Math.Min(theColor.mAlpha * activeFontLayer.mBaseFontLayer.mColorMult.mAlpha / 255 + activeFontLayer.mBaseFontLayer.mColorAdd.mAlpha, 255);
							}
							else
							{
								Color color = activeFontLayer.mColorStack[activeFontLayer.mColorStack.Count - 1];
								mColor.mRed = Math.Min(theColor.mRed * activeFontLayer.mBaseFontLayer.mColorMult.mRed * color.mRed / 65025 + activeFontLayer.mBaseFontLayer.mColorAdd.mRed * color.mRed / 255, 255);
								mColor.mGreen = Math.Min(theColor.mGreen * activeFontLayer.mBaseFontLayer.mColorMult.mGreen * color.mGreen / 65025 + activeFontLayer.mBaseFontLayer.mColorAdd.mGreen * color.mGreen / 255, 255);
								mColor.mBlue = Math.Min(theColor.mBlue * activeFontLayer.mBaseFontLayer.mColorMult.mBlue * color.mBlue / 65025 + activeFontLayer.mBaseFontLayer.mColorAdd.mBlue * color.mBlue / 255, 255);
								mColor.mAlpha = Math.Min(theColor.mAlpha * activeFontLayer.mBaseFontLayer.mColorMult.mAlpha * color.mAlpha / 65025 + activeFontLayer.mBaseFontLayer.mColorAdd.mAlpha * color.mAlpha / 255, 255);
							}
							int num11 = activeFontLayer.mBaseFontLayer.mBaseOrder + charData.mOrder;
							if (num2 >= 4096)
							{
								break;
							}
							RenderCommand renderCommand = GlobalMembersImageFont.GetRenderCommandPool()[num2++];
							renderCommand.mFontLayer = activeFontLayer;
							renderCommand.mColor = mColor;
							renderCommand.mDest[0] = num7;
							renderCommand.mDest[1] = num8;
							renderCommand.mSrc[0] = GlobalMembers.sexyatof(activeFontLayer.mScaledCharImageRects, mappedChar).mX;
							renderCommand.mSrc[1] = GlobalMembers.sexyatof(activeFontLayer.mScaledCharImageRects, mappedChar).mY;
							renderCommand.mSrc[2] = GlobalMembers.sexyatof(activeFontLayer.mScaledCharImageRects, mappedChar).mWidth;
							renderCommand.mSrc[3] = GlobalMembers.sexyatof(activeFontLayer.mScaledCharImageRects, mappedChar).mHeight;
							renderCommand.mMode = activeFontLayer.mBaseFontLayer.mDrawMode;
							renderCommand.mNext = null;
							int num12 = Math.Min(Math.Max(num11 + 128, 0), 255);
							if (GlobalMembersImageFont.gRenderTail[num12] == null)
							{
								GlobalMembersImageFont.gRenderTail[num12] = renderCommand;
								GlobalMembersImageFont.gRenderHead[num12] = renderCommand;
							}
							else
							{
								GlobalMembersImageFont.gRenderTail[num12].mNext = renderCommand;
								GlobalMembersImageFont.gRenderTail[num12] = renderCommand;
							}
							if (theDrawnAreas != null)
							{
								Rect rect = new Rect(num7, num8, activeFontLayer.mScaledCharImageRects[mappedChar].mWidth, activeFontLayer.mScaledCharImageRects[mappedChar].mHeight);
								theDrawnAreas.AddLast(rect);
							}
							int num13 = num9 + num10;
							if (num13 <= 0 && mappedChar == ' ')
							{
								num13 = num9;
							}
							num4 += num13;
							if (num4 > num3)
							{
								num3 = num4;
							}
						}
						num = num3;
					}
					theWidth = num - theX;
					Color color2 = g.GetColor();
					for (int i = 0; i < 256; i++)
					{
						for (RenderCommand renderCommand2 = GlobalMembersImageFont.gRenderHead[i]; renderCommand2 != null; renderCommand2 = renderCommand2.mNext)
						{
							if (renderCommand2.mFontLayer != null)
							{
								int drawMode = g.GetDrawMode();
								if (renderCommand2.mMode != -1)
								{
									g.SetDrawMode(renderCommand2.mMode);
								}
								g.SetColor(renderCommand2.mColor);
								int num14 = renderCommand2.mColor.mRed * 19660 + renderCommand2.mColor.mGreen * 38666 + renderCommand2.mColor.mBlue * 7208 >> 21;
								if (renderCommand2.mFontLayer.mUseAlphaCorrection && renderCommand2.mFontLayer.mBaseFontLayer.mUseAlphaCorrection && ImageFont.mAlphaCorrectionEnabled && num14 != 7)
								{
									MemoryImage memoryImage = renderCommand2.mFontLayer.mScaledImages[0].GetMemoryImage();
									if (g.Is3D())
									{
										bool flag2 = false;
										if (flag2)
										{
											if (memoryImage == null || memoryImage.mColorTable == null || memoryImage.mColorTable[254] != GlobalMembersImageFont.FONT_PALETTES[0][254])
											{
												memoryImage = renderCommand2.mFontLayer.GenerateAlphaCorrectedImage(0).GetMemoryImage();
											}
										}
										else
										{
											MemoryImage memoryImage2 = renderCommand2.mFontLayer.mScaledImages[num14].GetMemoryImage();
											if (memoryImage2 == null || memoryImage2.mColorTable == null || memoryImage2.mColorTable[254] != GlobalMembersImageFont.FONT_PALETTES[num14][254])
											{
												memoryImage2 = renderCommand2.mFontLayer.GenerateAlphaCorrectedImage(num14).GetMemoryImage();
											}
											g.DrawImage(memoryImage2, renderCommand2.mDest[0], renderCommand2.mDest[1], new Rect(renderCommand2.mSrc[0], renderCommand2.mSrc[1], renderCommand2.mSrc[2], renderCommand2.mSrc[3]));
										}
									}
									else
									{
										if (memoryImage == null || memoryImage.mColorTable == null)
										{
											memoryImage = renderCommand2.mFontLayer.GenerateAlphaCorrectedImage(0).GetMemoryImage();
										}
										if (memoryImage.mColorTable[254] != GlobalMembersImageFont.FONT_PALETTES[num14][254])
										{
											Array.Copy(memoryImage.mColorTable, GlobalMembersImageFont.FONT_PALETTES[num14], 1024);
											if (memoryImage.mNativeAlphaData != null)
											{
												for (int n = 0; n < 256; n++)
												{
													uint num15 = GlobalMembersImageFont.FONT_PALETTES[num14][n] >> 24;
													memoryImage.mNativeAlphaData[n] = (num15 << 24) | (num15 << 16) | (num15 << 8) | num15;
												}
											}
										}
										g.DrawImage(memoryImage, renderCommand2.mDest[0], renderCommand2.mDest[1], new Rect(renderCommand2.mSrc[0], renderCommand2.mSrc[1], renderCommand2.mSrc[2], renderCommand2.mSrc[3]));
									}
								}
								else
								{
									g.DrawImage(renderCommand2.mFontLayer.mScaledImages[7].GetImage(), renderCommand2.mDest[0], renderCommand2.mDest[1], new Rect(renderCommand2.mSrc[0], renderCommand2.mSrc[1], renderCommand2.mSrc[2], renderCommand2.mSrc[3]));
								}
								g.SetDrawMode(drawMode);
							}
						}
					}
					g.SetColor(color2);
					g.SetColorizeImages(colorizeImages);
				}
			}
		}

		public static void EnableAlphaCorrection()
		{
			ImageFont.EnableAlphaCorrection(true);
		}

		public static void EnableAlphaCorrection(bool alphaCorrect)
		{
			ImageFont.mAlphaCorrectionEnabled = alphaCorrect;
		}

		public static void SetOrderedHashing()
		{
			ImageFont.SetOrderedHashing(true);
		}

		public static void SetOrderedHashing(bool orderedHash)
		{
			ImageFont.mOrderedHash = orderedHash;
		}

		public char GetMappedChar(char theChar)
		{
			if (this.mFontData.mCharMap.ContainsKey(theChar))
			{
				return this.mFontData.mCharMap[theChar];
			}
			if (theChar == '\u00a0')
			{
				theChar = ' ';
			}
			return theChar;
		}

		private ImageFont()
		{
			lock (GlobalMembers.gSexyAppBase.mImageSetCritSect)
			{
				GlobalMembers.gSexyAppBase.mImageFontSet.Add(this);
			}
			this.mFontImage = null;
			this.mScale = 1.0;
			this.mWantAlphaCorrection = false;
			this.mFontData = new FontData();
			this.mFontData.Ref();
		}

		public ImageFont(byte[] buffer)
		{
			this.mFontImage = null;
			this.mScale = 1.0;
			this.mWantAlphaCorrection = false;
			this.mFontData = new FontData();
			this.mFontData.Ref();
			if (!false)
			{
				this.mFontData.Load(buffer);
				this.mPointSize = this.mFontData.mDefaultPointSize;
				this.mActivateAllLayers = false;
				this.mActiveListValid = true;
				this.mForceScaledImagesWhite = false;
			}
		}

		public ImageFont(SexyAppBase theSexyApp, string theFontDescFileName, string theImagePathPrefix)
		{
			lock (GlobalMembers.gSexyAppBase.mImageSetCritSect)
			{
				GlobalMembers.gSexyAppBase.mImageFontSet.Add(this);
			}
			this.mFontImage = null;
			this.mScale = 1.0;
			this.mWantAlphaCorrection = false;
			this.mFontData = new FontData();
			this.mFontData.Ref();
			this.mFontData.mImagePathPrefix = theImagePathPrefix;
			string text = theFontDescFileName + ".cfw2";
			string text2 = "cached\\" + text;
			string theFileName = text2;
			Buffer buffer = new Buffer();
			bool flag2 = false;
			if (theSexyApp.ReadBufferFromStream(text, ref buffer) && buffer.GetDataLen() >= 16)
			{
				flag2 = true;
			}
			else if (theSexyApp.ReadBufferFromStream(text2, ref buffer) && buffer.GetDataLen() >= 16)
			{
				flag2 = true;
			}
			else if (theSexyApp.ReadBufferFromStream(theFileName, ref buffer) && buffer.GetDataLen() >= 16)
			{
				flag2 = true;
			}
			bool flag3 = false;
			if (flag2)
			{
				if (theSexyApp.mResStreamsManager != null && theSexyApp.mResStreamsManager.IsInitialized())
				{
					flag3 = true;
				}
				else
				{
					this.SerializeRead(buffer.GetDataPtr(), buffer.GetDataLen() - 16, 16);
					flag3 = true;
				}
			}
			if (!flag3)
			{
				this.mFontData.Load(theSexyApp, theFontDescFileName);
				this.mPointSize = this.mFontData.mDefaultPointSize;
				this.mActivateAllLayers = false;
				this.GenerateActiveFontLayers();
				this.mActiveListValid = true;
				this.mForceScaledImagesWhite = false;
				bool mWriteFontCacheDir = theSexyApp.mWriteFontCacheDir;
			}
		}

		public ImageFont(Image theFontImage)
		{
			lock (GlobalMembers.gSexyAppBase.mImageSetCritSect)
			{
				GlobalMembers.gSexyAppBase.mImageFontSet.Add(this);
			}
			this.mScale = 1.0;
			this.mWantAlphaCorrection = false;
			this.mFontData = new FontData();
			this.mFontData.Ref();
			this.mFontData.mInitialized = true;
			this.mPointSize = this.mFontData.mDefaultPointSize;
			this.mActiveListValid = false;
			this.mForceScaledImagesWhite = false;
			this.mActivateAllLayers = false;
			this.mFontData.mFontLayerList.AddLast(new FontLayer(this.mFontData));
			FontLayer value = this.mFontData.mFontLayerList.Last.Value;
			this.mFontData.mFontLayerMap.Add("", value);
			this.mFontImage = (MemoryImage)theFontImage;
			value.mImage.mUnsharedImage = this.mFontImage;
			value.mDefaultHeight = value.mImage.GetImage().GetHeight();
			value.mAscent = value.mImage.GetImage().GetHeight();
		}

		public ImageFont(ImageFont theImageFont)
			: base(theImageFont)
		{
			this.mScale = theImageFont.mScale;
			this.mFontData = theImageFont.mFontData;
			this.mPointSize = theImageFont.mPointSize;
			this.mTagVector = theImageFont.mTagVector;
			this.mActiveListValid = theImageFont.mActiveListValid;
			this.mForceScaledImagesWhite = theImageFont.mForceScaledImagesWhite;
			this.mWantAlphaCorrection = theImageFont.mWantAlphaCorrection;
			this.mActivateAllLayers = theImageFont.mActivateAllLayers;
			this.mFontImage = theImageFont.mFontImage;
			lock (GlobalMembers.gSexyAppBase.mImageSetCritSect)
			{
				GlobalMembers.gSexyAppBase.mImageFontSet.Add(this);
			}
			this.mFontData.Ref();
			if (this.mActiveListValid)
			{
				this.mActiveLayerList = theImageFont.mActiveLayerList;
			}
		}

		public override void Dispose()
		{
			lock (GlobalMembers.gSexyAppBase.mImageSetCritSect)
			{
				GlobalMembers.gSexyAppBase.mImageFontSet.Remove(this);
			}
			this.mFontData.DeRef();
			base.Dispose();
		}

		public ImageFont(Image theFontImage, string theFontDescFileName)
		{
			lock (GlobalMembers.gSexyAppBase.mImageSetCritSect)
			{
				GlobalMembers.gSexyAppBase.mImageFontSet.Add(this);
			}
			this.mScale = 1.0;
			this.mFontImage = null;
			this.mFontData = new FontData();
			this.mFontData.Ref();
			this.mFontData.LoadLegacy(theFontImage, theFontDescFileName);
			this.mPointSize = this.mFontData.mDefaultPointSize;
			this.mActivateAllLayers = false;
			this.GenerateActiveFontLayers();
			this.mActiveListValid = true;
		}

		public override ImageFont AsImageFont()
		{
			return this;
		}

		public override int CharWidth(char theChar)
		{
			return this.CharWidthKern(theChar, '\0');
		}

		public override int CharWidthKern(char theChar, char thePrevChar)
		{
			this.Prepare();
			int num = 0;
			double num2 = (double)this.mPointSize * this.mScale;
			theChar = this.GetMappedChar(theChar);
			if (thePrevChar != '\0')
			{
				thePrevChar = this.GetMappedChar(thePrevChar);
			}
			for (int i = 0; i < this.mActiveLayerList.Length; i++)
			{
				ActiveFontLayer activeFontLayer = this.mActiveLayerList[i];
				int num3 = 0;
				int num4 = activeFontLayer.mBaseFontLayer.mPointSize;
				int num5;
				int num6;
				if (num4 == 0)
				{
					num5 = (((double)activeFontLayer.mBaseFontLayer.GetCharData(theChar).mWidth * this.mScale >= 0.0) ? ((int)((double)activeFontLayer.mBaseFontLayer.GetCharData(theChar).mWidth * this.mScale + 0.501)) : ((int)((double)activeFontLayer.mBaseFontLayer.GetCharData(theChar).mWidth * this.mScale - 0.501)));
					if (thePrevChar != '\0')
					{
						num6 = activeFontLayer.mBaseFontLayer.mSpacing;
						CharData charData = activeFontLayer.mBaseFontLayer.GetCharData(thePrevChar);
						if (charData.mKerningCount != 0)
						{
							int mKerningCount = (int)charData.mKerningCount;
							for (int j = 0; j < mKerningCount; j++)
							{
								if (activeFontLayer.mBaseFontLayer.mKerningData[j + (int)charData.mKerningFirst].mChar == (ushort)theChar)
								{
									num6 += (int)activeFontLayer.mBaseFontLayer.mKerningData[j + (int)charData.mKerningFirst].mOffset * (int)this.mScale;
								}
							}
						}
					}
					else
					{
						num6 = 0;
					}
				}
				else
				{
					num5 = (((double)activeFontLayer.mBaseFontLayer.GetCharData(theChar).mWidth * num2 / (double)((float)num4) >= 0.0) ? ((int)((double)activeFontLayer.mBaseFontLayer.GetCharData(theChar).mWidth * num2 / (double)((float)num4) + 0.501)) : ((int)((double)activeFontLayer.mBaseFontLayer.GetCharData(theChar).mWidth * num2 / (double)((float)num4) - 0.501)));
					if (thePrevChar != '\0')
					{
						if (thePrevChar == ' ')
						{
							num6 = 0;
						}
						else
						{
							num6 = activeFontLayer.mBaseFontLayer.mSpacing;
						}
						CharData charData2 = activeFontLayer.mBaseFontLayer.GetCharData(thePrevChar);
						if (charData2.mKerningCount != 0)
						{
							int mKerningCount2 = (int)charData2.mKerningCount;
							for (int k = 0; k < mKerningCount2; k++)
							{
								if (activeFontLayer.mBaseFontLayer.mKerningData[k + (int)charData2.mKerningFirst].mChar == (ushort)theChar)
								{
									num6 += (int)activeFontLayer.mBaseFontLayer.mKerningData[k + (int)charData2.mKerningFirst].mOffset * (int)num2 / num4;
								}
							}
						}
					}
					else
					{
						num6 = 0;
					}
				}
				int num7 = num5 + num6;
				if (num7 <= 0 && theChar == ' ')
				{
					num7 = num5;
				}
				num3 += num7;
				if (num3 > num)
				{
					num = num3;
				}
			}
			return num;
		}

		public override int StringWidth(string theString)
		{
			int num = 0;
			char thePrevChar = '\0';
			for (int i = 0; i < theString.Length; i++)
			{
				char c = theString.get_Chars(i);
				num += this.CharWidthKern(c, thePrevChar);
				thePrevChar = c;
			}
			return num;
		}

		public override void DrawString(Graphics g, int theX, int theY, string theString, Color theColor, Rect theClipRect)
		{
			int num = 0;
			this.DrawStringEx(g, theX, theY, theString, theColor, theClipRect, null, ref num);
		}

		public override Font Duplicate()
		{
			return new ImageFont(this);
		}

		public virtual void SetPointSize(int thePointSize)
		{
			this.mPointSize = thePointSize;
			this.mActiveListValid = false;
		}

		public virtual int GetPointSize()
		{
			return this.mPointSize;
		}

		public virtual void SetScale(double theScale)
		{
			this.mScale = theScale;
			this.mActiveListValid = false;
		}

		public virtual int GetDefaultPointSize()
		{
			return this.mFontData.mDefaultPointSize;
		}

		public virtual bool AddTag(string theTagName)
		{
			if (this.HasTag(theTagName))
			{
				return false;
			}
			string text = theTagName.ToUpper();
			this.mTagVector.Add(text);
			this.mActiveListValid = false;
			return true;
		}

		public virtual bool RemoveTag(string theTagName)
		{
			string text = theTagName.ToUpper();
			if (this.mTagVector.Remove(text))
			{
				this.mActiveListValid = false;
				return true;
			}
			return false;
		}

		public virtual bool HasTag(string theTagName)
		{
			return this.mTagVector.Contains(theTagName);
		}

		public virtual string GetDefine(string theName)
		{
			DataElement dataElement = this.mFontData.Dereference(theName);
			if (dataElement == null)
			{
				return "";
			}
			return this.mFontData.DataElementToString(dataElement, true);
		}

		public virtual void Prepare()
		{
			if (!this.mActiveListValid)
			{
				this.GenerateActiveFontLayers();
				this.mActiveListValid = true;
			}
		}

		public static bool CheckCache(string theSrcFile, string theAltData)
		{
			return false;
		}

		public static bool SetCacheUpToDate(string theSrcFile, string theAltData)
		{
			return false;
		}

		public static ImageFont ReadFromCache(string theSrcFile, string theAltData)
		{
			return null;
		}

		public virtual void WriteToCache(string theSrcFile, string theAltData)
		{
		}

		public string SerializeReadStr(byte[] thePtr, int theStartIndex, int size)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < size; i++)
			{
				stringBuilder.Append((char)thePtr[theStartIndex + i]);
			}
			return stringBuilder.ToString();
		}

		public bool SerializeRead(byte[] thePtr, int theSize, int theStartIndex)
		{
			if (thePtr == null)
			{
				return false;
			}
			bool result = false;
			this.mAscent = BitConverter.ToInt32(thePtr, theStartIndex);
			int num = theStartIndex + 4;
			this.mAscentPadding = BitConverter.ToInt32(thePtr, num);
			num += 4;
			this.mHeight = BitConverter.ToInt32(thePtr, num);
			num += 4;
			this.mLineSpacingOffset = BitConverter.ToInt32(thePtr, num);
			num += 4;
			this.mFontData.mApp = GlobalMembers.gSexyAppBase;
			this.mFontData.mInitialized = BitConverter.ToBoolean(thePtr, num);
			num++;
			this.mFontData.mDefaultPointSize = BitConverter.ToInt32(thePtr, num);
			num += 4;
			int num2 = BitConverter.ToInt32(thePtr, num);
			num += 4;
			for (int i = 0; i < num2; i++)
			{
				ushort num3 = BitConverter.ToUInt16(thePtr, num);
				num += 2;
				ushort num4 = BitConverter.ToUInt16(thePtr, num);
				num += 2;
				this.mFontData.mCharMap.Add((char)num3, (char)num4);
			}
			int num5 = BitConverter.ToInt32(thePtr, num);
			num += 4;
			for (int j = 0; j < num5; j++)
			{
				this.mFontData.mFontLayerList.AddLast(new FontLayer(this.mFontData));
				FontLayer value = this.mFontData.mFontLayerList.Last.Value;
				int num6 = BitConverter.ToInt32(thePtr, num);
				num += 4;
				value.mLayerName = this.SerializeReadStr(thePtr, num, num6);
				num += num6;
				this.mFontData.mFontLayerMap.Add(value.mLayerName, value);
				int num7 = BitConverter.ToInt32(thePtr, num);
				num += 4;
				for (int k = 0; k < num7; k++)
				{
					int num8 = BitConverter.ToInt32(thePtr, num);
					num += 4;
					string text = this.SerializeReadStr(thePtr, num, num8);
					num += num8;
					value.mRequiredTags.Add(text);
				}
				num7 = BitConverter.ToInt32(thePtr, num);
				num += 4;
				for (int l = 0; l < num7; l++)
				{
					int num9 = BitConverter.ToInt32(thePtr, num);
					num += 4;
					string text2 = this.SerializeReadStr(thePtr, num, num9);
					num += num9;
					value.mExcludedTags.Add(text2);
				}
				int num10 = BitConverter.ToInt32(thePtr, num);
				num += 4;
				if (num10 != 0)
				{
					List<FontLayer.KerningValue> list = new List<FontLayer.KerningValue>();
					for (int m = 0; m < num10; m++)
					{
						FontLayer.KerningValue kerningValue = default(FontLayer.KerningValue);
						kerningValue.mInt = BitConverter.ToInt32(thePtr, num);
						num += 4;
						kerningValue.mChar = (ushort)((kerningValue.mInt >> 16) & 65535);
						kerningValue.mOffset = (short)(kerningValue.mInt & 65535);
						list.Add(kerningValue);
					}
					value.mKerningData = list.ToArray();
				}
				int num11 = BitConverter.ToInt32(thePtr, num);
				num += 4;
				for (int n = 0; n < num11; n++)
				{
					ushort inChar = BitConverter.ToUInt16(thePtr, num);
					num += 2;
					CharData charData = value.mCharDataHashTable.GetCharData((char)inChar, true);
					charData.mImageRect.mX = BitConverter.ToInt32(thePtr, num);
					num += 4;
					charData.mImageRect.mY = BitConverter.ToInt32(thePtr, num);
					num += 4;
					charData.mImageRect.mWidth = BitConverter.ToInt32(thePtr, num);
					num += 4;
					charData.mImageRect.mHeight = BitConverter.ToInt32(thePtr, num);
					num += 4;
					charData.mOffset.mX = BitConverter.ToInt32(thePtr, num);
					num += 4;
					charData.mOffset.mY = BitConverter.ToInt32(thePtr, num);
					num += 4;
					charData.mKerningFirst = BitConverter.ToUInt16(thePtr, num);
					num += 2;
					charData.mKerningCount = BitConverter.ToUInt16(thePtr, num);
					num += 2;
					charData.mWidth = BitConverter.ToInt32(thePtr, num);
					num += 4;
					charData.mOrder = BitConverter.ToInt32(thePtr, num);
					num += 4;
				}
				value.mColorMult.mRed = BitConverter.ToInt32(thePtr, num);
				num += 4;
				value.mColorMult.mGreen = BitConverter.ToInt32(thePtr, num);
				num += 4;
				value.mColorMult.mBlue = BitConverter.ToInt32(thePtr, num);
				num += 4;
				value.mColorMult.mAlpha = BitConverter.ToInt32(thePtr, num);
				num += 4;
				value.mColorAdd.mRed = BitConverter.ToInt32(thePtr, num);
				num += 4;
				value.mColorAdd.mGreen = BitConverter.ToInt32(thePtr, num);
				num += 4;
				value.mColorAdd.mBlue = BitConverter.ToInt32(thePtr, num);
				num += 4;
				value.mColorAdd.mAlpha = BitConverter.ToInt32(thePtr, num);
				num += 4;
				int num12 = BitConverter.ToInt32(thePtr, num);
				num += 4;
				value.mImageFileName = this.SerializeReadStr(thePtr, num, num12);
				num += num12;
				bool flag = false;
				SharedImageRef sharedImageRef = new SharedImageRef();
				if (GlobalMembers.gSexyAppBase.mResourceManager != null && string.IsNullOrEmpty(this.mFontData.mImagePathPrefix))
				{
					string idByPath = GlobalMembers.gSexyAppBase.mResourceManager.GetIdByPath(value.mImageFileName);
					if (!string.IsNullOrEmpty(idByPath))
					{
						sharedImageRef = GlobalMembers.gSexyAppBase.mResourceManager.GetImage(idByPath);
						if (sharedImageRef.GetDeviceImage() == null)
						{
							sharedImageRef = GlobalMembers.gSexyAppBase.mResourceManager.LoadImage(idByPath);
						}
						if (sharedImageRef.GetDeviceImage() != null)
						{
							flag = true;
						}
					}
				}
				if (!flag)
				{
					sharedImageRef = GlobalMembers.gSexyAppBase.GetSharedImage(this.mFontData.mImagePathPrefix + value.mImageFileName);
				}
				value.mImage = new SharedImageRef(sharedImageRef);
				if (value.mImage.GetDeviceImage() == null)
				{
					result = true;
				}
				value.mDrawMode = BitConverter.ToInt32(thePtr, num);
				num += 4;
				value.mOffset.mX = BitConverter.ToInt32(thePtr, num);
				num += 4;
				value.mOffset.mY = BitConverter.ToInt32(thePtr, num);
				num += 4;
				value.mSpacing = BitConverter.ToInt32(thePtr, num);
				num += 4;
				value.mMinPointSize = BitConverter.ToInt32(thePtr, num);
				num += 4;
				value.mMaxPointSize = BitConverter.ToInt32(thePtr, num);
				num += 4;
				value.mPointSize = BitConverter.ToInt32(thePtr, num);
				num += 4;
				value.mAscent = BitConverter.ToInt32(thePtr, num);
				num += 4;
				value.mAscentPadding = BitConverter.ToInt32(thePtr, num);
				num += 4;
				value.mHeight = BitConverter.ToInt32(thePtr, num);
				num += 4;
				value.mDefaultHeight = BitConverter.ToInt32(thePtr, num);
				num += 4;
				value.mLineSpacingOffset = BitConverter.ToInt32(thePtr, num);
				num += 4;
				value.mBaseOrder = BitConverter.ToInt32(thePtr, num);
				num += 4;
			}
			int num13 = BitConverter.ToInt32(thePtr, num);
			num += 4;
			this.mFontData.mSourceFile = this.SerializeReadStr(thePtr, num, num13);
			num += num13;
			int num14 = BitConverter.ToInt32(thePtr, num);
			num += 4;
			this.mFontData.mFontErrorHeader = this.SerializeReadStr(thePtr, num, num14);
			num += num14;
			this.mPointSize = BitConverter.ToInt32(thePtr, num);
			num += 4;
			int num15 = BitConverter.ToInt32(thePtr, num);
			num += 4;
			for (int num16 = 0; num16 < num15; num16++)
			{
				int num17 = BitConverter.ToInt32(thePtr, num);
				num += 4;
				string text3 = this.SerializeReadStr(thePtr, num, num17);
				num += num17;
				this.mTagVector.Add(text3);
			}
			this.mScale = BitConverter.ToDouble(thePtr, num);
			num += 8;
			this.mForceScaledImagesWhite = BitConverter.ToBoolean(thePtr, num);
			num++;
			this.mActivateAllLayers = BitConverter.ToBoolean(thePtr, num);
			num++;
			this.mActiveListValid = false;
			return result;
		}

		public bool SerializeReadEndian(IntPtr thePtr, int theSize)
		{
			return false;
		}

		public bool SerializeWrite(IntPtr thePtr)
		{
			return this.SerializeWrite(thePtr, 0);
		}

		public bool SerializeWrite(IntPtr thePtr, int theSizeIfKnown)
		{
			return false;
		}

		public int GetLayerCount()
		{
			LinkedList<FontLayer>.Enumerator enumerator = this.mFontData.mFontLayerList.GetEnumerator();
			int num = 0;
			while (enumerator.MoveNext())
			{
				FontLayer fontLayer = enumerator.Current;
				if (fontLayer.mLayerName.Length < 6 || !fontLayer.mLayerName.EndsWith("__MOD"))
				{
					num++;
				}
			}
			return num;
		}

		public void PushLayerColor(string theLayerName, Color theColor)
		{
			this.Prepare();
			for (int i = 0; i < this.mActiveLayerList.Length; i++)
			{
				ActiveFontLayer activeFontLayer = this.mActiveLayerList[i];
				string mLayerName = activeFontLayer.mBaseFontLayer.mLayerName;
				if (string.Compare(activeFontLayer.mBaseFontLayer.mLayerName, theLayerName, 5) == 0 || (mLayerName.StartsWith(theLayerName, 5) && mLayerName.EndsWith("__MOD") && mLayerName.Length == theLayerName.Length + 5))
				{
					activeFontLayer.PushColor(theColor);
				}
			}
		}

		public void PushLayerColor(int theLayer, Color theColor)
		{
			this.Prepare();
			LinkedList<FontLayer>.Enumerator enumerator = this.mFontData.mFontLayerList.GetEnumerator();
			int num = 0;
			while (enumerator.MoveNext())
			{
				FontLayer fontLayer = enumerator.Current;
				if (fontLayer.mLayerName.Length < 6 || !fontLayer.mLayerName.EndsWith("__MOD"))
				{
					if (num == theLayer)
					{
						this.PushLayerColor(fontLayer.mLayerName, theColor);
						return;
					}
					num++;
				}
			}
		}

		public void PopLayerColor(string theLayerName)
		{
			for (int i = 0; i < this.mActiveLayerList.Length; i++)
			{
				ActiveFontLayer activeFontLayer = this.mActiveLayerList[i];
				string mLayerName = activeFontLayer.mBaseFontLayer.mLayerName;
				if (string.Compare(mLayerName, theLayerName, 5) == 0 || (mLayerName.StartsWith(theLayerName, 5) && mLayerName.EndsWith("__MOD") && mLayerName.Length == theLayerName.Length + 5))
				{
					activeFontLayer.PopColor();
				}
			}
		}

		public void PopLayerColor(int theLayer)
		{
			LinkedList<FontLayer>.Enumerator enumerator = this.mFontData.mFontLayerList.GetEnumerator();
			int num = 0;
			while (enumerator.MoveNext())
			{
				FontLayer fontLayer = enumerator.Current;
				if (fontLayer.mLayerName.Length < 6 || !fontLayer.mLayerName.EndsWith("__MOD"))
				{
					if (num == theLayer)
					{
						this.PopLayerColor(fontLayer.mLayerName);
						return;
					}
					num++;
				}
			}
		}

		public static bool mAlphaCorrectionEnabled;

		public static bool mOrderedHash;

		public FontData mFontData;

		public int mPointSize;

		public List<string> mTagVector = new List<string>();

		public bool mActivateAllLayers;

		public bool mActiveListValid;

		public ActiveFontLayer[] mActiveLayerList = new ActiveFontLayer[0];

		public double mScale;

		public bool mForceScaledImagesWhite;

		public bool mWantAlphaCorrection;

		public MemoryImage mFontImage;
	}
}
