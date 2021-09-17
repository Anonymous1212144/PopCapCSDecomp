using System;
using System.Collections.Generic;
using BejeweledLIVE;
using Microsoft.Xna.Framework;
using Sexy;

namespace Bejeweled3
{
	public class StaticParticleEmitter : ParticleEmitter
	{
		public static Color ProgressBarBackColour { get; private set; }

		public static void PreAllocateMemory()
		{
			List<StaticParticleEmitter> list = new List<StaticParticleEmitter>();
			for (int i = 0; i < 5; i++)
			{
				list.AddRange(StaticParticleEmitter.CreateProgressBarEmitters(0, 0, new Vector2(100f), GameMode.MODE_CLASSIC));
			}
			for (int j = 0; j < list.Count; j++)
			{
				list[j].PrepareForReuse();
			}
		}

		private static StaticParticleEmitter GetNewStaticParticleEmitter(float emissionRate, float particleSpeed, float spreadMin, float spreadMax, float particleLifeTime, float particleSize, Vector2 emitterSize, EmitterShape shape, ParticleType particleType, Color colour, Vector2 position, bool randomiseColour)
		{
			if (StaticParticleEmitter.unusedObjects.Count > 0)
			{
				StaticParticleEmitter staticParticleEmitter = StaticParticleEmitter.unusedObjects.Pop();
				staticParticleEmitter.Reset(emissionRate, particleSpeed, spreadMin, spreadMax, particleLifeTime, particleSize, emitterSize, shape, particleType, colour, randomiseColour);
				return staticParticleEmitter;
			}
			return new StaticParticleEmitter(emissionRate, particleSpeed, spreadMin, spreadMax, particleLifeTime, particleSize, emitterSize, shape, particleType, colour, randomiseColour);
		}

		public override void PrepareForReuse()
		{
			StaticParticleEmitter.unusedObjects.Push(this);
		}

		private new void Reset(float emissionRate, float particleSpeed, float spreadMin, float spreadMax, float particleLifeTime, float particleSize, Vector2 emitterSize, EmitterShape shape, ParticleType particleType, Color colour, bool randomiseColour)
		{
			base.Reset(emissionRate, particleSpeed, spreadMin, spreadMax, particleLifeTime, particleSize, emitterSize, shape, particleType, colour, randomiseColour);
			this.Enabled = true;
		}

		private StaticParticleEmitter(float emissionRate, float particleSpeed, float spreadMin, float spreadMax, float particleLifeTime, float particleSize, Vector2 emitterSize, EmitterShape shape, ParticleType particleType, Color colour, bool randomiseColour)
			: base(emissionRate, particleSpeed, spreadMin, spreadMax, particleLifeTime, particleSize, emitterSize, shape, particleType, colour, randomiseColour)
		{
			this.Reset(emissionRate, particleSpeed, spreadMin, spreadMax, particleLifeTime, particleSize, emitterSize, shape, particleType, colour, randomiseColour);
		}

		public static List<StaticParticleEmitter> CreateProgressBarEmitters(int x, int y, Vector2 size, GameMode mode)
		{
			List<StaticParticleEmitter> list = new List<StaticParticleEmitter>();
			Color[] array;
			if (mode == GameMode.MODE_TIMED)
			{
				array = StaticParticleEmitter.ProgressBarColours_Action;
			}
			else
			{
				array = StaticParticleEmitter.ProgressBarColours_Classic;
			}
			StaticParticleEmitter.ProgressBarBackColour = array[0];
			StaticParticleEmitter newStaticParticleEmitter = StaticParticleEmitter.GetNewStaticParticleEmitter(1.2f, Constants.mConstants.S(0.5f), 0f, 0f, 50f, Constants.mConstants.S(80f), size, EmitterShape.Rectangular, ParticleType.RoundFaded, array[1], new Vector2((float)x, (float)y), false);
			newStaticParticleEmitter.DrawMode = Graphics.DrawMode.DRAWMODE_NORMAL;
			list.Add(newStaticParticleEmitter);
			newStaticParticleEmitter = StaticParticleEmitter.GetNewStaticParticleEmitter(0.7f, Constants.mConstants.S(0.5f), 0f, 0f, 50f, Constants.mConstants.S(30f), size, EmitterShape.Rectangular, ParticleType.RoundFaded, array[2], new Vector2((float)x, (float)y), false);
			newStaticParticleEmitter.DrawMode = Graphics.DrawMode.DRAWMODE_ADDITIVE;
			list.Add(newStaticParticleEmitter);
			newStaticParticleEmitter = StaticParticleEmitter.GetNewStaticParticleEmitter(0.2f, Constants.mConstants.S(0.5f), 0f, 0f, 50f, Constants.mConstants.S(50f), size, EmitterShape.Rectangular, ParticleType.RoundFaded, array[3], new Vector2((float)x, (float)y), false);
			newStaticParticleEmitter.DrawMode = Graphics.DrawMode.DRAWMODE_NORMAL;
			list.Add(newStaticParticleEmitter);
			return list;
		}

		public override void Update()
		{
			base.Update();
		}

		private static readonly Color[] ProgressBarColours_Classic = new Color[]
		{
			new Color(185, 15, 100, 255),
			new Color(255, 0, 250),
			new Color(250, 190, 250),
			new Color(255, 100, 255)
		};

		private static readonly Color[] ProgressBarColours_Action = new Color[]
		{
			new Color(240, 170, 40),
			new Color(230, 90, 50),
			new Color(250, 40, 30),
			new Color(240, 230, 30)
		};

		private static Stack<StaticParticleEmitter> unusedObjects = new Stack<StaticParticleEmitter>();
	}
}
