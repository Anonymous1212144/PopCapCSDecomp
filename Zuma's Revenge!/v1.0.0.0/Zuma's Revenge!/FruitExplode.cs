using System;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	public class FruitExplode
	{
		public FruitExplode(Board board)
		{
			this.mBoard = board;
			this.mAnim = null;
			this.Reset();
		}

		public virtual void Dispose()
		{
		}

		public void Reset()
		{
			this.mDone = false;
			if (this.mBoard.mLevel == null || this.mBoard.mCurTreasure == null)
			{
				return;
			}
			switch (this.mBoard.mLevel.mZone)
			{
			case 1:
				this.mAnim = Res.GetPopAnimByID(ResID.POPANIM_NONRESIZE_PINEAPPLEMUSH);
				break;
			case 2:
				this.mAnim = Res.GetPopAnimByID(ResID.POPANIM_NONRESIZE_BANANAMUSH);
				break;
			case 3:
				this.mAnim = Res.GetPopAnimByID(ResID.POPANIM_NONRESIZE_COCOAMUSH);
				break;
			case 4:
				this.mAnim = Res.GetPopAnimByID(ResID.POPANIM_NONRESIZE_MANGOMUSH);
				break;
			case 5:
				this.mAnim = Res.GetPopAnimByID(ResID.POPANIM_NONRESIZE_COCONUTMUSH);
				break;
			case 6:
			case 7:
				this.mAnim = Res.GetPopAnimByID(ResID.POPANIM_NONRESIZE_ACORNMUSH);
				break;
			default:
				this.mAnim = null;
				break;
			}
			this.mAnim.Play("Main");
			int num = (int)Common._S((float)this.mBoard.mCurTreasure.x + ModVal.M(-130f));
			int num2 = (int)Common._S((float)this.mBoard.mCurTreasure.y + ModVal.M(-120f));
			this.mGlobalTranform.Reset();
			this.mGlobalTranform.Translate((float)num, (float)num2);
			this.mAnim.SetTransform(this.mGlobalTranform.GetMatrix());
		}

		public void Update()
		{
			if (this.mDone || this.mAnim == null || this.mBoard.mCurTreasure == null)
			{
				return;
			}
			int num = (int)Common._S((float)this.mBoard.mCurTreasure.x + ModVal.M(-130f));
			int num2 = (int)Common._S((float)this.mBoard.mCurTreasure.y + ModVal.M(-120f));
			this.mGlobalTranform.Reset();
			this.mGlobalTranform.Translate((float)num, (float)num2);
			this.mAnim.SetTransform(this.mGlobalTranform.GetMatrix());
			this.mAnim.Update();
			if (!this.mAnim.IsActive() || this.mAnim.mMainSpriteInst.mFrameNum >= (float)(this.mAnim.mMainSpriteInst.mDef.mFrames.Count - 1))
			{
				this.mDone = true;
			}
		}

		public void Draw(Graphics g)
		{
			if (this.mAnim != null)
			{
				this.mAnim.Draw(g);
			}
		}

		protected PopAnim mAnim;

		protected Board mBoard;

		protected Transform mGlobalTranform = new Transform();

		public bool mDone;
	}
}
