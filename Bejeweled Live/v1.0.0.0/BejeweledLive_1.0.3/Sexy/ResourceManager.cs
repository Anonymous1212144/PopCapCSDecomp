using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using BejeweledLIVE;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Sexy
{
	public class ResourceManager : IDisposable
	{
		public ResourceManager(SexyAppBase theApp)
		{
			this.mApp = theApp;
			this.mHasFailed = false;
			this.mXMLParser = null;
			this.mCurResGroupList = null;
			this.mResGroupMap = new Dictionary<string, List<BaseRes>>();
			this.mImageMap = new Dictionary<string, BaseRes>();
			this.mSoundMap = new Dictionary<string, BaseRes>();
			this.mFontMap = new Dictionary<string, BaseRes>();
			this.mMusicMap = new Dictionary<string, BaseRes>();
			this.mLevelMap = new Dictionary<string, BaseRes>();
			this.mLoadedGroups = new List<string>();
			this.mContentManager = this.mApp.mContentManager;
			this.currentBackDropContentManager = new ContentManager(this.mApp.XnaGame.Services);
			this.currentBackDropContentManager.RootDirectory = this.mContentManager.RootDirectory;
			this.mAllowAlreadyDefinedResources = false;
			this.mAllowMissingProgramResources = false;
			this.mProgress = 0.0;
			this.mTotalResources = 0;
			this.mLoadedCount = 0;
		}

		public virtual string GetResourceDir()
		{
			return "";
		}

		~ResourceManager()
		{
			this.Dispose(false);
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
		}

		public bool HadError()
		{
			return this.mHasFailed;
		}

		private bool IsGroupLoaded(string theResGroup)
		{
			return this.mLoadedGroups.Contains(theResGroup);
		}

		public bool ParseResourcesFile(string theFilename)
		{
			this.mXMLParser = new XMLParser();
			if (!this.mXMLParser.OpenFile(Path.Combine(this.GetResourceDir(), theFilename)))
			{
				this.Fail("Resource file not found: " + theFilename);
			}
			XMLElement xmlelement = new XMLElement();
			while (!this.mXMLParser.HasFailed())
			{
				if (!this.mXMLParser.NextElement(ref xmlelement))
				{
					this.Fail(this.mXMLParser.GetErrorText());
				}
				if (xmlelement.mType == XMLElementType.TYPE_START)
				{
					if (!(xmlelement.mValue != "ResourceManifest"))
					{
						return this.DoParseResources();
					}
					break;
				}
			}
			this.Fail("Expecting ResourceManifest tag");
			return this.DoParseResources();
		}

		private bool DoParseResources()
		{
			if (!this.mXMLParser.HasFailed())
			{
				XMLElement xmlelement;
				for (;;)
				{
					xmlelement = new XMLElement();
					if (!this.mXMLParser.NextElement(ref xmlelement))
					{
						goto IL_10A;
					}
					if (xmlelement.mType == XMLElementType.TYPE_START)
					{
						if (!(xmlelement.mValue == "Resources"))
						{
							goto IL_C4;
						}
						this.mCurResGroup = xmlelement.mAttributes["id"];
						if (!this.mResGroupMap.ContainsKey(this.mCurResGroup))
						{
							this.mResGroupMap.Add(this.mCurResGroup, new List<BaseRes>());
						}
						this.mCurResGroupList = this.mResGroupMap[this.mCurResGroup];
						if (this.mCurResGroup.Length == 0)
						{
							break;
						}
						if (!this.ParseResources())
						{
							goto Block_6;
						}
					}
					else if (xmlelement.mType == XMLElementType.TYPE_ELEMENT)
					{
						goto Block_7;
					}
				}
				this.Fail("No id specified.");
				Block_6:
				goto IL_10A;
				IL_C4:
				this.Fail("Invalid Section '" + xmlelement.mValue + "'");
				goto IL_10A;
				Block_7:
				this.Fail("Element Not Expected '" + xmlelement.mValue + "'");
			}
			IL_10A:
			if (this.mXMLParser.HasFailed())
			{
				this.Fail(this.mXMLParser.GetErrorText());
			}
			this.mXMLParser = null;
			return !this.mHasFailed;
		}

		private bool ParseResources()
		{
			this.mDefaultPath = this.GetResourceDir();
			XMLElement xmlelement;
			for (;;)
			{
				xmlelement = new XMLElement();
				if (!this.mXMLParser.NextElement(ref xmlelement))
				{
					break;
				}
				if (xmlelement.mType == XMLElementType.TYPE_START)
				{
					if (xmlelement.mValue == "Image")
					{
						if (!this.ParseImageResource(xmlelement))
						{
							return false;
						}
						if (!this.mXMLParser.NextElement(ref xmlelement))
						{
							return false;
						}
						if (xmlelement.mType != XMLElementType.TYPE_END)
						{
							goto Block_6;
						}
					}
					else if (xmlelement.mValue == "Sound")
					{
						if (!this.ParseSoundResource(xmlelement))
						{
							return false;
						}
						if (!this.mXMLParser.NextElement(ref xmlelement))
						{
							return false;
						}
						if (xmlelement.mType != XMLElementType.TYPE_END)
						{
							goto Block_10;
						}
					}
					else if (xmlelement.mValue == "Font")
					{
						if (!this.ParseFontResource(xmlelement))
						{
							return false;
						}
						if (!this.mXMLParser.NextElement(ref xmlelement))
						{
							return false;
						}
						if (xmlelement.mType != XMLElementType.TYPE_END)
						{
							goto Block_14;
						}
					}
					else if (xmlelement.mValue == "Music")
					{
						if (!this.ParseMusicResource(xmlelement))
						{
							return false;
						}
						if (!this.mXMLParser.NextElement(ref xmlelement))
						{
							return false;
						}
						if (xmlelement.mType != XMLElementType.TYPE_END)
						{
							goto Block_18;
						}
					}
					else if (xmlelement.mValue == "Level")
					{
						if (!this.ParseLevelResource(xmlelement))
						{
							return false;
						}
						if (!this.mXMLParser.NextElement(ref xmlelement))
						{
							return false;
						}
						if (xmlelement.mType != XMLElementType.TYPE_END)
						{
							goto Block_22;
						}
					}
					else
					{
						if (!(xmlelement.mValue == "SetDefaults"))
						{
							goto IL_1D0;
						}
						if (!this.ParseSetDefaults(xmlelement))
						{
							return false;
						}
						if (!this.mXMLParser.NextElement(ref xmlelement))
						{
							return false;
						}
						if (xmlelement.mType != XMLElementType.TYPE_END)
						{
							goto Block_26;
						}
					}
				}
				else
				{
					if (xmlelement.mType == XMLElementType.TYPE_ELEMENT)
					{
						goto Block_27;
					}
					if (xmlelement.mType == XMLElementType.TYPE_END)
					{
						return true;
					}
				}
			}
			return false;
			Block_6:
			return this.Fail("Unexpected element found.");
			Block_10:
			return this.Fail("Unexpected element found.");
			Block_14:
			return this.Fail("Unexpected element found.");
			Block_18:
			return this.Fail("Unexpected element found.");
			Block_22:
			return this.Fail("Unexpected element found.");
			Block_26:
			return this.Fail("Unexpected element found.");
			IL_1D0:
			this.Fail("Invalid Section '" + xmlelement.mValue + "'");
			return false;
			Block_27:
			this.Fail("Element Not Expected '" + xmlelement.mValue + "'");
			return false;
		}

		private bool ParseCommonResource(XMLElement theElement, BaseRes theRes, Dictionary<string, BaseRes> theMap)
		{
			this.mHadAlreadyDefinedError = false;
			string text = theElement.mAttributes["path"];
			theRes.mXMLAttributes = theElement.mAttributes;
			theRes.mFromProgram = false;
			if (text.Length > 0 && text.get_Chars(0) == '!')
			{
				theRes.mPath = text;
				if (text == "!program")
				{
					theRes.mFromProgram = true;
				}
			}
			else
			{
				theRes.mPath = this.mDefaultPath + text;
			}
			Dictionary<string, string>.Enumerator enumerator = theElement.mAttributes.GetEnumerator();
			string text2 = this.mDefaultIdPrefix;
			while (enumerator.MoveNext())
			{
				KeyValuePair<string, string> keyValuePair = enumerator.Current;
				if (keyValuePair.Key == "id")
				{
					string text3 = this.mDefaultIdPrefix;
					KeyValuePair<string, string> keyValuePair2 = enumerator.Current;
					text2 = text3 + keyValuePair2.Value;
				}
			}
			theRes.mResGroup = this.mCurResGroup;
			theRes.mId = text2;
			if (theMap.ContainsKey(text2))
			{
				theMap[text2] = theRes;
			}
			else
			{
				theMap.Add(text2, theRes);
			}
			bool flag = false;
			for (int i = 0; i < this.mCurResGroupList.Count; i++)
			{
				if (this.mCurResGroupList[i].mId == theRes.mId)
				{
					this.mCurResGroupList[i] = theRes;
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				this.mCurResGroupList.Add(theRes);
			}
			return true;
		}

		private bool ParseSetDefaults(XMLElement theElement)
		{
			foreach (KeyValuePair<string, string> keyValuePair in theElement.mAttributes)
			{
				Dictionary<string, string>.Enumerator enumerator;
				if (keyValuePair.Key == "path")
				{
					string resourceDir = this.GetResourceDir();
					KeyValuePair<string, string> keyValuePair2 = enumerator.Current;
					this.mDefaultPath = resourceDir + keyValuePair2.Value + "/";
				}
				KeyValuePair<string, string> keyValuePair3 = enumerator.Current;
				if (keyValuePair3.Key == "idprefix")
				{
					KeyValuePair<string, string> keyValuePair4 = enumerator.Current;
					this.mDefaultIdPrefix = keyValuePair4.Value;
				}
			}
			return true;
		}

		private bool ParseFontResource(XMLElement theElement)
		{
			FontRes fontRes = new FontRes();
			if (!this.ParseCommonResource(theElement, fontRes, this.mFontMap))
			{
				if (!this.mHadAlreadyDefinedError || !this.mAllowAlreadyDefinedResources)
				{
					fontRes.DeleteResource();
					return false;
				}
				this.mError = "";
				this.mHasFailed = false;
				FontRes fontRes2 = fontRes;
				fontRes = (FontRes)this.mFontMap[fontRes2.mId];
				fontRes.mPath = fontRes2.mPath;
				fontRes.mXMLAttributes = fontRes2.mXMLAttributes;
				fontRes2.DeleteResource();
			}
			fontRes.mFont = null;
			foreach (KeyValuePair<string, string> keyValuePair in theElement.mAttributes)
			{
				Dictionary<string, string>.Enumerator enumerator;
				if (keyValuePair.Key == "tags")
				{
					FontRes fontRes3 = fontRes;
					KeyValuePair<string, string> keyValuePair2 = enumerator.Current;
					fontRes3.mTags = keyValuePair2.Value;
				}
				KeyValuePair<string, string> keyValuePair3 = enumerator.Current;
				if (keyValuePair3.Key == "isDefault")
				{
					fontRes.mDefault = true;
				}
			}
			if (fontRes.mPath.Substring(0, 5) == "!sys:")
			{
				fontRes.mSysFont = true;
				fontRes.mPath = fontRes.mPath.Substring(5);
				Dictionary<string, string>.Enumerator enumerator = theElement.mAttributes.GetEnumerator();
				fontRes.mSize = -1;
				fontRes.mBold = false;
				fontRes.mItalic = false;
				fontRes.mShadow = false;
				fontRes.mUnderline = false;
				while (enumerator.MoveNext())
				{
					KeyValuePair<string, string> keyValuePair4 = enumerator.Current;
					if (keyValuePair4.Key == "size")
					{
						FontRes fontRes4 = fontRes;
						KeyValuePair<string, string> keyValuePair5 = enumerator.Current;
						fontRes4.mSize = Convert.ToInt32(keyValuePair5.Value, 10);
					}
					else
					{
						KeyValuePair<string, string> keyValuePair6 = enumerator.Current;
						if (keyValuePair6.Key == "bold")
						{
							fontRes.mBold = true;
						}
						else
						{
							KeyValuePair<string, string> keyValuePair7 = enumerator.Current;
							if (keyValuePair7.Key == "italic")
							{
								fontRes.mBold = true;
							}
							else
							{
								KeyValuePair<string, string> keyValuePair8 = enumerator.Current;
								if (keyValuePair8.Key == "shadow")
								{
									fontRes.mBold = true;
								}
								else
								{
									KeyValuePair<string, string> keyValuePair9 = enumerator.Current;
									if (keyValuePair9.Key == "underline")
									{
										fontRes.mBold = true;
									}
								}
							}
						}
					}
				}
				if (fontRes.mSize <= 0)
				{
					return this.Fail("SysFont needs point size");
				}
			}
			else
			{
				fontRes.mSysFont = false;
			}
			return true;
		}

		public bool ParseSoundResource(XMLElement theElement)
		{
			SoundRes soundRes = new SoundRes();
			if (!this.ParseCommonResource(theElement, soundRes, this.mSoundMap))
			{
				if (!this.mHadAlreadyDefinedError || !this.mAllowAlreadyDefinedResources)
				{
					soundRes.DeleteResource();
					return false;
				}
				this.mError = "";
				this.mHasFailed = false;
				SoundRes soundRes2 = soundRes;
				soundRes = (SoundRes)this.mSoundMap[soundRes2.mId];
				soundRes.mPath = soundRes2.mPath;
				soundRes.mXMLAttributes = soundRes2.mXMLAttributes;
				soundRes2.DeleteResource();
			}
			soundRes.mSoundId = -1;
			soundRes.mVolume = -1.0;
			soundRes.mPanning = 0;
			foreach (KeyValuePair<string, string> keyValuePair in theElement.mAttributes)
			{
				Dictionary<string, string>.Enumerator enumerator;
				if (keyValuePair.Key == "volume")
				{
					SoundRes soundRes3 = soundRes;
					KeyValuePair<string, string> keyValuePair2 = enumerator.Current;
					soundRes3.mVolume = Convert.ToDouble(keyValuePair2.Value);
				}
				KeyValuePair<string, string> keyValuePair3 = enumerator.Current;
				if (keyValuePair3.Key == "pan")
				{
					SoundRes soundRes4 = soundRes;
					KeyValuePair<string, string> keyValuePair4 = enumerator.Current;
					soundRes4.mPanning = Convert.ToInt32(keyValuePair4.Value);
				}
			}
			return true;
		}

		public bool ParseMusicResource(XMLElement theElement)
		{
			MusicRes musicRes = new MusicRes();
			if (!this.ParseCommonResource(theElement, musicRes, this.mMusicMap))
			{
				if (!this.mHadAlreadyDefinedError || !this.mAllowAlreadyDefinedResources)
				{
					musicRes.DeleteResource();
					return false;
				}
				this.mError = "";
				this.mHasFailed = false;
				MusicRes musicRes2 = musicRes;
				musicRes = (MusicRes)this.mMusicMap[musicRes2.mId];
				musicRes.mPath = musicRes2.mPath;
				musicRes.mXMLAttributes = musicRes2.mXMLAttributes;
				musicRes2.DeleteResource();
			}
			musicRes.mSongId = -1;
			return true;
		}

		public bool ParseLevelResource(XMLElement theElement)
		{
			LevelRes levelRes = new LevelRes();
			if (!this.ParseCommonResource(theElement, levelRes, this.mLevelMap))
			{
				if (!this.mHadAlreadyDefinedError || !this.mAllowAlreadyDefinedResources)
				{
					levelRes.DeleteResource();
					return false;
				}
				this.mError = "";
				this.mHasFailed = false;
				LevelRes levelRes2 = levelRes;
				levelRes = (LevelRes)this.mLevelMap[levelRes2.mId];
				levelRes.mPath = levelRes2.mPath;
				levelRes.mXMLAttributes = levelRes2.mXMLAttributes;
				levelRes2.DeleteResource();
			}
			levelRes.mLevelNumber = Convert.ToInt32(levelRes.mId);
			this.LevelResourceParsedGameSpecific(levelRes);
			return true;
		}

		public bool ParseImageResource(XMLElement theElement)
		{
			ImageRes imageRes = new ImageRes();
			if (!this.ParseCommonResource(theElement, imageRes, this.mImageMap))
			{
				if (!this.mHadAlreadyDefinedError || !this.mAllowAlreadyDefinedResources)
				{
					imageRes.DeleteResource();
					return false;
				}
				this.mError = "";
				this.mHasFailed = false;
				ImageRes imageRes2 = imageRes;
				imageRes = (ImageRes)this.mImageMap[imageRes2.mId];
				imageRes.mPath = imageRes2.mPath;
				imageRes.mXMLAttributes = imageRes2.mXMLAttributes;
				imageRes2.DeleteResource();
			}
			imageRes.mPalletize = !theElement.mAttributes.ContainsKey("nopal");
			imageRes.mA4R4G4B4 = theElement.mAttributes.ContainsKey("a4r4g4b4");
			imageRes.mDDSurface = theElement.mAttributes.ContainsKey("ddsurface");
			imageRes.mPurgeBits = theElement.mAttributes.ContainsKey("nobits") || (this.mApp.Is3DAccelerated() && theElement.mAttributes.ContainsKey("nobits3d")) || (!this.mApp.Is3DAccelerated() && theElement.mAttributes.ContainsKey("nobits2d"));
			imageRes.mA8R8G8B8 = theElement.mAttributes.ContainsKey("a8r8g8b8");
			imageRes.mR5G6B5 = theElement.mAttributes.ContainsKey("r5g6b5");
			imageRes.mA1R5G5B5 = theElement.mAttributes.ContainsKey("a1r5g5b5");
			imageRes.mMinimizeSubdivisions = theElement.mAttributes.ContainsKey("minsubdivide");
			imageRes.mAutoFindAlpha = !theElement.mAttributes.ContainsKey("noalpha");
			Dictionary<string, string>.Enumerator enumerator = theElement.mAttributes.GetEnumerator();
			imageRes.mAlphaColor = 16777215U;
			imageRes.mRows = 1;
			imageRes.mCols = 1;
			AnimType animType = AnimType.AnimType_None;
			while (enumerator.MoveNext())
			{
				KeyValuePair<string, string> keyValuePair = enumerator.Current;
				if (keyValuePair.Key == "alphaimage")
				{
					ImageRes imageRes3 = imageRes;
					string text = this.mDefaultPath;
					KeyValuePair<string, string> keyValuePair2 = enumerator.Current;
					imageRes3.mAlphaImage = text + keyValuePair2.Value;
				}
				else
				{
					KeyValuePair<string, string> keyValuePair3 = enumerator.Current;
					if (keyValuePair3.Key == "alphacolor")
					{
						ImageRes imageRes4 = imageRes;
						KeyValuePair<string, string> keyValuePair4 = enumerator.Current;
						imageRes4.mAlphaColor = Convert.ToUInt32(keyValuePair4.Value);
					}
					else
					{
						KeyValuePair<string, string> keyValuePair5 = enumerator.Current;
						if (keyValuePair5.Key == "variant")
						{
							ImageRes imageRes5 = imageRes;
							KeyValuePair<string, string> keyValuePair6 = enumerator.Current;
							imageRes5.mVariant = keyValuePair6.Value;
						}
						else
						{
							KeyValuePair<string, string> keyValuePair7 = enumerator.Current;
							if (keyValuePair7.Key == "alphagrid")
							{
								ImageRes imageRes6 = imageRes;
								string text2 = this.mDefaultPath;
								KeyValuePair<string, string> keyValuePair8 = enumerator.Current;
								imageRes6.mAlphaGridImage = text2 + keyValuePair8.Value;
							}
							else
							{
								KeyValuePair<string, string> keyValuePair9 = enumerator.Current;
								if (keyValuePair9.Key == "rows")
								{
									ImageRes imageRes7 = imageRes;
									KeyValuePair<string, string> keyValuePair10 = enumerator.Current;
									imageRes7.mRows = Convert.ToInt32(keyValuePair10.Value);
								}
								else
								{
									KeyValuePair<string, string> keyValuePair11 = enumerator.Current;
									if (keyValuePair11.Key == "cols")
									{
										ImageRes imageRes8 = imageRes;
										KeyValuePair<string, string> keyValuePair12 = enumerator.Current;
										imageRes8.mCols = Convert.ToInt32(keyValuePair12.Value);
									}
									else
									{
										KeyValuePair<string, string> keyValuePair13 = enumerator.Current;
										if (keyValuePair13.Key == "PNG")
										{
											ImageRes imageRes9 = imageRes;
											KeyValuePair<string, string> keyValuePair14 = enumerator.Current;
											imageRes9.mIsPNG = Convert.ToBoolean(keyValuePair14.Value);
										}
										else
										{
											KeyValuePair<string, string> keyValuePair15 = enumerator.Current;
											if (keyValuePair15.Key == "Premultiply")
											{
												ImageRes imageRes10 = imageRes;
												KeyValuePair<string, string> keyValuePair16 = enumerator.Current;
												imageRes10.mNeedsPremultiply = Convert.ToBoolean(keyValuePair16.Value);
											}
											else
											{
												KeyValuePair<string, string> keyValuePair17 = enumerator.Current;
												if (keyValuePair17.Key == "anim")
												{
													KeyValuePair<string, string> keyValuePair18 = enumerator.Current;
													string value = keyValuePair18.Value;
													if (value == "none")
													{
														animType = AnimType.AnimType_None;
													}
													else if (value == "once")
													{
														animType = AnimType.AnimType_Once;
													}
													else if (value == "loop")
													{
														animType = AnimType.AnimType_Loop;
													}
													else
													{
														if (!(value == "pingpong"))
														{
															this.Fail("Invalid animation type.");
															return false;
														}
														animType = AnimType.AnimType_PingPong;
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			imageRes.mAnimInfo.mAnimType = animType;
			if (animType != AnimType.AnimType_None)
			{
				int theNumCels = Math.Max(imageRes.mRows, imageRes.mCols);
				int theBeginFrameTime = 0;
				int theEndFrameTime = 0;
				foreach (KeyValuePair<string, string> keyValuePair19 in theElement.mAttributes)
				{
					if (keyValuePair19.Key == "framedelay")
					{
						AnimInfo mAnimInfo = imageRes.mAnimInfo;
						KeyValuePair<string, string> keyValuePair20 = enumerator.Current;
						mAnimInfo.mFrameDelay = Convert.ToInt32(keyValuePair20.Value);
					}
					else
					{
						KeyValuePair<string, string> keyValuePair21 = enumerator.Current;
						if (keyValuePair21.Key == "begindelay")
						{
							KeyValuePair<string, string> keyValuePair22 = enumerator.Current;
							theBeginFrameTime = Convert.ToInt32(keyValuePair22.Value);
						}
						else
						{
							KeyValuePair<string, string> keyValuePair23 = enumerator.Current;
							if (keyValuePair23.Key == "enddelay")
							{
								KeyValuePair<string, string> keyValuePair24 = enumerator.Current;
								theEndFrameTime = Convert.ToInt32(keyValuePair24.Value);
							}
							else
							{
								KeyValuePair<string, string> keyValuePair25 = enumerator.Current;
								if (keyValuePair25.Key == "perframedelay")
								{
									KeyValuePair<string, string> keyValuePair26 = enumerator.Current;
									ResourceManager.ReadIntVector(keyValuePair26.Value, ref imageRes.mAnimInfo.mPerFrameDelay);
								}
								else
								{
									KeyValuePair<string, string> keyValuePair27 = enumerator.Current;
									if (keyValuePair27.Key == "framemap")
									{
										KeyValuePair<string, string> keyValuePair28 = enumerator.Current;
										ResourceManager.ReadIntVector(keyValuePair28.Value, ref imageRes.mAnimInfo.mFrameMap);
									}
								}
							}
						}
					}
				}
				imageRes.mAnimInfo.Compute(theNumCels, theBeginFrameTime, theEndFrameTime);
			}
			return true;
		}

		private static void ReadIntVector(string theVal, ref List<int> theVector)
		{
			theVector.Clear();
			char[] array = new char[] { ',' };
			string[] array2 = theVal.Split(array);
			for (int i = 0; i < array2.Length; i++)
			{
				theVector.Add(Convert.ToInt32(array2[i]));
			}
		}

		public Font GetFontThrow(string theRes)
		{
			if (this.mFontMap.ContainsKey(theRes))
			{
				return ((FontRes)this.mFontMap[theRes]).mFont;
			}
			return null;
		}

		public Image GetImageThrow(string theRes)
		{
			if (this.mImageMap.ContainsKey(theRes))
			{
				return ((ImageRes)this.mImageMap[theRes]).mImage;
			}
			return null;
		}

		public int GetSoundThrow(string theRes)
		{
			if (this.mSoundMap.ContainsKey(theRes))
			{
				return ((SoundRes)this.mSoundMap[theRes]).mSoundId;
			}
			return -1;
		}

		public int GetMusicThrow(string theRes)
		{
			if (this.mMusicMap.ContainsKey(theRes))
			{
				return ((MusicRes)this.mMusicMap[theRes]).mSongId;
			}
			return -1;
		}

		private int GetNumResources(string theResGroup, Dictionary<string, BaseRes> theResMap)
		{
			if (string.IsNullOrEmpty(theResGroup))
			{
				return theResMap.Count;
			}
			int num = 0;
			foreach (KeyValuePair<string, BaseRes> keyValuePair in theResMap)
			{
				BaseRes value = keyValuePair.Value;
				if (value.mResGroup == theResGroup && !value.mFromProgram)
				{
					num++;
				}
			}
			return num;
		}

		private int GetNumResources(string theResGroup)
		{
			return this.GetNumImages(theResGroup) + this.GetNumSounds(theResGroup) + this.GetNumFonts(theResGroup) + this.GetNumSongs(theResGroup) + this.GetNumLevels(theResGroup);
		}

		private int GetNumLevels(string theResGroup)
		{
			return this.GetNumResources(theResGroup, this.mLevelMap);
		}

		private int GetNumImages(string theResGroup)
		{
			return this.GetNumResources(theResGroup, this.mImageMap);
		}

		private int GetNumSounds(string theResGroup)
		{
			return this.GetNumResources(theResGroup, this.mSoundMap);
		}

		private int GetNumFonts(string theResGroup)
		{
			return this.GetNumResources(theResGroup, this.mFontMap);
		}

		private int GetNumSongs(string theResGroup)
		{
			return this.GetNumResources(theResGroup, this.mMusicMap);
		}

		public void LoadAllResources()
		{
			this.mTotalResources = this.GetNumResources("");
			this.mLoadedCount = 0;
			this.mProgress = 0.0;
			foreach (KeyValuePair<string, List<BaseRes>> keyValuePair in this.mResGroupMap)
			{
				Dictionary<string, List<BaseRes>>.Enumerator enumerator;
				if (keyValuePair.Key == "Levels")
				{
					KeyValuePair<string, List<BaseRes>> keyValuePair2 = enumerator.Current;
					using (List<BaseRes>.Enumerator enumerator2 = keyValuePair2.Value.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							BaseRes baseRes = enumerator2.Current;
							this.mLoadedCount++;
							Image image;
							this.LoadLevelBackgrounds(int.Parse(baseRes.mId), -1, out image, out image);
						}
						continue;
					}
				}
				KeyValuePair<string, List<BaseRes>> keyValuePair3 = enumerator.Current;
				this.StartLoadResources(keyValuePair3.Key);
				KeyValuePair<string, List<BaseRes>> keyValuePair4 = enumerator.Current;
				this.LoadResources(keyValuePair4.Key);
				if (this.HadError())
				{
					return;
				}
			}
		}

		private void StartLoadResources(string theResGroup)
		{
			this.mError = "";
			this.mHasFailed = false;
			this.mCurResGroup = theResGroup;
			this.mCurResGroupList = this.mResGroupMap[theResGroup];
			this.mCurResGroupListItr = this.mCurResGroupList.GetEnumerator();
		}

		private bool LoadResources(string theResGroup)
		{
			this.mError = "";
			this.mHasFailed = false;
			this.StartLoadResources(theResGroup);
			while (this.LoadNextResource())
			{
			}
			if (!this.HadError())
			{
				this.mLoadedGroups.Add(theResGroup);
				return true;
			}
			return false;
		}

		private bool LoadNextResource()
		{
			if (this.HadError())
			{
				return false;
			}
			if (this.mCurResGroupList == null)
			{
				return false;
			}
			bool flag = false;
			bool flag2 = false;
			while (this.mCurResGroupListItr.MoveNext())
			{
				BaseRes baseRes = this.mCurResGroupListItr.Current;
				if (!baseRes.mFromProgram)
				{
					switch (baseRes.mType)
					{
					case ResType.ResType_Image:
					{
						ImageRes imageRes = (ImageRes)baseRes;
						if (imageRes.mImage != null)
						{
							continue;
						}
						flag = this.DoLoadImage(imageRes);
						flag2 = true;
						break;
					}
					case ResType.ResType_Sound:
					{
						SoundRes soundRes = (SoundRes)baseRes;
						if (soundRes.mSoundId != -1)
						{
							continue;
						}
						flag = this.DoLoadSound(soundRes);
						flag2 = true;
						break;
					}
					case ResType.ResType_Font:
					{
						FontRes fontRes = (FontRes)baseRes;
						if (fontRes.mFont != null)
						{
							continue;
						}
						flag = this.DoLoadFont(fontRes);
						flag2 = true;
						break;
					}
					case ResType.ResType_Music:
					{
						MusicRes musicRes = (MusicRes)baseRes;
						if (musicRes.mSongId != -1)
						{
							continue;
						}
						flag = this.DoLoadMusic(musicRes);
						flag2 = true;
						break;
					}
					}
					if (flag2)
					{
						break;
					}
				}
			}
			if (flag)
			{
				this.mLoadedCount++;
				this.mProgress = (double)this.mLoadedCount / (double)this.mTotalResources;
				Debug.OutputDebug<string>(this.mProgress.ToString());
			}
			return flag;
		}

		public bool LoadLevelBackgrounds(int levelNumber, int nextLevelNumber, out Image verticalBackground, out Image horizontalBackground)
		{
			bool result = false;
			lock (this.backdropLocker)
			{
				string text = levelNumber.ToString();
				if (this.mLevelMap.ContainsKey(text))
				{
					LevelRes levelRes = (LevelRes)this.mLevelMap[text];
					verticalBackground = new Image(this.currentBackDropContentManager.Load<Texture2D>(levelRes.mPath + "portrait" + levelRes.mId));
					horizontalBackground = new Image(this.currentBackDropContentManager.Load<Texture2D>(levelRes.mPath + "landscape" + levelRes.mId));
					this.loadedCurrentBackdrop = levelNumber;
					result = true;
				}
				else
				{
					verticalBackground = null;
					horizontalBackground = null;
				}
			}
			return result;
		}

		private void PremultiplyPixel(ref Color c)
		{
			c.R = c.R * c.A / byte.MaxValue;
			c.G = c.G * c.A / byte.MaxValue;
			c.B = c.B * c.A / byte.MaxValue;
		}

		private Texture2D LoadTextureFromStream(string filename, bool premultiply)
		{
			Texture2D texture2D = null;
			GraphicsDevice graphicsDevice = GlobalStaticVars.g.GraphicsDevice;
			using (Stream stream = TitleContainer.OpenStream("Content\\" + filename + ".png"))
			{
				texture2D = Texture2D.FromStream(graphicsDevice, stream);
			}
			bool flag = false;
			if (!premultiply)
			{
				return texture2D;
			}
			if (!flag)
			{
				Color[] array = new Color[texture2D.Width * texture2D.Height];
				texture2D.GetData<Color>(array);
				for (int i = 0; i < array.Length; i++)
				{
					this.PremultiplyPixel(ref array[i]);
				}
				texture2D.SetData<Color>(array);
			}
			return texture2D;
		}

		private bool DoLoadImage(ImageRes theRes)
		{
			Texture2D texture2D;
			try
			{
				if (theRes.mIsPNG)
				{
					texture2D = this.LoadTextureFromStream(theRes.mPath, theRes.mNeedsPremultiply);
				}
				else
				{
					texture2D = this.mContentManager.Load<Texture2D>(theRes.mPath);
				}
			}
			catch (Exception ex)
			{
				string message = ex.Message;
				return this.Fail("Failed to load image: " + theRes.mPath);
			}
			theRes.mImage = new Image(texture2D, 0, 0, texture2D.Width, texture2D.Height);
			if (theRes.mAnimInfo.mAnimType != AnimType.AnimType_None)
			{
				theRes.mImage.mAnimInfo = new AnimInfo(theRes.mAnimInfo);
			}
			theRes.mImage.mNumRows = theRes.mRows;
			theRes.mImage.mNumCols = theRes.mCols;
			this.ResourceLoadedHook(theRes);
			return true;
		}

		private bool DoLoadFont(FontRes fontRes)
		{
			Font font = new Font();
			if (fontRes.mSysFont)
			{
				return this.Fail("SysFont not supported");
			}
			if (fontRes.mDefault)
			{
				font.AddLayer(this.mContentManager.Load<SpriteFont>("fonts/Arial"));
				fontRes.mFont = font;
				return true;
			}
			XmlReader xmlReader = XmlReader.Create(TitleContainer.OpenStream(fontRes.mPath));
			xmlReader.Read();
			while (xmlReader.Read())
			{
				if (xmlReader.NodeType == 1)
				{
					if (xmlReader.Name == "Offsets")
					{
						Vector2 zero = Vector2.Zero;
						while (xmlReader.NodeType != 15 || !(xmlReader.Name == "Offsets"))
						{
							if (xmlReader.NodeType == 1 && xmlReader.Name == "Offset")
							{
								string text = xmlReader[ResourceManager.offsetX];
								string text2 = xmlReader[ResourceManager.offsetY];
								char c = xmlReader[ResourceManager.character].ToCharArray()[0];
								if (!string.IsNullOrEmpty(text))
								{
									zero.X = (float)Convert.ToInt32(text);
								}
								if (!string.IsNullOrEmpty(text2))
								{
									zero.Y = (float)Convert.ToInt32(text2);
								}
								font.AddCharacterOffset(c, zero);
							}
							xmlReader.Read();
						}
					}
					if (xmlReader.Name == "Layers")
					{
						string text3 = xmlReader["magic"];
						if (!string.IsNullOrEmpty(text3))
						{
							font.characterOffsetMagic = Convert.ToInt32(text3);
						}
						else
						{
							font.characterOffsetMagic = 0;
						}
						Vector2 zero2 = Vector2.Zero;
						while (xmlReader.NodeType != 15 || !(xmlReader.Name == "Layers"))
						{
							if (xmlReader.NodeType == 1 && xmlReader.Name == "Layer")
							{
								if (!xmlReader.HasAttributes)
								{
									zero2 = Vector2.Zero;
								}
								else
								{
									string text4 = xmlReader["xOffset"];
									string text5 = xmlReader["yOffset"];
									if (!string.IsNullOrEmpty(text4))
									{
										zero2.X = (float)Convert.ToInt32(text4);
									}
									if (!string.IsNullOrEmpty(text5))
									{
										zero2.Y = (float)Convert.ToInt32(text5);
									}
								}
							}
							if (xmlReader.NodeType == 3)
							{
								font.AddLayer(this.mContentManager.Load<SpriteFont>(xmlReader.Value), zero2);
							}
							xmlReader.Read();
						}
					}
					if (xmlReader.Name == "Ascent")
					{
						xmlReader.Read();
						font.mAscent = -1 * Convert.ToInt32(xmlReader.Value);
					}
					if (xmlReader.Name == "Height")
					{
						xmlReader.Read();
						font.mHeight = -1 * Convert.ToInt32(xmlReader.Value);
					}
					if (xmlReader.Name == "SpaceChar")
					{
						xmlReader.Read();
						font.SpaceChar = xmlReader.Value;
					}
					if (xmlReader.Name == "StringWidthCaching")
					{
						xmlReader.Read();
						font.StringWidthCachingEnabled = Convert.ToBoolean(xmlReader.Value);
					}
				}
			}
			fontRes.mFont = font;
			this.ResourceLoadedHook(fontRes);
			return true;
		}

		private bool DoLoadSound(SoundRes soundRes)
		{
			int freeSoundId = this.mApp.mSoundManager.GetFreeSoundId();
			if (freeSoundId < 0)
			{
				return this.Fail("Out of free sound ids");
			}
			if (!this.mApp.mSoundManager.LoadSound((uint)freeSoundId, soundRes.mPath))
			{
				return this.Fail("Failed to load sound: " + soundRes.mPath);
			}
			if (soundRes.mVolume >= 0.0)
			{
				this.mApp.mSoundManager.SetBaseVolume((uint)freeSoundId, soundRes.mVolume);
			}
			if (soundRes.mPanning != 0)
			{
				this.mApp.mSoundManager.SetBasePan((uint)freeSoundId, soundRes.mPanning);
			}
			soundRes.mSoundId = freeSoundId;
			this.ResourceLoadedHook(soundRes);
			return true;
		}

		private bool DoLoadMusic(MusicRes musicRes)
		{
			int freeMusicId = this.mApp.mMusicInterface.GetFreeMusicId();
			if (freeMusicId < 0)
			{
				return this.Fail("Out of free song ids");
			}
			if (!this.mApp.mMusicInterface.LoadMusic(freeMusicId, musicRes.mPath))
			{
				return this.Fail("Failed to load song: " + musicRes.mPath);
			}
			musicRes.mSongId = freeMusicId;
			this.ResourceLoadedHook(musicRes);
			return true;
		}

		private void ResourceLoadedHook(BaseRes theRes)
		{
		}

		private bool Fail(string theErrorText)
		{
			if (!this.mHasFailed)
			{
				this.mHasFailed = true;
				if (this.mXMLParser == null)
				{
					this.mError = theErrorText;
					return false;
				}
				int currentLineNum = this.mXMLParser.GetCurrentLineNum();
				this.mError = theErrorText;
				if (currentLineNum > 0)
				{
					this.mError = this.mError + " on Line " + currentLineNum.ToString();
				}
				if (!string.IsNullOrEmpty(this.mXMLParser.GetFileName()))
				{
					this.mError = this.mError + " in File '" + this.mXMLParser.GetFileName() + "'";
				}
			}
			return false;
		}

		private void LevelResourceParsedGameSpecific(LevelRes aRes)
		{
			this.NumberOfBackdrops = (GameApp.NumberOfBackdrops = Math.Max(aRes.mLevelNumber, GameApp.NumberOfBackdrops));
		}

		public double mProgress;

		private int mTotalResources;

		private int mLoadedCount;

		private Dictionary<string, BaseRes> mImageMap;

		private Dictionary<string, BaseRes> mSoundMap;

		private Dictionary<string, BaseRes> mFontMap;

		private Dictionary<string, BaseRes> mMusicMap;

		private Dictionary<string, BaseRes> mLevelMap;

		private string mError;

		private bool mHasFailed;

		private SexyAppBase mApp;

		private string mCurResGroup;

		private string mDefaultPath;

		private string mDefaultIdPrefix;

		private XMLParser mXMLParser;

		private bool mAllowMissingProgramResources;

		private bool mAllowAlreadyDefinedResources;

		private bool mHadAlreadyDefinedError;

		private Dictionary<string, List<BaseRes>> mResGroupMap;

		private List<BaseRes> mCurResGroupList;

		private List<BaseRes>.Enumerator mCurResGroupListItr;

		private List<string> mLoadedGroups;

		private ContentManager mContentManager;

		private object backdropLocker = new object();

		private int NumberOfBackdrops;

		private ContentManager currentBackDropContentManager;

		private int loadedCurrentBackdrop = -1;

		private int loadedNextBackDrop = -1;

		private SpriteBatch imageLoadSpritebatch = new SpriteBatch(GlobalStaticVars.g.GraphicsDevice);

		private BlendState imageLoadBlendAlpha = new BlendState
		{
			ColorWriteChannels = 8,
			AlphaDestinationBlend = 1,
			ColorDestinationBlend = 1,
			AlphaSourceBlend = 0,
			ColorSourceBlend = 0
		};

		private BlendState blendColorLoadState = new BlendState
		{
			ColorWriteChannels = 7,
			AlphaDestinationBlend = 1,
			ColorDestinationBlend = 1,
			AlphaSourceBlend = 4,
			ColorSourceBlend = 4
		};

		private static string offsetX = "offsetX";

		private static string offsetY = "offsetY";

		private static string character = "character";
	}
}
