using System;
using System.Collections.Generic;
using SexyFramework.Graphics;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	public class ChallengeMenuScrollContainer : Widget
	{
		public ChallengeMenuScrollContainer(ChallengeMenu aChallengeMenu)
		{
			this.mChallengeMenu = aChallengeMenu;
			this.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES);
			this.IMAGE_UI_CHALLENGESCREEN_DIVIDER = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_DIVIDER);
			this.mTableOfContents = new TableOfContents(this.mChallengeMenu);
			this.mTableOfContents.Init();
			this.mTableOfContents.Resize(0, 0, this.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES.GetWidth(), this.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES.GetHeight());
			this.AddWidget(this.mTableOfContents);
			int num = 4095;
			int num2 = this.mTableOfContents.mWidth;
			int num3 = 0;
			for (int i = 0; i < GlobalChallenge.NUM_CHALLENGE_ZONES; i++)
			{
				ZoneFrame zoneFrame = new ZoneFrame(this.mChallengeMenu, num3++, num);
				this.mZoneFrames.Add(zoneFrame);
				num += num;
				this.mZoneFrames[i].Move(num2, 0);
				num2 += this.mZoneFrames[i].mWidth;
				this.AddWidget(this.mZoneFrames[i]);
			}
			this.Resize(0, 0, this.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES.GetWidth() * (this.mZoneFrames.Count + 1), this.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES.GetHeight());
		}

		public override void Dispose()
		{
			for (int i = 0; i < this.mZoneFrames.Count; i++)
			{
				this.RemoveWidget(this.mZoneFrames[i]);
				if (this.mZoneFrames[i] != null)
				{
					this.mZoneFrames[i].Dispose();
				}
				this.mZoneFrames[i] = null;
			}
			this.mZoneFrames.Clear();
			this.RemoveWidget(this.mTableOfContents);
			if (this.mTableOfContents != null)
			{
				this.mTableOfContents.Dispose();
			}
			this.mTableOfContents = null;
		}

		public void RehupChallengeButtons()
		{
			if (this.mZoneFrames.Count != 0)
			{
				for (int i = 0; i < this.mZoneFrames.Count; i++)
				{
					if (this.mZoneFrames[i] != null)
					{
						this.mZoneFrames[i].RehupChallengeButtons();
					}
				}
			}
		}

		public override void Draw(Graphics g)
		{
			int num = this.NumPages() - 1;
			int num2 = this.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES.GetWidth() + GameApp.gApp.GetScreenRect().mX / 2;
			int theY = (this.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES.GetHeight() - this.IMAGE_UI_CHALLENGESCREEN_DIVIDER.GetHeight()) / 2;
			for (int i = 0; i < num; i++)
			{
				g.DrawImage(this.IMAGE_UI_CHALLENGESCREEN_DIVIDER, num2 - this.IMAGE_UI_CHALLENGESCREEN_DIVIDER.GetWidth() / 2, theY);
				num2 += this.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES.GetWidth();
			}
		}

		public int NumPages()
		{
			return this.mZoneFrames.Count + 1;
		}

		public void AwardMedal(int theZone, bool isAce)
		{
			this.mTableOfContents.AwardMedal(theZone, isAce);
		}

		public void PreloadButtonImage(int theZone)
		{
			ZoneFrame zoneFrame = this.mZoneFrames[theZone];
			if (zoneFrame != null)
			{
				zoneFrame.PreLoadButtonsImage();
			}
		}

		private ChallengeMenu mChallengeMenu;

		private List<ZoneFrame> mZoneFrames = new List<ZoneFrame>();

		private Image IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES;

		private Image IMAGE_UI_CHALLENGESCREEN_DIVIDER;

		private TableOfContents mTableOfContents;
	}
}
