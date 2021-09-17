using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sexy
{
	public class Graphics : GraphicsState
	{
		public static void PreAllocateMemory()
		{
			for (int i = 0; i < 10; i++)
			{
				new Graphics().PrepareForReuse();
			}
		}

		public static Graphics GetNew()
		{
			if (Graphics.unusedObjects.Count > 0)
			{
				return Graphics.unusedObjects.Pop();
			}
			return new Graphics();
		}

		public static Graphics GetNew(Graphics theGraphics)
		{
			if (Graphics.unusedObjects.Count > 0)
			{
				Graphics graphics = Graphics.unusedObjects.Pop();
				graphics.CopyStateFrom(theGraphics);
				return graphics;
			}
			return new Graphics(theGraphics);
		}

		public static Graphics GetNew(Game theGame)
		{
			if (Graphics.unusedObjects.Count > 0)
			{
				Graphics graphics = Graphics.unusedObjects.Pop();
				graphics.mGame = theGame;
				GraphicsState.mGraphicsDeviceManager = new GraphicsDeviceManager(theGame);
				return graphics;
			}
			return new Graphics(theGame);
		}

		public static Graphics GetNew(MemoryImage theDestImage)
		{
			if (Graphics.unusedObjects.Count > 0)
			{
				Graphics graphics = Graphics.unusedObjects.Pop();
				graphics.mDestImage = theDestImage.RenderTarget;
				graphics.mClipRect = new TRect(0, 0, graphics.mDestImage.Width, graphics.mDestImage.Height);
				graphics.SetRenderTarget(graphics.mDestImage);
				return graphics;
			}
			return new Graphics(theDestImage);
		}

		public void PrepareForReuse()
		{
			this.ResetForReuse();
			Graphics.unusedObjects.Push(this);
		}

		private void ResetForReuse()
		{
			this.mTransX = 0;
			this.mTransY = 0;
			this.mFastStretch = false;
			this.mWriteColoredString = false;
			this.mLinearBlend = false;
			this.mScaleX = 1f;
			this.mScaleY = 1f;
			this.mScaleOrigX = 0f;
			this.mScaleOrigY = 0f;
			this.mFont = null;
			this.mColor = default(SexyColor);
			this.mColorizeImages = false;
			this.WorldRotation = 0f;
			this.mDrawMode = 0;
			this.mClipRect = new TRect(0, 0, base.mScreenWidth, base.mScreenHeight);
		}

		private protected static bool spritebatchBegan { protected get; private set; }

		public GraphicsDevice GraphicsDevice
		{
			get
			{
				return GraphicsState.mGraphicsDeviceManager.GraphicsDevice;
			}
		}

		public GraphicsDeviceManager GraphicsDeviceManager
		{
			get
			{
				return GraphicsState.mGraphicsDeviceManager;
			}
		}

		public int PreferredBackBufferWidth
		{
			get
			{
				return GraphicsState.mGraphicsDeviceManager.PreferredBackBufferWidth;
			}
			set
			{
				GraphicsState.mGraphicsDeviceManager.PreferredBackBufferWidth = value;
			}
		}

		public int PreferredBackBufferHeight
		{
			get
			{
				return GraphicsState.mGraphicsDeviceManager.PreferredBackBufferHeight;
			}
			set
			{
				GraphicsState.mGraphicsDeviceManager.PreferredBackBufferHeight = value;
			}
		}

		public bool IsFullScreen
		{
			get
			{
				return GraphicsState.mGraphicsDeviceManager.IsFullScreen;
			}
			set
			{
				GraphicsState.mGraphicsDeviceManager.IsFullScreen = value;
			}
		}

		private Graphics()
		{
			Graphics.spriteBatch = Graphics.gSpriteBatch;
			Graphics.primitiveBatch = Graphics.gPrimitiveBatch;
		}

		private Graphics(Graphics theGraphics)
		{
			base.CopyStateFrom(theGraphics);
			Graphics.spriteBatch = Graphics.gSpriteBatch;
			Graphics.primitiveBatch = Graphics.gPrimitiveBatch;
		}

		private Graphics(Game theGame)
			: base(theGame)
		{
			this.mGame = theGame;
			Graphics.PreAllocateMemory();
		}

		private Graphics(MemoryImage theDestImage)
		{
			this.mDestImage = theDestImage.RenderTarget;
			this.mClipRect = new TRect(0, 0, this.mDestImage.Width, this.mDestImage.Height);
			Graphics.spriteBatch = Graphics.gSpriteBatch;
			Graphics.primitiveBatch = Graphics.gPrimitiveBatch;
			this.SetRenderTarget(this.mDestImage);
		}

		public void Init()
		{
			Graphics.gSpriteBatch = new SpriteBatch(GraphicsState.mGraphicsDeviceManager.GraphicsDevice);
			Graphics.gPrimitiveBatch = new PrimitiveBatch(GraphicsState.mGraphicsDeviceManager.GraphicsDevice);
			Graphics.spriteBatch = Graphics.gSpriteBatch;
			Graphics.primitiveBatch = Graphics.gPrimitiveBatch;
		}

		public virtual void Dispose()
		{
			RenderTarget2D renderTarget2D = this.mDestImage;
		}

		public void BeginFrame()
		{
			this.BeginFrame(0);
		}

		public void BeginFrame(SpriteSortMode sortmode)
		{
			this.BeginFrame(null, sortmode);
		}

		public void BeginFrame(RasterizerState rasterState)
		{
			this.BeginFrame(rasterState, 0);
		}

		public void BeginFrame(RasterizerState rasterState, SpriteSortMode sortmode)
		{
			if (this.NeedToSetWorldRotation)
			{
				base.ApplyWorldRotation();
				this.NeedToSetWorldRotation = false;
			}
			BlendState blendState = ((this.mDrawMode == 1) ? BlendState.Additive : BlendState.AlphaBlend);
			if (Graphics.gTransformStack.empty<SexyTransform2D>())
			{
				Graphics.spriteBatch.Begin(sortmode, blendState, Graphics.NormalSamplerState, null, rasterState);
			}
			else
			{
				Graphics.spriteBatch.Begin(sortmode, blendState, Graphics.NormalSamplerState, null, rasterState, null, Graphics.gTransformStack.Peek().mMatrix);
			}
			Graphics.spritebatchBegan = true;
		}

		public void EndFrame()
		{
			this.EndDrawImageTransformed();
			Graphics.spriteBatch.End();
			Graphics.spritebatchBegan = false;
		}

		public static void OrientationChanged()
		{
			Graphics.primitiveBatch.SetupMatrices();
		}

		protected void SetupDrawMode(int theDrawingMode)
		{
			if (Graphics.spritebatchBegan)
			{
				if (theDrawingMode != this.mDrawMode)
				{
					this.mDrawMode = theDrawingMode;
					this.EndFrame();
					this.BeginFrame();
					Graphics.spritebatchBegan = true;
					return;
				}
			}
			else
			{
				this.mDrawMode = theDrawingMode;
				if (theDrawingMode == 1)
				{
					this.GraphicsDevice.BlendState = BlendState.Additive;
					return;
				}
				this.GraphicsDevice.BlendState = BlendState.AlphaBlend;
			}
		}

		public void SetRenderTarget(RenderTarget2D renderTarget)
		{
			bool spritebatchBegan = Graphics.spritebatchBegan;
			if (Graphics.spritebatchBegan)
			{
				this.EndFrame();
			}
			if (renderTarget == null && Graphics.gTransformStack.Count > 0)
			{
				Graphics.gTransformStack.Pop();
			}
			else if (Graphics.gTransformStack.Count > 0)
			{
				Graphics.gTransformStack.Push(new SexyTransform2D(Matrix.Identity));
			}
			this.mDestImage = renderTarget;
			this.GraphicsDevice.SetRenderTarget(this.mDestImage);
			this.ClearClipRect();
			if (spritebatchBegan)
			{
				this.BeginFrame();
			}
		}

		public void Clear()
		{
			this.Clear(Color.Black);
		}

		public void Clear(Color color)
		{
			this.GraphicsDevice.Clear(color);
		}

		public Graphics Create()
		{
			return new Graphics(this);
		}

		public void SetFont(Font theFont)
		{
			this.mFont = theFont;
			if (this.mFont != null)
			{
				this.mFont.mScaleX = this.mScaleX;
				this.mFont.mScaleY = this.mScaleY;
			}
		}

		public Font GetFont()
		{
			return this.mFont;
		}

		public void SetColor(Color theColor)
		{
			this.mColor = new Color((int)theColor.R, (int)theColor.G, (int)theColor.B);
			float num = (float)theColor.A * 0.003921569f;
			this.mColor *= num;
		}

		public Color GetColor()
		{
			return this.mColor;
		}

		public void SetDrawMode(Graphics.DrawMode theDrawMode)
		{
			this.SetupDrawMode((int)theDrawMode);
		}

		public void SetDrawMode(int theDrawMode)
		{
			this.SetupDrawMode(theDrawMode);
		}

		public int GetDrawMode()
		{
			return this.mDrawMode;
		}

		public void SetColorizeImages(bool colorizeImages)
		{
			this.mColorizeImages = colorizeImages;
		}

		public bool GetColorizeImages()
		{
			return this.mColorizeImages;
		}

		public void SetScaleX(float scaleX)
		{
			this.mScaleX = scaleX;
			if (this.mFont != null)
			{
				this.mFont.mScaleX = this.mScaleX;
			}
		}

		public void SetScaleY(float scaleY)
		{
			this.mScaleY = scaleY;
			if (this.mFont != null)
			{
				this.mFont.mScaleY = scaleY;
			}
		}

		public void SetScale(float scale)
		{
			this.SetScale(scale, scale);
		}

		public void SetScale(float scaleX, float scaleY)
		{
			this.SetScaleX(scaleX);
			this.SetScaleY(scaleY);
		}

		public void SetScale(float theScaleX, float theScaleY, float theOrigX, float theOrigY)
		{
			this.mScaleX = theScaleX;
			this.mScaleY = theScaleY;
			this.mScaleOrigX = theOrigX + (float)this.mTransX;
			this.mScaleOrigY = theOrigY + (float)this.mTransY;
		}

		public void SetFastStretch(bool fastStretch)
		{
			this.mFastStretch = fastStretch;
		}

		public bool GetFastStretch()
		{
			return this.mFastStretch;
		}

		public void SetLinearBlend(bool linear)
		{
			this.mLinearBlend = linear;
		}

		public bool GetLinearBlend()
		{
			return this.mLinearBlend;
		}

		public void FillRect(TRect theRect)
		{
			this.FillRect(theRect.mX, theRect.mY, theRect.mWidth, theRect.mHeight);
		}

		public void FillRect(int theX, int theY, int theWidth, int theHeight)
		{
			this.DrawImage(GraphicsState.dummy, theX, theY, theWidth, theHeight);
		}

		public void DrawRect(TRect theRect)
		{
			this.DrawRect(theRect.mX, theRect.mY, theRect.mWidth, theRect.mHeight);
		}

		public void DrawRect(int theX, int theY, int theWidth, int theHeight)
		{
			if (this.mColor.A == 0)
			{
				return;
			}
			this.FillRect(theX, theY, theWidth + 1, 1);
			this.FillRect(theX, theY + theHeight, theWidth + 1, 1);
			this.FillRect(theX, theY + 1, 1, theHeight - 1);
			this.FillRect(theX + theWidth, theY + 1, 1, theHeight - 1);
		}

		public void DrawStringLayer(string theString, int theX, int theY, int theLayer)
		{
			this.EndDrawImageTransformed();
			if (this.mFont != null)
			{
				this.mFont.DrawStringLayer(this, theX + this.mTransX, theY + this.mTransY, theString, this.mColorizeImages ? this.mColor : Color.White, theLayer);
			}
		}

		public void DrawString(string theString, int theX, int theY)
		{
			if (this.mFont != null)
			{
				this.mFont.DrawString(this, theX + this.mTransX, theY + this.mTransY, theString, this.mColorizeImages ? this.mColor : Color.White);
			}
		}

		public void DrawString(StringBuilder theString, int theX, int theY)
		{
			this.EndDrawImageTransformed();
			if (this.mFont != null)
			{
				this.mFont.DrawString(this, theX + this.mTransX, theY + this.mTransY, theString, this.mColorizeImages ? this.mColor : Color.White);
			}
		}

		public void DrawLine(int theStartX, int theStartY, int theEndX, int theEndY)
		{
		}

		public void BeginPolyFill()
		{
			this.EndFrame();
			Matrix? transform = default(Matrix?);
			if (Graphics.gTransformStack.Count > 0)
			{
				transform = new Matrix?(Graphics.gTransformStack.Peek().mMatrix);
			}
			Graphics.primitiveBatch.Begin(0, this.mTransX, this.mTransY, transform, null);
			this.polyFillBegun = true;
		}

		public void EndPolyFill()
		{
			Graphics.primitiveBatch.End();
			this.BeginFrame();
			this.polyFillBegun = false;
		}

		public void PolyFill(TPoint[] theVertexList, int theNumVertices)
		{
			for (int i = 0; i < theNumVertices; i++)
			{
				Vector2 vertex;
				vertex..ctor((float)(theVertexList[i].mX + this.mTransX), (float)(theVertexList[i].mY + this.mTransY));
				Graphics.primitiveBatch.AddVertex(vertex, this.mColor);
			}
		}

		private void PrepareRectsForClipping(ref TRect source, ref TRect destination)
		{
			Vector2 vector;
			vector..ctor((float)destination.mWidth / (float)((source.mWidth != 0) ? source.mWidth : destination.mWidth), (float)destination.mHeight / (float)((source.mHeight != 0) ? source.mHeight : destination.mHeight));
			if (vector.X == 0f)
			{
				vector.X = 1f;
			}
			if (vector.Y == 0f)
			{
				vector.Y = 1f;
			}
			int num = Math.Max(0, (int)((float)(this.mClipRect.mX - destination.mX) / vector.X));
			int num2 = Math.Max(0, (int)((float)(this.mClipRect.mY - destination.mY) / vector.Y));
			source.mX += num;
			source.mY += num2;
			source.mWidth += -num + Math.Min(0, (int)((float)(this.mClipRect.mX + this.mClipRect.mWidth - (destination.mX + destination.mWidth)) / vector.X));
			source.mHeight += -num2 + Math.Min(0, (int)((float)(this.mClipRect.mY + this.mClipRect.mHeight - (destination.mY + destination.mHeight)) / vector.Y));
			destination = this.mClipRect.Intersection(destination);
			destination.mX += (int)((float)destination.mWidth * ((1f - this.mScaleX) / 2f));
			destination.mY += (int)((float)destination.mHeight * ((1f - this.mScaleY) / 2f));
			destination.mWidth = (int)((float)destination.mWidth * this.mScaleX);
			destination.mHeight = (int)((float)destination.mHeight * this.mScaleY);
		}

		private static Matrix? GetTransform()
		{
			Matrix? result = default(Matrix?);
			if (Graphics.gTransformStack.Count > 0)
			{
				result = new Matrix?(Graphics.gTransformStack.Peek().mMatrix);
			}
			return result;
		}

		public void BeginDrawImageTransformed(Image texture)
		{
			if (!Graphics.primitiveBatch.HasBegun)
			{
				this.EndFrame();
				Graphics.primitiveBatch.Begin(0, this.mTransX, this.mTransY, Graphics.GetTransform(), texture);
			}
			else
			{
				Graphics.primitiveBatch.Transform = Graphics.GetTransform();
				Graphics.primitiveBatch.Texture = texture;
				Graphics.primitiveBatch.OffsetX = this.mTransX;
				Graphics.primitiveBatch.OffsetY = this.mTransY;
			}
			this.SetDrawMode(this.mDrawMode);
		}

		public void EndDrawImageTransformed()
		{
			this.EndDrawImageTransformed(true);
		}

		public void EndDrawImageTransformed(bool startSpritebatch)
		{
			if (Graphics.primitiveBatch.HasBegun)
			{
				Graphics.primitiveBatch.End();
				if (startSpritebatch)
				{
					this.BeginFrame();
					return;
				}
			}
			else if (!Graphics.spritebatchBegan && startSpritebatch)
			{
				this.BeginFrame();
			}
		}

		public void DrawImageTransformed(Image theImage, Matrix theTransform, bool center, Color theColor, TRect theSrcRect, bool clip)
		{
			this.BeginDrawImageTransformed(theImage);
			TRect destination = new TRect(-this.mTransX, -this.mTransY, theImage.mWidth, theImage.mHeight);
			if (clip)
			{
				this.PrepareRectsForClipping(ref theSrcRect, ref destination);
			}
			Vector2 center2 = (center ? new Vector2((float)theSrcRect.mWidth * 0.5f, (float)theSrcRect.mHeight * 0.5f) : Vector2.Zero);
			Graphics.primitiveBatch.Draw(theImage, destination, theSrcRect, new Matrix?(theTransform), center2, theColor);
		}

		public void DrawImageRotatedScaled(Image theImage, TRect dest, TRect src, Color col, float rotation, Vector2 origin)
		{
			this.EndDrawImageTransformed();
			Graphics.spriteBatch.Draw(theImage, dest, new Rectangle?(src), col, rotation, origin, 0, 0f);
		}

		public void DrawImage(Image theImage, float theX, float theY)
		{
			this.DrawImage(theImage, (int)theX, (int)theY);
		}

		public void DrawImage(Image theImage, int theX, int theY)
		{
			this.EndDrawImageTransformed();
			TRect aRect = new TRect(theImage.mS, theImage.mT, theImage.mWidth, theImage.mHeight);
			TRect aRect2 = new TRect(theX + this.mTransX, theY + this.mTransY, aRect.mWidth, aRect.mHeight);
			if (aRect.mWidth > 0 && aRect.mHeight > 0)
			{
				this.PrepareRectsForClipping(ref aRect, ref aRect2);
				Graphics.spriteBatch.Draw(theImage, aRect2, new Rectangle?(aRect), this.mColorizeImages ? this.mColor : Color.White);
			}
		}

		public void DrawImage(Image theImage, int theX, int theY, TRect theSrcRect)
		{
			this.EndDrawImageTransformed();
			TRect aRect = new TRect(theImage.mS + theSrcRect.mX, theImage.mT + theSrcRect.mY, theSrcRect.mWidth, theSrcRect.mHeight);
			TRect aRect2 = new TRect(theX + this.mTransX, theY + this.mTransY, aRect.mWidth, aRect.mHeight);
			if (theSrcRect.mWidth > 0 && theSrcRect.mHeight > 0)
			{
				this.PrepareRectsForClipping(ref aRect, ref aRect2);
				Graphics.spriteBatch.Draw(theImage, aRect2, new Rectangle?(aRect), this.mColorizeImages ? this.mColor : Color.White);
			}
		}

		public void DrawImage(Image theImage, TRect theDestRect, TRect theSrcRect)
		{
			this.EndDrawImageTransformed();
			theDestRect.mX += this.mTransX;
			theDestRect.mY += this.mTransY;
			theSrcRect.mX += theImage.mS;
			theSrcRect.mY += theImage.mT;
			this.PrepareRectsForClipping(ref theSrcRect, ref theDestRect);
			Graphics.spriteBatch.Draw(theImage.Texture, theDestRect, new Rectangle?(theSrcRect), this.mColorizeImages ? this.mColor : Color.White);
		}

		public void DrawImage(Image theImage, int theX, int theY, int theStretchedWidth, int theStretchedHeight)
		{
			this.EndDrawImageTransformed();
			TRect aRect = new TRect(theX + this.mTransX, theY + this.mTransY, theStretchedWidth, theStretchedHeight);
			TRect aRect2 = new TRect(theImage.mS, theImage.mT, theImage.mWidth, theImage.mHeight);
			this.PrepareRectsForClipping(ref aRect2, ref aRect);
			Graphics.spriteBatch.Draw(theImage, aRect, new Rectangle?(aRect2), this.mColorizeImages ? this.mColor : Color.White);
		}

		public void DrawImageF(Image theImage, float theX, float theY)
		{
			this.DrawImage(theImage, (int)theX, (int)theY);
		}

		public void DrawImageF(Image theImage, float theX, float theY, TRect theSrcRect)
		{
			this.DrawImage(theImage, (int)theX, (int)theY, theSrcRect);
		}

		public void DrawImageMirror(Image theImage, int theX, int theY)
		{
			this.DrawImageMirror(theImage, theX, theY, true);
		}

		public void DrawImageMirror(Image theImage, int theX, int theY, bool mirror)
		{
			this.DrawImageMirror(theImage, theX, theY, new TRect(theImage.mS, theImage.mT, theImage.mWidth, theImage.mHeight), mirror);
		}

		public void DrawImageMirror(Image theImage, int theX, int theY, TRect theSrcRect)
		{
			this.DrawImageMirror(theImage, theX, theY, theSrcRect, true);
		}

		public void DrawImageMirror(Image theImage, int theX, int theY, TRect theSrcRect, bool mirror)
		{
			this.EndDrawImageTransformed();
			if (theSrcRect.mX + theSrcRect.mWidth > theImage.GetWidth() || theSrcRect.mY + theSrcRect.mHeight > theImage.GetHeight())
			{
				return;
			}
			if (!mirror)
			{
				this.DrawImage(theImage, theX, theY, theSrcRect);
				return;
			}
			TRect aRect = new TRect(theX + this.mTransX, theY + this.mTransY, theSrcRect.mWidth, theSrcRect.mHeight);
			if (theSrcRect.mWidth > 0 && theSrcRect.mHeight > 0)
			{
				this.PrepareRectsForClipping(ref theSrcRect, ref aRect);
				Graphics.spriteBatch.Draw(theImage, aRect, new Rectangle?(theSrcRect), this.mColorizeImages ? this.mColor : Color.White, 0f, Vector2.Zero, 1, 0f);
			}
		}

		public void DrawImageMirror(Image theImage, TRect theDestRect, TRect theSrcRect)
		{
			this.DrawImageMirror(theImage, theDestRect, theSrcRect, true);
		}

		public void DrawImageMirror(Image theImage, TRect theDestRect, TRect theSrcRect, bool mirror)
		{
			this.EndDrawImageTransformed();
			if (!mirror)
			{
				this.DrawImage(theImage, theDestRect, theSrcRect);
				return;
			}
			this.PrepareRectsForClipping(ref theSrcRect, ref theDestRect);
			Graphics.spriteBatch.Draw(theImage, theDestRect, new Rectangle?(theSrcRect), this.mColorizeImages ? this.mColor : Color.White, 0f, Vector2.Zero, 1, 0f);
		}

		public void DrawImageRotated(Image theImage, int theX, int theY, double theRot)
		{
			this.DrawImageRotated(theImage, theX, theY, theRot, new TRect(0, 0, theImage.GetWidth(), theImage.GetHeight()));
		}

		public void DrawImageRotated(Image theImage, int theX, int theY, double theRot, TRect theSrcRect)
		{
			theSrcRect.mX += theImage.mS;
			theSrcRect.mY += theImage.mT;
			this.DrawImageRotatedF(theImage, (float)theX, (float)theY, theRot, theSrcRect);
		}

		public void DrawImageRotated(Image theImage, int theX, int theY, double theRot, int theRotCenterX, int theRotCenterY)
		{
			this.DrawImageRotated(theImage, theX, theY, theRot, theRotCenterX, theRotCenterY, new TRect(theImage.mS, theImage.mT, theImage.GetWidth(), theImage.GetHeight()));
		}

		public void DrawImageRotated(Image theImage, int theX, int theY, double theRot, int theRotCenterX, int theRotCenterY, TRect theSrcRect)
		{
			theSrcRect.mX += theImage.mS;
			theSrcRect.mY += theImage.mT;
			this.DrawImageRotatedF(theImage, (float)theX, (float)theY, theRot, (float)theRotCenterX, (float)theRotCenterY, new TRect?(theSrcRect));
		}

		public void DrawImageRotatedF(Image theImage, float theX, float theY, double theRot)
		{
			this.DrawImageRotatedF(theImage, theX, theY, theRot, new TRect(theImage.mS, theImage.mT, theImage.GetWidth(), theImage.GetHeight()));
		}

		public void DrawImageRotatedF(Image theImage, float theX, float theY, double theRot, TRect theSrcRect)
		{
			int num = theSrcRect.mWidth / 2;
			int num2 = theSrcRect.mHeight / 2;
			this.DrawImageRotatedF(theImage, theX, theY, theRot, (float)num, (float)num2, new TRect?(theSrcRect));
		}

		public void DrawImageRotatedF(Image theImage, float theX, float theY, double theRot, float theRotCenterX, float theRotCenterY)
		{
			this.DrawImageRotatedF(theImage, theX, theY, theRot, theRotCenterX, theRotCenterY, new TRect?(new TRect(theImage.mS, theImage.mT, theImage.GetWidth(), theImage.GetHeight())));
		}

		public void DrawImageRotatedF(Image theImage, float theX, float theY, double theRot, float theRotCenterX, float theRotCenterY, TRect? theSrcRect)
		{
			this.DrawImageRotatedScaled(theImage, theX, theY, theRot, theRotCenterX, theRotCenterY, theSrcRect, theImage.GetCelWidth(), theImage.GetCelHeight());
		}

		public void DrawImageRotatedScaled(Image theImage, float theX, float theY, double theRot, float theRotCenterX, float theRotCenterY, TRect? theSrcRect, int stretchedHeight, int stretchedWidth)
		{
			this.EndDrawImageTransformed();
			theX += (float)this.mTransX;
			theY += (float)this.mTransY;
			if (theSrcRect == null)
			{
				TRect aRect = new TRect(theImage.mS, theImage.mT, theImage.mWidth, theImage.mHeight);
				TRect aRect2 = new TRect((int)theX, (int)theY, stretchedHeight, stretchedWidth);
				Graphics.spriteBatch.Draw(theImage, aRect2, new Rectangle?(aRect), this.mColorizeImages ? this.mColor : Color.White, (float)theRot, new Vector2(theRotCenterX, theRotCenterY), 0, 0f);
				return;
			}
			TRect aRect3 = new TRect((int)theX, (int)theY, stretchedHeight, stretchedWidth);
			SpriteBatch spriteBatch = Graphics.spriteBatch;
			Texture2D texture2D = theImage;
			Rectangle rectangle = aRect3;
			TRect? trect = theSrcRect;
			spriteBatch.Draw(texture2D, rectangle, (trect != null) ? new Rectangle?(trect.GetValueOrDefault()) : default(Rectangle?), this.mColorizeImages ? this.mColor : Color.White, (float)theRot, new Vector2(theRotCenterX, theRotCenterY), 0, 0f);
		}

		public void DrawTriangle(TriVertex p1, TriVertex p2, TriVertex p3, Color theColor, int theDrawMode)
		{
			this.EndDrawImageTransformed();
			bool spritebatchBegan = Graphics.spritebatchBegan;
			if (Graphics.spritebatchBegan)
			{
				this.EndFrame();
			}
			this.SetupDrawMode(theDrawMode);
			Matrix? transform = default(Matrix?);
			if (Graphics.gTransformStack.Count > 0)
			{
				transform = new Matrix?(Graphics.gTransformStack.Peek().mMatrix);
			}
			Graphics.primitiveBatch.Begin(0, this.mTransX, this.mTransY, transform, null);
			Graphics.primitiveBatch.AddVertex(new Vector2(p1.x, p1.y), theColor);
			Graphics.primitiveBatch.AddVertex(new Vector2(p2.x, p2.y), theColor);
			Graphics.primitiveBatch.AddVertex(new Vector2(p3.x, p3.y), theColor);
			Graphics.primitiveBatch.End();
			if (spritebatchBegan)
			{
				this.BeginFrame();
			}
		}

		public void DrawImageCel(Image theImageStrip, int theX, int theY, int theCel)
		{
			this.DrawImageCel(theImageStrip, theX, theY, theCel % theImageStrip.mNumCols, theCel / theImageStrip.mNumCols);
		}

		public void DrawImageCel(Image theImageStrip, TRect theDestRect, int theCel)
		{
			this.DrawImageCel(theImageStrip, theDestRect, theCel % theImageStrip.mNumCols, theCel / theImageStrip.mNumCols);
		}

		public void DrawImageCel(Image theImageStrip, int theX, int theY, int theCelCol, int theCelRow)
		{
			if (theCelRow < 0 || theCelCol < 0 || theCelRow >= theImageStrip.mNumRows || theCelCol >= theImageStrip.mNumCols)
			{
				return;
			}
			int num = theImageStrip.mWidth / theImageStrip.mNumCols;
			int num2 = theImageStrip.mHeight / theImageStrip.mNumRows;
			TRect theSrcRect = new TRect(num * theCelCol, num2 * theCelRow, num, num2);
			this.DrawImage(theImageStrip, theX, theY, theSrcRect);
		}

		public void DrawImageCel(Image theImageStrip, TRect theDestRect, int theCelCol, int theCelRow)
		{
			if (theCelRow < 0 || theCelCol < 0 || theCelRow >= theImageStrip.mNumRows || theCelCol >= theImageStrip.mNumCols)
			{
				return;
			}
			int num = theImageStrip.mWidth / theImageStrip.mNumCols;
			int num2 = theImageStrip.mHeight / theImageStrip.mNumRows;
			TRect theSrcRect = new TRect(num * theCelCol, num2 * theCelRow, num, num2);
			if (num == theDestRect.mWidth && num2 == theDestRect.mHeight)
			{
				this.DrawImage(theImageStrip, theDestRect.mX, theDestRect.mY, theSrcRect);
				return;
			}
			theSrcRect = new TRect(num * theCelCol, num2 * theCelRow, num, num2);
			this.DrawImage(theImageStrip, theDestRect, theSrcRect);
		}

		public void DrawImageAnim(Image theImageAnim, int theX, int theY, int theTime)
		{
			this.DrawImageCel(theImageAnim, theX, theY, theImageAnim.GetAnimCel(theTime));
		}

		public void ClearClipRect()
		{
			TRect mClipRect;
			if (this.mDestImage != null)
			{
				mClipRect = new TRect(0, 0, this.mDestImage.Bounds.Width, this.mDestImage.Bounds.Height);
			}
			else
			{
				mClipRect = new TRect(0, 0, GlobalStaticVars.gSexyAppBase.mWidth, GlobalStaticVars.gSexyAppBase.mHeight);
			}
			this.mClipRect = mClipRect;
		}

		public void SetClipRect(int theX, int theY, int theWidth, int theHeight)
		{
			this.ClearClipRect();
			this.mClipRect = this.mClipRect.Intersection(new TRect(theX + this.mTransX, theY + this.mTransY, theWidth, theHeight));
		}

		public void SetClipRect(TRect theRect)
		{
			this.SetClipRect(theRect.mX, theRect.mY, theRect.mWidth, theRect.mHeight);
		}

		public TRect GetClipRect()
		{
			return this.mClipRect;
		}

		public void ClipRect(int theX, int theY, int theWidth, int theHeight)
		{
			TRect trect = new TRect(theX + this.mTransX, theY + this.mTransY, theWidth, theHeight);
			if (trect == this.mClipRect)
			{
				return;
			}
			this.mClipRect = this.mClipRect.Intersection(trect);
		}

		public void ClipRect(TRect theRect)
		{
			this.ClipRect(theRect.mX, theRect.mY, theRect.mWidth, theRect.mHeight);
		}

		public int StringWidth(string theString)
		{
			return this.mFont.StringWidth(theString);
		}

		public void DrawImageBox(TRect theDest, Image theComponentImage)
		{
			this.DrawImageBox(new TRect(0, 0, theComponentImage.mWidth, theComponentImage.mHeight), theDest, theComponentImage);
		}

		public void DrawImageBox(TRect theSrc, TRect theDest, Image theComponentImage)
		{
			this.EndDrawImageTransformed();
			if (theSrc.mWidth <= 0 || theSrc.mHeight <= 0)
			{
				return;
			}
			int num = theSrc.mWidth / 3;
			int num2 = theSrc.mHeight / 3;
			int mX = theSrc.mX;
			int mY = theSrc.mY;
			int num3 = theSrc.mWidth - num * 2;
			int num4 = theSrc.mHeight - num2 * 2;
			this.DrawImage(theComponentImage, theDest.mX, theDest.mY, new TRect(mX, mY, num, num2));
			this.DrawImage(theComponentImage, theDest.mX + theDest.mWidth - num, theDest.mY, new TRect(mX + num + num3, mY, num, num2));
			this.DrawImage(theComponentImage, theDest.mX, theDest.mY + theDest.mHeight - num2, new TRect(mX, mY + num2 + num4, num, num2));
			this.DrawImage(theComponentImage, theDest.mX + theDest.mWidth - num, theDest.mY + theDest.mHeight - num2, new TRect(mX + num + num3, mY + num2 + num4, num, num2));
			Graphics @new = Graphics.GetNew(this);
			@new.ClipRect(theDest.mX + num, theDest.mY, theDest.mWidth - num * 2, theDest.mHeight);
			for (int i = 0; i < (theDest.mWidth - num * 2 + num3 - 1) / num3; i++)
			{
				@new.DrawImage(theComponentImage, theDest.mX + num + i * num3, theDest.mY, new TRect(mX + num, mY, num3, num2));
				@new.DrawImage(theComponentImage, theDest.mX + num + i * num3, theDest.mY + theDest.mHeight - num2, new TRect(mX + num, mY + num2 + num4, num3, num2));
			}
			@new.PrepareForReuse();
			Graphics new2 = Graphics.GetNew(this);
			new2.ClipRect(theDest.mX, theDest.mY + num2, theDest.mWidth, theDest.mHeight - num2 * 2);
			for (int j = 0; j < (theDest.mHeight - num2 * 2 + num4 - 1) / num4; j++)
			{
				new2.DrawImage(theComponentImage, theDest.mX, theDest.mY + num2 + j * num4, new TRect(mX, mY + num2, num, num4));
				new2.DrawImage(theComponentImage, theDest.mX + theDest.mWidth - num, theDest.mY + num2 + j * num4, new TRect(mX + num + num3, mY + num2, num, num4));
			}
			new2.PrepareForReuse();
			Graphics new3 = Graphics.GetNew(this);
			new3.ClipRect(theDest.mX + num, theDest.mY + num2, theDest.mWidth - num * 2, theDest.mHeight - num2 * 2);
			for (int i = 0; i < (theDest.mWidth - num * 2 + num3 - 1) / num3; i++)
			{
				for (int j = 0; j < (theDest.mHeight - num2 * 2 + num4 - 1) / num4; j++)
				{
					new3.DrawImage(theComponentImage, theDest.mX + num + i * num3, theDest.mY + num2 + j * num4, new TRect(mX + num, mY + num2, num3, num4));
				}
			}
			new3.PrepareForReuse();
		}

		private Vector2 scale
		{
			get
			{
				return new Vector2(this.mScaleX, this.mScaleY);
			}
		}

		public int WriteString(string theString, int theX, int theY, int theWidth, int theJustification, bool drawString, int theOffset, int theLength)
		{
			return this.WriteString(theString, theX, theY, theWidth, theJustification, drawString, theOffset, theLength, -1);
		}

		public int WriteString(string theString, int theX, int theY, int theWidth, int theJustification, bool drawString, int theOffset, int theLength, int theOldColor)
		{
			this.mFont.DrawString(this, theX, theY, theString, new SexyColor(theOldColor.ToString()));
			return theX;
		}

		public int WriteWordWrappedLayer(TRect theRect, string theLine, int theLineSpacing, int theJustification, int layer)
		{
			return this.WriteWordWrappedLayer(theRect, theLine, theLineSpacing, theJustification, 0, -1, 0, 0, layer, false);
		}

		public int WriteWordWrappedLayer(TRect theRect, string theLine, int theLineSpacing, int theJustification, int layer, bool centerVertically)
		{
			return this.WriteWordWrappedLayer(theRect, theLine, theLineSpacing, theJustification, 0, -1, 0, 0, layer, centerVertically);
		}

		public int WriteWordWrappedLayer(TRect theRect, string theLine, int theLineSpacing, int theJustification, int theMaxWidth, int theMaxChars, int theLastWidth, int theLineCount, int layer, bool centerVertically)
		{
			Font.CachedStringInfo wordWrappedSubStrings = this.mFont.GetWordWrappedSubStrings(theLine, theRect);
			theRect.mX += this.mTransX;
			theRect.mY += this.mTransY;
			Vector2 vector;
			vector..ctor((float)theRect.mX, (float)theRect.mY);
			this.mFont.GetHeight();
			if (centerVertically)
			{
				float num = 0f;
				for (int i = 0; i < wordWrappedSubStrings.Strings.Length; i++)
				{
					num += wordWrappedSubStrings.StringDimensions[i].Y;
				}
				vector.Y += (float)(theRect.mHeight / 2) - num / 2f;
			}
			for (int j = 0; j < wordWrappedSubStrings.Strings.Length; j++)
			{
				vector.X = (float)theRect.mX;
				if (theJustification == 0)
				{
					vector.X += ((float)theRect.mWidth - wordWrappedSubStrings.StringDimensions[j].X) / 2f;
				}
				this.mFont.DrawStringLayer(this, (int)(vector.X + 0.5f), (int)(vector.Y + 0.5f), wordWrappedSubStrings.Strings[j], this.mColor, layer);
				vector.Y += wordWrappedSubStrings.StringDimensions[j].Y;
			}
			return (int)vector.Y;
		}

		public int WriteWordWrapped(TRect theRect, string theLine, int theLineSpacing, int theJustification)
		{
			return this.WriteWordWrapped(theRect, theLine, theLineSpacing, theJustification, false);
		}

		public int WriteWordWrapped(TRect theRect, string theLine, int theLineSpacing, int theJustification, bool centerVertically)
		{
			return this.WriteWordWrapped(theRect, theLine, theLineSpacing, theJustification, 0, -1, 0, 0, centerVertically);
		}

		public int WriteWordWrapped(TRect theRect, string theLine, int theLineSpacing, int theJustification, int theMaxWidth, int theMaxChars, int theLastWidth, int theLineCount)
		{
			return this.WriteWordWrapped(theRect, theLine, theLineSpacing, theJustification, theMaxWidth, theMaxChars, theLastWidth, theLineCount, false);
		}

		public int WriteWordWrapped(TRect theRect, string theLine, int theLineSpacing, int theJustification, int theMaxWidth, int theMaxChars, int theLastWidth, int theLineCount, bool centerVertically)
		{
			Font.CachedStringInfo wordWrappedSubStrings = this.mFont.GetWordWrappedSubStrings(theLine, theRect);
			theRect.mX += this.mTransX;
			theRect.mY += this.mTransY;
			Vector2 vector;
			vector..ctor((float)theRect.mX, (float)theRect.mY);
			this.mFont.GetHeight();
			if (centerVertically)
			{
				float num = 0f;
				for (int i = 0; i < wordWrappedSubStrings.Strings.Length; i++)
				{
					num += wordWrappedSubStrings.StringDimensions[i].Y;
				}
				vector.Y += (float)(theRect.mHeight / 2) - num / 2f;
			}
			for (int j = 0; j < wordWrappedSubStrings.Strings.Length; j++)
			{
				vector.X = (float)theRect.mX;
				if (theJustification == 0)
				{
					vector.X += ((float)theRect.mWidth - wordWrappedSubStrings.StringDimensions[j].X) / 2f;
				}
				this.mFont.DrawString(this, (int)(vector.X + 0.5f), (int)(vector.Y + 0.5f), wordWrappedSubStrings.Strings[j], this.mColor);
				vector.Y += wordWrappedSubStrings.StringDimensions[j].Y;
			}
			return (int)vector.Y;
		}

		public void DrawStringColor(string theLine, int theX, int theY, int theOldColor)
		{
			this.mFont.DrawString(this, theX, theY, theLine, new SexyColor(theOldColor.ToString()));
		}

		public int GetWordWrappedHeight(int theWidth, string theLine, int theLineSpacing, ref int theMaxWidth, int theMaxChars)
		{
			Graphics g = GlobalStaticVars.g;
			g.SetFont(this.mFont);
			g.SetClipRect(0, 0, 0, 0);
			return g.WriteWordWrapped(new TRect(0, 0, theWidth, 0), theLine, theLineSpacing, -1, theMaxWidth, theMaxChars, 0, 0);
		}

		public bool Is3D()
		{
			return true;
		}

		internal void PopState()
		{
			if (Graphics.mStateStack.Count > 0)
			{
				int mDrawMode = this.mDrawMode;
				int mDrawMode2 = Graphics.mStateStack.Peek().mDrawMode;
				base.CopyStateFrom(Graphics.mStateStack.Peek());
				Graphics graphics = Graphics.mStateStack.Peek();
				bool flag = graphics.mDrawMode != this.mDrawMode;
				graphics.PrepareForReuse();
				Graphics.mStateStack.Pop();
				if (flag && Graphics.spritebatchBegan)
				{
					this.EndFrame();
					this.BeginFrame();
				}
			}
		}

		internal void PushState()
		{
			Graphics @new = Graphics.GetNew(this);
			Graphics.mStateStack.Push(@new);
			if (this.mDrawMode != @new.mDrawMode && Graphics.spritebatchBegan)
			{
				this.EndFrame();
				this.BeginFrame();
			}
		}

		internal void Translate(int x, int y)
		{
			this.mTransX += x;
			this.mTransY += y;
		}

		public void Reset()
		{
			this.mTransX = 0;
			this.mTransY = 0;
			this.mScaleX = 1f;
			this.mScaleY = 1f;
			this.mScaleOrigX = 0f;
			this.mScaleOrigY = 0f;
			this.mFastStretch = false;
			this.mWriteColoredString = false;
			this.mLinearBlend = false;
			this.mClipRect = new TRect(0, 0, this.GraphicsDevice.Viewport.Width, this.GraphicsDevice.Viewport.Height);
			this.ClearClipRect();
			this.mColor = Color.White;
			this.mDrawMode = 0;
			this.mColorizeImages = false;
		}

		internal void PrepareDrawing()
		{
			this.BeginFrame();
		}

		internal void FinishedDrawing()
		{
			this.EndFrame();
		}

		public void DrawTriangleTex(TriVertex p1, TriVertex p2, TriVertex p3, Color theColor, int theDrawMode, Image theTexture)
		{
			this.DrawTriangleTex(p1, p2, p3, theColor, theDrawMode, theTexture, true);
		}

		public void DrawTriangleTex(TriVertex p1, TriVertex p2, TriVertex p3, Color theColor, int theDrawMode, Image theTexture, bool blend)
		{
			this.tempTriangles[0, 0] = p1;
			this.tempTriangles[0, 1] = p2;
			this.tempTriangles[0, 2] = p3;
			this.DrawTrianglesTex(this.tempTriangles, 1, new Color?(theColor), theDrawMode, theTexture, (float)this.mTransX, (float)this.mTransY, blend);
		}

		public void DrawTriangleTex(Image theTexture, TriVertex v1, TriVertex v2, TriVertex v3)
		{
			this.DrawTriangleTex(v1, v2, v3, this.mColorizeImages ? this.mColor : Color.White, this.mDrawMode, theTexture);
		}

		public void DrawTrianglesTex(Image theTexture, TriVertex[,] theVertices, int theNumTriangles)
		{
			this.DrawTrianglesTex(theVertices, theNumTriangles, new Color?(this.mColorizeImages ? this.mColor : Color.White), this.mDrawMode, theTexture, (float)this.mTransX, (float)this.mTransY, this.mLinearBlend);
		}

		public void DrawTrianglesTex(Image theTexture, TriVertex[,] theVertices, int theNumTriangles, Color? theColor, int theDrawMode)
		{
			this.DrawTrianglesTex(theVertices, theNumTriangles, theColor, theDrawMode, theTexture, (float)this.mTransX, (float)this.mTransY, this.mLinearBlend);
		}

		public void DrawTrianglesTex(SamplerState st, Image theTexture, TriVertex[,] theVertices, int theNumTriangles, Color? theColor, int theDrawMode)
		{
			this.DrawTrianglesTex(st, theVertices, theNumTriangles, theColor, theDrawMode, theTexture, (float)this.mTransX, (float)this.mTransY, this.mLinearBlend);
		}

		public void DrawTrianglesTex(TriVertex[,] theVertices, int theNumTriangles, Color? theColor, int theDrawMode, Image theTexture, float tx, float ty, bool blend)
		{
			this.DrawTrianglesTex(null, theVertices, theNumTriangles, theColor, theDrawMode, theTexture, tx, ty, blend);
		}

		public void DrawTrianglesTex(TriVertex[,] theVertices, int theNumTriangles, Color theColor, int theDrawMode, Image theTexture)
		{
			this.DrawTrianglesTex(theVertices, theNumTriangles, new Color?(theColor), theDrawMode, theTexture, 0f, 0f, true);
		}

		public void DrawTrianglesTex(SamplerState st, TriVertex[,] theVertices, int theNumTriangles, Color? theColor, int theDrawMode, Image theTexture, float tx, float ty, bool blend)
		{
			bool spritebatchBegan = Graphics.spritebatchBegan;
			if (Graphics.spritebatchBegan)
			{
				this.EndFrame();
			}
			this.EndDrawImageTransformed(false);
			for (int i = 0; i < theNumTriangles; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					int width = theTexture.Texture.Width;
					int height = theTexture.Texture.Height;
					theVertices[i, j].u = (theVertices[i, j].u * (float)theTexture.GetWidth() + (float)theTexture.mS) / (float)width;
					theVertices[i, j].v = (theVertices[i, j].v * (float)theTexture.GetHeight() + (float)theTexture.mT) / (float)height;
				}
			}
			this.SetupDrawMode(theDrawMode);
			Matrix? transform = default(Matrix?);
			if (Graphics.gTransformStack.Count > 0)
			{
				transform = new Matrix?(Graphics.gTransformStack.Peek().mMatrix);
			}
			if (st != null)
			{
				this.GraphicsDevice.SamplerStates[0] = st;
			}
			Graphics.primitiveBatch.Begin(0, this.mTransX, this.mTransY, transform, theTexture);
			Graphics.primitiveBatch.AddTriVertices(theVertices, theNumTriangles, theColor);
			Graphics.primitiveBatch.End();
			if (spritebatchBegan)
			{
				this.BeginFrame();
			}
		}

		public void BeginDrawTrianglesTexBatch(SamplerState st, int theDrawMode, Image theTexture)
		{
			if (Graphics.spritebatchBegan)
			{
				this.EndFrame();
			}
			this.SetupDrawMode(theDrawMode);
			Matrix? transform = default(Matrix?);
			if (Graphics.gTransformStack.Count > 0)
			{
				transform = new Matrix?(Graphics.gTransformStack.Peek().mMatrix);
			}
			if (st != null)
			{
				this.GraphicsDevice.SamplerStates[0] = st;
			}
			Graphics.primitiveBatch.Begin(0, this.mTransX, this.mTransY, transform, theTexture);
			this.triangleBatchTexture = theTexture;
		}

		public void DrawTrianglesTexBatch(TriVertex[,] theVertices, int theNumTriangles, Color? theColor)
		{
			for (int i = 0; i < theNumTriangles; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					int width = this.triangleBatchTexture.Texture.Width;
					int height = this.triangleBatchTexture.Texture.Height;
					theVertices[i, j].u = (theVertices[i, j].u * (float)this.triangleBatchTexture.GetWidth() + (float)this.triangleBatchTexture.mS) / (float)width;
					theVertices[i, j].v = (theVertices[i, j].v * (float)this.triangleBatchTexture.GetHeight() + (float)this.triangleBatchTexture.mT) / (float)height;
				}
			}
			Graphics.primitiveBatch.AddTriVertices(theVertices, theNumTriangles, theColor);
		}

		public void EndDrawTrianglesTexBatch()
		{
			Graphics.primitiveBatch.End();
			this.BeginFrame();
		}

		public void pushTransform(ref SexyTransform2D theTransform, bool concatenate)
		{
			if (Graphics.gTransformStack.empty<SexyTransform2D>() || !concatenate)
			{
				Graphics.gTransformStack.push_back(theTransform);
			}
			else
			{
				SexyTransform2D b = Graphics.gTransformStack.back<SexyTransform2D>();
				Graphics.gTransformStack.push_back(theTransform * b);
			}
			if (Graphics.spritebatchBegan)
			{
				this.EndFrame();
				this.BeginFrame();
			}
		}

		public void popTransform()
		{
			if (!Graphics.gTransformStack.empty<SexyTransform2D>())
			{
				Graphics.gTransformStack.pop_back<SexyTransform2D>();
				if (Graphics.spritebatchBegan)
				{
					this.EndFrame();
					this.BeginFrame();
				}
			}
		}

		public static void PushTransform(ref SexyTransform2D theTransform, bool concatenate)
		{
			GlobalStaticVars.g.pushTransform(ref theTransform, concatenate);
		}

		public static void PushTransform(ref SexyTransform2D theTransform)
		{
			Graphics.PushTransform(ref theTransform, true);
		}

		public static void ClearTransformStack()
		{
			GlobalStaticVars.g.clearTransformStack();
		}

		public void clearTransformStack()
		{
			Graphics.gTransformStack.Clear();
		}

		public static void PopTransform()
		{
			GlobalStaticVars.g.popTransform();
		}

		public void HardwareClip()
		{
			this.HardwareClip(0);
		}

		public void HardwareClip(SpriteSortMode spriteSortMode)
		{
			this.EndFrame();
			TRect aRect = this.mClipRect;
			aRect = aRect.Intersection(new TRect(0, 0, base.mScreenWidth, base.mScreenHeight));
			this.GraphicsDevice.ScissorRectangle = aRect;
			this.BeginFrame(Graphics.hardwareClipState, spriteSortMode);
		}

		public void EndHardwareClip()
		{
			this.EndFrame();
			this.BeginFrame();
		}

		public void HardwareClipRect(TRect theClip)
		{
			this.HardwareClipRect(theClip, 0);
		}

		public void HardwareClipRect(TRect theClip, SpriteSortMode sortMode)
		{
			Graphics.spriteBatch.End();
			theClip.mX += this.mTransX;
			theClip.mY += this.mTransY;
			Rectangle scissorRectangle = theClip.Intersection(new TRect(0, 0, base.mScreenWidth, base.mScreenHeight));
			this.GraphicsDevice.ScissorRectangle = scissorRectangle;
			this.BeginFrame(Graphics.hardwareClipState, sortMode);
		}

		public void ClearHardwareClipRect()
		{
			Graphics.spriteBatch.End();
			this.BeginFrame();
		}

		public static readonly SamplerState WrapSamplerState = new SamplerState
		{
			AddressU = 0,
			AddressV = 0
		};

		private static readonly SamplerState NormalSamplerState = new SamplerState
		{
			AddressU = 1,
			AddressV = 1,
			AddressW = 1,
			Filter = 0
		};

		private static Stack<Graphics> unusedObjects = new Stack<Graphics>(20);

		public static SpriteBatch gSpriteBatch;

		public static PrimitiveBatch gPrimitiveBatch;

		public RenderTarget2D mDestImage;

		public Game mGame;

		public BlendState blendState = BlendState.AlphaBlend;

		protected static Stack<SexyTransform2D> gTransformStack = new Stack<SexyTransform2D>();

		internal static SpriteBatch spriteBatch;

		protected static PrimitiveBatch primitiveBatch;

		public static Stack<Graphics> mStateStack = new Stack<Graphics>();

		private bool polyFillBegun;

		private TriVertex[,] tempTriangles = new TriVertex[1, 3];

		private Image triangleBatchTexture;

		private static RasterizerState hardwareClipState = new RasterizerState
		{
			ScissorTestEnable = true
		};

		public enum DrawMode
		{
			DRAWMODE_NORMAL,
			DRAWMODE_ADDITIVE
		}
	}
}
