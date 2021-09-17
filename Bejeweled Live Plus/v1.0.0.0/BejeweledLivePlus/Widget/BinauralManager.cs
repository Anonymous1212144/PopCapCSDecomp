using System;
using System.Collections.Generic;
using System.Globalization;
using SexyFramework.Misc;
using SexyFramework.Resource;

namespace BejeweledLivePlus.Widget
{
	public class BinauralManager
	{
		public BinauralManager()
		{
			this.Reset();
		}

		public void Reset()
		{
			this.mCurTime = 0.0;
			this.mCurSeqIdx = -1;
			this.mToneSetMap.Clear();
			this.mTimeSequenceVector.Clear();
		}

		private static bool IsFloatPart(char ch, bool acceptSign)
		{
			string text = "0123456789.";
			if (acceptSign)
			{
				text += "+-";
			}
			return text.IndexOf(ch) > 0;
		}

		private static bool ExtractParameters(string str, out double carrier, out char sign, out double freq, out double amp)
		{
			char c = '\0';
			bool flag = false;
			bool acceptSign = true;
			bool result = false;
			carrier = 0.0;
			sign = '\0';
			freq = 0.0;
			amp = 0.0;
			for (int i = 0; i < str.Length; i++)
			{
				char c2 = str.get_Chars(i);
				if (!BinauralManager.IsFloatPart(c2, acceptSign))
				{
					c = c2;
					flag = true;
					break;
				}
				acceptSign = false;
			}
			if (flag)
			{
				string[] array = str.Split(new char[] { c, '/' });
				if (array.Length == 3 && double.TryParse(array[0], 511, CultureInfo.InvariantCulture, ref carrier) && double.TryParse(array[1], 511, CultureInfo.InvariantCulture, ref freq) && double.TryParse(array[2], 511, CultureInfo.InvariantCulture, ref amp))
				{
					sign = c;
					result = true;
				}
			}
			return result;
		}

		private static int ExtractParameters(string str, out int hour, out int min, out int sec, out string cmd, out string addParam)
		{
			int num = 0;
			hour = 0;
			min = 0;
			sec = 0;
			cmd = string.Empty;
			addParam = string.Empty;
			string[] array = str.Split(new char[] { ' ', ':' });
			if (array.Length >= 4 && int.TryParse(array[0], ref hour))
			{
				num++;
				if (int.TryParse(array[1], ref min))
				{
					num++;
					if (int.TryParse(array[2], ref sec))
					{
						num++;
						cmd = array[3];
						num++;
						if (array.Length > 4)
						{
							addParam = array[4];
							num++;
						}
					}
				}
			}
			return num;
		}

		public bool Load(string theFileName)
		{
			this.Reset();
			Buffer theBuffer = null;
			GlobalMembers.gApp.ReadBufferFromFile(theFileName, theBuffer);
			int num = 0;
			int i = 0;
			EncodingParser encodingParser = new EncodingParser();
			if (encodingParser.OpenFile(theFileName))
			{
				string text = null;
				char c = '\0';
				while (encodingParser.GetChar(ref c) == EncodingParser.GetCharReturnType.SUCCESSFUL)
				{
					if (c == '\n' || c == '\r')
					{
						text = text.Trim();
						if (text.Substring(0, 3) == "NOW")
						{
							num = 1;
						}
						if (text.Substring(0, 1) == "#")
						{
							if (text.Substring(0, 6).ToUpper() == "#DESC:")
							{
								this.mDesc = text.Substring(6).Trim();
							}
						}
						else if (num == 0)
						{
							int num2 = text.IndexOf(':');
							if (num2 != -1)
							{
								string text2 = text.Substring(0, num2);
								text = text.Substring(num2 + 1).Trim();
								BinauralToneSet binauralToneSet = new BinauralToneSet();
								double mCarrier = 0.0;
								char c2 = '\0';
								double mFreq = 0.0;
								double num3 = 0.0;
								if (BinauralManager.ExtractParameters(text, out mCarrier, out c2, out mFreq, out num3))
								{
									BinauralToneDef binauralToneDef = new BinauralToneDef();
									binauralToneDef.mCarrier = mCarrier;
									binauralToneDef.mFreq = mFreq;
									if (c2 == '-')
									{
										binauralToneDef.mFreq = -binauralToneDef.mFreq;
									}
									binauralToneDef.mVolume = num3 / 100.0;
									binauralToneSet.mToneDefVector.Add(binauralToneDef);
								}
								i = GlobalMembers.MAX(i, binauralToneSet.mToneDefVector.Count);
								this.mToneSetMap[text2] = binauralToneSet;
							}
						}
						else if (num == 1)
						{
							BinauralTimeSequence binauralTimeSequence = new BinauralTimeSequence();
							int num4 = 0;
							int num5 = 0;
							int num6 = 0;
							string empty = string.Empty;
							string empty2 = string.Empty;
							if (text.Substring(0, 3) == "NOW")
							{
								text = text.Substring(3).Trim();
							}
							else if (4 >= BinauralManager.ExtractParameters(text, out num4, out num5, out num6, out empty, out empty2))
							{
								text = empty.Trim();
							}
							if (string.Compare(empty2, ".") == 0)
							{
								binauralTimeSequence.mCrossfade = true;
							}
							else
							{
								binauralTimeSequence.mCrossfade = false;
							}
							binauralTimeSequence.mTime = num4 * 60 * 60 + num5 * 60 + num6;
							binauralTimeSequence.mToneSet = this.mToneSetMap[text];
							this.mTimeSequenceVector.Add(binauralTimeSequence);
						}
					}
					else
					{
						text += c;
					}
				}
			}
			if (this.mTimeSequenceVector.Count > 0 && !this.mTimeSequenceVector[0].mCrossfade)
			{
				this.mTimeSequenceVector[0].mTime = 2;
				BinauralTimeSequence binauralTimeSequence2 = new BinauralTimeSequence();
				binauralTimeSequence2.mTime = 0;
				binauralTimeSequence2.mCrossfade = true;
				binauralTimeSequence2.mToneSet = this.mToneSetMap["alloff"];
				this.mTimeSequenceVector.Insert(0, binauralTimeSequence2);
			}
			while (i > this.mChannels.Count)
			{
				bool flag = true;
				Channel channel = null;
				channel.mLeftInstance = GlobalMembers.gApp.mSoundManager.GetSoundInstance(GlobalMembersResourcesWP.SOUND_SIN500);
				if (channel.mLeftInstance != null)
				{
					channel.mLeftInstance.SetMasterVolumeIdx(3);
					channel.mLeftInstance.SetVolume(0.0);
					channel.mLeftInstance.SetPan(-10000);
					channel.mLeftInstance.Play(true, false);
					channel.mRightInstance = GlobalMembers.gApp.mSoundManager.GetSoundInstance(GlobalMembersResourcesWP.SOUND_SIN500);
					if (channel.mRightInstance != null)
					{
						channel.mRightInstance.SetMasterVolumeIdx(3);
						channel.mRightInstance.SetVolume(0.0);
						channel.mRightInstance.SetPan(10000);
						channel.mRightInstance.Play(true, false);
						this.mChannels.Add(channel);
						flag = false;
					}
					else
					{
						channel.mLeftInstance.Release();
					}
				}
				if (flag)
				{
					break;
				}
			}
			return true;
		}

		public void Update()
		{
			this.mCurTime += 0.01;
			if (this.mTimeSequenceVector.Count > 0)
			{
				if (this.mCurSeqIdx == -1)
				{
					this.mCurSeqIdx = 0;
				}
				BinauralTimeSequence binauralTimeSequence = this.mTimeSequenceVector[this.mCurSeqIdx];
				while (this.mCurSeqIdx < this.mTimeSequenceVector.Count - 1)
				{
					BinauralTimeSequence binauralTimeSequence2 = this.mTimeSequenceVector[this.mCurSeqIdx + 1];
					if (this.mCurTime < (double)binauralTimeSequence2.mTime)
					{
						break;
					}
					this.mCurSeqIdx++;
					binauralTimeSequence = this.mTimeSequenceVector[this.mCurSeqIdx];
				}
				if (binauralTimeSequence.mCrossfade)
				{
					BinauralTimeSequence binauralTimeSequence3 = this.mTimeSequenceVector[this.mCurSeqIdx + 1];
					float num = (float)((this.mCurTime - (double)binauralTimeSequence.mTime) / (double)(binauralTimeSequence3.mTime - binauralTimeSequence.mTime));
					int num2 = GlobalMembers.MAX(binauralTimeSequence.mToneSet.mToneDefVector.Count, binauralTimeSequence3.mToneSet.mToneDefVector.Count);
					for (int i = 0; i < num2; i++)
					{
						double num3 = 0.0;
						double num4 = 0.0;
						double num5 = 0.0;
						BinauralToneDef binauralToneDef = null;
						if (i < binauralTimeSequence.mToneSet.mToneDefVector.Count)
						{
							binauralToneDef = binauralTimeSequence.mToneSet.mToneDefVector[i];
							num5 = binauralToneDef.mVolume;
							num3 = binauralToneDef.mCarrier;
							num4 = binauralToneDef.mFreq;
						}
						double num6 = num3;
						double num7 = num4;
						double num8 = 0.0;
						if (i < binauralTimeSequence3.mToneSet.mToneDefVector.Count)
						{
							BinauralToneDef binauralToneDef2 = binauralTimeSequence3.mToneSet.mToneDefVector[i];
							num6 = binauralToneDef2.mCarrier;
							num7 = binauralToneDef2.mFreq;
							num8 = binauralToneDef2.mVolume;
							if (binauralToneDef == null)
							{
								num3 = num6;
								num4 = num7;
							}
						}
						double num9 = num3 * (1.0 - (double)num) + num6 * (double)num;
						double num10 = num4 * (1.0 - (double)num) + num7 * (double)num;
						double volume = num5 * (1.0 - (double)num) + num8 * (double)num;
						if (this.mChannels.Count > i)
						{
							Channel channel = this.mChannels[i];
							channel.mLeftInstance.SetBaseRate(num9 / 500.0);
							channel.mRightInstance.SetBaseRate((num9 + num10) / 500.0);
							if (GlobalMembers.gApp.mUpdateCount % 25 == 0)
							{
								channel.mLeftInstance.AdjustPitch(0.0);
								channel.mLeftInstance.SetVolume(volume);
								channel.mRightInstance.AdjustPitch(0.0);
								channel.mRightInstance.SetVolume(volume);
							}
						}
					}
					return;
				}
			}
			else
			{
				for (int j = 0; j < this.mChannels.Count; j++)
				{
					Channel channel2 = this.mChannels[j];
					if (channel2.mLeftInstance.GetVolume() > 0.0)
					{
						channel2.mLeftInstance.SetVolume(channel2.mLeftInstance.GetVolume() - (double)GlobalMembers.M(0.01f));
					}
					if (channel2.mRightInstance.GetVolume() > 0.0)
					{
						channel2.mRightInstance.SetVolume(channel2.mRightInstance.GetVolume() - (double)GlobalMembers.M(0.01f));
					}
				}
			}
		}

		public Dictionary<string, BinauralToneSet> mToneSetMap = new Dictionary<string, BinauralToneSet>();

		public List<BinauralTimeSequence> mTimeSequenceVector = new List<BinauralTimeSequence>();

		public int mCurSeqIdx;

		public List<Channel> mChannels = new List<Channel>();

		public double mCurTime;

		public string mDesc;
	}
}
