using System;
using System.Collections.Generic;
using Sexy;

namespace BejeweledLIVE
{
	public class FancySmallButton : FancyPodButton
	{
		public static void PreAllocateMemory()
		{
			List<FancySmallButton> list = new List<FancySmallButton>();
			for (int i = 0; i < 30; i++)
			{
				list.Add(FancySmallButton.GetNewFancySmallButton(0, 0, "", null));
			}
			for (int j = 0; j < list.Count; j++)
			{
				list[j].PrepareForReuse();
			}
		}

		public static FancySmallButton GetNewFancySmallButton(int id, int numButtons, int buttonNum, string label, ButtonListener listener)
		{
			if (FancySmallButton.unusedObjects.Count > 0)
			{
				FancySmallButton fancySmallButton = FancySmallButton.unusedObjects.Pop();
				fancySmallButton.Reset(id, numButtons, buttonNum, label, listener);
				return fancySmallButton;
			}
			return new FancySmallButton(id, numButtons, buttonNum, label, listener);
		}

		public static FancySmallButton GetNewFancySmallButton(int id, int colour, string label, ButtonListener listener)
		{
			if (FancySmallButton.unusedObjects.Count > 0)
			{
				FancySmallButton fancySmallButton = FancySmallButton.unusedObjects.Pop();
				fancySmallButton.Reset(id, colour, label, listener);
				return fancySmallButton;
			}
			return new FancySmallButton(id, colour, label, listener);
		}

		protected override void Reset(int id, int numButtons, int buttonNum, string label, ButtonListener listener)
		{
			base.Reset(id, numButtons, buttonNum, label, listener);
			this.SetFont(Resources.FONT_BUTTON);
			this.mLabel = label;
			this.mButtonImage = AtlasResources.IMAGE_PILL_SMALL_CENTER1;
			this.mRingImage = AtlasResources.IMAGE_PILL_SMALL;
			this.mOverlayImage = AtlasResources.IMAGE_PILL_SMALL_CENTER2;
			this.mAdditiveImage = AtlasResources.IMAGE_PILL_SMALL_ADDITIVE;
			this.mNormalRect = this.mButtonImage.GetCelRect(0);
			this.mWidth = base.DefaultWidth();
			this.mHeight = base.DefaultHeight();
			this.ComputeDrawFrames();
			this.SetFont(Resources.FONT_BUTTON);
			this.mIsFancy = false;
		}

		protected void Reset(int id, int colour, string label, ButtonListener listener)
		{
			this.Reset(id, 0, 0, label, listener);
		}

		public void PrepareForReuse()
		{
			FancySmallButton.unusedObjects.Push(this);
		}

		private FancySmallButton(int id, int colour, string label, ButtonListener listener)
			: this(id, 0, 0, label, listener)
		{
		}

		private FancySmallButton(int id, int numButtons, int buttonNum, string label, ButtonListener listener)
			: base(id, numButtons, buttonNum, label, listener)
		{
			this.Reset(id, numButtons, buttonNum, label, listener);
		}

		public override void Dispose()
		{
			base.Dispose();
			this.PrepareForReuse();
		}

		private static Stack<FancySmallButton> unusedObjects = new Stack<FancySmallButton>();
	}
}
