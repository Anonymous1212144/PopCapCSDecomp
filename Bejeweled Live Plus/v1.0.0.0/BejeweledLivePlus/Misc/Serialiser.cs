using System;
using System.Collections.Generic;
using SexyFramework;
using SexyFramework.Misc;

namespace BejeweledLivePlus.Misc
{
	public class Serialiser : Buffer
	{
		public Serialiser()
		{
			this.mNewerFile = false;
			this.mNewerBoard = false;
			this.mChunkError = false;
			this.mLoadingV1 = false;
			this.mLoadedHeaders = new GameChunkHeader[10];
			for (int i = 0; i < 10; i++)
			{
				this.mLoadedHeaders[i] = new GameChunkHeader();
			}
		}

		public void Copyfrom(Serialiser data)
		{
			this.mHeader.Copyfrom(data.mHeader);
			this.mNewerFile = data.mNewerFile;
			this.mChunkError = data.mChunkError;
			this.mLoadingV1 = data.mLoadingV1;
			for (int i = 0; i < 10; i++)
			{
				this.mLoadedHeaders[i].CopyFrom(data.mLoadedHeaders[i]);
			}
			this.mDataBitSize = data.mDataBitSize;
			this.mReadBitPos = data.mReadBitPos;
			this.mWriteBitPos = data.mWriteBitPos;
			base.SetData(data.mData);
		}

		public void WriteFileHeader(int BoardVersion, int GameType)
		{
			this.WriteFileHeader(BoardVersion, GameType, 0);
		}

		public void WriteFileHeader(int BoardVersion, int GameType, int NumberGameChunks)
		{
			base.Seek(Buffer.SeekMode.eWriteStart);
			this.mHeader.mOldHeader.mMagic = 37803;
			this.mHeader.mOldHeader.mGameVersion = 103;
			this.mHeader.mOldHeader.mBoardVersion = BoardVersion;
			this.mHeader.mOldHeader.mPlatform = 1;
			this.mHeader.mGameChunkCount = NumberGameChunks;
			this.mHeader.mGameType = GameType;
			this.mHeader.write(this);
		}

		public bool FinalizeFileHeader()
		{
			bool result;
			using (new BufferRestoreSeekRaii(this))
			{
				base.Seek(Buffer.SeekMode.eReadStart);
				if (base.ReadInt32() != 37803)
				{
					result = false;
				}
				else
				{
					base.SeekWriteByte(24);
					base.WriteInt32(this.mHeader.mGameChunkCount);
					result = true;
				}
			}
			return result;
		}

		public int WriteGameChunkHeader(GameChunkId GameId)
		{
			return this.WriteGameChunkHeader(GameId, -1);
		}

		public int WriteGameChunkHeader(GameChunkId GameId, int Size)
		{
			int currWriteBytePos = base.GetCurrWriteBytePos();
			new GameChunkHeader
			{
				mMagic = 4557,
				mId = (int)GameId,
				mOffset = currWriteBytePos,
				mSize = Size
			}.write(this);
			this.mHeader.mGameChunkCount++;
			return currWriteBytePos;
		}

		public bool FinalizeGameChunkHeader(int chunkBeginLoc)
		{
			int theInt = base.GetCurrWriteBytePos() - chunkBeginLoc - GameChunkHeader.size();
			bool result;
			using (new BufferRestoreSeekRaii(this))
			{
				base.SeekReadByte(chunkBeginLoc);
				if (base.ReadInt32() != 4557)
				{
					result = false;
				}
				else
				{
					base.SeekWriteByte(chunkBeginLoc + 12);
					base.WriteInt32(theInt);
					result = true;
				}
			}
			return result;
		}

		public void WriteValuePair(Serialiser.PairID id, int value)
		{
			base.WriteInt32((int)id);
			base.WriteInt32(value);
		}

		public void WriteValuePair(Serialiser.PairID id, uint value)
		{
			base.WriteInt32((int)((Serialiser.PairID)1048576 | id));
			base.WriteInt32((int)value);
		}

		public void WriteValuePair(Serialiser.PairID id, long value)
		{
			base.WriteInt32((int)((Serialiser.PairID)11534336 | id));
			base.WriteInt64(value);
		}

		public void WriteValuePair(Serialiser.PairID id, ulong value)
		{
			base.WriteInt32((int)((Serialiser.PairID)12582912 | id));
			base.WriteInt64((long)value);
		}

		public void WriteValuePair(Serialiser.PairID id, short value)
		{
			base.WriteInt32((int)((Serialiser.PairID)4194304 | id));
			base.WriteInt16(value);
		}

		public void WriteValuePair(Serialiser.PairID id, byte value)
		{
			base.WriteInt32((int)((Serialiser.PairID)6291456 | id));
			base.WriteByte(value);
		}

		public void WriteValuePair(Serialiser.PairID id, sbyte value)
		{
			base.WriteInt32((int)((Serialiser.PairID)7340032 | id));
			base.WriteByte((byte)value);
		}

		public void WriteValuePair(Serialiser.PairID id, float value)
		{
			base.WriteInt32((int)((Serialiser.PairID)8388608 | id));
			base.WriteFloat(value);
		}

		public void WriteValuePair(Serialiser.PairID id, double value)
		{
			base.WriteInt32((int)((Serialiser.PairID)9437184 | id));
			base.WriteDouble(value);
		}

		public void WriteValuePair(Serialiser.PairID id, bool value)
		{
			base.WriteInt32((int)((Serialiser.PairID)10485760 | id));
			base.WriteBoolean(value);
		}

		public void WriteValuePair(Serialiser.PairID id, CurvedVal value)
		{
			base.WriteInt32((int)((Serialiser.PairID)16777216 | id));
			this.WriteCurvedVal(value);
		}

		public void WriteArrayPair(Serialiser.PairID id, int num, int[] array)
		{
			base.WriteInt32((int)((Serialiser.PairID)268435456 | id));
			base.WriteInt32(num);
			for (int i = 0; i < num; i++)
			{
				base.WriteInt32(array[i]);
			}
		}

		public void WriteArrayPair(Serialiser.PairID id, int num, uint[] array)
		{
			base.WriteInt32((int)((Serialiser.PairID)269484032 | id));
			base.WriteInt32(num);
			for (int i = 0; i < num; i++)
			{
				base.WriteInt32((int)array[i]);
			}
		}

		public void WriteArrayPair(Serialiser.PairID id, int num, short[] array)
		{
			base.WriteInt32((int)((Serialiser.PairID)272629760 | id));
			base.WriteInt32(num);
			for (int i = 0; i < num; i++)
			{
				base.WriteInt16(array[i]);
			}
		}

		public void WriteArrayPair(Serialiser.PairID id, int num, byte[] array)
		{
			base.WriteInt32((int)((Serialiser.PairID)274726912 | id));
			base.WriteInt32(num);
			for (int i = 0; i < num; i++)
			{
				base.WriteByte(array[i]);
			}
		}

		public void WriteArrayPair(Serialiser.PairID id, int num, float[] array)
		{
			base.WriteInt32((int)((Serialiser.PairID)276824064 | id));
			base.WriteInt32(num);
			for (int i = 0; i < num; i++)
			{
				base.WriteFloat(array[i]);
			}
		}

		public void WriteArrayPair(Serialiser.PairID id, int num, double[] array)
		{
			base.WriteInt32((int)((Serialiser.PairID)277872640 | id));
			base.WriteInt32(num);
			for (int i = 0; i < num; i++)
			{
				base.WriteDouble(array[i]);
			}
		}

		public void WriteVectorPair(Serialiser.PairID id, List<int> array)
		{
			base.WriteInt32((int)((Serialiser.PairID)537919488 | id));
			int count = array.Count;
			base.WriteInt32(count);
			for (int i = 0; i < count; i++)
			{
				base.WriteInt32(array[i]);
			}
		}

		public void WriteByteVectorPair(Serialiser.PairID id, List<byte> buffer)
		{
			base.WriteInt32((int)((Serialiser.PairID)543162368 | id));
			int num = buffer.size<byte>();
			base.WriteInt32(num);
			for (int i = 0; i < num; i++)
			{
				base.WriteByte(buffer[i]);
			}
		}

		public void WriteBufferPair(Serialiser.PairID id, Buffer theBuffer)
		{
			this.WriteByteVectorPair(id, theBuffer.mData);
		}

		public void WriteStringPair(Serialiser.PairID id, string str)
		{
			base.WriteInt32((int)((Serialiser.PairID)33554432 | id));
			base.WriteString(str);
		}

		public void WriteSpecialBlock(Serialiser.PairID id, int num)
		{
			base.WriteInt32((int)((Serialiser.PairID)(-2146435072) | id));
			base.WriteInt32(num);
		}

		public void WriteSpecialBlock(Serialiser.PairID id, int num, int num2)
		{
			base.WriteInt32((int)((Serialiser.PairID)(-1877999616) | id));
			base.WriteInt32(num);
			base.WriteInt32(num2);
		}

		public bool ReadFileHeader(out int GameVersion, out int BoardVersion, out int platform)
		{
			GameVersion = -1;
			BoardVersion = -1;
			platform = -1;
			base.SeekFront();
			this.mHeader.mOldHeader.read(this);
			if (this.mHeader.mOldHeader.mMagic != 37803)
			{
				return false;
			}
			if (this.mHeader.mOldHeader.mGameVersion == 101)
			{
				this.mLoadingV1 = true;
			}
			else
			{
				this.mHeader.read(this, SaveFileHeader.ReadContent.Self);
			}
			GameVersion = this.mHeader.mOldHeader.mGameVersion;
			BoardVersion = this.mHeader.mOldHeader.mBoardVersion;
			platform = this.mHeader.mOldHeader.mPlatform;
			if (GameVersion < 101)
			{
				return false;
			}
			if (BoardVersion < 101)
			{
				return false;
			}
			if (GameVersion > 103)
			{
				this.mNewerFile = true;
			}
			if (BoardVersion > 101)
			{
				this.mNewerBoard = true;
			}
			if (this.mNewerFile)
			{
				base.SeekReadByte(GlobalMembersSerialiser.SaveFileHeader_GetOffsetToFirstChunk(this.mHeader));
			}
			if (!this.mLoadingV1)
			{
				for (int i = 0; i < 10; i++)
				{
					this.mLoadedHeaders[i] = new GameChunkHeader();
					this.mLoadedHeaders[i].zero();
				}
				using (new BufferRestoreSeekRaii(this))
				{
					int num = 0;
					int num2 = 0;
					while (!this.mChunkError && num2 < this.mHeader.mGameChunkCount)
					{
						if (this.ReadGameChunkHeader(this.mLoadedHeaders[num2], out num))
						{
							base.Seek(Buffer.SeekMode.eReadForward, this.mLoadedHeaders[num2].mSize);
						}
						else
						{
							this.mChunkError = true;
						}
						num2++;
					}
				}
				if (this.mChunkError)
				{
					return false;
				}
			}
			return true;
		}

		public bool ReadGameChunkHeader(GameChunkHeader header, out int chunkBeginPos)
		{
			chunkBeginPos = 0;
			if (this.mLoadingV1)
			{
				return true;
			}
			chunkBeginPos = base.GetCurrReadBytePos();
			header.read(this);
			if (header.mMagic != 4557)
			{
				chunkBeginPos = 0;
				return false;
			}
			return true;
		}

		public bool CheckReadGameChunkHeader(GameChunkId expectedChunkId, GameChunkHeader header, out int chunkBeginPos)
		{
			chunkBeginPos = 0;
			if (this.mLoadingV1)
			{
				return true;
			}
			bool flag = this.ReadGameChunkHeader(header, out chunkBeginPos);
			return (!flag || expectedChunkId == (GameChunkId)header.mId) && flag;
		}

		public void ReadValuePair(out int theValue)
		{
			base.ReadInt32();
			theValue = base.ReadInt32();
		}

		public void ReadValuePair(out uint theValue)
		{
			base.ReadInt32();
			theValue = (uint)base.ReadInt32();
		}

		public void ReadValuePair(out long theValue)
		{
			base.ReadInt32();
			theValue = base.ReadInt64();
		}

		public void ReadValuePair(out ulong theValue)
		{
			base.ReadInt32();
			theValue = (ulong)base.ReadInt64();
		}

		public void ReadValuePair(out short theValue)
		{
			base.ReadInt32();
			theValue = base.ReadInt16();
		}

		public void ReadValuePair(out byte theValue)
		{
			base.ReadInt32();
			theValue = base.ReadByte();
		}

		public void ReadValuePair(out sbyte theValue)
		{
			base.ReadInt32();
			theValue = (sbyte)base.ReadByte();
		}

		public void ReadValuePair(out float theValue)
		{
			base.ReadInt32();
			theValue = base.ReadFloat();
		}

		public void ReadValuePair(out double theValue)
		{
			base.ReadInt32();
			theValue = base.ReadDouble();
		}

		public void ReadValuePair(out bool theValue)
		{
			base.ReadInt32();
			theValue = base.ReadBoolean();
		}

		public void ReadValuePair(out CurvedVal theValue)
		{
			base.ReadInt32();
			theValue = this.ReadCurvedVal();
		}

		public int ReadArrayPair(int max, uint[] array)
		{
			base.ReadInt32();
			int num = base.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				uint num2 = (uint)base.ReadInt32();
				if (i < max)
				{
					array[i] = num2;
				}
			}
			return num;
		}

		public int ReadArrayPair(int max, int[] array)
		{
			base.ReadInt32();
			int num = base.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				int num2 = base.ReadInt32();
				if (i < max)
				{
					array[i] = num2;
				}
			}
			return num;
		}

		public int ReadArrayPair(int max, short[] array)
		{
			base.ReadInt32();
			int num = base.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				short num2 = base.ReadInt16();
				if (i < max)
				{
					array[i] = num2;
				}
			}
			return num;
		}

		public int ReadArrayPair(int max, sbyte[] array)
		{
			base.ReadInt32();
			int num = base.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				sbyte b = (sbyte)base.ReadByte();
				if (i < max)
				{
					array[i] = b;
				}
			}
			return num;
		}

		public int ReadArrayPair(int max, float[] array)
		{
			base.ReadInt32();
			int num = base.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				float num2 = base.ReadFloat();
				if (i < max)
				{
					array[i] = num2;
				}
			}
			return num;
		}

		public int ReadArrayPair(int max, double[] array)
		{
			base.ReadInt32();
			int num = base.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				double num2 = base.ReadDouble();
				if (i < max)
				{
					array[i] = num2;
				}
			}
			return num;
		}

		public int ReadVectorPair(out List<int> array)
		{
			base.ReadInt32();
			int num = base.ReadInt32();
			array = new List<int>();
			for (int i = 0; i < num; i++)
			{
				array.Add(base.ReadInt32());
			}
			return num;
		}

		public void ReadByteVectorPair(out List<byte> buffer)
		{
			base.ReadInt32();
			int num = base.ReadInt32();
			buffer = new List<byte>();
			for (int i = 0; i < num; i++)
			{
				buffer.Add(base.ReadByte());
			}
		}

		public void ReadBufferPair(out Buffer theBuffer)
		{
			List<byte> data;
			this.ReadByteVectorPair(out data);
			theBuffer = new Buffer();
			theBuffer.SetData(data);
		}

		public void ReadStringPair(out string str)
		{
			base.ReadInt32();
			str = base.ReadString();
		}

		public void ReadSpecialBlock(out int num)
		{
			base.ReadInt32();
			num = base.ReadInt32();
		}

		public void ReadSpecialBlock(out int num, out int num2)
		{
			base.ReadInt32();
			num = base.ReadInt32();
			num2 = base.ReadInt32();
		}

		public void WriteIntArray(List<int> theArr)
		{
			base.WriteInt32(theArr.Count);
			for (int i = 0; i < theArr.Count; i++)
			{
				base.WriteInt32(theArr[i]);
			}
		}

		public void WriteCurvedVal(CurvedVal theCurvedVal)
		{
			int num;
			if (theCurvedVal.mRamp == 0)
			{
				num = 0;
			}
			else if (theCurvedVal.mRamp == 1 && theCurvedVal.mInMin == theCurvedVal.mInMax && theCurvedVal.mOutMin == theCurvedVal.mOutMax)
			{
				num = 1;
			}
			else
			{
				num = 2;
			}
			base.WriteInt32(num);
			if (num == 1)
			{
				base.WriteDouble(theCurvedVal.mOutMin);
				return;
			}
			if (num == 2)
			{
				base.WriteString(theCurvedVal.mCurveCacheRecord.mDataStr);
				base.WriteByte(theCurvedVal.mMode);
				base.WriteByte(theCurvedVal.mRamp);
				base.WriteDouble(theCurvedVal.mIncRate);
				base.WriteDouble(theCurvedVal.mOutMin);
				base.WriteDouble(theCurvedVal.mOutMax);
				int theInt = ((theCurvedVal.mAppUpdateCountSrc != null) ? theCurvedVal.mAppUpdateCountSrc : GlobalMembers.gApp.mUpdateCount) - theCurvedVal.mInitAppUpdateCount;
				base.WriteInt32(theInt);
				base.WriteDouble(theCurvedVal.mCurOutVal);
				base.WriteDouble(theCurvedVal.mPrevOutVal);
				base.WriteDouble(theCurvedVal.mInMin);
				base.WriteDouble(theCurvedVal.mInMax);
				base.WriteBoolean(theCurvedVal.mNoClip);
				base.WriteBoolean(theCurvedVal.mSingleTrigger);
				base.WriteBoolean(theCurvedVal.mOutputSync);
				base.WriteBoolean(theCurvedVal.mTriggered);
				base.WriteBoolean(theCurvedVal.mIsHermite);
				base.WriteBoolean(theCurvedVal.mAutoInc);
				base.WriteDouble(theCurvedVal.mPrevInVal);
				base.WriteDouble(theCurvedVal.mInVal);
			}
		}

		public List<int> ReadIntArray()
		{
			List<int> list = new List<int>();
			int num = base.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				list.Add(base.ReadInt32());
			}
			return list;
		}

		public CurvedVal ReadCurvedVal()
		{
			CurvedVal curvedVal = new CurvedVal();
			int num = base.ReadInt32();
			if (num == 0)
			{
				curvedVal.SetConstant(0.0);
				curvedVal.mRamp = 0;
			}
			else if (num == 1)
			{
				double num2 = base.ReadDouble();
				curvedVal.SetConstant((double)((float)num2));
			}
			else if (num == 2)
			{
				string text = base.ReadString();
				if (text.Length > 0)
				{
					curvedVal.SetCurve(text);
				}
				curvedVal.mMode = base.ReadByte();
				curvedVal.mRamp = base.ReadByte();
				curvedVal.mIncRate = base.ReadDouble();
				curvedVal.mOutMin = base.ReadDouble();
				curvedVal.mOutMax = base.ReadDouble();
				int num3 = base.ReadInt32();
				curvedVal.mInitAppUpdateCount = ((curvedVal.mAppUpdateCountSrc != null) ? curvedVal.mAppUpdateCountSrc : GlobalMembers.gApp.mUpdateCount) - num3;
				curvedVal.mCurOutVal = base.ReadDouble();
				curvedVal.mPrevOutVal = base.ReadDouble();
				curvedVal.mInMin = base.ReadDouble();
				curvedVal.mInMax = base.ReadDouble();
				curvedVal.mNoClip = base.ReadBoolean();
				curvedVal.mSingleTrigger = base.ReadBoolean();
				curvedVal.mOutputSync = base.ReadBoolean();
				curvedVal.mTriggered = base.ReadBoolean();
				curvedVal.mIsHermite = base.ReadBoolean();
				curvedVal.mAutoInc = base.ReadBoolean();
				curvedVal.mPrevInVal = base.ReadDouble();
				curvedVal.mInVal = base.ReadDouble();
			}
			return curvedVal;
		}

		public void WriteBytes(List<byte> buffer, int length)
		{
			int num = 0;
			foreach (byte theByte in buffer)
			{
				base.WriteByte(theByte);
				if (++num >= length)
				{
					break;
				}
			}
		}

		public SaveFileHeader mHeader = new SaveFileHeader();

		public bool mNewerFile;

		public bool mNewerBoard;

		public GameChunkHeader[] mLoadedHeaders;

		public bool mChunkError;

		public bool mLoadingV1;

		public enum TypeID : uint
		{
			SerialiserTypeSInt,
			SerialiserTypeUInt = 1048576U,
			SerialiserTypeSLong = 2097152U,
			SerialiserTypeULong = 3145728U,
			SerialiserTypeSShort = 4194304U,
			SerialiserTypeUShort = 5242880U,
			SerialiserTypeUChar = 6291456U,
			SerialiserTypeSChar = 7340032U,
			SerialiserTypeFloat = 8388608U,
			SerialiserTypeDouble = 9437184U,
			SerialiserTypeBool = 10485760U,
			SerialiserTypeSLongLong = 11534336U,
			SerialiserTypeULongLong = 12582912U,
			SerialiserTypeCurvedVal = 16777216U,
			SerialiserTypeStdString = 33554432U,
			SerialiserTypeUTF8String = 50331648U,
			SerialiserArray = 268435456U,
			SerialiserVector = 536870912U,
			SerialiserSpecialOne = 2147483648U,
			SerialiserSpecialTwo = 2415919104U,
			SerialiserTypeMask = 4293918720U
		}

		public enum PairID
		{
			Unknown,
			BoardUpdateCnt,
			BoardPieces,
			BoardBumpVelocities,
			BoardNextColumnCredit,
			BoardRand,
			BoardGameStats,
			BoardPoints,
			BoardPointsBreakdown,
			BoardMoneyDisp,
			BoardLevelBarPct,
			BoardCountdownBarPct,
			BoardLevelPointsTotal,
			BoardLevel,
			BoardPointMultiplier,
			BoardCurMoveCreditId,
			BoardCurMatchId,
			BoardGemFallDelay,
			BoardMoveCounter,
			BoardGameTicks,
			BoardIdleTicks,
			BoardLastMatchTick,
			BoardLastMatchTime,
			BoardMatchTallyCount,
			BoardSpeedModeFactor,
			BoardSpeedBonusAlpha,
			BoardSpeedBonusText,
			BoardSpeedometerPopup,
			BoardSpeedometerGlow,
			BoardSpeedBonusDisp,
			BoardSpeedNeedle,
			BoardSpeedBonusPoints,
			BoardSpeedBonusNum,
			BoardSpeedBonusCount,
			BoardSpeedBonusCountHighest,
			BoardSpeedBonusLastCount,
			BoardHasBoardSettled,
			BoardComboColors,
			BoardComboCount,
			BoardComboLen,
			BoardComboCountDisp,
			BoardComboFlashPct,
			BoardLastPlayerSwapColor,
			BoardWholeGameReplay,
			BoardNextPieceId,
			BoardScrambleDelayTicks,
			BoardGameFinished,
			BoardSwapData,
			BoardMoveData,
			BoardQueuedMoves,
			ReplayVersion,
			ReplayID,
			ReplaySaveBuffer,
			ReplayQueuedMoves,
			ReplayTutorialFlags,
			ReplayStateInfo,
			ReplayTicks,
			DigIdToTileData,
			DigTreasureEarnings,
			DigTextFlashTicks,
			DigNextBottomHypermixerWait,
			DigArtifactMinTiles,
			DigArtifactMaxTiles,
			DigNextArtifactTileCount,
			DigStartArtifactRow,
			DigHypercubes,
			DigGridDepth,
			DigRandRowIdx,
			DigFirstChestPt,
			DigArtifactPoss,
			DigPlacedArtifacts,
			DigCollectedArtifacts,
			DigTimeLimit,
			DigOldPieceData,
			DigRotatingCounter,
			DigChestOffsetY,
			DigChestPosX,
			DigChestSaveDataVec,
			SpeedBoard5SecChance,
			SpeedBoard5SecChanceStep,
			SpeedBoard5SecChanceLastRoll,
			SpeedBoard10SecChance,
			SpeedBoard10SecChanceStep,
			SpeedBoard10SecChanceLastRoll,
			SpeedBoardBonusTime,
			SpeedBoardBonusTimeDisp,
			SpeedBoardGameTicks,
			SpeedBoardCollectorExtendPct,
			SpeedBoardPanicScalePct,
			SpeedBoardTimeScaleOverride,
			SpeedBoardTotalBonusTime,
			IceStormStageNum,
			IceStormColCountColComboValueDisp,
			IceStormColCountStartTick,
			IceStormColCountStartUpdateTick,
			IceStormColCountDuration,
			IceStormColCountComboCount,
			IceStormColCountColComboAlpha,
			IceStormColCountColComboScale,
			IceStormColCountColComboY,
			IceStormColComboBonusPoints,
			IceStormColComboHighest,
			IceStormColClearBonusPoints,
			IceStormAnimUpdateCount,
			IceStormLoseColumn,
			IceStormLastIceRemoved,
			IceStormIceRemoved,
			IceStormStartDelay,
			IceStormLevelProgress,
			IceStormLevelProgressTotal,
			IceStormTotalLoseTicks,
			IceStormNextTryColStart,
			IceStormStageDuration,
			IceStormStageStartAtTick,
			IceStormShakeCooldown,
			IceStormBackDim,
			BflyDefMoveCountdown,
			BflyDefDropCountdown,
			BflyGrabbedAt,
			BflyDropCountdown,
			BflySpawnCountStart,
			BflyDropCountdownPerLevel,
			BflyMoveCountdownPerLevel,
			BflySpawnCountMax,
			BflySpawnCountPerLevel,
			BflySpawnCountAcc,
			BflyLidOpenPct,
			BflySpiderCol,
			BflySpiderWalkColFrom,
			BflySpiderWalkColTo,
			BflySpawnCount,
			BflySideSpawnChance,
			BflySideSpawnChancePerLevel,
			BflySideSpawnChanceMax,
			BflyAllowNewComboFloaters,
			BflySpiderWalkPct,
			BflyLidOpenTarget,
			PokerCardIdx,
			PokerCardScoreIdx,
			PokerGoal,
			PokerHands,
			PokerHandsDelat,
			PokerSkullsBusted,
			PokerBestHandsPts,
			PokerStartHands,
			PokerChipSoundDelay,
			PokerSkullHand,
			PokerSkullMax,
			PokerNumCoinFlips,
			PokerFlameBonus,
			PokerStarBonus,
			PokerScoreTally,
			PokerCurrentHandIdx,
			PokerFlameMoveCreditId,
			PokerLaserMoveCreditId,
			PokerHandCount,
			PokerSkullSpawnPct,
			PokerSkullBusterPct,
			PokerSkullBusterDisp,
			PokerBadFlip,
			PokerScoreHandPct,
			PokerCardBulgePct,
			PokerSkullScale,
			PokerHiLitePct,
			PokerCoinFlipPct,
			PokerCoinWonPct,
			PokerSkullCrusherAnimPct,
			PokerSkullBarLidPct,
			PokerScoreName,
			BlazingSpeedPercent,
			BlazingSpeedNum,
			MetricsVersion,
			GameSessionIdClassic,
			GameSessionIdZen,
			GameSessionIdDiamondMine,
			GameSessionIdButterfly
		}

		public enum Platforms
		{
			SerialiserPlatformUnknown,
			SerialiserPlatformIOSPhone,
			SerialiserPlatformIOSPad
		}
	}
}
