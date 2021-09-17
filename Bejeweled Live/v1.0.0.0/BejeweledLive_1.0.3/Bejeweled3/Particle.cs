using System;
using Microsoft.Xna.Framework;
using Sexy;

namespace Bejeweled3
{
	public class Particle
	{
		public void Reset(float timeToLive, Vector2 pos, float size, Vector2 speed, Color colour, ParticleType type)
		{
			this.TimeToLive = timeToLive;
			this.startTimeToLive = timeToLive;
			this.position = pos;
			this.size = (int)size;
			this.speed = speed;
			this.colour = colour;
			this.particleVariation = type;
			this.img = AtlasResources.GetImageInAtlasById((int)(10170 + this.particleVariation));
		}

		public void Draw(Graphics g, float opacity)
		{
			this.colour.A = (byte)(Math.Min(1f, this.TimeToLive / (this.startTimeToLive * 0.5f)) * 255f);
			this.colour.A = (byte)((float)this.colour.A * opacity);
			g.SetColor(this.colour);
			g.DrawImage(this.img, (int)(this.position.X - (float)(this.size / 2)), (int)(this.position.Y - (float)(this.size / 2)), this.size, this.size);
		}

		public void Update()
		{
			this.TimeToLive -= 1f;
			this.position += this.speed;
		}

		private const float FADE_DELAY = 0.5f;

		private float startTimeToLive;

		public float TimeToLive;

		private Vector2 position;

		private int size;

		private Vector2 speed;

		private Color colour;

		private ParticleType particleVariation;

		private Image img = AtlasResources.GetImageInAtlasById(10170);
	}
}
