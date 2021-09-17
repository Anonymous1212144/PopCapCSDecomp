using System;
using BejeweledLivePlus.Misc;
using SexyFramework;
using SexyFramework.Graphics;

namespace BejeweledLivePlus.Bej3Graphics
{
	public class GemCollectEffect : ParticleEffect
	{
		public void initWith(Piece thePiece, PIEffect theEffect, int theTargetX, int theTargetY)
		{
			base.initWithPIEffect(theEffect);
			base.SetEmitAfterTimeline(true);
			this.mTargetX = theTargetX;
			this.mTargetY = theTargetY;
			this.mOX = (int)thePiece.CX() - this.mTargetX;
			this.mOY = (int)thePiece.CY() - this.mTargetY;
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eEFFECTS_TRAVEL_PCT_GEM_COLLECT, this.mTravelPct);
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eEFFECTS_ARC_GEM_COLLECT, this.mArc, this.mTravelPct);
		}

		public override void Update()
		{
			base.Update();
			this.mX = (float)this.mTargetX + (float)this.mOX * (float)this.mTravelPct;
			this.mY = (float)this.mTargetY + (float)this.mOY * (float)this.mTravelPct - (float)this.mArc;
			if (this.mTravelPct.HasBeenTriggered())
			{
				base.Stop();
			}
		}

		public new static void initPool()
		{
			GemCollectEffect.thePool_ = new SimpleObjectPool(4096, typeof(GemCollectEffect));
		}

		public static GemCollectEffect from(Piece thePiece, PIEffect theEffect, int theTargetX, int theTargetY)
		{
			GemCollectEffect gemCollectEffect = (GemCollectEffect)GemCollectEffect.thePool_.alloc();
			gemCollectEffect.initWith(thePiece, theEffect, theTargetX, theTargetY);
			return gemCollectEffect;
		}

		public override void release()
		{
			this.Dispose();
			GemCollectEffect.thePool_.release(this);
		}

		public int mTargetX;

		public int mTargetY;

		public int mOX;

		public int mOY;

		public CurvedVal mTravelPct;

		public CurvedVal mArc;

		private static SimpleObjectPool thePool_;
	}
}
