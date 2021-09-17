using System;
using System.Collections.Generic;

namespace Bejeweled3
{
	internal class PowerUpPiecePair
	{
		public static PowerUpPiecePair GetNewPowerUpPiecePair(int matchCount, PieceBejLive piece)
		{
			if (PowerUpPiecePair.unusedObjects.Count > 0)
			{
				PowerUpPiecePair powerUpPiecePair = PowerUpPiecePair.unusedObjects.Pop();
				powerUpPiecePair.Reset(matchCount, piece);
				return powerUpPiecePair;
			}
			return new PowerUpPiecePair(matchCount, piece);
		}

		public void PrepareForReuse()
		{
			PowerUpPiecePair.unusedObjects.Push(this);
		}

		private PowerUpPiecePair(int matchCount, PieceBejLive piece)
		{
			this.Reset(matchCount, piece);
		}

		private void Reset(int matchCount, PieceBejLive piece)
		{
			this.Piece = piece;
			this.MatchCount = matchCount;
		}

		public PieceBejLive Piece;

		public int MatchCount;

		private static Stack<PowerUpPiecePair> unusedObjects = new Stack<PowerUpPiecePair>(100);
	}
}
