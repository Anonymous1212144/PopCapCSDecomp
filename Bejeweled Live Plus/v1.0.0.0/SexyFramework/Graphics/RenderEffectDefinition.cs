using System;
using SexyFramework.Resource;

namespace SexyFramework.Graphics
{
	public class RenderEffectDefinition : IDisposable
	{
		public bool LoadFromMem(int inDataLen, byte[] inData, string inSrcFileName, string inDataFormat)
		{
			this.mData = inData;
			this.mSrcFileName = inSrcFileName;
			this.mDataFormat = inDataFormat;
			return inData != null;
		}

		public bool LoadFromFile(string inFileName, string inSrcFileName)
		{
			bool result = false;
			string text = Common.GetFileDir(inFileName, true) + Common.GetFileName(inFileName, true);
			PFILE pfile = new PFILE(text, "rb");
			if (pfile.Open())
			{
				byte[] data = pfile.GetData();
				if (data != null)
				{
					string text2 = string.Empty;
					int num = text.LastIndexOf('.');
					if (num >= 0)
					{
						text2 = text.Substring(num);
					}
					if (text2.Length > 1)
					{
						text2 = text2.Substring(1);
					}
					result = this.LoadFromMem(data.Length, data, inSrcFileName, text2);
				}
				pfile.Close();
			}
			return result;
		}

		public virtual void Dispose()
		{
			this.mData = null;
		}

		public byte[] mData;

		public string mSrcFileName;

		public string mDataFormat;
	}
}
