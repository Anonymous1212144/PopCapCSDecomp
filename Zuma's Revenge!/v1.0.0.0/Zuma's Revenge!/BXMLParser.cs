using System;
using SexyFramework;
using SexyFramework.Misc;
using SexyFramework.Resource;

namespace ZumasRevenge
{
	public class BXMLParser
	{
		protected string UnpackString()
		{
			string text = "";
			for (int num = this.mSexyBuffer.ReadInt32(); num != 0; num = this.mSexyBuffer.ReadInt32())
			{
				text += (char)num;
			}
			return text;
		}

		protected short UnpackShort()
		{
			return this.mSexyBuffer.ReadShort();
		}

		public BXMLParser()
		{
			this.mSexyBuffer = null;
		}

		public virtual void Dispose()
		{
			this.mSexyBuffer = null;
		}

		public virtual bool OpenFile(string filename)
		{
			PFILE pfile = new PFILE(filename, "rb");
			if (!pfile.Open())
			{
				return false;
			}
			byte[] data = pfile.GetData();
			this.mSexyBuffer = new Buffer();
			this.mSexyBuffer.SetData(data, data.Length);
			return true;
		}

		public virtual bool OpenStream(string filename)
		{
			this.mSexyBuffer = new Buffer();
			return GlobalMembers.gSexyApp.ReadBufferFromStream(filename, ref this.mSexyBuffer);
		}

		public virtual bool OpenBuffer(Buffer buffer)
		{
			this.mSexyBuffer = buffer;
			return true;
		}

		public virtual bool NextElement(ref BXMLElement theElement)
		{
			if (this.mSexyBuffer.AtEnd())
			{
				return false;
			}
			theElement.mType = 0;
			theElement.mValue = "";
			theElement.mAttributes.Clear();
			theElement.mType = (int)this.UnpackShort();
			theElement.mValue = this.UnpackString();
			int num = (int)this.UnpackShort();
			while (num-- > 0)
			{
				string text = this.UnpackString().ToLower();
				string text2 = this.UnpackString();
				theElement.mAttributes[text] = text2;
			}
			return true;
		}

		public static bool CompileXML(string theSrcName, string theSrcDestName)
		{
			return true;
		}

		public bool HasFailed()
		{
			return false;
		}

		private Buffer mSexyBuffer;
	}
}
