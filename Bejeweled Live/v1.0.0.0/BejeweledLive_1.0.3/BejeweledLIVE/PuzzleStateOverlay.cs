using System;
using Microsoft.Xna.Framework;
using Sexy;

namespace BejeweledLIVE
{
	public class PuzzleStateOverlay : Widget, ButtonListener
	{
		protected void GenerateThumbnails()
		{
		}

		public PuzzleStateOverlay(GameApp theApp)
		{
			this.mApp = theApp;
			this.mPodOver = -1;
			this.mPuzzleSelected = -1;
			this.mHasAlpha = true;
			this.mMouseVisible = false;
			for (int i = 0; i < 7; i++)
			{
				this.mPodAlphas[i] = 0f;
				this.mThumbnails[i] = new MemoryImage();
				this.mThumbnails[i].Create(24, 24, PixelFormat.kPixelFormat_RGBA4444);
			}
			this.GenerateThumbnails();
		}

		public override void Dispose()
		{
			for (int i = 0; i < 7; i++)
			{
				this.mThumbnails[i].Dispose();
			}
			base.Dispose();
		}

		public override void AddedToManager(WidgetManager theWidgetManager)
		{
			base.AddedToManager(theWidgetManager);
		}

		public override void RemovedFromManager(WidgetManager theWidgetManager)
		{
			base.RemovedFromManager(theWidgetManager);
		}

		public override void Update()
		{
			for (int i = 0; i < 7; i++)
			{
				float num = ((i == this.mPuzzleSelected) ? 1f : ((i == this.mPodOver) ? 0.5f : 0f));
				if (this.mApp.mBoard.mInTutorialMode)
				{
					num = 0f;
				}
				if (num > this.mPodAlphas[i])
				{
					this.mPodAlphas[i] = Math.Min(this.mPodAlphas[i] + 0.025f, 1f);
				}
				else if (num < this.mPodAlphas[i])
				{
					this.mPodAlphas[i] = Math.Max(this.mPodAlphas[i] - 0.025f, 0f);
				}
			}
			base.Update();
			this.MarkDirty();
		}

		public override void Draw(Graphics g)
		{
			for (int i = 0; i < 7; i++)
			{
				if (this.mPodAlphas[i] > 0f)
				{
					g.SetColorizeImages(true);
					g.SetColor(new Color(255, 255, 255, (int)(255f * this.mPodAlphas[i])));
					g.SetColorizeImages(false);
				}
			}
			for (int i = 0; i < 7; i++)
			{
				g.DrawImage(this.mThumbnails[i], 4 + PuzzleStateOverlay.POD_POSITIONS[i, 0] - 10, 2 + PuzzleStateOverlay.POD_POSITIONS[i, 1] - 10);
			}
		}

		public int GetPodAt(int theX, int theY)
		{
			for (int i = 0; i < 7; i++)
			{
				int num = 4 + PuzzleStateOverlay.POD_POSITIONS[i, 0];
				int num2 = 2 + PuzzleStateOverlay.POD_POSITIONS[i, 1];
				int num3 = theX - num;
				int num4 = theY - num2;
				if (Math.Sqrt((double)((float)(num3 * num3 + num4 * num4))) < 18.0)
				{
					return i;
				}
			}
			return -1;
		}

		public bool MouseOverDome(int theX, int theY)
		{
			return false;
		}

		public void SetPodOver(int thePodNum)
		{
			this.mPodOver = thePodNum;
		}

		public void SelectPod(int thePodNum)
		{
			this.mPuzzleSelected = thePodNum;
		}

		public void PuzzleCompleted()
		{
		}

		public void ButtonPress(int theId)
		{
			this.mApp.PlaySample(Resources.SOUND_CLICK);
		}

		public void ButtonDepress(int theId)
		{
		}

		void ButtonListener.ButtonPress(int theId, int theClickCount)
		{
			this.ButtonPress(theId);
		}

		void ButtonListener.ButtonDownTick(int theId)
		{
		}

		void ButtonListener.ButtonMouseEnter(int theId)
		{
		}

		void ButtonListener.ButtonMouseLeave(int theId)
		{
		}

		void ButtonListener.ButtonMouseMove(int theId, int theX, int theY)
		{
		}

		public static readonly SexyColor[] TYPE_COLORS = new SexyColor[]
		{
			new SexyColor(255, 255, 0),
			new SexyColor(255, 255, 255),
			new SexyColor(140, 140, 255),
			new SexyColor(255, 0, 0),
			new SexyColor(255, 0, 255),
			new SexyColor(255, 173, 45),
			new SexyColor(0, 200, 0),
			new SexyColor(92, 92, 92),
			new SexyColor(192, 192, 192),
			new SexyColor(255, 200, 180)
		};

		private static int[,] POD_POSITIONS = new int[,]
		{
			{ 46, 26 },
			{ 88, 26 },
			{ 25, 64 },
			{ 67, 64 },
			{ 109, 64 },
			{ 46, 102 },
			{ 88, 102 }
		};

		public GameApp mApp;

		public int mPodOver;

		public int mPuzzleSelected;

		public MemoryImage[] mThumbnails = new MemoryImage[7];

		public float[] mPodAlphas = new float[7];
	}
}
