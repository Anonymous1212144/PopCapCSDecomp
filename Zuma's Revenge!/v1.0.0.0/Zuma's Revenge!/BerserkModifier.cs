using System;
using System.Globalization;

namespace ZumasRevenge
{
	public class BerserkModifier
	{
		public BerserkModifier(BerserkModifier rhs)
		{
			this.mParamName = rhs.mParamName;
			this.mStringValue = rhs.mStringValue;
			this.mMinStr = rhs.mMinStr;
			this.mMaxStr = rhs.mMaxStr;
			this.mOverride = rhs.mOverride;
			this.mParamType = rhs.mParamType;
			this.mHasMin = rhs.mHasMin;
			this.mHasMax = rhs.mHasMax;
		}

		public BerserkModifier(string p, string value, string minval, string maxval, bool _override)
		{
			this.mParamName = p;
			this.mStringValue = value;
			this.mHasMin = (this.mHasMax = false);
			this.mOverride = _override;
			if (minval != null && minval.Length > 0)
			{
				this.mHasMin = true;
				this.mMinStr = minval;
			}
			if (maxval != null && maxval.Length > 0)
			{
				this.mHasMax = true;
				this.mMaxStr = maxval;
			}
		}

		public BerserkModifier(string p, string value)
		{
			this.mParamName = p;
			this.mStringValue = value;
			this.mHasMin = (this.mHasMax = false);
			this.mOverride = false;
		}

		public void AddPointerFloat(object fptr)
		{
			this.mParamType = 1;
			this.mVariablePtr = fptr;
			if (this.mStringValue.get_Chars(0) == '.')
			{
				this.mStringValue = "0" + this.mStringValue;
			}
			double num = 0.0;
			double.TryParse(this.mStringValue, 167, CultureInfo.InvariantCulture, ref num);
			this.mValue = num;
			if (this.mHasMin)
			{
				this.mMin = Convert.ToSingle(this.mMinStr);
			}
			if (this.mHasMax)
			{
				this.mMax = Convert.ToSingle(this.mMaxStr);
			}
		}

		public void AddPointerInt(object iptr)
		{
			this.mParamType = 0;
			this.mVariablePtr = iptr;
			try
			{
				this.mValue = Convert.ToInt32(this.mStringValue);
			}
			catch (Exception)
			{
				this.mValue = 0;
			}
			if (this.mHasMin)
			{
				this.mMin = Convert.ToInt32(this.mMinStr);
			}
			if (this.mHasMax)
			{
				this.mMax = Convert.ToInt32(this.mMaxStr);
			}
		}

		public void AddPointerBool(object bptr)
		{
			this.mParamType = 2;
			this.mVariablePtr = bptr;
			this.mValue = Convert.ToBoolean(this.mStringValue);
			if (this.mHasMin)
			{
				this.mMin = Convert.ToBoolean(this.mMinStr);
			}
			if (this.mHasMax)
			{
				this.mMax = Convert.ToBoolean(this.mMaxStr);
			}
		}

		public void ModifyVariable()
		{
			if (this.mParamType == 1)
			{
				ParamData<float> paramData = this.mVariablePtr as ParamData<float>;
				if (this.mOverride)
				{
					paramData.value = Convert.ToSingle(this.mValue);
					return;
				}
				paramData.value += Convert.ToSingle(this.mValue);
				if (this.mHasMin && paramData.value < Convert.ToSingle(this.mMin))
				{
					paramData.value = Convert.ToSingle(this.mMin);
					return;
				}
				if (this.mHasMax && paramData.value > Convert.ToSingle(this.mMax))
				{
					paramData.value = Convert.ToSingle(this.mMax);
					return;
				}
			}
			else if (this.mParamType == 0)
			{
				ParamData<int> paramData2 = this.mVariablePtr as ParamData<int>;
				if (this.mOverride)
				{
					paramData2.value = Convert.ToInt32(this.mValue);
					return;
				}
				paramData2.value += Convert.ToInt32(this.mValue);
				if (this.mHasMin && paramData2.value < Convert.ToInt32(this.mMin))
				{
					paramData2.value = Convert.ToInt32(this.mMin);
					return;
				}
				if (this.mHasMax && paramData2.value > Convert.ToInt32(this.mMax))
				{
					paramData2.value = Convert.ToInt32(this.mMax);
					return;
				}
			}
			else if (this.mParamType == 2)
			{
				ParamData<bool> paramData3 = this.mVariablePtr as ParamData<bool>;
				paramData3.value = Convert.ToBoolean(this.mValue);
			}
		}

		public string mParamName;

		public string mStringValue;

		public string mMinStr;

		public string mMaxStr;

		public bool mOverride;

		public int mParamType;

		protected object mValue;

		protected object mVariablePtr;

		protected object mMin;

		protected object mMax;

		protected bool mHasMin;

		protected bool mHasMax;

		public enum DataType
		{
			Type_Int,
			Type_Float,
			Type_Bool
		}
	}
}
