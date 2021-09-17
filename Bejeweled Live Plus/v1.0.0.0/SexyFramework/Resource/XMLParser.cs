using System;
using System.Text;

namespace SexyFramework.Resource
{
	public class XMLParser : EncodingParser
	{
		protected void Fail(string theErrorText)
		{
			this.mHasFailed = true;
			this.mErrorText = theErrorText;
		}

		public void Init()
		{
			this.mSection = string.Empty;
			this.mLineNum = 1;
			this.mHasFailed = false;
			this.mErrorText = string.Empty;
		}

		protected bool AddAttribute(XMLElement theElement, string theAttributeKey, string theAttributeValue)
		{
			return theElement.AddAttribute(theAttributeKey, theAttributeValue);
		}

		protected bool AddAttributeEncoded(XMLElement theElement, string theAttributeKey, string theAttributeValue)
		{
			bool result = !theElement.mAttributesEncoded.ContainsKey(theAttributeKey);
			theElement.mAttributesEncoded[theAttributeKey] = theAttributeValue;
			return result;
		}

		public XMLParser()
		{
			this.mFile = null;
			this.mLineNum = 0;
			this.mAllowComments = false;
		}

		public override void Dispose()
		{
			base.Dispose();
		}

		public override bool OpenFile(string theFilename)
		{
			if (!base.OpenFile(theFilename))
			{
				this.mLineNum = 0;
				this.Fail("Unable to open file " + theFilename);
				return false;
			}
			this.mFileName = theFilename;
			this.Init();
			return true;
		}

		public virtual bool NextElement(XMLElement theElement)
		{
			string text6;
			for (;;)
			{
				theElement.mType = XMLElement.XMLElementType.TYPE_NONE;
				theElement.mSection = new StringBuilder(this.mSection);
				theElement.mInstruction = new StringBuilder("");
				theElement.mValue = new StringBuilder("");
				theElement.mValueEncoded = new StringBuilder("");
				theElement.ClearAttributes();
				theElement.mAttributesEncoded.Clear();
				theElement.mInstruction.Clear();
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				bool flag4 = false;
				bool flag5 = false;
				string text = string.Empty;
				string text2 = string.Empty;
				string text3 = string.Empty;
				string text4 = string.Empty;
				char c;
				int length;
				StringBuilder stringBuilder;
				int length2;
				for (;;)
				{
					c = '\0';
					int num;
					switch (this.GetChar(ref c))
					{
					case EncodingParser.GetCharReturnType.SUCCESSFUL:
						num = 1;
						break;
					case EncodingParser.GetCharReturnType.INVALID_CHARACTER:
						goto IL_BA;
					case EncodingParser.GetCharReturnType.END_OF_FILE:
						goto IL_D4;
					case EncodingParser.GetCharReturnType.FAILURE:
						goto IL_C7;
					default:
						goto IL_D4;
					}
					IL_D7:
					if (num != 1)
					{
						goto IL_698;
					}
					bool flag6 = false;
					if (c == '\n')
					{
						this.mLineNum++;
					}
					if (theElement.mType == XMLElement.XMLElementType.TYPE_COMMENT)
					{
						theElement.mInstruction.Append(c);
						length = theElement.mInstruction.Length;
						if (c == '>' && length >= 3 && theElement.mInstruction.get_Chars(length - 2) == '-' && theElement.mInstruction.get_Chars(length - 3) == '-')
						{
							goto Block_7;
						}
						continue;
					}
					else if (theElement.mType == XMLElement.XMLElementType.TYPE_INSTRUCTION)
					{
						stringBuilder = theElement.mValue;
						if (theElement.mInstruction.Length != 0 || char.IsWhiteSpace(c))
						{
							stringBuilder = theElement.mInstruction;
						}
						stringBuilder.Append(c);
						length2 = stringBuilder.Length;
						if (c == '>' && length2 >= 2 && stringBuilder.get_Chars(length2 - 2) == '?')
						{
							goto Block_12;
						}
						continue;
					}
					else
					{
						if (c == '"')
						{
							flag2 = !flag2;
							if (theElement.mType == XMLElement.XMLElementType.TYPE_NONE || theElement.mType == XMLElement.XMLElementType.TYPE_ELEMENT)
							{
								flag6 = true;
							}
							if (!flag2)
							{
								flag3 = true;
							}
						}
						else if (!flag2)
						{
							if (c == '<')
							{
								if (theElement.mType == XMLElement.XMLElementType.TYPE_ELEMENT)
								{
									goto Block_18;
								}
								if (theElement.mType != XMLElement.XMLElementType.TYPE_NONE)
								{
									goto IL_247;
								}
								theElement.mType = XMLElement.XMLElementType.TYPE_START;
							}
							else
							{
								if (c == '>')
								{
									goto Block_20;
								}
								if (c == '/' && theElement.mType == XMLElement.XMLElementType.TYPE_START && theElement.mValue.Length == 0)
								{
									theElement.mType = XMLElement.XMLElementType.TYPE_END;
								}
								else if (c == '?' && theElement.mType == XMLElement.XMLElementType.TYPE_START && theElement.mValue.Length == 0)
								{
									theElement.mType = XMLElement.XMLElementType.TYPE_INSTRUCTION;
								}
								else if (char.IsWhiteSpace(c))
								{
									if (theElement.mValue.Length != 0)
									{
										flag = true;
									}
									if (theElement.mType == XMLElement.XMLElementType.TYPE_START && theElement.mValue.ToString() == "!--")
									{
										theElement.mType = XMLElement.XMLElementType.TYPE_COMMENT;
									}
								}
								else
								{
									if (c <= ' ')
									{
										goto IL_57E;
									}
									flag6 = true;
								}
							}
						}
						else
						{
							flag6 = true;
						}
						if (!flag6)
						{
							continue;
						}
						if (theElement.mType == XMLElement.XMLElementType.TYPE_NONE)
						{
							theElement.mType = XMLElement.XMLElementType.TYPE_ELEMENT;
						}
						if (theElement.mType != XMLElement.XMLElementType.TYPE_START)
						{
							if (flag)
							{
								theElement.mValue.Append(" ");
								flag = false;
							}
							theElement.mValue.Append(c);
							continue;
						}
						if (flag)
						{
							if (!flag4 || (!flag5 && c != '=') || (flag5 && (text2.Length > 0 || flag3)))
							{
								if (flag4)
								{
									this.AddAttribute(theElement, Common.XMLDecodeString(text), Common.XMLDecodeString(text2));
									this.AddAttributeEncoded(theElement, text, text2);
									text = string.Empty;
									text2 = string.Empty;
									text3 = string.Empty;
									text4 = string.Empty;
								}
								else
								{
									flag4 = true;
								}
								flag5 = false;
							}
							flag = false;
						}
						if (!flag4)
						{
							theElement.mValue.Append(c);
							continue;
						}
						if (c == '=')
						{
							flag5 = true;
							flag3 = false;
							continue;
						}
						if (!flag5)
						{
							text += c;
							continue;
						}
						text2 += c;
						continue;
					}
					IL_D4:
					num = 0;
					goto IL_D7;
				}
				IL_6AD:
				if (text.Length > 0)
				{
					this.AddAttribute(theElement, Common.XMLDecodeString(text), Common.XMLDecodeString(text2));
					this.AddAttribute(theElement, text, text2);
				}
				theElement.mValueEncoded = new StringBuilder(theElement.mValue.ToString());
				string theString = theElement.mValue.ToString();
				theElement.mValue.Clear();
				theElement.mValue.Append(Common.XMLDecodeString(theString));
				if (theElement.mType != XMLElement.XMLElementType.TYPE_COMMENT || this.mAllowComments)
				{
					return true;
				}
				continue;
				Block_7:
				theElement.mInstruction.Remove(length - 3, 3);
				goto IL_6AD;
				Block_12:
				stringBuilder.Remove(length2 - 2, 2);
				goto IL_6AD;
				Block_18:
				this.PutChar(c);
				goto IL_6AD;
				Block_20:
				if (theElement.mType == XMLElement.XMLElementType.TYPE_START)
				{
					bool flag7 = false;
					if (text == "/")
					{
						flag7 = true;
					}
					else
					{
						if (text.Length > 0)
						{
							text3 = Common.XMLDecodeString(text);
							text4 = text;
							this.AddAttribute(theElement, Common.XMLDecodeString(text), Common.XMLDecodeString(text2));
							this.AddAttributeEncoded(theElement, text, text2);
							text = string.Empty;
							text2 = string.Empty;
						}
						if (text3.Length > 0)
						{
							string text5 = theElement.GetAttribute(text3);
							int length3 = text5.Length;
							if (length3 > 0 && text5.get_Chars(length3 - 1) == '/')
							{
								this.AddAttribute(theElement, text3, Common.XMLDecodeString(text5.Substring(0, length3 - 1)));
								flag7 = true;
							}
							text5 = theElement.mAttributesEncoded[text4];
							length3 = text5.Length;
							if (length3 > 0 && text5.get_Chars(length3 - 1) == '/')
							{
								this.AddAttributeEncoded(theElement, text4, text5.Substring(0, length3 - 1));
								flag7 = true;
							}
						}
						else
						{
							int length4 = theElement.mValue.Length;
							if (length4 > 0 && theElement.mValue.get_Chars(length4 - 1) == '/')
							{
								theElement.mValue.Remove(length4 - 1, 1);
								flag7 = true;
							}
						}
					}
					if (flag7)
					{
						string theString2 = "</" + theElement.mValue + ">";
						this.PutString(theString2);
						text = string.Empty;
					}
					if (this.mSection.Length != 0)
					{
						this.mSection += "/";
					}
					this.mSection += theElement.mValue.ToString();
					goto IL_6AD;
				}
				if (theElement.mType != XMLElement.XMLElementType.TYPE_END)
				{
					goto IL_4D8;
				}
				int num2 = this.mSection.LastIndexOf('/');
				if (num2 == -1 && this.mSection.Length == 0)
				{
					goto Block_35;
				}
				text6 = this.mSection.Substring(num2 + 1);
				if (text6 != theElement.mValue.ToString())
				{
					goto Block_36;
				}
				if (num2 == -1)
				{
					this.mSection = string.Empty;
					goto IL_6AD;
				}
				this.mSection = this.mSection.Remove(num2);
				goto IL_6AD;
			}
			IL_BA:
			this.Fail("Illegal Character");
			return false;
			IL_C7:
			this.Fail("Internal Error");
			return false;
			IL_247:
			this.Fail("Unexpected '<'");
			return false;
			Block_35:
			this.Fail("Unexpected End");
			return false;
			Block_36:
			this.Fail(string.Concat(new object[] { "End '", theElement.mValue, "' Doesn't Match Start '", text6, "'" }));
			return false;
			IL_4D8:
			this.Fail("Unexpected '>'");
			return false;
			IL_57E:
			this.Fail("Illegal Character");
			return false;
			IL_698:
			if (theElement.mType != XMLElement.XMLElementType.TYPE_NONE)
			{
				this.Fail("Unexpected End of File");
			}
			return false;
		}

		public string GetErrorText()
		{
			return this.mErrorText;
		}

		public int GetCurrentLineNum()
		{
			return this.mLineNum;
		}

		public string GetFileName()
		{
			return this.mFileName;
		}

		public override void SetStringSource(string theString)
		{
			this.Init();
			base.SetStringSource(theString);
		}

		public void AllowComments(bool doAllow)
		{
			this.mAllowComments = doAllow;
		}

		public bool HasFailed()
		{
			return this.mHasFailed;
		}

		protected string mFileName = string.Empty;

		protected string mErrorText = string.Empty;

		protected int mLineNum;

		protected bool mHasFailed;

		protected bool mAllowComments;

		protected string mSection = string.Empty;
	}
}
