using System;
using BejeweledLivePlus.Misc;
using BejeweledLivePlus.Widget;
using SexyFramework;
using SexyFramework.Graphics;

namespace BejeweledLivePlus.Bej3Graphics
{
	public class TextNotifyEffect : Effect
	{
		public new static void initPool()
		{
			TextNotifyEffect.thePool_ = new SimpleObjectPool(512, typeof(TextNotifyEffect));
		}

		public new static TextNotifyEffect alloc()
		{
			TextNotifyEffect textNotifyEffect = (TextNotifyEffect)TextNotifyEffect.thePool_.alloc();
			textNotifyEffect.init();
			return textNotifyEffect;
		}

		public override void release()
		{
			this.Dispose();
			TextNotifyEffect.thePool_.release(this);
		}

		public TextNotifyEffect()
			: base(Effect.Type.TYPE_CUSTOMCLASS)
		{
		}

		public void init()
		{
			base.init(Effect.Type.TYPE_CUSTOMCLASS);
			this.mUpdateCnt = 0;
			this.mDuration = GlobalMembers.M(200);
			this.mFont = GlobalMembersResources.FONT_HUGE;
			this.mTexture = null;
			this.mDAlpha = 0f;
			this.mIgnoreTexture = false;
		}

		public override void Dispose()
		{
			if (this.mTexture != null)
			{
				this.mTexture.Dispose();
				this.mTexture = null;
			}
			base.Dispose();
		}

		public override void Draw(Graphics g)
		{
			if (g.mPushedColorVector.Count > 0)
			{
				g.PopColorMult();
			}
			Color color = g.GetColor();
			g.SetScale(this.mScale, this.mScale, 0f, 0f);
			g.SetColor(this.mColor);
			g.DrawString(this.mText, (int)(GlobalMembers.S(this.mX) - (float)g.StringWidth(this.mText) * this.mScale / 2f), (int)((GlobalMembers.S(this.mY) + (float)(g.GetFont().GetAscent() / 2)) / this.mScale));
			g.SetColor(color);
			g.SetScale(1f, 1f, 0f, 0f);
		}

		public override void Update()
		{
			if (this.mDelay > 0f)
			{
				this.mDelay -= 1f;
				return;
			}
			this.mUpdateCnt++;
			if (this.mUpdateCnt < 0)
			{
				return;
			}
			if (!this.mIgnoreTexture && this.mTexture == null)
			{
				this.mTexture = new DeviceImage();
				this.mTexture.AddImageFlags(16U);
				this.mTexture.Create(this.mFont.StringWidth(this.mText), this.mFont.GetLineSpacing());
				this.mTexture.mHasAlpha = true;
				this.mTexture.mHasTrans = true;
				Graphics graphics = new Graphics(this.mTexture);
				graphics.Get3D().ClearColorBuffer(new Color(0, 0));
				graphics.SetColor(new Color(-1));
				graphics.SetFont(this.mFont);
				Utils.SetFontLayerColor((ImageFont)this.mFont, 0, Bej3Widget.COLOR_INGAME_ANNOUNCEMENT);
				Utils.SetFontLayerColor((ImageFont)this.mFont, 1, Color.White);
				graphics.WriteString(this.mText, this.mTexture.GetWidth() / 2, this.mFont.GetAscent());
				return;
			}
			if (this.mUpdateCnt >= this.mDuration)
			{
				this.mDeleteMe = true;
			}
		}

		private static SimpleObjectPool thePool_;

		public string mText = string.Empty;

		public int mUpdateCnt;

		public int mDuration;

		public Font mFont;

		public DeviceImage mTexture;

		public bool mIgnoreTexture;

		private CurvedVal Draw_cvScaleIn = new CurvedVal(GlobalMembers.MP("b+0,1.3,0,0.2,#6g<     8~###    ii###"));

		private CurvedVal Draw_cvScaleOut = new CurvedVal(GlobalMembers.MP("b+0,1,0,0.2,~###         ~#>Hu"));
	}
}
