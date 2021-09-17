using System;
using SexyFramework.Graphics;

namespace BejeweledLivePlus.Bej3Graphics
{
	public class BlingParticleEffect : ParticleEffect
	{
		public void initWithPIEffectAndPieceId(PIEffect thePIEffect, int thePieceId)
		{
			base.initWithPIEffect(thePIEffect);
			this.mPieceId = thePieceId;
			base.SetEmitAfterTimeline(true);
			this.mActive = true;
		}

		public override void Dispose()
		{
			base.Dispose();
		}

		public override void Update()
		{
			Piece pieceById = this.mFXManager.mBoard.GetPieceById(this.mPieceId);
			if (pieceById != null)
			{
				this.mX = pieceById.CX();
				this.mY = pieceById.CY();
			}
			else
			{
				base.Stop();
			}
			base.Update();
		}

		public void SetActive(bool theVal)
		{
			if (this.mActive != theVal)
			{
				this.mActive = theVal;
				for (int i = 0; i < this.mPIEffect.mLayerVector.Count; i++)
				{
					PILayer layer = this.mPIEffect.GetLayer(i);
					for (int j = 0; j < layer.mEmitterInstanceVector.Count; j++)
					{
						PIEmitterInstance piemitterInstance = layer.mEmitterInstanceVector[j];
						piemitterInstance.mNumberScale = (this.mActive ? 1f : 0f);
					}
				}
			}
		}

		public new static void initPool()
		{
			BlingParticleEffect.thePool_ = new SimpleObjectPool(4096, typeof(BlingParticleEffect));
		}

		public static BlingParticleEffect fromPIEffectAndPieceId(PIEffect thePIEffect, int thePieceId)
		{
			BlingParticleEffect blingParticleEffect = (BlingParticleEffect)BlingParticleEffect.thePool_.alloc();
			blingParticleEffect.initWithPIEffectAndPieceId(thePIEffect, thePieceId);
			return blingParticleEffect;
		}

		public override void release()
		{
			this.Dispose();
			BlingParticleEffect.thePool_.release(this);
		}

		public new int mPieceId;

		public bool mActive;

		private static SimpleObjectPool thePool_;
	}
}
