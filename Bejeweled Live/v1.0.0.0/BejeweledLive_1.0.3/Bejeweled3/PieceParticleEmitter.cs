using System;
using System.Collections.Generic;
using BejeweledLIVE;
using Microsoft.Xna.Framework;
using Sexy;

namespace Bejeweled3
{
	public class PieceParticleEmitter : ParticleEmitter
	{
		public static void PreAllocateMemory()
		{
			List<PieceParticleEmitter> list = new List<PieceParticleEmitter>();
			for (int i = 0; i < 50; i++)
			{
				list.Add(PieceParticleEmitter.CreateHyperCubeEmitter(null));
			}
			for (int j = 0; j < list.Count; j++)
			{
				list[j].PrepareForReuse();
			}
		}

		private static PieceParticleEmitter GetNewPieceParticleEmitter(float emissionRate, float particleSpeed, float spreadMin, float spreadMax, float particleLifeTime, float particleSize, Vector2 emitterSize, EmitterShape shape, ParticleType particleType, Color colour, bool randomiseColour, PieceBejLive associatedPiece)
		{
			if (PieceParticleEmitter.unusedObjects.Count > 0)
			{
				PieceParticleEmitter pieceParticleEmitter = PieceParticleEmitter.unusedObjects.Pop();
				pieceParticleEmitter.Reset(emissionRate, particleSpeed, spreadMin, spreadMax, particleLifeTime, particleSize, emitterSize, shape, particleType, colour, randomiseColour, associatedPiece);
				return pieceParticleEmitter;
			}
			return new PieceParticleEmitter(emissionRate, particleSpeed, spreadMin, spreadMax, particleLifeTime, particleSize, emitterSize, shape, particleType, colour, randomiseColour, associatedPiece);
		}

		public override void PrepareForReuse()
		{
			if (PieceParticleEmitter.unusedObjects.Contains(this))
			{
				return;
			}
			if (this.associatedPiece != null && this.associatedPiece.AssociatedParticleEmitter != this)
			{
				return;
			}
			PieceParticleEmitter.unusedObjects.Push(this);
		}

		private void Reset(float emissionRate, float particleSpeed, float spreadMin, float spreadMax, float particleLifeTime, float particleSize, Vector2 emitterSize, EmitterShape shape, ParticleType particleType, Color colour, bool randomiseColour, PieceBejLive associatedPiece)
		{
			base.Reset(emissionRate, particleSpeed, spreadMin, spreadMax, particleLifeTime, particleSize, emitterSize, shape, particleType, colour, randomiseColour);
			this.associatedPiece = associatedPiece;
			if (associatedPiece != null)
			{
				associatedPiece.AssociatedParticleEmitter = this;
			}
			this.SetpositionFromPiece();
			this.Enabled = true;
		}

		private PieceParticleEmitter(float emissionRate, float particleSpeed, float spreadMin, float spreadMax, float particleLifeTime, float particleSize, Vector2 emitterSize, EmitterShape shape, ParticleType particleType, Color colour, bool randomiseColour, PieceBejLive associatedPiece)
			: base(emissionRate, particleSpeed, spreadMin, spreadMax, particleLifeTime, particleSize, emitterSize, shape, particleType, colour, randomiseColour)
		{
			this.Reset(emissionRate, particleSpeed, spreadMin, spreadMax, particleLifeTime, particleSize, emitterSize, shape, particleType, colour, randomiseColour, associatedPiece);
		}

		public override void Update()
		{
			this.SetpositionFromPiece();
			base.Update();
		}

		private void SetpositionFromPiece()
		{
			if (this.associatedPiece != null)
			{
				base.Position = new Vector2(this.associatedPiece.mX + (float)(GameConstants.GEM_WIDTH / 2) + (float)this.associatedPiece.mOfsX + this.associatedPiece.mFlyVX, this.associatedPiece.mY + (float)(GameConstants.GEM_HEIGHT / 2) + (float)this.associatedPiece.mOfsY + this.associatedPiece.mFlyVY);
			}
		}

		public static PieceParticleEmitter CreateHyperCubeEmitter(PieceBejLive associatedPiece)
		{
			PieceParticleEmitter newPieceParticleEmitter = PieceParticleEmitter.GetNewPieceParticleEmitter(1.3f, Constants.mConstants.S(0.9f), 0f, 6.28318548f, 100f, Constants.mConstants.S(60f), new Vector2(1f), EmitterShape.Rectangular, ParticleType.Rectangular, Color.White, true, associatedPiece);
			newPieceParticleEmitter.DrawMode = Graphics.DrawMode.DRAWMODE_ADDITIVE;
			return newPieceParticleEmitter;
		}

		public override void ForceSetPosition()
		{
			this.SetpositionFromPiece();
			base.ForceSetPosition();
		}

		public static PieceParticleEmitter CreateFlameGemEmitter(PieceBejLive associatedPiece)
		{
			PieceParticleEmitter newPieceParticleEmitter = PieceParticleEmitter.GetNewPieceParticleEmitter(1.2f, Constants.mConstants.S(0.7f), -1.57079637f, -1.57079637f, 50f, Constants.mConstants.S(25f), new Vector2(1f), (EmitterShape)associatedPiece.mColor, ParticleType.RoundFaded, new Color(255, 150, 0), false, associatedPiece);
			newPieceParticleEmitter.DrawMode = Graphics.DrawMode.DRAWMODE_ADDITIVE;
			return newPieceParticleEmitter;
		}

		private PieceBejLive associatedPiece;

		private static Stack<PieceParticleEmitter> unusedObjects = new Stack<PieceParticleEmitter>();
	}
}
