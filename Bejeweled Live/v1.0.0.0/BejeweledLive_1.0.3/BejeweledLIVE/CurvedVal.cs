using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Sexy;

namespace BejeweledLIVE
{
	public class CurvedVal
	{
		public static void PreAllocateMemory()
		{
			List<CurvedVal> list = new List<CurvedVal>();
			for (int i = 0; i < 100; i++)
			{
				list.Add(CurvedVal.GetNewCurvedVal());
			}
			for (int j = 0; j < list.Count; j++)
			{
				list[j].PrepareForReuse();
			}
		}

		public static CurvedVal GetNewCurvedVal()
		{
			if (CurvedVal.unusedObjects.Count > 0)
			{
				CurvedVal curvedVal = CurvedVal.unusedObjects.Pop();
				curvedVal.Reset();
				return curvedVal;
			}
			return new CurvedVal();
		}

		public void PrepareForReuse()
		{
			this.Reset();
			CurvedVal.unusedObjects.Push(this);
		}

		public void Reset()
		{
			this.mMode = CurvedVal.CurveMode.MODE_CLAMP;
			this.mRamp = CurvedVal.Ramp.RAMP_NONE;
			this.mCurveCacheRecord = null;
			this.mSingleTrigger = false;
			this.mNoClip = false;
			this.mOutputSync = false;
			this.mTriggered = false;
			this.mIsHermite = false;
			this.mAutoInc = false;
			this.mInitAppUpdateCount = 0;
			this.mOutMin = 0.0;
			this.mOutMax = 1.0;
			this.mInMin = 0.0;
			this.mInMax = 1.0;
			this.mLinkedVal = null;
			this.mCurOutVal = 0.0;
			this.mInVal = 0.0;
			this.mPrevInVal = 0.0;
			this.mIncRate = 0.0;
			this.mPrevOutVal = 0.0;
			this.mDataP = null;
			this.mCurDataPStr = null;
		}

		private CurvedVal()
		{
			this.Reset();
		}

		protected bool CheckCurveChange()
		{
			if (this.mDataP != null && this.mDataP != this.mCurDataPStr)
			{
				this.mCurDataPStr = this.mDataP;
				this.ParseDataString(this.mCurDataPStr);
				return true;
			}
			return false;
		}

		protected bool CheckClamping()
		{
			this.CheckCurveChange();
			if (this.mMode == CurvedVal.CurveMode.MODE_CLAMP)
			{
				if (this.mInVal < this.mInMin)
				{
					this.mInVal = this.mInMin;
					return false;
				}
				if (this.mInVal > this.mInMax)
				{
					this.mInVal = this.mInMax;
					return false;
				}
			}
			else if (this.mMode == CurvedVal.CurveMode.MODE_REPEAT || this.mMode == CurvedVal.CurveMode.MODE_PING_PONG)
			{
				double num = this.mInMax - this.mInMin;
				if (this.mInVal > this.mInMax || this.mInVal < this.mInMin)
				{
					this.mInVal = this.mInMin + Math.IEEERemainder(this.mInVal - this.mInMin + num, num);
				}
			}
			return true;
		}

		protected void GenerateTable(List<CurvedVal.DataPoint> theDataPointVector, float[] theBuffer, int theSize)
		{
			BSpline newBSpline = BSpline.GetNewBSpline();
			for (int i = 0; i < theDataPointVector.Count; i++)
			{
				CurvedVal.DataPoint dataPoint = theDataPointVector[i];
				newBSpline.AddPoint(dataPoint.mX, dataPoint.mY);
			}
			newBSpline.CalculateSpline();
			bool flag = true;
			int num = 0;
			float num2 = 0f;
			for (int j = 1; j < theDataPointVector.Count; j++)
			{
				CurvedVal.DataPoint dataPoint2 = theDataPointVector[j - 1];
				CurvedVal.DataPoint dataPoint3 = theDataPointVector[j];
				int num3 = (int)((double)(dataPoint2.mX * (float)(theSize - 1)) + 0.5);
				int num4 = (int)((double)(dataPoint3.mX * (float)(theSize - 1)) + 0.5);
				for (int k = num3; k <= num4; k++)
				{
					float t = (float)(j - 1) + (float)(k - num3) / (float)(num4 - num3);
					float ypoint = newBSpline.GetYPoint(t);
					float xpoint = newBSpline.GetXPoint(t);
					int num5 = (int)((double)(xpoint * (float)(theSize - 1)) + 0.5);
					if (num5 >= num && num5 <= num4)
					{
						if (!flag)
						{
							if (num5 > num + 1)
							{
								for (int l = num; l <= num5; l++)
								{
									float num6 = (float)(l - num) / (float)(num5 - num);
									float num7 = num6 * ypoint + (1f - num6) * num2;
									if (!this.mNoClip)
									{
										num7 = Math.Min(Math.Max(num7, 0f), 1f);
									}
									if (theBuffer.Length - 1 < l && theBuffer.Length < l + 1)
									{
										Array.Resize<float>(ref theBuffer, l + 1);
									}
									theBuffer[l] = num7;
								}
							}
							else
							{
								float num8 = ypoint;
								if (!this.mNoClip)
								{
									num8 = Math.Min(Math.Max(num8, 0f), 1f);
								}
								if (theBuffer.Length - 1 < num5 && theBuffer.Length < num5 + 1)
								{
									Array.Resize<float>(ref theBuffer, num5 + 1);
								}
								theBuffer[num5] = num8;
							}
						}
						num = num5;
						num2 = ypoint;
						flag = false;
					}
				}
			}
			for (int m = 0; m < theDataPointVector.Count; m++)
			{
				CurvedVal.DataPoint dataPoint4 = theDataPointVector[m];
				int num9 = (int)((double)(dataPoint4.mX * (float)(theSize - 1)) + 0.5);
				theBuffer[num9] = dataPoint4.mY;
			}
			newBSpline.PrepareForReuse();
		}

		protected void ParseDataString(CurvedValDefinition theString)
		{
			this.mIncRate = 0.0;
			this.mOutMin = 0.0;
			this.mOutMax = 1.0;
			this.mSingleTrigger = false;
			this.mNoClip = false;
			this.mOutputSync = false;
			this.mIsHermite = false;
			this.mAutoInc = false;
			int aVersion = theString.aVersion;
			if (aVersion >= 1)
			{
				int aFlags = theString.aFlags;
				this.mNoClip = (aFlags & 1) != 0;
				this.mSingleTrigger = (aFlags & 2) != 0;
				this.mOutputSync = (aFlags & 4) != 0;
				this.mIsHermite = (aFlags & 8) != 0;
				this.mAutoInc = (aFlags & 16) != 0;
			}
			if (theString.mIsHermite)
			{
				this.mIsHermite = true;
				return;
			}
			this.mOutMin = (double)theString.mOutMin;
			this.mOutMax = (double)theString.mOutMax;
			this.mIncRate = (double)theString.mIncRate;
			if (aVersion >= 1)
			{
				this.mInMax = (double)theString.mInMax;
			}
			this.mCurveCacheRecord = null;
			if (!CurvedVal.mCurveCacheMap.TryGetValue(theString.aCurveString, ref this.mCurveCacheRecord))
			{
				this.mCurveCacheRecord = CurvedVal.CurveCacheRecord.GetNewCurveCacheRecord();
				CurvedVal.mCurveCacheMap.Add(theString.aCurveString, this.mCurveCacheRecord);
				float num = 0f;
				List<CurvedVal.DataPoint> list = new List<CurvedVal.DataPoint>();
				int i = 0;
				while (i < theString.aCurveString.Length)
				{
					sbyte b = (sbyte)theString.aCurveString.get_Chars(i++);
					CurvedVal.DataPoint dataPoint = default(CurvedVal.DataPoint);
					dataPoint.mX = num;
					dataPoint.mY = GlobalMembersCurvedVal.CVCharToFloat(b);
					if (this.mIsHermite)
					{
						string theStr = theString.aCurveString.Substring(i, 3);
						dataPoint.mAngleDeg = GlobalMembersCurvedVal.CVStrToAngle(theStr);
						i += 3;
					}
					else
					{
						dataPoint.mAngleDeg = 0f;
					}
					list.Add(dataPoint);
					while (i < theString.aCurveString.Length)
					{
						b = (sbyte)theString.aCurveString.get_Chars(i++);
						if (b != 32)
						{
							num = Math.Min(num + GlobalMembersCurvedVal.CVCharToFloat(b) * 0.1f, 1f);
							break;
						}
						num += 0.1f;
					}
				}
				this.GenerateTable(list, this.mCurveCacheRecord.mTable, 8192);
				this.mCurveCacheRecord.mHermiteCurve.mPoints.Clear();
				for (int j = 0; j < list.Count; j++)
				{
					float inFxPrime = (float)Math.Tan((double)SexyMath.DegToRad(list[j].mAngleDeg));
					this.mCurveCacheRecord.mHermiteCurve.mPoints.Add(new SexyMathHermite.SPoint(list[j].mX, list[j].mY, inFxPrime));
				}
				this.mCurveCacheRecord.mHermiteCurve.Rebuild();
			}
		}

		public void SetCurve(CurvedValDefinition theData)
		{
			this.SetCurve(theData, null);
		}

		public void SetCurve(CurvedValDefinition theData, CurvedVal theLinkedVal)
		{
			this.mDataP = null;
			this.mCurDataPStr = null;
			if (GlobalStaticVars.gSexyAppBase != null)
			{
				this.mInitAppUpdateCount = GlobalStaticVars.gSexyAppBase.mUpdateCount;
			}
			this.mTriggered = false;
			this.mLinkedVal = theLinkedVal;
			this.mRamp = CurvedVal.Ramp.RAMP_CURVEDATA;
			this.ParseDataString(theData);
			this.mInVal = this.mInMin;
		}

		public void SetCurve(ref CurvedValDefinition theData)
		{
			this.SetCurve(theData, null);
		}

		public void SetCurve(ref CurvedValDefinition theData, CurvedVal theLinkedVal)
		{
			this.mDataP = theData;
			this.mCurDataPStr = theData;
			if (GlobalStaticVars.gSexyAppBase != null)
			{
				this.mInitAppUpdateCount = GlobalStaticVars.gSexyAppBase.mUpdateCount;
			}
			this.mTriggered = false;
			this.mLinkedVal = theLinkedVal;
			this.mRamp = CurvedVal.Ramp.RAMP_CURVEDATA;
			this.ParseDataString(this.mCurDataPStr);
			this.mInVal = this.mInMin;
		}

		public void SetConstant(double theValue)
		{
			this.mInVal = 0.0;
			this.mTriggered = false;
			this.mLinkedVal = null;
			this.mRamp = CurvedVal.Ramp.RAMP_LINEAR;
			this.mInMin = (this.mInMax = 0.0);
			this.mOutMax = theValue;
			this.mOutMin = theValue;
		}

		public bool IsInitialized()
		{
			return this.mRamp != CurvedVal.Ramp.RAMP_NONE;
		}

		public void SetMode(int theMode)
		{
			this.SetMode((CurvedVal.CurveMode)theMode);
		}

		public void SetMode(CurvedVal.CurveMode theMode)
		{
			this.mMode = theMode;
		}

		public void SetRamp(int theRamp)
		{
			this.SetRamp((CurvedVal.Ramp)theRamp);
		}

		public void SetRamp(CurvedVal.Ramp theRamp)
		{
			this.mRamp = theRamp;
		}

		public void SetOutRange(double theMin, double theMax)
		{
			this.mOutMin = theMin;
			this.mOutMax = theMax;
		}

		public void SetInRange(double theMin, double theMax)
		{
			this.mInMin = theMin;
			this.mInMax = theMax;
		}

		public double GetOutVal()
		{
			double outVal = this.GetOutVal(this.GetInVal());
			this.mCurOutVal = outVal;
			return outVal;
		}

		public double GetOutVal(double theInVal)
		{
			switch (this.mRamp)
			{
			case CurvedVal.Ramp.RAMP_NONE:
			case CurvedVal.Ramp.RAMP_LINEAR:
				if (this.mMode == CurvedVal.CurveMode.MODE_PING_PONG)
				{
					if (theInVal - this.mInMin <= (this.mInMax - this.mInMin) / 2.0)
					{
						return this.mOutMin + (theInVal - this.mInMin) / (this.mInMax - this.mInMin) * (this.mOutMax - this.mOutMin) * 2.0;
					}
					return this.mOutMin + (1.0 - (theInVal - this.mInMin) / (this.mInMax - this.mInMin)) * (this.mOutMax - this.mOutMin) * 2.0;
				}
				else
				{
					if (this.mInMin == this.mInMax)
					{
						return this.mOutMin;
					}
					return this.mOutMin + (theInVal - this.mInMin) / (this.mInMax - this.mInMin) * (this.mOutMax - this.mOutMin);
				}
				break;
			case CurvedVal.Ramp.RAMP_SLOW_TO_FAST:
			{
				double num = (theInVal - this.mInMin) / (this.mInMax - this.mInMin) * 3.14159 / 2.0;
				if (this.mMode == CurvedVal.CurveMode.MODE_PING_PONG)
				{
					num *= 2.0;
				}
				if (num > 1.570795)
				{
					num = 3.14159 - num;
				}
				return this.mOutMin + (1.0 - Math.Cos(num)) * (this.mOutMax - this.mOutMin);
			}
			case CurvedVal.Ramp.RAMP_FAST_TO_SLOW:
			{
				double num2 = (theInVal - this.mInMin) / (this.mInMax - this.mInMin) * 3.14159 / 2.0;
				if (this.mMode == CurvedVal.CurveMode.MODE_PING_PONG)
				{
					num2 *= 2.0;
				}
				return this.mOutMin + Math.Sin(num2) * (this.mOutMax - this.mOutMin);
			}
			case CurvedVal.Ramp.RAMP_SLOW_FAST_SLOW:
			{
				double num3 = (theInVal - this.mInMin) / (this.mInMax - this.mInMin) * 3.14159;
				if (this.mMode == CurvedVal.CurveMode.MODE_PING_PONG)
				{
					num3 *= 2.0;
				}
				return this.mOutMin + (-Math.Cos(num3) + 1.0) / 2.0 * (this.mOutMax - this.mOutMin);
			}
			case CurvedVal.Ramp.RAMP_FAST_SLOW_FAST:
			{
				double num4 = (theInVal - this.mInMin) / (this.mInMax - this.mInMin) * 3.14159;
				if (this.mMode == CurvedVal.CurveMode.MODE_PING_PONG)
				{
					num4 *= 2.0;
				}
				if (num4 > 3.14159)
				{
					num4 = 6.28318 - num4;
				}
				if (num4 < 1.570795)
				{
					return this.mOutMin + Math.Sin(num4) / 2.0 * (this.mOutMax - this.mOutMin);
				}
				return this.mOutMin + (2.0 - Math.Sin(num4)) / 2.0 * (this.mOutMax - this.mOutMin);
			}
			case CurvedVal.Ramp.RAMP_CURVEDATA:
			{
				this.CheckCurveChange();
				if (this.mCurveCacheRecord == null)
				{
					return 0.0;
				}
				if (this.mInMax - this.mInMin == 0.0)
				{
					return 0.0;
				}
				float num5 = Math.Min((float)((theInVal - this.mInMin) / (this.mInMax - this.mInMin)), 1f);
				if (this.mMode == CurvedVal.CurveMode.MODE_PING_PONG)
				{
					if (num5 > 0.5f)
					{
						num5 = (1f - num5) * 2f;
					}
					else
					{
						num5 *= 2f;
					}
				}
				if (this.mIsHermite)
				{
					double num6 = this.mOutMin + (double)this.mCurveCacheRecord.mHermiteCurve.Evaluate(num5) * (this.mOutMax - this.mOutMin);
					if (!this.mNoClip)
					{
						if (this.mOutMin < this.mOutMax)
						{
							num6 = Math.Min(Math.Max(num6, this.mOutMin), this.mOutMax);
						}
						else
						{
							num6 = Math.Max(Math.Min(num6, this.mOutMin), this.mOutMax);
						}
					}
					return num6;
				}
				float num7 = num5 * 8191f;
				int num8 = (int)num7;
				if (num8 == 8191)
				{
					return this.mOutMin + (double)this.mCurveCacheRecord.mTable[num8] * (this.mOutMax - this.mOutMin);
				}
				float num9 = num7 - (float)num8;
				return this.mOutMin + (double)(this.mCurveCacheRecord.mTable[num8] * (1f - num9) + this.mCurveCacheRecord.mTable[num8 + 1] * num9) * (this.mOutMax - this.mOutMin);
			}
			default:
				return this.mOutMin;
			}
		}

		public double GetOutValDelta()
		{
			return this.GetOutVal() - this.mPrevOutVal;
		}

		public double GetInVal()
		{
			double num = this.mInVal;
			if (this.mLinkedVal != null)
			{
				if (this.mLinkedVal.mOutputSync)
				{
					num = this.mLinkedVal.GetOutVal();
				}
				else
				{
					num = this.mLinkedVal.GetInVal();
				}
			}
			else if (this.mAutoInc)
			{
				if (GlobalStaticVars.gSexyAppBase != null)
				{
					num = this.mInMin + (double)(GlobalStaticVars.gSexyAppBase.mUpdateCount - this.mInitAppUpdateCount) * this.mIncRate;
				}
				if (this.mMode == CurvedVal.CurveMode.MODE_REPEAT || this.mMode == CurvedVal.CurveMode.MODE_PING_PONG)
				{
					num = Math.IEEERemainder(num - this.mInMin, this.mInMax - this.mInMin) + this.mInMin;
				}
				else
				{
					num = Math.Min(num, this.mInMax);
				}
			}
			if (this.mMode != CurvedVal.CurveMode.MODE_PING_PONG)
			{
				return num;
			}
			double num2 = (double)((float)((num - this.mInMin) / (this.mInMax - this.mInMin)));
			if (num2 > 0.5)
			{
				return this.mInMin + (1.0 - num2) * 2.0 * (this.mInMax - this.mInMin);
			}
			return this.mInMin + num2 * 2.0 * (this.mInMax - this.mInMin);
		}

		public bool SetInVal(double theVal)
		{
			this.mPrevOutVal = this.GetOutVal();
			this.mTriggered = false;
			this.mPrevInVal = theVal;
			this.mInVal = theVal;
			if (this.CheckClamping())
			{
				return true;
			}
			if (!this.mTriggered)
			{
				this.mTriggered = true;
				return false;
			}
			return this.mSingleTrigger;
		}

		public bool IncInVal(double theInc)
		{
			this.mPrevOutVal = this.GetOutVal();
			this.mPrevInVal = this.mInVal;
			this.mInVal += theInc;
			if (this.CheckClamping())
			{
				return true;
			}
			if (!this.mTriggered)
			{
				this.mTriggered = true;
				return false;
			}
			return this.mSingleTrigger;
		}

		public bool IncInVal()
		{
			return this.mIncRate != 0.0 && this.IncInVal(this.mIncRate);
		}

		public double GetInValAtUpdate(int theUpdateCount)
		{
			return this.mInMin + (double)theUpdateCount * this.mIncRate;
		}

		public int GetLengthInUpdates()
		{
			if (this.mIncRate == 0.0)
			{
				return -1;
			}
			return (int)Math.Ceiling((this.mInMax - this.mInMin) / this.mIncRate);
		}

		public bool CheckInThreshold(double theInVal)
		{
			double inVal = this.mInVal;
			double num = this.mPrevInVal;
			if (this.mAutoInc)
			{
				inVal = this.GetInVal();
				num = inVal - this.mIncRate * 1.5;
			}
			return theInVal > num && theInVal <= inVal;
		}

		public bool CheckUpdatesFromEndThreshold(int theUpdateCount)
		{
			return this.CheckInThreshold(this.GetInValAtUpdate(this.GetLengthInUpdates() - theUpdateCount));
		}

		public bool HasBeenTriggered()
		{
			if (this.mAutoInc)
			{
				this.mTriggered = this.GetInVal() == this.mInMax;
			}
			return this.mTriggered;
		}

		public void ClearTrigger()
		{
			this.mTriggered = false;
		}

		public double GetDouble
		{
			get
			{
				return this.GetOutVal();
			}
		}

		public float GetFloat
		{
			get
			{
				return (float)this.GetOutVal();
			}
		}

		public Color GetColour
		{
			get
			{
				return new Color(255, 255, 255, (int)(255.0 * this.GetOutVal()));
			}
		}

		public const double PI = 3.14159;

		public static Dictionary<string, CurvedVal.CurveCacheRecord> mCurveCacheMap = new Dictionary<string, CurvedVal.CurveCacheRecord>();

		public CurvedVal.CurveMode mMode;

		public CurvedVal.Ramp mRamp;

		public double mIncRate;

		public double mOutMin;

		public double mOutMax;

		public CurvedValDefinition mDataP;

		public CurvedValDefinition mCurDataPStr;

		public int mInitAppUpdateCount;

		public CurvedVal mLinkedVal;

		public CurvedVal.CurveCacheRecord mCurveCacheRecord;

		public double mCurOutVal;

		public double mPrevOutVal;

		public double mInMin;

		public double mInMax;

		public bool mNoClip;

		public bool mSingleTrigger;

		public bool mOutputSync;

		public bool mTriggered;

		public bool mIsHermite;

		public bool mAutoInc;

		public double mPrevInVal;

		public double mInVal;

		private static Stack<CurvedVal> unusedObjects = new Stack<CurvedVal>();

		public enum CurveMode
		{
			MODE_CLAMP,
			MODE_REPEAT,
			MODE_PING_PONG
		}

		public enum Ramp
		{
			RAMP_NONE,
			RAMP_LINEAR,
			RAMP_SLOW_TO_FAST,
			RAMP_FAST_TO_SLOW,
			RAMP_SLOW_FAST_SLOW,
			RAMP_FAST_SLOW_FAST,
			RAMP_CURVEDATA
		}

		public enum DFlags
		{
			DFLAG_NOCLIP = 1,
			DFLAG_SINGLETRIGGER,
			DFLAG_OUTPUTSYNC = 4,
			DFLAG_HERMITE = 8,
			DFLAG_AUTOINC = 16
		}

		public struct DataPoint
		{
			public override string ToString()
			{
				return string.Format("DataPoint: {0},{1},{2}", this.mX, this.mY, this.mAngleDeg);
			}

			public float mX;

			public float mY;

			public float mAngleDeg;
		}

		public class CurveCacheRecord
		{
			public static CurvedVal.CurveCacheRecord GetNewCurveCacheRecord()
			{
				if (CurvedVal.CurveCacheRecord.unusedObjects.Count > 0)
				{
					return CurvedVal.CurveCacheRecord.unusedObjects.Pop();
				}
				return new CurvedVal.CurveCacheRecord();
			}

			public void PrepareForReuse()
			{
				this.Reset();
				CurvedVal.CurveCacheRecord.unusedObjects.Push(this);
			}

			private CurveCacheRecord()
			{
			}

			private void Reset()
			{
				for (int i = 0; i < this.mTable.Length; i++)
				{
					this.mTable[i] = 0f;
				}
				this.mHermiteCurve.Reset();
			}

			public float[] mTable = new float[8192];

			public SexyMathHermite mHermiteCurve = SexyMathHermite.GetNewSexyMathHermite();

			private static Stack<CurvedVal.CurveCacheRecord> unusedObjects = new Stack<CurvedVal.CurveCacheRecord>();
		}
	}
}
