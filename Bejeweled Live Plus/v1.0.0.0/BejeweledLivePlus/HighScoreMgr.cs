using System;
using System.Collections.Generic;

namespace BejeweledLivePlus
{
	public class HighScoreMgr
	{
		public HighScoreMgr()
		{
			this.mNeedSave = false;
		}

		public bool Submit(string theTable, string theName, int theValue, int thePicture)
		{
			HighScoreTable orCreateTable = this.GetOrCreateTable(theTable);
			if (orCreateTable.Submit(theName, theValue, thePicture))
			{
				GlobalMembers.gApp.SaveHighscores();
				return true;
			}
			return false;
		}

		public HighScoreTable GetOrCreateTable(string theTable)
		{
			string highScoreTableId = GlobalMembers.gApp.GetHighScoreTableId(theTable);
			int num = 0;
			while (num < 7 && !(GlobalMembers.gApp.GetModeHeading((GameMode)num) == theTable))
			{
				num++;
			}
			HighScoreTable result = null;
			if (this.mHighScoreMap.TryGetValue(highScoreTableId, ref result))
			{
				return result;
			}
			this.mHighScoreMap.Add(highScoreTableId, new HighScoreTable(highScoreTableId));
			this.mHighScoreMap[highScoreTableId].mManager = this;
			this.mNeedSave = true;
			return this.mHighScoreMap[highScoreTableId];
		}

		public Dictionary<string, HighScoreTable> mHighScoreMap = new Dictionary<string, HighScoreTable>();

		public bool mNeedSave;
	}
}
