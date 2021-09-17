using System;
using System.Collections.Generic;
using System.Globalization;
using JeffLib;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Resource;

namespace ZumasRevenge
{
	public class Credits
	{
		public Credits(bool isFromMainMenu)
		{
			this.mYScrollAmt = 0f;
			this.mAlpha = 0f;
			this.mTitleFont = null;
			this.mNameFont = null;
			this.mSpaceAfterTitle = 0;
			this.mSpaceAfterName = 0;
			this.mSpaceAfterImage = 0;
			this.mScrollSpeed = 0f;
			this.mFFAlpha = 0f;
			this.mInitialDelay = 0;
			this.mSpeedUp = false;
			this.mFromMainMenu = isFromMainMenu;
			this.mEntries = new List<Credits.CreditEntry>();
			this.FONT_SHAGLOUNGE28_SHADOW = Res.GetFontByID(ResID.FONT_SHAGLOUNGE28_SHADOW);
			this.IMAGE_CREDITS_IMAGES_POLAROID = Res.GetImageByID(ResID.IMAGE_CREDITS_IMAGES_POLAROID);
		}

		public virtual void Dispose()
		{
			if (GameApp.gApp.mResourceManager.IsGroupLoaded("Credits"))
			{
				GameApp.gApp.mResourceManager.DeleteResources("Credits");
			}
		}

		private bool GetAttribute(XMLElement elem, string theName, ref string theValue)
		{
			if (elem.GetAttributeMap().ContainsKey(theName))
			{
				theValue = elem.GetAttributeMap()[theName];
				return true;
			}
			return false;
		}

		public void Init(bool advmode)
		{
			if (!GameApp.gApp.mResourceManager.IsGroupLoaded("Credits"))
			{
				GameApp.gApp.mResourceManager.LoadResources("Credits");
			}
			XMLParser xmlparser = new XMLParser();
			string languageSuffix = Localization.GetLanguageSuffix(Localization.GetCurrentLanguage());
			string theFilename = "properties/credits/credits" + languageSuffix + ".xml";
			xmlparser.OpenFile(theFilename);
			XMLElement xmlelement = new XMLElement();
			while (!xmlparser.HasFailed() && xmlparser.NextElement(xmlelement))
			{
				if (xmlelement.mType == XMLElement.XMLElementType.TYPE_START)
				{
					if (xmlelement.mValue.ToString() != this._S("Credits"))
					{
						break;
					}
					while (xmlparser.NextElement(xmlelement))
					{
						if (xmlelement.mType == XMLElement.XMLElementType.TYPE_START)
						{
							if (this.StrEquals(xmlelement.mValue.ToString(), this._S("Defaults")))
							{
								string text = "";
								if (this.GetAttribute(xmlelement, this._S("SpaceAfterTitle"), ref text))
								{
									this.mSpaceAfterTitle = this.StrToInt(text);
								}
								if (this.GetAttribute(xmlelement, this._S("SpaceAfterName"), ref text))
								{
									this.mSpaceAfterName = this.StrToInt(text);
								}
								if (this.GetAttribute(xmlelement, this._S("SpaceAfterPic"), ref text))
								{
									this.mSpaceAfterImage = this.StrToInt(text);
								}
								if (this.GetAttribute(xmlelement, this._S("ScrollSpeed"), ref text))
								{
									this.mScrollSpeed = Common._DS(this.StrToFloat(text));
								}
								if (this.GetAttribute(xmlelement, this._S("TitleColor"), ref text))
								{
									this.mTitleColor = new Color((int)Common.StrToHex(this.ToString(text)));
								}
								if (this.GetAttribute(xmlelement, this._S("NameColor"), ref text))
								{
									this.mNameColor = new Color((int)Common.StrToHex(this.ToString(text)));
								}
								if (this.GetAttribute(xmlelement, this._S("TitleFont"), ref text))
								{
									this.mTitleFont = GameApp.gApp.mResourceManager.LoadFont(text);
								}
								if (this.GetAttribute(xmlelement, this._S("NameFont"), ref text))
								{
									this.mNameFont = GameApp.gApp.mResourceManager.LoadFont(text);
								}
							}
							else if (this.StrEquals(xmlelement.mValue.ToString(), this._S("Text")))
							{
								string text2 = "";
								Credits.CreditEntry creditEntry = new Credits.CreditEntry();
								creditEntry.mSpaceAfterTitle = this.mSpaceAfterTitle;
								creditEntry.mSpaceAfterName = this.mSpaceAfterName;
								creditEntry.mSpaceAfterImage = this.mSpaceAfterImage;
								creditEntry.mTitleColor = this.mTitleColor;
								creditEntry.mNameColor = this.mNameColor;
								creditEntry.mTitleFont = this.mTitleFont;
								creditEntry.mNameFont = this.mNameFont;
								if (this.GetAttribute(xmlelement, this._S("mode"), ref text2))
								{
									creditEntry.mAlwaysShow = false;
									creditEntry.mAdvMode = this.StrEquals(text2, this._S("adventure"));
								}
								if (creditEntry.mAdvMode == advmode || creditEntry.mAlwaysShow)
								{
									if (this.GetAttribute(xmlelement, this._S("Title"), ref text2))
									{
										creditEntry.mTitle = text2;
									}
									if (this.GetAttribute(xmlelement, this._S("Name"), ref text2))
									{
										creditEntry.mName = text2;
									}
									if (this.GetAttribute(xmlelement, this._S("TitleFont"), ref text2))
									{
										creditEntry.mTitleFont = GameApp.gApp.mResourceManager.LoadFont(text2);
									}
									if (this.GetAttribute(xmlelement, this._S("NameFont"), ref text2))
									{
										creditEntry.mNameFont = GameApp.gApp.mResourceManager.LoadFont(text2);
									}
									if (this.GetAttribute(xmlelement, this._S("YOff"), ref text2))
									{
										creditEntry.mYOff = this.StrToInt(text2);
									}
									if (this.GetAttribute(xmlelement, this._S("XCenterOff"), ref text2))
									{
										creditEntry.mXCenterOff = Common._S(this.StrToInt(text2));
									}
									if (this.GetAttribute(xmlelement, this._S("TitleColor"), ref text2))
									{
										creditEntry.mTitleColor = new Color((int)Common.StrToHex(this.ToString(text2)));
									}
									if (this.GetAttribute(xmlelement, this._S("NameColor"), ref text2))
									{
										creditEntry.mNameColor = new Color((int)Common.StrToHex(this.ToString(text2)));
									}
									if (this.GetAttribute(xmlelement, this._S("SpaceAfterTitle"), ref text2))
									{
										creditEntry.mSpaceAfterTitle = this.StrToInt(text2);
									}
									if (this.GetAttribute(xmlelement, this._S("SpaceAfterName"), ref text2))
									{
										creditEntry.mSpaceAfterName = this.StrToInt(text2);
									}
									if (this.GetAttribute(xmlelement, this._S("SpaceAfterPic"), ref text2))
									{
										creditEntry.mSpaceAfterImage = this.StrToInt(text2);
									}
									this.mEntries.Add(creditEntry);
								}
							}
							else if (this.StrEquals(xmlelement.mValue.ToString(), this._S("Image")))
							{
								string text3 = "";
								Credits.CreditEntry creditEntry2 = new Credits.CreditEntry();
								if (this.GetAttribute(xmlelement, this._S("resid"), ref text3))
								{
									creditEntry2.mImage = GameApp.gApp.mResourceManager.LoadImage(text3).GetImage();
								}
								if (this.GetAttribute(xmlelement, this._S("YOff"), ref text3))
								{
									creditEntry2.mYOff = this.StrToInt(text3);
								}
								if (this.GetAttribute(xmlelement, this._S("xflip"), ref text3))
								{
									creditEntry2.mXFlip = this.StrToBool(text3);
								}
								if (this.GetAttribute(xmlelement, this._S("polaroid"), ref text3))
								{
									creditEntry2.mDoPolaroid = this.StrToBool(text3);
									if (!creditEntry2.mDoPolaroid)
									{
										creditEntry2.mImgAlpha = 255f;
									}
								}
								if (this.GetAttribute(xmlelement, this._S("SpaceAfterPic"), ref text3))
								{
									creditEntry2.mSpaceAfterImage = this.StrToInt(text3);
								}
								if (this.GetAttribute(xmlelement, this._S("x"), ref text3))
								{
									if (this.StrEquals(text3, this._S("center")))
									{
										creditEntry2.mXCenterOff = -creditEntry2.mImage.mWidth / 2;
									}
									else
									{
										creditEntry2.mXCenterOff = -GameApp.gApp.mWidth / 2 + Common._S(this.StrToInt(text3));
									}
								}
								this.mEntries.Add(creditEntry2);
							}
						}
					}
				}
			}
			xmlparser.CloseFile();
			int num = GameApp.gApp.mHeight;
			for (int i = 0; i < this.mEntries.Count; i++)
			{
				Credits.CreditEntry creditEntry3 = this.mEntries[i];
				num += Common._S(creditEntry3.mYOff);
				creditEntry3.mInitialY = num;
				if (creditEntry3.mImage == null)
				{
					if (creditEntry3.mTitle.Length > 0)
					{
						num += creditEntry3.mTitleFont.GetHeight() + Common._S(creditEntry3.mSpaceAfterTitle);
					}
					if (creditEntry3.mName.Length > 0)
					{
						num += creditEntry3.mNameFont.GetHeight() + Common._S(creditEntry3.mSpaceAfterName);
					}
				}
				else
				{
					num += Common._S(creditEntry3.mSpaceAfterImage) + creditEntry3.mImage.mHeight;
				}
			}
		}

		public bool AtEnd()
		{
			Credits.CreditEntry creditEntry = this.mEntries[this.mEntries.Count - 1];
			return (creditEntry.mImage != null && (float)creditEntry.mInitialY + this.mYScrollAmt <= (float)(GameApp.gApp.mHeight / 2 - creditEntry.mImage.mHeight / 2 - Common._DS(Common._M(200)))) || (creditEntry.mImage == null && (float)creditEntry.mInitialY + this.mYScrollAmt <= (float)(GameApp.gApp.mHeight / 2 - creditEntry.mTitleFont.mHeight / 2));
		}

		public void Update()
		{
			if (GameApp.gApp.IsHardwareBackButtonPressed() && !this.mFromMainMenu)
			{
				this.ProcessHardwareBackButton();
			}
			if (this.mAlpha < 255f)
			{
				this.mAlpha += Common._M(8f);
				if (this.mAlpha > 255f)
				{
					this.mAlpha = 255f;
					return;
				}
			}
			else
			{
				for (int i = 0; i < this.mEntries.Count; i++)
				{
					Credits.CreditEntry creditEntry = this.mEntries[i];
					int num = (int)((float)creditEntry.mInitialY + this.mYScrollAmt);
					if (creditEntry.mImage != null && creditEntry.mDoPolaroid && num <= Common._DS(Common._M(900)))
					{
						if (creditEntry.mImgAlpha < 255f)
						{
							creditEntry.mImgAlpha += Common._M(0.5f);
						}
						if (creditEntry.mImgAlpha > 255f)
						{
							creditEntry.mImgAlpha = 255f;
						}
					}
				}
				if (!this.AtEnd())
				{
					if (++this.mInitialDelay >= Common._M(100))
					{
						this.mYScrollAmt -= this.mScrollSpeed * (float)(this.mSpeedUp ? Common._M(4) : 1);
					}
					if (this.mInitialDelay >= Common._M(300))
					{
						this.mFFAlpha += Common._M(2f) * (float)(this.mSpeedUp ? Common._M1(4) : 1);
						if (this.mFFAlpha > 255f)
						{
							this.mFFAlpha = 255f;
							return;
						}
					}
				}
				else
				{
					this.mFFAlpha -= Common._M(2f);
					if (this.mFFAlpha < 0f)
					{
						this.mFFAlpha = 0f;
					}
				}
			}
		}

		public void Draw(Graphics g)
		{
			g.SetColor(new Color(0, 0, 0, (int)this.mAlpha));
			g.FillRect(Common._S(-80), 0, GameApp.gApp.mWidth + Common._S(160), GameApp.gApp.mHeight);
			for (int i = 0; i < this.mEntries.Count; i++)
			{
				Credits.CreditEntry creditEntry = this.mEntries[i];
				int num = (int)((float)creditEntry.mInitialY + this.mYScrollAmt);
				if (this.AtEnd() && i == this.mEntries.Count - 1)
				{
					num = ((creditEntry.mImage != null) ? ((GameApp.gApp.mHeight - creditEntry.mImage.mHeight) / 2 - Common._DS(Common._M(200))) : ((GameApp.gApp.mHeight - creditEntry.mTitleFont.mHeight) / 2));
				}
				g.PushState();
				bool flag = GameApp.gApp.mUserProfile.mAdvModeVars.mHighestZoneBeat >= 6 || !this.mFromMainMenu;
				if (flag && creditEntry.mImage != null)
				{
					if (num > -350 && num < 700)
					{
						g.PushState();
						float num2 = (float)(creditEntry.mXFlip ? Common._DS(Common._M(this.mRoll)) : 0);
						if (creditEntry.mDoPolaroid)
						{
							g.DrawImageMirror(this.IMAGE_CREDITS_IMAGES_POLAROID, (int)((float)(GameApp.gApp.mWidth / 2 + creditEntry.mXCenterOff - Common._DS(Common._M(60))) - num2), num - Common._DS(Common._M1(36)), creditEntry.mXFlip);
							g.SetColorizeImages(true);
							g.SetColor(new Color(255, 255, 255, (int)creditEntry.mImgAlpha));
						}
						g.DrawImageMirror(creditEntry.mImage, GameApp.gApp.mWidth / 2 + creditEntry.mXCenterOff, num, creditEntry.mXFlip);
						g.PopState();
					}
				}
				else if (num > -100 && num < 700)
				{
					int theAlpha = 255;
					if (creditEntry.mTitle.Length > 0)
					{
						g.SetFont(creditEntry.mTitleFont);
						g.SetColor(new Color(creditEntry.mTitleColor.mRed, creditEntry.mTitleColor.mGreen, creditEntry.mTitleColor.mBlue, theAlpha));
						g.WriteString(creditEntry.mTitle, creditEntry.mXCenterOff, num + creditEntry.mTitleFont.GetAscent(), GameApp.gApp.mWidth, 0);
						num += creditEntry.mSpaceAfterTitle + creditEntry.mTitleFont.GetHeight();
					}
					if (creditEntry.mName.Length > 0)
					{
						g.SetFont(creditEntry.mNameFont);
						g.SetColor(new Color(creditEntry.mNameColor.mRed, creditEntry.mNameColor.mGreen, creditEntry.mNameColor.mBlue, theAlpha));
						g.WriteString(creditEntry.mName, creditEntry.mXCenterOff, num + creditEntry.mNameFont.GetAscent(), GameApp.gApp.mWidth, 0);
					}
				}
				g.PopState();
			}
			g.SetFont(this.FONT_SHAGLOUNGE28_SHADOW);
			if (!this.AtEnd())
			{
				g.SetColor(new Color(Common._M(255), Common._M1(255), Common._M2(255), (int)(this.mFFAlpha * Common._M3(0.5f))));
				g.DrawString(TextManager.getInstance().getString(435), Common._DS(Common._M(750)), Common._DS(Common._M1(1176)));
				return;
			}
			g.SetColor(new Color(Common._M(255), Common._M1(255), Common._M2(255), 200));
			g.DrawString(TextManager.getInstance().getString(433), Common._DS(Common._M(750)), Common._DS(Common._M1(1176)));
		}

		public void ProcessHardwareBackButton()
		{
			GameApp.gApp.ReturnFromCredits();
			GameApp.gApp.OnHardwareBackButtonPressProcessed();
		}

		private float StrToFloat(string str)
		{
			if (str.Length == 0)
			{
				return 0f;
			}
			return float.Parse(str, 167, CultureInfo.InvariantCulture);
		}

		private int StrToInt(string str)
		{
			if (str.Length == 0)
			{
				return 0;
			}
			return int.Parse(str);
		}

		private bool StrToBool(string str)
		{
			return str.Length != 0 && bool.Parse(str);
		}

		private string ToString(string str)
		{
			return str;
		}

		private string _S(string str)
		{
			return str;
		}

		private int sexyatoi(string str)
		{
			return this.StrToInt(str);
		}

		private float sexyatof(string str)
		{
			return this.StrToFloat(str);
		}

		private bool StrEquals(string str, string cmp)
		{
			return str == cmp;
		}

		private string StringToUpper(string str)
		{
			return str.ToUpper();
		}

		private string StringToLower(string str)
		{
			return str.ToLower();
		}

		public List<Credits.CreditEntry> mEntries;

		public float mYScrollAmt;

		public float mAlpha;

		public float mFFAlpha;

		public Font mTitleFont;

		public Font mNameFont;

		public int mSpaceAfterTitle;

		public int mSpaceAfterName;

		public int mSpaceAfterImage;

		public Color mTitleColor;

		public Color mNameColor;

		public int mRoll = -12;

		public float mScrollSpeed;

		public int mInitialDelay;

		public bool mSpeedUp;

		public bool mFromMainMenu;

		public bool mTapDown;

		private Font FONT_SHAGLOUNGE28_SHADOW;

		private Image IMAGE_CREDITS_IMAGES_POLAROID;

		public class CreditEntry
		{
			public CreditEntry()
			{
				this.mImage = null;
				this.mTitleFont = null;
				this.mXFlip = false;
				this.mImgAlpha = 0f;
				this.mDoPolaroid = true;
				this.mNameFont = null;
				this.mYOff = 0;
				this.mSpaceAfterTitle = 0;
				this.mSpaceAfterName = 0;
				this.mSpaceAfterImage = 0;
				this.mAdvMode = true;
				this.mAlwaysShow = true;
				this.mXCenterOff = 0;
				this.mInitialY = 0;
				this.mTitle = "";
				this.mName = "";
			}

			public string mTitle;

			public string mName;

			public Image mImage;

			public Font mTitleFont;

			public Font mNameFont;

			public int mXCenterOff;

			public int mYOff;

			public int mInitialY;

			public int mSpaceAfterTitle;

			public int mSpaceAfterName;

			public int mSpaceAfterImage;

			public Color mTitleColor;

			public Color mNameColor;

			public bool mAdvMode;

			public bool mAlwaysShow;

			public float mImgAlpha;

			public bool mDoPolaroid;

			public bool mXFlip;
		}
	}
}
