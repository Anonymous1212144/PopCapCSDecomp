using System;
using System.Collections.Generic;

namespace SexyFramework.Resource
{
	public class PropertiesParser
	{
		protected void Fail(string theErrorText)
		{
			if (!this.mHasFailed)
			{
				this.mHasFailed = true;
				int currentLineNum = this.mXMLParser.GetCurrentLineNum();
				this.mError = theErrorText;
				if (currentLineNum > 0)
				{
					this.mError = this.mError + " on Line " + currentLineNum;
				}
				if (this.mXMLParser.GetFileName().Length <= 0)
				{
					this.mError = this.mError + " in File " + this.mXMLParser.GetFileName();
				}
			}
		}

		protected bool ParseSingleElement(out string aString)
		{
			aString = string.Empty;
			XMLElement xmlelement;
			for (;;)
			{
				xmlelement = new XMLElement();
				if (!this.mXMLParser.NextElement(xmlelement))
				{
					break;
				}
				if (xmlelement.mType == XMLElement.XMLElementType.TYPE_START)
				{
					goto Block_2;
				}
				if (xmlelement.mType == XMLElement.XMLElementType.TYPE_ELEMENT)
				{
					aString = xmlelement.mValue.ToString();
				}
				else if (xmlelement.mType == XMLElement.XMLElementType.TYPE_END)
				{
					return true;
				}
			}
			return false;
			Block_2:
			this.Fail("Unexpected Section: '" + xmlelement.mValue + "'");
			return false;
		}

		protected bool ParseStringArray(List<string> theStringVector)
		{
			theStringVector.Clear();
			XMLElement xmlelement;
			for (;;)
			{
				xmlelement = new XMLElement();
				if (!this.mXMLParser.NextElement(xmlelement))
				{
					break;
				}
				if (xmlelement.mType == XMLElement.XMLElementType.TYPE_START)
				{
					if (!(xmlelement.mValue.ToString() == "String"))
					{
						goto IL_57;
					}
					string text = "";
					if (!this.ParseSingleElement(out text))
					{
						return false;
					}
					theStringVector.Add(text);
				}
				else
				{
					if (xmlelement.mType == XMLElement.XMLElementType.TYPE_ELEMENT)
					{
						goto Block_5;
					}
					if (xmlelement.mType == XMLElement.XMLElementType.TYPE_END)
					{
						return true;
					}
				}
			}
			return false;
			IL_57:
			this.Fail("Invalid Section '" + xmlelement.mValue + "'");
			return false;
			Block_5:
			this.Fail("Element Not Expected '" + xmlelement.mValue + "'");
			return false;
		}

		protected bool ParseProperties()
		{
			XMLElement xmlelement;
			string text;
			string text2;
			string text3;
			for (;;)
			{
				xmlelement = new XMLElement();
				if (!this.mXMLParser.NextElement(xmlelement))
				{
					break;
				}
				if (xmlelement.mType == XMLElement.XMLElementType.TYPE_START)
				{
					if (xmlelement.mValue.ToString() == "String")
					{
						string value = "";
						if (!this.ParseSingleElement(out value))
						{
							return false;
						}
						string attribute = xmlelement.GetAttribute("id");
						this.mApp.SetString(attribute, value);
					}
					else if (xmlelement.mValue.ToString() == "StringArray")
					{
						List<string> list = new List<string>();
						if (!this.ParseStringArray(list))
						{
							return false;
						}
						string attribute2 = xmlelement.GetAttribute("id");
						this.mApp.mStringVectorProperties[attribute2] = list;
					}
					else if (xmlelement.mValue.ToString() == "Boolean")
					{
						text = "";
						if (!this.ParseSingleElement(out text))
						{
							return false;
						}
						text = text.ToUpper();
						bool boolValue;
						if (text == "1" || text == "YES" || text == "ON" || text == "TRUE")
						{
							boolValue = true;
						}
						else
						{
							if (!(text == "0") && !(text == "NO") && !(text == "OFF") && !(text == "FALSE"))
							{
								goto IL_166;
							}
							boolValue = false;
						}
						string attribute3 = xmlelement.GetAttribute("id");
						this.mApp.SetBoolean(attribute3, boolValue);
					}
					else if (xmlelement.mValue.ToString() == "Integer")
					{
						text2 = "";
						if (!this.ParseSingleElement(out text2))
						{
							return false;
						}
						int anInt = 0;
						if (!Common.StringToInt(text2, ref anInt))
						{
							goto Block_16;
						}
						string attribute4 = xmlelement.GetAttribute("id");
						this.mApp.SetInteger(attribute4, anInt);
					}
					else
					{
						if (!(xmlelement.mValue.ToString() == "Double"))
						{
							goto IL_28C;
						}
						text3 = "";
						if (!this.ParseSingleElement(out text3))
						{
							return false;
						}
						double aDouble = 0.0;
						if (!Common.StringToDouble(text3, ref aDouble))
						{
							goto Block_19;
						}
						string attribute5 = xmlelement.GetAttribute("id");
						this.mApp.SetDouble(attribute5, aDouble);
					}
				}
				else
				{
					if (xmlelement.mType == XMLElement.XMLElementType.TYPE_ELEMENT)
					{
						goto Block_20;
					}
					if (xmlelement.mType == XMLElement.XMLElementType.TYPE_END)
					{
						return true;
					}
				}
			}
			return false;
			IL_166:
			this.Fail("Invalid Boolean Value: '" + text + "'");
			return false;
			Block_16:
			this.Fail("Invalid Integer Value: '" + text2 + "'");
			return false;
			Block_19:
			this.Fail("Invalid Double Value: '" + text3 + "'");
			return false;
			IL_28C:
			this.Fail("Invalid Section '" + xmlelement.mValue + "'");
			return false;
			Block_20:
			this.Fail("Element Not Expected '" + xmlelement.mValue + "'");
			return false;
		}

		protected bool DoParseProperties()
		{
			if (!this.mXMLParser.HasFailed())
			{
				XMLElement xmlelement;
				for (;;)
				{
					xmlelement = new XMLElement();
					if (!this.mXMLParser.NextElement(xmlelement))
					{
						break;
					}
					if (xmlelement.mType == XMLElement.XMLElementType.TYPE_START)
					{
						if (!(xmlelement.mValue.ToString() == "Properties"))
						{
							goto IL_4B;
						}
						if (!this.ParseProperties())
						{
							break;
						}
					}
					else if (xmlelement.mType == XMLElement.XMLElementType.TYPE_ELEMENT)
					{
						goto Block_5;
					}
				}
				goto IL_8C;
				IL_4B:
				this.Fail("Invalid Section '" + xmlelement.mValue + "'");
				goto IL_8C;
				Block_5:
				this.Fail("Element Not Expected '" + xmlelement.mValue + "'");
			}
			IL_8C:
			if (this.mXMLParser.HasFailed())
			{
				this.Fail(this.mXMLParser.GetErrorText());
			}
			this.mXMLParser = null;
			return !this.mHasFailed;
		}

		public PropertiesParser(SexyAppBase theApp)
		{
			this.mApp = theApp;
			this.mHasFailed = false;
		}

		public virtual void Dispose()
		{
		}

		public bool ParsePropertiesFile(string theFilename)
		{
			this.mXMLParser = new XMLParser();
			this.mXMLParser.OpenFile(theFilename);
			return this.DoParseProperties();
		}

		public bool ParsePropertiesBuffer(byte[] theBuffer)
		{
			this.mXMLParser = new XMLParser();
			this.mXMLParser.checkEncodingType(theBuffer);
			this.mXMLParser.SetBytes(theBuffer);
			return this.DoParseProperties();
		}

		public string GetErrorText()
		{
			return this.mError;
		}

		public SexyAppBase mApp;

		public XMLParser mXMLParser;

		public string mError = string.Empty;

		public bool mHasFailed;
	}
}
