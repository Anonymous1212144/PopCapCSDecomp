using System;
using System.Collections.Generic;
using BejeweledLIVE;
using Microsoft.Xna.Framework;
using Sexy;

namespace Bejeweled3
{
	public abstract class ParticleEmitter
	{
		static ParticleEmitter()
		{
			List<Line> list = new List<Line>();
			list.Add(new Line(new Vector2(0.5f, 0f), new Vector2(1f, 0.5f)));
			list.Add(new Line(new Vector2(1f, 0.5f), new Vector2(0.5f, 1f)));
			list.Add(new Line(new Vector2(0.5f, 1f), new Vector2(0f, 0.5f)));
			list.Add(new Line(new Vector2(0f, 0.5f), new Vector2(0.5f, 0f)));
			ParticleEmitter.flameEmitterLines.Add(list);
			List<Line> list2 = new List<Line>();
			ParticleEmitter.flameEmitterLines.Add(list2);
			List<Line> list3 = new List<Line>();
			list3.Add(new Line(new Vector2(0f, 0.3f), new Vector2(0.3f, 0f)));
			list3.Add(new Line(new Vector2(0.3f, 0f), new Vector2(0.7f, 0f)));
			list3.Add(new Line(new Vector2(0.7f, 0f), new Vector2(1f, 0.3f)));
			list3.Add(new Line(new Vector2(1f, 0.3f), new Vector2(0.5f, 1f)));
			list3.Add(new Line(new Vector2(0.5f, 1f), new Vector2(0f, 0.3f)));
			ParticleEmitter.flameEmitterLines.Add(list3);
			List<Line> list4 = new List<Line>();
			list4.Add(new Line(new Vector2(0f, 0.2f), new Vector2(0.2f, 0f)));
			list4.Add(new Line(new Vector2(0.2f, 0f), new Vector2(0.8f, 0f)));
			list4.Add(new Line(new Vector2(0.8f, 0f), new Vector2(1f, 0.2f)));
			list4.Add(new Line(new Vector2(1f, 0.2f), new Vector2(1f, 0.8f)));
			list4.Add(new Line(new Vector2(1f, 0.8f), new Vector2(0.8f, 1f)));
			list4.Add(new Line(new Vector2(0.8f, 1f), new Vector2(0.2f, 1f)));
			list4.Add(new Line(new Vector2(0.2f, 1f), new Vector2(0f, 0.8f)));
			list4.Add(new Line(new Vector2(0f, 0.8f), new Vector2(0f, 0.2f)));
			ParticleEmitter.flameEmitterLines.Add(list4);
			List<Line> list5 = new List<Line>();
			list5.Add(new Line(new Vector2(0.5f, 0f), new Vector2(1f, 1f)));
			list5.Add(new Line(new Vector2(1f, 1f), new Vector2(0f, 1f)));
			list5.Add(new Line(new Vector2(0f, 1f), new Vector2(0.5f, 0f)));
			ParticleEmitter.flameEmitterLines.Add(list5);
			List<Line> list6 = new List<Line>();
			list6.Add(new Line(new Vector2(0f, 0.3f), new Vector2(0.5f, 0f)));
			list6.Add(new Line(new Vector2(0.5f, 0f), new Vector2(1f, 0.3f)));
			list6.Add(new Line(new Vector2(1f, 0.3f), new Vector2(1f, 0.7f)));
			list6.Add(new Line(new Vector2(1f, 0.7f), new Vector2(0.5f, 1f)));
			list6.Add(new Line(new Vector2(0.5f, 1f), new Vector2(0f, 0.7f)));
			list6.Add(new Line(new Vector2(0f, 0.7f), new Vector2(0f, 0.3f)));
			ParticleEmitter.flameEmitterLines.Add(list6);
			List<Line> list7 = new List<Line>();
			list7.Add(new Line(new Vector2(0f, 0.5f), new Vector2(0.1f, 0.2f)));
			list7.Add(new Line(new Vector2(0.1f, 0.2f), new Vector2(0.4f, 0f)));
			list7.Add(new Line(new Vector2(0.4f, 0f), new Vector2(0.6f, 0f)));
			list7.Add(new Line(new Vector2(0.6f, 0f), new Vector2(0.9f, 0.2f)));
			list7.Add(new Line(new Vector2(0.9f, 0.2f), new Vector2(1f, 0.5f)));
			list7.Add(new Line(new Vector2(1f, 0.5f), new Vector2(0.9f, 0.8f)));
			list7.Add(new Line(new Vector2(0.9f, 0.8f), new Vector2(0.6f, 1f)));
			list7.Add(new Line(new Vector2(0.6f, 1f), new Vector2(0.4f, 1f)));
			list7.Add(new Line(new Vector2(0.4f, 1f), new Vector2(0.1f, 0.8f)));
			list7.Add(new Line(new Vector2(0.1f, 0.8f), new Vector2(0f, 0.5f)));
			ParticleEmitter.flameEmitterLines.Add(list7);
			List<Line> list8 = new List<Line>();
			ParticleEmitter.flameEmitterLines.Add(list8);
		}

		protected ParticleEmitter(float emissionRate, float particleSpeed, float spreadMin, float spreadMax, float particleLifeTime, float particleSize, Vector2 emitterSize, EmitterShape shape, ParticleType particleType, Color colour, bool randomiseColour)
		{
			for (int i = 0; i < this.particles.Length; i++)
			{
				this.particles[i] = new Particle();
			}
			this.Reset(emissionRate, particleSpeed, spreadMin, spreadMax, particleLifeTime, particleSize, emitterSize, shape, particleType, colour, randomiseColour);
		}

		public abstract void PrepareForReuse();

		protected void Reset(float emissionRate, float particleSpeed, float spreadMin, float spreadMax, float particleLifeTime, float particleSize, Vector2 emitterSize, EmitterShape shape, ParticleType particleType, Color colour, bool randomiseColour)
		{
			this.Enabled = false;
			this.firstUsedParticle = (this.firstFreeParticle = 0);
			this.EmissionRate = emissionRate;
			this.ParticleSpeed = particleSpeed;
			this.Shape = shape;
			this.ParticleLifeTime = particleLifeTime;
			this.ParticleSize = particleSize;
			this.SpreadMin = spreadMin;
			this.SpreadMax = spreadMax;
			this.Size = emitterSize;
			this.randomiseColour = randomiseColour;
			this.ParticleType = particleType;
			this.ParticleColour = colour;
		}

		public void ClearParticles()
		{
			this.firstFreeParticle = (this.firstUsedParticle = 0);
			this.position = this.nextPosition;
		}

		public virtual void Update()
		{
			if (this.firstUsedParticle <= this.firstFreeParticle)
			{
				for (int i = this.firstUsedParticle; i < this.firstFreeParticle; i++)
				{
					this.UpdateParticleAt(ref this.particles[i]);
				}
			}
			else
			{
				for (int j = this.firstUsedParticle; j < this.particles.Length; j++)
				{
					this.UpdateParticleAt(ref this.particles[j]);
				}
				for (int k = 0; k < this.firstFreeParticle; k++)
				{
					this.UpdateParticleAt(ref this.particles[k]);
				}
			}
			if (!this.Enabled)
			{
				return;
			}
			this.emmissionsDue = this.EmissionRate + this.emmissionsDue;
			for (int l = 0; l < (int)this.emmissionsDue; l++)
			{
				this.firstFreeParticle = this.GetParticleAfter(this.firstFreeParticle);
				this.EnsureCapacity();
				Vector2 pos;
				Vector2.Lerp(ref this.position, ref this.nextPosition, (float)l / this.emmissionsDue, ref pos);
				switch (this.Shape)
				{
				case EmitterShape.Gem0:
				case EmitterShape.Gem2:
				case EmitterShape.Gem3:
				case EmitterShape.Gem4:
				case EmitterShape.Gem5:
				case EmitterShape.Gem6:
				case EmitterShape.Gem7:
				{
					Line line = ParticleEmitter.flameEmitterLines[(int)this.Shape][ParticleEmitter.rand.Next(ParticleEmitter.flameEmitterLines[(int)this.Shape].Count)];
					float num = (float)ParticleEmitter.rand.NextDouble();
					pos.X += (line.Start.X + num * (line.End.X - line.Start.X) - 0.5f) * (float)GameConstants.GEM_WIDTH * 0.8f;
					pos.Y += (line.Start.Y + num * (line.End.Y - line.Start.Y) - 0.5f) * (float)GameConstants.GEM_HEIGHT * 0.8f;
					break;
				}
				case EmitterShape.Gem1:
				{
					Vector2 vector;
					vector..ctor((float)GameConstants.GEM_WIDTH * 0.8f / 2f, (float)GameConstants.GEM_HEIGHT * 0.8f / 2f);
					float num2 = vector.X;
					float num3 = vector.Y;
					float num4;
					float num5;
					if (ParticleEmitter.rand.Next(2) == 0)
					{
						num4 = (float)ParticleEmitter.rand.NextDouble() * vector.Y;
						num2 *= num2;
						num5 = (float)Math.Sqrt((double)(num2 - num4 * num4 * num2 / (num3 * num3)));
					}
					else
					{
						num5 = (float)ParticleEmitter.rand.NextDouble() * vector.X;
						num3 *= num3;
						num4 = (float)Math.Sqrt((double)(num3 - num5 * num5 * num3 / (num2 * num2)));
					}
					if (ParticleEmitter.rand.Next(2) == 0)
					{
						num5 *= -1f;
					}
					if (ParticleEmitter.rand.Next(2) == 0)
					{
						num4 *= -1f;
					}
					pos.X += num5;
					pos.Y += num4;
					break;
				}
				case EmitterShape.Rectangular:
					pos.X += (float)ParticleEmitter.rand.NextDouble() * this.Size.X - this.Size.X / 2f;
					pos.Y += (float)ParticleEmitter.rand.NextDouble() * this.Size.Y - this.Size.Y / 2f;
					break;
				}
				Vector2 vector2 = new Vector2((float)ParticleEmitter.rand.NextDouble() * this.ParticleSpeed, 0f);
				Matrix matrix = Matrix.CreateRotationZ(this.SpreadMin + (float)ParticleEmitter.rand.NextDouble() * (this.SpreadMax - this.SpreadMin));
				Vector2.Transform(ref vector2, ref matrix, ref vector2);
				Color particleColour;
				if (this.randomiseColour)
				{
					particleColour..ctor(ParticleEmitter.rand.Next(255), ParticleEmitter.rand.Next(255), ParticleEmitter.rand.Next(255));
				}
				else
				{
					particleColour = this.ParticleColour;
				}
				this.particles[this.firstFreeParticle].Reset(this.ParticleLifeTime, pos, (float)ParticleEmitter.rand.NextDouble() * this.ParticleSize, (float)ParticleEmitter.rand.NextDouble() * vector2, particleColour, this.ParticleType);
			}
			this.emmissionsDue %= 1f;
			this.position = this.nextPosition;
		}

		public virtual void ForceSetPosition()
		{
			this.position = this.nextPosition;
		}

		private void UpdateParticleAt(ref Particle p)
		{
			p.Update();
			if (p.TimeToLive <= 0f)
			{
				this.firstUsedParticle = this.GetParticleAfter(this.firstUsedParticle);
			}
		}

		private void EnsureCapacity()
		{
			if (this.GetParticleAfter(this.firstFreeParticle) == this.firstUsedParticle)
			{
				int num = this.particles.Length * 2;
				Particle[] array = new Particle[num];
				if (this.firstUsedParticle <= this.firstFreeParticle)
				{
					Array.Copy(this.particles, array, this.particles.Length);
					for (int i = this.particles.Length; i < array.Length; i++)
					{
						array[i] = new Particle();
					}
				}
				else
				{
					Array.Copy(this.particles, this.firstUsedParticle, array, 0, this.particles.Length - this.firstUsedParticle);
					Array.Copy(this.particles, 0, array, this.particles.Length - this.firstUsedParticle, this.firstFreeParticle + 1);
					this.firstUsedParticle = 0;
					this.firstFreeParticle = this.particles.Length;
					for (int j = this.particles.Length; j < array.Length; j++)
					{
						array[j] = new Particle();
					}
				}
				this.particles = array;
			}
		}

		private int GetParticleAfter(int particle)
		{
			return (particle + 1) % this.particles.Length;
		}

		public void Draw(Graphics g, float opacity)
		{
			g.SetDrawMode(this.DrawMode);
			g.SetColorizeImages(true);
			if (this.firstUsedParticle <= this.firstFreeParticle)
			{
				for (int i = this.firstUsedParticle; i < this.firstFreeParticle; i++)
				{
					this.particles[i].Draw(g, opacity);
				}
				return;
			}
			for (int j = this.firstUsedParticle; j < this.particles.Length; j++)
			{
				this.particles[j].Draw(g, opacity);
			}
			for (int k = 0; k < this.firstFreeParticle; k++)
			{
				this.particles[k].Draw(g, opacity);
			}
		}

		public Vector2 Position
		{
			get
			{
				return this.position;
			}
			set
			{
				if (this.Enabled)
				{
					this.nextPosition = value;
					return;
				}
				this.position = value;
				this.nextPosition = value;
			}
		}

		private static readonly List<List<Line>> flameEmitterLines = new List<List<Line>>();

		public Graphics.DrawMode DrawMode = Graphics.DrawMode.DRAWMODE_ADDITIVE;

		public bool randomiseColour;

		public EmitterShape Shape;

		public Vector2 Size;

		public bool Enabled;

		public float ParticleLifeTime;

		public float ParticleSize;

		public Color ParticleColour;

		public float EmissionRate;

		public float ParticleSpeed;

		public float SpreadMin;

		public float SpreadMax;

		public ParticleType ParticleType;

		private static Random rand = new Random();

		private Particle[] particles = new Particle[400];

		private Vector2 position;

		private Vector2 nextPosition;

		private int firstUsedParticle;

		private int firstFreeParticle;

		private float emmissionsDue;
	}
}
