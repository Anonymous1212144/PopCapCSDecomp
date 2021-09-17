using System;
using Microsoft.Xna.Framework.Graphics;

namespace Sexy
{
	public class Image
	{
		public virtual Texture2D Texture
		{
			get
			{
				return this.GetTexture();
			}
			set
			{
				this.mTexture = value;
				this.mInAtlas = false;
				this.mOwnsTexture = true;
			}
		}

		public int GetCelCount()
		{
			return this.mNumCols * this.mNumRows;
		}

		public int GetWidth()
		{
			return this.mWidth;
		}

		public int GetHeight()
		{
			return this.mHeight;
		}

		public int GetCelWidth()
		{
			return this.mWidth / this.mNumCols;
		}

		public int GetCelHeight()
		{
			return this.mHeight / this.mNumRows;
		}

		public int GetAnimCel(int theTime)
		{
			if (this.mAnimInfo == null)
			{
				return 0;
			}
			return this.mAnimInfo.GetCel(theTime);
		}

		public TRect GetAnimCelRect(int theTime)
		{
			int animCel = this.GetAnimCel(theTime);
			int celWidth = this.GetCelWidth();
			int celHeight = this.GetCelHeight();
			if (this.mNumCols > 1)
			{
				return new TRect(animCel * celWidth, 0, celWidth, this.mHeight);
			}
			return new TRect(0, animCel * celHeight, this.mWidth, celHeight);
		}

		public TRect GetCelRect(int theCel)
		{
			int celHeight = this.GetCelHeight();
			int celWidth = this.GetCelWidth();
			int theX = theCel % this.mNumCols * celWidth;
			int theY = theCel / this.mNumCols * celHeight;
			return new TRect(theX, theY, celWidth, celHeight);
		}

		public TRect GetCelRect(int theCol, int theRow)
		{
			int celHeight = this.GetCelHeight();
			int celWidth = this.GetCelWidth();
			int theX = theCol * celWidth;
			int theY = theRow * celHeight;
			return new TRect(theX, theY, celWidth, celHeight);
		}

		public void CopyAttributes(Image from)
		{
			this.mNumCols = from.mNumCols;
			this.mNumRows = from.mNumRows;
			this.mAnimInfo.Dispose();
			this.mAnimInfo = null;
			if (from.mAnimInfo != null)
			{
				this.mAnimInfo = new AnimInfo(from.mAnimInfo);
			}
		}

		public Image(Texture2D theTexture)
			: this(theTexture, 0, 0, theTexture.Width, theTexture.Height)
		{
		}

		public Image()
		{
			this.mWidth = 0;
			this.mHeight = 0;
			this.mNumRows = 1;
			this.mNumCols = 1;
			this.mAnimInfo = null;
			this.mS = (this.mT = 0);
			this.mMaxS = (this.mMaxT = 0f);
			this.mFormat = PixelFormat.kPixelFormat_Automatic;
			this.mTextureName = 0U;
			this.mParentWidth = this.mWidth;
			this.mParentHeight = this.mHeight;
			this.mOwnsTexture = false;
			this.mInAtlas = false;
		}

		public Image(Image theParent, int s, int t, int theWidth, int theHeight)
		{
			this.mNumRows = 1;
			this.mNumCols = 1;
			this.mAnimInfo = null;
			this.mTextureName = 0U;
			this.mParentImage = theParent;
			this.mS = s;
			this.mT = t;
			this.mMaxS = theParent.mMaxS;
			this.mMaxT = theParent.mMaxT;
			this.mFormat = theParent.mFormat;
			this.mWidth = theWidth;
			this.mHeight = theHeight;
			this.mParentWidth = theParent.mParentWidth;
			this.mParentHeight = theParent.mParentHeight;
			this.mOwnsTexture = false;
			this.mInAtlas = true;
			this.mTexture = theParent.GetTexture();
		}

		public Image(Texture2D theTexture, int s, int t, int theWidth, int theHeight)
		{
			this.mNumRows = 1;
			this.mNumCols = 1;
			this.mAnimInfo = null;
			this.mTexture = theTexture;
			this.mS = s;
			this.mT = t;
			this.mWidth = theWidth;
			this.mHeight = theHeight;
			this.mParentWidth = this.mWidth;
			this.mParentHeight = this.mHeight;
			this.mOwnsTexture = true;
			this.mInAtlas = false;
		}

		public Image(Texture2D theTexture, PixelFormat theFormat, float maxS, float maxT, int s, int t, int theWidth, int theHeight)
		{
			this.mNumRows = 1;
			this.mNumCols = 1;
			this.mAnimInfo = null;
			this.mTexture = theTexture;
			this.mS = s;
			this.mT = t;
			this.mMaxS = maxS;
			this.mMaxT = maxT;
			this.mFormat = theFormat;
			this.mWidth = theWidth;
			this.mHeight = theHeight;
			this.mParentWidth = this.mWidth;
			this.mParentHeight = this.mHeight;
			this.mOwnsTexture = true;
			this.mInAtlas = false;
		}

		public static Image FromMemory(ushort[] info, int width, int height)
		{
			Image image = new Image();
			image.mFormat = PixelFormat.kPixelFormat_RGB565;
			image.mWidth = width;
			image.mHeight = height;
			image.mOwnsTexture = true;
			image.mInAtlas = false;
			image.mTexture = new Texture2D(GlobalStaticVars.g.GraphicsDevice, width, height, false, 1);
			image.mTexture.SetData<ushort>(info);
			return image;
		}

		public virtual void Dispose()
		{
		}

		protected virtual Texture2D GetTexture()
		{
			if (this.mOwnsTexture && !this.mInAtlas)
			{
				return this.mTexture;
			}
			if (this.mInAtlas && this.mParentImage != null)
			{
				return this.mParentImage.GetTexture();
			}
			return null;
		}

		public static explicit operator Image(Texture2D aTexture)
		{
			return new Image
			{
				Texture = aTexture
			};
		}

		public static implicit operator Texture2D(Image anImage)
		{
			return anImage.GetTexture();
		}

		public int mWidth;

		public int mHeight;

		public int mNumRows;

		public int mNumCols;

		public AnimInfo mAnimInfo;

		private Texture2D mTexture;

		public uint mTextureName;

		public float mMaxS;

		public float mMaxT;

		public int mS;

		public int mT;

		public PixelFormat mFormat;

		public bool mInAtlas;

		public Image mParentImage;

		protected int mParentWidth;

		protected int mParentHeight;

		protected bool mOwnsTexture;
	}
}
