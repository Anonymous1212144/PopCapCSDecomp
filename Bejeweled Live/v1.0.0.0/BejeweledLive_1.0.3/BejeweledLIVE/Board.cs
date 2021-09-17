using System;
using System.Collections.Generic;
using Sexy;

namespace BejeweledLIVE
{
	public abstract class Board : InterfaceWidget, ButtonListener, DialogListener
	{
		public static float GetRandFloat()
		{
			return (float)(Board.rand.NextDouble() - 0.5);
		}

		public static int Rand()
		{
			return Board.rand.Next();
		}

		public static int Rand(int max)
		{
			return Board.rand.Next(max);
		}

		static Board()
		{
			for (int i = 0; i <= 90; i++)
			{
				Board.GetTimeString(i);
			}
		}

		public static string GetTimeString(int time)
		{
			string text;
			if (!Board.timeLeftStrings.TryGetValue(time, ref text))
			{
				text = string.Format("{0:00}:{1:00}", time / 60, time % 60);
				Board.timeLeftStrings.Add(time, text);
			}
			return text;
		}

		public Board(GameApp theApp)
			: base(theApp)
		{
			this.mFirstUpdate = true;
			this.mBackdropNum = -1;
			this.mNextBackdropNum = -1;
			this.mPreparingLevelImages = false;
			this.mHintPiece = null;
			this.mExternalDraw = false;
			this.mPauseCount = 0;
			this.mVisPauseCount = 0;
			this.mDoubleSpeed = this.mApp.mGameMode == GameMode.MODE_TIMED_SECRET;
			this.mPuzzleStateOverlay = null;
			this.mNoMoreMoves = false;
			this.mInTutorialMode = false;
			this.mTotalGameTime = 0;
			this.mGemsCleared = 0;
			this.mLongestChainReaction = 0;
			this.mHighestScoringMove = 0;
			this.mPoints = 0;
			this.mFallDelay = 180;
			this.mTimedMode = false;
			this.mFlashBarRed = false;
			this.mGemShowPct = 1f;
			this.mPauseFade = 1f;
			this.mShowLevelDelay = 0;
			this.mGoDelay = 300;
			this.mSelectAnimCnt = 0;
			this.mShowingSun = false;
			this.mSunPosition = 0f;
			if (!Board.gTableInitialized)
			{
				for (int i = 0; i < 4096; i++)
				{
					Board.SIN_TAB[i] = (float)Math.Sin((double)i * 3.14159 * 2.0 / 4096.0);
					Board.COS_TAB[i] = (float)Math.Cos((double)i * 3.14159 * 2.0 / 4096.0);
				}
				Board.gTableInitialized = true;
			}
		}

		public override void Dispose()
		{
			base.Dispose();
		}

		public static bool HasSavedGame()
		{
			return GlobalStaticVars.gSexyAppBase.mGameMode != GameMode.MODE_PUZZLE && GlobalStaticVars.gSexyAppBase.FileExists(Board.GetSavedGameFileName());
		}

		public static string GetSavedGameFileName()
		{
			GameApp gSexyAppBase = GlobalStaticVars.gSexyAppBase;
			return GlobalStaticVars.GetDocumentsDir() + "/" + Board.aFileName[(int)gSexyAppBase.mGameMode];
		}

		public bool LoadPuzzleThumb(string theFileName, int[,] theBoard)
		{
			return true;
		}

		public void GetBoardCenter(out int theX, out int theY)
		{
			int num = this.GetColX(7) - this.GetColX(0) + GameConstants.GEM_WIDTH;
			theX = this.GetColX(0) + num / 2;
			int num2 = this.GetRowY(7) - this.GetRowY(0) + GameConstants.GEM_HEIGHT;
			theY = this.GetRowY(0) + num2 / 2;
		}

		public virtual void Pause(bool visible)
		{
			this.Pause(visible, false);
		}

		public virtual void DrawBoard(Graphics g, bool noBack)
		{
			this.DrawBoard(g, noBack, false);
		}

		public virtual void DrawBoard(Graphics g)
		{
			this.DrawBoard(g, false, false);
		}

		public override void InterfaceTransactionBegin(int uiState, int uiStateParam, int uiStateLayout)
		{
			if (1 == this.mInterfaceStateParam && -1 == uiStateParam)
			{
				this.Hide();
				this.Pause(true);
			}
			else if (-1 == this.mInterfaceStateParam && 1 == uiStateParam)
			{
				this.Show();
				this.Unpause(true);
			}
			base.InterfaceTransactionBegin(uiState, uiStateParam, uiStateLayout);
		}

		public override void InterfaceTransactionEnd(int uiState)
		{
			base.InterfaceTransactionEnd(uiState);
		}

		public override void TransitionInToState(int uiState, int uiStateParam, int uiStateLayout)
		{
			base.TransitionInToState(uiState, uiStateParam, uiStateLayout);
			this.SetVisible(false);
			bool useBoardBackdrop = this.mApp.WidgetIsInForState(4, this.mInterfaceState);
			this.mApp.mEffectOverlay.DoPortal(useBoardBackdrop);
		}

		public override void TransitionOutToState(int uiState, int uiStateParam, int uiStateLayout)
		{
			base.TransitionOutToState(uiState, uiStateParam, uiStateLayout);
			this.SetVisible(false);
		}

		protected virtual void Hide()
		{
			this.mInterfaceOffset.SetOutRange(0.0, (double)Constants.mConstants.OutValueX);
			this.mInterfaceOffset.SetInVal(0.0);
			this.mInterfaceOffset.SetMode(0);
			this.mInterfaceOffset.SetRamp(2);
			this.mSHOWSTATE = Board.SHOWSTATE.SHOWSTATE_HIDING;
		}

		protected void StartPrepareLevelImages()
		{
			this.mPreparingLevelImages = true;
			GameConstants.LoadLevelBackdrops(this.mBackdropNum + 1, this.mNextBackdropNum + 1);
		}

		protected void ProcessLevelImages()
		{
		}

		public int GetColX(int theCol)
		{
			return Constants.mConstants.Board_Board_X + theCol * GameConstants.GEM_WIDTH;
		}

		public int GetRowY(int theRow)
		{
			return Constants.mConstants.Board_Board_Y + theRow * GameConstants.GEM_HEIGHT;
		}

		protected int GetPanPosition(int theX)
		{
			int num = this.GetColX(7) - this.GetColX(0) + GameConstants.GEM_WIDTH;
			int num2 = theX - (this.GetColX(0) + num / 2);
			double num3 = (double)num2 / (double)num * 2.0;
			return (int)(num3 * 80.0);
		}

		protected bool PointInPiece(Piece thePiece, int x, int y)
		{
			return (float)x >= thePiece.mX && (float)y >= thePiece.mY && (float)x < thePiece.mX + (float)GameConstants.GEM_WIDTH && (float)y < thePiece.mY + (float)GameConstants.GEM_HEIGHT;
		}

		protected bool PointInPiece(Piece thePiece, int x, int y, int theFuzzFactor)
		{
			return this.PointInPiece(thePiece, x, y) || this.PointInPiece(thePiece, x - theFuzzFactor, y - theFuzzFactor) || this.PointInPiece(thePiece, x, y - theFuzzFactor) || this.PointInPiece(thePiece, x + theFuzzFactor, y - theFuzzFactor) || this.PointInPiece(thePiece, x + theFuzzFactor, y) || this.PointInPiece(thePiece, x + theFuzzFactor, y + theFuzzFactor) || this.PointInPiece(thePiece, x, y + theFuzzFactor) || this.PointInPiece(thePiece, x - theFuzzFactor, y + theFuzzFactor) || this.PointInPiece(thePiece, x - theFuzzFactor, y);
		}

		protected virtual void DrawWidgetTo(Graphics g, Widget theWidget)
		{
			g.Translate(theWidget.mX, theWidget.mY);
			theWidget.Draw(g);
			g.Translate(-theWidget.mX, -theWidget.mY);
		}

		protected virtual void DrawBackdrop(Graphics g)
		{
			if (this.mState == Board.BoardState.STATE_GAME_OVER_DISPLAY)
			{
				return;
			}
			if (this.mApp.IsLandscape())
			{
				g.DrawImage(GameConstants.BGH_TEXTURE, 0f, 0f);
				return;
			}
			g.DrawImage(GameConstants.BGV_TEXTURE, 0f, 0f);
		}

		public abstract bool IsInProgress();

		public abstract void Unpause(bool visible);

		public abstract void Pause(bool visible, bool immediate);

		public abstract void DeleteSavedGame();

		public abstract void DeleteAllSavedGames();

		public abstract bool LoadGame();

		public abstract bool SaveGame();

		public abstract void Show();

		public abstract void HideNow();

		public abstract void ShowNow();

		public abstract void DrawBoard(Graphics g, bool noBack, bool noFront);

		protected abstract void DoUpdate();

		public abstract void ButtonPress(int theId);

		public virtual void ButtonPress(int theId, int x)
		{
			this.ButtonPress(theId);
		}

		public abstract void ButtonDepress(int theId);

		public abstract void DialogButtonPress(int theDialogId, int theButtonId);

		public abstract void DialogButtonDepress(int dialogId, int buttonId);

		public abstract void ResizeButtons();

		public abstract int GetColAt(int theX);

		public abstract int GetRowAt(int theY);

		protected abstract void UpdateFalling();

		protected abstract void UpdateSwapping();

		protected abstract void UpdateLightning();

		public virtual void LeftTrialmode()
		{
		}

		public void ButtonDownTick(int theId)
		{
		}

		public void ButtonMouseEnter(int theId)
		{
		}

		public void ButtonMouseLeave(int theId)
		{
		}

		public void ButtonMouseMove(int theId, int theX, int theY)
		{
		}

		protected const int STANDARD_GO_DELAY = 300;

		public const int NUM_COLS = 8;

		public const int NUM_ROWS = 8;

		public const int SAVEGAME_VERSION = 4;

		public const int PUZZLE_VERSION = 2;

		public const int SOLUTION_VERSION = 2;

		public const int NUM_WARP_COLS = 16;

		public const int NUM_WARP_ROWS = 16;

		public const int HYPERWARP_ROWS = 7;

		public const int HYPERWARP_COLS = 7;

		public const int HINT_LIFE = 290;

		private static Random rand = new Random(DateTime.Now.Millisecond);

		private static Dictionary<int, string> timeLeftStrings = new Dictionary<int, string>();

		public static float[] COS_TAB = new float[4096];

		public static float[] SIN_TAB = new float[4096];

		public static bool gTableInitialized = false;

		public bool mExternalDraw;

		public int mPauseCount;

		public bool mDoubleSpeed;

		public int mVisPauseCount;

		public CPieceBej2 mHintPiece;

		public Board.SHOWSTATE mSHOWSTATE;

		public PuzzleStateOverlay mPuzzleStateOverlay;

		public Board.BoardState mState;

		public bool mNoMoreMoves;

		public bool mInTutorialMode;

		public float mHintAngle;

		public int mTotalGameTime;

		public int mGemsCleared;

		public int mLongestChainReaction;

		public int mHighestScoringMove;

		public int mPoints;

		public int mFallDelay;

		public bool mTimedMode;

		public int mNoMoveCount;

		protected bool mPreparingLevelImages;

		protected int mBackdropNum;

		protected int mNextBackdropNum;

		protected bool mFirstUpdate;

		protected CurvedVal mInterfaceOffset = CurvedVal.GetNewCurvedVal();

		protected bool mFlashBarRed;

		protected float mGemShowPct;

		protected float mPauseFade;

		protected int mShowLevelDelay;

		protected int mGoDelay;

		protected int mSelectAnimCnt;

		protected bool mShowingSun;

		protected float mSunPosition;

		protected static string[] aFileName = new string[] { "normal.sav", "timed.sav", "puzzle.sav", "endless.sav", "s_normal.sav", "s_timed.sav", "s_puzzle.sav", "s_endless.sav", "twilight.sav", "blitz.sav" };

		public enum BoardState
		{
			STATE_FALLING,
			STATE_MAKE_MOVE,
			STATE_SWAPPING,
			STATE_SWAP_INVALID,
			STATE_SWAPPING_BACK,
			STATE_CLEARING,
			STATE_GAME_OVER_ANIM,
			STATE_GAME_OVER_DISPLAY,
			STATE_LEVEL_COMPLETE,
			STATE_WHIRLPOOL,
			STATE_LEVEL_TRANSITION,
			STATE_BOARD_DROPPING,
			STATE_INTERFACE_RESTORE,
			STATE_LIGHTNING,
			STATE_PUZZLE_COMPLETED,
			STATE_PUZZLE_NO_MOVES,
			STATE_PUZZLE_AT_END,
			STATE_PUZZLE_AT_END_CLOSE,
			STATE_PUZZLE_MODE_DONE,
			STATE_SHOWING_GALAXY_MAP,
			STATE_GALAXY_MAP,
			STATE_HIDING_GALAXY_MAP,
			STATE_BOARD_STILL,
			STATE_GAME_OVER_FALL
		}

		public enum SHOWSTATE
		{
			SHOWSTATE_SHOWING,
			SHOWSTATE_SHOWN,
			SHOWSTATE_HIDING,
			SHOWSTATE_HIDDEN,
			SHOWSTATE_FADING_OUT
		}
	}
}
