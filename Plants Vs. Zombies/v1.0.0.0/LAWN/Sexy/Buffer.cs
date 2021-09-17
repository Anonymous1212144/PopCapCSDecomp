﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sexy
{
	public class Buffer
	{
		public byte[] Data
		{
			get
			{
				return this.mData.GetBuffer();
			}
			set
			{
				this.mData.Close();
				this.mData = new MemoryStream(value);
			}
		}

		public Buffer()
		{
			this.mDataBitSize = 0;
			this.mReadBitPos = 0;
			this.mWriteBitPos = 0;
			this.mData = new MemoryStream();
		}

		public virtual void Dispose()
		{
		}

		public void Clear()
		{
			this.mReadBitPos = 0;
			this.mWriteBitPos = 0;
			this.mDataBitSize = 0;
			this.mData.Close();
			this.mData = new MemoryStream();
		}

		public void SeekFront()
		{
			this.mReadBitPos = 0;
			this.mData.Seek(0L, 0);
		}

		public int GetDataLen()
		{
			return (this.mDataBitSize + 7) / 8;
		}

		public void FromWebString(string theString)
		{
			this.Clear();
			if (theString.Length < 4)
			{
				return;
			}
			int num = 0;
			for (int i = 0; i < 8; i++)
			{
				char c = theString.get_Chars(i);
				int num2 = 0;
				if (c >= '0' && c <= '9')
				{
					num2 = (int)(c - '0');
				}
				else if (c >= 'A' && c <= 'F')
				{
					num2 = (int)(c - 'A' + '\n');
				}
				else if (c >= 'a' && c <= 'f')
				{
					num2 = (int)(c - 'f' + '\n');
				}
				num += num2 << (7 - i) * 4;
			}
			int num3 = 8;
			int num4;
			for (int j = num; j > 0; j -= num4)
			{
				char c2 = theString.get_Chars(num3++);
				int theNum = Buffer.gWebDecodeMap[(int)c2];
				num4 = Math.Min(j, 6);
				this.WriteNumBits(theNum, num4);
			}
			this.SeekFront();
		}

		public void WriteByte(byte theByte)
		{
			this.mData.WriteByte(theByte);
		}

		public void WriteNumBits(int theNum, int theBits)
		{
		}

		public static int GetBitsRequired(int theNum, bool isSigned)
		{
			if (theNum < 0)
			{
				theNum = -theNum - 1;
			}
			int num = 0;
			while (theNum >= 1 << num)
			{
				num++;
			}
			if (isSigned)
			{
				num++;
			}
			return num;
		}

		public void WriteBoolean(bool theBool)
		{
			byte[] bytes = BitConverter.GetBytes(theBool);
			this.mData.Write(bytes, 0, bytes.Length);
		}

		public void WriteBooleanArray(bool[] theBool)
		{
			this.WriteLong(theBool.Length);
			for (int i = 0; i < theBool.Length; i++)
			{
				this.WriteBoolean(theBool[i]);
			}
		}

		public void WriteBoolean2DArray(bool[,] theBool)
		{
			int length = theBool.GetLength(0);
			int length2 = theBool.GetLength(1);
			this.WriteLong(length);
			this.WriteLong(length2);
			for (int i = 0; i < length; i++)
			{
				for (int j = 0; j < length2; j++)
				{
					this.WriteBoolean(theBool[i, j]);
				}
			}
		}

		public void WriteShort(short theShort)
		{
			byte[] bytes = BitConverter.GetBytes(theShort);
			this.mData.Write(bytes, 0, bytes.Length);
		}

		public void WriteLong(int theLong)
		{
			byte[] bytes = BitConverter.GetBytes(theLong);
			this.mData.Write(bytes, 0, bytes.Length);
		}

		public void WriteLongArray(int[] theLong)
		{
			this.WriteLong(theLong.Length);
			for (int i = 0; i < theLong.Length; i++)
			{
				this.WriteLong(theLong[i]);
			}
		}

		public void WriteLong2DArray(int[,] theLong)
		{
			int length = theLong.GetLength(0);
			int length2 = theLong.GetLength(1);
			this.WriteLong(length);
			this.WriteLong(length2);
			for (int i = 0; i < length; i++)
			{
				for (int j = 0; j < length2; j++)
				{
					this.WriteLong(theLong[i, j]);
				}
			}
		}

		public void WriteLong3DArray(int[,,] theLong)
		{
			int length = theLong.GetLength(0);
			int length2 = theLong.GetLength(1);
			int length3 = theLong.GetLength(2);
			this.WriteLong(length);
			this.WriteLong(length2);
			this.WriteLong(length3);
			for (int i = 0; i < length; i++)
			{
				for (int j = 0; j < length2; j++)
				{
					for (int k = 0; k < length3; k++)
					{
						this.WriteLong(theLong[i, j, k]);
					}
				}
			}
		}

		public void WriteFloat(float theFloat)
		{
			byte[] bytes = BitConverter.GetBytes(theFloat);
			this.mData.Write(bytes, 0, bytes.Length);
		}

		public void WriteDouble(double theDouble)
		{
			byte[] bytes = BitConverter.GetBytes(theDouble);
			this.mData.Write(bytes, 0, bytes.Length);
		}

		public void WriteDateTime(DateTime theTime)
		{
			this.WriteLong(theTime.Millisecond);
			this.WriteLong(theTime.Second);
			this.WriteLong(theTime.Minute);
			this.WriteLong(theTime.Hour);
			this.WriteLong(theTime.Day);
			this.WriteLong(theTime.Month);
			this.WriteLong(theTime.Year);
		}

		public DateTime ReadDateTime()
		{
			int num = this.ReadLong();
			int num2 = this.ReadLong();
			int num3 = this.ReadLong();
			int num4 = this.ReadLong();
			int num5 = this.ReadLong();
			int num6 = this.ReadLong();
			int num7 = this.ReadLong();
			return new DateTime(num7, num6, num5, num4, num3, num2, num, 0);
		}

		public void WriteString(string theString)
		{
			this.WriteLong(theString.Length);
			byte[] bytes = this.encoding.GetBytes(theString);
			for (int i = 0; i < bytes.Length; i++)
			{
				this.WriteByte(bytes[i]);
			}
		}

		public void WriteStringArray(string[] theString)
		{
			this.WriteLong(theString.Length);
			for (int i = 0; i < theString.Length; i++)
			{
				this.WriteString(theString[i]);
			}
		}

		public void WriteRect(TRect theRect)
		{
			this.WriteLong(theRect.mX);
			this.WriteLong(theRect.mY);
			this.WriteLong(theRect.mWidth);
			this.WriteLong(theRect.mHeight);
		}

		public void WriteUTF8String(string theString)
		{
			this.WriteString(theString);
		}

		public void WriteLine(string theString)
		{
			this.WriteString(theString + "\r\n");
		}

		public void WriteBuffer(List<byte> theBuffer)
		{
		}

		public void WriteBytes(string theByte, int theCount)
		{
			for (int i = 0; i < theCount; i++)
			{
				this.WriteByte((byte)theByte.get_Chars(i));
			}
		}

		public void SetData(List<byte> theBuffer)
		{
			this.mData.Seek(0L, 0);
			for (int i = 0; i < theBuffer.Count; i++)
			{
				this.mData.WriteByte(theBuffer[i]);
			}
			this.mDataBitSize = (int)this.mData.Length * 8;
		}

		public void SetData(List<byte> theOtherList, int theCount)
		{
			this.mData = new MemoryStream();
			this.mData.Write(theOtherList.ToArray(), 0, theCount);
			this.mDataBitSize = (int)this.mData.Length * 8;
		}

		public string ToWebString()
		{
			string text = "";
			int num = this.mWriteBitPos;
			int num2 = this.mReadBitPos;
			this.mReadBitPos = 0;
			string text2 = new string(new char[256]);
			text2 = string.Format("{0:X8}", num);
			text += text2;
			int num3 = (num + 5) / 6;
			for (int i = 0; i < num3; i++)
			{
				text += Buffer.gWebEncodeMap.get_Chars(this.ReadNumBits(6, false));
			}
			this.mReadBitPos = num2;
			return text;
		}

		public byte ReadByte()
		{
			return (byte)this.mData.ReadByte();
		}

		public int ReadNumBits(int theBits, bool isSigned)
		{
			return 0;
		}

		public bool ReadBoolean()
		{
			byte[] array = new byte[1];
			this.mData.Read(array, 0, array.Length);
			return BitConverter.ToBoolean(array, 0);
		}

		public bool[] ReadBooleanArray()
		{
			int num = this.ReadLong();
			bool[] array = new bool[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = this.ReadBoolean();
			}
			return array;
		}

		public bool[,] ReadBoolean2DArray()
		{
			int num = this.ReadLong();
			int num2 = this.ReadLong();
			bool[,] array = new bool[num, num2];
			for (int i = 0; i < num; i++)
			{
				for (int j = 0; j < num2; j++)
				{
					array[i, j] = this.ReadBoolean();
				}
			}
			return array;
		}

		public short ReadShort()
		{
			byte[] array = new byte[2];
			this.mData.Read(array, 0, array.Length);
			return BitConverter.ToInt16(array, 0);
		}

		public int ReadLong()
		{
			byte[] array = new byte[4];
			this.mData.Read(array, 0, array.Length);
			return BitConverter.ToInt32(array, 0);
		}

		public int[] ReadLongArray()
		{
			int num = this.ReadLong();
			int[] array = new int[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = this.ReadLong();
			}
			return array;
		}

		public int[,] ReadLong2DArray()
		{
			int num = this.ReadLong();
			int num2 = this.ReadLong();
			int[,] array = new int[num, num2];
			for (int i = 0; i < num; i++)
			{
				for (int j = 0; j < num2; j++)
				{
					array[i, j] = this.ReadLong();
				}
			}
			return array;
		}

		public int[,,] ReadLong3DArray()
		{
			int num = this.ReadLong();
			int num2 = this.ReadLong();
			int num3 = this.ReadLong();
			int[,,] array = new int[num, num2, num3];
			for (int i = 0; i < num; i++)
			{
				for (int j = 0; j < num2; j++)
				{
					for (int k = 0; k < num3; k++)
					{
						array[i, j, k] = this.ReadLong();
					}
				}
			}
			return array;
		}

		public float ReadFloat()
		{
			byte[] array = new byte[4];
			this.mData.Read(array, 0, 4);
			return BitConverter.ToSingle(array, 0);
		}

		public double ReadDouble()
		{
			byte[] array = new byte[8];
			this.mData.Read(array, 0, 8);
			return BitConverter.ToDouble(array, 0);
		}

		public string ReadString()
		{
			int num = this.ReadLong();
			byte[] array = new byte[num];
			this.mData.Read(array, 0, num);
			return this.encoding.GetString(array, 0, num);
		}

		public string[] ReadStringArray()
		{
			int num = this.ReadLong();
			string[] array = new string[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = this.ReadString();
			}
			return array;
		}

		public TRect ReadRect()
		{
			int theX = this.ReadLong();
			int theY = this.ReadLong();
			int theWidth = this.ReadLong();
			int theHeight = this.ReadLong();
			return new TRect(theX, theY, theWidth, theHeight);
		}

		public string ReadUTF8String()
		{
			return this.ReadString();
		}

		public string ReadLine()
		{
			string text = "";
			for (;;)
			{
				byte b = this.ReadByte();
				if (b == 0 || b == 10)
				{
					break;
				}
				if (b != 13)
				{
					text += b;
				}
			}
			return text;
		}

		public void ReadBytes(byte[] theData, int theLen)
		{
			for (int i = 0; i < theLen; i++)
			{
				theData[i] = this.ReadByte();
			}
		}

		public void ReadBuffer(ref List<byte> theByteVector)
		{
		}

		public string GetDataPtr()
		{
			return "";
		}

		public int GetDataLenBits()
		{
			return this.mDataBitSize;
		}

		public uint GetCRC32()
		{
			return this.GetCRC32(0U);
		}

		public uint GetCRC32(uint theSeed)
		{
			return theSeed;
		}

		public bool AtEnd()
		{
			return this.mData.Position == this.mData.Length;
		}

		public bool PastEnd()
		{
			return this.mData.Position > this.mData.Length;
		}

		internal static int[] gWebDecodeMap = new int[]
		{
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, 0, -1, 1, 0, -1, 2, 3,
			4, 5, 6, 7, 8, 9, 10, 11, -1, -1,
			-1, -1, -1, -1, -1, 12, 13, 14, 15, 16,
			17, 18, 19, 20, 21, 22, 23, 24, 25, 26,
			27, 28, 29, 30, 31, 32, 33, 34, 35, 36,
			37, -1, -1, -1, -1, -1, -1, 38, 39, 40,
			41, 42, 43, 44, 45, 46, 47, 48, 49, 50,
			51, 52, 53, 54, 55, 56, 57, 58, 59, 60,
			61, 62, 63, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1
		};

		internal static string gWebEncodeMap = ".-0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

		public MemoryStream mData;

		public int mDataBitSize;

		public int mReadBitPos;

		public int mWriteBitPos;

		private UTF8Encoding encoding = new UTF8Encoding();
	}
}
