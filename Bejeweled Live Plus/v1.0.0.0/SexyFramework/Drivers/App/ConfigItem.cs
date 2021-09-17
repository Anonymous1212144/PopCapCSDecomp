using System;
using SexyFramework.Misc;

namespace SexyFramework.Drivers.App
{
	public class ConfigItem
	{
		public ConfigItem()
		{
			this.name = string.Empty;
			this.type = ConfigType.String;
		}

		public virtual void load(Buffer buffer)
		{
			this.name = buffer.ReadString();
		}

		public virtual void save(Buffer buffer)
		{
			buffer.WriteInt32((int)this.type);
			buffer.WriteString(this.name);
		}

		public ConfigType type;

		public string name;
	}
}
