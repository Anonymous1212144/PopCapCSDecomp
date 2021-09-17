using System;
using SexyFramework.Misc;

namespace SexyFramework.Drivers.App
{
	public class ConfigItemString : ConfigItem
	{
		public ConfigItemString()
		{
			this.value = string.Empty;
			this.type = ConfigType.String;
		}

		public override void load(Buffer buffer)
		{
			base.load(buffer);
			this.value = buffer.ReadString();
		}

		public override void save(Buffer buffer)
		{
			base.save(buffer);
			buffer.WriteString(this.value);
		}

		public string value;
	}
}
