using System;
using System.Collections.Generic;
using System.Text;

namespace SexyFramework.Resource
{
	public class XMLElement
	{
		public bool GetAttributeBool(string theKey, bool theDefaultValue)
		{
			if (!this.HasAttribute(theKey))
			{
				return theDefaultValue;
			}
			string text = this.mAttributes[theKey];
			return text.Length == 0 || (string.Compare(text, "true", 5) == 0 || text == "1") || (string.Compare(text, "false", 5) != 0 && !(text == "0") && theDefaultValue);
		}

		public string GetAttribute(string key)
		{
			if (!this.HasAttribute(key))
			{
				return "";
			}
			return this.mAttributes[key];
		}

		public bool HasAttribute(string key)
		{
			return this.mAttributes.ContainsKey(key);
		}

		public Dictionary<string, string> GetAttributeMap()
		{
			return this.mAttributes;
		}

		public void ClearAttributes()
		{
			this.mAttributes.Clear();
		}

		public bool AddAttribute(string key, string value)
		{
			bool result = !this.HasAttribute(key);
			this.mAttributes[key] = value;
			return result;
		}

		public XMLElement.XMLElementType mType;

		public StringBuilder mSection;

		public StringBuilder mValue;

		public StringBuilder mValueEncoded;

		public StringBuilder mInstruction;

		private Dictionary<string, string> mAttributes = new Dictionary<string, string>();

		public Dictionary<string, string> mAttributesEncoded = new Dictionary<string, string>();

		public enum XMLElementType
		{
			TYPE_NONE,
			TYPE_START,
			TYPE_END,
			TYPE_ELEMENT,
			TYPE_INSTRUCTION,
			TYPE_COMMENT
		}
	}
}
