using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sexy
{
	public class PrimitiveBatch : IDisposable
	{
		public PrimitiveBatch(GraphicsDevice graphicsDevice)
		{
			if (graphicsDevice == null)
			{
				throw new ArgumentNullException("graphicsDevice");
			}
			this.device = graphicsDevice;
			this.basicEffect = new BasicEffect(graphicsDevice);
			this.basicEffect.VertexColorEnabled = true;
		}

		public void SetupMatrices()
		{
			this.basicEffect.Projection = Matrix.CreateOrthographicOffCenter(0f, (float)this.device.PresentationParameters.BackBufferWidth, (float)(this.device.PresentationParameters.BackBufferHeight - 2), 1f, 0f, 1f);
			this.screenWidth = this.device.PresentationParameters.BackBufferWidth;
			this.screenHeight = this.device.PresentationParameters.BackBufferHeight;
		}

		public void Draw(Image img, TRect destination, TRect source, Matrix? transform, Vector2 center, Color colour)
		{
			if (transform != null)
			{
				Matrix value = transform.Value;
				value.M41 -= center.X;
				value.M42 -= center.Y;
				this.Transform = new Matrix?(value);
			}
			else
			{
				this.Transform = default(Matrix?);
			}
			this.Texture = img;
			VertexPositionColorTexture vertexPositionColorTexture = default(VertexPositionColorTexture);
			vertexPositionColorTexture.Color = colour;
			source.mX += img.mS;
			source.mY += img.mT;
			source.mWidth -= img.mWidth - source.mWidth;
			source.mHeight -= img.mHeight - source.mHeight;
			float num = (float)source.mY / (float)img.Texture.Height;
			float num2 = (float)(source.mY + source.mHeight) / (float)img.Texture.Height;
			float num3 = (float)source.mX / (float)img.Texture.Width;
			float num4 = (float)(source.mX + source.mWidth) / (float)img.Texture.Width;
			vertexPositionColorTexture.Position = new Vector3((float)destination.mX, (float)destination.mY, 0f);
			vertexPositionColorTexture.TextureCoordinate = new Vector2(num3, num);
			this.AddVertex(ref vertexPositionColorTexture, center);
			vertexPositionColorTexture.Position = new Vector3((float)(destination.mX + destination.mWidth), (float)destination.mY, 0f);
			vertexPositionColorTexture.TextureCoordinate = new Vector2(num4, num);
			this.AddVertex(ref vertexPositionColorTexture, center);
			vertexPositionColorTexture.Position = new Vector3((float)destination.mX, (float)(destination.mY + destination.mHeight), 0f);
			vertexPositionColorTexture.TextureCoordinate = new Vector2(num3, num2);
			this.AddVertex(ref vertexPositionColorTexture, center);
			vertexPositionColorTexture.Position = new Vector3((float)destination.mX, (float)(destination.mY + destination.mHeight), 0f);
			vertexPositionColorTexture.TextureCoordinate = new Vector2(num3, num2);
			this.AddVertex(ref vertexPositionColorTexture, center);
			vertexPositionColorTexture.Position = new Vector3((float)(destination.mX + destination.mWidth), (float)(destination.mY + destination.mHeight), 0f);
			vertexPositionColorTexture.TextureCoordinate = new Vector2(num4, num2);
			this.AddVertex(ref vertexPositionColorTexture, center);
			vertexPositionColorTexture.Position = new Vector3((float)(destination.mX + destination.mWidth), (float)destination.mY, 0f);
			vertexPositionColorTexture.TextureCoordinate = new Vector2(num4, num);
			this.AddVertex(ref vertexPositionColorTexture, center);
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing && !this.isDisposed)
			{
				if (this.basicEffect != null)
				{
					this.basicEffect.Dispose();
				}
				this.isDisposed = true;
			}
		}

		public void Begin(PrimitiveType primitiveType, int offsetX, int offsetY, Matrix? transform, Image texture)
		{
			GlobalStaticVars.g.GraphicsDevice.RasterizerState = this.rasterizerState;
			if (this.hasBegun)
			{
				throw new InvalidOperationException("End must be called before Begin can be called again.");
			}
			if (this.screenWidth != this.device.PresentationParameters.BackBufferWidth || this.screenHeight != this.device.PresentationParameters.BackBufferHeight)
			{
				this.SetupMatrices();
			}
			if (primitiveType == 3 || primitiveType == 1)
			{
				throw new NotSupportedException("The specified primitiveType is not supported by PrimitiveBatch.");
			}
			this.hasBegun = true;
			this.primitiveType = primitiveType;
			this.numVertsPerPrimitive = PrimitiveBatch.NumVertsPerPrimitive(primitiveType);
			this.Texture = texture;
			this.OffsetX = offsetX;
			this.OffsetY = offsetY;
			this.Transform = transform;
			this.basicEffect.CurrentTechnique.Passes[0].Apply();
		}

		public void AddTriVertices(TriVertex[,] vertices, int triangleCount, Color? theColor)
		{
			for (int i = 0; i < triangleCount; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					if (theColor != null)
					{
						vertices[i, j].color = theColor.Value;
					}
					this.AddVertex(ref vertices[i, j].mVert);
				}
			}
		}

		public void AddVertex(Vector2 vertex, Color color)
		{
			if (!this.hasBegun)
			{
				throw new InvalidOperationException("Begin must be called before AddVertex can be called.");
			}
			bool flag = this.positionInBuffer % this.numVertsPerPrimitive == 0;
			if (flag && this.positionInBuffer + this.numVertsPerPrimitive >= this.VerticesLength)
			{
				this.Flush();
			}
			vertex.X += (float)this.OffsetX;
			vertex.Y += (float)this.OffsetY;
			if (this.Transform != null)
			{
				vertex = Vector2.Transform(vertex, this.Transform.Value);
			}
			if (this.texture == null)
			{
				this.vertices[this.positionInBuffer].Position = new Vector3(vertex.X, vertex.Y, 0f);
				this.vertices[this.positionInBuffer].Color = color;
			}
			else
			{
				this.texturedVertices[this.positionInBuffer].Position = new Vector3(vertex.X, vertex.Y, 0f);
				this.texturedVertices[this.positionInBuffer].Color = color;
				this.texturedVertices[this.positionInBuffer].TextureCoordinate = Vector2.Zero;
			}
			this.positionInBuffer++;
		}

		public void AddVertex(ref VertexPositionColorTexture vertex)
		{
			this.AddVertex(ref vertex, Vector2.Zero);
		}

		public void AddVertex(ref VertexPositionColorTexture vertex, Vector2 center)
		{
			if (!this.hasBegun)
			{
				throw new InvalidOperationException("Begin must be called before AddVertex can be called.");
			}
			bool flag = this.positionInBuffer % this.numVertsPerPrimitive == 0;
			if (flag && this.positionInBuffer + this.numVertsPerPrimitive >= this.VerticesLength)
			{
				this.Flush();
			}
			vertex.Position.X = vertex.Position.X + (float)this.OffsetX;
			vertex.Position.Y = vertex.Position.Y + (float)this.OffsetY;
			if (this.Transform != null)
			{
				vertex.Position.X = vertex.Position.X - center.X;
				vertex.Position.Y = vertex.Position.Y - center.Y;
				vertex.Position = Vector3.Transform(vertex.Position, this.Transform.Value);
				vertex.Position.X = vertex.Position.X + center.X;
				vertex.Position.Y = vertex.Position.Y + center.Y;
			}
			if (this.texture == null)
			{
				this.vertices[this.positionInBuffer].Position = vertex.Position;
				this.vertices[this.positionInBuffer].Color = vertex.Color;
			}
			else
			{
				this.texturedVertices[this.positionInBuffer] = vertex;
			}
			this.positionInBuffer++;
		}

		private int VerticesLength
		{
			get
			{
				if (this.texture != null)
				{
					return this.texturedVertices.Length;
				}
				return this.vertices.Length;
			}
		}

		public void End()
		{
			if (!this.hasBegun)
			{
				throw new InvalidOperationException("Begin must be called before End can be called.");
			}
			this.Flush();
			this.hasBegun = false;
		}

		private void Flush()
		{
			if (!this.hasBegun)
			{
				throw new InvalidOperationException("Begin must be called before Flush can be called.");
			}
			if (this.positionInBuffer == 0)
			{
				return;
			}
			int num = this.positionInBuffer / this.numVertsPerPrimitive;
			if (this.texture == null)
			{
				this.device.DrawUserPrimitives<VertexPositionColor>(this.primitiveType, this.vertices, 0, num);
			}
			else
			{
				this.device.DrawUserPrimitives<VertexPositionColorTexture>(this.primitiveType, this.texturedVertices, 0, num);
			}
			this.positionInBuffer = 0;
		}

		public bool HasBegun
		{
			get
			{
				return this.hasBegun;
			}
		}

		public Image Texture
		{
			get
			{
				return this.texture;
			}
			set
			{
				if (this.texture == null && value == null)
				{
					return;
				}
				if (this.texture != null && value != null && this.texture.Texture == value.Texture)
				{
					return;
				}
				this.Flush();
				this.texture = value;
				if (this.texture != null)
				{
					this.basicEffect.TextureEnabled = true;
					this.basicEffect.Texture = this.texture.Texture;
				}
				else
				{
					this.basicEffect.TextureEnabled = false;
					this.basicEffect.Texture = null;
				}
				this.basicEffect.CurrentTechnique.Passes[0].Apply();
			}
		}

		private static int NumVertsPerPrimitive(PrimitiveType primitive)
		{
			switch (primitive)
			{
			case 0:
				return 3;
			case 2:
				return 2;
			}
			throw new InvalidOperationException("primitive is not valid");
		}

		private VertexPositionColor[] vertices = new VertexPositionColor[1024];

		private VertexPositionColorTexture[] texturedVertices = new VertexPositionColorTexture[1024];

		private int positionInBuffer;

		private BasicEffect basicEffect;

		private GraphicsDevice device;

		private PrimitiveType primitiveType;

		private int numVertsPerPrimitive;

		private bool hasBegun;

		private bool isDisposed;

		private Image texture;

		public int OffsetX;

		public int OffsetY;

		public Matrix? Transform;

		private int screenWidth;

		private int screenHeight;

		private RasterizerState rasterizerState = new RasterizerState
		{
			CullMode = 0
		};
	}
}
