using System;
using System.Collections.Generic;
using SexyFramework;

namespace BejeweledLivePlus.Localization
{
	public class LocalizedString : ILocalizedStringProvider
	{
		public LocalizedString()
		{
			GlobalMembers.gApp.mPopLoc.LocalizedString = this;
			this.idstrMap_ = new Dictionary<int, string>();
		}

		public string fromID(int id)
		{
			if (!this.idstrMap_.ContainsKey(id))
			{
				this.idstrMap_[id] = string.Format("IDS_{0}", id);
			}
			return Strings.ResourceManager.GetString(this.idstrMap_[id], Strings.Culture);
		}

		private Dictionary<int, string> idstrMap_;
	}
}
