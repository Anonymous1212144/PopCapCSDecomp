using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SexyFramework
{
	public class PopLoc
	{
		public PopLoc()
		{
			this.mIdStrings = new Dictionary<int, string>();
			this.mNameStrings = new Dictionary<string, string>();
		}

		public ILocalizedStringProvider LocalizedString
		{
			get
			{
				return this.localizedString;
			}
			set
			{
				ILocalizedStringProvider localizedStringProvider = this.localizedString;
				this.localizedString = value;
			}
		}

		public string ConvertFormatFields(string fmt)
		{
			int num = 0;
			string text = string.Empty;
			PopLoc.FormatField formatField = PopLoc.FormatField.None;
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < fmt.Length; i++)
			{
				char c = fmt.get_Chars(i);
				if (formatField == PopLoc.FormatField.None)
				{
					if (c == '%')
					{
						formatField = PopLoc.FormatField.Expect;
					}
					else
					{
						stringBuilder.Append(c);
					}
				}
				else if (char.IsDigit(c) || c == '-' || c == '.')
				{
					text += c.ToString();
					if (text == ".")
					{
						text = "0.";
					}
					if (text == "-.")
					{
						text = "-0.";
					}
				}
				else if (c == '%')
				{
					stringBuilder.Append('%');
					formatField = PopLoc.FormatField.None;
				}
				else
				{
					stringBuilder.Append('{');
					stringBuilder.AppendFormat("{0}", new object[] { num++ });
					if (!string.IsNullOrEmpty(text))
					{
						stringBuilder.AppendFormat(":{0}{1}", new object[] { c, text });
					}
					stringBuilder.Append('}');
					text = string.Empty;
					formatField = PopLoc.FormatField.None;
				}
			}
			return stringBuilder.ToString();
		}

		private string readDumpTemplate(string fileName)
		{
			string result = null;
			try
			{
				using (FileStream fileStream = new FileStream(fileName, 3, 1))
				{
					using (StreamReader streamReader = new StreamReader(fileStream, true))
					{
						result = streamReader.ReadToEnd();
					}
				}
			}
			catch
			{
			}
			return result;
		}

		private string xmlConvert(string str)
		{
			StringBuilder stringBuilder = new StringBuilder(str);
			stringBuilder.Replace("&", "&amp;");
			stringBuilder.Replace("<", "&lt;");
			stringBuilder.Replace(">", "&gt;");
			return stringBuilder.ToString();
		}

		public void dumpLocalizedTextResource(string fileName)
		{
			string text = this.readDumpTemplate("StringsTemplate.resx");
			if (text != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (int num in this.mIdStrings.Keys)
				{
					stringBuilder.AppendFormat("  <data name=\"IDS_{0}\" xml:space=\"preserve\"><value>{1}</value></data>\n", new object[]
					{
						num,
						this.xmlConvert(this.mIdStrings[num])
					});
				}
				StringBuilder stringBuilder2 = new StringBuilder(text);
				stringBuilder2.Replace("%DUMPED_RESOURCE%", stringBuilder.ToString());
				try
				{
					using (FileStream fileStream = new FileStream(fileName, 2, 2))
					{
						using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8))
						{
							streamWriter.WriteLine(stringBuilder2.ToString());
						}
					}
				}
				catch
				{
				}
			}
		}

		public string GetString(int id, string strDefault)
		{
			string text = null;
			if (this.LocalizedString != null)
			{
				text = this.LocalizedString.fromID(id);
			}
			if (text == null)
			{
				return strDefault;
			}
			return text;
		}

		public string GetString(string name, string strDefault)
		{
			if (!this.mNameStrings.ContainsKey(name))
			{
				return strDefault;
			}
			return this.mNameStrings[name];
		}

		public bool SetString(int id, string str, bool reset)
		{
			bool result = false;
			str = this.ConvertFormatFields(str);
			if (!this.mIdStrings.ContainsKey(id))
			{
				this.mIdStrings[id] = str;
				result = true;
			}
			else if (reset)
			{
				this.mIdStrings.Add(id, str);
				result = true;
			}
			return result;
		}

		public bool SetString(string name, string str, bool reset)
		{
			bool result = false;
			str = this.ConvertFormatFields(str);
			name = name.ToUpper();
			if (!this.mNameStrings.ContainsKey(name))
			{
				this.mNameStrings[name] = str;
				result = true;
			}
			else if (reset)
			{
				this.mNameStrings.Add(name, str);
				result = true;
			}
			return result;
		}

		public bool RemoveString(int id)
		{
			return this.mIdStrings.Remove(id);
		}

		public bool RemoveString(string name)
		{
			return this.mNameStrings.Remove(name.ToUpper());
		}

		public string Evaluate(string input)
		{
			int num = 0;
			do
			{
				int num2 = input.IndexOf('%', num);
				if (num2 < 0)
				{
					break;
				}
				int num3 = input.IndexOf('%', num2 + 1);
				if (num3 < 0)
				{
					break;
				}
				if (num3 == num2 + 1)
				{
					input.Remove(num3, 1);
					num = num3;
				}
				else
				{
					string text = input.Substring(num2 + 1, num3 - (num2 + 1));
					int id = 0;
					if (!int.TryParse(text, ref id))
					{
						string @string = this.GetString(text, string.Empty);
						input.Replace("%" + text + "%", @string);
						num = num2;
					}
					else
					{
						string string2 = this.GetString(id, this.GetString(text, string.Empty));
						input.Replace("%" + text + "%", string2);
						num = num2;
					}
				}
			}
			while (num < input.Length);
			return input;
		}

		private ILocalizedStringProvider localizedString;

		private Dictionary<int, string> mIdStrings;

		private Dictionary<string, string> mNameStrings;

		private enum FormatField
		{
			None,
			Expect
		}
	}
}
