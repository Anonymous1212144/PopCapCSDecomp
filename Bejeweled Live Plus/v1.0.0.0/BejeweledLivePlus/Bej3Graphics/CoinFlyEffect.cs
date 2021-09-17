using System;
using BejeweledLivePlus.Misc;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace BejeweledLivePlus.Bej3Graphics
{
	public class CoinFlyEffect : Effect
	{
		public new static void initPool()
		{
			CoinFlyEffect.thePool_ = new SimpleObjectPool(512, typeof(CoinFlyEffect));
		}

		public static CoinFlyEffect alloc(Point theFromCoord, Point theToCoord)
		{
			return CoinFlyEffect.alloc(theFromCoord, theToCoord, 1f);
		}

		public static CoinFlyEffect alloc(Point theFromCoord, Point theToCoord, float theScale)
		{
			CoinFlyEffect coinFlyEffect = (CoinFlyEffect)CoinFlyEffect.thePool_.alloc();
			coinFlyEffect.init(theFromCoord, theToCoord, theScale);
			return coinFlyEffect;
		}

		public override void release()
		{
			this.Dispose();
			CoinFlyEffect.thePool_.release(this);
		}

		public CoinFlyEffect()
			: base(Effect.Type.TYPE_CUSTOMCLASS)
		{
		}

		public void init(Point theFromCoord, Point theToCoord, float theScale)
		{
			base.init(Effect.Type.TYPE_CUSTOMCLASS);
			this.mDAlpha = 0f;
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eEFFECTS_TRANS_PCT_COIN_FLY, this.mTransPct);
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eEFFECTS_CURVED_SCALE_COIN_FLY, this.mCurvedScale, this.mTransPct);
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eEFFECTS_SINK_PCT_COIN_FLY, this.mSinkPct, this.mTransPct);
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eEFFECTS_CURVED_ALPHA_COIN_FLY, this.mCurvedAlpha, this.mTransPct);
			this.mX = (this.mOrigX = (float)theFromCoord.mX);
			this.mY = (this.mOrigY = (float)theFromCoord.mY);
			this.mToX = theToCoord.mX;
			this.mToY = theToCoord.mY;
			this.mRotPct = 0f;
			this.mScale = theScale;
		}

		public CoinFlyEffect(int theCounter, Piece thePiece)
			: base(Effect.Type.TYPE_CUSTOMCLASS)
		{
			this.mDAlpha = 0f;
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eEFFECTS_TRANS_PCT_COIN_FLY, this.mTransPct);
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eEFFECTS_CURVED_SCALE_COIN_FLY, this.mCurvedScale, this.mTransPct);
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eEFFECTS_SINK_PCT_COIN_FLY, this.mSinkPct, this.mTransPct);
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eEFFECTS_CURVED_ALPHA_COIN_FLY, this.mCurvedAlpha, this.mTransPct);
			this.mToX = 240;
			this.mToY = 570;
			this.mScale = 1f;
			if (thePiece == null)
			{
				this.mX = (this.mOrigX = 240f);
				this.mY = (this.mOrigY = 260f);
				this.mRotPct = 0f;
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eEFFECTS_SINK_PCT_COIN_FLY_NULL, this.mSinkPct, this.mTransPct);
				return;
			}
			this.mX = (this.mOrigX = thePiece.CX());
			this.mY = (this.mOrigY = thePiece.CY());
			this.mRotPct = thePiece.mRotPct;
		}

		public override void Update()
		{
			base.Update();
			this.mX = this.mOrigX - (float)this.mTransPct * (this.mOrigX - (float)this.mToX);
			this.mY = this.mOrigY - (float)this.mTransPct * (this.mOrigY - (float)this.mToY);
			this.mRotPct += GlobalMembers.M(0.04f);
			if (this.mRotPct >= 1f)
			{
				this.mRotPct -= 1f;
			}
			this.mTransPct.IncInVal();
			this.mSinkPct.IncInVal();
			if (this.mTransPct.HasBeenTriggered())
			{
				this.mDeleteMe = true;
				this.mFXManager.mBoard.DepositCoin();
			}
		}

		public override void Draw(Graphics g)
		{
			Transform transform = new Transform();
			transform.Scale((float)this.mCurvedScale * this.mScale, (float)this.mCurvedScale * this.mScale);
			int frame = Math.Min((int)(this.mRotPct * 20f), this.mImage.mNumCols * this.mImage.mNumRows - 1);
			g.PushState();
			g.SetColorizeImages(true);
			int num = (int)(96.0 * this.mCurvedAlpha);
			g.SetDrawMode(Graphics.DrawMode.Additive);
			g.SetColor(new Color(num, num, num));
			g.SetDrawMode(Graphics.DrawMode.Normal);
			g.SetColor(Color.White);
			g.mColor.mAlpha = (int)(255.0 * this.mCurvedAlpha);
			if (GlobalMembers.gApp.mBoard != null)
			{
				g.mColor.mAlpha = (int)GlobalMembers.gApp.mBoard.GetAlpha();
			}
			g.SetDrawMode(Graphics.DrawMode.Normal);
			g.DrawImageTransform(this.mImage, transform, GlobalMembers.IMGSRCRECT(this.mImage, frame), GlobalMembers.S(this.mX), GlobalMembers.S(this.mY) + (float)((double)GlobalMembers.MS(150) * this.mSinkPct));
			g.PopState();
		}

		private static SimpleObjectPool thePool_;

		public float mRotPct;

		public float mOrigX;

		public float mOrigY;

		public int mToX;

		public int mToY;

		public CurvedVal mTransPct = new CurvedVal();

		public CurvedVal mSinkPct = new CurvedVal();

		public new float mScale;

		public new Image mImage;
	}
}
