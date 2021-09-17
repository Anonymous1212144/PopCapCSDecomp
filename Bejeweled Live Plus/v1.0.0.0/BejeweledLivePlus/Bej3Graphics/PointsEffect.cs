using System;
using BejeweledLivePlus.Misc;
using BejeweledLivePlus.Widget;
using SexyFramework;
using SexyFramework.Graphics;

namespace BejeweledLivePlus.Bej3Graphics
{
	public class PointsEffect : Effect
	{
		public new static void initPool()
		{
			PointsEffect.thePool_ = new SimpleObjectPool(512, typeof(PointsEffect));
		}

		public static PointsEffect alloc(int thePointCount, int thePieceId, bool theShowNuggetText)
		{
			PointsEffect pointsEffect = (PointsEffect)PointsEffect.thePool_.alloc();
			pointsEffect.init(thePointCount, thePieceId, theShowNuggetText);
			return pointsEffect;
		}

		public override void release()
		{
			this.Dispose();
			PointsEffect.thePool_.release(this);
		}

		public PointsEffect()
			: base(Effect.Type.TYPE_CUSTOMCLASS)
		{
		}

		public void init(int thePointCount, int thePieceId, bool theShowNuggetText)
		{
			base.init(Effect.Type.TYPE_CUSTOMCLASS);
			this.mShowNuggetText = theShowNuggetText;
			this.mCount = thePointCount;
			this.mPieceId = thePieceId;
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eEFFECTS_CURVED_ALPHA_DIG_GOAL_POINTS, this.mCurvedAlpha);
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eEFFECTS_CV_Y_DIG_GOAL_POINTS, this.mCvY);
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eEFFECTS_CURVED_SCALE_DIG_GOAL_POINTS, this.mCurvedScale);
		}

		public override void Draw(Graphics g)
		{
			g.PushState();
			g.SetScale((float)(this.mCurvedScale * (double)ConstantsWP.DIG_BOARD_FLOATING_SCORE_SCALE), (float)(this.mCurvedScale * (double)ConstantsWP.DIG_BOARD_FLOATING_SCORE_SCALE), GlobalMembers.S(this.mX), GlobalMembers.S(this.mY));
			g.SetFont(GlobalMembersResources.FONT_HUGE);
			Utils.SetFontLayerColor((ImageFont)GlobalMembersResources.FONT_HUGE, 0, Bej3Widget.COLOR_DIGBOARD_SCORE_GLOW);
			Utils.SetFontLayerColor((ImageFont)GlobalMembersResources.FONT_HUGE, 1, Bej3Widget.COLOR_DIGBOARD_SCORE_STROKE);
			g.SetColorizeImages(true);
			g.SetColor(Color.FAlpha((float)this.mCurvedAlpha));
			string theString = string.Empty;
			if (this.mShowNuggetText)
			{
				theString = string.Format("+{0} GOLD", SexyFramework.Common.CommaSeperate(this.mCount));
			}
			else
			{
				theString = string.Format("+{0}", SexyFramework.Common.CommaSeperate(this.mCount));
			}
			int num = GlobalMembersResources.FONT_HUGE.StringWidth(theString) / 2;
			int num2 = (int)GlobalMembers.S(this.mX);
			int theX = ((num2 - num <= 0) ? num : ((num2 + num >= GlobalMembers.gApp.mWidth) ? (GlobalMembers.gApp.mWidth - num) : num2));
			int theY = (int)GlobalMembers.S((double)this.mY + this.mCvY);
			g.WriteString(theString, theX, theY);
			g.PopState();
		}

		private static SimpleObjectPool thePool_;

		public bool mShowNuggetText;

		public CurvedVal mCvY = new CurvedVal();

		public int mCount;

		public new int mPieceId;
	}
}
