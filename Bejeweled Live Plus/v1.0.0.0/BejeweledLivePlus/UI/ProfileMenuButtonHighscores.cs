using System;
using System.Collections.Generic;
using BejeweledLivePlus.Widget;
using SexyFramework;
using SexyFramework.Misc;

namespace BejeweledLivePlus.UI
{
	public class ProfileMenuButtonHighscores : ProfileMenuButton
	{
		public ProfileMenuButtonHighscores(Bej3ButtonListener theListener)
			: base(6, theListener, GlobalMembers._ID("TOP SCORES", 3433))
		{
			this.mCurrentScore = -1;
			for (int i = 0; i < 2; i++)
			{
				this.mHighscoresHeading[i] = new Label(GlobalMembersResources.FONT_DIALOG, Label_Alignment_Horizontal.LABEL_ALIGNMENT_HORIZONTAL_LEFT);
				this.mHighscoresHeading[i].Resize(ConstantsWP.PROFILEMENU_BUTTON_MESSAGE_X_1, ConstantsWP.PROFILEMENU_BUTTON_MESSAGE_Y_1, 0, 0);
				this.AddWidget(this.mHighscoresHeading[i]);
				this.mHighscores[i] = new Label(GlobalMembersResources.FONT_DIALOG, Label_Alignment_Horizontal.LABEL_ALIGNMENT_HORIZONTAL_RIGHT);
				this.mHighscores[i].Resize(ConstantsWP.PROFILEMENU_BUTTON_MESSAGE_X_2, ConstantsWP.PROFILEMENU_BUTTON_MESSAGE_Y_1, 0, 0);
				this.mHighscores[i].SetLayerColor(0, Bej3Widget.COLOR_DIALOG_4_FILL);
				this.AddWidget(this.mHighscores[i]);
				this.mHighscoresHeadingToday[i] = new Label(GlobalMembersResources.FONT_DIALOG, Label_Alignment_Horizontal.LABEL_ALIGNMENT_HORIZONTAL_LEFT);
				this.mHighscoresHeadingToday[i].Resize(ConstantsWP.PROFILEMENU_BUTTON_MESSAGE_X_1, ConstantsWP.PROFILEMENU_BUTTON_MESSAGE_Y_2, 0, 0);
				this.mHighscoresHeadingToday[i].SetText(GlobalMembers._ID("", 2050 + i));
				this.AddWidget(this.mHighscoresHeadingToday[i]);
				this.mHighscoresToday[i] = new Label(GlobalMembersResources.FONT_DIALOG, Label_Alignment_Horizontal.LABEL_ALIGNMENT_HORIZONTAL_RIGHT);
				this.mHighscoresToday[i].Resize(ConstantsWP.PROFILEMENU_BUTTON_MESSAGE_X_2, ConstantsWP.PROFILEMENU_BUTTON_MESSAGE_Y_2, 0, 0);
				this.mHighscoresToday[i].SetLayerColor(0, Bej3Widget.COLOR_DIALOG_4_FILL);
				this.AddWidget(this.mHighscoresToday[i]);
			}
			base.MakeChildrenTouchInvisible();
			this.SetNextHighScore();
		}

		public override void Update()
		{
			base.Update();
		}

		public void SetNextHighScore()
		{
			int num = this.mCurrentScore;
			this.mCurrentScore = (this.mCurrentScore + 1) % 2;
			if (this.mRelevantHighscores.Count == 0)
			{
				this.mNoHighscoreMessageLabel.FadeIn();
				num = (this.mCurrentScore = -1);
			}
			else
			{
				this.mNoHighscoreMessageLabel.FadeOut();
				this.mCurrentScore = (this.mCurrentScore + 1) % this.mRelevantHighscores.Count;
			}
			for (int i = 0; i < 2; i++)
			{
				if (i == this.mCurrentScore)
				{
					this.mHighscores[i].FadeIn();
					this.mHighscoresHeading[i].FadeIn();
					this.mHighscoresToday[i].FadeIn();
					this.mHighscoresHeadingToday[i].FadeIn();
				}
				else if (i == num)
				{
					this.mHighscores[i].FadeOut();
					this.mHighscoresHeading[i].FadeOut();
					this.mHighscoresToday[i].FadeOut();
					this.mHighscoresHeadingToday[i].FadeOut();
				}
				else
				{
					this.mHighscores[i].FadeOut();
					this.mHighscoresHeading[i].FadeOut();
					this.mHighscoresToday[i].FadeOut();
					this.mHighscoresHeadingToday[i].FadeOut();
					this.mHighscores[i].mAlpha = 0f;
					this.mHighscoresHeading[i].mAlpha = 0f;
					this.mHighscoresToday[i].mAlpha = 0f;
					this.mHighscoresHeadingToday[i].mAlpha = 0f;
				}
			}
		}

		public override void LinkUpAssets()
		{
			base.LinkUpAssets();
			this.mNoHighscoreMessageLabel.SetTextBlock(new Rect(ConstantsWP.PROFILEMENU_BUTTON_EXTRA_MESSAGE_X, ConstantsWP.PROFILEMENU_BUTTON_EXTRA_MESSAGE_Y, ConstantsWP.PROFILEMENU_BUTTON_EXTRA_MESSAGE_WIDTH, 0), true);
			this.mHighscores[0].SetText(Common.CommaSeperate(GlobalMembers.gApp.mProfile.GetModeHighScore(GameMode.MODE_CLASSIC)));
			this.mHighscores[1].SetText(Common.CommaSeperate(GlobalMembers.gApp.mProfile.GetModeHighScore(GameMode.MODE_DIAMOND_MINE)));
			this.mHighscoresToday[0].SetText(Common.CommaSeperate(GlobalMembers.gApp.mProfile.GetModeHighScoreToday(GameMode.MODE_CLASSIC)));
			this.mHighscoresToday[1].SetText(Common.CommaSeperate(GlobalMembers.gApp.mProfile.GetModeHighScoreToday(GameMode.MODE_DIAMOND_MINE)));
			this.mRelevantHighscores.Clear();
			if (GlobalMembers.gApp.mProfile.GetModeHighScore(GameMode.MODE_CLASSIC) > 0)
			{
				this.mRelevantHighscores.Add(0);
			}
			if (GlobalMembers.gApp.mProfile.GetModeHighScore(GameMode.MODE_DIAMOND_MINE) > 0)
			{
				this.mRelevantHighscores.Add(1);
			}
			if (this.mRelevantHighscores.Count > 0)
			{
				this.mNoHighscoreMessageLabel.mAlpha = 0f;
			}
		}

		private int mCurrentScore;

		private Label[] mHighscores = new Label[2];

		private Label[] mHighscoresToday = new Label[2];

		private Label[] mHighscoresHeading = new Label[2];

		private Label[] mHighscoresHeadingToday = new Label[2];

		private List<int> mRelevantHighscores = new List<int>();

		private Label mNoHighscoreMessageLabel;

		private enum ProfileMenuScores
		{
			SCORES_CLASSIC,
			SCORES_DIAMOND_MINE,
			SCORES_MAX
		}
	}
}
