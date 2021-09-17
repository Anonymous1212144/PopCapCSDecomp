using System;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace BejeweledLivePlus.Bej3Graphics
{
	public class GraphicsOperationRef
	{
		public GraphicsOperationRef()
		{
			this.opArray_ = null;
			this.opIndex_ = -1;
		}

		public void prepare(GraphicsOperation[] arr, int idx)
		{
			this.opArray_ = arr;
			this.opIndex_ = idx;
		}

		public int mTimestamp
		{
			get
			{
				return this.opArray_[this.opIndex_].mTimestamp;
			}
			set
			{
				this.opArray_[this.opIndex_].mTimestamp = value;
			}
		}

		public GraphicsOperation.IMAGE_TYPE mType
		{
			get
			{
				return this.opArray_[this.opIndex_].mType;
			}
			set
			{
				this.opArray_[this.opIndex_].mType = value;
			}
		}

		public Image mImage
		{
			get
			{
				return this.opArray_[this.opIndex_].mImage;
			}
			set
			{
				this.opArray_[this.opIndex_].mImage = value;
			}
		}

		public FRect mFRect
		{
			get
			{
				return this.opArray_[this.opIndex_].mFRect;
			}
			set
			{
				this.opArray_[this.opIndex_].mFRect = value;
			}
		}

		public Color mColor
		{
			get
			{
				return this.opArray_[this.opIndex_].mColor;
			}
			set
			{
				this.opArray_[this.opIndex_].mColor = value;
			}
		}

		public FRect mDestRect
		{
			get
			{
				return this.opArray_[this.opIndex_].mDestRect;
			}
			set
			{
				this.opArray_[this.opIndex_].mDestRect = value;
			}
		}

		public Rect mSrcRect
		{
			get
			{
				return this.opArray_[this.opIndex_].mSrcRect;
			}
			set
			{
				this.opArray_[this.opIndex_].mSrcRect = value;
			}
		}

		public int mDrawMode
		{
			get
			{
				return this.opArray_[this.opIndex_].mDrawMode;
			}
			set
			{
				this.opArray_[this.opIndex_].mDrawMode = value;
			}
		}

		public float mFloat
		{
			get
			{
				return this.opArray_[this.opIndex_].mFloat;
			}
			set
			{
				this.opArray_[this.opIndex_].mFloat = value;
			}
		}

		private GraphicsOperation[] opArray_;

		private int opIndex_;
	}
}
