using System;
using BejeweledLivePlus.Misc;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace BejeweledLivePlus.Bej3Graphics
{
	public class ButterflyEffect : Effect
	{
		public ButterflyEffect()
			: base(Effect.Type.TYPE_CUSTOMCLASS)
		{
		}

		public void init(Piece thePiece, ButterflyBoard Owner)
		{
			base.init(Effect.Type.TYPE_CUSTOMCLASS);
			this.mX = thePiece.GetScreenX();
			this.mY = thePiece.GetScreenY();
			this.mColorIdx = thePiece.mColor;
			this.mDAlpha = 0f;
			this.mFlap = 0f;
			this.mAccel = 0f;
			this.mCurvedAlpha.SetConstant(1.0);
			this.mOwner = Owner;
			this.mOwner.mCurrentReleasedButterflies++;
			this.mSparkles = ParticleEffect.fromPIEffect(GlobalMembersResourcesWP.PIEFFECT_BUTTERFLY);
			this.mSparkles.SetEmitAfterTimeline(true);
			this.mSparkles.mDoDrawTransform = false;
			this.mSparkles.SetEmitterTint(0, 0, GlobalMembers.gGemColors[this.mColorIdx]);
			this.mSparkles.SetEmitterTint(0, 1, GlobalMembers.gGemColors[this.mColorIdx]);
			this.mSparkles.mDoubleSpeed = true;
			GlobalMembers.gApp.mBoard.mPostFXManager.AddEffect(this.mSparkles);
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eEFFECTS_TARGET_X_BUTTERFLY, this.mTargetX);
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eEFFECTS_TARGET_Y_BUTTERFLY, this.mTargetY);
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eEFFECTS_CURVED_ALPHA_BUTTERFLY, this.mCurvedAlpha);
		}

		public override void Dispose()
		{
			this.mOwner.mCurrentReleasedButterflies--;
			this.mSparkles.Stop();
			base.Dispose();
		}

		public override void Update()
		{
			base.Update();
			this.mX += (float)((this.mTargetX - (double)this.mX) * (double)this.mAccel);
			this.mY += (float)((this.mTargetY - (double)this.mY) * (double)this.mAccel);
			this.mFlap += (((int)this.mX == (int)this.mTargetX && (int)this.mY == (int)this.mTargetY) ? GlobalMembers.M(0.05f) : GlobalMembers.M(0.4f));
			if ((double)this.mFlap >= 6.2831853071795862)
			{
				this.mFlap -= 6.28318548f;
			}
			this.mAccel = Math.Min(this.mAccel + GlobalMembers.M(0.0002f), 1f);
			this.mSparkles.mX = this.mX + 50f;
			this.mSparkles.mY = this.mY + 50f;
			if ((this.mX > 1900f || this.mCurvedAlpha.HasBeenTriggered()) && !this.mDeleteMe)
			{
				int num = (int)this.mSparkles.mX;
				int num2 = (int)this.mSparkles.mY;
				ParticleEffect particleEffect = ParticleEffect.fromPIEffect(GlobalMembersResourcesWP.PIEFFECT_BUTTERFLY_CREATE);
				particleEffect.SetEmitterTint(0, 0, GlobalMembers.gGemColors[this.mColorIdx]);
				particleEffect.SetEmitterTint(0, 1, GlobalMembers.gGemColors[this.mColorIdx]);
				particleEffect.SetEmitterTint(0, 2, GlobalMembers.gGemColors[this.mColorIdx]);
				particleEffect.mDoubleSpeed = true;
				particleEffect.mX = (float)num;
				particleEffect.mY = (float)num2;
				GlobalMembers.gApp.mBoard.mPostFXManager.AddEffect(particleEffect);
				this.mDeleteMe = true;
			}
			this.mTargetX.IncInVal();
			this.mTargetY.IncInVal();
			int mCurrentReleasedButterflies = this.mOwner.mCurrentReleasedButterflies;
		}

		public override void Draw(Graphics g)
		{
			float sx = (float)(Math.Cos((double)this.mFlap) * 0.25 + 0.75);
			g.SetDrawMode(Graphics.DrawMode.Normal);
			g.SetColorizeImages(true);
			float num = (float)(this.mCurvedAlpha * 255.0);
			if (GlobalMembers.gApp.mBoard != null)
			{
				num *= GlobalMembers.gApp.mBoard.GetAlpha();
			}
			g.SetColor(new Color(255, 255, 255, (int)num));
			g.DrawImageCel(GlobalMembersResourcesWP.IMAGE_BUTTERFLY_SHADOW, (int)GlobalMembers.S(this.mX), (int)GlobalMembers.S(this.mY), this.mColorIdx);
			Transform transform = new Transform();
			transform.Translate((float)GlobalMembers.S(ConstantsWP.BUTTERFLY_DRAW_OFFSET_1), 0f);
			transform.Scale(sx, 1f);
			g.DrawImageTransform(GlobalMembersResourcesWP.IMAGE_BUTTERFLY_WINGS, transform, GlobalMembers.IMGSRCRECT(GlobalMembersResourcesWP.IMAGE_BUTTERFLY_WINGS, this.mColorIdx), GlobalMembers.S(this.mX + (float)ConstantsWP.BUTTERFLY_DRAW_OFFSET_2), GlobalMembers.S(this.mY + (float)ConstantsWP.BUTTERFLY_DRAW_OFFSET_3));
			transform.Scale(-1f, 1f);
			g.DrawImageTransform(GlobalMembersResourcesWP.IMAGE_BUTTERFLY_WINGS, transform, GlobalMembers.IMGSRCRECT(GlobalMembersResourcesWP.IMAGE_BUTTERFLY_WINGS, this.mColorIdx), GlobalMembers.S(this.mX + (float)ConstantsWP.BUTTERFLY_DRAW_OFFSET_4), GlobalMembers.S(this.mY + (float)ConstantsWP.BUTTERFLY_DRAW_OFFSET_3));
			g.DrawImageCel(GlobalMembersResourcesWP.IMAGE_BUTTERFLY_BODY, (int)GlobalMembers.S(this.mX), (int)GlobalMembers.S(this.mY), this.mColorIdx);
			g.SetColor(Color.White);
			g.SetColorizeImages(false);
		}

		public new static void initPool()
		{
			ButterflyEffect.thePool_ = new SimpleObjectPool(512, typeof(ButterflyEffect));
		}

		public static ButterflyEffect alloc(Piece thePiece, ButterflyBoard owner)
		{
			ButterflyEffect butterflyEffect = (ButterflyEffect)ButterflyEffect.thePool_.alloc();
			butterflyEffect.init(thePiece, owner);
			return butterflyEffect;
		}

		public override void release()
		{
			this.Dispose();
			ButterflyEffect.thePool_.release(this);
		}

		public CurvedVal mTargetX = new CurvedVal();

		public CurvedVal mTargetY = new CurvedVal();

		public ParticleEffect mSparkles;

		public int mColorIdx;

		public float mFlap;

		public float mAccel;

		public ButterflyBoard mOwner;

		private static SimpleObjectPool thePool_;
	}
}
