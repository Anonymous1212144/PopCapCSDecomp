using System;
using System.Collections.Generic;
using BejeweledLivePlus.Misc;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace BejeweledLivePlus
{
	public class QuestGoal : IDisposable
	{
		public QuestGoal(QuestBoard theQuestBoard)
		{
			this.mUpdateCnt.value = 0;
			this.mQuestBoard = theQuestBoard;
			this.mWantShowPoints = false;
		}

		public virtual void Dispose()
		{
		}

		public virtual bool AllowSpeedBonus()
		{
			return false;
		}

		public virtual bool AllowNoMoreMoves()
		{
			return false;
		}

		public virtual bool WantColorCombos()
		{
			return false;
		}

		public virtual int GetSidebarTextY()
		{
			return 0;
		}

		public virtual int GetBoardX()
		{
			return 0;
		}

		public virtual int GetBoardY()
		{
			return 0;
		}

		public virtual uint GetRandSeedOverride()
		{
			return this.mQuestBoard.CallBoardGetRandSeedOverride();
		}

		public virtual bool DoEndLevelDialog()
		{
			return false;
		}

		public virtual bool WantDrawTimer()
		{
			return true;
		}

		public virtual bool AllowGameOver()
		{
			return true;
		}

		public virtual void DoUpdate()
		{
		}

		public virtual void GameOverExit()
		{
		}

		public virtual bool SaveGameExtra(Serialiser theBuffer)
		{
			if (!this.ExtraSaveGameInfo())
			{
				return true;
			}
			int chunkBeginLoc = theBuffer.WriteGameChunkHeader(GameChunkId.eChunkQuestGoal);
			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					theBuffer.WriteInt32((int)this.mGridState[i, j]);
				}
			}
			theBuffer.FinalizeGameChunkHeader(chunkBeginLoc);
			return true;
		}

		public virtual void LoadGameExtra(Serialiser theBuffer)
		{
			if (!this.ExtraSaveGameInfo())
			{
				return;
			}
			int num = 0;
			GameChunkHeader header = new GameChunkHeader();
			if (!theBuffer.CheckReadGameChunkHeader(GameChunkId.eChunkQuestGoal, header, out num))
			{
				return;
			}
			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					this.mGridState[i, j] = (uint)theBuffer.ReadInt32();
				}
			}
		}

		public virtual bool ExtraSaveGameInfo()
		{
			return false;
		}

		public virtual void CelDestroyedBySpecial(int theCol, int theRow)
		{
		}

		public virtual void ProcessMatches(List<MatchSet> theMatches, Dictionary<Piece, int> theTallySet, bool fromUpdateSwapping)
		{
		}

		public virtual bool DeletePiece(Piece thePiece)
		{
			return true;
		}

		public virtual void DoHypercube(Piece thePiece, Piece theSwappedPiece)
		{
		}

		public virtual bool CreateMatchPowerup(int theMatchCount, Piece thePiece, Dictionary<Piece, int> thePieceSet)
		{
			return false;
		}

		public virtual void ExamineBoard()
		{
		}

		public virtual int GetLevelPoints()
		{
			return 0;
		}

		public virtual void DoHypergem(Piece thePiece, int theColor)
		{
		}

		public virtual void SwapSucceeded(SwapData theSwapData)
		{
		}

		public virtual void FillInBlanks()
		{
			this.FillInBlanks(true);
		}

		public virtual void FillInBlanks(bool allowCascades)
		{
			this.mQuestBoard.CallBoardFillInBlanks(allowCascades);
		}

		public virtual bool CheckWin()
		{
			return this.mQuestBoard.CallBoardCheckWin();
		}

		public virtual int GetPowerGemThreshold()
		{
			return 0;
		}

		public virtual bool WantsBackground()
		{
			return this.mQuestBoard.CallBoardWantsBackground();
		}

		public virtual int GetUICenterX()
		{
			return this.mQuestBoard.CallBoardGetUICenterX();
		}

		public virtual void SetupBackground()
		{
			this.SetupBackground(0);
		}

		public virtual void SetupBackground(int theDeltaIdx)
		{
			this.mQuestBoard.CallBoardSetupBackground(theDeltaIdx);
		}

		public virtual bool DrawFrame(Graphics g)
		{
			return false;
		}

		public virtual bool WantBottomFrame()
		{
			return this.mQuestBoard.CallBoardWantBottomFrame();
		}

		public virtual bool WantTopFrame()
		{
			return this.mQuestBoard.CallBoardWantTopFrame();
		}

		public virtual bool WantTopLevelBar()
		{
			return this.mQuestBoard.CallBoardWantTopLevelBar();
		}

		public virtual bool WantWarningGlow()
		{
			return this.mQuestBoard.CallBoardWantWarningGlow();
		}

		public virtual int GetTimeDrawX()
		{
			return this.mQuestBoard.CallBoardGetTimeDrawX();
		}

		public virtual bool IsGameSuspended()
		{
			return this.mQuestBoard.CallBoardIsGameSuspended();
		}

		public virtual Rect GetCountdownBarRect()
		{
			return this.mQuestBoard.CallBoardGetCountdownBarRect();
		}

		public virtual void TallyPiece(Piece thePiece, bool thePieceDestroyed)
		{
		}

		public virtual void PieceTallied(Piece thePiece)
		{
		}

		public virtual void DrawPieces(Graphics g, Piece[] pPieces, int numPieces, bool thePostFX)
		{
		}

		public virtual void DrawPiecesPre(Graphics g, bool thePostFX)
		{
		}

		public virtual void DrawPiecesPost(Graphics g, bool thePostFX)
		{
		}

		public virtual void DrawPieceShadow(Graphics g, Piece thePiece)
		{
		}

		public virtual void DrawHypercube(Graphics g, Piece thePiece)
		{
			this.mQuestBoard.CallBoardDrawHypercube(g, thePiece);
		}

		public virtual bool IsHypermixerCancelledBy(Piece thePiece)
		{
			return this.mQuestBoard.CallBoardIsHypermixerCancelledBy(thePiece);
		}

		public virtual bool WantSpecialPiece(List<Piece> thePieceVector)
		{
			return false;
		}

		public virtual bool DropSpecialPiece(List<Piece> thePieceVector)
		{
			return false;
		}

		public virtual void BlanksFilled(bool specialDropped)
		{
		}

		public virtual bool PiecesDropped(List<Piece> thePieceVector)
		{
			return true;
		}

		public virtual void KeyChar(char theChar)
		{
		}

		public virtual bool TriggerSpecial(Piece aPiece)
		{
			return this.TriggerSpecial(aPiece, null);
		}

		public virtual bool TriggerSpecial(Piece aPiece, Piece aSpecialPiece)
		{
			return false;
		}

		public virtual void NewGamePreSetup()
		{
		}

		public virtual void NewGame()
		{
		}

		public virtual void Update()
		{
			if (this.mQuestBoard.IsGamePaused())
			{
				return;
			}
			this.mUpdateCnt.value++;
		}

		public virtual void Draw(Graphics g)
		{
		}

		public virtual void DrawOverlay(Graphics g)
		{
		}

		public virtual void DrawCountPopups(Graphics g)
		{
		}

		public virtual void DrawOverlayPost(Graphics g)
		{
		}

		public virtual void DrawGameElements(Graphics g)
		{
		}

		public virtual void DrawGameElementsPost(Graphics g)
		{
		}

		public virtual void DrawPiecePre(Graphics g, Piece thePiece)
		{
			this.DrawPiecePre(g, thePiece, 1f);
		}

		public virtual void DrawPiecePre(Graphics g, Piece thePiece, float theScale)
		{
		}

		public virtual void DrawPiecePost(Graphics g, Piece thePiece)
		{
			this.DrawPiecePost(g, thePiece, 1f);
		}

		public virtual void DrawPiecePost(Graphics g, Piece thePiece, float theScale)
		{
		}

		public virtual bool DrawScore(Graphics g)
		{
			return false;
		}

		public virtual bool DrawScoreWidget(Graphics g)
		{
			return false;
		}

		public virtual void PieceDestroyedInSwap(Piece thePiece)
		{
		}

		public virtual void DoQuestBonus()
		{
			this.DoQuestBonus(1f);
		}

		public virtual void DoQuestBonus(float iOpt_multiplier)
		{
		}

		public virtual void DoQuestPenalty()
		{
			this.DoQuestPenalty(1f);
		}

		public virtual void DoQuestPenalty(float iOpt_multiplier)
		{
		}

		public virtual bool TrySwap(Piece theSelected, int theSwappedRow, int theSwappedCol, bool forceSwap, bool playerSwapped)
		{
			return this.TrySwap(theSelected, theSwappedRow, theSwappedCol, forceSwap, playerSwapped, false);
		}

		public virtual bool TrySwap(Piece theSelected, int theSwappedRow, int theSwappedCol, bool forceSwap)
		{
			return this.TrySwap(theSelected, theSwappedRow, theSwappedCol, forceSwap, true, false);
		}

		public virtual bool TrySwap(Piece theSelected, int theSwappedRow, int theSwappedCol)
		{
			return this.TrySwap(theSelected, theSwappedRow, theSwappedCol, false, true, false);
		}

		public virtual bool TrySwap(Piece theSelected, int theSwappedRow, int theSwappedCol, bool forceSwap, bool playerSwapped, bool destroyTarget)
		{
			return this.mQuestBoard.CallBaseTrySwap(theSelected, theSwappedRow, theSwappedCol, forceSwap, playerSwapped, destroyTarget);
		}

		public virtual bool CanPlay()
		{
			return true;
		}

		public virtual void LevelUp()
		{
		}

		public virtual bool GetTooltipText(Piece thePiece, ref string theHeader, ref string theBody)
		{
			return false;
		}

		public virtual bool IsGameIdle()
		{
			return this.mQuestBoard.CallBoardIsGameIdle();
		}

		public void GOAL_MSG(string theMsg)
		{
			if (this.mQuestBoard.mMessager != null)
			{
				this.mQuestBoard.mMessager.AddMessage(theMsg);
			}
		}

		public uint[,] mGridState = new uint[8, 8];

		public QuestBoard mQuestBoard;

		public Ref<int> mUpdateCnt = new Ref<int>(0);

		public bool mWantShowPoints;
	}
}
