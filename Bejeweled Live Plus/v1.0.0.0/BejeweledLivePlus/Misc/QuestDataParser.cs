using System;
using System.Collections.Generic;
using SexyFramework;
using SexyFramework.Resource;

namespace BejeweledLivePlus.Misc
{
	public class QuestDataParser : DescParser
	{
		public override bool Error(string theError)
		{
			if (this.mError.Length == 0)
			{
				this.mError = theError;
				if (this.mCurrentLineNum > 0)
				{
					this.mError = this.mError + " on line " + this.mCurrentLineNum.ToString();
				}
			}
			return false;
		}

		public override bool HandleCommand(ListDataElement theParams)
		{
			string text = ((SingleDataElement)theParams.mElementVector[0]).mString.ToString();
			int count = theParams.mElementVector.Count;
			if (string.Compare(text, "Quest", 5) == 0)
			{
				if (count < 3)
				{
					return this.Error("Not enough params");
				}
				string text2 = base.Unquote(((SingleDataElement)theParams.mElementVector[1]).mString.ToString());
				QuestData questData = new QuestData();
				questData.mQuestName = text2;
				questData.mParams["Title"] = text2;
				if (count >= 3)
				{
					for (int i = 2; i < count; i++)
					{
						string text3 = base.Unquote(((SingleDataElement)theParams.mElementVector[i]).mString.ToString());
						string theQuotedString = ((SingleDataElement)((SingleDataElement)theParams.mElementVector[i]).mValue).mString.ToString();
						string text4 = base.Unquote(theQuotedString);
						questData.mParams[text3] = text4;
					}
				}
				if (questData.mParams.ContainsKey("TitleText"))
				{
					questData.mQuestName = questData.mParams["TitleText"];
				}
				else
				{
					questData.mParams["TitleText"] = text2;
				}
				this.mQuestDataVector.Add(questData);
				return true;
			}
			else
			{
				if (string.Compare(text, "QuickJumpQuestIdx", 5) == 0)
				{
					int mJumpToQuest = GlobalMembers.sexyatoi(base.Unquote(((SingleDataElement)theParams.mElementVector[1]).mString.ToString()));
					GlobalMembers.gApp.mJumpToQuest = mJumpToQuest;
					return true;
				}
				return this.Error("Unknown command: " + text);
			}
		}

		public List<QuestData> mQuestDataVector = new List<QuestData>();
	}
}
