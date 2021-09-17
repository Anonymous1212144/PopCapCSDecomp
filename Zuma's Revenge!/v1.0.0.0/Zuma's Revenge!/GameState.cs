using System;

namespace ZumasRevenge
{
	public enum GameState
	{
		GameState_None,
		GameState_Playing,
		GameState_Losing,
		GameState_LevelUp,
		GameState_BossDead,
		GameState_LevelBegin,
		GameState_FinalBossPart1Finished,
		GameState_Boss6Transition,
		GameState_Boss6FakeCredits,
		GameState_Boss6StoneHeadBurst,
		GameState_Boss6DarkFrog,
		GameState_BossIntro,
		GameState_BeatLevelBonus,
		GameState_ScorePage
	}
}
